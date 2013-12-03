using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator
{
    public class MetarWind
    {
        public Boolean VRB { get; set; }
        public String windHeading { get; set; }
        public String windKnots { get; set; }
        public String windGustMin { get; set; }
        public String windGustMax { get; set; }
        public String windVariableLeft { get; set; }
        public String windVariableRight { get; set; }

        public MetarWind(Boolean VRB, String windKnots)
        {
            this.VRB = VRB;
            this.windKnots = windKnots;
        }


        public MetarWind(String windHeading, String windKnots)
        {
            this.windHeading = windHeading;
            this.windKnots = windKnots;
        }

        public MetarWind(String windHeading, String windGustMin, String windGustMax)
        {
            this.windHeading = windHeading;
            this.windGustMin = windGustMin;
            this.windGustMax = windGustMax;
        }
    }
}
