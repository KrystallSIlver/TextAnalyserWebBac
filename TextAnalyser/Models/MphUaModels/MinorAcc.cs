using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class MinorAcc
    {
        public long NomOld { get; set; }
        public string WordE1 { get; set; }
        public long? Occur1 { get; set; }
        public long? Occur2 { get; set; }
        public long? Occur3 { get; set; }
        public long? Double1 { get; set; }
        public long? Double2 { get; set; }
    }
}
