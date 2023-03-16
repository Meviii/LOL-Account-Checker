using AccountChecker;
using AccountChecker.Models;
using AccountChecker.Connections;
using AccountChecker.Utility;
using Microsoft.EntityFrameworkCore.Update;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Azure.Core.HttpHeader;

namespace AccountChecker;

public partial class Main : Form
{

    private readonly CoreUtility _coreUtility;
    private readonly UIUtility _uIUtility;
    private static readonly object comboListLock = new();

    public Main()
    {
        _uIUtility = new UIUtility();
        _coreUtility = new CoreUtility();
        InitializeComponent();
        LoadConfigAsync();

    }

    private void LoadConfigAsync()
    {
        var settings = _coreUtility.LoadFromSettingsFile();

        if (settings == null)
            return;

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

    private async void ComboLoadButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFile = new();

        openFile.Filter = "Text Files (*.txt)|*.txt";
        openFile.Title = "Select a TXT File.";

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFile.FileName;

            ComboListText.Text = filePath;
            await _coreUtility.SaveToSettingsFileAsync("ComboListPath", filePath);
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

    }

    private char ValidateDelimiter()
    {
        if (DelimiterTextBox.Text != "")
        {
            return Convert.ToChar(DelimiterTextBox.Text);
        }

        return ':';
    }

    private async void StartOrStopButton_Click(object sender, EventArgs e)
    {
        var button = (Button)sender;

        if (button.Text == "Start")
        {
            // Started operation
            StartButtonOperation(button);

            // handler if user doesnt enter a thread count or delimiter
            int threadCount = ValidateThreadCount();
            char delimiter = ValidateDelimiter();

            // Runs a new thread to start the operation to prevent Main form from freezing
            await Task.Run(async () =>
            {
                await InitRunnerAsync(threadCount, delimiter);

            });

            // Once tasks are complete
            ConsoleTextBox.Clear(); // Clears Console
            Console.WriteLine("Tasks Completed.");

            // Returns to default settings
            button.Text = "Start";
            button.Enabled = true;
            ProgressBar.Visible = false;
            button.ForeColor = System.Drawing.Color.Black;
        }

    }

    // This method updates the Start button with new values to represent the work has started.
    private void StartButtonOperation(Button button)
    {
        button.Text = "Started";
        button.Enabled = false;
        button.ForeColor = System.Drawing.Color.Green;
        ProgressBar.Visible = true;
    }

    // This method validates the entered (or default) thread count
    private int ValidateThreadCount()
    {
        int actualThreadCount;

        if (Convert.ToString(ThreadCountTextBox.Text) == "")
        {
            actualThreadCount = 1;
        }
        else
        {
            actualThreadCount = int.Parse(ThreadCountTextBox.Text);
        }

        return actualThreadCount;
    }

    private async Task InitRunnerAsync(int actualThreadCount, char delimiter)
    {

        var settings = _coreUtility.LoadFromSettingsFile(); // Gets Settings file

        await _coreUtility.ReadAndAddComboListToQueue(ComboListText.Text, delimiter); // Reads the combolist(accounts) path

        // Update accounts left and progress bar 
        UpdateProgress(AccountQueue.Count().ToString());

        // Initializes progress bar with min max
        _uIUtility.InitializeProgressBar(ProgressBar, AccountQueue.Count() + 1);

        await RunTasksAsync(actualThreadCount, settings);

        return;
    }

    // This method asynchronously runs tasks to process a list of combos
    public async Task<bool> RunTasksAsync(int threadCount, JObject settings)
    {

        // Initialize variables
        var remainingCombos = AccountQueue.Count();

        // Limit thread count to remaining combos
        if (threadCount > remainingCombos)
            threadCount = remainingCombos;

        // Loop through combos until all have been processed
        while (remainingCombos > 0)
        {
            Console.WriteLine($"Remaining Combos: {remainingCombos}");
            var tasks = new List<Task>();

            // Start a new task for each thread
            for (int i = 0; i < threadCount; i++)
            {
                if (remainingCombos == 0)
                {
                    await Task.WhenAll(tasks);
                    tasks.Clear();
                    return true;
                }

                AccountCombo combo;
                lock (comboListLock)
                {
                    // Get the first combo from the queue
                    combo = AccountQueue.Dequeue();
                }

                Console.WriteLine($"Starting task for {combo.Username}");

                // Run the Work method as a Task
                var task = Task.Run(async () =>
                {
                    var runner = new Runner();

                    // Job of thread
                    await runner.Job_AccountFetchingWithoutTasks(combo, settings);

                    // Update accounts left counter
                    UpdateProgress(remainingCombos.ToString());
                });

                remainingCombos--;

                // Remove the combo from the list and add the task to the tasks list
                //comboList.RemoveAt(0);
                tasks.Add(task);

            }

            // Wait for all tasks to complete before proceeding
            await Task.WhenAll(tasks);

            // Clear the tasks list and perform cleanup
            tasks.Clear();
        }

        // Return false if the method did not complete successfully
        return false;
    }

    private void UpdateAccountsLeft(string accountsLeft)
    {
        if (accountsLeftLabel.InvokeRequired)
        {
            accountsLeftLabel.Invoke(new Action(() => UpdateAccountsLeft(accountsLeft)));
        }
        else
        {
            if (accountsLeft == "0")
                accountsLeft = "";

            accountsLeftLabel.Text = $"{accountsLeft}";
        }
    }

    // This method updates the account checking progress.
    private void UpdateProgress(string accountsLeft)
    {
        UpdateAccountsLeft(accountsLeft);
        _uIUtility.IncrementProgressBar(ProgressBar);

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
        //this.Hide();
        _uIUtility.LoadAccountsListView(this);
        this.Hide();
    }

    private void Main_Load(object sender, EventArgs e)
    {

    }

    private async void QuickCheckButton_Click(object sender, EventArgs e)
    {
        var username = UsernameTextBox.Text;
        var password = PasswordTextBox.Text;
        const int ACCOUNT_COUNT = 1;

        // Validate start
        if (username == "" || password == "")
            return;

        // Disable start/check buttons
        StartButton.Enabled = false;
        QuickCheckButton.Enabled = false;
        ProgressBar.Visible = true;

        var settings = _coreUtility.LoadFromSettingsFile(); // Gets Settings file

        // Update accounts left and progress bar 
        UpdateProgress(ACCOUNT_COUNT.ToString());

        // Initializes progress bar with min max
        _uIUtility.InitializeProgressBar(ProgressBar, ACCOUNT_COUNT + 1);

        // Run Check
        var task = Task.Run(async () =>
        {
            var runner = new Runner();

            // Job of thread
            await runner.Job_AccountFetchingWithoutTasks(new AccountCombo()
            {
                Username = username,
                Password = password,
            }, settings);

            // Update accounts left counter
            UpdateProgress(Convert.ToString(ACCOUNT_COUNT - 1));

        });
        await task;

        // Finalize
        StartButton.Enabled = true;
        QuickCheckButton.Enabled = true;
        ProgressBar.Visible = false;
        ConsoleTextBox.Clear(); // Clears Console
        Console.WriteLine("Task Completed.");

    }
}
