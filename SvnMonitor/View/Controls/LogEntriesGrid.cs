﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Janus.Windows.GridEX;

using SVNMonitor.Entities;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Controls
{
	internal class LogEntriesGrid : Janus.Windows.GridEX.GridEX
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable<SVNLogEntry> logEntries;

		protected override void OnPaint(PaintEventArgs e)
		{
			try
			{
				base.OnPaint(e);
				if (LogEntries == null)
				{
					int y = 30;
					if (base.GroupByBoxVisible)
					{
						y += 30;
					}
					e.Graphics.DrawString(Strings.SelectASourceToShowLog, Font, Brushes.DarkGray, 10f, y);
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
				base.BeginInvoke(new MethodInvoker(SafeRefetch));
			}
			else
			{
				base.Refetch();
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<SVNLogEntry> LogEntries
		{
			[DebuggerNonUserCode]
			get { return logEntries; }
			set
			{
				logEntries = value;
				base.DataSource = logEntries;
				SafeRefetch();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public SVNLogEntry SelectedLogEntry
		{
			get
			{
				SVNLogEntry logEntry = null;
				GridEXRow row = SelectedRow;
				if (row != null)
				{
					object dataRow = row.DataRow;
					if (dataRow is SVNLogEntry)
					{
						return (SVNLogEntry)dataRow;
					}
					if (dataRow is SVNPath)
					{
						logEntry = ((SVNPath)dataRow).LogEntry;
					}
				}
				return logEntry;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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