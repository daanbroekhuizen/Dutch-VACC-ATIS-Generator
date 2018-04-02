using Newtonsoft.Json;
using System.Collections.Generic;

namespace DutchVACCATISGenerator.Types
{
    public class SchipholPlanningInterface
    {
        [JsonProperty("lvnl_start")]
        public string Start { get; set; }

        [JsonProperty("lvnl_land")]
        public string Landing { get; set; }
    }

    public class SchipholPlanningInterfaceData
    {
        public List<string> DepartureRunways { get; set; }
        public List<string> LandingRunways { get; set; }

        public SchipholPlanningInterfaceData()
        {
            DepartureRunways = new List<string>();
            LandingRunways = new List<string>();
        }
    }
}
