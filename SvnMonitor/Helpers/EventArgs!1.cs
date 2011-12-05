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

        public T Item
        {
            [CompilerGenerated]
            get
            {
                return this.<Item>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<Item>k__BackingField = value;
            }
        }
    }
}

