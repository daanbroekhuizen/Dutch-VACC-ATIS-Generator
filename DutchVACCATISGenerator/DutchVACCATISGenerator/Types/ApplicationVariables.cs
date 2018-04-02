using System.Collections.Generic;
using System.Drawing;

namespace DutchVACCATISGenerator.Types
{
    public class ApplicationVariables
    {
        public int ATISIndex { get; set; }
        public List<string> ATISSamples { get; set; }
        public Rectangle MainFormBounds { get; set; }
        public METAR METAR { get; set; }
        public List<string> PhoneticAlphabet { get; set; }
        public string SelectedAirport { get; set; }
        public SchipholPlanningInterfaceData SchipholPlanningInterfaceData { get; set; }

        public ApplicationVariables()
        {
            ATISSamples = new List<string>();
            SelectedAirport = "EHAM";
        }
    }
}
