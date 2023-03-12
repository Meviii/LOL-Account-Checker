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

namespace LOLClient.Connections;

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
    private async Task<bool> CanAuthenticate(string username, string password)
    {
        
        var data = new Dictionary<string, object>()
        {
            {"username", username},
            {"password", password},
            {"persistLogin", false},
        };

        while (true)
        {
            var response = await _connection.RequestAsync(HttpMethod.Put, "/rso-auth/v1/session/credentials", data);
            var content = JToken.Parse(await response.Content.ReadAsStringAsync());

            //lock (_lock)
            //{
            //    string logFilePath = @"..\..\..\logRSO_CREDENTIALS.txt";
            //    File.AppendAllTextAsync(logFilePath, $"{DateTime.Now} - STATUS CODE: {response.StatusCode} - ACCOUNT: {username} {password}\n\n {response.Content.ReadAsStringAsync().Result}\n\n\n\n\n").Wait();
            //}

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

    private async Task AcceptEULA()
    {

        await _connection.RequestAsync(HttpMethod.Put, "/eula/v1/agreement/acceptance", null);

    }

    public async Task<bool> Login(string username, string password)
    {

        bool canAuth = await CanAuthenticate(username, password);

        if (!canAuth)
            return false;

        await AcceptEULA();

        return true;
    }

}
