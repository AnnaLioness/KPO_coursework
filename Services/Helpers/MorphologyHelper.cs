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
            word = Regex.Replace(word.ToLowerInvariant(), @"[^\p{L}]", "");
            word = word.ToLowerInvariant();
            // убираем простые окончания
            foreach (var suffix in new[] { "ами", "ями", "ами", "ах", "ях", "ов", "ев", "ин", "ом","ая", "ое", "ие", "ый", "ый", "ой", "ю", "и", "а", "я", "ы", "е", "о", "у" })
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

            var queryLemmas = GetLemmas(query).ToHashSet();

            var words = Regex.Matches(content, @"\w+|[^\w\s]")
                             .Cast<Match>()
                             .Select(m => m.Value)
                             .ToArray();

            for (int i = 0; i < words.Length; i++)
            {
                var lemma = SimpleStem(words[i]);
                if (queryLemmas.Contains(lemma))
                    words[i] = $"**{words[i]}**";
            }

            // делаем срез вокруг первого найденного слова
            int firstIndex = Array.FindIndex(words, w => w.Contains("**"));
            if (firstIndex == -1) return "";

            int start = Math.Max(0, firstIndex - snippetLength);
            int end = Math.Min(words.Length, firstIndex + snippetLength);

            return string.Join(" ", words.Skip(start).Take(end - start));
        }
    }
}
