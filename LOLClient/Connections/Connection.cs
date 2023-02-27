using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;

namespace LOLClient.Connections;

public class Connection
{
    public readonly string Port;
    public readonly string AuthToken;
    public readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    private static HashSet<string> _usedPorts = new HashSet<string>();
    private static object _lock = new object(); // mutex object
   
    public Connection(ILogger logger)
    {

        _logger = logger;

        AuthToken = GenerateAuthToken();
        Port = Convert.ToString(GetFreePort());

        _httpClient = new HttpClient(GetHandlerSettings());
        _httpClient.BaseAddress = new Uri("https://127.0.0.1:" + Port);
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{AuthToken}")));
        Console.WriteLine($"Initializing new Connection. Port: {Port}, Auth Token: {AuthToken}");
        
    }

    // Finds a free port.
    private string GetFreePort(int minPort = 50000, int maxPort = 65000)
    {
        lock (_lock)
        {
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            for (var port = minPort; port < maxPort; port++)
            {
                try
                {
                    // check if port is already in use
                    if (_usedPorts.Contains(port.ToString())) continue;

                    sock.Bind(new IPEndPoint(IPAddress.Loopback, port));
                    sock.Close();

                    _usedPorts.Add(port.ToString());
                    return port.ToString();
                }
                catch (SocketException)
                {
                    // Port taken.
                }
            }

            throw new IOException("No available port");
        }
    }
    private string GenerateAuthToken(int length = 22)
    {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var result = new StringBuilder(length);

        for (var i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }

        return result.ToString();
    }

    private HttpClientHandler GetHandlerSettings()
    {
        //var proxy = new WebProxy(""); proxy
        return new HttpClientHandler()
        {
            //Proxy = proxy,
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
    }

    private void URLFixer(ref string url)
    {
        if (url[0] == '/')
            url = url.Remove(0, 1);
    }

    public async Task<HttpResponseMessage> RequestAsync(HttpMethod method, string url, Dictionary<string, object> requestData)
    {
        URLFixer(ref url);

        var requestAddress = _httpClient.BaseAddress + url;
        Console.WriteLine($"Sending {method} request. URL: {url}");

        HttpResponseMessage response;

        switch (method.Method)
        {
            case "GET":
                response = _httpClient.GetAsync(requestAddress).Result;
                break;
            case "POST":
                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync(requestAddress, content);
                break;
            case "PUT":
                json = JsonConvert.SerializeObject(requestData);
                content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _httpClient.PutAsync(requestAddress, content);
                break;
            case "DELETE":
                response = _httpClient.DeleteAsync(requestAddress).Result;
                break;
            default:
                throw new Exception("Unsupported HTTP method.");
        }
        Console.WriteLine($"Response: {response.StatusCode}");

        return response;
    }
}