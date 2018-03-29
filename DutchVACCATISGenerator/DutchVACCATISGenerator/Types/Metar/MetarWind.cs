using System;

namespace DutchVACCATISGenerator
{
    /// <summary>
    /// Represents the wind of a METAR as easy accessible fields.
    /// </summary>
    public class MetarWind
    {
        public Boolean VRB { get; set; }
        public String windHeading { get; set; }
        public String windKnots { get; set; }
        public String windGustMin { get; set; }
        public String windGustMax { get; set; }
        public String windVariableLeft { get; set; }
        public String windVariableRight { get; set; }

        /// <summary>
        /// Constructs a MetarWind with a variable wind and wind strength.
        /// </summary>
        /// <param name="VRB">Indicates that the wind is variable.</param>
        /// <param name="windKnots">Wind strength (knots).</param>
        public MetarWind(Boolean VRB, String windKnots)
        {
            this.VRB = VRB;
            this.windKnots = windKnots;
        }

        /// <summary>
        /// Constructs a MetarWind with the wind heading and wind strength.
        /// </summary>
        /// <param name="windHeading">Heading of the wind.</param>
        /// <param name="windKnots">Wind strength (knots).</param>
        public MetarWind(String windHeading, String windKnots)
        {
            this.windHeading = windHeading;
            this.windKnots = windKnots;
        }

        /// <summary>
        /// Constructs a MetarWind with a wind heading, minimal wind gust and maximum wind gust.
        /// </summary>
        /// <param name="windHeading">Heading of the wind.</param>
        /// <param name="windGustMin">Minimal speed of the wind (knots).</param>
        /// <param name="windGustMax">Maximum speed of the wind (knots).</param>
        public MetarWind(String windHeading, String windGustMin, String windGustMax)
        {
            this.windHeading = windHeading;
            this.windGustMin = windGustMin;
            this.windGustMax = windGustMax;
        }
    }
}
