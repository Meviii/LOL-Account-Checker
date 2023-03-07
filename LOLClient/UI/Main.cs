using LOLClient.Utility;
using Microsoft.EntityFrameworkCore.Update;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient;

public partial class Main : Form
{

    private readonly CoreUtility _coreUtility;
    private readonly UIUtility _uIUtility;

    public Main()
    {
        _uIUtility = new UIUtility();
        _coreUtility = new CoreUtility();
        InitializeComponent();
        LoadConfigAsync();

    }

    private async void LoadConfigAsync()
    {
        var settings = await _coreUtility.LoadFromSettingsFileAsync();

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
            _coreUtility.SaveToSettingsFileAsync("ComboListPath", filePath);
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

        var settings = await _coreUtility.LoadFromSettingsFileAsync(); // Gets Settings file

        var comboList = _coreUtility.ReadComboList(ComboListText.Text, delimiter); // Reads the combolist(accounts) path

        // Update accounts left and progress bar 
        UpdateProgress(comboList.Count.ToString());

        // Initializes progress bar with min max
        InitializeProgressBar(comboList.Count + 1);

        await RunTasksAsync(actualThreadCount, comboList, settings);

        return;
    }

    // This method asynchronously runs tasks to process a list of combos
    public async Task<bool> RunTasksAsync(int threadCount, List<Tuple<string, string>> comboList, JObject settings)
    {
        // Initialize variables
        var remainingCombos = comboList.Count;

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
                var localIndex = i; // create local copy of i
                if (remainingCombos == 0)
                {
                    tasks.Clear();
                    return true;
                }

                // Get the first combo from the list
                var combo = comboList[0];

                Console.WriteLine($"Starting task for {combo.Item1}");

                // Run the Work method as a Task
                var task = Task.Run(async () =>
                {
                    var runner = new Runner();

                    // Job of thread
                    await runner.Job_AccountFetchingWithoutTasks(combo.Item1, combo.Item2, settings);

                    // Update accounts left counter
                    UpdateProgress(Convert.ToString(Convert.ToInt32(comboList.Count)));

                });

                remainingCombos--;

                // Remove the combo from the list and add the task to the tasks list
                comboList.RemoveAt(0);
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

    // This method initializes the progress bar without allowing the form thread and account thread access the progress bar
    private void InitializeProgressBar(int accountCount)
    {
        if (ProgressBar.InvokeRequired)
        {
            ProgressBar.Invoke(new Action(() => InitializeProgressBar(accountCount)));
        }
        else
        {
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = accountCount;
        }
    }

    // This method updates the account checking progress.
    private void UpdateProgress(string accountsLeft)
    {

        if (accountsLeftLabel.InvokeRequired || ProgressBar.InvokeRequired)
        {
            accountsLeftLabel.Invoke(new Action(() => UpdateProgress(accountsLeft)));
        }
        else
        {
            accountsLeftLabel.Text = $"{accountsLeft}";

            if (ProgressBar.Maximum >= ProgressBar.Value + 1)
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
