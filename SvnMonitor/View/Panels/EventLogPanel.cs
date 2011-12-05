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
	internal partial class EventLogPanel : GridPanel, ISearchablePanel<SVNMonitor.EventLogEntry>
	{

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