using System;
using System.ComponentModel;

namespace DutchVACCATISGenerator.Types.Application
{
    public static class ApplicationEvents
    {
        public static event EventHandler BuildAITSCompletedEvent;
        public static event ProgressChangedEventHandler BuildAITSProgressChangedEvent;
        public static event EventHandler MainFormMovedEvent;
        public static event EventHandler SelectedAirportChangedEvent;

        public static void MainFormMoved(object sender, EventArgs args)
        {
            MainFormMovedEvent?.Invoke(sender, args);
        }

        public static void SelectedAirportChanged(object sender, EventArgs args)
        {
            SelectedAirportChangedEvent?.Invoke(sender, args);
        }
    }
}
