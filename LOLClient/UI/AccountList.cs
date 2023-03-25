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
    }

    private async void FillAccountsDataTableAsync()
    {
        var accounts = await _coreUtility.LoadAccountsFromExportsFolderAsync();
        _accounts = accounts;
        if (accounts == null)
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


        foreach (var account in accounts)
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
            else
            {
            }
        }
    }

}
