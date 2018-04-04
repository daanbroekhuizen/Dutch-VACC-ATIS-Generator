using System;

namespace DutchVACCATISGenerator.Types
{
    public class METARDownloadEventArgs : EventArgs
    {
        public string METAR { get; set; }
    }
}
