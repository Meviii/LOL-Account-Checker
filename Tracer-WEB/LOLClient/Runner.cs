using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using LOLClient.Connections;
using Tracer_WEB.Models;

namespace LOLClient;

/*
 * This class is the main executor for running an account session.
 * It is responsible for connecting, creating and retreiving data from
 * the league and riot client services.
 */

public class Runner
{
    private readonly ILogger _logger;
    private readonly Client _client;
    private Dictionary<string, object> _riotClientCredentials;
    private Account _account;
    private Data _data;
    private string _region;
    private readonly Mutex _runnerMutex = new();

    public Runner()
    {
        try
        {
            _runnerMutex.WaitOne();

            _account = new(); // Create a new Account object to store account related information
            _logger = GetLoggerFactory().CreateLogger<Runner>(); // Initialize a new logger
            _client = new(_logger); // Initialize Client 
        }
        finally
        {
            _runnerMutex.ReleaseMutex();
        }

    }

    private ILoggerFactory GetLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("Debug", LogLevel.Debug)
                .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
                .AddConsole();
        });
    }

    /* 
     * Runs all required methods to connect and create the RiotClient Service.
     * Must be run before the LeagueClientRunner()
     * 
     * Requires account username, account password and the path to the RiotClientServices.exe
     */
    public bool RiotClientRunner(string username, string password, string riotClientPath)
    {

        Connection connection = new(_logger); // Initialize a new connection for the RiotClientServices.exe

        RiotConnection riotConnection = new(_client, connection, _logger); // Creates client 

        riotConnection.Run(); // Run steps
        
        _riotClientCredentials = riotConnection.GetRiotCredentials(); // Retrieves Riot Client Connection Credentials for the LeagueClient.

        RiotAuth riotAuth = new(connection, riotConnection, _logger); // Initializes Auth session

        bool didLogin = riotAuth.Login(username, password, riotClientPath).Result; // Attempts to login the Summoner
        
        if (!didLogin) // Check if login fails
        {
            _logger.LogWarning("Thread Stopped. Login incorrect.");
            return false;
        }

        _region = riotConnection.RequestRegion(); // Gets the Region of the Summoner for the League Client.

        riotConnection.WaitForLaunch(); // Wait for League Client to Launch. Must be processed after LOGIN

        return true;
    }

    /* 
     * Runs all required methods to connect and create the LeagueClient Service.
     * Must be run after the RiotClientRunner()
     * 
     * Requires path to the LeagueClient.exe
     */
    public void LeagueClientRunner(string leagueClientPath)
    {
        Connection connection = new(_logger); // Initialize a new connection for the LeagueClient.exe

        LeagueConnection leagueConnection = new(connection, _client, _logger, leagueClientPath, _region) // Creates Client
        {
            RiotCredentials = _riotClientCredentials
        };

        leagueConnection.Run(); // Run steps

        _data = new(connection); // Sets Data object with LeagueClient's Connection
        
        FetchData(); // Fetches Summoner Account data
        _account.Region = _region;

        _data.ExportAccount(_account); // Export account
    }

    private void FetchData()
    {
        _data.GetSkins(_account);
        _data.GetChampions(_account);
        _data.GetSummonerData(_account);

    }

    
    public void CleanUp()
    {
        _client.CloseClients();
    }

}
