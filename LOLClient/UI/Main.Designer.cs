using System.Windows.Forms;

namespace AccountChecker
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
            ConsoleTextBox = new RichTextBox();
            label1 = new Label();
            SettingsButton = new Button();
            LoadComboButton = new Button();
            label2 = new Label();
            label3 = new Label();
            DelimiterTextBox = new TextBox();
            ComboListText = new TextBox();
            label4 = new Label();
            ThreadCountTextBox = new TextBox();
            StartButton = new Button();
            ProgressBar = new ProgressBar();
            accountsListButton = new Button();
            accountsLeftLabel = new Label();
            label5 = new Label();
            label6 = new Label();
            UsernameTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            QuickCheckButton = new Button();
            TasksButton = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label7 = new Label();
            ExecuteTasksOnAllCombosCheckBox = new CheckBox();
            ReleaseLabel = new Label();
            SuccessLabel = new Label();
            FailLabel = new Label();
            HitAccounts = new Label();
            FailedAccounts = new Label();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // ConsoleTextBox
            // 
            ConsoleTextBox.BackColor = System.Drawing.Color.LightSkyBlue;
            ConsoleTextBox.ImeMode = ImeMode.NoControl;
            ConsoleTextBox.Location = new System.Drawing.Point(49, 122);
            ConsoleTextBox.Name = "ConsoleTextBox";
            ConsoleTextBox.ReadOnly = true;
            ConsoleTextBox.Size = new System.Drawing.Size(742, 464);
            ConsoleTextBox.TabIndex = 0;
            ConsoleTextBox.Text = "";
            ConsoleTextBox.TextChanged += richTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new System.Drawing.Font("Segoe Print", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.LightYellow;
            label1.Location = new System.Drawing.Point(321, 53);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(397, 61);
            label1.TabIndex = 1;
            label1.Text = "LOL Account Checker";
            label1.Click += label1_Click;
            // 
            // SettingsButton
            // 
            SettingsButton.BackColor = System.Drawing.Color.LightCyan;
            SettingsButton.FlatAppearance.BorderSize = 0;
            SettingsButton.ForeColor = System.Drawing.SystemColors.Desktop;
            SettingsButton.Location = new System.Drawing.Point(0, 0);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new System.Drawing.Size(79, 35);
            SettingsButton.TabIndex = 6;
            SettingsButton.Text = "Settings";
            SettingsButton.UseVisualStyleBackColor = false;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // LoadComboButton
            // 
            LoadComboButton.BackColor = System.Drawing.Color.LightCyan;
            LoadComboButton.Location = new System.Drawing.Point(960, 294);
            LoadComboButton.Name = "LoadComboButton";
            LoadComboButton.Size = new System.Drawing.Size(67, 30);
            LoadComboButton.TabIndex = 12;
            LoadComboButton.Text = "Load";
            LoadComboButton.UseVisualStyleBackColor = false;
            LoadComboButton.Click += ComboLoadButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.Color.LightYellow;
            label2.Location = new System.Drawing.Point(869, 263);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(114, 25);
            label2.TabIndex = 13;
            label2.Text = "Combo List";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label3.ForeColor = System.Drawing.Color.LightYellow;
            label3.Location = new System.Drawing.Point(844, 331);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(94, 25);
            label3.TabIndex = 15;
            label3.Text = "Delimiter";
            label3.Click += label3_Click;
            // 
            // DelimiterTextBox
            // 
            DelimiterTextBox.BackColor = System.Drawing.Color.LightCyan;
            DelimiterTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            DelimiterTextBox.Location = new System.Drawing.Point(965, 330);
            DelimiterTextBox.MaxLength = 1;
            DelimiterTextBox.Name = "DelimiterTextBox";
            DelimiterTextBox.PlaceholderText = ":";
            DelimiterTextBox.Size = new System.Drawing.Size(58, 29);
            DelimiterTextBox.TabIndex = 17;
            DelimiterTextBox.TextAlign = HorizontalAlignment.Center;
            DelimiterTextBox.Leave += DelimiterTextBox_OnLeave;
            // 
            // ComboListText
            // 
            ComboListText.BackColor = System.Drawing.Color.LightCyan;
            ComboListText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ComboListText.Location = new System.Drawing.Point(823, 295);
            ComboListText.Name = "ComboListText";
            ComboListText.ReadOnly = true;
            ComboListText.Size = new System.Drawing.Size(131, 29);
            ComboListText.TabIndex = 18;
            ComboListText.TextChanged += ComboListText_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label4.ForeColor = System.Drawing.Color.LightYellow;
            label4.Location = new System.Drawing.Point(823, 370);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(134, 25);
            label4.TabIndex = 19;
            label4.Text = "Thread Count";
            label4.Click += label4_Click;
            // 
            // ThreadCountTextBox
            // 
            ThreadCountTextBox.BackColor = System.Drawing.Color.LightCyan;
            ThreadCountTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            ThreadCountTextBox.ForeColor = System.Drawing.SystemColors.MenuText;
            ThreadCountTextBox.Location = new System.Drawing.Point(965, 367);
            ThreadCountTextBox.MaxLength = 3;
            ThreadCountTextBox.Name = "ThreadCountTextBox";
            ThreadCountTextBox.PlaceholderText = "1";
            ThreadCountTextBox.Size = new System.Drawing.Size(58, 29);
            ThreadCountTextBox.TabIndex = 20;
            ThreadCountTextBox.TextAlign = HorizontalAlignment.Center;
            ThreadCountTextBox.TextChanged += ThreadCountTextBox_TextChanged;
            ThreadCountTextBox.KeyPress += ThreadCountTextBox_TextChanged;
            // 
            // StartButton
            // 
            StartButton.BackColor = System.Drawing.Color.LightCyan;
            StartButton.Location = new System.Drawing.Point(872, 532);
            StartButton.Name = "StartButton";
            StartButton.Size = new System.Drawing.Size(114, 54);
            StartButton.TabIndex = 21;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartOrStopButton_Click;
            // 
            // ProgressBar
            // 
            ProgressBar.BackColor = System.Drawing.Color.LightSkyBlue;
            ProgressBar.Location = new System.Drawing.Point(845, 501);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new System.Drawing.Size(170, 23);
            ProgressBar.TabIndex = 22;
            ProgressBar.Visible = false;
            // 
            // accountsListButton
            // 
            accountsListButton.BackColor = System.Drawing.Color.LightCyan;
            accountsListButton.FlatAppearance.BorderSize = 0;
            accountsListButton.ForeColor = System.Drawing.SystemColors.Desktop;
            accountsListButton.Location = new System.Drawing.Point(83, 0);
            accountsListButton.Name = "accountsListButton";
            accountsListButton.Size = new System.Drawing.Size(79, 35);
            accountsListButton.TabIndex = 23;
            accountsListButton.Text = "Accounts";
            accountsListButton.UseVisualStyleBackColor = false;
            accountsListButton.Click += accountsListButton_Click;
            // 
            // accountsLeftLabel
            // 
            accountsLeftLabel.BackColor = System.Drawing.Color.LightCyan;
            accountsLeftLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            accountsLeftLabel.Location = new System.Drawing.Point(884, 431);
            accountsLeftLabel.Name = "accountsLeftLabel";
            accountsLeftLabel.Size = new System.Drawing.Size(89, 29);
            accountsLeftLabel.TabIndex = 24;
            accountsLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label5.ForeColor = System.Drawing.Color.LightYellow;
            label5.Location = new System.Drawing.Point(863, 403);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(133, 25);
            label5.TabIndex = 25;
            label5.Text = "Accounts Left";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.FlatStyle = FlatStyle.Flat;
            label6.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label6.ForeColor = System.Drawing.Color.LightYellow;
            label6.Location = new System.Drawing.Point(866, 161);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(120, 25);
            label6.TabIndex = 26;
            label6.Text = "Quick Check";
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.BackColor = System.Drawing.Color.LightCyan;
            UsernameTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            UsernameTextBox.Location = new System.Drawing.Point(825, 192);
            UsernameTextBox.MaxLength = 32;
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.PlaceholderText = "Username";
            UsernameTextBox.Size = new System.Drawing.Size(92, 29);
            UsernameTextBox.TabIndex = 27;
            UsernameTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.BackColor = System.Drawing.Color.LightCyan;
            PasswordTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            PasswordTextBox.Location = new System.Drawing.Point(927, 192);
            PasswordTextBox.MaxLength = 99;
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PlaceholderText = "Password";
            PasswordTextBox.Size = new System.Drawing.Size(92, 29);
            PasswordTextBox.TabIndex = 28;
            PasswordTextBox.TextAlign = HorizontalAlignment.Center;
            PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // QuickCheckButton
            // 
            QuickCheckButton.BackColor = System.Drawing.Color.LightCyan;
            QuickCheckButton.Location = new System.Drawing.Point(872, 227);
            QuickCheckButton.Name = "QuickCheckButton";
            QuickCheckButton.Size = new System.Drawing.Size(100, 31);
            QuickCheckButton.TabIndex = 29;
            QuickCheckButton.Text = "Check";
            QuickCheckButton.UseVisualStyleBackColor = false;
            QuickCheckButton.Click += QuickCheckButton_Click;
            // 
            // TasksButton
            // 
            TasksButton.BackColor = System.Drawing.Color.LightCyan;
            TasksButton.Location = new System.Drawing.Point(948, 126);
            TasksButton.Name = "TasksButton";
            TasksButton.Size = new System.Drawing.Size(71, 31);
            TasksButton.TabIndex = 30;
            TasksButton.Text = "Tasks";
            TasksButton.UseVisualStyleBackColor = false;
            TasksButton.Click += TasksButton_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            flowLayoutPanel1.Controls.Add(label7);
            flowLayoutPanel1.Controls.Add(ExecuteTasksOnAllCombosCheckBox);
            flowLayoutPanel1.Location = new System.Drawing.Point(814, 130);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(139, 31);
            flowLayoutPanel1.TabIndex = 31;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.ForeColor = System.Drawing.Color.LightYellow;
            label7.Location = new System.Drawing.Point(3, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(105, 20);
            label7.TabIndex = 0;
            label7.Text = "Execute Tasks";
            // 
            // ExecuteTasksOnAllCombosCheckBox
            // 
            ExecuteTasksOnAllCombosCheckBox.AutoSize = true;
            ExecuteTasksOnAllCombosCheckBox.Location = new System.Drawing.Point(114, 3);
            ExecuteTasksOnAllCombosCheckBox.Name = "ExecuteTasksOnAllCombosCheckBox";
            ExecuteTasksOnAllCombosCheckBox.Size = new System.Drawing.Size(15, 14);
            ExecuteTasksOnAllCombosCheckBox.TabIndex = 1;
            ExecuteTasksOnAllCombosCheckBox.UseVisualStyleBackColor = true;
            ExecuteTasksOnAllCombosCheckBox.CheckedChanged += ExecuteTasksOnAllCombosCheckBox_CheckedChanged;
            // 
            // ReleaseLabel
            // 
            ReleaseLabel.AutoSize = true;
            ReleaseLabel.BackColor = System.Drawing.Color.Transparent;
            ReleaseLabel.ForeColor = System.Drawing.Color.LightYellow;
            ReleaseLabel.Location = new System.Drawing.Point(3, 609);
            ReleaseLabel.Name = "ReleaseLabel";
            ReleaseLabel.Size = new System.Drawing.Size(70, 15);
            ReleaseLabel.TabIndex = 32;
            ReleaseLabel.Text = "Release v0.0";
            // 
            // SuccessLabel
            // 
            SuccessLabel.AutoSize = true;
            SuccessLabel.BackColor = System.Drawing.Color.Transparent;
            SuccessLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            SuccessLabel.ForeColor = System.Drawing.Color.LightYellow;
            SuccessLabel.Location = new System.Drawing.Point(884, 466);
            SuccessLabel.Name = "SuccessLabel";
            SuccessLabel.Size = new System.Drawing.Size(71, 15);
            SuccessLabel.TabIndex = 33;
            SuccessLabel.Text = "Successful: ";
            // 
            // FailLabel
            // 
            FailLabel.AutoSize = true;
            FailLabel.BackColor = System.Drawing.Color.Transparent;
            FailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            FailLabel.ForeColor = System.Drawing.Color.LightYellow;
            FailLabel.Location = new System.Drawing.Point(910, 482);
            FailLabel.Name = "FailLabel";
            FailLabel.Size = new System.Drawing.Size(42, 15);
            FailLabel.TabIndex = 35;
            FailLabel.Text = "Failed:";
            // 
            // HitAccounts
            // 
            HitAccounts.AutoSize = true;
            HitAccounts.BackColor = System.Drawing.Color.Transparent;
            HitAccounts.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            HitAccounts.ForeColor = System.Drawing.Color.Lime;
            HitAccounts.Location = new System.Drawing.Point(952, 466);
            HitAccounts.Name = "HitAccounts";
            HitAccounts.Size = new System.Drawing.Size(14, 15);
            HitAccounts.TabIndex = 36;
            HitAccounts.Text = "0";
            // 
            // FailedAccounts
            // 
            FailedAccounts.AutoSize = true;
            FailedAccounts.BackColor = System.Drawing.Color.Transparent;
            FailedAccounts.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            FailedAccounts.ForeColor = System.Drawing.Color.Lime;
            FailedAccounts.Location = new System.Drawing.Point(952, 482);
            FailedAccounts.Name = "FailedAccounts";
            FailedAccounts.Size = new System.Drawing.Size(14, 15);
            FailedAccounts.TabIndex = 37;
            FailedAccounts.Text = "0";
            // 
            // Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Desktop;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1037, 627);
            Controls.Add(FailedAccounts);
            Controls.Add(HitAccounts);
            Controls.Add(FailLabel);
            Controls.Add(SuccessLabel);
            Controls.Add(ReleaseLabel);
            Controls.Add(TasksButton);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(QuickCheckButton);
            Controls.Add(PasswordTextBox);
            Controls.Add(UsernameTextBox);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(accountsLeftLabel);
            Controls.Add(accountsListButton);
            Controls.Add(ProgressBar);
            Controls.Add(StartButton);
            Controls.Add(ThreadCountTextBox);
            Controls.Add(label4);
            Controls.Add(ComboListText);
            Controls.Add(DelimiterTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(LoadComboButton);
            Controls.Add(SettingsButton);
            Controls.Add(label1);
            Controls.Add(ConsoleTextBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Home - Account Checker";
            FormClosing += CloseButton_Click;
            Load += Main_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private Label accountsLeftLabel;
        private Label label5;
        private Label label6;
        private TextBox UsernameTextBox;
        private TextBox PasswordTextBox;
        private Button QuickCheckButton;
        private Button TasksButton;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label7;
        private CheckBox ExecuteTasksOnAllCombosCheckBox;
        private Label ReleaseLabel;
        private Label SuccessLabel;
        private Label FailLabel;
        private Label HitAccounts;
        private Label FailedAccounts;
    }
}