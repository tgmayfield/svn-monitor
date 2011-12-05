namespace SVNMonitor.Extensions
{
    using System;
    using System.Runtime.CompilerServices;

    public static class Misc
    {
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}

