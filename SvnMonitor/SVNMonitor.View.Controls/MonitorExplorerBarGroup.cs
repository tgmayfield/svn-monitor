using Janus.Windows.ExplorerBar;
using SVNMonitor.Entities;
using System.Collections.Generic;
using System;
using SVNMonitor.Extensions;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using System.Windows.Forms;
using SVNMonitor.View;
using Janus.Windows.GridEX;

namespace SVNMonitor.View.Controls
{
public class MonitorExplorerBarGroup : UserEntityExplorerBarGroup<Monitor>
{
	private ExplorerBarItem conditionItem;

	public Monitor Monitor
	{
		get
		{
			return base.Entity;
		}
	}

	public MonitorExplorerBarGroup(Monitor monitor)
	{
	}

	private IEnumerable<ExplorerBarItem> CreateActionItems()
	{
		IEnumerable<ExplorerBarItem> items = this.Monitor.Actions.Where<Action>(new Predicate<Action>((a) => a.Enabled)).Select<Action,ExplorerBarItem>(new Func<Action, ExplorerBarItem>((action) => {
			ExplorerBarItem explorerBarItem = new ExplorerBarItem();
			explorerBarItem.Image = Images.plug;
			explorerBarItem.Text = action.SummaryInfo;
			explorerBarItem.ToolTipText = string.Format("{0}{1}({2})", action.SummaryInfo, Environment.NewLine, Strings.ClickToEditMonitor);
			explorerBarItem.Key = "Action";
			return explorerBarItem;
		}
		));
		return items;
	}

	protected override void CreateSubItems()
	{
		ExplorerBarItem[] explorerBarItemArray;
		base.CreateSubItems();
		this.conditionItem = new ExplorerBarItem();
		this.conditionItem.Image = Images.funnel;
		this.conditionItem.ItemType = ItemType.LinkButton;
		this.conditionItem.Cursor = Cursors.Hand;
		this.conditionItem.Key = "Condition";
		IEnumerable<ExplorerBarItem> actions = this.CreateActionItems();
		base.Items.AddRange(new ExplorerBarItem[] { this.conditionItem, base.ErrorItem });
		base.Items.AddRange(actions.ToArray<ExplorerBarItem>());
	}

	public override void RefreshEntity()
	{
		if (MainForm.FormInstance.InvokeRequired)
		{
			base.BeginInvoke(new MethodInvoker(this.RefreshEntity));
			return;
		}
		base.RefreshEntity();
		this.SetCondition();
	}

	private void SetCondition()
	{
		GridEXFilterCondition condition = this.Monitor.FilterCondition;
		if (condition == null)
		{
			this.conditionItem.Text = Strings.ConditionForAllUpdates;
		}
		else
		{
			this.conditionItem.Text = condition.ToString();
		}
		this.conditionItem.ToolTipText = string.Format("{0}{1}({2})", this.conditionItem.Text, Environment.NewLine, Strings.ClickToEditMonitor);
	}

	protected override void SetNameAndImage()
	{
		base.Text = this.Monitor.Name;
		if (this.Monitor.Enabled)
		{
			base.Image = Images.satellite_dish_32;
			return;
		}
		base.Text = string.Concat(this.Text, string.Format(" ({0})", Strings.DisabledEntity));
		base.Image = Images.satellite_dish_32_disabled;
	}

	protected override void SetProperties()
	{
		base.Expandable = false;
	}
}
}