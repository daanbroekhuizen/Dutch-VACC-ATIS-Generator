namespace DutchVACCATISGenerator
{
    partial class TrendSelection
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
            this.selectButton = new System.Windows.Forms.Button();
            this.trendComboBox = new System.Windows.Forms.ComboBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.aboutGroupBox = new System.Windows.Forms.GroupBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.aboutGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectButton
            // 
            this.selectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectButton.Location = new System.Drawing.Point(11, 72);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(78, 26);
            this.selectButton.TabIndex = 0;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            // 
            // trendComboBox
            // 
            this.trendComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trendComboBox.FormattingEnabled = true;
            this.trendComboBox.Items.AddRange(new object[] {
            "TEMPO",
            "BECMG"});
            this.trendComboBox.Location = new System.Drawing.Point(11, 45);
            this.trendComboBox.Name = "trendComboBox";
            this.trendComboBox.Size = new System.Drawing.Size(171, 21);
            this.trendComboBox.TabIndex = 1;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(8, 16);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(174, 26);
            this.errorLabel.TabIndex = 2;
            this.errorLabel.Text = "Unable to determine trend type, \r\nplease select trend type to process:\r\n";
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aboutGroupBox
            // 
            this.aboutGroupBox.Controls.Add(this.closeButton);
            this.aboutGroupBox.Controls.Add(this.errorLabel);
            this.aboutGroupBox.Controls.Add(this.selectButton);
            this.aboutGroupBox.Controls.Add(this.trendComboBox);
            this.aboutGroupBox.Location = new System.Drawing.Point(13, 7);
            this.aboutGroupBox.Name = "aboutGroupBox";
            this.aboutGroupBox.Size = new System.Drawing.Size(190, 107);
            this.aboutGroupBox.TabIndex = 3;
            this.aboutGroupBox.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(104, 72);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(78, 26);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // TrendSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 127);
            this.Controls.Add(this.aboutGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrendSelection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ATIS Trend Type Selection";
            this.aboutGroupBox.ResumeLayout(false);
            this.aboutGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button selectButton;
        public System.Windows.Forms.ComboBox trendComboBox;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.GroupBox aboutGroupBox;
        private System.Windows.Forms.Button closeButton;
    }
}