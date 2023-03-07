using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LOLClient.Connections;

public class RiotAuth
{
    private readonly Connection _connection;
    private readonly RiotConnection _riotConnection;

    public RiotAuth(Connection connection, RiotConnection riotConnection)
    {

        _connection = connection;
        _riotConnection = riotConnection;

    }

    private bool CanAuthenticate(string username, string password)
    {

        var data = new Dictionary<string, object>()
        {
            {"username", username},
            {"password", password},
            {"persistLogin", false},
        };

        while (true)
        {


            var response = _connection.RequestAsync(HttpMethod.Put, "/rso-auth/v1/session/credentials", data).Result;
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


    private async void AcceptEULA()
    {
        await _connection.RequestAsync(HttpMethod.Put, "/eula/v1/agreement/acceptance", null);
    }

    public bool Login(string username, string password, string riotClientPath)
    {

        bool canAuth = CanAuthenticate(username, password);

        if (!canAuth)
            return false;

        AcceptEULA();

        return true;
    }

}
