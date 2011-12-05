using System;

namespace SVNMonitor
{
	public enum EventLogEntryType
	{
		Error,
		Info,
		Warning,
		Monitor,
		CheckingUpdates,
		AvailableUpdates,
		System,
		Source,
		Conflict,
		Recommended
	}
}