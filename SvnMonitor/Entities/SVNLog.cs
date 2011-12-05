using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;

namespace SVNMonitor.Entities
{
	[Serializable]
	public class SVNLog : VersionEntity, IEnumerable<SVNLogEntry>, IEnumerable
	{
		[NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SVNMonitor.Entities.Source source;

		public SVNLog()
		{
			LogEntries = new List<SVNLogEntry>();
		}

		private void Compact()
		{
			int pageSize = ApplicationSettingsManager.Settings.LogEntriesPageSize;
			if ((pageSize >= 0) && (LogEntries.Count > pageSize))
			{
				Logger.Log.DebugFormat("From {0} to {1}", LogEntries.Count, pageSize);
				SVNLogEntry[] entries = new SVNLogEntry[pageSize];
				LogEntries.CopyTo(LogEntries.Count - pageSize, entries, 0, pageSize);
				LogEntries = new List<SVNLogEntry>(entries);
			}
		}

		internal static SVNLog Create(SVNMonitor.Entities.Source source)
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
			Source.DeleteCache();
		}

		public SVNLogEntry[] GetEnumerableLogEntries()
		{
			SVNLogEntry[] array = new SVNLogEntry[LogEntries.Count];
			LogEntries.CopyTo(array);
			return array;
		}

		public IEnumerator GetEnumerator()
		{
			return LogEntries.GetEnumerator();
		}

		internal static long GetMaxRevision(IEnumerable<SVNLogEntry> list)
		{
			long max = 0L;
			foreach (SVNLogEntry entry in list)
			{
				if (entry.Revision > max)
				{
					max = entry.Revision;
				}
			}
			return max;
		}

		internal static SVNLog Load(SVNMonitor.Entities.Source source, bool createIfNotExist)
		{
			Logger.Log.DebugFormat("GetFileNameForSource(source={0}, createIfNotExist={1})", source, createIfNotExist);
			string fileName = source.CacheFileName;
			SVNLog log = null;
			bool fileExists = FileSystemHelper.FileExists(fileName);
			Logger.Log.DebugFormat("File {0} exists: {1}", fileName, fileExists);
			if (fileExists)
			{
				try
				{
					Logger.Log.DebugFormat("SerializationHelper.BinaryDeserialize(fileName={0})", fileName);
					log = (SVNLog)SerializationHelper.BinaryDeserialize(fileName);
					log.Source = source;
				}
				catch (Exception ex)
				{
					ErrorHandler.Append(Strings.ErrorLoadingLogFile_FORMAT.FormatWith(new object[]
					{
						fileName
					}), source, ex);
				}
			}
			if ((log == null) && createIfNotExist)
			{
				Logger.Log.DebugFormat("Create(source={0})", source);
				log = Create(source);
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
			if (log.LogEntries.Count != 0)
			{
				foreach (SVNLogEntry entry in log.GetEnumerableLogEntries())
				{
					if (!LogEntries.Contains(entry))
					{
						newEntries.Add(entry);
						LogEntries.Add(entry);
					}
				}
				newEntries.Sort();
				LogEntries.Sort();
				Compact();
			}
			return newEntries;
		}

		internal virtual void Save()
		{
			SVNMonitor.Helpers.ThreadHelper.Queue(Save, "SAVE_LOG");
		}

		private void Save(object state)
		{
			if (!Source.IsAlive)
			{
				Logger.Log.DebugFormat("This source is no longer alive. (Source={0})", Source);
			}
			else
			{
				Logger.Log.DebugFormat("GetFileNameForSource(Source={0})", Source);
				string fileName = Source.CacheFileName;
				Logger.Log.DebugFormat("BinarySerialize(fileName={0})", fileName);
				SerializationHelper.BinarySerialize(this, fileName);
			}
		}

		private void SetLogEntriesSource()
		{
			foreach (SVNLogEntry entry in GetEnumerableLogEntries())
			{
				entry.Source = Source;
			}
		}

		IEnumerator<SVNLogEntry> IEnumerable<SVNLogEntry>.GetEnumerator()
		{
			return LogEntries.GetEnumerator();
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} Entries", Source.Name, LogEntries.Count);
		}

		internal virtual List<SVNLogEntry> Update()
		{
			SVNLog updates = SVNFactory.GetUpdates(Source);
			List<SVNLogEntry> newEntries = Merge(updates);
			if (newEntries.Count > 0)
			{
				Logger.Log.DebugFormat("newEntries={0}", newEntries.Count);
				UpdateRevision();
				Save();
			}
			return newEntries;
		}

		private void UpdateRevision()
		{
			long oldRevision = Source.Revision;
			Source.Revision = GetMaxRevision(LogEntries);
			Logger.Log.InfoFormat("Source:{0}, From:{1}, To:{2}", Source, oldRevision, Source.Revision);
		}

		internal bool IsCached
		{
			get { return FileSystemHelper.FileExists(Source.CacheFileName); }
		}

		public List<SVNLogEntry> LogEntries { get; private set; }

		public List<string> PossibleConflictedFilePaths
		{
			get
			{
				List<string> list = new List<string>();
				foreach (SVNLogEntry entry in GetEnumerableLogEntries())
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

		public SVNMonitor.Entities.Source Source
		{
			[DebuggerNonUserCode]
			get { return source; }
			set
			{
				source = value;
				SetLogEntriesSource();
			}
		}

		public IEnumerable<SVNLogEntry> UnreadLogEntries
		{
			get { return GetEnumerableLogEntries().Where(e => e.Unread); }
		}
	}
}