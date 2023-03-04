using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LOLClient.Models;
using Newtonsoft.Json.Linq;
using LOLClient.Connections;
using LOLClient.Utility;

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
    private Data _data;
    private string _region;
    private readonly CoreUtility _coreUtility;

    public Runner()
    {
        _coreUtility = new CoreUtility();
        _account = new();
        _client = new();
        
    }

    // This method takes in the username, password, and settings JObject as parameters and runs the RiotClientRunner and LeagueClientRunner methods.
    public async Task Job(string username, string password, JObject settings)
    {
        // Run RiotClientRunner with the given username, password, and RiotClientPath
        bool didRiotSucceed = RiotClientRunner(username, password, settings["RiotClientPath"].ToString());

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

    /* 
     * Runs all required methods to connect and create the RiotClient Service.
     * Must be run before the LeagueClientRunner()
     * 
     * Requires account username, account password and the path to the RiotClientServices.exe
     */
    private bool RiotClientRunner(string username, string password, string riotClientPath)
    {

        Connection connection = new(); // Initialize a new connection for the RiotClientServices.exe

        RiotConnection riotConnection = new(_client, connection, riotClientPath); // Creates client 

        riotConnection.Run(); // Run steps

        _riotClientCredentials = riotConnection.GetRiotCredentials(); // Retrieves Riot Client Connection Credentials for the LeagueClient.

        RiotAuth riotAuth = new(connection, riotConnection); // Initializes Auth session

        bool didLogin = riotAuth.Login(username, password, riotClientPath); // Attempts to login the Summoner

        if (!didLogin) // Check if login fails
        {
            Console.WriteLine("Thread Stopped. Login incorrect.");
            _client.CloseClient(riotConnection.ProcessID);
            return false;
        }

        _region = riotConnection.RequestRegion(); // Gets the Region of the Summoner for the League Client.

        _account.Region = _region;
        _account.Username = username;
        _account.Password = password;
        
        var didLaunch = riotConnection.WaitForLaunch(); // Wait for League Client to Launch. Must be processed after LOGIN

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
     * Requires path to the LeagueClient.exe
     */
    private async Task<bool> LeagueClientRunner(string leagueClientPath)
    {

        Connection connection = new(); // Initialize a new connection for the LeagueClient.exe

        LeagueConnection leagueConnection = new(connection, _client, leagueClientPath, _region) // Creates Client
        {
            RiotCredentials = _riotClientCredentials
        };

        var isCreated = leagueConnection.Run(); // Run steps

        if (!isCreated)
            return false;

        _data = new(connection); // Sets Data object with LeagueClient's Connection


        await FetchDataAsync(_account); // Fetches Summoner Account data
        await _data.ExportAccount(_account); // Export account

        return true;
    }

    // This method is responsible for fetching account data for the passed account asynchronously.
    private async Task FetchDataAsync(Account account)
    {
        await _data.GetSkinsAsync(account);
        await _data.GetChampionsAsync(account);
        await _data.GetSummonerDataAsync(account);
    }

    public void CleanUp()
    {
        _client.CloseClients();
        
    }

}
