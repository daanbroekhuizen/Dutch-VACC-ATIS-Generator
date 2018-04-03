namespace DutchVACCATISGenerator.Types
{
    public class Phenomena
    {
        public PhenomenaItensity Intensity { get; set; }
        public string Type { get; set; }

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
