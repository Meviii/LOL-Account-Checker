﻿using AccountChecker.Models;
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

    public RiotAuth(RiotConnection connection, Client client)
    {
        lock (_lock)
        {
            _client = client;
            _connection = connection;
        }
    }

    private async Task<bool> CanAuthenticateAsync(AccountCombo combo)
    {
        
        var data = new Dictionary<string, object>()
        {
            {"username", combo.Username},
            {"password", combo.Password},
            {"persistLogin", false},
        };

        while (true)
        {
            var response = await _connection.RequestAsync(HttpMethod.Put, "/rso-auth/v1/session/credentials", data);
            var content = JToken.Parse(await response.Content.ReadAsStringAsync());

            if (content.SelectToken("error") != null)
            {
                if (content["error"].ToString() == "auth_failure")
                {
                    return false;
                }
            }
            
            if (content.SelectToken("errorCode") != null)
            {
                if (content["errorCode"].ToString() == "RPC_ERROR")
                {
                    Console.WriteLine("Riot Session error. Try again later.");
                    _client.CloseClients();
                    throw new Exception("Riot API Error. Try again in 5 minutes");
                }
            }

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            Thread.Sleep(3000);

            return false;
        }

    }

    private async void AcceptEULA(int timeout = 10)
    {
        while (timeout > 0)
        {
            var response = await _connection.RequestAsync(HttpMethod.Put, "/eula/v1/agreement/acceptance", null);

            if (response.IsSuccessStatusCode)
                return;

            Thread.Sleep(1500);
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
