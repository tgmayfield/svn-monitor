using System;

namespace SVNMonitor.Helpers
{
public class EventArgs<T> : EventArgs
{
	public T Item
	{
		get
		{
			return this.<Item>k__BackingField;
		}
		private set
		{
			this.<Item>k__BackingField = value;
		}
	}

	public EventArgs(T item)
	{
		base.Item = item;
	}
}
}