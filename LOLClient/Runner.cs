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

namespace LOLClient;

/*
 * This class is the main executor for running an account session.
 * It is responsible for connecting, creating and retreiving data from
 * the league and riot client services.
 */

public delegate void AccountsLeftUpdatedEventHandler(object sender, int accountsLeft);

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
    private static object _lock = new object(); // mutex object
    public event AccountsLeftUpdatedEventHandler AccountsLeftUpdated;
    private int _accountsLeft;

    public Runner(Main main)
    {
        _utility = new UIUtility();
        _account = new(); // Create a new Account object to store account related information
        _logger = GetLoggerFactory().CreateLogger<Runner>(); // Initialize a new logger
        _client = new(_logger); // Initialize Client 
        
    }

    private void OnAccountsLeftUpdated(int accountsLeft)
    {
        AccountsLeftUpdated?.Invoke(this, accountsLeft);
    }

    public void RunOperation(int threadCount, char delimiter)
    {

        var settings = _utility.LoadFromSettingsFile();
        var path = settings.GetValue("ComboListPath").ToString();
        var comboList = ReadComboList(path, delimiter);
        _ = RunThreadsAsync(threadCount, comboList, settings);
        CleanUp();
    }

    public bool RunThreadsAsync(int threadCount, List<Tuple<string, string>> comboList, JObject settings)
    {

        var remainingCombos = comboList.Count;
        _accountsLeft = remainingCombos;

        if (threadCount > remainingCombos)
            threadCount = remainingCombos;

        while (remainingCombos > 0)
        {
            Console.WriteLine($"Remaining Combos: {remainingCombos}");
            var tasks = new List<Thread>();
            
            for (int i = 0; i < threadCount; i++)
            {
                if (remainingCombos <= 0)
                {
                    tasks.Clear();
                    CleanUp();
                    return true;
                }

                var combo = comboList[0];

                Console.WriteLine($"Starting thread for {combo.Item1}");
                var task = new Thread(() => Work(combo.Item1, combo.Item2, settings));
                comboList.RemoveAt(0);
                tasks.Add(task);
                remainingCombos--;
                OnAccountsLeftUpdated(_accountsLeft);
                _accountsLeft--;
            }
            foreach (var task in tasks)
            {
                task.Start();
            }
            
            foreach (var task in tasks)
            {
                task.Join();
            }
            tasks.Clear();
            CleanUp();
        }
        
        return false;
        
    }

    public void Work(string username, string password, JObject settings)
    {
        lock (_lock)
        {
            bool didRiotSucceed = RiotClientRunner(username, password, settings["RiotClientPath"].ToString());

            if (!didRiotSucceed)
                return;

            bool didLeagueSucceed = LeagueClientRunner(settings["LeagueClientPath"].ToString());

            if (!didLeagueSucceed)
                return;
            
            Console.WriteLine($"Completed {username}");
        }
    }

    public List<Tuple<string,string>> ReadComboList(string comboListPath, char delimiter)
    {
        if (!File.Exists(comboListPath))
            return null;

        string content = File.ReadAllTextAsync(comboListPath).Result;

        var accounts = new List<Tuple<string, string>>();

        foreach (string line in content.Split())
        {
            if (line != "")
            {
                var accString = line.Split(delimiter);
                accounts.Add(new Tuple<string, string>(accString[0], accString[1]));
            }
        }

        return accounts;
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

        bool didLogin = riotAuth.Login(username, password, riotClientPath); // Attempts to login the Summoner

        if (!didLogin) // Check if login fails
        {
            Console.WriteLine("Thread Stopped. Login incorrect.");
            _client.CloseClient(riotConnection.ProcessID);
            return false;
        }

        _region = riotConnection.RequestRegion(); // Gets the Region of the Summoner for the League Client.

        _account.Region = _region;
        _account.UserName = username;
        _account.Password = password;
        riotConnection.WaitForLaunch(); // Wait for League Client to Launch. Must be processed after LOGIN

        return true;
        
    }

    /* 
     * Runs all required methods to connect and create the LeagueClient Service.
     * Must be run after the RiotClientRunner()
     * 
     * Requires path to the LeagueClient.exe
     */
    private bool LeagueClientRunner(string leagueClientPath)
    {

        Connection connection = new(_logger); // Initialize a new connection for the LeagueClient.exe

        LeagueConnection leagueConnection = new(connection, _client, _logger, leagueClientPath, _region) // Creates Client
        {
            RiotCredentials = _riotClientCredentials
        };

        var isCreated = leagueConnection.Run(); // Run steps

        if (!isCreated)
            return false;

        _data = new(connection); // Sets Data object with LeagueClient's Connection
        
        FetchData(_account); // Fetches Summoner Account data
        _data.ExportAccount(_account); // Export account

        return true;
    }

    private void FetchData(Account account)
    {
        
        _data.GetSkins(account);
        _data.GetChampions(account);
        _data.GetSummonerData(account);
    }

    public void CleanUp()
    {
        _client.CloseClients();
        
    }

}
