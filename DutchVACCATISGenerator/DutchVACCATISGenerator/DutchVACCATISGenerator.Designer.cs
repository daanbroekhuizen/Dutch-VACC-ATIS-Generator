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
            this.mainRunwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.mainLandingGroupBox = new System.Windows.Forms.GroupBox();
            this.mainLandingRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.mainLandingRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.mainDepartureRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.mainDepartureRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.mainDepartureRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.secondaryLandingRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.secondaryLandingRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.secondaryLandingRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.secondaryRunwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.secondaryDepartureRunwayGroupBox = new System.Windows.Forms.GroupBox();
            this.secondaryDepartureRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.secondaryDepartureRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.tlLevelGroupBox = new System.Windows.Forms.GroupBox();
            this.tlOutLabel = new System.Windows.Forms.Label();
            this.tlHeaderLabel = new System.Windows.Forms.Label();
            this.generateATISButton = new System.Windows.Forms.Button();
            this.metarInputGroupBox = new System.Windows.Forms.GroupBox();
            this.additionalOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.markTempCheckBox = new System.Windows.Forms.CheckBox();
            this.appArrOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.arrOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.appOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.copyOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.outputOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.outputGroupBox = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.ICAOTabControl = new System.Windows.Forms.TabControl();
            this.EHAM = new System.Windows.Forms.TabPage();
            this.EHBK = new System.Windows.Forms.TabPage();
            this.EHEH = new System.Windows.Forms.TabPage();
            this.EHGG = new System.Windows.Forms.TabPage();
            this.EHRD = new System.Windows.Forms.TabPage();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atisIndexGroupBox.SuspendLayout();
            this.mainRunwaysGroupBox.SuspendLayout();
            this.mainLandingGroupBox.SuspendLayout();
            this.mainDepartureRunwayGroupBox.SuspendLayout();
            this.secondaryLandingRunwayGroupBox.SuspendLayout();
            this.secondaryRunwaysGroupBox.SuspendLayout();
            this.secondaryDepartureRunwayGroupBox.SuspendLayout();
            this.tlLevelGroupBox.SuspendLayout();
            this.metarInputGroupBox.SuspendLayout();
            this.additionalOptionsGroupBox.SuspendLayout();
            this.outputOptionsGroupBox.SuspendLayout();
            this.outputGroupBox.SuspendLayout();
            this.ICAOTabControl.SuspendLayout();
            this.EHAM.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // getMetarButton
            // 
            this.getMetarButton.Location = new System.Drawing.Point(50, 19);
            this.getMetarButton.Name = "getMetarButton";
            this.getMetarButton.Size = new System.Drawing.Size(75, 20);
            this.getMetarButton.TabIndex = 2;
            this.getMetarButton.Text = "Get Metar";
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
            this.metarTextBox.Size = new System.Drawing.Size(430, 32);
            this.metarTextBox.TabIndex = 3;
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
            this.processMetarButton.Location = new System.Drawing.Point(442, 44);
            this.processMetarButton.Name = "processMetarButton";
            this.processMetarButton.Size = new System.Drawing.Size(83, 83);
            this.processMetarButton.TabIndex = 4;
            this.processMetarButton.Text = "Process Metar";
            this.processMetarButton.UseVisualStyleBackColor = true;
            this.processMetarButton.Click += new System.EventHandler(this.processMetarButton_Click);
            // 
            // atisIndexGroupBox
            // 
            this.atisIndexGroupBox.Controls.Add(this.nextATISLetterButton);
            this.atisIndexGroupBox.Controls.Add(this.previousATISLetterButton);
            this.atisIndexGroupBox.Controls.Add(this.atisLetterLabel);
            this.atisIndexGroupBox.Location = new System.Drawing.Point(12, 404);
            this.atisIndexGroupBox.Name = "atisIndexGroupBox";
            this.atisIndexGroupBox.Size = new System.Drawing.Size(116, 112);
            this.atisIndexGroupBox.TabIndex = 0;
            this.atisIndexGroupBox.TabStop = false;
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
            // mainRunwaysGroupBox
            // 
            this.mainRunwaysGroupBox.Controls.Add(this.mainLandingGroupBox);
            this.mainRunwaysGroupBox.Controls.Add(this.mainDepartureRunwayGroupBox);
            this.mainRunwaysGroupBox.Location = new System.Drawing.Point(3, 3);
            this.mainRunwaysGroupBox.Name = "mainRunwaysGroupBox";
            this.mainRunwaysGroupBox.Size = new System.Drawing.Size(353, 96);
            this.mainRunwaysGroupBox.TabIndex = 0;
            this.mainRunwaysGroupBox.TabStop = false;
            this.mainRunwaysGroupBox.Text = "Main Runway(s)";
            // 
            // mainLandingGroupBox
            // 
            this.mainLandingGroupBox.Controls.Add(this.mainLandingRunwayCheckBox);
            this.mainLandingGroupBox.Controls.Add(this.mainLandingRunwayComboBox);
            this.mainLandingGroupBox.Location = new System.Drawing.Point(6, 19);
            this.mainLandingGroupBox.Name = "mainLandingGroupBox";
            this.mainLandingGroupBox.Size = new System.Drawing.Size(169, 65);
            this.mainLandingGroupBox.TabIndex = 0;
            this.mainLandingGroupBox.TabStop = false;
            // 
            // mainLandingRunwayCheckBox
            // 
            this.mainLandingRunwayCheckBox.AutoSize = true;
            this.mainLandingRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.mainLandingRunwayCheckBox.Name = "mainLandingRunwayCheckBox";
            this.mainLandingRunwayCheckBox.Size = new System.Drawing.Size(123, 17);
            this.mainLandingRunwayCheckBox.TabIndex = 6;
            this.mainLandingRunwayCheckBox.Text = "Main landing runway";
            this.mainLandingRunwayCheckBox.UseVisualStyleBackColor = true;
            this.mainLandingRunwayCheckBox.CheckedChanged += new System.EventHandler(this.mainLandingRunwayCheckBox_CheckedChanged);
            // 
            // mainLandingRunwayComboBox
            // 
            this.mainLandingRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainLandingRunwayComboBox.Enabled = false;
            this.mainLandingRunwayComboBox.FormattingEnabled = true;
            this.mainLandingRunwayComboBox.Items.AddRange(new object[] {
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
            this.mainLandingRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.mainLandingRunwayComboBox.Name = "mainLandingRunwayComboBox";
            this.mainLandingRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.mainLandingRunwayComboBox.TabIndex = 7;
            // 
            // mainDepartureRunwayGroupBox
            // 
            this.mainDepartureRunwayGroupBox.Controls.Add(this.mainDepartureRunwayCheckBox);
            this.mainDepartureRunwayGroupBox.Controls.Add(this.mainDepartureRunwayComboBox);
            this.mainDepartureRunwayGroupBox.Location = new System.Drawing.Point(177, 19);
            this.mainDepartureRunwayGroupBox.Name = "mainDepartureRunwayGroupBox";
            this.mainDepartureRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.mainDepartureRunwayGroupBox.TabIndex = 0;
            this.mainDepartureRunwayGroupBox.TabStop = false;
            // 
            // mainDepartureRunwayCheckBox
            // 
            this.mainDepartureRunwayCheckBox.AutoSize = true;
            this.mainDepartureRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.mainDepartureRunwayCheckBox.Name = "mainDepartureRunwayCheckBox";
            this.mainDepartureRunwayCheckBox.Size = new System.Drawing.Size(134, 17);
            this.mainDepartureRunwayCheckBox.TabIndex = 8;
            this.mainDepartureRunwayCheckBox.Text = "Main departure runway";
            this.mainDepartureRunwayCheckBox.UseVisualStyleBackColor = true;
            this.mainDepartureRunwayCheckBox.CheckedChanged += new System.EventHandler(this.mainDepartureRunwayCheckBox_CheckedChanged);
            // 
            // mainDepartureRunwayComboBox
            // 
            this.mainDepartureRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainDepartureRunwayComboBox.Enabled = false;
            this.mainDepartureRunwayComboBox.FormattingEnabled = true;
            this.mainDepartureRunwayComboBox.Items.AddRange(new object[] {
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
            this.mainDepartureRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.mainDepartureRunwayComboBox.Name = "mainDepartureRunwayComboBox";
            this.mainDepartureRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.mainDepartureRunwayComboBox.TabIndex = 9;
            // 
            // secondaryLandingRunwayCheckBox
            // 
            this.secondaryLandingRunwayCheckBox.AutoSize = true;
            this.secondaryLandingRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.secondaryLandingRunwayCheckBox.Name = "secondaryLandingRunwayCheckBox";
            this.secondaryLandingRunwayCheckBox.Size = new System.Drawing.Size(151, 17);
            this.secondaryLandingRunwayCheckBox.TabIndex = 10;
            this.secondaryLandingRunwayCheckBox.Text = "Secondary landing runway";
            this.secondaryLandingRunwayCheckBox.UseVisualStyleBackColor = true;
            this.secondaryLandingRunwayCheckBox.CheckedChanged += new System.EventHandler(this.secondaryLandingRunway_CheckedChanged);
            // 
            // secondaryLandingRunwayComboBox
            // 
            this.secondaryLandingRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondaryLandingRunwayComboBox.Enabled = false;
            this.secondaryLandingRunwayComboBox.FormattingEnabled = true;
            this.secondaryLandingRunwayComboBox.Items.AddRange(new object[] {
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
            this.secondaryLandingRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.secondaryLandingRunwayComboBox.Name = "secondaryLandingRunwayComboBox";
            this.secondaryLandingRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.secondaryLandingRunwayComboBox.TabIndex = 11;
            // 
            // secondaryLandingRunwayGroupBox
            // 
            this.secondaryLandingRunwayGroupBox.Controls.Add(this.secondaryLandingRunwayComboBox);
            this.secondaryLandingRunwayGroupBox.Controls.Add(this.secondaryLandingRunwayCheckBox);
            this.secondaryLandingRunwayGroupBox.Location = new System.Drawing.Point(6, 19);
            this.secondaryLandingRunwayGroupBox.Name = "secondaryLandingRunwayGroupBox";
            this.secondaryLandingRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.secondaryLandingRunwayGroupBox.TabIndex = 0;
            this.secondaryLandingRunwayGroupBox.TabStop = false;
            // 
            // secondaryRunwaysGroupBox
            // 
            this.secondaryRunwaysGroupBox.Controls.Add(this.secondaryLandingRunwayGroupBox);
            this.secondaryRunwaysGroupBox.Controls.Add(this.secondaryDepartureRunwayGroupBox);
            this.secondaryRunwaysGroupBox.Location = new System.Drawing.Point(3, 105);
            this.secondaryRunwaysGroupBox.Name = "secondaryRunwaysGroupBox";
            this.secondaryRunwaysGroupBox.Size = new System.Drawing.Size(353, 96);
            this.secondaryRunwaysGroupBox.TabIndex = 0;
            this.secondaryRunwaysGroupBox.TabStop = false;
            this.secondaryRunwaysGroupBox.Text = "Secondary Runway(s)";
            // 
            // secondaryDepartureRunwayGroupBox
            // 
            this.secondaryDepartureRunwayGroupBox.Controls.Add(this.secondaryDepartureRunwayComboBox);
            this.secondaryDepartureRunwayGroupBox.Controls.Add(this.secondaryDepartureRunwayCheckBox);
            this.secondaryDepartureRunwayGroupBox.Location = new System.Drawing.Point(178, 19);
            this.secondaryDepartureRunwayGroupBox.Name = "secondaryDepartureRunwayGroupBox";
            this.secondaryDepartureRunwayGroupBox.Size = new System.Drawing.Size(169, 65);
            this.secondaryDepartureRunwayGroupBox.TabIndex = 0;
            this.secondaryDepartureRunwayGroupBox.TabStop = false;
            // 
            // secondaryDepartureRunwayComboBox
            // 
            this.secondaryDepartureRunwayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondaryDepartureRunwayComboBox.Enabled = false;
            this.secondaryDepartureRunwayComboBox.FormattingEnabled = true;
            this.secondaryDepartureRunwayComboBox.Items.AddRange(new object[] {
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
            this.secondaryDepartureRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.secondaryDepartureRunwayComboBox.Name = "secondaryDepartureRunwayComboBox";
            this.secondaryDepartureRunwayComboBox.Size = new System.Drawing.Size(157, 21);
            this.secondaryDepartureRunwayComboBox.TabIndex = 13;
            // 
            // secondaryDepartureRunwayCheckBox
            // 
            this.secondaryDepartureRunwayCheckBox.AutoSize = true;
            this.secondaryDepartureRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.secondaryDepartureRunwayCheckBox.Name = "secondaryDepartureRunwayCheckBox";
            this.secondaryDepartureRunwayCheckBox.Size = new System.Drawing.Size(162, 17);
            this.secondaryDepartureRunwayCheckBox.TabIndex = 12;
            this.secondaryDepartureRunwayCheckBox.Text = "Secondary departure runway";
            this.secondaryDepartureRunwayCheckBox.UseVisualStyleBackColor = true;
            this.secondaryDepartureRunwayCheckBox.CheckedChanged += new System.EventHandler(this.secondaryDepartureRunwayCheckBox_CheckedChanged);
            // 
            // tlLevelGroupBox
            // 
            this.tlLevelGroupBox.Controls.Add(this.tlOutLabel);
            this.tlLevelGroupBox.Controls.Add(this.tlHeaderLabel);
            this.tlLevelGroupBox.Location = new System.Drawing.Point(134, 404);
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
            // generateATISButton
            // 
            this.generateATISButton.Enabled = false;
            this.generateATISButton.Location = new System.Drawing.Point(256, 410);
            this.generateATISButton.Name = "generateATISButton";
            this.generateATISButton.Size = new System.Drawing.Size(109, 106);
            this.generateATISButton.TabIndex = 21;
            this.generateATISButton.Text = "Generate ATIS";
            this.generateATISButton.UseVisualStyleBackColor = true;
            this.generateATISButton.Click += new System.EventHandler(this.generateATISButton_Click);
            // 
            // metarInputGroupBox
            // 
            this.metarInputGroupBox.Controls.Add(this.icaoTextBox);
            this.metarInputGroupBox.Controls.Add(this.getMetarButton);
            this.metarInputGroupBox.Controls.Add(this.metarTextBox);
            this.metarInputGroupBox.Controls.Add(this.lastLabel);
            this.metarInputGroupBox.Controls.Add(this.processMetarButton);
            this.metarInputGroupBox.Location = new System.Drawing.Point(12, 27);
            this.metarInputGroupBox.Name = "metarInputGroupBox";
            this.metarInputGroupBox.Size = new System.Drawing.Size(532, 133);
            this.metarInputGroupBox.TabIndex = 0;
            this.metarInputGroupBox.TabStop = false;
            this.metarInputGroupBox.Text = "Metar";
            // 
            // additionalOptionsGroupBox
            // 
            this.additionalOptionsGroupBox.Controls.Add(this.markTempCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.appArrOnlyCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.arrOnlyCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.appOnlyCheckBox);
            this.additionalOptionsGroupBox.Location = new System.Drawing.Point(362, 3);
            this.additionalOptionsGroupBox.Name = "additionalOptionsGroupBox";
            this.additionalOptionsGroupBox.Size = new System.Drawing.Size(157, 198);
            this.additionalOptionsGroupBox.TabIndex = 0;
            this.additionalOptionsGroupBox.TabStop = false;
            this.additionalOptionsGroupBox.Text = "Additional Options";
            // 
            // markTempCheckBox
            // 
            this.markTempCheckBox.AutoSize = true;
            this.markTempCheckBox.Location = new System.Drawing.Point(6, 81);
            this.markTempCheckBox.Name = "markTempCheckBox";
            this.markTempCheckBox.Size = new System.Drawing.Size(122, 30);
            this.markTempCheckBox.TabIndex = 17;
            this.markTempCheckBox.Text = "Inverted surface-\r\ntemperature warning";
            this.markTempCheckBox.UseVisualStyleBackColor = true;
            // 
            // appArrOnlyCheckBox
            // 
            this.appArrOnlyCheckBox.AutoSize = true;
            this.appArrOnlyCheckBox.Location = new System.Drawing.Point(6, 65);
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
            this.arrOnlyCheckBox.Location = new System.Drawing.Point(6, 42);
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
            // copyOutputCheckBox
            // 
            this.copyOutputCheckBox.AutoSize = true;
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
            this.outputOptionsGroupBox.Location = new System.Drawing.Point(371, 404);
            this.outputOptionsGroupBox.Name = "outputOptionsGroupBox";
            this.outputOptionsGroupBox.Size = new System.Drawing.Size(173, 112);
            this.outputOptionsGroupBox.TabIndex = 0;
            this.outputOptionsGroupBox.TabStop = false;
            this.outputOptionsGroupBox.Text = "Output options";
            // 
            // outputGroupBox
            // 
            this.outputGroupBox.Controls.Add(this.outputTextBox);
            this.outputGroupBox.Location = new System.Drawing.Point(12, 522);
            this.outputGroupBox.Name = "outputGroupBox";
            this.outputGroupBox.Size = new System.Drawing.Size(532, 93);
            this.outputGroupBox.TabIndex = 0;
            this.outputGroupBox.TabStop = false;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(3, 9);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(526, 81);
            this.outputTextBox.TabIndex = 22;
            // 
            // ICAOTabControl
            // 
            this.ICAOTabControl.Controls.Add(this.EHAM);
            this.ICAOTabControl.Controls.Add(this.EHBK);
            this.ICAOTabControl.Controls.Add(this.EHEH);
            this.ICAOTabControl.Controls.Add(this.EHGG);
            this.ICAOTabControl.Controls.Add(this.EHRD);
            this.ICAOTabControl.Location = new System.Drawing.Point(12, 166);
            this.ICAOTabControl.Name = "ICAOTabControl";
            this.ICAOTabControl.SelectedIndex = 0;
            this.ICAOTabControl.Size = new System.Drawing.Size(532, 232);
            this.ICAOTabControl.TabIndex = 5;
            this.ICAOTabControl.SelectedIndexChanged += new System.EventHandler(this.ICAOTabControl_SelectedIndexChanged);
            // 
            // EHAM
            // 
            this.EHAM.Controls.Add(this.mainRunwaysGroupBox);
            this.EHAM.Controls.Add(this.secondaryRunwaysGroupBox);
            this.EHAM.Controls.Add(this.additionalOptionsGroupBox);
            this.EHAM.Location = new System.Drawing.Point(4, 22);
            this.EHAM.Name = "EHAM";
            this.EHAM.Size = new System.Drawing.Size(524, 206);
            this.EHAM.TabIndex = 5;
            this.EHAM.Text = "EHAM";
            this.EHAM.UseVisualStyleBackColor = true;
            // 
            // EHBK
            // 
            this.EHBK.Location = new System.Drawing.Point(4, 22);
            this.EHBK.Name = "EHBK";
            this.EHBK.Padding = new System.Windows.Forms.Padding(3);
            this.EHBK.Size = new System.Drawing.Size(524, 206);
            this.EHBK.TabIndex = 1;
            this.EHBK.Text = "EHBK";
            this.EHBK.UseVisualStyleBackColor = true;
            // 
            // EHEH
            // 
            this.EHEH.Location = new System.Drawing.Point(4, 22);
            this.EHEH.Name = "EHEH";
            this.EHEH.Size = new System.Drawing.Size(524, 206);
            this.EHEH.TabIndex = 2;
            this.EHEH.Text = "EHEH";
            this.EHEH.UseVisualStyleBackColor = true;
            // 
            // EHGG
            // 
            this.EHGG.Location = new System.Drawing.Point(4, 22);
            this.EHGG.Name = "EHGG";
            this.EHGG.Size = new System.Drawing.Size(524, 206);
            this.EHGG.TabIndex = 3;
            this.EHGG.Text = "EHGG";
            this.EHGG.UseVisualStyleBackColor = true;
            // 
            // EHRD
            // 
            this.EHRD.Location = new System.Drawing.Point(4, 22);
            this.EHRD.Name = "EHRD";
            this.EHRD.Size = new System.Drawing.Size(524, 206);
            this.EHRD.TabIndex = 4;
            this.EHRD.Text = "EHRD";
            this.EHRD.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(557, 24);
            this.menuStrip.TabIndex = 19;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // DutchVACCATISGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 627);
            this.Controls.Add(this.ICAOTabControl);
            this.Controls.Add(this.outputGroupBox);
            this.Controls.Add(this.outputOptionsGroupBox);
            this.Controls.Add(this.metarInputGroupBox);
            this.Controls.Add(this.generateATISButton);
            this.Controls.Add(this.tlLevelGroupBox);
            this.Controls.Add(this.atisIndexGroupBox);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "DutchVACCATISGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dutch VACC ATIS Generator";
            this.atisIndexGroupBox.ResumeLayout(false);
            this.atisIndexGroupBox.PerformLayout();
            this.mainRunwaysGroupBox.ResumeLayout(false);
            this.mainLandingGroupBox.ResumeLayout(false);
            this.mainLandingGroupBox.PerformLayout();
            this.mainDepartureRunwayGroupBox.ResumeLayout(false);
            this.mainDepartureRunwayGroupBox.PerformLayout();
            this.secondaryLandingRunwayGroupBox.ResumeLayout(false);
            this.secondaryLandingRunwayGroupBox.PerformLayout();
            this.secondaryRunwaysGroupBox.ResumeLayout(false);
            this.secondaryDepartureRunwayGroupBox.ResumeLayout(false);
            this.secondaryDepartureRunwayGroupBox.PerformLayout();
            this.tlLevelGroupBox.ResumeLayout(false);
            this.metarInputGroupBox.ResumeLayout(false);
            this.metarInputGroupBox.PerformLayout();
            this.additionalOptionsGroupBox.ResumeLayout(false);
            this.additionalOptionsGroupBox.PerformLayout();
            this.outputOptionsGroupBox.ResumeLayout(false);
            this.outputOptionsGroupBox.PerformLayout();
            this.outputGroupBox.ResumeLayout(false);
            this.outputGroupBox.PerformLayout();
            this.ICAOTabControl.ResumeLayout(false);
            this.EHAM.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
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
        private System.Windows.Forms.GroupBox mainRunwaysGroupBox;
        private System.Windows.Forms.ComboBox mainLandingRunwayComboBox;
        private System.Windows.Forms.CheckBox secondaryLandingRunwayCheckBox;
        private System.Windows.Forms.CheckBox mainLandingRunwayCheckBox;
        private System.Windows.Forms.ComboBox secondaryLandingRunwayComboBox;
        private System.Windows.Forms.GroupBox secondaryLandingRunwayGroupBox;
        private System.Windows.Forms.GroupBox mainLandingGroupBox;
        private System.Windows.Forms.GroupBox secondaryRunwaysGroupBox;
        private System.Windows.Forms.GroupBox secondaryDepartureRunwayGroupBox;
        private System.Windows.Forms.ComboBox secondaryDepartureRunwayComboBox;
        private System.Windows.Forms.CheckBox secondaryDepartureRunwayCheckBox;
        private System.Windows.Forms.GroupBox mainDepartureRunwayGroupBox;
        private System.Windows.Forms.CheckBox mainDepartureRunwayCheckBox;
        private System.Windows.Forms.ComboBox mainDepartureRunwayComboBox;
        private System.Windows.Forms.GroupBox tlLevelGroupBox;
        private System.Windows.Forms.Label tlHeaderLabel;
        private System.Windows.Forms.Button generateATISButton;
        private System.Windows.Forms.Label tlOutLabel;
        private System.Windows.Forms.GroupBox metarInputGroupBox;
        private System.Windows.Forms.GroupBox additionalOptionsGroupBox;
        private System.Windows.Forms.CheckBox markTempCheckBox;
        private System.Windows.Forms.CheckBox appArrOnlyCheckBox;
        private System.Windows.Forms.CheckBox arrOnlyCheckBox;
        private System.Windows.Forms.CheckBox appOnlyCheckBox;
        private System.Windows.Forms.CheckBox copyOutputCheckBox;
        private System.Windows.Forms.GroupBox outputOptionsGroupBox;
        private System.Windows.Forms.GroupBox outputGroupBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.TabControl ICAOTabControl;
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
    }
}

