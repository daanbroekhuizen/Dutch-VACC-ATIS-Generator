using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator
{
    public class Metar
    {
        public String ICAO { get; set; }
        public String Time { get; set; }
        public Dictionary<int, MetarWind> winds { get; set; }
        public Dictionary<int, MetarPhenoma> phenomena { get; set; }
        public Boolean CAVOK { get; set; }
        public Dictionary<int, int> Visibility { get; set; }
        public String verticalVisibility;
        public Boolean SKC { get; set; }
        public Boolean NSC { get; set; }
        public Dictionary<int, MetarCloud> clouds { get; set; }
        public String Temperature { get; set; }
        public String Dewpoint { get; set; }
        public int QNH { get; set; }
        public Boolean NOSIG { get; set; }
        public Boolean TEMPO { get; set; }
        public int trendTEMPOPosition { get; set; }
        public Boolean BECMG { get; set; }
        public int trendBECMGPosition { get; set; }
        public Boolean NSW { get; set; }

        public Metar()
        {
            winds = new Dictionary<int, MetarWind>();
            phenomena = new Dictionary<int, MetarPhenoma>();
            Visibility = new Dictionary<int, int>();
            clouds = new Dictionary<int, MetarCloud>();
        }
    }
}
