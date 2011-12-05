using SVNMonitor.Resources;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using SVNMonitor.Entities;

namespace SVNMonitor.Actions
{
[ResourceProvider("Show a tray-icon with some information in a balloon tip")]
[Serializable]
internal class InfoBalloonTipAction : BalloonTipAction
{
	[Browsable(false)]
	public bool ShowBalloonTip
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

	public string SummaryInfo
	{
		get
		{
			return "Show a balloon tip with a list of sources that have available updates.";
		}
	}

	[Browsable(false)]
	public ToolTipIcon TipIcon
	{
		get
		{
			return base.TipIcon;
		}
		set
		{
			base.TipIcon = value;
		}
	}

	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[Browsable(false)]
	public string TipText
	{
		get
		{
			return base.TipText;
		}
		set
		{
			base.TipText = value;
		}
	}

	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[Browsable(false)]
	public string TipTitle
	{
		get
		{
			return base.TipTitle;
		}
		set
		{
			base.TipTitle = value;
		}
	}

	public InfoBalloonTipAction()
	{
		base.TipIcon = ToolTipIcon.Info;
		base.TipTitle = "SVN-Monitor";
		base.ShowBalloonTip = true;
	}

	private void CreateTipTextForMultipleUpdates(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		StringBuilder text = new StringBuilder();
		string sourcesString = string.Empty;
		if (logEntries != null)
		{
			if (logEntries.Count <= 0)
			{
				break;
			}
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
		if (paths != null)
		{
			if (paths.Count <= 0)
			{
				break;
			}
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
		base.TipText = text.ToString();
	}

	private void CreateTipTextForSingleLogEntry(SVNLogEntry logEntry)
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendFormat("{0} has updated {1}:{2}", logEntry.Author, logEntry.SourceName, Environment.NewLine);
		sb.AppendLine(logEntry.Message);
		int count = logEntry.Paths.Count;
		sb.AppendFormat("({0} item{1} updated)", count, (count == 1 ? string.Empty : "s"));
		base.TipText = sb.ToString();
	}

	private void CreateTipTextForSinglePath(SVNPath path)
	{
		StringBuilder sb = new StringBuilder();
		SVNLogEntry logEntry = path.LogEntry;
		sb.AppendFormat("{0} has updated {1}:{2}", logEntry.Author, logEntry.SourceName, Environment.NewLine);
		sb.AppendLine(logEntry.Message);
		sb.AppendLine(path.FilePath);
		base.TipText = sb.ToString();
	}

	protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		if (logEntries != null && logEntries.Count == 1)
		{
			this.CreateTipTextForSingleLogEntry(logEntries[0]);
		}
		base.Run(logEntries, paths);
	}
}
}