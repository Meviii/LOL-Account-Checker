using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace LOLClient.Connections;

public class LeagueConnection
{
    private readonly string _path;
    private readonly Connection _connection;
    public Dictionary<string, object> RiotCredentials = null;
    private readonly string _region;
    private readonly ILogger _logger;
    private readonly Client _client;

    public LeagueConnection(Connection connection, Client client, ILogger logger, string path, string region)
    {

        _logger = logger;
        _path = path;
        _client = client;
        _connection = connection;
        _region = region;

    }

    public void Run()
    {

        CreateLeagueClient(); // Create the client
        WaitForSession(); // Wait for Session

    }

    private void WaitForSession()
    {

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

                        return;
                    }
                }
                Thread.Sleep(1000);
            }
            catch
            {
                // retry
            }
        }
    }

    private void CreateLeagueClient()
    {
        var processArgs = new List<string>
        {
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

        _client.CreateClient(processArgs, _path);
    }

}
