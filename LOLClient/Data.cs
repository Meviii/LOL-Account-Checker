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


namespace LOLClient;

public class Data
{

    private readonly Connection _leagueConnection;
    private readonly object _lock = new();

    public Data(Connection leagueConnection)
    {
        _leagueConnection = leagueConnection;
    }

    private JArray RequestChampions()
    {
        while (true)
        {
            var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-champions/v1/owned-champions-minimal", null).Result;
            
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                LogToFile($"Champions from Request: \n{jsonResponse}");
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
            }

            Thread.Sleep(5000);
        }
    }

    private List<JToken> GetOwnedChampions()
    {

        var ownedChamps = new List<JToken>();
        var champs = RequestChampions();

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

    public void GetChampions(Account account)
    {
        var ownedChamps = GetOwnedChampions();

        var champs = new List<Champion>();

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
                        PurchaseDate = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(ownedChamp["purchased"])).LocalDateTime,

                    };
                    champs.Add(champion);
                }
            }
        }

        account.Champions = champs;
    }

    private void LogToFile(string message)
    {
        string logFilePath = @"..\..\..\log.txt";

        File.AppendAllTextAsync(logFilePath, $"{DateTime.Now} - {message}\n\n\n\n\n").Wait();
    }

    public void GetSummonerData(Account account)
    {

        var levelAndName = RequestSummonerNameAndLevel();
        account.Level = levelAndName.Item1;
        account.SummonerName = levelAndName.Item2;
        account.IsEmailVerified = RequestEmailVerification();

        RequestLoot(account);
    }

    private void RequestLoot(Account account)
    {
        var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-loot/v1/player-loot", null).Result;

        var data = JArray.Parse(response.Content.ReadAsStringAsync().Result);
        
        LogToFile($"Loot:\n {data}");
        
        foreach (var item in data)
        {
            if (item["lootId"].ToString() == "CURRENCY_champion")
                account.BE = item["count"].ToString();

            if (item["lootId"].ToString() == "CURRENCY_RP")
                account.RP = item["count"].ToString();

            if (item["lootId"].ToString() == "CURRENCY_cosmetic")
                account.OE = item["count"].ToString();
        }
    }

    // Returns tuple of Summoner Name and Level in the Order: <Level, Name>
    private Tuple<string, string> RequestSummonerNameAndLevel()
    {
        var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-summoner/v1/current-summoner", null).Result;

        var data = JToken.Parse(response.Content.ReadAsStringAsync().Result);
        LogToFile($"Current Summoner:\n {data}");
        return new Tuple<string, string>(data["summonerLevel"].ToString(), data["displayName"].ToString());
    }

    private bool RequestEmailVerification()
    {
        try
        {
            var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-email-verification/v1/email", null).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JToken.Parse(response.Content.ReadAsStringAsync().Result);

                var verified = data["emailVerified"].ToString();

                if (verified.ToLower() == "true")
                    return true;
            }
        }
        catch (Exception e)
        {
            return true; // better to return null
        }
        

        return false;
    }

    public void ExportAccount(Account account)
    {

        string filePath = $@"{Config.ExportsFolder}{account.SummonerName.Trim()}.json";

        if (!File.Exists(filePath))
            File.Create(filePath).Dispose();

        string json = JsonConvert.SerializeObject(account, Formatting.Indented);

        File.WriteAllText(filePath, json);

    }


    private JArray RequestSkins()
    {
        while (true)
        {
            var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-inventory/v2/inventory/CHAMPION_SKIN", null).Result;

            var skinsResult = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonSkins = JArray.Parse(skinsResult);

                if (jsonSkins != null)
                {
                    LogToFile($"Skins:\n {jsonSkins}");
                    return jsonSkins;
                }
            }

            Thread.Sleep(5000);
        }

    }

    private JArray GetOwnedSkins()
    {

        var skins = RequestSkins();
        var ownedSkins = new JArray();

        foreach (var skin in skins)
        {
            string skinName = (string) skin["itemId"];
            if (skinName != null && (string)skin["ownershipType"] == "OWNED")
            {
                ownedSkins.Add(skin);
            }
        }

        return ownedSkins;
    }

    public void GetSkins(Account account)
    {
        var skins = new List<Skin>();
        var ownedSkins = GetOwnedSkins();
        string filePath = $"{Config.SkinsFile}";
        string content = File.ReadAllText(filePath);
        var skinJsonData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(content);

        
        foreach (var jsonSkin in skinJsonData)
        {
            foreach (var ownedSkin in ownedSkins)
            {
                if ((string) ownedSkin["itemId"] == (jsonSkin.Key))
                {
                    Skin skin = new()
                    {
                        ID = jsonSkin.Key,
                        Name = Convert.ToString(jsonSkin.Value["name"]),
                        PurchaseDate = DateTimeOffset.ParseExact(Convert.ToString(ownedSkin["purchaseDate"]), "yyyyMMdd'T'HHmmss.fff'Z'", null).LocalDateTime,
                    };
                    skins.Add(skin);
                }
            }
        }

        account.Skins = skins;
    }


    public void GetRank()
    {

    }

    private void RequestRank()
    {
        
        var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-ranked/v1/current-ranked-stats", null).Result;

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var data = JToken.Parse(response.Content.ReadAsStringAsync().Result);

            var verified = data["emailVerified"].ToString();
        }
        
    }

}
