using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public class Readability
    {
        public ReadabilityBase FleschKincaid { get; set; }
        public ReadabilityBase DaleChall { get; set; }
        public ReadabilityBase ColemanLiau { get; set; }
        public ReadabilityBase SMOG { get; set; }
        public ReadabilityBase ARI { get; set; }
        public long ElapsedTime { get; set; }
        
    }
}
