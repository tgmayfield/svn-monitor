namespace SVNMonitor.View.Panels
{
    using Janus.Windows.GridEX;
    using Janus.Windows.UI;
    using Janus.Windows.UI.CommandBars;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.View.Controls;
    using SVNMonitor.View.Dialogs;
    using SVNMonitor.View.Interfaces;
    using SVNMonitor.Wizards;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

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
        private Dictionary<SVNMonitor.Entities.Source, long> selectedRevisions = new Dictionary<SVNMonitor.Entities.Source, long>();
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
            [DebuggerNonUserCode] add
            {
                this.selectionChanged = (EventHandler) Delegate.Combine(this.selectionChanged, value);
                this.Grid.SelectionChanged += value;
            }
            [DebuggerNonUserCode] remove
            {
                this.selectionChanged = (EventHandler) Delegate.Remove(this.selectionChanged, value);
                this.Grid.SelectionChanged -= value;
            }
        }

        public LogEntriesPanel()
        {
            this.InitializeComponent();
            if (!base.DesignMode)
            {
                UIHelper.ApplyResources(this.uiCommandManager1);
                UIHelper.ApplyResources(this.Grid, this);
                this.InitializeClipboardDelegates();
            }
        }

        public void BeginInit()
        {
        }

        public bool CanGetNextLogEntry(SVNLogEntry nextFrom)
        {
            try
            {
                int index = this.IndexOfLogEntry(nextFrom);
                return (this.LogEntries.Count > (index + 1));
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
                return (this.IndexOfLogEntry(previousFrom) > 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error checking if can get next log entry:", ex);
                return false;
            }
        }

        public void ClearSearch()
        {
            this.SetLogEntries(this.allEntries);
        }

        private void cmdDiff_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenDiff();
        }

        private void cmdRecommend_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.Recommend();
        }

        private void cmdRollback_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.Rollback();
        }

        private void cmdShowDetails_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.ShowDetails();
        }

        private void cmdSVNLog_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenSVNLog();
        }

        private void cmdSVNUpdate_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNUpdate();
        }

        private bool ConfirmUpdateOldRevision(SVNLogEntry entry, long revision)
        {
            SVNInfo info = entry.Source.GetInfo(true);
            return ((info == null) || ((info.Revision <= revision) || (RollbackPromptDialog.Prompt(base.ParentForm, revision) == DialogResult.Yes)));
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
            SVNLogEntry entry = this.SelectedItem;
            this.CanOpenSVNLog = SVNLogEntryHelper.CanOpenSVNLog(entry);
            this.CanDiff = SVNLogEntryHelper.CanDiff(entry);
            this.CanSVNUpdate = SVNLogEntryHelper.CanSVNUpdate(entry);
            this.CanRollback = SVNLogEntryHelper.CanRollback(entry);
            this.CanRunAuthorWizard = SVNLogEntryHelper.CanRunAuthorWizard(entry);
            this.CanCopyToClipboard = SVNLogEntryHelper.CanCopyToClipboard(entry);
            this.CanCopyMessageToClipboard = SVNLogEntryHelper.CanCopyMessageToClipboard(entry);
            this.CanCopyAuthorToClipboard = SVNLogEntryHelper.CanCopyAuthorToClipboard(entry);
            this.CanCopyPaths = SVNLogEntryHelper.CanCopyPaths(entry);
            this.CanRecommend = SVNLogEntryHelper.CanRecommend(entry);
            this.CanShowDetails = SVNLogEntryHelper.CanShowDetails(entry);
        }

        public void EndInit()
        {
            if (!base.DesignMode)
            {
                this.SyncUIWithSettings();
                UIHelper.GetBaseName getLogEntryAuthor = item => ((SVNLogEntry) item).Author;
                UIHelper.InitializeWizardsMenu<SVNLogEntry>(this, this.uiContextMenu1, this.menuAuthorWizards, getLogEntryAuthor, "LogEntries", "Author");
                this.EnableCommands();
            }
        }

        private void FormatLogEntryRow(GridEXRow row)
        {
            SVNLogEntry entry = (SVNLogEntry) row.DataRow;
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
                foreach (GridEXCell cell in (IEnumerable) row.Cells)
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
            if (this.allEntries == null)
            {
                return new SVNLogEntry[0];
            }
            SVNLogEntry[] entries = new SVNLogEntry[this.allEntries.Count];
            this.allEntries.CopyTo(entries);
            return entries;
        }

        public SVNLogEntry GetNextLogEntry(SVNLogEntry nextFrom)
        {
            int index = this.IndexOfLogEntry(nextFrom);
            return this.LogEntries[index + 1];
        }

        public SVNLogEntry GetPreviousLogEntry(SVNLogEntry previousFrom)
        {
            int index = this.IndexOfLogEntry(previousFrom);
            return this.LogEntries[index - 1];
        }

        private string GetSelectedAuthorToClipboard()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry == null)
            {
                return string.Empty;
            }
            return entry.Author;
        }

        private string GetSelectedDateToClipboard()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry == null)
            {
                return string.Empty;
            }
            return entry.CommitedOn.ToString();
        }

        private string GetSelectedLocalPathsToClipboard()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry == null)
            {
                return string.Empty;
            }
            return string.Join(Environment.NewLine, entry.Paths.Select<SVNPath, string>(p => p.DisplayName).ToArray<string>());
        }

        private string GetSelectedMessageToClipboard()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry == null)
            {
                return string.Empty;
            }
            return entry.Message;
        }

        private string GetSelectedPathsToClipboard()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry == null)
            {
                return string.Empty;
            }
            return string.Join(Environment.NewLine, entry.Paths.Select<SVNPath, string>(p => p.Name).ToArray<string>());
        }

        private string GetSelectedRevisionToClipboard()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry == null)
            {
                return string.Empty;
            }
            return entry.Revision.ToString();
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (!this.updatingDataSource)
            {
                SVNLogEntry entry = this.SelectedItem;
                if ((entry != null) && !this.suspendSelectedRevision)
                {
                    this.SelectedRevisions[this.Source] = entry.Revision;
                }
            }
        }

        private void HandleRowDoubleClick(GridEXRow row)
        {
            if ((row != null) && (row.DataRow is SVNLogEntry))
            {
                Logger.LogUserAction();
                this.PerformDefaultLogEntryAction();
            }
        }

        private void HandleSourceSelectionChanged()
        {
            lock (this)
            {
                if (this.Source != null)
                {
                    this.Source.StatusChanged -= new EventHandler<StatusChangedEventArgs>(this.source_StatusChanged);
                }
                this.Source = this.SourcesView.SelectedItem;
                this.currentRevision = 0L;
                if (this.Source != null)
                {
                    this.Source.StatusChanged += new EventHandler<StatusChangedEventArgs>(this.source_StatusChanged);
                }
                this.UpdateSelectedSource();
                this.EnableCommands();
            }
        }

        private int IndexOfLogEntry(SVNLogEntry entry)
        {
            return this.LogEntries.IndexOf(entry);
        }

        private void InitializeClipboardDelegates()
        {
            UIHelper.AddCopyCommand(this.cmdCopyAuthor, new UIHelper.GetStringDelegate(this.GetSelectedAuthorToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyDate, new UIHelper.GetStringDelegate(this.GetSelectedDateToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyRevision, new UIHelper.GetStringDelegate(this.GetSelectedRevisionToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyMessage, new UIHelper.GetStringDelegate(this.GetSelectedMessageToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyPaths, new UIHelper.GetStringDelegate(this.GetSelectedPathsToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyLocalPaths, new UIHelper.GetStringDelegate(this.GetSelectedLocalPathsToClipboard));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            GridEXLayout logEntriesGrid1_DesignTimeLayout = new GridEXLayout();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(LogEntriesPanel));
            this.logEntriesGrid1 = new LogEntriesGrid();
            this.uiCommandManager1 = new UICommandManager(this.components);
            this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
            this.cmdSVNUpdate2 = new UICommand("cmdSVNUpdate");
            this.cmdRollback1 = new UICommand("cmdRollback");
            this.cmdRecommend1 = new UICommand("cmdRecommend");
            this.Separator5 = new UICommand("Separator");
            this.cmdSVNLog1 = new UICommand("cmdSVNLog");
            this.cmdShowDetails2 = new UICommand("cmdShowDetails");
            this.cmdDiff1 = new UICommand("cmdDiff");
            this.menuClipboard1 = new UICommand("menuClipboard");
            this.Separator1 = new UICommand("Separator");
            this.menuAuthorWizards1 = new UICommand("menuAuthorWizards");
            this.BottomRebar1 = new UIRebar();
            this.uiCommandBar1 = new UICommandBar();
            this.cmdSVNUpdate1 = new UICommand("cmdSVNUpdate");
            this.cmdRollback2 = new UICommand("cmdRollback");
            this.cmdRecommend2 = new UICommand("cmdRecommend");
            this.Separator6 = new UICommand("Separator");
            this.cmdSVNLog2 = new UICommand("cmdSVNLog");
            this.cmdShowDetails1 = new UICommand("cmdShowDetails");
            this.cmdDiff2 = new UICommand("cmdDiff");
            this.Separator2 = new UICommand("Separator");
            this.menuAuthorWizards2 = new UICommand("menuAuthorWizards");
            this.cmdSVNLog = new UICommand("cmdSVNLog");
            this.menuAuthorWizards = new UICommand("menuAuthorWizards");
            this.cmdCopyMessage1 = new UICommand("cmdCopyMessage");
            this.cmdCopyAuthor1 = new UICommand("cmdCopyAuthor");
            this.cmdCopyRevision1 = new UICommand("cmdCopyRevision");
            this.cmdCopyDate1 = new UICommand("cmdCopyDate");
            this.cmdDiff = new UICommand("cmdDiff");
            this.cmdSVNUpdate = new UICommand("cmdSVNUpdate");
            this.cmdRollback = new UICommand("cmdRollback");
            this.cmdRecommend = new UICommand("cmdRecommend");
            this.menuClipboard = new UICommand("menuClipboard");
            this.cmdCopyMessage2 = new UICommand("cmdCopyMessage");
            this.cmdCopyAuthor2 = new UICommand("cmdCopyAuthor");
            this.cmdCopyRevision2 = new UICommand("cmdCopyRevision");
            this.cmdCopyDate2 = new UICommand("cmdCopyDate");
            this.cmdCopyPaths1 = new UICommand("cmdCopyPaths");
            this.cmdCopyLocalPaths1 = new UICommand("cmdCopyLocalPaths");
            this.cmdCopyRevision = new UICommand("cmdCopyRevision");
            this.cmdCopyAuthor = new UICommand("cmdCopyAuthor");
            this.cmdCopyDate = new UICommand("cmdCopyDate");
            this.cmdCopyMessage = new UICommand("cmdCopyMessage");
            this.cmdCopyPaths = new UICommand("cmdCopyPaths");
            this.cmdCopyLocalPaths = new UICommand("cmdCopyLocalPaths");
            this.cmdShowDetails = new UICommand("cmdShowDetails");
            this.LeftRebar1 = new UIRebar();
            this.RightRebar1 = new UIRebar();
            this.TopRebar1 = new UIRebar();
            this.panel1 = new Panel();
            this.panelNote = new Panel();
            this.lblNote = new Label();
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize) this.logEntriesGrid1).BeginInit();
            ((ISupportInitialize) this.uiCommandManager1).BeginInit();
            ((ISupportInitialize) this.uiContextMenu1).BeginInit();
            ((ISupportInitialize) this.BottomRebar1).BeginInit();
            ((ISupportInitialize) this.uiCommandBar1).BeginInit();
            ((ISupportInitialize) this.LeftRebar1).BeginInit();
            ((ISupportInitialize) this.RightRebar1).BeginInit();
            ((ISupportInitialize) this.TopRebar1).BeginInit();
            this.TopRebar1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelNote.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.logEntriesGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.logEntriesGrid1.AlternatingColors = true;
            this.logEntriesGrid1.AlternatingRowFormatStyle.BackColor = Color.WhiteSmoke;
            this.logEntriesGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.logEntriesGrid1.ColumnAutoResize = true;
            this.uiCommandManager1.SetContextMenu(this.logEntriesGrid1, this.uiContextMenu1);
            logEntriesGrid1_DesignTimeLayout.LayoutString = resources.GetString("logEntriesGrid1_DesignTimeLayout.LayoutString");
            this.logEntriesGrid1.DesignTimeLayout = logEntriesGrid1_DesignTimeLayout;
            this.logEntriesGrid1.Dock = DockStyle.Fill;
            this.logEntriesGrid1.EnterKeyBehavior = EnterKeyBehavior.None;
            this.logEntriesGrid1.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.logEntriesGrid1.GridLineColor = Color.Gainsboro;
            this.logEntriesGrid1.GridLines = GridLines.Horizontal;
            this.logEntriesGrid1.GroupByBoxVisible = false;
            this.logEntriesGrid1.HideSelection = HideSelection.HighlightInactive;
            this.logEntriesGrid1.Location = new Point(0, 0x17);
            this.logEntriesGrid1.Name = "logEntriesGrid1";
            this.logEntriesGrid1.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
            this.logEntriesGrid1.SelectedFormatStyle.BackColor = Color.SteelBlue;
            this.logEntriesGrid1.SelectedFormatStyle.ForeColor = Color.White;
            this.logEntriesGrid1.SelectedInactiveFormatStyle.BackColor = Color.FromArgb(0xec, 0xf5, 0xff);
            this.logEntriesGrid1.SettingsKey = "logEntriesGrid1";
            this.logEntriesGrid1.Size = new Size(0x243, 0x14b);
            this.logEntriesGrid1.TabIndex = 4;
            this.logEntriesGrid1.TabKeyBehavior = TabKeyBehavior.ControlNavigation;
            this.logEntriesGrid1.ThemedAreas = ThemedArea.CheckBoxes | ThemedArea.Cards | ThemedArea.ControlBorder | ThemedArea.GroupRows | ThemedArea.TreeGliphs | ThemedArea.GroupByBox | ThemedArea.Headers | ThemedArea.EditControls | ThemedArea.ScrollBars;
            this.logEntriesGrid1.TreeLineColor = Color.Gainsboro;
            this.logEntriesGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.logEntriesGrid1.KeyDown += new KeyEventHandler(this.logEntriesGrid1_KeyDown);
            this.logEntriesGrid1.RowDoubleClick += new RowActionEventHandler(this.logEntriesGrid1_RowDoubleClick);
            this.logEntriesGrid1.FormattingRow += new RowLoadEventHandler(this.logEntriesGrid1_FormattingRow);
            this.logEntriesGrid1.MouseDown += new MouseEventHandler(this.logEntriesGrid1_MouseDown);
            this.logEntriesGrid1.SelectionChanged += new EventHandler(this.logEntriesGrid1_SelectionChanged);
            this.uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.BottomRebar = this.BottomRebar1;
            this.uiCommandManager1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.uiCommandManager1.Commands.AddRange(new UICommand[] { this.cmdSVNLog, this.menuAuthorWizards, this.cmdDiff, this.cmdSVNUpdate, this.cmdRollback, this.cmdRecommend, this.menuClipboard, this.cmdCopyRevision, this.cmdCopyAuthor, this.cmdCopyDate, this.cmdCopyMessage, this.cmdCopyPaths, this.cmdCopyLocalPaths, this.cmdShowDetails });
            this.uiCommandManager1.ContainerControl = this;
            this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] { this.uiContextMenu1 });
            this.uiCommandManager1.Id = new Guid("456fbda7-0fae-48a1-84cd-e9cc0c1a5be1");
            this.uiCommandManager1.LeftRebar = this.LeftRebar1;
            this.uiCommandManager1.LockCommandBars = true;
            this.uiCommandManager1.RightRebar = this.RightRebar1;
            this.uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.ShowQuickCustomizeMenu = false;
            this.uiCommandManager1.TopRebar = this.TopRebar1;
            this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
            this.uiContextMenu1.CommandManager = this.uiCommandManager1;
            this.uiContextMenu1.Commands.AddRange(new UICommand[] { this.cmdSVNUpdate2, this.cmdRollback1, this.cmdRecommend1, this.Separator5, this.cmdSVNLog1, this.cmdShowDetails2, this.cmdDiff1, this.menuClipboard1, this.Separator1, this.menuAuthorWizards1 });
            this.uiContextMenu1.Key = "ContextMenu1";
            this.uiContextMenu1.Popup += new EventHandler(this.uiContextMenu1_Popup);
            this.cmdSVNUpdate2.Key = "cmdSVNUpdate";
            this.cmdSVNUpdate2.Name = "cmdSVNUpdate2";
            this.cmdSVNUpdate2.Text = "&Update to this revision";
            this.cmdRollback1.Key = "cmdRollback";
            this.cmdRollback1.Name = "cmdRollback1";
            this.cmdRollback1.Text = "&Rollback this revision";
            this.cmdRecommend1.Key = "cmdRecommend";
            this.cmdRecommend1.Name = "cmdRecommend1";
            this.cmdRecommend1.Text = "Rec&ommend";
            this.Separator5.CommandType = CommandType.Separator;
            this.Separator5.Key = "Separator";
            this.Separator5.Name = "Separator5";
            this.cmdSVNLog1.Key = "cmdSVNLog";
            this.cmdSVNLog1.Name = "cmdSVNLog1";
            this.cmdSVNLog1.Text = "Show &log";
            this.cmdShowDetails2.Key = "cmdShowDetails";
            this.cmdShowDetails2.Name = "cmdShowDetails2";
            this.cmdDiff1.Key = "cmdDiff";
            this.cmdDiff1.Name = "cmdDiff1";
            this.cmdDiff1.Text = "&Diff";
            this.menuClipboard1.Key = "menuClipboard";
            this.menuClipboard1.Name = "menuClipboard1";
            this.Separator1.CommandType = CommandType.Separator;
            this.Separator1.Key = "Separator";
            this.Separator1.Name = "Separator1";
            this.menuAuthorWizards1.Key = "menuAuthorWizards";
            this.menuAuthorWizards1.Name = "menuAuthorWizards1";
            this.menuAuthorWizards1.Text = "&Monitor this author";
            this.BottomRebar1.CommandManager = this.uiCommandManager1;
            this.BottomRebar1.Dock = DockStyle.Bottom;
            this.BottomRebar1.Location = new Point(0, 0x17e);
            this.BottomRebar1.Name = "BottomRebar1";
            this.BottomRebar1.Size = new Size(0x238, 0);
            this.uiCommandBar1.CommandManager = this.uiCommandManager1;
            this.uiCommandBar1.Commands.AddRange(new UICommand[] { this.cmdSVNUpdate1, this.cmdRollback2, this.cmdRecommend2, this.Separator6, this.cmdSVNLog2, this.cmdShowDetails1, this.cmdDiff2, this.Separator2, this.menuAuthorWizards2 });
            this.uiCommandBar1.FullRow = true;
            this.uiCommandBar1.Key = "CommandBar1";
            this.uiCommandBar1.Location = new Point(0, 0);
            this.uiCommandBar1.Name = "uiCommandBar1";
            this.uiCommandBar1.RowIndex = 0;
            this.uiCommandBar1.Size = new Size(0x243, 0x1c);
            this.uiCommandBar1.Text = "CommandBar1";
            this.cmdSVNUpdate1.Key = "cmdSVNUpdate";
            this.cmdSVNUpdate1.Name = "cmdSVNUpdate1";
            this.cmdSVNUpdate1.Text = "Update";
            this.cmdRollback2.Key = "cmdRollback";
            this.cmdRollback2.Name = "cmdRollback2";
            this.cmdRollback2.Text = "Rollback";
            this.cmdRecommend2.Key = "cmdRecommend";
            this.cmdRecommend2.Name = "cmdRecommend2";
            this.Separator6.CommandType = CommandType.Separator;
            this.Separator6.Key = "Separator";
            this.Separator6.Name = "Separator6";
            this.cmdSVNLog2.Key = "cmdSVNLog";
            this.cmdSVNLog2.Name = "cmdSVNLog2";
            this.cmdShowDetails1.Key = "cmdShowDetails";
            this.cmdShowDetails1.Name = "cmdShowDetails1";
            this.cmdDiff2.Key = "cmdDiff";
            this.cmdDiff2.Name = "cmdDiff2";
            this.Separator2.CommandType = CommandType.Separator;
            this.Separator2.Key = "Separator";
            this.Separator2.Name = "Separator2";
            this.menuAuthorWizards2.CommandStyle = CommandStyle.TextImage;
            this.menuAuthorWizards2.Key = "menuAuthorWizards";
            this.menuAuthorWizards2.Name = "menuAuthorWizards2";
            this.menuAuthorWizards2.Text = "Monitor";
            this.menuAuthorWizards2.Click += new CommandEventHandler(this.menuAuthorWizards2_Click);
            this.cmdSVNLog.Image = (Image) resources.GetObject("cmdSVNLog.Image");
            this.cmdSVNLog.Key = "cmdSVNLog";
            this.cmdSVNLog.Name = "cmdSVNLog";
            this.cmdSVNLog.Text = "Show log";
            this.cmdSVNLog.ToolTipText = "Show log";
            this.cmdSVNLog.Click += new CommandEventHandler(this.cmdSVNLog_Click);
            this.menuAuthorWizards.Commands.AddRange(new UICommand[] { this.cmdCopyMessage1, this.cmdCopyAuthor1, this.cmdCopyRevision1, this.cmdCopyDate1 });
            this.menuAuthorWizards.Image = (Image) resources.GetObject("menuAuthorWizards.Image");
            this.menuAuthorWizards.Key = "menuAuthorWizards";
            this.menuAuthorWizards.Name = "menuAuthorWizards";
            this.menuAuthorWizards.Text = "Monitor this author";
            this.menuAuthorWizards.ToolTipText = "Monitor this author";
            this.cmdCopyMessage1.Key = "cmdCopyMessage";
            this.cmdCopyMessage1.Name = "cmdCopyMessage1";
            this.cmdCopyAuthor1.Key = "cmdCopyAuthor";
            this.cmdCopyAuthor1.Name = "cmdCopyAuthor1";
            this.cmdCopyRevision1.Key = "cmdCopyRevision";
            this.cmdCopyRevision1.Name = "cmdCopyRevision1";
            this.cmdCopyDate1.Key = "cmdCopyDate";
            this.cmdCopyDate1.Name = "cmdCopyDate1";
            this.cmdDiff.Image = (Image) resources.GetObject("cmdDiff.Image");
            this.cmdDiff.Key = "cmdDiff";
            this.cmdDiff.Name = "cmdDiff";
            this.cmdDiff.Text = "Diff";
            this.cmdDiff.ToolTipText = "Diff with previous version";
            this.cmdDiff.Click += new CommandEventHandler(this.cmdDiff_Click);
            this.cmdSVNUpdate.Image = (Image) resources.GetObject("cmdSVNUpdate.Image");
            this.cmdSVNUpdate.Key = "cmdSVNUpdate";
            this.cmdSVNUpdate.Name = "cmdSVNUpdate";
            this.cmdSVNUpdate.Text = "Update to this revision";
            this.cmdSVNUpdate.ToolTipText = "Update to this revision";
            this.cmdSVNUpdate.Click += new CommandEventHandler(this.cmdSVNUpdate_Click);
            this.cmdRollback.Image = (Image) resources.GetObject("cmdRollback.Image");
            this.cmdRollback.Key = "cmdRollback";
            this.cmdRollback.Name = "cmdRollback";
            this.cmdRollback.Text = "Rollback this revision";
            this.cmdRollback.ToolTipText = "Rollback this revision";
            this.cmdRollback.Click += new CommandEventHandler(this.cmdRollback_Click);
            this.cmdRecommend.Image = (Image) resources.GetObject("cmdRecommend.Image");
            this.cmdRecommend.Key = "cmdRecommend";
            this.cmdRecommend.Name = "cmdRecommend";
            this.cmdRecommend.Text = "Recommend...";
            this.cmdRecommend.ToolTipText = "Recommend this revision to other users and commit the changes";
            this.cmdRecommend.Click += new CommandEventHandler(this.cmdRecommend_Click);
            this.menuClipboard.Commands.AddRange(new UICommand[] { this.cmdCopyMessage2, this.cmdCopyAuthor2, this.cmdCopyRevision2, this.cmdCopyDate2, this.cmdCopyPaths1, this.cmdCopyLocalPaths1 });
            this.menuClipboard.Image = (Image) resources.GetObject("menuClipboard.Image");
            this.menuClipboard.Key = "menuClipboard";
            this.menuClipboard.Name = "menuClipboard";
            this.menuClipboard.Text = "&Copy to clipboard";
            this.menuClipboard.Popup += new CommandEventHandler(this.menuClipboard_Popup);
            this.cmdCopyMessage2.Key = "cmdCopyMessage";
            this.cmdCopyMessage2.Name = "cmdCopyMessage2";
            this.cmdCopyAuthor2.Key = "cmdCopyAuthor";
            this.cmdCopyAuthor2.Name = "cmdCopyAuthor2";
            this.cmdCopyRevision2.Key = "cmdCopyRevision";
            this.cmdCopyRevision2.Name = "cmdCopyRevision2";
            this.cmdCopyDate2.Key = "cmdCopyDate";
            this.cmdCopyDate2.Name = "cmdCopyDate2";
            this.cmdCopyPaths1.Key = "cmdCopyPaths";
            this.cmdCopyPaths1.Name = "cmdCopyPaths1";
            this.cmdCopyLocalPaths1.Key = "cmdCopyLocalPaths";
            this.cmdCopyLocalPaths1.Name = "cmdCopyLocalPaths1";
            this.cmdCopyRevision.Key = "cmdCopyRevision";
            this.cmdCopyRevision.Name = "cmdCopyRevision";
            this.cmdCopyRevision.Text = "&Revision";
            this.cmdCopyAuthor.Key = "cmdCopyAuthor";
            this.cmdCopyAuthor.Name = "cmdCopyAuthor";
            this.cmdCopyAuthor.Text = "&Author";
            this.cmdCopyDate.Key = "cmdCopyDate";
            this.cmdCopyDate.Name = "cmdCopyDate";
            this.cmdCopyDate.Text = "&Date and Time";
            this.cmdCopyMessage.Key = "cmdCopyMessage";
            this.cmdCopyMessage.Name = "cmdCopyMessage";
            this.cmdCopyMessage.Text = "&Message";
            this.cmdCopyPaths.Key = "cmdCopyPaths";
            this.cmdCopyPaths.Name = "cmdCopyPaths";
            this.cmdCopyPaths.Text = "&List Of Items";
            this.cmdCopyLocalPaths.Key = "cmdCopyLocalPaths";
            this.cmdCopyLocalPaths.Name = "cmdCopyLocalPaths";
            this.cmdCopyLocalPaths.Text = "List Of Local Items";
            this.cmdShowDetails.Image = (Image) resources.GetObject("cmdShowDetails.Image");
            this.cmdShowDetails.Key = "cmdShowDetails";
            this.cmdShowDetails.Name = "cmdShowDetails";
            this.cmdShowDetails.Text = "Show details";
            this.cmdShowDetails.Click += new CommandEventHandler(this.cmdShowDetails_Click);
            this.LeftRebar1.CommandManager = this.uiCommandManager1;
            this.LeftRebar1.Dock = DockStyle.Left;
            this.LeftRebar1.Location = new Point(0, 0);
            this.LeftRebar1.Name = "LeftRebar1";
            this.LeftRebar1.Size = new Size(0, 0x17e);
            this.RightRebar1.CommandManager = this.uiCommandManager1;
            this.RightRebar1.Dock = DockStyle.Right;
            this.RightRebar1.Location = new Point(0x238, 0);
            this.RightRebar1.Name = "RightRebar1";
            this.RightRebar1.Size = new Size(0, 0x17e);
            this.TopRebar1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.TopRebar1.CommandManager = this.uiCommandManager1;
            this.TopRebar1.Controls.Add(this.uiCommandBar1);
            this.TopRebar1.Dock = DockStyle.Top;
            this.TopRebar1.Location = new Point(0, 0);
            this.TopRebar1.Name = "TopRebar1";
            this.TopRebar1.Size = new Size(0x243, 0x1c);
            this.panel1.BackColor = Color.DimGray;
            this.panel1.Controls.Add(this.logEntriesGrid1);
            this.panel1.Controls.Add(this.panelNote);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x1c);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(0, 1, 0, 0);
            this.panel1.Size = new Size(0x243, 0x162);
            this.panel1.TabIndex = 5;
            this.panelNote.BackColor = Color.Gray;
            this.panelNote.Controls.Add(this.lblNote);
            this.panelNote.Controls.Add(this.pictureBox1);
            this.panelNote.Dock = DockStyle.Top;
            this.panelNote.Location = new Point(0, 1);
            this.panelNote.Name = "panelNote";
            this.panelNote.Padding = new Padding(2, 3, 2, 2);
            this.panelNote.Size = new Size(0x243, 0x16);
            this.panelNote.TabIndex = 5;
            this.panelNote.Visible = false;
            this.lblNote.Dock = DockStyle.Fill;
            this.lblNote.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.lblNote.ForeColor = Color.White;
            this.lblNote.Location = new Point(20, 3);
            this.lblNote.Name = "lblNote";
            this.lblNote.Padding = new Padding(2, 1, 2, 2);
            this.lblNote.Size = new Size(0x22d, 0x11);
            this.lblNote.TabIndex = 0;
            this.lblNote.Text = "This source is readonly. All svn actions are disabled.";
            this.pictureBox1.Dock = DockStyle.Left;
            this.pictureBox1.Image = (Image) resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x12, 0x11);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.TopRebar1);
            base.Name = "LogEntriesPanel";
            base.Size = new Size(0x243, 0x17e);
            ((ISupportInitialize) this.logEntriesGrid1).EndInit();
            ((ISupportInitialize) this.uiCommandManager1).EndInit();
            ((ISupportInitialize) this.uiContextMenu1).EndInit();
            ((ISupportInitialize) this.BottomRebar1).EndInit();
            ((ISupportInitialize) this.uiCommandBar1).EndInit();
            ((ISupportInitialize) this.LeftRebar1).EndInit();
            ((ISupportInitialize) this.RightRebar1).EndInit();
            ((ISupportInitialize) this.TopRebar1).EndInit();
            this.TopRebar1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelNote.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        private void logEntriesGrid1_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.DataRow is SVNLogEntry)
            {
                this.FormatLogEntryRow(e.Row);
            }
        }

        private void logEntriesGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Logger.LogUserAction();
                this.PerformDefaultLogEntryAction();
            }
            if ((((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down)) || ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right))) || ((e.KeyCode == Keys.Next) || (e.KeyCode == Keys.Prior)))
            {
                this.SelectedWithKeyboard = true;
            }
        }

        private void logEntriesGrid1_MouseDown(object sender, MouseEventArgs e)
        {
            this.SelectedWithKeyboard = false;
        }

        private void logEntriesGrid1_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            Logger.LogUserAction();
            this.HandleRowDoubleClick(e.Row);
        }

        private void logEntriesGrid1_SelectionChanged(object sender, EventArgs e)
        {
            this.EnableCommands();
        }

        private void menuAuthorWizards2_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.RunCustomWizard();
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
                ApplicationSettingsManager.SavedSettings += (, ) => this.SyncUIWithSettings();
                this.Grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
            }
        }

        protected virtual void OnSelectionChanged()
        {
            if (this.selectionChanged != null)
            {
                this.selectionChanged(this, EventArgs.Empty);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (base.Visible && ((this.Source != null) && !this.Source.IsUpToDate))
            {
                this.Grid.FirstRow = 0;
            }
        }

        private void OpenDiff()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry != null)
            {
                entry.Diff();
            }
        }

        private void OpenSVNLog()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry != null)
            {
                entry.OpenSVNLog();
            }
        }

        private void PerformDefaultLogEntryAction()
        {
            this.ShowDetails();
        }

        private void Recommend()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry != null)
            {
                entry.Recommend();
            }
        }

        private void Rollback()
        {
            SVNLogEntry entry = this.SelectedItem;
            if ((entry != null) && this.ConfirmUpdateOldRevision(entry, entry.Revision - 1L))
            {
                entry.Rollback();
            }
        }

        private void RunCustomWizard()
        {
            SVNLogEntry entry = this.SelectedItem;
            new CustomWizard().Run(entry.Author, "LogEntries", "Author");
        }

        private void SelectLastRecord()
        {
            long maxRevision = SVNLog.GetMaxRevision(this.LogEntries);
            this.SelectRevision(maxRevision);
        }

        protected void SelectRevision(long revision)
        {
            GridEXFilterCondition condition = new GridEXFilterCondition {
                Column = this.Grid.RootTable.Columns["Revision"],
                ConditionOperator = ConditionOperator.Equal,
                Value1 = revision
            };
            int found = 0;
            try
            {
                found = this.Grid.FindAll(condition);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error in Grid.FindAll: ", ex);
            }
            if (found == 0)
            {
                Logger.Log.ErrorFormat("Tried to select revision {0}, but not found. (source={1})", revision, this.Source);
            }
        }

        private void SetDefaultPopupCommand()
        {
            this.cmdSVNLog.DefaultItem = Janus.Windows.UI.InheritableBoolean.True;
        }

        private void SetLogEntries()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.SetLogEntries));
            }
            else
            {
                this.SearchTextBox.Search();
                try
                {
                    this.suspendSelectedRevision = true;
                    this.Grid.Refetch();
                    this.suspendSelectedRevision = false;
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Error trying to refetch log-entries:", ex);
                }
            }
        }

        private void SetLogEntries(IEnumerable<SVNLogEntry> entries)
        {
            this.updatingDataSource = true;
            ((LogEntriesGrid) this.Grid).LogEntries = entries;
            this.ShowOrHideReadonlyNote();
            this.updatingDataSource = false;
            try
            {
                if (((this.Source != null) && (this.LogEntries != null)) && !this.SelectedRevisions.ContainsKey(this.Source))
                {
                    this.SelectLastRecord();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error selecting the last row.", ex);
            }
        }

        public void SetSearchResults(IEnumerable<SVNLogEntry> results)
        {
            this.SetLogEntries(results);
        }

        private void ShowDetails()
        {
            SVNLogEntry entry = this.SelectedItem;
            if (entry != null)
            {
                LogEntryDetailsDialog.ShowLogEntryDetails(this, entry);
            }
        }

        private void ShowOrHideReadonlyNote()
        {
            this.panelNote.Visible = (this.Source != null) && this.Source.IsURL;
        }

        private void source_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            if ((e.Reason != StatusChangedReason.Updating) && (e.Reason != StatusChangedReason.Deleted))
            {
                SVNMonitor.Entities.Source updatedSource = (SVNMonitor.Entities.Source) e.Entity;
                if ((this.Source != null) && (this.Source == updatedSource))
                {
                    this.UpdateSelectedSourceSafe();
                }
            }
        }

        private void sourcesView_SelectionChanged(object sender, EventArgs e)
        {
            this.HandleSourceSelectionChanged();
        }

        object ISelectableView<SVNLogEntry>.Invoke(Delegate delegate1)
        {
            return base.Invoke(delegate1);
        }

        private void SVNUpdate()
        {
            SVNLogEntry entry = this.SelectedItem;
            if ((entry != null) && this.ConfirmUpdateOldRevision(entry, entry.Revision))
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
                    this.GroupByBoxVisible = ApplicationSettingsManager.Settings.LogGroupByBox;
                    this.Grid.RootTable.PreviewRowLines = ApplicationSettingsManager.Settings.PreviewRowLines;
                    this.Grid.Refetch();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error synchronizing the Log-Entries panel with the settings: ", ex);
            }
        }

        private void uiContextMenu1_Popup(object sender, EventArgs e)
        {
            this.SetDefaultPopupCommand();
        }

        private void UpdateSelectedSource()
        {
            try
            {
                if ((this.Source != null) && this.Source.HasLog)
                {
                    if (this.currentRevision <= this.Source.Revision)
                    {
                        SVNLog log = this.Source.GetLog(false);
                        if (log != null)
                        {
                            this.LogEntries = log.LogEntries;
                            this.currentRevision = this.Source.Revision;
                            if (this.SelectedRevisions.ContainsKey(this.Source))
                            {
                                long previousRevision = this.SelectedRevisions[this.Source];
                                try
                                {
                                    this.SelectRevision(previousRevision);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Log.Debug(string.Format("Error selecting previous revision. Previous revision: {0}. Row count: {1}", previousRevision, this.Grid.RowCount), ex);
                                }
                            }
                        }
                        else
                        {
                            this.LogEntries = null;
                        }
                    }
                }
                else
                {
                    Logger.Log.DebugFormat("Source does not have a log: {0}", this.Source);
                    this.LogEntries = null;
                    this.currentRevision = 0L;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                ErrorHandler.Append(ex.Message, this.Source, ex);
                this.LogEntries = null;
            }
            finally
            {
                this.OnSelectionChanged();
            }
        }

        private void UpdateSelectedSourceSafe()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.UpdateSelectedSourceSafe));
            }
            else
            {
                this.UpdateSelectedSource();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanCopyAuthorToClipboard
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopyAuthor);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopyAuthor, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanCopyMessageToClipboard
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopyMessage);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopyMessage, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanCopyPaths
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopyPaths);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopyPaths, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanCopyToClipboard
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.menuClipboard);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.menuClipboard, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanDiff
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdDiff);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdDiff, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanOpenSVNLog
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdSVNLog);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdSVNLog, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanRecommend
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdRecommend);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdRecommend, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanRollback
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdRollback);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdRollback, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanRunAuthorWizard
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.menuAuthorWizards);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.menuAuthorWizards, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanShowDetails
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdShowDetails);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdShowDetails, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanSVNUpdate
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdSVNUpdate);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdSVNUpdate, value);
            }
        }

        protected override Janus.Windows.GridEX.GridEX Grid
        {
            [DebuggerNonUserCode]
            get
            {
                return this.logEntriesGrid1;
            }
        }

        [Category("Table View"), DefaultValue(true), Description("Determines whether the 'Group By Box' should be drawn.")]
        public bool GroupByBoxVisible
        {
            [DebuggerNonUserCode]
            get
            {
                return this.Grid.GroupByBoxVisible;
            }
            [DebuggerNonUserCode]
            set
            {
                this.Grid.GroupByBoxVisible = value;
            }
        }

        protected override string LayoutSettings
        {
            get
            {
                return ApplicationSettingsManager.Settings.UILogEntriesGridLayout;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public List<SVNLogEntry> LogEntries
        {
            [DebuggerNonUserCode]
            get
            {
                return this.allEntries;
            }
            set
            {
                this.allEntries = value;
                this.SetLogEntries();
            }
        }

        [Browsable(false)]
        public SearchTextBox<SVNLogEntry> SearchTextBox { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SVNLogEntry SelectedItem
        {
            [DebuggerNonUserCode]
            get
            {
                return ((LogEntriesGrid) this.Grid).SelectedLogEntry;
            }
        }

        private Dictionary<SVNMonitor.Entities.Source, long> SelectedRevisions
        {
            [DebuggerNonUserCode]
            get
            {
                return this.selectedRevisions;
            }
        }

        public bool SelectedWithKeyboard
        {
            [DebuggerNonUserCode]
            get
            {
                return this.selectedWithKeyboard;
            }
            [DebuggerNonUserCode]
            set
            {
                this.selectedWithKeyboard = value;
            }
        }

        private SVNMonitor.Entities.Source Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
            }
        }

        [Browsable(true)]
        public IUserEntityView<SVNMonitor.Entities.Source> SourcesView
        {
            [DebuggerNonUserCode]
            get
            {
                return this.sourcesView;
            }
            set
            {
                if (this.sourcesView != null)
                {
                    this.sourcesView.SelectionChanged -= new EventHandler(this.sourcesView_SelectionChanged);
                }
                this.sourcesView = value;
                if (this.sourcesView != null)
                {
                    this.sourcesView.SelectionChanged += new EventHandler(this.sourcesView_SelectionChanged);
                }
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

