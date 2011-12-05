using System;

namespace SVNMonitor.Helpers
{
	public enum UserAction
	{
		Explore,
		Browse,
		Open,
		OpenWith,
		Diff,
		DiffLocalWithBase,
		DiffWithPrevious,
		Blame,
		ShowLog,
		Update,
		Rollback,
		Commit,
		Revert,
		Edit,
		SaveRevision
	}
}