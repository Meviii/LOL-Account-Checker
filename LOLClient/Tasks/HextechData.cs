using AccountChecker.Data;
using LOLClient.Connections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
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
    public async void CraftKeys() {
        await _loot.RefreshLoot();

        var keyFragmentCount = _loot.GetLootCountByID("MATERIAL_key_fragment");

        if (keyFragmentCount >= 3)
        {
            var craftableKeys = Math.Floor(Convert.ToDouble(keyFragmentCount / 3));

            await PostRecipe("MATERIAL_key_fragment_forge", new List<string> { "MATERIAL_key_fragment" }, Convert.ToInt32(craftableKeys));
        }
    }
    public async void OpenChests()
    {
        await _loot.RefreshLoot();

        var masterWorkChestCount = _loot.GetLootCountByID("CHEST_224");
        var standardChestCount = _loot.GetLootCountByID("CHEST_generic");
        var masteryChestCount = _loot.GetLootCountByID("CHEST_champion_mastery");
        var keyCount = _loot.GetLootCountByID("MATERIAL_key");

        if ((masterWorkChestCount + standardChestCount + masteryChestCount) == 0 || keyCount == 0)
            return;

        int repeat;
        while (keyCount > 0)
        {
            if (masterWorkChestCount > 0)
            {
                repeat = Math.Min(keyCount, masterWorkChestCount);
                await PostRecipe("CHEST_224_OPEN", new List<string> {"CHEST_224", "MATERIAL_key" }, repeat);
                keyCount -= repeat;
                continue;
            }

            if (standardChestCount > 0)
            {
                repeat = Math.Min(keyCount, standardChestCount);
                await PostRecipe("CHEST_generic_OPEN", new List<string> { "CHEST_generic",  "MATERIAL_key" }, repeat);
                keyCount -= standardChestCount;
                continue;
            }

            if (masteryChestCount > 0)
            {
                repeat = Math.Min(keyCount, masteryChestCount);
                await PostRecipe("CHEST_champion_mastery_OPEN", new List<string> {"CHEST_champion_mastery", "MATERIAL_key" }, repeat);
                keyCount -= masteryChestCount;
                continue;
            }
        }

        OpenChests();
    }

    public async void OpenLoot()
    {
        await _loot.RefreshLoot();
        string notHextechChest = "CHEST_((?!(224|generic|champion_mastery)).)*";
        var allLoot = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(_loot.Data.ToString());

        List<Dictionary<string, object>> lootToOpen = allLoot.FindAll(currentLoot =>
            Regex.IsMatch(currentLoot["lootId"].ToString(), notHextechChest)
        );

        foreach (Dictionary<string, object> currentLoot in lootToOpen)
        {
            string name = currentLoot["lootName"].ToString();
            int count = Convert.ToInt32(currentLoot["count"]);
            await PostRecipe($"{name}_OPEN", new List<string> { name }, count);
            Thread.Sleep(1000);
        }
    }
}
