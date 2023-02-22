using LOLClient.UI;
using LOLClient.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace LOLClient;

class Program
{

    [STAThread]
    public static void Main(string[] args)
    {

        //Runner runner = new();
        //var settings = new UIUtility().LoadFromSettingsFile();
        //runner.Work("mevismurf2", "", settings);

        ApplicationConfiguration.Initialize();

        if (IsSettingsFileEmpty())
        {
            Application.Run(new Settings());
        }
        else
        {
            Application.Run(new Main());
        }

    }

    static bool IsSettingsFileEmpty()
    {
        string filePath = @"..\..\..\Data\settings.json";

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