using Janus.Windows.GridEX;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using SVNMonitor.Actions;
using System;

namespace SVNMonitor.View.Controls
{
public class ActionsGrid : GridEX
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private List<Action> actions;

	public List<Action> Actions
	{
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

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Action SelectedAction
	{
		get
		{
			Action action = null;
			GridEXRow row = this.SelectedRow;
			if (row != null)
			{
				object dataRow = row.DataRow;
				action = (Action)dataRow;
			}
			return action;
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	private GridEXRow SelectedRow
	{
		get
		{
			GridEXRow row = null;
			if (base.SelectedItems.Count > 0)
			{
				GridEXSelectedItem item = base.SelectedItems[0];
				row = item.GetRow();
			}
			return row;
		}
	}

	public ActionsGrid()
	{
	}
}
}