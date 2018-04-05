namespace DutchVACCATISGenerator.Types
{
    public class Phenomena
    {
        public PhenomenaItensity Intensity { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Constructs a Phenomena with intensity and type.
        /// </summary>
        /// <param name="intensity"></param>
        /// <param name="type"></param>
        public Phenomena(PhenomenaItensity intensity, string type)
        {
            Intensity = intensity;
            Type = type;
        }
    }

    public enum PhenomenaItensity
    {
        NORMAL,
        LIGHT,
        HEAVY
    }
}
