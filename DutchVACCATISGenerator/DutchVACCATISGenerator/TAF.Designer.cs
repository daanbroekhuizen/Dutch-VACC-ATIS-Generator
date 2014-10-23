namespace DutchVACCATISGenerator
{
    partial class TAF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TAF));
            this.TAFRichTextBox = new System.Windows.Forms.RichTextBox();
            this.TAFGroupBox = new System.Windows.Forms.GroupBox();
            this.TAFGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TAFRichTextBox
            // 
            this.TAFRichTextBox.Location = new System.Drawing.Point(7, 13);
            this.TAFRichTextBox.Name = "TAFRichTextBox";
            this.TAFRichTextBox.ReadOnly = true;
            this.TAFRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.TAFRichTextBox.Size = new System.Drawing.Size(558, 184);
            this.TAFRichTextBox.TabIndex = 0;
            this.TAFRichTextBox.Text = "";
            // 
            // TAFGroupBox
            // 
            this.TAFGroupBox.Controls.Add(this.TAFRichTextBox);
            this.TAFGroupBox.Location = new System.Drawing.Point(12, 6);
            this.TAFGroupBox.Name = "TAFGroupBox";
            this.TAFGroupBox.Size = new System.Drawing.Size(572, 204);
            this.TAFGroupBox.TabIndex = 1;
            this.TAFGroupBox.TabStop = false;
            // 
            // TAF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 222);
            this.Controls.Add(this.TAFGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TAF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TAF Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TAF_FormClosing);
            this.TAFGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox TAFRichTextBox;
        private System.Windows.Forms.GroupBox TAFGroupBox;

    }
}