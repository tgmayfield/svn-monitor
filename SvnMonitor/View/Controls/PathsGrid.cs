using Janus.Windows.GridEX;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using System;
using SVNMonitor.Resources.Text;
using SVNMonitor.Resources;
using System.Windows.Forms;
using SVNMonitor.Logging;
using System.Drawing;

namespace SVNMonitor.View.Controls
{
internal class PathsGrid : GridEX
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private IEnumerable<SVNPath> paths;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public IEnumerable<SVNPath> Paths
	{
		get
		{
			return this.paths;
		}
		set
		{
			this.paths = value;
			this.RefreshData();
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public SVNPath SelectedPath
	{
		get
		{
			SVNPath path = null;
			if (base.SelectedItems.Count == 1)
			{
				path = (SVNPath)base.SelectedItems[0].GetRow().DataRow;
			}
			return path;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public IEnumerable<SVNPath> SelectedPaths
	{
		get
		{
			return base.SelectedItems.Cast<GridEXSelectedItem>().Select<GridEXSelectedItem,SVNPath>(new Func<GridEXSelectedItem, SVNPath>((item) => (SVNPath)item.GetRow().DataRow));
		}
	}

	public PathsGrid()
	{
	}

	protected override void OnFormattingRow(RowLoadEventArgs e)
	{
		base.OnFormattingRow(e);
		if (!e.Row.DataRow as SVNPath)
		{
			return;
		}
		SVNPath path = (SVNPath)e.Row.DataRow;
		string text = path.ActionString;
		GridEXCell possibleConflictedCell = e.Row.Cells["PossibleConflicted"];
		string suffix = string.Empty;
		if (path.Modified)
		{
			suffix = string.Format(", {0}", Strings.PathModified);
			possibleConflictedCell.Image = Images.arrow_up_blue;
			possibleConflictedCell.ToolTipText = Strings.PathModified;
		}
		if (path.Unread)
		{
			suffix = string.Format(", {0}", Strings.PathUnread);
			possibleConflictedCell.Image = Images.arrow_down_green;
			possibleConflictedCell.ToolTipText = Strings.PathUnread;
		}
		if (path.PossibleConflicted)
		{
			suffix = string.Format(", {0}", Strings.PathPossibleConflict);
			possibleConflictedCell.Image = Images.warning;
			possibleConflictedCell.ToolTipText = Strings.PathPossibleConflict;
		}
		text = string.Concat(text, suffix);
		if (!path.ExistsLocally)
		{
			text = string.Concat(text, string.Format(" ({0})", Strings.PathDoesNotExist));
		}
		GridEXCell filePathCell = e.Row.Cells["DisplayName"];
		filePathCell.ToolTipText = text;
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		try
		{
			base.OnKeyDown(e);
			if (e.Control || e.KeyCode == 65)
			{
				for (int i = 0; i < (int)base.GetDataRows().Length; i++)
				{
					base.SelectedItems.Add(i);
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error selecting all rows", ex);
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		try
		{
			base.OnPaint(e);
			if (this.Paths == null)
			{
				int y = 30;
				e.Graphics.DrawString(Strings.SelectALogEntryToShowPaths, base.Font, Brushes.DarkGray, 10, (float)y);
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

	private void RefreshData()
	{
		if (base.InvokeRequired)
		{
			base.BeginInvoke(new MethodInvoker(this.RefreshData));
			return;
		}
		base.DataSource = this.paths;
	}
}
}