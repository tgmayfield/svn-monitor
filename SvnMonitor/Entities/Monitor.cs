using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using Janus.Windows.GridEX;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.View.Interfaces;

namespace SVNMonitor.Entities
{
	[Serializable]
	public class Monitor : UserEntity, ISearchable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Actions.Action> actions = new List<Actions.Action>();
		private ConditionSerializationContext conditionSerializationContext;
		[NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private GridEXFilterCondition filterCondition;
		private string filterConditionString;
		[NonSerialized]
		private List<Actions.Action> rejectionActions = new List<Actions.Action>();
		[NonSerialized]
		private ConditionSerializationContext rejectionConditionSerializationContext;

		protected override void BeforeUpgrade()
		{
			base.BeforeUpgrade();
			if (base.Version < new Version("1.1.0.417"))
			{
				Logger.Log.InfoFormat("Upgrading Monitor {0} from {1} to {2}: Renaming the LogEntries RootTable", this, base.Version, FileSystemHelper.CurrentVersion);
				UpgradeLogEntriesRootTableInFilterCondition();
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
			base.DeleteFile();
			MonitorSettings.Instance.RemoveMonitor(this);
			SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Monitor, Strings.MonitorDeleted_FORMAT.FormatWith(new object[]
			{
				base.Name
			}), this);
			OnStatusChanged(StatusChangedReason.Deleted);
		}

		private IEnumerable<string> GetActionsKeywords()
		{
			List<string> list = new List<string>();
			if (Actions != null)
			{
				foreach (var action in Actions.ToArray())
				{
					list.Add(action.DisplayName);
					list.Add(action.SummaryInfo);
				}
			}
			return list;
		}

		public IEnumerable<string> GetSearchKeywords()
		{
			List<string> keywords = new List<string>();
			keywords.AddRange(new[]
			{
				base.ErrorText, base.Name, ToString()
			});
			keywords.AddRange(GetActionsKeywords());
			if (FilterCondition != null)
			{
				keywords.Add(FilterCondition.ToString());
			}
			return keywords;
		}

		private void RefreshFilterCondition()
		{
			if (conditionSerializationContext != null)
			{
				filterCondition = ConditionSerializer.Deserialize(conditionSerializationContext);
				filterConditionString = filterCondition.ToString();
			}
		}

		private void RejectActionsChanges()
		{
			Actions = rejectionActions;
			foreach (Actions.Action action in Actions)
			{
				action.RejectChanges();
			}
		}

		internal override void RejectChanges()
		{
			base.RejectChanges();
			conditionSerializationContext = rejectionConditionSerializationContext;
			RejectActionsChanges();
			filterCondition = null;
			RefreshFilterCondition();
		}

		private void RunAction(Actions.Action action, List<SVNLogEntry> updates, List<SVNPath> paths)
		{
			action.RunAction(updates, paths);
		}

		private void SetActionsRejectionPoint()
		{
			rejectionActions = new List<Actions.Action>();
			foreach (Actions.Action action in Actions)
			{
				action.SetRejectionPoint();
				rejectionActions.Add(action);
			}
		}

		internal override void SetRejectionPoint()
		{
			base.SetRejectionPoint();
			rejectionConditionSerializationContext = conditionSerializationContext;
			SetActionsRejectionPoint();
		}

		public override string ToString()
		{
			return base.Name;
		}

		public void Trigger(List<SVNLogEntry> updates, List<SVNPath> paths)
		{
			Logger.Log.DebugFormat("Actions={0}", Actions.Count);
			SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Monitor, Strings.MonitorTriggering_FORMAT.FormatWith(new object[]
			{
				base.Name
			}), this);
			try
			{
				foreach (Actions.Action action in Actions.ToArray())
				{
					try
					{
						Logger.Log.InfoFormat("Trying to run action: {0}", action.DisplayName);
						RunAction(action, updates, paths);
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
			if (conditionSerializationContext != null)
			{
				foreach (ColumnInfo colInfo in conditionSerializationContext.ColumnKeys)
				{
					if (colInfo.TableKey == string.Empty)
					{
						colInfo.TableKey = "LogEntries";
					}
				}
			}
		}

		[Browsable(false)]
		public List<Actions.Action> Actions
		{
			[DebuggerNonUserCode]
			get { return actions; }
			[DebuggerNonUserCode]
			set { actions = value; }
		}

		[Browsable(false)]
		public override string FileExtension
		{
			[DebuggerNonUserCode]
			get { return ".monitor"; }
		}

		[Browsable(false)]
		public GridEXFilterCondition FilterCondition
		{
			get
			{
				if ((filterCondition == null) && (conditionSerializationContext != null))
				{
					RefreshFilterCondition();
				}
				return filterCondition;
			}
			set
			{
				if (value != null)
				{
					filterCondition = value;
					filterConditionString = filterCondition.ToString();
					conditionSerializationContext = ConditionSerializer.Serialize(filterCondition);
				}
				else
				{
					filterCondition = null;
					filterConditionString = null;
					conditionSerializationContext = null;
				}
			}
		}

		public override bool IsAlive
		{
			get { return MonitorSettings.Instance.Monitors.Contains(this); }
		}
	}
}