using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public class ZipfModel : SemanticCore
    {
        public int Rank { get; set; }
        public int IdealCount { get; set; }
        public int Recomendation { get; set; }
        public double IdealPerc { get; set; }
        public double CurrentPerc { get; set; }
    }
}
