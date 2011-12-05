using SVNMonitor.Entities;
using System.Collections.Generic;

namespace SVNMonitor.Helpers
{
internal static class SVNPathsExtension
{
	public static SVNPathCommands CreateCommands(this SVNPath path)
	{
		List<SVNPath> sVNPaths = new List<SVNPath>();
		sVNPaths.Add(path);
		return sVNPaths.CreateCommands();
	}

	public static SVNPathCommands CreateCommands(this IEnumerable<SVNPath> paths)
	{
		if (paths == null || paths.Count<SVNPath>() == 0)
		{
			return null;
		}
		return new SVNPathCommands(paths);
	}
}
}