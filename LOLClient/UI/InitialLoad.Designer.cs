namespace LOLClient.UI
{
    partial class InitialLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialLoad));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RiotBrowseButton = new System.Windows.Forms.Button();
            this.LeagueBrowseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LeaguePathLabel = new System.Windows.Forms.TextBox();
            this.RiotPathLabel = new System.Windows.Forms.TextBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.LightYellow;
            this.label2.Location = new System.Drawing.Point(100, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "RiotClientServices.exe Location";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.LightYellow;
            this.label1.Location = new System.Drawing.Point(115, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "LeagueClient.exe Location";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // RiotBrowseButton
            // 
            this.RiotBrowseButton.BackColor = System.Drawing.Color.LightCyan;
            this.RiotBrowseButton.Location = new System.Drawing.Point(325, 140);
            this.RiotBrowseButton.Name = "RiotBrowseButton";
            this.RiotBrowseButton.Size = new System.Drawing.Size(74, 33);
            this.RiotBrowseButton.TabIndex = 9;
            this.RiotBrowseButton.Text = "Browse";
            this.RiotBrowseButton.UseVisualStyleBackColor = false;
            this.RiotBrowseButton.Click += new System.EventHandler(this.BrowseRiotButton_Click);
            // 
            // LeagueBrowseButton
            // 
            this.LeagueBrowseButton.BackColor = System.Drawing.Color.LightCyan;
            this.LeagueBrowseButton.Location = new System.Drawing.Point(325, 257);
            this.LeagueBrowseButton.Name = "LeagueBrowseButton";
            this.LeagueBrowseButton.Size = new System.Drawing.Size(74, 33);
            this.LeagueBrowseButton.TabIndex = 11;
            this.LeagueBrowseButton.Text = "Browse";
            this.LeagueBrowseButton.UseVisualStyleBackColor = false;
            this.LeagueBrowseButton.Click += new System.EventHandler(this.LeagueBrowseButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.Aqua;
            this.SaveButton.Location = new System.Drawing.Point(201, 316);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(74, 33);
            this.SaveButton.TabIndex = 12;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LeaguePathLabel
            // 
            this.LeaguePathLabel.BackColor = System.Drawing.Color.LightCyan;
            this.LeaguePathLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LeaguePathLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            this.LeaguePathLabel.Location = new System.Drawing.Point(87, 259);
            this.LeaguePathLabel.Name = "LeaguePathLabel";
            this.LeaguePathLabel.ReadOnly = true;
            this.LeaguePathLabel.Size = new System.Drawing.Size(232, 29);
            this.LeaguePathLabel.TabIndex = 13;
            // 
            // RiotPathLabel
            // 
            this.RiotPathLabel.BackColor = System.Drawing.Color.LightCyan;
            this.RiotPathLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RiotPathLabel.ForeColor = System.Drawing.SystemColors.MenuText;
            this.RiotPathLabel.Location = new System.Drawing.Point(87, 141);
            this.RiotPathLabel.Name = "RiotPathLabel";
            this.RiotPathLabel.ReadOnly = true;
            this.RiotPathLabel.Size = new System.Drawing.Size(232, 29);
            this.RiotPathLabel.TabIndex = 14;
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel.Location = new System.Drawing.Point(125, 363);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(224, 30);
            this.StatusLabel.TabIndex = 15;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InitialLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(484, 481);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.RiotPathLabel);
            this.Controls.Add(this.LeaguePathLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LeagueBrowseButton);
            this.Controls.Add(this.RiotBrowseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "InitialLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}