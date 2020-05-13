using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public partial class Nom
    {
        public Nom()
        {
            WordList = new HashSet<WordList>();
        }
        public string Reestr { get; set; }
        public long Field2 { get; set; }
        public long Part { get; set; }
        public long Type { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Digit { get; set; }
        public long NomOld { get; set; }
        public long? Own { get; set; }
        public byte[] Isdel { get; set; }
        public string Reverse { get; set; }
        public byte[] Isproblem { get; set; }
        public long? Accent { get; set; }
        public byte[] SupplAccent { get; set; }

        public virtual AccentsClass AccentNavigation { get; set; }
        public virtual Parts PartNavigation { get; set; }
        public virtual Indents TypeNavigation { get; set; }
        public virtual ICollection<WordList> WordList { get; set; }

    }
}
