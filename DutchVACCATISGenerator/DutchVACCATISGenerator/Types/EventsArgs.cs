using System;
using System.Diagnostics.CodeAnalysis;

namespace DutchVACCATISGenerator.Types
{
    [ExcludeFromCodeCoverage]
    public class METARDownloadedEventArgs : EventArgs
    {
        public string METAR { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class SchipholRunwaysEventArgs : EventArgs
    {
        public SchipholPlanningInterfaceData SchipholPlanningInterfaceData { get; set; }
    }
}
