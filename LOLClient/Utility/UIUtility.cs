using LOLClient.DataFiles;
using LOLClient.Models;
using LOLClient.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient.Utility;

/*
 * This class provides utility methods for the application UI.
 */
public class UIUtility
{
    public UIUtility() { }

    // This method saves a key-value pair to a JSON settings file
    public async void SaveToSettingsFileAsync(string key, object value)
    {
        // Get the file path of the settings file
        string filePath = $"{Config.SettingsFile}";

        // Declare a JObject variable to hold the settings
        JObject settings;

        // Check if the data folder exists; if not, create it
        if (!Directory.Exists($"{Config.DataFolder}"))
            Directory.CreateDirectory($"{Config.DataFolder}");

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
    public async Task<JObject> LoadFromSettingsFileAsync()
    {
        // Declare a JObject variable to hold the settings
        JObject settings;

        // Get the file path of the settings file
        string filePath = $"{Config.SettingsFile}";

        // If the settings file exists, load its contents and return them as a JObject
        if (File.Exists(filePath))
        {
            // Read the contents of the file asynchronously and store them in a string variable
            string content = await File.ReadAllTextAsync(filePath);

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
        string folderPath = $"{Config.ExportsFolder}";

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

    // This method is in charge of switching to (or creating) the Main form.
    public void LoadMainView()
    {
        // Loops over each active form and makes it visible, else, creates new instance of the form
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(Main))
            {

                form.Visible = true;
                form.Focus();
                return;
            }
        }

        new Main().Show();
    }

    // This method is in charge of switching to the Single Account form.
    public void LoadSingleAccountView(Account account)
    {
        // Loops over each form and closes the existant Single Account form and creates a new instance.
        // This is to ensure that an updated account of the current application instance is also updated in this view.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(SingleAccount))
            {
                form.Close();
            }
        }

        new SingleAccount(account).Show();
    }

    // This method is in charge of switching to the Accounts List form.
    public void LoadAccountsListView()
    {
        // Loops over active forms and closes the instance of an AccountList if initiated.
        // This is to ensure that a new account that is checked is also visible in the view.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(AccountList))
            {
                form.Hide();
            }
        }
        
        new AccountList().Show();

    }

    // This method is responsible of opening the Settings Form as a Dialog.
    public void LoadSettingsViewAsDialog()
    {

        // Loops over active forms and shows (or creates) the Settings form.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(Settings))
            {
                form.Visible = true;
                form.Show();
                return;
            }
        }

        new Settings().ShowDialog();
    }
}
