using System;
using System.Diagnostics;

namespace SVNMonitor.Entities
{
	public class StatusChangedEventArgs : EventArgs
	{
		[DebuggerNonUserCode]
		public StatusChangedEventArgs(UserEntity entity, StatusChangedReason reason)
		{
			Entity = entity;
			Reason = reason;
		}

		public UserEntity Entity { get; private set; }

		public StatusChangedReason Reason { get; private set; }
	}
}