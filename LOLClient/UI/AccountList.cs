using LOLClient.Models;
using LOLClient.UI;
using LOLClient.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace LOLClient;

public partial class AccountList : Form
{
    private readonly UIUtility _uiUtility;
    private readonly CoreUtility _coreUtility;
    private List<Account> _accounts;

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
        if (accounts == null )
        {
            return;
        }

        var dataTable = new DataTable();
        dataTable.Columns.Add("Summoner");
        dataTable.Columns.Add("Level");
        dataTable.Columns.Add("BE");
        dataTable.Columns.Add("RP");
        dataTable.Columns.Add("OE");
        dataTable.Columns.Add("Skins");
        dataTable.Columns.Add("Champions");
        dataTable.Columns.Add("Verified");


        foreach (var account in accounts)
        {
            dataTable.Rows.Add(
                account.SummonerName,
                account.Level,
                account.BE,
                account.RP,
                account.OE,
                account.Skins.Count,
                account.Champions.Count,
                account.IsEmailVerified
                );
        }

        accountsGridView.DataSource = dataTable;

    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void accountsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) // Check if the clicked row is valid
        {
            DataGridViewRow row = accountsGridView.Rows[e.RowIndex];

            foreach (var account in _accounts)
            {
                if (row.Cells["Summoner"].Value.ToString() == account.SummonerName)
                {
                    this.Hide();
                    _uiUtility.LoadSingleAccountView(account);
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
        this.Hide();

        _uiUtility.LoadMainView();
    }
}
