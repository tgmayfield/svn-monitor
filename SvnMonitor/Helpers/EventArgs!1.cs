namespace SVNMonitor.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T item)
        {
            this.Item = item;
        }

		public T Item { get; private set; }
    }
}

