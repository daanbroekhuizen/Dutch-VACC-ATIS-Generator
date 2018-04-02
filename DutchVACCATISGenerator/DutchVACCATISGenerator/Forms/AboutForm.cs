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
            dutchVACCLinkLabel.Links.Add(0, dutchVACCLinkLabel.Text.Length, "www.dutchvacc.nl");

            //Set current version in label.
            applicationVersionLabel.Text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        }
        
        private void DutchVACCLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }
    }
}
