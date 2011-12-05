using System;

namespace SVNMonitor.Entities
{
	public enum StatusChangedReason
	{
		HasError,
		Enabled,
		Deleted,
		Updating,
		Updated,
		SVNCommit,
		SVNStatusChanged,
		SVNUpdate,
		SVNRevert,
		SVNSwitch,
		SVNRelocate,
		Refreshed,
		Recommended,
		SVNResolve,
		SVNMerge,
		SVNApplyPatch,
		SVNDeleteUnversioned
	}
}