﻿namespace SVNMonitor.View.Panels
{
    using Janus.Windows.Common.Layouts;
    using Janus.Windows.GridEX;
    using Janus.Windows.UI;
    using Janus.Windows.UI.CommandBars;
    using SVNMonitor;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Settings;
    using SVNMonitor.View.Controls;
    using SVNMonitor.View.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class EventLogPanel : GridPanel, ISearchablePanel<SVNMonitor.EventLogEntry>
    {
        private UIRebar BottomRebar1;
        private UICommand cmdCopy;
        private UICommand cmdCopy1;
        private UICommand cmdCopy2;
        private UICommand cmdCopyError;
        private UICommand cmdCopyError1;
        private UICommand cmdCopyError2;
        private UICommand cmdDeleteAll;
        private UICommand cmdDeleteAll1;
        private UICommand cmdDeleteAll2;
        private UICommand cmdExport;
        private UICommand cmdExport1;
        private UICommand cmdExport2;
        private UICommand cmdOpen;
        private IContainer components;
        private EventLogGrid eventLogGrid1;
        private UIRebar LeftRebar1;
        private Panel panel1;
        private UIRebar RightRebar1;
        private UIRebar TopRebar1;
        private UICommandBar uiCommandBar1;
        private UICommandManager uiCommandManager1;
        private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;

        public EventLogPanel()
        {
            this.InitializeComponent();
            if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
            {
                UIHelper.ApplyResources(this.uiCommandManager1);
                this.InitializeClipboardDelegates();
            }
        }

        private void AdjustDPI()
        {
            int dpi = 0x60;
            try
            {
                dpi = (int) base.CreateGraphics().DpiX;
                Logger.Log.DebugFormat("Adjusting to {0} dpi", dpi);
                GridEXColumn col = this.Grid.RootTable.Columns["DateTime"];
                col.Width = (130 * dpi) / 100;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error adjusting dpi: {0}", dpi), ex);
            }
        }

        public void ClearSearch()
        {
            this.SetEventList(SVNMonitor.EventLog.List);
        }

        private void cmdDeleteAll_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.DeleteAllEventLog();
        }

        private void cmdExport_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.ExportEventLog();
        }

        private void DeleteAllEventLog()
        {
            SVNMonitor.EventLog.List.Clear();
            this.Refetch();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EnableCommands()
        {
            SVNMonitor.EventLogEntry entry = this.SelectedEntry;
            this.CanCopy = entry != null;
            this.CanOpen = entry != null;
            this.CanCopyError = (entry != null) && (entry.Exception != null);
            bool listNotEmpty = SVNMonitor.EventLog.List.Count > 0;
            this.CanExport = listNotEmpty;
            this.CanDeleteAll = false;
        }

        private void EventLog_AfterLog(object sender, EventArgs e)
        {
            this.Refetch();
        }

        private void EventLog_OpenEntry(object sender, EventArgs<long> e)
        {
            Logger.LogUserAction();
            this.FocusEventID(e.Item);
        }

        private void eventLogGrid1_SelectionChanged(object sender, EventArgs e)
        {
            this.EnableCommands();
        }

        private void ExportEventLog()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.DefaultExt = ".txt";
                dialog.AddExtension = true;
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                dialog.Title = "Export As...";
                dialog.Filter = "Text Documents (*.txt)|*.txt";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        FileSystemHelper.WriteAllText(dialog.FileName, SVNMonitor.EventLog.ListString);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler.Append(string.Format("Error exporting the event-log to '{0}'", dialog.FileName), this, ex);
                    }
                }
            }
        }

        public void FocusEventID(long id)
        {
            this.FocusEventID(id, true);
        }

        public void FocusEventID(long id, bool retryIfNotFound)
        {
            if (SVNMonitor.EventLog.List.Count != 0)
            {
                bool found = false;
                try
                {
                    GridEXColumn idColumn = this.Grid.RootTable.Columns["ID"];
                    found = this.Grid.Find(idColumn, ConditionOperator.Equal, id, 0, 1);
                    this.Grid.Focus();
                }
                catch (ArgumentOutOfRangeException aex)
                {
                    Logger.Log.Debug(string.Format("Event ID {0} is not in the grid.", id), aex);
                }
                catch (IndexOutOfRangeException iex)
                {
                    Logger.Log.Debug(string.Format("Event ID {0} is not in the grid.", id), iex);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Can't find the requested event.", ex);
                }
                if (!found && retryIfNotFound)
                {
                    Logger.Log.DebugFormat("Event ID {0} not found. Clearing the search and trying again.", id);
                    this.SearchTextBox.ClearNoFocus();
                    this.FocusEventID(id, false);
                }
            }
        }

        public IEnumerable<SVNMonitor.EventLogEntry> GetAllItems()
        {
            SVNMonitor.EventLogEntry[] entries = new SVNMonitor.EventLogEntry[SVNMonitor.EventLog.List.Count];
            SVNMonitor.EventLog.List.CopyTo(entries);
            return entries;
        }

        private string GetEntryDetailsToClipboard()
        {
            SVNMonitor.EventLogEntry entry = this.SelectedEntry;
            if (entry == null)
            {
                return string.Empty;
            }
            return entry.ToString();
        }

        private string GetErrorToClipboard()
        {
            SVNMonitor.EventLogEntry entry = this.SelectedEntry;
            if (entry == null)
            {
                return string.Empty;
            }
            if (!entry.HasException)
            {
                return string.Empty;
            }
            return entry.ToErrorString();
        }

        private void InitializeClipboardDelegates()
        {
            UIHelper.AddCopyCommand(this.cmdCopy, new UIHelper.GetStringDelegate(this.GetEntryDetailsToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyError, new UIHelper.GetStringDelegate(this.GetErrorToClipboard));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            GridEXLayout eventLogGrid1_DesignTimeLayout = new GridEXLayout();
            JanusLayoutReference eventLogGrid1_DesignTimeLayout_Reference_0 = new JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.HeaderImage");
            JanusLayoutReference eventLogGrid1_DesignTimeLayout_Reference_1 = new JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(EventLogPanel));
            this.eventLogGrid1 = new EventLogGrid();
            this.uiCommandManager1 = new UICommandManager(this.components);
            this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
            this.cmdExport2 = new UICommand("cmdExport");
            this.cmdCopy2 = new UICommand("cmdCopy");
            this.cmdCopyError2 = new UICommand("cmdCopyError");
            this.cmdDeleteAll2 = new UICommand("cmdDeleteAll");
            this.BottomRebar1 = new UIRebar();
            this.uiCommandBar1 = new UICommandBar();
            this.cmdExport1 = new UICommand("cmdExport");
            this.cmdCopy1 = new UICommand("cmdCopy");
            this.cmdCopyError1 = new UICommand("cmdCopyError");
            this.cmdDeleteAll1 = new UICommand("cmdDeleteAll");
            this.cmdExport = new UICommand("cmdExport");
            this.cmdOpen = new UICommand("cmdOpen");
            this.cmdCopy = new UICommand("cmdCopy");
            this.cmdCopyError = new UICommand("cmdCopyError");
            this.cmdDeleteAll = new UICommand("cmdDeleteAll");
            this.LeftRebar1 = new UIRebar();
            this.RightRebar1 = new UIRebar();
            this.TopRebar1 = new UIRebar();
            this.panel1 = new Panel();
            ((ISupportInitialize) this.eventLogGrid1).BeginInit();
            ((ISupportInitialize) this.uiCommandManager1).BeginInit();
            ((ISupportInitialize) this.uiContextMenu1).BeginInit();
            ((ISupportInitialize) this.BottomRebar1).BeginInit();
            ((ISupportInitialize) this.uiCommandBar1).BeginInit();
            ((ISupportInitialize) this.LeftRebar1).BeginInit();
            ((ISupportInitialize) this.RightRebar1).BeginInit();
            ((ISupportInitialize) this.TopRebar1).BeginInit();
            this.TopRebar1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.eventLogGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.eventLogGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.eventLogGrid1.ColumnAutoResize = true;
            this.uiCommandManager1.SetContextMenu(this.eventLogGrid1, this.uiContextMenu1);
            eventLogGrid1_DesignTimeLayout_Reference_0.Instance = resources.GetObject("eventLogGrid1_DesignTimeLayout_Reference_0.Instance");
            eventLogGrid1_DesignTimeLayout_Reference_1.Instance = resources.GetObject("eventLogGrid1_DesignTimeLayout_Reference_1.Instance");
            eventLogGrid1_DesignTimeLayout.LayoutReferences.AddRange(new JanusLayoutReference[] { eventLogGrid1_DesignTimeLayout_Reference_0, eventLogGrid1_DesignTimeLayout_Reference_1 });
            eventLogGrid1_DesignTimeLayout.LayoutString = resources.GetString("eventLogGrid1_DesignTimeLayout.LayoutString");
            this.eventLogGrid1.DesignTimeLayout = eventLogGrid1_DesignTimeLayout;
            this.eventLogGrid1.Dock = DockStyle.Fill;
            this.eventLogGrid1.EnterKeyBehavior = EnterKeyBehavior.None;
            this.eventLogGrid1.GridLineColor = SystemColors.Control;
            this.eventLogGrid1.GridLines = GridLines.Horizontal;
            this.eventLogGrid1.GroupByBoxVisible = false;
            this.eventLogGrid1.HideSelection = HideSelection.HighlightInactive;
            this.eventLogGrid1.Location = new Point(0, 1);
            this.eventLogGrid1.Name = "eventLogGrid1";
            this.eventLogGrid1.SelectedFormatStyle.BackColor = Color.SteelBlue;
            this.eventLogGrid1.SelectedInactiveFormatStyle.BackColor = Color.FromArgb(0xec, 0xf5, 0xff);
            this.eventLogGrid1.SettingsKey = "eventLogGrid1";
            this.eventLogGrid1.Size = new Size(0x24f, 0x120);
            this.eventLogGrid1.TabIndex = 0;
            this.eventLogGrid1.TabKeyBehavior = TabKeyBehavior.ControlNavigation;
            this.eventLogGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.eventLogGrid1.SelectionChanged += new EventHandler(this.eventLogGrid1_SelectionChanged);
            this.uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.BottomRebar = this.BottomRebar1;
            this.uiCommandManager1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.uiCommandManager1.Commands.AddRange(new UICommand[] { this.cmdExport, this.cmdOpen, this.cmdCopy, this.cmdCopyError, this.cmdDeleteAll });
            this.uiCommandManager1.ContainerControl = this;
            this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] { this.uiContextMenu1 });
            this.uiCommandManager1.Id = new Guid("cf934eb0-aa69-4ae6-b41b-8ea4204b5814");
            this.uiCommandManager1.LeftRebar = this.LeftRebar1;
            this.uiCommandManager1.LockCommandBars = true;
            this.uiCommandManager1.RightRebar = this.RightRebar1;
            this.uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.ShowQuickCustomizeMenu = false;
            this.uiCommandManager1.TopRebar = this.TopRebar1;
            this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
            this.uiContextMenu1.CommandManager = this.uiCommandManager1;
            this.uiContextMenu1.Commands.AddRange(new UICommand[] { this.cmdExport2, this.cmdCopy2, this.cmdCopyError2, this.cmdDeleteAll2 });
            this.uiContextMenu1.Key = "ContextMenu1";
            this.uiContextMenu1.Popup += new EventHandler(this.uiContextMenu1_Popup);
            this.cmdExport2.Key = "cmdExport";
            this.cmdExport2.Name = "cmdExport2";
            this.cmdCopy2.Key = "cmdCopy";
            this.cmdCopy2.Name = "cmdCopy2";
            this.cmdCopyError2.Key = "cmdCopyError";
            this.cmdCopyError2.Name = "cmdCopyError2";
            this.cmdDeleteAll2.Key = "cmdDeleteAll";
            this.cmdDeleteAll2.Name = "cmdDeleteAll2";
            this.BottomRebar1.CommandManager = this.uiCommandManager1;
            this.BottomRebar1.Dock = DockStyle.Bottom;
            this.BottomRebar1.Location = new Point(0, 0x13d);
            this.BottomRebar1.Name = "BottomRebar1";
            this.BottomRebar1.Size = new Size(0x24f, 0);
            this.uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.Animation = DropDownAnimation.System;
            this.uiCommandBar1.CommandManager = this.uiCommandManager1;
            this.uiCommandBar1.Commands.AddRange(new UICommand[] { this.cmdExport1, this.cmdCopy1, this.cmdCopyError1, this.cmdDeleteAll1 });
            this.uiCommandBar1.FullRow = true;
            this.uiCommandBar1.Key = "CommandBar1";
            this.uiCommandBar1.Location = new Point(0, 0);
            this.uiCommandBar1.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
            this.uiCommandBar1.Name = "uiCommandBar1";
            this.uiCommandBar1.RowIndex = 0;
            this.uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.Size = new Size(0x24f, 0x1c);
            this.uiCommandBar1.Text = "CommandBar1";
            this.cmdExport1.Key = "cmdExport";
            this.cmdExport1.Name = "cmdExport1";
            this.cmdCopy1.Key = "cmdCopy";
            this.cmdCopy1.Name = "cmdCopy1";
            this.cmdCopyError1.Key = "cmdCopyError";
            this.cmdCopyError1.Name = "cmdCopyError1";
            this.cmdDeleteAll1.Key = "cmdDeleteAll";
            this.cmdDeleteAll1.Name = "cmdDeleteAll1";
            this.cmdExport.Image = (Image) resources.GetObject("cmdExport.Image");
            this.cmdExport.Key = "cmdExport";
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Text = "Export";
            this.cmdExport.ToolTipText = "Export list to file";
            this.cmdExport.Click += new CommandEventHandler(this.cmdExport_Click);
            this.cmdOpen.Image = (Image) resources.GetObject("cmdOpen.Image");
            this.cmdOpen.Key = "cmdOpen";
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Text = "Open";
            this.cmdOpen.ToolTipText = "Open selected entry";
            this.cmdCopy.Image = (Image) resources.GetObject("cmdCopy.Image");
            this.cmdCopy.Key = "cmdCopy";
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Text = "Copy";
            this.cmdCopy.ToolTipText = "Copy details to clipboard";
            this.cmdCopyError.Image = (Image) resources.GetObject("cmdCopyError.Image");
            this.cmdCopyError.Key = "cmdCopyError";
            this.cmdCopyError.Name = "cmdCopyError";
            this.cmdCopyError.Text = "Copy Error";
            this.cmdCopyError.ToolTipText = "Copy error details to clipboard";
            this.cmdDeleteAll.Image = (Image) resources.GetObject("cmdDeleteAll.Image");
            this.cmdDeleteAll.Key = "cmdDeleteAll";
            this.cmdDeleteAll.Name = "cmdDeleteAll";
            this.cmdDeleteAll.Text = "Delete All";
            this.cmdDeleteAll.ToolTipText = "Delete all messages";
            this.cmdDeleteAll.Click += new CommandEventHandler(this.cmdDeleteAll_Click);
            this.LeftRebar1.CommandManager = this.uiCommandManager1;
            this.LeftRebar1.Dock = DockStyle.Left;
            this.LeftRebar1.Location = new Point(0, 0x1c);
            this.LeftRebar1.Name = "LeftRebar1";
            this.LeftRebar1.Size = new Size(0, 0x121);
            this.RightRebar1.CommandManager = this.uiCommandManager1;
            this.RightRebar1.Dock = DockStyle.Right;
            this.RightRebar1.Location = new Point(0x24f, 0x1c);
            this.RightRebar1.Name = "RightRebar1";
            this.RightRebar1.Size = new Size(0, 0x121);
            this.TopRebar1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.TopRebar1.CommandManager = this.uiCommandManager1;
            this.TopRebar1.Controls.Add(this.uiCommandBar1);
            this.TopRebar1.Dock = DockStyle.Top;
            this.TopRebar1.Location = new Point(0, 0);
            this.TopRebar1.Name = "TopRebar1";
            this.TopRebar1.Size = new Size(0x24f, 0x1c);
            this.panel1.BackColor = Color.DimGray;
            this.panel1.Controls.Add(this.eventLogGrid1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x1c);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(0, 1, 0, 0);
            this.panel1.Size = new Size(0x24f, 0x121);
            this.panel1.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.TopRebar1);
            base.Name = "EventLogPanel";
            base.Size = new Size(0x24f, 0x13d);
            ((ISupportInitialize) this.eventLogGrid1).EndInit();
            ((ISupportInitialize) this.uiCommandManager1).EndInit();
            ((ISupportInitialize) this.uiContextMenu1).EndInit();
            ((ISupportInitialize) this.BottomRebar1).EndInit();
            ((ISupportInitialize) this.uiCommandBar1).EndInit();
            ((ISupportInitialize) this.LeftRebar1).EndInit();
            ((ISupportInitialize) this.RightRebar1).EndInit();
            ((ISupportInitialize) this.TopRebar1).EndInit();
            this.TopRebar1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
            {
                this.SetEventList(SVNMonitor.EventLog.List);
                SVNMonitor.EventLog.AfterLog += new EventHandler(this.EventLog_AfterLog);
                SVNMonitor.EventLog.OpenEntry += new EventHandler<EventArgs<long>>(this.EventLog_OpenEntry);
                this.AdjustDPI();
            }
        }

        private void Refetch()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.Refetch));
            }
            else
            {
                this.SearchTextBox.Search();
                this.Grid.Refetch();
                if (!this.Grid.Focused)
                {
                    this.Grid.MoveLast();
                }
            }
        }

        private void SetEventList(IEnumerable<SVNMonitor.EventLogEntry> list)
        {
            this.Grid.DataSource = list;
        }

        public void SetSearchResults(IEnumerable<SVNMonitor.EventLogEntry> results)
        {
            this.SetEventList(results);
        }

        private void uiContextMenu1_Popup(object sender, EventArgs e)
        {
            UIHelper.RefreshCopyCommands(this.uiContextMenu1.Commands);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanCopy
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopy);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopy, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanCopyError
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopyError);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopyError, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanDeleteAll
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandVisible(this.cmdDeleteAll);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandVisible(this.cmdDeleteAll, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanExport
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdExport);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdExport, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanOpen
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdOpen);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdOpen, value);
            }
        }

        protected override Janus.Windows.GridEX.GridEX Grid
        {
            [DebuggerNonUserCode]
            get
            {
                return this.eventLogGrid1;
            }
        }

        protected override string LayoutSettings
        {
            get
            {
                return ApplicationSettingsManager.Settings.UIEventLogGridLayout;
            }
        }

        [Browsable(false)]
        public SearchTextBox<SVNMonitor.EventLogEntry> SearchTextBox { get; set; }

        private SVNMonitor.EventLogEntry SelectedEntry
        {
            get
            {
                if ((this.Grid.SelectedItems != null) && (this.Grid.SelectedItems.Count != 0))
                {
                    return (SVNMonitor.EventLogEntry) this.Grid.SelectedItems[0].GetRow().DataRow;
                }
                return null;
            }
        }

        public Janus.Windows.UI.CommandBars.UIContextMenu UIContextMenu
        {
            get
            {
                return this.uiContextMenu1;
            }
        }
    }
}

