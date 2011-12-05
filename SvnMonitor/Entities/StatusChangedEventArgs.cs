namespace SVNMonitor.Entities
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class StatusChangedEventArgs : EventArgs
    {
        [DebuggerNonUserCode]
        public StatusChangedEventArgs(UserEntity entity, StatusChangedReason reason)
        {
            this.Entity = entity;
            this.Reason = reason;
        }

        public UserEntity Entity { get; private set; }

        public StatusChangedReason Reason { get; private set; }
    }
}

