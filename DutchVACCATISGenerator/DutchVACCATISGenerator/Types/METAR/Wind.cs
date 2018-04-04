namespace DutchVACCATISGenerator.Types
{
    public class Wind
    {
        public int? GustMax { get; set; }
        public int? GustMin { get; set; }
        public int Heading { get; set; }
        public int Knots { get; set; }
        public bool Variable { get; set; }
        public int? VariableLeft { get; set; }
        public int? VariableRight { get; set; }

        /// <summary>
        /// Constructs a Wind with a variable wind and strength.
        /// </summary>
        /// <param name="variable">Indicates that the wind is variable</param>
        /// <param name="knots">Wind strength</param>
        public Wind(bool variable, int knots)
        {
            Variable = variable;
            Knots = knots;
        }

        /// <summary>
        /// Constructs a Wind with the heading and strength.
        /// </summary>
        /// <param name="heading">Heading of the wind</param>
        /// <param name="knots">Wind strength</param>
        public Wind(int heading, int knots)
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
        public Wind(int heading, int gustMin, int gustMax)
        {
            Heading = heading;
            GustMin = gustMin;
            GustMax = gustMax;
        }
    }
}
