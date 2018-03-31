using System.Collections.Generic;
using System.Drawing;

namespace DutchVACCATISGenerator.Types
{
    public class ApplicationVariables
    {
        public List<string> ATISSamples { get; set; }
        public Rectangle MainFormBounds { get; set; }
        public Metar METAR { get; set; }
        public string SelectedAirport { get; set; }

        public ApplicationVariables()
        {
            ATISSamples = new List<string>();
            SelectedAirport = "EHAM";
        }
    }
}
