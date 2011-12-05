namespace SVNMonitor.Actions
{
    using SVNMonitor.Resources;
    using System;
    using System.Diagnostics;

    [Serializable, ResourceProvider("Write an event log entry")]
    internal class EventLogAction : TextAppenderAction
    {
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

        public override bool IsValid
        {
            get
            {
                return true;
            }
        }

        public override string SummaryInfo
        {
            get
            {
                return "Write updates summary to the event log.";
            }
        }
    }
}

