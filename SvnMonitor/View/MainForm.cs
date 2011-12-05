using System.Linq;

namespace SVNMonitor.View
{
    using Janus.Windows.EditControls;
    using Janus.Windows.GridEX;
    using Janus.Windows.UI;
    using Janus.Windows.UI.CommandBars;
    using Janus.Windows.UI.Dock;
    using Janus.Windows.UI.StatusBar;
    using SVNMonitor;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Resources.UI;
    using SVNMonitor.Settings;
    using SVNMonitor.Support;
    using SVNMonitor.SVN;
    using SVNMonitor.View.Controls;
    using SVNMonitor.View.Dialogs;
    using SVNMonitor.View.Panels;
    using SVNMonitor.Web;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Timers;
    using System.Windows.Forms;

    internal class MainForm : Form
    {
        private SVNMonitor.View.Controls.AnimationProgressBar animationProgressBar1;
        private const int BalloonTipTimeOut = 0xea60;
        private Dictionary<UICommand, string> baseMenuTexts = new Dictionary<UICommand, string>();
        private UIRebar BottomRebar1;
        private UICheckBox checkGroupByBox;
        private UICommand cmdAbout;
        private UICommand cmdAbout1;
        private UICommand cmdBigCheckModifications;
        private UICommand cmdBigCheckModifications1;
        private UICommand cmdBigCheckSource;
        private UICommand cmdBigCheckSource1;
        private UICommand cmdBigCheckSources;
        private UICommand cmdBigCheckSources1;
        private UICommand cmdBigExplore;
        private UICommand cmdBigExplore1;
        private UICommand cmdBigOptions;
        private UICommand cmdBigOptions1;
        private UICommand cmdBigRevert;
        private UICommand cmdBigRevert1;
        private UICommand cmdBigSendFeedback;
        private UICommand cmdBigSendFeedback1;
        private UICommand cmdBigSourceCommit;
        private UICommand cmdBigSourceCommit1;
        private UICommand cmdBigUpdate;
        private UICommand cmdBigUpdate1;
        private UICommand cmdBigUpdateAll;
        private UICommand cmdBigUpdateAll1;
        private UICommand cmdBigUpdateAllAvailable;
        private UICommand cmdBigUpdateAllAvailable1;
        private UICommand cmdCheckAllSources;
        private UICommand cmdCheckAllSources1;
        private UICommand cmdCheckNewVersion;
        private UICommand cmdCheckNewVersion1;
        private UICommand cmdClose;
        private UICommand cmdClose1;
        private UICommand cmdClose2;
        private UICommand cmdEnableUpdates;
        private UICommand cmdEnableUpdates1;
        private UICommand cmdFeedback;
        private UICommand cmdFeedback1;
        private UICommand cmdFeedback2;
        private UICommand cmdGenerateError;
        private UICommand cmdGenerateError1;
        private UICommand cmdGenerateInvokeError;
        private UICommand cmdGenerateInvokeError1;
        private UICommand cmdNew;
        private UICommand cmdNew1;
        private UICommand cmdNewMonitor;
        private UICommand cmdNewMonitor1;
        private UICommand cmdNewSource;
        private UICommand cmdNewSource1;
        private UICommand cmdNewVersionAvailable;
        private UICommand cmdNewVersionAvailable1;
        private UICommand cmdOpen;
        private UICommand cmdOpen1;
        private UICommand cmdOptions;
        private UICommand cmdOptions1;
        private UICommand cmdOptions2;
        private UICommand cmdSVNUpdateAll;
        private UICommand cmdSVNUpdateAll1;
        private UICommand cmdSVNUpdateAllAvailable;
        private UICommand cmdSVNUpdateAllUnread1;
        private UICommand cmdTestDialogNewVersion;
        private UICommand cmdTestDialogNewVersion1;
        private UICommand cmdTestNewVersion;
        private UICommand cmdTestNewVersion1;
        private UICommand cmdTestSetLogEntriesGridLayout;
        private UICommand cmdTestSetLogEntriesGridLayout1;
        private UICommand cmdTSVNHelp;
        private UICommand cmdTSVNHelp1;
        private UICommand cmdTSVNSettings;
        private UICommand cmdTSVNSettings1;
        private IContainer components;
        private bool endSessionPending;
        private EventLogEntrySearchTextBox eventLogEntrySearchTextBox1;
        private SVNMonitor.View.Panels.EventLogPanel eventLogPanel1;
        private static MainForm formInstance;
        private FormWindowState lastWindowState;
        private UIRebar LeftRebar1;
        private SVNMonitor.View.Panels.LogEntriesPanel logEntriesPanel1;
        private UICommand menuCheckModifications;
        private UICommand menuCheckModifications1;
        private UICommand menuDebug;
        private UICommand menuDebug1;
        private UICommand menuDialogs;
        private UICommand menuDialogs1;
        private UICommand menuEventLog;
        private UICommand menuEventLog1;
        private UICommand menuFile;
        private UICommand menuFile1;
        private UICommand menuHelp;
        private UICommand menuHelp1;
        private UICommand menuItem;
        private UICommand menuItem1;
        private UICommand menuLog;
        private UICommand menuLog1;
        private UICommand menuMonitor;
        private UICommand menuMonitor1;
        private UICommand menuSource;
        private UICommand menuSource1;
        private UICommand menuSVNCommit;
        private UICommand menuSVNCommit1;
        private UICommand menuSVNRevert;
        private UICommand menuSVNRevert1;
        private UICommand menuSVNUpdate;
        private UICommand menuSVNUpdate1;
        private UICommand menuTestGridLayouts;
        private UICommand menuTestGridLayouts1;
        private UICommand menuTestNewVersion;
        private UICommand menuTestNewVersion1;
        private UICommand menuTools;
        private UICommand menuTools1;
        private MonitorSearchTextBox monitorSearchTextBox1;
        private SVNMonitor.View.Panels.MonitorsPanel monitorsPanel1;
        private NotifyIcon notifyIcon1;
        private SVNMonitor.View.Panels.PathsPanel pathsPanel1;
        private bool realClose;
        private UIRebar RightRebar1;
        private UICommand Separator1;
        private UICommand Separator2;
        private UICommand Separator3;
        private UICommand Separator4;
        private UICommand Separator5;
        private UICommand Separator6;
        private UICommand Separator7;
        private UICommand Separator8;
        private UICommand Separator9;
        private SourceSearchTextBox sourceSearchTextBox1;
        private SVNMonitor.View.Panels.SourcesPanel sourcesPanel1;
        private SVNLogEntrySearchTextBox svnLogEntrySearchTextBox1;
        private SVNPathSearchTextBox svnPathSearchTextBox1;
        private UIRebar TopRebar1;
        private UICommand txtTestNewVersion;
        private UICommand txtTestNewVersion1;
        private UICommand txtTestNewVersionFile;
        private UICommand txtTestNewVersionFile1;
        private UICommandBar uiCommandBar1;
        private UICommandBar uiCommandBar2;
        private UICommandManager uiCommandManager1;
        private UIContextMenu uiContextMenu1;
        private UIPanel uiPanelEventLog;
        private UIPanelCaptionContainer uiPanelEventLogCaptionContainer;
        private UIPanelInnerContainer uiPanelEventLogContainer;
        private UIPanelGroup uiPanelLeft;
        private UIPanel uiPanelLog;
        private UIPanelCaptionContainer uiPanelLogCaptionContainer;
        private UIPanelInnerContainer uiPanelLogContainer;
        private UIPanelManager uiPanelManager1;
        private UIPanel uiPanelMonitors;
        private UIPanelCaptionContainer uiPanelMonitorsCaptionContainer;
        private UIPanelInnerContainer uiPanelMonitorsContainer;
        private UIPanel uiPanelPaths;
        private UIPanelCaptionContainer uiPanelPathsCaptionContainer;
        private UIPanelInnerContainer uiPanelPathsContainer;
        private UIPanel uiPanelSources;
        private UIPanelCaptionContainer uiPanelSourcesCaptionContainer;
        private UIPanelInnerContainer uiPanelSourcesContainer;
        private UIPanel uiPanelUpdatesGridContainer;
        private UIPanelInnerContainer uiPanelUpdatesGridContainerContainer;
        private UIStatusBar uiStatusBar1;
        private UpdatesGridContainer updatesGridContainer1;
        private const int WM_QUERYENDSESSION = 0x11;

        public MainForm()
        {
            if (formInstance != null)
            {
                throw new InvalidOperationException("An instance of MainForm already exists.");
            }
            ProcessHelper.LogLoadedAssemblies();
            this.InitializeComponent();
            if (!base.DesignMode)
            {
                UIHelper.ApplyResources(this.uiCommandManager1);
                this.Notifier = new MainNotifier(this.notifyIcon1);
                if (ApplicationSettingsManager.Settings.StartMinimized)
                {
                    base.Opacity = 0.0;
                }
                MonitorSettings.Instance.LoadCaches();
                this.SetLogGroupByBoxImage();
                formInstance = this;
            }
        }

        private void BrowseDownloadPage()
        {
            SharpRegion.BrowseDownloadPage();
        }

        private void CheckAllSources()
        {
            this.SourcesPanel.UpdateAllSources();
        }

        private void checkGroupByBox_CheckedChanged(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.SetGroupByBox();
        }

        private void CheckModifications()
        {
            this.SourcesPanel.SVNCheckModifications();
        }

        private void CheckSelectedSource()
        {
            this.SourcesPanel.UpdateSource();
        }

        private void CheckStartMinimized()
        {
            if (ApplicationSettingsManager.Settings.StartMinimized)
            {
                base.WindowState = FormWindowState.Minimized;
            }
        }

        private void cmdAbout_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.ShowAbout();
        }

