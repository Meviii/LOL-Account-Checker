using AccountChecker.Data;
using LOLClient.DataFiles;
using LOLClient.Models;
using LOLClient.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient.Utility;

/*
 * This class provides utility methods for the core application.
 */
public class CoreUtility
{
    private static readonly object _lock = new();

    public CoreUtility() { }

    // This method saves a key-value pair to a JSON settings file
    public async Task SaveToSettingsFileAsync(string key, object value)
    {
        // Get the file path of the settings file
        string filePath = $"{PathConfig.SettingsFile}";

        // Declare a JObject variable to hold the settings
        JObject settings;

        // Check if the data folder exists; if not, create it
        if (!Directory.Exists($"{PathConfig.DataFolder}"))
            Directory.CreateDirectory($"{PathConfig.DataFolder}");

        // If the settings file exists, load its contents, update the specified key-value pair, and save the updated settings
        if (File.Exists(filePath))
        {
            // Read the contents of the file asynchronously and store them in a string variable
            string content = await File.ReadAllTextAsync(filePath);

            // Parse the string as a JObject and store it in the settings variable
            settings = JObject.Parse(content);

            // Update the specified key-value pair
            settings[key] = JToken.FromObject(value);

            // Write the updated settings to the file
            File.WriteAllText(filePath, settings.ToString());
        }
        // If the settings file does not exist, create a new JObject with the specified key-value pair and save it to a new file
        else
        {
            // Create a new JObject with the specified key-value pair
            settings = new()
            {
                [key] = JToken.FromObject(value)
            };

            // Write the settings to a new file
            File.WriteAllText(filePath, settings.ToString());
        }
    }

    // This method loads the contents of a JSON settings file and returns them as a JObject
    public JObject LoadFromSettingsFile()
    {
        // Declare a JObject variable to hold the settings
        JObject settings;

        // Get the file path of the settings file
        string filePath = $"{PathConfig.SettingsFile}";

        // If the settings file exists, load its contents and return them as a JObject
        if (File.Exists(filePath))
        {
            // Read the contents of the file asynchronously and store them in a string variable
            string content = File.ReadAllTextAsync(filePath).Result;

            // Parse the string as a JObject and store it in the settings variable
            settings = JObject.Parse(content);

            // Return the settings as a JObject
            return settings;
        }

        // If the settings file does not exist, return null
        return null;
    }

    // This method loads a list of accounts from JSON files in a specified folder
    public async Task<List<Account>> LoadAccountsFromExportsFolderAsync()
    {
        // Declare a JArray variable to hold the accounts
        JArray accounts = new();

        // Get the path of the exports folder
        string folderPath = $"{PathConfig.ExportsFolder}";

        // If the exports folder does not exist, return null
        if (!Directory.Exists(folderPath))
            return null;

        // Loop through each file in the exports folder
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            // If the file exists and has a .json extension, attempt to parse its contents as a JObject
            if (File.Exists(filePath) && filePath.EndsWith(".json"))
            {
                try
                {
                    // Read the contents of the file asynchronously and store them in a string variable
                    string content = await File.ReadAllTextAsync(filePath);

                    // Parse the string as a JObject and store it in a variable
                    var account = JObject.Parse(content);

                    // Add the account to the accounts JArray
                    accounts.Add(account);
                }
                catch
                {
                    // If there is an error parsing the file, skip it and continue to the next one
                    continue;
                }
            }
        }

        // Convert the accounts JArray to a list of Account objects and return it
        return JsonConvert.DeserializeObject<List<Account>>(accounts.ToString());
    }

    // This method returns a List holding tuples of each account extracted from the combolist. In <User,Pass> format
    public async Task<List<Tuple<string, string>>> ReadComboList(string comboListPath, char delimiter)
    {
        // Check if the file exists, if not, return null
        if (!File.Exists(comboListPath))
            return null;

        // Read the content of the file and store it in the "content" variable
        string content = await File.ReadAllTextAsync(comboListPath);

        // Create a new list to store the account tuples
        var accounts = new List<Tuple<string, string>>();

        // Loop through each line in the content
        foreach (string line in content.Split())
        {
            try
            {
                // If the line is not empty
                if (line != "")
                {
                    // Split the line by the delimiter and add a new tuple to the "accounts" list
                    var accString = line.Split(delimiter);
                    accounts.Add(new Tuple<string, string>(accString[0], accString[1]));
                }
            }catch
            {
                Console.WriteLine($"Couldn't read account at {line}");
            }
        }

        // Return the list of account tuples
        return accounts;
    }

    // This method creates the initial tasks config for all tasks. Every task is defaulted to true (checked)
    public async void InitializeTaskConfigFileAsync()
    {
        // better to not hard code
        var tasksConfigDict = new Dictionary<string, bool>()
        {
            { TasksConfig.CraftKeys, false },
            { TasksConfig.OpenChests, false },
            { TasksConfig.DisenchantChampionShards, false },
            { TasksConfig.DisenchantSkinShards, false },
            { TasksConfig.DisenchantEternalShards, false },
            { TasksConfig.OpenCapsulesOrbsShards, false },
            { TasksConfig.BuyBlueEssence, false },
            { TasksConfig.BuyChampionShards, false },
            { TasksConfig.ClaimEventRewards, false },
            { TasksConfig.DisenchantWardSkinShards, false }
        };

        if (!Directory.Exists(PathConfig.DataFolder))
            Directory.CreateDirectory(PathConfig.DataFolder);

        if (!File.Exists(PathConfig.TasksFile))
            File.Create(PathConfig.TasksFile).Dispose();

        using var streamWriter = new StreamWriter(PathConfig.TasksFile);
        streamWriter.Write(JsonConvert.SerializeObject(tasksConfigDict, Formatting.Indented));

        //var json = JsonConvert.SerializeObject(tasksConfigDict, Formatting.Indented);

        ////await File.WriteAllTextAsync(PathConfig.TasksFile, json);
        //using (var streamWriter = new StreamWriter(PathConfig.TasksFile))
        //{
        //    streamWriter.Write(json);
        //}
    }

    public async Task<Dictionary<string,bool>> ReadFromTasksConfigFile()
    {
        using var file = new StreamReader(PathConfig.TasksFile);
        var json = await file.ReadToEndAsync();
        return JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);
    }

    public void OverwriteTaskConfigFile(Dictionary<string, bool> tasksUpdated)
    {

        if (!Directory.Exists(PathConfig.DataFolder))
            Directory.CreateDirectory(PathConfig.DataFolder);

        if (!File.Exists(PathConfig.TasksFile))
            File.Create(PathConfig.TasksFile);

        var json = JsonConvert.SerializeObject(tasksUpdated, Formatting.Indented);

        using var file = new StreamWriter(PathConfig.TasksFile);
        file.Write(json);
    }
}
