namespace DutchVACCATISGenerator.Forms
{
    partial class TerminalAerodromeForecastForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalAerodromeForecastForm));
            this.terminalAerodromeForecastRichTextBox = new System.Windows.Forms.RichTextBox();
            this.terminalAerodromeForecastGroupBox = new System.Windows.Forms.GroupBox();
            this.terminalAerodromeForecastBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.terminalAerodromeForecastGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // terminalAerodromeForecastRichTextBox
            // 
            this.terminalAerodromeForecastRichTextBox.Location = new System.Drawing.Point(7, 13);
            this.terminalAerodromeForecastRichTextBox.Name = "terminalAerodromeForecastRichTextBox";
            this.terminalAerodromeForecastRichTextBox.ReadOnly = true;
            this.terminalAerodromeForecastRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.terminalAerodromeForecastRichTextBox.Size = new System.Drawing.Size(558, 184);
            this.terminalAerodromeForecastRichTextBox.TabIndex = 0;
            this.terminalAerodromeForecastRichTextBox.Text = "";
            // 
            // terminalAerodromeForecastGroupBox
            // 
            this.terminalAerodromeForecastGroupBox.Controls.Add(this.terminalAerodromeForecastRichTextBox);
            this.terminalAerodromeForecastGroupBox.Location = new System.Drawing.Point(12, 6);
            this.terminalAerodromeForecastGroupBox.Name = "terminalAerodromeForecastGroupBox";
            this.terminalAerodromeForecastGroupBox.Size = new System.Drawing.Size(572, 204);
            this.terminalAerodromeForecastGroupBox.TabIndex = 1;
            this.terminalAerodromeForecastGroupBox.TabStop = false;
            // 
            // terminalAerodromeForecastBackgroundWorker
            // 
            this.terminalAerodromeForecastBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TerminalAerodromeForecastBackgroundWorker_DoWork);
            this.terminalAerodromeForecastBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TerminalAerodromeForecastBackgroundWorker_RunWorkerCompleted);
            // 
            // TerminalAerodromeForecastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 222);
            this.Controls.Add(this.terminalAerodromeForecastGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TerminalAerodromeForecastForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TAF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TerminalAerodromeForecastForm_FormClosing);
            this.terminalAerodromeForecastGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox terminalAerodromeForecastRichTextBox;
        private System.Windows.Forms.GroupBox terminalAerodromeForecastGroupBox;
        private System.ComponentModel.BackgroundWorker terminalAerodromeForecastBackgroundWorker;
    }
}