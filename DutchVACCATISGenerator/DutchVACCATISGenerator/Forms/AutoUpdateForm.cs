using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using System;
using System.Net;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Forms
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

                //If anything fail during the auto update call, the error will not bubble to the form.
                //Therfore this line should only be called if an error occurs to exit the program.
                Application.Exit();
            }
            catch
            {
                this.Close();
            }
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Invoke(new Action(() => progressBar.Value = e.ProgressPercentage));
        }
    }
}

