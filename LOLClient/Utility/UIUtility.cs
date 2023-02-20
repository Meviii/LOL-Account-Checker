using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient.Utility;

public class UIUtility
{

    public UIUtility() { }

    
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
        new Main().Show();
    }

}
