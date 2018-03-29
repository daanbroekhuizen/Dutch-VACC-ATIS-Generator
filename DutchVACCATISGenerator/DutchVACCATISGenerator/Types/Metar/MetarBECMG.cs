using System;
using System.Collections.Generic;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// Represents the fields for a BECMG trend in a METAR.
    /// </summary>
    public class MetarBECMG
    {
        public MetarWind Wind { get; set; }
        public List<MetarPhenomena> Phenomena { get; set; }
        public Boolean CAVOK { get; set; }
        public int Visibility { get; set; }
        public int VerticalVisibility { get; set; }
        public Boolean SKC { get; set; }
        public List<MetarCloud> Clouds { get; set; }
        public Boolean NSW { get; set; }

        /// <summary>
        /// Construct a MetarBECMG. Initializes fields.
        /// </summary>
        public MetarBECMG()
        {
            Phenomena = new List<MetarPhenomena>();
            Clouds = new List<MetarCloud>();
        }
    }
}
