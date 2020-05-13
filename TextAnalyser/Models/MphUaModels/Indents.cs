using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class Indents
    {
        public Indents()
        {
            Flexes = new HashSet<Flexes>();
            Nom = new HashSet<Nom>();
        }

        public long Type { get; set; }
        public long Indent { get; set; }
        public long? Field3 { get; set; }
        public long? Field4 { get; set; }
        public string Comment { get; set; }
        public long GrId { get; set; }

        public virtual Gr Gr { get; set; }
        public virtual ICollection<Flexes> Flexes { get; set; }
        public virtual ICollection<Nom> Nom { get; set; }
    }
}
