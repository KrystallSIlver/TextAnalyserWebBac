using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public class ExtendedSemanticCore : SemanticCore
    {
        public ExtendedSemanticCore()
        {

        }

        public ExtendedSemanticCore(WordForms wordForms)
        {
            this.Phrase = wordForms.Word;
            this.Froms = wordForms.Words;
            this.Count = wordForms.Words.Count;
        }
        public List<string> Froms { get; set; }
    }
}
