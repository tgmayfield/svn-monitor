using System;
using SVNMonitor.Entities;
using SVNMonitor;
using SVNMonitor.Settings;

namespace SVNMonitor.Helpers
{
public class SourceHelper
{
	public SourceHelper()
	{
	}

	internal static bool CanAdditionalTortoiseCommands(Source source)
	{
		return source != null;
	}

	internal static bool CanApplyPatch(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanBranchTag(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanCheckForUpdates(Source source)
	{
		if (source != null && source.Enabled)
		{
			return !source.Updating;
		}
		return false;
	}

	internal static bool CanCheckout(Source source)
	{
		return source != null;
	}

	internal static bool CanCleanUp(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanClearAllErrors()
	{
		bool hasError = MonitorSettings.Instance.GetEnumerableSources().Any<Source>(new Predicate<Source>((s) => s.HasError));
		return hasError;
	}

	internal static bool CanClearError(Source source)
	{
		if (source != null)
		{
			return source.HasError;
		}
		return false;
	}

	internal static bool CanCopyConflictedItems(Source source)
	{
		if (source == null)
		{
			return false;
		}
		return source.PossibleConflictedFilePathsCount > 0;
	}

	internal static bool CanCopyError(Source source)
	{
		if (source != null)
		{
			return source.HasError;
		}
		return false;
	}

	internal static bool CanCopyModifiedItems(Source source)
	{
		if (source == null)
		{
			return false;
		}
		return source.ModifiedCount > 0;
	}

	internal static bool CanCopyName(Source source)
	{
		return source != null;
	}

	internal static bool CanCopyPath(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanCopyToClipboard(Source source)
	{
		return source != null;
	}

	internal static bool CanCopyUnversionedItems(Source source)
	{
		if (source == null)
		{
			return false;
		}
		return source.UnversionedCount > 0;
	}

	internal static bool CanCopyURL(Source source)
	{
		if (source == null)
		{
			return false;
		}
		if (source.IsURL)
		{
			return true;
		}
		SVNInfo info = source.GetInfo(false);
		if (info == null)
		{
			return false;
		}
		return true;
	}

	internal static bool CanCreatePatch(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanDeleteUnversioned(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanEnable(Source source)
	{
		return source != null;
	}

	internal static bool CanExplore(Source source)
	{
		return source != null;
	}

	internal static bool CanExport(Source source)
	{
		return source != null;
	}

	internal static bool CanGetLock(Source source)
	{
		return false;
	}

	internal static bool CanMerge(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanProperties(Source source)
	{
		return source != null;
	}

	internal static bool CanRefreshLog(Source source)
	{
		if (source != null && source.Enabled)
		{
			return !source.Updating;
		}
		return false;
	}

	internal static bool CanReintegrate(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanReleaseLock(Source source)
	{
		return false;
	}

	internal static bool CanRelocate(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanRepoBrowser(Source source)
	{
		return source != null;
	}

	internal static bool CanResolve(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanRevisionGraph(Source source)
	{
		return source != null;
	}

	internal static bool CanRunWizard(Source source)
	{
		return source != null;
	}

	internal static bool CanShowSVNLog(Source source)
	{
		return source != null;
	}

	internal static bool CanSVNCheckForModifications(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanSVNCommit(Source source)
	{
		if (ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled)
		{
			return true;
		}
		if (source != null)
		{
			return source.HasLocalChanges;
		}
		return false;
	}

	internal static bool CanSVNRevert(Source source)
	{
		if (source != null)
		{
			return source.HasLocalVersionedChanges;
		}
		return false;
	}

	internal static bool CanSVNUpdate(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanSwitch(Source source)
	{
		if (source != null)
		{
			return !source.IsURL;
		}
		return false;
	}

	internal static bool CanTSVNHelp(Source source)
	{
		return true;
	}

	internal static bool CanTSVNSettings(Source source)
	{
		return true;
	}
}
}