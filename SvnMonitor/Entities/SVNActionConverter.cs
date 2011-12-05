using System;

using SharpSvn;

namespace SVNMonitor.Entities
{
	internal static class SVNActionConverter
	{
		public static SVNAction ToSVNAction(SvnChangeAction action)
		{
			SvnChangeAction tempAnotherLocal0 = action;
			if (tempAnotherLocal0 <= SvnChangeAction.Add)
			{
				if ((tempAnotherLocal0 != SvnChangeAction.None) && (tempAnotherLocal0 == SvnChangeAction.Add))
				{
					return SVNAction.Added;
				}
			}
			else
			{
				switch (tempAnotherLocal0)
				{
					case SvnChangeAction.Delete:
						return SVNAction.Deleted;

					case SvnChangeAction.Modify:
						return SVNAction.Modified;

					case SvnChangeAction.Replace:
						return SVNAction.Replaced;
				}
			}
			return SVNAction.Added;
		}
	}
}