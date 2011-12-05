using System;
using System.Drawing;

using Janus.Windows.GridEX;

namespace SVNMonitor.View.Controls
{
	internal class EventLogGrid : Janus.Windows.GridEX.GridEX
	{
		protected override void OnFormattingRow(RowLoadEventArgs e)
		{
			base.OnFormattingRow(e);
			if (!base.DesignMode)
			{
				EventLogEntry entry = (EventLogEntry)e.Row.DataRow;
				Image image = EventLog.GetImageByEventType(entry.Type);
				GridEXCell typeCell = e.Row.Cells["Type"];
				GridEXCell messageCell = e.Row.Cells["Message"];
				GridEXCell dateTimeCell = e.Row.Cells["DateTime"];
				typeCell.Image = image;
				typeCell.ToolTipText = messageCell.ToolTipText = dateTimeCell.ToolTipText = entry.ToMessageString();
			}
		}
	}
}