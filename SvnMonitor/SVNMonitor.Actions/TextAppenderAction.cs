using System;
using System.Collections.Generic;
using System.Text;
using SVNMonitor.Entities;

namespace SVNMonitor.Actions
{
[Serializable]
internal abstract class TextAppenderAction : Action
{
	protected TextAppenderAction()
	{
	}

	protected abstract void AppendText(string text);

	protected virtual string HandleLogEntries(List<SVNLogEntry> logEntries)
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("Matching Log Entries:");
		foreach (SVNLogEntry entry in logEntries)
		{
			sb.AppendLine(entry.ToString());
		}
		sb.AppendLine("------------------------------------");
		sb.AppendLine();
		return sb.ToString();
	}

	protected virtual string HandlePaths(List<SVNPath> paths)
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("Matching Paths:");
		foreach (SVNPath path in paths)
		{
			sb.AppendLine(path.ToString());
		}
		sb.AppendLine("------------------------------------");
		sb.AppendLine();
		return sb.ToString();
	}

	protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		StringBuilder sb = new StringBuilder();
		if (logEntries != null && logEntries.Count > 0)
		{
			string logEntriesString = this.HandleLogEntries(logEntries);
			sb.Append(logEntriesString);
		}
		if (paths != null && paths.Count > 0)
		{
			string pathsString = this.HandlePaths(paths);
			sb.Append(pathsString);
		}
		this.AppendText(sb.ToString());
	}
}
}