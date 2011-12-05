namespace SVNMonitor
{
    using System;

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

