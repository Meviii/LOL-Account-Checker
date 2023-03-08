using LOLClient.Models;
using LOLClient.UI;
using LOLClient.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Azure.Core.HttpHeader;

namespace LOLClient;

public partial class SingleAccount : Form
{
    private readonly UIUtility _uiUtility;
    private readonly Account _account;
    private readonly CoreUtility _coreUtility;

    public SingleAccount(Account account)
    {
        _account = account;
        _uiUtility = new UIUtility();
        _coreUtility = new CoreUtility();
        InitializeComponent();
        InitializeView();
    }

    private void InitializeView()
    {
        accountNameTitle.Text = _account.SummonerName;
        FillChampionsTable();
        FillSkinsTable();
        FillOverviewTable();
    }

    private Dictionary<string, bool> GetTaskCheckBoxes()
    {
        return new Dictionary<string, bool>()
        {
            {CraftKeysCheckBox.Name, CraftKeysCheckBox.Checked},
            {OpenChestsCheckBox.Name, OpenChestsCheckBox.Checked},
            {DisenchantChampionShardsCheckBox.Name, DisenchantChampionShardsCheckBox.Checked},
            {DisenchantEternalShardsCheckBox.Name, DisenchantEternalShardsCheckBox.Checked},
            {OpenCapsulesOrbsShardsCheckBox.Name, OpenCapsulesOrbsShardsCheckBox.Checked}
        };
    }

    private void FillOverviewTable()
    {

        var dataTable = new DataTable();

        dataTable.Columns.Add("Properties");
        dataTable.Columns.Add(_account.SummonerName);

        foreach (var propertyInfo in _account.GetType().GetProperties())
        {
            if (propertyInfo.PropertyType == typeof(List<Skin>) || propertyInfo.PropertyType == typeof(List<Champion>))
            {
                if (propertyInfo.PropertyType == typeof(List<Skin>))
                    dataTable.Rows.Add(propertyInfo.Name, ((List<Skin>)propertyInfo.GetValue(_account, null)).Count);

                if (propertyInfo.PropertyType == typeof(List<Champion>))
                    dataTable.Rows.Add(propertyInfo.Name, ((List<Champion>)propertyInfo.GetValue(_account, null)).Count);
            }
            else
            {
                dataTable.Rows.Add(propertyInfo.Name, propertyInfo.GetValue(_account, null));
            }
        }

        overviewGridView.DataSource = dataTable;
    }

    private void FillChampionsTable()
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("Name");
        dataTable.Columns.Add("Purchase Date");

        foreach (var champ in _account.Champions)
        {
            dataTable.Rows.Add(
                champ.Name,
                champ.PurchaseDate.ToString()
                );
        }

        championGridView.DataSource = dataTable;
    }

    private void FillSkinsTable()
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("Name");
        dataTable.Columns.Add("Purchase Date");

        foreach (var skin in _account.Skins)
        {
            dataTable.Rows.Add(
                skin.Name,
                skin.PurchaseDate.ToString()
                );
        }

        skinsGridView.DataSource = dataTable;
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        this.Hide();
        this.Close();
    }

    private void championFlowLayout_Paint(object sender, PaintEventArgs e)
    {

    }

    private void BackButton_Click_1(object sender, EventArgs e)
    {
        this.Hide();

        _uiUtility.LoadAccountsListView();
    }

    private void label3_Click(object sender, EventArgs e)
    {

    }

    private void label6_Click(object sender, EventArgs e)
    {

    }

    private void label8_Click(object sender, EventArgs e)
    {

    }

    private void Overview_Click(object sender, EventArgs e)
    {

    }

    private async void ExecuteButton_Click(object sender, EventArgs e)
    {
        return;// TODO

        if (!AreTasksSelected())
            return;

        ProgressBar.Visible = true;
        _uiUtility.InitializeProgressBar(ProgressBar, 1 + 1); // 1 account + 1 for functional progress bar

        var settings = await _coreUtility.LoadFromSettingsFileAsync(); // Gets Settings file

        // Run the Work method as a Task
        var task = Task.Run(async () =>
        {
            var runner = new Runner();

            // Job of thread
            await runner.Job_ExecuteTasksWithoutAccountFetching(_account.Username, _account.Password, settings, GetTaskCheckBoxes());

        });

        _uiUtility.IncrementProgressBar(ProgressBar);
        ProgressBar.Visible = false;
    }

    private bool AreTasksSelected()
    {
        foreach (var task in GetTaskCheckBoxes())
        {
            if (task.Value == true) return true;
        }

        return false;
    }
}
