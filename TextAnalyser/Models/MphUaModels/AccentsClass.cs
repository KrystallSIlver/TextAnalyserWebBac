using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class AccentsClass
    {
        public AccentsClass()
        {
            Accent = new HashSet<Accent>();
            Nom = new HashSet<Nom>();
        }

        public long Id { get; set; }
        public string PartDesc { get; set; }

        public virtual ICollection<Accent> Accent { get; set; }
        public virtual ICollection<Nom> Nom { get; set; }
    }
}
