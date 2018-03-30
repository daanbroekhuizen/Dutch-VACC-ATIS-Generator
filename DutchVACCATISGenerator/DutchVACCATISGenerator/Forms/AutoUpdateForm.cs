using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types.Application;
using System;
using System.Net;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    public partial class AutoUpdaterForm : Form
    {
        public AutoUpdaterForm(IAutoUpdateLogic autoUpdateLogic)
        {
            InitializeComponent();

            ApplicationEvents.DownloadProgressChangedEvent += DownloadProgressChanged;

            try
            {
                autoUpdateLogic.AutoUpdate();
            }
            catch (Exception)
            {
                this.Close();
            }
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (progressBar.InvokeRequired)
                progressBar.Invoke(new Action(() => progressBar.Value = e.ProgressPercentage));
            else
                progressBar.Value = e.ProgressPercentage;
        }
    }
}

