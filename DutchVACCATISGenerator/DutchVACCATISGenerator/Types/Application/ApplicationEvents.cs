using System;

namespace DutchVACCATISGenerator.Types.Application
{
    public static class ApplicationEvents
    {
        public static event EventHandler SelectedAirportChangedEvent;

        public static void SelectedAirportChanged(object sender, EventArgs args)
        {
            SelectedAirportChangedEvent?.Invoke(sender, args);
        }
    }
}
