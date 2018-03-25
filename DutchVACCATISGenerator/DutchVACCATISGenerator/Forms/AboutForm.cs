using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            //Add URL to Dutch VACC site to link label.
            DutchVACCLinkLabel.Links.Add(0, DutchVACCLinkLabel.Text.Length, "www.dutchvacc.nl");

            //Set current version in label.
            ApplicationVersionLabel.Text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        }

        /// <summary>
        /// Method called when link label is clicked.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event arguments</param>
        private void DutchVACCLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }
    }
}
