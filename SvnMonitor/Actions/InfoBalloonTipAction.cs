namespace SVNMonitor.Actions
{
    using SVNMonitor.Entities;
    using SVNMonitor.Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Drawing.Design;
    using System.Text;
    using System.Windows.Forms;

    [Serializable, ResourceProvider("Show a tray-icon with some information in a balloon tip")]
    internal class InfoBalloonTipAction : BalloonTipAction
    {
        public InfoBalloonTipAction()
        {
            this.TipIcon = ToolTipIcon.Info;
            this.TipTitle = "SVN-Monitor";
            this.ShowBalloonTip = true;
        }

        private void CreateTipTextForMultipleUpdates(List<SVNLogEntry> logEntries, List<SVNPath> paths)
        {
            StringBuilder text = new StringBuilder();
            string sourcesString = string.Empty;
            if ((logEntries != null) && (logEntries.Count > 0))
            {
                List<Source> sources = new List<Source>();
                List<string> sourceStrings = new List<string>();
                foreach (SVNLogEntry entry in logEntries)
                {
                    if (!sources.Contains(entry.Source))
                    {
                        sources.Add(entry.Source);
                        sourceStrings.Add(entry.Source.Name);
                    }
                }
                sourcesString = string.Join(", ", sourceStrings.ToArray());
            }
            if ((paths != null) && (paths.Count > 0))
            {
                List<Source> sources = new List<Source>();
                List<string> sourceStrings = new List<string>();
                foreach (SVNPath path in paths)
                {
                    if (!sources.Contains(path.Source))
                    {
                        sources.Add(path.Source);
                        sourceStrings.Add(path.Source.Name);
                    }
                }
                sourcesString = string.Join(", ", sourceStrings.ToArray());
            }
            text.AppendLine("Updates are available for:");
            text.Append(sourcesString);
            this.TipText = text.ToString();
        }

        private void CreateTipTextForSingleLogEntry(SVNLogEntry logEntry)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} has updated {1}:{2}", logEntry.Author, logEntry.SourceName, Environment.NewLine);
            sb.AppendLine(logEntry.Message);
            int count = logEntry.Paths.Count;
            sb.AppendFormat("({0} item{1} updated)", count, (count == 1) ? string.Empty : "s");
            this.TipText = sb.ToString();
        }

        private void CreateTipTextForSinglePath(SVNPath path)
        {
            StringBuilder sb = new StringBuilder();
            SVNLogEntry logEntry = path.LogEntry;
            sb.AppendFormat("{0} has updated {1}:{2}", logEntry.Author, logEntry.SourceName, Environment.NewLine);
            sb.AppendLine(logEntry.Message);
            sb.AppendLine(path.FilePath);
            this.TipText = sb.ToString();
        }

        protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
        {
            if ((logEntries != null) && (logEntries.Count == 1))
            {
                this.CreateTipTextForSingleLogEntry(logEntries[0]);
            }
            else if ((paths != null) && (paths.Count == 1))
            {
                this.CreateTipTextForSinglePath(paths[0]);
            }
            else
            {
                this.CreateTipTextForMultipleUpdates(logEntries, paths);
            }
            base.Run(logEntries, paths);
        }

        [Browsable(false)]
        public override bool ShowBalloonTip
        {
            get
            {
                return base.ShowBalloonTip;
            }
            set
            {
                base.ShowBalloonTip = value;
            }
        }

        public override string SummaryInfo
        {
            get
            {
                return "Show a balloon tip with a list of sources that have available updates.";
            }
        }

        [Browsable(false)]
        public override ToolTipIcon TipIcon
        {
            [DebuggerNonUserCode]
            get
            {
                return base.TipIcon;
            }
            [DebuggerNonUserCode]
            set
            {
                base.TipIcon = value;
            }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), Browsable(false)]
        public override string TipText
        {
            [DebuggerNonUserCode]
            get
            {
                return base.TipText;
            }
            [DebuggerNonUserCode]
            set
            {
                base.TipText = value;
            }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), Browsable(false)]
        public override string TipTitle
        {
            [DebuggerNonUserCode]
            get
            {
                return base.TipTitle;
            }
            [DebuggerNonUserCode]
            set
            {
                base.TipTitle = value;
            }
        }
    }
}

