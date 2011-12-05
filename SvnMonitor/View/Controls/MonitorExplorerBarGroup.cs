using System.Linq;

namespace SVNMonitor.View.Controls
{
    using Janus.Windows.ExplorerBar;
    using Janus.Windows.GridEX;
    using SVNMonitor.Actions;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class MonitorExplorerBarGroup : UserEntityExplorerBarGroup<SVNMonitor.Entities.Monitor>
    {
        private ExplorerBarItem conditionItem;

        public MonitorExplorerBarGroup(SVNMonitor.Entities.Monitor monitor) : base(monitor)
        {
        }

        private IEnumerable<ExplorerBarItem> CreateActionItems()
        {
			return this.Monitor.Actions.Where<Actions.Action>(a => a.Enabled).Select<Actions.Action, ExplorerBarItem>(action => new ExplorerBarItem { Image = Images.plug, Text = action.SummaryInfo, ToolTipText = string.Format("{0}{1}({2})", action.SummaryInfo, Environment.NewLine, Strings.ClickToEditMonitor), Key = "Action" });
        }

        protected override void CreateSubItems()
        {
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
                MainForm.FormInstance.BeginInvoke(new MethodInvoker(this.RefreshEntity));
            }
            else
            {
                base.RefreshEntity();
                this.SetCondition();
            }
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
            get
            {
                return base.Entity;
            }
        }
    }
}

