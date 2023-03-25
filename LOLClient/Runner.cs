using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountChecker.Models;
using Newtonsoft.Json.Linq;
using AccountChecker.Connections;
using AccountChecker.Utility;
using AccountChecker.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using AccountChecker.Data;

namespace AccountChecker;

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
    private AccountData _accountData;
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
    public async Task Job_AccountFetchingWithoutTasks(AccountCombo combo, JObject settings)
    {
        // Run RiotClientRunner with the given username, password, and RiotClientPath
        bool didRiotSucceed = await RiotClientRunner(combo, settings["RiotClientPath"].ToString());

        // If RiotClientRunner did not succeed, return from the method
        if (!didRiotSucceed)
        {
            
            CleanUp();
            return;
        }

        // Run LeagueClientRunner with the given LeagueClientPath
        bool didLeagueSucceed = await LeagueClientRunner(combo, settings["LeagueClientPath"].ToString());

        // If LeagueClientRunner did not succeed, return from the method
        if (!didLeagueSucceed)
        {
            CleanUp();
            return;
        }
        
        Main.SuccessfulAccounts += 1;
        
        // If both RiotClientRunner and LeagueClientRunner succeed, print the completed username to the console
        Console.WriteLine($"Completed {combo.Username}");
        CleanUp();
    }

    // This method takes in the username, password, and settings JObject as parameters
    // and runs the RiotClientRunner and LeagueClientRunner methods including fetching
    // account data and executing Tasks(hextech, event).
    public async Task<bool> Job_AccountFetchingWithTasks(AccountCombo combo, JObject settings)
    {
        // Run RiotClientRunner with the given username, password, and RiotClientPath
        bool didRiotSucceed = await RiotClientRunner(combo, settings["RiotClientPath"].ToString());

        // If RiotClientRunner did not succeed, return from the method
        if (!didRiotSucceed)
        {
            CleanUp();
            return false;
        }

        // Run LeagueClientRunner with the given LeagueClientPath. Execute Account fetching and Tasks.
        bool didLeagueSucceed = await LeagueClientRunner(combo, settings["LeagueClientPath"].ToString(), false, false);

        // If LeagueClientRunner did not succeed, return from the method
        if (!didLeagueSucceed)
        {
            CleanUp();
            return false;
        }

        Main.SuccessfulAccounts += 1;
        
        // If both RiotClientRunner and LeagueClientRunner succeed, print the completed username to the console
        Console.WriteLine($"Completed {combo.Username}");
        CleanUp();
        return true;
    }

    // This method takes in the username, password, and settings JObject as parameters
    // and runs the RiotClientRunner and LeagueClientRunner methods excluding fetching
    // account data but executing Tasks(hextech, event).
    public async Task Job_ExecuteTasksWithoutAccountFetching(AccountCombo combo, JObject settings)
    {
        // Run RiotClientRunner with the given username, password, and RiotClientPath
        bool didRiotSucceed = await RiotClientRunner(combo, settings["RiotClientPath"].ToString());

        // If RiotClientRunner did not succeed, return from the method
        if (!didRiotSucceed)
        {
            CleanUp();
            return;
        }

        // Run LeagueClientRunner with the given LeagueClientPath. Execute Account fetching and Tasks.
        bool didLeagueSucceed = await LeagueClientRunner(combo, settings["LeagueClientPath"].ToString(), true, false);

        // If LeagueClientRunner did not succeed, return from the method
        if (!didLeagueSucceed)
        {
            CleanUp();
            return;
        }

        Main.SuccessfulAccounts += 1;

        // If both RiotClientRunner and LeagueClientRunner succeed, print the completed username to the console
        Console.WriteLine($"Completed {combo.Username}");
        CleanUp();
    }

    /* 
     * Runs all required methods to connect and create the RiotClient Service.
     * Must be run before the LeagueClientRunner()
     * 
     * Requires account username, account password and the path to the RiotClientServices.exe
     */
    private async Task<bool> RiotClientRunner(AccountCombo combo, string riotClientPath)
    {

        RiotConnection riotConnection = new(combo, _client, riotClientPath); // Creates client 

        var riotSucc = await riotConnection.RunAsync(); // Run steps

        if (!riotSucc)
        {
            riotConnection.Dispose();
            return false;
        }

        _riotClientCredentials = riotConnection.GetRiotCredentials(); // Retrieves Riot Client Connection Credentials for the LeagueClient.

        RiotAuth riotAuth = new(riotConnection, _client); // Initializes Auth session

        bool didLogin = await riotAuth.Login(combo); // Attempts to login the Summoner

        if (!didLogin) // Check if login fails
        {
            Console.WriteLine("Thread Stopped. Login incorrect.");
            riotConnection.Dispose();
            _client.CloseClient(riotConnection.ProcessID);
            return false;
        }

        _region = await riotConnection.RequestRegionAsync(); // Gets the Region of the Summoner for the League Client.

        _account.Region = _region;
        _account.Username = combo.Username;
        _account.Password = combo.Password;

        var didLaunch = await riotConnection.WaitForLaunchAsync(); // Wait for League Client to Launch. Must be processed after LOGIN

        if (!didLaunch)
        {
            riotConnection.Dispose();
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
    private async Task<bool> LeagueClientRunner(AccountCombo combo,
                                                string leagueClientPath,
                                                bool skipAccountData = false,
                                                bool skipAccountTasks = true)
    {

        LeagueConnection leagueConnection = new(combo, _client, leagueClientPath, _region) // Creates Client
        {
            RiotCredentials = _riotClientCredentials
        };

        var isCreated = await leagueConnection.Run(); // Run steps

        if (!isCreated)
        {
            leagueConnection.Dispose();
            _client.CloseClient(leagueConnection.ProcessID);
            return false;
        }

        _accountData = new(leagueConnection, _account); // Sets Data object with LeagueClient's Connection

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

            _eventData = new(leagueConnection);
            await ExecuteEventTasks(); // Executes Event tasks on account

           await _loot.RefreshLootAsync();
        }

        await _coreUtility.ExportAccount(_account); // Export account

        return true;
    }

    private async Task FetchAccountLootAsync()
    {

        _loot = await _accountData.GetLootAsync();

    }

    // This method is responsible for fetching account data for the passed account asynchronously.
    private async Task FetchAccountDataAsync()
    {
        
        _loot = await _accountData.GetLootAsync();
        
        await _accountData.GetSkinsAsync();
        await _accountData.GetChampionsAsync();
        await _accountData.GetSummonerDataAsync();
        await _accountData.GetRank();
        await _accountData.GetQueueStats();
        await _accountData.GetHonorStatsAsync();
        await _accountData.GetFriendsDataAsync();
        await _accountData.GetLastPlayDateAsync();
    }

    // This method executes the wanted Hextech Tasks on an account asynchronously.
    private async Task ExecuteHextechTasks()
    {
        var tasks = await _coreUtility.ReadFromTasksConfigFile();

        foreach (var task in tasks)
        {
            if (task.Key == TasksConfig.CraftKeys && task.Value == true)
            {
                await _hextech.CraftKeysAsync();
            }

            if (task.Key == TasksConfig.OpenChests && task.Value == true)
            {
                await _hextech.OpenChestsAsync();
            }

            if (task.Key == TasksConfig.DisenchantChampionShards && task.Value == true)
            {
                await _hextech.DisenchantChampionShards();
            }

            if (task.Key == TasksConfig.DisenchantEternalShards && task.Value == true)
            {
                await _hextech.DisenchantEternalShards();
            }

            if (task.Key == TasksConfig.DisenchantSkinShards && task.Value == true)
            {
                await _hextech.DisenchantSkinShards();
            }

            if (task.Key == TasksConfig.DisenchantWardSkinShards && task.Value == true)
            {
                await _hextech.DisenchantWardSkinShards();
            }

            if (tasks[TasksConfig.OpenCapsulesOrbsShards] && task.Value == true)
            {
                await _hextech.OpenLootAsync();
            }

            if (tasks[TasksConfig.RemoveFriends] && task.Value == true)
            {
                await _accountData.RemoveFriendsAsync();
                await _accountData.GetFriendsDataAsync();
            }

            if (tasks[TasksConfig.RemoveFriendRequests] && task.Value == true)
            {
                await _accountData.RemoveFriendRequestsAsync();
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

        foreach (var task in tasks)
        {
            if (tasks[TasksConfig.ClaimEventRewards] && task.Value == true)
            {
                await _eventData.ClaimEventRewardsAsync();
            }
        }

    }

    // This method kills all existing clients
    public void CleanUp()
    {
        _client.CloseClients();

    }

}
