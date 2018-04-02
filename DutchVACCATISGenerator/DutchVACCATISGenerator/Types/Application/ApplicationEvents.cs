using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Types
{
    public static class ApplicationEvents
    {
        public static event EventHandler BuildAITSCompletedEvent;
        public static event ProgressChangedEventHandler BuildAITSProgressChangedEvent;
        public static event EventHandler BuildAITSStartedEvent;
        public static event DownloadProgressChangedEventHandler DownloadProgressChangedEvent;
        public static event EventHandler MainFormMovedEvent;
        public static event EventHandler METARProcessedEvent;
        public static event EventHandler NewVersionEvent;
        public static event EventHandler PlaybackStoppedEvent;
        public static event EventHandler SelectedAirportChangedEvent;
        public static event FormClosingEventHandler TerminalAerodromeForecastFormClosingEvent;

        internal static void BuildAITSCompleted()
        {
            BuildAITSCompletedEvent?.Invoke(null, null);
        }

        internal static void BuildAITSProgressChanged(int progressPercentage)
        {
            BuildAITSProgressChangedEvent?.Invoke(null, new ProgressChangedEventArgs(progressPercentage, null));
        }

        internal static void BuildAITSStarted()
        {
            BuildAITSStartedEvent?.Invoke(null, null);
        }

        internal static void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChangedEvent?.Invoke(sender, e);
        }

        internal static void MainFormMoved(object sender, EventArgs e)
        {
            MainFormMovedEvent?.Invoke(sender, e);
        }

        internal static void METARProcessed(object sender, EventArgs e)
        {
            METARProcessedEvent?.Invoke(sender, e);
        }
        
        internal static void NewVersion()
        {
            NewVersionEvent?.Invoke(null, null);
        }

        internal static void PlaybackStopped(object sender, EventArgs e)
        {
            PlaybackStoppedEvent?.Invoke(sender, e);
        }

        internal static void SelectedAirportChanged(object sender, EventArgs e)
        {
            SelectedAirportChangedEvent?.Invoke(sender, e);
        }

        internal static void TerminalAerodromeForecastFormClosing(object sender, FormClosingEventArgs e)
        {
            TerminalAerodromeForecastFormClosingEvent?.Invoke(sender, e);
        }
    }
}
