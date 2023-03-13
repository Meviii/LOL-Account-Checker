using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LOLClient.Models;
using Newtonsoft.Json.Linq;
using LOLClient.Connections;
using LOLClient.Utility;
using LOLClient.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using AccountChecker.Data;
using System.Reflection.Metadata.Ecma335;

namespace LOLClient;

/*
 * This class is the main executor for running an account session.
 * It is responsible for connecting, creating and retreiving data from
 * the league and riot client services.
 */
public class Runner
{

    private readonly Client _client;
    private Dictionary<string, object> _riotClientCredentials;
    private readonly Account _account;
    private AccountData _data;
    private string _region;
    private readonly CoreUtility _coreUtility;
    private Loot _loot;
    private HextechData _hextech;
    private EventData _eventData;
    private static readonly object _lock = new();

    public Runner()
    {
        lock (_lock)
        {
            _coreUtility = new CoreUtility();
            _account = new();
            _client = new();
        }
    }

    // This method takes in the username, password, and settings JObject as parameters
    // and runs the RiotClientRunner and LeagueClientRunner methods without executing Tasks(hextech, event).
    public async Task Job_AccountFetchingWithoutTasks(string username, string password, JObject settings)
    {
        // Run RiotClientRunner with the given username, password, and RiotClientPath
        bool didRiotSucceed = await RiotClientRunner(username, password, settings["RiotClientPath"].ToString());

        // If RiotClientRunner did not succeed, return from the method
        if (!didRiotSucceed)
        {
            CleanUp();
            return;
        }

        // Run LeagueClientRunner with the given LeagueClientPath
        bool didLeagueSucceed = await LeagueClientRunner(settings["LeagueClientPath"].ToString());

        // If LeagueClientRunner did not succeed, return from the method
        if (!didLeagueSucceed)
        {
            CleanUp();
            return;
        }

        // If both RiotClientRunner and LeagueClientRunner succeed, print the completed username to the console
        Console.WriteLine($"Completed {username}");
        CleanUp();
    }

    // This method takes in the username, password, and settings JObject as parameters
    // and runs the RiotClientRunner and LeagueClientRunner methods including fetching
    // account data and executing Tasks(hextech, event).
    public async Task Job_AccountFetchingWithTasks(string username, string password, JObject settings)
    {
        // Run RiotClientRunner with the given username, password, and RiotClientPath
        bool didRiotSucceed = await RiotClientRunner(username, password, settings["RiotClientPath"].ToString());

        // If RiotClientRunner did not succeed, return from the method
        if (!didRiotSucceed)
        {
            CleanUp();
            return;
        }

        // Run LeagueClientRunner with the given LeagueClientPath. Execute Account fetching and Tasks.
        bool didLeagueSucceed = await LeagueClientRunner(settings["LeagueClientPath"].ToString(), false, false);

        // If LeagueClientRunner did not succeed, return from the method
        if (!didLeagueSucceed)
        {
            CleanUp();
            return;
        }

        // If both RiotClientRunner and LeagueClientRunner succeed, print the completed username to the console
        Console.WriteLine($"Completed {username}");
        CleanUp();
    }

    // This method takes in the username, password, and settings JObject as parameters
    // and runs the RiotClientRunner and LeagueClientRunner methods excluding fetching
    // account data but executing Tasks(hextech, event).
    public async Task Job_ExecuteTasksWithoutAccountFetching(string username, string password, JObject settings)
    {
        // Run RiotClientRunner with the given username, password, and RiotClientPath
        bool didRiotSucceed = await RiotClientRunner(username, password, settings["RiotClientPath"].ToString());

        // If RiotClientRunner did not succeed, return from the method
        if (!didRiotSucceed)
        {
            CleanUp();
            return;
        }

        // Run LeagueClientRunner with the given LeagueClientPath. Execute Account fetching and Tasks.
        bool didLeagueSucceed = await LeagueClientRunner(settings["LeagueClientPath"].ToString(), true, false);

        // If LeagueClientRunner did not succeed, return from the method
        if (!didLeagueSucceed)
        {
            CleanUp();
            return;
        }

        // If both RiotClientRunner and LeagueClientRunner succeed, print the completed username to the console
        Console.WriteLine($"Completed {username}");
        CleanUp();
    }

