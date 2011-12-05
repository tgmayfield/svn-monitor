namespace SVNMonitor.Entities
{
    using SVNMonitor.Helpers;
    using SVNMonitor.SVN;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SVNStatus
    {
        private List<SVNStatusEntry> entries;
        private Dictionary<string, SVNStatusEntry> map;
        private List<SVNStatusEntry> modifiedEntries;
        private List<SVNStatusEntry> unversionedEntries;

        public SVNStatus()
        {
            this.Entries = new List<SVNStatusEntry>();
            this.Map = new Dictionary<string, SVNStatusEntry>();
        }

        private void AddToMap(string key, SVNStatusEntry entry)
        {
            if (!this.Map.ContainsKey(key))
            {
                this.Map.Add(key, entry);
            }
        }

        internal bool Contains(string pathOrUri)
        {
            return this.Map.ContainsKey(pathOrUri);
        }

        private void Count()
        {
            this.modifiedEntries = new List<SVNStatusEntry>();
            this.unversionedEntries = new List<SVNStatusEntry>();
            foreach (SVNStatusEntry entry in this.GetEnumerableStatusEntries())
            {
                if (entry.Modified)
                {
                    this.modifiedEntries.Add(entry);
                }
                if (entry.Unversioned)
                {
                    this.unversionedEntries.Add(entry);
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
            if (this.Map != null)
            {
                this.Map = new Dictionary<string, SVNStatusEntry>();
            }
            if (this.entries != null)
            {
                foreach (SVNStatusEntry entry in this.entries)
                {
                    this.AddToMap(entry.Uri, entry);
                    if (entry.Uri.EndsWith("/"))
                    {
                        string uriWithoutSlash = entry.Uri.Remove(entry.Uri.Length - 1);
                        this.AddToMap(uriWithoutSlash, entry);
                    }
                    if (entry.Path != null)
                    {
                        this.AddToMap(entry.Path, entry);
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
            SVNStatusEntry[] array = new SVNStatusEntry[this.Entries.Count];
            this.Entries.CopyTo(array);
            return array;
        }

        public bool IsIgnoreOnCommit(string pathOrUri)
        {
            if (pathOrUri == null)
            {
                return false;
            }
            if (!this.Map.ContainsKey(pathOrUri))
            {
                return false;
            }
            SVNStatusEntry entry = this.Map[pathOrUri];
            return entry.IgnoreOnCommit;
        }

        public bool IsModified(string pathOrUri, bool forConflict)
        {
            if (pathOrUri == null)
            {
                return false;
            }
            if (!this.Map.ContainsKey(pathOrUri))
            {
                return false;
            }
            SVNStatusEntry entry = this.Map[pathOrUri];
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
            if (!this.Map.ContainsKey(pathOrUri))
            {
                return false;
            }
            SVNStatusEntry entry = this.Map[pathOrUri];
            return entry.ModifiedOrUnversionedNoIgnore;
        }

        public bool IsUnversioned(string pathOrUri)
        {
            if (pathOrUri == null)
            {
                return false;
            }
            if (!this.Map.ContainsKey(pathOrUri))
            {
                return false;
            }
            SVNStatusEntry entry = this.Map[pathOrUri];
            return entry.Unversioned;
        }

        public SortedList<string, SVNChangeList> ChangeLists
        {
            get
            {
                SortedList<string, SVNChangeList> changeLists = new SortedList<string, SVNChangeList>();
                foreach (SVNStatusEntry entry in this.Entries)
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
            get
            {
                return this.entries;
            }
            internal set
            {
                this.entries = value;
                this.CreateMap();
                this.Count();
            }
        }

        internal SVNStatusEntry this[string pathOrUri]
        {
            get
            {
                if (this.Map.ContainsKey(pathOrUri))
                {
                    return this.Map[pathOrUri];
                }
                return null;
            }
        }

        private Dictionary<string, SVNStatusEntry> Map
        {
            [DebuggerNonUserCode]
            get
            {
                return this.map;
            }
            [DebuggerNonUserCode]
            set
            {
                this.map = value;
            }
        }

        public List<SVNStatusEntry> ModifiedEntries
        {
            get
            {
                if (this.modifiedEntries == null)
                {
                    this.Count();
                }
                return this.modifiedEntries;
            }
        }

        public SVNMonitor.Entities.Source Source { get; set; }

        public List<SVNStatusEntry> UnversionedEntries
        {
            get
            {
                if (this.unversionedEntries == null)
                {
                    this.Count();
                }
                return this.unversionedEntries;
            }
        }

        internal enum StatusCreationOption
        {
            LocalOnly,
            LocalAndRemote
        }
    }
}

