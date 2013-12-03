using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator
{
    public class MetarPhenoma
    {
        public Boolean hasIntensity { get; set; }
        public String phenoma { get; set; }

        public MetarPhenoma(Boolean hasIntensity, String phenoma)
        {
            this.hasIntensity = hasIntensity;
            this.phenoma = phenoma;
        }
           
        public MetarPhenoma(String phenoma)
        {
            this.phenoma = phenoma;
        }
    }
}
