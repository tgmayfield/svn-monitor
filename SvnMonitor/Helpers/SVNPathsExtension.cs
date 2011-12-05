using System;
using System.Collections.Generic;
using System.Linq;

using SVNMonitor.Entities;

namespace SVNMonitor.Helpers
{
	internal static class SVNPathsExtension
	{
		public static SVNPathCommands CreateCommands(this SVNPath path)
		{
			return new List<SVNPath>
			{
				path
			}.CreateCommands();
		}

		public static SVNPathCommands CreateCommands(this IEnumerable<SVNPath> paths)
		{
			if ((paths != null) && (paths.Count() != 0))
			{
				return new SVNPathCommands(paths);
			}
			return null;
		}
	}
}