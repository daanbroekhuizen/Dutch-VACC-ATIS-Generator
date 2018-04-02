namespace DutchVACCATISGenerator.Types
{
    public class Wind
    {
        public bool VRB { get; set; }
        public string windHeading { get; set; }
        public string windKnots { get; set; }
        public string windGustMin { get; set; }
        public string windGustMax { get; set; }
        public string windVariableLeft { get; set; }
        public string windVariableRight { get; set; }

        /// <summary>
        /// Constructs a MetarWind with a variable wind and wind strength.
        /// </summary>
        /// <param name="VRB">Indicates that the wind is variable.</param>
        /// <param name="windKnots">Wind strength (knots).</param>
        public Wind(bool VRB, string windKnots)
        {
            this.VRB = VRB;
            this.windKnots = windKnots;
        }

        /// <summary>
        /// Constructs a MetarWind with the wind heading and wind strength.
        /// </summary>
        /// <param name="windHeading">Heading of the wind.</param>
        /// <param name="windKnots">Wind strength (knots).</param>
        public Wind(string windHeading, string windKnots)
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
        public Wind(string windHeading, string windGustMin, string windGustMax)
        {
            this.windHeading = windHeading;
            this.windGustMin = windGustMin;
            this.windGustMax = windGustMax;
        }
    }
}
