using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LOLClient.Models;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using LOLClient.Connections;
using LOLClient.Utility;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
    private readonly UIUtility _utility;
    public Runner()
    {
        try
        {
            _runnerMutex.WaitOne();

            _utility = new UIUtility();
            _account = new(); // Create a new Account object to store account related information
            _logger = GetLoggerFactory().CreateLogger<Runner>(); // Initialize a new logger
            _client = new(_logger); // Initialize Client 
        }
        finally
        {
            _runnerMutex.ReleaseMutex();
        }

    }

    public void RunOperation(int threadCount)
    {

        var settings = _utility.LoadFromSettingsFile();
        var comboList = ReadComboList(settings["ComboListPath"].ToString());

        RunThreads(threadCount, comboList, settings);

    }

    public bool RunThreads(int threadCount, List<Tuple<string, string>> comboList, JObject settings)
    {

        Console.WriteLine($"Remaining Combos: {comboList.Count}");

        if (comboList.Count <= 0)
            return true;

        for (int i = 0; i < threadCount; i++)
        {
            if (comboList.Count <= 0)
                return true;

            Console.WriteLine($"Thread {i} started.");

            Thread thread = new(() => Work(comboList[0].Item1, comboList[0].Item2, settings));
            thread.Start();

            comboList.RemoveAt(0);
        }

        Task.WaitAll();

        return RunThreads(threadCount, comboList, settings);
    }

    private void Work(string username, string password, JObject settings)
    {
        bool didRiotSucceed = RiotClientRunner(username, password, settings["RiotClientPath"].ToString());

        if (!didRiotSucceed)
        {
            CleanUp();
            return;
        }

        LeagueClientRunner(settings["LeagueClientPath"].ToString());
        CleanUp();
    }

    public List<Tuple<string,string>> ReadComboList(string comboListPath)
    {
        Console.WriteLine(comboListPath);

        if (!File.Exists(comboListPath))
            return null;

        string content = File.ReadAllTextAsync(@"C:\Users\Mevi\Desktop\acombo.txt").Result;
        Console.WriteLine(content);

        return null;
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
    private bool RiotClientRunner(string username, string password, string riotClientPath)
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
    private void LeagueClientRunner(string leagueClientPath)
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
