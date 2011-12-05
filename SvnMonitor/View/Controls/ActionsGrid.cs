using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using Janus.Windows.GridEX;

namespace SVNMonitor.View.Controls
{
	public class ActionsGrid : Janus.Windows.GridEX.GridEX
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Actions.Action> actions;

		public List<Actions.Action> Actions
		{
			[DebuggerNonUserCode]
			get { return actions; }
			set
			{
				actions = value;
				base.DataSource = actions;
				base.Refetch();
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Actions.Action SelectedAction
		{
			get
			{
				Actions.Action action = null;
				GridEXRow row = SelectedRow;
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