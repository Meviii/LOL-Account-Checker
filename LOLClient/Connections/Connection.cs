using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Polly;

namespace AccountChecker.Connections;
public class Connection : IDisposable
{
    public readonly string Port;
    public readonly string AuthToken;
    public readonly HttpClient _httpClient;
    private static readonly HashSet<int> _usedPorts = new();
    private static readonly object _lock = new();

    public Connection()
    {

        lock (_lock)
        {
            AuthToken = GenerateAuthToken();
            Port = GetFreePort();

            _httpClient = new HttpClient(GetHandlerSettings())
            {
                BaseAddress = new Uri("https://127.0.0.1:" + Port)
            };
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{AuthToken}")));

            Console.WriteLine($"Initializing new Connection. Port: {Port}, Auth Token: {AuthToken}");
        }
    }

    public string GetFreePort(int minPort = 50000, int maxPort = 65000)
    {

        var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var random = new Random();
        var attempts = maxPort - minPort;

        while (attempts-- > 0)
        {
            var port = random.Next(minPort, maxPort);

            try
            {
                // check if port is already in dictionary
                if (_usedPorts.Contains(port))
                    continue;

                sock.Bind(new IPEndPoint(IPAddress.Loopback, port));

                sock.Close();

                _usedPorts.Add(port);

                return port.ToString();
            }
            catch (SocketException e)
            {
                // check if port is in use
                if (e.SocketErrorCode == SocketError.AddressAlreadyInUse)
                {
                    // Retry
                    Thread.Sleep(1000);
                    continue;
                }

                // Retry with new Connection if socket refused
                if (e.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    // Retry
                    Thread.Sleep(1000);
                    continue;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("HTTP Request exception at Socket Creation.");

                //Retry
                Thread.Sleep(1000);
                continue;

            }
            catch (AggregateException e)
            {
                Console.WriteLine("Aggregate exception at Socket Creation.");
                //Retry
                Thread.Sleep(1000);
                continue;
            }
        }

        throw new Exception("No port available.");

    }

    private string GenerateAuthToken(int length = 22)
    {
        lock (_lock)
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
        HttpResponseMessage response = null;

        await Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound
                                               || r.StatusCode == HttpStatusCode.InternalServerError
                                               || (int)r.StatusCode == 429)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(() =>
            {
                lock (_lock)
                {
                    URLFixer(ref url);

                    var requestAddress = _httpClient.BaseAddress + url;
                    Console.WriteLine($"Sending {method} request. URL: {url}");

                    switch (method.Method)
                    {
                        case "GET":
                            response = _httpClient.GetAsync(requestAddress).Result;
                            break;
                        case "POST":
                            var json = JsonConvert.SerializeObject(requestData);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            response = _httpClient.PostAsync(requestAddress, content).Result;
                            break;
                        case "PUT":
                            json = JsonConvert.SerializeObject(requestData);
                            content = new StringContent(json, Encoding.UTF8, "application/json");
                            response = _httpClient.PutAsync(requestAddress, content).Result;
                            break;
                        case "DELETE":
                            response = _httpClient.DeleteAsync(requestAddress).Result;
                            break;
                        default:
                            throw new Exception("Unsupported HTTP method.");
                    }
                    Console.WriteLine($"Response: {response.StatusCode}");
                    return Task.FromResult(response);
                }
            });
        return response;
    }

    public async Task<HttpResponseMessage> RequestAsync(HttpMethod method, string url, List<string> requestData, bool isList)
    {
        HttpResponseMessage response = null;

        await Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound
                                               || r.StatusCode == HttpStatusCode.InternalServerError
                                               || (int)r.StatusCode == 429)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(() =>
            {
                lock (_lock)
                {
                    URLFixer(ref url);

                    var requestAddress = _httpClient.BaseAddress + url;
                    Console.WriteLine($"Sending {method} request. URL: {url}");

                    switch (method.Method)
                    {
                        case "GET":
                            response = _httpClient.GetAsync(requestAddress).Result;
                            break;
                        case "POST":
                            var json = JsonConvert.SerializeObject(requestData);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            response = _httpClient.PostAsync(requestAddress, content).Result;
                            break;
                        case "PUT":
                            json = JsonConvert.SerializeObject(requestData);
                            content = new StringContent(json, Encoding.UTF8, "application/json");
                            response = _httpClient.PutAsync(requestAddress, content).Result;
                            break;
                        case "DELETE":
                            response = _httpClient.DeleteAsync(requestAddress).Result;
                            break;
                        default:
                            throw new Exception("Unsupported HTTP method.");
                    }
                    Console.WriteLine($"Response: {response.StatusCode}");
                    return Task.FromResult(response);
                }
            });
        return response;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}