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
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.RWY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XWIND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TWIND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DREF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NPRF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMPLY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nextATISLetterButton = new System.Windows.Forms.Button();
            this.previousATISLetterButton = new System.Windows.Forms.Button();
            this.atisLetterLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mainLandingRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.mainLandingRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.mainDepartureRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.mainDepartureRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.secondaryLandingRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.secondaryLandingRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.secondaryDepartureRunwayComboBox = new System.Windows.Forms.ComboBox();
            this.secondaryDepartureRunwayCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.generateATISButton = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // getMetarButton
            // 
            this.getMetarButton.Location = new System.Drawing.Point(50, 19);
            this.getMetarButton.Name = "getMetarButton";
            this.getMetarButton.Size = new System.Drawing.Size(75, 20);
            this.getMetarButton.TabIndex = 0;
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
            this.metarTextBox.Size = new System.Drawing.Size(415, 32);
            this.metarTextBox.TabIndex = 2;
            // 
            // lastLabel
            // 
            this.lastLabel.AutoSize = true;
            this.lastLabel.Location = new System.Drawing.Point(6, 80);
            this.lastLabel.Name = "lastLabel";
            this.lastLabel.Size = new System.Drawing.Size(65, 13);
            this.lastLabel.TabIndex = 3;
            this.lastLabel.Text = "place holder";
            // 
            // processMetarButton
            // 
            this.processMetarButton.Location = new System.Drawing.Point(427, 44);
            this.processMetarButton.Name = "processMetarButton";
            this.processMetarButton.Size = new System.Drawing.Size(83, 83);
            this.processMetarButton.TabIndex = 4;
            this.processMetarButton.Text = "Process Metar";
            this.processMetarButton.UseVisualStyleBackColor = true;
            this.processMetarButton.Click += new System.EventHandler(this.processMetarButton_Click);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(969, 397);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(100, 20);
            this.outputTextBox.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RWY,
            this.XWIND,
            this.TWIND,
            this.DREF,
            this.NPRF,
            this.COMPLY});
            this.dataGridView1.Location = new System.Drawing.Point(692, 516);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(602, 336);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.Visible = false;
            // 
            // RWY
            // 
            this.RWY.Frozen = true;
            this.RWY.HeaderText = "RWY";
            this.RWY.Name = "RWY";
            this.RWY.ReadOnly = true;
            this.RWY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RWY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // XWIND
            // 
            this.XWIND.Frozen = true;
            this.XWIND.HeaderText = "XWIND";
            this.XWIND.Name = "XWIND";
            this.XWIND.ReadOnly = true;
            this.XWIND.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.XWIND.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TWIND
            // 
            this.TWIND.Frozen = true;
            this.TWIND.HeaderText = "TWIND";
            this.TWIND.Name = "TWIND";
            this.TWIND.ReadOnly = true;
            this.TWIND.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TWIND.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // DREF
            // 
            this.DREF.Frozen = true;
            this.DREF.HeaderText = "DREF";
            this.DREF.Name = "DREF";
            this.DREF.ReadOnly = true;
            this.DREF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DREF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NPRF
            // 
            this.NPRF.Frozen = true;
            this.NPRF.HeaderText = "NPRF";
            this.NPRF.Name = "NPRF";
            this.NPRF.ReadOnly = true;
            this.NPRF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NPRF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // COMPLY
            // 
            this.COMPLY.Frozen = true;
            this.COMPLY.HeaderText = "COMPLY";
            this.COMPLY.Name = "COMPLY";
            this.COMPLY.ReadOnly = true;
            this.COMPLY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.COMPLY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nextATISLetterButton);
            this.groupBox1.Controls.Add(this.previousATISLetterButton);
            this.groupBox1.Controls.Add(this.atisLetterLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 355);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 112);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // nextATISLetterButton
            // 
            this.nextATISLetterButton.Location = new System.Drawing.Point(68, 63);
            this.nextATISLetterButton.Name = "nextATISLetterButton";
            this.nextATISLetterButton.Size = new System.Drawing.Size(40, 40);
            this.nextATISLetterButton.TabIndex = 1;
            this.nextATISLetterButton.Text = ">";
            this.nextATISLetterButton.UseVisualStyleBackColor = true;
            this.nextATISLetterButton.Click += new System.EventHandler(this.nextATISLetterButton_Click);
            // 
            // previousATISLetterButton
            // 
            this.previousATISLetterButton.Location = new System.Drawing.Point(7, 63);
            this.previousATISLetterButton.Name = "previousATISLetterButton";
            this.previousATISLetterButton.Size = new System.Drawing.Size(40, 40);
            this.previousATISLetterButton.TabIndex = 2;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Location = new System.Drawing.Point(12, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(353, 96);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Main Runway(s)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mainLandingRunwayCheckBox);
            this.groupBox3.Controls.Add(this.mainLandingRunwayComboBox);
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(169, 65);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // mainLandingRunwayCheckBox
            // 
            this.mainLandingRunwayCheckBox.AutoSize = true;
            this.mainLandingRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.mainLandingRunwayCheckBox.Name = "mainLandingRunwayCheckBox";
            this.mainLandingRunwayCheckBox.Size = new System.Drawing.Size(123, 17);
            this.mainLandingRunwayCheckBox.TabIndex = 0;
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
            "18L",
            "18C",
            "18R",
            "22",
            "24",
            "27",
            "36L",
            "36C",
            "36R"});
            this.mainLandingRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.mainLandingRunwayComboBox.Name = "mainLandingRunwayComboBox";
            this.mainLandingRunwayComboBox.Size = new System.Drawing.Size(115, 21);
            this.mainLandingRunwayComboBox.TabIndex = 2;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.mainDepartureRunwayCheckBox);
            this.groupBox7.Controls.Add(this.mainDepartureRunwayComboBox);
            this.groupBox7.Location = new System.Drawing.Point(177, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(169, 65);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            // 
            // mainDepartureRunwayCheckBox
            // 
            this.mainDepartureRunwayCheckBox.AutoSize = true;
            this.mainDepartureRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.mainDepartureRunwayCheckBox.Name = "mainDepartureRunwayCheckBox";
            this.mainDepartureRunwayCheckBox.Size = new System.Drawing.Size(134, 17);
            this.mainDepartureRunwayCheckBox.TabIndex = 0;
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
            "18R",
            "22",
            "24",
            "27",
            "36L",
            "36C",
            "36R"});
            this.mainDepartureRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.mainDepartureRunwayComboBox.Name = "mainDepartureRunwayComboBox";
            this.mainDepartureRunwayComboBox.Size = new System.Drawing.Size(115, 21);
            this.mainDepartureRunwayComboBox.TabIndex = 2;
            // 
            // secondaryLandingRunwayCheckBox
            // 
            this.secondaryLandingRunwayCheckBox.AutoSize = true;
            this.secondaryLandingRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.secondaryLandingRunwayCheckBox.Name = "secondaryLandingRunwayCheckBox";
            this.secondaryLandingRunwayCheckBox.Size = new System.Drawing.Size(151, 17);
            this.secondaryLandingRunwayCheckBox.TabIndex = 1;
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
            "18L",
            "18C",
            "18R",
            "22",
            "24",
            "27",
            "36L",
            "36C",
            "36R"});
            this.secondaryLandingRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.secondaryLandingRunwayComboBox.Name = "secondaryLandingRunwayComboBox";
            this.secondaryLandingRunwayComboBox.Size = new System.Drawing.Size(115, 21);
            this.secondaryLandingRunwayComboBox.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.secondaryLandingRunwayComboBox);
            this.groupBox4.Controls.Add(this.secondaryLandingRunwayCheckBox);
            this.groupBox4.Location = new System.Drawing.Point(6, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(169, 65);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(12, 253);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(353, 96);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Secondary Runway(s)";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.secondaryDepartureRunwayComboBox);
            this.groupBox6.Controls.Add(this.secondaryDepartureRunwayCheckBox);
            this.groupBox6.Location = new System.Drawing.Point(177, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(169, 65);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
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
            "18R",
            "22",
            "24",
            "27",
            "36L",
            "36C",
            "36R"});
            this.secondaryDepartureRunwayComboBox.Location = new System.Drawing.Point(6, 36);
            this.secondaryDepartureRunwayComboBox.Name = "secondaryDepartureRunwayComboBox";
            this.secondaryDepartureRunwayComboBox.Size = new System.Drawing.Size(115, 21);
            this.secondaryDepartureRunwayComboBox.TabIndex = 3;
            // 
            // secondaryDepartureRunwayCheckBox
            // 
            this.secondaryDepartureRunwayCheckBox.AutoSize = true;
            this.secondaryDepartureRunwayCheckBox.Location = new System.Drawing.Point(6, 13);
            this.secondaryDepartureRunwayCheckBox.Name = "secondaryDepartureRunwayCheckBox";
            this.secondaryDepartureRunwayCheckBox.Size = new System.Drawing.Size(162, 17);
            this.secondaryDepartureRunwayCheckBox.TabIndex = 1;
            this.secondaryDepartureRunwayCheckBox.Text = "Secondary departure runway";
            this.secondaryDepartureRunwayCheckBox.UseVisualStyleBackColor = true;
            this.secondaryDepartureRunwayCheckBox.CheckedChanged += new System.EventHandler(this.secondaryDepartureRunwayCheckBox_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Location = new System.Drawing.Point(134, 355);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(116, 112);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "..";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "TL:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // generateATISButton
            // 
            this.generateATISButton.Enabled = false;
            this.generateATISButton.Location = new System.Drawing.Point(256, 361);
            this.generateATISButton.Name = "generateATISButton";
            this.generateATISButton.Size = new System.Drawing.Size(109, 106);
            this.generateATISButton.TabIndex = 12;
            this.generateATISButton.Text = "Generate ATIS";
            this.generateATISButton.UseVisualStyleBackColor = true;
            this.generateATISButton.Click += new System.EventHandler(this.generateATISButton_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.icaoTextBox);
            this.groupBox9.Controls.Add(this.getMetarButton);
            this.groupBox9.Controls.Add(this.metarTextBox);
            this.groupBox9.Controls.Add(this.lastLabel);
            this.groupBox9.Controls.Add(this.processMetarButton);
            this.groupBox9.Location = new System.Drawing.Point(12, 12);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(516, 133);
            this.groupBox9.TabIndex = 13;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Metar";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.checkBox5);
            this.groupBox10.Controls.Add(this.checkBox6);
            this.groupBox10.Controls.Add(this.checkBox7);
            this.groupBox10.Controls.Add(this.checkBox8);
            this.groupBox10.Controls.Add(this.checkBox3);
            this.groupBox10.Controls.Add(this.checkBox4);
            this.groupBox10.Controls.Add(this.checkBox2);
            this.groupBox10.Controls.Add(this.checkBox1);
            this.groupBox10.Location = new System.Drawing.Point(371, 151);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(157, 198);
            this.groupBox10.TabIndex = 14;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Additional Options";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "APP callsign only";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 42);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(109, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "ARR callsign only";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 88);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(80, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Visible = false;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 65);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(142, 17);
            this.checkBox4.TabIndex = 2;
            this.checkBox4.Text = "APP + ARR callsign only";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(6, 180);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(80, 17);
            this.checkBox5.TabIndex = 7;
            this.checkBox5.Text = "checkBox5";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.Visible = false;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(6, 157);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(80, 17);
            this.checkBox6.TabIndex = 6;
            this.checkBox6.Text = "checkBox6";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.Visible = false;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(6, 134);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(80, 17);
            this.checkBox7.TabIndex = 5;
            this.checkBox7.Text = "checkBox7";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.Visible = false;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(6, 111);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(80, 17);
            this.checkBox8.TabIndex = 4;
            this.checkBox8.Text = "checkBox8";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.Visible = false;
            // 
            // DutchVACCATISGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 499);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.generateATISButton);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.outputTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DutchVACCATISGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dutch VACC ATIS Generator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
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
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RWY;
        private System.Windows.Forms.DataGridViewTextBoxColumn XWIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn TWIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn DREF;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPRF;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMPLY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button previousATISLetterButton;
        private System.Windows.Forms.Button nextATISLetterButton;
        private System.Windows.Forms.Label atisLetterLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox mainLandingRunwayComboBox;
        private System.Windows.Forms.CheckBox secondaryLandingRunwayCheckBox;
        private System.Windows.Forms.CheckBox mainLandingRunwayCheckBox;
        private System.Windows.Forms.ComboBox secondaryLandingRunwayComboBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox secondaryDepartureRunwayComboBox;
        private System.Windows.Forms.CheckBox secondaryDepartureRunwayCheckBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox mainDepartureRunwayCheckBox;
        private System.Windows.Forms.ComboBox mainDepartureRunwayComboBox;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button generateATISButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

