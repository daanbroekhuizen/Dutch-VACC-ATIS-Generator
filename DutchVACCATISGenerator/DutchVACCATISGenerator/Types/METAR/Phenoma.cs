namespace DutchVACCATISGenerator.Types
{
    public class Phenomena
    {
        public bool hasIntensity { get; set; }
        public string phenomena { get; set; }

        /// <summary>
        /// Constructs a MetarPhenomena.
        /// </summary>
        /// <param name="phenoma">Phenomena observed.</param>
        public Phenomena(string phenoma)
        {
            this.phenomena = phenoma;
        }

        /// <summary>
        /// Constructs a MetarPhenomena with intensity.
        /// </summary>
        /// <param name="hasIntensity">Indicates if the phenomena has a intensity indicator.</param>
        /// <param name="phenomena">Phenomena observed.</param>
        public Phenomena(bool hasIntensity, string phenomena)
        {
            this.hasIntensity = hasIntensity;
            this.phenomena = phenomena;
        }
    }
}
