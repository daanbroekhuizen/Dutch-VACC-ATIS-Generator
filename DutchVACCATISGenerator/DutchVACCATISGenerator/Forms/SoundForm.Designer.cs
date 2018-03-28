namespace DutchVACCATISGenerator
{
    partial class SoundForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundForm));
            this.playATISButton = new System.Windows.Forms.Button();
            this.ATISFileTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.browseButton = new System.Windows.Forms.Button();
            this.ATISFileGroupBox = new System.Windows.Forms.GroupBox();
            this.buildATISButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buildGroupBox = new System.Windows.Forms.GroupBox();
            this.playGroupBox = new System.Windows.Forms.GroupBox();
            this.ATISFileGroupBox.SuspendLayout();
            this.buildGroupBox.SuspendLayout();
            this.playGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // playATISButton
            // 
            this.playATISButton.Location = new System.Drawing.Point(6, 19);
            this.playATISButton.Name = "playATISButton";
            this.playATISButton.Size = new System.Drawing.Size(104, 34);
            this.playATISButton.TabIndex = 0;
            this.playATISButton.Text = "Play ATIS";
            this.playATISButton.UseVisualStyleBackColor = true;
            this.playATISButton.Click += new System.EventHandler(this.PlayATISButton_Click);
            // 
            // ATISFileTextBox
            // 
            this.ATISFileTextBox.Location = new System.Drawing.Point(6, 19);
            this.ATISFileTextBox.Multiline = true;
            this.ATISFileTextBox.Name = "ATISFileTextBox";
            this.ATISFileTextBox.Size = new System.Drawing.Size(444, 23);
            this.ATISFileTextBox.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "atiseham.txt";
            this.openFileDialog.Filter = "Text Documents|*.txt";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(456, 18);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 25);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // ATISFileGroupBox
            // 
            this.ATISFileGroupBox.Controls.Add(this.ATISFileTextBox);
            this.ATISFileGroupBox.Controls.Add(this.browseButton);
            this.ATISFileGroupBox.Location = new System.Drawing.Point(12, 6);
            this.ATISFileGroupBox.Name = "ATISFileGroupBox";
            this.ATISFileGroupBox.Size = new System.Drawing.Size(537, 55);
            this.ATISFileGroupBox.TabIndex = 3;
            this.ATISFileGroupBox.TabStop = false;
            // 
            // buildATISButton
            // 
            this.buildATISButton.Enabled = false;
            this.buildATISButton.Location = new System.Drawing.Point(6, 19);
            this.buildATISButton.Name = "buildATISButton";
            this.buildATISButton.Size = new System.Drawing.Size(105, 34);
            this.buildATISButton.TabIndex = 4;
            this.buildATISButton.Text = "Build ATIS.wav";
            this.buildATISButton.UseVisualStyleBackColor = true;
            this.buildATISButton.Click += new System.EventHandler(this.BuildATISButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(117, 25);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(292, 22);
            this.progressBar.TabIndex = 5;
            // 
            // buildGroupBox
            // 
            this.buildGroupBox.Controls.Add(this.buildATISButton);
            this.buildGroupBox.Controls.Add(this.progressBar);
            this.buildGroupBox.Location = new System.Drawing.Point(12, 67);
            this.buildGroupBox.Name = "buildGroupBox";
            this.buildGroupBox.Size = new System.Drawing.Size(415, 66);
            this.buildGroupBox.TabIndex = 6;
            this.buildGroupBox.TabStop = false;
            // 
            // playGroupBox
            // 
            this.playGroupBox.Controls.Add(this.playATISButton);
            this.playGroupBox.Location = new System.Drawing.Point(433, 67);
            this.playGroupBox.Name = "playGroupBox";
            this.playGroupBox.Size = new System.Drawing.Size(116, 66);
            this.playGroupBox.TabIndex = 7;
            this.playGroupBox.TabStop = false;
            // 
            // SoundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 145);
            this.ControlBox = false;
            this.Controls.Add(this.playGroupBox);
            this.Controls.Add(this.buildGroupBox);
            this.Controls.Add(this.ATISFileGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SoundForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sound Dialog";
            this.Load += new System.EventHandler(this.Sound_Load);
            this.ATISFileGroupBox.ResumeLayout(false);
            this.ATISFileGroupBox.PerformLayout();
            this.buildGroupBox.ResumeLayout(false);
            this.playGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playATISButton;
        private System.Windows.Forms.TextBox ATISFileTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.GroupBox ATISFileGroupBox;
        private System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.Button buildATISButton;
        private System.Windows.Forms.GroupBox buildGroupBox;
        private System.Windows.Forms.GroupBox playGroupBox;
    }
}