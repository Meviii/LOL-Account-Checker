using LOLClient.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient.UI;

public partial class Settings : Form
{

    private readonly UIUtility _utility;
    public Settings()
    {
        _utility = new UIUtility();
        InitializeComponent();
        LoadLabels();
    }

    private void LoadLabels()
    {
        var settings = _utility.LoadFromSettingsFile();

        if (settings == null)
            return;

        if (settings.ContainsKey("RiotClientPath"))
            RiotPathLabel.Text = settings["RiotClientPath"].ToString();

        if (settings.ContainsKey("LeagueClientPath"))
            LeaguePathLabel.Text = settings["LeagueClientPath"].ToString();
    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void BrowseRiotButton_Click(object sender, EventArgs e)
    {
        
        OpenFileDialog openFile = new();

        openFile.Filter = "Executable Files (*.exe)|*.exe";
        openFile.Title = "Select an Exe File.";

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFile.FileName;
            RiotPathLabel.Text = filePath;
            _utility.SaveToSettingsFile("RiotClientPath", filePath);
        }

    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        if (LeaguePathLabel.Text == "" || RiotPathLabel.Text == "")
        {
            StatusLabel.Text = "Please locate all paths.";
            StatusLabel.ForeColor = Color.MediumVioletRed;
            StatusLabel.Font = new Font("Segoe UI Light", 14F, FontStyle.Bold);
            return;
        }
        else
        {
            this.Hide();
            _utility.LoadMainView();
        }
    }

    private void LeagueBrowseButton_Click(object sender, EventArgs e)
    {

        OpenFileDialog openFile = new();

        openFile.Filter = "Executable Files (*.exe)|*.exe";
        openFile.Title = "Select an Exe File.";

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFile.FileName;
            LeaguePathLabel.Text = filePath;
            _utility.SaveToSettingsFile("LeagueClientPath",  filePath);
        }
    }
}
