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
            FilterLevel29AboveCheckBox = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            RegionComboBox = new ComboBox();
            FilterButton = new Button();
            label5 = new Label();
            FilterLevel30BelowCheckBox = new CheckBox();
            label6 = new Label();
            FilterUnVerifiedCheckBox = new CheckBox();
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
            accountsGridView.Location = new System.Drawing.Point(51, 142);
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
            ExportsFolderButton.Location = new System.Drawing.Point(745, 91);
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
            button1.Location = new System.Drawing.Point(788, 585);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(86, 36);
            button1.TabIndex = 23;
            button1.Text = "Refresh";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // SearchTextBox
            // 
            SearchTextBox.Location = new System.Drawing.Point(63, 114);
            SearchTextBox.Name = "SearchTextBox";
            SearchTextBox.PlaceholderText = "Search Summoner";
            SearchTextBox.Size = new System.Drawing.Size(108, 23);
            SearchTextBox.TabIndex = 24;
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
            // 
            // FilterLevel29AboveCheckBox
            // 
            FilterLevel29AboveCheckBox.AutoSize = true;
            FilterLevel29AboveCheckBox.BackColor = System.Drawing.Color.Transparent;
            FilterLevel29AboveCheckBox.Location = new System.Drawing.Point(1007, 234);
            FilterLevel29AboveCheckBox.Name = "FilterLevel29AboveCheckBox";
            FilterLevel29AboveCheckBox.Size = new System.Drawing.Size(15, 14);
            FilterLevel29AboveCheckBox.TabIndex = 28;
            FilterLevel29AboveCheckBox.UseVisualStyleBackColor = false;
            FilterLevel29AboveCheckBox.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new System.Drawing.Font("Segoe Print", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.Color.LightYellow;
            label2.Location = new System.Drawing.Point(913, 144);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(92, 43);
            label2.TabIndex = 29;
            label2.Text = "Filters";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new System.Drawing.Font("Segoe Print", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label3.ForeColor = System.Drawing.Color.LightYellow;
            label3.Location = new System.Drawing.Point(915, 224);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(90, 31);
            label3.TabIndex = 30;
            label3.Text = "> Lvl 29";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new System.Drawing.Font("Segoe Print", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label4.ForeColor = System.Drawing.Color.LightYellow;
            label4.Location = new System.Drawing.Point(924, 287);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(73, 31);
            label4.TabIndex = 32;
            label4.Text = "Region";
            label4.Click += label4_Click;
            // 
            // RegionComboBox
            // 
            RegionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            RegionComboBox.FormattingEnabled = true;
            RegionComboBox.Location = new System.Drawing.Point(914, 318);
            RegionComboBox.Name = "RegionComboBox";
            RegionComboBox.Size = new System.Drawing.Size(93, 23);
            RegionComboBox.TabIndex = 33;
            RegionComboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // FilterButton
            // 
            FilterButton.BackColor = System.Drawing.Color.LightCyan;
            FilterButton.Location = new System.Drawing.Point(918, 357);
            FilterButton.Name = "FilterButton";
            FilterButton.Size = new System.Drawing.Size(86, 36);
            FilterButton.TabIndex = 34;
            FilterButton.Text = "Filter";
            FilterButton.UseVisualStyleBackColor = false;
            FilterButton.Click += FilterButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new System.Drawing.Font("Segoe Print", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label5.ForeColor = System.Drawing.Color.LightYellow;
            label5.Location = new System.Drawing.Point(915, 188);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(90, 31);
            label5.TabIndex = 36;
            label5.Text = "< Lvl 30";
            // 
            // FilterLevel30BelowCheckBox
            // 
            FilterLevel30BelowCheckBox.AutoSize = true;
            FilterLevel30BelowCheckBox.BackColor = System.Drawing.Color.Transparent;
            FilterLevel30BelowCheckBox.Location = new System.Drawing.Point(1007, 198);
            FilterLevel30BelowCheckBox.Name = "FilterLevel30BelowCheckBox";
            FilterLevel30BelowCheckBox.Size = new System.Drawing.Size(15, 14);
            FilterLevel30BelowCheckBox.TabIndex = 35;
            FilterLevel30BelowCheckBox.UseVisualStyleBackColor = false;
            FilterLevel30BelowCheckBox.CheckedChanged += FilterLevel30BelowCheckBox_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.FlatStyle = FlatStyle.Flat;
            label6.Font = new System.Drawing.Font("Segoe Print", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label6.ForeColor = System.Drawing.Color.LightYellow;
            label6.Location = new System.Drawing.Point(885, 256);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(121, 31);
            label6.TabIndex = 38;
            label6.Text = "Un-Verified";
            // 
            // FilterUnVerifiedCheckBox
            // 
            FilterUnVerifiedCheckBox.AutoSize = true;
            FilterUnVerifiedCheckBox.BackColor = System.Drawing.Color.Transparent;
            FilterUnVerifiedCheckBox.Location = new System.Drawing.Point(1007, 265);
            FilterUnVerifiedCheckBox.Name = "FilterUnVerifiedCheckBox";
            FilterUnVerifiedCheckBox.Size = new System.Drawing.Size(15, 14);
            FilterUnVerifiedCheckBox.TabIndex = 37;
            FilterUnVerifiedCheckBox.UseVisualStyleBackColor = false;
            // 
            // AccountList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Desktop;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1037, 627);
            Controls.Add(label6);
            Controls.Add(FilterUnVerifiedCheckBox);
            Controls.Add(label5);
            Controls.Add(FilterLevel30BelowCheckBox);
            Controls.Add(FilterButton);
            Controls.Add(RegionComboBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(FilterLevel29AboveCheckBox);
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
        private CheckBox FilterLevel29AboveCheckBox;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox RegionComboBox;
        private Button FilterButton;
        private Label label5;
        private CheckBox FilterLevel30BelowCheckBox;
        private Label label6;
        private CheckBox FilterUnVerifiedCheckBox;
    }
}