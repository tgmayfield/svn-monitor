using System;
using System.Diagnostics;

namespace SVNMonitor.Entities
{
public class StatusChangedEventArgs : EventArgs
{
	public UserEntity Entity
	{
		get;
		private set;
	}

	public StatusChangedReason Reason
	{
		get;
		private set;
	}

	[DebuggerNonUserCode]
	public StatusChangedEventArgs(UserEntity entity, StatusChangedReason reason)
	{
		this.Entity = entity;
		this.Reason = reason;
	}
}
}