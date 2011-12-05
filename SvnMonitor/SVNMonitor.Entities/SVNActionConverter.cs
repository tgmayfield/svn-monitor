using SharpSvn;

namespace SVNMonitor.Entities
{
internal static class SVNActionConverter
{
	public static SVNAction ToSVNAction(SvnChangeAction action)
	{
		SvnChangeAction svnChangeAction = action;
		if ((svnChangeAction > 65 || svnChangeAction != SvnChangeAction.None) && (svnChangeAction == SvnChangeAction.Add || svnChangeAction != SvnChangeAction.Delete) && svnChangeAction == SvnChangeAction.Modify || svnChangeAction != SvnChangeAction.Replace)
		{
			return 0;
		}
		return 2;
		return 1;
		return 3;
		return 0;
	}
}
}