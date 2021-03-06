﻿using DutchVACCATISGenerator.Logic;
using DutchVACCATISGenerator.Types;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Forms
{
    [ExcludeFromCodeCoverage]
    public partial class TerminalAerodromeForecastForm : Form
    {
        private readonly ITerminalAerodromeForecastLogic terminalAerodromeForecastLogic;

        private string terminalAerodromeForecast;

        public TerminalAerodromeForecastForm(ITerminalAerodromeForecastLogic terminalAerodromeForecastLogic)
        {
            this.terminalAerodromeForecastLogic = terminalAerodromeForecastLogic;

            ApplicationEvents.SelectedAirportChangedEvent += SelectedAirportChanged;

            InitializeComponent();

            //Run worker.
            terminalAerodromeForecastBackgroundWorker.RunWorkerAsync();
        }

        #region UI events
        private void TerminalAerodromeForecastForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplicationEvents.TerminalAerodromeForecastFormClosing(sender, e);
        }
        #endregion

        #region Application events
        private void TerminalAerodromeForecastBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                terminalAerodromeForecast = terminalAerodromeForecastLogic.DownloadTerminalAerodromeForecast();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load TAF from the Internet.", "Error");
            }
        }

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
                MessageBox.Show("Unable to load TAF from the Internet.", "Error");
            }
        }
        #endregion

        private void SelectedAirportChanged(object sender, EventArgs e)
        {
            if (terminalAerodromeForecastBackgroundWorker.IsBusy)
                terminalAerodromeForecastBackgroundWorker.CancelAsync();

            terminalAerodromeForecastBackgroundWorker.RunWorkerAsync();
        }
    }
}
