namespace DutchVACCATISGenerator.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.getMetarButton = new System.Windows.Forms.Button();
            this.icaoTextBox = new System.Windows.Forms.TextBox();
            this.METARTextBox = new System.Windows.Forms.TextBox();
            this.lastLabel = new System.Windows.Forms.Label();
            this.processMETARButton = new System.Windows.Forms.Button();
            this.atisIndexGroupBox = new System.Windows.Forms.GroupBox();
            this.nextATISLetterButton = new System.Windows.Forms.Button();
            this.previousATISLetterButton = new System.Windows.Forms.Button();
            this.atisLetterLabel = new System.Windows.Forms.Label();
            this.EHAMmainRunwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMmainLandingRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.SchipholMainLandingRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.SchipholMainLandingRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHAMmainDepartureRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.SchipholMainDepartureRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.SchipholMainDepartureRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.SchipholSecondaryLandingRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.SchipholSecondaryLandingRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHAMsecondaryLandingRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMsecondaryRunwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.EHAMsecondaryDepartureRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.SchipholSecondaryDepartureRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.SchipholSecondaryDepartureRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.tlLevelGroupBox = new System.Windows.Forms.GroupBox();
            this.tlOutLabel = new System.Windows.Forms.Label();
            this.tlHeaderLabel = new System.Windows.Forms.Label();
            this.generateATISButton = new System.Windows.Forms.Button();
            this.metarInputGroupBox = new System.Windows.Forms.GroupBox();
            this.fetchMETARLabel = new System.Windows.Forms.Label();
            this.selectBestRunwayButton = new System.Windows.Forms.Button();
            this.copyOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.outputOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.outputGroupBox = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.ICAOTabControl = new System.Windows.Forms.TabControl();
            this.EHAM = new System.Windows.Forms.TabPage();
            this.EHBK = new System.Windows.Forms.TabPage();
            this.EHBKmainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHBKchildMainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.BeekRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.BeekRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHEH = new System.Windows.Forms.TabPage();
            this.EHEHmainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHEHchildMainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EindhovenRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EindhovenRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHGG = new System.Windows.Forms.TabPage();
            this.EHGGmainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHGGchildMainRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EeldeRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.EeldeRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.EHRD = new System.Windows.Forms.TabPage();
            this.EHRDmainRubwayGroupBox = new System.Windows.Forms.GroupBox();
            this.EHRDchildMainRunwyaGroupBox = new System.Windows.Forms.GroupBox();
            this.RotterdamRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.RotterdamRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amsterdamInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dutchVACCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seperatorDutchVACCToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoFetchMETARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoLoadEHAMRunwayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoProcessMETARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seperatorAutoProcessToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.ehamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ehrdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seperatorEHRDToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.playSoundWhenMETARIsFetchedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomLetterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runwayInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminalAerodromeForecastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seperatorAboutToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.atcOperationalInformationManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dutchVACCATISGeneratorV2ManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.additionalOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.userDefinedExtraCheckBox = new System.Windows.Forms.CheckBox();
            this.addWindRecordCheckBox = new System.Windows.Forms.CheckBox();
            this.markTempCheckBox = new System.Windows.Forms.CheckBox();
            this.approachAndArrivalCheckBox = new System.Windows.Forms.CheckBox();
            this.arrivalCheckBox = new System.Windows.Forms.CheckBox();
            this.approachCheckBox = new System.Windows.Forms.CheckBox();
            this.runwayInfoButton = new System.Windows.Forms.Button();
            this.soundButton = new System.Windows.Forms.Button();
            this.fetchMETARTimer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.getMetarButton.Click += new System.EventHandler(this.GetMetarButton_Click);
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
            // METARTextBox
            // 
            this.METARTextBox.Location = new System.Drawing.Point(6, 45);
            this.METARTextBox.Multiline = true;
            this.METARTextBox.Name = "METARTextBox";
            this.METARTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.METARTextBox.Size = new System.Drawing.Size(436, 32);
            this.METARTextBox.TabIndex = 3;
            this.METARTextBox.TextChanged += new System.EventHandler(this.METARTextBox_TextChanged);
            // 
            // lastLabel
            // 
            this.lastLabel.AutoSize = true;
            this.lastLabel.Location = new System.Drawing.Point(6, 80);
            this.lastLabel.Name = "lastLabel";
            this.lastLabel.Size = new System.Drawing.Size(0, 13);
            this.lastLabel.TabIndex = 0;
            // 
            // processMETARButton
            // 
            this.processMETARButton.Enabled = false;
            this.processMETARButton.Location = new System.Drawing.Point(445, 44);
            this.processMETARButton.Name = "processMETARButton";
            this.processMETARButton.Size = new System.Drawing.Size(84, 83);
            this.processMETARButton.TabIndex = 4;
            this.processMETARButton.Text = "Process METAR";
            this.processMETARButton.UseVisualStyleBackColor = true;
            this.processMETARButton.Click += new System.EventHandler(this.ProcessMETARButton_Click);
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
            this.nextATISLetterButton.Click += new System.EventHandler(this.NextATISLetterButton_Click);
            // 
            // previousATISLetterButton
            // 
            this.previousATISLetterButton.Location = new System.Drawing.Point(7, 63);
            this.previousATISLetterButton.Name = "previousATISLetterButton";
            this.previousATISLetterButton.Size = new System.Drawing.Size(40, 40);
            this.previousATISLetterButton.TabIndex = 18;
            this.previousATISLetterButton.Text = "<";
            this.previousATISLetterButton.UseVisualStyleBackColor = true;
            this.previousATISLetterButton.Click += new System.EventHandler(this.PreviousATISLetterButton_Click);
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
            this.EHAMmainLandingRunwayGroupBox.Controls.Add(this.SchipholMainLandingRunwayCheckBox);
            this.EHAMmainLandingRunwayGroupBox.Controls.Add(this.SchipholMainLandingRunwayComboBox);
            this.EHAMmainLandingRunwayGroupBox.Location = new System.Drawing.Point(177, 19);
            this.EHAMmainLandingRunwayGroupBox.Name = "EHAMmainLandingRunwayGroupBox";
            this.EHAMmainLandingRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHAMmainLandingRunwayGroupBox.TabIndex = 0;
            this.EHAMmainLandingRunwayGroupBox.TabStop = false;
            // 
            // SchipholMainLandingRunwayCheckBox
            // 
            this.SchipholMainLandingRunwayCheckBox.AutoSize = true;
            this.SchipholMainLandingRunwayCheckBox.Checked = true;
            this.SchipholMainLandingRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SchipholMainLandingRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.SchipholMainLandingRunwayCheckBox.Name = "SchipholMainLandingRunwayCheckBox";
            this.SchipholMainLandingRunwayCheckBox.Size = new System.Drawing.Size(123, 17);
            this.SchipholMainLandingRunwayCheckBox.TabIndex = 6;
            this.SchipholMainLandingRunwayCheckBox.Text = "Main landing runway";
            this.SchipholMainLandingRunwayCheckBox.UseVisualStyleBackColor = true;
            this.SchipholMainLandingRunwayCheckBox.CheckedChanged += new System.EventHandler(this.SchipholMainLandingRunwayCheckBox_CheckedChanged);
            // 
            // SchipholMainLandingRunwayComboBox
            // 
            this.SchipholMainLandingRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SchipholMainLandingRunwayComboBox.FormattingEnabled = true;
            this.SchipholMainLandingRunwayComboBox.Items.AddRange(new object[] {
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
            this.SchipholMainLandingRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.SchipholMainLandingRunwayComboBox.Name = "SchipholMainLandingRunwayComboBox";
            this.SchipholMainLandingRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.SchipholMainLandingRunwayComboBox.TabIndex = 7;
            // 
            // EHAMmainDepartureRunwayGroupBox
            // 
            this.EHAMmainDepartureRunwayGroupBox.Controls.Add(this.SchipholMainDepartureRunwayCheckBox);
            this.EHAMmainDepartureRunwayGroupBox.Controls.Add(this.SchipholMainDepartureRunwayComboBox);
            this.EHAMmainDepartureRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHAMmainDepartureRunwayGroupBox.Name = "EHAMmainDepartureRunwayGroupBox";
            this.EHAMmainDepartureRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHAMmainDepartureRunwayGroupBox.TabIndex = 0;
            this.EHAMmainDepartureRunwayGroupBox.TabStop = false;
            // 
            // SchipholMainDepartureRunwayCheckBox
            // 
            this.SchipholMainDepartureRunwayCheckBox.AutoSize = true;
            this.SchipholMainDepartureRunwayCheckBox.Checked = true;
            this.SchipholMainDepartureRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SchipholMainDepartureRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.SchipholMainDepartureRunwayCheckBox.Name = "SchipholMainDepartureRunwayCheckBox";
            this.SchipholMainDepartureRunwayCheckBox.Size = new System.Drawing.Size(134, 17);
            this.SchipholMainDepartureRunwayCheckBox.TabIndex = 8;
            this.SchipholMainDepartureRunwayCheckBox.Text = "Main departure runway";
            this.SchipholMainDepartureRunwayCheckBox.UseVisualStyleBackColor = true;
            this.SchipholMainDepartureRunwayCheckBox.CheckedChanged += new System.EventHandler(this.SchipholMainDepartureRunwayCheckBox_CheckedChanged);
            // 
            // SchipholMainDepartureRunwayComboBox
            // 
            this.SchipholMainDepartureRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SchipholMainDepartureRunwayComboBox.FormattingEnabled = true;
            this.SchipholMainDepartureRunwayComboBox.Items.AddRange(new object[] {
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
            this.SchipholMainDepartureRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.SchipholMainDepartureRunwayComboBox.Name = "SchipholMainDepartureRunwayComboBox";
            this.SchipholMainDepartureRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.SchipholMainDepartureRunwayComboBox.TabIndex = 9;
            // 
            // SchipholSecondaryLandingRunwayCheckBox
            // 
            this.SchipholSecondaryLandingRunwayCheckBox.AutoSize = true;
            this.SchipholSecondaryLandingRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.SchipholSecondaryLandingRunwayCheckBox.Name = "SchipholSecondaryLandingRunwayCheckBox";
            this.SchipholSecondaryLandingRunwayCheckBox.Size = new System.Drawing.Size(151, 17);
            this.SchipholSecondaryLandingRunwayCheckBox.TabIndex = 10;
            this.SchipholSecondaryLandingRunwayCheckBox.Text = "Secondary landing runway";
            this.SchipholSecondaryLandingRunwayCheckBox.UseVisualStyleBackColor = true;
            this.SchipholSecondaryLandingRunwayCheckBox.CheckedChanged += new System.EventHandler(this.SchipholSecondaryLandingRunway_CheckedChanged);
            // 
            // SchipholSecondaryLandingRunwayComboBox
            // 
            this.SchipholSecondaryLandingRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SchipholSecondaryLandingRunwayComboBox.Enabled = false;
            this.SchipholSecondaryLandingRunwayComboBox.FormattingEnabled = true;
            this.SchipholSecondaryLandingRunwayComboBox.Items.AddRange(new object[] {
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
            this.SchipholSecondaryLandingRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.SchipholSecondaryLandingRunwayComboBox.Name = "SchipholSecondaryLandingRunwayComboBox";
            this.SchipholSecondaryLandingRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.SchipholSecondaryLandingRunwayComboBox.TabIndex = 11;
            // 
            // EHAMsecondaryLandingRunwayGroupBox
            // 
            this.EHAMsecondaryLandingRunwayGroupBox.Controls.Add(this.SchipholSecondaryLandingRunwayComboBox);
            this.EHAMsecondaryLandingRunwayGroupBox.Controls.Add(this.SchipholSecondaryLandingRunwayCheckBox);
            this.EHAMsecondaryLandingRunwayGroupBox.Location = new System.Drawing.Point(177, 19);
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
            this.EHAMsecondaryDepartureRunwayGroupBox.Controls.Add(this.SchipholSecondaryDepartureRunwayComboBox);
            this.EHAMsecondaryDepartureRunwayGroupBox.Controls.Add(this.SchipholSecondaryDepartureRunwayCheckBox);
            this.EHAMsecondaryDepartureRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHAMsecondaryDepartureRunwayGroupBox.Name = "EHAMsecondaryDepartureRunwayGroupBox";
            this.EHAMsecondaryDepartureRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHAMsecondaryDepartureRunwayGroupBox.TabIndex = 0;
            this.EHAMsecondaryDepartureRunwayGroupBox.TabStop = false;
            // 
            // SchipholSecondaryDepartureRunwayComboBox
            // 
            this.SchipholSecondaryDepartureRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SchipholSecondaryDepartureRunwayComboBox.Enabled = false;
            this.SchipholSecondaryDepartureRunwayComboBox.FormattingEnabled = true;
            this.SchipholSecondaryDepartureRunwayComboBox.Items.AddRange(new object[] {
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
            this.SchipholSecondaryDepartureRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.SchipholSecondaryDepartureRunwayComboBox.Name = "SchipholSecondaryDepartureRunwayComboBox";
            this.SchipholSecondaryDepartureRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.SchipholSecondaryDepartureRunwayComboBox.TabIndex = 13;
            // 
            // SchipholSecondaryDepartureRunwayCheckBox
            // 
            this.SchipholSecondaryDepartureRunwayCheckBox.AutoSize = true;
            this.SchipholSecondaryDepartureRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.SchipholSecondaryDepartureRunwayCheckBox.Name = "SchipholSecondaryDepartureRunwayCheckBox";
            this.SchipholSecondaryDepartureRunwayCheckBox.Size = new System.Drawing.Size(162, 17);
            this.SchipholSecondaryDepartureRunwayCheckBox.TabIndex = 12;
            this.SchipholSecondaryDepartureRunwayCheckBox.Text = "Secondary departure runway";
            this.SchipholSecondaryDepartureRunwayCheckBox.UseVisualStyleBackColor = true;
            this.SchipholSecondaryDepartureRunwayCheckBox.CheckedChanged += new System.EventHandler(this.SchipholSecondaryDepartureRunwayCheckBox_CheckedChanged);
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
            this.generateATISButton.Click += new System.EventHandler(this.GenerateATISButton_Click);
            // 
            // metarInputGroupBox
            // 
            this.metarInputGroupBox.Controls.Add(this.fetchMETARLabel);
            this.metarInputGroupBox.Controls.Add(this.selectBestRunwayButton);
            this.metarInputGroupBox.Controls.Add(this.icaoTextBox);
            this.metarInputGroupBox.Controls.Add(this.getMetarButton);
            this.metarInputGroupBox.Controls.Add(this.METARTextBox);
            this.metarInputGroupBox.Controls.Add(this.lastLabel);
            this.metarInputGroupBox.Controls.Add(this.processMETARButton);
            this.metarInputGroupBox.Location = new System.Drawing.Point(13, 29);
            this.metarInputGroupBox.Name = "metarInputGroupBox";
            this.metarInputGroupBox.Size = new System.Drawing.Size(535, 133);
            this.metarInputGroupBox.TabIndex = 0;
            this.metarInputGroupBox.TabStop = false;
            this.metarInputGroupBox.Text = "Metar";
            // 
            // fetchMETARLabel
            // 
            this.fetchMETARLabel.AutoSize = true;
            this.fetchMETARLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.fetchMETARLabel.Location = new System.Drawing.Point(131, 22);
            this.fetchMETARLabel.Name = "fetchMETARLabel";
            this.fetchMETARLabel.Size = new System.Drawing.Size(106, 13);
            this.fetchMETARLabel.TabIndex = 9;
            this.fetchMETARLabel.Text = "Fetching METAR in: ";
            this.fetchMETARLabel.Visible = false;
            // 
            // selectBestRunwayButton
            // 
            this.selectBestRunwayButton.Location = new System.Drawing.Point(415, 18);
            this.selectBestRunwayButton.Name = "selectBestRunwayButton";
            this.selectBestRunwayButton.Size = new System.Drawing.Size(114, 22);
            this.selectBestRunwayButton.TabIndex = 8;
            this.selectBestRunwayButton.Text = "Get EHAM runway(s)";
            this.selectBestRunwayButton.UseVisualStyleBackColor = true;
            this.selectBestRunwayButton.Click += new System.EventHandler(this.SelectBestRunwayButton_Click);
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
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(529, 81);
            this.outputTextBox.TabIndex = 22;
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
            this.EHBKchildMainRunwayGroupBox.Controls.Add(this.BeekRunwayCheckBox);
            this.EHBKchildMainRunwayGroupBox.Controls.Add(this.BeekRunwayComboBox);
            this.EHBKchildMainRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHBKchildMainRunwayGroupBox.Name = "EHBKchildMainRunwayGroupBox";
            this.EHBKchildMainRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHBKchildMainRunwayGroupBox.TabIndex = 0;
            this.EHBKchildMainRunwayGroupBox.TabStop = false;
            // 
            // BeekRunwayCheckBox
            // 
            this.BeekRunwayCheckBox.AutoSize = true;
            this.BeekRunwayCheckBox.Checked = true;
            this.BeekRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BeekRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.BeekRunwayCheckBox.Name = "BeekRunwayCheckBox";
            this.BeekRunwayCheckBox.Size = new System.Drawing.Size(86, 17);
            this.BeekRunwayCheckBox.TabIndex = 6;
            this.BeekRunwayCheckBox.Text = "Main runway";
            this.BeekRunwayCheckBox.UseVisualStyleBackColor = true;
            this.BeekRunwayCheckBox.CheckedChanged += new System.EventHandler(this.BeekRunwayCheckBox_CheckedChanged);
            // 
            // BeekRunwayComboBox
            // 
            this.BeekRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BeekRunwayComboBox.FormattingEnabled = true;
            this.BeekRunwayComboBox.Items.AddRange(new object[] {
            "03",
            "21"});
            this.BeekRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.BeekRunwayComboBox.Name = "BeekRunwayComboBox";
            this.BeekRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.BeekRunwayComboBox.TabIndex = 7;
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
            this.EHEHchildMainRunwayGroupBox.Controls.Add(this.EindhovenRunwayCheckBox);
            this.EHEHchildMainRunwayGroupBox.Controls.Add(this.EindhovenRunwayComboBox);
            this.EHEHchildMainRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHEHchildMainRunwayGroupBox.Name = "EHEHchildMainRunwayGroupBox";
            this.EHEHchildMainRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHEHchildMainRunwayGroupBox.TabIndex = 0;
            this.EHEHchildMainRunwayGroupBox.TabStop = false;
            // 
            // EindhovenRunwayCheckBox
            // 
            this.EindhovenRunwayCheckBox.AutoSize = true;
            this.EindhovenRunwayCheckBox.Checked = true;
            this.EindhovenRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EindhovenRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EindhovenRunwayCheckBox.Name = "EindhovenRunwayCheckBox";
            this.EindhovenRunwayCheckBox.Size = new System.Drawing.Size(86, 17);
            this.EindhovenRunwayCheckBox.TabIndex = 6;
            this.EindhovenRunwayCheckBox.Text = "Main runway";
            this.EindhovenRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EindhovenRunwayCheckBox.CheckedChanged += new System.EventHandler(this.EindhovenRunwayCheckBox_CheckedChanged);
            // 
            // EindhovenRunwayComboBox
            // 
            this.EindhovenRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EindhovenRunwayComboBox.FormattingEnabled = true;
            this.EindhovenRunwayComboBox.Items.AddRange(new object[] {
            "03",
            "21"});
            this.EindhovenRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EindhovenRunwayComboBox.Name = "EindhovenRunwayComboBox";
            this.EindhovenRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EindhovenRunwayComboBox.TabIndex = 7;
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
            this.EHGGchildMainRunwayGroupBox.Controls.Add(this.EeldeRunwayCheckBox);
            this.EHGGchildMainRunwayGroupBox.Controls.Add(this.EeldeRunwayComboBox);
            this.EHGGchildMainRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHGGchildMainRunwayGroupBox.Name = "EHGGchildMainRunwayGroupBox";
            this.EHGGchildMainRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHGGchildMainRunwayGroupBox.TabIndex = 0;
            this.EHGGchildMainRunwayGroupBox.TabStop = false;
            // 
            // EeldeRunwayCheckBox
            // 
            this.EeldeRunwayCheckBox.AutoSize = true;
            this.EeldeRunwayCheckBox.Checked = true;
            this.EeldeRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EeldeRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.EeldeRunwayCheckBox.Name = "EeldeRunwayCheckBox";
            this.EeldeRunwayCheckBox.Size = new System.Drawing.Size(86, 17);
            this.EeldeRunwayCheckBox.TabIndex = 6;
            this.EeldeRunwayCheckBox.Text = "Main runway";
            this.EeldeRunwayCheckBox.UseVisualStyleBackColor = true;
            this.EeldeRunwayCheckBox.CheckedChanged += new System.EventHandler(this.EeldeRunwayCheckBox_CheckedChanged);
            // 
            // EeldeRunwayComboBox
            // 
            this.EeldeRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EeldeRunwayComboBox.FormattingEnabled = true;
            this.EeldeRunwayComboBox.Items.AddRange(new object[] {
            "01",
            "05",
            "19",
            "23"});
            this.EeldeRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.EeldeRunwayComboBox.Name = "EeldeRunwayComboBox";
            this.EeldeRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.EeldeRunwayComboBox.TabIndex = 7;
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
            this.EHRDchildMainRunwyaGroupBox.Controls.Add(this.RotterdamRunwayCheckBox);
            this.EHRDchildMainRunwyaGroupBox.Controls.Add(this.RotterdamRunwayComboBox);
            this.EHRDchildMainRunwyaGroupBox.Location = new System.Drawing.Point(6, 19);
            this.EHRDchildMainRunwyaGroupBox.Name = "EHRDchildMainRunwyaGroupBox";
            this.EHRDchildMainRunwyaGroupBox.Size = new System.Drawing.Size(169, 65);
            this.EHRDchildMainRunwyaGroupBox.TabIndex = 0;
            this.EHRDchildMainRunwyaGroupBox.TabStop = false;
            // 
            // RotterdamRunwayCheckBox
            // 
            this.RotterdamRunwayCheckBox.AutoSize = true;
            this.RotterdamRunwayCheckBox.Checked = true;
            this.RotterdamRunwayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RotterdamRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.RotterdamRunwayCheckBox.Name = "RotterdamRunwayCheckBox";
            this.RotterdamRunwayCheckBox.Size = new System.Drawing.Size(86, 17);
            this.RotterdamRunwayCheckBox.TabIndex = 6;
            this.RotterdamRunwayCheckBox.Text = "Main runway";
            this.RotterdamRunwayCheckBox.UseVisualStyleBackColor = true;
            this.RotterdamRunwayCheckBox.CheckedChanged += new System.EventHandler(this.RotterdamRunwayCheckBox_CheckedChanged);
            // 
            // RotterdamRunwayComboBox
            // 
            this.RotterdamRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RotterdamRunwayComboBox.FormattingEnabled = true;
            this.RotterdamRunwayComboBox.Items.AddRange(new object[] {
            "06",
            "24"});
            this.RotterdamRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.RotterdamRunwayComboBox.Name = "RotterdamRunwayComboBox";
            this.RotterdamRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.RotterdamRunwayComboBox.TabIndex = 7;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.runwayInfoToolStripMenuItem,
            this.soundToolStripMenuItem,
            this.terminalAerodromeForecastToolStripMenuItem,
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
            this.seperatorDutchVACCToolStripMenuItem,
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
            this.amsterdamInfoToolStripMenuItem.Click += new System.EventHandler(this.AmsterdamInfoToolStripMenuItem_Click);
            // 
            // dutchVACCToolStripMenuItem
            // 
            this.dutchVACCToolStripMenuItem.AutoSize = false;
            this.dutchVACCToolStripMenuItem.Name = "dutchVACCToolStripMenuItem";
            this.dutchVACCToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.dutchVACCToolStripMenuItem.Text = "Dutch VACC";
            this.dutchVACCToolStripMenuItem.Click += new System.EventHandler(this.DutchVACCToolStripMenuItem_Click);
            // 
            // seperatorDutchVACCToolStripMenuItem
            // 
            this.seperatorDutchVACCToolStripMenuItem.Name = "seperatorDutchVACCToolStripMenuItem";
            this.seperatorDutchVACCToolStripMenuItem.Size = new System.Drawing.Size(157, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoFetchMETARToolStripMenuItem,
            this.autoLoadEHAMRunwayToolStripMenuItem,
            this.autoProcessMETARToolStripMenuItem,
            this.seperatorAutoProcessToolStripMenuItem,
            this.ehamToolStripMenuItem,
            this.ehrdToolStripMenuItem,
            this.seperatorEHRDToolStripMenuItem,
            this.playSoundWhenMETARIsFetchedToolStripMenuItem,
            this.randomLetterToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // autoFetchMETARToolStripMenuItem
            // 
            this.autoFetchMETARToolStripMenuItem.CheckOnClick = true;
            this.autoFetchMETARToolStripMenuItem.Name = "autoFetchMETARToolStripMenuItem";
            this.autoFetchMETARToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.autoFetchMETARToolStripMenuItem.Text = "Auto fetch METAR ";
            this.autoFetchMETARToolStripMenuItem.CheckedChanged += new System.EventHandler(this.AutoFetchMETARToolStripMenuItem_CheckedChanged);
            // 
            // autoLoadEHAMRunwayToolStripMenuItem
            // 
            this.autoLoadEHAMRunwayToolStripMenuItem.CheckOnClick = true;
            this.autoLoadEHAMRunwayToolStripMenuItem.Name = "autoLoadEHAMRunwayToolStripMenuItem";
            this.autoLoadEHAMRunwayToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.autoLoadEHAMRunwayToolStripMenuItem.Text = "Auto load EHAM runways (on startup)";
            this.autoLoadEHAMRunwayToolStripMenuItem.CheckedChanged += new System.EventHandler(this.AutoLoadEHAMRunwayToolStripMenuItem_CheckedChanged);
            // 
            // autoProcessMETARToolStripMenuItem
            // 
            this.autoProcessMETARToolStripMenuItem.CheckOnClick = true;
            this.autoProcessMETARToolStripMenuItem.Name = "autoProcessMETARToolStripMenuItem";
            this.autoProcessMETARToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.autoProcessMETARToolStripMenuItem.Text = "Auto process METAR";
            this.autoProcessMETARToolStripMenuItem.CheckedChanged += new System.EventHandler(this.AutoProcessMETARToolStripMenuItem_CheckedChanged);
            // 
            // seperatorAutoProcessToolStripMenuItem
            // 
            this.seperatorAutoProcessToolStripMenuItem.Name = "seperatorAutoProcessToolStripMenuItem";
            this.seperatorAutoProcessToolStripMenuItem.Size = new System.Drawing.Size(292, 6);
            // 
            // ehamToolStripMenuItem
            // 
            this.ehamToolStripMenuItem.CheckOnClick = true;
            this.ehamToolStripMenuItem.Name = "ehamToolStripMenuItem";
            this.ehamToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.ehamToolStripMenuItem.Text = "EHAM: A - M";
            this.ehamToolStripMenuItem.CheckedChanged += new System.EventHandler(this.EHAMToolStripMenuItem_CheckedChanged);
            // 
            // ehrdToolStripMenuItem
            // 
            this.ehrdToolStripMenuItem.CheckOnClick = true;
            this.ehrdToolStripMenuItem.Name = "ehrdToolStripMenuItem";
            this.ehrdToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.ehrdToolStripMenuItem.Text = "EHRD: N - Z";
            this.ehrdToolStripMenuItem.CheckedChanged += new System.EventHandler(this.EHRDToolStripMenuItem_CheckedChanged);
            // 
            // seperatorEHRDToolStripMenuItem
            // 
            this.seperatorEHRDToolStripMenuItem.Name = "seperatorEHRDToolStripMenuItem";
            this.seperatorEHRDToolStripMenuItem.Size = new System.Drawing.Size(292, 6);
            // 
            // playSoundWhenMETARIsFetchedToolStripMenuItem
            // 
            this.playSoundWhenMETARIsFetchedToolStripMenuItem.CheckOnClick = true;
            this.playSoundWhenMETARIsFetchedToolStripMenuItem.Name = "playSoundWhenMETARIsFetchedToolStripMenuItem";
            this.playSoundWhenMETARIsFetchedToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.playSoundWhenMETARIsFetchedToolStripMenuItem.Text = "Play sound when METAR is fetched";
            this.playSoundWhenMETARIsFetchedToolStripMenuItem.CheckedChanged += new System.EventHandler(this.PlaySoundWhenMETARIsFetchedToolStripMenuItem_CheckedChanged);
            // 
            // randomLetterToolStripMenuItem
            // 
            this.randomLetterToolStripMenuItem.CheckOnClick = true;
            this.randomLetterToolStripMenuItem.Name = "randomLetterToolStripMenuItem";
            this.randomLetterToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.randomLetterToolStripMenuItem.Text = "Random letter (on startup/switching tabs)";
            this.randomLetterToolStripMenuItem.CheckedChanged += new System.EventHandler(this.RandomLetterToolStripMenuItem_CheckedChanged);
            // 
            // runwayInfoToolStripMenuItem
            // 
            this.runwayInfoToolStripMenuItem.Enabled = false;
            this.runwayInfoToolStripMenuItem.Name = "runwayInfoToolStripMenuItem";
            this.runwayInfoToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.runwayInfoToolStripMenuItem.Text = "Runway Info";
            this.runwayInfoToolStripMenuItem.Click += new System.EventHandler(this.RunwayToolStripMenuItem_Click);
            // 
            // soundToolStripMenuItem
            // 
            this.soundToolStripMenuItem.Name = "soundToolStripMenuItem";
            this.soundToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.soundToolStripMenuItem.Text = "Sound";
            this.soundToolStripMenuItem.Click += new System.EventHandler(this.SoundToolStripMenuItem_Click);
            // 
            // terminalAerodromeForecastToolStripMenuItem
            // 
            this.terminalAerodromeForecastToolStripMenuItem.Name = "terminalAerodromeForecastToolStripMenuItem";
            this.terminalAerodromeForecastToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.terminalAerodromeForecastToolStripMenuItem.Text = "TAF";
            this.terminalAerodromeForecastToolStripMenuItem.Click += new System.EventHandler(this.TerminalAerodromeForecastToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.seperatorAboutToolStripMenuItem,
            this.atcOperationalInformationManualToolStripMenuItem,
            this.dutchVACCATISGeneratorV2ManualToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.infoToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // seperatorAboutToolStripMenuItem
            // 
            this.seperatorAboutToolStripMenuItem.Name = "seperatorAboutToolStripMenuItem";
            this.seperatorAboutToolStripMenuItem.Size = new System.Drawing.Size(275, 6);
            // 
            // atcOperationalInformationManualToolStripMenuItem
            // 
            this.atcOperationalInformationManualToolStripMenuItem.Name = "atcOperationalInformationManualToolStripMenuItem";
            this.atcOperationalInformationManualToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.atcOperationalInformationManualToolStripMenuItem.Text = "ATC Operational Information Manual";
            this.atcOperationalInformationManualToolStripMenuItem.Click += new System.EventHandler(this.ATCOperationalInformationManualToolStripMenuItem_Click);
            // 
            // dutchVACCATISGeneratorV2ManualToolStripMenuItem
            // 
            this.dutchVACCATISGeneratorV2ManualToolStripMenuItem.Name = "dutchVACCATISGeneratorV2ManualToolStripMenuItem";
            this.dutchVACCATISGeneratorV2ManualToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
            this.dutchVACCATISGeneratorV2ManualToolStripMenuItem.Text = "Dutch VACC ATIS Generator v2 Manual";
            this.dutchVACCATISGeneratorV2ManualToolStripMenuItem.Click += new System.EventHandler(this.DutchVACCATISGeneratorV2ManualToolStripMenuItem_Click);
            // 
            // additionalOptionsGroupBox
            // 
            this.additionalOptionsGroupBox.Controls.Add(this.userDefinedExtraCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.addWindRecordCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.markTempCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.approachAndArrivalCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.arrivalCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.approachCheckBox);
            this.additionalOptionsGroupBox.Location = new System.Drawing.Point(399, 168);
            this.additionalOptionsGroupBox.Name = "additionalOptionsGroupBox";
            this.additionalOptionsGroupBox.Size = new System.Drawing.Size(149, 232);
            this.additionalOptionsGroupBox.TabIndex = 22;
            this.additionalOptionsGroupBox.TabStop = false;
            this.additionalOptionsGroupBox.Text = "Additional Options";
            // 
            // userDefinedExtraCheckBox
            // 
            this.userDefinedExtraCheckBox.AutoSize = true;
            this.userDefinedExtraCheckBox.Location = new System.Drawing.Point(6, 159);
            this.userDefinedExtraCheckBox.Name = "userDefinedExtraCheckBox";
            this.userDefinedExtraCheckBox.Size = new System.Drawing.Size(112, 17);
            this.userDefinedExtraCheckBox.TabIndex = 19;
            this.userDefinedExtraCheckBox.Text = "User defined extra";
            this.userDefinedExtraCheckBox.UseVisualStyleBackColor = true;
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
            // approachAndArrivalCheckBox
            // 
            this.approachAndArrivalCheckBox.AutoSize = true;
            this.approachAndArrivalCheckBox.Location = new System.Drawing.Point(6, 75);
            this.approachAndArrivalCheckBox.Name = "approachAndArrivalCheckBox";
            this.approachAndArrivalCheckBox.Size = new System.Drawing.Size(142, 17);
            this.approachAndArrivalCheckBox.TabIndex = 16;
            this.approachAndArrivalCheckBox.Text = "APP + ARR callsign only";
            this.approachAndArrivalCheckBox.UseVisualStyleBackColor = true;
            this.approachAndArrivalCheckBox.CheckedChanged += new System.EventHandler(this.ApproachAndArrivalCheckBox_CheckedChanged);
            // 
            // arrivalCheckBox
            // 
            this.arrivalCheckBox.AutoSize = true;
            this.arrivalCheckBox.Location = new System.Drawing.Point(6, 47);
            this.arrivalCheckBox.Name = "arrivalCheckBox";
            this.arrivalCheckBox.Size = new System.Drawing.Size(109, 17);
            this.arrivalCheckBox.TabIndex = 15;
            this.arrivalCheckBox.Text = "ARR callsign only";
            this.arrivalCheckBox.UseVisualStyleBackColor = true;
            this.arrivalCheckBox.CheckedChanged += new System.EventHandler(this.ArrivalCheckBox_CheckedChanged);
            // 
            // approachCheckBox
            // 
            this.approachCheckBox.AutoSize = true;
            this.approachCheckBox.Location = new System.Drawing.Point(6, 19);
            this.approachCheckBox.Name = "approachCheckBox";
            this.approachCheckBox.Size = new System.Drawing.Size(107, 17);
            this.approachCheckBox.TabIndex = 14;
            this.approachCheckBox.Text = "APP callsign only";
            this.approachCheckBox.UseVisualStyleBackColor = true;
            this.approachCheckBox.CheckedChanged += new System.EventHandler(this.ApproachCheckBox_CheckedChanged);
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
            this.runwayInfoButton.Click += new System.EventHandler(this.RunwayButton_Click);
            // 
            // soundButton
            // 
            this.soundButton.Location = new System.Drawing.Point(195, 623);
            this.soundButton.Name = "soundButton";
            this.soundButton.Size = new System.Drawing.Size(171, 19);
            this.soundButton.TabIndex = 24;
            this.soundButton.Text = "▼";
            this.soundButton.UseVisualStyleBackColor = true;
            this.soundButton.Click += new System.EventHandler(this.SoundButton_Click);
            // 
            // fetchMETARTimer
            // 
            this.fetchMETARTimer.Interval = 1000;
            this.fetchMETARTimer.Tick += new System.EventHandler(this.METARFetchTimer_Tick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
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
            this.Name = "MainForm";
            this.Text = "Dutch VACC ATIS Generator";
            this.LocationChanged += new System.EventHandler(this.DutchVACCATISGenerator_LocationChanged);
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
        private System.Windows.Forms.TextBox METARTextBox;
        private System.Windows.Forms.Label lastLabel;
        private System.Windows.Forms.Button processMETARButton;
        private System.Windows.Forms.GroupBox atisIndexGroupBox;
        private System.Windows.Forms.Button previousATISLetterButton;
        private System.Windows.Forms.Button nextATISLetterButton;
        private System.Windows.Forms.Label atisLetterLabel;
        private System.Windows.Forms.GroupBox EHAMmainRunwaysGroupBox;
        private System.Windows.Forms.CheckBox SchipholSecondaryLandingRunwayCheckBox;
        private System.Windows.Forms.CheckBox SchipholMainLandingRunwayCheckBox;
        private System.Windows.Forms.ComboBox SchipholSecondaryLandingRunwayComboBox;
        private System.Windows.Forms.GroupBox EHAMsecondaryLandingRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHAMmainLandingRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHAMsecondaryRunwaysGroupBox;
        private System.Windows.Forms.GroupBox EHAMsecondaryDepartureRunwayGroupBox;
        private System.Windows.Forms.ComboBox SchipholSecondaryDepartureRunwayComboBox;
        private System.Windows.Forms.CheckBox SchipholSecondaryDepartureRunwayCheckBox;
        private System.Windows.Forms.GroupBox EHAMmainDepartureRunwayGroupBox;
        private System.Windows.Forms.CheckBox SchipholMainDepartureRunwayCheckBox;
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
        private System.Windows.Forms.CheckBox RotterdamRunwayCheckBox;
        private System.Windows.Forms.GroupBox EHGGmainRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHGGchildMainRunwayGroupBox;
        private System.Windows.Forms.CheckBox EeldeRunwayCheckBox;
        private System.Windows.Forms.GroupBox additionalOptionsGroupBox;
        private System.Windows.Forms.CheckBox markTempCheckBox;
        private System.Windows.Forms.CheckBox approachAndArrivalCheckBox;
        private System.Windows.Forms.CheckBox arrivalCheckBox;
        private System.Windows.Forms.CheckBox approachCheckBox;
        private System.Windows.Forms.GroupBox EHBKmainRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHBKchildMainRunwayGroupBox;
        private System.Windows.Forms.CheckBox BeekRunwayCheckBox;
        private System.Windows.Forms.GroupBox EHEHmainRunwayGroupBox;
        private System.Windows.Forms.GroupBox EHEHchildMainRunwayGroupBox;
        private System.Windows.Forms.CheckBox EindhovenRunwayCheckBox;
        public System.Windows.Forms.TabControl ICAOTabControl;
        public System.Windows.Forms.Button runwayInfoButton;
        public System.Windows.Forms.TextBox outputTextBox;
        public System.Windows.Forms.Button soundButton;
        public System.Windows.Forms.ComboBox SchipholMainLandingRunwayComboBox;
        public System.Windows.Forms.ComboBox SchipholMainDepartureRunwayComboBox;
        public System.Windows.Forms.ComboBox RotterdamRunwayComboBox;
        public System.Windows.Forms.ComboBox EeldeRunwayComboBox;
        public System.Windows.Forms.ComboBox BeekRunwayComboBox;
        public System.Windows.Forms.ComboBox EindhovenRunwayComboBox;
        private System.Windows.Forms.CheckBox addWindRecordCheckBox;
        private System.Windows.Forms.ToolStripMenuItem amsterdamInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dutchVACCToolStripMenuItem;
        private System.Windows.Forms.Button selectBestRunwayButton;
        public System.Windows.Forms.ToolStripMenuItem terminalAerodromeForecastToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem runwayInfoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem soundToolStripMenuItem;
        private System.Windows.Forms.CheckBox userDefinedExtraCheckBox;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoProcessMETARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoFetchMETARToolStripMenuItem;
        private System.Windows.Forms.Label fetchMETARLabel;
        private System.Windows.Forms.Timer fetchMETARTimer;
        private System.Windows.Forms.ToolStripMenuItem autoLoadEHAMRunwayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ehamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ehrdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomLetterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playSoundWhenMETARIsFetchedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atcOperationalInformationManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dutchVACCATISGeneratorV2ManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator seperatorAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator seperatorDutchVACCToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator seperatorAutoProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator seperatorEHRDToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

