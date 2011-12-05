﻿namespace SVNMonitor.View.Panels
{
    using Janus.Windows.GridEX;
    using SVNMonitor.Entities;
    using SVNMonitor.Helpers;
    using SVNMonitor.View.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal class UpdatesGridContainer : UserControl
    {
        private IContainer components;
        private LogEntriesGrid logEntriesGrid1;

        public UpdatesGridContainer()
        {
            this.InitializeComponent();
            UIHelper.ApplyResources(this.Grid, this);
            this.SetActionColumnValueList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            GridEXLayout logEntriesGrid1_DesignTimeLayout = new GridEXLayout();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(UpdatesGridContainer));
            this.logEntriesGrid1 = new LogEntriesGrid();
            ((ISupportInitialize) this.logEntriesGrid1).BeginInit();
            base.SuspendLayout();
            this.logEntriesGrid1.AllowEdit = InheritableBoolean.False;
            this.logEntriesGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.logEntriesGrid1.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowConditionEqual>xxxxx</FilterRowConditionEqual></LocalizableData>";
            this.logEntriesGrid1.ColumnAutoResize = true;
            logEntriesGrid1_DesignTimeLayout.LayoutString = resources.GetString("logEntriesGrid1_DesignTimeLayout.LayoutString");
            this.logEntriesGrid1.DesignTimeLayout = logEntriesGrid1_DesignTimeLayout;
            this.logEntriesGrid1.Dock = DockStyle.Fill;
            this.logEntriesGrid1.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.logEntriesGrid1.GridLineColor = Color.WhiteSmoke;
            this.logEntriesGrid1.GridLines = GridLines.Horizontal;
            this.logEntriesGrid1.GroupByBoxVisible = false;
            this.logEntriesGrid1.HideSelection = HideSelection.HighlightInactive;
            this.logEntriesGrid1.Hierarchical = true;
            this.logEntriesGrid1.Location = new Point(0, 0);
            this.logEntriesGrid1.Name = "logEntriesGrid1";
            this.logEntriesGrid1.RepeatHeaders = InheritableBoolean.False;
            this.logEntriesGrid1.SelectedInactiveFormatStyle.BackColor = Color.WhiteSmoke;
            this.logEntriesGrid1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.logEntriesGrid1.Size = new Size(0x238, 0x17e);
            this.logEntriesGrid1.TabIndex = 4;
            this.logEntriesGrid1.TreeLineColor = SystemColors.ControlLight;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.logEntriesGrid1);
            base.Name = "UpdatesGridContainer";
            base.Size = new Size(0x238, 0x17e);
            ((ISupportInitialize) this.logEntriesGrid1).EndInit();
            base.ResumeLayout(false);
        }

        private void SetActionColumnValueList()
        {
            GridEXColumn actionColumn = this.Grid.Tables["Paths"].Columns["Action"];
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
            get
            {
                return this.logEntriesGrid1;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<SVNLogEntry> LogEntries
        {
            [DebuggerNonUserCode]
            get
            {
                return ((LogEntriesGrid) this.Grid).LogEntries;
            }
            [DebuggerNonUserCode]
            set
            {
                ((LogEntriesGrid) this.Grid).LogEntries = value;
            }
        }
    }
}
