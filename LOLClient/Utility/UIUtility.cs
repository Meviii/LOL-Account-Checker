using LOLClient.DataFiles;
using LOLClient.Models;
using LOLClient.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LOLClient.Utility;

public class UIUtility
{
    public UIUtility() { }

    public string ReadFromFile(string path)
    {
        if (!File.Exists(path))
            return null;

        string content = File.ReadAllTextAsync(path).Result;
        Console.WriteLine(content);

        return content;
    }


    public void SaveToSettingsFile(string key, object value)
    {
        string filePath = $"{Config.SettingsFile}";

        JObject settings;

        if (!Directory.Exists($"{Config.DataFolder}"))
            Directory.CreateDirectory($"{Config.DataFolder}");

        if (File.Exists(filePath))
        {
            string content = File.ReadAllTextAsync(filePath).Result;
            settings = JObject.Parse(content);

            settings[key] = JToken.FromObject(value);

            File.WriteAllText(filePath, settings.ToString());
        }
        else
        {
            settings = new();

            settings[key] = JToken.FromObject(value);

            File.WriteAllText(filePath, settings.ToString());
        }
    }

    public JObject LoadFromSettingsFile()
    {
        JObject settings;
        string filePath = $"{Config.SettingsFile}";

        if (File.Exists(filePath))
        {
            string content = File.ReadAllTextAsync(filePath).Result;
            settings = JObject.Parse(content);

            return settings;
        }

        return null;
    }

    public List<Account> LoadAccountsFromExportsFolder()
    {
        JArray accounts = new();
        string folderPath = $"{Config.ExportsFolder}";

        foreach (string filePath in Directory.GetFiles(folderPath))
        {

            if (File.Exists(filePath) && filePath.EndsWith(".json"))
            {
                try
                {
                    string content = File.ReadAllTextAsync(filePath).Result;
                    var account = JObject.Parse(content);

                    accounts.Add(account);

                }catch
                {
                    continue;
                }
            }
        }

        return JsonConvert.DeserializeObject<List<Account>>(accounts.ToString());
    }


    public bool IsDigit(object value)
    {

        if (value == null) return false;


        if (int.TryParse(value.ToString(), out int digit))
            return true;

        if (value.GetType() == typeof(int)) return true;

        return false;
    }

    public void LoadMainView()
    {

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


    public void LoadSingleAccountView(Account account)
    {
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(SingleAccount))
            {
                form.Close();
            }
        }

        new SingleAccount(account).Show();
    }
    public void LoadAccountsListView()
    {

        bool isCreatedAlready = false;
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(AccountList))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.Visible = true;
                isCreatedAlready = true;
            }
        }

        if (!isCreatedAlready)
            new AccountList().Show();

    }

    public void LoadSettingsViewAsDialog()
    {
        new Settings().ShowDialog();
    }
}
