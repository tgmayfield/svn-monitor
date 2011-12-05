using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Janus.Windows.EditControls;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;
using Janus.Windows.UI.Dock;
using Janus.Windows.UI.StatusBar;

using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;
using SVNMonitor.Support;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Dialogs;
using SVNMonitor.View.Panels;

namespace SVNMonitor.View
{
	internal class MainForm : Form
	{
		private SVNMonitor.View.Controls.AnimationProgressBar animationProgressBar1;
		private const int BalloonTipTimeOut = 0xea60;
		private readonly Dictionary<UICommand, string> baseMenuTexts = new Dictionary<UICommand, string>();
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
			InitializeComponent();
			if (!base.DesignMode)
			{
				UIHelper.ApplyResources(uiCommandManager1);
				Notifier = new MainNotifier(notifyIcon1);
				if (ApplicationSettingsManager.Settings.StartMinimized)
				{
					base.Opacity = 0.0;
				}
				MonitorSettings.Instance.LoadCaches();
				SetLogGroupByBoxImage();
				formInstance = this;
			}
		}

		private void BrowseDownloadPage()
		{
			Web.SharpRegion.BrowseDownloadPage();
		}

		private void CheckAllSources()
		{
			SourcesPanel.UpdateAllSources();
		}

		private void checkGroupByBox_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SetGroupByBox();
		}

		private void CheckModifications()
		{
			SourcesPanel.SVNCheckModifications();
		}

		private void CheckSelectedSource()
		{
			SourcesPanel.UpdateSource();
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
			ShowAbout();
		}

		private void cmdBigCheckModifications_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			CheckModifications();
		}

		private void cmdBigCheckSource_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			CheckSelectedSource();
		}

		private void cmdBigCheckSources_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			CheckAllSources();
		}

		private void cmdBigExplore_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ExploreSelectedSource();
		}

		private void cmdBigOptions_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenOptionsDialog();
		}

		private void cmdBigRevert_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceRevert();
		}

		private void cmdBigSendFeedback_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SendFeedback();
		}

		private void cmdBigSourceCommit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNCommitSelectedSource();
		}

		private void cmdBigUpdate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateSelectedSource();
		}

		private void cmdBigUpdateAll_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateAllSources();
		}

		private void cmdBigUpdateAllAvailable_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateAllAvailableSources();
		}

		private void cmdCheckAllSources_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			UpdateAllSources();
		}

		private void cmdCheckNewVersion_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			VersionChecker.Instance.CheckVersionAsync();
		}

		private void cmdClose_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			RealClose();
		}

		private void cmdEnableUpdates_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ToggleEnableUpdates();
		}

		private void cmdFeedback_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SendFeedback();
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
			NewMonitor();
		}

		private void cmdNewSource_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			NewSource();
		}

		private void cmdNewVersionAvailable_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			VersionChecker.VersionEventArgs args = (VersionChecker.VersionEventArgs)cmdNewVersionAvailable.Tag;
			if (args.UpgradeAvailable)
			{
				ShowNewVersionDialog(args);
			}
			else
			{
				BrowseDownloadPage();
			}
		}

		private void cmdOpen_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			RestoreForm();
		}

		private void cmdOptions_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenOptionsDialog();
		}

		private void cmdSVNCheckModifications_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceCheckModifications((Source)e.Command.Tag);
		}

		private void cmdSVNCommit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceSVNCommit((Source)e.Command.Tag);
		}

		private void cmdSVNRevert_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceSVNRevert((Source)e.Command.Tag);
		}

		private void cmdSVNUpdate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceSVNUpdate((Source)e.Command.Tag);
		}

		private void cmdSVNUpdateAll_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateAllSources();
		}

		private void cmdSVNUpdateAllAvailable_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateAllAvailableSources();
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
			if (!baseMenuTexts.ContainsKey(menu))
			{
				baseMenuTexts.Add(menu, menu.Text);
			}
			else
			{
				menu.Text = baseMenuTexts[menu];
			}
			menu.Commands.Clear();
			menu.Click -= eventHandler;
			IEnumerable<Source> matchingSources = Status.EnabledSources.Where(s => predicate(s));
			switch (matchingSources.Count())
			{
				case 0:
					menu.Enabled = Janus.Windows.UI.InheritableBoolean.False;
					return;

				case 1:
				{
					Source source = matchingSources.First();
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
			CreateSourceCommandsSubMenu(menuCheckModifications, src => !src.IsURL, "SVNCheckModifications", ModifiedOrUnversionedCountSuffixProvider, CheckModificationsImageProvider, cmdSVNCheckModifications_Click);
		}

		private void CreateSVNCommitSubMenu()
		{
			CreateSourceCommandsSubMenu(menuSVNCommit, delegate(Source src)
			{
				if (!ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled)
				{
					return src.HasLocalChanges;
				}
				return true;
			}, "SVNCommit", ModifiedOrUnversionedCountSuffixProvider, null, cmdSVNCommit_Click);
		}

		private void CreateSVNRevertSubMenu()
		{
			CreateSourceCommandsSubMenu(menuSVNRevert, src => src.HasLocalVersionedChanges, "SVNRevert", ModifiedCountSuffixProvider, null, cmdSVNRevert_Click);
		}

		private void CreateSVNUpdateSubMenu()
		{
			CreateSourceCommandsSubMenu(menuSVNUpdate, src => !src.IsURL, "SVNUpdate", UnreadCountSuffixProvider, SVNUpdateImageProvider, cmdSVNUpdate_Click);
		}

		[Conditional("DEBUG")]
		private void DEBUG_GenerateError()
		{
			throw new Exception("Generated Error");
		}

		[Conditional("DEBUG")]
		private void DEBUG_GenerateInvokeError()
		{
			new Thread(DEBUG_GenerateInvokeException).Start();
		}

		private void DEBUG_GenerateInvokeException()
		{
			base.BeginInvoke(new MethodInvoker(DEBUG_GenerateNullableException));
		}

		private void DEBUG_GenerateNullableException()
		{
			int? number = null;
			int local1 = number.Value;
		}

		[Conditional("DEBUG")]
		private void DEBUG_SetLogEntriesGridLayout()
		{
			LogEntriesPanel.SetGridLayout(ApplicationSettingsManager.Settings.UILogEntriesGridLayout);
		}

		[Conditional("DEBUG")]
		private void DEBUG_ShowDebugCommands()
		{
			menuDebug.Visible = Janus.Windows.UI.InheritableBoolean.True;
		}

		[Conditional("DEBUG")]
		private void DEBUG_ShowNewVersionDialog()
		{
			NewVersionDialog.ShowNewVersionDialog(FileSystemHelper.CurrentVersion, new Version("9.9.9.9"), "Some message");
		}

		private void DeleteObsoleteFiles()
		{
			string[] files = new[]
			{
				Path.Combine(FileSystemHelper.AppData, "SVNMonitor.state"), Path.Combine(FileSystemHelper.AppData, "SVNMonitor.activity")
			};
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
			SVNMonitor.Helpers.ThreadHelper.Queue(DeleteUpgradeDirectory, "DELUPGRADE");
		}

		protected override void Dispose(bool disposing)
		{
		}

		private void EnableCommands()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(EnableCommands));
			}
			else
			{
				bool tempAnotherLocal0;
				bool tempAnotherLocal1;
				bool tempAnotherLocal2;
				bool sourcesExists = MonitorSettings.Instance.Sources.Count > 0;
				bool availableUpdatesExists = Status.NotUpToDateSources.Count() > 0;
				CanSVNUpdateAllAvailable = availableUpdatesExists;
				CanSVNUpdateAll = sourcesExists;
				CanCheckUpdates = tempAnotherLocal0 = true;
				CanRevert = tempAnotherLocal1 = tempAnotherLocal0;
				CanCommit = tempAnotherLocal2 = tempAnotherLocal1;
				CanCheckModifications = CanUpdate = tempAnotherLocal2;
				CanBigCheckModifications = SourcesPanel.CanSVNCheckForModifications;
				CanBigCheckSource = SourcesPanel.CanCheckForUpdates;
				CanBigCheckSources = sourcesExists;
				CanBigExplore = SourcesPanel.CanExplore;
				CanBigOptions = true;
				CanBigSendFeedback = true;
				CanBigUpdate = SourcesPanel.CanSVNUpdate;
				CanBigCommit = SourcesPanel.CanSVNCommit;
				CanBigRevert = SourcesPanel.CanSVNRevert;
				CanBigUpdateAll = sourcesExists;
				CanBigUpdateAllAvailable = availableUpdatesExists;
			}
		}

		private void ExploreSelectedSource()
		{
			SourcesPanel.Browse();
		}

		public void FocusEventLog()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(FocusEventLog));
			}
			else
			{
				base.BringToFront();
				base.Activate();
				uiPanelEventLog.AutoHideActive = true;
				uiPanelEventLog.Focus();
			}
		}

		public void FocusEventLog(long eventID)
		{
			FocusEventLog();
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
			components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
			UIStatusBarPanel uiStatusBarPanel1 = new UIStatusBarPanel();
			UICommandCategory uiCommandCategory1 = new UICommandCategory();
			animationProgressBar1 = new SVNMonitor.View.Controls.AnimationProgressBar();
			uiPanelManager1 = new UIPanelManager(components);
			uiPanelEventLog = new UIPanel();
			uiPanelEventLogCaptionContainer = new UIPanelCaptionContainer();
			eventLogEntrySearchTextBox1 = new EventLogEntrySearchTextBox();
			eventLogPanel1 = new SVNMonitor.View.Panels.EventLogPanel();
			uiPanelEventLogContainer = new UIPanelInnerContainer();
			uiPanelLeft = new UIPanelGroup();
			uiPanelSources = new UIPanel();
			uiPanelSourcesCaptionContainer = new UIPanelCaptionContainer();
			sourceSearchTextBox1 = new SourceSearchTextBox();
			sourcesPanel1 = new SVNMonitor.View.Panels.SourcesPanel();
			uiPanelSourcesContainer = new UIPanelInnerContainer();
			uiPanelMonitors = new UIPanel();
			uiPanelMonitorsCaptionContainer = new UIPanelCaptionContainer();
			monitorSearchTextBox1 = new MonitorSearchTextBox();
			monitorsPanel1 = new SVNMonitor.View.Panels.MonitorsPanel();
			uiPanelMonitorsContainer = new UIPanelInnerContainer();
			uiPanelPaths = new UIPanel();
			uiPanelPathsCaptionContainer = new UIPanelCaptionContainer();
			svnPathSearchTextBox1 = new SVNPathSearchTextBox();
			pathsPanel1 = new SVNMonitor.View.Panels.PathsPanel();
			logEntriesPanel1 = new SVNMonitor.View.Panels.LogEntriesPanel();
			svnLogEntrySearchTextBox1 = new SVNLogEntrySearchTextBox();
			uiPanelPathsContainer = new UIPanelInnerContainer();
			uiPanelUpdatesGridContainer = new UIPanel();
			uiPanelUpdatesGridContainerContainer = new UIPanelInnerContainer();
			updatesGridContainer1 = new UpdatesGridContainer();
			uiPanelLog = new UIPanel();
			uiPanelLogCaptionContainer = new UIPanelCaptionContainer();
			checkGroupByBox = new UICheckBox();
			uiPanelLogContainer = new UIPanelInnerContainer();
			notifyIcon1 = new NotifyIcon(components);
			uiStatusBar1 = new UIStatusBar();
			uiCommandManager1 = new UICommandManager(components);
			BottomRebar1 = new UIRebar();
			uiCommandBar1 = new UICommandBar();
			menuFile1 = new UICommand("menuFile");
			menuSource1 = new UICommand("menuSource");
			menuMonitor1 = new UICommand("menuMonitor");
			menuLog1 = new UICommand("menuLog");
			menuItem1 = new UICommand("menuItem");
			menuEventLog1 = new UICommand("menuEventLog");
			menuTools1 = new UICommand("menuTools");
			menuHelp1 = new UICommand("menuHelp");
			menuDebug1 = new UICommand("menuDebug");
			cmdNewVersionAvailable1 = new UICommand("cmdNewVersionAvailable");
			uiCommandBar2 = new UICommandBar();
			cmdBigCheckSource1 = new UICommand("cmdBigCheckSource");
			cmdBigCheckSources1 = new UICommand("cmdBigCheckSources");
			cmdBigCheckModifications1 = new UICommand("cmdBigCheckModifications");
			cmdBigExplore1 = new UICommand("cmdBigExplore");
			Separator6 = new UICommand("Separator");
			cmdBigUpdate1 = new UICommand("cmdBigUpdate");
			cmdBigSourceCommit1 = new UICommand("cmdBigSourceCommit");
			cmdBigRevert1 = new UICommand("cmdBigRevert");
			Separator8 = new UICommand("Separator");
			cmdBigUpdateAllAvailable1 = new UICommand("cmdBigUpdateAllAvailable");
			cmdBigUpdateAll1 = new UICommand("cmdBigUpdateAll");
			Separator7 = new UICommand("Separator");
			cmdBigOptions1 = new UICommand("cmdBigOptions");
			cmdBigSendFeedback1 = new UICommand("cmdBigSendFeedback");
			menuFile = new UICommand("menuFile");
			cmdNew1 = new UICommand("cmdNew");
			cmdClose2 = new UICommand("cmdClose");
			menuSource = new UICommand("menuSource");
			menuMonitor = new UICommand("menuMonitor");
			menuLog = new UICommand("menuLog");
			menuItem = new UICommand("menuItem");
			menuTools = new UICommand("menuTools");
			cmdOptions1 = new UICommand("cmdOptions");
			cmdTSVNSettings1 = new UICommand("cmdTSVNSettings");
			menuEventLog = new UICommand("menuEventLog");
			cmdOptions = new UICommand("cmdOptions");
			cmdEnableUpdates = new UICommand("cmdEnableUpdates");
			cmdOpen = new UICommand("cmdOpen");
			cmdClose = new UICommand("cmdClose");
			cmdCheckAllSources = new UICommand("cmdCheckAllSources");
			cmdSVNUpdateAll = new UICommand("cmdSVNUpdateAll");
			cmdSVNUpdateAllAvailable = new UICommand("cmdSVNUpdateAllAvailable");
			menuSVNUpdate = new UICommand("menuSVNUpdate");
			menuSVNCommit = new UICommand("menuSVNCommit");
			menuSVNRevert = new UICommand("menuSVNRevert");
			menuCheckModifications = new UICommand("menuCheckModifications");
			menuHelp = new UICommand("menuHelp");
			cmdCheckNewVersion1 = new UICommand("cmdCheckNewVersion");
			Separator5 = new UICommand("Separator");
			cmdTSVNHelp1 = new UICommand("cmdTSVNHelp");
			cmdFeedback1 = new UICommand("cmdFeedback");
			cmdAbout1 = new UICommand("cmdAbout");
			cmdAbout = new UICommand("cmdAbout");
			cmdCheckNewVersion = new UICommand("cmdCheckNewVersion");
			cmdNew = new UICommand("cmdNew");
			cmdNewSource1 = new UICommand("cmdNewSource");
			cmdNewMonitor1 = new UICommand("cmdNewMonitor");
			cmdNewSource = new UICommand("cmdNewSource");
			cmdNewMonitor = new UICommand("cmdNewMonitor");
			cmdNewVersionAvailable = new UICommand("cmdNewVersionAvailable");
			cmdTSVNSettings = new UICommand("cmdTSVNSettings");
			cmdTSVNHelp = new UICommand("cmdTSVNHelp");
			cmdFeedback = new UICommand("cmdFeedback");
			menuDebug = new UICommand("menuDebug");
			menuDialogs1 = new UICommand("menuDialogs");
			Separator9 = new UICommand("Separator");
			cmdGenerateError1 = new UICommand("cmdGenerateError");
			cmdGenerateInvokeError1 = new UICommand("cmdGenerateInvokeError");
			menuTestNewVersion1 = new UICommand("menuTestNewVersion");
			cmdGenerateError = new UICommand("cmdGenerateError");
			cmdGenerateInvokeError = new UICommand("cmdGenerateInvokeError");
			cmdTestNewVersion = new UICommand("cmdTestNewVersion");
			txtTestNewVersion = new UICommand("txtTestNewVersion");
			txtTestNewVersionFile = new UICommand("txtTestNewVersionFile");
			menuTestNewVersion = new UICommand("menuTestNewVersion");
			txtTestNewVersion1 = new UICommand("txtTestNewVersion");
			txtTestNewVersionFile1 = new UICommand("txtTestNewVersionFile");
			cmdTestNewVersion1 = new UICommand("cmdTestNewVersion");
			cmdBigCheckSources = new UICommand("cmdBigCheckSources");
			cmdBigCheckSource = new UICommand("cmdBigCheckSource");
			cmdBigCheckModifications = new UICommand("cmdBigCheckModifications");
			cmdBigExplore = new UICommand("cmdBigExplore");
			cmdBigUpdate = new UICommand("cmdBigUpdate");
			cmdBigUpdateAll = new UICommand("cmdBigUpdateAll");
			cmdBigUpdateAllAvailable = new UICommand("cmdBigUpdateAllAvailable");
			cmdBigOptions = new UICommand("cmdBigOptions");
			cmdBigSendFeedback = new UICommand("cmdBigSendFeedback");
			cmdBigSourceCommit = new UICommand("cmdBigSourceCommit");
			cmdBigRevert = new UICommand("cmdBigRevert");
			cmdTestDialogNewVersion = new UICommand("cmdTestDialogNewVersion");
			menuDialogs = new UICommand("menuDialogs");
			cmdTestDialogNewVersion1 = new UICommand("cmdTestDialogNewVersion");
			uiContextMenu1 = new UIContextMenu();
			cmdEnableUpdates1 = new UICommand("cmdEnableUpdates");
			Separator1 = new UICommand("Separator");
			menuCheckModifications1 = new UICommand("menuCheckModifications");
			menuSVNUpdate1 = new UICommand("menuSVNUpdate");
			cmdSVNUpdateAllUnread1 = new UICommand("cmdSVNUpdateAllAvailable");
			cmdSVNUpdateAll1 = new UICommand("cmdSVNUpdateAll");
			menuSVNCommit1 = new UICommand("menuSVNCommit");
			menuSVNRevert1 = new UICommand("menuSVNRevert");
			Separator4 = new UICommand("Separator");
			cmdCheckAllSources1 = new UICommand("cmdCheckAllSources");
			Separator3 = new UICommand("Separator");
			cmdOptions2 = new UICommand("cmdOptions");
			cmdOpen1 = new UICommand("cmdOpen");
			Separator2 = new UICommand("Separator");
			cmdFeedback2 = new UICommand("cmdFeedback");
			cmdClose1 = new UICommand("cmdClose");
			LeftRebar1 = new UIRebar();
			RightRebar1 = new UIRebar();
			TopRebar1 = new UIRebar();
			menuTestGridLayouts = new UICommand("menuTestGridLayouts");
			cmdTestSetLogEntriesGridLayout = new UICommand("cmdTestSetLogEntriesGridLayout");
			cmdTestSetLogEntriesGridLayout1 = new UICommand("cmdTestSetLogEntriesGridLayout");
			menuTestGridLayouts1 = new UICommand("menuTestGridLayouts");
			((ISupportInitialize)uiPanelManager1).BeginInit();
			((ISupportInitialize)uiPanelEventLog).BeginInit();
			uiPanelEventLog.SuspendLayout();
			uiPanelEventLogCaptionContainer.SuspendLayout();
			uiPanelEventLogContainer.SuspendLayout();
			((ISupportInitialize)uiPanelLeft).BeginInit();
			uiPanelLeft.SuspendLayout();
			((ISupportInitialize)uiPanelSources).BeginInit();
			uiPanelSources.SuspendLayout();
			uiPanelSourcesCaptionContainer.SuspendLayout();
			sourcesPanel1.BeginInit();
			uiPanelSourcesContainer.SuspendLayout();
			((ISupportInitialize)uiPanelMonitors).BeginInit();
			uiPanelMonitors.SuspendLayout();
			uiPanelMonitorsCaptionContainer.SuspendLayout();
			uiPanelMonitorsContainer.SuspendLayout();
			((ISupportInitialize)uiPanelPaths).BeginInit();
			uiPanelPaths.SuspendLayout();
			uiPanelPathsCaptionContainer.SuspendLayout();
			pathsPanel1.BeginInit();
			logEntriesPanel1.BeginInit();
			uiPanelPathsContainer.SuspendLayout();
			((ISupportInitialize)uiPanelUpdatesGridContainer).BeginInit();
			uiPanelUpdatesGridContainer.SuspendLayout();
			uiPanelUpdatesGridContainerContainer.SuspendLayout();
			((ISupportInitialize)uiPanelLog).BeginInit();
			uiPanelLog.SuspendLayout();
			uiPanelLogCaptionContainer.SuspendLayout();
			uiPanelLogContainer.SuspendLayout();
			uiStatusBar1.SuspendLayout();
			((ISupportInitialize)uiCommandManager1).BeginInit();
			((ISupportInitialize)BottomRebar1).BeginInit();
			((ISupportInitialize)uiCommandBar1).BeginInit();
			((ISupportInitialize)uiCommandBar2).BeginInit();
			((ISupportInitialize)uiContextMenu1).BeginInit();
			((ISupportInitialize)LeftRebar1).BeginInit();
			((ISupportInitialize)RightRebar1).BeginInit();
			((ISupportInitialize)TopRebar1).BeginInit();
			TopRebar1.SuspendLayout();
			base.SuspendLayout();
			animationProgressBar1.BackColor = Color.Transparent;
			resources.ApplyResources(animationProgressBar1, "animationProgressBar1");
			animationProgressBar1.Name = "animationProgressBar1";
			uiPanelManager1.AllowAutoHideAnimation = false;
			uiPanelManager1.AllowPanelDrag = false;
			uiPanelManager1.AllowPanelDrop = false;
			uiPanelManager1.BackColorAutoHideStrip = Color.FromArgb(160, 160, 160);
			uiPanelManager1.ContainerControl = this;
			uiPanelManager1.SettingsKey = "uiPanelManager1";
			uiPanelManager1.VisualStyle = PanelVisualStyle.Standard;
			uiPanelEventLog.Id = new Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e");
			uiPanelManager1.Panels.Add(uiPanelEventLog);
			uiPanelLeft.Id = new Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247");
			uiPanelSources.Id = new Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811");
			uiPanelLeft.Panels.Add(uiPanelSources);
			uiPanelMonitors.Id = new Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a");
			uiPanelLeft.Panels.Add(uiPanelMonitors);
			uiPanelManager1.Panels.Add(uiPanelLeft);
			uiPanelPaths.Id = new Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e");
			uiPanelManager1.Panels.Add(uiPanelPaths);
			uiPanelUpdatesGridContainer.Id = new Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09");
			uiPanelManager1.Panels.Add(uiPanelUpdatesGridContainer);
			uiPanelLog.Id = new Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae");
			uiPanelManager1.Panels.Add(uiPanelLog);
			uiPanelManager1.BeginPanelInfo();
			uiPanelManager1.AddDockPanelInfo(new Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e"), PanelDockStyle.Bottom, new Size(0x31a, 0xa2), true);
			uiPanelManager1.AddDockPanelInfo(new Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), PanelGroupStyle.HorizontalTiles, PanelDockStyle.Left, false, new Size(300, 640), true);
			uiPanelManager1.AddDockPanelInfo(new Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811"), new Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), 250, true);
			uiPanelManager1.AddDockPanelInfo(new Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a"), new Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), 0xba, true);
			uiPanelManager1.AddDockPanelInfo(new Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e"), PanelDockStyle.Bottom, new Size(0x2a6, 200), true);
			uiPanelManager1.AddDockPanelInfo(new Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09"), PanelDockStyle.Bottom, new Size(0x271, 200), true);
			uiPanelManager1.AddDockPanelInfo(new Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae"), PanelDockStyle.Fill, new Size(0x2a6, 440), true);
			uiPanelManager1.AddFloatingPanelInfo(new Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811"), new Point(-1, -1), new Size(-1, -1), false);
			uiPanelManager1.AddFloatingPanelInfo(new Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae"), new Point(-1, -1), new Size(-1, -1), false);
			uiPanelManager1.AddFloatingPanelInfo(new Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e"), new Point(-1, -1), new Size(-1, -1), false);
			uiPanelManager1.AddFloatingPanelInfo(new Guid("8377ca32-f17f-4127-ae20-70684bb27510"), new Point(0x1b6, 0x1fb), new Size(200, 200), false);
			uiPanelManager1.AddFloatingPanelInfo(new Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09"), new Point(-1, -1), new Size(-1, -1), false);
			uiPanelManager1.AddFloatingPanelInfo(new Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a"), new Point(0x1a7, 0x179), new Size(200, 200), false);
			uiPanelManager1.AddFloatingPanelInfo(new Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e"), new Point(0x23e, 0x224), new Size(200, 200), false);
			uiPanelManager1.EndPanelInfo();
			uiPanelEventLog.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
			resources.ApplyResources(uiPanelEventLog, "uiPanelEventLog");
			uiPanelEventLog.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
			uiPanelEventLog.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			uiPanelEventLog.ContainerCaption = true;
			uiPanelEventLog.ContainerCaptionControl = uiPanelEventLogCaptionContainer;
			uiPanelEventLog.FloatingLocation = new Point(0x23e, 0x224);
			uiPanelEventLog.InnerContainer = uiPanelEventLogContainer;
			uiPanelEventLog.Name = "uiPanelEventLog";
			uiPanelEventLogCaptionContainer.Controls.Add(eventLogEntrySearchTextBox1);
			resources.ApplyResources(uiPanelEventLogCaptionContainer, "uiPanelEventLogCaptionContainer");
			uiPanelEventLogCaptionContainer.Name = "uiPanelEventLogCaptionContainer";
			uiPanelEventLogCaptionContainer.Panel = uiPanelEventLog;
			resources.ApplyResources(eventLogEntrySearchTextBox1, "eventLogEntrySearchTextBox1");
			eventLogEntrySearchTextBox1.BackColor = Color.Transparent;
			eventLogEntrySearchTextBox1.Name = "eventLogEntrySearchTextBox1";
			eventLogEntrySearchTextBox1.RightMargin = 0x18;
			eventLogEntrySearchTextBox1.SearchablePanel = eventLogPanel1;
			resources.ApplyResources(eventLogPanel1, "eventLogPanel1");
			eventLogPanel1.Name = "eventLogPanel1";
			eventLogPanel1.SearchTextBox = eventLogEntrySearchTextBox1;
			uiPanelEventLogContainer.Controls.Add(eventLogPanel1);
			resources.ApplyResources(uiPanelEventLogContainer, "uiPanelEventLogContainer");
			uiPanelEventLogContainer.Name = "uiPanelEventLogContainer";
			resources.ApplyResources(uiPanelLeft, "uiPanelLeft");
			uiPanelLeft.Name = "uiPanelLeft";
			uiPanelSources.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
			uiPanelSources.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
			resources.ApplyResources(uiPanelSources, "uiPanelSources");
			uiPanelSources.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			uiPanelSources.ContainerCaption = true;
			uiPanelSources.ContainerCaptionControl = uiPanelSourcesCaptionContainer;
			uiPanelSources.InnerContainer = uiPanelSourcesContainer;
			uiPanelSources.Name = "uiPanelSources";
			uiPanelSourcesCaptionContainer.Controls.Add(sourceSearchTextBox1);
			resources.ApplyResources(uiPanelSourcesCaptionContainer, "uiPanelSourcesCaptionContainer");
			uiPanelSourcesCaptionContainer.Name = "uiPanelSourcesCaptionContainer";
			uiPanelSourcesCaptionContainer.Panel = uiPanelSources;
			resources.ApplyResources(sourceSearchTextBox1, "sourceSearchTextBox1");
			sourceSearchTextBox1.BackColor = Color.Transparent;
			sourceSearchTextBox1.Name = "sourceSearchTextBox1";
			sourceSearchTextBox1.RightMargin = 0x18;
			sourceSearchTextBox1.SearchablePanel = sourcesPanel1;
			sourcesPanel1.AllowDrop = true;
			sourcesPanel1.BackColor = Color.Transparent;
			resources.ApplyResources(sourcesPanel1, "sourcesPanel1");
			sourcesPanel1.Name = "sourcesPanel1";
			sourcesPanel1.SearchTextBox = sourceSearchTextBox1;
			sourcesPanel1.ShowingAllItems = false;
			sourcesPanel1.SelectionChanged += sourcesPanel1_SelectionChanged;
			uiPanelSourcesContainer.Controls.Add(sourcesPanel1);
			resources.ApplyResources(uiPanelSourcesContainer, "uiPanelSourcesContainer");
			uiPanelSourcesContainer.Name = "uiPanelSourcesContainer";
			uiPanelMonitors.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
			uiPanelMonitors.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
			resources.ApplyResources(uiPanelMonitors, "uiPanelMonitors");
			uiPanelMonitors.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			uiPanelMonitors.ContainerCaption = true;
			uiPanelMonitors.ContainerCaptionControl = uiPanelMonitorsCaptionContainer;
			uiPanelMonitors.FloatingLocation = new Point(0x1a7, 0x179);
			uiPanelMonitors.InnerContainer = uiPanelMonitorsContainer;
			uiPanelMonitors.Name = "uiPanelMonitors";
			uiPanelMonitorsCaptionContainer.Controls.Add(monitorSearchTextBox1);
			resources.ApplyResources(uiPanelMonitorsCaptionContainer, "uiPanelMonitorsCaptionContainer");
			uiPanelMonitorsCaptionContainer.Name = "uiPanelMonitorsCaptionContainer";
			uiPanelMonitorsCaptionContainer.Panel = uiPanelMonitors;
			resources.ApplyResources(monitorSearchTextBox1, "monitorSearchTextBox1");
			monitorSearchTextBox1.BackColor = Color.Transparent;
			monitorSearchTextBox1.Name = "monitorSearchTextBox1";
			monitorSearchTextBox1.RightMargin = 0x18;
			monitorSearchTextBox1.SearchablePanel = monitorsPanel1;
			monitorsPanel1.BackColor = Color.Transparent;
			resources.ApplyResources(monitorsPanel1, "monitorsPanel1");
			monitorsPanel1.Name = "monitorsPanel1";
			monitorsPanel1.SearchTextBox = monitorSearchTextBox1;
			uiPanelMonitorsContainer.Controls.Add(monitorsPanel1);
			resources.ApplyResources(uiPanelMonitorsContainer, "uiPanelMonitorsContainer");
			uiPanelMonitorsContainer.Name = "uiPanelMonitorsContainer";
			uiPanelPaths.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
			uiPanelPaths.AutoHideButtonVisible = Janus.Windows.UI.InheritableBoolean.True;
			uiPanelPaths.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
			resources.ApplyResources(uiPanelPaths, "uiPanelPaths");
			uiPanelPaths.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			uiPanelPaths.ContainerCaption = true;
			uiPanelPaths.ContainerCaptionControl = uiPanelPathsCaptionContainer;
			uiPanelPaths.InnerContainer = uiPanelPathsContainer;
			uiPanelPaths.Name = "uiPanelPaths";
			uiPanelPathsCaptionContainer.Controls.Add(svnPathSearchTextBox1);
			resources.ApplyResources(uiPanelPathsCaptionContainer, "uiPanelPathsCaptionContainer");
			uiPanelPathsCaptionContainer.Name = "uiPanelPathsCaptionContainer";
			uiPanelPathsCaptionContainer.Panel = uiPanelPaths;
			resources.ApplyResources(svnPathSearchTextBox1, "svnPathSearchTextBox1");
			svnPathSearchTextBox1.BackColor = Color.Transparent;
			svnPathSearchTextBox1.Name = "svnPathSearchTextBox1";
			svnPathSearchTextBox1.RightMargin = 0x18;
			svnPathSearchTextBox1.SearchablePanel = pathsPanel1;
			pathsPanel1.BackColor = Color.Transparent;
			resources.ApplyResources(pathsPanel1, "pathsPanel1");
			pathsPanel1.LogEntriesView = logEntriesPanel1;
			pathsPanel1.Name = "pathsPanel1";
			pathsPanel1.SearchTextBox = svnPathSearchTextBox1;
			logEntriesPanel1.BackColor = Color.Transparent;
			resources.ApplyResources(logEntriesPanel1, "logEntriesPanel1");
			logEntriesPanel1.GroupByBoxVisible = false;
			logEntriesPanel1.Name = "logEntriesPanel1";
			logEntriesPanel1.SearchTextBox = svnLogEntrySearchTextBox1;
			logEntriesPanel1.SelectedWithKeyboard = false;
			logEntriesPanel1.SourcesView = sourcesPanel1;
			logEntriesPanel1.SelectionChanged += logEntriesPanel1_SelectionChanged;
			resources.ApplyResources(svnLogEntrySearchTextBox1, "svnLogEntrySearchTextBox1");
			svnLogEntrySearchTextBox1.BackColor = Color.Transparent;
			svnLogEntrySearchTextBox1.Name = "svnLogEntrySearchTextBox1";
			svnLogEntrySearchTextBox1.RightMargin = 0x18;
			svnLogEntrySearchTextBox1.SearchablePanel = logEntriesPanel1;
			uiPanelPathsContainer.Controls.Add(pathsPanel1);
			resources.ApplyResources(uiPanelPathsContainer, "uiPanelPathsContainer");
			uiPanelPathsContainer.Name = "uiPanelPathsContainer";
			uiPanelUpdatesGridContainer.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
			uiPanelUpdatesGridContainer.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			resources.ApplyResources(uiPanelUpdatesGridContainer, "uiPanelUpdatesGridContainer");
			uiPanelUpdatesGridContainer.InnerContainer = uiPanelUpdatesGridContainerContainer;
			uiPanelUpdatesGridContainer.Name = "uiPanelUpdatesGridContainer";
			uiPanelUpdatesGridContainerContainer.Controls.Add(updatesGridContainer1);
			resources.ApplyResources(uiPanelUpdatesGridContainerContainer, "uiPanelUpdatesGridContainerContainer");
			uiPanelUpdatesGridContainerContainer.Name = "uiPanelUpdatesGridContainerContainer";
			updatesGridContainer1.BackColor = Color.Transparent;
			resources.ApplyResources(updatesGridContainer1, "updatesGridContainer1");
			updatesGridContainer1.Name = "updatesGridContainer1";
			uiPanelLog.ActiveCaptionFormatStyle.BackColor = SystemColors.Highlight;
			uiPanelLog.CaptionDoubleClickAction = CaptionDoubleClickAction.None;
			resources.ApplyResources(uiPanelLog, "uiPanelLog");
			uiPanelLog.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			uiPanelLog.ContainerCaption = true;
			uiPanelLog.ContainerCaptionControl = uiPanelLogCaptionContainer;
			uiPanelLog.InnerContainer = uiPanelLogContainer;
			uiPanelLog.Name = "uiPanelLog";
			uiPanelLogCaptionContainer.Controls.Add(svnLogEntrySearchTextBox1);
			uiPanelLogCaptionContainer.Controls.Add(checkGroupByBox);
			resources.ApplyResources(uiPanelLogCaptionContainer, "uiPanelLogCaptionContainer");
			uiPanelLogCaptionContainer.Name = "uiPanelLogCaptionContainer";
			uiPanelLogCaptionContainer.Panel = uiPanelLog;
			resources.ApplyResources(checkGroupByBox, "checkGroupByBox");
			checkGroupByBox.Appearance = System.Windows.Forms.Appearance.Button;
			checkGroupByBox.Checked = true;
			checkGroupByBox.CheckState = CheckState.Checked;
			checkGroupByBox.ImageAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center;
			checkGroupByBox.Name = "checkGroupByBox";
			checkGroupByBox.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
			checkGroupByBox.CheckedChanged += checkGroupByBox_CheckedChanged;
			uiPanelLogContainer.Controls.Add(logEntriesPanel1);
			resources.ApplyResources(uiPanelLogContainer, "uiPanelLogContainer");
			uiPanelLogContainer.Name = "uiPanelLogContainer";
			resources.ApplyResources(notifyIcon1, "notifyIcon1");
			notifyIcon1.MouseClick += notifyIcon1_MouseClick;
			notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
			uiStatusBar1.Controls.Add(animationProgressBar1);
			resources.ApplyResources(uiStatusBar1, "uiStatusBar1");
			uiStatusBar1.Name = "uiStatusBar1";
			uiStatusBarPanel1.AutoSize = StatusBarPanelAutoSize.Spring;
			uiStatusBarPanel1.BorderColor = Color.Empty;
			uiStatusBarPanel1.Control = animationProgressBar1;
			uiStatusBarPanel1.Key = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
			uiStatusBarPanel1.PanelType = StatusBarPanelType.ControlContainer;
			resources.ApplyResources(uiStatusBarPanel1, "uiStatusBarPanel1");
			uiStatusBar1.Panels.AddRange(new[]
			{
				uiStatusBarPanel1
			});
			uiStatusBar1.PanelsBorderColor = Color.Transparent;
			uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.BottomRebar = BottomRebar1;
			resources.ApplyResources(uiCommandCategory1, "uiCommandCategory1");
			uiCommandManager1.Categories.AddRange(new[]
			{
				uiCommandCategory1
			});
			uiCommandManager1.CommandBars.AddRange(new[]
			{
				uiCommandBar1, uiCommandBar2
			});
			uiCommandManager1.Commands.AddRange(new[]
			{
				menuFile, menuSource, menuMonitor, menuLog, menuItem, menuTools, menuEventLog, cmdOptions, cmdEnableUpdates, cmdOpen, cmdClose, cmdCheckAllSources, cmdSVNUpdateAll, cmdSVNUpdateAllAvailable, menuSVNUpdate, menuSVNCommit,
				menuSVNRevert, menuCheckModifications, menuHelp, cmdAbout, cmdCheckNewVersion, cmdNew, cmdNewSource, cmdNewMonitor, cmdNewVersionAvailable, cmdTSVNSettings, cmdTSVNHelp, cmdFeedback, menuDebug, cmdGenerateError, cmdGenerateInvokeError, cmdTestNewVersion,
				txtTestNewVersion, txtTestNewVersionFile, menuTestNewVersion, cmdBigCheckSources, cmdBigCheckSource, cmdBigCheckModifications, cmdBigExplore, cmdBigUpdate, cmdBigUpdateAll, cmdBigUpdateAllAvailable, cmdBigOptions, cmdBigSendFeedback, cmdBigSourceCommit, cmdBigRevert, cmdTestDialogNewVersion, menuDialogs,
				menuTestGridLayouts, cmdTestSetLogEntriesGridLayout
			});
			uiCommandManager1.ContainerControl = this;
			uiCommandManager1.ContextMenus.AddRange(new[]
			{
				uiContextMenu1
			});
			uiCommandManager1.Id = new Guid("4058ab29-b891-4df5-8758-9fc75311fb2c");
			uiCommandManager1.LeftRebar = LeftRebar1;
			resources.ApplyResources(uiCommandManager1, "uiCommandManager1");
			uiCommandManager1.RightRebar = RightRebar1;
			uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.ShowCustomizeButton = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.TopRebar = TopRebar1;
			uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			BottomRebar1.CommandManager = uiCommandManager1;
			resources.ApplyResources(BottomRebar1, "BottomRebar1");
			BottomRebar1.Name = "BottomRebar1";
			uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.Animation = DropDownAnimation.System;
			uiCommandBar1.CommandBarType = CommandBarType.Menu;
			uiCommandBar1.CommandManager = uiCommandManager1;
			uiCommandBar1.Commands.AddRange(new[]
			{
				menuFile1, menuSource1, menuMonitor1, menuLog1, menuItem1, menuEventLog1, menuTools1, menuHelp1, menuDebug1, cmdNewVersionAvailable1
			});
			resources.ApplyResources(uiCommandBar1, "uiCommandBar1");
			uiCommandBar1.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
			uiCommandBar1.Name = "uiCommandBar1";
			uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.ShowCustomizeButton = Janus.Windows.UI.InheritableBoolean.False;
			resources.ApplyResources(menuFile1, "menuFile1");
			menuFile1.Name = "menuFile1";
			resources.ApplyResources(menuSource1, "menuSource1");
			menuSource1.Name = "menuSource1";
			resources.ApplyResources(menuMonitor1, "menuMonitor1");
			menuMonitor1.Name = "menuMonitor1";
			resources.ApplyResources(menuLog1, "menuLog1");
			menuLog1.Name = "menuLog1";
			resources.ApplyResources(menuItem1, "menuItem1");
			menuItem1.Name = "menuItem1";
			resources.ApplyResources(menuEventLog1, "menuEventLog1");
			menuEventLog1.Name = "menuEventLog1";
			resources.ApplyResources(menuTools1, "menuTools1");
			menuTools1.Name = "menuTools1";
			resources.ApplyResources(menuHelp1, "menuHelp1");
			menuHelp1.Name = "menuHelp1";
			resources.ApplyResources(menuDebug1, "menuDebug1");
			menuDebug1.Name = "menuDebug1";
			resources.ApplyResources(cmdNewVersionAvailable1, "cmdNewVersionAvailable1");
			cmdNewVersionAvailable1.Name = "cmdNewVersionAvailable1";
			uiCommandBar2.AllowCustomize = Janus.Windows.UI.InheritableBoolean.True;
			uiCommandBar2.CommandManager = uiCommandManager1;
			uiCommandBar2.Commands.AddRange(new[]
			{
				cmdBigCheckSource1, cmdBigCheckSources1, cmdBigCheckModifications1, cmdBigExplore1, Separator6, cmdBigUpdate1, cmdBigSourceCommit1, cmdBigRevert1, Separator8, cmdBigUpdateAllAvailable1, cmdBigUpdateAll1, Separator7, cmdBigOptions1, cmdBigSendFeedback1
			});
			uiCommandBar2.FullRow = true;
			resources.ApplyResources(uiCommandBar2, "uiCommandBar2");
			uiCommandBar2.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
			uiCommandBar2.Name = "uiCommandBar2";
			uiCommandBar2.View = Janus.Windows.UI.CommandBars.View.LargeIcons;
			resources.ApplyResources(cmdBigCheckSource1, "cmdBigCheckSource1");
			cmdBigCheckSource1.Name = "cmdBigCheckSource1";
			resources.ApplyResources(cmdBigCheckSources1, "cmdBigCheckSources1");
			cmdBigCheckSources1.Name = "cmdBigCheckSources1";
			resources.ApplyResources(cmdBigCheckModifications1, "cmdBigCheckModifications1");
			cmdBigCheckModifications1.Name = "cmdBigCheckModifications1";
			resources.ApplyResources(cmdBigExplore1, "cmdBigExplore1");
			cmdBigExplore1.Name = "cmdBigExplore1";
			Separator6.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator6, "Separator6");
			Separator6.Name = "Separator6";
			resources.ApplyResources(cmdBigUpdate1, "cmdBigUpdate1");
			cmdBigUpdate1.Name = "cmdBigUpdate1";
			resources.ApplyResources(cmdBigSourceCommit1, "cmdBigSourceCommit1");
			cmdBigSourceCommit1.Name = "cmdBigSourceCommit1";
			resources.ApplyResources(cmdBigRevert1, "cmdBigRevert1");
			cmdBigRevert1.Name = "cmdBigRevert1";
			Separator8.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator8, "Separator8");
			Separator8.Name = "Separator8";
			resources.ApplyResources(cmdBigUpdateAllAvailable1, "cmdBigUpdateAllAvailable1");
			cmdBigUpdateAllAvailable1.Name = "cmdBigUpdateAllAvailable1";
			resources.ApplyResources(cmdBigUpdateAll1, "cmdBigUpdateAll1");
			cmdBigUpdateAll1.Name = "cmdBigUpdateAll1";
			Separator7.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator7, "Separator7");
			Separator7.Name = "Separator7";
			resources.ApplyResources(cmdBigOptions1, "cmdBigOptions1");
			cmdBigOptions1.Name = "cmdBigOptions1";
			resources.ApplyResources(cmdBigSendFeedback1, "cmdBigSendFeedback1");
			cmdBigSendFeedback1.Name = "cmdBigSendFeedback1";
			menuFile.Commands.AddRange(new[]
			{
				cmdNew1, cmdClose2
			});
			resources.ApplyResources(menuFile, "menuFile");
			menuFile.Name = "menuFile";
			resources.ApplyResources(cmdNew1, "cmdNew1");
			cmdNew1.Name = "cmdNew1";
			resources.ApplyResources(cmdClose2, "cmdClose2");
			cmdClose2.Name = "cmdClose2";
			resources.ApplyResources(menuSource, "menuSource");
			menuSource.Name = "menuSource";
			resources.ApplyResources(menuMonitor, "menuMonitor");
			menuMonitor.Name = "menuMonitor";
			resources.ApplyResources(menuLog, "menuLog");
			menuLog.Name = "menuLog";
			resources.ApplyResources(menuItem, "menuItem");
			menuItem.Name = "menuItem";
			menuTools.Commands.AddRange(new[]
			{
				cmdOptions1, cmdTSVNSettings1
			});
			resources.ApplyResources(menuTools, "menuTools");
			menuTools.Name = "menuTools";
			resources.ApplyResources(cmdOptions1, "cmdOptions1");
			cmdOptions1.Name = "cmdOptions1";
			resources.ApplyResources(cmdTSVNSettings1, "cmdTSVNSettings1");
			cmdTSVNSettings1.Name = "cmdTSVNSettings1";
			resources.ApplyResources(menuEventLog, "menuEventLog");
			menuEventLog.Name = "menuEventLog";
			resources.ApplyResources(cmdOptions, "cmdOptions");
			cmdOptions.Name = "cmdOptions";
			cmdOptions.Click += cmdOptions_Click;
			cmdEnableUpdates.CommandType = CommandType.ToggleButton;
			resources.ApplyResources(cmdEnableUpdates, "cmdEnableUpdates");
			cmdEnableUpdates.Name = "cmdEnableUpdates";
			cmdEnableUpdates.Click += cmdEnableUpdates_Click;
			resources.ApplyResources(cmdOpen, "cmdOpen");
			cmdOpen.Name = "cmdOpen";
			cmdOpen.Click += cmdOpen_Click;
			resources.ApplyResources(cmdClose, "cmdClose");
			cmdClose.Name = "cmdClose";
			cmdClose.Click += cmdClose_Click;
			resources.ApplyResources(cmdCheckAllSources, "cmdCheckAllSources");
			cmdCheckAllSources.Name = "cmdCheckAllSources";
			cmdCheckAllSources.Click += cmdCheckAllSources_Click;
			resources.ApplyResources(cmdSVNUpdateAll, "cmdSVNUpdateAll");
			cmdSVNUpdateAll.Name = "cmdSVNUpdateAll";
			cmdSVNUpdateAll.Click += cmdSVNUpdateAll_Click;
			resources.ApplyResources(cmdSVNUpdateAllAvailable, "cmdSVNUpdateAllAvailable");
			cmdSVNUpdateAllAvailable.Name = "cmdSVNUpdateAllAvailable";
			cmdSVNUpdateAllAvailable.Click += cmdSVNUpdateAllAvailable_Click;
			resources.ApplyResources(menuSVNUpdate, "menuSVNUpdate");
			menuSVNUpdate.Name = "menuSVNUpdate";
			resources.ApplyResources(menuSVNCommit, "menuSVNCommit");
			menuSVNCommit.Name = "menuSVNCommit";
			resources.ApplyResources(menuSVNRevert, "menuSVNRevert");
			menuSVNRevert.Name = "menuSVNRevert";
			resources.ApplyResources(menuCheckModifications, "menuCheckModifications");
			menuCheckModifications.Name = "menuCheckModifications";
			menuHelp.Commands.AddRange(new[]
			{
				cmdCheckNewVersion1, Separator5, cmdTSVNHelp1, cmdFeedback1, cmdAbout1
			});
			resources.ApplyResources(menuHelp, "menuHelp");
			menuHelp.Name = "menuHelp";
			resources.ApplyResources(cmdCheckNewVersion1, "cmdCheckNewVersion1");
			cmdCheckNewVersion1.Name = "cmdCheckNewVersion1";
			Separator5.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator5, "Separator5");
			Separator5.Name = "Separator5";
			resources.ApplyResources(cmdTSVNHelp1, "cmdTSVNHelp1");
			cmdTSVNHelp1.Name = "cmdTSVNHelp1";
			resources.ApplyResources(cmdFeedback1, "cmdFeedback1");
			cmdFeedback1.Name = "cmdFeedback1";
			resources.ApplyResources(cmdAbout1, "cmdAbout1");
			cmdAbout1.Name = "cmdAbout1";
			resources.ApplyResources(cmdAbout, "cmdAbout");
			cmdAbout.Name = "cmdAbout";
			cmdAbout.Click += cmdAbout_Click;
			resources.ApplyResources(cmdCheckNewVersion, "cmdCheckNewVersion");
			cmdCheckNewVersion.Name = "cmdCheckNewVersion";
			cmdCheckNewVersion.Click += cmdCheckNewVersion_Click;
			cmdNew.Commands.AddRange(new[]
			{
				cmdNewSource1, cmdNewMonitor1
			});
			resources.ApplyResources(cmdNew, "cmdNew");
			cmdNew.Name = "cmdNew";
			resources.ApplyResources(cmdNewSource1, "cmdNewSource1");
			cmdNewSource1.Name = "cmdNewSource1";
			resources.ApplyResources(cmdNewMonitor1, "cmdNewMonitor1");
			cmdNewMonitor1.Name = "cmdNewMonitor1";
			resources.ApplyResources(cmdNewSource, "cmdNewSource");
			cmdNewSource.Name = "cmdNewSource";
			cmdNewSource.Click += cmdNewSource_Click;
			resources.ApplyResources(cmdNewMonitor, "cmdNewMonitor");
			cmdNewMonitor.Name = "cmdNewMonitor";
			cmdNewMonitor.Click += cmdNewMonitor_Click;
			cmdNewVersionAvailable.CommandStyle = CommandStyle.TextImage;
			resources.ApplyResources(cmdNewVersionAvailable, "cmdNewVersionAvailable");
			cmdNewVersionAvailable.Name = "cmdNewVersionAvailable";
			cmdNewVersionAvailable.Visible = Janus.Windows.UI.InheritableBoolean.False;
			cmdNewVersionAvailable.Click += cmdNewVersionAvailable_Click;
			resources.ApplyResources(cmdTSVNSettings, "cmdTSVNSettings");
			cmdTSVNSettings.Name = "cmdTSVNSettings";
			cmdTSVNSettings.Click += cmdTSVNSettings_Click;
			resources.ApplyResources(cmdTSVNHelp, "cmdTSVNHelp");
			cmdTSVNHelp.Name = "cmdTSVNHelp";
			cmdTSVNHelp.Click += cmdTSVNHelp_Click;
			resources.ApplyResources(cmdFeedback, "cmdFeedback");
			cmdFeedback.Name = "cmdFeedback";
			cmdFeedback.Click += cmdFeedback_Click;
			resources.ApplyResources(menuDebug, "menuDebug");
			menuDebug.Commands.AddRange(new[]
			{
				menuDialogs1, Separator9, cmdGenerateError1, cmdGenerateInvokeError1, menuTestNewVersion1, menuTestGridLayouts1
			});
			menuDebug.Name = "menuDebug";
			menuDebug.Visible = Janus.Windows.UI.InheritableBoolean.False;
			resources.ApplyResources(menuDialogs1, "menuDialogs1");
			menuDialogs1.Name = "menuDialogs1";
			Separator9.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator9, "Separator9");
			Separator9.Name = "Separator9";
			resources.ApplyResources(cmdGenerateError1, "cmdGenerateError1");
			cmdGenerateError1.Name = "cmdGenerateError1";
			resources.ApplyResources(cmdGenerateInvokeError1, "cmdGenerateInvokeError1");
			cmdGenerateInvokeError1.Name = "cmdGenerateInvokeError1";
			resources.ApplyResources(menuTestNewVersion1, "menuTestNewVersion1");
			menuTestNewVersion1.Name = "menuTestNewVersion1";
			resources.ApplyResources(cmdGenerateError, "cmdGenerateError");
			cmdGenerateError.Name = "cmdGenerateError";
			cmdGenerateError.Click += cmdGenerateError_Click;
			resources.ApplyResources(cmdGenerateInvokeError, "cmdGenerateInvokeError");
			cmdGenerateInvokeError.Name = "cmdGenerateInvokeError";
			cmdGenerateInvokeError.Click += cmdGenerateInvokeError_Click;
			resources.ApplyResources(cmdTestNewVersion, "cmdTestNewVersion");
			cmdTestNewVersion.Name = "cmdTestNewVersion";
			cmdTestNewVersion.Click += cmdTestNewVersion_Click;
			resources.ApplyResources(txtTestNewVersion, "txtTestNewVersion");
			txtTestNewVersion.CommandType = CommandType.TextBoxCommand;
			txtTestNewVersion.ControlValue = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
			txtTestNewVersion.IsEditableControl = Janus.Windows.UI.InheritableBoolean.True;
			txtTestNewVersion.Name = "txtTestNewVersion";
			resources.ApplyResources(txtTestNewVersionFile, "txtTestNewVersionFile");
			txtTestNewVersionFile.CommandType = CommandType.TextBoxCommand;
			txtTestNewVersionFile.ControlValue = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
			txtTestNewVersionFile.IsEditableControl = Janus.Windows.UI.InheritableBoolean.True;
			txtTestNewVersionFile.Name = "txtTestNewVersionFile";
			resources.ApplyResources(menuTestNewVersion, "menuTestNewVersion");
			menuTestNewVersion.Commands.AddRange(new[]
			{
				txtTestNewVersion1, txtTestNewVersionFile1, cmdTestNewVersion1
			});
			menuTestNewVersion.Name = "menuTestNewVersion";
			txtTestNewVersion1.ControlValue = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
			resources.ApplyResources(txtTestNewVersion1, "txtTestNewVersion1");
			txtTestNewVersion1.Name = "txtTestNewVersion1";
			txtTestNewVersionFile1.ControlValue = SVNMonitor.Resources.UI.GridEX.FilterRowInfoText;
			resources.ApplyResources(txtTestNewVersionFile1, "txtTestNewVersionFile1");
			txtTestNewVersionFile1.Name = "txtTestNewVersionFile1";
			resources.ApplyResources(cmdTestNewVersion1, "cmdTestNewVersion1");
			cmdTestNewVersion1.Name = "cmdTestNewVersion1";
			resources.ApplyResources(cmdBigCheckSources, "cmdBigCheckSources");
			cmdBigCheckSources.Name = "cmdBigCheckSources";
			cmdBigCheckSources.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigCheckSources.Click += cmdBigCheckSources_Click;
			resources.ApplyResources(cmdBigCheckSource, "cmdBigCheckSource");
			cmdBigCheckSource.Name = "cmdBigCheckSource";
			cmdBigCheckSource.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigCheckSource.Click += cmdBigCheckSource_Click;
			resources.ApplyResources(cmdBigCheckModifications, "cmdBigCheckModifications");
			cmdBigCheckModifications.Name = "cmdBigCheckModifications";
			cmdBigCheckModifications.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigCheckModifications.Click += cmdBigCheckModifications_Click;
			resources.ApplyResources(cmdBigExplore, "cmdBigExplore");
			cmdBigExplore.Name = "cmdBigExplore";
			cmdBigExplore.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigExplore.Click += cmdBigExplore_Click;
			resources.ApplyResources(cmdBigUpdate, "cmdBigUpdate");
			cmdBigUpdate.Name = "cmdBigUpdate";
			cmdBigUpdate.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigUpdate.Click += cmdBigUpdate_Click;
			resources.ApplyResources(cmdBigUpdateAll, "cmdBigUpdateAll");
			cmdBigUpdateAll.Name = "cmdBigUpdateAll";
			cmdBigUpdateAll.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigUpdateAll.Click += cmdBigUpdateAll_Click;
			resources.ApplyResources(cmdBigUpdateAllAvailable, "cmdBigUpdateAllAvailable");
			cmdBigUpdateAllAvailable.Name = "cmdBigUpdateAllAvailable";
			cmdBigUpdateAllAvailable.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigUpdateAllAvailable.Click += cmdBigUpdateAllAvailable_Click;
			resources.ApplyResources(cmdBigOptions, "cmdBigOptions");
			cmdBigOptions.Name = "cmdBigOptions";
			cmdBigOptions.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigOptions.Click += cmdBigOptions_Click;
			resources.ApplyResources(cmdBigSendFeedback, "cmdBigSendFeedback");
			cmdBigSendFeedback.Name = "cmdBigSendFeedback";
			cmdBigSendFeedback.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigSendFeedback.Click += cmdBigSendFeedback_Click;
			resources.ApplyResources(cmdBigSourceCommit, "cmdBigSourceCommit");
			cmdBigSourceCommit.Name = "cmdBigSourceCommit";
			cmdBigSourceCommit.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigSourceCommit.Click += cmdBigSourceCommit_Click;
			resources.ApplyResources(cmdBigRevert, "cmdBigRevert");
			cmdBigRevert.Name = "cmdBigRevert";
			cmdBigRevert.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			cmdBigRevert.Click += cmdBigRevert_Click;
			resources.ApplyResources(cmdTestDialogNewVersion, "cmdTestDialogNewVersion");
			cmdTestDialogNewVersion.Name = "cmdTestDialogNewVersion";
			cmdTestDialogNewVersion.Click += cmdTestDialogNewVersion_Click;
			resources.ApplyResources(menuDialogs, "menuDialogs");
			menuDialogs.Commands.AddRange(new[]
			{
				cmdTestDialogNewVersion1
			});
			menuDialogs.Name = "menuDialogs";
			resources.ApplyResources(cmdTestDialogNewVersion1, "cmdTestDialogNewVersion1");
			cmdTestDialogNewVersion1.Name = "cmdTestDialogNewVersion1";
			uiContextMenu1.CommandManager = uiCommandManager1;
			uiContextMenu1.Commands.AddRange(new[]
			{
				cmdEnableUpdates1, Separator1, menuCheckModifications1, menuSVNUpdate1, cmdSVNUpdateAllUnread1, cmdSVNUpdateAll1, menuSVNCommit1, menuSVNRevert1, Separator4, cmdCheckAllSources1, Separator3, cmdOptions2, cmdOpen1, Separator2, cmdFeedback2, cmdClose1
			});
			resources.ApplyResources(uiContextMenu1, "uiContextMenu1");
			uiContextMenu1.Popup += uiContextMenu1_Popup;
			resources.ApplyResources(cmdEnableUpdates1, "cmdEnableUpdates1");
			cmdEnableUpdates1.Name = "cmdEnableUpdates1";
			Separator1.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator1, "Separator1");
			Separator1.Name = "Separator1";
			resources.ApplyResources(menuCheckModifications1, "menuCheckModifications1");
			menuCheckModifications1.Name = "menuCheckModifications1";
			resources.ApplyResources(menuSVNUpdate1, "menuSVNUpdate1");
			menuSVNUpdate1.Name = "menuSVNUpdate1";
			resources.ApplyResources(cmdSVNUpdateAllUnread1, "cmdSVNUpdateAllUnread1");
			cmdSVNUpdateAllUnread1.Name = "cmdSVNUpdateAllUnread1";
			resources.ApplyResources(cmdSVNUpdateAll1, "cmdSVNUpdateAll1");
			cmdSVNUpdateAll1.Name = "cmdSVNUpdateAll1";
			resources.ApplyResources(menuSVNCommit1, "menuSVNCommit1");
			menuSVNCommit1.Name = "menuSVNCommit1";
			resources.ApplyResources(menuSVNRevert1, "menuSVNRevert1");
			menuSVNRevert1.Name = "menuSVNRevert1";
			Separator4.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator4, "Separator4");
			Separator4.Name = "Separator4";
			resources.ApplyResources(cmdCheckAllSources1, "cmdCheckAllSources1");
			cmdCheckAllSources1.Name = "cmdCheckAllSources1";
			Separator3.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator3, "Separator3");
			Separator3.Name = "Separator3";
			resources.ApplyResources(cmdOptions2, "cmdOptions2");
			cmdOptions2.Name = "cmdOptions2";
			cmdOpen1.DefaultItem = Janus.Windows.UI.InheritableBoolean.True;
			resources.ApplyResources(cmdOpen1, "cmdOpen1");
			cmdOpen1.Name = "cmdOpen1";
			Separator2.CommandType = CommandType.Separator;
			resources.ApplyResources(Separator2, "Separator2");
			Separator2.Name = "Separator2";
			resources.ApplyResources(cmdFeedback2, "cmdFeedback2");
			cmdFeedback2.Name = "cmdFeedback2";
			resources.ApplyResources(cmdClose1, "cmdClose1");
			cmdClose1.Name = "cmdClose1";
			LeftRebar1.CommandManager = uiCommandManager1;
			resources.ApplyResources(LeftRebar1, "LeftRebar1");
			LeftRebar1.Name = "LeftRebar1";
			RightRebar1.CommandManager = uiCommandManager1;
			resources.ApplyResources(RightRebar1, "RightRebar1");
			RightRebar1.Name = "RightRebar1";
			TopRebar1.CommandBars.AddRange(new[]
			{
				uiCommandBar1, uiCommandBar2
			});
			TopRebar1.CommandManager = uiCommandManager1;
			TopRebar1.Controls.Add(uiCommandBar1);
			TopRebar1.Controls.Add(uiCommandBar2);
			resources.ApplyResources(TopRebar1, "TopRebar1");
			TopRebar1.Name = "TopRebar1";
			menuTestGridLayouts.Commands.AddRange(new[]
			{
				cmdTestSetLogEntriesGridLayout1
			});
			resources.ApplyResources(menuTestGridLayouts, "menuTestGridLayouts");
			menuTestGridLayouts.Name = "menuTestGridLayouts";
			resources.ApplyResources(cmdTestSetLogEntriesGridLayout, "cmdTestSetLogEntriesGridLayout");
			cmdTestSetLogEntriesGridLayout.Name = "cmdTestSetLogEntriesGridLayout";
			cmdTestSetLogEntriesGridLayout.Click += cmdTestSetLogEntriesGridLayout_Click;
			resources.ApplyResources(cmdTestSetLogEntriesGridLayout1, "cmdTestSetLogEntriesGridLayout1");
			cmdTestSetLogEntriesGridLayout1.Name = "cmdTestSetLogEntriesGridLayout1";
			resources.ApplyResources(menuTestGridLayouts1, "menuTestGridLayouts1");
			menuTestGridLayouts1.Name = "menuTestGridLayouts1";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(uiPanelLog);
			base.Controls.Add(uiPanelUpdatesGridContainer);
			base.Controls.Add(uiPanelPaths);
			base.Controls.Add(uiPanelLeft);
			base.Controls.Add(uiStatusBar1);
			base.Controls.Add(TopRebar1);
			base.KeyPreview = true;
			base.Name = "MainForm";
			base.KeyDown += MainForm_KeyDown;
			((ISupportInitialize)uiPanelManager1).EndInit();
			((ISupportInitialize)uiPanelEventLog).EndInit();
			uiPanelEventLog.ResumeLayout(false);
			uiPanelEventLogCaptionContainer.ResumeLayout(false);
			uiPanelEventLogContainer.ResumeLayout(false);
			((ISupportInitialize)uiPanelLeft).EndInit();
			uiPanelLeft.ResumeLayout(false);
			((ISupportInitialize)uiPanelSources).EndInit();
			uiPanelSources.ResumeLayout(false);
			uiPanelSourcesCaptionContainer.ResumeLayout(false);
			sourcesPanel1.EndInit();
			uiPanelSourcesContainer.ResumeLayout(false);
			((ISupportInitialize)uiPanelMonitors).EndInit();
			uiPanelMonitors.ResumeLayout(false);
			uiPanelMonitorsCaptionContainer.ResumeLayout(false);
			uiPanelMonitorsContainer.ResumeLayout(false);
			((ISupportInitialize)uiPanelPaths).EndInit();
			uiPanelPaths.ResumeLayout(false);
			uiPanelPathsCaptionContainer.ResumeLayout(false);
			pathsPanel1.EndInit();
			logEntriesPanel1.EndInit();
			uiPanelPathsContainer.ResumeLayout(false);
			((ISupportInitialize)uiPanelUpdatesGridContainer).EndInit();
			uiPanelUpdatesGridContainer.ResumeLayout(false);
			uiPanelUpdatesGridContainerContainer.ResumeLayout(false);
			((ISupportInitialize)uiPanelLog).EndInit();
			uiPanelLog.ResumeLayout(false);
			uiPanelLogCaptionContainer.ResumeLayout(false);
			uiPanelLogContainer.ResumeLayout(false);
			uiStatusBar1.ResumeLayout(false);
			((ISupportInitialize)uiCommandManager1).EndInit();
			((ISupportInitialize)BottomRebar1).EndInit();
			((ISupportInitialize)uiCommandBar1).EndInit();
			((ISupportInitialize)uiCommandBar2).EndInit();
			((ISupportInitialize)uiContextMenu1).EndInit();
			((ISupportInitialize)LeftRebar1).EndInit();
			((ISupportInitialize)RightRebar1).EndInit();
			((ISupportInitialize)TopRebar1).EndInit();
			TopRebar1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void InstallKeyHooks()
		{
			KeyboardHookHelper.Install();
		}

		private void Instance_UpgradeAvailable(object sender, VersionChecker.VersionEventArgs e)
		{
			ShowNewVersionMessage(e);
		}

		private void LoadUISettings()
		{
			Logger.Log.Info("Reading Application State for MainForm...");
			checkGroupByBox.Checked = ApplicationSettingsManager.Settings.LogGroupByBox;
			base.Height = ApplicationSettingsManager.Settings.UIMainFormHeight;
			base.Location = new Point(ApplicationSettingsManager.Settings.UIMainFormLocationX, ApplicationSettingsManager.Settings.UIMainFormLocationY);
			base.Width = ApplicationSettingsManager.Settings.UIMainFormWidth;
			base.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), ApplicationSettingsManager.Settings.UIMainFormWindowState);
			uiPanelEventLog.AutoHide = ApplicationSettingsManager.Settings.UIPanelEventLogAutoHide;
			uiPanelEventLog.Height = ApplicationSettingsManager.Settings.UIPanelEventLogHeight;
			uiPanelLeft.Width = ApplicationSettingsManager.Settings.UIPanelLeftWidth;
			uiPanelMonitors.AutoHide = ApplicationSettingsManager.Settings.UIPanelMonitiorsAutoHide;
			uiPanelMonitors.Height = ApplicationSettingsManager.Settings.UIPanelMonitiorsHeight;
			uiPanelPaths.AutoHide = ApplicationSettingsManager.Settings.UIPanelPathsAutoHide;
			uiPanelPaths.Height = ApplicationSettingsManager.Settings.UIPanelPathsHeight;
			uiPanelSources.AutoHide = ApplicationSettingsManager.Settings.UIPanelSourcesAutoHide;
			Logger.Log.Info("Done reading Application State for MainForm...");
		}

		private void logEntriesPanel1_SelectionChanged(object sender, EventArgs e)
		{
			SetLogEntryName();
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			bool log = true;
			switch (e.KeyCode)
			{
				case Keys.F3:
					Search();
					break;

				case Keys.F5:
					UpdateSource();
					break;

				case Keys.F8:
					FocusEventLog();
					break;

				case Keys.F9:
					UpdateAllSources();
					break;

				case Keys.F:
					if (e.Control)
					{
						Search();
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
			base.SizeChanged -= MainForm_SizeChanged;
			SetGridLayouts();
		}

		private void MergeCommands()
		{
			foreach (UICommand cmd in SourcesPanel.UIContextMenu.Commands)
			{
				menuSource.Commands.Add(cmd);
			}
			foreach (UICommand cmd in MonitorsPanel.UIContextMenu.Commands)
			{
				menuMonitor.Commands.Add(cmd);
			}
			foreach (UICommand cmd in LogEntriesPanel.UIContextMenu.Commands)
			{
				menuLog.Commands.Add(cmd);
			}
			foreach (UICommand cmd in PathsPanel.UIContextMenu.Commands)
			{
				menuItem.Commands.Add(cmd);
			}
			foreach (UICommand cmd in EventLogPanel.UIContextMenu.Commands)
			{
				menuEventLog.Commands.Add(cmd);
			}
		}

		private void NewMonitor()
		{
			MonitorsPanel.CreateNew();
		}

		private void NewSource()
		{
			SourcesPanel.CreateNew();
		}

		private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
		{
			Logger.LogUserAction();
			if (e.Button == MouseButtons.Right)
			{
				try
				{
					uiContextMenu1.Show();
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
				ShowOrHideForm();
			}
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Logger.LogUserAction();
			RestoreForm();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (endSessionPending)
			{
				e.Cancel = false;
				SVNMonitor.EventLog.LogInfo(Strings.WindowsIsShuttingDown, this);
			}
			else
			{
				if (realClose || !ApplicationSettingsManager.Settings.MinimizeWhenClosing)
				{
					SVNMonitor.EventLog.LogSystem(Strings.ApplicationClosing, this);
					Logger.Log.Debug("Saving UI Settings");
					SaveUISettings();
					Logger.Log.Debug("Hiding the tray icon.");
					Notifier.HideTrayIcon();
					Logger.Log.Debug("Hiding the main window.");
					Hide();
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
				RegisterEvents();
				MergeCommands();
				Notifier.SetTrayIcon();
				StartupNotification();
				DeleteObsoleteFiles();
				LoadUISettings();
				CheckStartMinimized();
				Updater.Instance.UpdatesGrid = UpdatesGrid;
				ConditionSerializer.Grid = UpdatesGrid;
				SetPanelsEntities();
				RegisterMonitorSettingsEvents();
				VersionChecker.Start();
				Updater.Start();
				InstallKeyHooks();
				base.SizeChanged += MainForm_SizeChanged;
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
						Notifier.SetTrayIconVisible(true);
						Hide();
					}
				}
				else
				{
					base.Opacity = 100.0;
					lastWindowState = base.WindowState;
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
				base.BeginInvoke(new MethodInvoker(PopupTrayContextMenu));
			}
			else
			{
				ReadSettings();
				if (!Updater.Instance.Enabled)
				{
					bool tempAnotherLocal0;
					bool tempAnotherLocal1;
					bool tempAnotherLocal2;
					bool tempAnotherLocal3;
					bool tempAnotherLocal4;
					CanCheckUpdates = tempAnotherLocal0 = false;
					CanRevert = tempAnotherLocal1 = tempAnotherLocal0;
					CanCommit = tempAnotherLocal2 = tempAnotherLocal1;
					CanUpdate = tempAnotherLocal3 = tempAnotherLocal2;
					CanCheckModifications = tempAnotherLocal4 = tempAnotherLocal3;
					CanSVNUpdateAll = CanSVNUpdateAllAvailable = tempAnotherLocal4;
				}
				else
				{
					CreateSVNCheckModificationsSubMenu();
					CreateSVNCommitSubMenu();
					CreateSVNRevertSubMenu();
					CreateSVNUpdateSubMenu();
					EnableCommands();
				}
			}
		}

		private void PostUpgrade()
		{
			string message = Strings.VersionUpgradedNotification_FORMAT.FormatWith(new object[]
			{
				FileSystemHelper.CurrentVersion
			});
			SVNMonitor.EventLog.LogSystem(message);
			Notifier.ShowBalloonTip(0xea60, Strings.SVNMonitorCaption, message, ToolTipIcon.Info);
			DeleteUpgradeDirectoryAsync();
			Web.SharpRegion.TrySendUpgradeInfo(ApplicationSettingsManager.Settings.InstanceID);
		}

		private void ReadSettings()
		{
			cmdEnableUpdates.Checked = ApplicationSettingsManager.Settings.EnableUpdates.ToInheritableBoolean();
		}

		public void RealClose()
		{
			realClose = true;
			base.Close();
		}

		private void RegisterEvents()
		{
			Status.StatusChanged += Status_StatusChanged;
			VersionChecker.Instance.NewVersionAvailable += versionChecker_NewVersionAvailable;
			VersionChecker.Instance.NoNewVersionAvailable += versionChecker_NoNewVersionAvailable;
			VersionChecker.Instance.UpgradeAvailable += Instance_UpgradeAvailable;
			ApplicationSettingsManager.SavedSettings += (s, ea) => InstallKeyHooks();
		}

		private void RegisterMonitorSettingsEvents()
		{
			MonitorSettings settings = MonitorSettings.Instance;
			settings.SourcesChanged += (s, ea) => SetPanelsEntities();
			settings.MonitorsChanged += (s, ea) => SetPanelsEntities();
		}

		public void ReportError(ErrorReportFeedback report)
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new Action<ErrorReportFeedback>(ReportError), new object[]
				{
					report
				});
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
				base.WindowState = lastWindowState;
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
			ApplicationSettingsManager.Settings.UIPanelEventLogAutoHide = uiPanelEventLog.AutoHide;
			ApplicationSettingsManager.Settings.UIPanelEventLogHeight = uiPanelEventLog.Height;
			ApplicationSettingsManager.Settings.UIPanelLeftWidth = uiPanelLeft.Width;
			ApplicationSettingsManager.Settings.UIPanelMonitiorsAutoHide = uiPanelMonitors.AutoHide;
			ApplicationSettingsManager.Settings.UIPanelMonitiorsHeight = uiPanelMonitors.Height;
			ApplicationSettingsManager.Settings.UIPanelPathsAutoHide = uiPanelPaths.AutoHide;
			ApplicationSettingsManager.Settings.UIPanelPathsHeight = uiPanelPaths.Height;
			ApplicationSettingsManager.Settings.UIPanelSourcesAutoHide = uiPanelSources.AutoHide;
			ApplicationSettingsManager.Settings.UILogEntriesGridLayout = LogEntriesPanel.GetGridLayout();
			ApplicationSettingsManager.Settings.UIPathsGridLayout = PathsPanel.GetGridLayout();
			ApplicationSettingsManager.Settings.UIEventLogGridLayout = EventLogPanel.GetGridLayout();
			ApplicationSettingsManager.SaveSettings();
			Logger.Log.Info("Done writing Application State for MainForm.");
		}

		private void Search()
		{
			svnLogEntrySearchTextBox1.Focus();
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
				base.BeginInvoke(new MethodInvoker(SetAnimation));
			}
			else
			{
				AnimationProgressBar.Visible = Status.Updating;
				AnimationProgressBar.Text = Strings.StatusAnimationTitle_FORMAT.FormatWith(new object[]
				{
					Status.UpdatingSourcesString
				});
			}
		}

		private void SetGridLayouts()
		{
			LogEntriesPanel.SetGridLayout();
			PathsPanel.SetGridLayout();
			EventLogPanel.SetGridLayout();
		}

		private void SetGroupByBox()
		{
			ApplicationSettingsManager.Settings.LogGroupByBox = checkGroupByBox.Checked;
			ApplicationSettingsManager.SaveSettings();
			SetLogGroupByBoxImage();
		}

		private void SetLogEntryName()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(SetLogEntryName));
			}
			else
			{
				SVNLogEntry logEntry = LogEntriesPanel.SelectedItem;
				if (logEntry == null)
				{
					uiPanelPaths.ResetText();
				}
				else
				{
					uiPanelPaths.Text = string.Format("{0} {1} [{2}/{3}]", new object[]
					{
						Strings.LogItemsPanelTitle, logEntry.SourceName, logEntry.Revision, logEntry.Author
					});
				}
				uiPanelPathsCaptionContainer.Invalidate();
			}
		}

		private void SetLogGroupByBoxImage()
		{
			checkGroupByBox.Image = checkGroupByBox.Checked ? Images.elements1 : Images.elements1_unchecked;
		}

		private void SetLogPanelText()
		{
			Source source = SourcesPanel.SelectedItem;
			if (source == null)
			{
				uiPanelLog.Text = Strings.PanelCaptionLog;
			}
			else
			{
				uiPanelLog.Text = Strings.PanelCaptionLogAndName_FORMAT.FormatWith(new object[]
				{
					source.Name
				});
			}
			uiPanelLog.Refresh();
		}

		private void SetPanelsEntities()
		{
			MonitorSettings settings = MonitorSettings.Instance;
			SourcesPanel.Entities = settings.Sources;
			MonitorsPanel.Entities = settings.Monitors;
		}

		private void ShowAbout()
		{
			AboutDialog.ShowStaticDialog(this);
		}

		public DialogResult ShowErrorMessage(string text, string caption)
		{
			return ShowMessage(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
				return (DialogResult)base.Invoke(new ShowMessageInvoker(ShowMessage), new object[]
				{
					text, caption, messageBoxButtons, messageBoxIcon
				});
			}
			return MessageBox.Show(this, text, caption, messageBoxButtons, messageBoxIcon);
		}

		private void ShowNewVersionBalloon(VersionChecker.VersionEventArgs e)
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new Action<VersionChecker.VersionEventArgs>(ShowNewVersionBalloon), new object[]
				{
					e
				});
			}
			else
			{
				EventHandler handler = (s, ea) => ShowNewVersionDialog(e);
				Notifier.ShowBalloonTip(0xea60, Strings.SVNMonitorCaption, Strings.ANewVersionIsAvailable, ToolTipIcon.Info, handler);
			}
		}

		private void ShowNewVersionCommand(VersionChecker.VersionEventArgs e)
		{
			cmdNewVersionAvailable.Visible = Janus.Windows.UI.InheritableBoolean.True;
			cmdNewVersionAvailable.Tag = e;
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
				base.BeginInvoke(new Action<VersionChecker.VersionEventArgs>(ShowNewVersionMessage), new object[]
				{
					e
				});
			}
			else
			{
				SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.System, Strings.ANewVersionIsAvailableWithVersion_FORMAT.FormatWith(new object[]
				{
					e.LatestVersion
				}), VersionChecker.Instance);
				ShowNewVersionCommand(e);
				if (base.Visible)
				{
					ShowNewVersionDialog(e);
				}
				else
				{
					ShowNewVersionBalloon(e);
				}
			}
		}

		private void ShowNoNewVersionMessage()
		{
			string message = "No newer version is available right now";
			SVNMonitor.EventLog.LogInfo(message, this);
			ShowMessage("No newer version is available right now", "SVN-Monitor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		internal void ShowOrHideForm()
		{
			if (base.Visible)
			{
				Hide();
			}
			else
			{
				RestoreForm();
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
			SourcesPanel.SVNRevert();
		}

		private void sourcesPanel1_SelectionChanged(object sender, EventArgs e)
		{
			SetLogPanelText();
			EnableCommands();
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
					PostUpgrade();
				}
				else if (ShowFirstRunNotification)
				{
					Notifier.ShowBalloonTip(0xea60, Strings.SVNMonitorCaption, Strings.HereItIs, ToolTipIcon.Info);
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message, ex);
			}
		}

		private void Status_StatusChanged(object sender, EventArgs e)
		{
			Notifier.SetTrayIcon();
			EnableCommands();
			SetAnimation();
		}

		private void SVNCommitSelectedSource()
		{
			SourcesPanel.SVNCommit();
		}

		private void SVNUpdateAllAvailableSources()
		{
			SVNUpdateAllSources(true);
		}

		private void SVNUpdateAllSources()
		{
			SVNUpdateAllSources(false);
		}

		private void SVNUpdateAllSources(bool ignoreUpToDate)
		{
			Source.SVNUpdateAll(ignoreUpToDate);
		}

		private void SVNUpdateSelectedSource()
		{
			SourcesPanel.SVNUpdate();
		}

		[Conditional("DEBUG")]
		private void TestNewVersionUpgrade(string version, string upgradeFile)
		{
		}

		private void ToggleEnableUpdates()
		{
			ApplicationSettingsManager.Settings.EnableUpdates = cmdEnableUpdates.Checked == Janus.Windows.UI.InheritableBoolean.True;
			ApplicationSettingsManager.SaveSettings();
		}

		private static void TriggerShutdownTimeoutTimer()
		{
			System.Timers.Timer timer = new System.Timers.Timer();
			Logger.Log.Info("Waiting 20 seconds for current SVN activity to end before aborting...");
			timer.Interval = 20000.0;
			timer.AutoReset = false;
			timer.Elapsed += delegate
			{
				SVNMonitor.Helpers.ThreadHelper.SetThreadName("KILL");
				Logger.Log.Info("20 seconds elapsed. Aborting!");
				Shutdown();
			};
			timer.Start();
		}

		private void uiContextMenu1_Popup(object sender, EventArgs e)
		{
			PopupTrayContextMenu();
		}

		private void UpdateAllSources()
		{
			Updater.Instance.QueueUpdates();
		}

		private void UpdateSource()
		{
			Source source = SourcesPanel.SelectedItem;
			if (SourceHelper.CanCheckForUpdates(source))
			{
				Updater.Instance.QueueUpdate(source, true);
			}
		}

		private void versionChecker_NewVersionAvailable(object sender, VersionChecker.VersionEventArgs e)
		{
			ShowNewVersionMessage(e);
		}

		private void versionChecker_NoNewVersionAvailable(object sender, EventArgs e)
		{
			ShowNoNewVersionMessage();
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
				Status.SetClosing(delegate
				{
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
				endSessionPending = true;
			}
			base.WndProc(ref m);
		}

		private SVNMonitor.View.Controls.AnimationProgressBar AnimationProgressBar
		{
			get { return animationProgressBar1; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigCheckModifications
		{
			[DebuggerNonUserCode]
			get { return (cmdBigCheckModifications.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigCheckModifications.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigCheckSource
		{
			[DebuggerNonUserCode]
			get { return (cmdBigCheckSource.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigCheckSource.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigCheckSources
		{
			[DebuggerNonUserCode]
			get { return (cmdBigCheckSources.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigCheckSources.Enabled = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigCommit
		{
			[DebuggerNonUserCode]
			get { return (cmdBigSourceCommit.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigSourceCommit.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigExplore
		{
			[DebuggerNonUserCode]
			get { return (cmdBigExplore.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigExplore.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigOptions
		{
			[DebuggerNonUserCode]
			get { return (cmdBigOptions.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigOptions.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigRevert
		{
			[DebuggerNonUserCode]
			get { return (cmdBigRevert.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigRevert.Enabled = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigSendFeedback
		{
			[DebuggerNonUserCode]
			get { return (cmdBigSendFeedback.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigSendFeedback.Enabled = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigUpdate
		{
			[DebuggerNonUserCode]
			get { return (cmdBigUpdate.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigUpdate.Enabled = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigUpdateAll
		{
			[DebuggerNonUserCode]
			get { return (cmdBigUpdateAll.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigUpdateAll.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigUpdateAllAvailable
		{
			[DebuggerNonUserCode]
			get { return (cmdBigUpdateAllAvailable.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigUpdateAllAvailable.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanCheckModifications
		{
			[DebuggerNonUserCode]
			get { return (menuCheckModifications.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { menuCheckModifications.Visible = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanCheckUpdates
		{
			[DebuggerNonUserCode]
			get { return (cmdCheckAllSources.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdCheckAllSources.Visible = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanCommit
		{
			[DebuggerNonUserCode]
			get { return (menuSVNCommit.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { menuSVNCommit.Visible = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanRevert
		{
			[DebuggerNonUserCode]
			get { return (menuSVNRevert.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { menuSVNRevert.Visible = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanSVNUpdateAll
		{
			[DebuggerNonUserCode]
			get { return (cmdSVNUpdateAll.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdSVNUpdateAll.Visible = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanSVNUpdateAllAvailable
		{
			[DebuggerNonUserCode]
			get { return (cmdSVNUpdateAllAvailable.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdSVNUpdateAllAvailable.Visible = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanUpdate
		{
			[DebuggerNonUserCode]
			get { return (menuSVNUpdate.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { menuSVNUpdate.Visible = value.ToInheritableBoolean(); }
		}

		[Browsable(false)]
		private CommandImageProvider CheckModificationsImageProvider
		{
			get
			{
				return delegate(Source source)
				{
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
			get { return eventLogPanel1; }
		}

		public static MainForm FormInstance
		{
			[DebuggerNonUserCode]
			get { return formInstance; }
		}

		private SVNMonitor.View.Panels.LogEntriesPanel LogEntriesPanel
		{
			get { return logEntriesPanel1; }
		}

		[Browsable(false)]
		private CommandSuffixProvider ModifiedCountSuffixProvider
		{
			get
			{
				return delegate(Source source)
				{
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
				return delegate(Source source)
				{
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
			get { return monitorsPanel1; }
		}

		private MainNotifier Notifier { get; set; }

		private SVNMonitor.View.Panels.PathsPanel PathsPanel
		{
			get { return pathsPanel1; }
		}

		internal bool ShowFirstRunNotification { get; set; }

		private SVNMonitor.View.Panels.SourcesPanel SourcesPanel
		{
			[DebuggerNonUserCode]
			get { return sourcesPanel1; }
		}

		[Browsable(false)]
		private CommandImageProvider SVNUpdateImageProvider
		{
			get
			{
				return delegate(Source source)
				{
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
				return delegate(Source source)
				{
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
			get { return updatesGridContainer1.Grid; }
		}

		private delegate Image CommandImageProvider(Source source);

		private delegate string CommandSuffixProvider(Source source);

		private delegate DialogResult ShowMessageInvoker(string text, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon);
	}
}