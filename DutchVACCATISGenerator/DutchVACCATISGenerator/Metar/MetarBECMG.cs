using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator
{
    public class MetarBECMG
    {
        public MetarWind Wind { get; set; }
        public List<MetarPhenoma> Phenomena { get; set; }
        public int Visibility { get; set; }
        public String VerticalVisibility { get; set; }
        public Boolean SKC { get; set; }
        public List<MetarCloud> Clouds { get; set; }
        public Boolean NSW { get; set; }

        public MetarBECMG()
        {
            Phenomena = new List<MetarPhenoma>();
            Clouds = new List<MetarCloud>();
        }
    }
}
