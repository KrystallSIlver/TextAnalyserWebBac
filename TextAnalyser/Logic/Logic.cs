using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        /// <summary>
        /// Виконує семантичні підрахунки
        /// </summary>
        /// <param name="text">Текст українською мовою</param>
        public async Task<SemanticModel> Semantic(string text)
        {
            var semantic = new SemanticModel();
            
            if (string.IsNullOrEmpty(text)) return semantic;

            text = text.ToLower();
            //Отримання кількості символів з та без пробілів
            semantic.CharCount = text.Length;
            semantic.CharCountWithoutSpaces = text.Count(x => !char.IsWhiteSpace(x));
            //Отримання повного списку слів та їх кількості
            var words = GetWordList(text);
            semantic.WordCount = words.Count;
            //Отримання списку речень та їх кількості
            var sentences = text.Split(Constant.sentencesSeparator).Where(x => !string.IsNullOrEmpty(x) && !x.Equals('\n')).ToList();
            semantic.SenctenceCount = sentences.Count;
            //Список унікальніх слів
            var unicWords = words.Distinct();
            semantic.UnicWordCount = unicWords.Count();
            //Список стоп слів
            var stopWords = words.Where(x => Constant.StopWordsList.Contains(x)).ToList();
            semantic.StopWordCount = stopWords.Count();
            //Список унікальних стоп слів
            var unicStopWords = stopWords.Distinct();
            //Список слів без стоп слів
            var wordsExceptStopWords = words.Where(x=> !unicStopWords.Contains(x));
            //Список значимих слів
            var znWords = await GetSignificantListAsync(wordsExceptStopWords);

            semantic.SignificantWordCount = znWords.Count();
            //Список складних слів
            semantic.Polysyllables = words.Where(x => x.Count(c => Constant.Vowles.Contains(c)) > 2).Count();
            //Перетворення списків слів до табличного вигляду
            semantic.SemanticCore = GetSemanticCore(znWords, semantic.WordCount);
            semantic.Words = GetSemanticCore(words, semantic.WordCount);
            semantic.StopWords = GetSemanticCore(stopWords, semantic.WordCount);
            //Підрахунок водності тексту
            semantic.WaterPercentage = Math.Round((double)stopWords.Count() / (double)semantic.WordCount * 100,2);
            //Підрахунок тошноти тексту
            semantic.ClasicNauseaPercentage = Math.Sqrt(semantic.SemanticCore.FirstOrDefault().Count) > 2.64 ? Math.Round(Math.Sqrt(semantic.SemanticCore.FirstOrDefault().Count),2) : 2.64;
            semantic.AcademicalNauseaPercentage = Math.Round((double)semantic.SemanticCore.FirstOrDefault().Count / (double)semantic.CharCount * 100,2);
            //Обрахунок читабельності
            await Task.Run(() => Readability(ref semantic,text));

            return semantic;
        }
        /// <summary>
        /// Аналіз методом Ципфа
        /// </summary>
        /// <param name="text">Текст українською мовою</param>
        public List<ZipfModel> Zipf(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<ZipfModel>();

            //Отримання повного списку слів
            var words = GetWordList(text);
            //Список слів без стоп слів
            var wordsExceptStopWords = words.Where(x => !Constant.StopWordsList.Contains(x));
            
            var grouped = wordsExceptStopWords.GroupBy(x => x);
            //Отримання списку слів та ії кількості у тексті
            var zipf = grouped.Select(x => new ZipfModel() { Phrase = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).ToArray();

            const double c = 0.065;
            //Підрахування інших параметрів
            for (int i = 1; i <= zipf.Length; i++)
            {
                zipf[i - 1].Rank = i;
                zipf[i - 1].IdealPerc = c / (double)zipf[i - 1].Rank;
                zipf[i - 1].IdealCount = (int)Math.Round(zipf[i - 1].IdealPerc * words.Count) < 1 ? 1 : (int)Math.Round(zipf[i - 1].IdealPerc * words.Count);
                zipf[i - 1].Frequency = zipf[i - 1].Count < zipf[i - 1].IdealCount 
                    ? Math.Round((double)zipf[i - 1].Count / (double)zipf[i - 1].IdealCount * 100) 
                    : Math.Round((double)zipf[i - 1].IdealCount / (double)zipf[i - 1].Count * 100);

                zipf[i - 1].Recomendation = zipf[i - 1].IdealCount - zipf[i - 1].Count;
                zipf[i - 1].CurrentPerc = (double)zipf[i - 1].Count / (double)words.Count;
            }

            return zipf.ToList();
        }
        /// <summary>
        /// Карта тексту
        /// </summary>
        /// <param name="text">Текст українською мовою</param>
        public async Task<List<WordForms>> Map(string text)
        {
            //Список слів без стоп слів
            var words = GetWordList(text).Except(Constant.StopWordsList);
            
            return await Task.Run(() => GetMapInfo(words));
        }
        private List<SemanticCore> GetSemanticCore(IEnumerable<string> words, int totalWordCount) 
        {
            var grouped = words.GroupBy(x => x);
            return grouped.Select(x => new SemanticCore() { Phrase = x.Key, Count = x.Count(), Frequency = Math.Round((double)x.Count() / (double)totalWordCount * 100, 2) }).OrderByDescending(x=>x.Count).ToList();
        }
        #region Readability
        /// <summary>
        /// Аналіз читабельності тексту
        /// </summary>
        /// <param name="semantic">Семантичні дані</param>
        /// <param name="text">Текст</param>
        private void Readability(ref SemanticModel semantic, string text)
        {
            var sw = new Stopwatch();
            sw.Start();
            semantic.Readability = new Readability();
            //Отримання кількості сладів у всьому тексті
            var syllables = text.Count(x => Constant.Vowles.Contains(x));
            var ASL = (double)semantic.WordCount / (double)semantic.SenctenceCount;
            var ASW = (double)syllables / (double)semantic.WordCount;
            //Індекс читабельності за Flesch-Kincaid
            semantic.Readability.FleschKincaid = FleschKincaid(ASL, ASW);

            var DWW = (double)semantic.Polysyllables / (double)semantic.WordCount;
            //Індекс читабельності за Dale-Chale
            semantic.Readability.DaleChall = DaleChall(DWW, ASL);

            var L = (double)semantic.CharCountWithoutSpaces / (double)semantic.WordCount * 100;
            var S = (double)semantic.SenctenceCount / (double)semantic.WordCount * 100;
            //Індекс читабельності за Coleman-Liau
            semantic.Readability.ColemanLiau = ColemanLiau(L, S);
            //Індекс читабельності SMOG
            semantic.Readability.SMOG = SMOG(semantic.Polysyllables, semantic.SenctenceCount);

            var CW = (double)semantic.CharCountWithoutSpaces / (double)semantic.WordCount;
            //Індекс читабельності Automated Readability
            semantic.Readability.ARI = ARI(CW, ASL);
            sw.Stop();
            semantic.Readability.ElapsedTime = sw.ElapsedMilliseconds;
        }
        /// <summary>
        /// Читабельність за Flesch-Kincaid 
        /// </summary>
        private ReadabilityBase FleschKincaid(double asl, double asw)
        {
            //Обчислення індексу
            var FRE = 206.835 - 1.3 * asl - 60.1 * asw;
            FRE = Math.Round(FRE, 2);
            //Отримання аудиторії
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
        /// <summary>
        /// Читабельність за DaleChall 
        /// </summary>
        private ReadabilityBase DaleChall(double dww, double ws)
        {
            //Обчислення індексу
            var Index = 0.1579 * (dww * 100) + 0.0496 * ws;
            Index = Math.Round(Index, 2);
            //Отримання аудиторії
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
        /// <summary>
        /// Читабельність за ColemanLiau 
        /// </summary>
        private ReadabilityBase ColemanLiau(double l, double s)
        {
            //Обчислення індексу
            var Index = 0.0588 * l - 0.296 * s - 15.8;
            Index = Math.Round(Index, 2);
            //Отримання аудиторії
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
        /// <summary>
        /// Читабельність SMOG 
        /// </summary>
        private ReadabilityBase SMOG(double np, double ns)
        {
            //Обчислення індексу
            var Index = 1.0430 * Math.Sqrt(np * ((double)30 / ns)) + 3.1291;
            Index = Math.Round(Index, 2);
            //Отримання аудиторії
            var Auditory = GetAuditory();

            string GetAuditory()
            {
                if (np <= 6) return "5 клас";
                else if (np <= 12) return "6 клас";
                else if (np <= 20) return "7 клас";
                else if (np <= 30) return "8 клас";
                else if (np <= 42) return "9 клас";
                else if (np <= 56) return "10 клас";
                else if (np <= 72) return "11 клас";
                else if (np <= 90) return "1 курс";
                else if (np <= 110) return "2 курс";
                else if (np <= 132) return "3 курс";
                else if (np <= 156) return "4 курс";
                else if (np <= 182) return "Mагістр";
                else if (np <= 210) return "Aспірант";
                else return "Професор";
            }
            return new ReadabilityBase() { Auditory = Auditory, Index = Index };
        }
        /// <summary>
        /// Читабельність ARI 
        /// </summary>
        private ReadabilityBase ARI(double cw, double ws)
        {
            //Обчислення індексу
            var Index = 4.71 * cw + 0.5 * ws - 21.43;
            Index = Math.Round(Index, 2);
            //Отримання аудиторії
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
        #endregion
        /// <summary>
        /// Отримання списку слів
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<string> GetWordList(string text)
        {
            return text.Split(Constant.wordSeperators).Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        private async Task<List<string>> GetSignificantListAsync(IEnumerable<string> words)
        {
            return await Task.Run(() => GetSignificantList(words));
        }
        /// <summary>
        /// Оримання списку значимих слів
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private List<string> GetSignificantList(IEnumerable<string> words)
        {
            using (var context = new mph_uaContext())
            {
                var list = context.WordList.Include(x=>x.Nom).Where(x => Constant.NounTypes.Contains((int)x.Nom.Part) && words.Contains(x.Word)).Select(x=> x.Nom.Reestr);
                return list.ToList();
            }
        }
        /// <summary>
        /// Отримання даних для побудови карти тексту
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private List<WordForms> GetMapInfo(IEnumerable<string> words)
        {
            using (var context = new mph_uaContext())
            {
                var list = context.WordList.Include(x => x.Nom)
                                                  .Where(x => Constant.NounTypes.Contains((int)x.Nom.Part) && words.Contains(x.Word))
                                                  .Select(x => new ReestrWord() { Reestr = x.Nom.Reestr, Word = x.Word })
                                                  .ToList();

                var listCopy = new List<ReestrWord>(list);
                listCopy.ForEach(x => {
                    var wordCount = words.Where(y => x.Word.Equals(y)).Count();
                    while (list.Where(y => y.Reestr.Equals(x.Reestr)).Count() < wordCount)
                        if (wordCount > 1)
                            list.Add(x);
                        else
                            break;
                });

                var groupedList = list.GroupBy(x => x.Reestr);

                return groupedList.Select(x => new WordForms(x)).OrderByDescending(x=>x.Words.Count).Take(5).ToList();
            }
        }
    }
}
