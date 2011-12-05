using SVNMonitor.Resources;
using System;
using SVNMonitor.Entities;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using SVNMonitor.Resources.Text;
using SVNMonitor.View;
using SVNMonitor.Helpers;

namespace SVNMonitor.Actions
{
[ResourceProvider("Show a tray-icon with/without a balloon tip")]
[Serializable]
internal class BalloonTipAction : Action
{
	private const int DefaultTimeOut = 60;

	private Source firstUpdatedSource;

	private static Icon icon;

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

	private bool showBalloonTip;

	[NonSerialized]
	private Dictionary<Source, StartEndRevisions> sourceRevisions;

	private int timeOut;

	private ToolTipIcon tipIcon;

	private string tipText;

	private string tipTitle;

	public static Icon Icon
	{
		get
		{
			return BalloonTipAction.icon;
		}
	}

	public bool IsValid
	{
		get
		{
			if (this.TimeOut < 0)
			{
				return false;
			}
			if (BalloonTipAction.Icon == null)
			{
				return false;
			}
			return true;
		}
	}

	[Description("Determines whether to show a popup balloon tip.")]
	[DisplayName("Show Balloon Tip")]
	[Category("Balloon Tip")]
	[DefaultValue(true)]
	public bool ShowBalloonTip
	{
		get
		{
			return this.showBalloonTip;
		}
		set
		{
			this.showBalloonTip = value;
		}
	}

	public string SummaryInfo
	{
		get
		{
			return "Show a balloon tip in the tray.";
		}
	}

	[DefaultValue(60)]
	[DisplayName("Timeout")]
	[Description("Number of seconds to show the tray-icon and balloon popup, or zero to show the icon until the user clicks it.")]
	[Category("Balloon Tip")]
	public int TimeOut
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

	[Description("The icon to show in the balloon popup.")]
	[Category("Balloon Tip")]
	[DefaultValue(ToolTipIcon.Info)]
	[DisplayName("Balloon Tip Icon")]
	public ToolTipIcon TipIcon
	{
		get
		{
			return this.tipIcon;
		}
		set
		{
			this.tipIcon = value;
		}
	}

	[DisplayName("Text")]
	[Description("The text of the balloon popup.")]
	[Category("Balloon Tip")]
	[DefaultValue("Updates are available")]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	public string TipText
	{
		get
		{
			return this.tipText;
		}
		set
		{
			this.tipText = value;
		}
	}

	[Category("Balloon Tip")]
	[DisplayName("Title")]
	[DefaultValue("SVN-Monitor")]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[Description("The title of the balloon popup.")]
	public string TipTitle
	{
		get
		{
			return this.tipTitle;
		}
		set
		{
			this.tipTitle = value;
		}
	}

	static BalloonTipAction()
	{
		BalloonTipAction.icon = Icons.lightbulb_on;
	}

	public BalloonTipAction()
	{
		this.timeOut = 60;
		this.tipIcon = ToolTipIcon.Info;
		this.tipText = "Updates are available";
		this.tipTitle = Strings.SVNMonitorCaption;
		this.showBalloonTip = true;
	}

	private ToolStripMenuItem[] CreateMenuItems()
	{
		ToolStripMenuItem[] items = new ToolStripMenuItem[this.sourceRevisions.Count];
		int index = 0;
		Enumerat<Source, StartEndRevisions> enumerator = this.sourceRevisions.Keys.GetEnumerator();
		try
		{
			BalloonTipAction balloonTipAction = new BalloonTipAction();
			while (&enumerator.MoveNext())
			{
				balloonTipAction.source = &enumerator.Current;
				BalloonTipAction item = new BalloonTipAction();
				item.CS$<>8__locals3 = balloonTipAction;
				item.revisions = this.sourceRevisions[balloonTipAction.source];
				string updatesString = string.Format((item.revisions.UpdatesCount == 1 ? "{0} update" : "{0} updates"), item.revisions.UpdatesCount);
				ToolStripMenuItem item = new ToolStripMenuItem(string.Format("Show log: {0} ({1} by {2})", balloonTipAction.source.Name, updatesString, item.revisions.AuthorsString));
				item.add_Click(new EventHandler(item.<CreateMenuItems>b__1));
				items[index] = item;
				index++;
			}
		}
		finally
		{
			&enumerator.Dispose();
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
		TrayNotifierInfo trayNotifierInfo = new TrayNotifierInfo();
		trayNotifierInfo.Icon = BalloonTipAction.Icon;
		trayNotifierInfo.Text = Strings.SVNMonitorCaption;
		trayNotifierInfo.TipText = this.TipText;
		trayNotifierInfo.TipTitle = this.TipTitle;
		trayNotifierInfo.TipIcon = this.TipIcon;
		trayNotifierInfo.MenuItems = menuItems;
		trayNotifierInfo.TimeOut = this.TimeOut;
		trayNotifierInfo.ShowBalloonTip = this.ShowBalloonTip;
		trayNotifierInfo.Source = this.firstUpdatedSource;
		TrayNotifier.Show(trayNotifierInfo);
	}

	private void SetFirstUpdatedSource(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		if (logEntries != null && logEntries.Count > 0)
		{
			this.firstUpdatedSource = logEntries[0].Source;
			return;
		}
		if (paths != null && paths.Count > 0)
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

	private void SetRevisions(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		this.sourceRevisions = new Dictionary<Source, StartEndRevisions>();
		if (logEntries != null && logEntries.Count > 0)
		{
			this.SetRevisions(logEntries);
			return;
		}
		this.SetRevisions(paths);
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

	private void UpdateSourceRevisionsEntry(Source source, long revision, string author)
	{
		if (!this.sourceRevisions.ContainsKey(source))
		{
			StartEndRevisions startEndRevision = new StartEndRevisions();
			startEndRevision.StartRevision = revision;
			startEndRevision.EndRevision = revision;
			startEndRevision.UpdatesCount = 1;
			StartEndRevisions revisions = startEndRevision;
			revisions.AddAuthor(author);
			this.sourceRevisions.Add(source, revisions);
			return;
		}
		StartEndRevisions startEndRevisions = this.sourceRevisions[source];
		startEndRevisions.UpdatesCount = startEndRevisions.UpdatesCount + 1;
		if (startEndRevisions.StartRevision > revision)
		{
			startEndRevisions.StartRevision = revision;
		}
		if (startEndRevisions.EndRevision < revision)
		{
			startEndRevisions.EndRevision = revision;
		}
	}

	private class StartEndRevisions
	{
		private List<string> authors;

		public string AuthorsString;

		public long EndRevision;

		public long StartRevision;

		public int UpdatesCount;

		public StartEndRevisions();

		public void AddAuthor(string author);

		public override string ToString();
	}
}
}