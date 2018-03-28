﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Types.Application
{
    public static class ApplicationEvents
    {
        public static event EventHandler BuildAITSCompletedEvent;
        public static event FormClosingEventHandler TerminalAerodromeForecastFormClosingEvent;
        public static event ProgressChangedEventHandler BuildAITSProgressChangedEvent;
        public static event EventHandler MainFormMovedEvent;
        public static event EventHandler SelectedAirportChangedEvent;

        public static void MainFormMoved(object sender, EventArgs e)
        {
            MainFormMovedEvent?.Invoke(sender, e);
        }

        public static void SelectedAirportChanged(object sender, EventArgs e)
        {
            SelectedAirportChangedEvent?.Invoke(sender, e);
        }

        internal static void TerminalAerodromeForecastFormClosing(object sender, FormClosingEventArgs e)
        {
            TerminalAerodromeForecastFormClosingEvent?.Invoke(sender, e);
        }
    }
}
