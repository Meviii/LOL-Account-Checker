using LOLClient.DataFiles;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient;

public class Update
{
    private readonly HttpClient _httpClient;

    public Update()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://raw.communitydragon.org")
        };
    }

    public async Task UpdateChampions()
    {
        string filePath = $"{Config.ChampionsFile}";
        try
        {
            var response = await _httpClient.GetAsync("https://raw.communitydragon.org/pbe/plugins/rcp-be-lol-game-data/global/default/v1/champion-summary.json");
            var champsRes = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject(champsRes);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Error getting data");
                return;
            }

            if (!Directory.Exists(Config.DataFolder))
                Directory.CreateDirectory(Config.DataFolder);


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
        catch
        {
            throw new Exception("Unable to retrieve champions.");
        }
    }

    public async Task UpdateSkins()
    {
        string filePath = $"{Config.SkinsFile}";
        try
        {
            var response = await _httpClient.GetAsync("latest/plugins/rcp-be-lol-game-data/global/default/v1/skins.json");
            var skinsRes = await response.Content.ReadAsStringAsync();
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
        catch
        {
            throw new Exception("Unable to retrieve skins.");
        }
    }


}
