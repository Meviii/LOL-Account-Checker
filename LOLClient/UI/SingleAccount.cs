using AccountChecker;
using AccountChecker.Data;
using AccountChecker.Models;
using AccountChecker.UI;
using AccountChecker.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker;

public partial class SingleAccount : Form
{
    private readonly UIUtility _uiUtility;
    private readonly Account _account;
    private readonly CoreUtility _coreUtility;
    private DataTable _originalChampionDataTable;
    private DataTable _originalSkinsDataTable;

    public SingleAccount(Account account)
    {
        _account = account;
        _uiUtility = new UIUtility();
        _coreUtility = new CoreUtility();
        InitializeComponent();
        InitializeView();

        // TEMP TILL IMPLEMENTED
        BuyChampionShardsCheckBox.Enabled = false;
        BuyBlueEssenceCheckBox.Enabled = false;

    }

    private void InitializeView()
    {
        accountNameTitle.Text = _account.SummonerName;
        FillChampionsTable();
        FillSkinsTable();
        FillOverviewTable();
        LoadTaskConfigAsync();
    }

    private async void LoadTaskConfigAsync()
    {
        var tasks = await _coreUtility.ReadFromTasksConfigFile();

        foreach (var field in typeof(TasksConfig).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.FieldType != typeof(string))
            {
                continue;
            }

            if (Controls.Find(field.GetValue(null).ToString(), true).FirstOrDefault() is not CheckBox checkBox)
            {
                continue;
            }

            if (tasks.TryGetValue(field.GetValue(null).ToString(), out bool value))
            {
                checkBox.Checked = value;
            }
        }
    }

    private Dictionary<string, bool> GetTaskCheckBoxes()
    {
        return new Dictionary<string, bool>()
        {
            {CraftKeysCheckBox.Name, CraftKeysCheckBox.Checked},
            {OpenChestsCheckBox.Name, OpenChestsCheckBox.Checked},
            {DisenchantChampionShardsCheckBox.Name, DisenchantChampionShardsCheckBox.Checked},
            {DisenchantEternalShardsCheckBox.Name, DisenchantEternalShardsCheckBox.Checked},
            {OpenCapsulesOrbsShardsCheckBox.Name, OpenCapsulesOrbsShardsCheckBox.Checked},
            {DisenchantSkinShardsCheckBox.Name, DisenchantSkinShardsCheckBox.Checked},
            {BuyChampionShardsCheckBox.Name, BuyChampionShardsCheckBox.Checked},
            {ClaimEventRewardsCheckBox.Name, ClaimEventRewardsCheckBox.Checked},
            {BuyBlueEssenceCheckBox.Name, BuyBlueEssenceCheckBox.Checked },
            {DisenchantWardSkinShardsCheckBox.Name, DisenchantWardSkinShardsCheckBox.Checked},
            {RemoveFriendRequestsCheckBox.Name, RemoveFriendRequestsCheckBox.Checked},
            {RemoveFriendsCheckBox.Name, RemoveFriendsCheckBox.Checked}

        };
    }

    private void FillOverviewTable()
    {

        var dataTable = new DataTable();

        dataTable.Columns.Add("Properties");
        dataTable.Columns.Add(_account.SummonerName);

        foreach (var propertyInfo in _account.GetType().GetProperties())
        {
            if (propertyInfo.PropertyType == typeof(List<Skin>) ||
                propertyInfo.PropertyType == typeof(List<Champion>) ||
                propertyInfo.PropertyType == typeof(List<Friend>) ||
                propertyInfo.PropertyType == typeof(Rank) ||
                propertyInfo.PropertyType == typeof(HonorLevel)
                )
            {
                if (propertyInfo.PropertyType == typeof(List<Skin>))
                    dataTable.Rows.Add(propertyInfo.Name, ((List<Skin>)propertyInfo.GetValue(_account, null)).Count);

                if (propertyInfo.PropertyType == typeof(List<Champion>))
                    dataTable.Rows.Add(propertyInfo.Name, ((List<Champion>)propertyInfo.GetValue(_account, null)).Count);

                if (propertyInfo.PropertyType == typeof(Rank))
                    dataTable.Rows.Add(propertyInfo.Name, ((Rank)propertyInfo.GetValue(_account, null)).ToString());

                if (propertyInfo.PropertyType == typeof(HonorLevel))
                    dataTable.Rows.Add(propertyInfo.Name, ((HonorLevel)propertyInfo.GetValue(_account, null)).ToString());

                if (propertyInfo.PropertyType == typeof(List<Friend>))
                    dataTable.Rows.Add(propertyInfo.Name, ((List<Friend>)propertyInfo.GetValue(_account, null)).Count);
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
        dataTable.Columns.Add("Purchase Date", typeof(DateTime));

        foreach (var champ in _account.Champions)
        {
            dataTable.Rows.Add(
                champ.Name,
                champ.PurchaseDate.ToString()
                );
        }

        championGridView.DataSource = dataTable;
        _originalChampionDataTable = dataTable;
    }

    private void FillSkinsTable()
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("Name");
        dataTable.Columns.Add("Purchase Date", typeof(DateTime));

        foreach (var skin in _account.Skins)
        {
            dataTable.Rows.Add(
                skin.Name,
                skin.PurchaseDate.ToString()
                );
        }

        skinsGridView.DataSource = dataTable;
        _originalSkinsDataTable = dataTable;
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
        //this.Hide();

        _uiUtility.LoadAccountsListView(this);
        this.Hide();
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
        // Disable Execute button
        executeButton.Enabled = false;
        executeButton.Text = "Executing...";
        BackButton.Enabled = false;

        // Get Tasks
        var tasks = GetTaskCheckBoxes();

        // Update Task file
        _coreUtility.OverwriteTaskConfigFile(tasks);

        // Check if any tasks are selected
        if (!AreTasksSelected())
            return;

        ProgressBar.Visible = true;
        ProgressBar.Minimum = 0;
        ProgressBar.Maximum = 2;

        var settings = _coreUtility.LoadFromSettingsFile(); // Gets Settings file

        // Run the Work method as a Task
        var task = Task.Run(async () =>
        {
            var runner = new Runner();

            _uiUtility.IncrementProgressBar(ProgressBar);

            // Job of thread
            var isSuccess = await runner.Job_AccountFetchingWithTasks(new AccountCombo()
            {
                Username = _account.Username,
                Password = _account.Password,
            }, settings);

            if (isSuccess)
            {
                MessageBox.Show("Completed Successfully!");
            }
            else
            {
                MessageBox.Show("Failed. Please try again.");
            }
        });

        await task;

        //_uiUtility.IncrementProgressBar(ProgressBar);
        executeButton.Enabled = true;
        ProgressBar.Visible = false;
        executeButton.Text = "Execute";
        BackButton.Enabled = true;
    }

    private bool AreTasksSelected()
    {
        foreach (var task in GetTaskCheckBoxes())
        {
            if (task.Value == true) return true;
        }

        return false;
    }

    private void TaskCheckedChanged(object sender, EventArgs e)
    {

    }

    private void ChampionsSearchTextBox_TextChanged(object sender, EventArgs e)
    {

        if (_originalChampionDataTable == null)
            return;

        // Get the text entered in the search box
        string searchText = ChampionsSearchTextBox.Text;

        championGridView.DataSource = _originalChampionDataTable;

        DataTable dataTable = championGridView.DataSource as DataTable;

        // Filter the DataGridView based on the search text
        if (string.IsNullOrEmpty(searchText))
        {
            championGridView.DataSource = _originalChampionDataTable;
        }
        else
        {

            // Filter the data based on the search text
            var rows = dataTable.Select($"Name LIKE '%{searchText}%'");
            if (rows.Length > 0)
            {
                // Show the filtered data
                championGridView.DataSource = rows.CopyToDataTable();
            }
            else
            {
            }
        }
    }

    private void SkinsSearchTextBox_TextChanged(object sender, EventArgs e)
    {

        if (_originalSkinsDataTable == null)
            return;

        // Get the text entered in the search box
        string searchText = SkinsSearchTextBox.Text;

        skinsGridView.DataSource = _originalSkinsDataTable;

        DataTable dataTable = skinsGridView.DataSource as DataTable;

        // Filter the DataGridView based on the search text
        if (string.IsNullOrEmpty(searchText))
        {
            skinsGridView.DataSource = _originalSkinsDataTable;
        }
        else
        {

            // Filter the data based on the search text
            var rows = dataTable.Select($"Name LIKE '%{searchText}%'");
            if (rows.Length > 0)
            {
                // Show the filtered data
                skinsGridView.DataSource = rows.CopyToDataTable();
            }
            else
            {
            }
        }
    }

    private void progressBar1_Click(object sender, EventArgs e)
    {

    }

    private void tabPage1_Click(object sender, EventArgs e)
    {

    }
}
