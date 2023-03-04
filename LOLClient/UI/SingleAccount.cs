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

namespace LOLClient;

public partial class SingleAccount : Form
{
    private readonly UIUtility _uiUtility;
    private readonly Account _account;

    public SingleAccount(Account account)
    {
        _account = account;
        _uiUtility = new UIUtility();
        InitializeComponent();
        InitializeView();
    }

    private void InitializeView()
    {
        accountNameTitle.Text = _account.SummonerName;
        FillChampionsTable();
        FillSkinsTable();
    }

    private void FillOverviewTable()
    {

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

    private void BackButton_Click(object sender, EventArgs e)
    {
        this.Hide();

        _uiUtility.LoadMainView();
    }

    private void championFlowLayout_Paint(object sender, PaintEventArgs e)
    {

    }
}
