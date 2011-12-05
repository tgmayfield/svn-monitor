using System;
using System.Linq;

using SVNMonitor.Entities;
using SVNMonitor.Settings;

namespace SVNMonitor.Helpers
{
	public class SourceHelper
	{
		internal static bool CanAdditionalTortoiseCommands(Source source)
		{
			return (source != null);
		}

		internal static bool CanApplyPatch(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanBranchTag(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanCheckForUpdates(Source source)
		{
			return (((source != null) && source.Enabled) && !source.Updating);
		}

		internal static bool CanCheckout(Source source)
		{
			return (source != null);
		}

		internal static bool CanCleanUp(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanClearAllErrors()
		{
			return MonitorSettings.Instance.GetEnumerableSources().Any(s => s.HasError);
		}

		internal static bool CanClearError(Source source)
		{
			return ((source != null) && source.HasError);
		}

		internal static bool CanCopyConflictedItems(Source source)
		{
			if (source == null)
			{
				return false;
			}
			return (source.PossibleConflictedFilePathsCount > 0);
		}

		internal static bool CanCopyError(Source source)
		{
			return ((source != null) && source.HasError);
		}

		internal static bool CanCopyModifiedItems(Source source)
		{
			if (source == null)
			{
				return false;
			}
			return (source.ModifiedCount > 0);
		}

		internal static bool CanCopyName(Source source)
		{
			return (source != null);
		}

		internal static bool CanCopyPath(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanCopyToClipboard(Source source)
		{
			return (source != null);
		}

		internal static bool CanCopyUnversionedItems(Source source)
		{
			if (source == null)
			{
				return false;
			}
			return (source.UnversionedCount > 0);
		}

		internal static bool CanCopyURL(Source source)
		{
			if (source == null)
			{
				return false;
			}
			if (!source.IsURL && (source.GetInfo(false) == null))
			{
				return false;
			}
			return true;
		}

		internal static bool CanCreatePatch(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanDeleteUnversioned(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanEnable(Source source)
		{
			return (source != null);
		}

		internal static bool CanExplore(Source source)
		{
			return (source != null);
		}

		internal static bool CanExport(Source source)
		{
			return (source != null);
		}

		internal static bool CanGetLock(Source source)
		{
			return false;
		}

		internal static bool CanMerge(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanProperties(Source source)
		{
			return (source != null);
		}

		internal static bool CanRefreshLog(Source source)
		{
			return (((source != null) && source.Enabled) && !source.Updating);
		}

		internal static bool CanReintegrate(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanReleaseLock(Source source)
		{
			return false;
		}

		internal static bool CanRelocate(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanRepoBrowser(Source source)
		{
			return (source != null);
		}

		internal static bool CanResolve(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanRevisionGraph(Source source)
		{
			return (source != null);
		}

		internal static bool CanRunWizard(Source source)
		{
			return (source != null);
		}

		internal static bool CanShowSVNLog(Source source)
		{
			return (source != null);
		}

		internal static bool CanSVNCheckForModifications(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanSVNCommit(Source source)
		{
			return (ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled || ((source != null) && source.HasLocalChanges));
		}

		internal static bool CanSVNRevert(Source source)
		{
			return ((source != null) && source.HasLocalVersionedChanges);
		}

		internal static bool CanSVNUpdate(Source source)
		{
			return ((source != null) && !source.IsURL);
		}

		internal static bool CanSwitch(Source source)
		{
			return ((source != null) && !source.IsURL);
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