using LOLClient.UI;
using LOLClient.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient;

public partial class Main : Form
{

    private readonly UIUtility _uIUtility;
    public Main()
    {
        _uIUtility = new UIUtility();

        InitializeComponent();
        LoadConfigAsync();

    }


    private async void LoadConfigAsync()
    {
        var settings = await _uIUtility.LoadFromSettingsFileAsync();

        if (settings.ContainsKey("ComboListPath"))
            ComboListText.Text = settings["ComboListPath"].ToString();

        Console.SetOut(new ConsoleWriter(ConsoleTextBox));
    }

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {
        ConsoleTextBox.SelectionStart = ConsoleTextBox.Text.Length;
        ConsoleTextBox.ScrollToCaret();
    }

    private void SettingsButton_Click(object sender, EventArgs e)
    {
        
        _uIUtility.LoadSettingsViewAsDialog();
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void label3_Click(object sender, EventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void ComboLoadButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFile = new();

        openFile.Filter = "Text Files (*.txt)|*.txt";
        openFile.Title = "Select a TXT File.";

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFile.FileName;

            ComboListText.Text = filePath;
            _uIUtility.SaveToSettingsFileAsync("ComboListPath", filePath);
        }

    }

    private void ThreadCountTextBox_TextChanged(object sender, KeyPressEventArgs e)
    {
        if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
        {
            e.Handled = true; 
        }
    }

    private void DelimiterTextBox_OnLeave(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (textBox.Text == "")
        {
            textBox.Text = ":";
        }
    }

    private async void StartOrStopButton_Click(object sender, EventArgs e)
    {
        var button = (Button)sender;

        if (button.Text == "Start")
        {

            button.Text = "Started";
            button.Enabled = false;
            button.ForeColor = System.Drawing.Color.Green;
            ProgressBar.Visible = true;

            // handle if user doesnt enter a thread count
            int actualThreadCount;

            if (Convert.ToString(ThreadCountTextBox.Text) == "")
            {
                actualThreadCount = 1;
            }
            else
            {

                actualThreadCount = int.Parse(ThreadCountTextBox.Text);
            }

            var delimiter = Convert.ToChar(DelimiterTextBox.Text);

            try
            {
                await Task.Run(async () =>
                {
                    await InitRunnerAsync(actualThreadCount, delimiter);

                });

                ConsoleTextBox.Clear();
                Console.WriteLine("Tasks Completed.");
                ProgressBar.Value = 0;
            }
            catch
            {
                Console.WriteLine("Tasks Unexpectedly Stopped.");
            }

            
            button.Text = "Start";
            ProgressBar.Visible = false;
            button.ForeColor = System.Drawing.Color.Black;
        }

    }

    private async Task InitRunnerAsync(int actualThreadCount, char delimiter)
    {
        var runner = new Runner();

        runner.AccountsLeftUpdated += Runner_AccountsLeftUpdated;
        var totalAccounts = runner.ReadComboList(ComboListText.Text, delimiter).Count;
        try
        {
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = totalAccounts + 1;
        }catch { }

        await runner.RunOperationAsync(actualThreadCount,
            Convert.ToChar(delimiter));

    }

    private void Runner_AccountsLeftUpdated(object sender, int accountsLeft)
    {

        if (accountsLeftLabel.InvokeRequired || ProgressBar.InvokeRequired)
        {
            accountsLeftLabel.Invoke(new Action(() => Runner_AccountsLeftUpdated(sender, accountsLeft)));
        }
        else
        {
            accountsLeftLabel.Text = $"{accountsLeft}";
            ProgressBar.Value += 1;
        }

    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        this.Hide();
        this.Close();
    }

    private void ComboListText_TextChanged(object sender, EventArgs e)
    {

    }

    private void accountsListButton_Click(object sender, EventArgs e)
    {
        this.Hide();
        _uIUtility.LoadAccountsListView();
    }

    private void Main_Load(object sender, EventArgs e)
    {

    }
}
