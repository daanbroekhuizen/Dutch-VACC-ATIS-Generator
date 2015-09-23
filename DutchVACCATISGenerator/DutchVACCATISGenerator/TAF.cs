using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// TAF class.
    /// </summary>
    public partial class TAF : Form
    {
        private DutchVACCATISGenerator dutchVACCATISGenerator;
        private string taf;

        /// <summary>
        /// Constructor of TAF. Initializes new instance of TAF.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator</param>
        public TAF(DutchVACCATISGenerator dutchVACCATISGenerator)
        {
            InitializeComponent();

            this.dutchVACCATISGenerator = dutchVACCATISGenerator;

            //Load TAF.
            tafBackgroundWorker.RunWorkerAsync();
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
        /// Method to determine if TAF contains AMD.
        /// </summary>
        /// <returns>String indicating TAF AMD to determine</returns>
        private string determineTAFAMDToLoad()
        {
            switch (dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Name)
            {
                case "EHAM":
                    return "TAF AMD EHAM";

                case "EHBK":
                    return "TAF AMD EHBK";

                case "EHEH":
                    return "TAF AMD EHEH";

                case "EHGG":
                    return "TAF AMD EHGG";

                case "EHRD":
                    return "TAF AMD EHRD";
            }

            return String.Empty;
        }

        /// <summary>
        /// Method to determine if TAF contains COR.
        /// </summary>
        /// <returns>String indicating TAF COR to determine</returns>
        private string determineTAFCORToLoad()
        {
            switch (dutchVACCATISGenerator.ICAOTabControl.SelectedTab.Name)
            {
                case "EHAM":
                    return "TAF COR EHAM";

                case "EHBK":
                    return "TAF COR EHBK";

                case "EHEH":
                    return "TAF COR EHEH";

                case "EHGG":
                    return "TAF COR EHGG";

                case "EHRD":
                    return "TAF COR EHRD";
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

        /// <summary>
        /// Called when the TAF background worker is started.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void tafBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //Create web client.
                WebClient client = new WebClient();

                //Set user Agent, make the site think we're not a bot.
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0"; //(Windows; U; Windows NT 6.1; en-US; rv:1.9.2.4) Gecko/20100611 Firefox/3.6.4";

                //Make web request to get TAF.
                //taf = client.DownloadString("http://www.aviationweather.gov/adds/tafs?station_ids=EHAM&std_trans=standard&submit_taf=Get+TAFs");
                taf = client.DownloadString("https://www.knmi.nl/nederland-nu/luchtvaart/vliegveldverwachtingen");
            }
            catch (Exception)
            {
                //Show error.
                MessageBox.Show("Unable to load TAF from the Internet.", "Error");
            }
        }

        /// <summary>
        /// Called when the TAF background worker is finished.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void tafBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Remove/clear old TAF from rich text box.
            TAFRichTextBox.Clear();

            try
            {
                if (taf.Contains(determineTAFAMDToLoad()))
                {
                    //Get TAF part from loaded HTML code.
                    string[] split = (determineTAFAMDToLoad() + taf.Split(new string[] { determineTAFAMDToLoad() }, StringSplitOptions.None)[1]).Split(new string[] { "=" }, StringSplitOptions.None)[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    foreach (string s in split)
                        TAFRichTextBox.Text += s.Trim() + "\r\n";
                }
                else if(taf.Contains(determineTAFCORToLoad()))
                {
                    //Get TAF part from loaded HTML code.
                    string[] split = (determineTAFCORToLoad() + taf.Split(new string[] { determineTAFCORToLoad() }, StringSplitOptions.None)[1]).Split(new string[] { "=" }, StringSplitOptions.None)[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    foreach (string s in split)
                        TAFRichTextBox.Text += s.Trim() + "\r\n";
                }
                else
                {
                    //Get TAF part from loaded HTML code.
                    string[] split = (determineTAFToLoad() + taf.Split(new string[] { determineTAFToLoad() }, StringSplitOptions.None)[1]).Split(new string[] { "=" }, StringSplitOptions.None)[0].Split(new string[] { "\n" }, StringSplitOptions.None);

                    foreach (string s in split)
                        TAFRichTextBox.Text += s.TrimEnd() + "\r\n";
                }
            }
            catch (Exception)
            {
                //Show error.
                MessageBox.Show("Unable to load TAF from the Internet.", "Error");
            }
        }
    }
}
