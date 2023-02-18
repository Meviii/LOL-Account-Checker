using LOLClient.Connections;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LOLClient;

public class RiotAuth
{
    private readonly Connection _connection;
    private readonly RiotConnection _riotConnection;
    private readonly ILogger _logger;

    private readonly Mutex _riotAuthMutex = new();
    private readonly object _lock = new();
    public RiotAuth(Connection connection, RiotConnection riotConnection, ILogger logger)
    {

        _logger = logger;
        _connection = connection;
        _riotConnection = riotConnection;

    }

    private async Task<bool> CanAuthenticate(string username, string password)
    {

        try
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
                var content = JToken.Parse(response.Content.ReadAsStringAsync().Result);

                if (content.SelectToken("error") != null)
                {
                    if (content["error"].ToString() == "auth_failure")
                    {

                        return false;
                    }
                }

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }
        finally { }

    }


    private async void AcceptEULA()
    {
        await _connection.RequestAsync(HttpMethod.Put, "/eula/v1/agreement/acceptance", null);
    }

    public async Task<bool> Login(string username, string password, string riotClientPath)
    {
        bool canAuth = await CanAuthenticate(username, password);

        if (!canAuth)
            return false;

        AcceptEULA();

        return true;
    }

}
