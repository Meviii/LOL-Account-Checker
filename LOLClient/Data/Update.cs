using AccountChecker.DataFiles;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountChecker.Data;

public class Update
{
    private readonly HttpClient _httpClient;

    public Update()
    {
        // Initialize HttpClient with base address for requests
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://raw.communitydragon.org")
        };
    }

    public async Task UpdateChampionsAsync()
    {
        // Path to champions file
        string filePath = $"{PathConfig.ChampionsFile}";
        try
        {
            // Send GET request to retrieve data
            var response = await _httpClient.GetAsync("https://raw.communitydragon.org/pbe/plugins/rcp-be-lol-game-data/global/default/v1/champion-summary.json");
            // Read response as string
            var champsRes = await response.Content.ReadAsStringAsync();
            // Deserialize JSON response
            var json = JsonConvert.DeserializeObject(champsRes);

            // Check if response was successful
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Error getting data");
                return;
            }

            // Check if data folder exists, create it if it doesn't
            if (!Directory.Exists(PathConfig.DataFolder))
                Directory.CreateDirectory(PathConfig.DataFolder);

            // Check if file exists, create it if it doesn't
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            // Write deserialized JSON data to file
            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(JsonConvert.SerializeObject(json, Formatting.Indented));
            }

            Console.WriteLine($"Creating and saving to {filePath}");

        }
        catch
        {
            throw new Exception("Unable to retrieve champions.");
        }
    }

    public async Task UpdateSkinsAsync()
    {
        // Path to skins file
        string filePath = $"{PathConfig.SkinsFile}";
        try
        {
            // Send GET request to retrieve data
            var response = await _httpClient.GetAsync("latest/plugins/rcp-be-lol-game-data/global/default/v1/skins.json");
            // Read response as string
            var skinsRes = await response.Content.ReadAsStringAsync();
            // Deserialize JSON response
            var json = JsonConvert.DeserializeObject(skinsRes);

            // Check if response was successful
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("error getting data");
                return;
            }

            // Check if file exists, create it if it doesn't
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            // Write deserialized JSON data to file
            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(JsonConvert.SerializeObject(json, Formatting.Indented));
            }

            Console.WriteLine($"Creating and saving to {filePath}");

        }
        catch
        {
            throw new Exception("Unable to retrieve skins.");
        }
    }
}

