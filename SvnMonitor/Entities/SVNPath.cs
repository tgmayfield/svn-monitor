using System;
using SVNMonitor.View.Interfaces;
using System.Collections.Generic;
using SVNMonitor.Helpers;
using System.IO;
using SVNMonitor.Settings;
using SVNMonitor.Logging;

namespace SVNMonitor.Entities
{
[Serializable]
public class SVNPath : ISearchable
{
	private string filePath;

	[NonSerialized]
	private long lastSourceRevision;

	private static Dictionary<string, string> pathsWithPathTooLongException;

	public SVNAction Action
	{
		get;
		private set;
	}

	public string ActionString
	{
		get
		{
			return this.Action.ToString();
		}
	}

	public string DisplayName
	{
		get
		{
			string displayName = this.Name;
			string fullUrl = string.Concat(this.Source.Path, this.Name);
			if (SVNPath.pathsWithPathTooLongException.ContainsKey(fullUrl))
			{
				return SVNPath.pathsWithPathTooLongException[fullUrl];
			}
			if (!this.Source.IsURL)
			{
				displayName = this.FilePath;
				try
				{
					FileSystemHelper.GetFullPath(displayName);
					if (!FileSystemHelper.ExistsNoResolve(displayName))
					{
					}
				}
				catch (PathTooLongException)
				{
					SVNPath.pathsWithPathTooLongException.Add(fullUrl, displayName);
				}
			}
			return displayName;
		}
	}

	public bool ExistsLocally
	{
		get
		{
			bool exists = FileSystemHelper.Exists(this.FilePath);
			return exists;
		}
	}

	public string FileName
	{
		get
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return string.Empty;
			}
			string temp = this.Name.Replace("\\", "/");
			if (temp.EndsWith("/"))
			{
				return string.Empty;
			}
			int index = temp.LastIndexOf(47);
			if (index < 0)
			{
				return this.Name;
			}
			string fileName = temp.Substring(index + 1);
			return fileName;
		}
	}

	public string FilePath
	{
		get
		{
			Source source = this.LogEntry.Source;
			SVNInfo info = source.GetInfo(false);
			if (info != null)
			{
				long revision = info.Revision;
				if (revision > this.lastSourceRevision && !FileSystemHelper.Exists(this.filePath))
				{
					this.filePath = this.GuessFilePath();
					this.lastSourceRevision = revision;
				}
			}
			if (!FileSystemHelper.IsUrl(this.filePath))
			{
				this.filePath = this.filePath.Replace("/", "\\");
			}
			return this.filePath;
		}
		internal set
		{
			this.filePath = value;
		}
	}

	public bool IgnoreOnCommit
	{
		get
		{
			SVNStatus localStatus = this.LogEntry.Source.LocalStatus;
			if (localStatus == null)
			{
				return false;
			}
			return localStatus.IsIgnoreOnCommit(this.FilePath);
		}
	}

	public SVNLogEntry LogEntry
	{
		get;
		private set;
	}

	public bool Modified
	{
		get
		{
			SVNStatus localStatus = this.LogEntry.Source.LocalStatus;
			if (localStatus == null)
			{
				return false;
			}
			return localStatus.IsModified(this.FilePath, false);
		}
	}

	public bool ModifiedForConflict
	{
		get
		{
			SVNStatus localStatus = this.LogEntry.Source.LocalStatus;
			if (localStatus == null)
			{
				return false;
			}
			return localStatus.IsModified(this.FilePath, true);
		}
	}

	public bool ModifiedNoIgnore
	{
		get
		{
			SVNStatus localStatus = this.LogEntry.Source.LocalStatus;
			if (localStatus == null)
			{
				return false;
			}
			return localStatus.IsModifiedNoIgnore(this.FilePath);
		}
	}

	public string Name
	{
		get;
		private set;
	}

	public bool PossibleConflicted
	{
		get
		{
			if (!this.Unread)
			{
				return false;
			}
			if (this.Unversioned)
			{
				return true;
			}
			if (ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommitConflicts)
			{
				return this.ModifiedForConflict;
			}
			return this.ModifiedNoIgnore;
		}
	}

	public long Revision
	{
		get
		{
			return this.LogEntry.Revision;
		}
	}

	public Source Source
	{
		get
		{
			if (this.LogEntry == null)
			{
				return null;
			}
			return this.LogEntry.Source;
		}
	}

	public bool Unread
	{
		get
		{
			SVNStatus remoteStatus = this.LogEntry.Source.RemoteStatus;
			if (remoteStatus == null)
			{
				return false;
			}
			if (!remoteStatus.Contains(this.Uri))
			{
				return false;
			}
			SVNStatusEntry entry = remoteStatus[this.Uri];
			bool unread = this.Revision > entry.WorkingCopyRevision;
			return unread;
		}
	}

	public bool Unversioned
	{
		get
		{
			SVNStatus localStatus = this.LogEntry.Source.LocalStatus;
			if (localStatus == null)
			{
				return false;
			}
			return localStatus.IsUnversioned(this.FilePath);
		}
	}

	public string Uri
	{
		get;
		internal set;
	}

	static SVNPath()
	{
		SVNPath.pathsWithPathTooLongException = new Dictionary<string, string>();
	}

	public SVNPath(SVNLogEntry logEntry, SVNAction action, string name)
	{
		this.LogEntry = logEntry;
		this.Action = action;
		this.Name = name;
	}

	public IEnumerable<string> GetSearchKeywords()
	{
		string[] actionString = new string[2];
		actionString[0] = this.ActionString;
		actionString[1] = this.DisplayName;
		string[] keywords = actionString;
		return keywords;
	}

	private string GuessFilePath()
	{
		SVNInfo info = this.LogEntry.Source.GetInfo(false);
		string path = SVNPath.GuessFilePath(this.LogEntry, this.Name, info);
		return path;
	}

	internal static string GuessFilePath(SVNLogEntry logEntry, string name, SVNInfo info)
	{
		char[] chrArray;
		if (info == null)
		{
			return name;
		}
		if (logEntry.Source.IsURL)
		{
			string urlTarget = string.Concat(info.RepositoryRoot, name);
			return urlTarget;
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
			target = name.Trim().Replace(47, 92);
			string rootBase = info.URL.Substring(info.RepositoryRoot.Length);
			if (string.IsNullOrEmpty(rootBase))
			{
				target = target.TrimStart(new char[] { 92 });
				target = Path.Combine(sourcePath, target);
			}
			else
			{
				if (name.Length > rootBase.Length)
				{
					target = string.Concat(sourcePath, name.Substring(rootBase.Length));
				}
			}
		}
		try
		{
			return FileSystemHelper.GetFullPath(target);
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error in System.IO.Path.GetFullPath (source={0}, sourcePath={1}, target={2})", logEntry.Source, sourcePath, target), ex);
		}
		return target;
	}

	public override string ToString()
	{
		string str = string.Format("[{0}] {1}\t{2}", this.Revision, this.Action, this.Name);
		return str;
	}
}
}