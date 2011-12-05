using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Janus.Windows.ExplorerBar;
using Janus.Windows.GridEX;

using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Controls
{
	public class MonitorExplorerBarGroup : UserEntityExplorerBarGroup<SVNMonitor.Entities.Monitor>
	{
		private ExplorerBarItem conditionItem;

		public MonitorExplorerBarGroup(SVNMonitor.Entities.Monitor monitor)
			: base(monitor)
		{
		}

		private IEnumerable<ExplorerBarItem> CreateActionItems()
		{
			return Monitor.Actions.Where(a => a.Enabled).Select(action => new ExplorerBarItem
			{
				Image = Images.plug,
				Text = action.SummaryInfo,
				ToolTipText = string.Format("{0}{1}({2})", action.SummaryInfo, Environment.NewLine, Strings.ClickToEditMonitor),
				Key = "Action"
			});
		}

		protected override void CreateSubItems()
		{
			base.CreateSubItems();
			conditionItem = new ExplorerBarItem();
			conditionItem.Image = Images.funnel;
			conditionItem.ItemType = ItemType.LinkButton;
			conditionItem.Cursor = Cursors.Hand;
			conditionItem.Key = "Condition";
			IEnumerable<ExplorerBarItem> actions = CreateActionItems();
			base.Items.AddRange(new[]
			{
				conditionItem, base.ErrorItem
			});
			base.Items.AddRange(actions.ToArray());
		}

		public override void RefreshEntity()
		{
			if (MainForm.FormInstance.InvokeRequired)
			{
				MainForm.FormInstance.BeginInvoke(new MethodInvoker(RefreshEntity));
			}
			else
			{
				base.RefreshEntity();
				SetCondition();
			}
		}

		private void SetCondition()
		{
			GridEXFilterCondition condition = Monitor.FilterCondition;
			if (condition == null)
			{
				conditionItem.Text = Strings.ConditionForAllUpdates;
			}
			else
			{
				conditionItem.Text = condition.ToString();
			}
			conditionItem.ToolTipText = string.Format("{0}{1}({2})", conditionItem.Text, Environment.NewLine, Strings.ClickToEditMonitor);
		}

		protected override void SetNameAndImage()
		{
			base.Text = Monitor.Name;
			if (Monitor.Enabled)
			{
				base.Image = Images.satellite_dish_32;
			}
			else
			{
				base.Text = base.Text + string.Format(" ({0})", Strings.DisabledEntity);
				base.Image = Images.satellite_dish_32_disabled;
			}
		}

		protected override void SetProperties()
		{
			base.Expandable = false;
		}

		public SVNMonitor.Entities.Monitor Monitor
		{
			get { return base.Entity; }
		}
	}
}