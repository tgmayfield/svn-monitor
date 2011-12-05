using System;
using System.Collections.Generic;
using System.Linq;

using SVNMonitor.Entities;
using SVNMonitor.Settings;

namespace SVNMonitor.Helpers
{
	public class SVNPathHelper
	{
		internal static bool CanBlame(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if (path.Action == SVNAction.Deleted)
			{
				return false;
			}
			return true;
		}

		internal static bool CanBrowse(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			return (path.Source.IsURL || FileSystemHelper.IsUrl(path.FilePath));
		}

		internal static bool CanCommit(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if (!ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled && !path.Modified)
			{
				return false;
			}
			return true;
		}

		internal static bool CanCopyFullName(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			return true;
		}

		internal static bool CanCopyName(SVNPath path)
		{
			return (path != null);
		}

		internal static bool CanCopyRelativeURL(SVNPath path)
		{
			return (path != null);
		}

		internal static bool CanCopyToClipboard(SVNPath path)
		{
			return (path != null);
		}

		internal static bool CanCopyURL(SVNPath path)
		{
			return (path != null);
		}

		internal static bool CanDiff(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if ((path.Action != SVNAction.Modified) && (path.Action != SVNAction.Replaced))
			{
				return false;
			}
			return true;
		}

		internal static bool CanEdit(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if (!path.Source.PathExists)
			{
				return false;
			}
			if (!FileSystemHelper.FileExists(path.FilePath))
			{
				return false;
			}
			return true;
		}

		internal static bool CanExplore(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if (path.Source.IsURL)
			{
				return false;
			}
			if (!path.Source.PathExists)
			{
				return false;
			}
			if (!FileSystemHelper.Exists(path.FilePath))
			{
				return false;
			}
			return true;
		}

		internal static bool CanExport(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if (path.ExistsLocally)
			{
				return FileSystemHelper.FileExists(path.FilePath);
			}
			return true;
		}

		internal static bool CanMultiple(IEnumerable<SVNPath> paths, SVNPathAction action)
		{
			return (((paths != null) && (paths.Count() != 0)) && paths.All(p => action(p)));
		}

		internal static bool CanOpen(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if (!path.Source.PathExists)
			{
				return false;
			}
			if (!FileSystemHelper.Exists(path.FilePath))
			{
				return false;
			}
			return true;
		}

		internal static bool CanOpenSVNLog(SVNPath path)
		{
			return (path != null);
		}

		internal static bool CanOpenWith(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if (!path.Source.PathExists)
			{
				return false;
			}
			if (!FileSystemHelper.FileExists(path.FilePath))
			{
				return false;
			}
			return true;
		}

		internal static bool CanRevert(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			return path.Modified;
		}

		internal static bool CanRollback(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			return (!path.Source.IsURL && !path.Unread);
		}

		internal static bool CanRunPathWizard(SVNPath path)
		{
			return (path != null);
		}

		internal static bool CanSVNUpdate(SVNPath path)
		{
			if (path == null)
			{
				return false;
			}
			if (path.Source.IsURL)
			{
				return false;
			}
			return true;
		}

		internal delegate bool SVNPathAction(SVNPath path);
	}
}