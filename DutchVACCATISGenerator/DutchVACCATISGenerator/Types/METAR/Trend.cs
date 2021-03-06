﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DutchVACCATISGenerator.Types
{
    [ExcludeFromCodeCoverage]
    public class Trend
    {
        public bool CAVOK { get; set; }
        public List<Cloud> Clouds { get; set; }
        public bool NSC { get; set; }
        public bool NSW { get; set; }
        public Part Part { get; set; }
        public List<Phenomena> Phenomena { get; set; }
        public bool SKC { get; set; }
        public int? VerticalVisibility { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }

        public Trend(Part part)
        {
            Clouds = new List<Cloud>();
            Part = part;
            Phenomena = new List<Phenomena>();
        }
    }
}
