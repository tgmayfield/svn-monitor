using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Janus.Windows.Common.Layouts;
using Janus.Windows.GridEX;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Settings;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Interfaces;

namespace SVNMonitor.View.Panels
{
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
			InitializeComponent();
			if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
			{
				UIHelper.ApplyResources(uiCommandManager1);
				InitializeClipboardDelegates();
			}
		}

		private void AdjustDPI()
		{
			int dpi = 0x60;
			try
			{
				dpi = (int)base.CreateGraphics().DpiX;
				Logger.Log.DebugFormat("Adjusting to {0} dpi", dpi);
				GridEXColumn col = Grid.RootTable.Columns["DateTime"];
				col.Width = (130 * dpi) / 100;
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Error adjusting dpi: {0}", dpi), ex);
			}
		}

		public void ClearSearch()
		{
			SetEventList(SVNMonitor.EventLog.List);
		}

		private void cmdDeleteAll_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			DeleteAllEventLog();
		}

		private void cmdExport_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ExportEventLog();
		}

		private void DeleteAllEventLog()
		{
			SVNMonitor.EventLog.List.Clear();
			Refetch();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EnableCommands()
		{
			SVNMonitor.EventLogEntry entry = SelectedEntry;
			CanCopy = entry != null;
			CanOpen = entry != null;
			CanCopyError = (entry != null) && (entry.Exception != null);
			bool listNotEmpty = SVNMonitor.EventLog.List.Count > 0;
			CanExport = listNotEmpty;
			CanDeleteAll = false;
		}

		private void EventLog_AfterLog(object sender, EventArgs e)
		{
			Refetch();
		}

		private void EventLog_OpenEntry(object sender, EventArgs<long> e)
		{
			Logger.LogUserAction();
			FocusEventID(e.Item);
		}

		private void eventLogGrid1_SelectionChanged(object sender, EventArgs e)
		{
			EnableCommands();
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
			FocusEventID(id, true);
		}

		public void FocusEventID(long id, bool retryIfNotFound)
		{
			if (SVNMonitor.EventLog.List.Count != 0)
			{
				bool found = false;
				try
				{
					GridEXColumn idColumn = Grid.RootTable.Columns["ID"];
					found = Grid.Find(idColumn, ConditionOperator.Equal, id, 0, 1);
					Grid.Focus();
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
					SearchTextBox.ClearNoFocus();
					FocusEventID(id, false);
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
			SVNMonitor.EventLogEntry entry = SelectedEntry;
			if (entry == null)
			{
				return string.Empty;
			}
			return entry.ToString();
		}

		private string GetErrorToClipboard()
		{
			SVNMonitor.EventLogEntry entry = SelectedEntry;
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
			UIHelper.AddCopyCommand(cmdCopy, GetEntryDetailsToClipboard);
			UIHelper.AddCopyCommand(cmdCopyError, GetErrorToClipboard);
		}

		private void InitializeComponent()
		{
			components = new Container();
			GridEXLayout eventLogGrid1_DesignTimeLayout = new GridEXLayout();
			JanusLayoutReference eventLogGrid1_DesignTimeLayout_Reference_0 = new JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.HeaderImage");
			JanusLayoutReference eventLogGrid1_DesignTimeLayout_Reference_1 = new JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image");
			ComponentResourceManager resources = new ComponentResourceManager(typeof(EventLogPanel));
			eventLogGrid1 = new EventLogGrid();
			uiCommandManager1 = new UICommandManager(components);
			uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			cmdExport2 = new UICommand("cmdExport");
			cmdCopy2 = new UICommand("cmdCopy");
			cmdCopyError2 = new UICommand("cmdCopyError");
			cmdDeleteAll2 = new UICommand("cmdDeleteAll");
			BottomRebar1 = new UIRebar();
			uiCommandBar1 = new UICommandBar();
			cmdExport1 = new UICommand("cmdExport");
			cmdCopy1 = new UICommand("cmdCopy");
			cmdCopyError1 = new UICommand("cmdCopyError");
			cmdDeleteAll1 = new UICommand("cmdDeleteAll");
			cmdExport = new UICommand("cmdExport");
			cmdOpen = new UICommand("cmdOpen");
			cmdCopy = new UICommand("cmdCopy");
			cmdCopyError = new UICommand("cmdCopyError");
			cmdDeleteAll = new UICommand("cmdDeleteAll");
			LeftRebar1 = new UIRebar();
			RightRebar1 = new UIRebar();
			TopRebar1 = new UIRebar();
			panel1 = new Panel();
			((ISupportInitialize)eventLogGrid1).BeginInit();
			((ISupportInitialize)uiCommandManager1).BeginInit();
			((ISupportInitialize)uiContextMenu1).BeginInit();
			((ISupportInitialize)BottomRebar1).BeginInit();
			((ISupportInitialize)uiCommandBar1).BeginInit();
			((ISupportInitialize)LeftRebar1).BeginInit();
			((ISupportInitialize)RightRebar1).BeginInit();
			((ISupportInitialize)TopRebar1).BeginInit();
			TopRebar1.SuspendLayout();
			panel1.SuspendLayout();
			base.SuspendLayout();
			eventLogGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			eventLogGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			eventLogGrid1.ColumnAutoResize = true;
			uiCommandManager1.SetContextMenu(eventLogGrid1, uiContextMenu1);
			eventLogGrid1_DesignTimeLayout_Reference_0.Instance = resources.GetObject("eventLogGrid1_DesignTimeLayout_Reference_0.Instance");
			eventLogGrid1_DesignTimeLayout_Reference_1.Instance = resources.GetObject("eventLogGrid1_DesignTimeLayout_Reference_1.Instance");
			eventLogGrid1_DesignTimeLayout.LayoutReferences.AddRange(new[]
			{
				eventLogGrid1_DesignTimeLayout_Reference_0, eventLogGrid1_DesignTimeLayout_Reference_1
			});
			eventLogGrid1_DesignTimeLayout.LayoutString = resources.GetString("eventLogGrid1_DesignTimeLayout.LayoutString");
			eventLogGrid1.DesignTimeLayout = eventLogGrid1_DesignTimeLayout;
			eventLogGrid1.Dock = DockStyle.Fill;
			eventLogGrid1.EnterKeyBehavior = EnterKeyBehavior.None;
			eventLogGrid1.GridLineColor = SystemColors.Control;
			eventLogGrid1.GridLines = GridLines.Horizontal;
			eventLogGrid1.GroupByBoxVisible = false;
			eventLogGrid1.HideSelection = HideSelection.HighlightInactive;
			eventLogGrid1.Location = new Point(0, 1);
			eventLogGrid1.Name = "eventLogGrid1";
			eventLogGrid1.SelectedFormatStyle.BackColor = Color.SteelBlue;
			eventLogGrid1.SelectedInactiveFormatStyle.BackColor = Color.FromArgb(0xec, 0xf5, 0xff);
			eventLogGrid1.SettingsKey = "eventLogGrid1";
			eventLogGrid1.Size = new Size(0x24f, 0x120);
			eventLogGrid1.TabIndex = 0;
			eventLogGrid1.TabKeyBehavior = TabKeyBehavior.ControlNavigation;
			eventLogGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			eventLogGrid1.SelectionChanged += eventLogGrid1_SelectionChanged;
			uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.BottomRebar = BottomRebar1;
			uiCommandManager1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			uiCommandManager1.Commands.AddRange(new[]
			{
				cmdExport, cmdOpen, cmdCopy, cmdCopyError, cmdDeleteAll
			});
			uiCommandManager1.ContainerControl = this;
			uiCommandManager1.ContextMenus.AddRange(new[]
			{
				uiContextMenu1
			});
			uiCommandManager1.Id = new Guid("cf934eb0-aa69-4ae6-b41b-8ea4204b5814");
			uiCommandManager1.LeftRebar = LeftRebar1;
			uiCommandManager1.LockCommandBars = true;
			uiCommandManager1.RightRebar = RightRebar1;
			uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.ShowQuickCustomizeMenu = false;
			uiCommandManager1.TopRebar = TopRebar1;
			uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			uiContextMenu1.CommandManager = uiCommandManager1;
			uiContextMenu1.Commands.AddRange(new[]
			{
				cmdExport2, cmdCopy2, cmdCopyError2, cmdDeleteAll2
			});
			uiContextMenu1.Key = "ContextMenu1";
			uiContextMenu1.Popup += uiContextMenu1_Popup;
			cmdExport2.Key = "cmdExport";
			cmdExport2.Name = "cmdExport2";
			cmdCopy2.Key = "cmdCopy";
			cmdCopy2.Name = "cmdCopy2";
			cmdCopyError2.Key = "cmdCopyError";
			cmdCopyError2.Name = "cmdCopyError2";
			cmdDeleteAll2.Key = "cmdDeleteAll";
			cmdDeleteAll2.Name = "cmdDeleteAll2";
			BottomRebar1.CommandManager = uiCommandManager1;
			BottomRebar1.Dock = DockStyle.Bottom;
			BottomRebar1.Location = new Point(0, 0x13d);
			BottomRebar1.Name = "BottomRebar1";
			BottomRebar1.Size = new Size(0x24f, 0);
			uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.Animation = DropDownAnimation.System;
			uiCommandBar1.CommandManager = uiCommandManager1;
			uiCommandBar1.Commands.AddRange(new[]
			{
				cmdExport1, cmdCopy1, cmdCopyError1, cmdDeleteAll1
			});
			uiCommandBar1.FullRow = true;
			uiCommandBar1.Key = "CommandBar1";
			uiCommandBar1.Location = new Point(0, 0);
			uiCommandBar1.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
			uiCommandBar1.Name = "uiCommandBar1";
			uiCommandBar1.RowIndex = 0;
			uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.Size = new Size(0x24f, 0x1c);
			uiCommandBar1.Text = "CommandBar1";
			cmdExport1.Key = "cmdExport";
			cmdExport1.Name = "cmdExport1";
			cmdCopy1.Key = "cmdCopy";
			cmdCopy1.Name = "cmdCopy1";
			cmdCopyError1.Key = "cmdCopyError";
			cmdCopyError1.Name = "cmdCopyError1";
			cmdDeleteAll1.Key = "cmdDeleteAll";
			cmdDeleteAll1.Name = "cmdDeleteAll1";
			cmdExport.Image = (Image)resources.GetObject("cmdExport.Image");
			cmdExport.Key = "cmdExport";
			cmdExport.Name = "cmdExport";
			cmdExport.Text = "Export";
			cmdExport.ToolTipText = "Export list to file";
			cmdExport.Click += cmdExport_Click;
			cmdOpen.Image = (Image)resources.GetObject("cmdOpen.Image");
			cmdOpen.Key = "cmdOpen";
			cmdOpen.Name = "cmdOpen";
			cmdOpen.Text = "Open";
			cmdOpen.ToolTipText = "Open selected entry";
			cmdCopy.Image = (Image)resources.GetObject("cmdCopy.Image");
			cmdCopy.Key = "cmdCopy";
			cmdCopy.Name = "cmdCopy";
			cmdCopy.Text = "Copy";
			cmdCopy.ToolTipText = "Copy details to clipboard";
			cmdCopyError.Image = (Image)resources.GetObject("cmdCopyError.Image");
			cmdCopyError.Key = "cmdCopyError";
			cmdCopyError.Name = "cmdCopyError";
			cmdCopyError.Text = "Copy Error";
			cmdCopyError.ToolTipText = "Copy error details to clipboard";
			cmdDeleteAll.Image = (Image)resources.GetObject("cmdDeleteAll.Image");
			cmdDeleteAll.Key = "cmdDeleteAll";
			cmdDeleteAll.Name = "cmdDeleteAll";
			cmdDeleteAll.Text = "Delete All";
			cmdDeleteAll.ToolTipText = "Delete all messages";
			cmdDeleteAll.Click += cmdDeleteAll_Click;
			LeftRebar1.CommandManager = uiCommandManager1;
			LeftRebar1.Dock = DockStyle.Left;
			LeftRebar1.Location = new Point(0, 0x1c);
			LeftRebar1.Name = "LeftRebar1";
			LeftRebar1.Size = new Size(0, 0x121);
			RightRebar1.CommandManager = uiCommandManager1;
			RightRebar1.Dock = DockStyle.Right;
			RightRebar1.Location = new Point(0x24f, 0x1c);
			RightRebar1.Name = "RightRebar1";
			RightRebar1.Size = new Size(0, 0x121);
			TopRebar1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			TopRebar1.CommandManager = uiCommandManager1;
			TopRebar1.Controls.Add(uiCommandBar1);
			TopRebar1.Dock = DockStyle.Top;
			TopRebar1.Location = new Point(0, 0);
			TopRebar1.Name = "TopRebar1";
			TopRebar1.Size = new Size(0x24f, 0x1c);
			panel1.BackColor = Color.DimGray;
			panel1.Controls.Add(eventLogGrid1);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0x1c);
			panel1.Name = "panel1";
			panel1.Padding = new Padding(0, 1, 0, 0);
			panel1.Size = new Size(0x24f, 0x121);
			panel1.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(panel1);
			base.Controls.Add(TopRebar1);
			base.Name = "EventLogPanel";
			base.Size = new Size(0x24f, 0x13d);
			((ISupportInitialize)eventLogGrid1).EndInit();
			((ISupportInitialize)uiCommandManager1).EndInit();
			((ISupportInitialize)uiContextMenu1).EndInit();
			((ISupportInitialize)BottomRebar1).EndInit();
			((ISupportInitialize)uiCommandBar1).EndInit();
			((ISupportInitialize)LeftRebar1).EndInit();
			((ISupportInitialize)RightRebar1).EndInit();
			((ISupportInitialize)TopRebar1).EndInit();
			TopRebar1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
			{
				SetEventList(SVNMonitor.EventLog.List);
				SVNMonitor.EventLog.AfterLog += EventLog_AfterLog;
				SVNMonitor.EventLog.OpenEntry += EventLog_OpenEntry;
				AdjustDPI();
			}
		}

		private void Refetch()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(Refetch));
			}
			else
			{
				SearchTextBox.Search();
				Grid.Refetch();
				if (!Grid.Focused)
				{
					Grid.MoveLast();
				}
			}
		}

		private void SetEventList(IEnumerable<SVNMonitor.EventLogEntry> list)
		{
			Grid.DataSource = list;
		}

		public void SetSearchResults(IEnumerable<SVNMonitor.EventLogEntry> results)
		{
			SetEventList(results);
		}

		private void uiContextMenu1_Popup(object sender, EventArgs e)
		{
			UIHelper.RefreshCopyCommands(uiContextMenu1.Commands);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanCopy
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopy); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopy, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanCopyError
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopyError); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopyError, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanDeleteAll
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdDeleteAll); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdDeleteAll, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanExport
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdExport); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdExport, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanOpen
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdOpen); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdOpen, value); }
		}

		protected override Janus.Windows.GridEX.GridEX Grid
		{
			[DebuggerNonUserCode]
			get { return eventLogGrid1; }
		}

		protected override string LayoutSettings
		{
			get { return ApplicationSettingsManager.Settings.UIEventLogGridLayout; }
		}

		[Browsable(false)]
		public SearchTextBox<SVNMonitor.EventLogEntry> SearchTextBox { get; set; }

		private SVNMonitor.EventLogEntry SelectedEntry
		{
			get
			{
				if ((Grid.SelectedItems != null) && (Grid.SelectedItems.Count != 0))
				{
					return (SVNMonitor.EventLogEntry)Grid.SelectedItems[0].GetRow().DataRow;
				}
				return null;
			}
		}

		public Janus.Windows.UI.CommandBars.UIContextMenu UIContextMenu
		{
			get { return uiContextMenu1; }
		}
	}
}