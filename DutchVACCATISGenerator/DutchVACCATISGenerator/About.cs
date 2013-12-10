using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            dutchVACCLinkLabel.Links.Add(0, dutchVACCLinkLabel.Text.Length, "www.dutchvacc.nl");
        }

        /// <summary>
        /// Method called when link label is clicked.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event arguments</param>
        private void dutchVACCLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}
