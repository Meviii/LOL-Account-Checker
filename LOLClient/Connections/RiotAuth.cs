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
    private readonly Connection _connection;
    private static readonly object _lock = new();

    public RiotAuth(Connection connection)
    {
        lock (_lock)
        {
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
            lock (_lock)
            {
                string logFilePath = @"..\..\..\logCREDENTIALS.txt";

                File.AppendAllTextAsync(logFilePath, $"{DateTime.Now} - {content}\n\n\n\n\n").Wait();
            }
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
                    // re add to queue, false for now.
                    return false;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            await Task.Delay(3000);

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
