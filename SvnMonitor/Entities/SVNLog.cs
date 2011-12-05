using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using SVNMonitor.Helpers;
using SVNMonitor.Settings;
using SVNMonitor.Logging;
using SVNMonitor.SVN;
using SVNMonitor.Resources.Text;
using System.Threading;

namespace SVNMonitor.Entities
{
[Serializable]
public class SVNLog : VersionEntity, IEnumerable<SVNLogEntry>, IEnumerable
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[NonSerialized]
	private Source source;

	internal bool IsCached
	{
		get
		{
			string fileName = this.Source.CacheFileName;
			bool fileExists = FileSystemHelper.FileExists(fileName);
			return fileExists;
		}
	}

	public List<SVNLogEntry> LogEntries
	{
		get;
		private set;
	}

	public List<string> PossibleConflictedFilePaths
	{
		get
		{
			List<string> list = new List<string>();
			SVNLogEntry[] enumerableLogEntries = this.GetEnumerableLogEntries();
			foreach (SVNLogEntry entry in enumerableLogEntries)
			{
				foreach (string path in entry.PossibleConflictedPaths)
				{
					if (!list.Contains(path))
					{
						list.Add(path);
					}
				}
			}
			return list;
		}
	}

	public Source Source
	{
		get
		{
			return this.source;
		}
		set
		{
			this.source = value;
			this.SetLogEntriesSource();
		}
	}

	public IEnumerable<SVNLogEntry> UnreadLogEntries
	{
		get
		{
			return this.GetEnumerableLogEntries().Where<SVNLogEntry>(new Predicate<SVNLogEntry>((e) => e.Unread));
		}
	}

	public SVNLog()
	{
		this.LogEntries = new List<SVNLogEntry>();
	}

	private void Compact()
	{
		int pageSize = ApplicationSettingsManager.Settings.LogEntriesPageSize;
		if (pageSize < 0)
		{
			return;
		}
		if (this.LogEntries.Count <= pageSize)
		{
			return;
		}
		Logger.Log.DebugFormat("From {0} to {1}", this.LogEntries.Count, pageSize);
		SVNLogEntry[] entries = new SVNLogEntry[pageSize];
		this.LogEntries.CopyTo(this.LogEntries.Count - pageSize, entries, 0, pageSize);
		this.LogEntries = new List<SVNLogEntry>(entries);
	}

	internal static SVNLog Create(Source source)
	{
		Logger.Log.DebugFormat("SVNFactory.GetLog(source={0})", source);
		SVNLog log = SVNFactory.GetLog(source);
		log.LogEntries.Sort();
		Logger.Log.Debug("log.Save");
		log.Save();
		return log;
	}

	internal void Delete()
	{
		this.Source.DeleteCache();
	}

	public SVNLogEntry[] GetEnumerableLogEntries()
	{
		SVNLogEntry[] array = new SVNLogEntry[this.LogEntries.Count];
		this.LogEntries.CopyTo(array);
		return array;
	}

	public IEnumerator GetEnumerator()
	{
		return this.LogEntries.GetEnumerator();
	}

	internal static long GetMaxRevision(IEnumerable<SVNLogEntry> list)
	{
		long max = (long)0;
		foreach (SVNLogEntry entry in list)
		{
			if (entry.Revision > max)
			{
				max = entry.Revision;
			}
		}
		return max;
	}

	internal static SVNLog Load(Source source, bool createIfNotExist)
	{
		object[] objArray;
		Logger.Log.DebugFormat("GetFileNameForSource(source={0}, createIfNotExist={1})", source, createIfNotExist);
		string fileName = source.CacheFileName;
		SVNLog log = null;
		bool fileExists = FileSystemHelper.FileExists(fileName);
		Logger.Log.DebugFormat("File {0} exists: {1}", fileName, fileExists);
		if (fileExists)
		{
			Logger.Log.DebugFormat("SerializationHelper.BinaryDeserialize(fileName={0})", fileName);
			log = (SVNLog)SerializationHelper.BinaryDeserialize(fileName);
			log.Source = source;
			ErrorHandler.Append(Strings.ErrorLoadingLogFile_FORMAT.FormatWith(new object[] { fileName }), source, ex);
		}
		try
		{
		}
		catch (Exception ex)
		{
		}
		if (log == null && createIfNotExist)
		{
			Logger.Log.DebugFormat("Create(source={0})", source);
			log = SVNLog.Create(source);
		}
		if (log != null)
		{
			Logger.Log.Debug("UpdateRevision");
			log.UpdateRevision();
		}
		return log;
	}

	protected virtual List<SVNLogEntry> Merge(SVNLog log)
	{
		List<SVNLogEntry> newEntries = new List<SVNLogEntry>();
		if (log.LogEntries.Count == 0)
		{
			return newEntries;
		}
		SVNLogEntry[] enumerableLogEntries = log.GetEnumerableLogEntries();
		foreach (SVNLogEntry entry in enumerableLogEntries)
		{
			if (!this.LogEntries.Contains(entry))
			{
				newEntries.Add(entry);
				this.LogEntries.Add(entry);
			}
		}
		newEntries.Sort();
		this.LogEntries.Sort();
		this.Compact();
		return newEntries;
	}

	internal virtual void Save()
	{
		ThreadHelper.Queue(new WaitCallback(this.Save), "SAVE_LOG");
	}

	private void Save(object state)
	{
		if (!this.Source.IsAlive)
		{
			Logger.Log.DebugFormat("This source is no longer alive. (Source={0})", this.Source);
			return;
		}
		Logger.Log.DebugFormat("GetFileNameForSource(Source={0})", this.Source);
		string fileName = this.Source.CacheFileName;
		Logger.Log.DebugFormat("BinarySerialize(fileName={0})", fileName);
		SerializationHelper.BinarySerialize(this, fileName);
	}

	private void SetLogEntriesSource()
	{
		SVNLogEntry[] enumerableLogEntries = this.GetEnumerableLogEntries();
		foreach (SVNLogEntry entry in enumerableLogEntries)
		{
			entry.Source = this.Source;
		}
	}

	private IEnumerator<SVNLogEntry> System.Collections.Generic.IEnumerable<SVNMonitor.Entities.SVNLogEntry>.GetEnumerator()
	{
		return this.LogEntries.GetEnumerator();
	}

	public override string ToString()
	{
		return string.Format("{0}: {1} Entries", this.Source.Name, this.LogEntries.Count);
	}

	internal virtual List<SVNLogEntry> Update()
	{
		SVNLog updates = SVNFactory.GetUpdates(this.Source);
		List<SVNLogEntry> newEntries = this.Merge(updates);
		if (newEntries.Count > 0)
		{
			Logger.Log.DebugFormat("newEntries={0}", newEntries.Count);
			this.UpdateRevision();
			this.Save();
		}
		return newEntries;
	}

	private void UpdateRevision()
	{
		long oldRevision = this.Source.Revision;
		this.Source.Revision = SVNLog.GetMaxRevision(this.LogEntries);
		Logger.Log.InfoFormat("Source:{0}, From:{1}, To:{2}", this.Source, oldRevision, this.Source.Revision);
	}
}
}