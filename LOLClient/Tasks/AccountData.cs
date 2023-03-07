using LOLClient.Connections;
using LOLClient.DataFiles;
using LOLClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LOLClient.Tasks;

public class AccountData
{
    // The Connection instance used to make HTTP requests to the League API.
    private readonly Connection _leagueConnection;

    // Object used as a lock for thread-safety.
    private readonly object _lock = new();

    // Constructor that initializes the _leagueConnection field with the provided Connection instance.
    public AccountData(Connection leagueConnection)
    {
        _leagueConnection = leagueConnection;
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
                LogToFile($"Champions from Request: \n{jsonResponse}");
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
            }

            // Wait 5 seconds before retrying the request.
            Thread.Sleep(5000);
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
    public async Task GetChampionsAsync(Account account)
    {
        // Retrieve the list of owned champions using the GetOwnedChampionsAsync method.
        var ownedChamps = await GetOwnedChampionsAsync();

        // Initialize an empty list of Champion objects.
        var champs = new List<Champion>();

        // Retrieve the local JSON data for champions and match them up with the owned champions.
        string filePath = $"{Config.ChampionsFile}";
        string content = File.ReadAllText(filePath);
        var localChampJsonData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
        LogToFile($"Champions:\n {content}");


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

        account.Champions = champs;
    }

    private void LogToFile(string message)
    {
        lock (_lock)
        {
            string logFilePath = @"..\..\..\log.txt";

            File.AppendAllTextAsync(logFilePath, $"{DateTime.Now} - {message}\n\n\n\n\n").Wait();

        }
    }

    // Method that asynchronously retrieves the summoner data for the account and assigns it to the account object.
    public async Task GetSummonerDataAsync(Account account)
    {
        // Retrieve the summoner name and level using the RequestSummonerNameAndLevelAsync method.
        var levelAndName = await RequestSummonerNameAndLevelAsync();

        // Assign the summoner level and name to the account object.
        account.Level = levelAndName.Item1;
        account.SummonerName = levelAndName.Item2;

        // Retrieve the email verification status using the RequestEmailVerificationAsync method.
        account.IsEmailVerified = await RequestEmailVerificationAsync();

        // Retrieve the loot data using the RequestLootAsync method and assign it to the account object.
        await RequestLootAsync(account);
    }

    // Method that asynchronously requests the account loot.
    private async Task RequestLootAsync(Account account)
    {
        // Make a GET request to retrieve player loot data.
        var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-loot/v1/player-loot", null);

        // Parse the response content as a JSON array.
        var data = JArray.Parse(await response.Content.ReadAsStringAsync());

        // Log the retrieved loot data to a file.
        LogToFile($"Loot:\n {data}");

        // Loop through each item in the loot data.
        foreach (var item in data)
        {
            // If the item is a currency for champions, update the BE count in the account object.
            if (item["lootId"].ToString() == "CURRENCY_champion")
                account.BE = item["count"].ToString();

            // If the item is a currency for RP, update the RP count in the account object.
            if (item["lootId"].ToString() == "CURRENCY_RP")
                account.RP = item["count"].ToString();

            // If the item is a currency for cosmetic items, update the OE count in the account object.
            if (item["lootId"].ToString() == "CURRENCY_cosmetic")
                account.OE = item["count"].ToString();

            // If the item is a hextech skin rental, add the skin information to the HextechSkins list in the account object.
            if (item != null && item["lootId"].ToString().StartsWith("CHAMPION_SKIN_RENTAL_"))
            {
                var champion = item["itemDesc"].ToString();
                if (!string.IsNullOrEmpty(champion))
                {
                    account.HextechSkins.Add(new Skin()
                    {
                        Name = champion
                    });
                }
            }
        }
    }

    // Returns a tuple of the current summoner's name and level in the order <Level, Name>.
    private async Task<Tuple<string, string>> RequestSummonerNameAndLevelAsync()
    {
        // Make an HTTP GET request to the "/lol-summoner/v1/current-summoner" endpoint using the _leagueConnection object.
        var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-summoner/v1/current-summoner", null);

        // Parse the response content as a JSON string and create a JToken object from it.
        var data = JToken.Parse(await response.Content.ReadAsStringAsync());

        // Log the current summoner data to a file using the LogToFile method.
        LogToFile($"Current Summoner:\n {data}");

        // Create a new Tuple object containing the summoner's level and name as strings.
        return new Tuple<string, string>(data["summonerLevel"].ToString(), data["displayName"].ToString());
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
        catch (Exception e)
        {
            // If an exception occurs, return null to indicate that the verification status is unknown.
            return false;
        }

        // If the email has not been verified or the response status code is not OK, return false to indicate that the email has not been verified.
        return false;
    }


    // This method exports an Account object to a JSON file.
    // The file is saved to the Exports folder with the file name "{summonerName}.json".
    public async Task ExportAccount(Account account)
    {

        // Build the file path for the JSON file based on the account's summoner name and the ExportsFolder directory.
        string filePath = $@"{Config.ExportsFolder}{account.SummonerName.Trim()}.json";
        Console.WriteLine($"Saved to {filePath}");
        // Create the ExportsFolder directory if it doesn't exist.
        if (!Directory.Exists(Config.ExportsFolder))
            Directory.CreateDirectory(Config.ExportsFolder);

        // Create the JSON file if it doesn't exist and immediately dispose of the file stream to release the resources.
        if (!File.Exists(filePath))
            File.Create(filePath).Dispose();

        // Serialize the Account object to a formatted JSON string.
        string json = JsonConvert.SerializeObject(account, Formatting.Indented);

        // Write the JSON string to the file.
        await File.WriteAllTextAsync(filePath, json);

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
                    LogToFile($"Skins:\n {jsonSkins}");
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
    // It takes the specific account as an argument
    public async Task GetSkinsAsync(Account account)
    {
        // Initialize a list to store the owned skins
        var ownedSkins = await GetOwnedSkinsAsync();

        // Load the skin data from file
        string filePath = $"{Config.SkinsFile}";
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
        account.Skins = skins;
    }
    public void GetRank()
    {

    }
    private async Task RequestRankAsync()
    {

        var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-ranked/v1/current-ranked-stats", null);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var data = JToken.Parse(await response.Content.ReadAsStringAsync());

            var verified = data["emailVerified"].ToString();
        }

    }

}
