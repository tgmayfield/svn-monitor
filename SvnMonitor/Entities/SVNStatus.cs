using System;
using System.Collections.Generic;
using System.Diagnostics;

using SVNMonitor.Helpers;
using SVNMonitor.SVN;

namespace SVNMonitor.Entities
{
	[Serializable]
	public class SVNStatus
	{
		private List<SVNStatusEntry> entries;
		private Dictionary<string, SVNStatusEntry> map;
		private List<SVNStatusEntry> modifiedEntries;
		private List<SVNStatusEntry> unversionedEntries;

		public SVNStatus()
		{
			Entries = new List<SVNStatusEntry>();
			Map = new Dictionary<string, SVNStatusEntry>();
		}

		private void AddToMap(string key, SVNStatusEntry entry)
		{
			if (!Map.ContainsKey(key))
			{
				Map.Add(key, entry);
			}
		}

		internal bool Contains(string pathOrUri)
		{
			return Map.ContainsKey(pathOrUri);
		}

		private void Count()
		{
			modifiedEntries = new List<SVNStatusEntry>();
			unversionedEntries = new List<SVNStatusEntry>();
			foreach (SVNStatusEntry entry in GetEnumerableStatusEntries())
			{
				if (entry.Modified)
				{
					modifiedEntries.Add(entry);
				}
				if (entry.Unversioned)
				{
					unversionedEntries.Add(entry);
				}
			}
		}

		internal static SVNStatus Create(SVNMonitor.Entities.Source source, StatusCreationOption option)
		{
			try
			{
				bool getRemote = option == StatusCreationOption.LocalAndRemote;
				return SVNFactory.GetStatus(source, getRemote);
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleEntityException(source, ex);
			}
			return Empty();
		}

		private void CreateMap()
		{
			if (Map != null)
			{
				Map = new Dictionary<string, SVNStatusEntry>();
			}
			if (entries != null)
			{
				foreach (SVNStatusEntry entry in entries)
				{
					AddToMap(entry.Uri, entry);
					if (entry.Uri.EndsWith("/"))
					{
						string uriWithoutSlash = entry.Uri.Remove(entry.Uri.Length - 1);
						AddToMap(uriWithoutSlash, entry);
					}
					if (entry.Path != null)
					{
						AddToMap(entry.Path, entry);
					}
				}
			}
		}

		internal static SVNStatus Empty()
		{
			return new SVNStatus();
		}

		internal SVNStatusEntry[] GetEnumerableStatusEntries()
		{
			SVNStatusEntry[] array = new SVNStatusEntry[Entries.Count];
			Entries.CopyTo(array);
			return array;
		}

		public bool IsIgnoreOnCommit(string pathOrUri)
		{
			if (pathOrUri == null)
			{
				return false;
			}
			if (!Map.ContainsKey(pathOrUri))
			{
				return false;
			}
			SVNStatusEntry entry = Map[pathOrUri];
			return entry.IgnoreOnCommit;
		}

		public bool IsModified(string pathOrUri, bool forConflict)
		{
			if (pathOrUri == null)
			{
				return false;
			}
			if (!Map.ContainsKey(pathOrUri))
			{
				return false;
			}
			SVNStatusEntry entry = Map[pathOrUri];
			if (forConflict)
			{
				return entry.ModifiedOrUnversionedForConflict;
			}
			return entry.ModifiedOrUnversioned;
		}

		public bool IsModifiedNoIgnore(string pathOrUri)
		{
			if (pathOrUri == null)
			{
				return false;
			}
			if (!Map.ContainsKey(pathOrUri))
			{
				return false;
			}
			SVNStatusEntry entry = Map[pathOrUri];
			return entry.ModifiedOrUnversionedNoIgnore;
		}

		public bool IsUnversioned(string pathOrUri)
		{
			if (pathOrUri == null)
			{
				return false;
			}
			if (!Map.ContainsKey(pathOrUri))
			{
				return false;
			}
			SVNStatusEntry entry = Map[pathOrUri];
			return entry.Unversioned;
		}

		public SortedList<string, SVNChangeList> ChangeLists
		{
			get
			{
				SortedList<string, SVNChangeList> changeLists = new SortedList<string, SVNChangeList>();
				foreach (SVNStatusEntry entry in Entries)
				{
					string name = "(no changelist)";
					if (entry.ChangeList != null)
					{
						name = entry.ChangeList;
					}
					if (!changeLists.ContainsKey(name))
					{
						SVNChangeList changeList = new SVNChangeList(name);
						changeLists.Add(name, changeList);
					}
					changeLists[name].Add(entry);
				}
				return changeLists;
			}
		}

		public List<SVNStatusEntry> Entries
		{
			[DebuggerNonUserCode]
			get { return entries; }
			internal set
			{
				entries = value;
				CreateMap();
				Count();
			}
		}

		internal SVNStatusEntry this[string pathOrUri]
		{
			get
			{
				if (Map.ContainsKey(pathOrUri))
				{
					return Map[pathOrUri];
				}
				return null;
			}
		}

		private Dictionary<string, SVNStatusEntry> Map
		{
			[DebuggerNonUserCode]
			get { return map; }
			[DebuggerNonUserCode]
			set { map = value; }
		}

		public List<SVNStatusEntry> ModifiedEntries
		{
			get
			{
				if (modifiedEntries == null)
				{
					Count();
				}
				return modifiedEntries;
			}
		}

		public SVNMonitor.Entities.Source Source { get; set; }

		public List<SVNStatusEntry> UnversionedEntries
		{
			get
			{
				if (unversionedEntries == null)
				{
					Count();
				}
				return unversionedEntries;
			}
		}

		internal enum StatusCreationOption
		{
			LocalOnly,
			LocalAndRemote
		}
	}
}