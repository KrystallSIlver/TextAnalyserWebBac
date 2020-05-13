using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public class WordList
    {
        public virtual int Id { get; set; }
        public virtual string Word { get; set; }
        public virtual long NomId { get; set; }
        public virtual Nom Nom { get; set; }

    }
}
