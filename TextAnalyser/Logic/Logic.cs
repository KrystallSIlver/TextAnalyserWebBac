using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TextAnalyser.Models;
using Constant = TextAnalyser.Constants.Constants;

namespace TextAnalyser.Logic
{
    public class AnalysisLogic
    {
        private readonly mph_uaContext _context;
        public AnalysisLogic(mph_uaContext context)
        {         
            _context = context;
        }

        public async Task<SemanticModel> Semantic(string text)        
        {
            var semantic = new SemanticModel();

            if (string.IsNullOrEmpty(text)) return semantic;

            text = text.ToLower();
            semantic.CharCount = text.Length;
            semantic.CharCountWithoutSpaces = text.Count(x => !char.IsWhiteSpace(x));

            var words = GetWordList(text);
            semantic.WordCount = words.Count;

            var sentences = text.Split('.', '?', '!').Where(x => !string.IsNullOrEmpty(x) && !x.Equals('\n')).ToList();
            semantic.SenctenceCount = sentences.Count;

            var unicWords = words.Distinct();
            semantic.UnicWordCount = unicWords.Count();

            var stopWords = words.Where(x => Constant.StopWordsList.Contains(x)).ToList();
            semantic.StopWordCount = stopWords.Count();
            var unicStopWords = stopWords.Distinct();
            var wordsExceptStopWords = words.Where(x=> !unicStopWords.Contains(x));

            var znWords = await GetListAsync(wordsExceptStopWords);

            //var znWords = wordsExceptStopWords.SelectMany(x => _context.WordList.Include(w => w.Nom)
            //        .Where(y => y.Word.Equals(x) && Constant.NounTypes.Contains((int)y.Nom.Part))
            //        .Select(x => x.Nom.Reestr)).ToList();  

            semantic.SignificantWordCount = znWords.Count();

            semantic.Polysyllables = words.Where(x => x.Count(c => Constant.Vowles.Contains(c)) > 2).Count();

            semantic.SemanticCore = GetSemanticCore(znWords, semantic.WordCount);
            semantic.Words = GetSemanticCore(words, semantic.WordCount);
            semantic.StopWords = GetSemanticCore(stopWords, semantic.WordCount);

            semantic.WaterPercentage = Math.Round((double)stopWords.Count() / (double)semantic.WordCount * 100,2);
            semantic.ClasicNauseaPercentage = Math.Sqrt(semantic.SemanticCore.FirstOrDefault().Count) > 2.64 ? Math.Round(Math.Sqrt(semantic.SemanticCore.FirstOrDefault().Count),2) : 2.64;
            semantic.AcademicalNauseaPercentage = Math.Round((double)semantic.SemanticCore.FirstOrDefault().Count / (double)semantic.CharCount * 100,2);

            Readability(ref semantic,text);

            return semantic;
        }

