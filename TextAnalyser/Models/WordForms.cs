using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public class WordForms
    {
        public WordForms() { }
        public WordForms(IGrouping<string, ReestrWord> grouping) 
        {
            Word = grouping.Key;
            Words = grouping.Select(x=>x.Word).OrderByDescending(x=>x.Length).ToList();
        }
        public string Word { get; set; }
        public List<string> Words { get; set; }
    }

    public class ReestrWord
    {
        public string Reestr { get; set; }
        public string Word { get; set; }
    }
}
