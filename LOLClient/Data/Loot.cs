using LOLClient.Connections;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountChecker.Data;

public class Loot
{
    private readonly Connection _leagueConnection;

    public JArray Data;

    public Loot(Connection connection)
    {
        _leagueConnection = connection;
        RefreshLoot().Wait();
    }


    public List<JToken> GetLootByDisplayCategory(string category)
    {
        List<JToken> items = new();
        foreach (var item in Data)
        {
            if (item["displayCategories"].ToString() == category)
                items.Add(item);
        }

        return items;
    }

    public async Task RefreshLoot()
    {
        // Make a GET request to retrieve player loot data.
        var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-loot/v1/player-loot", null);

        // Parse the response content as a JSON array.
        Data = JArray.Parse(await response.Content.ReadAsStringAsync());
        
    }

}
