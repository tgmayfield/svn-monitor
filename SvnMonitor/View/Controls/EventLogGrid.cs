using Janus.Windows.GridEX;
using System;
using SVNMonitor;
using System.Drawing;

namespace SVNMonitor.View.Controls
{
internal class EventLogGrid : GridEX
{
	public EventLogGrid()
	{
	}

	protected override void OnFormattingRow(RowLoadEventArgs e)
	{
		base.OnFormattingRow(e);
		if (base.DesignMode)
		{
			return;
		}
		EventLogEntry entry = (EventLogEntry)e.Row.DataRow;
		Image image = EventLog.GetImageByEventType(entry.Type);
		GridEXCell typeCell = e.Row.Cells["Type"];
		GridEXCell messageCell = e.Row.Cells["Message"];
		GridEXCell dateTimeCell = e.Row.Cells["DateTime"];
		typeCell.Image = image;
		string str1.set_ToolTipText(string str2 = str1).ToolTipText = str2;
	}
}
}