using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types.Application;
using System;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Forms
{
    public partial class TerminalAerodromeForecastForm : Form
    {
        private readonly ITerminalAerodromeForecastLogic terminalAerodromeForecastLogic;

        private string terminalAerodromeForecast;

        public TerminalAerodromeForecastForm(ITerminalAerodromeForecastLogic terminalAerodromeForecastLogic)
        {
            this.terminalAerodromeForecastLogic = terminalAerodromeForecastLogic;

            ApplicationEvents.SelectedAirportChangedEvent += SelectedAirportChangedEvent;

            InitializeComponent();
        }

        /// <summary>
        /// Called when the TAF background worker is started.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void TerminalAerodromeForecastBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                terminalAerodromeForecast = terminalAerodromeForecastLogic.DownloadTerminalAerodromeForecast();
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
        private void TerminalAerodromeForecastBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Remove/clear old TAF from rich text box.
            terminalAerodromeForecastRichTextBox.Clear();

            try
            {
                terminalAerodromeForecastRichTextBox.Text = terminalAerodromeForecastLogic.GetTerminalAerodromeForecast(terminalAerodromeForecast);
            }
            catch (Exception)
            {
                //Show error.
                MessageBox.Show("Unable to load TAF from the Internet.", "Error");
            }
        }

        /// <summary>
        /// Fired when other airport tab is selected.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void SelectedAirportChangedEvent(object sender, EventArgs e)
        {
            if (terminalAerodromeForecastBackgroundWorker.IsBusy)
                terminalAerodromeForecastBackgroundWorker.CancelAsync();

            terminalAerodromeForecastBackgroundWorker.RunWorkerAsync();
        }
    }
}
