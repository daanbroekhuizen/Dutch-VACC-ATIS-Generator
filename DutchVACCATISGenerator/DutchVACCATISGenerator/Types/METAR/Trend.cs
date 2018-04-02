using System.Collections.Generic;

namespace DutchVACCATISGenerator.Types
{
    public class Trend
    {
        public Wind Wind { get; set; }
        public List<Phenomena> Phenomena { get; set; }
        public bool CAVOK { get; set; }
        public int Visibility { get; set; }
        public int VerticalVisibility { get; set; }
        public bool SKC { get; set; }
        public List<Cloud> Clouds { get; set; }
        public bool NSW { get; set; }

        public Trend()
        {
            Phenomena = new List<Phenomena>();
            Clouds = new List<Cloud>();
        }
    }
}
