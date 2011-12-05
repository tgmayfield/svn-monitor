using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;

using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.View;

namespace SVNMonitor.Actions
{
	[Serializable, ResourceProvider("Show a tray-icon with/without a balloon tip")]
	internal class BalloonTipAction : Action
	{
		private const int DefaultTimeOut = 60;
		private Source firstUpdatedSource;
		private static readonly System.Drawing.Icon icon = Icons.lightbulb_on;
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
			ToolStripMenuItem[] items = new ToolStripMenuItem[sourceRevisions.Count];
			int index = 0;
			using (Dictionary<Source, StartEndRevisions>.KeyCollection.Enumerator tempAnotherLocal0 = sourceRevisions.Keys.GetEnumerator())
			{
				Source source;
				while (tempAnotherLocal0.MoveNext())
				{
					source = tempAnotherLocal0.Current;
					StartEndRevisions revisions = sourceRevisions[source];
					string updatesString = string.Format((revisions.UpdatesCount == 1) ? "{0} update" : "{0} updates", revisions.UpdatesCount);
					ToolStripMenuItem item = new ToolStripMenuItem(string.Format("Show log: {0} ({1} by {2})", source.Name, updatesString, revisions.AuthorsString));
					item.Click += delegate { TortoiseProcess.Log(source.Path, revisions.StartRevision, revisions.EndRevision); };
					items[index] = item;
					index++;
				}
			}
			return items;
		}

		public override void RejectChanges()
		{
			TimeOut = rejectionTimeOut;
			TipTitle = rejectionTipTitle;
			TipText = rejectionTipText;
			TipIcon = rejectionTipIcon;
			ShowBalloonTip = rejectionShowBalloonTip;
		}

		protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
		{
			SetRevisions(logEntries, paths);
			SetFirstUpdatedSource(logEntries, paths);
			MainForm.FormInstance.BeginInvoke(new MethodInvoker(RunSync));
		}

		private void RunSync()
		{
			ToolStripMenuItem[] menuItems = CreateMenuItems();
			new TrayNotifier();
			TrayNotifier.TrayNotifierInfo tempLocal0 = new TrayNotifier.TrayNotifierInfo
			{
				Icon = Icon,
				Text = Strings.SVNMonitorCaption,
				TipText = TipText,
				TipTitle = TipTitle,
				TipIcon = TipIcon,
				MenuItems = menuItems,
				TimeOut = TimeOut,
				ShowBalloonTip = ShowBalloonTip,
				Source = firstUpdatedSource
			};
			TrayNotifier.Show(tempLocal0);
		}

		private void SetFirstUpdatedSource(List<SVNLogEntry> logEntries, List<SVNPath> paths)
		{
			if ((logEntries != null) && (logEntries.Count > 0))
			{
				firstUpdatedSource = logEntries[0].Source;
			}
			else if ((paths != null) && (paths.Count > 0))
			{
				firstUpdatedSource = paths[0].Source;
			}
		}

		public override void SetRejectionPoint()
		{
			rejectionTimeOut = TimeOut;
			rejectionTipTitle = TipTitle;
			rejectionTipText = TipText;
			rejectionTipIcon = TipIcon;
			rejectionShowBalloonTip = ShowBalloonTip;
		}

		private void SetRevisions(List<SVNLogEntry> logEntries)
		{
			foreach (SVNLogEntry logEntry in logEntries)
			{
				Source source = logEntry.Source;
				long revision = logEntry.Revision;
				string author = logEntry.Author;
				UpdateSourceRevisionsEntry(source, revision, author);
			}
		}

		private void SetRevisions(List<SVNPath> paths)
		{
			foreach (SVNPath path in paths)
			{
				Source source = path.Source;
				long revision = path.Revision;
				string author = path.LogEntry.Author;
				UpdateSourceRevisionsEntry(source, revision, author);
			}
		}

		private void SetRevisions(List<SVNLogEntry> logEntries, List<SVNPath> paths)
		{
			sourceRevisions = new Dictionary<Source, StartEndRevisions>();
			if ((logEntries != null) && (logEntries.Count > 0))
			{
				SetRevisions(logEntries);
			}
			else
			{
				SetRevisions(paths);
			}
		}

		private void UpdateSourceRevisionsEntry(Source source, long revision, string author)
		{
			if (!sourceRevisions.ContainsKey(source))
			{
				StartEndRevisions tempLocal6 = new StartEndRevisions
				{
					StartRevision = revision,
					EndRevision = revision,
					UpdatesCount = 1
				};
				StartEndRevisions revisions = tempLocal6;
				revisions.AddAuthor(author);
				sourceRevisions.Add(source, revisions);
			}
			else
			{
				StartEndRevisions startEndRevisions = sourceRevisions[source];
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
			get { return icon; }
		}

		public override bool IsValid
		{
			get
			{
				if (TimeOut < 0)
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
			get { return showBalloonTip; }
			[DebuggerNonUserCode]
			set { showBalloonTip = value; }
		}

		public override string SummaryInfo
		{
			get { return "Show a balloon tip in the tray."; }
		}

		[DefaultValue(60), DisplayName("Timeout"), Description("Number of seconds to show the tray-icon and balloon popup, or zero to show the icon until the user clicks it."), Category("Balloon Tip")]
		public virtual int TimeOut
		{
			get
			{
				if (timeOut < 0)
				{
					return 60;
				}
				return timeOut;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Must be greater or equal to zero.");
				}
				timeOut = value;
			}
		}

		[Description("The icon to show in the balloon popup."), Category("Balloon Tip"), DefaultValue(1), DisplayName("Balloon Tip Icon")]
		public virtual ToolTipIcon TipIcon
		{
			[DebuggerNonUserCode]
			get { return tipIcon; }
			[DebuggerNonUserCode]
			set { tipIcon = value; }
		}

		[DisplayName("Text"), Description("The text of the balloon popup."), Category("Balloon Tip"), DefaultValue("Updates are available"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public virtual string TipText
		{
			[DebuggerNonUserCode]
			get { return tipText; }
			[DebuggerNonUserCode]
			set { tipText = value; }
		}

		[Category("Balloon Tip"), DisplayName("Title"), DefaultValue("SVN-Monitor"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), Description("The title of the balloon popup.")]
		public virtual string TipTitle
		{
			[DebuggerNonUserCode]
			get { return tipTitle; }
			[DebuggerNonUserCode]
			set { tipTitle = value; }
		}

		private class StartEndRevisions
		{
			private readonly List<string> authors = new List<string>();

			public void AddAuthor(string author)
			{
				if (!authors.Contains(author))
				{
					authors.Add(author);
				}
			}

			public override string ToString()
			{
				return string.Format("Start: {0}, End: {1}, Count: {2}, Author(s): {3}", new object[]
				{
					StartRevision, EndRevision, UpdatesCount, AuthorsString
				});
			}

			public string AuthorsString
			{
				get
				{
					string authorsString = string.Join(", ", authors.ToArray());
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