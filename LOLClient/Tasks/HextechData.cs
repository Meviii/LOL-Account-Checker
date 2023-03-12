using AccountChecker.Data;
using LOLClient.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LOLClient.Tasks;

public class HextechData
{
    private readonly LeagueConnection _connection;
    private readonly Loot _loot;
    private static readonly object _lock = new();
    public HextechData(LeagueConnection connection, Loot loot)
    {
        lock (_lock)
        {
            _loot = loot;
            _connection = connection;
        }
    }

    public async Task PostRecipe(string recipeName,List<string> materials, int repeat = 1)
    {
        await _connection.RequestAsync(HttpMethod.Post,
                                       $"/lol-loot/v1/recipes/{recipeName}/craft?repeat={Convert.ToInt32(repeat)}",
                                       materials, true);
    }

    public async void DisenchantEternalShards()
    {
        var eternals = _loot.GetLootByDisplayCategory("ETERNALS");
        foreach (var eternal in eternals)
        {
            var materials = new List<string>() { eternal["lootName"].ToString() };

            await PostRecipe($"{eternal["type"]}_DISENCHANT", materials, Convert.ToInt32(eternal["count"].ToString()));

            Thread.Sleep(1000);
        }
    }

    public async void DisenchantChampionShards()
    {
        var champions = _loot.GetLootByDisplayCategory("CHAMPION");
        foreach (var champ in champions)
        {
            var materials = new List<string>() { champ["lootName"].ToString() };

            await PostRecipe($"{champ["type"]}_disenchant", materials, Convert.ToInt32(champ["count"].ToString()));

            Thread.Sleep(1000);
        }
    }

    public async void DisenchantWardSkinShards()
    {
        var champions = _loot.GetLootByDisplayCategory("WARDSKIN");
        foreach (var champ in champions)
        {
            var materials = new List<string>() { champ["lootName"].ToString() };

            await PostRecipe($"{champ["type"]}_disenchant", materials, Convert.ToInt32(champ["count"].ToString()));

            Thread.Sleep(1000);
        }
    }
    public async void DisenchantSkinShards()
    {
        var skins = _loot.GetLootByDisplayCategory("SKIN");
        foreach (var sk in skins)
        {
            var materials = new List<string>() { sk["lootName"].ToString() };
            
            await PostRecipe($"{sk["type"]}_disenchant", materials,Convert.ToInt32(sk["count"].ToString()));

            Thread.Sleep(1000);
        }
    }
}
