namespace DutchVACCATISGenerator.Forms
{
    partial class RunwayForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunwayForm));
            this.schipholLandingDataGridView = new System.Windows.Forms.DataGridView();
            this.schipholLandingRWY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholLandingXWIND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholLandingTWIND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholLandingDREF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholLandingNPRF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholLandingCOMPLY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholDepartureDataGridView = new System.Windows.Forms.DataGridView();
            this.schipholDepartureRWY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholDepartureXWIND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholDepartureTWIND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholDepartureDREF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholDepartureNPRF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholDepartureCOMPLY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schipholLandingRunwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.schipholDepartureRunwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.frictionGroupBox = new System.Windows.Forms.GroupBox();
            this.frictionLabel = new System.Windows.Forms.Label();
            this.frictionComboBox = new System.Windows.Forms.ComboBox();
            this.runwayGroupBox = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.runwayInfoRWY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runwayInfoXWIND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runwayInfoTWIND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runwayInfoPRF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runwayInfoCOMPLY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.schipholLandingDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schipholDepartureDataGridView)).BeginInit();
            this.schipholLandingRunwaysGroupBox.SuspendLayout();
            this.schipholDepartureRunwaysGroupBox.SuspendLayout();
            this.frictionGroupBox.SuspendLayout();
            this.runwayGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // EHAMlandingRunwayInfoDataGridView
            // 
            this.schipholLandingDataGridView.AllowUserToAddRows = false;
            this.schipholLandingDataGridView.AllowUserToDeleteRows = false;
            this.schipholLandingDataGridView.AllowUserToResizeColumns = false;
            this.schipholLandingDataGridView.AllowUserToResizeRows = false;
            this.schipholLandingDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.schipholLandingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.schipholLandingDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.schipholLandingRWY,
            this.schipholLandingXWIND,
            this.schipholLandingTWIND,
            this.schipholLandingDREF,
            this.schipholLandingNPRF,
            this.schipholLandingCOMPLY});
            this.schipholLandingDataGridView.Location = new System.Drawing.Point(6, 19);
            this.schipholLandingDataGridView.MultiSelect = false;
            this.schipholLandingDataGridView.Name = "EHAMlandingRunwayInfoDataGridView";
            this.schipholLandingDataGridView.ReadOnly = true;
            this.schipholLandingDataGridView.RowHeadersVisible = false;
            this.schipholLandingDataGridView.RowHeadersWidth = 40;
            this.schipholLandingDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.schipholLandingDataGridView.ShowEditingIcon = false;
            this.schipholLandingDataGridView.Size = new System.Drawing.Size(362, 241);
            this.schipholLandingDataGridView.TabIndex = 0;
            // 
            // EHAMlandingRunwayInfoRWY
            // 
            this.schipholLandingRWY.HeaderText = "RWY";
            this.schipholLandingRWY.Name = "EHAMlandingRunwayInfoRWY";
            this.schipholLandingRWY.ReadOnly = true;
            this.schipholLandingRWY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholLandingRWY.Width = 60;
            // 
            // EHAMlandingRunwayInfoXWIND
            // 
            this.schipholLandingXWIND.HeaderText = "XWIND";
            this.schipholLandingXWIND.Name = "EHAMlandingRunwayInfoXWIND";
            this.schipholLandingXWIND.ReadOnly = true;
            this.schipholLandingXWIND.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholLandingXWIND.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.schipholLandingXWIND.Width = 60;
            // 
            // EHAMlandingRunwayInfoTWIND
            // 
            dataGridViewCellStyle1.NullValue = null;
            this.schipholLandingTWIND.DefaultCellStyle = dataGridViewCellStyle1;
            this.schipholLandingTWIND.HeaderText = "HWIND";
            this.schipholLandingTWIND.Name = "EHAMlandingRunwayInfoTWIND";
            this.schipholLandingTWIND.ReadOnly = true;
            this.schipholLandingTWIND.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholLandingTWIND.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.schipholLandingTWIND.Width = 60;
            // 
            // EHAMlandingRunwayInfoDREF
            // 
            this.schipholLandingDREF.HeaderText = "DPRF";
            this.schipholLandingDREF.Name = "EHAMlandingRunwayInfoDREF";
            this.schipholLandingDREF.ReadOnly = true;
            this.schipholLandingDREF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholLandingDREF.ToolTipText = "Day preference";
            this.schipholLandingDREF.Width = 60;
            // 
            // EHAMlandingRunwayInfoNPRF
            // 
            this.schipholLandingNPRF.HeaderText = "NPRF";
            this.schipholLandingNPRF.Name = "EHAMlandingRunwayInfoNPRF";
            this.schipholLandingNPRF.ReadOnly = true;
            this.schipholLandingNPRF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholLandingNPRF.ToolTipText = "Night preference";
            this.schipholLandingNPRF.Width = 60;
            // 
            // EHAMlandingRunwayInfoCOMPLY
            // 
            this.schipholLandingCOMPLY.HeaderText = "COMPLY";
            this.schipholLandingCOMPLY.Name = "EHAMlandingRunwayInfoCOMPLY";
            this.schipholLandingCOMPLY.ReadOnly = true;
            this.schipholLandingCOMPLY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholLandingCOMPLY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.schipholLandingCOMPLY.Width = 60;
            // 
            // EHAMdepartureRunwayInfoDataGridView
            // 
            this.schipholDepartureDataGridView.AllowUserToAddRows = false;
            this.schipholDepartureDataGridView.AllowUserToDeleteRows = false;
            this.schipholDepartureDataGridView.AllowUserToResizeColumns = false;
            this.schipholDepartureDataGridView.AllowUserToResizeRows = false;
            this.schipholDepartureDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.schipholDepartureDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.schipholDepartureDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.schipholDepartureRWY,
            this.schipholDepartureXWIND,
            this.schipholDepartureTWIND,
            this.schipholDepartureDREF,
            this.schipholDepartureNPRF,
            this.schipholDepartureCOMPLY});
            this.schipholDepartureDataGridView.Location = new System.Drawing.Point(6, 19);
            this.schipholDepartureDataGridView.MultiSelect = false;
            this.schipholDepartureDataGridView.Name = "EHAMdepartureRunwayInfoDataGridView";
            this.schipholDepartureDataGridView.ReadOnly = true;
            this.schipholDepartureDataGridView.RowHeadersVisible = false;
            this.schipholDepartureDataGridView.RowHeadersWidth = 40;
            this.schipholDepartureDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.schipholDepartureDataGridView.ShowEditingIcon = false;
            this.schipholDepartureDataGridView.Size = new System.Drawing.Size(362, 241);
            this.schipholDepartureDataGridView.TabIndex = 1;
            // 
            // EHAMdepartureRunwayInfoRWY
            // 
            this.schipholDepartureRWY.HeaderText = "RWY";
            this.schipholDepartureRWY.Name = "EHAMdepartureRunwayInfoRWY";
            this.schipholDepartureRWY.ReadOnly = true;
            this.schipholDepartureRWY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholDepartureRWY.Width = 60;
            // 
            // EHAMdepartureRunwayInfoXWIND
            // 
            this.schipholDepartureXWIND.HeaderText = "XWIND";
            this.schipholDepartureXWIND.Name = "EHAMdepartureRunwayInfoXWIND";
            this.schipholDepartureXWIND.ReadOnly = true;
            this.schipholDepartureXWIND.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholDepartureXWIND.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.schipholDepartureXWIND.Width = 60;
            // 
            // EHAMdepartureRunwayInfoTWIND
            // 
            this.schipholDepartureTWIND.HeaderText = "HWIND";
            this.schipholDepartureTWIND.Name = "EHAMdepartureRunwayInfoTWIND";
            this.schipholDepartureTWIND.ReadOnly = true;
            this.schipholDepartureTWIND.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholDepartureTWIND.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.schipholDepartureTWIND.Width = 60;
            // 
            // EHAMdepartureRunwayInfoDREF
            // 
            dataGridViewCellStyle2.NullValue = null;
            this.schipholDepartureDREF.DefaultCellStyle = dataGridViewCellStyle2;
            this.schipholDepartureDREF.HeaderText = "DPRF";
            this.schipholDepartureDREF.Name = "EHAMdepartureRunwayInfoDREF";
            this.schipholDepartureDREF.ReadOnly = true;
            this.schipholDepartureDREF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholDepartureDREF.ToolTipText = "Day preference";
            this.schipholDepartureDREF.Width = 60;
            // 
            // EHAMdepartureRunwayInfoNPRF
            // 
            this.schipholDepartureNPRF.HeaderText = "NPRF";
            this.schipholDepartureNPRF.Name = "EHAMdepartureRunwayInfoNPRF";
            this.schipholDepartureNPRF.ReadOnly = true;
            this.schipholDepartureNPRF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholDepartureNPRF.ToolTipText = "Night preference";
            this.schipholDepartureNPRF.Width = 60;
            // 
            // EHAMdepartureRunwayInfoCOMPLY
            // 
            this.schipholDepartureCOMPLY.HeaderText = "COMPLY";
            this.schipholDepartureCOMPLY.Name = "EHAMdepartureRunwayInfoCOMPLY";
            this.schipholDepartureCOMPLY.ReadOnly = true;
            this.schipholDepartureCOMPLY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.schipholDepartureCOMPLY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.schipholDepartureCOMPLY.Width = 60;
            // 
            // EHAMLandingRunwaysGroupBox
            // 
            this.schipholLandingRunwaysGroupBox.Controls.Add(this.schipholLandingDataGridView);
            this.schipholLandingRunwaysGroupBox.Location = new System.Drawing.Point(12, 56);
            this.schipholLandingRunwaysGroupBox.Name = "EHAMLandingRunwaysGroupBox";
            this.schipholLandingRunwaysGroupBox.Size = new System.Drawing.Size(374, 266);
            this.schipholLandingRunwaysGroupBox.TabIndex = 2;
            this.schipholLandingRunwaysGroupBox.TabStop = false;
            this.schipholLandingRunwaysGroupBox.Text = "Landing Runways";
            // 
            // EHAMdepartureRunwaysGroupBox
            // 
            this.schipholDepartureRunwaysGroupBox.Controls.Add(this.schipholDepartureDataGridView);
            this.schipholDepartureRunwaysGroupBox.Location = new System.Drawing.Point(12, 328);
            this.schipholDepartureRunwaysGroupBox.Name = "EHAMdepartureRunwaysGroupBox";
            this.schipholDepartureRunwaysGroupBox.Size = new System.Drawing.Size(374, 266);
            this.schipholDepartureRunwaysGroupBox.TabIndex = 3;
            this.schipholDepartureRunwaysGroupBox.TabStop = false;
            this.schipholDepartureRunwaysGroupBox.Text = "Departure Runways";
            // 
            // runwayFrictionGroupBox
            // 
            this.frictionGroupBox.Controls.Add(this.frictionLabel);
            this.frictionGroupBox.Controls.Add(this.frictionComboBox);
            this.frictionGroupBox.Location = new System.Drawing.Point(12, 6);
            this.frictionGroupBox.Name = "runwayFrictionGroupBox";
            this.frictionGroupBox.Size = new System.Drawing.Size(374, 44);
            this.frictionGroupBox.TabIndex = 4;
            this.frictionGroupBox.TabStop = false;
            this.frictionGroupBox.Text = "Runway Friction";
            // 
            // estimatedRwyFrictionLabel
            // 
            this.frictionLabel.AutoSize = true;
            this.frictionLabel.Location = new System.Drawing.Point(6, 20);
            this.frictionLabel.Name = "estimatedRwyFrictionLabel";
            this.frictionLabel.Size = new System.Drawing.Size(127, 13);
            this.frictionLabel.TabIndex = 1;
            this.frictionLabel.Text = "Estimated runway friction:";
            // 
            // frictionComboBox
            // 
            this.frictionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frictionComboBox.FormattingEnabled = true;
            this.frictionComboBox.Items.AddRange(new object[] {
            "Good",
            "Medium to good",
            "Medium",
            "Medium to poor",
            "Poor"});
            this.frictionComboBox.Location = new System.Drawing.Point(214, 17);
            this.frictionComboBox.Name = "frictionComboBox";
            this.frictionComboBox.Size = new System.Drawing.Size(153, 21);
            this.frictionComboBox.TabIndex = 0;
            this.frictionComboBox.SelectedIndexChanged += new System.EventHandler(this.FrictionComboBox_SelectedIndexChanged);
            // 
            // runwayGroupBox
            // 
            this.runwayGroupBox.Controls.Add(this.dataGridView);
            this.runwayGroupBox.Location = new System.Drawing.Point(12, 56);
            this.runwayGroupBox.Name = "runwayGroupBox";
            this.runwayGroupBox.Size = new System.Drawing.Size(374, 266);
            this.runwayGroupBox.TabIndex = 5;
            this.runwayGroupBox.TabStop = false;
            this.runwayGroupBox.Text = "Runways";
            // 
            // runwayInfoDataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.runwayInfoRWY,
            this.runwayInfoXWIND,
            this.runwayInfoTWIND,
            this.runwayInfoPRF,
            this.runwayInfoCOMPLY});
            this.dataGridView.Location = new System.Drawing.Point(6, 19);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "runwayInfoDataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 40;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.Size = new System.Drawing.Size(302, 241);
            this.dataGridView.TabIndex = 1;
            // 
            // runwayInfoRWY
            // 
            this.runwayInfoRWY.HeaderText = "RWY";
            this.runwayInfoRWY.Name = "runwayInfoRWY";
            this.runwayInfoRWY.ReadOnly = true;
            this.runwayInfoRWY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.runwayInfoRWY.Width = 60;
            // 
            // runwayInfoXWIND
            // 
            this.runwayInfoXWIND.HeaderText = "XWIND";
            this.runwayInfoXWIND.Name = "runwayInfoXWIND";
            this.runwayInfoXWIND.ReadOnly = true;
            this.runwayInfoXWIND.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.runwayInfoXWIND.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.runwayInfoXWIND.Width = 60;
            // 
            // runwayInfoTWIND
            // 
            this.runwayInfoTWIND.HeaderText = "HWIND";
            this.runwayInfoTWIND.Name = "runwayInfoTWIND";
            this.runwayInfoTWIND.ReadOnly = true;
            this.runwayInfoTWIND.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.runwayInfoTWIND.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.runwayInfoTWIND.Width = 60;
            // 
            // runwayInfoPRF
            // 
            this.runwayInfoPRF.HeaderText = "PRF";
            this.runwayInfoPRF.Name = "runwayInfoPRF";
            this.runwayInfoPRF.ReadOnly = true;
            this.runwayInfoPRF.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.runwayInfoPRF.Width = 60;
            // 
            // runwayInfoCOMPLY
            // 
            this.runwayInfoCOMPLY.HeaderText = "COMPLY";
            this.runwayInfoCOMPLY.Name = "runwayInfoCOMPLY";
            this.runwayInfoCOMPLY.ReadOnly = true;
            this.runwayInfoCOMPLY.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.runwayInfoCOMPLY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.runwayInfoCOMPLY.Width = 60;
            // 
            // RunwayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 606);
            this.Controls.Add(this.frictionGroupBox);
            this.Controls.Add(this.schipholDepartureRunwaysGroupBox);
            this.Controls.Add(this.schipholLandingRunwaysGroupBox);
            this.Controls.Add(this.runwayGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RunwayForm";
            this.ShowInTaskbar = false;
            this.Text = "Runway criteria";
            this.Load += new System.EventHandler(this.Runway_Load);
            ((System.ComponentModel.ISupportInitialize)(this.schipholLandingDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schipholDepartureDataGridView)).EndInit();
            this.schipholLandingRunwaysGroupBox.ResumeLayout(false);
            this.schipholDepartureRunwaysGroupBox.ResumeLayout(false);
            this.frictionGroupBox.ResumeLayout(false);
            this.frictionGroupBox.PerformLayout();
            this.runwayGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView schipholLandingDataGridView;
        private System.Windows.Forms.DataGridView schipholDepartureDataGridView;
        private System.Windows.Forms.GroupBox schipholLandingRunwaysGroupBox;
        private System.Windows.Forms.GroupBox schipholDepartureRunwaysGroupBox;
        private System.Windows.Forms.GroupBox frictionGroupBox;
        private System.Windows.Forms.ComboBox frictionComboBox;
        private System.Windows.Forms.Label frictionLabel;
        private System.Windows.Forms.GroupBox runwayGroupBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn runwayInfoRWY;
        private System.Windows.Forms.DataGridViewTextBoxColumn runwayInfoXWIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn runwayInfoTWIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn runwayInfoPRF;
        private System.Windows.Forms.DataGridViewTextBoxColumn runwayInfoCOMPLY;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholDepartureRWY;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholDepartureXWIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholDepartureTWIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholDepartureDREF;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholDepartureNPRF;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholDepartureCOMPLY;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholLandingRWY;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholLandingXWIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholLandingTWIND;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholLandingDREF;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholLandingNPRF;
        private System.Windows.Forms.DataGridViewTextBoxColumn schipholLandingCOMPLY;
    }
}