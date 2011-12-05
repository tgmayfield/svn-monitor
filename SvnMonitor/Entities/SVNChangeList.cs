using System;
using System.Collections.Generic;

namespace SVNMonitor.Entities
{
[Serializable]
public class SVNChangeList
{
	private List<SVNStatusEntry> entries;

	public string Name
	{
		get;
		private set;
	}

	public SVNChangeList(string name)
	{
		this.entries = new List<SVNStatusEntry>();
		base();
		this.Name = name;
	}

	internal void Add(SVNStatusEntry entry)
	{
		this.entries.Add(entry);
	}

	internal bool Contains(SVNStatusEntry entry)
	{
		bool contains = this.entries.Contains(entry);
		return contains;
	}
}
}