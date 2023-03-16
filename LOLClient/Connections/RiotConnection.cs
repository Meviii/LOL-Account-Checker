﻿using AccountChecker.DataFiles;
using AccountChecker.Models;
using AccountChecker.Utility;
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
    private readonly CoreUtility _coreUtility;
    public RiotConnection(AccountCombo accountCombo, Client client, string riotClientPath)
    {
        lock (_lock)
        {
            _coreUtility = new();
            _accountCombo = accountCombo;
            _client = client;
            _riotClientPath = riotClientPath;
        }
    }

    public async Task<bool> RunAsync()
    {
        CreateRiotClient();
        var connSucc = await WaitForConnectionAsync();
        if (!connSucc)
            return false;

        return true;
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

    private async Task<bool> WaitForConnectionAsync(int timeout = 10)
    {

        while (timeout > 0)
        {
            try {
                var data = new Dictionary<string, object>()
                {
                    { "clientId", "riot-client"},
                    { "trustLevels", new List<string> { "always_trusted" } }
                };

                var response = await RequestAsync(HttpMethod.Post, "/rso-auth/v2/authorizations", data);

                // Log response, add while loop to catch intended response
                _coreUtility.LogToFile("Auth_LOG.txt", $"{_accountCombo.Username} - {response.StatusCode} - \n{await response.Content.ReadAsStringAsync()}\n\n");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                timeout--;
                Thread.Sleep(1500);
            }catch (HttpRequestException e)
            {
                Console.WriteLine("HTTP Request exception at Authorization. Re adding to queue");
                AccountQueue.Enqueue(_accountCombo);
                return false;
            }catch (AggregateException e)
            {
                Console.WriteLine("Aggregate Exception at Authorization. Re adding to queue");
                AccountQueue.Enqueue(_accountCombo);
                return false;
            }
        }

        return false;
    }

    public async Task<bool> WaitForLaunchAsync(int timeout = 30)
    {
 
        var startTime = DateTime.Now;

        while (true)
        {
            var response = await RequestAsync(HttpMethod.Get, "/rnet-lifecycle/v1/product-context-phase", null);
            var result = JToken.Parse(await response.Content.ReadAsStringAsync());

            // Log result
            _coreUtility.LogToFile("ContextPhase_LOG.txt", $"{_accountCombo.Username} - {response.StatusCode} - \n{await response.Content.ReadAsStringAsync()}\n\n");


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
            {
                Console.WriteLine("Failed to get product context phase. Re-added to queue.");
                AccountQueue.Enqueue(_accountCombo);
                return false;
            }

            Thread.Sleep(3000);
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


    public async Task<string> RequestRegionAsync(int timeout = 10)
    {
        while (timeout > 0)
        {
            var response = await RequestAsync(HttpMethod.Get, "/riotclient/region-locale", null);

            _coreUtility.LogToFile("RegionLocale_LOG.txt", $"{_accountCombo.Username} - {response.StatusCode} - \n{await response.Content.ReadAsStringAsync()}\n\n");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JToken.Parse(await response.Content.ReadAsStringAsync());

                return jsonResponse["region"].ToString();
            }
            timeout--;
        }

        throw new Exception("Error requesting region locale.");
    }
}
