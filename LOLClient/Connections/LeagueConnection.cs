using AccountChecker.Models;
using AccountChecker.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace AccountChecker.Connections;

public class LeagueConnection : Connection
{
    private readonly string _path;
    public Dictionary<string, object> RiotCredentials = null;
    private readonly string _region;
    private readonly Client _client;
    private readonly AccountCombo _accountCombo;
    public int ProcessID { get; private set; }
    private static readonly object _lock = new();
    private readonly CoreUtility _coreUtility;

    public LeagueConnection(AccountCombo combo, Client client, string path, string region)
    {
        lock (_lock)
        {
            _coreUtility = new CoreUtility();
            _accountCombo = combo;
            _path = path;
            _client = client;
            _region = region;
        }
    }

    public async Task<bool> Run()
    {

        CreateLeagueClient(); // Create the client
        var isCreated = await WaitForSessionAsync(); // Wait for Session

        if (!isCreated)
        {
            Console.WriteLine("Failed to create League session. Stopping thread.");
            return false;
        }

        return true;
    }

    private async Task<bool> WaitForSessionAsync(int timeout = 12)
    {

        while (timeout > 0)
        {
            try
            {
                var response = await RequestAsync(HttpMethod.Get, "/lol-login/v1/session", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = JToken.Parse(await response.Content.ReadAsStringAsync());

                    // Log result

                    _coreUtility.LogToFile("Session_LOG.txt", $"{_accountCombo.Username} - {response.StatusCode} - \n{await response.Content.ReadAsStringAsync()}\n\n");
                    

                    if (result["state"].ToString().ToLower() == "SUCCEEDED".ToLower())
                    {
                        Thread.Sleep(2000);
                        Console.WriteLine("Session succeeded.");
                        return true;
                    }

                    if (result["state"].ToString().ToLower() == "ERROR".ToLower())
                    {
                        if (result["error"]["messageId"].ToString().ToLower() == "ACCOUNT_BANNED".ToLower())
                        {
                            Main.FailAccounts += 1;
                            Console.WriteLine("Account banned.");
                            return false;
                        }else if (result["error"]["messageId"].ToString().ToLower() == "FAILED_TO_COMMUNICATE_WITH_LOGIN_QUEUE".ToLower())
                        {
                            AccountQueue.Enqueue(_accountCombo);
                            Console.WriteLine("Failed to communicate with login queue. Re-added to queue");
                            return false;
                        }
                    }
                }

                timeout--;
                Thread.Sleep(1500);
            }
            catch
            {
                timeout--;
                // retry
            }
        }

        AccountQueue.Enqueue(_accountCombo);
        Console.WriteLine("Timed Out. Re-added to queue.");
        return false;
    }

    private void CreateLeagueClient()
    {
        lock (_lock)
        {
            var processArgs = new List<string> {
            _path,
            "--riotclient-app-port=" + RiotCredentials["riotPort"],
            "--riotclient-auth-token=" + RiotCredentials["riotAuthToken"],
            "--app-port=" + Port,
            "--remoting-auth-token=" + AuthToken,
            "--allow-multiple-clients",
            "--locale=en_GB",
            "--disable-self-update",
            "--region=" + _region,
            "--headless"
        };

            ProcessID = _client.CreateClient(processArgs, _path);
        }
    }

}
