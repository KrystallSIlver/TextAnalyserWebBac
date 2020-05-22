using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyser.Models
{
    public class RequestBody
    {
        public string TextForAnalysis { get; set; }
    }

    public class LanguageToolRequestBody
    {
        public string Text { get; set; }
        public string Language { get; set; }
    }

}
