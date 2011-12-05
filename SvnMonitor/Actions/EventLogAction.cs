using System;

using SVNMonitor.Resources;

namespace SVNMonitor.Actions
{
	[Serializable, ResourceProvider("Write an event log entry")]
	internal class EventLogAction : TextAppenderAction
	{
		protected override void AppendText(string text)
		{
			System.Diagnostics.EventLog.WriteEntry("SVN-Monitor", text, System.Diagnostics.EventLogEntryType.Information);
		}

		public override void RejectChanges()
		{
		}

		public override void SetRejectionPoint()
		{
		}

		public override bool IsValid
		{
			get { return true; }
		}

		public override string SummaryInfo
		{
			get { return "Write updates summary to the event log."; }
		}
	}
}