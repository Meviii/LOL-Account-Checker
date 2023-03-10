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

namespace LOLClient.Connections;

public class RiotConnection
{

    public string _riotClientPath;
    private readonly Connection _connection;
    private readonly Client _client;
    public int ProcessID { get; private set; }
    private static readonly object _lock = new();

    public RiotConnection(Client client, Connection connection, string riotClientPath)
    {
        lock (_lock)
        {
            _client = client;
            _connection = connection;
            _riotClientPath = riotClientPath;
        }
    }

    public async Task RunAsync()
    {
        await CreateRiotClient();
        await WaitForConnectionAsync();
    }


    public Dictionary<string, object> GetRiotCredentials()
    {

        var creds = new Dictionary<string, object>
        {
            { "riotPort", _connection.Port },
            { "riotAuthToken", _connection.AuthToken }
        };
        return creds;

    }

    private async Task WaitForConnectionAsync()
    {

        var data = new Dictionary<string, object>()
        {
            { "clientId", "riot-client"},
            { "trustLevels", new List<string> { "always_trusted" } }
        };

        await _connection.RequestAsync(HttpMethod.Post, "/rso-auth/v2/authorizations", data);

        await Task.Delay(2000);

    }

    public async Task<bool> WaitForLaunchAsync(int timeout = 60)
    {
 
        var startTime = DateTime.Now;

        while (true)
        {
            var phase = await _connection.RequestAsync(HttpMethod.Get, "/rnet-lifecycle/v1/product-context-phase", null);
            var result = JToken.Parse(await phase.Content.ReadAsStringAsync());

            lock (_lock)
            {
                string logFilePath = @"..\..\..\logPRODUCTCONTEXTPHASE.txt";

                File.AppendAllTextAsync(logFilePath, $"{DateTime.Now} - {result}\n\n\n\n\n").Wait();
            }
            if (result.ToString().ToLower() == "WaitForLaunch".ToLower())
            {
                await Task.Delay(1000);
                return true;
            }

            if (result.ToString().ToLower() == "Login".ToLower())
            {
                // if account needs to be re authenticated
                return false;
            }

            if ((DateTime.Now - startTime).TotalSeconds >= timeout)
            return false;

            await Task.Delay(1000);
        }
            
    }

    private async Task CreateRiotClient()
    {
        List<string> processArgs = new()
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

        ProcessID = await _client.CreateClient(processArgs, _riotClientPath);
    }


    public async Task<string> RequestRegion()
    {
        var response = await _connection.RequestAsync(HttpMethod.Get, "/riotclient/region-locale", null);

        var jsonResponse = JToken.Parse(await response.Content.ReadAsStringAsync());

        return jsonResponse["region"].ToString();

    }
}
