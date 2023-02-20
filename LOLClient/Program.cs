using LOLClient.UI;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;
namespace LOLClient;

class Program
{

    [STAThread]
    public static void Main(string[] args)
    {
        

        ApplicationConfiguration.Initialize();

        if (IsSettingsFileEmpty())
        {
            Application.Run(new InitialLoad());
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

    static void Work(string username, string password, int threadNumber)
    {

        // Runner
        Runner runner = new();

        bool didRiotSucceed = runner.RiotClientRunner(username, password, "H:\\Riot Games\\Riot Client\\RiotClientServices.exe");

        if (!didRiotSucceed)
        {
            runner.CleanUp();
            return;
        }

        runner.LeagueClientRunner("H:\\Riot Games\\League of Legends\\LeagueClient.exe");
        runner.CleanUp();
        
        Console.WriteLine($"Thread number {threadNumber} completed.");
    }

}