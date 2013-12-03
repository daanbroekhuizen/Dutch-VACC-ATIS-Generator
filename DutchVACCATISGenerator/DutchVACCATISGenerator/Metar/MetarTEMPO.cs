using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator
{
    public class MetarTEMPO
    {
        public MetarWind Wind { get; set; }
        public List<MetarPhenoma> Phenomena { get; set; }
        public int Visibility { get; set; }
        public String VerticalVisibility { get; set; }
        public Boolean SKC { get; set; }
        public List<MetarCloud> Clouds { get; set; }
        public Boolean NSW { get; set; }

        public MetarTEMPO()
        {
            Phenomena = new List<MetarPhenoma>();
            Clouds = new List<MetarCloud>();
        }
    }
}
