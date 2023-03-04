using System.Windows.Forms;

namespace LOLClient
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ConsoleTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.LoadComboButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DelimiterTextBox = new System.Windows.Forms.TextBox();
            this.ComboListText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ThreadCountTextBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.accountsListButton = new System.Windows.Forms.Button();
            this.accountsLeftLabel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ConsoleTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConsoleTextBox.Location = new System.Drawing.Point(49, 133);
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ReadOnly = true;
            this.ConsoleTextBox.Size = new System.Drawing.Size(742, 444);
            this.ConsoleTextBox.TabIndex = 0;
            this.ConsoleTextBox.Text = "";
            this.ConsoleTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.LightYellow;
            this.label1.Location = new System.Drawing.Point(321, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 61);
            this.label1.TabIndex = 1;
            this.label1.Text = "LOL Account Checker";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.BackColor = System.Drawing.Color.LightCyan;
            this.SettingsButton.FlatAppearance.BorderSize = 0;
            this.SettingsButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.SettingsButton.Location = new System.Drawing.Point(0, 0);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(79, 35);
            this.SettingsButton.TabIndex = 6;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = false;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // LoadComboButton
            // 
            this.LoadComboButton.BackColor = System.Drawing.Color.LightCyan;
            this.LoadComboButton.Location = new System.Drawing.Point(963, 186);
            this.LoadComboButton.Name = "LoadComboButton";
            this.LoadComboButton.Size = new System.Drawing.Size(67, 30);
            this.LoadComboButton.TabIndex = 12;
            this.LoadComboButton.Text = "Load";
            this.LoadComboButton.UseVisualStyleBackColor = false;
            this.LoadComboButton.Click += new System.EventHandler(this.ComboLoadButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.LightYellow;
            this.label2.Location = new System.Drawing.Point(872, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Combo List";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.LightYellow;
            this.label3.Location = new System.Drawing.Point(847, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 25);
            this.label3.TabIndex = 15;
            this.label3.Text = "Delimiter";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // DelimiterTextBox
            // 
            this.DelimiterTextBox.BackColor = System.Drawing.Color.LightCyan;
            this.DelimiterTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DelimiterTextBox.Location = new System.Drawing.Point(967, 244);
            this.DelimiterTextBox.MaxLength = 1;
            this.DelimiterTextBox.Name = "DelimiterTextBox";
            this.DelimiterTextBox.Size = new System.Drawing.Size(58, 29);
            this.DelimiterTextBox.TabIndex = 17;
            this.DelimiterTextBox.Text = ":";
            this.DelimiterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DelimiterTextBox.Leave += new System.EventHandler(this.DelimiterTextBox_OnLeave);
            // 
            // ComboListText
            // 
            this.ComboListText.BackColor = System.Drawing.Color.LightCyan;
            this.ComboListText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboListText.Location = new System.Drawing.Point(826, 186);
            this.ComboListText.Name = "ComboListText";
            this.ComboListText.ReadOnly = true;
            this.ComboListText.Size = new System.Drawing.Size(131, 29);
            this.ComboListText.TabIndex = 18;
            this.ComboListText.TextChanged += new System.EventHandler(this.ComboListText_TextChanged);
            // 
            // label4
            // 
            this.label4.Visible = false; // TEMP
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.LightYellow;
            this.label4.Location = new System.Drawing.Point(826, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 25);
            this.label4.TabIndex = 19;
            this.label4.Text = "Thread Count";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // ThreadCountTextBox
            // 
            this.ThreadCountTextBox.Visible = false; // TEMP
            this.ThreadCountTextBox.BackColor = System.Drawing.Color.LightCyan;
            this.ThreadCountTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ThreadCountTextBox.ForeColor = System.Drawing.SystemColors.MenuText;
            this.ThreadCountTextBox.Location = new System.Drawing.Point(968, 297);
            this.ThreadCountTextBox.MaxLength = 3;
            this.ThreadCountTextBox.Name = "ThreadCountTextBox";
            this.ThreadCountTextBox.PlaceholderText = "1";
            this.ThreadCountTextBox.Size = new System.Drawing.Size(58, 29);
            this.ThreadCountTextBox.TabIndex = 20;
            this.ThreadCountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ThreadCountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ThreadCountTextBox_TextChanged);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.LightCyan;
            this.StartButton.Location = new System.Drawing.Point(872, 487);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(114, 54);
            this.StartButton.TabIndex = 21;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartOrStopButton_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(847, 456);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(170, 23);
            this.ProgressBar.TabIndex = 22;
            this.ProgressBar.Visible = false;
            // 
            // accountsListButton
            // 
            this.accountsListButton.BackColor = System.Drawing.Color.LightCyan;
            this.accountsListButton.FlatAppearance.BorderSize = 0;
            this.accountsListButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.accountsListButton.Location = new System.Drawing.Point(83, 0);
            this.accountsListButton.Name = "accountsListButton";
            this.accountsListButton.Size = new System.Drawing.Size(79, 35);
            this.accountsListButton.TabIndex = 23;
            this.accountsListButton.Text = "Accounts";
            this.accountsListButton.UseVisualStyleBackColor = false;
            this.accountsListButton.Click += new System.EventHandler(this.accountsListButton_Click);
            // 
            // accountsLeftLabel
            // 
            this.accountsLeftLabel.BackColor = System.Drawing.Color.LightCyan;
            this.accountsLeftLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.accountsLeftLabel.Location = new System.Drawing.Point(884, 399);
            this.accountsLeftLabel.Name = "accountsLeftLabel";
            this.accountsLeftLabel.ReadOnly = true;
            this.accountsLeftLabel.Size = new System.Drawing.Size(89, 29);
            this.accountsLeftLabel.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.LightYellow;
            this.label5.Location = new System.Drawing.Point(863, 359);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 25);
            this.label5.TabIndex = 25;
            this.label5.Text = "Accounts Left";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1037, 627);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.accountsLeftLabel);
            this.Controls.Add(this.accountsListButton);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ThreadCountTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ComboListText);
            this.Controls.Add(this.DelimiterTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LoadComboButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConsoleTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home - Account Checker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseButton_Click);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ConsoleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button LoadComboButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DelimiterTextBox;
        private System.Windows.Forms.TextBox ComboListText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ThreadCountTextBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private Button accountsListButton;
        private TextBox accountsLeftLabel;
        private Label label5;
    }
}