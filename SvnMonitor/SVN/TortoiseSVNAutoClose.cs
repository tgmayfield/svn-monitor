using System;

namespace SVNMonitor.SVN
{
	public enum TortoiseSVNAutoClose
	{
		NoAutoClose,
		IfNoErrors,
		IfNoErrorsAndConflicts,
		IfNoErrorsAndConflictsAndMerges,
		IfNoErrorsAndConflictsAndMergesForLocalOperations
	}
}