using System.Collections.Generic;
using System.Drawing;

namespace DutchVACCATISGenerator.Types.Application
{
    public class ApplicationVariables
    {
        public List<string> ATISSamples { get; set; }
        public string SelectedAirport { get; set; }
        public Rectangle MainFormBounds { get; set; }

        public ApplicationVariables()
        {
            ATISSamples = new List<string>();
            SelectedAirport = "EHAM";
        }
    }
}
