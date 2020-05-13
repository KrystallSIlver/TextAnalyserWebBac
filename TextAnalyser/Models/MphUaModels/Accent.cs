using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class Accent
    {
        public long Id { get; set; }
        public long? Indent1 { get; set; }
        public long? Indent2 { get; set; }
        public long? Indent3 { get; set; }
        public long? Indent4 { get; set; }
        public long? AccentType { get; set; }
        public long? Gram { get; set; }
        public string Xmpl { get; set; }

        public virtual AccentsClass AccentTypeNavigation { get; set; }
    }
}
