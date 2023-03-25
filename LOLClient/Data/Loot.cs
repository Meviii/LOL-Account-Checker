using AccountChecker.Connections;
using AccountChecker.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker.Data;

// This class represents a player's loot collection in League of Legends.
public class Loot
{
    private readonly Connection _leagueConnection;
    public JArray Data;
    private static readonly object _lock = new();

    // Constructor to initialize the connection and data.
    public Loot(Connection connection)
    {
        lock (_lock)
        {
            _leagueConnection = connection;
        }
    }

    // Returns a list of loot items based on the given display category.
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

    // Refreshes the player's loot data by making a GET request to the League of Legends API.
    public async Task RefreshLootAsync(int timeout = 4)
    {
        while (timeout > 0)
        {
            var response = await _leagueConnection.RequestAsync(HttpMethod.Get, "/lol-loot/v1/player-loot", null);

            if (response.IsSuccessStatusCode)
            {
                Data = JArray.Parse(await response.Content.ReadAsStringAsync());
            }
            // Sleeps for 1 second to avoid making too many requests to the API.
            Thread.Sleep(500);
            timeout--;
        }
    }

    // Returns the count of a particular loot item based on the given loot ID.
    public int GetLootCountByID(string lootId)
    {
        var lootCount = 0;

        foreach (var item in Data)
        {
            if (item["lootId"].ToString() == lootId)
            {
                lootCount += Convert.ToInt32(item["count"].ToString());
            }
        }

        return lootCount;
    }
}