using AccountChecker.Tests;
using AccountChecker.DataFiles;
using AccountChecker.UI;
using AccountChecker.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker;

class Program
{

    [STAThread]
    public static void Main(string[] args)
    {
        // test
        //var test = new MainTest();
        //test.TestRequest().Wait();
        //return;

        ApplicationConfiguration.Initialize();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Check if settings file exists.
        if (IsSettingsFileEmpty().Result)
        { 
            // Run updates if settings file is empty (first run).
            Application.Run(new Settings(true));

        }
        else
        {
            // Run main form if settings file is populated.
            Application.Run(new Main());
        }

    }

    // Check if the settings.json file has any content.
    static async Task<bool> IsSettingsFileEmpty()
    {
        string filePath = $"{PathConfig.SettingsFile}";

        JObject settings;

        if (File.Exists(filePath))
        {
            string content = await File.ReadAllTextAsync(filePath);
            settings = JObject.Parse(content);

            if (settings.ContainsKey("RiotClientPath") &&
                settings.ContainsKey("LeagueClientPath"))
                return false;

            if (settings["RiotClientPath"] != null &&
                settings["LeagueClientPath"] != null)
                return false;
        }

        return true;
    }

}