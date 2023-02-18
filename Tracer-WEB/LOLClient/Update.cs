using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace LOLClient;

public class Update
{
    private readonly HttpClient _httpClient;

    public Update()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://raw.communitydragon.org");
    }

    public void UpdateChampions()
    {
        string filePath = @"..\..\..\Updates\champions.json";
        try
        {
            var response = _httpClient.GetAsync("https://raw.communitydragon.org/pbe/plugins/rcp-be-lol-game-data/global/default/v1/champion-summary.json").Result;
            var champsRes = response.Content.ReadAsStringAsync().Result;
            var json = JsonConvert.DeserializeObject(champsRes);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("error getting data");
                return;
            }

            if (!File.Exists(filePath))
            {
                // Create the file if it does not exist
                File.Create(filePath).Dispose();
            }

            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(JsonConvert.SerializeObject(json, Formatting.Indented));
            }

            Console.WriteLine($"Creating and saving to {filePath}");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void UpdateSkins()
    {
        string filePath = @"..\..\..\Updates\skins.json";
        try
        {
            var response = _httpClient.GetAsync("latest/plugins/rcp-be-lol-game-data/global/default/v1/skins.json").Result;
            var skinsRes = response.Content.ReadAsStringAsync().Result;
            var json = JsonConvert.DeserializeObject(skinsRes);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("error getting data");
                return;
            }

            if (!File.Exists(filePath))
            {
                // Create the file if it does not exist
                File.Create(filePath).Dispose();
            }

            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(JsonConvert.SerializeObject(json, Formatting.Indented));
            }

            Console.WriteLine($"Creating and saving to {filePath}");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }


}
