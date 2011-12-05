using System.Linq;

namespace SVNMonitor.View.Controls
{
    using Janus.Windows.GridEX;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal class PathsGrid : Janus.Windows.GridEX.GridEX
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IEnumerable<SVNPath> paths;

        protected override void OnFormattingRow(RowLoadEventArgs e)
        {
            base.OnFormattingRow(e);
            if (e.Row.DataRow is SVNPath)
            {
                SVNPath path = (SVNPath) e.Row.DataRow;
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
                text = text + suffix;
                if (!path.ExistsLocally)
                {
                    text = text + string.Format(" ({0})", Strings.PathDoesNotExist);
                }
                GridEXCell filePathCell = e.Row.Cells["DisplayName"];
                filePathCell.ToolTipText = text;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                base.OnKeyDown(e);
                if (e.Control && (e.KeyCode == Keys.A))
                {
                    for (int i = 0; i < base.GetDataRows().Length; i++)
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
                    e.Graphics.DrawString(Strings.SelectALogEntryToShowPaths, this.Font, Brushes.DarkGray, 10f, (float) y);
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
            }
            else
            {
                base.DataSource = this.paths;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IEnumerable<SVNPath> Paths
        {
            [DebuggerNonUserCode]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public SVNPath SelectedPath
        {
            get
            {
                SVNPath path = null;
                if (base.SelectedItems.Count == 1)
                {
                    path = (SVNPath) base.SelectedItems[0].GetRow().DataRow;
                }
                return path;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IEnumerable<SVNPath> SelectedPaths
        {
            get
            {
                return base.SelectedItems.Cast<GridEXSelectedItem>().Select<GridEXSelectedItem, SVNPath>(item => ((SVNPath) item.GetRow().DataRow));
            }
        }
    }
}

