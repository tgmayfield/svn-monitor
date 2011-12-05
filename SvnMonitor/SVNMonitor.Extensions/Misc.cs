using System;

namespace SVNMonitor.Extensions
{
public static class Misc
{
	public static string FormatWith(this string format, object[] args)
	{
		return string.Format(format, args);
	}
}
}