using System;

namespace LOLClient.DataFiles;

public class Config
{
    public static string DataFolder = @$"{AppDomain.CurrentDomain.BaseDirectory}DataFiles\";
    public static string ExportsFolder = @$"{AppDomain.CurrentDomain.BaseDirectory}Exports\";
    public static string SettingsFile = $@"{DataFolder}settings.json";
    public static string SkinsFile = @$"{DataFolder}skins.json";
    public static string ChampionsFile = @$"{DataFolder}champions.json";
}
