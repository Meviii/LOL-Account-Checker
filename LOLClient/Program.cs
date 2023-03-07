using LOLClient.DataFiles;
using LOLClient.UI;
using LOLClient.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient;

class Program
{

    [STAThread]
    public static void Main(string[] args)
    {
        
        ApplicationConfiguration.Initialize();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Check if settings file exists.
        if (IsSettingsFileEmpty())
        {
            // Run updates if settings file is empty (first run).
            var update = new Update();
            var updateTask = Task.Run(async () =>
            {
                await update.UpdateSkins();
                await update.UpdateChampions();
            });

            // Run Settings form.
            Application.Run(new Settings());
        }
        else
        {
            // Run main form if settings file is populated.
            Application.Run(new Main());
        }

    }

    static bool IsSettingsFileEmpty()
    {
        string filePath = $"{Config.SettingsFile}";

        JObject settings;

        if (File.Exists(filePath))
        {
            string content = File.ReadAllTextAsync(filePath).Result;
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