namespace SVNMonitor.SVN
{
public enum TortoiseSVNAutoClose
{
	IfNoErrors
	,
	IfNoErrorsAndConflicts
	,
	IfNoErrorsAndConflictsAndMerges
	,
	IfNoErrorsAndConflictsAndMergesForLocalOperations
	,
	NoAutoClose

}
}