using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class TranscIrregular
    {
        public long Id { get; set; }
        public string Reestr { get; set; }
        public long? PositionDel1 { get; set; }
        public long? PositionDel2 { get; set; }
    }
}
