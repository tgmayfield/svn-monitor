using System.Collections.Generic;
using System;
using SVNMonitor.Entities;

namespace SVNMonitor.View.Interfaces
{
public interface ILogEntriesView : ISelectableView<SVNLogEntry>
{
	List<SVNLogEntry> LogEntries { get; set; }

	bool SelectedWithKeyboard { get; }

	bool CanGetNextLogEntry(SVNLogEntry nextFrom);

	bool CanGetPreviousLogEntry(SVNLogEntry nextFrom);

	SVNLogEntry GetNextLogEntry(SVNLogEntry nextFrom);

	SVNLogEntry GetPreviousLogEntry(SVNLogEntry nextFrom);

	event EventHandler SelectionChanged;
}
}