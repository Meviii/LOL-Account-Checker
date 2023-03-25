namespace AccountChecker.UI
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            RiotBrowseButton = new System.Windows.Forms.Button();
            LeagueBrowseButton = new System.Windows.Forms.Button();
            SaveButton = new System.Windows.Forms.Button();
            LeaguePathLabel = new System.Windows.Forms.TextBox();
            RiotPathLabel = new System.Windows.Forms.TextBox();
            StatusLabel = new System.Windows.Forms.Label();
            updateButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label2.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.Color.LightYellow;
            label2.Location = new System.Drawing.Point(100, 98);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(290, 25);
            label2.TabIndex = 6;
            label2.Text = "RiotClientServices.exe Location";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label1.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.LightYellow;
            label1.Location = new System.Drawing.Point(115, 215);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(247, 25);
            label1.TabIndex = 7;
            label1.Text = "LeagueClient.exe Location";
            label1.Click += label1_Click;
            // 
            // RiotBrowseButton
            // 
            RiotBrowseButton.BackColor = System.Drawing.Color.LightCyan;
            RiotBrowseButton.Location = new System.Drawing.Point(325, 140);
            RiotBrowseButton.Name = "RiotBrowseButton";
            RiotBrowseButton.Size = new System.Drawing.Size(74, 33);
            RiotBrowseButton.TabIndex = 9;
            RiotBrowseButton.Text = "Browse";
            RiotBrowseButton.UseVisualStyleBackColor = false;
            RiotBrowseButton.Click += BrowseRiotButton_Click;
            // 
            // LeagueBrowseButton
            // 
            LeagueBrowseButton.BackColor = System.Drawing.Color.LightCyan;
            LeagueBrowseButton.Location = new System.Drawing.Point(325, 257);
            LeagueBrowseButton.Name = "LeagueBrowseButton";
            LeagueBrowseButton.Size = new System.Drawing.Size(74, 33);
            LeagueBrowseButton.TabIndex = 11;
            LeagueBrowseButton.Text = "Browse";
            LeagueBrowseButton.UseVisualStyleBackColor = false;
            LeagueBrowseButton.Click += LeagueBrowseButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.BackColor = System.Drawing.Color.Aqua;
            SaveButton.Location = new System.Drawing.Point(201, 316);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new System.Drawing.Size(74, 33);
            SaveButton.TabIndex = 12;
            SaveButton.Text = "Done";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // LeaguePathLabel
            // 
            LeaguePathLabel.BackColor = System.Drawing.Color.LightCyan;
            LeaguePathLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LeaguePathLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            LeaguePathLabel.Location = new System.Drawing.Point(87, 259);
            LeaguePathLabel.Name = "LeaguePathLabel";
            LeaguePathLabel.ReadOnly = true;
            LeaguePathLabel.Size = new System.Drawing.Size(232, 29);
            LeaguePathLabel.TabIndex = 13;
            // 
            // RiotPathLabel
            // 
            RiotPathLabel.BackColor = System.Drawing.Color.LightCyan;
            RiotPathLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            RiotPathLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            RiotPathLabel.Location = new System.Drawing.Point(87, 141);
            RiotPathLabel.Name = "RiotPathLabel";
            RiotPathLabel.ReadOnly = true;
            RiotPathLabel.Size = new System.Drawing.Size(232, 29);
            RiotPathLabel.TabIndex = 14;
            // 
            // StatusLabel
            // 
            StatusLabel.BackColor = System.Drawing.Color.Transparent;
            StatusLabel.Location = new System.Drawing.Point(125, 363);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new System.Drawing.Size(224, 30);
            StatusLabel.TabIndex = 15;
            StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateButton
            // 
            updateButton.BackColor = System.Drawing.Color.LightCyan;
            updateButton.Location = new System.Drawing.Point(380, 413);
            updateButton.Name = "updateButton";
            updateButton.Size = new System.Drawing.Size(92, 56);
            updateButton.TabIndex = 16;
            updateButton.Text = "Update Skins/Champs";
            updateButton.UseVisualStyleBackColor = false;
            updateButton.Click += UpdateButton_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ClientSize = new System.Drawing.Size(484, 481);
            Controls.Add(updateButton);
            Controls.Add(StatusLabel);
            Controls.Add(RiotPathLabel);
            Controls.Add(LeaguePathLabel);
            Controls.Add(SaveButton);
            Controls.Add(LeagueBrowseButton);
            Controls.Add(RiotBrowseButton);
            Controls.Add(label1);
            Controls.Add(label2);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "Settings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Settings";
            FormClosing += CloseButton_Click;
            Load += Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RiotBrowseButton;
        private System.Windows.Forms.Button LeagueBrowseButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox LeaguePathLabel;
        private System.Windows.Forms.TextBox RiotPathLabel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button updateButton;
    }
}