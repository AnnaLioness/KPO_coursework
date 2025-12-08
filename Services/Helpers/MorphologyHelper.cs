using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Services.Helpers
{
    public class MorphologyHelper
    {
        public static string[] GetLemmas(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return Array.Empty<string>();
            var words = text.Split(new char[] { ' ', '.', ',', '!', '?', '-', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Select(w => SimpleStem(w)).ToArray();
        }

        private static string SimpleStem(string word)
        {
            word = word.ToLowerInvariant();
            // убираем простые окончания
            foreach (var suffix in new[] { "ами", "ями", "ами", "ах", "ях", "ов", "ев", "ин", "ая", "ое", "ие", "ый", "ый", "ой", "ю", "и", "а", "я", "ы", "е", "о", "у" })
            {
                if (word.EndsWith(suffix))
                {
                    return word.Substring(0, word.Length - suffix.Length);
                }
            }
            return word;
        }

        public static string HighlightText(string content, string query, int snippetLength = 10)
        {
            if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(query))
                return content;

            var queryLemmas = GetLemmas(query).ToArray();

            var words = Regex.Matches(content, @"\w+|[^\w\s]", RegexOptions.Multiline)
                             .Cast<Match>()
                             .Select(m => new { Word = m.Value, Index = m.Index })
                             .ToList();

            for (int i = 0; i <= words.Count - queryLemmas.Length; i++)
            {
                var segment = words.Skip(i).Take(queryLemmas.Length)
                                   .Select(w => SimpleStem(w.Word))
                                   .ToArray();

                if (queryLemmas.SequenceEqual(segment, StringComparer.OrdinalIgnoreCase))
                {
                    // подсветка
                    for (int j = 0; j < queryLemmas.Length; j++)
                    {
                        words[i + j] = new
                        {
                            Word = $"**{words[i + j].Word}**",
                            Index = words[i + j].Index
                        };
                    }

                    // вычисляем границы среза по количеству слов вокруг
                    int start = Math.Max(0, i - snippetLength);
                    int end = Math.Min(words.Count, i + queryLemmas.Length + snippetLength);

                    return string.Join(" ", words.Skip(start).Take(end - start).Select(w => w.Word));
                }
            }

            // если не найдено, возвращаем пустую строку
            return "";
        }
    }
}
