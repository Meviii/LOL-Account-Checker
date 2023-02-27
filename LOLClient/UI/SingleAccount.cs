using LOLClient.Models;
using LOLClient.UI;
using LOLClient.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
