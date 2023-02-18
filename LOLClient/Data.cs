using LOLClient.Connections;
using LOLClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LOLClient;

public class Data
{

    private readonly Connection _leagueConnection;

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

        string filePath = @"..\..\..\Updates\champions.json";
        string content = File.ReadAllText(filePath);
        var localChampJsonData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);

        // Fix to avoid n*m time complexity
        foreach (var localChamp in localChampJsonData)
        {
            foreach (var ownedChamp in ownedChamps)
            {
                if (ownedChamp["name"].ToString() == localChamp["name"].ToString())
                {
                    Champion champion = new()
                    {
                        Name = (string)ownedChamp["name"],
                        ID = (string)ownedChamp["id"]

                    };
                    champs.Add(champion);
                }
            }
        }

        account.Champions = champs;
    }

    private dynamic RequestSkins()
    {
        while (true)
        {
            var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-inventory/v2/inventory/CHAMPION_SKIN", null).Result;

            var skinsResult = response.Content.ReadAsStringAsync().Result;

            var jsonSkins = JsonConvert.DeserializeObject<dynamic>(skinsResult);

            if (response.IsSuccessStatusCode)
            {
                if (jsonSkins != null)
                {
                    return jsonSkins;
                }
            }

            Thread.Sleep(5000);
        }

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

        foreach (var item in data)
        {
            if (item["lootId"].ToString() == "CURRENCY_champion")
                account.BE = item["count"].ToString();

            if (item["lootId"].ToString() == "CURRENCY_cosmetic")
                account.RP = item["count"].ToString();
        }
    }

    // Returns tuple of Summoner Name and Level in the Order: <Level, Name>
    private Tuple<string, string> RequestSummonerNameAndLevel()
    {
        var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-summoner/v1/current-summoner", null).Result;

        var data = JToken.Parse(response.Content.ReadAsStringAsync().Result);

        return new Tuple<string, string>(data["summonerLevel"].ToString(), data["displayName"].ToString());
    }

    private bool RequestEmailVerification()
    {
        var response = _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-email-verification/v1/email", null).Result;

        var data = JToken.Parse(response.Content.ReadAsStringAsync().Result);

        var verified = data["emailVerified"].ToString();

        if (verified.ToLower() == "true")
            return true;

        return false;
    }

    public void ExportAccount(Account account)
    {

        string filePath = $@"..\..\..\Exports\{account.SummonerName.Trim()}.json";

        if (!File.Exists(filePath))
            File.Create(filePath).Dispose();

        string json = JsonConvert.SerializeObject(account, Formatting.Indented);

        File.WriteAllText(filePath, json);

    }

    private List<string> GetOwnedSkins()
    {

        var skins = RequestSkins();
        var ownedSkinIds = new List<string>();

        foreach (var skin in skins)
        {
            string skinName = skin["itemId"];
            if (skinName != null && skin["ownershipType"] == "OWNED")
            {
                ownedSkinIds.Add(skinName);
            }
        }

        return ownedSkinIds;
    }

    public void GetSkins(Account account)
    {
        var skins = new List<Skin>();
        var ownedSkinsIds = GetOwnedSkins();
        string filePath = @"..\..\..\Updates\skins.json";
        string content = File.ReadAllText(filePath);
        var skinJsonData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(content);

        foreach (var jsonSkin in skinJsonData)
        {
            if (ownedSkinsIds.Contains(jsonSkin.Key))
            {
                Skin skin = new()
                {
                    ID = jsonSkin.Key,
                    Name = Convert.ToString(jsonSkin.Value["name"])
                };
                skins.Add(skin);
            }
        }

        account.Skins = skins;
    }

}
