using System.Windows.Forms;

namespace LOLClient
{
    partial class SingleAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleAccount));
            this.SummonerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Overview = new System.Windows.Forms.TabPage();
            this.Champions = new System.Windows.Forms.TabPage();
            this.Skins = new System.Windows.Forms.TabPage();
            this.accountNameTitle = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.Champions.SuspendLayout();
            this.Skins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // SummonerName
            // 
            this.SummonerName.Name = "SummonerName";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Overview);
            this.tabControl1.Controls.Add(this.Champions);
            this.tabControl1.Controls.Add(this.Skins);
            this.tabControl1.Location = new System.Drawing.Point(83, 98);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(871, 503);
            this.tabControl1.TabIndex = 0;
            // 
            // Overview
            // 
            this.Overview.Location = new System.Drawing.Point(4, 24);
            this.Overview.Name = "Overview";
            this.Overview.Padding = new System.Windows.Forms.Padding(3);
            this.Overview.Size = new System.Drawing.Size(863, 475);
            this.Overview.TabIndex = 0;
            this.Overview.Text = "Overview";
            this.Overview.UseVisualStyleBackColor = true;
            // 
            // Champions
            // 
            this.Champions.Controls.Add(this.dataGridView1);
            this.Champions.Location = new System.Drawing.Point(4, 24);
            this.Champions.Name = "Champions";
            this.Champions.Padding = new System.Windows.Forms.Padding(3);
            this.Champions.Size = new System.Drawing.Size(863, 475);
            this.Champions.TabIndex = 1;
            this.Champions.Text = "Champions";
            this.Champions.UseVisualStyleBackColor = true;
            // 
            // Skins
            // 
            this.Skins.Controls.Add(this.dataGridView2);
            this.Skins.Location = new System.Drawing.Point(4, 24);
            this.Skins.Name = "Skins";
            this.Skins.Size = new System.Drawing.Size(863, 475);
            this.Skins.TabIndex = 2;
            this.Skins.Text = "Skins";
            this.Skins.UseVisualStyleBackColor = true;
            // 
            // accountNameTitle
            // 
            this.accountNameTitle.AutoSize = true;
            this.accountNameTitle.BackColor = System.Drawing.Color.Transparent;
            this.accountNameTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.accountNameTitle.Font = new System.Drawing.Font("Segoe Print", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.accountNameTitle.ForeColor = System.Drawing.Color.LightYellow;
            this.accountNameTitle.Location = new System.Drawing.Point(408, 34);
            this.accountNameTitle.Name = "accountNameTitle";
            this.accountNameTitle.Size = new System.Drawing.Size(188, 61);
            this.accountNameTitle.TabIndex = 2;
            this.accountNameTitle.Text = "Template";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(29, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(804, 426);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(29, 25);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 25;
            this.dataGridView2.Size = new System.Drawing.Size(804, 426);
            this.dataGridView2.TabIndex = 1;
            // 
            // SingleAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1037, 627);
            this.Controls.Add(this.accountNameTitle);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SingleAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home - Account Checker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseButton_Click);
            this.tabControl1.ResumeLayout(false);
            this.Champions.ResumeLayout(false);
            this.Skins.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridViewTextBoxColumn SummonerName;
        private TabControl tabControl1;
        private TabPage Overview;
        private TabPage Champions;
        private TabPage Skins;
        private Label accountNameTitle;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
    }
}