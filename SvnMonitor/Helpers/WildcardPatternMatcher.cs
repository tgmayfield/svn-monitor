namespace SVNMonitor.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class WildcardPatternMatcher
    {
        private static Dictionary<string, Wildcard> dict = new Dictionary<string, Wildcard>();

        public static bool IsFileOrDirectoryMatch(string pattern, string fileName)
        {
            if (fileName.EndsWith(@"\"))
            {
                return false;
            }
            if (!FileSystemHelper.Exists(fileName))
            {
                return false;
            }
            int index = fileName.LastIndexOf('\\');
            string name = fileName.Substring(index + 1);
            return IsMatch(pattern, name);
        }

        public static bool IsMatch(string pattern, string input)
        {
            Wildcard wildcard;
            if (!dict.ContainsKey(pattern))
            {
                wildcard = new Wildcard(pattern, RegexOptions.IgnoreCase);
                dict.Add(pattern, wildcard);
            }
            wildcard = dict[pattern];
            return wildcard.IsMatch(input);
        }

        internal class Wildcard : Regex
        {
            public Wildcard(string pattern) : base(WildcardToRegex(pattern))
            {
            }

            public Wildcard(string pattern, RegexOptions options) : base(WildcardToRegex(pattern), options)
            {
            }

            public static string WildcardToRegex(string pattern)
            {
                return ("^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$");
            }
        }
    }
}

