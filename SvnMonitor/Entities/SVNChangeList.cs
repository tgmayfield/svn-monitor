namespace SVNMonitor.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SVNChangeList
    {
        private List<SVNStatusEntry> entries = new List<SVNStatusEntry>();

        public SVNChangeList(string name)
        {
            this.Name = name;
        }

        internal void Add(SVNStatusEntry entry)
        {
            this.entries.Add(entry);
        }

        internal bool Contains(SVNStatusEntry entry)
        {
            return this.entries.Contains(entry);
        }

        public string Name { get; private set; }
    }
}

