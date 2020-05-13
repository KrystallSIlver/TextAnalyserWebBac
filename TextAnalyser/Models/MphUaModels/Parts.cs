using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class Parts
    {
        public Parts()
        {
            Nom = new HashSet<Nom>();
        }

        public long Id { get; set; }
        public string Part { get; set; }
        public string Com { get; set; }
        public string Ac { get; set; }
        public long? GrId { get; set; }
        public long? Rid { get; set; }
        public long? Mnozh { get; set; }
        public long? Istota { get; set; }
        public long? Vid { get; set; }
        public long? Adjekt { get; set; }

        public virtual Gr Gr { get; set; }
        public virtual ICollection<Nom> Nom { get; set; }
    }

}
