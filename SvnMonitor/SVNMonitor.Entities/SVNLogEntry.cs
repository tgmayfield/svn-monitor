using System;
using SVNMonitor.View.Interfaces;
using System.Diagnostics;
using System.Collections.Generic;
using SVNMonitor.Resources.Text;
using SVNMonitor.Extensions;
using SVNMonitor.Settings;
using SVNMonitor.SVN;
using System.Windows.Forms;

namespace SVNMonitor.Entities
{
[Serializable]
public class SVNLogEntry : IComparable, ISearchable
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private List<SVNPath> paths;

	[NonSerialized]
	private Source source;

	public string Author
	{
		get;
		private set;
	}

	public DateTime CommitedOn
	{
		get;
		private set;
	}

	public DateTime CommitedOnDay
	{
		get
		{
			DateTime commitedOn = this.CommitedOn;
			return commitedOn.Date;
		}
	}

	public TimeSpan CommitedOnTime
	{
		get
		{
			DateTime commitedOn = this.CommitedOn;
			return commitedOn.TimeOfDay;
		}
	}

	public string FilesCountString
	{
		get
		{
			object[] objArray;
			object[] objArray2;
			int count = this.PathsCount;
			int unreadCount = this.UnreadPathsCount;
			int possibleConflictedCount = this.PossibleConflictedPathsCount;
			string unreadCountString = string.Empty;
			string possibleConflictedCountString = string.Empty;
			if (unreadCount > 0)
			{
				unreadCountString = Strings.LogUnreadCount_FORMAT.FormatWith(new object[] { unreadCount });
			}
			if (possibleConflictedCount > 0)
			{
				possibleConflictedCountString = Strings.LogPossibleConflicts_FORMAT.FormatWith(new object[] { possibleConflictedCount, (possibleConflictedCount == 1 ? Strings.LogPossibleConflicts_Conflict : Strings.LogPossibleConflicts_Conflicts) });
			}
			string suffix = string.Empty;
			if (unreadCount > 0 || possibleConflictedCount > 0)
			{
				suffix = string.Format("({0}{1})", unreadCountString, possibleConflictedCountString);
			}
			return string.Format("{0} {1} {2}", count, (count == 1 ? Strings.LogItemsCount_Item : Strings.LogItemsCount_Items), suffix);
		}
	}

	public string Message
	{
		get;
		private set;
	}

	public bool Modified
	{
		get
		{
			return this.Paths.Any<SVNPath>(new Predicate<SVNPath>((p) => p.Modified));
		}
	}

	public List<SVNPath> Paths
	{
		get
		{
			return this.paths;
		}
		set
		{
			this.paths = value;
		}
	}

	public int PathsCount
	{
		get
		{
			return this.Paths.Count;
		}
	}

	public bool PossibleConflicted
	{
		get
		{
			return this.Paths.Any<SVNPath>(new Predicate<SVNPath>((p) => p.PossibleConflicted));
		}
	}

	public IEnumerable<string> PossibleConflictedPaths
	{
		get
		{
			return this.UnreadPaths.Where<SVNPath>(new Predicate<SVNPath>((p) => p.PossibleConflicted)).Select<SVNPath,string>(new Func<SVNPath, string>((p) => p.FilePath));
		}
	}

	public int PossibleConflictedPathsCount
	{
		get
		{
			return this.PossibleConflictedPaths.Count<string>();
		}
	}

	public bool Recommended
	{
		get
		{
			bool isRecommended = this.Source.IsRecommended(this.Revision);
			return isRecommended;
		}
	}

	public long Revision
	{
		get;
		private set;
	}

	public string ShortMessage
	{
		get
		{
			string[] strArrays;
			if (string.IsNullOrEmpty(this.Message))
			{
				if (ApplicationSettingsManager.Settings.ShowDefaultTextInsteadOfEmptyLogMessage)
				{
					return Strings.EmptyLogMessage;
				}
			}
			return string.Empty;
			string separator = Environment.NewLine;
			string[] messageLines = this.Message.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
			if ((int)messageLines.Length <= ApplicationSettingsManager.Settings.PreviewRowLines)
			{
				return this.Message;
			}
			string shortMessage = string.Join(separator, messageLines, 0, ApplicationSettingsManager.Settings.PreviewRowLines);
			shortMessage = string.Concat(shortMessage, "...");
			return shortMessage;
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
		}
	}

	public string SourceName
	{
		get
		{
			return this.Source.Name;
		}
	}

	public bool Unread
	{
		get
		{
			return this.Paths.Any<SVNPath>(new Predicate<SVNPath>((p) => p.Unread));
		}
	}

	public IEnumerable<SVNPath> UnreadPaths
	{
		get
		{
			return this.Paths.Where<SVNPath>(new Predicate<SVNPath>((p) => p.Unread));
		}
	}

	public int UnreadPathsCount
	{
		get
		{
			int count = this.Paths.Where<SVNPath>(new Predicate<SVNPath>((p) => p.Unread)).Count<SVNPath>();
			return count;
		}
	}

	public SVNLogEntry(Source source, long revision, string author, DateTime date, string message)
	{
		this.paths = new List<SVNPath>();
		base();
		this.Source = source;
		this.Revision = revision;
		this.Author = author;
		this.CommitedOn = date;
		this.Message = message;
	}

	public int CompareTo(object obj)
	{
		long revision = this.Revision;
		return revision.CompareTo((SVNLogEntry)obj.Revision);
	}

	internal void Diff()
	{
		SVNInfo info = this.Source.GetInfo(true);
		if (info == null)
		{
			return;
		}
		TortoiseProcess.DiffWithPrevious(info.URL, this.Revision - (long)1, this.Revision);
	}

	public override bool Equals(object obj)
	{
		if (!obj as SVNLogEntry)
		{
			return false;
		}
		bool equals = (SVNLogEntry)obj.Revision == this.Revision;
		return equals;
	}

	public override int GetHashCode()
	{
		long revision = this.Revision;
		return revision.GetHashCode();
	}

	public IEnumerable<string> GetSearchKeywords()
	{
		string[] strArrays = null;
		DateTime dateTime;
		long num;
		List<string> keywords = new List<string>();
		4[int num2 = this.PathsCount] = num2.ToString().AddRange(strArrays);
		if (this.Paths == null)
		{
			break;
		}
		foreach (SVNPath path in this.Paths)
		{
			string[] displayName[1] = path.DisplayName.AddRange(displayName);
		}
		return keywords;
	}

	internal void OpenSVNLog()
	{
		TortoiseProcess.Log(this.Source.Path, this.Revision, this.Revision);
	}

	internal void Recommend()
	{
		this.Source.Recommend(this.Revision);
	}

	internal void Rollback()
	{
		TortoiseProcess.Update(this.Source.Path, this.Revision - (long)1, new MethodInvoker(this.TortoiseProcessCallBack));
	}

	internal void SVNUpdate()
	{
		TortoiseProcess.Update(this.Source.Path, this.Revision, new MethodInvoker(this.TortoiseProcessCallBack));
	}

	private void TortoiseProcessCallBack()
	{
		this.Source.Refresh();
	}

	public override string ToString()
	{
		object[] objArray;
		string str = string.Format("Source: {0}{1}Revision: {2}{1}Author: {3}{1}Date: {4}{1}Message: {5}{1}", new object[] { this.Source, Environment.NewLine, this.Revision, this.Author, this.CommitedOn, this.Message });
		return str;
	}
}
}