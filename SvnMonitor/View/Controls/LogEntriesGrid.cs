using Janus.Windows.GridEX;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using SVNMonitor.Entities;
using System;
using System.Windows.Forms;
using SVNMonitor.Resources.Text;
using System.Drawing;
using SVNMonitor.Logging;

namespace SVNMonitor.View.Controls
{
internal class LogEntriesGrid : GridEX
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private IEnumerable<SVNLogEntry> logEntries;

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IEnumerable<SVNLogEntry> LogEntries
	{
		get
		{
			return this.logEntries;
		}
		set
		{
			this.logEntries = value;
			base.DataSource = this.logEntries;
			this.SafeRefetch();
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public SVNLogEntry SelectedLogEntry
	{
		get
		{
			SVNLogEntry logEntry = null;
			GridEXRow row = this.SelectedRow;
			if (row != null)
			{
				object dataRow = row.DataRow;
				if (dataRow as SVNLogEntry)
				{
					return (SVNLogEntry)dataRow;
				}
				if (dataRow as SVNPath)
				{
					logEntry = (SVNPath)dataRow.LogEntry;
				}
			}
			return logEntry;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
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

	public LogEntriesGrid()
	{
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		try
		{
			base.OnPaint(e);
			if (this.LogEntries == null)
			{
				int y = 30;
				if (base.GroupByBoxVisible)
				{
					y = y + 30;
				}
				e.Graphics.DrawString(Strings.SelectASourceToShowLog, base.Font, Brushes.DarkGray, 10, (float)y);
			}
		}
		catch (Exception ex1)
		{
			Logger.Log.Error("Error painting the grid.", ex1);
			try
			{
				e.Graphics.ReleaseHdc();
				base.OnPaint(e);
			}
			catch (Exception ex2)
			{
				Logger.Log.Error("Error painting the grid, again.", ex2);
			}
		}
	}

	private void SafeRefetch()
	{
		if (base.InvokeRequired)
		{
			base.BeginInvoke(new MethodInvoker(this.SafeRefetch));
			return;
		}
		base.Refetch();
	}
}
}