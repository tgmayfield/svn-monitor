using System.Linq;

namespace SVNMonitor.Entities
{
    using SVNMonitor.Extensions;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.SVN;
    using SVNMonitor.View.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [Serializable]
    public class SVNLogEntry : IComparable, ISearchable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<SVNPath> paths = new List<SVNPath>();
        [NonSerialized]
        private SVNMonitor.Entities.Source source;

        public SVNLogEntry(SVNMonitor.Entities.Source source, long revision, string author, DateTime date, string message)
        {
            this.Source = source;
            this.Revision = revision;
            this.Author = author;
            this.CommitedOn = date;
            this.Message = message;
        }

        public int CompareTo(object obj)
        {
            return this.Revision.CompareTo(((SVNLogEntry) obj).Revision);
        }

        internal void Diff()
        {
            SVNInfo info = this.Source.GetInfo(true);
            if (info != null)
            {
                TortoiseProcess.DiffWithPrevious(info.URL, this.Revision - 1L, this.Revision);
            }
        }

        public override bool Equals(object obj)
        {
            return ((obj is SVNLogEntry) && (((SVNLogEntry) obj).Revision == this.Revision));
        }

        public override int GetHashCode()
        {
            return this.Revision.GetHashCode();
        }

        public IEnumerable<string> GetSearchKeywords()
        {
            List<string> keywords = new List<string>();
            keywords.AddRange(new string[] { this.Author, this.CommitedOn.ToString(), this.Message, this.Revision.ToString(), this.PathsCount.ToString() });
            if (this.Paths != null)
            {
                foreach (SVNPath path in this.Paths)
                {
                    keywords.AddRange(new string[] { path.ActionString, path.DisplayName });
                }
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
            TortoiseProcess.Update(this.Source.Path, (long) (this.Revision - 1L), new MethodInvoker(this.TortoiseProcessCallBack));
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
            return string.Format("Source: {0}{1}Revision: {2}{1}Author: {3}{1}Date: {4}{1}Message: {5}{1}", new object[] { this.Source, Environment.NewLine, this.Revision, this.Author, this.CommitedOn, this.Message });
        }

        public string Author { get; private set; }

        public DateTime CommitedOn { get; private set; }

        public DateTime CommitedOnDay
        {
            get
            {
                return this.CommitedOn.Date;
            }
        }

        public TimeSpan CommitedOnTime
        {
            get
            {
                return this.CommitedOn.TimeOfDay;
            }
        }

        public string FilesCountString
        {
            get
            {
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
                    possibleConflictedCountString = Strings.LogPossibleConflicts_FORMAT.FormatWith(new object[] { possibleConflictedCount, (possibleConflictedCount == 1) ? Strings.LogPossibleConflicts_Conflict : Strings.LogPossibleConflicts_Conflicts });
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
            get
            {
                return this.Paths.Any<SVNPath>(p => p.Modified);
            }
        }

        public List<SVNPath> Paths
        {
            [DebuggerNonUserCode]
            get
            {
                return this.paths;
            }
            [DebuggerNonUserCode]
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
                return this.Paths.Any<SVNPath>(p => p.PossibleConflicted);
            }
        }

        public IEnumerable<string> PossibleConflictedPaths
        {
            get
            {
                return this.UnreadPaths.Where<SVNPath>(p => p.PossibleConflicted).Select<SVNPath, string>(p => p.FilePath);
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
                return this.Source.IsRecommended(this.Revision);
            }
        }

        public long Revision { get; private set; }

        public string ShortMessage
        {
            get
            {
                if (string.IsNullOrEmpty(this.Message))
                {
                    if (ApplicationSettingsManager.Settings.ShowDefaultTextInsteadOfEmptyLogMessage)
                    {
                        return Strings.EmptyLogMessage;
                    }
                    return string.Empty;
                }
                string separator = Environment.NewLine;
                string[] messageLines = this.Message.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                if (messageLines.Length <= ApplicationSettingsManager.Settings.PreviewRowLines)
                {
                    return this.Message;
                }
                return (string.Join(separator, messageLines, 0, ApplicationSettingsManager.Settings.PreviewRowLines) + "...");
            }
        }

        public SVNMonitor.Entities.Source Source
        {
            [DebuggerNonUserCode]
            get
            {
                return this.source;
            }
            [DebuggerNonUserCode]
            set
            {
                this.source = value;
            }
        }

        public string SourceName
        {
            [DebuggerNonUserCode]
            get
            {
                return this.Source.Name;
            }
        }

        public bool Unread
        {
            get
            {
                return this.Paths.Any<SVNPath>(p => p.Unread);
            }
        }

        public IEnumerable<SVNPath> UnreadPaths
        {
            get
            {
                return this.Paths.Where<SVNPath>(p => p.Unread);
            }
        }

        public int UnreadPathsCount
        {
            get
            {
                return this.Paths.Where<SVNPath>(p => p.Unread).Count<SVNPath>();
            }
        }
    }
}

