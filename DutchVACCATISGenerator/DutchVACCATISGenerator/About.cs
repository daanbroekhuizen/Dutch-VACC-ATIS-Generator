using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// About class.
    /// </summary>
    public partial class About : Form
    {
        /// <summary>
        /// Constructor of About class.
        /// </summary>
        public About()
        {
            InitializeComponent();

            //Add URL to Dutch VACC site to link label.
            dutchVACCLinkLabel.Links.Add(0, dutchVACCLinkLabel.Text.Length, "www.dutchvacc.nl");

            //Set current version in label.
            applicationVersionLabel.Text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        }

        /// <summary>
        /// Method called when link label is clicked.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event arguments</param>
        private void dutchVACCLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Open default browser and open www.dutchvacc.nl URL.
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}
