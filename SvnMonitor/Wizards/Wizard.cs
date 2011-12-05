using System.Collections.Generic;
using System;
using Janus.Windows.GridEX;
using SVNMonitor;
using SVNMonitor.Entities;
using System.Windows.Forms;
using SVNMonitor.View.Dialogs;

namespace SVNMonitor.Wizards
{
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
		Monitor monitor = new Monitor();
		monitor.Name = this.GetWizardName(baseName);
		Monitor monitor = monitor;
		GridEXFilterCondition gridEXFilterCondition = new GridEXFilterCondition();
		gridEXFilterCondition.Column = Wizard.GetColumn(tableName, columnName);
		gridEXFilterCondition.ConditionOperator = ConditionOperator.Equal;
		gridEXFilterCondition.Value1 = baseName;
		GridEXFilterCondition condition = gridEXFilterCondition;
		monitor.FilterCondition = condition;
		IEnumerable<Action> actions = this.CreateActions(baseName);
		if (actions != null)
		{
			monitor.Actions.AddRange(actions);
		}
		DialogResult result = MonitorPropertiesDialog.ShowDialog(monitor);
		if (result == DialogResult.OK)
		{
			MonitorSettings.Instance.AddEntity(monitor);
		}
	}
}
}