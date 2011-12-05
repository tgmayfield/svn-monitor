﻿namespace SVNMonitor.View.Controls
{
    using Janus.Windows.GridEX;
    using SVNMonitor.Actions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    public class ActionsGrid : Janus.Windows.GridEX.GridEX
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Actions.Action> actions;

		public List<Actions.Action> Actions
        {
            [DebuggerNonUserCode]
            get
            {
                return this.actions;
            }
            set
            {
                this.actions = value;
                base.DataSource = this.actions;
                base.Refetch();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Actions.Action SelectedAction
        {
            get
            {
				Actions.Action action = null;
                GridEXRow row = this.SelectedRow;
                if (row != null)
                {
					action = (Actions.Action)row.DataRow;
                }
                return action;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private GridEXRow SelectedRow
        {
            get
            {
                GridEXRow row = null;
                if (base.SelectedItems.Count > 0)
                {
                    row = base.SelectedItems[0].GetRow();
                }
                return row;
            }
        }
    }
}

