namespace DutchVACCATISGenerator
{
    partial class DutchVACCATISGenerator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DutchVACCATISGenerator));
            this.getMetarButton = new System.Windows.Forms.Button();
            this.icaoTextBox = new System.Windows.Forms.TextBox();
            this.metarBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.metarTextBox = new System.Windows.Forms.TextBox();
            this.lastLabel = new System.Windows.Forms.Label();
            this.processMetarButton = new System.Windows.Forms.Button();
            this.atisIndexGroupBox = new System.Windows.Forms.GroupBox();
            this.nextATISLetterButton = new System.Windows.Forms.Button();
            this.previousATISLetterButton = new System.Windows.Forms.Button();
            this.atisLetterLabel = new System.Windows.Forms.Label();
            this.EHAMmainRunwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMmainLandingRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMmainLandingRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EHAMmainLandingRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHAMmainDepartureRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMmainDepartureRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EHAMmainDepartureRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHAMsecondaryLandingRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EHAMsecondaryLandingRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHAMsecondaryLandingRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMsecondaryRunwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMsecondaryDepartureRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMsecondaryDepartureRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHAMsecondaryDepartureRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.tlLevelGroupBox = new System.Windows.Forms.GroupBox();
            this.tlOutLabel = new System.Windows.Forms.Label();
            this.tlHeaderLabel = new System.Windows.Forms.Label();
            this.generateATISButton = new System.Windows.Forms.Button();
            this.metarInputGroupBox = new System.Windows.Forms.GroupBox();
            this.fetchMetarLabel = new System.Windows.Forms.Label();
            this.getSelectBestRunwayButton = new System.Windows.Forms.Button();
            this.copyOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.outputOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.outputGroupBox = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.ICAOTabControl = new System.Windows.Forms.TabControl();
            this.EHAM = new System.Windows.Forms.TabPage();
            this.EHBK = new System.Windows.Forms.TabPage();
            this.EHBKmainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHBKchildMainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHBKmainRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EHBKmainRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHEH = new System.Windows.Forms.TabPage();
            this.EHEHmainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHEHchildMainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHEHmainRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EHEHmainRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHGG = new System.Windows.Forms.TabPage();
            this.EHGGmainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHGGchildMainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHGGmainRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EHGGmainRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHRD = new System.Windows.Forms.TabPage();
            this.EHRDmainRubwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHRDchildMainRunwyaGroupBox = new System.Windows.Forms.GroupBox();
            this.EHRDmainRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EHRDmainRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amsterdamInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dutchVACCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fetchMETAREvery30MinutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoProcessMETARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runwayInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tAFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.additionalOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.userDefinedWaveCheckBox = new System.Windows.Forms.CheckBox();
            this.addWindRecordCheckBox = new System.Windows.Forms.CheckBox();
            this.markTempCheckBox = new System.Windows.Forms.CheckBox();
            this.appArrOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.arrOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.appOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.runwayInfoButton = new System.Windows.Forms.Button();
            this.soundButton = new System.Windows.Forms.Button();
            this.versionBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.realRunwayBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.metarFetchTimer = new System.Windows.Forms.Timer(this.components);
            this.atisIndexGroupBox.SuspendLayout();
            this.EHAMmainRunwaysGroupBox.SuspendLayout();
            this.EHAMmainLandingRunwayGroupBox.SuspendLayout();
            this.EHAMmainDepartureRunwayGroupBox.SuspendLayout();
            this.EHAMsecondaryLandingRunwayGroupBox.SuspendLayout();
            this.EHAMsecondaryRunwaysGroupBox.SuspendLayout();
            this.EHAMsecondaryDepartureRunwayGroupBox.SuspendLayout();
            this.tlLevelGroupBox.SuspendLayout();
            this.metarInputGroupBox.SuspendLayout();
            this.outputOptionsGroupBox.SuspendLayout();
            this.outputGroupBox.SuspendLayout();
            this.ICAOTabControl.SuspendLayout();
            this.EHAM.SuspendLayout();
            this.EHBK.SuspendLayout();
            this.EHBKmainRunwayGroupBox.SuspendLayout();
            this.EHBKchildMainRunwayGroupBox.SuspendLayout();
            this.EHEH.SuspendLayout();
            this.EHEHmainRunwayGroupBox.SuspendLayout();
            this.EHEHchildMainRunwayGroupBox.SuspendLayout();
            this.EHGG.SuspendLayout();
            this.EHGGmainRunwayGroupBox.SuspendLayout();
            this.EHGGchildMainRunwayGroupBox.SuspendLayout();
            this.EHRD.SuspendLayout();
            this.EHRDmainRubwayGroupBox.SuspendLayout();
            this.EHRDchildMainRunwyaGroupBox.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.additionalOptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // getMetarButton
            // 
            this.getMetarButton.Enabled = false;
            this.getMetarButton.Location = new System.Drawing.Point(50, 18);
            this.getMetarButton.Name = "getMetarButton";
            this.getMetarButton.Size = new System.Drawing.Size(75, 22);
            this.getMetarButton.TabIndex = 2;
            this.getMetarButton.Text = "Get METAR";
            this.getMetarButton.UseVisualStyleBackColor = true;
            this.getMetarButton.Click += new System.EventHandler(this.getMetarButton_Click);
            // 
            // icaoTextBox
            // 
            this.icaoTextBox.Location = new System.Drawing.Point(6, 19);
            this.icaoTextBox.MaxLength = 4;
            this.icaoTextBox.Name = "icaoTextBox";
            this.icaoTextBox.Size = new System.Drawing.Size(38, 20);
            this.icaoTextBox.TabIndex = 1;
            this.icaoTextBox.Text = "EHAM";
            this.icaoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metarBackgroundWorker
            // 
            this.metarBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.metarBackgroundWorker_DoWork);
            this.metarBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.metarBackgroundWorker_RunWorkerCompleted);
            // 
            // metarTextBox
            // 
            this.metarTextBox.Location = new System.Drawing.Point(6, 45);
            this.metarTextBox.Multiline = true;
            this.metarTextBox.Name = "metarTextBox";
            this.metarTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metarTextBox.Size = new System.Drawing.Size(436, 32);
            this.metarTextBox.TabIndex = 3;
            this.metarTextBox.TextChanged += new System.EventHandler(this.metarTextBox_TextChanged);
            // 
            // lastLabel
            // 
            this.lastLabel.AutoSize = true;
            this.lastLabel.Location = new System.Drawing.Point(6, 80);
            this.lastLabel.Name = "lastLabel";
            this.lastLabel.Size = new System.Drawing.Size(0, 13);
            this.lastLabel.TabIndex = 0;
            // 
            // processMetarButton
            // 
            this.processMetarButton.Enabled = false;
            this.processMetarButton.Location = new System.Drawing.Point(445, 44);
            this.processMetarButton.Name = "processMetarButton";
            this.processMetarButton.Size = new System.Drawing.Size(84, 83);
            this.processMetarButton.TabIndex = 4;
            this.processMetarButton.Text = "Process METAR";
            this.processMetarButton.UseVisualStyleBackColor = true;
            this.processMetarButton.Click += new System.EventHandler(this.processMetarButton_Click);
            // 
            // atisIndexGroupBox
            // 
            this.atisIndexGroupBox.Controls.Add(this.nextATISLetterButton);
            this.atisIndexGroupBox.Controls.Add(this.previousATISLetterButton);
            this.atisIndexGroupBox.Controls.Add(this.atisLetterLabel);
            this.atisIndexGroupBox.Location = new System.Drawing.Point(13, 406);
            this.atisIndexGroupBox.Name = "atisIndexGroupBox";
            this.atisIndexGroupBox.Size = new System.Drawing.Size(116, 112);
            this.atisIndexGroupBox.TabIndex = 0;
            this.atisIndexGroupBox.TabStop = false;
            // 
            // nextATISLetterButton
            // 
            this.nextATISLetterButton.Location = new System.Drawing.Point(68, 63);
            this.nextATISLetterButton.Name = "nextATISLetterButton";
            this.nextATISLetterButton.Size = new System.Drawing.Size(40, 40);
            this.nextATISLetterButton.TabIndex = 19;
            this.nextATISLetterButton.Text = ">";
            this.nextATISLetterButton.UseVisualStyleBackColor = true;
            this.nextATISLetterButton.Click += new System.EventHandler(this.nextATISLetterButton_Click);
            // 
            // previousATISLetterButton
            // 
            this.previousATISLetterButton.Location = new System.Drawing.Point(7, 63);
            this.previousATISLetterButton.Name = "previousATISLetterButton";
            this.previousATISLetterButton.Size = new System.Drawing.Size(40, 40);
            this.previousATISLetterButton.TabIndex = 18;
            this.previousATISLetterButton.Text = "<";
            this.previousATISLetterButton.UseVisualStyleBackColor = true;
            this.previousATISLetterButton.Click += new System.EventHandler(this.previousATISLetterButton_Click);
            // 
            // atisLetterLabel
            // 
            this.atisLetterLabel.AutoSize = true;
            this.atisLetterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.atisLetterLabel.Location = new System.Drawing.Point(39, 17);
            this.atisLetterLabel.Name = "atisLetterLabel";
            this.atisLetterLabel.Size = new System.Drawing.Size(41, 39);
            this.atisLetterLabel.TabIndex = 0;
            this.atisLetterLabel.Text = "A";
            this.atisLetterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EHAMmainRunwaysGroupBox
            // 
            this.EHAMmainRunwaysGroupBox.Controls.Add(this.EHAMmainLandingRunwayGroupBox);
            this.EHAMmainRunwaysGroupBox.Controls.Add(this.EHAMmainDepartureRunwayGroupBox);
            this.EHAMmainRunwaysGroupBox.Location = new System.Drawing.Point(3, 3);
            this.EHAMmainRunwaysGroupBox.Name = "EHAMmainRunwaysGroupBox";
            this.EHAMmainRunwaysGroupBox.Size = new System.Drawing.Size(353, 96);
            this.EHAMmainRunwaysGroupBox.TabIndex = 0;
            this.EHAMmainRunwaysGroupBox.TabStop = false;
            this.EHAMmainRunwaysGroupBox.Text = "Main Runway(s)";
            // 
            // EHAMmainLandingRunwayGroupBox
            // 
            this.EHAMmainLandingRunwayGroupBox.Controls.Add(this.EHAMmainLandingRunwayCheckBox);
            this.EHAMmainLandingRunwayGroupBox.Controls.Add(this.EHAMmainLandingRunwayComboBox);
            this.EHAMmainLandingRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHAMmainLandingRunwayGroupBox.Name = "EHAMmainLandingRunwayGroupBox";
            this.EHAMmainLandingRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHAMmainLandingRunwayGroupBox.TabIndex = 0;
            this.EHAMmainLandingRunwayGroupBox.TabStop = false;
            // 
            // EHAMmainLandingRunwayCheckBox
            // 
            this.EHAMmainLandingRunwayCheckBox.AutoSize = true;
            this.EHAMmainLandingRunwayCheckBox.Checked = true;
            this.EHAMmainLandingRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EHAMmainLandingRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EHAMmainLandingRunwayCheckBox.Name = "EHAMmainLandingRunwayCheckBox";
            this.EHAMmainLandingRunwayCheckBox.Size = new System.Drawing.Size(123, 17);
            this.EHAMmainLandingRunwayCheckBox.TabIndex = 6;
            this.EHAMmainLandingRunwayCheckBox.Text = "Main landing runway";
            this.EHAMmainLandingRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EHAMmainLandingRunwayCheckBox.CheckedChanged += new System.EventHandler(this.mainLandingRunwayCheckBox_CheckedChanged);
            // 
            // EHAMmainLandingRunwayComboBox
            // 
            this.EHAMmainLandingRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EHAMmainLandingRunwayComboBox.FormattingEnabled = true;
            this.EHAMmainLandingRunwayComboBox.Items.AddRange(new object[] {
            "04",
            "06",
            "09",
            "18C",
            "18R",
            "22",
            "24",
            "27",
            "36C",
            "36R"});
            this.EHAMmainLandingRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EHAMmainLandingRunwayComboBox.Name = "EHAMmainLandingRunwayComboBox";
            this.EHAMmainLandingRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EHAMmainLandingRunwayComboBox.TabIndex = 7;
            // 
            // EHAMmainDepartureRunwayGroupBox
            // 
            this.EHAMmainDepartureRunwayGroupBox.Controls.Add(this.EHAMmainDepartureRunwayCheckBox);
            this.EHAMmainDepartureRunwayGroupBox.Controls.Add(this.EHAMmainDepartureRunwayComboBox);
            this.EHAMmainDepartureRunwayGroupBox.Location = new System.Drawing.Point(177, 19);
            this.EHAMmainDepartureRunwayGroupBox.Name = "EHAMmainDepartureRunwayGroupBox";
            this.EHAMmainDepartureRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHAMmainDepartureRunwayGroupBox.TabIndex = 0;
            this.EHAMmainDepartureRunwayGroupBox.TabStop = false;
            // 
            // EHAMmainDepartureRunwayCheckBox
            // 
            this.EHAMmainDepartureRunwayCheckBox.AutoSize = true;
            this.EHAMmainDepartureRunwayCheckBox.Checked = true;
            this.EHAMmainDepartureRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EHAMmainDepartureRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EHAMmainDepartureRunwayCheckBox.Name = "EHAMmainDepartureRunwayCheckBox";
            this.EHAMmainDepartureRunwayCheckBox.Size = new System.Drawing.Size(134, 17);
            this.EHAMmainDepartureRunwayCheckBox.TabIndex = 8;
            this.EHAMmainDepartureRunwayCheckBox.Text = "Main departure runway";
            this.EHAMmainDepartureRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EHAMmainDepartureRunwayCheckBox.CheckedChanged += new System.EventHandler(this.mainDepartureRunwayCheckBox_CheckedChanged);
            // 
            // EHAMmainDepartureRunwayComboBox
            // 
            this.EHAMmainDepartureRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EHAMmainDepartureRunwayComboBox.FormattingEnabled = true;
            this.EHAMmainDepartureRunwayComboBox.Items.AddRange(new object[] {
            "04",
            "06",
            "09",
            "18L",
            "18C",
            "22",
            "24",
            "27",
            "36L",
            "36C"});
            this.EHAMmainDepartureRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EHAMmainDepartureRunwayComboBox.Name = "EHAMmainDepartureRunwayComboBox";
            this.EHAMmainDepartureRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EHAMmainDepartureRunwayComboBox.TabIndex = 9;
            // 
            // EHAMsecondaryLandingRunwayCheckBox
            // 
            this.EHAMsecondaryLandingRunwayCheckBox.AutoSize = true;
            this.EHAMsecondaryLandingRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EHAMsecondaryLandingRunwayCheckBox.Name = "EHAMsecondaryLandingRunwayCheckBox";
            this.EHAMsecondaryLandingRunwayCheckBox.Size = new System.Drawing.Size(151, 17);
            this.EHAMsecondaryLandingRunwayCheckBox.TabIndex = 10;
            this.EHAMsecondaryLandingRunwayCheckBox.Text = "Secondary landing runway";
            this.EHAMsecondaryLandingRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EHAMsecondaryLandingRunwayCheckBox.CheckedChanged += new System.EventHandler(this.secondaryLandingRunway_CheckedChanged);
            // 
            // EHAMsecondaryLandingRunwayComboBox
            // 
            this.EHAMsecondaryLandingRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EHAMsecondaryLandingRunwayComboBox.Enabled = false;
            this.EHAMsecondaryLandingRunwayComboBox.FormattingEnabled = true;
            this.EHAMsecondaryLandingRunwayComboBox.Items.AddRange(new object[] {
            "04",
            "06",
            "09",
            "18C",
            "18R",
            "22",
            "24",
            "27",
            "36C",
            "36R"});
            this.EHAMsecondaryLandingRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EHAMsecondaryLandingRunwayComboBox.Name = "EHAMsecondaryLandingRunwayComboBox";
            this.EHAMsecondaryLandingRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EHAMsecondaryLandingRunwayComboBox.TabIndex = 11;
            // 
            // EHAMsecondaryLandingRunwayGroupBox
            // 
            this.EHAMsecondaryLandingRunwayGroupBox.Controls.Add(this.EHAMsecondaryLandingRunwayComboBox);
            this.EHAMsecondaryLandingRunwayGroupBox.Controls.Add(this.EHAMsecondaryLandingRunwayCheckBox);
            this.EHAMsecondaryLandingRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHAMsecondaryLandingRunwayGroupBox.Name = "EHAMsecondaryLandingRunwayGroupBox";
            this.EHAMsecondaryLandingRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHAMsecondaryLandingRunwayGroupBox.TabIndex = 0;
            this.EHAMsecondaryLandingRunwayGroupBox.TabStop = false;
            // 
            // EHAMsecondaryRunwaysGroupBox
            // 
            this.EHAMsecondaryRunwaysGroupBox.Controls.Add(this.EHAMsecondaryLandingRunwayGroupBox);
            this.EHAMsecondaryRunwaysGroupBox.Controls.Add(this.EHAMsecondaryDepartureRunwayGroupBox);
            this.EHAMsecondaryRunwaysGroupBox.Location = new System.Drawing.Point(3, 105);
            this.EHAMsecondaryRunwaysGroupBox.Name = "EHAMsecondaryRunwaysGroupBox";
            this.EHAMsecondaryRunwaysGroupBox.Size = new System.Drawing.Size(353, 96);
            this.EHAMsecondaryRunwaysGroupBox.TabIndex = 0;
            this.EHAMsecondaryRunwaysGroupBox.TabStop = false;
            this.EHAMsecondaryRunwaysGroupBox.Text = "Secondary Runway(s)";
            // 
            // EHAMsecondaryDepartureRunwayGroupBox
            // 
            this.EHAMsecondaryDepartureRunwayGroupBox.Controls.Add(this.EHAMsecondaryDepartureRunwayComboBox);
            this.EHAMsecondaryDepartureRunwayGroupBox.Controls.Add(this.EHAMsecondaryDepartureRunwayCheckBox);
            this.EHAMsecondaryDepartureRunwayGroupBox.Location = new System.Drawing.Point(177, 19);
            this.EHAMsecondaryDepartureRunwayGroupBox.Name = "EHAMsecondaryDepartureRunwayGroupBox";
            this.EHAMsecondaryDepartureRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHAMsecondaryDepartureRunwayGroupBox.TabIndex = 0;
            this.EHAMsecondaryDepartureRunwayGroupBox.TabStop = false;
            // 
            // EHAMsecondaryDepartureRunwayComboBox
            // 
            this.EHAMsecondaryDepartureRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EHAMsecondaryDepartureRunwayComboBox.Enabled = false;
            this.EHAMsecondaryDepartureRunwayComboBox.FormattingEnabled = true;
            this.EHAMsecondaryDepartureRunwayComboBox.Items.AddRange(new object[] {
            "04",
            "06",
            "09",
            "18L",
            "18C",
            "22",
            "24",
            "27",
            "36L",
            "36C"});
            this.EHAMsecondaryDepartureRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EHAMsecondaryDepartureRunwayComboBox.Name = "EHAMsecondaryDepartureRunwayComboBox";
            this.EHAMsecondaryDepartureRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EHAMsecondaryDepartureRunwayComboBox.TabIndex = 13;
            // 
            // EHAMsecondaryDepartureRunwayCheckBox
            // 
            this.EHAMsecondaryDepartureRunwayCheckBox.AutoSize = true;
            this.EHAMsecondaryDepartureRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EHAMsecondaryDepartureRunwayCheckBox.Name = "EHAMsecondaryDepartureRunwayCheckBox";
            this.EHAMsecondaryDepartureRunwayCheckBox.Size = new System.Drawing.Size(162, 17);
            this.EHAMsecondaryDepartureRunwayCheckBox.TabIndex = 12;
            this.EHAMsecondaryDepartureRunwayCheckBox.Text = "Secondary departure runway";
            this.EHAMsecondaryDepartureRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EHAMsecondaryDepartureRunwayCheckBox.CheckedChanged += new System.EventHandler(this.secondaryDepartureRunwayCheckBox_CheckedChanged);
            // 
            // tlLevelGroupBox
            // 
            this.tlLevelGroupBox.Controls.Add(this.tlOutLabel);
            this.tlLevelGroupBox.Controls.Add(this.tlHeaderLabel);
            this.tlLevelGroupBox.Location = new System.Drawing.Point(136, 406);
            this.tlLevelGroupBox.Name = "tlLevelGroupBox";
            this.tlLevelGroupBox.Size = new System.Drawing.Size(116, 112);
            this.tlLevelGroupBox.TabIndex = 0;
            this.tlLevelGroupBox.TabStop = false;
            // 
            // tlOutLabel
            // 
            this.tlOutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlOutLabel.Location = new System.Drawing.Point(19, 62);
            this.tlOutLabel.Name = "tlOutLabel";
            this.tlOutLabel.Size = new System.Drawing.Size(81, 39);
            this.tlOutLabel.TabIndex = 0;
            this.tlOutLabel.Text = "..";
            this.tlOutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlHeaderLabel
            // 
            this.tlHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlHeaderLabel.Location = new System.Drawing.Point(19, 17);
            this.tlHeaderLabel.Name = "tlHeaderLabel";
            this.tlHeaderLabel.Size = new System.Drawing.Size(81, 39);
            this.tlHeaderLabel.TabIndex = 0;
            this.tlHeaderLabel.Text = "TL:";
            this.tlHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // generateATISButton
            // 
            this.generateATISButton.Enabled = false;
            this.generateATISButton.Location = new System.Drawing.Point(258, 411);
            this.generateATISButton.Name = "generateATISButton";
            this.generateATISButton.Size = new System.Drawing.Size(109, 108);
            this.generateATISButton.TabIndex = 21;
            this.generateATISButton.Text = "Generate ATIS";
            this.generateATISButton.UseVisualStyleBackColor = true;
            this.generateATISButton.Click += new System.EventHandler(this.generateATISButton_Click);
            // 
            // metarInputGroupBox
            // 
            this.metarInputGroupBox.Controls.Add(this.fetchMetarLabel);
            this.metarInputGroupBox.Controls.Add(this.getSelectBestRunwayButton);
            this.metarInputGroupBox.Controls.Add(this.icaoTextBox);
            this.metarInputGroupBox.Controls.Add(this.getMetarButton);
            this.metarInputGroupBox.Controls.Add(this.metarTextBox);
            this.metarInputGroupBox.Controls.Add(this.lastLabel);
            this.metarInputGroupBox.Controls.Add(this.processMetarButton);
            this.metarInputGroupBox.Location = new System.Drawing.Point(13, 29);
            this.metarInputGroupBox.Name = "metarInputGroupBox";
            this.metarInputGroupBox.Size = new System.Drawing.Size(535, 133);
            this.metarInputGroupBox.TabIndex = 0;
            this.metarInputGroupBox.TabStop = false;
            this.metarInputGroupBox.Text = "Metar";
            // 
            // fetchMetarLabel
            // 
            this.fetchMetarLabel.AutoSize = true;
            this.fetchMetarLabel.Location = new System.Drawing.Point(131, 22);
            this.fetchMetarLabel.Name = "fetchMetarLabel";
            this.fetchMetarLabel.Size = new System.Drawing.Size(106, 13);
            this.fetchMetarLabel.TabIndex = 9;
            this.fetchMetarLabel.Text = "Fetching METAR in: ";
            this.fetchMetarLabel.Visible = false;
            // 
            // getSelectBestRunwayButton
            // 
            this.getSelectBestRunwayButton.Location = new System.Drawing.Point(415, 18);
            this.getSelectBestRunwayButton.Name = "getSelectBestRunwayButton";
            this.getSelectBestRunwayButton.Size = new System.Drawing.Size(114, 22);
            this.getSelectBestRunwayButton.TabIndex = 8;
            this.getSelectBestRunwayButton.Text = "Get EHAM runway(s)";
            this.getSelectBestRunwayButton.UseVisualStyleBackColor = true;
            this.getSelectBestRunwayButton.Click += new System.EventHandler(this.getSelectBestRunwayButton_Click);
            // 
            // copyOutputCheckBox
            // 
            this.copyOutputCheckBox.AutoSize = true;
            this.copyOutputCheckBox.Checked = true;
            this.copyOutputCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.copyOutputCheckBox.Location = new System.Drawing.Point(6, 19);
            this.copyOutputCheckBox.Name = "copyOutputCheckBox";
            this.copyOutputCheckBox.Size = new System.Drawing.Size(141, 17);
            this.copyOutputCheckBox.TabIndex = 20;
            this.copyOutputCheckBox.Text = "Copy output to clipboard";
            this.copyOutputCheckBox.UseVisualStyleBackColor = true;
            // 
            // outputOptionsGroupBox
            // 
            this.outputOptionsGroupBox.Controls.Add(this.copyOutputCheckBox);
            this.outputOptionsGroupBox.Location = new System.Drawing.Point(373, 406);
            this.outputOptionsGroupBox.Name = "outputOptionsGroupBox";
            this.outputOptionsGroupBox.Size = new System.Drawing.Size(175, 112);
            this.outputOptionsGroupBox.TabIndex = 0;
            this.outputOptionsGroupBox.TabStop = false;
            this.outputOptionsGroupBox.Text = "Output Options";
            // 
            // outputGroupBox
            // 
            this.outputGroupBox.Controls.Add(this.outputTextBox);
            this.outputGroupBox.Location = new System.Drawing.Point(13, 524);
            this.outputGroupBox.Name = "outputGroupBox";
            this.outputGroupBox.Size = new System.Drawing.Size(535, 93);
            this.outputGroupBox.TabIndex = 0;
            this.outputGroupBox.TabStop = false;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(3, 9);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(529, 81);
            this.outputTextBox.TabIndex = 22;
            this.outputTextBox.TextChanged += new System.EventHandler(this.outputTextBox_TextChanged);
            // 
            // ICAOTabControl
            // 
            this.ICAOTabControl.Controls.Add(this.EHAM);
            this.ICAOTabControl.Controls.Add(this.EHBK);
            this.ICAOTabControl.Controls.Add(this.EHEH);
            this.ICAOTabControl.Controls.Add(this.EHGG);
            this.ICAOTabControl.Controls.Add(this.EHRD);
            this.ICAOTabControl.Location = new System.Drawing.Point(13, 168);
            this.ICAOTabControl.Name = "ICAOTabControl";
            this.ICAOTabControl.SelectedIndex = 0;
            this.ICAOTabControl.Size = new System.Drawing.Size(369, 232);
            this.ICAOTabControl.TabIndex = 5;
            this.ICAOTabControl.SelectedIndexChanged += new System.EventHandler(this.ICAOTabControl_SelectedIndexChanged);
            // 
            // EHAM
            // 
            this.EHAM.Controls.Add(this.EHAMmainRunwaysGroupBox);
            this.EHAM.Controls.Add(this.EHAMsecondaryRunwaysGroupBox);
            this.EHAM.Location = new System.Drawing.Point(4, 22);
            this.EHAM.Name = "EHAM";
            this.EHAM.Size = new System.Drawing.Size(361, 206);
            this.EHAM.TabIndex = 5;
            this.EHAM.Text = "EHAM";
            this.EHAM.UseVisualStyleBackColor = true;
            // 
            // EHBK
            // 
            this.EHBK.Controls.Add(this.EHBKmainRunwayGroupBox);
            this.EHBK.Location = new System.Drawing.Point(4, 22);
            this.EHBK.Name = "EHBK";
            this.EHBK.Padding = new System.Windows.Forms.Padding(3);
            this.EHBK.Size = new System.Drawing.Size(361, 206);
            this.EHBK.TabIndex = 1;
            this.EHBK.Text = "EHBK";
            this.EHBK.UseVisualStyleBackColor = true;
            // 
            // EHBKmainRunwayGroupBox
            // 
            this.EHBKmainRunwayGroupBox.Controls.Add(this.EHBKchildMainRunwayGroupBox);
            this.EHBKmainRunwayGroupBox.Location = new System.Drawing.Point(3, 3);
            this.EHBKmainRunwayGroupBox.Name = "EHBKmainRunwayGroupBox";
            this.EHBKmainRunwayGroupBox.Size = new System.Drawing.Size(182, 96);
            this.EHBKmainRunwayGroupBox.TabIndex = 3;
            this.EHBKmainRunwayGroupBox.TabStop = false;
            this.EHBKmainRunwayGroupBox.Text = "Main Runway";
            // 
            // EHBKchildMainRunwayGroupBox
            // 
            this.EHBKchildMainRunwayGroupBox.Controls.Add(this.EHBKmainRunwayCheckBox);
            this.EHBKchildMainRunwayGroupBox.Controls.Add(this.EHBKmainRunwayComboBox);
            this.EHBKchildMainRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHBKchildMainRunwayGroupBox.Name = "EHBKchildMainRunwayGroupBox";
            this.EHBKchildMainRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHBKchildMainRunwayGroupBox.TabIndex = 0;
            this.EHBKchildMainRunwayGroupBox.TabStop = false;
            // 
            // EHBKmainRunwayCheckBox
            // 
            this.EHBKmainRunwayCheckBox.AutoSize = true;
            this.EHBKmainRunwayCheckBox.Checked = true;
            this.EHBKmainRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EHBKmainRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EHBKmainRunwayCheckBox.Name = "EHBKmainRunwayCheckBox";
            this.EHBKmainRunwayCheckBox.Size = new System.Drawing.Size(86, 17);
            this.EHBKmainRunwayCheckBox.TabIndex = 6;
            this.EHBKmainRunwayCheckBox.Text = "Main runway";
            this.EHBKmainRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EHBKmainRunwayCheckBox.CheckedChanged += new System.EventHandler(this.EHBKmainRunwayCheckBox_CheckedChanged);
            // 
            // EHBKmainRunwayComboBox
            // 
            this.EHBKmainRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EHBKmainRunwayComboBox.FormattingEnabled = true;
            this.EHBKmainRunwayComboBox.Items.AddRange(new object[] {
            "03",
            "21"});
            this.EHBKmainRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EHBKmainRunwayComboBox.Name = "EHBKmainRunwayComboBox";
            this.EHBKmainRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EHBKmainRunwayComboBox.TabIndex = 7;
            // 
            // EHEH
            // 
            this.EHEH.Controls.Add(this.EHEHmainRunwayGroupBox);
            this.EHEH.Location = new System.Drawing.Point(4, 22);
            this.EHEH.Name = "EHEH";
            this.EHEH.Size = new System.Drawing.Size(361, 206);
            this.EHEH.TabIndex = 2;
            this.EHEH.Text = "EHEH";
            this.EHEH.UseVisualStyleBackColor = true;
            // 
            // EHEHmainRunwayGroupBox
            // 
            this.EHEHmainRunwayGroupBox.Controls.Add(this.EHEHchildMainRunwayGroupBox);
            this.EHEHmainRunwayGroupBox.Location = new System.Drawing.Point(3, 3);
            this.EHEHmainRunwayGroupBox.Name = "EHEHmainRunwayGroupBox";
            this.EHEHmainRunwayGroupBox.Size = new System.Drawing.Size(182, 96);
            this.EHEHmainRunwayGroupBox.TabIndex = 3;
            this.EHEHmainRunwayGroupBox.TabStop = false;
            this.EHEHmainRunwayGroupBox.Text = "Main Runway";
            // 
            // EHEHchildMainRunwayGroupBox
            // 
            this.EHEHchildMainRunwayGroupBox.Controls.Add(this.EHEHmainRunwayCheckBox);
            this.EHEHchildMainRunwayGroupBox.Controls.Add(this.EHEHmainRunwayComboBox);
            this.EHEHchildMainRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHEHchildMainRunwayGroupBox.Name = "EHEHchildMainRunwayGroupBox";
            this.EHEHchildMainRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHEHchildMainRunwayGroupBox.TabIndex = 0;
            this.EHEHchildMainRunwayGroupBox.TabStop = false;
            // 
            // EHEHmainRunwayCheckBox
            // 
            this.EHEHmainRunwayCheckBox.AutoSize = true;
            this.EHEHmainRunwayCheckBox.Checked = true;
            this.EHEHmainRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EHEHmainRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EHEHmainRunwayCheckBox.Name = "EHEHmainRunwayCheckBox";
            this.EHEHmainRunwayCheckBox.Size = new System.Drawing.Size(86, 17);
            this.EHEHmainRunwayCheckBox.TabIndex = 6;
            this.EHEHmainRunwayCheckBox.Text = "Main runway";
            this.EHEHmainRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EHEHmainRunwayCheckBox.CheckedChanged += new System.EventHandler(this.EHEHmainRunwayCheckBox_CheckedChanged);
            // 
            // EHEHmainRunwayComboBox
            // 
            this.EHEHmainRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EHEHmainRunwayComboBox.FormattingEnabled = true;
            this.EHEHmainRunwayComboBox.Items.AddRange(new object[] {
            "03",
            "21"});
            this.EHEHmainRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EHEHmainRunwayComboBox.Name = "EHEHmainRunwayComboBox";
            this.EHEHmainRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EHEHmainRunwayComboBox.TabIndex = 7;
            // 
            // EHGG
            // 
            this.EHGG.Controls.Add(this.EHGGmainRunwayGroupBox);
            this.EHGG.Location = new System.Drawing.Point(4, 22);
            this.EHGG.Name = "EHGG";
            this.EHGG.Size = new System.Drawing.Size(361, 206);
            this.EHGG.TabIndex = 3;
            this.EHGG.Text = "EHGG";
            this.EHGG.UseVisualStyleBackColor = true;
            // 
            // EHGGmainRunwayGroupBox
            // 
            this.EHGGmainRunwayGroupBox.Controls.Add(this.EHGGchildMainRunwayGroupBox);
            this.EHGGmainRunwayGroupBox.Location = new System.Drawing.Point(3, 3);
            this.EHGGmainRunwayGroupBox.Name = "EHGGmainRunwayGroupBox";
            this.EHGGmainRunwayGroupBox.Size = new System.Drawing.Size(182, 96);
            this.EHGGmainRunwayGroupBox.TabIndex = 2;
            this.EHGGmainRunwayGroupBox.TabStop = false;
            this.EHGGmainRunwayGroupBox.Text = "Main Runway";
            // 
            // EHGGchildMainRunwayGroupBox
            // 
            this.EHGGchildMainRunwayGroupBox.Controls.Add(this.EHGGmainRunwayCheckBox);
            this.EHGGchildMainRunwayGroupBox.Controls.Add(this.EHGGmainRunwayComboBox);
            this.EHGGchildMainRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHGGchildMainRunwayGroupBox.Name = "EHGGchildMainRunwayGroupBox";
            this.EHGGchildMainRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHGGchildMainRunwayGroupBox.TabIndex = 0;
            this.EHGGchildMainRunwayGroupBox.TabStop = false;
            // 
            // EHGGmainRunwayCheckBox
            // 
            this.EHGGmainRunwayCheckBox.AutoSize = true;
            this.EHGGmainRunwayCheckBox.Checked = true;
            this.EHGGmainRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EHGGmainRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EHGGmainRunwayCheckBox.Name = "EHGGmainRunwayCheckBox";
            this.EHGGmainRunwayCheckBox.Size = new System.Drawing.Size(86, 17);
            this.EHGGmainRunwayCheckBox.TabIndex = 6;
            this.EHGGmainRunwayCheckBox.Text = "Main runway";
            this.EHGGmainRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EHGGmainRunwayCheckBox.CheckedChanged += new System.EventHandler(this.EHGGmainRunwayCheckBox_CheckedChanged);
            // 
            // EHGGmainRunwayComboBox
            // 
            this.EHGGmainRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EHGGmainRunwayComboBox.FormattingEnabled = true;
            this.EHGGmainRunwayComboBox.Items.AddRange(new object[] {
            "01",
            "05",
            "19",
            "23"});
            this.EHGGmainRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EHGGmainRunwayComboBox.Name = "EHGGmainRunwayComboBox";
            this.EHGGmainRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EHGGmainRunwayComboBox.TabIndex = 7;
            // 
            // EHRD
            // 
            this.EHRD.Controls.Add(this.EHRDmainRubwayGroupBox);
            this.EHRD.Location = new System.Drawing.Point(4, 22);
            this.EHRD.Name = "EHRD";
            this.EHRD.Size = new System.Drawing.Size(361, 206);
            this.EHRD.TabIndex = 4;
            this.EHRD.Text = "EHRD";
            this.EHRD.UseVisualStyleBackColor = true;
            // 
            // EHRDmainRubwayGroupBox
            // 
            this.EHRDmainRubwayGroupBox.Controls.Add(this.EHRDchildMainRunwyaGroupBox);
            this.EHRDmainRubwayGroupBox.Location = new System.Drawing.Point(3, 3);
            this.EHRDmainRubwayGroupBox.Name = "EHRDmainRubwayGroupBox";
            this.EHRDmainRubwayGroupBox.Size = new System.Drawing.Size(182, 96);
            this.EHRDmainRubwayGroupBox.TabIndex = 1;
            this.EHRDmainRubwayGroupBox.TabStop = false;
            this.EHRDmainRubwayGroupBox.Text = "Main Runway";
            // 
            // EHRDchildMainRunwyaGroupBox
            // 
            this.EHRDchildMainRunwyaGroupBox.Controls.Add(this.EHRDmainRunwayCheckBox);
            this.EHRDchildMainRunwyaGroupBox.Controls.Add(this.EHRDmainRunwayComboBox);
            this.EHRDchildMainRunwyaGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHRDchildMainRunwyaGroupBox.Name = "EHRDchildMainRunwyaGroupBox";
            this.EHRDchildMainRunwyaGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHRDchildMainRunwyaGroupBox.TabIndex = 0;
            this.EHRDchildMainRunwyaGroupBox.TabStop = false;
            // 
            // EHRDmainRunwayCheckBox
            // 
            this.EHRDmainRunwayCheckBox.AutoSize = true;
            this.EHRDmainRunwayCheckBox.Checked = true;
            this.EHRDmainRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EHRDmainRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EHRDmainRunwayCheckBox.Name = "EHRDmainRunwayCheckBox";
            this.EHRDmainRunwayCheckBox.Size = new System.Drawing.Size(86, 17);
            this.EHRDmainRunwayCheckBox.TabIndex = 6;
            this.EHRDmainRunwayCheckBox.Text = "Main runway";
            this.EHRDmainRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EHRDmainRunwayCheckBox.CheckedChanged += new System.EventHandler(this.EHRDmainRunwayCheckBox_CheckedChanged);
            // 
            // EHRDmainRunwayComboBox
            // 
            this.EHRDmainRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EHRDmainRunwayComboBox.FormattingEnabled = true;
            this.EHRDmainRunwayComboBox.Items.AddRange(new object[] {
            "06",
            "24"});
            this.EHRDmainRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EHRDmainRunwayComboBox.Name = "EHRDmainRunwayComboBox";
            this.EHRDmainRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EHRDmainRunwayComboBox.TabIndex = 7;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.runwayInfoToolStripMenuItem,
            this.soundToolStripMenuItem,
            this.tAFToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(561, 24);
            this.menuStrip.TabIndex = 19;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amsterdamInfoToolStripMenuItem,
            this.dutchVACCToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // amsterdamInfoToolStripMenuItem
            // 
            this.amsterdamInfoToolStripMenuItem.Name = "amsterdamInfoToolStripMenuItem";
            this.amsterdamInfoToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.amsterdamInfoToolStripMenuItem.Text = "Amsterdam Info";
            this.amsterdamInfoToolStripMenuItem.Click += new System.EventHandler(this.amsterdamInfoToolStripMenuItem_Click);
            // 
            // dutchVACCToolStripMenuItem
            // 
            this.dutchVACCToolStripMenuItem.AutoSize = false;
            this.dutchVACCToolStripMenuItem.Name = "dutchVACCToolStripMenuItem";
            this.dutchVACCToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.dutchVACCToolStripMenuItem.Text = "Dutch VACC";
            this.dutchVACCToolStripMenuItem.Click += new System.EventHandler(this.dutchVACCToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fetchMETAREvery30MinutesToolStripMenuItem,
            this.autoProcessMETARToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // fetchMETAREvery30MinutesToolStripMenuItem
            // 
            this.fetchMETAREvery30MinutesToolStripMenuItem.CheckOnClick = true;
            this.fetchMETAREvery30MinutesToolStripMenuItem.Name = "fetchMETAREvery30MinutesToolStripMenuItem";
            this.fetchMETAREvery30MinutesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.fetchMETAREvery30MinutesToolStripMenuItem.Text = "Auto fetch METAR ";
            this.fetchMETAREvery30MinutesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.fetchMETAREvery30MinutesToolStripMenuItem_CheckedChanged);
            // 
            // autoProcessMETARToolStripMenuItem
            // 
            this.autoProcessMETARToolStripMenuItem.CheckOnClick = true;
            this.autoProcessMETARToolStripMenuItem.Name = "autoProcessMETARToolStripMenuItem";
            this.autoProcessMETARToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.autoProcessMETARToolStripMenuItem.Text = "Auto process METAR";
            this.autoProcessMETARToolStripMenuItem.CheckedChanged += new System.EventHandler(this.autoProcessMETARToolStripMenuItem_CheckedChanged);
            // 
            // runwayInfoToolStripMenuItem
            // 
            this.runwayInfoToolStripMenuItem.Enabled = false;
            this.runwayInfoToolStripMenuItem.Name = "runwayInfoToolStripMenuItem";
            this.runwayInfoToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.runwayInfoToolStripMenuItem.Text = "Runway Info";
            this.runwayInfoToolStripMenuItem.Click += new System.EventHandler(this.runwayInfoToolStripMenuItem_Click);
            // 
            // soundToolStripMenuItem
            // 
            this.soundToolStripMenuItem.Name = "soundToolStripMenuItem";
            this.soundToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.soundToolStripMenuItem.Text = "Sound";
            this.soundToolStripMenuItem.Click += new System.EventHandler(this.soundToolStripMenuItem_Click);
            // 
            // tAFToolStripMenuItem
            // 
            this.tAFToolStripMenuItem.Name = "tAFToolStripMenuItem";
            this.tAFToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.tAFToolStripMenuItem.Text = "TAF";
            this.tAFToolStripMenuItem.Click += new System.EventHandler(this.tAFToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // additionalOptionsGroupBox
            // 
            this.additionalOptionsGroupBox.Controls.Add(this.userDefinedWaveCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.addWindRecordCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.markTempCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.appArrOnlyCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.arrOnlyCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.appOnlyCheckBox);
            this.additionalOptionsGroupBox.Location = new System.Drawing.Point(399, 168);
            this.additionalOptionsGroupBox.Name = "additionalOptionsGroupBox";
            this.additionalOptionsGroupBox.Size = new System.Drawing.Size(149, 232);
            this.additionalOptionsGroupBox.TabIndex = 22;
            this.additionalOptionsGroupBox.TabStop = false;
            this.additionalOptionsGroupBox.Text = "Additional Options";
            // 
            // userDefinedWaveCheckBox
            // 
            this.userDefinedWaveCheckBox.AutoSize = true;
            this.userDefinedWaveCheckBox.Location = new System.Drawing.Point(6, 159);
            this.userDefinedWaveCheckBox.Name = "userDefinedWaveCheckBox";
            this.userDefinedWaveCheckBox.Size = new System.Drawing.Size(135, 17);
            this.userDefinedWaveCheckBox.TabIndex = 19;
            this.userDefinedWaveCheckBox.Text = "User defined extra wav";
            this.userDefinedWaveCheckBox.UseVisualStyleBackColor = true;
            // 
            // addWindRecordCheckBox
            // 
            this.addWindRecordCheckBox.AutoSize = true;
            this.addWindRecordCheckBox.Location = new System.Drawing.Point(6, 131);
            this.addWindRecordCheckBox.Name = "addWindRecordCheckBox";
            this.addWindRecordCheckBox.Size = new System.Drawing.Size(84, 17);
            this.addWindRecordCheckBox.TabIndex = 18;
            this.addWindRecordCheckBox.Text = "Wind record\r\n";
            this.addWindRecordCheckBox.UseVisualStyleBackColor = true;
            // 
            // markTempCheckBox
            // 
            this.markTempCheckBox.AutoSize = true;
            this.markTempCheckBox.Location = new System.Drawing.Point(6, 96);
            this.markTempCheckBox.Name = "markTempCheckBox";
            this.markTempCheckBox.Size = new System.Drawing.Size(122, 30);
            this.markTempCheckBox.TabIndex = 17;
            this.markTempCheckBox.Text = "Inverted surface-\r\ntemperature warning";
            this.markTempCheckBox.UseVisualStyleBackColor = true;
            // 
            // appArrOnlyCheckBox
            // 
            this.appArrOnlyCheckBox.AutoSize = true;
            this.appArrOnlyCheckBox.Location = new System.Drawing.Point(6, 75);
            this.appArrOnlyCheckBox.Name = "appArrOnlyCheckBox";
            this.appArrOnlyCheckBox.Size = new System.Drawing.Size(142, 17);
            this.appArrOnlyCheckBox.TabIndex = 16;
            this.appArrOnlyCheckBox.Text = "APP + ARR callsign only";
            this.appArrOnlyCheckBox.UseVisualStyleBackColor = true;
            this.appArrOnlyCheckBox.CheckedChanged += new System.EventHandler(this.appArrOnlyCheckBox_CheckedChanged);
            // 
            // arrOnlyCheckBox
            // 
            this.arrOnlyCheckBox.AutoSize = true;
            this.arrOnlyCheckBox.Location = new System.Drawing.Point(6, 47);
            this.arrOnlyCheckBox.Name = "arrOnlyCheckBox";
            this.arrOnlyCheckBox.Size = new System.Drawing.Size(109, 17);
            this.arrOnlyCheckBox.TabIndex = 15;
            this.arrOnlyCheckBox.Text = "ARR callsign only";
            this.arrOnlyCheckBox.UseVisualStyleBackColor = true;
            this.arrOnlyCheckBox.CheckedChanged += new System.EventHandler(this.arrOnlyCheckBox_CheckedChanged);
            // 
            // appOnlyCheckBox
            // 
            this.appOnlyCheckBox.AutoSize = true;
            this.appOnlyCheckBox.Location = new System.Drawing.Point(6, 19);
            this.appOnlyCheckBox.Name = "appOnlyCheckBox";
            this.appOnlyCheckBox.Size = new System.Drawing.Size(107, 17);
            this.appOnlyCheckBox.TabIndex = 14;
            this.appOnlyCheckBox.Text = "APP callsign only";
            this.appOnlyCheckBox.UseVisualStyleBackColor = true;
            this.appOnlyCheckBox.CheckedChanged += new System.EventHandler(this.appOnlyCheckBox_CheckedChanged);
            // 
            // runwayInfoButton
            // 
            this.runwayInfoButton.Enabled = false;
            this.runwayInfoButton.Location = new System.Drawing.Point(383, 187);
            this.runwayInfoButton.Name = "runwayInfoButton";
            this.runwayInfoButton.Size = new System.Drawing.Size(15, 213);
            this.runwayInfoButton.TabIndex = 23;
            this.runwayInfoButton.Text = ">";
            this.runwayInfoButton.UseVisualStyleBackColor = true;
            this.runwayInfoButton.Click += new System.EventHandler(this.runwayInfoButton_Click);
            // 
            // soundButton
            // 
            this.soundButton.Location = new System.Drawing.Point(195, 623);
            this.soundButton.Name = "soundButton";
            this.soundButton.Size = new System.Drawing.Size(171, 19);
            this.soundButton.TabIndex = 24;
            this.soundButton.Text = "▼";
            this.soundButton.UseVisualStyleBackColor = true;
            this.soundButton.Click += new System.EventHandler(this.soundButton_Click);
            // 
            // versionBackgroundWorker
            // 
            this.versionBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.versionBackgroundWorker_DoWork);
            this.versionBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.versionBackgroundWorker_RunWorkerCompleted);
            // 
            // realRunwayBackgroundWorker
            // 
            this.realRunwayBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.realRunwayBackgroundWorker_DoWork);
            this.realRunwayBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.realRunwayBackgroundWorker_RunWorkerCompleted);
            // 
            // metarFetchTimer
            // 
            this.metarFetchTimer.Interval = 1000;
            this.metarFetchTimer.Tick += new System.EventHandler(this.metarFetchTimer_Tick);
            // 
            // DutchVACCATISGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 654);
            this.Controls.Add(this.soundButton);
            this.Controls.Add(this.runwayInfoButton);
            this.Controls.Add(this.additionalOptionsGroupBox);
            this.Controls.Add(this.ICAOTabControl);
            this.Controls.Add(this.outputGroupBox);
            this.Controls.Add(this.outputOptionsGroupBox);
            this.Controls.Add(this.metarInputGroupBox);
            this.Controls.Add(this.generateATISButton);
            this.Controls.Add(this.tlLevelGroupBox);
            this.Controls.Add(this.atisIndexGroupBox);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "DutchVACCATISGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dutch VACC ATIS Generator";
            this.Resize += new System.EventHandler(this.DutchVACCATISGenerator_Resize);
            this.atisIndexGroupBox.ResumeLayout(false);
            this.atisIndexGroupBox.PerformLayout();
            this.EHAMmainRunwaysGroupBox.ResumeLayout(false);
            this.EHAMmainLandingRunwayGroupBox.ResumeLayout(false);
            this.EHAMmainLandingRunwayGroupBox.PerformLayout();
            this.EHAMmainDepartureRunwayGroupBox.ResumeLayout(false);
            this.EHAMmainDepartureRunwayGroupBox.PerformLayout();
            this.EHAMsecondaryLandingRunwayGroupBox.ResumeLayout(false);
            this.EHAMsecondaryLandingRunwayGroupBox.PerformLayout();
            this.EHAMsecondaryRunwaysGroupBox.ResumeLayout(false);
            this.EHAMsecondaryDepartureRunwayGroupBox.ResumeLayout(false);
            this.EHAMsecondaryDepartureRunwayGroupBox.PerformLayout();
            this.tlLevelGroupBox.ResumeLayout(false);
            this.metarInputGroupBox.ResumeLayout(false);
            this.metarInputGroupBox.PerformLayout();
            this.outputOptionsGroupBox.ResumeLayout(false);
            this.outputOptionsGroupBox.PerformLayout();
            this.outputGroupBox.ResumeLayout(false);
            this.outputGroupBox.PerformLayout();
            this.ICAOTabControl.ResumeLayout(false);
            this.EHAM.ResumeLayout(false);
            this.EHBK.ResumeLayout(false);
            this.EHBKmainRunwayGroupBox.ResumeLayout(false);
            this.EHBKchildMainRunwayGroupBox.ResumeLayout(false);
            this.EHBKchildMainRunwayGroupBox.PerformLayout();
            this.EHEH.ResumeLayout(false);
            this.EHEHmainRunwayGroupBox.ResumeLayout(false);
            this.EHEHchildMainRunwayGroupBox.ResumeLayout(false);
            this.EHEHchildMainRunwayGroupBox.PerformLayout();
            this.EHGG.ResumeLayout(false);
            this.EHGGmainRunwayGroupBox.ResumeLayout(false);
            this.EHGGchildMainRunwayGroupBox.ResumeLayout(false);
            this.EHGGchildMainRunwayGroupBox.PerformLayout();
            this.EHRD.ResumeLayout(false);
            this.EHRDmainRubwayGroupBox.ResumeLayout(false);
            this.EHRDchildMainRunwyaGroupBox.ResumeLayout(false);
            this.EHRDchildMainRunwyaGroupBox.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.additionalOptionsGroupBox.ResumeLayout(false);
            this.additionalOptionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getMetarButton;
        private System.Windows.Forms.TextBox icaoTextBox;
        private System.ComponentModel.BackgroundWorker metarBackgroundWorker;
        private System.Windows.Forms.TextBox metarTextBox;
        private System.Windows.Forms.Label lastLabel;
        private System.Windows.Forms.Button processMetarButton;
        private System.Windows.Forms.GroupBox atisIndexGroupBox;
        private System.Windows.Forms.Button previousATISLetterButton;
        private System.Windows.Forms.Button nextATISLetterButton;
        private System.Windows.Forms.Label atisLetterLabel;
        private System.Windows.Forms.GroupBox EHAMmainRunwaysGroupBox;
        private System.Windows.Forms.CheckBox EHAMsecondaryLandingRunwayCheckBox;
        private System.Windows.Forms.CheckBox EHAMmainLandingRunwayCheckBox;
        private System.Windows.Forms.ComboBox EHAMsecondaryLandingRunwayComboBox;
        private System.Windows.Forms.GroupBox EHAMsecondaryLandingRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHAMmainLandingRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHAMsecondaryRunwaysGroupBox;
        private System.Windows.Forms.GroupBox EHAMsecondaryDepartureRunwayGroupBox;
        private System.Windows.Forms.ComboBox EHAMsecondaryDepartureRunwayComboBox;
        private System.Windows.Forms.CheckBox EHAMsecondaryDepartureRunwayCheckBox;
        private System.Windows.Forms.GroupBox EHAMmainDepartureRunwayGroupBox;
        private System.Windows.Forms.CheckBox EHAMmainDepartureRunwayCheckBox;
        private System.Windows.Forms.GroupBox tlLevelGroupBox;
        private System.Windows.Forms.Label tlHeaderLabel;
        private System.Windows.Forms.Button generateATISButton;
        private System.Windows.Forms.Label tlOutLabel;
        private System.Windows.Forms.GroupBox metarInputGroupBox;
        private System.Windows.Forms.CheckBox copyOutputCheckBox;
        private System.Windows.Forms.GroupBox outputOptionsGroupBox;
        private System.Windows.Forms.GroupBox outputGroupBox;
        private System.Windows.Forms.TabPage EHBK;
        private System.Windows.Forms.TabPage EHEH;
        private System.Windows.Forms.TabPage EHGG;
        private System.Windows.Forms.TabPage EHRD;
        private System.Windows.Forms.TabPage EHAM;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox EHRDmainRubwayGroupBox;
        private System.Windows.Forms.GroupBox EHRDchildMainRunwyaGroupBox;
        private System.Windows.Forms.CheckBox EHRDmainRunwayCheckBox;
        private System.Windows.Forms.GroupBox EHGGmainRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHGGchildMainRunwayGroupBox;
        private System.Windows.Forms.CheckBox EHGGmainRunwayCheckBox;
        private System.Windows.Forms.GroupBox additionalOptionsGroupBox;
        private System.Windows.Forms.CheckBox markTempCheckBox;
        private System.Windows.Forms.CheckBox appArrOnlyCheckBox;
        private System.Windows.Forms.CheckBox arrOnlyCheckBox;
        private System.Windows.Forms.CheckBox appOnlyCheckBox;
        private System.Windows.Forms.GroupBox EHBKmainRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHBKchildMainRunwayGroupBox;
        private System.Windows.Forms.CheckBox EHBKmainRunwayCheckBox;
        private System.Windows.Forms.GroupBox EHEHmainRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHEHchildMainRunwayGroupBox;
        private System.Windows.Forms.CheckBox EHEHmainRunwayCheckBox;
        public System.Windows.Forms.TabControl ICAOTabControl;
        public System.Windows.Forms.Button runwayInfoButton;
        public System.Windows.Forms.TextBox outputTextBox;
        public System.Windows.Forms.Button soundButton;
        public System.Windows.Forms.ComboBox EHAMmainLandingRunwayComboBox;
        public System.Windows.Forms.ComboBox EHAMmainDepartureRunwayComboBox;
        public System.Windows.Forms.ComboBox EHRDmainRunwayComboBox;
        public System.Windows.Forms.ComboBox EHGGmainRunwayComboBox;
        public System.Windows.Forms.ComboBox EHBKmainRunwayComboBox;
        public System.Windows.Forms.ComboBox EHEHmainRunwayComboBox;
        private System.ComponentModel.BackgroundWorker versionBackgroundWorker;
        private System.ComponentModel.BackgroundWorker realRunwayBackgroundWorker;
        private System.Windows.Forms.CheckBox addWindRecordCheckBox;
        private System.Windows.Forms.ToolStripMenuItem amsterdamInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dutchVACCToolStripMenuItem;
        private System.Windows.Forms.Button getSelectBestRunwayButton;
        public System.Windows.Forms.ToolStripMenuItem tAFToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem runwayInfoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem soundToolStripMenuItem;
        private System.Windows.Forms.CheckBox userDefinedWaveCheckBox;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoProcessMETARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fetchMETAREvery30MinutesToolStripMenuItem;
        private System.Windows.Forms.Label fetchMetarLabel;
        private System.Windows.Forms.Timer metarFetchTimer;
    }
}

