using AccountChecker.Data;
using AccountChecker.Models;
using AccountChecker.Connections;
using AccountChecker.DataFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AccountChecker.Tasks;

public class AccountData
{
    // The Connection instance used to make HTTP requests to the League API.
    private readonly LeagueConnection _leagueConnection;

    // Object used to perform and persists the tasks on the specific account
    private Account _account;

    // Object used as a lock for thread-safety.
    private readonly object _lock = new();

    // The Loot instance to perform tasks on the loot data
    private readonly Loot _loot;

    private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    // Constructor that initializes the _leagueConnection field with the provided Connection instance.
    // Needs the Connection instance used for the LeagueConnection instance.
    public AccountData(LeagueConnection leagueConnection, Account account)
    {
        lock (_lock)
        {
            _loot = new Loot(leagueConnection);
            _account = account;
            _leagueConnection = leagueConnection;

        }
    }

    // Method that asynchronously makes a GET request to the League API's "/lol-champions/v1/owned-champions-minimal" endpoint
    // to retrieve the list of champions that the account owns in minimal form.
    private async Task<JArray> RequestChampionsAsync()
    {
        // Loop indefinitely until a successful response is received.
        while (true)
        {
            // Make the GET request to the API.
            var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-champions/v1/owned-champions-minimal", null);

            // If the response is successful, parse the JSON content and return it as a JArray.
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JArray.Parse(await response.Content.ReadAsStringAsync());
                //await LogToFile($"Champions from Request: \n{jsonResponse}");
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
            }

            // Wait 5 seconds before retrying the request.
            Thread.Sleep(3000);
        }
    }

    // Method that asynchronously retrieves the list of champions owned by the account.
    private async Task<List<JToken>> GetOwnedChampionsAsync()
    {
        var ownedChamps = new List<JToken>();

        // Retrieve the list of champions using the RequestChampionsAsync method.
        var champs = await RequestChampionsAsync();

        // Filter the champions to only include those that are owned by the account.
        foreach (var champ in champs)
        {
            string champName = (string)champ["name"];

            if (champName != null && (bool)champ["ownership"]["owned"] == true)
            {
                ownedChamps.Add(champ);
            }
        }

        return ownedChamps;
    }

    // Method that asynchronously retrieves the account's champions and assigns them to the account object.
    public async Task GetChampionsAsync()
    {
        // Retrieve the list of owned champions using the GetOwnedChampionsAsync method.
        var ownedChamps = await GetOwnedChampionsAsync();

        // Initialize an empty list of Champion objects.
        var champs = new List<Champion>();

        // Retrieve the local JSON data for champions and match them up with the owned champions.
        string filePath = $"{PathConfig.ChampionsFile}";
        string content = File.ReadAllText(filePath);
        var localChampJsonData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
        //await LogToFile($"Champions:\n {content}");


        // Fix to avoid n*m 
        foreach (var localChamp in localChampJsonData)
        {
            foreach (var ownedChamp in ownedChamps)
            {
                if (ownedChamp["name"].ToString() == localChamp["name"].ToString())
                {
                    Champion champion = new()
                    {
                        Name = (string)ownedChamp["name"],
                        ID = (string)ownedChamp["id"],
                        SquarePortraitPath = (string)localChamp["squarePortraitPath"],
                        PurchaseDate = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(ownedChamp["purchased"])).LocalDateTime,

                    };
                    champs.Add(champion);
                }
            }
        }

        // Assign the list of Champion objects to the account

        _account.Champions = champs;
    }

    // Method that asynchronously retrieves the summoner data for the account and assigns it to the account object.
    public async Task GetSummonerDataAsync()
    {
        // Retrieve the summoner name and level using the RequestSummonerNameAndLevelAsync method.
        var levelAndName = await RequestSummonerNameAndLevelAsync();

        // Assign the summoner level and name to the account object.
        _account.Level = levelAndName.Item1;
        _account.SummonerName = levelAndName.Item2;

        // Retrieve the email verification status using the RequestEmailVerificationAsync method.
        _account.IsEmailVerified = await RequestEmailVerificationAsync();
    }

    // This method gets the loot data from the Loot instance
    public async Task<Loot> GetLootAsync()
    {

        await _loot.RefreshLootAsync();

        // Loop through each item in the loot data.
        foreach (var item in _loot.Data)
        {
            // If the item is a currency for champions, update the BE count in the account object.
            if (item["lootId"].ToString() == "CURRENCY_champion")
                _account.BE = item["count"].ToString();

            // If the item is a currency for RP, update the RP count in the account object.
            if (item["lootId"].ToString() == "CURRENCY_RP")
                _account.RP = item["count"].ToString();

            // If the item is a currency for cosmetic items, update the OE count in the account object.
            if (item["lootId"].ToString() == "CURRENCY_cosmetic")
                _account.OE = item["count"].ToString();

            // If the item is a hextech skin rental, add the skin information to the HextechSkins list in the account object.
            if (item != null && item["lootId"].ToString().StartsWith("CHAMPION_SKIN_RENTAL_"))
            {
                var champion = item["itemDesc"].ToString();
                if (!string.IsNullOrEmpty(champion))
                {
                    _account.HextechSkins.Add(new Skin()
                    {
                        Name = champion
                    });
                }
            }
        }

        return _loot;
    }

    // Returns a tuple of the current summoner's name and level in the order <Level, Name>.
    private async Task<Tuple<string, string>> RequestSummonerNameAndLevelAsync()
    {
        // Make an HTTP GET request to the "/lol-summoner/v1/current-summoner" endpoint using the _leagueConnection object.
        var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-summoner/v1/current-summoner", null);

        // Parse the response content as a JSON string and create a JToken object from it.
        var data = JToken.Parse(await response.Content.ReadAsStringAsync());

        string sumName = "{Invalid Name}";
        if (data["displayName"].ToString() != "")
        {
            sumName = data["displayName"].ToString();
        }

        // Create a new Tuple object containing the summoner's level and name as strings.
        return new Tuple<string, string>(data["summonerLevel"].ToString(), sumName);
    }


    // This asynchronous method checks if the current summoner's email address has been verified.
    private async Task<bool> RequestEmailVerificationAsync()
    {
        try
        {
            // Make an HTTP GET request to the "/lol-email-verification/v1/email" endpoint using the _leagueConnection object.
            var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-email-verification/v1/email", null);

            // If the response status code is OK, parse the response content as a JSON string and create a JToken object from it.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JToken.Parse(await response.Content.ReadAsStringAsync());

                // Get the value of the "emailVerified" property from the JToken object and convert it to a string.
                var verified = data["emailVerified"].ToString();

                // If the value is "true", return true to indicate that the email has been verified.
                if (verified.ToLower() == "true")
                    return true;
            }
        }
        catch
        {
            // If an exception occurs, return null to indicate that the verification status is unknown.
            return false;
        }

        // If the email has not been verified or the response status code is not OK, return false to indicate that the email has not been verified.
        return false;
    }

    // This asynchronous method requests a list of all the skins owned by the current summoner.
    // If the request fails or returns an empty array, it will retry every 5 seconds until it succeeds.
    private async Task<JArray> RequestSkinsAsync()
    {
        // Keep retrying until the request succeeds.
        while (true)
        {
            // Make an HTTP GET request to the "/lol-inventory/v2/inventory/CHAMPION_SKIN" endpoint using the _leagueConnection object.
            var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-inventory/v2/inventory/CHAMPION_SKIN", null);

            // Read the response content as a string.
            var skinsResult = await response.Content.ReadAsStringAsync();

            // If the response is successful, parse the response content as a JSON array and return it.
            if (response.IsSuccessStatusCode)
            {
                var jsonSkins = JArray.Parse(skinsResult);

                // If the parsed JSON array is not null, log it to a file and return it.
                if (jsonSkins != null)
                {
                    //await LogToFile($"Skins:\n {jsonSkins}");
                    return jsonSkins;
                }
            }

            // If the request fails or returns an empty array, wait for 5 seconds and try again.
            Thread.Sleep(5000);
        }

    }

    // This asynchronous method returns a JSON array of all the skins that the current summoner owns.
    private async Task<JArray> GetOwnedSkinsAsync()
    {
        // Call the RequestSkinsAsync() method to get a list of all the skins.
        var skins = await RequestSkinsAsync();

        // Create a new JSON array to hold the owned skins.
        var ownedSkins = new JArray();

        // Loop through each skin in the skins list and check if it is owned.
        foreach (var skin in skins)
        {
            // Get the name of the skin and check if it is not null and the ownershipType is "OWNED".
            string skinName = (string)skin["itemId"];
            if (skinName != null && (string)skin["ownershipType"] == "OWNED")
            {
                // If the skin is owned, add it to the ownedSkins array.
                ownedSkins.Add(skin);
            }
        }

        // Return the JSON array of owned skins.
        return ownedSkins;
    }

    // This method gets the owned skins for a League of Legends account and populates the account object with the skin data.
    public async Task GetSkinsAsync()
    {
        // Initialize a list to store the owned skins
        var ownedSkins = await GetOwnedSkinsAsync();

        // Load the skin data from file
        string filePath = $"{PathConfig.SkinsFile}";
        string content = File.ReadAllText(filePath);
        var skinJsonData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(content);

        // Initialize a list to store the matched skins
        var skins = new List<Skin>();

        // Loop through each skin in the skin data and check if it matches an owned skin
        foreach (var jsonSkin in skinJsonData)
        {
            foreach (var ownedSkin in ownedSkins)
            {
                if ((string)ownedSkin["itemId"] == jsonSkin.Key)
                {
                    // Create a new Skin object and add it to the list of matched skins
                    Skin skin = new Skin()
                    {
                        ID = jsonSkin.Key,
                        Name = Convert.ToString(jsonSkin.Value["name"]),
                        PurchaseDate = DateTimeOffset.ParseExact(Convert.ToString(ownedSkin["purchaseDate"]), "yyyyMMdd'T'HHmmss.fff'Z'", null).LocalDateTime,
                    };
                    skins.Add(skin);
                }
            }
        }

        // Set the skins list of the account
        _account.Skins = skins;
    }
    
    // This method grabs the account rank data and populated the account object's ranked stats.
    public async Task GetRank()
    {
        var data = await RequestRankAsync();

        if (data == null)
            return;

        _account.CurrentRank = new Rank()
        {
            Tier = data["queueMap"]["RANKED_SOLO_5x5"]["tier"].ToString(),
            Division = data["queueMap"]["RANKED_SOLO_5x5"]["division"].ToString(),
            Wins = Convert.ToInt32(data["queueMap"]["RANKED_SOLO_5x5"]["wins"]),
            Losses = Convert.ToInt32(data["queueMap"]["RANKED_SOLO_5x5"]["losses"]),
            LeaguePoints = Convert.ToInt32(data["queueMap"]["RANKED_SOLO_5x5"]["leaguePoints"].ToString())
        };

        _account.HighestRank = new Rank()
        {
            Tier = data["queueMap"]["RANKED_SOLO_5x5"]["highestTier"].ToString(),
            Division = data["queueMap"]["RANKED_SOLO_5x5"]["highestDivision"].ToString(),
            Wins = 0,
            Losses = 0,
            LeaguePoints = 0
        };
    }

    // This method makes a get reqeust to the league client to retrieve account stats
    private async Task<JToken> RequestRankAsync()
    {

        var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-ranked/v1/current-ranked-stats", null);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var data = JToken.Parse(await response.Content.ReadAsStringAsync());

            //await LogToFile($"\n\nRANKED DATA :  {data["queueMap"]["RANKED_SOLO_5x5"]}");
            return data;
        }

        return null;
    }

    // This method grabs the Queue Statistics and populates the account object's queue stats.
    public async Task GetQueueStats()
    {
        var data = await RequestQueueStatsAsync();

        if (data == null)
            return;

        if (data["lowPriorityData"]["penaltyTime"].ToString() != "0.0")
            _account.LowPriorityQueue = true;
    }

    // Makes a request to retrieve queue statistics
    private async Task<JToken> RequestQueueStatsAsync()
    {
        await _leagueConnection.RequestAsync(HttpMethod.Post,
                                       "/lol-lobby/v2/lobby",
                                       new Dictionary<string, object>
                                       {
                                           {"queueId", 430}
                                       });
        Thread.Sleep(1000);

        await _leagueConnection.RequestAsync(HttpMethod.Post, "/lol-lobby/v2/lobby/matchmaking/search", null);

        Thread.Sleep(2000);

        var queueStatsResponse = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-lobby/v2/lobby/matchmaking/search-state", null);

        if (queueStatsResponse.StatusCode == HttpStatusCode.OK)
        {
            var data = JToken.Parse(await queueStatsResponse.Content.ReadAsStringAsync());

            return data;
        }

        return null;
    }

}
