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
        public MetarWind Wind { get; set; }
        public List<MetarPhenoma> Phenomena { get; set; }
        public Boolean CAVOK { get; set; }
        public int Visibility { get; set; }
        public String VerticalVisibility;
        public Boolean SKC { get; set; }
        public Boolean NSC { get; set; }
        public List<MetarCloud> Clouds { get; set; }
        public String Temperature { get; set; }
        public String Dewpoint { get; set; }
        public int QNH { get; set; }
        public Boolean NOSIG { get; set; }
        public Boolean NSW { get; set; }
        public MetarBECMG metarBECMG {get; set; }
        public MetarTEMPO metarTEMPO {get; set; }
    
        public Metar()
        {
            Phenomena = new List<MetarPhenoma>();
            Clouds = new List<MetarCloud>();
        }
    }
}
