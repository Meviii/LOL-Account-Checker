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
using System.Drawing;
using System.Diagnostics;
using System.Configuration;

namespace AccountChecker;

public partial class Main : Form
{
    private static readonly object _lock = new();
    private static int _successfulAccounts;
    private static int _failedAccounts;
    public static int SuccessfulAccounts
    {
        get
        {
            lock (_lock)
            {
                return _successfulAccounts;
            }
        }
        set
        {
            lock (_lock)
            {
                _successfulAccounts = value;
            }
        }
    }
    public static int FailAccounts
    {
        get
        {
            lock (_lock)
            {
                return _failedAccounts;
            }
        }
        set
        {
            lock (_lock)
            {
                _failedAccounts = value;
            }
        }
    }
    private readonly CoreUtility _coreUtility;
    private readonly UIUtility _uIUtility;
    private static readonly object comboListLock = new();

    public Main(bool isNewestVersion = true)
    {
        _uIUtility = new UIUtility();
        _coreUtility = new CoreUtility();
        InitializeComponent();
        LoadConfigAsync();

        if (!isNewestVersion)
            DisplayNewVersion();
    }

    private void UpdateSuccessAccounts()
    {
        if (HitAccounts.InvokeRequired)
        {
            HitAccounts.Invoke(new Action(() => UpdateSuccessAccounts()));
        }
        else
        {
            HitAccounts.Text = $"{_successfulAccounts}";
        }
    }

    private void UpdateFailAccounts()
    {
        if (FailedAccounts.InvokeRequired)
        {
            FailedAccounts.Invoke(new Action(() => UpdateFailAccounts()));
        }
        else
        {
            FailedAccounts.Text = $"{_failedAccounts}";
        }
    }

    private void DisplayNewVersion()
    {
        string message = "An update is available. Would you like to update now?";
        string caption = "Update Available";
        MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
        DialogResult result;

        // Show the MessageBox
        result = MessageBox.Show(message, caption, buttons);

        // Check the user's choice and take action accordingly
        if (result == DialogResult.OK)
        {
            // Navigate to the GitHub repository to update
            Process.Start(new ProcessStartInfo("https://github.com/Meviii/LOL-Account-Checker") { UseShellExecute = true });

        }
        else
        {
            // Do nothing, the user has chosen to cancel
        }
    }

    private void LoadConfigAsync()
    {
        // Release
        ReleaseLabel.Text = "Release v" + ConfigurationManager.AppSettings["Version"];

        // Account Checking Stats
        SuccessLabel.Visible = false;
        FailLabel.Visible = false;
        HitAccounts.Visible = false;
        FailedAccounts.Visible = false;

        // Account Stats Initialization
        SuccessfulAccounts = 0;
        FailAccounts = 0;

        // Get Settings
        var settings = _coreUtility.LoadFromSettingsFile();

        if (settings == null)
            return;

        if (settings.ContainsKey("ComboListPath"))
            ComboListText.Text = settings["ComboListPath"].ToString();

        // Disable Tasks Button as default
        TasksButton.Enabled = false;

        // Set Console output to TextBox
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

    private bool IsInaccurateThreadCount(int threadcount)
    {
        int MIN_WARNING_THREAD_COUNT = 11;
        if (threadcount >= MIN_WARNING_THREAD_COUNT)
            return true;

        return false;
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


            AccountQueue.Clear();
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
            button.ForeColor = Color.Black;
        }

    }

