using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace DutchVACCATISGenerator.Types
{
    [ExcludeFromCodeCoverage]
    public class ApplicationVariables
    {
        public int ATISIndex { get; set; }
        public List<string> ATISSamples { get; set; }
        public int FrictionIndex { get; set; }
        public Rectangle MainFormBounds { get; set; }
        public METAR METAR { get; set; }
        public List<string> PhoneticAlphabet { get; set; }
        public string SelectedAirport { get; set; }

        public ApplicationVariables()
        {
            ATISSamples = new List<string>();
            FrictionIndex = 0;
            SelectedAirport = "EHAM";
        }
    }
}
