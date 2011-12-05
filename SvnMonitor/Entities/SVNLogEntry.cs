using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using SVNMonitor.Extensions;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;
using SVNMonitor.View.Interfaces;

namespace SVNMonitor.Entities
{
	[Serializable]
	public class SVNLogEntry : IComparable, ISearchable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<SVNPath> paths = new List<SVNPath>();
		[NonSerialized]
		private SVNMonitor.Entities.Source source;

		public SVNLogEntry(SVNMonitor.Entities.Source source, long revision, string author, DateTime date, string message)
		{
			Source = source;
			Revision = revision;
			Author = author;
			CommitedOn = date;
			Message = message;
		}

		public int CompareTo(object obj)
		{
			return Revision.CompareTo(((SVNLogEntry)obj).Revision);
		}

		internal void Diff()
		{
			SVNInfo info = Source.GetInfo(true);
			if (info != null)
			{
				TortoiseProcess.DiffWithPrevious(info.URL, Revision - 1L, Revision);
			}
		}

		public override bool Equals(object obj)
		{
			return ((obj is SVNLogEntry) && (((SVNLogEntry)obj).Revision == Revision));
		}

		public override int GetHashCode()
		{
			return Revision.GetHashCode();
		}

		public IEnumerable<string> GetSearchKeywords()
		{
			List<string> keywords = new List<string>();
			keywords.AddRange(new[]
			{
				Author, CommitedOn.ToString(), Message, Revision.ToString(), PathsCount.ToString()
			});
			if (Paths != null)
			{
				foreach (SVNPath path in Paths)
				{
					keywords.AddRange(new[]
					{
						path.ActionString, path.DisplayName
					});
				}
			}
			return keywords;
		}

		internal void OpenSVNLog()
		{
			TortoiseProcess.Log(Source.Path, Revision, Revision);
		}

		internal void Recommend()
		{
			Source.Recommend(Revision);
		}

		internal void Rollback()
		{
			TortoiseProcess.Update(Source.Path, (Revision - 1L), TortoiseProcessCallBack);
		}

		internal void SVNUpdate()
		{
			TortoiseProcess.Update(Source.Path, Revision, TortoiseProcessCallBack);
		}

		private void TortoiseProcessCallBack()
		{
			Source.Refresh();
		}

		public override string ToString()
		{
			return string.Format("Source: {0}{1}Revision: {2}{1}Author: {3}{1}Date: {4}{1}Message: {5}{1}", new object[]
			{
				Source, Environment.NewLine, Revision, Author, CommitedOn, Message
			});
		}

		public string Author { get; private set; }

		public DateTime CommitedOn { get; private set; }

		public DateTime CommitedOnDay
		{
			get { return CommitedOn.Date; }
		}

		public TimeSpan CommitedOnTime
		{
			get { return CommitedOn.TimeOfDay; }
		}

		public string FilesCountString
		{
			get
			{
				int count = PathsCount;
				int unreadCount = UnreadPathsCount;
				int possibleConflictedCount = PossibleConflictedPathsCount;
				string unreadCountString = string.Empty;
				string possibleConflictedCountString = string.Empty;
				if (unreadCount > 0)
				{
					unreadCountString = Strings.LogUnreadCount_FORMAT.FormatWith(new object[]
					{
						unreadCount
					});
				}
				if (possibleConflictedCount > 0)
				{
					possibleConflictedCountString = Strings.LogPossibleConflicts_FORMAT.FormatWith(new object[]
					{
						possibleConflictedCount, (possibleConflictedCount == 1) ? Strings.LogPossibleConflicts_Conflict : Strings.LogPossibleConflicts_Conflicts
					});
				}
				string suffix = string.Empty;
				if ((unreadCount > 0) || (possibleConflictedCount > 0))
				{
					suffix = string.Format("({0}{1})", unreadCountString, possibleConflictedCountString);
				}
				return string.Format("{0} {1} {2}", count, (count == 1) ? Strings.LogItemsCount_Item : Strings.LogItemsCount_Items, suffix);
			}
		}

		public string Message { get; private set; }

		public bool Modified
		{
			get { return Paths.Any(p => p.Modified); }
		}

		public List<SVNPath> Paths
		{
			[DebuggerNonUserCode]
			get { return paths; }
			[DebuggerNonUserCode]
			set { paths = value; }
		}

		public int PathsCount
		{
			get { return Paths.Count; }
		}

		public bool PossibleConflicted
		{
			get { return Paths.Any(p => p.PossibleConflicted); }
		}

		public IEnumerable<string> PossibleConflictedPaths
		{
			get { return UnreadPaths.Where(p => p.PossibleConflicted).Select(p => p.FilePath); }
		}

		public int PossibleConflictedPathsCount
		{
			get { return PossibleConflictedPaths.Count(); }
		}

		public bool Recommended
		{
			get { return Source.IsRecommended(Revision); }
		}

		public long Revision { get; private set; }

		public string ShortMessage
		{
			get
			{
				if (string.IsNullOrEmpty(Message))
				{
					if (ApplicationSettingsManager.Settings.ShowDefaultTextInsteadOfEmptyLogMessage)
					{
						return Strings.EmptyLogMessage;
					}
					return string.Empty;
				}
				string separator = Environment.NewLine;
				string[] messageLines = Message.Split(new[]
				{
					separator
				}, StringSplitOptions.RemoveEmptyEntries);
				if (messageLines.Length <= ApplicationSettingsManager.Settings.PreviewRowLines)
				{
					return Message;
				}
				return (string.Join(separator, messageLines, 0, ApplicationSettingsManager.Settings.PreviewRowLines) + "...");
			}
		}

		public SVNMonitor.Entities.Source Source
		{
			[DebuggerNonUserCode]
			get { return source; }
			[DebuggerNonUserCode]
			set { source = value; }
		}

		public string SourceName
		{
			[DebuggerNonUserCode]
			get { return Source.Name; }
		}

		public bool Unread
		{
			get { return Paths.Any(p => p.Unread); }
		}

		public IEnumerable<SVNPath> UnreadPaths
		{
			get { return Paths.Where(p => p.Unread); }
		}

		public int UnreadPathsCount
		{
			get { return Paths.Where(p => p.Unread).Count(); }
		}
	}
}