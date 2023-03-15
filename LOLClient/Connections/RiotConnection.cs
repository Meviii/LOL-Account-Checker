using AccountChecker.Models;
using Azure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;


namespace AccountChecker.Connections;

public class RiotConnection : Connection
{

    public string _riotClientPath;
    private readonly Client _client;
    public int ProcessID { get; private set; }
    private static readonly object _lock = new();
    private readonly AccountCombo _accountCombo;

    public RiotConnection(AccountCombo accountCombo, Client client, string riotClientPath)
    {
        lock (_lock)
        {
            _accountCombo = accountCombo;
            _client = client;
            _riotClientPath = riotClientPath;
        }
    }

    public void Run()
    {
        CreateRiotClient();
        WaitForConnection();
    }


    public Dictionary<string, object> GetRiotCredentials()
    {

        var creds = new Dictionary<string, object>
        {
            { "riotPort", Port },
            { "riotAuthToken", AuthToken }
        };
        return creds;

    }

    private void WaitForConnection()
    {

        var data = new Dictionary<string, object>()
        {
            { "clientId", "riot-client"},
            { "trustLevels", new List<string> { "always_trusted" } }
        };

        var response = Request(HttpMethod.Post, "/rso-auth/v2/authorizations", data);

        Thread.Sleep(1000);
    }

    public async Task<bool> WaitForLaunchAsync(int timeout = 60)
    {
 
        var startTime = DateTime.Now;

        while (true)
        {
            var phase = Request(HttpMethod.Get, "/rnet-lifecycle/v1/product-context-phase", null);
            var result = JToken.Parse(await phase.Content.ReadAsStringAsync());

            if (result.ToString().ToLower() == "VngAccountRequired".ToLower())
            {
                Console.WriteLine("'VngAccountRequired' error. Re-adding to queue");
                AccountQueue.Enqueue(_accountCombo);
                return false;
            }

            if (result.ToString().ToLower() == "WaitForLaunch".ToLower())
            {
                Thread.Sleep(1000);
                return true;
            }

            if (result.ToString().ToLower() == "Login".ToLower())
            {
                // if account needs to be re authenticated
                Console.WriteLine("'Login' error. Re-adding to queue");
                AccountQueue.Enqueue(_accountCombo);
                return false;
            }

            if ((DateTime.Now - startTime).TotalSeconds >= timeout)
            return false;

            Thread.Sleep(1000);
        }
            
    }

    private void CreateRiotClient()
    {
        List<string> processArgs = new()
        {
            _riotClientPath,
            "--app-port=" + Port,
            "--remoting-auth-token=" + AuthToken,
            "--launch-product=league_of_legends",
            "--launch-patchline=live",
            "--allow-multiple-clients",
            "--locale=en_GB",
            "--disable-auto-launch",
            "--headless",
        };

        ProcessID = _client.CreateClient(processArgs, _riotClientPath);
    }


    public async Task<string> RequestRegionAsync()
    {
        var response = Request(HttpMethod.Get, "/riotclient/region-locale", null);

        var jsonResponse = JToken.Parse(await response.Content.ReadAsStringAsync());

        return jsonResponse["region"].ToString();

    }
}
