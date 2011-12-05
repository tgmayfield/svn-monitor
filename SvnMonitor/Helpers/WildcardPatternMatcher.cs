using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace SVNMonitor.Helpers
{
public class WildcardPatternMatcher
{
	private static Dictionary<string, Wildcard> dict;

	static WildcardPatternMatcher()
	{
		WildcardPatternMatcher.dict = new Dictionary<string, Wildcard>();
	}

	public WildcardPatternMatcher()
	{
	}

	public static bool IsFileOrDirectoryMatch(string pattern, string fileName)
	{
		if (fileName.EndsWith("\\"))
		{
			return false;
		}
		if (!FileSystemHelper.Exists(fileName))
		{
			return false;
		}
		int index = fileName.LastIndexOf(92);
		string name = fileName.Substring(index + 1);
		bool isMatch = WildcardPatternMatcher.IsMatch(pattern, name);
		return isMatch;
	}

	public static bool IsMatch(string pattern, string input)
	{
		Wildcard wildcard;
		if (!WildcardPatternMatcher.dict.ContainsKey(pattern))
		{
			wildcard = new Wildcard(pattern, RegexOptions.IgnoreCase);
			WildcardPatternMatcher.dict.Add(pattern, wildcard);
		}
		wildcard = WildcardPatternMatcher.dict[pattern];
		bool isMatch = wildcard.IsMatch(input);
		return isMatch;
	}

	internal class Wildcard : Regex
	{
		public Wildcard(string pattern);

		public Wildcard(string pattern, RegexOptions options);

		public static string WildcardToRegex(string pattern);
	}
}
}