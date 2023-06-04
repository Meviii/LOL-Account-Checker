using AccountChecker;
using AccountChecker.Data;
using AccountChecker.DataFiles;
using AccountChecker.Models;
using AccountChecker.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker.Utility;

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
        string folderPath = $"{PathConfig.ExportsFolder}";

        if (!Directory.Exists(folderPath))
            return null;

        var fileTasks = Directory.GetFiles(folderPath)
            .Where(filePath => filePath.EndsWith(".json"))
            .Select(async filePath =>
            {
                try
                {
                    string content = await File.ReadAllTextAsync(filePath);
                    return JObject.Parse(content);
                }
                catch
                {
                    return null;
                }
            });

        var accounts = (await Task.WhenAll(fileTasks))
            .Where(account => account != null)
            .ToList();

        return JsonConvert.DeserializeObject<List<Account>>(JsonConvert.SerializeObject(accounts));
    }

    // This method returns a List holding tuples of each account extracted from the combolist. In <User,Pass> format
    public async Task ReadAndAddComboListToQueue(string comboListPath, char delimiter)
    {
        // Check if the file exists, if not, return null
        if (!File.Exists(comboListPath))
            return;

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
                    // Split the line by the delimiter and add to queue
                    var accString = line.Split(delimiter);
                    AccountQueue.Enqueue(new AccountCombo()
                    {
                        Username = accString[0],
                        Password = accString[1]
                    });
                }
            }catch
            {
                Console.WriteLine($"Couldn't read account at {line}");
            }
        }

        return;
    }

    // This method creates the initial tasks configuration file for all tasks. 
    // Every task is defaulted to false (unchecked).
    public async void InitializeTaskConfigFileAsync()
    {
        // Create a dictionary with all task names as keys and set the default value to false.
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
            { TasksConfig.DisenchantWardSkinShards, false },
            { TasksConfig.RemoveFriendRequests, false },
            { TasksConfig.RemoveFriends, false }
        };

        // Create the data folder if it doesn't exist.
        if (!Directory.Exists(PathConfig.DataFolder))
            Directory.CreateDirectory(PathConfig.DataFolder);

        // Create the tasks file if it doesn't exist.
        if (!File.Exists(PathConfig.TasksFile))
            File.Create(PathConfig.TasksFile).Dispose();

        // Write the default task configuration to the tasks file.
        using var streamWriter = new StreamWriter(PathConfig.TasksFile);
        streamWriter.Write(JsonConvert.SerializeObject(tasksConfigDict, Formatting.Indented));
    }

    // This method reads the tasks configuration file and returns its content as a dictionary.
    public async Task<Dictionary<string, bool>> ReadFromTasksConfigFile()
    {
        // Open the tasks file and read its content.
        using var file = new StreamReader(PathConfig.TasksFile);
        var json = await file.ReadToEndAsync();

        // Deserialize the JSON string into a dictionary and return it.
        return JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);
    }

    // This method overwrites the tasks configuration file with a new dictionary.
    public void OverwriteTaskConfigFile(Dictionary<string, bool> tasksUpdated)
    {
        // Create the data folder if it doesn't exist.
        if (!Directory.Exists(PathConfig.DataFolder))
            Directory.CreateDirectory(PathConfig.DataFolder);

        // Create the tasks file if it doesn't exist.
        if (!File.Exists(PathConfig.TasksFile))
            File.Create(PathConfig.TasksFile);

        // Serialize the updated task dictionary into a JSON string.
        var json = JsonConvert.SerializeObject(tasksUpdated, Formatting.Indented);

        // Overwrite the tasks file with the updated task dictionary.
        using var file = new StreamWriter(PathConfig.TasksFile);
        file.Write(json);
    }

    // This method logs a message to a file.
    public void LogToFile(string filePath, string message)
    {
        // Create the full path of the log file.
        string fullPath = Path.Combine(PathConfig.LogsFolder, filePath);

        // Use a lock object to avoid race conditions when multiple threads try to access the same log file.
        lock (_lock)
        {
            // Create the logs folder if it doesn't exist.
            if (!Directory.Exists(PathConfig.LogsFolder))
                Directory.CreateDirectory(PathConfig.LogsFolder);

            // Create the log file if it doesn't exist.
            if (!File.Exists(fullPath))
                File.Create(fullPath).Dispose();

            // Append the message to the log file.
            File.AppendAllText(fullPath, message);
        }
    }


    // This method exports an Account object to a JSON file.
    // The file is saved to the Exports folder with the file name "{summonerName}.json".
    public async Task ExportAccount(Account account)
    {
        lock (_lock)
        {
            WriteToFile(account); // write to simple txt file

            // Build the file path for the JSON file based on the account's summoner name and the ExportsFolder directory.
            string filePath = $@"{PathConfig.ExportsFolder}{account.Username}.json";
            Console.WriteLine($"Saved to {filePath}");
            // Create the ExportsFolder directory if it doesn't exist.
            if (!Directory.Exists(PathConfig.ExportsFolder))
                Directory.CreateDirectory(PathConfig.ExportsFolder);

            // Create the JSON file if it doesn't exist and immediately dispose of the file stream to release the resources.
            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();

            // Serialize the Account object to a formatted JSON string.
            string json = JsonConvert.SerializeObject(account, Formatting.Indented);

            // Write the JSON string to the file.
            File.WriteAllText(filePath, json);
        }

    }

    private void WriteToFile(Account account)
    {
        lock (_lock)
        {
            // Build the file path for the txt file based on the account's username.
            string filePath = $@"{PathConfig.SimpleExportsFolder}{account.Username}.txt";

            // Create the directory if it doesn't exist.
            if (!Directory.Exists(PathConfig.SimpleExportsFolder))
                Directory.CreateDirectory(PathConfig.SimpleExportsFolder);

            // Create the JSON file if it doesn't exist and immediately dispose of the file stream to release the resources.
            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();

            // Write the JSON string to the file.
            File.WriteAllText(filePath, account.ToString());
        }
    }
}
