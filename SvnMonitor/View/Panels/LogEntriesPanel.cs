using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using Janus.Windows.GridEX;
using Janus.Windows.UI.CommandBars;

using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.Settings;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Dialogs;
using SVNMonitor.View.Interfaces;
using SVNMonitor.Wizards;

namespace SVNMonitor.View.Panels
{
	internal partial class LogEntriesPanel : GridPanel, ILogEntriesView, ISelectableView<SVNLogEntry>, ISupportInitialize, ISearchablePanel<SVNLogEntry>
	{
		private List<SVNLogEntry> allEntries;
		private readonly Dictionary<SVNMonitor.Entities.Source, long> selectedRevisions = new Dictionary<SVNMonitor.Entities.Source, long>();
		private EventHandler selectionChanged;
		private bool selectedWithKeyboard;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IUserEntityView<SVNMonitor.Entities.Source> sourcesView;
		private bool suspendSelectedRevision;
		private bool updatingDataSource;
		private long currentRevision;
		
		public event EventHandler SelectionChanged
		{
			[DebuggerNonUserCode]
			add
			{
				selectionChanged = (EventHandler)Delegate.Combine(selectionChanged, value);
				Grid.SelectionChanged += value;
			}
			[DebuggerNonUserCode]
			remove
			{
				selectionChanged = (EventHandler)Delegate.Remove(selectionChanged, value);
				Grid.SelectionChanged -= value;
			}
		}

		public LogEntriesPanel()
		{
			InitializeComponent();
			if (!base.DesignMode)
			{
				UIHelper.ApplyResources(uiCommandManager1);
				UIHelper.ApplyResources(Grid, this);
				InitializeClipboardDelegates();
			}
		}

		public void BeginInit()
		{
		}

		public bool CanGetNextLogEntry(SVNLogEntry nextFrom)
		{
			try
			{
				int index = IndexOfLogEntry(nextFrom);
				return (LogEntries.Count > (index + 1));
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error checking if can get next log entry:", ex);
				return false;
			}
		}

		public bool CanGetPreviousLogEntry(SVNLogEntry previousFrom)
		{
			try
			{
				return (IndexOfLogEntry(previousFrom) > 0);
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error checking if can get next log entry:", ex);
				return false;
			}
		}

		public void ClearSearch()
		{
			SetLogEntries(allEntries);
		}

		private void cmdDiff_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenDiff();
		}

