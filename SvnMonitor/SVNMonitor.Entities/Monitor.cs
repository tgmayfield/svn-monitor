using System;
using SVNMonitor.View.Interfaces;
using System.Diagnostics;
using System.Collections.Generic;
using SVNMonitor.Helpers;
using Janus.Windows.GridEX;
using System.ComponentModel;
using SVNMonitor;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.Actions;

namespace SVNMonitor.Entities
{
[Serializable]
public class Monitor : UserEntity, ISearchable
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private List<Action> actions;

	private ConditionSerializationContext conditionSerializationContext;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[NonSerialized]
	private GridEXFilterCondition filterCondition;

	private string filterConditionString;

	[NonSerialized]
	private List<Action> rejectionActions;

	[NonSerialized]
	private ConditionSerializationContext rejectionConditionSerializationContext;

	[Browsable(false)]
	public List<Action> Actions
	{
		get
		{
			return this.actions;
		}
		set
		{
			this.actions = value;
		}
	}

	[Browsable(false)]
	public string FileExtension
	{
		get
		{
			return ".monitor";
		}
	}

	[Browsable(false)]
	public GridEXFilterCondition FilterCondition
	{
		get
		{
			if (this.filterCondition == null && this.conditionSerializationContext != null)
			{
				this.RefreshFilterCondition();
			}
			return this.filterCondition;
		}
		set
		{
			if (value != null)
			{
				this.filterCondition = value;
				this.filterConditionString = this.filterCondition.ToString();
				this.conditionSerializationContext = ConditionSerializer.Serialize(this.filterCondition);
				return;
			}
			this.filterCondition = null;
			this.filterConditionString = null;
			this.conditionSerializationContext = null;
		}
	}

	public bool IsAlive
	{
		get
		{
			return MonitorSettings.Instance.Monitors.Contains(this);
		}
	}

	public Monitor()
	{
		this.actions = new List<Action>();
		this.rejectionActions = new List<Action>();
	}

	protected override void BeforeUpgrade()
	{
		base.BeforeUpgrade();
		if (base.Version < new Version("1.1.0.417"))
		{
			Logger.Log.InfoFormat("Upgrading Monitor {0} from {1} to {2}: Renaming the LogEntries RootTable", this, base.Version, FileSystemHelper.CurrentVersion);
			this.UpgradeLogEntriesRootTableInFilterCondition();
		}
	}

	internal static void ClearAllErrors()
	{
		foreach (Monitor monitor in MonitorSettings.Instance.GetEnumerableMonitors())
		{
			monitor.ClearError();
		}
	}

	internal override void DeleteFile()
	{
		object[] objArray;
		base.DeleteFile();
		MonitorSettings.Instance.RemoveMonitor(this);
		EventLog.Log(EventLogEntryType.Monitor, Strings.MonitorDeleted_FORMAT.FormatWith(new object[] { base.Name }), this);
		base.OnStatusChanged(StatusChangedReason.Deleted);
	}

	private IEnumerable<string> GetActionsKeywords()
	{
		List<string> list = new List<string>();
		if (this.Actions == null)
		{
			return list;
		}
		Action[] actionsCopy = this.Actions.ToArray();
		Action[] actionArray = actionsCopy;
		foreach (Action action in actionArray)
		{
			list.Add(action.DisplayName);
			list.Add(action.SummaryInfo);
		}
		return list;
	}

	public IEnumerable<string> GetSearchKeywords()
	{
		List<string> keywords = new List<string>();
		string[] str[2] = base.ToString().AddRange(str);
		keywords.AddRange(this.GetActionsKeywords());
		if (this.FilterCondition != null)
		{
			keywords.Add(this.FilterCondition.ToString());
		}
		return keywords;
	}

	private void RefreshFilterCondition()
	{
		if (this.conditionSerializationContext == null)
		{
			return;
		}
		this.filterCondition = ConditionSerializer.Deserialize(this.conditionSerializationContext);
		this.filterConditionString = this.filterCondition.ToString();
	}

	private void RejectActionsChanges()
	{
		this.Actions = this.rejectionActions;
		foreach (Action action in this.Actions)
		{
			action.RejectChanges();
		}
	}

	internal override void RejectChanges()
	{
		base.RejectChanges();
		this.conditionSerializationContext = this.rejectionConditionSerializationContext;
		this.RejectActionsChanges();
		this.filterCondition = null;
		this.RefreshFilterCondition();
	}

	private void RunAction(Action action, List<SVNLogEntry> updates, List<SVNPath> paths)
	{
		action.RunAction(updates, paths);
	}

	private void SetActionsRejectionPoint()
	{
		this.rejectionActions = new List<Action>();
		foreach (Action action in this.Actions)
		{
			action.SetRejectionPoint();
			this.rejectionActions.Add(action);
		}
	}

	internal override void SetRejectionPoint()
	{
		base.SetRejectionPoint();
		this.rejectionConditionSerializationContext = this.conditionSerializationContext;
		this.SetActionsRejectionPoint();
	}

	public override string ToString()
	{
		return base.Name;
	}

	public void Trigger(List<SVNLogEntry> updates, List<SVNPath> paths)
	{
		object[] objArray;
		Logger.Log.DebugFormat("Actions={0}", this.Actions.Count);
		EventLog.Log(EventLogEntryType.Monitor, Strings.MonitorTriggering_FORMAT.FormatWith(new object[] { base.Name }), this);
		try
		{
			Action[] actionsCopy = this.Actions.ToArray();
			Action[] actionArray = actionsCopy;
			foreach (Action action in actionArray)
			{
				try
				{
					Logger.Log.InfoFormat("Trying to run action: {0}", action.DisplayName);
					this.RunAction(action, updates, paths);
					Logger.Log.InfoFormat("Action OK: {0}", action.DisplayName);
				}
				catch (Exception ex)
				{
					Logger.Log.Error(string.Format("Error running '{0}'", action.DisplayName), ex);
					Logger.Log.Info("Will not run other actions of this monitor.");
					throw;
				}
			}
		}
		catch (InvalidOperationException invex)
		{
			Logger.Log.Error(invex.Message, invex);
		}
	}

	private void UpgradeLogEntriesRootTableInFilterCondition()
	{
		if (this.conditionSerializationContext == null)
		{
			return;
		}
		foreach (ColumnInfo colInfo in this.conditionSerializationContext.ColumnKeys)
		{
			if (colInfo.TableKey == string.Empty)
			{
				colInfo.TableKey = "LogEntries";
			}
		}
	}
}
}