    /* 
     * Runs all required methods to connect and create the RiotClient Service.
     * Must be run before the LeagueClientRunner()
     * 
     * Requires account username, account password and the path to the RiotClientServices.exe
     */
    private async Task<bool> RiotClientRunner(string username, string password, string riotClientPath)
    {

        //Connection connection = new(); // Initialize a new connection for the RiotClientServices.exe

        RiotConnection riotConnection = new(_client, null, riotClientPath); // Creates client 

        await riotConnection.RunAsync(); // Run steps

        _riotClientCredentials = riotConnection.GetRiotCredentials(); // Retrieves Riot Client Connection Credentials for the LeagueClient.

        RiotAuth riotAuth = new(riotConnection, _client); // Initializes Auth session

        bool didLogin = await riotAuth.Login(username, password); // Attempts to login the Summoner

        if (!didLogin) // Check if login fails
        {
            Console.WriteLine("Thread Stopped. Login incorrect.");
            _client.CloseClient(riotConnection.ProcessID);
            return false;
        }

        _region = await riotConnection.RequestRegion(); // Gets the Region of the Summoner for the League Client.

        _account.Region = _region;
        _account.Username = username;
        _account.Password = password;

        var didLaunch = await riotConnection.WaitForLaunchAsync(); // Wait for League Client to Launch. Must be processed after LOGIN

        if (!didLaunch)
        {
            CleanUp();
            return false;
        }

        return true;

    }

    /* 
     * Runs all required methods to connect and create the LeagueClient Service.
     * Must be run after the RiotClientRunner()
     * 
     * Params:
     * 
     * Requires path to the LeagueClient.exe
     * Skip fetching account data. Defaulted to false
     * Skip running misc tasks. Defaulted to true
     */
    private async Task<bool> LeagueClientRunner(string leagueClientPath,
                                                bool skipAccountData = false,
                                                bool skipAccountTasks = true)
    {

        //Connection connection = new(); // Initialize a new connection for the LeagueClient.exe

        LeagueConnection leagueConnection = new(null, _client, leagueClientPath, _region) // Creates Client
        {
            RiotCredentials = _riotClientCredentials
        };

        var isCreated = await leagueConnection.Run(); // Run steps

        if (!isCreated)
        {
            _client.CloseClient(leagueConnection.ProcessID);
            return false;
        }

        _data = new(leagueConnection, _account); // Sets Data object with LeagueClient's Connection

        if (!skipAccountData)
        {
            await FetchAccountDataAsync(); // Fetches Summoner Account data
        }

        if (!skipAccountTasks)
        {
            if (skipAccountData)
            {
                await FetchAccountLootAsync();
            }
            _hextech = new(leagueConnection, _loot);
            await ExecuteHextechTasks(); // Executes Hextech tasks on account
            await ExecuteEventTasks(); // Executes Event tasks on account

            await _loot.RefreshLoot();
        }

        await _data.ExportAccount(); // Export account

        return true;
    }

    private async Task FetchAccountLootAsync()
    {

        _loot = await _data.GetLootAsync();

    }

    // This method is responsible for fetching account data for the passed account asynchronously.
    private async Task FetchAccountDataAsync()
    {
        
        _loot = await _data.GetLootAsync();
        
        await _data.GetSkinsAsync();
        await _data.GetChampionsAsync();
        await _data.GetSummonerDataAsync();
        await _data.GetRank();
        await _data.GetQueueStats();
    }

    // This method executes the wanted Hextech Tasks on an account asynchronously.
    private async Task ExecuteHextechTasks()
    {
        var tasks = await _coreUtility.ReadFromTasksConfigFile();

        foreach (var task in tasks)
        {
            if (task.Key == TasksConfig.CraftKeys && task.Value == true)
            {
                _hextech.CraftKeys();
            }

            if (task.Key == TasksConfig.OpenChests && task.Value == true)
            {
                _hextech.OpenChests();
            }

            if (task.Key == TasksConfig.DisenchantChampionShards && task.Value == true)
            {
                _hextech.DisenchantChampionShards();
            }

            if (task.Key == TasksConfig.DisenchantEternalShards && task.Value == true)
            {
                _hextech.DisenchantEternalShards();
            }

            if (task.Key == TasksConfig.DisenchantSkinShards && task.Value == true)
            {
                _hextech.DisenchantSkinShards();
            }

            if (task.Key == TasksConfig.DisenchantWardSkinShards && task.Value == true)
            {
                _hextech.DisenchantWardSkinShards();
            }

            if (tasks[TasksConfig.OpenCapsulesOrbsShards])
            {
                _hextech.OpenLoot();
            }

            //if (tasks[TasksConfig.BuyBlueEssence])
            //{

            //}

            //if (tasks[TasksConfig.BuyChampionShards])
            //{

            //}
        }
        return;
    }

    // This method executes the wanted Event Tasks on an account asynchronously.
    private async Task ExecuteEventTasks()
    {
        var tasks = await _coreUtility.ReadFromTasksConfigFile();

        if (tasks[TasksConfig.ClaimEventRewards])
            {
            return;
        }

    }

    // This method kills all existing clients
    public void CleanUp()
    {
        _client.CloseClients();

    }

}
