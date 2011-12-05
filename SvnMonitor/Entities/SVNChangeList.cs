using System;
using System.Collections.Generic;

namespace SVNMonitor.Entities
{
	[Serializable]
	public class SVNChangeList
	{
		private readonly List<SVNStatusEntry> entries = new List<SVNStatusEntry>();

		public SVNChangeList(string name)
		{
			Name = name;
		}

		internal void Add(SVNStatusEntry entry)
		{
			entries.Add(entry);
		}

		internal bool Contains(SVNStatusEntry entry)
		{
			return entries.Contains(entry);
		}

		public string Name { get; private set; }
	}
}