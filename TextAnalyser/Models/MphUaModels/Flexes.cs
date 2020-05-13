using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class Flexes
    {
        public long Id { get; set; }
        public string Flex { get; set; }
        public long? Field2 { get; set; }
        public string Xmpl { get; set; }
        public long Type { get; set; }
        public string Digit { get; set; }

        public virtual Indents TypeNavigation { get; set; }
    }
}
