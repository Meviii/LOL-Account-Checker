using AccountChecker.DataFiles;
using AccountChecker.Models;
using AccountChecker.UI;
using AccountChecker.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Windows.Forms;

namespace AccountChecker;

public partial class AccountList : Form
{
    private readonly UIUtility _uiUtility;
    private readonly CoreUtility _coreUtility;
    private List<Account> _accounts;
    private DataTable _originalAccountsDataTable;
    public AccountList()
    {
        _uiUtility = new UIUtility();
        _coreUtility = new CoreUtility();
        InitializeComponent();
        FillAccountsDataTableAsync();
        LoadComboBox();
    }

    private void LoadComboBox()
    {
        // Default
        RegionComboBox.Items.Add("None");
        RegionComboBox.SelectedIndex = 0;

        // Hard coded Regions. Better to use ENUM
        RegionComboBox.Items.Add("NA");
        RegionComboBox.Items.Add("EUW");
        RegionComboBox.Items.Add("OCE");
        RegionComboBox.Items.Add("EUNE");
        RegionComboBox.Items.Add("RU");
        RegionComboBox.Items.Add("TR");
        RegionComboBox.Items.Add("BR");
        RegionComboBox.Items.Add("LAS");
        RegionComboBox.Items.Add("LAN");
    }

    private async void FillAccountsDataTableAsync()
    {
        _accounts = await _coreUtility.LoadAccountsFromExportsFolderAsync();
        
        if (_accounts == null)
        {
            return;
        }

        var dataTable = new DataTable();
        dataTable.Columns.Add("Summoner");
        dataTable.Columns.Add("Region");
        dataTable.Columns.Add("Level", typeof(int));
        dataTable.Columns.Add("BE", typeof(int));
        dataTable.Columns.Add("RP", typeof(int));
        dataTable.Columns.Add("Rank");
        dataTable.Columns.Add("Skins", typeof(int));
        dataTable.Columns.Add("Champions", typeof(int));
        dataTable.Columns.Add("Verified");
        dataTable.Columns.Add("Last Game");


        dataTable.BeginLoadData();

        foreach (var account in _accounts)
        {
            dataTable.Rows.Add(
                account.SummonerName,
                account.Region,
                account.Level,
                account.BE,
                account.RP,
                $"{account.CurrentRank}",
                account.Skins.Count,
                account.Champions.Count,
                account.IsEmailVerified,
                account.LastPlayDate
                );
        }

        dataTable.EndLoadData();

        accountsGridView.DataSource = dataTable;
        _originalAccountsDataTable = dataTable;
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void accountsGridView_CellContentDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            // Get the row that was clicked
            DataGridViewRow row = accountsGridView.Rows[e.RowIndex];

            // Open the new form
            foreach (var account in _accounts)
            {
                if (row.Cells["Summoner"].Value.ToString() == account.SummonerName)
                {
                    _uiUtility.LoadSingleAccountView(account, this);
                    this.Hide();
                }
            }
        }
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        this.Hide();
        this.Close();
    }

    private void BackButton_Click(object sender, EventArgs e)
    {
        _uiUtility.LoadMainView(this);
        this.Hide();
    }

    private void ExportsFolderButton_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", PathConfig.ExportsFolder);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        FillAccountsDataTableAsync();
    }

    private void SearchTextBox_TextChanged(object sender, EventArgs e)
    {
        if (_originalAccountsDataTable == null)
            return;

        // Get the text entered in the search box
        string searchText = SearchTextBox.Text;

        accountsGridView.DataSource = _originalAccountsDataTable;

        DataTable dataTable = accountsGridView.DataSource as DataTable;

        // Filter the DataGridView based on the search text
        if (string.IsNullOrEmpty(searchText))
        {
            accountsGridView.DataSource = _originalAccountsDataTable;
        }
        else
        {

            // Filter the data based on the search text
            var rows = dataTable.Select($"Summoner LIKE '%{searchText}%'");
            if (rows.Length > 0)
            {
                // Show the filtered data
                accountsGridView.DataSource = rows.CopyToDataTable();
            }

        }
    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    // Filter by Region
    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    // Filter > Level 29 
    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (FilterLevel29AboveCheckBox.Checked)
        {
            if (!FilterLevel30BelowCheckBox.Checked)
            {
                FilterLevel30BelowCheckBox.Enabled = false;
            }
        }
        else
        {
            FilterLevel30BelowCheckBox.Enabled = true;
        }
    }

    private void FilterButton_Click(object sender, EventArgs e)
    {

        if (_originalAccountsDataTable == null)
            return;

        string filterQuery = "";

        accountsGridView.DataSource = _originalAccountsDataTable;

        DataTable dataTable = accountsGridView.DataSource as DataTable;

        // > Level 29 
        if (FilterLevel29AboveCheckBox.Checked)
        {
            filterQuery += "Level > 29";
        }

        // < Level 30
        if (FilterLevel30BelowCheckBox.Checked)
        {
            filterQuery += "Level < 30";
        }

        // Unverified
        if (FilterUnVerifiedCheckBox.Checked)
        {
            if (FilterLevel29AboveCheckBox.Checked || FilterLevel30BelowCheckBox.Checked)
            {
                filterQuery += " AND Verified LIKE 'False'";
            }
            else
            {
                filterQuery += "Verified LIKE 'False'";
            }
        }

        // Region
        // HARD CODED FOR PROPER REGION SELECTION
        string regionText = "";
        if (RegionComboBox.Text == "EUW")
        {
            regionText = "Europe";
        }
        else if (RegionComboBox.Text == "NA")
        {
            regionText = "North America";
        }
        else if (RegionComboBox.Text == "OCE")
        {
            regionText = "Oceania";
        }
        else if (RegionComboBox.Text == "EUNE")
        {
            regionText = "EU Nordic & East";
        }
        else if (RegionComboBox.Text == "RU")
        {
            regionText = "Russia";
        }
        else if (RegionComboBox.Text == "TR")
        {
            regionText = "Turkey";
        }
        else if (RegionComboBox.Text == "LAS")
        {
            regionText = "Latin America South";
        }
        else if (RegionComboBox.Text == "LAN")
        {
            regionText = "Latin America North";
        }
        else if (RegionComboBox.Text == "BR")
        {
            regionText = "Brazil";
        }

        // Check if default is selected
        if (RegionComboBox.SelectedIndex != 0)
        {
            if (FilterLevel29AboveCheckBox.Checked || 
                FilterLevel30BelowCheckBox.Checked || 
                FilterUnVerifiedCheckBox.Checked)
            {
                filterQuery += $" AND Region LIKE '%{regionText}%'";
            }
            else
            {
                filterQuery += $"Region LIKE '%{regionText}%'";
            }
        }

        var rows = dataTable.Select(filterQuery);

        // If no match found
        if (rows.Length == 0)
        {
            // Create an empty DataTable with the same schema
            DataTable emptyDataTable = dataTable.Clone();
            accountsGridView.DataSource = emptyDataTable;
            return;
        }

        // Show the filtered data
        accountsGridView.DataSource = rows.CopyToDataTable();

    }

    private void FilterLevel30BelowCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (FilterLevel30BelowCheckBox.Checked)
        {
            if (!FilterLevel29AboveCheckBox.Checked)
            {
                FilterLevel29AboveCheckBox.Enabled = false;
            }
        }
        else
        {
            FilterLevel29AboveCheckBox.Enabled = true;
        }
    }
}
