namespace SVNMonitor.View.Panels
{
    using Janus.Windows.GridEX;
    using Janus.Windows.UI;
    using Janus.Windows.UI.CommandBars;
    using SVNMonitor;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.View;
    using SVNMonitor.View.Controls;
    using SVNMonitor.View.Interfaces;
    using SVNMonitor.Wizards;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Timers;
    using System.Windows.Forms;

    internal class PathsPanel : GridPanel, ISupportInitialize, ISelectableView<SVNPath>, ISearchablePanel<SVNPath>
    {
        private List<SVNPath> allPaths;
        private UIRebar BottomRebar1;
        private UICommand cmdBlame;
        private UICommand cmdBlame1;
        private UICommand cmdBlame2;
        private UICommand cmdBrowse;
        private UICommand cmdBrowse1;
        private UICommand cmdBrowse2;
        private UICommand cmdCommit;
        private UICommand cmdCommit1;
        private UICommand cmdCommit2;
        private UICommand cmdCopyFullName;
        private UICommand cmdCopyFullName1;
        private UICommand cmdCopyName;
        private UICommand cmdCopyName1;
        private UICommand cmdCopyRelativeURL;
        private UICommand cmdCopyRelativeUrl1;
        private UICommand cmdCopyURL;
        private UICommand cmdCopyURL1;
        private UICommand cmdDiff;
        private UICommand cmdDiff2;
        private UICommand cmdDiffLocalWithBase;
        private UICommand cmdDiffLocalWithBase1;
        private UICommand cmdDiffLocalWithBase2;
        private UICommand cmdDiffWithPrevious;
        private UICommand cmdDiffWithPrevious1;
        private UICommand cmdDiffWithPrevious2;
        private UICommand cmdEdit;
        private UICommand cmdEdit1;
        private UICommand cmdEdit2;
        private UICommand cmdExplore;
        private UICommand cmdExplore1;
        private UICommand cmdExplore2;
        private UICommand cmdExport;
        private UICommand cmdExport1;
        private UICommand cmdOpen;
        private UICommand cmdOpen1;
        private UICommand cmdOpen2;
        private UICommand cmdOpenWith;
        private UICommand cmdOpenWith2;
        private UICommand cmdOpenWith3;
        private UICommand cmdRevert;
        private UICommand cmdRevert1;
        private UICommand cmdRevert2;
        private UICommand cmdRollback;
        private UICommand cmdRollback1;
        private UICommand cmdRollback2;
        private UICommand cmdSVNLog;
        private UICommand cmdSVNLog1;
        private UICommand cmdSVNLog2;
        private UICommand cmdSVNUpdate;
        private UICommand cmdSVNUpdate1;
        private UICommand cmdSVNUpdate2;
        private IContainer components;
        private SVNLogEntry lastLogEntry;
        private Source lastSource;
        private UIRebar LeftRebar1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ILogEntriesView logEntriesView;
        private System.Timers.Timer logEntrySelectionTimer;
        private UICommand menuClipboard;
        private UICommand menuClipboard1;
        private UICommand menuFileWizard;
        private UICommand menuFileWizard1;
        private UICommand menuFileWizard2;
        private Panel panel1;
        private System.Timers.Timer pathSelectionTimer;
        private PathsGrid pathsGrid1;
        private UIRebar RightRebar1;
        private bool selectedWithKeyboard;
        private UICommand Separator1;
        private UICommand Separator2;
        private UICommand Separator3;
        private UICommand Separator4;
        private UICommand Separator5;
        private UICommand Separator6;
        private bool suppressMoveFirst;
        private bool suppressSelectionChanged;
        private UIRebar TopRebar1;
        private UICommandBar uiCommandBar1;
        private UICommandManager uiCommandManager1;
        private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
        private Dictionary<UserAction, UICommand> userCommandsMap;

        public PathsPanel()
        {
            this.InitializeComponent();
            if (!base.DesignMode)
            {
                UIHelper.ApplyResources(this.uiCommandManager1);
                UIHelper.ApplyResources(this.Grid, this);
                this.InitSelectionTimer();
                this.MapUserCommands();
                this.InitializeClipboardDelegates();
            }
        }

        public void BeginInit()
        {
        }

