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
            SummonerName = new DataGridViewTextBoxColumn();
            tabControl1 = new TabControl();
            Overview = new TabPage();
            ProgressBar = new ProgressBar();
            label8 = new Label();
            label7 = new Label();
            executeButton = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel3 = new Panel();
            CraftKeysCheckBox = new CheckBox();
            label4 = new Label();
            panel4 = new Panel();
            OpenChestsCheckBox = new CheckBox();
            label5 = new Label();
            panel15 = new Panel();
            panel16 = new Panel();
            panel17 = new Panel();
            panel18 = new Panel();
            checkBox15 = new CheckBox();
            label18 = new Label();
            checkBox16 = new CheckBox();
            label19 = new Label();
            panel19 = new Panel();
            checkBox17 = new CheckBox();
            label20 = new Label();
            checkBox18 = new CheckBox();
            label21 = new Label();
            panel20 = new Panel();
            panel21 = new Panel();
            checkBox19 = new CheckBox();
            label22 = new Label();
            checkBox20 = new CheckBox();
            label23 = new Label();
            panel22 = new Panel();
            checkBox21 = new CheckBox();
            label24 = new Label();
            DisenchantChampionShardsCheckBox = new CheckBox();
            label25 = new Label();
            panel7 = new Panel();
            panel11 = new Panel();
            panel12 = new Panel();
            panel13 = new Panel();
            checkBox11 = new CheckBox();
            label14 = new Label();
            checkBox12 = new CheckBox();
            label15 = new Label();
            panel14 = new Panel();
            checkBox13 = new CheckBox();
            label16 = new Label();
            checkBox14 = new CheckBox();
            label17 = new Label();
            panel9 = new Panel();
            panel10 = new Panel();
            checkBox9 = new CheckBox();
            label12 = new Label();
            checkBox10 = new CheckBox();
            label13 = new Label();
            panel8 = new Panel();
            checkBox8 = new CheckBox();
            label11 = new Label();
            DisenchantEternalShardsCheckBox = new CheckBox();
            label10 = new Label();
            panel6 = new Panel();
            OpenCapsulesOrbsShardsCheckBox = new CheckBox();
            label6 = new Label();
            eventTaskPanel = new FlowLayoutPanel();
            panel5 = new Panel();
            claimEvent = new CheckBox();
            label3 = new Label();
            panel1 = new Panel();
            checkBox1 = new CheckBox();
            label1 = new Label();
            panel2 = new Panel();
            checkBox2 = new CheckBox();
            label2 = new Label();
            label9 = new Label();
            overviewGridView = new DataGridView();
            Champions = new TabPage();
            championGridView = new DataGridView();
            Skins = new TabPage();
            skinsGridView = new DataGridView();
            accountNameTitle = new Label();
            BackButton = new Button();
            tabControl1.SuspendLayout();
            Overview.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel15.SuspendLayout();
            panel16.SuspendLayout();
            panel17.SuspendLayout();
            panel18.SuspendLayout();
            panel19.SuspendLayout();
            panel20.SuspendLayout();
            panel21.SuspendLayout();
            panel22.SuspendLayout();
            panel7.SuspendLayout();
            panel11.SuspendLayout();
            panel12.SuspendLayout();
            panel13.SuspendLayout();
            panel14.SuspendLayout();
            panel9.SuspendLayout();
            panel10.SuspendLayout();
            panel8.SuspendLayout();
            panel6.SuspendLayout();
            eventTaskPanel.SuspendLayout();
            panel5.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)overviewGridView).BeginInit();
            Champions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)championGridView).BeginInit();
            Skins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)skinsGridView).BeginInit();
            SuspendLayout();
            // 
            // SummonerName
            // 
            SummonerName.Name = "SummonerName";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Overview);
            tabControl1.Controls.Add(Champions);
            tabControl1.Controls.Add(Skins);
            tabControl1.Location = new System.Drawing.Point(83, 98);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(871, 503);
            tabControl1.TabIndex = 0;
            // 
            // Overview
            // 
            Overview.Controls.Add(ProgressBar);
            Overview.Controls.Add(label8);
            Overview.Controls.Add(label7);
            Overview.Controls.Add(executeButton);
            Overview.Controls.Add(flowLayoutPanel1);
            Overview.Controls.Add(eventTaskPanel);
            Overview.Controls.Add(label9);
            Overview.Controls.Add(overviewGridView);
            Overview.Location = new System.Drawing.Point(4, 24);
            Overview.Name = "Overview";
            Overview.Padding = new Padding(3);
            Overview.Size = new System.Drawing.Size(863, 475);
            Overview.TabIndex = 0;
            Overview.Text = "Overview";
            Overview.UseVisualStyleBackColor = true;
            Overview.Click += Overview_Click;
            // 
            // ProgressBar
            // 
            ProgressBar.BackColor = System.Drawing.Color.LightSkyBlue;
            ProgressBar.Location = new System.Drawing.Point(518, 433);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new System.Drawing.Size(113, 23);
            ProgressBar.TabIndex = 16;
            ProgressBar.Visible = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.FlatStyle = FlatStyle.Flat;
            label8.Font = new System.Drawing.Font("Segoe Print", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label8.ForeColor = System.Drawing.Color.Black;
            label8.Location = new System.Drawing.Point(637, 205);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(91, 33);
            label8.TabIndex = 12;
            label8.Text = "Hextech";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.FlatStyle = FlatStyle.Flat;
            label7.Font = new System.Drawing.Font("Segoe Print", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.ForeColor = System.Drawing.Color.Black;
            label7.Location = new System.Drawing.Point(622, 65);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(125, 33);
            label7.TabIndex = 11;
            label7.Text = "Event Shop";
            // 
            // executeButton
            // 
            executeButton.BackColor = System.Drawing.Color.LightCyan;
            executeButton.FlatAppearance.BorderSize = 0;
            executeButton.ForeColor = System.Drawing.SystemColors.Desktop;
            executeButton.Location = new System.Drawing.Point(644, 427);
            executeButton.Name = "executeButton";
            executeButton.Size = new System.Drawing.Size(79, 32);
            executeButton.TabIndex = 9;
            executeButton.Text = "Execute";
            executeButton.UseVisualStyleBackColor = false;
            executeButton.Click += ExecuteButton_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(panel3);
            flowLayoutPanel1.Controls.Add(panel4);
            flowLayoutPanel1.Controls.Add(panel15);
            flowLayoutPanel1.Controls.Add(panel7);
            flowLayoutPanel1.Controls.Add(panel6);
            flowLayoutPanel1.Location = new System.Drawing.Point(508, 245);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(338, 178);
            flowLayoutPanel1.TabIndex = 15;
            // 
            // panel3
            // 
            panel3.Controls.Add(CraftKeysCheckBox);
            panel3.Controls.Add(label4);
            panel3.Location = new System.Drawing.Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(160, 38);
            panel3.TabIndex = 13;
            // 
            // CraftKeysCheckBox
            // 
            CraftKeysCheckBox.AutoSize = true;
            CraftKeysCheckBox.Location = new System.Drawing.Point(134, 13);
            CraftKeysCheckBox.Name = "CraftKeysCheckBox";
            CraftKeysCheckBox.Size = new System.Drawing.Size(15, 14);
            CraftKeysCheckBox.TabIndex = 1;
            CraftKeysCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(3, 12);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(60, 15);
            label4.TabIndex = 0;
            label4.Text = "Craft Keys";
            // 
            // panel4
            // 
            panel4.Controls.Add(OpenChestsCheckBox);
            panel4.Controls.Add(label5);
            panel4.Location = new System.Drawing.Point(169, 3);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(160, 38);
            panel4.TabIndex = 14;
            // 
            // OpenChestsCheckBox
            // 
            OpenChestsCheckBox.AutoSize = true;
            OpenChestsCheckBox.Location = new System.Drawing.Point(134, 13);
            OpenChestsCheckBox.Name = "OpenChestsCheckBox";
            OpenChestsCheckBox.Size = new System.Drawing.Size(15, 14);
            OpenChestsCheckBox.TabIndex = 1;
            OpenChestsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(3, 12);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(74, 15);
            label5.TabIndex = 0;
            label5.Text = "Open Chests";
            // 
            // panel15
            // 
            panel15.Controls.Add(panel16);
            panel15.Controls.Add(panel20);
            panel15.Controls.Add(panel22);
            panel15.Controls.Add(DisenchantChampionShardsCheckBox);
            panel15.Controls.Add(label25);
            panel15.Location = new System.Drawing.Point(3, 47);
            panel15.Name = "panel15";
            panel15.Size = new System.Drawing.Size(330, 38);
            panel15.TabIndex = 17;
            // 
            // panel16
            // 
            panel16.Controls.Add(panel17);
            panel16.Controls.Add(panel19);
            panel16.Controls.Add(checkBox18);
            panel16.Controls.Add(label21);
            panel16.Location = new System.Drawing.Point(169, 91);
            panel16.Name = "panel16";
            panel16.Size = new System.Drawing.Size(160, 38);
            panel16.TabIndex = 16;
            // 
            // panel17
            // 
            panel17.Controls.Add(panel18);
            panel17.Controls.Add(checkBox16);
            panel17.Controls.Add(label19);
            panel17.Location = new System.Drawing.Point(169, 91);
            panel17.Name = "panel17";
            panel17.Size = new System.Drawing.Size(160, 38);
            panel17.TabIndex = 15;
            // 
            // panel18
            // 
            panel18.Controls.Add(checkBox15);
            panel18.Controls.Add(label18);
            panel18.Location = new System.Drawing.Point(169, 91);
            panel18.Name = "panel18";
            panel18.Size = new System.Drawing.Size(160, 38);
            panel18.TabIndex = 14;
            // 
            // checkBox15
            // 
            checkBox15.AutoSize = true;
            checkBox15.Location = new System.Drawing.Point(134, 13);
            checkBox15.Name = "checkBox15";
            checkBox15.Size = new System.Drawing.Size(15, 14);
            checkBox15.TabIndex = 1;
            checkBox15.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(3, 12);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(60, 15);
            label18.TabIndex = 0;
            label18.Text = "Craft Keys";
            // 
            // checkBox16
            // 
            checkBox16.AutoSize = true;
            checkBox16.Location = new System.Drawing.Point(134, 13);
            checkBox16.Name = "checkBox16";
            checkBox16.Size = new System.Drawing.Size(15, 14);
            checkBox16.TabIndex = 1;
            checkBox16.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(3, 12);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(60, 15);
            label19.TabIndex = 0;
            label19.Text = "Craft Keys";
            // 
            // panel19
            // 
            panel19.Controls.Add(checkBox17);
            panel19.Controls.Add(label20);
            panel19.Location = new System.Drawing.Point(169, 91);
            panel19.Name = "panel19";
            panel19.Size = new System.Drawing.Size(160, 38);
            panel19.TabIndex = 14;
            // 
            // checkBox17
            // 
            checkBox17.AutoSize = true;
            checkBox17.Location = new System.Drawing.Point(134, 13);
            checkBox17.Name = "checkBox17";
            checkBox17.Size = new System.Drawing.Size(15, 14);
            checkBox17.TabIndex = 1;
            checkBox17.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(3, 12);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(60, 15);
            label20.TabIndex = 0;
            label20.Text = "Craft Keys";
            // 
            // checkBox18
            // 
            checkBox18.AutoSize = true;
            checkBox18.Location = new System.Drawing.Point(134, 13);
            checkBox18.Name = "checkBox18";
            checkBox18.Size = new System.Drawing.Size(15, 14);
            checkBox18.TabIndex = 1;
            checkBox18.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new System.Drawing.Point(3, 12);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(60, 15);
            label21.TabIndex = 0;
            label21.Text = "Craft Keys";
            // 
            // panel20
            // 
            panel20.Controls.Add(panel21);
            panel20.Controls.Add(checkBox20);
            panel20.Controls.Add(label23);
            panel20.Location = new System.Drawing.Point(169, 91);
            panel20.Name = "panel20";
            panel20.Size = new System.Drawing.Size(160, 38);
            panel20.TabIndex = 15;
            // 
            // panel21
            // 
            panel21.Controls.Add(checkBox19);
            panel21.Controls.Add(label22);
            panel21.Location = new System.Drawing.Point(169, 91);
            panel21.Name = "panel21";
            panel21.Size = new System.Drawing.Size(160, 38);
            panel21.TabIndex = 14;
            // 
            // checkBox19
            // 
            checkBox19.AutoSize = true;
            checkBox19.Location = new System.Drawing.Point(134, 13);
            checkBox19.Name = "checkBox19";
            checkBox19.Size = new System.Drawing.Size(15, 14);
            checkBox19.TabIndex = 1;
            checkBox19.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(3, 12);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(60, 15);
            label22.TabIndex = 0;
            label22.Text = "Craft Keys";
            // 
            // checkBox20
            // 
            checkBox20.AutoSize = true;
            checkBox20.Location = new System.Drawing.Point(134, 13);
            checkBox20.Name = "checkBox20";
            checkBox20.Size = new System.Drawing.Size(15, 14);
            checkBox20.TabIndex = 1;
            checkBox20.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new System.Drawing.Point(3, 12);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(60, 15);
            label23.TabIndex = 0;
            label23.Text = "Craft Keys";
            // 
            // panel22
            // 
            panel22.Controls.Add(checkBox21);
            panel22.Controls.Add(label24);
            panel22.Location = new System.Drawing.Point(169, 91);
            panel22.Name = "panel22";
            panel22.Size = new System.Drawing.Size(160, 38);
            panel22.TabIndex = 14;
            // 
            // checkBox21
            // 
            checkBox21.AutoSize = true;
            checkBox21.Location = new System.Drawing.Point(134, 13);
            checkBox21.Name = "checkBox21";
            checkBox21.Size = new System.Drawing.Size(15, 14);
            checkBox21.TabIndex = 1;
            checkBox21.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new System.Drawing.Point(3, 12);
            label24.Name = "label24";
            label24.Size = new System.Drawing.Size(60, 15);
            label24.TabIndex = 0;
            label24.Text = "Craft Keys";
            // 
            // DisenchantChampionShardsCheckBox
            // 
            DisenchantChampionShardsCheckBox.AutoSize = true;
            DisenchantChampionShardsCheckBox.Location = new System.Drawing.Point(300, 13);
            DisenchantChampionShardsCheckBox.Name = "DisenchantChampionShardsCheckBox";
            DisenchantChampionShardsCheckBox.Size = new System.Drawing.Size(15, 14);
            DisenchantChampionShardsCheckBox.TabIndex = 1;
            DisenchantChampionShardsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new System.Drawing.Point(3, 12);
            label25.Name = "label25";
            label25.Size = new System.Drawing.Size(169, 15);
            label25.TabIndex = 0;
            label25.Text = "Disenchcant Champion Shards";
            // 
            // panel7
            // 
            panel7.Controls.Add(panel11);
            panel7.Controls.Add(panel9);
            panel7.Controls.Add(panel8);
            panel7.Controls.Add(DisenchantEternalShardsCheckBox);
            panel7.Controls.Add(label10);
            panel7.Location = new System.Drawing.Point(3, 91);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(330, 38);
            panel7.TabIndex = 14;
            // 
            // panel11
            // 
            panel11.Controls.Add(panel12);
            panel11.Controls.Add(panel14);
            panel11.Controls.Add(checkBox14);
            panel11.Controls.Add(label17);
            panel11.Location = new System.Drawing.Point(169, 91);
            panel11.Name = "panel11";
            panel11.Size = new System.Drawing.Size(160, 38);
            panel11.TabIndex = 16;
            // 
            // panel12
            // 
            panel12.Controls.Add(panel13);
            panel12.Controls.Add(checkBox12);
            panel12.Controls.Add(label15);
            panel12.Location = new System.Drawing.Point(169, 91);
            panel12.Name = "panel12";
            panel12.Size = new System.Drawing.Size(160, 38);
            panel12.TabIndex = 15;
            // 
            // panel13
            // 
            panel13.Controls.Add(checkBox11);
            panel13.Controls.Add(label14);
            panel13.Location = new System.Drawing.Point(169, 91);
            panel13.Name = "panel13";
            panel13.Size = new System.Drawing.Size(160, 38);
            panel13.TabIndex = 14;
            // 
            // checkBox11
            // 
            checkBox11.AutoSize = true;
            checkBox11.Location = new System.Drawing.Point(134, 13);
            checkBox11.Name = "checkBox11";
            checkBox11.Size = new System.Drawing.Size(15, 14);
            checkBox11.TabIndex = 1;
            checkBox11.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(3, 12);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(60, 15);
            label14.TabIndex = 0;
            label14.Text = "Craft Keys";
            // 
            // checkBox12
            // 
            checkBox12.AutoSize = true;
            checkBox12.Location = new System.Drawing.Point(134, 13);
            checkBox12.Name = "checkBox12";
            checkBox12.Size = new System.Drawing.Size(15, 14);
            checkBox12.TabIndex = 1;
            checkBox12.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(3, 12);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(60, 15);
            label15.TabIndex = 0;
            label15.Text = "Craft Keys";
            // 
            // panel14
            // 
            panel14.Controls.Add(checkBox13);
            panel14.Controls.Add(label16);
            panel14.Location = new System.Drawing.Point(169, 91);
            panel14.Name = "panel14";
            panel14.Size = new System.Drawing.Size(160, 38);
            panel14.TabIndex = 14;
            // 
            // checkBox13
            // 
            checkBox13.AutoSize = true;
            checkBox13.Location = new System.Drawing.Point(134, 13);
            checkBox13.Name = "checkBox13";
            checkBox13.Size = new System.Drawing.Size(15, 14);
            checkBox13.TabIndex = 1;
            checkBox13.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(3, 12);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(60, 15);
            label16.TabIndex = 0;
            label16.Text = "Craft Keys";
            // 
            // checkBox14
            // 
            checkBox14.AutoSize = true;
            checkBox14.Location = new System.Drawing.Point(134, 13);
            checkBox14.Name = "checkBox14";
            checkBox14.Size = new System.Drawing.Size(15, 14);
            checkBox14.TabIndex = 1;
            checkBox14.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(3, 12);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(60, 15);
            label17.TabIndex = 0;
            label17.Text = "Craft Keys";
            // 
            // panel9
            // 
            panel9.Controls.Add(panel10);
            panel9.Controls.Add(checkBox10);
            panel9.Controls.Add(label13);
            panel9.Location = new System.Drawing.Point(169, 91);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(160, 38);
            panel9.TabIndex = 15;
            // 
            // panel10
            // 
            panel10.Controls.Add(checkBox9);
            panel10.Controls.Add(label12);
            panel10.Location = new System.Drawing.Point(169, 91);
            panel10.Name = "panel10";
            panel10.Size = new System.Drawing.Size(160, 38);
            panel10.TabIndex = 14;
            // 
            // checkBox9
            // 
            checkBox9.AutoSize = true;
            checkBox9.Location = new System.Drawing.Point(134, 13);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new System.Drawing.Size(15, 14);
            checkBox9.TabIndex = 1;
            checkBox9.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(3, 12);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(60, 15);
            label12.TabIndex = 0;
            label12.Text = "Craft Keys";
            // 
            // checkBox10
            // 
            checkBox10.AutoSize = true;
            checkBox10.Location = new System.Drawing.Point(134, 13);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new System.Drawing.Size(15, 14);
            checkBox10.TabIndex = 1;
            checkBox10.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(3, 12);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(60, 15);
            label13.TabIndex = 0;
            label13.Text = "Craft Keys";
            // 
            // panel8
            // 
            panel8.Controls.Add(checkBox8);
            panel8.Controls.Add(label11);
            panel8.Location = new System.Drawing.Point(169, 91);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(160, 38);
            panel8.TabIndex = 14;
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Location = new System.Drawing.Point(134, 13);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new System.Drawing.Size(15, 14);
            checkBox8.TabIndex = 1;
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(3, 12);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(60, 15);
            label11.TabIndex = 0;
            label11.Text = "Craft Keys";
            // 
            // DisenchantEternalShardsCheckBox
            // 
            DisenchantEternalShardsCheckBox.AutoSize = true;
            DisenchantEternalShardsCheckBox.Location = new System.Drawing.Point(300, 13);
            DisenchantEternalShardsCheckBox.Name = "DisenchantEternalShardsCheckBox";
            DisenchantEternalShardsCheckBox.Size = new System.Drawing.Size(15, 14);
            DisenchantEternalShardsCheckBox.TabIndex = 1;
            DisenchantEternalShardsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(4, 12);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(143, 15);
            label10.TabIndex = 0;
            label10.Text = "Disenchant Eternal Shards";
            // 
            // panel6
            // 
            panel6.Controls.Add(OpenCapsulesOrbsShardsCheckBox);
            panel6.Controls.Add(label6);
            panel6.Location = new System.Drawing.Point(3, 135);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(330, 38);
            panel6.TabIndex = 14;
            // 
            // OpenCapsulesOrbsShardsCheckBox
            // 
            OpenCapsulesOrbsShardsCheckBox.AutoSize = true;
            OpenCapsulesOrbsShardsCheckBox.Location = new System.Drawing.Point(300, 13);
            OpenCapsulesOrbsShardsCheckBox.Name = "OpenCapsulesOrbsShardsCheckBox";
            OpenCapsulesOrbsShardsCheckBox.Size = new System.Drawing.Size(15, 14);
            OpenCapsulesOrbsShardsCheckBox.TabIndex = 1;
            OpenCapsulesOrbsShardsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(3, 12);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(206, 15);
            label6.TabIndex = 0;
            label6.Text = "Open Capsules, Orbs, Random Shards";
            // 
            // eventTaskPanel
            // 
            eventTaskPanel.Controls.Add(panel5);
            eventTaskPanel.Controls.Add(panel1);
            eventTaskPanel.Controls.Add(panel2);
            eventTaskPanel.Location = new System.Drawing.Point(508, 106);
            eventTaskPanel.Name = "eventTaskPanel";
            eventTaskPanel.Size = new System.Drawing.Size(338, 93);
            eventTaskPanel.TabIndex = 10;
            // 
            // panel5
            // 
            panel5.Controls.Add(claimEvent);
            panel5.Controls.Add(label3);
            panel5.Location = new System.Drawing.Point(3, 3);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(160, 38);
            panel5.TabIndex = 13;
            // 
            // claimEvent
            // 
            claimEvent.AutoSize = true;
            claimEvent.Location = new System.Drawing.Point(134, 13);
            claimEvent.Name = "claimEvent";
            claimEvent.Size = new System.Drawing.Size(15, 14);
            claimEvent.TabIndex = 1;
            claimEvent.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 12);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(117, 15);
            label3.TabIndex = 0;
            label3.Text = "Claim Event Rewards";
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(169, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(160, 38);
            panel1.TabIndex = 14;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(134, 13);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(15, 14);
            checkBox1.TabIndex = 1;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(124, 15);
            label1.TabIndex = 0;
            label1.Text = "Buy Champion Shards";
            // 
            // panel2
            // 
            panel2.Controls.Add(checkBox2);
            panel2.Controls.Add(label2);
            panel2.Location = new System.Drawing.Point(3, 47);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(160, 38);
            panel2.TabIndex = 14;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new System.Drawing.Point(134, 13);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(15, 14);
            checkBox2.TabIndex = 1;
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 12);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(97, 15);
            label2.TabIndex = 0;
            label2.Text = "Buy Blue Essence";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.FlatStyle = FlatStyle.Flat;
            label9.Font = new System.Drawing.Font("Segoe Print", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label9.ForeColor = System.Drawing.Color.Black;
            label9.Location = new System.Drawing.Point(636, 17);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(92, 47);
            label9.TabIndex = 9;
            label9.Text = "Tasks";
            // 
            // overviewGridView
            // 
            overviewGridView.AllowUserToAddRows = false;
            overviewGridView.AllowUserToDeleteRows = false;
            overviewGridView.AllowUserToOrderColumns = true;
            overviewGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            overviewGridView.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            overviewGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            overviewGridView.GridColor = System.Drawing.Color.LightSkyBlue;
            overviewGridView.Location = new System.Drawing.Point(15, 20);
            overviewGridView.Name = "overviewGridView";
            overviewGridView.ReadOnly = true;
            overviewGridView.RowTemplate.Height = 25;
            overviewGridView.Size = new System.Drawing.Size(477, 436);
            overviewGridView.TabIndex = 9;
            // 
            // Champions
            // 
            Champions.Controls.Add(championGridView);
            Champions.Location = new System.Drawing.Point(4, 24);
            Champions.Name = "Champions";
            Champions.Padding = new Padding(3);
            Champions.Size = new System.Drawing.Size(863, 475);
            Champions.TabIndex = 1;
            Champions.Text = "Champions";
            Champions.UseVisualStyleBackColor = true;
            // 
            // championGridView
            // 
            championGridView.AllowUserToAddRows = false;
            championGridView.AllowUserToDeleteRows = false;
            championGridView.AllowUserToOrderColumns = true;
            championGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            championGridView.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            championGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            championGridView.GridColor = System.Drawing.Color.LightSkyBlue;
            championGridView.Location = new System.Drawing.Point(29, 24);
            championGridView.Name = "championGridView";
            championGridView.ReadOnly = true;
            championGridView.RowTemplate.Height = 25;
            championGridView.Size = new System.Drawing.Size(804, 426);
            championGridView.TabIndex = 2;
            // 
            // Skins
            // 
            Skins.Controls.Add(skinsGridView);
            Skins.Location = new System.Drawing.Point(4, 24);
            Skins.Name = "Skins";
            Skins.Size = new System.Drawing.Size(863, 475);
            Skins.TabIndex = 2;
            Skins.Text = "Skins";
            Skins.UseVisualStyleBackColor = true;
            // 
            // skinsGridView
            // 
            skinsGridView.AllowUserToAddRows = false;
            skinsGridView.AllowUserToDeleteRows = false;
            skinsGridView.AllowUserToOrderColumns = true;
            skinsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            skinsGridView.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            skinsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            skinsGridView.GridColor = System.Drawing.Color.LightSkyBlue;
            skinsGridView.Location = new System.Drawing.Point(29, 24);
            skinsGridView.Name = "skinsGridView";
            skinsGridView.ReadOnly = true;
            skinsGridView.RowTemplate.Height = 25;
            skinsGridView.Size = new System.Drawing.Size(804, 426);
            skinsGridView.TabIndex = 1;
            // 
            // accountNameTitle
            // 
            accountNameTitle.AutoSize = true;
            accountNameTitle.BackColor = System.Drawing.Color.Transparent;
            accountNameTitle.FlatStyle = FlatStyle.Flat;
            accountNameTitle.Font = new System.Drawing.Font("Segoe Print", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            accountNameTitle.ForeColor = System.Drawing.Color.LightYellow;
            accountNameTitle.Location = new System.Drawing.Point(408, 34);
            accountNameTitle.Name = "accountNameTitle";
            accountNameTitle.Size = new System.Drawing.Size(188, 61);
            accountNameTitle.TabIndex = 2;
            accountNameTitle.Text = "Template";
            // 
            // BackButton
            // 
            BackButton.BackColor = System.Drawing.Color.LightCyan;
            BackButton.FlatAppearance.BorderSize = 0;
            BackButton.ForeColor = System.Drawing.SystemColors.Desktop;
            BackButton.Location = new System.Drawing.Point(2, 3);
            BackButton.Name = "BackButton";
            BackButton.Size = new System.Drawing.Size(79, 35);
            BackButton.TabIndex = 8;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = false;
            BackButton.Click += BackButton_Click_1;
            // 
            // SingleAccount
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Desktop;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1037, 627);
            Controls.Add(BackButton);
            Controls.Add(accountNameTitle);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "SingleAccount";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Single Account - Account Checker";
            FormClosing += CloseButton_Click;
            tabControl1.ResumeLayout(false);
            Overview.ResumeLayout(false);
            Overview.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel15.ResumeLayout(false);
            panel15.PerformLayout();
            panel16.ResumeLayout(false);
            panel16.PerformLayout();
            panel17.ResumeLayout(false);
            panel17.PerformLayout();
            panel18.ResumeLayout(false);
            panel18.PerformLayout();
            panel19.ResumeLayout(false);
            panel19.PerformLayout();
            panel20.ResumeLayout(false);
            panel20.PerformLayout();
            panel21.ResumeLayout(false);
            panel21.PerformLayout();
            panel22.ResumeLayout(false);
            panel22.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            eventTaskPanel.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)overviewGridView).EndInit();
            Champions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)championGridView).EndInit();
            Skins.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)skinsGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridViewTextBoxColumn SummonerName;
        private TabControl tabControl1;
        private TabPage Overview;
        private TabPage Champions;
        private TabPage Skins;
        private Label accountNameTitle;
        private DataGridView skinsGridView;
        private DataGridView championGridView;
        private Button BackButton;
        private DataGridView overviewGridView;
        private Label label9;
        private FlowLayoutPanel eventTaskPanel;
        private Button executeButton;
        private Panel panel5;
        private CheckBox claimEvent;
        private Label label3;
        private Label label8;
        private Label label7;
        private Panel panel1;
        private CheckBox checkBox1;
        private Label label1;
        private Panel panel2;
        private CheckBox checkBox2;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel3;
        private CheckBox CraftKeysCheckBox;
        private Label label4;
        private Panel panel4;
        private CheckBox OpenChestsCheckBox;
        private Label label5;
        private Panel panel6;
        private CheckBox OpenCapsulesOrbsShardsCheckBox;
        private Label label6;
        private Panel panel7;
        private Panel panel9;
        private Panel panel10;
        private CheckBox checkBox9;
        private Label label12;
        private CheckBox checkBox10;
        private Label label13;
        private Panel panel8;
        private CheckBox checkBox8;
        private Label label11;
        private CheckBox DisenchantEternalShardsCheckBox;
        private Label label10;
        private Panel panel15;
        private Panel panel16;
        private Panel panel17;
        private Panel panel18;
        private CheckBox checkBox15;
        private Label label18;
        private CheckBox checkBox16;
        private Label label19;
        private Panel panel19;
        private CheckBox checkBox17;
        private Label label20;
        private CheckBox checkBox18;
        private Label label21;
        private Panel panel20;
        private Panel panel21;
        private CheckBox checkBox19;
        private Label label22;
        private CheckBox checkBox20;
        private Label label23;
        private Panel panel22;
        private CheckBox checkBox21;
        private Label label24;
        private CheckBox DisenchantChampionShardsCheckBox;
        private Label label25;
        private Panel panel11;
        private Panel panel12;
        private Panel panel13;
        private CheckBox checkBox11;
        private Label label14;
        private CheckBox checkBox12;
        private Label label15;
        private Panel panel14;
        private CheckBox checkBox13;
        private Label label16;
        private CheckBox checkBox14;
        private Label label17;
        private ProgressBar ProgressBar;
    }
}