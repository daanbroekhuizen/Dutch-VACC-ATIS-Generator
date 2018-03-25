using DutchVACCATISGenerator.Logic;
using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Forms
{
    /// <summary>
    /// TAF class.
    /// </summary>
    public partial class TerminalAerodromeForecastForm : Form
    {
        private string taf;

        private readonly ITerminalAerodromeForecastLogic terminalAerodromeForecastLogic;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dutchVACCATISGenerator">Parent DutchVACCATISGenerator</param>
        public TerminalAerodromeForecastForm(ITerminalAerodromeForecastLogic terminalAerodromeForecastLogic)
        {
            InitializeComponent();

            this.terminalAerodromeForecastLogic = terminalAerodromeForecastLogic;
        }

        /// <summary>
        /// Called when the TAF background worker is started.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void TafBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                taf = terminalAerodromeForecastLogic.GetTerminalAerodromeForecast();
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
        private void TafBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Remove/clear old TAF from rich text box.
            terminalAerodromeForecastRichTextBox.Clear();

            try
            {
                if (taf.Contains(terminalAerodromeForecastLogic.DetermineTAFAMDToLoad()))
                {
                    //Get TAF part from loaded HTML code.
                    string[] split = (terminalAerodromeForecastLogic.DetermineTAFAMDToLoad() + taf.Split(new string[] { terminalAerodromeForecastLogic.DetermineTAFAMDToLoad() }, StringSplitOptions.None)[1]).Split(new string[] { "=" }, StringSplitOptions.None)[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    foreach (string s in split)
                        terminalAerodromeForecastRichTextBox.Text += s.Trim() + "\r\n";
                }
                else if (taf.Contains(terminalAerodromeForecastLogic.DetermineTAFCORToLoad()))
                {
                    //Get TAF part from loaded HTML code.
                    string[] split = (terminalAerodromeForecastLogic.DetermineTAFCORToLoad() + taf.Split(new string[] { terminalAerodromeForecastLogic.DetermineTAFCORToLoad() }, StringSplitOptions.None)[1]).Split(new string[] { "=" }, StringSplitOptions.None)[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    foreach (string s in split)
                        terminalAerodromeForecastRichTextBox.Text += s.Trim() + "\r\n";
                }
                else
                {
                    //Get TAF part from loaded HTML code.
                    string[] split = (terminalAerodromeForecastLogic.DetermineTAFToLoad() + taf.Split(new string[] { terminalAerodromeForecastLogic.DetermineTAFToLoad() }, StringSplitOptions.None)[1]).Split(new string[] { "=" }, StringSplitOptions.None)[0].Split(new string[] { "\n" }, StringSplitOptions.None);

                    foreach (string s in split)
                        terminalAerodromeForecastRichTextBox.Text += s.TrimEnd() + "\r\n";
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
