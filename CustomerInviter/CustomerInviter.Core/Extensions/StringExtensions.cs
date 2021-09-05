using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CustomerInviter.Core.Extensions
{
    public static class StringExtensions
    {
        private class Rule
        {
            private readonly Regex _regex;
            private readonly string _replacement;

            public Rule(string pattern, string replacement)
            {
                _regex = new Regex(pattern, RegexOptions.IgnoreCase);
                _replacement = replacement;
            }

            public string Apply(string word)
            {
                if (!_regex.IsMatch(word))
                    return null;

                return _regex.Replace(word, _replacement);
            }
        }

        public static string ToLowerCaseFirstLetter(this string s)
        {
            return char.ToLower(s[0]) + s.Substring(1);
        }

        public static string CapitalizeIfUppercase(this string s)
        {
            return s.Length > 1 && s == s.ToUpper() ? s.Capitalize() : s;
        }
        public static string Replace(this string originalString, string oldValue, string newValue, StringComparison comparisonType)
        {
            var startIndex = 0;
            while (true)
            {
                startIndex = originalString.IndexOf(oldValue, startIndex, comparisonType);
                if (startIndex == -1)
                    break;

                originalString = originalString.Substring(0, startIndex) + newValue + originalString.Substring(startIndex + oldValue.Length);

                startIndex += newValue.Length;
            }

            return originalString;
        }

        private static void AddIrregular(string singular, string plural)
        {
            AddPlural("(" + singular[0] + ")" + singular.Substring(1) + "$", "$1" + plural.Substring(1));
            AddSingular("(" + plural[0] + ")" + plural.Substring(1) + "$", "$1" + singular.Substring(1));
        }

        private static void AddUncountable(string word)
        {
            _uncountables.Add(word.ToLower());
        }

        private static void AddPlural(string rule, string replacement)
        {
            _plurals.Add(new Rule(rule, replacement));
        }

        private static void AddSingular(string rule, string replacement)
        {
            _singulars.Add(new Rule(rule, replacement));
        }

        private static readonly List<Rule> _plurals = new List<Rule>();
        private static readonly List<Rule> _singulars = new List<Rule>();
        private static readonly List<string> _uncountables = new List<string>();

        public static string Pluralize(this string word)
        {
            return ApplyRules(_plurals, word);
        }

        public static string Singularize(this string word)
        {
            return ApplyRules(_singulars, word);
        }

#if NET45 || NETFX_CORE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static string ApplyRules(List<Rule> rules, string word)
        {
            var result = word;

            if (!_uncountables.Contains(word.ToLower()))
                for (var i = rules.Count - 1; i >= 0; i--)
                    if ((result = rules[i].Apply(word)) != null)
                        break;

            return result;
        }

        public static string Titleize(this string word)
        {
            return Regex.Replace(Humanize(Underscore(word)), @"\b([a-z])",
                delegate(Match match) { return match.Captures[0].Value.ToUpper(); });
        }

        public static string Humanize(this string lowercaseAndUnderscoredWord)
        {
            return Capitalize(Regex.Replace(lowercaseAndUnderscoredWord, @"_", " "));
        }

        public static string Pascalize(this string lowercaseAndUnderscoredWord)
        {
            return Regex.Replace(lowercaseAndUnderscoredWord, "(?:^|_)(.)",
                delegate(Match match) { return match.Groups[1].Value.ToUpper(); });
        }

        public static string Camelize(this string lowercaseAndUnderscoredWord)
        {
            return Uncapitalize(Pascalize(lowercaseAndUnderscoredWord));
        }

        public static string Underscore(this string pascalCasedWord)
        {
            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(pascalCasedWord, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])",
                    "$1_$2"), @"[-\s]", "_").ToLower();
        }

        public static string Capitalize(this string word)
        {
            return word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
        }

        public static string Uncapitalize(this string word)
        {
            return word.Substring(0, 1).ToLower() + word.Substring(1);
        }

        public static string Ordinalize(this string numberString)
        {
            return Ordanize(int.Parse(numberString), numberString);
        }

        public static string Ordinalize(this int number)
        {
            return Ordanize(number, number.ToString());
        }

#if NET45 || NETFX_CORE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static string Ordanize(int number, string numberString)
        {
            var nMod100 = number % 100;

            if (nMod100 >= 11 && nMod100 <= 13)
                return numberString + "th";

            switch (number % 10)
            {
                case 1:
                    return numberString + "st";
                case 2:
                    return numberString + "nd";
                case 3:
                    return numberString + "rd";
                default:
                    return numberString + "th";
            }
        }


        public static string Dasherize(this string underscoredWord)
        {
            return underscoredWord.Replace('_', '-');
        }
    }
}