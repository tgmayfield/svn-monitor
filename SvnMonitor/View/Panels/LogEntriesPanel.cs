using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
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
	internal class LogEntriesPanel : GridPanel, ILogEntriesView, ISelectableView<SVNLogEntry>, ISupportInitialize, ISearchablePanel<SVNLogEntry>
	{
		private List<SVNLogEntry> allEntries;
		private UIRebar BottomRebar1;
		private UICommand cmdCopyAuthor;
		private UICommand cmdCopyAuthor1;
		private UICommand cmdCopyAuthor2;
		private UICommand cmdCopyDate;
		private UICommand cmdCopyDate1;
		private UICommand cmdCopyDate2;
		private UICommand cmdCopyLocalPaths;
		private UICommand cmdCopyLocalPaths1;
		private UICommand cmdCopyMessage;
		private UICommand cmdCopyMessage1;
		private UICommand cmdCopyMessage2;
		private UICommand cmdCopyPaths;
		private UICommand cmdCopyPaths1;
		private UICommand cmdCopyRevision;
		private UICommand cmdCopyRevision1;
		private UICommand cmdCopyRevision2;
		private UICommand cmdDiff;
		private UICommand cmdDiff1;
		private UICommand cmdDiff2;
		private UICommand cmdRecommend;
		private UICommand cmdRecommend1;
		private UICommand cmdRecommend2;
		private UICommand cmdRollback;
		private UICommand cmdRollback1;
		private UICommand cmdRollback2;
		private UICommand cmdShowDetails;
		private UICommand cmdShowDetails1;
		private UICommand cmdShowDetails2;
		private UICommand cmdSVNLog;
		private UICommand cmdSVNLog1;
		private UICommand cmdSVNLog2;
		private UICommand cmdSVNUpdate;
		private UICommand cmdSVNUpdate1;
		private UICommand cmdSVNUpdate2;
		private IContainer components;
		private long currentRevision;
		private Label lblNote;
		private UIRebar LeftRebar1;
		private LogEntriesGrid logEntriesGrid1;
		private UICommand menuAuthorWizards;
		private UICommand menuAuthorWizards1;
		private UICommand menuAuthorWizards2;
		private UICommand menuClipboard;
		private UICommand menuClipboard1;
		private Panel panel1;
		private Panel panelNote;
		private PictureBox pictureBox1;
		private UIRebar RightRebar1;
		private readonly Dictionary<SVNMonitor.Entities.Source, long> selectedRevisions = new Dictionary<SVNMonitor.Entities.Source, long>();
		private bool selectedWithKeyboard;
		private EventHandler selectionChanged;
		private UICommand Separator1;
		private UICommand Separator2;
		private UICommand Separator5;
		private UICommand Separator6;
		private SVNMonitor.Entities.Source source;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IUserEntityView<SVNMonitor.Entities.Source> sourcesView;
		private bool suspendSelectedRevision;
		private UIRebar TopRebar1;
		private UICommandBar uiCommandBar1;
		private UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
		private bool updatingDataSource;

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
			return string.Join(Environment.NewLine, entry.Paths.Select(p => p.DisplayName).ToArray<string>());
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
			return string.Join(Environment.NewLine, entry.Paths.Select(p => p.Name).ToArray<string>());
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

		private void InitializeComponent()
		{
			components = new Container();
			GridEXLayout logEntriesGrid1_DesignTimeLayout = new GridEXLayout();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(LogEntriesPanel));
			logEntriesGrid1 = new LogEntriesGrid();
			uiCommandManager1 = new UICommandManager(components);
			uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			cmdSVNUpdate2 = new UICommand("cmdSVNUpdate");
			cmdRollback1 = new UICommand("cmdRollback");
			cmdRecommend1 = new UICommand("cmdRecommend");
			Separator5 = new UICommand("Separator");
			cmdSVNLog1 = new UICommand("cmdSVNLog");
			cmdShowDetails2 = new UICommand("cmdShowDetails");
			cmdDiff1 = new UICommand("cmdDiff");
			menuClipboard1 = new UICommand("menuClipboard");
			Separator1 = new UICommand("Separator");
			menuAuthorWizards1 = new UICommand("menuAuthorWizards");
			BottomRebar1 = new UIRebar();
			uiCommandBar1 = new UICommandBar();
			cmdSVNUpdate1 = new UICommand("cmdSVNUpdate");
			cmdRollback2 = new UICommand("cmdRollback");
			cmdRecommend2 = new UICommand("cmdRecommend");
			Separator6 = new UICommand("Separator");
			cmdSVNLog2 = new UICommand("cmdSVNLog");
			cmdShowDetails1 = new UICommand("cmdShowDetails");
			cmdDiff2 = new UICommand("cmdDiff");
			Separator2 = new UICommand("Separator");
			menuAuthorWizards2 = new UICommand("menuAuthorWizards");
			cmdSVNLog = new UICommand("cmdSVNLog");
			menuAuthorWizards = new UICommand("menuAuthorWizards");
			cmdCopyMessage1 = new UICommand("cmdCopyMessage");
			cmdCopyAuthor1 = new UICommand("cmdCopyAuthor");
			cmdCopyRevision1 = new UICommand("cmdCopyRevision");
			cmdCopyDate1 = new UICommand("cmdCopyDate");
			cmdDiff = new UICommand("cmdDiff");
			cmdSVNUpdate = new UICommand("cmdSVNUpdate");
			cmdRollback = new UICommand("cmdRollback");
			cmdRecommend = new UICommand("cmdRecommend");
			menuClipboard = new UICommand("menuClipboard");
			cmdCopyMessage2 = new UICommand("cmdCopyMessage");
			cmdCopyAuthor2 = new UICommand("cmdCopyAuthor");
			cmdCopyRevision2 = new UICommand("cmdCopyRevision");
			cmdCopyDate2 = new UICommand("cmdCopyDate");
			cmdCopyPaths1 = new UICommand("cmdCopyPaths");
			cmdCopyLocalPaths1 = new UICommand("cmdCopyLocalPaths");
			cmdCopyRevision = new UICommand("cmdCopyRevision");
			cmdCopyAuthor = new UICommand("cmdCopyAuthor");
			cmdCopyDate = new UICommand("cmdCopyDate");
			cmdCopyMessage = new UICommand("cmdCopyMessage");
			cmdCopyPaths = new UICommand("cmdCopyPaths");
			cmdCopyLocalPaths = new UICommand("cmdCopyLocalPaths");
			cmdShowDetails = new UICommand("cmdShowDetails");
			LeftRebar1 = new UIRebar();
			RightRebar1 = new UIRebar();
			TopRebar1 = new UIRebar();
			panel1 = new Panel();
			panelNote = new Panel();
			lblNote = new Label();
			pictureBox1 = new PictureBox();
			((ISupportInitialize)logEntriesGrid1).BeginInit();
			((ISupportInitialize)uiCommandManager1).BeginInit();
			((ISupportInitialize)uiContextMenu1).BeginInit();
			((ISupportInitialize)BottomRebar1).BeginInit();
			((ISupportInitialize)uiCommandBar1).BeginInit();
			((ISupportInitialize)LeftRebar1).BeginInit();
			((ISupportInitialize)RightRebar1).BeginInit();
			((ISupportInitialize)TopRebar1).BeginInit();
			TopRebar1.SuspendLayout();
			panel1.SuspendLayout();
			panelNote.SuspendLayout();
			((ISupportInitialize)pictureBox1).BeginInit();
			base.SuspendLayout();
			logEntriesGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			logEntriesGrid1.AlternatingColors = true;
			logEntriesGrid1.AlternatingRowFormatStyle.BackColor = Color.WhiteSmoke;
			logEntriesGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			logEntriesGrid1.ColumnAutoResize = true;
			uiCommandManager1.SetContextMenu(logEntriesGrid1, uiContextMenu1);
			logEntriesGrid1_DesignTimeLayout.LayoutString = resources.GetString("logEntriesGrid1_DesignTimeLayout.LayoutString");
			logEntriesGrid1.DesignTimeLayout = logEntriesGrid1_DesignTimeLayout;
			logEntriesGrid1.Dock = DockStyle.Fill;
			logEntriesGrid1.EnterKeyBehavior = EnterKeyBehavior.None;
			logEntriesGrid1.Font = new Font("Microsoft Sans Serif", 8.25f);
			logEntriesGrid1.GridLineColor = Color.Gainsboro;
			logEntriesGrid1.GridLines = GridLines.Horizontal;
			logEntriesGrid1.GroupByBoxVisible = false;
			logEntriesGrid1.HideSelection = HideSelection.HighlightInactive;
			logEntriesGrid1.Location = new Point(0, 0x17);
			logEntriesGrid1.Name = "logEntriesGrid1";
			logEntriesGrid1.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
			logEntriesGrid1.SelectedFormatStyle.BackColor = Color.SteelBlue;
			logEntriesGrid1.SelectedFormatStyle.ForeColor = Color.White;
			logEntriesGrid1.SelectedInactiveFormatStyle.BackColor = Color.FromArgb(0xec, 0xf5, 0xff);
			logEntriesGrid1.SettingsKey = "logEntriesGrid1";
			logEntriesGrid1.Size = new Size(0x243, 0x14b);
			logEntriesGrid1.TabIndex = 4;
			logEntriesGrid1.TabKeyBehavior = TabKeyBehavior.ControlNavigation;
			logEntriesGrid1.ThemedAreas = ThemedArea.CheckBoxes | ThemedArea.Cards | ThemedArea.ControlBorder | ThemedArea.GroupRows | ThemedArea.TreeGliphs | ThemedArea.GroupByBox | ThemedArea.Headers | ThemedArea.EditControls | ThemedArea.ScrollBars;
			logEntriesGrid1.TreeLineColor = Color.Gainsboro;
			logEntriesGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			logEntriesGrid1.KeyDown += logEntriesGrid1_KeyDown;
			logEntriesGrid1.RowDoubleClick += logEntriesGrid1_RowDoubleClick;
			logEntriesGrid1.FormattingRow += logEntriesGrid1_FormattingRow;
			logEntriesGrid1.MouseDown += logEntriesGrid1_MouseDown;
			logEntriesGrid1.SelectionChanged += logEntriesGrid1_SelectionChanged;
			uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.BottomRebar = BottomRebar1;
			uiCommandManager1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			uiCommandManager1.Commands.AddRange(new[]
			{
				cmdSVNLog, menuAuthorWizards, cmdDiff, cmdSVNUpdate, cmdRollback, cmdRecommend, menuClipboard, cmdCopyRevision, cmdCopyAuthor, cmdCopyDate, cmdCopyMessage, cmdCopyPaths, cmdCopyLocalPaths, cmdShowDetails
			});
			uiCommandManager1.ContainerControl = this;
			uiCommandManager1.ContextMenus.AddRange(new[]
			{
				uiContextMenu1
			});
			uiCommandManager1.Id = new Guid("456fbda7-0fae-48a1-84cd-e9cc0c1a5be1");
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
				cmdSVNUpdate2, cmdRollback1, cmdRecommend1, Separator5, cmdSVNLog1, cmdShowDetails2, cmdDiff1, menuClipboard1, Separator1, menuAuthorWizards1
			});
			uiContextMenu1.Key = "ContextMenu1";
			uiContextMenu1.Popup += uiContextMenu1_Popup;
			cmdSVNUpdate2.Key = "cmdSVNUpdate";
			cmdSVNUpdate2.Name = "cmdSVNUpdate2";
			cmdSVNUpdate2.Text = "&Update to this revision";
			cmdRollback1.Key = "cmdRollback";
			cmdRollback1.Name = "cmdRollback1";
			cmdRollback1.Text = "&Rollback this revision";
			cmdRecommend1.Key = "cmdRecommend";
			cmdRecommend1.Name = "cmdRecommend1";
			cmdRecommend1.Text = "Rec&ommend";
			Separator5.CommandType = CommandType.Separator;
			Separator5.Key = "Separator";
			Separator5.Name = "Separator5";
			cmdSVNLog1.Key = "cmdSVNLog";
			cmdSVNLog1.Name = "cmdSVNLog1";
			cmdSVNLog1.Text = "Show &log";
			cmdShowDetails2.Key = "cmdShowDetails";
			cmdShowDetails2.Name = "cmdShowDetails2";
			cmdDiff1.Key = "cmdDiff";
			cmdDiff1.Name = "cmdDiff1";
			cmdDiff1.Text = "&Diff";
			menuClipboard1.Key = "menuClipboard";
			menuClipboard1.Name = "menuClipboard1";
			Separator1.CommandType = CommandType.Separator;
			Separator1.Key = "Separator";
			Separator1.Name = "Separator1";
			menuAuthorWizards1.Key = "menuAuthorWizards";
			menuAuthorWizards1.Name = "menuAuthorWizards1";
			menuAuthorWizards1.Text = "&Monitor this author";
			BottomRebar1.CommandManager = uiCommandManager1;
			BottomRebar1.Dock = DockStyle.Bottom;
			BottomRebar1.Location = new Point(0, 0x17e);
			BottomRebar1.Name = "BottomRebar1";
			BottomRebar1.Size = new Size(0x238, 0);
			uiCommandBar1.CommandManager = uiCommandManager1;
			uiCommandBar1.Commands.AddRange(new[]
			{
				cmdSVNUpdate1, cmdRollback2, cmdRecommend2, Separator6, cmdSVNLog2, cmdShowDetails1, cmdDiff2, Separator2, menuAuthorWizards2
			});
			uiCommandBar1.FullRow = true;
			uiCommandBar1.Key = "CommandBar1";
			uiCommandBar1.Location = new Point(0, 0);
			uiCommandBar1.Name = "uiCommandBar1";
			uiCommandBar1.RowIndex = 0;
			uiCommandBar1.Size = new Size(0x243, 0x1c);
			uiCommandBar1.Text = "CommandBar1";
			cmdSVNUpdate1.Key = "cmdSVNUpdate";
			cmdSVNUpdate1.Name = "cmdSVNUpdate1";
			cmdSVNUpdate1.Text = "Update";
			cmdRollback2.Key = "cmdRollback";
			cmdRollback2.Name = "cmdRollback2";
			cmdRollback2.Text = "Rollback";
			cmdRecommend2.Key = "cmdRecommend";
			cmdRecommend2.Name = "cmdRecommend2";
			Separator6.CommandType = CommandType.Separator;
			Separator6.Key = "Separator";
			Separator6.Name = "Separator6";
			cmdSVNLog2.Key = "cmdSVNLog";
			cmdSVNLog2.Name = "cmdSVNLog2";
			cmdShowDetails1.Key = "cmdShowDetails";
			cmdShowDetails1.Name = "cmdShowDetails1";
			cmdDiff2.Key = "cmdDiff";
			cmdDiff2.Name = "cmdDiff2";
			Separator2.CommandType = CommandType.Separator;
			Separator2.Key = "Separator";
			Separator2.Name = "Separator2";
			menuAuthorWizards2.CommandStyle = CommandStyle.TextImage;
			menuAuthorWizards2.Key = "menuAuthorWizards";
			menuAuthorWizards2.Name = "menuAuthorWizards2";
			menuAuthorWizards2.Text = "Monitor";
			menuAuthorWizards2.Click += menuAuthorWizards2_Click;
			cmdSVNLog.Image = (Image)resources.GetObject("cmdSVNLog.Image");
			cmdSVNLog.Key = "cmdSVNLog";
			cmdSVNLog.Name = "cmdSVNLog";
			cmdSVNLog.Text = "Show log";
			cmdSVNLog.ToolTipText = "Show log";
			cmdSVNLog.Click += cmdSVNLog_Click;
			menuAuthorWizards.Commands.AddRange(new[]
			{
				cmdCopyMessage1, cmdCopyAuthor1, cmdCopyRevision1, cmdCopyDate1
			});
			menuAuthorWizards.Image = (Image)resources.GetObject("menuAuthorWizards.Image");
			menuAuthorWizards.Key = "menuAuthorWizards";
			menuAuthorWizards.Name = "menuAuthorWizards";
			menuAuthorWizards.Text = "Monitor this author";
			menuAuthorWizards.ToolTipText = "Monitor this author";
			cmdCopyMessage1.Key = "cmdCopyMessage";
			cmdCopyMessage1.Name = "cmdCopyMessage1";
			cmdCopyAuthor1.Key = "cmdCopyAuthor";
			cmdCopyAuthor1.Name = "cmdCopyAuthor1";
			cmdCopyRevision1.Key = "cmdCopyRevision";
			cmdCopyRevision1.Name = "cmdCopyRevision1";
			cmdCopyDate1.Key = "cmdCopyDate";
			cmdCopyDate1.Name = "cmdCopyDate1";
			cmdDiff.Image = (Image)resources.GetObject("cmdDiff.Image");
			cmdDiff.Key = "cmdDiff";
			cmdDiff.Name = "cmdDiff";
			cmdDiff.Text = "Diff";
			cmdDiff.ToolTipText = "Diff with previous version";
			cmdDiff.Click += cmdDiff_Click;
			cmdSVNUpdate.Image = (Image)resources.GetObject("cmdSVNUpdate.Image");
			cmdSVNUpdate.Key = "cmdSVNUpdate";
			cmdSVNUpdate.Name = "cmdSVNUpdate";
			cmdSVNUpdate.Text = "Update to this revision";
			cmdSVNUpdate.ToolTipText = "Update to this revision";
			cmdSVNUpdate.Click += cmdSVNUpdate_Click;
			cmdRollback.Image = (Image)resources.GetObject("cmdRollback.Image");
			cmdRollback.Key = "cmdRollback";
			cmdRollback.Name = "cmdRollback";
			cmdRollback.Text = "Rollback this revision";
			cmdRollback.ToolTipText = "Rollback this revision";
			cmdRollback.Click += cmdRollback_Click;
			cmdRecommend.Image = (Image)resources.GetObject("cmdRecommend.Image");
			cmdRecommend.Key = "cmdRecommend";
			cmdRecommend.Name = "cmdRecommend";
			cmdRecommend.Text = "Recommend...";
			cmdRecommend.ToolTipText = "Recommend this revision to other users and commit the changes";
			cmdRecommend.Click += cmdRecommend_Click;
			menuClipboard.Commands.AddRange(new[]
			{
				cmdCopyMessage2, cmdCopyAuthor2, cmdCopyRevision2, cmdCopyDate2, cmdCopyPaths1, cmdCopyLocalPaths1
			});
			menuClipboard.Image = (Image)resources.GetObject("menuClipboard.Image");
			menuClipboard.Key = "menuClipboard";
			menuClipboard.Name = "menuClipboard";
			menuClipboard.Text = "&Copy to clipboard";
			menuClipboard.Popup += menuClipboard_Popup;
			cmdCopyMessage2.Key = "cmdCopyMessage";
			cmdCopyMessage2.Name = "cmdCopyMessage2";
			cmdCopyAuthor2.Key = "cmdCopyAuthor";
			cmdCopyAuthor2.Name = "cmdCopyAuthor2";
			cmdCopyRevision2.Key = "cmdCopyRevision";
			cmdCopyRevision2.Name = "cmdCopyRevision2";
			cmdCopyDate2.Key = "cmdCopyDate";
			cmdCopyDate2.Name = "cmdCopyDate2";
			cmdCopyPaths1.Key = "cmdCopyPaths";
			cmdCopyPaths1.Name = "cmdCopyPaths1";
			cmdCopyLocalPaths1.Key = "cmdCopyLocalPaths";
			cmdCopyLocalPaths1.Name = "cmdCopyLocalPaths1";
			cmdCopyRevision.Key = "cmdCopyRevision";
			cmdCopyRevision.Name = "cmdCopyRevision";
			cmdCopyRevision.Text = "&Revision";
			cmdCopyAuthor.Key = "cmdCopyAuthor";
			cmdCopyAuthor.Name = "cmdCopyAuthor";
			cmdCopyAuthor.Text = "&Author";
			cmdCopyDate.Key = "cmdCopyDate";
			cmdCopyDate.Name = "cmdCopyDate";
			cmdCopyDate.Text = "&Date and Time";
			cmdCopyMessage.Key = "cmdCopyMessage";
			cmdCopyMessage.Name = "cmdCopyMessage";
			cmdCopyMessage.Text = "&Message";
			cmdCopyPaths.Key = "cmdCopyPaths";
			cmdCopyPaths.Name = "cmdCopyPaths";
			cmdCopyPaths.Text = "&List Of Items";
			cmdCopyLocalPaths.Key = "cmdCopyLocalPaths";
			cmdCopyLocalPaths.Name = "cmdCopyLocalPaths";
			cmdCopyLocalPaths.Text = "List Of Local Items";
			cmdShowDetails.Image = (Image)resources.GetObject("cmdShowDetails.Image");
			cmdShowDetails.Key = "cmdShowDetails";
			cmdShowDetails.Name = "cmdShowDetails";
			cmdShowDetails.Text = "Show details";
			cmdShowDetails.Click += cmdShowDetails_Click;
			LeftRebar1.CommandManager = uiCommandManager1;
			LeftRebar1.Dock = DockStyle.Left;
			LeftRebar1.Location = new Point(0, 0);
			LeftRebar1.Name = "LeftRebar1";
			LeftRebar1.Size = new Size(0, 0x17e);
			RightRebar1.CommandManager = uiCommandManager1;
			RightRebar1.Dock = DockStyle.Right;
			RightRebar1.Location = new Point(0x238, 0);
			RightRebar1.Name = "RightRebar1";
			RightRebar1.Size = new Size(0, 0x17e);
			TopRebar1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			TopRebar1.CommandManager = uiCommandManager1;
			TopRebar1.Controls.Add(uiCommandBar1);
			TopRebar1.Dock = DockStyle.Top;
			TopRebar1.Location = new Point(0, 0);
			TopRebar1.Name = "TopRebar1";
			TopRebar1.Size = new Size(0x243, 0x1c);
			panel1.BackColor = Color.DimGray;
			panel1.Controls.Add(logEntriesGrid1);
			panel1.Controls.Add(panelNote);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0x1c);
			panel1.Name = "panel1";
			panel1.Padding = new Padding(0, 1, 0, 0);
			panel1.Size = new Size(0x243, 0x162);
			panel1.TabIndex = 5;
			panelNote.BackColor = Color.Gray;
			panelNote.Controls.Add(lblNote);
			panelNote.Controls.Add(pictureBox1);
			panelNote.Dock = DockStyle.Top;
			panelNote.Location = new Point(0, 1);
			panelNote.Name = "panelNote";
			panelNote.Padding = new Padding(2, 3, 2, 2);
			panelNote.Size = new Size(0x243, 0x16);
			panelNote.TabIndex = 5;
			panelNote.Visible = false;
			lblNote.Dock = DockStyle.Fill;
			lblNote.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
			lblNote.ForeColor = Color.White;
			lblNote.Location = new Point(20, 3);
			lblNote.Name = "lblNote";
			lblNote.Padding = new Padding(2, 1, 2, 2);
			lblNote.Size = new Size(0x22d, 0x11);
			lblNote.TabIndex = 0;
			lblNote.Text = "This source is readonly. All svn actions are disabled.";
			pictureBox1.Dock = DockStyle.Left;
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(2, 3);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(0x12, 0x11);
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(panel1);
			base.Controls.Add(TopRebar1);
			base.Name = "LogEntriesPanel";
			base.Size = new Size(0x243, 0x17e);
			((ISupportInitialize)logEntriesGrid1).EndInit();
			((ISupportInitialize)uiCommandManager1).EndInit();
			((ISupportInitialize)uiContextMenu1).EndInit();
			((ISupportInitialize)BottomRebar1).EndInit();
			((ISupportInitialize)uiCommandBar1).EndInit();
			((ISupportInitialize)LeftRebar1).EndInit();
			((ISupportInitialize)RightRebar1).EndInit();
			((ISupportInitialize)TopRebar1).EndInit();
			TopRebar1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panelNote.ResumeLayout(false);
			((ISupportInitialize)pictureBox1).EndInit();
			base.ResumeLayout(false);
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