using System;
using System.ComponentModel;
using SVNMonitor.Helpers;
using System.Collections.Generic;
using SVNMonitor.Logging;
using SVNMonitor;
using SVNMonitor.Entities;
using SVNMonitor.View;

namespace SVNMonitor.Actions
{
[Obsolete("Actions will be rewritten fram scratch")]
[Serializable]
public abstract class Action
{
	private bool enabled;

	[Description("Indicates that this action can be tested at design-time.")]
	[DisplayName("Can Be Tested")]
	[Category("System Resources")]
	public bool CanBeTested
	{
		get
		{
			return true;
		}
	}

	[Browsable(false)]
	public string DisplayName
	{
		get
		{
			string displayName = UserTypesFactory<Action>.GetUserTypeDisplayName(this);
			return displayName;
		}
	}

	[Category("General")]
	[Description("Indicates that this action is enabled.")]
	public bool Enabled
	{
		get
		{
			return this.enabled;
		}
		set
		{
			this.enabled = value;
		}
	}

	[Browsable(false)]
	public bool IsValid { get; }

	[Browsable(false)]
	public string SummaryInfo
	{
		get
		{
			return this.DisplayName;
		}
	}

	protected Action()
	{
		this.enabled = true;
	}

	public abstract void RejectChanges();

	protected abstract void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths);

	public void RunAction(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		if (!this.Enabled)
		{
			Logger.Log.Debug(string.Format("Action '{0}' is disabled.", this.DisplayName));
			return;
		}
		EventLog.LogInfo(string.Format("Running action '{0}'...", this.DisplayName), this);
		this.Run(logEntries, paths);
	}

	public virtual void RunTest()
	{
		List<SVNLogEntry> list = new List<SVNLogEntry>();
		Source source = new Source();
		source.Name = "SVNMonitor";
		Source source = source;
		SVNLogEntry entry = new SVNLogEntry(source, (long)1234, "Chuck", DateTime.Now, "Fixed all bugs in the world.");
		list.Add(entry);
		EventLog.LogInfo(string.Format("Testing '{0}'...", this.DisplayName), this);
		this.Run(list, null);
	}

	public abstract void SetRejectionPoint();

	public void Test()
	{
		try
		{
			if (this.CanBeTested)
			{
				this.RunTest();
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error(ex.Message, ex);
			MainForm.FormInstance.ShowErrorMessage(ex.Message, this.DisplayName);
		}
	}
}
}