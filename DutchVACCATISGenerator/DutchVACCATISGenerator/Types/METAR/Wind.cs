namespace DutchVACCATISGenerator.Types
{
    public class Wind
    {
        public string GustMax { get; set; }
        public string GustMin { get; set; }
        public string Heading { get; set; }
        public string Knots { get; set; }
        public bool Variable { get; set; }
        public string VariableLeft { get; set; }
        public string VariableRight { get; set; }

        /// <summary>
        /// Constructs a Wind with a variable wind and strength.
        /// </summary>
        /// <param name="variable">Indicates that the wind is variable</param>
        /// <param name="knots">Wind strength</param>
        public Wind(bool variable, string knots)
        {
            Variable = variable;
            Knots = knots;
        }

        /// <summary>
        /// Constructs a Wind with the heading and strength.
        /// </summary>
        /// <param name="heading">Heading of the wind</param>
        /// <param name="knots">Wind strength</param>
        public Wind(string heading, string knots)
        {
            Heading = heading;
            Knots = knots;
        }

        /// <summary>
        /// Constructs a Wind with a heading, minimal gust and maximum gust.
        /// </summary>
        /// <param name="heading">Heading of the wind</param>
        /// <param name="gustMin">Minimal speed of the wind</param>
        /// <param name="gustMax">Maximum speed of the wind</param>
        public Wind(string heading, string gustMin, string gustMax)
        {
            Heading = heading;
            GustMin = gustMin;
            GustMax = gustMax;
        }
    }
}
