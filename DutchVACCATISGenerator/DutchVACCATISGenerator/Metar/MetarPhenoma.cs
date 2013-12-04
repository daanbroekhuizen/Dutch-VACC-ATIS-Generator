using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// Represents the phenomena of a METAR as easy accessible fields.
    /// </summary>
    public class MetarPhenomena
    {
        public Boolean hasIntensity { get; set; }
        public String phenomena { get; set; }

        /// <summary>
        /// Constructs a MetarPhenomena.
        /// </summary>
        /// <param name="phenoma">Phenomena observed.</param>
        public MetarPhenomena(String phenoma)
        {
            this.phenomena = phenoma;
        }

        /// <summary>
        /// Constructs a MetarPhenomena with intensity.
        /// </summary>
        /// <param name="hasIntensity">Indicates if the phenomena has a intensity indicator.</param>
        /// <param name="phenomena">Phenomena observed.</param>
        public MetarPhenomena(Boolean hasIntensity, String phenomena)
        {
            this.hasIntensity = hasIntensity;
            this.phenomena = phenomena;
        }
    }
}