        private void cmdBigCheckModifications_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.CheckModifications();
        }

        private void cmdBigCheckSource_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.CheckSelectedSource();
        }

        private void cmdBigCheckSources_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.CheckAllSources();
        }

        private void cmdBigExplore_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.ExploreSelectedSource();
        }

        private void cmdBigOptions_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenOptionsDialog();
        }

        private void cmdBigRevert_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SourceRevert();
        }

        private void cmdBigSendFeedback_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SendFeedback();
        }

        private void cmdBigSourceCommit_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNCommitSelectedSource();
        }

        private void cmdBigUpdate_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNUpdateSelectedSource();
        }

        private void cmdBigUpdateAll_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNUpdateAllSources();
        }

        private void cmdBigUpdateAllAvailable_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNUpdateAllAvailableSources();
        }

        private void cmdCheckAllSources_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.UpdateAllSources();
        }

        private void cmdCheckNewVersion_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            VersionChecker.Instance.CheckVersionAsync();
        }

        private void cmdClose_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.RealClose();
        }

        private void cmdEnableUpdates_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.ToggleEnableUpdates();
        }

        private void cmdFeedback_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SendFeedback();
        }

        private void cmdGenerateError_Click(object sender, CommandEventArgs e)
        {
        }

        private void cmdGenerateInvokeError_Click(object sender, CommandEventArgs e)
        {
        }

        private void cmdNewMonitor_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.NewMonitor();
        }

        private void cmdNewSource_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.NewSource();
        }

        private void cmdNewVersionAvailable_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            VersionChecker.VersionEventArgs args = (VersionChecker.VersionEventArgs) this.cmdNewVersionAvailable.Tag;
            if (args.UpgradeAvailable)
            {
                this.ShowNewVersionDialog(args);
            }
            else
            {
                this.BrowseDownloadPage();
            }
        }

        private void cmdOpen_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.RestoreForm();
        }

        private void cmdOptions_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.OpenOptionsDialog();
        }

        private void cmdSVNCheckModifications_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SourceCheckModifications((Source) e.Command.Tag);
        }

        private void cmdSVNCommit_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SourceSVNCommit((Source) e.Command.Tag);
        }

        private void cmdSVNRevert_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SourceSVNRevert((Source) e.Command.Tag);
        }

        private void cmdSVNUpdate_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SourceSVNUpdate((Source) e.Command.Tag);
        }

        private void cmdSVNUpdateAll_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNUpdateAllSources();
        }

        private void cmdSVNUpdateAllAvailable_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.SVNUpdateAllAvailableSources();
        }

        private void cmdTestDialogNewVersion_Click(object sender, CommandEventArgs e)
        {
        }

        private void cmdTestNewVersion_Click(object sender, CommandEventArgs e)
        {
        }

        private void cmdTestSetLogEntriesGridLayout_Click(object sender, CommandEventArgs e)
        {
        }

        private void cmdTSVNHelp_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            TortoiseProcess.Help();
        }

        private void cmdTSVNSettings_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            TortoiseProcess.Settings();
        }

        private void CreateSourceCommandsSubMenu(UICommand menu, Predicate<Source> predicate, string keyPrefix, CommandSuffixProvider suffixProvider, CommandImageProvider imageProvider, CommandEventHandler eventHandler)
        {
            if (!this.baseMenuTexts.ContainsKey(menu))
            {
                this.baseMenuTexts.Add(menu, menu.Text);
            }
            else
            {
                menu.Text = this.baseMenuTexts[menu];
            }
            menu.Commands.Clear();
            menu.Click -= eventHandler;
            IEnumerable<Source> matchingSources = Status.EnabledSources.Where<Source>(s => predicate(s));
            switch (matchingSources.Count<Source>())
            {
                case 0:
                    menu.Enabled = Janus.Windows.UI.InheritableBoolean.False;
                    return;

                case 1:
                {
                    Source source = matchingSources.First<Source>();
                    string suffix = suffixProvider(source);
                    string text = string.Format("{0} \"{1}\" {2}", menu.Text, source.Name, suffix).Trim();
                    menu.Text = text;
                    menu.Key = Guid.NewGuid().ToString();
                    menu.Tag = source;
                    menu.Click += eventHandler;
                    menu.Enabled = Janus.Windows.UI.InheritableBoolean.True;
                    return;
                }
            }
            foreach (Source source in matchingSources)
            {
                UICommand cmd = new UICommand();
                string suffix = suffixProvider(source);
                string text = string.Format("{0} {1}", source.Name, suffix).Trim();
                cmd.Text = text;
                if (imageProvider == null)
                {
                    cmd.Image = menu.Image;
                }
                else
                {
                    cmd.Image = imageProvider(source);
                }
                cmd.Key = Guid.NewGuid().ToString();
                cmd.Tag = source;
                cmd.Click += eventHandler;
                menu.Commands.Add(cmd);
            }
            menu.Enabled = (menu.Commands.Count > 0).ToInheritableBoolean();
        }

        private void CreateSVNCheckModificationsSubMenu()
        {
            this.CreateSourceCommandsSubMenu(this.menuCheckModifications, src => !src.IsURL, "SVNCheckModifications", this.ModifiedOrUnversionedCountSuffixProvider, this.CheckModificationsImageProvider, new CommandEventHandler(this.cmdSVNCheckModifications_Click));
        }

        private void CreateSVNCommitSubMenu()
        {
            this.CreateSourceCommandsSubMenu(this.menuSVNCommit, delegate (Source src) {
                if (!ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled)
                {
                    return src.HasLocalChanges;
                }
                return true;
            }, "SVNCommit", this.ModifiedOrUnversionedCountSuffixProvider, null, new CommandEventHandler(this.cmdSVNCommit_Click));
        }

        private void CreateSVNRevertSubMenu()
        {
            this.CreateSourceCommandsSubMenu(this.menuSVNRevert, src => src.HasLocalVersionedChanges, "SVNRevert", this.ModifiedCountSuffixProvider, null, new CommandEventHandler(this.cmdSVNRevert_Click));
        }

        private void CreateSVNUpdateSubMenu()
        {
            this.CreateSourceCommandsSubMenu(this.menuSVNUpdate, src => !src.IsURL, "SVNUpdate", this.UnreadCountSuffixProvider, this.SVNUpdateImageProvider, new CommandEventHandler(this.cmdSVNUpdate_Click));
        }

        [Conditional("DEBUG")]
        private void DEBUG_GenerateError()
        {
            throw new Exception("Generated Error");
        }

        [Conditional("DEBUG")]
        private void DEBUG_GenerateInvokeError()
        {
            new Thread(new ThreadStart(this.DEBUG_GenerateInvokeException)).Start();
        }

        private void DEBUG_GenerateInvokeException()
        {
            base.BeginInvoke(new MethodInvoker(this.DEBUG_GenerateNullableException));
        }

        private void DEBUG_GenerateNullableException()
        {
            int? number = null;
            int local1 = number.Value;
        }

        [Conditional("DEBUG")]
        private void DEBUG_SetLogEntriesGridLayout()
        {
            this.LogEntriesPanel.SetGridLayout(ApplicationSettingsManager.Settings.UILogEntriesGridLayout);
        }

        [Conditional("DEBUG")]
        private void DEBUG_ShowDebugCommands()
        {
            this.menuDebug.Visible = Janus.Windows.UI.InheritableBoolean.True;
        }

        [Conditional("DEBUG")]
        private void DEBUG_ShowNewVersionDialog()
        {
            NewVersionDialog.ShowNewVersionDialog(FileSystemHelper.CurrentVersion, new Version("9.9.9.9"), "Some message");
        }

        private void DeleteObsoleteFiles()
        {
            string[] files = new string[] { Path.Combine(FileSystemHelper.AppData, "SVNMonitor.state"), Path.Combine(FileSystemHelper.AppData, "SVNMonitor.activity") };
            foreach (string file in files)
            {
                if (FileSystemHelper.DeleteFile(file))
                {
                    Logger.Log.Debug("Obsolete file deleted: " + file);
                }
            }
        }

        private void DeleteUpgradeDirectory(object state)
        {
            try
            {
                Logger.Log.InfoFormat("Deleting upgrade info.", new object[0]);
                UpgradeInfo.DeleteSavedUpgradeInfo();
                Logger.Log.InfoFormat("Waiting 5 seconds before deleting temp upgrade files...", new object[0]);
                SVNMonitor.Helpers.ThreadHelper.Sleep(0x1388);
                Logger.Log.InfoFormat("Deleting upgrade temp directory: {0}", SessionInfo.UpgradedFrom);
                FileSystemHelper.DeleteDirectory(SessionInfo.UpgradedFrom);
                string upgradedFromOriginalZip = SessionInfo.UpgradedFrom.Substring(0, SessionInfo.UpgradedFrom.Length - 1);
                Logger.Log.InfoFormat("Deleting upgrade temp zip: {0}", upgradedFromOriginalZip);
                FileSystemHelper.DeleteFile(upgradedFromOriginalZip);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error trying to delete the upgrade directory and/or files: {0}", SessionInfo.UpgradedFrom), ex);
            }
        }

        private void DeleteUpgradeDirectoryAsync()
        {
            SVNMonitor.Helpers.ThreadHelper.Queue(new WaitCallback(this.DeleteUpgradeDirectory), "DELUPGRADE");
        }

        protected override void Dispose(bool disposing)
        {
        }

        private void EnableCommands()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.EnableCommands));
            }
            else
            {
                bool tempAnotherLocal0;
                bool tempAnotherLocal1;
                bool tempAnotherLocal2;
                bool sourcesExists = MonitorSettings.Instance.Sources.Count > 0;
                bool availableUpdatesExists = Status.NotUpToDateSources.Count<Source>() > 0;
                this.CanSVNUpdateAllAvailable = availableUpdatesExists;
                this.CanSVNUpdateAll = sourcesExists;
                this.CanCheckUpdates = tempAnotherLocal0 = true;
                this.CanRevert = tempAnotherLocal1 = tempAnotherLocal0;
                this.CanCommit = tempAnotherLocal2 = tempAnotherLocal1;
                this.CanCheckModifications = this.CanUpdate = tempAnotherLocal2;
                this.CanBigCheckModifications = this.SourcesPanel.CanSVNCheckForModifications;
                this.CanBigCheckSource = this.SourcesPanel.CanCheckForUpdates;
                this.CanBigCheckSources = sourcesExists;
                this.CanBigExplore = this.SourcesPanel.CanExplore;
                this.CanBigOptions = true;
                this.CanBigSendFeedback = true;
                this.CanBigUpdate = this.SourcesPanel.CanSVNUpdate;
                this.CanBigCommit = this.SourcesPanel.CanSVNCommit;
                this.CanBigRevert = this.SourcesPanel.CanSVNRevert;
                this.CanBigUpdateAll = sourcesExists;
                this.CanBigUpdateAllAvailable = availableUpdatesExists;
            }
        }

        private void ExploreSelectedSource()
        {
            this.SourcesPanel.Browse();
        }

        public void FocusEventLog()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.FocusEventLog));
            }
            else
            {
                base.BringToFront();
                base.Activate();
                this.uiPanelEventLog.AutoHideActive = true;
                this.uiPanelEventLog.Focus();
            }
        }

        public void FocusEventLog(long eventID)
        {
            this.FocusEventLog();
            SVNMonitor.EventLog.Open(eventID);
        }

        private void Hide()
        {
            List<Form> relevantForms = new List<Form>();
            foreach (Form form in Application.OpenForms)
            {
                if (!form.GetType().Assembly.FullName.StartsWith("Janus."))
                {
                    relevantForms.Add(form);
                }
            }
            if (relevantForms.Count > 1)
            {
                Logger.Log.Info("Trying to hide the main window, but there are more open windows.");
                try
                {
                    for (int i = 1; i < relevantForms.Count; i++)
                    {
                        Form otherForm = relevantForms[i];
                        Logger.Log.InfoFormat("Window: {0} ({1})", otherForm.Text, otherForm.GetType());
                        if (i == (relevantForms.Count - 1))
                        {
                            otherForm.Activate();
                        }
                        WindowFlasher.Flash(otherForm);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Error trying to flash the window.", ex);
                }
            }
            else
            {
                foreach (Form form in Application.OpenForms)
                {
                    try
                    {
                        form.Hide();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(string.Format("Error trying to hide a window: {0} ({1})", form.Text, form.GetType()), ex);
                    }
                }
                base.Hide();
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            UIStatusBarPanel uiStatusBarPanel1 = new UIStatusBarPanel();
            UICommandCategory uiCommandCategory1 = new UICommandCategory();
            this.animationProgressBar1 = new SVNMonitor.View.Controls.AnimationProgressBar();
            this.uiPanelManager1 = new UIPanelManager(this.components);
            this.uiPanelEventLog = new UIPanel();
            this.uiPanelEventLogCaptionContainer = new UIPanelCaptionContainer();
            this.eventLogEntrySearchTextBox1 = new EventLogEntrySearchTextBox();
            this.eventLogPanel1 = new SVNMonitor.View.Panels.EventLogPanel();
            this.uiPanelEventLogContainer = new UIPanelInnerContainer();
            this.uiPanelLeft = new UIPanelGroup();
            this.uiPanelSources = new UIPanel();
            this.uiPanelSourcesCaptionContainer = new UIPanelCaptionContainer();
            this.sourceSearchTextBox1 = new SourceSearchTextBox();
            this.sourcesPanel1 = new SVNMonitor.View.Panels.SourcesPanel();
            this.uiPanelSourcesContainer = new UIPanelInnerContainer();
            this.uiPanelMonitors = new UIPanel();
            this.uiPanelMonitorsCaptionContainer = new UIPanelCaptionContainer();
            this.monitorSearchTextBox1 = new MonitorSearchTextBox();
            this.monitorsPanel1 = new SVNMonitor.View.Panels.MonitorsPanel();
            this.uiPanelMonitorsContainer = new UIPanelInnerContainer();
            this.uiPanelPaths = new UIPanel();
            this.uiPanelPathsCaptionContainer = new UIPanelCaptionContainer();
            this.svnPathSearchTextBox1 = new SVNPathSearchTextBox();
            this.pathsPanel1 = new SVNMonitor.View.Panels.PathsPanel();
            this.logEntriesPanel1 = new SVNMonitor.View.Panels.LogEntriesPanel();
            this.svnLogEntrySearchTextBox1 = new SVNLogEntrySearchTextBox();
            this.uiPanelPathsContainer = new UIPanelInnerContainer();
            this.uiPanelUpdatesGridContainer = new UIPanel();
            this.uiPanelUpdatesGridContainerContainer = new UIPanelInnerContainer();
            this.updatesGridContainer1 = new UpdatesGridContainer();
            this.uiPanelLog = new UIPanel();
            this.uiPanelLogCaptionContainer = new UIPanelCaptionContainer();
            this.checkGroupByBox = new UICheckBox();
            this.uiPanelLogContainer = new UIPanelInnerContainer();
            this.notifyIcon1 = new NotifyIcon(this.components);
            this.uiStatusBar1 = new UIStatusBar();
            this.uiCommandManager1 = new UICommandManager(this.components);
            this.BottomRebar1 = new UIRebar();
            this.uiCommandBar1 = new UICommandBar();
            this.menuFile1 = new UICommand("menuFile");
            this.menuSource1 = new UICommand("menuSource");
            this.menuMonitor1 = new UICommand("menuMonitor");
            this.menuLog1 = new UICommand("menuLog");
            this.menuItem1 = new UICommand("menuItem");
            this.menuEventLog1 = new UICommand("menuEventLog");
            this.menuTools1 = new UICommand("menuTools");
            this.menuHelp1 = new UICommand("menuHelp");
            this.menuDebug1 = new UICommand("menuDebug");
            this.cmdNewVersionAvailable1 = new UICommand("cmdNewVersionAvailable");
            this.uiCommandBar2 = new UICommandBar();
            this.cmdBigCheckSource1 = new UICommand("cmdBigCheckSource");
            this.cmdBigCheckSources1 = new UICommand("cmdBigCheckSources");
            this.cmdBigCheckModifications1 = new UICommand("cmdBigCheckModifications");
            this.cmdBigExplore1 = new UICommand("cmdBigExplore");
            this.Separator6 = new UICommand("Separator");
            this.cmdBigUpdate1 = new UICommand("cmdBigUpdate");
            this.cmdBigSourceCommit1 = new UICommand("cmdBigSourceCommit");
            this.cmdBigRevert1 = new UICommand("cmdBigRevert");
            this.Separator8 = new UICommand("Separator");
            this.cmdBigUpdateAllAvailable1 = new UICommand("cmdBigUpdateAllAvailable");
            this.cmdBigUpdateAll1 = new UICommand("cmdBigUpdateAll");
            this.Separator7 = new UICommand("Separator");
            this.cmdBigOptions1 = new UICommand("cmdBigOptions");
            this.cmdBigSendFeedback1 = new UICommand("cmdBigSendFeedback");
            this.menuFile = new UICommand("menuFile");
            this.cmdNew1 = new UICommand("cmdNew");
            this.cmdClose2 = new UICommand("cmdClose");
            this.menuSource = new UICommand("menuSource");
            this.menuMonitor = new UICommand("menuMonitor");
            this.menuLog = new UICommand("menuLog");
            this.menuItem = new UICommand("menuItem");
            this.menuTools = new UICommand("menuTools");
            this.cmdOptions1 = new UICommand("cmdOptions");
            this.cmdTSVNSettings1 = new UICommand("cmdTSVNSettings");
            this.menuEventLog = new UICommand("menuEventLog");
            this.cmdOptions = new UICommand("cmdOptions");
            this.cmdEnableUpdates = new UICommand("cmdEnableUpdates");
            this.cmdOpen = new UICommand("cmdOpen");
            this.cmdClose = new UICommand("cmdClose");
            this.cmdCheckAllSources = new UICommand("cmdCheckAllSources");
            this.cmdSVNUpdateAll = new UICommand("cmdSVNUpdateAll");
            this.cmdSVNUpdateAllAvailable = new UICommand("cmdSVNUpdateAllAvailable");
            this.menuSVNUpdate = new UICommand("menuSVNUpdate");
            this.menuSVNCommit = new UICommand("menuSVNCommit");
            this.menuSVNRevert = new UICommand("menuSVNRevert");
            this.menuCheckModifications = new UICommand("menuCheckModifications");
            this.menuHelp = new UICommand("menuHelp");
            this.cmdCheckNewVersion1 = new UICommand("cmdCheckNewVersion");
            this.Separator5 = new UICommand("Separator");
            this.cmdTSVNHelp1 = new UICommand("cmdTSVNHelp");
            this.cmdFeedback1 = new UICommand("cmdFeedback");
            this.cmdAbout1 = new UICommand("cmdAbout");
            this.cmdAbout = new UICommand("cmdAbout");
            this.cmdCheckNewVersion = new UICommand("cmdCheckNewVersion");
            this.cmdNew = new UICommand("cmdNew");
            this.cmdNewSource1 = new UICommand("cmdNewSource");
            this.cmdNewMonitor1 = new UICommand("cmdNewMonitor");
            this.cmdNewSource = new UICommand("cmdNewSource");
            this.cmdNewMonitor = new UICommand("cmdNewMonitor");
            this.cmdNewVersionAvailable = new UICommand("cmdNewVersionAvailable");
            this.cmdTSVNSettings = new UICommand("cmdTSVNSettings");
            this.cmdTSVNHelp = new UICommand("cmdTSVNHelp");
            this.cmdFeedback = new UICommand("cmdFeedback");
            this.menuDebug = new UICommand("menuDebug");
            this.menuDialogs1 = new UICommand("menuDialogs");
            this.Separator9 = new UICommand("Separator");
            this.cmdGenerateError1 = new UICommand("cmdGenerateError");
            this.cmdGenerateInvokeError1 = new UICommand("cmdGenerateInvokeError");
            this.menuTestNewVersion1 = new UICommand("menuTestNewVersion");
            this.cmdGenerateError = new UICommand("cmdGenerateError");
            this.cmdGenerateInvokeError = new UICommand("cmdGenerateInvokeError");
            this.cmdTestNewVersion = new UICommand("cmdTestNewVersion");
            this.txtTestNewVersion = new UICommand("txtTestNewVersion");
            this.txtTestNewVersionFile = new UICommand("txtTestNewVersionFile");
            this.menuTestNewVersion = new UICommand("menuTestNewVersion");
            this.txtTestNewVersion1 = new UICommand("txtTestNewVersion");
            this.txtTestNewVersionFile1 = new UICommand("txtTestNewVersionFile");
            this.cmdTestNewVersion1 = new UICommand("cmdTestNewVersion");
            this.cmdBigCheckSources = new UICommand("cmdBigCheckSources");
            this.cmdBigCheckSource = new UICommand("cmdBigCheckSource");
            this.cmdBigCheckModifications = new UICommand("cmdBigCheckModifications");
            this.cmdBigExplore = new UICommand("cmdBigExplore");
            this.cmdBigUpdate = new UICommand("cmdBigUpdate");
            this.cmdBigUpdateAll = new UICommand("cmdBigUpdateAll");
            this.cmdBigUpdateAllAvailable = new UICommand("cmdBigUpdateAllAvailable");
            this.cmdBigOptions = new UICommand("cmdBigOptions");
            this.cmdBigSendFeedback = new UICommand("cmdBigSendFeedback");
            this.cmdBigSourceCommit = new UICommand("cmdBigSourceCommit");
            this.cmdBigRevert = new UICommand("cmdBigRevert");
            this.cmdTestDialogNewVersion = new UICommand("cmdTestDialogNewVersion");
            this.menuDialogs = new UICommand("menuDialogs");
            this.cmdTestDialogNewVersion1 = new UICommand("cmdTestDialogNewVersion");
            this.uiContextMenu1 = new UIContextMenu();
            this.cmdEnableUpdates1 = new UICommand("cmdEnableUpdates");
            this.Separator1 = new UICommand("Separator");
            this.menuCheckModifications1 = new UICommand("menuCheckModifications");
            this.menuSVNUpdate1 = new UICommand("menuSVNUpdate");
            this.cmdSVNUpdateAllUnread1 = new UICommand("cmdSVNUpdateAllAvailable");
            this.cmdSVNUpdateAll1 = new UICommand("cmdSVNUpdateAll");
            this.menuSVNCommit1 = new UICommand("menuSVNCommit");
            this.menuSVNRevert1 = new UICommand("menuSVNRevert");
            this.Separator4 = new UICommand("Separator");
            this.cmdCheckAllSources1 = new UICommand("cmdCheckAllSources");
            this.Separator3 = new UICommand("Separator");
            this.cmdOptions2 = new UICommand("cmdOptions");
            this.cmdOpen1 = new UICommand("cmdOpen");
            this.Separator2 = new UICommand("Separator");
            this.cmdFeedback2 = new UICommand("cmdFeedback");
            this.cmdClose1 = new UICommand("cmdClose");
            this.LeftRebar1 = new UIRebar();
            this.RightRebar1 = new UIRebar();
            this.TopRebar1 = new UIRebar();
            this.menuTestGridLayouts = new UICommand("menuTestGridLayouts");
            this.cmdTestSetLogEntriesGridLayout = new UICommand("cmdTestSetLogEntriesGridLayout");
            this.cmdTestSetLogEntriesGridLayout1 = new UICommand("cmdTestSetLogEntriesGridLayout");
            this.menuTestGridLayouts1 = new UICommand("menuTestGridLayouts");
            ((ISupportInitialize) this.uiPanelManager1).BeginInit();
            ((ISupportInitialize) this.uiPanelEventLog).BeginInit();
            this.uiPanelEventLog.SuspendLayout();
            this.uiPanelEventLogCaptionContainer.SuspendLayout();
            this.uiPanelEventLogContainer.SuspendLayout();
            ((ISupportInitialize) this.uiPanelLeft).BeginInit();
            this.uiPanelLeft.SuspendLayout();
            ((ISupportInitialize) this.uiPanelSources).BeginInit();
            this.uiPanelSources.SuspendLayout();
            this.uiPanelSourcesCaptionContainer.SuspendLayout();
            this.sourcesPanel1.BeginInit();
            this.uiPanelSourcesContainer.SuspendLayout();
            ((ISupportInitialize) this.uiPanelMonitors).BeginInit();
            this.uiPanelMonitors.SuspendLayout();
            this.uiPanelMonitorsCaptionContainer.SuspendLayout();
            this.uiPanelMonitorsContainer.SuspendLayout();
            ((ISupportInitialize) this.uiPanelPaths).BeginInit();
            this.uiPanelPaths.SuspendLayout();
            this.uiPanelPathsCaptionContainer.SuspendLayout();
            this.pathsPanel1.BeginInit();
            this.logEntriesPanel1.BeginInit();
            this.uiPanelPathsContainer.SuspendLayout();
            ((ISupportInitialize) this.uiPanelUpdatesGridContainer).BeginInit();
            this.uiPanelUpdatesGridContainer.SuspendLayout();
            this.uiPanelUpdatesGridContainerContainer.SuspendLayout();
            ((ISupportInitialize) this.uiPanelLog).BeginInit();
            this.uiPanelLog.SuspendLayout();
            this.uiPanelLogCaptionContainer.SuspendLayout();
            this.uiPanelLogContainer.SuspendLayout();
            this.uiStatusBar1.SuspendLayout();
            ((ISupportInitialize) this.uiCommandManager1).BeginInit();
            ((ISupportInitialize) this.BottomRebar1).BeginInit();
            ((ISupportInitialize) this.uiCommandBar1).BeginInit();
            ((ISupportInitialize) this.uiCommandBar2).BeginInit();
            ((ISupportInitialize) this.uiContextMenu1).BeginInit();
            ((ISupportInitialize) this.LeftRebar1).BeginInit();
            ((ISupportInitialize) this.RightRebar1).BeginInit();
            ((ISupportInitialize) this.TopRebar1).BeginInit();
            this.TopRebar1.SuspendLayout();
            base.SuspendLayout();
            this.animationProgressBar1.BackColor = Color.Transparent;
            resources.ApplyResources(this.animationProgressBar1, "animationProgressBar1");
            this.animationProgressBar1.Name = "animationProgressBar1";
            this.uiPanelManager1.AllowAutoHideAnimation = false;
            this.uiPanelManager1.AllowPanelDrag = false;
            this.uiPanelManager1.AllowPanelDrop = false;
            this.uiPanelManager1.BackColorAutoHideStrip = Color.FromArgb(160, 160, 160);
            this.uiPanelManager1.ContainerControl = this;
            this.uiPanelManager1.SettingsKey = "uiPanelManager1";
            this.uiPanelManager1.VisualStyle = PanelVisualStyle.Standard;
            this.uiPanelEventLog.Id = new Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e");
            this.uiPanelManager1.Panels.Add(this.uiPanelEventLog);
            this.uiPanelLeft.Id = new Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247");
            this.uiPanelSources.Id = new Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811");
            this.uiPanelLeft.Panels.Add(this.uiPanelSources);
            this.uiPanelMonitors.Id = new Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a");
            this.uiPanelLeft.Panels.Add(this.uiPanelMonitors);
            this.uiPanelManager1.Panels.Add(this.uiPanelLeft);
            this.uiPanelPaths.Id = new Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e");
            this.uiPanelManager1.Panels.Add(this.uiPanelPaths);
            this.uiPanelUpdatesGridContainer.Id = new Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09");
            this.uiPanelManager1.Panels.Add(this.uiPanelUpdatesGridContainer);
            this.uiPanelLog.Id = new Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae");
            this.uiPanelManager1.Panels.Add(this.uiPanelLog);
            this.uiPanelManager1.BeginPanelInfo();
            this.uiPanelManager1.AddDockPanelInfo(new Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e"), PanelDockStyle.Bottom, new Size(0x31a, 0xa2), true);
            this.uiPanelManager1.AddDockPanelInfo(new Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), PanelGroupStyle.HorizontalTiles, PanelDockStyle.Left, false, new Size(300, 640), true);
            this.uiPanelManager1.AddDockPanelInfo(new Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811"), new Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), 250, true);
            this.uiPanelManager1.AddDockPanelInfo(new Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a"), new Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), 0xba, true);
            this.uiPanelManager1.AddDockPanelInfo(new Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e"), PanelDockStyle.Bottom, new Size(0x2a6, 200), true);
            this.uiPanelManager1.AddDockPanelInfo(new Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09"), PanelDockStyle.Bottom, new Size(0x271, 200), true);
            this.uiPanelManager1.AddDockPanelInfo(new Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae"), PanelDockStyle.Fill, new Size(0x2a6, 440), true);
            this.uiPanelManager1.AddFloatingPanelInfo(new Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811"), new Point(-1, -1), new Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae"), new Point(-1, -1), new Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e"), new Point(-1, -1), new Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new Guid("8377ca32-f17f-4127-ae20-70684bb27510"), new Point(0x1b6, 0x1fb), new Size(200, 200), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09"), new Point(-1, -1), new Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a"), new Point(0x1a7, 0x179), new Size(200, 200), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e"), new Point(0x23e, 0x224), new Size(200, 200), false);
            this.uiPanelManager1.EndPanelInfo();
            this.uiPanelEventLog.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
            resources.ApplyResources(this.uiPanelEventLog, "uiPanelEventLog");
            this.uiPanelEventLog.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
            this.uiPanelEventLog.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPanelEventLog.ContainerCaption = true;
            this.uiPanelEventLog.ContainerCaptionControl = this.uiPanelEventLogCaptionContainer;
            this.uiPanelEventLog.FloatingLocation = new Point(0x23e, 0x224);
            this.uiPanelEventLog.InnerContainer = this.uiPanelEventLogContainer;
            this.uiPanelEventLog.Name = "uiPanelEventLog";
            this.uiPanelEventLogCaptionContainer.Controls.Add(this.eventLogEntrySearchTextBox1);
            resources.ApplyResources(this.uiPanelEventLogCaptionContainer, "uiPanelEventLogCaptionContainer");
            this.uiPanelEventLogCaptionContainer.Name = "uiPanelEventLogCaptionContainer";
            this.uiPanelEventLogCaptionContainer.Panel = this.uiPanelEventLog;
            resources.ApplyResources(this.eventLogEntrySearchTextBox1, "eventLogEntrySearchTextBox1");
            this.eventLogEntrySearchTextBox1.BackColor = Color.Transparent;
            this.eventLogEntrySearchTextBox1.Name = "eventLogEntrySearchTextBox1";
            this.eventLogEntrySearchTextBox1.RightMargin = 0x18;
            this.eventLogEntrySearchTextBox1.SearchablePanel = this.eventLogPanel1;
            resources.ApplyResources(this.eventLogPanel1, "eventLogPanel1");
            this.eventLogPanel1.Name = "eventLogPanel1";
            this.eventLogPanel1.SearchTextBox = this.eventLogEntrySearchTextBox1;
            this.uiPanelEventLogContainer.Controls.Add(this.eventLogPanel1);
            resources.ApplyResources(this.uiPanelEventLogContainer, "uiPanelEventLogContainer");
            this.uiPanelEventLogContainer.Name = "uiPanelEventLogContainer";
            resources.ApplyResources(this.uiPanelLeft, "uiPanelLeft");
            this.uiPanelLeft.Name = "uiPanelLeft";
            this.uiPanelSources.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
            this.uiPanelSources.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
            resources.ApplyResources(this.uiPanelSources, "uiPanelSources");
            this.uiPanelSources.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPanelSources.ContainerCaption = true;
            this.uiPanelSources.ContainerCaptionControl = this.uiPanelSourcesCaptionContainer;
            this.uiPanelSources.InnerContainer = this.uiPanelSourcesContainer;
            this.uiPanelSources.Name = "uiPanelSources";
            this.uiPanelSourcesCaptionContainer.Controls.Add(this.sourceSearchTextBox1);
            resources.ApplyResources(this.uiPanelSourcesCaptionContainer, "uiPanelSourcesCaptionContainer");
            this.uiPanelSourcesCaptionContainer.Name = "uiPanelSourcesCaptionContainer";
            this.uiPanelSourcesCaptionContainer.Panel = this.uiPanelSources;
            resources.ApplyResources(this.sourceSearchTextBox1, "sourceSearchTextBox1");
            this.sourceSearchTextBox1.BackColor = Color.Transparent;
            this.sourceSearchTextBox1.Name = "sourceSearchTextBox1";
            this.sourceSearchTextBox1.RightMargin = 0x18;
            this.sourceSearchTextBox1.SearchablePanel = this.sourcesPanel1;
            this.sourcesPanel1.AllowDrop = true;
            this.sourcesPanel1.BackColor = Color.Transparent;
            resources.ApplyResources(this.sourcesPanel1, "sourcesPanel1");
            this.sourcesPanel1.Name = "sourcesPanel1";
            this.sourcesPanel1.SearchTextBox = this.sourceSearchTextBox1;
            this.sourcesPanel1.ShowingAllItems = false;
            this.sourcesPanel1.SelectionChanged += new EventHandler(this.sourcesPanel1_SelectionChanged);
            this.uiPanelSourcesContainer.Controls.Add(this.sourcesPanel1);
            resources.ApplyResources(this.uiPanelSourcesContainer, "uiPanelSourcesContainer");
            this.uiPanelSourcesContainer.Name = "uiPanelSourcesContainer";
            this.uiPanelMonitors.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
            this.uiPanelMonitors.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
            resources.ApplyResources(this.uiPanelMonitors, "uiPanelMonitors");
            this.uiPanelMonitors.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPanelMonitors.ContainerCaption = true;
            this.uiPanelMonitors.ContainerCaptionControl = this.uiPanelMonitorsCaptionContainer;
            this.uiPanelMonitors.FloatingLocation = new Point(0x1a7, 0x179);
            this.uiPanelMonitors.InnerContainer = this.uiPanelMonitorsContainer;
            this.uiPanelMonitors.Name = "uiPanelMonitors";
            this.uiPanelMonitorsCaptionContainer.Controls.Add(this.monitorSearchTextBox1);
            resources.ApplyResources(this.uiPanelMonitorsCaptionContainer, "uiPanelMonitorsCaptionContainer");
            this.uiPanelMonitorsCaptionContainer.Name = "uiPanelMonitorsCaptionContainer";
            this.uiPanelMonitorsCaptionContainer.Panel = this.uiPanelMonitors;
            resources.ApplyResources(this.monitorSearchTextBox1, "monitorSearchTextBox1");
            this.monitorSearchTextBox1.BackColor = Color.Transparent;
            this.monitorSearchTextBox1.Name = "monitorSearchTextBox1";
            this.monitorSearchTextBox1.RightMargin = 0x18;
            this.monitorSearchTextBox1.SearchablePanel = this.monitorsPanel1;
            this.monitorsPanel1.BackColor = Color.Transparent;
            resources.ApplyResources(this.monitorsPanel1, "monitorsPanel1");
            this.monitorsPanel1.Name = "monitorsPanel1";
            this.monitorsPanel1.SearchTextBox = this.monitorSearchTextBox1;
            this.uiPanelMonitorsContainer.Controls.Add(this.monitorsPanel1);
            resources.ApplyResources(this.uiPanelMonitorsContainer, "uiPanelMonitorsContainer");
            this.uiPanelMonitorsContainer.Name = "uiPanelMonitorsContainer";
            this.uiPanelPaths.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
            this.uiPanelPaths.AutoHideButtonVisible = Janus.Windows.UI.InheritableBoolean.True;
            this.uiPanelPaths.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
            resources.ApplyResources(this.uiPanelPaths, "uiPanelPaths");
            this.uiPanelPaths.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPanelPaths.ContainerCaption = true;
            this.uiPanelPaths.ContainerCaptionControl = this.uiPanelPathsCaptionContainer;
            this.uiPanelPaths.InnerContainer = this.uiPanelPathsContainer;
            this.uiPanelPaths.Name = "uiPanelPaths";
            this.uiPanelPathsCaptionContainer.Controls.Add(this.svnPathSearchTextBox1);
            resources.ApplyResources(this.uiPanelPathsCaptionContainer, "uiPanelPathsCaptionContainer");
            this.uiPanelPathsCaptionContainer.Name = "uiPanelPathsCaptionContainer";
            this.uiPanelPathsCaptionContainer.Panel = this.uiPanelPaths;
            resources.ApplyResources(this.svnPathSearchTextBox1, "svnPathSearchTextBox1");
            this.svnPathSearchTextBox1.BackColor = Color.Transparent;
            this.svnPathSearchTextBox1.Name = "svnPathSearchTextBox1";
            this.svnPathSearchTextBox1.RightMargin = 0x18;
            this.svnPathSearchTextBox1.SearchablePanel = this.pathsPanel1;
            this.pathsPanel1.BackColor = Color.Transparent;
            resources.ApplyResources(this.pathsPanel1, "pathsPanel1");
            this.pathsPanel1.LogEntriesView = this.logEntriesPanel1;
            this.pathsPanel1.Name = "pathsPanel1";
            this.pathsPanel1.SearchTextBox = this.svnPathSearchTextBox1;
            this.logEntriesPanel1.BackColor = Color.Transparent;
            resources.ApplyResources(this.logEntriesPanel1, "logEntriesPanel1");
            this.logEntriesPanel1.GroupByBoxVisible = false;
            this.logEntriesPanel1.Name = "logEntriesPanel1";
            this.logEntriesPanel1.SearchTextBox = this.svnLogEntrySearchTextBox1;
            this.logEntriesPanel1.SelectedWithKeyboard = false;
            this.logEntriesPanel1.SourcesView = this.sourcesPanel1;
            this.logEntriesPanel1.SelectionChanged += new EventHandler(this.logEntriesPanel1_SelectionChanged);
            resources.ApplyResources(this.svnLogEntrySearchTextBox1, "svnLogEntrySearchTextBox1");
            this.svnLogEntrySearchTextBox1.BackColor = Color.Transparent;
            this.svnLogEntrySearchTextBox1.Name = "svnLogEntrySearchTextBox1";
            this.svnLogEntrySearchTextBox1.RightMargin = 0x18;
            this.svnLogEntrySearchTextBox1.SearchablePanel = this.logEntriesPanel1;
            this.uiPanelPathsContainer.Controls.Add(this.pathsPanel1);
            resources.ApplyResources(this.uiPanelPathsContainer, "uiPanelPathsContainer");
            this.uiPanelPathsContainer.Name = "uiPanelPathsContainer";
            this.uiPanelUpdatesGridContainer.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
            this.uiPanelUpdatesGridContainer.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            resources.ApplyResources(this.uiPanelUpdatesGridContainer, "uiPanelUpdatesGridContainer");
            this.uiPanelUpdatesGridContainer.InnerContainer = this.uiPanelUpdatesGridContainerContainer;
            this.uiPanelUpdatesGridContainer.Name = "uiPanelUpdatesGridContainer";
            this.uiPanelUpdatesGridContainerContainer.Controls.Add(this.updatesGridContainer1);
            resources.ApplyResources(this.uiPanelUpdatesGridContainerContainer, "uiPanelUpdatesGridContainerContainer");
            this.uiPanelUpdatesGridContainerContainer.Name = "uiPanelUpdatesGridContainerContainer";
            this.updatesGridContainer1.BackColor = Color.Transparent;
            resources.ApplyResources(this.updatesGridContainer1, "updatesGridContainer1");
            this.updatesGridContainer1.Name = "updatesGridContainer1";
            this.uiPanelLog.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
            this.uiPanelLog.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
            resources.ApplyResources(this.uiPanelLog, "uiPanelLog");
            this.uiPanelLog.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPanelLog.ContainerCaption = true;
            this.uiPanelLog.ContainerCaptionControl = this.uiPanelLogCaptionContainer;
            this.uiPanelLog.InnerContainer = this.uiPanelLogContainer;
            this.uiPanelLog.Name = "uiPanelLog";
            this.uiPanelLogCaptionContainer.Controls.Add(this.svnLogEntrySearchTextBox1);
            this.uiPanelLogCaptionContainer.Controls.Add(this.checkGroupByBox);
            resources.ApplyResources(this.uiPanelLogCaptionContainer, "uiPanelLogCaptionContainer");
            this.uiPanelLogCaptionContainer.Name = "uiPanelLogCaptionContainer";
            this.uiPanelLogCaptionContainer.Panel = this.uiPanelLog;
            resources.ApplyResources(this.checkGroupByBox, "checkGroupByBox");
            this.checkGroupByBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkGroupByBox.Checked = true;
            this.checkGroupByBox.CheckState = CheckState.Checked;
            this.checkGroupByBox.ImageAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center;
            this.checkGroupByBox.Name = "checkGroupByBox";
            this.checkGroupByBox.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.checkGroupByBox.CheckedChanged += new EventHandler(this.checkGroupByBox_CheckedChanged);
            this.uiPanelLogContainer.Controls.Add(this.logEntriesPanel1);
            resources.ApplyResources(this.uiPanelLogContainer, "uiPanelLogContainer");
            this.uiPanelLogContainer.Name = "uiPanelLogContainer";
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.MouseClick += new MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            this.uiStatusBar1.Controls.Add(this.animationProgressBar1);
            resources.ApplyResources(this.uiStatusBar1, "uiStatusBar1");
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = StatusBarPanelAutoSize.Spring;
            uiStatusBarPanel1.BorderColor = Color.Empty;
            uiStatusBarPanel1.Control = this.animationProgressBar1;
            uiStatusBarPanel1.Key = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
            uiStatusBarPanel1.PanelType = StatusBarPanelType.ControlContainer;
            resources.ApplyResources(uiStatusBarPanel1, "uiStatusBarPanel1");
            this.uiStatusBar1.Panels.AddRange(new UIStatusBarPanel[] { uiStatusBarPanel1 });
            this.uiStatusBar1.PanelsBorderColor = Color.Transparent;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
            this.uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.BottomRebar = this.BottomRebar1;
            resources.ApplyResources(uiCommandCategory1, "uiCommandCategory1");
            this.uiCommandManager1.Categories.AddRange(new UICommandCategory[] { uiCommandCategory1 });
            this.uiCommandManager1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1, this.uiCommandBar2 });
            this.uiCommandManager1.Commands.AddRange(new UICommand[] { 
                this.menuFile, this.menuSource, this.menuMonitor, this.menuLog, this.menuItem, this.menuTools, this.menuEventLog, this.cmdOptions, this.cmdEnableUpdates, this.cmdOpen, this.cmdClose, this.cmdCheckAllSources, this.cmdSVNUpdateAll, this.cmdSVNUpdateAllAvailable, this.menuSVNUpdate, this.menuSVNCommit, 
                this.menuSVNRevert, this.menuCheckModifications, this.menuHelp, this.cmdAbout, this.cmdCheckNewVersion, this.cmdNew, this.cmdNewSource, this.cmdNewMonitor, this.cmdNewVersionAvailable, this.cmdTSVNSettings, this.cmdTSVNHelp, this.cmdFeedback, this.menuDebug, this.cmdGenerateError, this.cmdGenerateInvokeError, this.cmdTestNewVersion, 
                this.txtTestNewVersion, this.txtTestNewVersionFile, this.menuTestNewVersion, this.cmdBigCheckSources, this.cmdBigCheckSource, this.cmdBigCheckModifications, this.cmdBigExplore, this.cmdBigUpdate, this.cmdBigUpdateAll, this.cmdBigUpdateAllAvailable, this.cmdBigOptions, this.cmdBigSendFeedback, this.cmdBigSourceCommit, this.cmdBigRevert, this.cmdTestDialogNewVersion, this.menuDialogs, 
                this.menuTestGridLayouts, this.cmdTestSetLogEntriesGridLayout
             });
            this.uiCommandManager1.ContainerControl = this;
            this.uiCommandManager1.ContextMenus.AddRange(new UIContextMenu[] { this.uiContextMenu1 });
            this.uiCommandManager1.Id = new Guid("4058ab29-b891-4df5-8758-9fc75311fb2c");
            this.uiCommandManager1.LeftRebar = this.LeftRebar1;
            resources.ApplyResources(this.uiCommandManager1, "uiCommandManager1");
            this.uiCommandManager1.RightRebar = this.RightRebar1;
            this.uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.ShowCustomizeButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.TopRebar = this.TopRebar1;
            this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
            this.BottomRebar1.CommandManager = this.uiCommandManager1;
            resources.ApplyResources(this.BottomRebar1, "BottomRebar1");
            this.BottomRebar1.Name = "BottomRebar1";
            this.uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.Animation = DropDownAnimation.System;
            this.uiCommandBar1.CommandBarType = CommandBarType.Menu;
            this.uiCommandBar1.CommandManager = this.uiCommandManager1;
            this.uiCommandBar1.Commands.AddRange(new UICommand[] { this.menuFile1, this.menuSource1, this.menuMonitor1, this.menuLog1, this.menuItem1, this.menuEventLog1, this.menuTools1, this.menuHelp1, this.menuDebug1, this.cmdNewVersionAvailable1 });
            resources.ApplyResources(this.uiCommandBar1, "uiCommandBar1");
            this.uiCommandBar1.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
            this.uiCommandBar1.Name = "uiCommandBar1";
            this.uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.ShowCustomizeButton = Janus.Windows.UI.InheritableBoolean.False;
            resources.ApplyResources(this.menuFile1, "menuFile1");
            this.menuFile1.Name = "menuFile1";
            resources.ApplyResources(this.menuSource1, "menuSource1");
            this.menuSource1.Name = "menuSource1";
            resources.ApplyResources(this.menuMonitor1, "menuMonitor1");
            this.menuMonitor1.Name = "menuMonitor1";
            resources.ApplyResources(this.menuLog1, "menuLog1");
            this.menuLog1.Name = "menuLog1";
            resources.ApplyResources(this.menuItem1, "menuItem1");
            this.menuItem1.Name = "menuItem1";
            resources.ApplyResources(this.menuEventLog1, "menuEventLog1");
            this.menuEventLog1.Name = "menuEventLog1";
            resources.ApplyResources(this.menuTools1, "menuTools1");
            this.menuTools1.Name = "menuTools1";
            resources.ApplyResources(this.menuHelp1, "menuHelp1");
            this.menuHelp1.Name = "menuHelp1";
            resources.ApplyResources(this.menuDebug1, "menuDebug1");
            this.menuDebug1.Name = "menuDebug1";
            resources.ApplyResources(this.cmdNewVersionAvailable1, "cmdNewVersionAvailable1");
            this.cmdNewVersionAvailable1.Name = "cmdNewVersionAvailable1";
            this.uiCommandBar2.AllowCustomize = Janus.Windows.UI.InheritableBoolean.True;
            this.uiCommandBar2.CommandManager = this.uiCommandManager1;
            this.uiCommandBar2.Commands.AddRange(new UICommand[] { this.cmdBigCheckSource1, this.cmdBigCheckSources1, this.cmdBigCheckModifications1, this.cmdBigExplore1, this.Separator6, this.cmdBigUpdate1, this.cmdBigSourceCommit1, this.cmdBigRevert1, this.Separator8, this.cmdBigUpdateAllAvailable1, this.cmdBigUpdateAll1, this.Separator7, this.cmdBigOptions1, this.cmdBigSendFeedback1 });
            this.uiCommandBar2.FullRow = true;
            resources.ApplyResources(this.uiCommandBar2, "uiCommandBar2");
            this.uiCommandBar2.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
            this.uiCommandBar2.Name = "uiCommandBar2";
            this.uiCommandBar2.View = Janus.Windows.UI.CommandBars.View.LargeIcons;
            resources.ApplyResources(this.cmdBigCheckSource1, "cmdBigCheckSource1");
            this.cmdBigCheckSource1.Name = "cmdBigCheckSource1";
            resources.ApplyResources(this.cmdBigCheckSources1, "cmdBigCheckSources1");
            this.cmdBigCheckSources1.Name = "cmdBigCheckSources1";
            resources.ApplyResources(this.cmdBigCheckModifications1, "cmdBigCheckModifications1");
            this.cmdBigCheckModifications1.Name = "cmdBigCheckModifications1";
            resources.ApplyResources(this.cmdBigExplore1, "cmdBigExplore1");
            this.cmdBigExplore1.Name = "cmdBigExplore1";
            this.Separator6.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator6, "Separator6");
            this.Separator6.Name = "Separator6";
            resources.ApplyResources(this.cmdBigUpdate1, "cmdBigUpdate1");
            this.cmdBigUpdate1.Name = "cmdBigUpdate1";
            resources.ApplyResources(this.cmdBigSourceCommit1, "cmdBigSourceCommit1");
            this.cmdBigSourceCommit1.Name = "cmdBigSourceCommit1";
            resources.ApplyResources(this.cmdBigRevert1, "cmdBigRevert1");
            this.cmdBigRevert1.Name = "cmdBigRevert1";
            this.Separator8.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator8, "Separator8");
            this.Separator8.Name = "Separator8";
            resources.ApplyResources(this.cmdBigUpdateAllAvailable1, "cmdBigUpdateAllAvailable1");
            this.cmdBigUpdateAllAvailable1.Name = "cmdBigUpdateAllAvailable1";
            resources.ApplyResources(this.cmdBigUpdateAll1, "cmdBigUpdateAll1");
            this.cmdBigUpdateAll1.Name = "cmdBigUpdateAll1";
            this.Separator7.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator7, "Separator7");
            this.Separator7.Name = "Separator7";
            resources.ApplyResources(this.cmdBigOptions1, "cmdBigOptions1");
            this.cmdBigOptions1.Name = "cmdBigOptions1";
            resources.ApplyResources(this.cmdBigSendFeedback1, "cmdBigSendFeedback1");
            this.cmdBigSendFeedback1.Name = "cmdBigSendFeedback1";
            this.menuFile.Commands.AddRange(new UICommand[] { this.cmdNew1, this.cmdClose2 });
            resources.ApplyResources(this.menuFile, "menuFile");
            this.menuFile.Name = "menuFile";
            resources.ApplyResources(this.cmdNew1, "cmdNew1");
            this.cmdNew1.Name = "cmdNew1";
            resources.ApplyResources(this.cmdClose2, "cmdClose2");
            this.cmdClose2.Name = "cmdClose2";
            resources.ApplyResources(this.menuSource, "menuSource");
            this.menuSource.Name = "menuSource";
            resources.ApplyResources(this.menuMonitor, "menuMonitor");
            this.menuMonitor.Name = "menuMonitor";
            resources.ApplyResources(this.menuLog, "menuLog");
            this.menuLog.Name = "menuLog";
            resources.ApplyResources(this.menuItem, "menuItem");
            this.menuItem.Name = "menuItem";
            this.menuTools.Commands.AddRange(new UICommand[] { this.cmdOptions1, this.cmdTSVNSettings1 });
            resources.ApplyResources(this.menuTools, "menuTools");
            this.menuTools.Name = "menuTools";
            resources.ApplyResources(this.cmdOptions1, "cmdOptions1");
            this.cmdOptions1.Name = "cmdOptions1";
            resources.ApplyResources(this.cmdTSVNSettings1, "cmdTSVNSettings1");
            this.cmdTSVNSettings1.Name = "cmdTSVNSettings1";
            resources.ApplyResources(this.menuEventLog, "menuEventLog");
            this.menuEventLog.Name = "menuEventLog";
            resources.ApplyResources(this.cmdOptions, "cmdOptions");
            this.cmdOptions.Name = "cmdOptions";
            this.cmdOptions.Click += new CommandEventHandler(this.cmdOptions_Click);
            this.cmdEnableUpdates.CommandType = CommandType.ToggleButton;
            resources.ApplyResources(this.cmdEnableUpdates, "cmdEnableUpdates");
            this.cmdEnableUpdates.Name = "cmdEnableUpdates";
            this.cmdEnableUpdates.Click += new CommandEventHandler(this.cmdEnableUpdates_Click);
            resources.ApplyResources(this.cmdOpen, "cmdOpen");
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Click += new CommandEventHandler(this.cmdOpen_Click);
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Click += new CommandEventHandler(this.cmdClose_Click);
            resources.ApplyResources(this.cmdCheckAllSources, "cmdCheckAllSources");
            this.cmdCheckAllSources.Name = "cmdCheckAllSources";
            this.cmdCheckAllSources.Click += new CommandEventHandler(this.cmdCheckAllSources_Click);
            resources.ApplyResources(this.cmdSVNUpdateAll, "cmdSVNUpdateAll");
            this.cmdSVNUpdateAll.Name = "cmdSVNUpdateAll";
            this.cmdSVNUpdateAll.Click += new CommandEventHandler(this.cmdSVNUpdateAll_Click);
            resources.ApplyResources(this.cmdSVNUpdateAllAvailable, "cmdSVNUpdateAllAvailable");
            this.cmdSVNUpdateAllAvailable.Name = "cmdSVNUpdateAllAvailable";
            this.cmdSVNUpdateAllAvailable.Click += new CommandEventHandler(this.cmdSVNUpdateAllAvailable_Click);
            resources.ApplyResources(this.menuSVNUpdate, "menuSVNUpdate");
            this.menuSVNUpdate.Name = "menuSVNUpdate";
            resources.ApplyResources(this.menuSVNCommit, "menuSVNCommit");
            this.menuSVNCommit.Name = "menuSVNCommit";
            resources.ApplyResources(this.menuSVNRevert, "menuSVNRevert");
            this.menuSVNRevert.Name = "menuSVNRevert";
            resources.ApplyResources(this.menuCheckModifications, "menuCheckModifications");
            this.menuCheckModifications.Name = "menuCheckModifications";
            this.menuHelp.Commands.AddRange(new UICommand[] { this.cmdCheckNewVersion1, this.Separator5, this.cmdTSVNHelp1, this.cmdFeedback1, this.cmdAbout1 });
            resources.ApplyResources(this.menuHelp, "menuHelp");
            this.menuHelp.Name = "menuHelp";
            resources.ApplyResources(this.cmdCheckNewVersion1, "cmdCheckNewVersion1");
            this.cmdCheckNewVersion1.Name = "cmdCheckNewVersion1";
            this.Separator5.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator5, "Separator5");
            this.Separator5.Name = "Separator5";
            resources.ApplyResources(this.cmdTSVNHelp1, "cmdTSVNHelp1");
            this.cmdTSVNHelp1.Name = "cmdTSVNHelp1";
            resources.ApplyResources(this.cmdFeedback1, "cmdFeedback1");
            this.cmdFeedback1.Name = "cmdFeedback1";
            resources.ApplyResources(this.cmdAbout1, "cmdAbout1");
            this.cmdAbout1.Name = "cmdAbout1";
            resources.ApplyResources(this.cmdAbout, "cmdAbout");
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Click += new CommandEventHandler(this.cmdAbout_Click);
            resources.ApplyResources(this.cmdCheckNewVersion, "cmdCheckNewVersion");
            this.cmdCheckNewVersion.Name = "cmdCheckNewVersion";
            this.cmdCheckNewVersion.Click += new CommandEventHandler(this.cmdCheckNewVersion_Click);
            this.cmdNew.Commands.AddRange(new UICommand[] { this.cmdNewSource1, this.cmdNewMonitor1 });
            resources.ApplyResources(this.cmdNew, "cmdNew");
            this.cmdNew.Name = "cmdNew";
            resources.ApplyResources(this.cmdNewSource1, "cmdNewSource1");
            this.cmdNewSource1.Name = "cmdNewSource1";
            resources.ApplyResources(this.cmdNewMonitor1, "cmdNewMonitor1");
            this.cmdNewMonitor1.Name = "cmdNewMonitor1";
            resources.ApplyResources(this.cmdNewSource, "cmdNewSource");
            this.cmdNewSource.Name = "cmdNewSource";
            this.cmdNewSource.Click += new CommandEventHandler(this.cmdNewSource_Click);
            resources.ApplyResources(this.cmdNewMonitor, "cmdNewMonitor");
            this.cmdNewMonitor.Name = "cmdNewMonitor";
            this.cmdNewMonitor.Click += new CommandEventHandler(this.cmdNewMonitor_Click);
            this.cmdNewVersionAvailable.CommandStyle = CommandStyle.TextImage;
            resources.ApplyResources(this.cmdNewVersionAvailable, "cmdNewVersionAvailable");
            this.cmdNewVersionAvailable.Name = "cmdNewVersionAvailable";
            this.cmdNewVersionAvailable.Visible = Janus.Windows.UI.InheritableBoolean.False;
            this.cmdNewVersionAvailable.Click += new CommandEventHandler(this.cmdNewVersionAvailable_Click);
            resources.ApplyResources(this.cmdTSVNSettings, "cmdTSVNSettings");
            this.cmdTSVNSettings.Name = "cmdTSVNSettings";
            this.cmdTSVNSettings.Click += new CommandEventHandler(this.cmdTSVNSettings_Click);
            resources.ApplyResources(this.cmdTSVNHelp, "cmdTSVNHelp");
            this.cmdTSVNHelp.Name = "cmdTSVNHelp";
            this.cmdTSVNHelp.Click += new CommandEventHandler(this.cmdTSVNHelp_Click);
            resources.ApplyResources(this.cmdFeedback, "cmdFeedback");
            this.cmdFeedback.Name = "cmdFeedback";
            this.cmdFeedback.Click += new CommandEventHandler(this.cmdFeedback_Click);
            resources.ApplyResources(this.menuDebug, "menuDebug");
            this.menuDebug.Commands.AddRange(new UICommand[] { this.menuDialogs1, this.Separator9, this.cmdGenerateError1, this.cmdGenerateInvokeError1, this.menuTestNewVersion1, this.menuTestGridLayouts1 });
            this.menuDebug.Name = "menuDebug";
            this.menuDebug.Visible = Janus.Windows.UI.InheritableBoolean.False;
            resources.ApplyResources(this.menuDialogs1, "menuDialogs1");
            this.menuDialogs1.Name = "menuDialogs1";
            this.Separator9.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator9, "Separator9");
            this.Separator9.Name = "Separator9";
            resources.ApplyResources(this.cmdGenerateError1, "cmdGenerateError1");
            this.cmdGenerateError1.Name = "cmdGenerateError1";
            resources.ApplyResources(this.cmdGenerateInvokeError1, "cmdGenerateInvokeError1");
            this.cmdGenerateInvokeError1.Name = "cmdGenerateInvokeError1";
            resources.ApplyResources(this.menuTestNewVersion1, "menuTestNewVersion1");
            this.menuTestNewVersion1.Name = "menuTestNewVersion1";
            resources.ApplyResources(this.cmdGenerateError, "cmdGenerateError");
            this.cmdGenerateError.Name = "cmdGenerateError";
            this.cmdGenerateError.Click += new CommandEventHandler(this.cmdGenerateError_Click);
            resources.ApplyResources(this.cmdGenerateInvokeError, "cmdGenerateInvokeError");
            this.cmdGenerateInvokeError.Name = "cmdGenerateInvokeError";
            this.cmdGenerateInvokeError.Click += new CommandEventHandler(this.cmdGenerateInvokeError_Click);
            resources.ApplyResources(this.cmdTestNewVersion, "cmdTestNewVersion");
            this.cmdTestNewVersion.Name = "cmdTestNewVersion";
            this.cmdTestNewVersion.Click += new CommandEventHandler(this.cmdTestNewVersion_Click);
            resources.ApplyResources(this.txtTestNewVersion, "txtTestNewVersion");
            this.txtTestNewVersion.CommandType = CommandType.TextBoxCommand;
            this.txtTestNewVersion.ControlValue = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
            this.txtTestNewVersion.IsEditableControl = Janus.Windows.UI.InheritableBoolean.True;
            this.txtTestNewVersion.Name = "txtTestNewVersion";
            resources.ApplyResources(this.txtTestNewVersionFile, "txtTestNewVersionFile");
            this.txtTestNewVersionFile.CommandType = CommandType.TextBoxCommand;
            this.txtTestNewVersionFile.ControlValue = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
            this.txtTestNewVersionFile.IsEditableControl = Janus.Windows.UI.InheritableBoolean.True;
            this.txtTestNewVersionFile.Name = "txtTestNewVersionFile";
            resources.ApplyResources(this.menuTestNewVersion, "menuTestNewVersion");
            this.menuTestNewVersion.Commands.AddRange(new UICommand[] { this.txtTestNewVersion1, this.txtTestNewVersionFile1, this.cmdTestNewVersion1 });
            this.menuTestNewVersion.Name = "menuTestNewVersion";
            this.txtTestNewVersion1.ControlValue = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
            resources.ApplyResources(this.txtTestNewVersion1, "txtTestNewVersion1");
            this.txtTestNewVersion1.Name = "txtTestNewVersion1";
            this.txtTestNewVersionFile1.ControlValue = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
            resources.ApplyResources(this.txtTestNewVersionFile1, "txtTestNewVersionFile1");
            this.txtTestNewVersionFile1.Name = "txtTestNewVersionFile1";
            resources.ApplyResources(this.cmdTestNewVersion1, "cmdTestNewVersion1");
            this.cmdTestNewVersion1.Name = "cmdTestNewVersion1";
            resources.ApplyResources(this.cmdBigCheckSources, "cmdBigCheckSources");
            this.cmdBigCheckSources.Name = "cmdBigCheckSources";
            this.cmdBigCheckSources.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigCheckSources.Click += new CommandEventHandler(this.cmdBigCheckSources_Click);
            resources.ApplyResources(this.cmdBigCheckSource, "cmdBigCheckSource");
            this.cmdBigCheckSource.Name = "cmdBigCheckSource";
            this.cmdBigCheckSource.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigCheckSource.Click += new CommandEventHandler(this.cmdBigCheckSource_Click);
            resources.ApplyResources(this.cmdBigCheckModifications, "cmdBigCheckModifications");
            this.cmdBigCheckModifications.Name = "cmdBigCheckModifications";
            this.cmdBigCheckModifications.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigCheckModifications.Click += new CommandEventHandler(this.cmdBigCheckModifications_Click);
            resources.ApplyResources(this.cmdBigExplore, "cmdBigExplore");
            this.cmdBigExplore.Name = "cmdBigExplore";
            this.cmdBigExplore.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigExplore.Click += new CommandEventHandler(this.cmdBigExplore_Click);
            resources.ApplyResources(this.cmdBigUpdate, "cmdBigUpdate");
            this.cmdBigUpdate.Name = "cmdBigUpdate";
            this.cmdBigUpdate.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigUpdate.Click += new CommandEventHandler(this.cmdBigUpdate_Click);
            resources.ApplyResources(this.cmdBigUpdateAll, "cmdBigUpdateAll");
            this.cmdBigUpdateAll.Name = "cmdBigUpdateAll";
            this.cmdBigUpdateAll.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigUpdateAll.Click += new CommandEventHandler(this.cmdBigUpdateAll_Click);
            resources.ApplyResources(this.cmdBigUpdateAllAvailable, "cmdBigUpdateAllAvailable");
            this.cmdBigUpdateAllAvailable.Name = "cmdBigUpdateAllAvailable";
            this.cmdBigUpdateAllAvailable.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigUpdateAllAvailable.Click += new CommandEventHandler(this.cmdBigUpdateAllAvailable_Click);
            resources.ApplyResources(this.cmdBigOptions, "cmdBigOptions");
            this.cmdBigOptions.Name = "cmdBigOptions";
            this.cmdBigOptions.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigOptions.Click += new CommandEventHandler(this.cmdBigOptions_Click);
            resources.ApplyResources(this.cmdBigSendFeedback, "cmdBigSendFeedback");
            this.cmdBigSendFeedback.Name = "cmdBigSendFeedback";
            this.cmdBigSendFeedback.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigSendFeedback.Click += new CommandEventHandler(this.cmdBigSendFeedback_Click);
            resources.ApplyResources(this.cmdBigSourceCommit, "cmdBigSourceCommit");
            this.cmdBigSourceCommit.Name = "cmdBigSourceCommit";
            this.cmdBigSourceCommit.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigSourceCommit.Click += new CommandEventHandler(this.cmdBigSourceCommit_Click);
            resources.ApplyResources(this.cmdBigRevert, "cmdBigRevert");
            this.cmdBigRevert.Name = "cmdBigRevert";
            this.cmdBigRevert.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
            this.cmdBigRevert.Click += new CommandEventHandler(this.cmdBigRevert_Click);
            resources.ApplyResources(this.cmdTestDialogNewVersion, "cmdTestDialogNewVersion");
            this.cmdTestDialogNewVersion.Name = "cmdTestDialogNewVersion";
            this.cmdTestDialogNewVersion.Click += new CommandEventHandler(this.cmdTestDialogNewVersion_Click);
            resources.ApplyResources(this.menuDialogs, "menuDialogs");
            this.menuDialogs.Commands.AddRange(new UICommand[] { this.cmdTestDialogNewVersion1 });
            this.menuDialogs.Name = "menuDialogs";
            resources.ApplyResources(this.cmdTestDialogNewVersion1, "cmdTestDialogNewVersion1");
            this.cmdTestDialogNewVersion1.Name = "cmdTestDialogNewVersion1";
            this.uiContextMenu1.CommandManager = this.uiCommandManager1;
            this.uiContextMenu1.Commands.AddRange(new UICommand[] { this.cmdEnableUpdates1, this.Separator1, this.menuCheckModifications1, this.menuSVNUpdate1, this.cmdSVNUpdateAllUnread1, this.cmdSVNUpdateAll1, this.menuSVNCommit1, this.menuSVNRevert1, this.Separator4, this.cmdCheckAllSources1, this.Separator3, this.cmdOptions2, this.cmdOpen1, this.Separator2, this.cmdFeedback2, this.cmdClose1 });
            resources.ApplyResources(this.uiContextMenu1, "uiContextMenu1");
            this.uiContextMenu1.Popup += new EventHandler(this.uiContextMenu1_Popup);
            resources.ApplyResources(this.cmdEnableUpdates1, "cmdEnableUpdates1");
            this.cmdEnableUpdates1.Name = "cmdEnableUpdates1";
            this.Separator1.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator1, "Separator1");
            this.Separator1.Name = "Separator1";
            resources.ApplyResources(this.menuCheckModifications1, "menuCheckModifications1");
            this.menuCheckModifications1.Name = "menuCheckModifications1";
            resources.ApplyResources(this.menuSVNUpdate1, "menuSVNUpdate1");
            this.menuSVNUpdate1.Name = "menuSVNUpdate1";
            resources.ApplyResources(this.cmdSVNUpdateAllUnread1, "cmdSVNUpdateAllUnread1");
            this.cmdSVNUpdateAllUnread1.Name = "cmdSVNUpdateAllUnread1";
            resources.ApplyResources(this.cmdSVNUpdateAll1, "cmdSVNUpdateAll1");
            this.cmdSVNUpdateAll1.Name = "cmdSVNUpdateAll1";
            resources.ApplyResources(this.menuSVNCommit1, "menuSVNCommit1");
            this.menuSVNCommit1.Name = "menuSVNCommit1";
            resources.ApplyResources(this.menuSVNRevert1, "menuSVNRevert1");
            this.menuSVNRevert1.Name = "menuSVNRevert1";
            this.Separator4.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator4, "Separator4");
            this.Separator4.Name = "Separator4";
            resources.ApplyResources(this.cmdCheckAllSources1, "cmdCheckAllSources1");
            this.cmdCheckAllSources1.Name = "cmdCheckAllSources1";
            this.Separator3.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator3, "Separator3");
            this.Separator3.Name = "Separator3";
            resources.ApplyResources(this.cmdOptions2, "cmdOptions2");
            this.cmdOptions2.Name = "cmdOptions2";
            this.cmdOpen1.DefaultItem = Janus.Windows.UI.InheritableBoolean.True;
            resources.ApplyResources(this.cmdOpen1, "cmdOpen1");
            this.cmdOpen1.Name = "cmdOpen1";
            this.Separator2.CommandType = CommandType.Separator;
            resources.ApplyResources(this.Separator2, "Separator2");
            this.Separator2.Name = "Separator2";
            resources.ApplyResources(this.cmdFeedback2, "cmdFeedback2");
            this.cmdFeedback2.Name = "cmdFeedback2";
            resources.ApplyResources(this.cmdClose1, "cmdClose1");
            this.cmdClose1.Name = "cmdClose1";
            this.LeftRebar1.CommandManager = this.uiCommandManager1;
            resources.ApplyResources(this.LeftRebar1, "LeftRebar1");
            this.LeftRebar1.Name = "LeftRebar1";
            this.RightRebar1.CommandManager = this.uiCommandManager1;
            resources.ApplyResources(this.RightRebar1, "RightRebar1");
            this.RightRebar1.Name = "RightRebar1";
            this.TopRebar1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1, this.uiCommandBar2 });
            this.TopRebar1.CommandManager = this.uiCommandManager1;
            this.TopRebar1.Controls.Add(this.uiCommandBar1);
            this.TopRebar1.Controls.Add(this.uiCommandBar2);
            resources.ApplyResources(this.TopRebar1, "TopRebar1");
            this.TopRebar1.Name = "TopRebar1";
            this.menuTestGridLayouts.Commands.AddRange(new UICommand[] { this.cmdTestSetLogEntriesGridLayout1 });
            resources.ApplyResources(this.menuTestGridLayouts, "menuTestGridLayouts");
            this.menuTestGridLayouts.Name = "menuTestGridLayouts";
            resources.ApplyResources(this.cmdTestSetLogEntriesGridLayout, "cmdTestSetLogEntriesGridLayout");
            this.cmdTestSetLogEntriesGridLayout.Name = "cmdTestSetLogEntriesGridLayout";
            this.cmdTestSetLogEntriesGridLayout.Click += new CommandEventHandler(this.cmdTestSetLogEntriesGridLayout_Click);
            resources.ApplyResources(this.cmdTestSetLogEntriesGridLayout1, "cmdTestSetLogEntriesGridLayout1");
            this.cmdTestSetLogEntriesGridLayout1.Name = "cmdTestSetLogEntriesGridLayout1";
            resources.ApplyResources(this.menuTestGridLayouts1, "menuTestGridLayouts1");
            this.menuTestGridLayouts1.Name = "menuTestGridLayouts1";
            resources.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.uiPanelLog);
            base.Controls.Add(this.uiPanelUpdatesGridContainer);
            base.Controls.Add(this.uiPanelPaths);
            base.Controls.Add(this.uiPanelLeft);
            base.Controls.Add(this.uiStatusBar1);
            base.Controls.Add(this.TopRebar1);
            base.KeyPreview = true;
            base.Name = "MainForm";
            base.KeyDown += new KeyEventHandler(this.MainForm_KeyDown);
            ((ISupportInitialize) this.uiPanelManager1).EndInit();
            ((ISupportInitialize) this.uiPanelEventLog).EndInit();
            this.uiPanelEventLog.ResumeLayout(false);
            this.uiPanelEventLogCaptionContainer.ResumeLayout(false);
            this.uiPanelEventLogContainer.ResumeLayout(false);
            ((ISupportInitialize) this.uiPanelLeft).EndInit();
            this.uiPanelLeft.ResumeLayout(false);
            ((ISupportInitialize) this.uiPanelSources).EndInit();
            this.uiPanelSources.ResumeLayout(false);
            this.uiPanelSourcesCaptionContainer.ResumeLayout(false);
            this.sourcesPanel1.EndInit();
            this.uiPanelSourcesContainer.ResumeLayout(false);
            ((ISupportInitialize) this.uiPanelMonitors).EndInit();
            this.uiPanelMonitors.ResumeLayout(false);
            this.uiPanelMonitorsCaptionContainer.ResumeLayout(false);
            this.uiPanelMonitorsContainer.ResumeLayout(false);
            ((ISupportInitialize) this.uiPanelPaths).EndInit();
            this.uiPanelPaths.ResumeLayout(false);
            this.uiPanelPathsCaptionContainer.ResumeLayout(false);
            this.pathsPanel1.EndInit();
            this.logEntriesPanel1.EndInit();
            this.uiPanelPathsContainer.ResumeLayout(false);
            ((ISupportInitialize) this.uiPanelUpdatesGridContainer).EndInit();
            this.uiPanelUpdatesGridContainer.ResumeLayout(false);
            this.uiPanelUpdatesGridContainerContainer.ResumeLayout(false);
            ((ISupportInitialize) this.uiPanelLog).EndInit();
            this.uiPanelLog.ResumeLayout(false);
            this.uiPanelLogCaptionContainer.ResumeLayout(false);
            this.uiPanelLogContainer.ResumeLayout(false);
            this.uiStatusBar1.ResumeLayout(false);
            ((ISupportInitialize) this.uiCommandManager1).EndInit();
            ((ISupportInitialize) this.BottomRebar1).EndInit();
            ((ISupportInitialize) this.uiCommandBar1).EndInit();
            ((ISupportInitialize) this.uiCommandBar2).EndInit();
            ((ISupportInitialize) this.uiContextMenu1).EndInit();
            ((ISupportInitialize) this.LeftRebar1).EndInit();
            ((ISupportInitialize) this.RightRebar1).EndInit();
            ((ISupportInitialize) this.TopRebar1).EndInit();
            this.TopRebar1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InstallKeyHooks()
        {
            KeyboardHookHelper.Install();
        }

        private void Instance_UpgradeAvailable(object sender, VersionChecker.VersionEventArgs e)
        {
            this.ShowNewVersionMessage(e);
        }

        private void LoadUISettings()
        {
            Logger.Log.Info("Reading Application State for MainForm...");
            this.checkGroupByBox.Checked = ApplicationSettingsManager.Settings.LogGroupByBox;
            base.Height = ApplicationSettingsManager.Settings.UIMainFormHeight;
            base.Location = new Point(ApplicationSettingsManager.Settings.UIMainFormLocationX, ApplicationSettingsManager.Settings.UIMainFormLocationY);
            base.Width = ApplicationSettingsManager.Settings.UIMainFormWidth;
            base.WindowState = (FormWindowState) Enum.Parse(typeof(FormWindowState), ApplicationSettingsManager.Settings.UIMainFormWindowState);
            this.uiPanelEventLog.AutoHide = ApplicationSettingsManager.Settings.UIPanelEventLogAutoHide;
            this.uiPanelEventLog.Height = ApplicationSettingsManager.Settings.UIPanelEventLogHeight;
            this.uiPanelLeft.Width = ApplicationSettingsManager.Settings.UIPanelLeftWidth;
            this.uiPanelMonitors.AutoHide = ApplicationSettingsManager.Settings.UIPanelMonitiorsAutoHide;
            this.uiPanelMonitors.Height = ApplicationSettingsManager.Settings.UIPanelMonitiorsHeight;
            this.uiPanelPaths.AutoHide = ApplicationSettingsManager.Settings.UIPanelPathsAutoHide;
            this.uiPanelPaths.Height = ApplicationSettingsManager.Settings.UIPanelPathsHeight;
            this.uiPanelSources.AutoHide = ApplicationSettingsManager.Settings.UIPanelSourcesAutoHide;
            Logger.Log.Info("Done reading Application State for MainForm...");
        }

        private void logEntriesPanel1_SelectionChanged(object sender, EventArgs e)
        {
            this.SetLogEntryName();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool log = true;
            switch (e.KeyCode)
            {
                case Keys.F3:
                    this.Search();
                    break;

                case Keys.F5:
                    this.UpdateSource();
                    break;

                case Keys.F8:
                    this.FocusEventLog();
                    break;

                case Keys.F9:
                    this.UpdateAllSources();
                    break;

                case Keys.F:
                    if (e.Control)
                    {
                        this.Search();
                    }
                    break;

                default:
                    log = false;
                    break;
            }
            if (log)
            {
                Logger.LogUserAction("key=" + e.KeyCode);
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            base.SizeChanged -= new EventHandler(this.MainForm_SizeChanged);
            this.SetGridLayouts();
        }

        private void MergeCommands()
        {
            foreach (UICommand cmd in this.SourcesPanel.UIContextMenu.Commands)
            {
                this.menuSource.Commands.Add(cmd);
            }
            foreach (UICommand cmd in this.MonitorsPanel.UIContextMenu.Commands)
            {
                this.menuMonitor.Commands.Add(cmd);
            }
            foreach (UICommand cmd in this.LogEntriesPanel.UIContextMenu.Commands)
            {
                this.menuLog.Commands.Add(cmd);
            }
            foreach (UICommand cmd in this.PathsPanel.UIContextMenu.Commands)
            {
                this.menuItem.Commands.Add(cmd);
            }
            foreach (UICommand cmd in this.EventLogPanel.UIContextMenu.Commands)
            {
                this.menuEventLog.Commands.Add(cmd);
            }
        }

        private void NewMonitor()
        {
            this.MonitorsPanel.CreateNew();
        }

        private void NewSource()
        {
            this.SourcesPanel.CreateNew();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Logger.LogUserAction();
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    this.uiContextMenu1.Show();
                }
                catch (InvalidOperationException)
                {
                }
                catch (Win32Exception)
                {
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                this.ShowOrHideForm();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Logger.LogUserAction();
            this.RestoreForm();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.endSessionPending)
            {
                e.Cancel = false;
                SVNMonitor.EventLog.LogInfo(Strings.WindowsIsShuttingDown, this);
            }
            else
            {
                if (this.realClose || !ApplicationSettingsManager.Settings.MinimizeWhenClosing)
                {
                    SVNMonitor.EventLog.LogSystem(Strings.ApplicationClosing, this);
                    Logger.Log.Debug("Saving UI Settings");
                    this.SaveUISettings();
                    Logger.Log.Debug("Hiding the tray icon.");
                    this.Notifier.HideTrayIcon();
                    Logger.Log.Debug("Hiding the main window.");
                    this.Hide();
                    Logger.Log.Debug("Waiting for the updater to finish.");
                    WaitForUpdaterToFinish();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    base.WindowState = FormWindowState.Minimized;
                }
                base.OnClosing(e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!base.DesignMode)
            {
                this.RegisterEvents();
                this.MergeCommands();
                this.Notifier.SetTrayIcon();
                this.StartupNotification();
                this.DeleteObsoleteFiles();
                this.LoadUISettings();
                this.CheckStartMinimized();
                Updater.Instance.UpdatesGrid = this.UpdatesGrid;
                ConditionSerializer.Grid = this.UpdatesGrid;
                this.SetPanelsEntities();
                this.RegisterMonitorSettingsEvents();
                VersionChecker.Start();
                Updater.Start();
                this.InstallKeyHooks();
                base.SizeChanged += new EventHandler(this.MainForm_SizeChanged);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!base.DesignMode)
            {
                if (base.WindowState == FormWindowState.Minimized)
                {
                    if (ApplicationSettingsManager.Settings.HideWhenMinimized)
                    {
                        this.Notifier.SetTrayIconVisible(true);
                        this.Hide();
                    }
                }
                else
                {
                    base.Opacity = 100.0;
                    this.lastWindowState = base.WindowState;
                }
            }
        }

        private void OpenOptionsDialog()
        {
            SettingsDialog.ShowInstanceDialog();
        }

        private void PopupTrayContextMenu()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.PopupTrayContextMenu));
            }
            else
            {
                this.ReadSettings();
                if (!Updater.Instance.Enabled)
                {
                    bool tempAnotherLocal0;
                    bool tempAnotherLocal1;
                    bool tempAnotherLocal2;
                    bool tempAnotherLocal3;
                    bool tempAnotherLocal4;
                    this.CanCheckUpdates = tempAnotherLocal0 = false;
                    this.CanRevert = tempAnotherLocal1 = tempAnotherLocal0;
                    this.CanCommit = tempAnotherLocal2 = tempAnotherLocal1;
                    this.CanUpdate = tempAnotherLocal3 = tempAnotherLocal2;
                    this.CanCheckModifications = tempAnotherLocal4 = tempAnotherLocal3;
                    this.CanSVNUpdateAll = this.CanSVNUpdateAllAvailable = tempAnotherLocal4;
                }
                else
                {
                    this.CreateSVNCheckModificationsSubMenu();
                    this.CreateSVNCommitSubMenu();
                    this.CreateSVNRevertSubMenu();
                    this.CreateSVNUpdateSubMenu();
                    this.EnableCommands();
                }
            }
        }

        private void PostUpgrade()
        {
            string message = Strings.VersionUpgradedNotification_FORMAT.FormatWith(new object[] { FileSystemHelper.CurrentVersion });
            SVNMonitor.EventLog.LogSystem(message);
            this.Notifier.ShowBalloonTip(0xea60, Strings.SVNMonitorCaption, message, ToolTipIcon.Info);
            this.DeleteUpgradeDirectoryAsync();
            SharpRegion.TrySendUpgradeInfo(ApplicationSettingsManager.Settings.InstanceID);
        }

        private void ReadSettings()
        {
            this.cmdEnableUpdates.Checked = ApplicationSettingsManager.Settings.EnableUpdates.ToInheritableBoolean();
        }

        public void RealClose()
        {
            this.realClose = true;
            base.Close();
        }

        private void RegisterEvents()
        {
            Status.StatusChanged += new EventHandler(this.Status_StatusChanged);
            VersionChecker.Instance.NewVersionAvailable += new EventHandler<VersionChecker.VersionEventArgs>(this.versionChecker_NewVersionAvailable);
            VersionChecker.Instance.NoNewVersionAvailable += new EventHandler(this.versionChecker_NoNewVersionAvailable);
            VersionChecker.Instance.UpgradeAvailable += new EventHandler<VersionChecker.VersionEventArgs>(this.Instance_UpgradeAvailable);
            ApplicationSettingsManager.SavedSettings += (s, ea) => this.InstallKeyHooks();
        }

        private void RegisterMonitorSettingsEvents()
        {
            MonitorSettings settings = MonitorSettings.Instance;
            settings.SourcesChanged += (s, ea) => this.SetPanelsEntities();
            settings.MonitorsChanged += (s, ea) => this.SetPanelsEntities();
        }

        public void ReportError(ErrorReportFeedback report)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new Action<ErrorReportFeedback>(this.ReportError), new object[] { report });
            }
            else
            {
                ErrorReportDialog.Report(report);
            }
        }

        public void RestoreForm()
        {
            if (Form.ActiveForm != this)
            {
                base.Show();
                base.WindowState = this.lastWindowState;
                base.Activate();
            }
        }

        private void SaveUISettings()
        {
            Logger.Log.Info("Writing Application State for MainForm...");
            ApplicationSettingsManager.Settings.UIMainFormHeight = base.Height;
            ApplicationSettingsManager.Settings.UIMainFormLocationX = base.Location.X;
            ApplicationSettingsManager.Settings.UIMainFormLocationY = base.Location.Y;
            ApplicationSettingsManager.Settings.UIMainFormWidth = base.Width;
            ApplicationSettingsManager.Settings.UIMainFormWindowState = base.WindowState.ToString();
            ApplicationSettingsManager.Settings.UIPanelEventLogAutoHide = this.uiPanelEventLog.AutoHide;
            ApplicationSettingsManager.Settings.UIPanelEventLogHeight = this.uiPanelEventLog.Height;
            ApplicationSettingsManager.Settings.UIPanelLeftWidth = this.uiPanelLeft.Width;
            ApplicationSettingsManager.Settings.UIPanelMonitiorsAutoHide = this.uiPanelMonitors.AutoHide;
            ApplicationSettingsManager.Settings.UIPanelMonitiorsHeight = this.uiPanelMonitors.Height;
            ApplicationSettingsManager.Settings.UIPanelPathsAutoHide = this.uiPanelPaths.AutoHide;
            ApplicationSettingsManager.Settings.UIPanelPathsHeight = this.uiPanelPaths.Height;
            ApplicationSettingsManager.Settings.UIPanelSourcesAutoHide = this.uiPanelSources.AutoHide;
            ApplicationSettingsManager.Settings.UILogEntriesGridLayout = this.LogEntriesPanel.GetGridLayout();
            ApplicationSettingsManager.Settings.UIPathsGridLayout = this.PathsPanel.GetGridLayout();
            ApplicationSettingsManager.Settings.UIEventLogGridLayout = this.EventLogPanel.GetGridLayout();
            ApplicationSettingsManager.SaveSettings();
            Logger.Log.Info("Done writing Application State for MainForm.");
        }

        private void Search()
        {
            this.svnLogEntrySearchTextBox1.Focus();
        }

        public void SelectSource(Source source)
        {
            formInstance.SourcesPanel.SelectSource(source);
        }

        private void SendFeedback()
        {
            FeedbackDialog.ShowFeedbackDialog();
        }

        private void SetAnimation()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.SetAnimation));
            }
            else
            {
                this.AnimationProgressBar.Visible = Status.Updating;
                this.AnimationProgressBar.Text = Strings.StatusAnimationTitle_FORMAT.FormatWith(new object[] { Status.UpdatingSourcesString });
            }
        }

        private void SetGridLayouts()
        {
            this.LogEntriesPanel.SetGridLayout();
            this.PathsPanel.SetGridLayout();
            this.EventLogPanel.SetGridLayout();
        }

        private void SetGroupByBox()
        {
            ApplicationSettingsManager.Settings.LogGroupByBox = this.checkGroupByBox.Checked;
            ApplicationSettingsManager.SaveSettings();
            this.SetLogGroupByBoxImage();
        }

        private void SetLogEntryName()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.SetLogEntryName));
            }
            else
            {
                SVNLogEntry logEntry = this.LogEntriesPanel.SelectedItem;
                if (logEntry == null)
                {
                    this.uiPanelPaths.ResetText();
                }
                else
                {
                    this.uiPanelPaths.Text = string.Format("{0} {1} [{2}/{3}]", new object[] { Strings.LogItemsPanelTitle, logEntry.SourceName, logEntry.Revision, logEntry.Author });
                }
                this.uiPanelPathsCaptionContainer.Invalidate();
            }
        }

        private void SetLogGroupByBoxImage()
        {
            this.checkGroupByBox.Image = this.checkGroupByBox.Checked ? Images.elements1 : Images.elements1_unchecked;
        }

        private void SetLogPanelText()
        {
            Source source = this.SourcesPanel.SelectedItem;
            if (source == null)
            {
                this.uiPanelLog.Text = Strings.PanelCaptionLog;
            }
            else
            {
                this.uiPanelLog.Text = Strings.PanelCaptionLogAndName_FORMAT.FormatWith(new object[] { source.Name });
            }
            this.uiPanelLog.Refresh();
        }

        private void SetPanelsEntities()
        {
            MonitorSettings settings = MonitorSettings.Instance;
            this.SourcesPanel.Entities = settings.Sources;
            this.MonitorsPanel.Entities = settings.Monitors;
        }

        private void ShowAbout()
        {
            AboutDialog.ShowStaticDialog(this);
        }

        public DialogResult ShowErrorMessage(string text, string caption)
        {
            return this.ShowMessage(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static void ShowInstance()
        {
            if (formInstance != null)
            {
                formInstance.Show();
                formInstance.BringToFront();
                formInstance.WindowState = FormWindowState.Maximized;
            }
        }

        public static void ShowInstance(Source source)
        {
            ShowInstance();
            FormInstance.SelectSource(source);
        }

        public DialogResult ShowMessage(string text, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            if (base.InvokeRequired)
            {
                return (DialogResult) base.Invoke(new ShowMessageInvoker(this.ShowMessage), new object[] { text, caption, messageBoxButtons, messageBoxIcon });
            }
            return MessageBox.Show(this, text, caption, messageBoxButtons, messageBoxIcon);
        }

        private void ShowNewVersionBalloon(VersionChecker.VersionEventArgs e)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new Action<VersionChecker.VersionEventArgs>(this.ShowNewVersionBalloon), new object[] { e });
            }
            else
            {
                EventHandler handler = (s, ea) => this.ShowNewVersionDialog(e);
                this.Notifier.ShowBalloonTip(0xea60, Strings.SVNMonitorCaption, Strings.ANewVersionIsAvailable, ToolTipIcon.Info, handler);
            }
        }

        private void ShowNewVersionCommand(VersionChecker.VersionEventArgs e)
        {
            this.cmdNewVersionAvailable.Visible = Janus.Windows.UI.InheritableBoolean.True;
            this.cmdNewVersionAvailable.Tag = e;
        }

        private void ShowNewVersionDialog(VersionChecker.VersionEventArgs e)
        {
            Version currentVersion = e.CurrentVersion;
            Version latestVersion = e.LatestVersion;
            string message = e.Message;
            NewVersionDialog.ShowNewVersionDialog(currentVersion, latestVersion, message, e.VersionFolder);
        }

        private void ShowNewVersionMessage(VersionChecker.VersionEventArgs e)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new Action<VersionChecker.VersionEventArgs>(this.ShowNewVersionMessage), new object[] { e });
            }
            else
            {
                SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.System, Strings.ANewVersionIsAvailableWithVersion_FORMAT.FormatWith(new object[] { e.LatestVersion }), VersionChecker.Instance);
                this.ShowNewVersionCommand(e);
                if (base.Visible)
                {
                    this.ShowNewVersionDialog(e);
                }
                else
                {
                    this.ShowNewVersionBalloon(e);
                }
            }
        }

        private void ShowNoNewVersionMessage()
        {
            string message = "No newer version is available right now";
            SVNMonitor.EventLog.LogInfo(message, this);
            this.ShowMessage("No newer version is available right now", "SVN-Monitor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        internal void ShowOrHideForm()
        {
            if (base.Visible)
            {
                this.Hide();
            }
            else
            {
                this.RestoreForm();
            }
        }

        private static void Shutdown()
        {
            Logger.Log.Info("Finished.");
            Application.Exit();
        }

        private void SourceCheckModifications(Source source)
        {
            source.CheckModifications();
        }

        private void SourceRevert()
        {
            this.SourcesPanel.SVNRevert();
        }

        private void sourcesPanel1_SelectionChanged(object sender, EventArgs e)
        {
            this.SetLogPanelText();
            this.EnableCommands();
        }

        private void SourceSVNCommit(Source source)
        {
            source.SVNCommit();
        }

        private void SourceSVNRevert(Source source)
        {
            source.SVNRevert();
        }

        private void SourceSVNUpdate(Source source)
        {
            DialogResult result = UpdateHeadPromptDialog.Prompt();
            Logger.Log.InfoFormat("Update Source: User clicked {0}", result);
            if (result == DialogResult.Yes)
            {
                source.SVNUpdate();
            }
        }

        private void StartupNotification()
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionInfo.UpgradedFrom))
                {
                    this.PostUpgrade();
                }
                else if (this.ShowFirstRunNotification)
                {
                    this.Notifier.ShowBalloonTip(0xea60, Strings.SVNMonitorCaption, Strings.HereItIs, ToolTipIcon.Info);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }

        private void Status_StatusChanged(object sender, EventArgs e)
        {
            this.Notifier.SetTrayIcon();
            this.EnableCommands();
            this.SetAnimation();
        }

        private void SVNCommitSelectedSource()
        {
            this.SourcesPanel.SVNCommit();
        }

        private void SVNUpdateAllAvailableSources()
        {
            this.SVNUpdateAllSources(true);
        }

        private void SVNUpdateAllSources()
        {
            this.SVNUpdateAllSources(false);
        }

        private void SVNUpdateAllSources(bool ignoreUpToDate)
        {
            Source.SVNUpdateAll(ignoreUpToDate);
        }

        private void SVNUpdateSelectedSource()
        {
            this.SourcesPanel.SVNUpdate();
        }

        [Conditional("DEBUG")]
        private void TestNewVersionUpgrade(string version, string upgradeFile)
        {
        }

        private void ToggleEnableUpdates()
        {
            ApplicationSettingsManager.Settings.EnableUpdates = this.cmdEnableUpdates.Checked == Janus.Windows.UI.InheritableBoolean.True;
            ApplicationSettingsManager.SaveSettings();
        }

        private static void TriggerShutdownTimeoutTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            Logger.Log.Info("Waiting 20 seconds for current SVN activity to end before aborting...");
            timer.Interval = 20000.0;
            timer.AutoReset = false;
            timer.Elapsed += delegate {
                SVNMonitor.Helpers.ThreadHelper.SetThreadName("KILL");
                Logger.Log.Info("20 seconds elapsed. Aborting!");
                Shutdown();
            };
            timer.Start();
        }

        private void uiContextMenu1_Popup(object sender, EventArgs e)
        {
            this.PopupTrayContextMenu();
        }

        private void UpdateAllSources()
        {
            Updater.Instance.QueueUpdates();
        }

        private void UpdateSource()
        {
            Source source = this.SourcesPanel.SelectedItem;
            if (SourceHelper.CanCheckForUpdates(source))
            {
                Updater.Instance.QueueUpdate(source, true);
            }
        }

        private void versionChecker_NewVersionAvailable(object sender, VersionChecker.VersionEventArgs e)
        {
            this.ShowNewVersionMessage(e);
        }

        private void versionChecker_NoNewVersionAvailable(object sender, EventArgs e)
        {
            this.ShowNoNewVersionMessage();
        }

        private static void WaitForUpdaterToFinish()
        {
            if (!Status.Updating)
            {
                Application.Exit();
            }
            else
            {
                Logger.Log.Info("Waiting for the updater to finish...");
                Status.SetClosing(delegate {
                    Status.SetClosing(null);
                    Shutdown();
                });
                TriggerShutdownTimeoutTimer();
            }
        }

        [DebuggerNonUserCode]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x11)
            {
                this.endSessionPending = true;
            }
            base.WndProc(ref m);
        }

        private SVNMonitor.View.Controls.AnimationProgressBar AnimationProgressBar
        {
            get
            {
                return this.animationProgressBar1;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanBigCheckModifications
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigCheckModifications.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigCheckModifications.Enabled = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanBigCheckSource
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigCheckSource.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigCheckSource.Enabled = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanBigCheckSources
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigCheckSources.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigCheckSources.Enabled = value.ToInheritableBoolean();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanBigCommit
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigSourceCommit.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigSourceCommit.Enabled = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanBigExplore
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigExplore.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigExplore.Enabled = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanBigOptions
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigOptions.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigOptions.Enabled = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanBigRevert
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigRevert.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigRevert.Enabled = value.ToInheritableBoolean();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanBigSendFeedback
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigSendFeedback.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigSendFeedback.Enabled = value.ToInheritableBoolean();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanBigUpdate
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigUpdate.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigUpdate.Enabled = value.ToInheritableBoolean();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanBigUpdateAll
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigUpdateAll.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigUpdateAll.Enabled = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanBigUpdateAllAvailable
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdBigUpdateAllAvailable.Enabled == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdBigUpdateAllAvailable.Enabled = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanCheckModifications
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.menuCheckModifications.Visible == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.menuCheckModifications.Visible = value.ToInheritableBoolean();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanCheckUpdates
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdCheckAllSources.Visible == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdCheckAllSources.Visible = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanCommit
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.menuSVNCommit.Visible == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.menuSVNCommit.Visible = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanRevert
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.menuSVNRevert.Visible == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.menuSVNRevert.Visible = value.ToInheritableBoolean();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanSVNUpdateAll
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdSVNUpdateAll.Visible == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdSVNUpdateAll.Visible = value.ToInheritableBoolean();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool CanSVNUpdateAllAvailable
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.cmdSVNUpdateAllAvailable.Visible == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.cmdSVNUpdateAllAvailable.Visible = value.ToInheritableBoolean();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool CanUpdate
        {
            [DebuggerNonUserCode]
            get
            {
                return (this.menuSVNUpdate.Visible == Janus.Windows.UI.InheritableBoolean.True);
            }
            [DebuggerNonUserCode]
            set
            {
                this.menuSVNUpdate.Visible = value.ToInheritableBoolean();
            }
        }

        [Browsable(false)]
        private CommandImageProvider CheckModificationsImageProvider
        {
            get
            {
                return delegate (Source source) {
                    if (source.ModifiedCount > 0)
                    {
                        return Images.svn_modifications_available;
                    }
                    return Images.svn_modifications;
                };
            }
        }

        private SVNMonitor.View.Panels.EventLogPanel EventLogPanel
        {
            get
            {
                return this.eventLogPanel1;
            }
        }

        public static MainForm FormInstance
        {
            [DebuggerNonUserCode]
            get
            {
                return formInstance;
            }
        }

        private SVNMonitor.View.Panels.LogEntriesPanel LogEntriesPanel
        {
            get
            {
                return this.logEntriesPanel1;
            }
        }

        [Browsable(false)]
        private CommandSuffixProvider ModifiedCountSuffixProvider
        {
            get
            {
                return delegate (Source source) {
                    if (source.ModifiedCount == 0)
                    {
                        return string.Empty;
                    }
                    return string.Format("({0})", source.ModifiedCountString);
                };
            }
        }

        [Browsable(false)]
        private CommandSuffixProvider ModifiedOrUnversionedCountSuffixProvider
        {
            get
            {
                return delegate (Source source) {
                    int count = source.ModifiedCount;
                    int unversionedCount = source.UnversionedCount;
                    if (count == 0)
                    {
                        if (unversionedCount > 0)
                        {
                            return string.Format("({0})", source.UnversionedCountString);
                        }
                        return string.Empty;
                    }
                    string unversionedString = string.Empty;
                    if (unversionedCount > 0)
                    {
                        unversionedString = string.Format(" ({0})", source.UnversionedCountString);
                    }
                    string conflictsString = string.Empty;
                    if (source.PossibleConflictedFilePathsCount > 0)
                    {
                        conflictsString = string.Format(" ({0})", source.PossibleConflictedFilePathsCountString);
                    }
                    return string.Format("({0}{1}{2})", source.ModifiedCountString, unversionedString, conflictsString);
                };
            }
        }

        private SVNMonitor.View.Panels.MonitorsPanel MonitorsPanel
        {
            get
            {
                return this.monitorsPanel1;
            }
        }

        private MainNotifier Notifier { get; set; }

        private SVNMonitor.View.Panels.PathsPanel PathsPanel
        {
            get
            {
                return this.pathsPanel1;
            }
        }

        internal bool ShowFirstRunNotification { get; set; }

        private SVNMonitor.View.Panels.SourcesPanel SourcesPanel
        {
            [DebuggerNonUserCode]
            get
            {
                return this.sourcesPanel1;
            }
        }

        [Browsable(false)]
        private CommandImageProvider SVNUpdateImageProvider
        {
            get
            {
                return delegate (Source source) {
                    if (source.IsUpToDate)
                    {
                        return Images.svn_update;
                    }
                    if (source.ReadOnlyRecommendedRevisions.Count > 0)
                    {
                        return Images.recommend_down;
                    }
                    return Images.svn_update_available;
                };
            }
        }

        [Browsable(false)]
        private CommandSuffixProvider UnreadCountSuffixProvider
        {
            get
            {
                return delegate (Source source) {
                    if (source.UnreadLogEntriesCount == 0)
                    {
                        return string.Empty;
                    }
                    return string.Format("({0})", source.UnreadCountString);
                };
            }
        }

        public Janus.Windows.GridEX.GridEX UpdatesGrid
        {
            [DebuggerNonUserCode]
            get
            {
                return this.updatesGridContainer1.Grid;
            }
        }

        private delegate Image CommandImageProvider(Source source);

        private delegate string CommandSuffixProvider(Source source);

        private delegate DialogResult ShowMessageInvoker(string text, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon);
    }
}

