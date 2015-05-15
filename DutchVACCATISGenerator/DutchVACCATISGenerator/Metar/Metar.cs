using System;
using System.Collections.Generic;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// Represents the fields of a METAR.
    /// </summary>
    public class Metar
    {
        public String ICAO { get; set; }
        public String Time { get; set; }
        public MetarWind Wind { get; set; }
        public List<MetarPhenomena> Phenomena { get; set; }
        public Boolean CAVOK { get; set; }
        public int Visibility { get; set; }
        public Boolean RVR { get; set; }
        public Dictionary<String, int> RVRValues { get; set; }
        public int VerticalVisibility { get; set; }
        public Boolean SKC { get; set; }
        public Boolean NSC { get; set; }
        public List<MetarCloud> Clouds { get; set; }
        public String Temperature { get; set; }
        public String Dewpoint { get; set; }
        public int QNH { get; set; }
        public Boolean NOSIG { get; set; }
        public Boolean NSW { get; set; }
        public MetarBECMG metarBECMG {get; set; }
        public MetarTEMPO metarTEMPO {get; set; }
    
        /// <summary>
        /// Construct a Metar. Initializes fields.
        /// </summary>
        public Metar()
        {
            Phenomena = new List<MetarPhenomena>();
            RVRValues = new Dictionary<String, int>();
            Clouds = new List<MetarCloud>();
        }
    }
}
