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
    private readonly Update _update;
    public Settings()
    {
        _update = new Update();
        _utility = new UIUtility();
        InitializeComponent();
        LoadLabels();
    }

    private async void LoadLabels()
    {
        var settings = await _utility.LoadFromSettingsFileAsync();

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
            _utility.SaveToSettingsFileAsync("RiotClientPath", filePath);
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
            _utility.SaveToSettingsFileAsync("LeagueClientPath",  filePath);
        }
    }

    private void updateButton_Click(object sender, EventArgs e)
    {
        updateButton.Enabled = false;
        _update.UpdateChampions();
        _update.UpdateSkins();
        updateButton.Enabled = true;
    }
}