    // This method updates the Start button with new values to represent the work has started.
    private void StartButtonOperation(Button button)
    {
        button.Text = "Started";
        button.Enabled = false;
        button.ForeColor = Color.Green;
        ProgressBar.Visible = true;
        ProgressBar.Minimum = 0;
        ProgressBar.Maximum = 0;
        QuickCheckButton.Enabled = false;

        SuccessfulAccounts = 0;
        FailAccounts = 0;
        SuccessLabel.Visible = true;
        FailLabel.Visible = true;
        HitAccounts.Visible = true;
        FailedAccounts.Visible = true;

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
        try
        {
            await RunTasksAsync(actualThreadCount, settings);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occured. {ex.Message}");
        }
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
            if (RiotConnection.SwitchToSingleThreadViaSingleClientOnly)
                threadCount = 1;

            Console.WriteLine($"Remaining Combos: {remainingCombos}");
            var tasks = new List<Task>();

            // Start a new task for each thread
            for (int i = 0; i < threadCount; i++)
            {
                if (remainingCombos == 0)
                {
                    UpdateSuccessAccounts();
                    UpdateFailAccounts();

                    await Task.WhenAll(tasks);
                    tasks.Clear();
                    return true;
                }

                AccountCombo combo;
                lock (comboListLock)
                {
                    // Get the first combo from the queue
                    
                    AccountQueue.Dequeue(out combo);
                }

                Console.WriteLine($"Starting task for {combo.Username}");

                Task task = null;
                if (ExecuteTasksOnAllCombosCheckBox.Checked)
                {
                    // Run Check
                    task = Task.Run(async () =>
                    {
                        var runner = new Runner();

                        // Job of thread
                        await runner.Job_AccountFetchingWithTasks(combo, settings);

                        // Update accounts left counter
                        UpdateProgress(AccountQueue.Count().ToString());

                    });
                }
                else
                {
                    // Run the Work method as a Task
                    task = Task.Run(async () =>
                    {
                        var runner = new Runner();

                        // Job of thread
                        await runner.Job_AccountFetchingWithoutTasks(combo, settings);

                        // Update accounts left counter
                        UpdateProgress(AccountQueue.Count().ToString());
                    });
                }

                remainingCombos--;

                // Remove the combo from the list and add the task to the tasks list
                //comboList.RemoveAt(0);
                tasks.Add(task);

            }
            
            // Wait for all tasks to complete before proceeding
            await Task.WhenAll(tasks);

            // Get new queue count to care for retry accounts
            remainingCombos = AccountQueue.Count();

            UpdateSuccessAccounts();
            UpdateFailAccounts();

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
        QuickCheckButton.Text = "Checking";
        ProgressBar.Visible = true;

        var settings = _coreUtility.LoadFromSettingsFile(); // Gets Settings file

        // Update accounts left and progress bar 
        UpdateProgress(ACCOUNT_COUNT.ToString());

        // Initializes progress bar with min max
        _uIUtility.InitializeProgressBar(ProgressBar, ACCOUNT_COUNT + 1);

        if (ExecuteTasksOnAllCombosCheckBox.Checked)
        {
            // Run Check
            var task = Task.Run(async () =>
            {
                var runner = new Runner();

                // Job of thread
                await runner.Job_AccountFetchingWithTasks(new AccountCombo()
                {
                    Username = username,
                    Password = password,
                }, settings);

                // Update accounts left counter
                UpdateProgress(Convert.ToString(ACCOUNT_COUNT - 1));

            });
            await task;
        }
        else
        {
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
        }

        // Finalize
        UpdateProgress("0");
        StartButton.Enabled = true;
        QuickCheckButton.Enabled = true;
        QuickCheckButton.Text = "Check";
        ProgressBar.Visible = false;
        ConsoleTextBox.Clear(); // Clears Console
        Console.WriteLine("Task Completed.");

    }

    private void ThreadCountTextBox_TextChanged(object sender, EventArgs e)
    {

        int threadCount = ValidateThreadCount();

        if (IsInaccurateThreadCount(threadCount))
        {
            MessageBox.Show("This many threads may rarely cause inaccurate checks. 1 thread per account.");
        }
    }

    private void ExecuteTasksOnAllCombosCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)sender).Checked)
        {
            TasksButton.Enabled = true;
        }
        else
        {
            TasksButton.Enabled = false;
        }

    }

    private void TasksButton_Click(object sender, EventArgs e)
    {
        _uIUtility.LoadTasksViewAsDialog();
    }
}
