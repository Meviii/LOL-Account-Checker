using LOLClient.Connections;
using LOLClient.Tasks;
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

public class Loot
{
    private readonly Connection _leagueConnection;
    public JArray Data;
    private static readonly object _lock = new();
    public Loot(Connection connection)
    {
        lock (_lock)
        {
            _leagueConnection = connection;
        }
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

        
        //lock (_lock)
        //{
            
        //    string logFilePath = @"..\..\..\logDATALOOT.txt";

        //    File.AppendAllTextAsync(logFilePath, $"{DateTime.Now} - STATUS CODE: {response.StatusCode} -\n\n {Data}\n\n\n\n\n").Wait();
        //}
        

        Thread.Sleep(1000);
    }
}
