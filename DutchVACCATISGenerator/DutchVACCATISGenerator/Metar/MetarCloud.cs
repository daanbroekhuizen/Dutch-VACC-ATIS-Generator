using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator
{
    public class MetarCloud
    {
        public String cloudType { get; set; }
        public int altitude { get; set; }
        public string addition { get; set; }

        public MetarCloud(String cloudType)
        {
            this.cloudType = cloudType;
        }

        public MetarCloud(String cloudType, int altitude)
        {
            this.cloudType = cloudType;
            this.altitude = altitude;
        }

        public MetarCloud(String cloudType, int altitude, string addition)
        {
            this.cloudType = cloudType;
            this.altitude = altitude;
            this.addition = addition;
        }
    }
}
