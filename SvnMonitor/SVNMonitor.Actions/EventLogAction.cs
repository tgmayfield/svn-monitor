using SVNMonitor.Resources;
using System;
using System.Diagnostics;

namespace SVNMonitor.Actions
{
[ResourceProvider("Write an event log entry")]
[Serializable]
internal class EventLogAction : TextAppenderAction
{
	public bool IsValid
	{
		get
		{
			return true;
		}
	}

	public string SummaryInfo
	{
		get
		{
			return "Write updates summary to the event log.";
		}
	}

	public EventLogAction()
	{
	}

	protected override void AppendText(string text)
	{
		EventLog.WriteEntry("SVN-Monitor", text, EventLogEntryType.Information);
	}

	public override void RejectChanges()
	{
	}

	public override void SetRejectionPoint()
	{
	}
}
}