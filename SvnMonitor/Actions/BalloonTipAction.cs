namespace SVNMonitor.Actions
{
    using SVNMonitor.Entities;
    using SVNMonitor.Helpers;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.SVN;
    using SVNMonitor.View;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [Serializable, ResourceProvider("Show a tray-icon with/without a balloon tip")]
    internal class BalloonTipAction : Action
    {
        private const int DefaultTimeOut = 60;
        private Source firstUpdatedSource;
        private static System.Drawing.Icon icon = Icons.lightbulb_on;
        [NonSerialized]
        private bool rejectionShowBalloonTip;
        [NonSerialized]
        private int rejectionTimeOut;
        [NonSerialized]
        private ToolTipIcon rejectionTipIcon;
        [NonSerialized]
        private string rejectionTipText;
        [NonSerialized]
        private string rejectionTipTitle;
        private bool showBalloonTip = true;
        [NonSerialized]
        private Dictionary<Source, StartEndRevisions> sourceRevisions;
        private int timeOut = 60;
        private ToolTipIcon tipIcon = ToolTipIcon.Info;
        private string tipText = "Updates are available";
        private string tipTitle = Strings.SVNMonitorCaption;

        private ToolStripMenuItem[] CreateMenuItems()
        {
            ToolStripMenuItem[] items = new ToolStripMenuItem[this.sourceRevisions.Count];
            int index = 0;
            using (Dictionary<Source, StartEndRevisions>.KeyCollection.Enumerator tempAnotherLocal0 = this.sourceRevisions.Keys.GetEnumerator())
            {
                Source source;
                while (tempAnotherLocal0.MoveNext())
                {
                    source = tempAnotherLocal0.Current;
                    StartEndRevisions revisions = this.sourceRevisions[source];
                    string updatesString = string.Format((revisions.UpdatesCount == 1) ? "{0} update" : "{0} updates", revisions.UpdatesCount);
                    ToolStripMenuItem item = new ToolStripMenuItem(string.Format("Show log: {0} ({1} by {2})", source.Name, updatesString, revisions.AuthorsString));
                    item.Click += delegate {
                        TortoiseProcess.Log(source.Path, revisions.StartRevision, revisions.EndRevision);
                    };
                    items[index] = item;
                    index++;
                }
            }
            return items;
        }

        public override void RejectChanges()
        {
            this.TimeOut = this.rejectionTimeOut;
            this.TipTitle = this.rejectionTipTitle;
            this.TipText = this.rejectionTipText;
            this.TipIcon = this.rejectionTipIcon;
            this.ShowBalloonTip = this.rejectionShowBalloonTip;
        }

        protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
        {
            this.SetRevisions(logEntries, paths);
            this.SetFirstUpdatedSource(logEntries, paths);
            MainForm.FormInstance.BeginInvoke(new MethodInvoker(this.RunSync));
        }

        private void RunSync()
        {
            ToolStripMenuItem[] menuItems = this.CreateMenuItems();
            new TrayNotifier();
            TrayNotifier.TrayNotifierInfo tempLocal0 = new TrayNotifier.TrayNotifierInfo {
                Icon = Icon,
                Text = Strings.SVNMonitorCaption,
                TipText = this.TipText,
                TipTitle = this.TipTitle,
                TipIcon = this.TipIcon,
                MenuItems = menuItems,
                TimeOut = this.TimeOut,
                ShowBalloonTip = this.ShowBalloonTip,
                Source = this.firstUpdatedSource
            };
            TrayNotifier.Show(tempLocal0);
        }

        private void SetFirstUpdatedSource(List<SVNLogEntry> logEntries, List<SVNPath> paths)
        {
            if ((logEntries != null) && (logEntries.Count > 0))
            {
                this.firstUpdatedSource = logEntries[0].Source;
            }
            else if ((paths != null) && (paths.Count > 0))
            {
                this.firstUpdatedSource = paths[0].Source;
            }
        }

        public override void SetRejectionPoint()
        {
            this.rejectionTimeOut = this.TimeOut;
            this.rejectionTipTitle = this.TipTitle;
            this.rejectionTipText = this.TipText;
            this.rejectionTipIcon = this.TipIcon;
            this.rejectionShowBalloonTip = this.ShowBalloonTip;
        }

        private void SetRevisions(List<SVNLogEntry> logEntries)
        {
            foreach (SVNLogEntry logEntry in logEntries)
            {
                Source source = logEntry.Source;
                long revision = logEntry.Revision;
                string author = logEntry.Author;
                this.UpdateSourceRevisionsEntry(source, revision, author);
            }
        }

        private void SetRevisions(List<SVNPath> paths)
        {
            foreach (SVNPath path in paths)
            {
                Source source = path.Source;
                long revision = path.Revision;
                string author = path.LogEntry.Author;
                this.UpdateSourceRevisionsEntry(source, revision, author);
            }
        }

        private void SetRevisions(List<SVNLogEntry> logEntries, List<SVNPath> paths)
        {
            this.sourceRevisions = new Dictionary<Source, StartEndRevisions>();
            if ((logEntries != null) && (logEntries.Count > 0))
            {
                this.SetRevisions(logEntries);
            }
            else
            {
                this.SetRevisions(paths);
            }
        }

        private void UpdateSourceRevisionsEntry(Source source, long revision, string author)
        {
            if (!this.sourceRevisions.ContainsKey(source))
            {
                StartEndRevisions tempLocal6 = new StartEndRevisions {
                    StartRevision = revision,
                    EndRevision = revision,
                    UpdatesCount = 1
                };
                StartEndRevisions revisions = tempLocal6;
                revisions.AddAuthor(author);
                this.sourceRevisions.Add(source, revisions);
            }
            else
            {
                StartEndRevisions startEndRevisions = this.sourceRevisions[source];
                startEndRevisions.UpdatesCount++;
                if (startEndRevisions.StartRevision > revision)
                {
                    startEndRevisions.StartRevision = revision;
                }
                if (startEndRevisions.EndRevision < revision)
                {
                    startEndRevisions.EndRevision = revision;
                }
            }
        }

        public static System.Drawing.Icon Icon
        {
            [DebuggerNonUserCode]
            get
            {
                return icon;
            }
        }

        public override bool IsValid
        {
            get
            {
                if (this.TimeOut < 0)
                {
                    return false;
                }
                if (Icon == null)
                {
                    return false;
                }
                return true;
            }
        }

        [Description("Determines whether to show a popup balloon tip."), DisplayName("Show Balloon Tip"), Category("Balloon Tip"), DefaultValue(true)]
        public virtual bool ShowBalloonTip
        {
            [DebuggerNonUserCode]
            get
            {
                return this.showBalloonTip;
            }
            [DebuggerNonUserCode]
            set
            {
                this.showBalloonTip = value;
            }
        }

        public override string SummaryInfo
        {
            get
            {
                return "Show a balloon tip in the tray.";
            }
        }

        [DefaultValue(60), DisplayName("Timeout"), Description("Number of seconds to show the tray-icon and balloon popup, or zero to show the icon until the user clicks it."), Category("Balloon Tip")]
        public virtual int TimeOut
        {
            get
            {
                if (this.timeOut < 0)
                {
                    return 60;
                }
                return this.timeOut;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Must be greater or equal to zero.");
                }
                this.timeOut = value;
            }
        }

        [Description("The icon to show in the balloon popup."), Category("Balloon Tip"), DefaultValue(1), DisplayName("Balloon Tip Icon")]
        public virtual ToolTipIcon TipIcon
        {
            [DebuggerNonUserCode]
            get
            {
                return this.tipIcon;
            }
            [DebuggerNonUserCode]
            set
            {
                this.tipIcon = value;
            }
        }

        [DisplayName("Text"), Description("The text of the balloon popup."), Category("Balloon Tip"), DefaultValue("Updates are available"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public virtual string TipText
        {
            [DebuggerNonUserCode]
            get
            {
                return this.tipText;
            }
            [DebuggerNonUserCode]
            set
            {
                this.tipText = value;
            }
        }

        [Category("Balloon Tip"), DisplayName("Title"), DefaultValue("SVN-Monitor"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), Description("The title of the balloon popup.")]
        public virtual string TipTitle
        {
            [DebuggerNonUserCode]
            get
            {
                return this.tipTitle;
            }
            [DebuggerNonUserCode]
            set
            {
                this.tipTitle = value;
            }
        }

        private class StartEndRevisions
        {
            private List<string> authors = new List<string>();

            public void AddAuthor(string author)
            {
                if (!this.authors.Contains(author))
                {
                    this.authors.Add(author);
                }
            }

            public override string ToString()
            {
                return string.Format("Start: {0}, End: {1}, Count: {2}, Author(s): {3}", new object[] { this.StartRevision, this.EndRevision, this.UpdatesCount, this.AuthorsString });
            }

            public string AuthorsString
            {
                get
                {
                    string authorsString = string.Join(", ", this.authors.ToArray());
                    if (authorsString.Length > 20)
                    {
                        authorsString = authorsString.Substring(0, 20) + "...";
                    }
                    return authorsString;
                }
            }

            public long EndRevision { get; set; }

            public long StartRevision { get; set; }

            public int UpdatesCount { get; set; }
        }
    }
}

