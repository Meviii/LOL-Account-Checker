using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;

namespace LOLClient.Connections;

public class RiotConnection
{

    public string _riotClientPath;
    private readonly Connection _connection;
    private readonly Client _client;
    private readonly ILogger _logger;


    public RiotConnection(Client client, Connection connection, ILogger logger, string riotClientPath = "H:\\Riot Games\\Riot Client\\RiotClientServices.exe")
    {
        _logger = logger;
        _client = client;
        _connection = connection;
        _riotClientPath = riotClientPath;

    }

    public void Run()
    {

        CreateRiotClient();
        WaitForConnection();

    }

    public Dictionary<string, object> GetRiotCredentials()
    {
        try
        {

            var creds = new Dictionary<string, object>
            {
                { "riotPort", _connection.Port },
                { "riotAuthToken", _connection.AuthToken }
            };
            return creds;
        }
        finally { }

    }

    private void WaitForConnection()
    {
        var data = new Dictionary<string, object>()
        {
            { "clientId", "riot-client"},
            { "trustLevels", new List<string> { "always_trusted" } }
        };

        _connection.RequestAsync(HttpMethod.Post, "/rso-auth/v2/authorizations", data).Wait();

    }

    public void WaitForLaunch(int timeout = 30)
    {
        var startTime = DateTime.Now;

        while (true)
        {
            var phase = _connection.RequestAsync(HttpMethod.Get, "/rnet-lifecycle/v1/product-context-phase", null).Result;
            var result = JToken.Parse(phase.Content.ReadAsStringAsync().Result);

            if (result.ToString().ToLower() == "WaitForLaunch".ToLower())
            {
                Thread.Sleep(1000);
                return;
            }

            if ((DateTime.Now - startTime).TotalSeconds >= timeout)
                throw new Exception("Timed out.");

            Thread.Sleep(1000);
        }
    }

    private void CreateRiotClient()
    {
        List<string> processArgs = new List<string>()
        {
            _riotClientPath,
            "--app-port=" + _connection.Port,
            "--remoting-auth-token=" + _connection.AuthToken,
            "--launch-product=league_of_legends",
            "--launch-patchline=live",
            "--allow-multiple-clients",
            "--locale=en_GB",
            "--disable-auto-launch",
            "--headless",
        };

        _client.CreateClient(processArgs, _riotClientPath);
    }


    public string RequestRegion()
    {
        var response = _connection.RequestAsync(HttpMethod.Get, "/riotclient/region-locale", null).Result;

        var jsonResponse = JToken.Parse(response.Content.ReadAsStringAsync().Result);

        return jsonResponse["region"].ToString();
    }
}
