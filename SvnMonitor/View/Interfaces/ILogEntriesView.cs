using System;
using System.Collections.Generic;

using SVNMonitor.Entities;

namespace SVNMonitor.View.Interfaces
{
	public interface ILogEntriesView : ISelectableView<SVNLogEntry>
	{
		event EventHandler SelectionChanged;

		bool CanGetNextLogEntry(SVNLogEntry nextFrom);
		bool CanGetPreviousLogEntry(SVNLogEntry nextFrom);
		SVNLogEntry GetNextLogEntry(SVNLogEntry nextFrom);
		SVNLogEntry GetPreviousLogEntry(SVNLogEntry nextFrom);

		List<SVNLogEntry> LogEntries { get; set; }

		bool SelectedWithKeyboard { get; }
	}
}