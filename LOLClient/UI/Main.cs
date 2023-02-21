using LOLClient.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace LOLClient;

public partial class Main : Form
{

    private readonly UIUtility _uIUtility;

    public Main()
    {
        _uIUtility = new UIUtility();
        InitializeComponent();
        LoadConfig();

    }

    private void LoadConfig()
    {
        var settings = _uIUtility.LoadFromSettingsFile();

        if (settings.ContainsKey("ComboListPath"))
            ComboListText.Text = settings["ComboListPath"].ToString();

        Console.SetOut(new ConsoleWriter(ConsoleTextBox));
    }

    private void openFile_FileOk(object sender, CancelEventArgs e)
    {
        
    }

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {
        
    }

    private void WriteTo_richTextBox1(string text)
    {
        ConsoleTextBox.AppendText(text + "\n");
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
            _uIUtility.SaveToSettingsFile("ComboListPath", filePath);
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

    private void StartButton_Click(object sender, EventArgs e)
    {

        var runner = new Runner();
        runner.ReadComboList(ComboListText.ToString());

        var button = (Button)sender;

        if (button.Text == "Start")
        {
            button.Text = "Stop";
            button.ForeColor = System.Drawing.Color.Red;
            ProgressBar.Visible = true;

        }
        else if (button.Text == "Stop")
        {
            button.Text = "Start";
            button.ForeColor = System.Drawing.Color.Black;
            ProgressBar.Visible = false;
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
}
