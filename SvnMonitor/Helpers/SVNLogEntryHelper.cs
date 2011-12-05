using System;

using SVNMonitor.Entities;

namespace SVNMonitor.Helpers
{
	public class SVNLogEntryHelper
	{
		internal static bool CanCopyAuthorToClipboard(SVNLogEntry entry)
		{
			if (entry == null)
			{
				return false;
			}
			return !string.IsNullOrEmpty(entry.Author);
		}

		internal static bool CanCopyMessageToClipboard(SVNLogEntry entry)
		{
			if (entry == null)
			{
				return false;
			}
			return !string.IsNullOrEmpty(entry.Message);
		}

		internal static bool CanCopyPaths(SVNLogEntry entry)
		{
			return (entry != null);
		}

		internal static bool CanCopyToClipboard(SVNLogEntry entry)
		{
			return (entry != null);
		}

		internal static bool CanDiff(SVNLogEntry entry)
		{
			return (entry != null);
		}

		internal static bool CanOpenSVNLog(SVNLogEntry entry)
		{
			return (entry != null);
		}

		internal static bool CanRecommend(SVNLogEntry entry)
		{
			if (entry == null)
			{
				return false;
			}
			if (!entry.Source.Enabled)
			{
				return false;
			}
			if (entry.Source.IsURL)
			{
				return false;
			}
			if (entry.Recommended)
			{
				return false;
			}
			if (entry.Source.IsFileURL)
			{
				return false;
			}
			if (!entry.Source.EnableRecommendations)
			{
				return false;
			}
			return true;
		}

		internal static bool CanRollback(SVNLogEntry entry)
		{
			if (entry == null)
			{
				return false;
			}
			if (entry.Source.IsURL)
			{
				return false;
			}
			if (entry.Unread)
			{
				return false;
			}
			return true;
		}

		internal static bool CanRunAuthorWizard(SVNLogEntry entry)
		{
			return (entry != null);
		}

		internal static bool CanShowDetails(SVNLogEntry entry)
		{
			return (entry != null);
		}

		internal static bool CanSVNUpdate(SVNLogEntry entry)
		{
			if (entry == null)
			{
				return false;
			}
			if (entry.Source.IsURL)
			{
				return false;
			}
			return true;
		}
	}
}