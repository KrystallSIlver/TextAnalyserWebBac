using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public class SemanticModel
    {
        public int CharCount { get; set; }
        public int CharCountWithoutSpaces { get; set; }
        public int WordCount { get; set; }
        public int UnicWordCount { get; set; }
        public int SenctenceCount { get; set; }
        public int SignificantWordCount { get; set; }
        public int StopWordCount { get; set; }
        public double WaterPercentage { get; set; }
        public int GrammaticalErrorsCount { get; set; }
        public double ClasicNauseaPercentage { get; set; }
        public double AcademicalNauseaPercentage { get; set; }
        public int Polysyllables { get; set; }
        public Readability Readability { get; set; }
        public List<SemanticCore> SemanticCore { get; set; }
        public List<SemanticCore> Words { get; set; }
        public List<SemanticCore> StopWords { get; set; }
    }
}