        public List<ZipfModel> Zipf(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<ZipfModel>(); ;
            var words = GetWordList(text);
            var wordsExceptStopWords = words.Where(x => !Constant.StopWordsList.Contains(x));

            var grouped = wordsExceptStopWords.GroupBy(x => x);
            var zipf = grouped.Select(x => new ZipfModel() { Phrase = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).ToArray();

            const double c = 0.065;

            for (int i = 1; i <= zipf.Length; i++)
            {
                zipf[i - 1].Rank = i;
                zipf[i - 1].IdealPerc = c / (double)zipf[i - 1].Rank;
                zipf[i - 1].IdealCount = (int)Math.Round(zipf[i - 1].IdealPerc * words.Count) < 1 ? 1 : (int)Math.Round(zipf[i - 1].IdealPerc * words.Count);
                zipf[i - 1].Frequency = Math.Round((double)zipf[i - 1].Count / (double)zipf[i - 1].IdealCount * 100);
                zipf[i - 1].Recomendation = zipf[i - 1].IdealCount - zipf[i - 1].Count;
                zipf[i - 1].CurrentPerc = (double)zipf[i - 1].Count / (double)words.Count;
            }

            return zipf.ToList();
        }

        private List<SemanticCore> GetSemanticCore(IEnumerable<string> words, int totalWordCount) 
        {
            var grouped = words.GroupBy(x => x);
            return grouped.Select(x => new SemanticCore() { Phrase = x.Key, Count = x.Count(), Frequency = Math.Round((double)x.Count() / (double)totalWordCount * 100, 2) }).OrderByDescending(x=>x.Count).ToList();
        }

        private void Readability(ref SemanticModel semantic, string text)
        {
            semantic.Readability = new Readability();

            var syllables = text.Count(x => Constant.Vowles.Contains(x));
            var ASL = (double)semantic.WordCount / (double)semantic.SenctenceCount;
            var ASW = (double)syllables / (double)semantic.WordCount;
            semantic.Readability.FleschKincaid = FleschKincaid(ASL, ASW);

            var DWW = (double)semantic.Polysyllables / (double)semantic.WordCount;
            semantic.Readability.DaleChall = DaleChall(DWW, ASL);

            var L = (double)semantic.CharCountWithoutSpaces / (double)semantic.WordCount * 100;
            var S = (double)semantic.SenctenceCount / (double)semantic.WordCount * 100;
            semantic.Readability.ColemanLiau = ColemanLiau(L, S);

            semantic.Readability.SMOG = SMOG(semantic.Polysyllables, semantic.SenctenceCount);

            var CW = (double)semantic.CharCountWithoutSpaces / (double)semantic.WordCount;
            semantic.Readability.ARI = ARI(CW, ASL);
        }

        private ReadabilityBase FleschKincaid(double asl, double asw)
        {
            var FRE = 206.835 - 1.3 * asl - 60.1 * asw;
            FRE = Math.Round(FRE, 2);
            var Auditory = GetAuditory();

            string GetAuditory()
            {
                if (FRE <= 30) return "Випускники ВНЗ";
                else if (FRE <= 50) return "Студенти";
                else if (FRE <= 60) return "10 - 11 клас";
                else if (FRE <= 70) return "8 - 9 клас";
                else if (FRE <= 80) return "7 клас";
                else if (FRE <= 90) return "5 - 6 клас";
                else return "Молодші школярі";
            }

            return new ReadabilityBase() { Auditory = Auditory, Index = FRE };
        }

        private ReadabilityBase DaleChall(double dww, double ws)
        {
            var Index = 0.1579 * (dww * 100) + 0.0496 * ws;
            Index = Math.Round(Index, 2);
            var Auditory = GetAuditory();

            string GetAuditory()
            {
                if (Index <= 4.9) return "Молодші школярі";
                else if (Index <= 5.9) return "5 - 6 клас";
                else if (Index <= 5.9) return "7 - 8 клас";
                else if (Index <= 7.9) return "9 клас";
                else if (Index <= 8.9) return "10 - 11 клас";
                else return "Студенти";
            }
            return new ReadabilityBase() { Auditory = Auditory, Index = Index };
        }

        private ReadabilityBase ColemanLiau(double l, double s)
        {
            var Index = 0.0588 * l - 0.296 * s - 15.8;
            Index = Math.Round(Index, 2);

            var Auditory = GetAuditory();

            string GetAuditory()
            {
                if (Index <= 6) return "5 клас";
                else if (Index <= 7) return "6 клас";
                else if (Index <= 8) return "7 клас";
                else if (Index <= 9) return "8 клас";
                else if (Index <= 10) return "9 клас";
                else if (Index <= 11) return "10 клас";
                else if (Index <= 12) return "11 клас";
                else if (Index <= 13) return "Студенти";
                else return "Випускники ВНЗ";
            }
            return new ReadabilityBase() { Auditory = Auditory, Index = Index };
        }

        private ReadabilityBase SMOG(double np, double ns)
        {
            var Index = 1.0430 * Math.Sqrt(np * ((double)30 / ns)) + 3.1291;
            Index = Math.Round(Index, 2);

            var Auditory = GetAuditory();

            string GetAuditory()
            {
                if (Index <= 6) return "5 клас";
                else if (Index <= 7) return "6 клас";
                else if (Index <= 8) return "7 клас";
                else if (Index <= 9) return "8 клас";
                else if (Index <= 10) return "9 клас";
                else if (Index <= 11) return "10 клас";
                else if (Index <= 12) return "11 клас";
                else if (Index <= 13) return "Студенти";
                else return "Випускники ВНЗ";
            }
            return new ReadabilityBase() { Auditory = Auditory, Index = Index };
        }

        private ReadabilityBase ARI(double cw, double ws)
        {
            var Index = 4.71 * cw + 0.5 * ws - 21.43;
            Index = Math.Round(Index, 2);

            var Auditory = GetAuditory();

            string GetAuditory()
            {
                if (Index <= 1) return "Дитсадовці";
                else if (Index <= 2) return "1 - 2 клас";
                else if (Index <= 3) return "3 клас";
                else if (Index <= 4) return "4 клас";
                else if (Index <= 5) return "5 клас";
                else if (Index <= 6) return "6 клас";
                else if (Index <= 7) return "7 клас";
                else if (Index <= 8) return "8 клас";
                else if (Index <= 9) return "9 клас";
                else if (Index <= 10) return "10 клас";
                else if (Index <= 11) return "11 клас";
                else if (Index <= 12) return "Студенти";
                else if (Index <= 13) return "Бакалаври";
                else return "Професори";
            }
            return new ReadabilityBase() { Auditory = Auditory, Index = Index };
        }
        private List<string> GetWordList(string text)
        {
            return text.Split(' ', ',', '.', '?', '!', ';', ':', '"', '(', ')', '—', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0','[',']','#','№','$','%', '*', '@','`','\'','\\','/', '|').Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        private async Task<List<string>> GetListAsync(IEnumerable<string> words)
        {
            return await Task.Run(() => GetList(words));
        }

        private List<string> GetList(IEnumerable<string> words)
        {
            using (var context = new mph_uaContext())
            {
                var list = context.WordList.Include(x=>x.Nom).Where(x => Constant.NounTypes.Contains((int)x.Nom.Part) && words.Contains(x.Word)).Select(x=>x.Nom.Reestr);
                return list.ToList();

            }


        }
    }
}
