namespace SVNMonitor.SVN
{
    using System;

    public enum TortoiseSVNAutoClose
    {
        NoAutoClose,
        IfNoErrors,
        IfNoErrorsAndConflicts,
        IfNoErrorsAndConflictsAndMerges,
        IfNoErrorsAndConflictsAndMergesForLocalOperations
    }
}

