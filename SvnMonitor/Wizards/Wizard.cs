﻿namespace SVNMonitor.Wizards
{
    using Janus.Windows.GridEX;
    using SVNMonitor;
    using SVNMonitor.Entities;
    using SVNMonitor.View.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public abstract class Wizard
    {
        protected Wizard()
        {
        }

        protected abstract IEnumerable<Action> CreateActions(string baseName);
        private static GridEXColumn GetColumn(string tableName, string columnName)
        {
            GridEXTable table = Updater.Instance.UpdatesGrid.Tables[tableName];
            return table.Columns[columnName];
        }

        protected abstract string GetWizardName(string baseName);
        public virtual void Run(string baseName, string tableName, string columnName)
        {
            try
            {
                this.TryRun(baseName, tableName, columnName);
            }
            catch (WizardCancelledException)
            {
            }
        }

        private void TryRun(string baseName, string tableName, string columnName)
        {
            Monitor <>g__initLocal0 = new Monitor {
                Name = this.GetWizardName(baseName)
            };
            Monitor monitor = <>g__initLocal0;
            GridEXFilterCondition <>g__initLocal1 = new GridEXFilterCondition {
                Column = GetColumn(tableName, columnName),
                ConditionOperator = ConditionOperator.Equal,
                Value1 = baseName
            };
            GridEXFilterCondition condition = <>g__initLocal1;
            monitor.FilterCondition = condition;
            IEnumerable<Action> actions = this.CreateActions(baseName);
            if (actions != null)
            {
                monitor.Actions.AddRange(actions);
            }
            if (MonitorPropertiesDialog.ShowDialog(monitor) == DialogResult.OK)
            {
                MonitorSettings.Instance.AddEntity(monitor);
            }
        }
    }
}
