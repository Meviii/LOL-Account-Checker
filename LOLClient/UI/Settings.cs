﻿using LOLClient.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LOLClient.UI;

public partial class Settings : Form
{

    private readonly UIUtility _uiUtility;
    private readonly CoreUtility _coreUtility;
    private readonly Update _update;
    public Settings()
    {
        _update = new Update();
        _uiUtility = new UIUtility();
        _coreUtility = new CoreUtility();
        InitializeComponent();
        LoadLabels();
    }

    private async void LoadLabels()
    {
        var settings = await _coreUtility.LoadFromSettingsFileAsync();

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

        OpenFileDialog openFile = new()
        {
            Filter = "Executable Files (*.exe)|*.exe",
            Title = "Select the RiotClientServices.exe File."
        };

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFile.FileName;
            RiotPathLabel.Text = filePath;
            _coreUtility.SaveToSettingsFileAsync("RiotClientPath", filePath);
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
            _uiUtility.LoadMainView();
        }
    }

    private void LeagueBrowseButton_Click(object sender, EventArgs e)
    {

        OpenFileDialog openFile = new();

        openFile.Filter = "Executable Files (*.exe)|*.exe";
        openFile.Title = "Select an LeagueClient.exe File.";

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFile.FileName;
            LeaguePathLabel.Text = filePath;
            _coreUtility.SaveToSettingsFileAsync("LeagueClientPath",  filePath);
        }
    }

    private void updateButton_Click(object sender, EventArgs e)
    {
        updateButton.Enabled = false;
        _update.UpdateChampions();
        _update.UpdateSkins();
        updateButton.Enabled = true;
    }

    private void Settings_Load(object sender, EventArgs e)
    {

    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        this.Hide();
    }
}
