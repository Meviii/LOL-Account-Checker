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

namespace LOLClient.Connections;

public class LeagueConnection : Connection
{
    private readonly string _path;
    //private readonly Connection _connection;
    public Dictionary<string, object> RiotCredentials = null;
    private readonly string _region;
    private readonly Client _client;
    public int ProcessID { get; private set; }
    private static readonly object _lock = new();

    public LeagueConnection(Connection connection, Client client, string path, string region)
    {
        lock (_lock)
        {
            _path = path;
            _client = client;
            //_connection = connection;
            _region = region;
        }
    }

    public async Task<bool> Run()
    {

        await CreateLeagueClient(); // Create the client
        var isCreated = await WaitForSession(); // Wait for Session

        if (!isCreated)
        {
            Console.WriteLine("Failed to create League session. Stopping thread.");
            return false;
        }

        return true;
    }

    private async Task<bool> WaitForSession(int timeout = 12)
    {
        int counter = 0;

        while (true)
        {
            try
            {
                var response = await RequestAsync(HttpMethod.Get, "/lol-login/v1/session", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = JToken.Parse(await response.Content.ReadAsStringAsync());

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
                            Console.WriteLine("Account banned.");
                            return false;
                        }else if (result["error"]["messageId"].ToString().ToLower() == "FAILED_TO_COMMUNICATE_WITH_LOGIN_QUEUE".ToLower())
                        {
                            // re add to queue
                            Console.WriteLine("Failed to communicate with login queue.");
                            return false;
                        }
                    }
                }

                if (counter == timeout)
                {
                    return false;
                }

                counter++;
                Thread.Sleep(1000);
            }
            catch
            {
                // retry
            }
        }
    }

    private async Task CreateLeagueClient()
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

            ProcessID = _client.CreateClient(processArgs, _path).Result;
        }
    }

}
