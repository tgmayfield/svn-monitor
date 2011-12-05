using System;
using SVNMonitor.Entities;
using SVNMonitor.Settings;
using System.Collections.Generic;

namespace SVNMonitor.Helpers
{
public class SVNPathHelper
{
	public SVNPathHelper()
	{
	}

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
		if (path.Source.IsURL)
		{
			return true;
		}
		if (FileSystemHelper.IsUrl(path.FilePath))
		{
			return true;
		}
		return false;
	}

	internal static bool CanCommit(SVNPath path)
	{
		if (path == null)
		{
			return false;
		}
		if (ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled)
		{
			return true;
		}
		if (!path.Modified)
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
		return path != null;
	}

	internal static bool CanCopyRelativeURL(SVNPath path)
	{
		return path != null;
	}

	internal static bool CanCopyToClipboard(SVNPath path)
	{
		return path != null;
	}

	internal static bool CanCopyURL(SVNPath path)
	{
		return path != null;
	}

	internal static bool CanDiff(SVNPath path)
	{
		if (path == null)
		{
			return false;
		}
		if (path.Action != SVNAction.Modified && path.Action != SVNAction.Replaced)
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
		if (paths == null || paths.Count<SVNPath>() == 0)
		{
		}
		bool can = paths.All<SVNPath>(new Predicate<SVNPath>(sVNPathHelper.<CanMultiple>b__0));
		return can;
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
		return path != null;
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
		if (path.Source.IsURL || path.Unread)
		{
			return false;
		}
		return true;
	}

	internal static bool CanRunPathWizard(SVNPath path)
	{
		return path != null;
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

	internal sealed class SVNPathAction : MulticastDelegate
	{
		public SVNPathAction(object object, IntPtr method);

		public virtual IAsyncResult BeginInvoke(SVNPath path, AsyncCallback callback, object object);

		public virtual bool EndInvoke(IAsyncResult result);

		public virtual bool Invoke(SVNPath path);
	}
}
}