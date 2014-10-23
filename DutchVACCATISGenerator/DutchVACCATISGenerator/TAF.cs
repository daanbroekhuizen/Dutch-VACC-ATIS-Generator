﻿using System;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// TAF class.
    /// </summary>
    public partial class TAF : Form
    {
        private DutchVACCATISGenerator dutchVACCATISGenerator;

        /// <summary>
        /// Constructor of TAF. Initializes new instance of TAF.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator.</param>
        public TAF(DutchVACCATISGenerator dutchVACCATISGenerator)
        {
            InitializeComponent();

            this.dutchVACCATISGenerator = dutchVACCATISGenerator;

            //Call get TAF method.
            getTAF();
        }

        /// <summary>
        /// Method to load TAF from KNMI website.
        /// </summary>
        public void getTAF()
        {
            //Remove/clear old TAF from rich text box.
            TAFRichTextBox.Clear();

            try
            {
                //Create web client.
                WebClient client = new WebClient();

                //Set user Agent, make the site think we're not a bot.
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0"; //(Windows; U; Windows NT 6.1; en-US; rv:1.9.2.4) Gecko/20100611 Firefox/3.6.4";

                //Make web request to http://www.knmi.nl/waarschuwingen_en_verwachtingen/luchtvaart/nederlandse_vliegveldverwachtingen.html.
                string data = client.DownloadString("http://www.knmi.nl/waarschuwingen_en_verwachtingen/luchtvaart/nederlandse_vliegveldverwachtingen.html");
              
                try
                {
                    //Get TAF part from loaded HTML code.
                    string[] split = (determineTAFToLoad() + data.Split(new string[] { determineTAFToLoad() }, StringSplitOptions.None)[1]).Split(new string[] { "=" }, StringSplitOptions.None)[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    foreach(string s in split)
                    {
                        TAFRichTextBox.Text += s.Trim() + "\r\n";
                    }
                }
                catch (Exception) { }
            }
            catch (Exception)
            {
                //Show error.
                MessageBox.Show("Unable to load TAF from the Internet.", "Error");
            }
        }

        /// <summary>
        /// Method to determine TAF to load.
        /// </summary>
        /// <returns>String indicating TAF to load</returns>
        private string determineTAFToLoad()
        {
            switch(dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Name)
            {
                case "EHAM":
                    return "TAF EHAM";

                case "EHBK":
                    return "TAF EHBK";

                case "EHEH":
                    return "TAF EHEH";

                case "EHGG":
                    return "TAF EHGG";

                case "EHRD":
                    return "TAF EHRD";
            }

            return String.Empty;
        }

        /// <summary>
        /// Method called if TAF form is closed.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void TAF_FormClosing(object sender, FormClosingEventArgs e)
        {
            dutchVACCATISGenerator.tAFToolStripMenuItem.BackColor = SystemColors.Control;
        }
    }
}