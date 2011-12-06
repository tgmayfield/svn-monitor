using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using Janus.Windows.GridEX;

using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.View.Controls;

namespace SVNMonitor.View.Panels
{
	internal partial class UpdatesGridContainer : UserControl
	{
		public UpdatesGridContainer()
		{
			InitializeComponent();
			UIHelper.ApplyResources(Grid, this);
			SetActionColumnValueList();
		}

		private void SetActionColumnValueList()
		{
			GridEXColumn actionColumn = Grid.Tables["Paths"].Columns["Action"];
			actionColumn.HasValueList = true;
			actionColumn.FilterEditType = FilterEditType.DropDownList;
			foreach (SVNAction action in Enum.GetValues(typeof(SVNAction)))
			{
				actionColumn.ValueList.Add(action, action.ToString());
			}
		}

		internal Janus.Windows.GridEX.GridEX Grid
		{
			[DebuggerNonUserCode]
			get { return logEntriesGrid1; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<SVNLogEntry> LogEntries
		{
			[DebuggerNonUserCode]
			get { return ((LogEntriesGrid)Grid).LogEntries; }
			[DebuggerNonUserCode]
			set { ((LogEntriesGrid)Grid).LogEntries = value; }
		}
	}
}