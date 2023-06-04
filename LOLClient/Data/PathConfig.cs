using System;

namespace AccountChecker.DataFiles;

// This class contains the paths to the different folders and files used by the application
public class PathConfig
{
    public static string DataFolder = @$"{AppDomain.CurrentDomain.BaseDirectory}DataFiles\";
    public static string ExportsFolder = @$"{AppDomain.CurrentDomain.BaseDirectory}Exports\";
    public static string SimpleExportsFolder = @$"{AppDomain.CurrentDomain.BaseDirectory}SimpleExports\";
    public static string LogsFolder = $@"{AppDomain.CurrentDomain.BaseDirectory}Logs\";
    public static string SettingsFile = $@"{DataFolder}settings.json";
    public static string SkinsFile = @$"{DataFolder}skins.json";
    public static string ChampionsFile = @$"{DataFolder}champions.json";
    public static string TasksFile = $@"{DataFolder}tasks.json";

}