        private void Browse()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.Browse();
            }
        }

        private bool CanDoDefaultAction()
        {
            string actionString = ApplicationSettingsManager.Settings.DefaultPathAction;
            UserAction action = EnumHelper.ParseEnum<UserAction>(actionString);
            foreach (PropertyInfo prop in typeof(PathsPanel).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where<PropertyInfo>(p => Attribute.IsDefined(p, typeof(AssociatedUserActionAttribute))))
            {
                AssociatedUserActionAttribute attribute = (AssociatedUserActionAttribute) Attribute.GetCustomAttribute(prop, typeof(AssociatedUserActionAttribute));
                if ((attribute != null) && (action == attribute.UserAction))
                {
                    return (bool) prop.GetValue(this, null);
                }
            }
            Logger.Log.ErrorFormat("Can't find property for associated user action: {0}", actionString);
            return false;
        }

        public void ClearSearch()
        {
            this.SetPaths(this.allPaths);
        }

        private void cmdBlame_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenBlame();
        }

        private void cmdBrowse_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.Browse();
        }

        private void cmdCommit_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNCommit();
        }

        private void cmdDiff_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenDiffAuto();
        }

        private void cmdDiffLocalWithBase_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenDiffLocalWithBase();
        }

        private void cmdDiffWithPrevious_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenDiffWithPrevious();
        }

        private void cmdEdit_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.FileEdit();
        }

        private void cmdExplore_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.Explore();
        }

        private void cmdExport_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SaveRevision();
        }

        private void cmdOpen_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.Open();
        }

        private void cmdOpenWith_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenWith();
        }

        private void cmdRevert_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNRevert();
        }

        private void cmdRollback_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.Rollback();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DoDefaultPathAction()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                if (!this.CanDoDefaultAction())
                {
                    string action = ApplicationSettingsManager.Settings.DefaultPathAction;
                    string message = Strings.ErrorCantPerformDefaultPathAction_FORMAT.FormatWith(new object[] { action });
                    MainForm.FormInstance.ShowMessage(message, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SVNMonitor.EventLog.LogWarning(message);
                }
                else
                {
                    cmd.DoDefaultPathAction();
                }
            }
        }

        private void EnableCommands()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.EnableCommands));
            }
            else
            {
                SVNPath path = this.SelectedItem;
                if (path != null)
                {
                    this.EnableCommandsForSingleSelectedPath(path);
                }
                else
                {
                    IEnumerable<SVNPath> paths = this.SelectedItems;
                    this.EnableCommandsForMultipleSelectedPaths(paths);
                }
            }
        }

        private void EnableCommandsForMultipleSelectedPaths(IEnumerable<SVNPath> paths)
        {
            this.CanBlame = false;
            this.CanDiff = false;
            this.CanDiffLocalWithBase = false;
            this.CanDiffWithPrevious = false;
            this.CanExplore = false;
            this.CanBrowse = false;
            this.CanOpen = false;
            this.CanOpenSVNLog = false;
            this.CanOpenWith = false;
            this.CanRollback = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanRollback));
            this.CanRunPathWizard = false;
            this.CanSVNUpdate = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanSVNUpdate));
            this.CanCommit = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanCommit));
            this.CanRevert = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanRevert));
            this.CanEdit = false;
            this.CanCopyToClipboard = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanCopyToClipboard));
            this.CanCopyFullName = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanCopyFullName));
            this.CanCopyName = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanCopyName));
            this.CanCopyURL = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanCopyURL));
            this.CanCopyRelativeURL = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanCopyRelativeURL));
            this.CanExport = SVNPathHelper.CanMultiple(paths, new SVNPathHelper.SVNPathAction(SVNPathHelper.CanExport));
        }

        private void EnableCommandsForSingleSelectedPath(SVNPath path)
        {
            this.CanBlame = SVNPathHelper.CanBlame(path);
            this.CanDiff = SVNPathHelper.CanDiff(path);
            this.CanDiffLocalWithBase = SVNPathHelper.CanDiff(path);
            this.CanDiffWithPrevious = SVNPathHelper.CanDiff(path);
            this.CanExplore = SVNPathHelper.CanExplore(path);
            this.CanBrowse = SVNPathHelper.CanBrowse(path);
            this.CanOpen = SVNPathHelper.CanOpen(path);
            this.CanOpenSVNLog = SVNPathHelper.CanOpenSVNLog(path);
            this.CanOpenWith = SVNPathHelper.CanOpenWith(path);
            this.CanRollback = SVNPathHelper.CanRollback(path);
            this.CanRunPathWizard = SVNPathHelper.CanRunPathWizard(path);
            this.CanSVNUpdate = SVNPathHelper.CanSVNUpdate(path);
            this.CanCommit = SVNPathHelper.CanCommit(path);
            this.CanRevert = SVNPathHelper.CanRevert(path);
            this.CanEdit = SVNPathHelper.CanEdit(path);
            this.CanCopyToClipboard = SVNPathHelper.CanCopyToClipboard(path);
            this.CanCopyFullName = SVNPathHelper.CanCopyFullName(path);
            this.CanCopyName = SVNPathHelper.CanCopyName(path);
            this.CanCopyURL = SVNPathHelper.CanCopyURL(path);
            this.CanCopyRelativeURL = SVNPathHelper.CanCopyRelativeURL(path);
            this.CanExport = SVNPathHelper.CanExport(path);
        }

        public void EndInit()
        {
            if (!base.DesignMode)
            {
                UIHelper.GetBaseName getFileName = item => ((SVNPath) item).FilePath;
                UIHelper.InitializeWizardsMenu<SVNPath>(this, this.uiContextMenu1, this.menuFileWizard, getFileName, "Paths", "Path");
                this.EnableCommands();
            }
        }

        private void Explore()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.Explore();
            }
        }

        private void FileEdit()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.FileEdit();
            }
        }

        public IEnumerable<SVNPath> GetAllItems()
        {
            return this.Paths;
        }

        private string GetFullNameToClipboard()
        {
            IEnumerable<SVNPath> paths = this.SelectedItems;
            if ((paths == null) || (paths.Count<SVNPath>() == 0))
            {
                return string.Empty;
            }
            return string.Join(Environment.NewLine, paths.Select<SVNPath, string>(p => p.FilePath).ToArray<string>());
        }

        private string GetNameToClipboard()
        {
            IEnumerable<SVNPath> paths = this.SelectedItems;
            if ((paths == null) || (paths.Count<SVNPath>() == 0))
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (SVNPath path in paths)
            {
                string[] parts = path.Name.Split(new char[] { '/', '\\' });
                if ((parts == null) || (parts.Length == 0))
                {
                    return string.Empty;
                }
                sb.AppendLine(parts[parts.Length - 1]);
            }
            return sb.ToString();
        }

        private string GetRelativeURLToClipboard()
        {
            IEnumerable<SVNPath> paths = this.SelectedItems;
            if ((paths == null) || (paths.Count<SVNPath>() == 0))
            {
                return string.Empty;
            }
            return string.Join(Environment.NewLine, paths.Select<SVNPath, string>(p => p.Name).ToArray<string>());
        }

        private string GetURLToClipboard()
        {
            IEnumerable<SVNPath> paths = this.SelectedItems;
            if ((paths == null) || (paths.Count<SVNPath>() == 0))
            {
                return string.Empty;
            }
            return string.Join(Environment.NewLine, paths.Select<SVNPath, string>(p => p.Uri).ToArray<string>());
        }

        private void InitializeClipboardDelegates()
        {
            UIHelper.AddCopyCommand(this.cmdCopyFullName, new UIHelper.GetStringDelegate(this.GetFullNameToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyName, new UIHelper.GetStringDelegate(this.GetNameToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyURL, new UIHelper.GetStringDelegate(this.GetURLToClipboard));
            UIHelper.AddCopyCommand(this.cmdCopyRelativeURL, new UIHelper.GetStringDelegate(this.GetRelativeURLToClipboard));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PathsPanel));
            GridEXLayout pathsGrid1_DesignTimeLayout = new GridEXLayout();
            this.cmdExplore = new UICommand("cmdExplore");
            this.pathsGrid1 = new PathsGrid();
            this.uiCommandManager1 = new UICommandManager(this.components);
            this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
            this.cmdSVNUpdate2 = new UICommand("cmdSVNUpdate");
            this.cmdCommit2 = new UICommand("cmdCommit");
            this.cmdRevert2 = new UICommand("cmdRevert");
            this.cmdRollback1 = new UICommand("cmdRollback");
            this.Separator3 = new UICommand("Separator");
            this.cmdSVNLog1 = new UICommand("cmdSVNLog");
            this.cmdDiffWithPrevious2 = new UICommand("cmdDiffWithPrevious");
            this.cmdDiffLocalWithBase2 = new UICommand("cmdDiffLocalWithBase");
            this.cmdBlame1 = new UICommand("cmdBlame");
            this.Separator1 = new UICommand("Separator");
            this.cmdExplore1 = new UICommand("cmdExplore");
            this.cmdBrowse2 = new UICommand("cmdBrowse");
            this.cmdOpen1 = new UICommand("cmdOpen");
            this.cmdOpenWith2 = new UICommand("cmdOpenWith");
            this.cmdEdit2 = new UICommand("cmdEdit");
            this.cmdExport1 = new UICommand("cmdExport");
            this.menuClipboard1 = new UICommand("menuClipboard");
            this.Separator5 = new UICommand("Separator");
            this.menuFileWizard1 = new UICommand("menuFileWizard");
            this.BottomRebar1 = new UIRebar();
            this.uiCommandBar1 = new UICommandBar();
            this.cmdSVNUpdate1 = new UICommand("cmdSVNUpdate");
            this.cmdCommit1 = new UICommand("cmdCommit");
            this.cmdRevert1 = new UICommand("cmdRevert");
            this.cmdRollback2 = new UICommand("cmdRollback");
            this.Separator4 = new UICommand("Separator");
            this.cmdSVNLog2 = new UICommand("cmdSVNLog");
            this.cmdDiff2 = new UICommand("cmdDiff");
            this.cmdBlame2 = new UICommand("cmdBlame");
            this.Separator2 = new UICommand("Separator");
            this.cmdExplore2 = new UICommand("cmdExplore");
            this.cmdBrowse1 = new UICommand("cmdBrowse");
            this.cmdOpen2 = new UICommand("cmdOpen");
            this.cmdOpenWith3 = new UICommand("cmdOpenWith");
            this.cmdEdit1 = new UICommand("cmdEdit");
            this.Separator6 = new UICommand("Separator");
            this.menuFileWizard2 = new UICommand("menuFileWizard");
            this.cmdOpen = new UICommand("cmdOpen");
            this.cmdOpenWith = new UICommand("cmdOpenWith");
            this.cmdSVNLog = new UICommand("cmdSVNLog");
            this.cmdDiff = new UICommand("cmdDiff");
            this.cmdDiffWithPrevious1 = new UICommand("cmdDiffWithPrevious");
            this.cmdDiffLocalWithBase1 = new UICommand("cmdDiffLocalWithBase");
            this.cmdDiffWithPrevious = new UICommand("cmdDiffWithPrevious");
            this.cmdDiffLocalWithBase = new UICommand("cmdDiffLocalWithBase");
            this.cmdBlame = new UICommand("cmdBlame");
            this.cmdSVNUpdate = new UICommand("cmdSVNUpdate");
            this.cmdRollback = new UICommand("cmdRollback");
            this.menuFileWizard = new UICommand("menuFileWizard");
            this.cmdCommit = new UICommand("cmdCommit");
            this.cmdRevert = new UICommand("cmdRevert");
            this.cmdBrowse = new UICommand("cmdBrowse");
            this.cmdEdit = new UICommand("cmdEdit");
            this.menuClipboard = new UICommand("menuClipboard");
            this.cmdCopyFullName1 = new UICommand("cmdCopyFullName");
            this.cmdCopyName1 = new UICommand("cmdCopyName");
            this.cmdCopyRelativeUrl1 = new UICommand("cmdCopyRelativeURL");
            this.cmdCopyURL1 = new UICommand("cmdCopyURL");
            this.cmdCopyFullName = new UICommand("cmdCopyFullName");
            this.cmdCopyName = new UICommand("cmdCopyName");
            this.cmdCopyURL = new UICommand("cmdCopyURL");
            this.cmdCopyRelativeURL = new UICommand("cmdCopyRelativeURL");
            this.cmdExport = new UICommand("cmdExport");
            this.LeftRebar1 = new UIRebar();
            this.RightRebar1 = new UIRebar();
            this.TopRebar1 = new UIRebar();
            this.panel1 = new Panel();
            ((ISupportInitialize) this.pathsGrid1).BeginInit();
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
            this.cmdExplore.Image = (Image) resources.GetObject("cmdExplore.Image");
            this.cmdExplore.Key = "cmdExplore";
            this.cmdExplore.Name = "cmdExplore";
            this.cmdExplore.Text = "Explore";
            this.cmdExplore.ToolTipText = "Explore";
            this.cmdExplore.Click += new CommandEventHandler(this.cmdExplore_Click);
            this.pathsGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.pathsGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.pathsGrid1.ColumnAutoResize = true;
            this.uiCommandManager1.SetContextMenu(this.pathsGrid1, this.uiContextMenu1);
            pathsGrid1_DesignTimeLayout.LayoutString = resources.GetString("pathsGrid1_DesignTimeLayout.LayoutString");
            this.pathsGrid1.DesignTimeLayout = pathsGrid1_DesignTimeLayout;
            this.pathsGrid1.Dock = DockStyle.Fill;
            this.pathsGrid1.EnterKeyBehavior = EnterKeyBehavior.None;
            this.pathsGrid1.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.pathsGrid1.GridLineColor = SystemColors.Control;
            this.pathsGrid1.GridLines = GridLines.Horizontal;
            this.pathsGrid1.GroupByBoxVisible = false;
            this.pathsGrid1.HideSelection = HideSelection.HighlightInactive;
            this.pathsGrid1.Location = new Point(0, 1);
            this.pathsGrid1.Name = "pathsGrid1";
            this.pathsGrid1.SelectedFormatStyle.BackColor = Color.SteelBlue;
            this.pathsGrid1.SelectedInactiveFormatStyle.BackColor = Color.FromArgb(0xec, 0xf5, 0xff);
            this.pathsGrid1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.pathsGrid1.SettingsKey = "pathsGrid1";
            this.pathsGrid1.Size = new Size(0x39b, 0x79);
            this.pathsGrid1.TabIndex = 4;
            this.pathsGrid1.TabKeyBehavior = TabKeyBehavior.ControlNavigation;
            this.pathsGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.pathsGrid1.KeyDown += new KeyEventHandler(this.pathsGrid1_KeyDown);
            this.pathsGrid1.RowDoubleClick += new RowActionEventHandler(this.pathsGrid1_RowDoubleClick);
            this.pathsGrid1.MouseDown += new MouseEventHandler(this.pathsGrid1_MouseDown);
            this.pathsGrid1.SelectionChanged += new EventHandler(this.pathsGrid1_SelectionChanged);
            this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.BottomRebar = this.BottomRebar1;
            this.uiCommandManager1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.uiCommandManager1.Commands.AddRange(new UICommand[] { 
                this.cmdExplore, this.cmdOpen, this.cmdOpenWith, this.cmdSVNLog, this.cmdDiff, this.cmdDiffWithPrevious, this.cmdDiffLocalWithBase, this.cmdBlame, this.cmdSVNUpdate, this.cmdRollback, this.menuFileWizard, this.cmdCommit, this.cmdRevert, this.cmdBrowse, this.cmdEdit, this.menuClipboard, 
                this.cmdCopyFullName, this.cmdCopyName, this.cmdCopyURL, this.cmdCopyRelativeURL, this.cmdExport
             });
            this.uiCommandManager1.ContainerControl = this;
            this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] { this.uiContextMenu1 });
            this.uiCommandManager1.Id = new Guid("3057650b-f916-4520-b6e8-0eb30e775936");
            this.uiCommandManager1.LeftRebar = this.LeftRebar1;
            this.uiCommandManager1.LockCommandBars = true;
            this.uiCommandManager1.RightRebar = this.RightRebar1;
            this.uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.ShowQuickCustomizeMenu = false;
            this.uiCommandManager1.TopRebar = this.TopRebar1;
            this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
            this.uiContextMenu1.CommandManager = this.uiCommandManager1;
            this.uiContextMenu1.Commands.AddRange(new UICommand[] { 
                this.cmdSVNUpdate2, this.cmdCommit2, this.cmdRevert2, this.cmdRollback1, this.Separator3, this.cmdSVNLog1, this.cmdDiffWithPrevious2, this.cmdDiffLocalWithBase2, this.cmdBlame1, this.Separator1, this.cmdExplore1, this.cmdBrowse2, this.cmdOpen1, this.cmdOpenWith2, this.cmdEdit2, this.cmdExport1, 
                this.menuClipboard1, this.Separator5, this.menuFileWizard1
             });
            this.uiContextMenu1.Key = "ContextMenu1";
            this.uiContextMenu1.Popup += new EventHandler(this.uiContextMenu1_Popup);
            this.cmdSVNUpdate2.Key = "cmdSVNUpdate";
            this.cmdSVNUpdate2.Name = "cmdSVNUpdate2";
            this.cmdSVNUpdate2.Text = "&Update to this revision";
            this.cmdCommit2.Key = "cmdCommit";
            this.cmdCommit2.Name = "cmdCommit2";
            this.cmdCommit2.Text = "&Commit";
            this.cmdRevert2.Key = "cmdRevert";
            this.cmdRevert2.Name = "cmdRevert2";
            this.cmdRevert2.Text = "Re&vert";
            this.cmdRollback1.Key = "cmdRollback";
            this.cmdRollback1.Name = "cmdRollback1";
            this.cmdRollback1.Text = "&Rollback this revision";
            this.Separator3.CommandType = CommandType.Separator;
            this.Separator3.Key = "Separator";
            this.Separator3.Name = "Separator3";
            this.cmdSVNLog1.Key = "cmdSVNLog";
            this.cmdSVNLog1.Name = "cmdSVNLog1";
            this.cmdSVNLog1.Text = "Show &log";
            this.cmdDiffWithPrevious2.Key = "cmdDiffWithPrevious";
            this.cmdDiffWithPrevious2.Name = "cmdDiffWithPrevious2";
            this.cmdDiffLocalWithBase2.Key = "cmdDiffLocalWithBase";
            this.cmdDiffLocalWithBase2.Name = "cmdDiffLocalWithBase2";
            this.cmdBlame1.Key = "cmdBlame";
            this.cmdBlame1.Name = "cmdBlame1";
            this.cmdBlame1.Text = "&Blame";
            this.Separator1.CommandType = CommandType.Separator;
            this.Separator1.Key = "Separator";
            this.Separator1.Name = "Separator1";
            this.cmdExplore1.Key = "cmdExplore";
            this.cmdExplore1.Name = "cmdExplore1";
            this.cmdExplore1.Text = "&Explore";
            this.cmdBrowse2.Key = "cmdBrowse";
            this.cmdBrowse2.Name = "cmdBrowse2";
            this.cmdBrowse2.Text = "Bro&wse";
            this.cmdOpen1.Key = "cmdOpen";
            this.cmdOpen1.Name = "cmdOpen1";
            this.cmdOpen1.Text = "&Open";
            this.cmdOpenWith2.Key = "cmdOpenWith";
            this.cmdOpenWith2.Name = "cmdOpenWith2";
            this.cmdOpenWith2.Text = "Open &with...";
            this.cmdEdit2.Key = "cmdEdit";
            this.cmdEdit2.Name = "cmdEdit2";
            this.cmdEdit2.Text = "Edi&t";
            this.cmdExport1.Key = "cmdExport";
            this.cmdExport1.Name = "cmdExport1";
            this.menuClipboard1.Key = "menuClipboard";
            this.menuClipboard1.Name = "menuClipboard1";
            this.menuClipboard1.Text = "Cop&y to clipboard";
            this.Separator5.CommandType = CommandType.Separator;
            this.Separator5.Key = "Separator";
            this.Separator5.Name = "Separator5";
            this.menuFileWizard1.Key = "menuFileWizard";
            this.menuFileWizard1.Name = "menuFileWizard1";
            this.menuFileWizard1.Text = "&Monitor this file";
            this.BottomRebar1.CommandManager = this.uiCommandManager1;
            this.BottomRebar1.Dock = DockStyle.Bottom;
            this.BottomRebar1.Location = new Point(0, 0x152);
            this.BottomRebar1.Name = "BottomRebar1";
            this.BottomRebar1.Size = new Size(0x1f9, 0);
            this.uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.CommandManager = this.uiCommandManager1;
            this.uiCommandBar1.Commands.AddRange(new UICommand[] { this.cmdSVNUpdate1, this.cmdCommit1, this.cmdRevert1, this.cmdRollback2, this.Separator4, this.cmdSVNLog2, this.cmdDiff2, this.cmdBlame2, this.Separator2, this.cmdExplore2, this.cmdBrowse1, this.cmdOpen2, this.cmdOpenWith3, this.cmdEdit1, this.Separator6, this.menuFileWizard2 });
            this.uiCommandBar1.FullRow = true;
            this.uiCommandBar1.Key = "CommandBar1";
            this.uiCommandBar1.Location = new Point(0, 0);
            this.uiCommandBar1.Name = "uiCommandBar1";
            this.uiCommandBar1.RowIndex = 0;
            this.uiCommandBar1.Size = new Size(0x39b, 0x1c);
            this.uiCommandBar1.Text = "CommandBar1";
            this.cmdSVNUpdate1.Key = "cmdSVNUpdate";
            this.cmdSVNUpdate1.Name = "cmdSVNUpdate1";
            this.cmdSVNUpdate1.Text = "Update";
            this.cmdCommit1.Key = "cmdCommit";
            this.cmdCommit1.Name = "cmdCommit1";
            this.cmdRevert1.Key = "cmdRevert";
            this.cmdRevert1.Name = "cmdRevert1";
            this.cmdRollback2.Key = "cmdRollback";
            this.cmdRollback2.Name = "cmdRollback2";
            this.cmdRollback2.Text = "Rollback";
            this.cmdRollback2.ToolTipText = "Rollback this revision";
            this.Separator4.CommandType = CommandType.Separator;
            this.Separator4.Key = "Separator";
            this.Separator4.Name = "Separator4";
            this.cmdSVNLog2.Key = "cmdSVNLog";
            this.cmdSVNLog2.Name = "cmdSVNLog2";
            this.cmdDiff2.Key = "cmdDiff";
            this.cmdDiff2.Name = "cmdDiff2";
            this.cmdBlame2.Key = "cmdBlame";
            this.cmdBlame2.Name = "cmdBlame2";
            this.Separator2.CommandType = CommandType.Separator;
            this.Separator2.Key = "Separator";
            this.Separator2.Name = "Separator2";
            this.cmdExplore2.Key = "cmdExplore";
            this.cmdExplore2.Name = "cmdExplore2";
            this.cmdBrowse1.Key = "cmdBrowse";
            this.cmdBrowse1.Name = "cmdBrowse1";
            this.cmdOpen2.Key = "cmdOpen";
            this.cmdOpen2.Name = "cmdOpen2";
            this.cmdOpenWith3.Key = "cmdOpenWith";
            this.cmdOpenWith3.Name = "cmdOpenWith3";
            this.cmdEdit1.Key = "cmdEdit";
            this.cmdEdit1.Name = "cmdEdit1";
            this.Separator6.CommandType = CommandType.Separator;
            this.Separator6.Key = "Separator";
            this.Separator6.Name = "Separator6";
            this.menuFileWizard2.Key = "menuFileWizard";
            this.menuFileWizard2.Name = "menuFileWizard2";
            this.menuFileWizard2.Text = "Monitor";
            this.menuFileWizard2.Click += new CommandEventHandler(this.menuFileWizard2_Click);
            this.cmdOpen.Image = (Image) resources.GetObject("cmdOpen.Image");
            this.cmdOpen.Key = "cmdOpen";
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Text = "Open";
            this.cmdOpen.ToolTipText = "Open";
            this.cmdOpen.Click += new CommandEventHandler(this.cmdOpen_Click);
            this.cmdOpenWith.Image = (Image) resources.GetObject("cmdOpenWith.Image");
            this.cmdOpenWith.Key = "cmdOpenWith";
            this.cmdOpenWith.Name = "cmdOpenWith";
            this.cmdOpenWith.Text = "Open with...";
            this.cmdOpenWith.ToolTipText = "Open with...";
            this.cmdOpenWith.Click += new CommandEventHandler(this.cmdOpenWith_Click);
            this.cmdSVNLog.Image = (Image) resources.GetObject("cmdSVNLog.Image");
            this.cmdSVNLog.Key = "cmdSVNLog";
            this.cmdSVNLog.Name = "cmdSVNLog";
            this.cmdSVNLog.Text = "Show log";
            this.cmdSVNLog.ToolTipText = "Show log";
            this.cmdSVNLog.Click += new CommandEventHandler(this.cmdSVNLog_Click);
            this.cmdDiff.Commands.AddRange(new UICommand[] { this.cmdDiffWithPrevious1, this.cmdDiffLocalWithBase1 });
            this.cmdDiff.Image = (Image) resources.GetObject("cmdDiff.Image");
            this.cmdDiff.Key = "cmdDiff";
            this.cmdDiff.Name = "cmdDiff";
            this.cmdDiff.Text = "Diff";
            this.cmdDiff.ToolTipText = "Diff with previous version";
            this.cmdDiff.Click += new CommandEventHandler(this.cmdDiff_Click);
            this.cmdDiffWithPrevious1.Key = "cmdDiffWithPrevious";
            this.cmdDiffWithPrevious1.Name = "cmdDiffWithPrevious1";
            this.cmdDiffLocalWithBase1.Key = "cmdDiffLocalWithBase";
            this.cmdDiffLocalWithBase1.Name = "cmdDiffLocalWithBase1";
            this.cmdDiffWithPrevious.Image = (Image) resources.GetObject("cmdDiffWithPrevious.Image");
            this.cmdDiffWithPrevious.Key = "cmdDiffWithPrevious";
            this.cmdDiffWithPrevious.Name = "cmdDiffWithPrevious";
            this.cmdDiffWithPrevious.Text = "Diff With &Previous";
            this.cmdDiffWithPrevious.Click += new CommandEventHandler(this.cmdDiffWithPrevious_Click);
            this.cmdDiffLocalWithBase.Image = (Image) resources.GetObject("cmdDiffLocalWithBase.Image");
            this.cmdDiffLocalWithBase.Key = "cmdDiffLocalWithBase";
            this.cmdDiffLocalWithBase.Name = "cmdDiffLocalWithBase";
            this.cmdDiffLocalWithBase.Text = "&Diff With Base";
            this.cmdDiffLocalWithBase.Click += new CommandEventHandler(this.cmdDiffLocalWithBase_Click);
            this.cmdBlame.Image = (Image) resources.GetObject("cmdBlame.Image");
            this.cmdBlame.Key = "cmdBlame";
            this.cmdBlame.Name = "cmdBlame";
            this.cmdBlame.Text = "Blame";
            this.cmdBlame.ToolTipText = "Blame";
            this.cmdBlame.Click += new CommandEventHandler(this.cmdBlame_Click);
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
            this.menuFileWizard.Image = (Image) resources.GetObject("menuFileWizard.Image");
            this.menuFileWizard.Key = "menuFileWizard";
            this.menuFileWizard.Name = "menuFileWizard";
            this.menuFileWizard.Text = "Monitor this file";
            this.cmdCommit.Image = (Image) resources.GetObject("cmdCommit.Image");
            this.cmdCommit.Key = "cmdCommit";
            this.cmdCommit.Name = "cmdCommit";
            this.cmdCommit.Text = "Commit";
            this.cmdCommit.ToolTipText = "Commit";
            this.cmdCommit.Click += new CommandEventHandler(this.cmdCommit_Click);
            this.cmdRevert.Image = (Image) resources.GetObject("cmdRevert.Image");
            this.cmdRevert.Key = "cmdRevert";
            this.cmdRevert.Name = "cmdRevert";
            this.cmdRevert.Text = "Revert";
            this.cmdRevert.ToolTipText = "Revert";
            this.cmdRevert.Click += new CommandEventHandler(this.cmdRevert_Click);
            this.cmdBrowse.Image = (Image) resources.GetObject("cmdBrowse.Image");
            this.cmdBrowse.Key = "cmdBrowse";
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.ToolTipText = "Browse";
            this.cmdBrowse.Click += new CommandEventHandler(this.cmdBrowse_Click);
            this.cmdEdit.Image = (Image) resources.GetObject("cmdEdit.Image");
            this.cmdEdit.Key = "cmdEdit";
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Text = "Edit";
            this.cmdEdit.ToolTipText = "Edit";
            this.cmdEdit.Click += new CommandEventHandler(this.cmdEdit_Click);
            this.menuClipboard.Commands.AddRange(new UICommand[] { this.cmdCopyFullName1, this.cmdCopyName1, this.cmdCopyRelativeUrl1, this.cmdCopyURL1 });
            this.menuClipboard.Image = (Image) resources.GetObject("menuClipboard.Image");
            this.menuClipboard.Key = "menuClipboard";
            this.menuClipboard.Name = "menuClipboard";
            this.menuClipboard.Text = "Copy to clipboard";
            this.menuClipboard.Popup += new CommandEventHandler(this.menuClipboard_Popup);
            this.cmdCopyFullName1.Key = "cmdCopyFullName";
            this.cmdCopyFullName1.Name = "cmdCopyFullName1";
            this.cmdCopyName1.Key = "cmdCopyName";
            this.cmdCopyName1.Name = "cmdCopyName1";
            this.cmdCopyRelativeUrl1.Key = "cmdCopyRelativeURL";
            this.cmdCopyRelativeUrl1.Name = "cmdCopyRelativeUrl1";
            this.cmdCopyURL1.Key = "cmdCopyURL";
            this.cmdCopyURL1.Name = "cmdCopyURL1";
            this.cmdCopyFullName.Image = (Image) resources.GetObject("cmdCopyFullName.Image");
            this.cmdCopyFullName.Key = "cmdCopyFullName";
            this.cmdCopyFullName.Name = "cmdCopyFullName";
            this.cmdCopyFullName.Text = "&Full name";
            this.cmdCopyName.Image = (Image) resources.GetObject("cmdCopyName.Image");
            this.cmdCopyName.Key = "cmdCopyName";
            this.cmdCopyName.Name = "cmdCopyName";
            this.cmdCopyName.Text = "&Name";
            this.cmdCopyURL.Image = (Image) resources.GetObject("cmdCopyURL.Image");
            this.cmdCopyURL.Key = "cmdCopyURL";
            this.cmdCopyURL.Name = "cmdCopyURL";
            this.cmdCopyURL.Text = "&URL";
            this.cmdCopyRelativeURL.Image = (Image) resources.GetObject("cmdCopyRelativeURL.Image");
            this.cmdCopyRelativeURL.Key = "cmdCopyRelativeURL";
            this.cmdCopyRelativeURL.Name = "cmdCopyRelativeURL";
            this.cmdCopyRelativeURL.Text = "&Relative URL";
            this.cmdExport.Image = (Image) resources.GetObject("cmdExport.Image");
            this.cmdExport.Key = "cmdExport";
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Text = "&Save this revision...";
            this.cmdExport.Click += new CommandEventHandler(this.cmdExport_Click);
            this.LeftRebar1.CommandManager = this.uiCommandManager1;
            this.LeftRebar1.Dock = DockStyle.Left;
            this.LeftRebar1.Location = new Point(0, 0x1c);
            this.LeftRebar1.Name = "LeftRebar1";
            this.LeftRebar1.Size = new Size(0, 310);
            this.RightRebar1.CommandManager = this.uiCommandManager1;
            this.RightRebar1.Dock = DockStyle.Right;
            this.RightRebar1.Location = new Point(0x1f9, 0x1c);
            this.RightRebar1.Name = "RightRebar1";
            this.RightRebar1.Size = new Size(0, 310);
            this.TopRebar1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.TopRebar1.CommandManager = this.uiCommandManager1;
            this.TopRebar1.Controls.Add(this.uiCommandBar1);
            this.TopRebar1.Dock = DockStyle.Top;
            this.TopRebar1.Location = new Point(0, 0);
            this.TopRebar1.Name = "TopRebar1";
            this.TopRebar1.Size = new Size(0x39b, 0x1c);
            this.panel1.BackColor = Color.DimGray;
            this.panel1.Controls.Add(this.pathsGrid1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x1c);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(0, 1, 0, 0);
            this.panel1.Size = new Size(0x39b, 0x7a);
            this.panel1.TabIndex = 5;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.TopRebar1);
            base.Name = "PathsPanel";
            base.Size = new Size(0x39b, 150);
            ((ISupportInitialize) this.pathsGrid1).EndInit();
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

        private void InitSelectionTimer()
        {
            this.logEntrySelectionTimer = new System.Timers.Timer();
            this.pathSelectionTimer = new System.Timers.Timer();
            this.logEntrySelectionTimer.Interval = 200.0;
            this.pathSelectionTimer.Interval = 200.0;
            this.logEntrySelectionTimer.AutoReset = false;
            this.pathSelectionTimer.AutoReset = false;
            this.logEntrySelectionTimer.Elapsed += new ElapsedEventHandler(this.logEntrySelectionTimer_Elapsed);
            this.pathSelectionTimer.Elapsed += new ElapsedEventHandler(this.pathSelectionTimer_Elapsed);
        }

        private void logEntriesView_SelectionChanged(object sender, EventArgs e)
        {
            this.StartLogEntrySelectionTimer();
        }

        private void logEntrySelectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.UpdateSelectedLogEntry();
        }

        private void MapUserCommands()
        {
            this.userCommandsMap = new Dictionary<UserAction, UICommand>();
            this.UserCommandsMap.Add(UserAction.Blame, this.cmdBlame);
            this.UserCommandsMap.Add(UserAction.Browse, this.cmdBrowse);
            this.UserCommandsMap.Add(UserAction.Commit, this.cmdCommit);
            this.UserCommandsMap.Add(UserAction.Diff, this.cmdDiff);
            this.UserCommandsMap.Add(UserAction.DiffLocalWithBase, this.cmdDiffLocalWithBase);
            this.UserCommandsMap.Add(UserAction.DiffWithPrevious, this.cmdDiffWithPrevious);
            this.UserCommandsMap.Add(UserAction.Edit, this.cmdEdit);
            this.UserCommandsMap.Add(UserAction.Explore, this.cmdExplore);
            this.UserCommandsMap.Add(UserAction.Open, this.cmdOpen);
            this.UserCommandsMap.Add(UserAction.OpenWith, this.cmdOpenWith);
            this.UserCommandsMap.Add(UserAction.Revert, this.cmdRevert);
            this.UserCommandsMap.Add(UserAction.Rollback, this.cmdRollback);
            this.UserCommandsMap.Add(UserAction.ShowLog, this.cmdSVNLog);
            this.UserCommandsMap.Add(UserAction.Update, this.cmdSVNUpdate);
            this.UserCommandsMap.Add(UserAction.SaveRevision, this.cmdExport);
        }

        private void menuClipboard_Popup(object sender, CommandEventArgs e)
        {
            UIHelper.RefreshCopyCommands(e.Command.Commands);
        }

        private void menuFileWizard2_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.RunCustomWizard();
        }

        private void Open()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.Open();
            }
        }

        private void OpenBlame()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.Blame();
            }
        }

        private void OpenDiffAuto()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.DiffAuto();
            }
        }

        private void OpenDiffLocalWithBase()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.DiffLocalWithBase();
            }
        }

        private void OpenDiffWithPrevious()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.DiffWithPrevious();
            }
        }

        private void OpenSVNLog()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.OpenSVNLog();
            }
        }

        private void OpenWith()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.OpenWith();
            }
        }

        private void pathSelectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.UpdateSelectedPath();
        }

        private void pathsGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Logger.LogUserAction("key=" + e.KeyCode);
                this.DoDefaultPathAction();
            }
            if ((((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down)) || ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right))) || ((e.KeyCode == Keys.Next) || (e.KeyCode == Keys.Prior)))
            {
                this.selectedWithKeyboard = true;
            }
        }

        private void pathsGrid1_MouseDown(object sender, MouseEventArgs e)
        {
            this.selectedWithKeyboard = false;
        }

        private void pathsGrid1_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            Logger.LogUserAction();
            if ((e.Row != null) && (e.Row.DataRow is SVNPath))
            {
                this.DoDefaultPathAction();
            }
        }

        private void pathsGrid1_SelectionChanged(object sender, EventArgs e)
        {
            if (!this.suppressSelectionChanged)
            {
                this.StartPathSelectionTimer();
            }
        }

        private void Refetch()
        {
            this.suppressSelectionChanged = true;
            int[] currentRows = this.Grid.SelectedItems.Cast<GridEXSelectedItem>().Select<GridEXSelectedItem, int>(item => item.Position).ToArray<int>();
            this.Grid.Refetch();
            this.Grid.SelectedItems.Clear();
            foreach (int row in currentRows)
            {
                this.Grid.SelectedItems.Add(row);
            }
            this.suppressSelectionChanged = false;
        }

        private void Rollback()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.Rollback();
            }
        }

        private void RunCustomWizard()
        {
            SVNPath path = this.SelectedItem;
            new CustomWizard().Run(path.FilePath, "Paths", "Path");
        }

        private void SaveRevision()
        {
            SVNPath path = this.SelectedItem;
            if (path != null)
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.FileName = path.FileName;
                    dialog.Title = Strings.SavingItemRevision_FORMAT.FormatWith(new object[] { path.Revision, path.Uri });
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string targetName = dialog.FileName;
                        path.CreateCommands().SaveRevision(targetName);
                    }
                }
            }
        }

        private void SetDefaultPopupCommand()
        {
            UserAction action = EnumHelper.ParseEnum<UserAction>(ApplicationSettingsManager.Settings.DefaultPathAction);
            foreach (UICommand cmd in this.uiCommandManager1.Commands)
            {
                cmd.DefaultItem = Janus.Windows.UI.InheritableBoolean.False;
            }
            UICommand defaultCommand = this.UserCommandsMap[action];
            string key = defaultCommand.Key;
            defaultCommand = this.uiCommandManager1.Commands[key];
            defaultCommand.DefaultItem = Janus.Windows.UI.InheritableBoolean.True;
        }

        private void SetOpenCommandImage()
        {
            this.cmdOpen.Image = Images.document_plain;
            SVNPath path = this.SelectedItem;
            if ((path != null) && path.ExistsLocally)
            {
                try
                {
                    Image image = FileSystemHelper.GetAssociatedIcon(path.FilePath);
                    if (this.cmdOpen.Image != null)
                    {
                        this.cmdOpen.Image.Dispose();
                    }
                    this.cmdOpen.Image = image;
                }
                catch (Exception ex)
                {
                    Logger.Log.InfoFormat("Error trying to extract associated icon of '{0}'", path.FilePath);
                    Logger.Log.Info(ex.Message, ex);
                }
            }
        }

        internal void SetPaths()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.SetPaths));
            }
            else
            {
                if (this.SearchTextBox != null)
                {
                    this.SearchTextBox.Search();
                }
                try
                {
                    this.Refetch();
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Error trying to refetch paths:", ex);
                }
            }
        }

        internal void SetPaths(IEnumerable<SVNPath> paths)
        {
            ((PathsGrid) this.Grid).Paths = paths;
            if (!this.suppressMoveFirst)
            {
                this.Grid.MoveFirst();
            }
        }

        public void SetSearchResults(IEnumerable<SVNPath> results)
        {
            this.SetPaths(results);
        }

        private void StartLogEntrySelectionTimer()
        {
            SVNLogEntry selectedLogEntry = this.LogEntriesView.SelectedItem;
            if (selectedLogEntry == null)
            {
                this.Paths = null;
            }
            else if ((selectedLogEntry.Source != this.lastSource) || !this.LogEntriesView.SelectedWithKeyboard)
            {
                this.UpdateSelectedLogEntry();
            }
            else
            {
                this.logEntrySelectionTimer.Stop();
                this.logEntrySelectionTimer.Start();
            }
        }

        private void StartPathSelectionTimer()
        {
            if (!this.selectedWithKeyboard)
            {
                this.UpdateSelectedPath();
            }
            else
            {
                this.pathSelectionTimer.Stop();
                this.pathSelectionTimer.Start();
            }
        }

        private void SVNCommit()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.Commit();
            }
        }

        object ISelectableView<SVNPath>.Invoke(Delegate delegate1)
        {
            return base.Invoke(delegate1);
        }

        private void SVNRevert()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.Revert();
            }
        }

        private void SVNUpdate()
        {
            SVNPathCommands cmd = this.SelectedItems.CreateCommands();
            if (cmd != null)
            {
                cmd.SVNUpdate();
            }
        }

        private void uiContextMenu1_Popup(object sender, EventArgs e)
        {
            this.SetDefaultPopupCommand();
        }

        private void UpdateSelectedLogEntry()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.UpdateSelectedLogEntry));
            }
            else
            {
                SVNLogEntry selectedLogEntry = this.LogEntriesView.SelectedItem;
                if (selectedLogEntry == null)
                {
                    this.Paths = null;
                }
                else
                {
                    if (this.lastLogEntry == selectedLogEntry)
                    {
                        this.suppressMoveFirst = true;
                    }
                    this.Paths = selectedLogEntry.Paths;
                    this.suppressMoveFirst = false;
                    this.lastSource = selectedLogEntry.Source;
                    this.lastLogEntry = selectedLogEntry;
                }
            }
        }

        private void UpdateSelectedPath()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.UpdateSelectedPath));
            }
            else
            {
                this.EnableCommands();
                this.SetOpenCommandImage();
            }
        }

        [Browsable(false), AssociatedUserAction(UserAction.Blame), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanBlame
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdBlame);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdBlame, value);
            }
        }

        [AssociatedUserAction(UserAction.Browse), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanBrowse
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdBrowse);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdBrowse, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), AssociatedUserAction(UserAction.Commit)]
        public bool CanCommit
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCommit);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCommit, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanCopyFullName
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopyFullName);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopyFullName, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanCopyName
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopyName);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopyName, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanCopyRelativeURL
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopyRelativeURL);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopyRelativeURL, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanCopyURL
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdCopyURL);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdCopyURL, value);
            }
        }

        [Browsable(false), AssociatedUserAction(UserAction.Diff), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [AssociatedUserAction(UserAction.DiffLocalWithBase), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanDiffLocalWithBase
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdDiffLocalWithBase);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdDiffLocalWithBase, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), AssociatedUserAction(UserAction.DiffWithPrevious)]
        public bool CanDiffWithPrevious
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdDiffWithPrevious);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdDiffWithPrevious, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), AssociatedUserAction(UserAction.Edit), Browsable(false)]
        public bool CanEdit
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdEdit);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdEdit, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), AssociatedUserAction(UserAction.Explore)]
        public bool CanExplore
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdExplore);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdExplore, value);
            }
        }

        [AssociatedUserAction(UserAction.SaveRevision), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanExport
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

        [Browsable(false), AssociatedUserAction(UserAction.Open), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), AssociatedUserAction(UserAction.ShowLog), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [AssociatedUserAction(UserAction.OpenWith), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanOpenWith
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdOpenWith);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdOpenWith, value);
            }
        }

        [Browsable(false), AssociatedUserAction(UserAction.Revert), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanRevert
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdRevert);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdRevert, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), AssociatedUserAction(UserAction.Rollback), Browsable(false)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanRunPathWizard
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.menuFileWizard);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.menuFileWizard, value);
            }
        }

        [AssociatedUserAction(UserAction.Update), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                return this.pathsGrid1;
            }
        }

        protected override string LayoutSettings
        {
            get
            {
                return ApplicationSettingsManager.Settings.UIPathsGridLayout;
            }
        }

        [Browsable(true)]
        public ILogEntriesView LogEntriesView
        {
            [DebuggerNonUserCode]
            get
            {
                return this.logEntriesView;
            }
            [DebuggerNonUserCode]
            set
            {
                if (this.logEntriesView != null)
                {
                    this.logEntriesView.SelectionChanged -= new EventHandler(this.logEntriesView_SelectionChanged);
                }
                this.logEntriesView = value;
                if (this.logEntriesView != null)
                {
                    this.logEntriesView.SelectionChanged += new EventHandler(this.logEntriesView_SelectionChanged);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private List<SVNPath> Paths
        {
            [DebuggerNonUserCode]
            get
            {
                return this.allPaths;
            }
            set
            {
                this.allPaths = value;
                this.SetPaths();
                this.EnableCommands();
            }
        }

        [Browsable(false)]
        public SearchTextBox<SVNPath> SearchTextBox { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public SVNPath SelectedItem
        {
            get
            {
                return ((PathsGrid) this.Grid).SelectedPath;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IEnumerable<SVNPath> SelectedItems
        {
            get
            {
                return ((PathsGrid) this.Grid).SelectedPaths;
            }
        }

        public Janus.Windows.UI.CommandBars.UIContextMenu UIContextMenu
        {
            get
            {
                return this.uiContextMenu1;
            }
        }

        private Dictionary<UserAction, UICommand> UserCommandsMap
        {
            [DebuggerNonUserCode]
            get
            {
                return this.userCommandsMap;
            }
        }
    }
}

