using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class Alphadigit
    {
        public long Lang { get; set; }
        public string Alpha { get; set; }
        public string Digit { get; set; }
        public long Ls { get; set; }
    }
}
