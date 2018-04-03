using System.Collections.Generic;

namespace DutchVACCATISGenerator.Types
{
    public class Trend
    {
        public bool CAVOK { get; set; }
        public List<Cloud> Clouds { get; set; }
        public bool NSC { get; set; }
        public bool NSW { get; set; }
        public List<Phenomena> Phenomena { get; set; }
        public bool SKC { get; set; }
        public int VerticalVisibility { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }

        public Trend()
        {
            Clouds = new List<Cloud>();
            Phenomena = new List<Phenomena>();
        }
    }
}
