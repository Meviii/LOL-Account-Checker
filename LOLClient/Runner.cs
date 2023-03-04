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

// AccountsLeft Event Handler for Main UI - Runner signaling
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

    public Runner()
    {
        _utility = new UIUtility();
        _account = new(); // Create a new Account object to store account related information
        _logger = GetLoggerFactory().CreateLogger<Runner>(); // Initialize a new logger
        _client = new(_logger); // Initialize Client 
        
    }

    // Signals account left text box in UI.
    private void OnAccountsLeftUpdated(int accountsLeft)
    {
        AccountsLeftUpdated?.Invoke(this, accountsLeft);
    }

    // This method runs the whole operation called by Main UI
    public async Task RunOperationAsync(int threadCount, char delimiter)
    {

        var settings = await _utility.LoadFromSettingsFileAsync(); // Gets Settings file
        var path = settings.GetValue("ComboListPath").ToString(); // Gets the current combolist path
        var comboList = ReadComboList(path, delimiter); // Reads the combolist(accounts) path 
        await RunTasksAsync(threadCount, comboList, settings); // Runs threads on combolist
        CleanUp(); // Force closes all clients
    }

    // This method asynchronously runs tasks to process a list of combos
    public async Task<bool> RunTasksAsync(int threadCount, List<Tuple<string, string>> comboList, JObject settings)
    {
        // Initialize variables
        var remainingCombos = comboList.Count;
        _accountsLeft = remainingCombos;

        // Limit thread count to remaining combos
        if (threadCount > remainingCombos)
            threadCount = remainingCombos;

        // Loop through combos until all have been processed
        while (remainingCombos > 0)
        {
            Console.WriteLine($"Remaining Combos: {remainingCombos}");
            var tasks = new List<Task>();

            // Start a new task for each thread
            for (int i = 0; i < threadCount; i++)
            {
                if (remainingCombos <= 0)
                {
                    tasks.Clear();
                    CleanUp();
                    return true;
                }

                // Get the first combo from the list
                var combo = comboList[0];

                Console.WriteLine($"Starting task for {combo.Item1}");

                // Run the Work method as a Task
                var task = Task.Run(() => Work(combo.Item1, combo.Item2, settings));

                // Remove the combo from the list and add the task to the tasks list
                comboList.RemoveAt(0);
                tasks.Add(task);

                // Update counters
                remainingCombos--;
                OnAccountsLeftUpdated(_accountsLeft);
                _accountsLeft--;
            }

            // Wait for all tasks to complete before proceeding
            await Task.WhenAll(tasks);

            // Clear the tasks list and perform cleanup
            tasks.Clear();
            CleanUp();
        }

        // Return false if the method did not complete successfully
        return false;
    }

    // This method takes in the username, password, and settings JObject as parameters and runs the RiotClientRunner and LeagueClientRunner methods.
    public async Task Work(string username, string password, JObject settings)
    {
        // Run RiotClientRunner with the given username, password, and RiotClientPath
        bool didRiotSucceed = RiotClientRunner(username, password, settings["RiotClientPath"].ToString());

        // If RiotClientRunner did not succeed, return from the method
        if (!didRiotSucceed)
        {
            return;
        }

        // Run LeagueClientRunner with the given LeagueClientPath
        bool didLeagueSucceed = await LeagueClientRunner(settings["LeagueClientPath"].ToString());

        // If LeagueClientRunner did not succeed, return from the method
        if (!didLeagueSucceed)
            return;

        // If both RiotClientRunner and LeagueClientRunner succeed, print the completed username to the console
        Console.WriteLine($"Completed {username}");
    }

    // This method returns a List holding tuples of each account extracted from the combolist. In <User,Pass> format
    public List<Tuple<string, string>> ReadComboList(string comboListPath, char delimiter)
    {
        // Check if the file exists, if not, return null
        if (!File.Exists(comboListPath))
            return null;

        // Read the content of the file and store it in the "content" variable
        string content = File.ReadAllTextAsync(comboListPath).Result;

        // Create a new list to store the account tuples
        var accounts = new List<Tuple<string, string>>();

        // Loop through each line in the content
        foreach (string line in content.Split())
        {
            // If the line is not empty
            if (line != "")
            {
                // Split the line by the delimiter and add a new tuple to the "accounts" list
                var accString = line.Split(delimiter);
                accounts.Add(new Tuple<string, string>(accString[0], accString[1]));
            }
        }

        // Return the list of account tuples
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
    private async Task<bool> LeagueClientRunner(string leagueClientPath)
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
