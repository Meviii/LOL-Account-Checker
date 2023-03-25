using System.Windows.Forms;

namespace AccountChecker
{
    partial class AccountList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountList));
            label1 = new Label();
            accountsGridView = new DataGridView();
            SummonerName = new DataGridViewTextBoxColumn();
            BackButton = new Button();
            ExportsFolderButton = new Button();
            button1 = new Button();
            SearchTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)accountsGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new System.Drawing.Font("Segoe Print", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.LightYellow;
            label1.Location = new System.Drawing.Point(337, 58);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(333, 61);
            label1.TabIndex = 1;
            label1.Text = "Checked Accounts";
            // 
            // accountsGridView
            // 
            accountsGridView.AllowUserToAddRows = false;
            accountsGridView.AllowUserToDeleteRows = false;
            accountsGridView.AllowUserToOrderColumns = true;
            accountsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            accountsGridView.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            accountsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            accountsGridView.GridColor = System.Drawing.Color.LightSkyBlue;
            accountsGridView.Location = new System.Drawing.Point(109, 142);
            accountsGridView.Name = "accountsGridView";
            accountsGridView.ReadOnly = true;
            accountsGridView.RowTemplate.Height = 25;
            accountsGridView.Size = new System.Drawing.Size(823, 437);
            accountsGridView.TabIndex = 2;
            accountsGridView.CellMouseDoubleClick += accountsGridView_CellContentDoubleClick;
            // 
            // SummonerName
            // 
            SummonerName.Name = "SummonerName";
            // 
            // BackButton
            // 
            BackButton.BackColor = System.Drawing.Color.LightCyan;
            BackButton.FlatAppearance.BorderSize = 0;
            BackButton.ForeColor = System.Drawing.SystemColors.Desktop;
            BackButton.Location = new System.Drawing.Point(2, 2);
            BackButton.Name = "BackButton";
            BackButton.Size = new System.Drawing.Size(79, 35);
            BackButton.TabIndex = 7;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click;
            // 
            // ExportsFolderButton
            // 
            ExportsFolderButton.BackColor = System.Drawing.Color.LightCyan;
            ExportsFolderButton.Location = new System.Drawing.Point(803, 91);
            ExportsFolderButton.Name = "ExportsFolderButton";
            ExportsFolderButton.Size = new System.Drawing.Size(129, 45);
            ExportsFolderButton.TabIndex = 22;
            ExportsFolderButton.Text = "Open Exports Folder";
            ExportsFolderButton.UseVisualStyleBackColor = false;
            ExportsFolderButton.Click += ExportsFolderButton_Click;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.LightCyan;
            button1.Location = new System.Drawing.Point(846, 585);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(86, 36);
            button1.TabIndex = 23;
            button1.Text = "Refresh";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // SearchTextBox
            // 
            SearchTextBox.Location = new System.Drawing.Point(443, 114);
            SearchTextBox.Name = "SearchTextBox";
            SearchTextBox.PlaceholderText = "Search Summoner";
            SearchTextBox.Size = new System.Drawing.Size(108, 23);
            SearchTextBox.TabIndex = 24;
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
            // 
            // AccountList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Desktop;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1037, 627);
            Controls.Add(SearchTextBox);
            Controls.Add(button1);
            Controls.Add(ExportsFolderButton);
            Controls.Add(BackButton);
            Controls.Add(accountsGridView);
            Controls.Add(label1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "AccountList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Accounts - Account Checker";
            FormClosing += CloseButton_Click;
            ((System.ComponentModel.ISupportInitialize)accountsGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label label1;
        private DataGridView accountsGridView;
        private DataGridViewTextBoxColumn SummonerName;
        private Button BackButton;
        private Button ExportsFolderButton;
        private Button button1;
        private TextBox SearchTextBox;
    }
}