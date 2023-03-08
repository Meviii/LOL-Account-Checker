using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Threading;

namespace LOLClient.Connections;

public class LeagueConnection
{
    private readonly string _path;
    private readonly Connection _connection;
    public Dictionary<string, object> RiotCredentials = null;
    private readonly string _region;
    private readonly Client _client;
    public int ProcessID { get; private set; }
    private readonly object _lock = new();

    public LeagueConnection(Connection connection, Client client, string path, string region)
    {
        lock (_lock)
        {
            _path = path;
            _client = client;
            _connection = connection;
            _region = region;
        }

    }

    public bool Run()
    {

        CreateLeagueClient(); // Create the client
        var isCreated = WaitForSession(); // Wait for Session

        if (!isCreated)
        {
            Console.WriteLine("Failed to create League session. Stopping thread.");
            return false;
        }

        return true;
    }

    private bool WaitForSession(int timeout = 12)
    {
        int counter = 0;

        while (true)
        {
            try
            {
                var response = _connection.RequestAsync(HttpMethod.Get, "/lol-login/v1/session", null).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = JToken.Parse(response.Content.ReadAsStringAsync().Result);

                    if (result["state"].ToString().ToLower() == "SUCCEEDED".ToLower())
                    {
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
                            Console.WriteLine("Failed to communicate with login queue. Re-adding to queue");
                            return false;
                        }
                    }
                }

                if (counter == timeout)
                {
                    return false;
                }

                counter++;
                Thread.Sleep(3000);
            }
            catch
            {
                // retry
            }
        }
    }

    private void CreateLeagueClient()
    {
        var processArgs = new List<string> {
            _path,
            "--riotclient-app-port=" + RiotCredentials["riotPort"],
            "--riotclient-auth-token=" + RiotCredentials["riotAuthToken"],
            "--app-port=" + _connection.Port,
            "--remoting-auth-token=" + _connection.AuthToken,
            "--allow-multiple-clients",
            "--locale=en_GB",
            "--disable-self-update",
            "--region=" + _region,
            "--headless"
        };

        ProcessID = _client.CreateClient(processArgs, _path);
    }

}
