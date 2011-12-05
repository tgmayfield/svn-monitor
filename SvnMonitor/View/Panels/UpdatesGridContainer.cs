using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Janus.Windows.GridEX;

using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.View.Controls;

namespace SVNMonitor.View.Panels
{
	internal class UpdatesGridContainer : UserControl
	{
		private IContainer components;
		private LogEntriesGrid logEntriesGrid1;

		public UpdatesGridContainer()
		{
			InitializeComponent();
			UIHelper.ApplyResources(Grid, this);
			SetActionColumnValueList();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			GridEXLayout logEntriesGrid1_DesignTimeLayout = new GridEXLayout();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(UpdatesGridContainer));
			logEntriesGrid1 = new LogEntriesGrid();
			((ISupportInitialize)logEntriesGrid1).BeginInit();
			base.SuspendLayout();
			logEntriesGrid1.AllowEdit = InheritableBoolean.False;
			logEntriesGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			logEntriesGrid1.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowConditionEqual>xxxxx</FilterRowConditionEqual></LocalizableData>";
			logEntriesGrid1.ColumnAutoResize = true;
			logEntriesGrid1_DesignTimeLayout.LayoutString = resources.GetString("logEntriesGrid1_DesignTimeLayout.LayoutString");
			logEntriesGrid1.DesignTimeLayout = logEntriesGrid1_DesignTimeLayout;
			logEntriesGrid1.Dock = DockStyle.Fill;
			logEntriesGrid1.Font = new Font("Microsoft Sans Serif", 8.25f);
			logEntriesGrid1.GridLineColor = Color.WhiteSmoke;
			logEntriesGrid1.GridLines = GridLines.Horizontal;
			logEntriesGrid1.GroupByBoxVisible = false;
			logEntriesGrid1.HideSelection = HideSelection.HighlightInactive;
			logEntriesGrid1.Hierarchical = true;
			logEntriesGrid1.Location = new Point(0, 0);
			logEntriesGrid1.Name = "logEntriesGrid1";
			logEntriesGrid1.RepeatHeaders = InheritableBoolean.False;
			logEntriesGrid1.SelectedInactiveFormatStyle.BackColor = Color.WhiteSmoke;
			logEntriesGrid1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
			logEntriesGrid1.Size = new Size(0x238, 0x17e);
			logEntriesGrid1.TabIndex = 4;
			logEntriesGrid1.TreeLineColor = SystemColors.ControlLight;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(logEntriesGrid1);
			base.Name = "UpdatesGridContainer";
			base.Size = new Size(0x238, 0x17e);
			((ISupportInitialize)logEntriesGrid1).EndInit();
			base.ResumeLayout(false);
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