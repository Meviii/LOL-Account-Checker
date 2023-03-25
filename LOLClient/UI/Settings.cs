using AccountChecker.Data;
using AccountChecker.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker.UI;

public partial class Settings : Form
{

    private readonly UIUtility _uiUtility;
    private readonly CoreUtility _coreUtility;
    private readonly Update _update;
    public Settings(bool initialRun = false)
    {
        _update = new Update();
        _uiUtility = new UIUtility();
        _coreUtility = new CoreUtility();

        InitializeComponent();
        LoadLabelsAsync();

        // Initialize config and skins/champs data on first run
        if (initialRun)
        {
            UpdateButton_Click(updateButton, null);

            _coreUtility.InitializeTaskConfigFileAsync();
        }

    }

    private void LoadLabelsAsync()
    {
        var settings = _coreUtility.LoadFromSettingsFile();

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

    private async void BrowseRiotButton_Click(object sender, EventArgs e)
    {

        OpenFileDialog openFile = new()
        {
            Filter = "Executable Files (*.exe)|*.exe",
            Title = "Select the RiotClientServices.exe File."
        };

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFile.FileName;
            RiotPathLabel.Text = filePath;
            await _coreUtility.SaveToSettingsFileAsync("RiotClientPath", filePath);
        }

    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        if (LeaguePathLabel.Text == "" || RiotPathLabel.Text == "")
        {
            StatusLabel.Text = "Please locate all paths.";
            StatusLabel.ForeColor = Color.Black;
            StatusLabel.Font = new Font("Segoe UI Light", 14F, FontStyle.Regular);
            return;
        }
        else
        {
            StatusLabel.Text = "";
            _uiUtility.LoadMainView();
            this.Hide();
        }
    }

    private async void LeagueBrowseButton_Click(object sender, EventArgs e)
    {

        OpenFileDialog openFile = new();

        openFile.Filter = "Executable Files (*.exe)|*.exe";
        openFile.Title = "Select an LeagueClient.exe File.";

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFile.FileName;
            LeaguePathLabel.Text = filePath;
            await _coreUtility.SaveToSettingsFileAsync("LeagueClientPath", filePath);
        }
    }

    private async void UpdateButton_Click(object sender, EventArgs e)
    {
        updateButton.Enabled = false;
        await _update.UpdateChampionsAsync();
        await _update.UpdateSkinsAsync();
        updateButton.Enabled = true;
    }

    private void Settings_Load(object sender, EventArgs e)
    {

    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        this.Hide();

        // if main form exists, only hide settings dialog
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(Main))
            {
                return;
            }
        }

        // if main form doesnt exist, close app
        Application.Exit();

    }
}
