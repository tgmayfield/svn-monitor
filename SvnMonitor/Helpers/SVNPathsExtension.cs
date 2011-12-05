namespace SVNMonitor.Helpers
{
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class SVNPathsExtension
    {
        public static SVNPathCommands CreateCommands(this SVNPath path)
        {
            return new List<SVNPath> { path }.CreateCommands();
        }

        public static SVNPathCommands CreateCommands(this IEnumerable<SVNPath> paths)
        {
            if ((paths != null) && (paths.Count<SVNPath>() != 0))
            {
                return new SVNPathCommands(paths);
            }
            return null;
        }
    }
}

