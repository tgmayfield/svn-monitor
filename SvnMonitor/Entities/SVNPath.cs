using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Settings;
using SVNMonitor.View.Interfaces;

namespace SVNMonitor.Entities
{
	[Serializable]
	public class SVNPath : ISearchable
	{
		private string filePath;
		[NonSerialized]
		private long lastSourceRevision;
		private static readonly Dictionary<string, string> pathsWithPathTooLongException = new Dictionary<string, string>();

		public SVNPath(SVNLogEntry logEntry, SVNAction action, string name)
		{
			LogEntry = logEntry;
			Action = action;
			Name = name;
		}

		public IEnumerable<string> GetSearchKeywords()
		{
			return new[]
			{
				ActionString, DisplayName
			};
		}

		private string GuessFilePath()
		{
			SVNInfo info = LogEntry.Source.GetInfo(false);
			return GuessFilePath(LogEntry, Name, info);
		}

		internal static string GuessFilePath(SVNLogEntry logEntry, string name, SVNInfo info)
		{
			if (info == null)
			{
				return name;
			}
			if (logEntry.Source.IsURL)
			{
				return (info.RepositoryRoot + name);
			}
			if (FileSystemHelper.FileExists(info.Path))
			{
				return info.Path;
			}
			string sourcePath = logEntry.Source.Path;
			string target = null;
			string fileProtocol = "file:///";
			if (info.URL.Contains(fileProtocol))
			{
				target = name;
				if (target.StartsWith(fileProtocol))
				{
					target = target.Substring(fileProtocol.Length);
				}
				else
				{
					string rootBase = info.URL.Substring(info.RepositoryRoot.Length);
					if (rootBase.Length <= target.Length)
					{
						target = target.Substring(rootBase.Length);
						if (target.StartsWith("/"))
						{
							target = target.Substring(1, target.Length - 1);
						}
						target = Path.Combine(sourcePath, target);
					}
				}
			}
			if (target == null)
			{
				target = name.Trim().Replace('/', '\\');
				string rootBase = info.URL.Substring(info.RepositoryRoot.Length);
				if (string.IsNullOrEmpty(rootBase))
				{
					target = target.TrimStart(new[]
					{
						'\\'
					});
					target = Path.Combine(sourcePath, target);
				}
				else if (name.Length > rootBase.Length)
				{
					target = sourcePath + name.Substring(rootBase.Length);
				}
			}
			try
			{
				target = FileSystemHelper.GetFullPath(target);
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Error in System.IO.Path.GetFullPath (source={0}, sourcePath={1}, target={2})", logEntry.Source, sourcePath, target), ex);
			}
			return target;
		}

		public override string ToString()
		{
			return string.Format("[{0}] {1}\t{2}", Revision, Action, Name);
		}

		public SVNAction Action { get; private set; }

		public string ActionString
		{
			[DebuggerNonUserCode]
			get { return Action.ToString(); }
		}

		public string DisplayName
		{
			get
			{
				string displayName = Name;
				string fullUrl = Source.Path + Name;
				if (pathsWithPathTooLongException.ContainsKey(fullUrl))
				{
					return pathsWithPathTooLongException[fullUrl];
				}
				if (!Source.IsURL)
				{
					displayName = FilePath;
					try
					{
						FileSystemHelper.GetFullPath(displayName);
						if (!FileSystemHelper.ExistsNoResolve(displayName))
						{
							displayName = Name;
						}
					}
					catch (PathTooLongException)
					{
						pathsWithPathTooLongException.Add(fullUrl, displayName);
					}
				}
				return displayName;
			}
		}

		public bool ExistsLocally
		{
			get { return FileSystemHelper.Exists(FilePath); }
		}

		public string FileName
		{
			get
			{
				if (string.IsNullOrEmpty(Name))
				{
					return string.Empty;
				}
				string temp = Name.Replace(@"\", "/");
				if (temp.EndsWith("/"))
				{
					return string.Empty;
				}
				int index = temp.LastIndexOf('/');
				if (index < 0)
				{
					return Name;
				}
				return temp.Substring(index + 1);
			}
		}

		public string FilePath
		{
			get
			{
				SVNInfo info = LogEntry.Source.GetInfo(false);
				if (info != null)
				{
					long revision = info.Revision;
					if ((revision > lastSourceRevision) && !FileSystemHelper.Exists(filePath))
					{
						filePath = GuessFilePath();
						lastSourceRevision = revision;
					}
				}
				if (!FileSystemHelper.IsUrl(filePath))
				{
					filePath = filePath.Replace("/", @"\");
				}
				return filePath;
			}
			[DebuggerNonUserCode]
			internal set { filePath = value; }
		}

		public bool IgnoreOnCommit
		{
			get
			{
				SVNStatus localStatus = LogEntry.Source.LocalStatus;
				if (localStatus == null)
				{
					return false;
				}
				return localStatus.IsIgnoreOnCommit(FilePath);
			}
		}

		public SVNLogEntry LogEntry { get; private set; }

		public bool Modified
		{
			get
			{
				SVNStatus localStatus = LogEntry.Source.LocalStatus;
				if (localStatus == null)
				{
					return false;
				}
				return localStatus.IsModified(FilePath, false);
			}
		}

		public bool ModifiedForConflict
		{
			get
			{
				SVNStatus localStatus = LogEntry.Source.LocalStatus;
				if (localStatus == null)
				{
					return false;
				}
				return localStatus.IsModified(FilePath, true);
			}
		}

		public bool ModifiedNoIgnore
		{
			get
			{
				SVNStatus localStatus = LogEntry.Source.LocalStatus;
				if (localStatus == null)
				{
					return false;
				}
				return localStatus.IsModifiedNoIgnore(FilePath);
			}
		}

		public string Name { get; private set; }

		public bool PossibleConflicted
		{
			get
			{
				if (!Unread)
				{
					return false;
				}
				if (Unversioned)
				{
					return true;
				}
				if (ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommitConflicts)
				{
					return ModifiedForConflict;
				}
				return ModifiedNoIgnore;
			}
		}

		public long Revision
		{
			get { return LogEntry.Revision; }
		}

		public SVNMonitor.Entities.Source Source
		{
			get
			{
				if (LogEntry == null)
				{
					return null;
				}
				return LogEntry.Source;
			}
		}

		public bool Unread
		{
			get
			{
				SVNStatus remoteStatus = LogEntry.Source.RemoteStatus;
				if (remoteStatus == null)
				{
					return false;
				}
				if (!remoteStatus.Contains(Uri))
				{
					return false;
				}
				SVNStatusEntry entry = remoteStatus[Uri];
				return (Revision > entry.WorkingCopyRevision);
			}
		}

		public bool Unversioned
		{
			get
			{
				SVNStatus localStatus = LogEntry.Source.LocalStatus;
				if (localStatus == null)
				{
					return false;
				}
				return localStatus.IsUnversioned(FilePath);
			}
		}

		public string Uri { get; internal set; }
	}
}