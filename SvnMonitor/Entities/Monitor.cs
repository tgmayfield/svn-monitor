namespace SVNMonitor.Entities
{
    using Janus.Windows.GridEX;
    using SVNMonitor;
    using SVNMonitor.Actions;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    [Serializable]
    public class Monitor : UserEntity, ISearchable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<Action> actions = new List<Action>();
        private ConditionSerializationContext conditionSerializationContext;
        [NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private GridEXFilterCondition filterCondition;
        private string filterConditionString;
        [NonSerialized]
        private List<Action> rejectionActions = new List<Action>();
        [NonSerialized]
        private ConditionSerializationContext rejectionConditionSerializationContext;

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
            base.DeleteFile();
            MonitorSettings.Instance.RemoveMonitor(this);
            SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Monitor, Strings.MonitorDeleted_FORMAT.FormatWith(new object[] { base.Name }), this);
            this.OnStatusChanged(StatusChangedReason.Deleted);
        }

        private IEnumerable<string> GetActionsKeywords()
        {
            List<string> list = new List<string>();
            if (this.Actions != null)
            {
                foreach (Action action in this.Actions.ToArray())
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
            keywords.AddRange(new string[] { base.ErrorText, base.Name, this.ToString() });
            keywords.AddRange(this.GetActionsKeywords());
            if (this.FilterCondition != null)
            {
                keywords.Add(this.FilterCondition.ToString());
            }
            return keywords;
        }

        private void RefreshFilterCondition()
        {
            if (this.conditionSerializationContext != null)
            {
                this.filterCondition = ConditionSerializer.Deserialize(this.conditionSerializationContext);
                this.filterConditionString = this.filterCondition.ToString();
            }
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
            Logger.Log.DebugFormat("Actions={0}", this.Actions.Count);
            SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Monitor, Strings.MonitorTriggering_FORMAT.FormatWith(new object[] { base.Name }), this);
            try
            {
                foreach (Action action in this.Actions.ToArray())
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
            if (this.conditionSerializationContext != null)
            {
                foreach (ColumnInfo colInfo in this.conditionSerializationContext.ColumnKeys)
                {
                    if (colInfo.TableKey == string.Empty)
                    {
                        colInfo.TableKey = "LogEntries";
                    }
                }
            }
        }

        [Browsable(false)]
        public List<Action> Actions
        {
            [DebuggerNonUserCode]
            get
            {
                return this.actions;
            }
            [DebuggerNonUserCode]
            set
            {
                this.actions = value;
            }
        }

        [Browsable(false)]
        public override string FileExtension
        {
            [DebuggerNonUserCode]
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
                if ((this.filterCondition == null) && (this.conditionSerializationContext != null))
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
                }
                else
                {
                    this.filterCondition = null;
                    this.filterConditionString = null;
                    this.conditionSerializationContext = null;
                }
            }
        }

        public override bool IsAlive
        {
            get
            {
                return MonitorSettings.Instance.Monitors.Contains(this);
            }
        }
    }
}

