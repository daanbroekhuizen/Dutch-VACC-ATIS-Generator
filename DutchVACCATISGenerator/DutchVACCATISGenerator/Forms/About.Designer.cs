namespace DutchVACCATISGenerator.Forms
{
    partial class About
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
            this.contentGroupBox = new System.Windows.Forms.GroupBox();
            this.ApplicationVersionLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.DutchVACCLinkLabel = new System.Windows.Forms.LinkLabel();
            this.CreditLabel2 = new System.Windows.Forms.Label();
            this.CreditLabel1 = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.contentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentGroupBox
            // 
            this.contentGroupBox.Controls.Add(this.ApplicationVersionLabel);
            this.contentGroupBox.Controls.Add(this.VersionLabel);
            this.contentGroupBox.Controls.Add(this.DutchVACCLinkLabel);
            this.contentGroupBox.Controls.Add(this.CreditLabel2);
            this.contentGroupBox.Controls.Add(this.CreditLabel1);
            this.contentGroupBox.Controls.Add(this.DescriptionLabel);
            this.contentGroupBox.Controls.Add(this.TitleLabel);
            this.contentGroupBox.Location = new System.Drawing.Point(12, 6);
            this.contentGroupBox.Name = "contentGroupBox";
            this.contentGroupBox.Size = new System.Drawing.Size(207, 133);
            this.contentGroupBox.TabIndex = 0;
            this.contentGroupBox.TabStop = false;
            // 
            // ApplicationVersionLabel
            // 
            this.ApplicationVersionLabel.AutoSize = true;
            this.ApplicationVersionLabel.Location = new System.Drawing.Point(93, 65);
            this.ApplicationVersionLabel.Name = "ApplicationVersionLabel";
            this.ApplicationVersionLabel.Size = new System.Drawing.Size(64, 13);
            this.ApplicationVersionLabel.TabIndex = 6;
            this.ApplicationVersionLabel.Text = "placeHolder";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.Location = new System.Drawing.Point(44, 65);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(53, 13);
            this.VersionLabel.TabIndex = 5;
            this.VersionLabel.Text = "Version:";
            // 
            // DutchVACCLinkLabel
            // 
            this.DutchVACCLinkLabel.AutoSize = true;
            this.DutchVACCLinkLabel.Location = new System.Drawing.Point(36, 110);
            this.DutchVACCLinkLabel.Name = "DutchVACCLinkLabel";
            this.DutchVACCLinkLabel.Size = new System.Drawing.Size(132, 13);
            this.DutchVACCLinkLabel.TabIndex = 4;
            this.DutchVACCLinkLabel.TabStop = true;
            this.DutchVACCLinkLabel.Text = "http://www.dutchvacc.nl/";
            this.DutchVACCLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DutchVACCLinkLabel_LinkClicked);
            // 
            // CreditLabel2
            // 
            this.CreditLabel2.AutoSize = true;
            this.CreditLabel2.Location = new System.Drawing.Point(25, 97);
            this.CreditLabel2.Name = "CreditLabel2";
            this.CreditLabel2.Size = new System.Drawing.Size(156, 13);
            this.CreditLabel2.TabIndex = 3;
            this.CreditLabel2.Text = "Sounds by Robert van der Leije";
            // 
            // CreditLabel1
            // 
            this.CreditLabel1.AutoSize = true;
            this.CreditLabel1.Location = new System.Drawing.Point(28, 84);
            this.CreditLabel1.Name = "CreditLabel1";
            this.CreditLabel1.Size = new System.Drawing.Size(149, 13);
            this.CreditLabel1.TabIndex = 2;
            this.CreditLabel1.Text = "Created by Daan Broekhuizen";
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Location = new System.Drawing.Point(8, 33);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(189, 26);
            this.DescriptionLabel.TabIndex = 1;
            this.DescriptionLabel.Text = "Utility to generate a text ouput used to \r\nsetup a voice ATIS in EuroScope.";
            this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(19, 16);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(169, 13);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Dutch VACC ATIS Generator";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 151);
            this.Controls.Add(this.contentGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.contentGroupBox.ResumeLayout(false);
            this.contentGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox contentGroupBox;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.LinkLabel DutchVACCLinkLabel;
        private System.Windows.Forms.Label CreditLabel2;
        private System.Windows.Forms.Label CreditLabel1;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label ApplicationVersionLabel;
    }
}