		private void cmdRecommend_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Recommend();
		}

		private void cmdRollback_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Rollback();
		}

		private void cmdShowDetails_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ShowDetails();
		}

		private void cmdSVNLog_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenSVNLog();
		}

		private void cmdSVNUpdate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdate();
		}

		private bool ConfirmUpdateOldRevision(SVNLogEntry entry, long revision)
		{
			SVNInfo info = entry.Source.GetInfo(true);
			return ((info == null) || ((info.Revision <= revision) || (RollbackPromptDialog.Prompt(base.ParentForm, revision) == DialogResult.Yes)));
		}

		private void EnableCommands()
		{
			SVNLogEntry entry = SelectedItem;
			CanOpenSVNLog = SVNLogEntryHelper.CanOpenSVNLog(entry);
			CanDiff = SVNLogEntryHelper.CanDiff(entry);
			CanSVNUpdate = SVNLogEntryHelper.CanSVNUpdate(entry);
			CanRollback = SVNLogEntryHelper.CanRollback(entry);
			CanRunAuthorWizard = SVNLogEntryHelper.CanRunAuthorWizard(entry);
			CanCopyToClipboard = SVNLogEntryHelper.CanCopyToClipboard(entry);
			CanCopyMessageToClipboard = SVNLogEntryHelper.CanCopyMessageToClipboard(entry);
			CanCopyAuthorToClipboard = SVNLogEntryHelper.CanCopyAuthorToClipboard(entry);
			CanCopyPaths = SVNLogEntryHelper.CanCopyPaths(entry);
			CanRecommend = SVNLogEntryHelper.CanRecommend(entry);
			CanShowDetails = SVNLogEntryHelper.CanShowDetails(entry);
		}

		public void EndInit()
		{
			if (!base.DesignMode)
			{
				SyncUIWithSettings();
				UIHelper.GetBaseName getLogEntryAuthor = item => ((SVNLogEntry)item).Author;
				UIHelper.InitializeWizardsMenu(this, uiContextMenu1, menuAuthorWizards, getLogEntryAuthor, "LogEntries", "Author");
				EnableCommands();
			}
		}

		private void FormatLogEntryRow(GridEXRow row)
		{
			SVNLogEntry entry = (SVNLogEntry)row.DataRow;
			if (entry != null)
			{
				try
				{
					GridEXCell imageCell = row.Cells["PossibleConflicted"];
					if (entry.Recommended)
					{
						imageCell.Image = Images.star_grey;
						imageCell.ToolTipText = Strings.ToolTipRecommended;
					}
					if (entry.Unread)
					{
						if (entry.Recommended)
						{
							imageCell.Image = Images.star_yellow;
							imageCell.ToolTipText = Strings.ToolTipUnreadAndRecommended;
						}
						else
						{
							imageCell.Image = Images.arrow_down_green;
							imageCell.ToolTipText = Strings.ToolTipUnread;
						}
					}
					if (entry.PossibleConflicted)
					{
						if (entry.Recommended)
						{
							imageCell.Image = Images.star_yellow_warning;
							imageCell.ToolTipText = Strings.ToolTipPossibleConflictAndRecommended;
						}
						else
						{
							imageCell.Image = Images.warning;
							imageCell.ToolTipText = Strings.ToolTipPossibleConflict;
						}
					}
					GridEXCell commitedOn = row.Cells["CommitedOn"];
					commitedOn.Text = commitedOn.Value.ToString();
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Error trying to format the LogEntries grid:", ex);
				}
				foreach (GridEXCell cell in row.Cells)
				{
					try
					{
						if (string.IsNullOrEmpty(cell.ToolTipText))
						{
							cell.ToolTipText = entry.Message;
						}
					}
					catch (Exception ex)
					{
						try
						{
							Logger.Log.Error(string.Format("Error setting ToolTipText in cell {0} (source={1}, revision={2})", cell.Column.Key, entry.Source, entry.Revision), ex);
						}
						catch
						{
						}
					}
				}
			}
		}

		public IEnumerable<SVNLogEntry> GetAllItems()
		{
			if (allEntries == null)
			{
				return new SVNLogEntry[0];
			}
			SVNLogEntry[] entries = new SVNLogEntry[allEntries.Count];
			allEntries.CopyTo(entries);
			return entries;
		}

		public SVNLogEntry GetNextLogEntry(SVNLogEntry nextFrom)
		{
			int index = IndexOfLogEntry(nextFrom);
			return LogEntries[index + 1];
		}

		public SVNLogEntry GetPreviousLogEntry(SVNLogEntry previousFrom)
		{
			int index = IndexOfLogEntry(previousFrom);
			return LogEntries[index - 1];
		}

		private string GetSelectedAuthorToClipboard()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry == null)
			{
				return string.Empty;
			}
			return entry.Author;
		}

		private string GetSelectedDateToClipboard()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry == null)
			{
				return string.Empty;
			}
			return entry.CommitedOn.ToString();
		}

		private string GetSelectedLocalPathsToClipboard()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry == null)
			{
				return string.Empty;
			}
			return string.Join(Environment.NewLine, entry.Paths.Select(p => p.DisplayName).ToArray());
		}

		private string GetSelectedMessageToClipboard()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry == null)
			{
				return string.Empty;
			}
			return entry.Message;
		}

		private string GetSelectedPathsToClipboard()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry == null)
			{
				return string.Empty;
			}
			return string.Join(Environment.NewLine, entry.Paths.Select(p => p.Name).ToArray());
		}

		private string GetSelectedRevisionToClipboard()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry == null)
			{
				return string.Empty;
			}
			return entry.Revision.ToString();
		}

		private void Grid_SelectionChanged(object sender, EventArgs e)
		{
			if (!updatingDataSource)
			{
				SVNLogEntry entry = SelectedItem;
				if ((entry != null) && !suspendSelectedRevision)
				{
					SelectedRevisions[Source] = entry.Revision;
				}
			}
		}

		private void HandleRowDoubleClick(GridEXRow row)
		{
			if ((row != null) && (row.DataRow is SVNLogEntry))
			{
				Logger.LogUserAction();
				PerformDefaultLogEntryAction();
			}
		}

		private void HandleSourceSelectionChanged()
		{
			lock (this)
			{
				if (Source != null)
				{
					Source.StatusChanged -= source_StatusChanged;
				}
				Source = SourcesView.SelectedItem;
				currentRevision = 0L;
				if (Source != null)
				{
					Source.StatusChanged += source_StatusChanged;
				}
				UpdateSelectedSource();
				EnableCommands();
			}
		}

		private int IndexOfLogEntry(SVNLogEntry entry)
		{
			return LogEntries.IndexOf(entry);
		}

		private void InitializeClipboardDelegates()
		{
			UIHelper.AddCopyCommand(cmdCopyAuthor, GetSelectedAuthorToClipboard);
			UIHelper.AddCopyCommand(cmdCopyDate, GetSelectedDateToClipboard);
			UIHelper.AddCopyCommand(cmdCopyRevision, GetSelectedRevisionToClipboard);
			UIHelper.AddCopyCommand(cmdCopyMessage, GetSelectedMessageToClipboard);
			UIHelper.AddCopyCommand(cmdCopyPaths, GetSelectedPathsToClipboard);
			UIHelper.AddCopyCommand(cmdCopyLocalPaths, GetSelectedLocalPathsToClipboard);
		}

		private void logEntriesGrid1_FormattingRow(object sender, RowLoadEventArgs e)
		{
			if (e.Row.DataRow is SVNLogEntry)
			{
				FormatLogEntryRow(e.Row);
			}
		}

		private void logEntriesGrid1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				Logger.LogUserAction();
				PerformDefaultLogEntryAction();
			}
			if ((((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down)) || ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right))) || ((e.KeyCode == Keys.Next) || (e.KeyCode == Keys.Prior)))
			{
				SelectedWithKeyboard = true;
			}
		}

		private void logEntriesGrid1_MouseDown(object sender, MouseEventArgs e)
		{
			SelectedWithKeyboard = false;
		}

		private void logEntriesGrid1_RowDoubleClick(object sender, RowActionEventArgs e)
		{
			Logger.LogUserAction();
			HandleRowDoubleClick(e.Row);
		}

		private void logEntriesGrid1_SelectionChanged(object sender, EventArgs e)
		{
			EnableCommands();
		}

		private void menuAuthorWizards2_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			RunCustomWizard();
		}

		private void menuClipboard_Popup(object sender, CommandEventArgs e)
		{
			UIHelper.RefreshCopyCommands(e.Command.Commands);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.DesignMode)
			{
				ApplicationSettingsManager.SavedSettings += (s, ea) => SyncUIWithSettings();
				Grid.SelectionChanged += Grid_SelectionChanged;
			}
		}

		protected virtual void OnSelectionChanged()
		{
			if (selectionChanged != null)
			{
				selectionChanged(this, EventArgs.Empty);
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible && ((Source != null) && !Source.IsUpToDate))
			{
				Grid.FirstRow = 0;
			}
		}

		private void OpenDiff()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry != null)
			{
				entry.Diff();
			}
		}

		private void OpenSVNLog()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry != null)
			{
				entry.OpenSVNLog();
			}
		}

		private void PerformDefaultLogEntryAction()
		{
			ShowDetails();
		}

		private void Recommend()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry != null)
			{
				entry.Recommend();
			}
		}

		private void Rollback()
		{
			SVNLogEntry entry = SelectedItem;
			if ((entry != null) && ConfirmUpdateOldRevision(entry, entry.Revision - 1L))
			{
				entry.Rollback();
			}
		}

		private void RunCustomWizard()
		{
			SVNLogEntry entry = SelectedItem;
			new CustomWizard().Run(entry.Author, "LogEntries", "Author");
		}

		private void SelectLastRecord()
		{
			long maxRevision = SVNLog.GetMaxRevision(LogEntries);
			SelectRevision(maxRevision);
		}

		protected void SelectRevision(long revision)
		{
			GridEXFilterCondition condition = new GridEXFilterCondition
			{
				Column = Grid.RootTable.Columns["Revision"],
				ConditionOperator = ConditionOperator.Equal,
				Value1 = revision
			};
			int found = 0;
			try
			{
				found = Grid.FindAll(condition);
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error in Grid.FindAll: ", ex);
			}
			if (found == 0)
			{
				Logger.Log.ErrorFormat("Tried to select revision {0}, but not found. (source={1})", revision, Source);
			}
		}

		private void SetDefaultPopupCommand()
		{
			cmdSVNLog.DefaultItem = Janus.Windows.UI.InheritableBoolean.True;
		}

		private void SetLogEntries()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(SetLogEntries));
			}
			else
			{
				SearchTextBox.Search();
				try
				{
					suspendSelectedRevision = true;
					Grid.Refetch();
					suspendSelectedRevision = false;
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Error trying to refetch log-entries:", ex);
				}
			}
		}

		private void SetLogEntries(IEnumerable<SVNLogEntry> entries)
		{
			updatingDataSource = true;
			((LogEntriesGrid)Grid).LogEntries = entries;
			ShowOrHideReadonlyNote();
			updatingDataSource = false;
			try
			{
				if (((Source != null) && (LogEntries != null)) && !SelectedRevisions.ContainsKey(Source))
				{
					SelectLastRecord();
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error selecting the last row.", ex);
			}
		}

		public void SetSearchResults(IEnumerable<SVNLogEntry> results)
		{
			SetLogEntries(results);
		}

		private void ShowDetails()
		{
			SVNLogEntry entry = SelectedItem;
			if (entry != null)
			{
				LogEntryDetailsDialog.ShowLogEntryDetails(this, entry);
			}
		}

		private void ShowOrHideReadonlyNote()
		{
			panelNote.Visible = (Source != null) && Source.IsURL;
		}

		private void source_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			if ((e.Reason != StatusChangedReason.Updating) && (e.Reason != StatusChangedReason.Deleted))
			{
				SVNMonitor.Entities.Source updatedSource = (SVNMonitor.Entities.Source)e.Entity;
				if ((Source != null) && (Source == updatedSource))
				{
					UpdateSelectedSourceSafe();
				}
			}
		}

		private void sourcesView_SelectionChanged(object sender, EventArgs e)
		{
			HandleSourceSelectionChanged();
		}

		object ISelectableView<SVNLogEntry>.Invoke(Delegate delegate1)
		{
			return base.Invoke(delegate1);
		}

		private void SVNUpdate()
		{
			SVNLogEntry entry = SelectedItem;
			if ((entry != null) && ConfirmUpdateOldRevision(entry, entry.Revision))
			{
				entry.SVNUpdate();
			}
		}

		private void SyncUIWithSettings()
		{
			try
			{
				if (!base.DesignMode)
				{
					GroupByBoxVisible = ApplicationSettingsManager.Settings.LogGroupByBox;
					Grid.RootTable.PreviewRowLines = ApplicationSettingsManager.Settings.PreviewRowLines;
					Grid.Refetch();
				}
			}
			catch (Exception ex)
			{
				Logger.Log.ErrorFormat("Error synchronizing the Log-Entries panel with the settings: ", ex);
			}
		}

		private void uiContextMenu1_Popup(object sender, EventArgs e)
		{
			SetDefaultPopupCommand();
		}

		private void UpdateSelectedSource()
		{
			try
			{
				if ((Source != null) && Source.HasLog)
				{
					if (currentRevision <= Source.Revision)
					{
						SVNLog log = Source.GetLog(false);
						if (log != null)
						{
							LogEntries = log.LogEntries;
							currentRevision = Source.Revision;
							if (SelectedRevisions.ContainsKey(Source))
							{
								long previousRevision = SelectedRevisions[Source];
								try
								{
									SelectRevision(previousRevision);
								}
								catch (Exception ex)
								{
									Logger.Log.Debug(string.Format("Error selecting previous revision. Previous revision: {0}. Row count: {1}", previousRevision, Grid.RowCount), ex);
								}
							}
						}
						else
						{
							LogEntries = null;
						}
					}
				}
				else
				{
					Logger.Log.DebugFormat("Source does not have a log: {0}", Source);
					LogEntries = null;
					currentRevision = 0L;
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message, ex);
				ErrorHandler.Append(ex.Message, Source, ex);
				LogEntries = null;
			}
			finally
			{
				OnSelectionChanged();
			}
		}

		private void UpdateSelectedSourceSafe()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(UpdateSelectedSourceSafe));
			}
			else
			{
				UpdateSelectedSource();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyAuthorToClipboard
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopyAuthor); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopyAuthor, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyMessageToClipboard
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopyMessage); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopyMessage, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyPaths
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopyPaths); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopyPaths, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyToClipboard
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(menuClipboard); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(menuClipboard, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanDiff
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdDiff); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdDiff, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanOpenSVNLog
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdSVNLog); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdSVNLog, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanRecommend
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdRecommend); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdRecommend, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanRollback
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdRollback); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdRollback, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanRunAuthorWizard
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(menuAuthorWizards); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(menuAuthorWizards, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanShowDetails
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdShowDetails); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdShowDetails, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanSVNUpdate
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdSVNUpdate); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdSVNUpdate, value); }
		}

		protected override Janus.Windows.GridEX.GridEX Grid
		{
			[DebuggerNonUserCode]
			get { return logEntriesGrid1; }
		}

		[Category("Table View"), DefaultValue(true), Description("Determines whether the 'Group By Box' should be drawn.")]
		public bool GroupByBoxVisible
		{
			[DebuggerNonUserCode]
			get { return Grid.GroupByBoxVisible; }
			[DebuggerNonUserCode]
			set { Grid.GroupByBoxVisible = value; }
		}

		protected override string LayoutSettings
		{
			get { return ApplicationSettingsManager.Settings.UILogEntriesGridLayout; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public List<SVNLogEntry> LogEntries
		{
			[DebuggerNonUserCode]
			get { return allEntries; }
			set
			{
				allEntries = value;
				SetLogEntries();
			}
		}

		[Browsable(false)]
		public SearchTextBox<SVNLogEntry> SearchTextBox { get; set; }

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SVNLogEntry SelectedItem
		{
			[DebuggerNonUserCode]
			get { return ((LogEntriesGrid)Grid).SelectedLogEntry; }
		}

		private Dictionary<SVNMonitor.Entities.Source, long> SelectedRevisions
		{
			[DebuggerNonUserCode]
			get { return selectedRevisions; }
		}

		public bool SelectedWithKeyboard
		{
			[DebuggerNonUserCode]
			get { return selectedWithKeyboard; }
			[DebuggerNonUserCode]
			set { selectedWithKeyboard = value; }
		}

		private SVNMonitor.Entities.Source Source
		{
			get { return source; }
			set { source = value; }
		}

		[Browsable(true)]
		public IUserEntityView<SVNMonitor.Entities.Source> SourcesView
		{
			[DebuggerNonUserCode]
			get { return sourcesView; }
			set
			{
				if (sourcesView != null)
				{
					sourcesView.SelectionChanged -= sourcesView_SelectionChanged;
				}
				sourcesView = value;
				if (sourcesView != null)
				{
					sourcesView.SelectionChanged += sourcesView_SelectionChanged;
				}
			}
		}

		public Janus.Windows.UI.CommandBars.UIContextMenu UIContextMenu
		{
			get { return uiContextMenu1; }
		}
	}
}