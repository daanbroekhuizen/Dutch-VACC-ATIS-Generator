namespace DutchVACCATISGenerator
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
            this.applicationVersionLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.dutchVACCLinkLabel = new System.Windows.Forms.LinkLabel();
            this.creditLabel2 = new System.Windows.Forms.Label();
            this.creditLabel1 = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.contentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentGroupBox
            // 
            this.contentGroupBox.Controls.Add(this.applicationVersionLabel);
            this.contentGroupBox.Controls.Add(this.versionLabel);
            this.contentGroupBox.Controls.Add(this.dutchVACCLinkLabel);
            this.contentGroupBox.Controls.Add(this.creditLabel2);
            this.contentGroupBox.Controls.Add(this.creditLabel1);
            this.contentGroupBox.Controls.Add(this.descriptionLabel);
            this.contentGroupBox.Controls.Add(this.titleLabel);
            this.contentGroupBox.Location = new System.Drawing.Point(12, 6);
            this.contentGroupBox.Name = "contentGroupBox";
            this.contentGroupBox.Size = new System.Drawing.Size(207, 133);
            this.contentGroupBox.TabIndex = 0;
            this.contentGroupBox.TabStop = false;
            // 
            // applicationVersionLabel
            // 
            this.applicationVersionLabel.AutoSize = true;
            this.applicationVersionLabel.Location = new System.Drawing.Point(93, 65);
            this.applicationVersionLabel.Name = "applicationVersionLabel";
            this.applicationVersionLabel.Size = new System.Drawing.Size(64, 13);
            this.applicationVersionLabel.TabIndex = 6;
            this.applicationVersionLabel.Text = "placeHolder";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(44, 65);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(53, 13);
            this.versionLabel.TabIndex = 5;
            this.versionLabel.Text = "Version:";
            // 
            // dutchVACCLinkLabel
            // 
            this.dutchVACCLinkLabel.AutoSize = true;
            this.dutchVACCLinkLabel.Location = new System.Drawing.Point(36, 110);
            this.dutchVACCLinkLabel.Name = "dutchVACCLinkLabel";
            this.dutchVACCLinkLabel.Size = new System.Drawing.Size(132, 13);
            this.dutchVACCLinkLabel.TabIndex = 4;
            this.dutchVACCLinkLabel.TabStop = true;
            this.dutchVACCLinkLabel.Text = "http://www.dutchvacc.nl/";
            this.dutchVACCLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.dutchVACCLinkLabel_LinkClicked);
            // 
            // creditLabel2
            // 
            this.creditLabel2.AutoSize = true;
            this.creditLabel2.Location = new System.Drawing.Point(25, 97);
            this.creditLabel2.Name = "creditLabel2";
            this.creditLabel2.Size = new System.Drawing.Size(156, 13);
            this.creditLabel2.TabIndex = 3;
            this.creditLabel2.Text = "Sounds by Robert van der Leije";
            // 
            // creditLabel1
            // 
            this.creditLabel1.AutoSize = true;
            this.creditLabel1.Location = new System.Drawing.Point(28, 84);
            this.creditLabel1.Name = "creditLabel1";
            this.creditLabel1.Size = new System.Drawing.Size(149, 13);
            this.creditLabel1.TabIndex = 2;
            this.creditLabel1.Text = "Created by Daan Broekhuizen";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(8, 33);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(189, 26);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "Utility to generate a text ouput used to \r\nsetup a voice ATIS in EuroScope.";
            this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(19, 16);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(169, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Dutch VACC ATIS Generator";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.LinkLabel dutchVACCLinkLabel;
        private System.Windows.Forms.Label creditLabel2;
        private System.Windows.Forms.Label creditLabel1;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label applicationVersionLabel;
    }
}