namespace DutchVACCATISGenerator
{
    partial class Sound
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sound));
            this.playATISButton = new System.Windows.Forms.Button();
            this.atisehamFileTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.browseButton = new System.Windows.Forms.Button();
            this.atisehamFileGroupBox = new System.Windows.Forms.GroupBox();
            this.buildATISButton = new System.Windows.Forms.Button();
            this.buildATISbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buildGroupBox = new System.Windows.Forms.GroupBox();
            this.playGroupBox = new System.Windows.Forms.GroupBox();
            this.atisehamFileGroupBox.SuspendLayout();
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
            this.playATISButton.Click += new System.EventHandler(this.playATISButton_Click);
            // 
            // atisehamFileTextBox
            // 
            this.atisehamFileTextBox.Location = new System.Drawing.Point(6, 19);
            this.atisehamFileTextBox.Multiline = true;
            this.atisehamFileTextBox.Name = "atisehamFileTextBox";
            this.atisehamFileTextBox.Size = new System.Drawing.Size(444, 23);
            this.atisehamFileTextBox.TabIndex = 1;
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
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // atisehamFileGroupBox
            // 
            this.atisehamFileGroupBox.Controls.Add(this.atisehamFileTextBox);
            this.atisehamFileGroupBox.Controls.Add(this.browseButton);
            this.atisehamFileGroupBox.Location = new System.Drawing.Point(12, 6);
            this.atisehamFileGroupBox.Name = "atisehamFileGroupBox";
            this.atisehamFileGroupBox.Size = new System.Drawing.Size(537, 55);
            this.atisehamFileGroupBox.TabIndex = 3;
            this.atisehamFileGroupBox.TabStop = false;
            // 
            // buildATISButton
            // 
            this.buildATISButton.Location = new System.Drawing.Point(6, 19);
            this.buildATISButton.Name = "buildATISButton";
            this.buildATISButton.Size = new System.Drawing.Size(105, 34);
            this.buildATISButton.TabIndex = 4;
            this.buildATISButton.Text = "Build ATIS.wav";
            this.buildATISButton.UseVisualStyleBackColor = true;
            this.buildATISButton.Click += new System.EventHandler(this.buildATISButton_Click);
            // 
            // buildATISbackgroundWorker
            // 
            this.buildATISbackgroundWorker.WorkerReportsProgress = true;
            this.buildATISbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.buildATISbackgroundWorker_DoWork);
            this.buildATISbackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.buildATISbackgroundWorker_ProgressChanged);
            this.buildATISbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.buildATISbackgroundWorker_RunWorkerCompleted);
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
            // Sound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 145);
            this.Controls.Add(this.playGroupBox);
            this.Controls.Add(this.buildGroupBox);
            this.Controls.Add(this.atisehamFileGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Sound";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ATIS Sound Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Sound_FormClosing);
            this.Load += new System.EventHandler(this.Sound_Load);
            this.atisehamFileGroupBox.ResumeLayout(false);
            this.atisehamFileGroupBox.PerformLayout();
            this.buildGroupBox.ResumeLayout(false);
            this.playGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playATISButton;
        private System.Windows.Forms.TextBox atisehamFileTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.GroupBox atisehamFileGroupBox;
        private System.ComponentModel.BackgroundWorker buildATISbackgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.Button buildATISButton;
        private System.Windows.Forms.GroupBox buildGroupBox;
        private System.Windows.Forms.GroupBox playGroupBox;
    }
}