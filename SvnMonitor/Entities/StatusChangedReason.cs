namespace SVNMonitor.Entities
{
    using System;

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

