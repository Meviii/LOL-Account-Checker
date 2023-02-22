﻿using LOLClient.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient.Utility;

public class UIUtility
{

    public UIUtility() { }

    public string ReadFromFile(string path)
    {
        if (!File.Exists(path))
            return null;

        string content = File.ReadAllTextAsync(@"C:\Users\Mevi\Desktop\acombo.txt").Result;
        Console.WriteLine(content);

        return content;
    }
    public void SaveToSettingsFile(string key, object value)
    {
        string filePath = @"..\..\..\Data\settings.json";

        JObject settings;

        if (!Directory.Exists(@"..\..\..\Data\"))
            Directory.CreateDirectory(@"..\..\..\Data\");

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
        string filePath = @"..\..\..\Data\settings.json";

        if (File.Exists(filePath))
        {
            string content = File.ReadAllTextAsync(filePath).Result;
            settings = JObject.Parse(content);

            return settings;
        }

        return null;
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
                form.Focus();
                return;
            }
        }

        new Main().Show();
    }

    public void LoadSettingsViewAsDialog()
    {
        new Settings().ShowDialog();
    }
}
