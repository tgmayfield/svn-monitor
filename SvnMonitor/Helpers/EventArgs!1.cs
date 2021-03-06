﻿using System;

namespace SVNMonitor.Helpers
{
	public class EventArgs<T> : EventArgs
	{
		public EventArgs(T item)
		{
			Item = item;
		}

		public T Item { get; private set; }
	}
}