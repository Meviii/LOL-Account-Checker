using System.Windows.Forms;

namespace LOLClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.accountsGridView = new System.Windows.Forms.DataGridView();
            this.SummonerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BackButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.accountsGridView)).BeginInit();
            this.SuspendLayout();
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
            // 
            // accountsGridView
            // 
            this.accountsGridView.AllowUserToAddRows = false;
            this.accountsGridView.AllowUserToDeleteRows = false;
            this.accountsGridView.AllowUserToOrderColumns = true;
            this.accountsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.accountsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.accountsGridView.Location = new System.Drawing.Point(110, 142);
            this.accountsGridView.Name = "accountsGridView";
            this.accountsGridView.ReadOnly = true;
            this.accountsGridView.RowTemplate.Height = 25;
            this.accountsGridView.Size = new System.Drawing.Size(823, 447);
            this.accountsGridView.TabIndex = 2;
            this.accountsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.accountsGridView_CellContentClick);
            // 
            // SummonerName
            // 
            this.SummonerName.Name = "SummonerName";
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.LightCyan;
            this.BackButton.FlatAppearance.BorderSize = 0;
            this.BackButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BackButton.Location = new System.Drawing.Point(2, 2);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(79, 35);
            this.BackButton.TabIndex = 7;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // AccountList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1037, 627);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.accountsGridView);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AccountList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home - Account Checker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseButton_Click);
            ((System.ComponentModel.ISupportInitialize)(this.accountsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private DataGridView accountsGridView;
        private DataGridViewTextBoxColumn SummonerName;
        private Button BackButton;
    }
}