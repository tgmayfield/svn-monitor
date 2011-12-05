namespace SVNMonitor.View
{
	internal partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
 
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
			Janus.Windows.UI.CommandBars.UICommandCategory uiCommandCategory1 = new Janus.Windows.UI.CommandBars.UICommandCategory();
			this.animationProgressBar1 = new SVNMonitor.View.Controls.AnimationProgressBar();
			this.uiPanelManager1 = new Janus.Windows.UI.Dock.UIPanelManager(this.components);
			this.uiPanelEventLog = new Janus.Windows.UI.Dock.UIPanel();
			this.uiPanelEventLogCaptionContainer = new Janus.Windows.UI.Dock.UIPanelCaptionContainer();
			this.eventLogEntrySearchTextBox1 = new SVNMonitor.View.Controls.EventLogEntrySearchTextBox();
			this.eventLogPanel1 = new SVNMonitor.View.Panels.EventLogPanel();
			this.uiPanelEventLogContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
			this.uiPanelLeft = new Janus.Windows.UI.Dock.UIPanelGroup();
			this.uiPanelSources = new Janus.Windows.UI.Dock.UIPanel();
			this.uiPanelSourcesCaptionContainer = new Janus.Windows.UI.Dock.UIPanelCaptionContainer();
			this.sourceSearchTextBox1 = new SVNMonitor.View.Controls.SourceSearchTextBox();
			this.sourcesPanel1 = new SVNMonitor.View.Panels.SourcesPanel();
			this.uiPanelSourcesContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
			this.uiPanelMonitors = new Janus.Windows.UI.Dock.UIPanel();
			this.uiPanelMonitorsCaptionContainer = new Janus.Windows.UI.Dock.UIPanelCaptionContainer();
			this.monitorSearchTextBox1 = new SVNMonitor.View.Controls.MonitorSearchTextBox();
			this.monitorsPanel1 = new SVNMonitor.View.Panels.MonitorsPanel();
			this.uiPanelMonitorsContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
			this.uiPanelPaths = new Janus.Windows.UI.Dock.UIPanel();
			this.uiPanelPathsCaptionContainer = new Janus.Windows.UI.Dock.UIPanelCaptionContainer();
			this.svnPathSearchTextBox1 = new SVNMonitor.View.Controls.SVNPathSearchTextBox();
			this.pathsPanel1 = new SVNMonitor.View.Panels.PathsPanel();
			this.logEntriesPanel1 = new SVNMonitor.View.Panels.LogEntriesPanel();
			this.svnLogEntrySearchTextBox1 = new SVNMonitor.View.Controls.SVNLogEntrySearchTextBox();
			this.uiPanelPathsContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
			this.uiPanelUpdatesGridContainer = new Janus.Windows.UI.Dock.UIPanel();
			this.uiPanelUpdatesGridContainerContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
			this.updatesGridContainer1 = new SVNMonitor.View.Panels.UpdatesGridContainer();
			this.uiPanelLog = new Janus.Windows.UI.Dock.UIPanel();
			this.uiPanelLogCaptionContainer = new Janus.Windows.UI.Dock.UIPanelCaptionContainer();
			this.checkGroupByBox = new Janus.Windows.EditControls.UICheckBox();
			this.uiPanelLogContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
			this.uiCommandManager1 = new Janus.Windows.UI.CommandBars.UICommandManager(this.components);
			this.BottomRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.uiCommandBar1 = new Janus.Windows.UI.CommandBars.UICommandBar();
			this.menuFile1 = new Janus.Windows.UI.CommandBars.UICommand("menuFile");
			this.menuSource1 = new Janus.Windows.UI.CommandBars.UICommand("menuSource");
			this.menuMonitor1 = new Janus.Windows.UI.CommandBars.UICommand("menuMonitor");
			this.menuLog1 = new Janus.Windows.UI.CommandBars.UICommand("menuLog");
			this.menuItem1 = new Janus.Windows.UI.CommandBars.UICommand("menuItem");
			this.menuEventLog1 = new Janus.Windows.UI.CommandBars.UICommand("menuEventLog");
			this.menuTools1 = new Janus.Windows.UI.CommandBars.UICommand("menuTools");
			this.menuHelp1 = new Janus.Windows.UI.CommandBars.UICommand("menuHelp");
			this.menuDebug1 = new Janus.Windows.UI.CommandBars.UICommand("menuDebug");
			this.uiCommandBar2 = new Janus.Windows.UI.CommandBars.UICommandBar();
			this.cmdBigCheckSource1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigCheckSource");
			this.cmdBigCheckSources1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigCheckSources");
			this.cmdBigCheckModifications1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigCheckModifications");
			this.cmdBigExplore1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigExplore");
			this.Separator6 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdBigUpdate1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigUpdate");
			this.cmdBigSourceCommit1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigSourceCommit");
			this.cmdBigRevert1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigRevert");
			this.Separator8 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdBigUpdateAllAvailable1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigUpdateAllAvailable");
			this.cmdBigUpdateAll1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigUpdateAll");
			this.Separator7 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdBigOptions1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBigOptions");
			this.menuFile = new Janus.Windows.UI.CommandBars.UICommand("menuFile");
			this.cmdNew1 = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdClose2 = new Janus.Windows.UI.CommandBars.UICommand("cmdClose");
			this.menuSource = new Janus.Windows.UI.CommandBars.UICommand("menuSource");
			this.menuMonitor = new Janus.Windows.UI.CommandBars.UICommand("menuMonitor");
			this.menuLog = new Janus.Windows.UI.CommandBars.UICommand("menuLog");
			this.menuItem = new Janus.Windows.UI.CommandBars.UICommand("menuItem");
			this.menuTools = new Janus.Windows.UI.CommandBars.UICommand("menuTools");
			this.cmdOptions1 = new Janus.Windows.UI.CommandBars.UICommand("cmdOptions");
			this.cmdTSVNSettings1 = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNSettings");
			this.menuEventLog = new Janus.Windows.UI.CommandBars.UICommand("menuEventLog");
			this.cmdOptions = new Janus.Windows.UI.CommandBars.UICommand("cmdOptions");
			this.cmdEnableUpdates = new Janus.Windows.UI.CommandBars.UICommand("cmdEnableUpdates");
			this.cmdOpen = new Janus.Windows.UI.CommandBars.UICommand("cmdOpen");
			this.cmdClose = new Janus.Windows.UI.CommandBars.UICommand("cmdClose");
			this.cmdCheckAllSources = new Janus.Windows.UI.CommandBars.UICommand("cmdCheckAllSources");
			this.cmdSVNUpdateAll = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdateAll");
			this.cmdSVNUpdateAllAvailable = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdateAllAvailable");
			this.menuSVNUpdate = new Janus.Windows.UI.CommandBars.UICommand("menuSVNUpdate");
			this.menuSVNCommit = new Janus.Windows.UI.CommandBars.UICommand("menuSVNCommit");
			this.menuSVNRevert = new Janus.Windows.UI.CommandBars.UICommand("menuSVNRevert");
			this.menuCheckModifications = new Janus.Windows.UI.CommandBars.UICommand("menuCheckModifications");
			this.menuHelp = new Janus.Windows.UI.CommandBars.UICommand("menuHelp");
			this.cmdTSVNHelp1 = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNHelp");
			this.cmdAbout1 = new Janus.Windows.UI.CommandBars.UICommand("cmdAbout");
			this.cmdAbout = new Janus.Windows.UI.CommandBars.UICommand("cmdAbout");
			this.cmdNew = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdNewSource1 = new Janus.Windows.UI.CommandBars.UICommand("cmdNewSource");
			this.cmdNewMonitor1 = new Janus.Windows.UI.CommandBars.UICommand("cmdNewMonitor");
			this.cmdNewSource = new Janus.Windows.UI.CommandBars.UICommand("cmdNewSource");
			this.cmdNewMonitor = new Janus.Windows.UI.CommandBars.UICommand("cmdNewMonitor");
			this.cmdTSVNSettings = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNSettings");
			this.cmdTSVNHelp = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNHelp");
			this.menuDebug = new Janus.Windows.UI.CommandBars.UICommand("menuDebug");
			this.menuDialogs1 = new Janus.Windows.UI.CommandBars.UICommand("menuDialogs");
			this.Separator9 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdGenerateError1 = new Janus.Windows.UI.CommandBars.UICommand("cmdGenerateError");
			this.cmdGenerateInvokeError1 = new Janus.Windows.UI.CommandBars.UICommand("cmdGenerateInvokeError");
			this.menuTestGridLayouts1 = new Janus.Windows.UI.CommandBars.UICommand("menuTestGridLayouts");
			this.cmdGenerateError = new Janus.Windows.UI.CommandBars.UICommand("cmdGenerateError");
			this.cmdGenerateInvokeError = new Janus.Windows.UI.CommandBars.UICommand("cmdGenerateInvokeError");
			this.cmdBigCheckSources = new Janus.Windows.UI.CommandBars.UICommand("cmdBigCheckSources");
			this.cmdBigCheckSource = new Janus.Windows.UI.CommandBars.UICommand("cmdBigCheckSource");
			this.cmdBigCheckModifications = new Janus.Windows.UI.CommandBars.UICommand("cmdBigCheckModifications");
			this.cmdBigExplore = new Janus.Windows.UI.CommandBars.UICommand("cmdBigExplore");
			this.cmdBigUpdate = new Janus.Windows.UI.CommandBars.UICommand("cmdBigUpdate");
			this.cmdBigUpdateAll = new Janus.Windows.UI.CommandBars.UICommand("cmdBigUpdateAll");
			this.cmdBigUpdateAllAvailable = new Janus.Windows.UI.CommandBars.UICommand("cmdBigUpdateAllAvailable");
			this.cmdBigOptions = new Janus.Windows.UI.CommandBars.UICommand("cmdBigOptions");
			this.cmdBigSourceCommit = new Janus.Windows.UI.CommandBars.UICommand("cmdBigSourceCommit");
			this.cmdBigRevert = new Janus.Windows.UI.CommandBars.UICommand("cmdBigRevert");
			this.menuDialogs = new Janus.Windows.UI.CommandBars.UICommand("menuDialogs");
			this.menuTestGridLayouts = new Janus.Windows.UI.CommandBars.UICommand("menuTestGridLayouts");
			this.cmdTestSetLogEntriesGridLayout1 = new Janus.Windows.UI.CommandBars.UICommand("cmdTestSetLogEntriesGridLayout");
			this.cmdTestSetLogEntriesGridLayout = new Janus.Windows.UI.CommandBars.UICommand("cmdTestSetLogEntriesGridLayout");
			this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			this.cmdEnableUpdates1 = new Janus.Windows.UI.CommandBars.UICommand("cmdEnableUpdates");
			this.Separator1 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.menuCheckModifications1 = new Janus.Windows.UI.CommandBars.UICommand("menuCheckModifications");
			this.menuSVNUpdate1 = new Janus.Windows.UI.CommandBars.UICommand("menuSVNUpdate");
			this.cmdSVNUpdateAllUnread1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdateAllAvailable");
			this.cmdSVNUpdateAll1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdateAll");
			this.menuSVNCommit1 = new Janus.Windows.UI.CommandBars.UICommand("menuSVNCommit");
			this.menuSVNRevert1 = new Janus.Windows.UI.CommandBars.UICommand("menuSVNRevert");
			this.Separator4 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdCheckAllSources1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCheckAllSources");
			this.Separator3 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdOptions2 = new Janus.Windows.UI.CommandBars.UICommand("cmdOptions");
			this.cmdOpen1 = new Janus.Windows.UI.CommandBars.UICommand("cmdOpen");
			this.Separator2 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdClose1 = new Janus.Windows.UI.CommandBars.UICommand("cmdClose");
			this.LeftRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.RightRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.TopRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.Separator5 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiPanelEventLog)).BeginInit();
			this.uiPanelEventLog.SuspendLayout();
			this.uiPanelEventLogCaptionContainer.SuspendLayout();
			this.uiPanelEventLogContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiPanelLeft)).BeginInit();
			this.uiPanelLeft.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiPanelSources)).BeginInit();
			this.uiPanelSources.SuspendLayout();
			this.uiPanelSourcesCaptionContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sourcesPanel1)).BeginInit();
			this.uiPanelSourcesContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiPanelMonitors)).BeginInit();
			this.uiPanelMonitors.SuspendLayout();
			this.uiPanelMonitorsCaptionContainer.SuspendLayout();
			this.uiPanelMonitorsContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiPanelPaths)).BeginInit();
			this.uiPanelPaths.SuspendLayout();
			this.uiPanelPathsCaptionContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pathsPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.logEntriesPanel1)).BeginInit();
			this.uiPanelPathsContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiPanelUpdatesGridContainer)).BeginInit();
			this.uiPanelUpdatesGridContainer.SuspendLayout();
			this.uiPanelUpdatesGridContainerContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiPanelLog)).BeginInit();
			this.uiPanelLog.SuspendLayout();
			this.uiPanelLogCaptionContainer.SuspendLayout();
			this.uiPanelLogContainer.SuspendLayout();
			this.uiStatusBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).BeginInit();
			this.TopRebar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// animationProgressBar1
			// 
			this.animationProgressBar1.BackColor = System.Drawing.Color.Transparent;
			this.animationProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.animationProgressBar1.Location = new System.Drawing.Point(0, 0);
			this.animationProgressBar1.Name = "animationProgressBar1";
			this.animationProgressBar1.Size = new System.Drawing.Size(984, 23);
			this.animationProgressBar1.TabIndex = 10;
			this.animationProgressBar1.Visible = false;
			// 
			// uiPanelManager1
			// 
			this.uiPanelManager1.AllowAutoHideAnimation = false;
			this.uiPanelManager1.AllowPanelDrag = false;
			this.uiPanelManager1.AllowPanelDrop = false;
			this.uiPanelManager1.BackColorAutoHideStrip = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.uiPanelManager1.ContainerControl = this;
			this.uiPanelManager1.SettingsKey = "uiPanelManager1";
			this.uiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Standard;
			this.uiPanelEventLog.Id = new System.Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e");
			this.uiPanelManager1.Panels.Add(this.uiPanelEventLog);
			this.uiPanelLeft.Id = new System.Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247");
			this.uiPanelSources.Id = new System.Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811");
			this.uiPanelLeft.Panels.Add(this.uiPanelSources);
			this.uiPanelMonitors.Id = new System.Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a");
			this.uiPanelLeft.Panels.Add(this.uiPanelMonitors);
			this.uiPanelManager1.Panels.Add(this.uiPanelLeft);
			this.uiPanelPaths.Id = new System.Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e");
			this.uiPanelManager1.Panels.Add(this.uiPanelPaths);
			this.uiPanelUpdatesGridContainer.Id = new System.Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09");
			this.uiPanelManager1.Panels.Add(this.uiPanelUpdatesGridContainer);
			this.uiPanelLog.Id = new System.Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae");
			this.uiPanelManager1.Panels.Add(this.uiPanelLog);
			// 
			// Design Time Panel Info:
			// 
			this.uiPanelManager1.BeginPanelInfo();
			this.uiPanelManager1.AddDockPanelInfo(new System.Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e"), Janus.Windows.UI.Dock.PanelDockStyle.Bottom, new System.Drawing.Size(794, 162), true);
			this.uiPanelManager1.AddDockPanelInfo(new System.Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), Janus.Windows.UI.Dock.PanelGroupStyle.HorizontalTiles, Janus.Windows.UI.Dock.PanelDockStyle.Left, false, new System.Drawing.Size(300, 644), true);
			this.uiPanelManager1.AddDockPanelInfo(new System.Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811"), new System.Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), 250, true);
			this.uiPanelManager1.AddDockPanelInfo(new System.Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a"), new System.Guid("4303cd0f-8b8c-4736-856a-7c9ef5bee247"), 186, true);
			this.uiPanelManager1.AddDockPanelInfo(new System.Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e"), Janus.Windows.UI.Dock.PanelDockStyle.Bottom, new System.Drawing.Size(678, 200), true);
			this.uiPanelManager1.AddDockPanelInfo(new System.Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09"), Janus.Windows.UI.Dock.PanelDockStyle.Bottom, new System.Drawing.Size(625, 200), true);
			this.uiPanelManager1.AddDockPanelInfo(new System.Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae"), Janus.Windows.UI.Dock.PanelDockStyle.Fill, new System.Drawing.Size(678, 444), true);
			this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("a5ca62d5-bb66-4b10-a0c0-b1ff65f9a811"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
			this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("06f5a0e9-6d2a-45d5-816c-fffcacf4d2ae"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
			this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("b02e9d6e-2d53-45d3-b449-6985c0e3350e"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
			this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("8377ca32-f17f-4127-ae20-70684bb27510"), new System.Drawing.Point(438, 507), new System.Drawing.Size(200, 200), false);
			this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("08c0aae6-e53d-4fc4-8299-6b937ff4bb09"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
			this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("e0951d42-dcc1-4ff5-acf8-e69f9a1c728a"), new System.Drawing.Point(423, 377), new System.Drawing.Size(200, 200), false);
			this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("b5c4ee38-a6ba-4ca3-b1c0-4f882a18711e"), new System.Drawing.Point(574, 548), new System.Drawing.Size(200, 200), false);
			this.uiPanelManager1.EndPanelInfo();
			// 
			// uiPanelEventLog
			// 
			this.uiPanelEventLog.ActiveCaptionFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
			this.uiPanelEventLog.AutoHide = true;
			this.uiPanelEventLog.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None;
			this.uiPanelEventLog.CaptionHeight = 24;
			this.uiPanelEventLog.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			this.uiPanelEventLog.ContainerCaption = true;
			this.uiPanelEventLog.ContainerCaptionControl = this.uiPanelEventLogCaptionContainer;
			this.uiPanelEventLog.FloatingLocation = new System.Drawing.Point(574, 548);
			this.uiPanelEventLog.Image = ((System.Drawing.Image)(resources.GetObject("uiPanelEventLog.Image")));
			this.uiPanelEventLog.InnerContainer = this.uiPanelEventLogContainer;
			this.uiPanelEventLog.Location = new System.Drawing.Point(3, 412);
			this.uiPanelEventLog.Name = "uiPanelEventLog";
			this.uiPanelEventLog.Size = new System.Drawing.Size(794, 162);
			this.uiPanelEventLog.TabIndex = 4;
			this.uiPanelEventLog.Text = "Event-Log";
			// 
			// uiPanelEventLogCaptionContainer
			// 
			this.uiPanelEventLogCaptionContainer.Controls.Add(this.eventLogEntrySearchTextBox1);
			this.uiPanelEventLogCaptionContainer.Location = new System.Drawing.Point(0, 4);
			this.uiPanelEventLogCaptionContainer.Name = "uiPanelEventLogCaptionContainer";
			this.uiPanelEventLogCaptionContainer.Panel = this.uiPanelEventLog;
			this.uiPanelEventLogCaptionContainer.Size = new System.Drawing.Size(794, 24);
			this.uiPanelEventLogCaptionContainer.TabIndex = 1;
			// 
			// eventLogEntrySearchTextBox1
			// 
			this.eventLogEntrySearchTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.eventLogEntrySearchTextBox1.BackColor = System.Drawing.Color.Transparent;
			this.eventLogEntrySearchTextBox1.Location = new System.Drawing.Point(656, 2);
			this.eventLogEntrySearchTextBox1.Name = "eventLogEntrySearchTextBox1";
			this.eventLogEntrySearchTextBox1.RightMargin = 24;
			this.eventLogEntrySearchTextBox1.SearchablePanel = this.eventLogPanel1;
			this.eventLogEntrySearchTextBox1.Size = new System.Drawing.Size(130, 20);
			this.eventLogEntrySearchTextBox1.TabIndex = 0;
			// 
			// eventLogPanel1
			// 
			this.eventLogPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.eventLogPanel1.Location = new System.Drawing.Point(0, 0);
			this.eventLogPanel1.Name = "eventLogPanel1";
			this.eventLogPanel1.SearchTextBox = this.eventLogEntrySearchTextBox1;
			this.eventLogPanel1.Size = new System.Drawing.Size(792, 133);
			this.eventLogPanel1.TabIndex = 0;
			// 
			// uiPanelEventLogContainer
			// 
			this.uiPanelEventLogContainer.Controls.Add(this.eventLogPanel1);
			this.uiPanelEventLogContainer.Location = new System.Drawing.Point(1, 28);
			this.uiPanelEventLogContainer.Name = "uiPanelEventLogContainer";
			this.uiPanelEventLogContainer.Size = new System.Drawing.Size(792, 133);
			this.uiPanelEventLogContainer.TabIndex = 0;
			// 
			// uiPanelLeft
			// 
			this.uiPanelLeft.Location = new System.Drawing.Point(3, 72);
			this.uiPanelLeft.Name = "uiPanelLeft";
			this.uiPanelLeft.Size = new System.Drawing.Size(300, 644);
			this.uiPanelLeft.TabIndex = 12;
			// 
			// uiPanelSources
			// 
			this.uiPanelSources.ActiveCaptionFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
			this.uiPanelSources.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None;
			this.uiPanelSources.CaptionHeight = 24;
			this.uiPanelSources.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			this.uiPanelSources.ContainerCaption = true;
			this.uiPanelSources.ContainerCaptionControl = this.uiPanelSourcesCaptionContainer;
			this.uiPanelSources.Image = ((System.Drawing.Image)(resources.GetObject("uiPanelSources.Image")));
			this.uiPanelSources.InnerContainer = this.uiPanelSourcesContainer;
			this.uiPanelSources.Location = new System.Drawing.Point(0, 0);
			this.uiPanelSources.Name = "uiPanelSources";
			this.uiPanelSources.Size = new System.Drawing.Size(296, 367);
			this.uiPanelSources.TabIndex = 4;
			this.uiPanelSources.Text = "Sources";
			// 
			// uiPanelSourcesCaptionContainer
			// 
			this.uiPanelSourcesCaptionContainer.Controls.Add(this.sourceSearchTextBox1);
			this.uiPanelSourcesCaptionContainer.Location = new System.Drawing.Point(0, 0);
			this.uiPanelSourcesCaptionContainer.Name = "uiPanelSourcesCaptionContainer";
			this.uiPanelSourcesCaptionContainer.Panel = this.uiPanelSources;
			this.uiPanelSourcesCaptionContainer.Size = new System.Drawing.Size(296, 24);
			this.uiPanelSourcesCaptionContainer.TabIndex = 1;
			// 
			// sourceSearchTextBox1
			// 
			this.sourceSearchTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sourceSearchTextBox1.BackColor = System.Drawing.Color.Transparent;
			this.sourceSearchTextBox1.Location = new System.Drawing.Point(142, 2);
			this.sourceSearchTextBox1.Name = "sourceSearchTextBox1";
			this.sourceSearchTextBox1.RightMargin = 24;
			this.sourceSearchTextBox1.SearchablePanel = this.sourcesPanel1;
			this.sourceSearchTextBox1.Size = new System.Drawing.Size(130, 20);
			this.sourceSearchTextBox1.TabIndex = 0;
			// 
			// sourcesPanel1
			// 
			this.sourcesPanel1.AllowDrop = true;
			this.sourcesPanel1.BackColor = System.Drawing.Color.Transparent;
			this.sourcesPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sourcesPanel1.Location = new System.Drawing.Point(0, 0);
			this.sourcesPanel1.Name = "sourcesPanel1";
			this.sourcesPanel1.SearchTextBox = this.sourceSearchTextBox1;
			this.sourcesPanel1.ShowingAllItems = false;
			this.sourcesPanel1.Size = new System.Drawing.Size(294, 342);
			this.sourcesPanel1.TabIndex = 0;
			// 
			// uiPanelSourcesContainer
			// 
			this.uiPanelSourcesContainer.Controls.Add(this.sourcesPanel1);
			this.uiPanelSourcesContainer.Location = new System.Drawing.Point(1, 24);
			this.uiPanelSourcesContainer.Name = "uiPanelSourcesContainer";
			this.uiPanelSourcesContainer.Size = new System.Drawing.Size(294, 342);
			this.uiPanelSourcesContainer.TabIndex = 0;
			// 
			// uiPanelMonitors
			// 
			this.uiPanelMonitors.ActiveCaptionFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
			this.uiPanelMonitors.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None;
			this.uiPanelMonitors.CaptionHeight = 24;
			this.uiPanelMonitors.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			this.uiPanelMonitors.ContainerCaption = true;
			this.uiPanelMonitors.ContainerCaptionControl = this.uiPanelMonitorsCaptionContainer;
			this.uiPanelMonitors.FloatingLocation = new System.Drawing.Point(423, 377);
			this.uiPanelMonitors.Image = ((System.Drawing.Image)(resources.GetObject("uiPanelMonitors.Image")));
			this.uiPanelMonitors.InnerContainer = this.uiPanelMonitorsContainer;
			this.uiPanelMonitors.Location = new System.Drawing.Point(0, 371);
			this.uiPanelMonitors.Name = "uiPanelMonitors";
			this.uiPanelMonitors.Size = new System.Drawing.Size(296, 273);
			this.uiPanelMonitors.TabIndex = 4;
			this.uiPanelMonitors.Text = "Monitors";
			// 
			// uiPanelMonitorsCaptionContainer
			// 
			this.uiPanelMonitorsCaptionContainer.Controls.Add(this.monitorSearchTextBox1);
			this.uiPanelMonitorsCaptionContainer.Location = new System.Drawing.Point(0, 0);
			this.uiPanelMonitorsCaptionContainer.Name = "uiPanelMonitorsCaptionContainer";
			this.uiPanelMonitorsCaptionContainer.Panel = this.uiPanelMonitors;
			this.uiPanelMonitorsCaptionContainer.Size = new System.Drawing.Size(296, 24);
			this.uiPanelMonitorsCaptionContainer.TabIndex = 1;
			// 
			// monitorSearchTextBox1
			// 
			this.monitorSearchTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.monitorSearchTextBox1.BackColor = System.Drawing.Color.Transparent;
			this.monitorSearchTextBox1.Location = new System.Drawing.Point(142, 2);
			this.monitorSearchTextBox1.Name = "monitorSearchTextBox1";
			this.monitorSearchTextBox1.RightMargin = 24;
			this.monitorSearchTextBox1.SearchablePanel = this.monitorsPanel1;
			this.monitorSearchTextBox1.Size = new System.Drawing.Size(130, 20);
			this.monitorSearchTextBox1.TabIndex = 0;
			// 
			// monitorsPanel1
			// 
			this.monitorsPanel1.BackColor = System.Drawing.Color.Transparent;
			this.monitorsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.monitorsPanel1.Location = new System.Drawing.Point(0, 0);
			this.monitorsPanel1.Name = "monitorsPanel1";
			this.monitorsPanel1.SearchTextBox = this.monitorSearchTextBox1;
			this.monitorsPanel1.Size = new System.Drawing.Size(294, 248);
			this.monitorsPanel1.TabIndex = 0;
			// 
			// uiPanelMonitorsContainer
			// 
			this.uiPanelMonitorsContainer.Controls.Add(this.monitorsPanel1);
			this.uiPanelMonitorsContainer.Location = new System.Drawing.Point(1, 24);
			this.uiPanelMonitorsContainer.Name = "uiPanelMonitorsContainer";
			this.uiPanelMonitorsContainer.Size = new System.Drawing.Size(294, 248);
			this.uiPanelMonitorsContainer.TabIndex = 0;
			// 
			// uiPanelPaths
			// 
			this.uiPanelPaths.ActiveCaptionFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
			this.uiPanelPaths.AutoHideButtonVisible = Janus.Windows.UI.InheritableBoolean.True;
			this.uiPanelPaths.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None;
			this.uiPanelPaths.CaptionHeight = 24;
			this.uiPanelPaths.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			this.uiPanelPaths.ContainerCaption = true;
			this.uiPanelPaths.ContainerCaptionControl = this.uiPanelPathsCaptionContainer;
			this.uiPanelPaths.Image = ((System.Drawing.Image)(resources.GetObject("uiPanelPaths.Image")));
			this.uiPanelPaths.InnerContainer = this.uiPanelPathsContainer;
			this.uiPanelPaths.Location = new System.Drawing.Point(303, 516);
			this.uiPanelPaths.Name = "uiPanelPaths";
			this.uiPanelPaths.Size = new System.Drawing.Size(678, 200);
			this.uiPanelPaths.TabIndex = 4;
			this.uiPanelPaths.Text = "Items";
			// 
			// uiPanelPathsCaptionContainer
			// 
			this.uiPanelPathsCaptionContainer.Controls.Add(this.svnPathSearchTextBox1);
			this.uiPanelPathsCaptionContainer.Location = new System.Drawing.Point(0, 4);
			this.uiPanelPathsCaptionContainer.Name = "uiPanelPathsCaptionContainer";
			this.uiPanelPathsCaptionContainer.Panel = this.uiPanelPaths;
			this.uiPanelPathsCaptionContainer.Size = new System.Drawing.Size(678, 24);
			this.uiPanelPathsCaptionContainer.TabIndex = 1;
			// 
			// svnPathSearchTextBox1
			// 
			this.svnPathSearchTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.svnPathSearchTextBox1.BackColor = System.Drawing.Color.Transparent;
			this.svnPathSearchTextBox1.Location = new System.Drawing.Point(524, 2);
			this.svnPathSearchTextBox1.Name = "svnPathSearchTextBox1";
			this.svnPathSearchTextBox1.RightMargin = 24;
			this.svnPathSearchTextBox1.SearchablePanel = this.pathsPanel1;
			this.svnPathSearchTextBox1.Size = new System.Drawing.Size(130, 20);
			this.svnPathSearchTextBox1.TabIndex = 0;
			// 
			// pathsPanel1
			// 
			this.pathsPanel1.BackColor = System.Drawing.Color.Transparent;
			this.pathsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pathsPanel1.Location = new System.Drawing.Point(0, 0);
			this.pathsPanel1.LogEntriesView = this.logEntriesPanel1;
			this.pathsPanel1.Name = "pathsPanel1";
			this.pathsPanel1.SearchTextBox = this.svnPathSearchTextBox1;
			this.pathsPanel1.Size = new System.Drawing.Size(676, 171);
			this.pathsPanel1.TabIndex = 0;
			// 
			// logEntriesPanel1
			// 
			this.logEntriesPanel1.BackColor = System.Drawing.Color.Transparent;
			this.logEntriesPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logEntriesPanel1.GroupByBoxVisible = false;
			this.logEntriesPanel1.Location = new System.Drawing.Point(0, 0);
			this.logEntriesPanel1.Name = "logEntriesPanel1";
			this.logEntriesPanel1.SearchTextBox = this.svnLogEntrySearchTextBox1;
			this.logEntriesPanel1.SelectedWithKeyboard = false;
			this.logEntriesPanel1.Size = new System.Drawing.Size(676, 419);
			this.logEntriesPanel1.SourcesView = this.sourcesPanel1;
			this.logEntriesPanel1.TabIndex = 0;
			// 
			// svnLogEntrySearchTextBox1
			// 
			this.svnLogEntrySearchTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.svnLogEntrySearchTextBox1.BackColor = System.Drawing.Color.Transparent;
			this.svnLogEntrySearchTextBox1.Location = new System.Drawing.Point(524, 2);
			this.svnLogEntrySearchTextBox1.Name = "svnLogEntrySearchTextBox1";
			this.svnLogEntrySearchTextBox1.RightMargin = 24;
			this.svnLogEntrySearchTextBox1.SearchablePanel = this.logEntriesPanel1;
			this.svnLogEntrySearchTextBox1.Size = new System.Drawing.Size(130, 20);
			this.svnLogEntrySearchTextBox1.TabIndex = 3;
			// 
			// uiPanelPathsContainer
			// 
			this.uiPanelPathsContainer.Controls.Add(this.pathsPanel1);
			this.uiPanelPathsContainer.Location = new System.Drawing.Point(1, 28);
			this.uiPanelPathsContainer.Name = "uiPanelPathsContainer";
			this.uiPanelPathsContainer.Size = new System.Drawing.Size(676, 171);
			this.uiPanelPathsContainer.TabIndex = 0;
			// 
			// uiPanelUpdatesGridContainer
			// 
			this.uiPanelUpdatesGridContainer.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None;
			this.uiPanelUpdatesGridContainer.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			this.uiPanelUpdatesGridContainer.Closed = true;
			this.uiPanelUpdatesGridContainer.InnerContainer = this.uiPanelUpdatesGridContainerContainer;
			this.uiPanelUpdatesGridContainer.Location = new System.Drawing.Point(203, -124);
			this.uiPanelUpdatesGridContainer.Name = "uiPanelUpdatesGridContainer";
			this.uiPanelUpdatesGridContainer.Size = new System.Drawing.Size(625, 200);
			this.uiPanelUpdatesGridContainer.TabIndex = 4;
			this.uiPanelUpdatesGridContainer.Text = "Updates";
			this.uiPanelUpdatesGridContainer.Visible = false;
			// 
			// uiPanelUpdatesGridContainerContainer
			// 
			this.uiPanelUpdatesGridContainerContainer.Controls.Add(this.updatesGridContainer1);
			this.uiPanelUpdatesGridContainerContainer.Location = new System.Drawing.Point(0, 0);
			this.uiPanelUpdatesGridContainerContainer.Name = "uiPanelUpdatesGridContainerContainer";
			this.uiPanelUpdatesGridContainerContainer.Size = new System.Drawing.Size(625, 200);
			this.uiPanelUpdatesGridContainerContainer.TabIndex = 0;
			// 
			// updatesGridContainer1
			// 
			this.updatesGridContainer1.BackColor = System.Drawing.Color.Transparent;
			this.updatesGridContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.updatesGridContainer1.Location = new System.Drawing.Point(0, 0);
			this.updatesGridContainer1.Name = "updatesGridContainer1";
			this.updatesGridContainer1.Size = new System.Drawing.Size(625, 200);
			this.updatesGridContainer1.TabIndex = 0;
			// 
			// uiPanelLog
			// 
			this.uiPanelLog.ActiveCaptionFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
			this.uiPanelLog.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None;
			this.uiPanelLog.CaptionHeight = 24;
			this.uiPanelLog.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
			this.uiPanelLog.ContainerCaption = true;
			this.uiPanelLog.ContainerCaptionControl = this.uiPanelLogCaptionContainer;
			this.uiPanelLog.Image = ((System.Drawing.Image)(resources.GetObject("uiPanelLog.Image")));
			this.uiPanelLog.InnerContainer = this.uiPanelLogContainer;
			this.uiPanelLog.Location = new System.Drawing.Point(303, 72);
			this.uiPanelLog.Name = "uiPanelLog";
			this.uiPanelLog.Size = new System.Drawing.Size(678, 444);
			this.uiPanelLog.TabIndex = 4;
			this.uiPanelLog.Text = "Log";
			// 
			// uiPanelLogCaptionContainer
			// 
			this.uiPanelLogCaptionContainer.Controls.Add(this.svnLogEntrySearchTextBox1);
			this.uiPanelLogCaptionContainer.Controls.Add(this.checkGroupByBox);
			this.uiPanelLogCaptionContainer.Location = new System.Drawing.Point(0, 0);
			this.uiPanelLogCaptionContainer.Name = "uiPanelLogCaptionContainer";
			this.uiPanelLogCaptionContainer.Panel = this.uiPanelLog;
			this.uiPanelLogCaptionContainer.Size = new System.Drawing.Size(678, 24);
			this.uiPanelLogCaptionContainer.TabIndex = 1;
			// 
			// checkGroupByBox
			// 
			this.checkGroupByBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkGroupByBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkGroupByBox.Checked = true;
			this.checkGroupByBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkGroupByBox.Image = ((System.Drawing.Image)(resources.GetObject("checkGroupByBox.Image")));
			this.checkGroupByBox.ImageAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center;
			this.checkGroupByBox.Location = new System.Drawing.Point(657, 3);
			this.checkGroupByBox.Name = "checkGroupByBox";
			this.checkGroupByBox.Size = new System.Drawing.Size(18, 18);
			this.checkGroupByBox.TabIndex = 2;
			this.checkGroupByBox.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
			// 
			// uiPanelLogContainer
			// 
			this.uiPanelLogContainer.Controls.Add(this.logEntriesPanel1);
			this.uiPanelLogContainer.Location = new System.Drawing.Point(1, 24);
			this.uiPanelLogContainer.Name = "uiPanelLogContainer";
			this.uiPanelLogContainer.Size = new System.Drawing.Size(676, 419);
			this.uiPanelLogContainer.TabIndex = 0;
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "SVN-Monitor";
			// 
			// uiStatusBar1
			// 
			this.uiStatusBar1.Controls.Add(this.animationProgressBar1);
			this.uiStatusBar1.Location = new System.Drawing.Point(0, 739);
			this.uiStatusBar1.Name = "uiStatusBar1";
			uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
			uiStatusBarPanel1.Control = this.animationProgressBar1;
			uiStatusBarPanel1.Key = "";
			uiStatusBarPanel1.PanelType = Janus.Windows.UI.StatusBar.StatusBarPanelType.ControlContainer;
			uiStatusBarPanel1.ProgressBarValue = 0;
			uiStatusBarPanel1.Width = 963;
			this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1});
			this.uiStatusBar1.PanelsBorderColor = System.Drawing.Color.Transparent;
			this.uiStatusBar1.Size = new System.Drawing.Size(984, 23);
			this.uiStatusBar1.TabIndex = 7;
			this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			// 
			// uiCommandManager1
			// 
			this.uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.BottomRebar = this.BottomRebar1;
			this.uiCommandManager1.Categories.AddRange(new Janus.Windows.UI.CommandBars.UICommandCategory[] {
            uiCommandCategory1});
			this.uiCommandManager1.CommandBars.AddRange(new Janus.Windows.UI.CommandBars.UICommandBar[] {
            this.uiCommandBar1,
            this.uiCommandBar2});
			this.uiCommandManager1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.menuFile,
            this.menuSource,
            this.menuMonitor,
            this.menuLog,
            this.menuItem,
            this.menuTools,
            this.menuEventLog,
            this.cmdOptions,
            this.cmdEnableUpdates,
            this.cmdOpen,
            this.cmdClose,
            this.cmdCheckAllSources,
            this.cmdSVNUpdateAll,
            this.cmdSVNUpdateAllAvailable,
            this.menuSVNUpdate,
            this.menuSVNCommit,
            this.menuSVNRevert,
            this.menuCheckModifications,
            this.menuHelp,
            this.cmdAbout,
            this.cmdNew,
            this.cmdNewSource,
            this.cmdNewMonitor,
            this.cmdTSVNSettings,
            this.cmdTSVNHelp,
            this.menuDebug,
            this.cmdGenerateError,
            this.cmdGenerateInvokeError,
            this.cmdBigCheckSources,
            this.cmdBigCheckSource,
            this.cmdBigCheckModifications,
            this.cmdBigExplore,
            this.cmdBigUpdate,
            this.cmdBigUpdateAll,
            this.cmdBigUpdateAllAvailable,
            this.cmdBigOptions,
            this.cmdBigSourceCommit,
            this.cmdBigRevert,
            this.menuDialogs,
            this.menuTestGridLayouts,
            this.cmdTestSetLogEntriesGridLayout});
			this.uiCommandManager1.ContainerControl = this;
			this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] {
            this.uiContextMenu1});
			this.uiCommandManager1.Id = new System.Guid("4058ab29-b891-4df5-8758-9fc75311fb2c");
			this.uiCommandManager1.LeftRebar = this.LeftRebar1;
			this.uiCommandManager1.RightRebar = this.RightRebar1;
			this.uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.ShowCustomizeButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.TopRebar = this.TopRebar1;
			this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			// 
			// BottomRebar1
			// 
			this.BottomRebar1.CommandManager = this.uiCommandManager1;
			this.BottomRebar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomRebar1.Location = new System.Drawing.Point(0, 396);
			this.BottomRebar1.Name = "BottomRebar1";
			this.BottomRebar1.Size = new System.Drawing.Size(831, 0);
			// 
			// uiCommandBar1
			// 
			this.uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.Animation = Janus.Windows.UI.DropDownAnimation.System;
			this.uiCommandBar1.CommandBarType = Janus.Windows.UI.CommandBars.CommandBarType.Menu;
			this.uiCommandBar1.CommandManager = this.uiCommandManager1;
			this.uiCommandBar1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.menuFile1,
            this.menuSource1,
            this.menuMonitor1,
            this.menuLog1,
            this.menuItem1,
            this.menuEventLog1,
            this.menuTools1,
            this.menuHelp1,
            this.menuDebug1});
			this.uiCommandBar1.Key = "CommandBar1";
			this.uiCommandBar1.Location = new System.Drawing.Point(0, 0);
			this.uiCommandBar1.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
			this.uiCommandBar1.Name = "uiCommandBar1";
			this.uiCommandBar1.RowIndex = 0;
			this.uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.ShowCustomizeButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.Size = new System.Drawing.Size(984, 22);
			this.uiCommandBar1.Text = "Menu";
			// 
			// menuFile1
			// 
			this.menuFile1.Key = "menuFile";
			this.menuFile1.Name = "menuFile1";
			// 
			// menuSource1
			// 
			this.menuSource1.Key = "menuSource";
			this.menuSource1.Name = "menuSource1";
			// 
			// menuMonitor1
			// 
			this.menuMonitor1.Key = "menuMonitor";
			this.menuMonitor1.Name = "menuMonitor1";
			// 
			// menuLog1
			// 
			this.menuLog1.Key = "menuLog";
			this.menuLog1.Name = "menuLog1";
			// 
			// menuItem1
			// 
			this.menuItem1.Key = "menuItem";
			this.menuItem1.Name = "menuItem1";
			// 
			// menuEventLog1
			// 
			this.menuEventLog1.Key = "menuEventLog";
			this.menuEventLog1.Name = "menuEventLog1";
			// 
			// menuTools1
			// 
			this.menuTools1.Key = "menuTools";
			this.menuTools1.Name = "menuTools1";
			// 
			// menuHelp1
			// 
			this.menuHelp1.Key = "menuHelp";
			this.menuHelp1.Name = "menuHelp1";
			// 
			// menuDebug1
			// 
			this.menuDebug1.Key = "menuDebug";
			this.menuDebug1.Name = "menuDebug1";
			// 
			// uiCommandBar2
			// 
			this.uiCommandBar2.AllowCustomize = Janus.Windows.UI.InheritableBoolean.True;
			this.uiCommandBar2.CommandManager = this.uiCommandManager1;
			this.uiCommandBar2.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdBigCheckSource1,
            this.cmdBigCheckSources1,
            this.cmdBigCheckModifications1,
            this.cmdBigExplore1,
            this.Separator6,
            this.cmdBigUpdate1,
            this.cmdBigSourceCommit1,
            this.cmdBigRevert1,
            this.Separator8,
            this.cmdBigUpdateAllAvailable1,
            this.cmdBigUpdateAll1,
            this.Separator7,
            this.cmdBigOptions1});
			this.uiCommandBar2.FullRow = true;
			this.uiCommandBar2.Key = "BigCommands";
			this.uiCommandBar2.Location = new System.Drawing.Point(0, 22);
			this.uiCommandBar2.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
			this.uiCommandBar2.Name = "uiCommandBar2";
			this.uiCommandBar2.RowIndex = 1;
			this.uiCommandBar2.Size = new System.Drawing.Size(984, 47);
			this.uiCommandBar2.Text = "BigCommands";
			this.uiCommandBar2.View = Janus.Windows.UI.CommandBars.View.LargeIcons;
			// 
			// cmdBigCheckSource1
			// 
			this.cmdBigCheckSource1.Key = "cmdBigCheckSource";
			this.cmdBigCheckSource1.Name = "cmdBigCheckSource1";
			// 
			// cmdBigCheckSources1
			// 
			this.cmdBigCheckSources1.Key = "cmdBigCheckSources";
			this.cmdBigCheckSources1.Name = "cmdBigCheckSources1";
			// 
			// cmdBigCheckModifications1
			// 
			this.cmdBigCheckModifications1.Key = "cmdBigCheckModifications";
			this.cmdBigCheckModifications1.Name = "cmdBigCheckModifications1";
			// 
			// cmdBigExplore1
			// 
			this.cmdBigExplore1.Key = "cmdBigExplore";
			this.cmdBigExplore1.Name = "cmdBigExplore1";
			// 
			// Separator6
			// 
			this.Separator6.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator6.Key = "Separator";
			this.Separator6.Name = "Separator6";
			// 
			// cmdBigUpdate1
			// 
			this.cmdBigUpdate1.Key = "cmdBigUpdate";
			this.cmdBigUpdate1.Name = "cmdBigUpdate1";
			// 
			// cmdBigSourceCommit1
			// 
			this.cmdBigSourceCommit1.Key = "cmdBigSourceCommit";
			this.cmdBigSourceCommit1.Name = "cmdBigSourceCommit1";
			// 
			// cmdBigRevert1
			// 
			this.cmdBigRevert1.Key = "cmdBigRevert";
			this.cmdBigRevert1.Name = "cmdBigRevert1";
			// 
			// Separator8
			// 
			this.Separator8.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator8.Key = "Separator";
			this.Separator8.Name = "Separator8";
			// 
			// cmdBigUpdateAllAvailable1
			// 
			this.cmdBigUpdateAllAvailable1.Key = "cmdBigUpdateAllAvailable";
			this.cmdBigUpdateAllAvailable1.Name = "cmdBigUpdateAllAvailable1";
			// 
			// cmdBigUpdateAll1
			// 
			this.cmdBigUpdateAll1.Key = "cmdBigUpdateAll";
			this.cmdBigUpdateAll1.Name = "cmdBigUpdateAll1";
			// 
			// Separator7
			// 
			this.Separator7.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator7.Key = "Separator";
			this.Separator7.Name = "Separator7";
			// 
			// cmdBigOptions1
			// 
			this.cmdBigOptions1.Key = "cmdBigOptions";
			this.cmdBigOptions1.Name = "cmdBigOptions1";
			// 
			// menuFile
			// 
			this.menuFile.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdNew1,
            this.cmdClose2});
			this.menuFile.Key = "menuFile";
			this.menuFile.Name = "menuFile";
			this.menuFile.Text = "&File";
			// 
			// cmdNew1
			// 
			this.cmdNew1.Key = "cmdNew";
			this.cmdNew1.Name = "cmdNew1";
			// 
			// cmdClose2
			// 
			this.cmdClose2.Key = "cmdClose";
			this.cmdClose2.Name = "cmdClose2";
			// 
			// menuSource
			// 
			this.menuSource.Key = "menuSource";
			this.menuSource.Name = "menuSource";
			this.menuSource.Text = "&Source";
			// 
			// menuMonitor
			// 
			this.menuMonitor.Key = "menuMonitor";
			this.menuMonitor.Name = "menuMonitor";
			this.menuMonitor.Text = "&Monitor";
			// 
			// menuLog
			// 
			this.menuLog.Key = "menuLog";
			this.menuLog.Name = "menuLog";
			this.menuLog.Text = "&Log";
			// 
			// menuItem
			// 
			this.menuItem.Key = "menuItem";
			this.menuItem.Name = "menuItem";
			this.menuItem.Text = "&Item";
			// 
			// menuTools
			// 
			this.menuTools.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdOptions1,
            this.cmdTSVNSettings1});
			this.menuTools.Key = "menuTools";
			this.menuTools.Name = "menuTools";
			this.menuTools.Text = "&Tools";
			// 
			// cmdOptions1
			// 
			this.cmdOptions1.Key = "cmdOptions";
			this.cmdOptions1.Name = "cmdOptions1";
			// 
			// cmdTSVNSettings1
			// 
			this.cmdTSVNSettings1.Key = "cmdTSVNSettings";
			this.cmdTSVNSettings1.Name = "cmdTSVNSettings1";
			// 
			// menuEventLog
			// 
			this.menuEventLog.Key = "menuEventLog";
			this.menuEventLog.Name = "menuEventLog";
			this.menuEventLog.Text = "&Event-Log";
			// 
			// cmdOptions
			// 
			this.cmdOptions.Image = ((System.Drawing.Image)(resources.GetObject("cmdOptions.Image")));
			this.cmdOptions.Key = "cmdOptions";
			this.cmdOptions.Name = "cmdOptions";
			this.cmdOptions.Text = "O&ptions";
			// 
			// cmdEnableUpdates
			// 
			this.cmdEnableUpdates.CommandType = Janus.Windows.UI.CommandBars.CommandType.ToggleButton;
			this.cmdEnableUpdates.Key = "cmdEnableUpdates";
			this.cmdEnableUpdates.Name = "cmdEnableUpdates";
			this.cmdEnableUpdates.Text = "&Enabled";
			// 
			// cmdOpen
			// 
			this.cmdOpen.Key = "cmdOpen";
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Text = "&Open";
			// 
			// cmdClose
			// 
			this.cmdClose.Key = "cmdClose";
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Text = "E&xit";
			// 
			// cmdCheckAllSources
			// 
			this.cmdCheckAllSources.Image = ((System.Drawing.Image)(resources.GetObject("cmdCheckAllSources.Image")));
			this.cmdCheckAllSources.Key = "cmdCheckAllSources";
			this.cmdCheckAllSources.Name = "cmdCheckAllSources";
			this.cmdCheckAllSources.Text = "Chec&k all sources for updates";
			// 
			// cmdSVNUpdateAll
			// 
			this.cmdSVNUpdateAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNUpdateAll.Image")));
			this.cmdSVNUpdateAll.Key = "cmdSVNUpdateAll";
			this.cmdSVNUpdateAll.Name = "cmdSVNUpdateAll";
			this.cmdSVNUpdateAll.Text = "Update &all sources";
			// 
			// cmdSVNUpdateAllAvailable
			// 
			this.cmdSVNUpdateAllAvailable.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNUpdateAllAvailable.Image")));
			this.cmdSVNUpdateAllAvailable.Key = "cmdSVNUpdateAllAvailable";
			this.cmdSVNUpdateAllAvailable.Name = "cmdSVNUpdateAllAvailable";
			this.cmdSVNUpdateAllAvailable.Text = "Up&date all available";
			// 
			// menuSVNUpdate
			// 
			this.menuSVNUpdate.Image = ((System.Drawing.Image)(resources.GetObject("menuSVNUpdate.Image")));
			this.menuSVNUpdate.Key = "menuSVNUpdate";
			this.menuSVNUpdate.Name = "menuSVNUpdate";
			this.menuSVNUpdate.Text = "&Update";
			// 
			// menuSVNCommit
			// 
			this.menuSVNCommit.Image = ((System.Drawing.Image)(resources.GetObject("menuSVNCommit.Image")));
			this.menuSVNCommit.Key = "menuSVNCommit";
			this.menuSVNCommit.Name = "menuSVNCommit";
			this.menuSVNCommit.Text = "&Commit";
			// 
			// menuSVNRevert
			// 
			this.menuSVNRevert.Image = ((System.Drawing.Image)(resources.GetObject("menuSVNRevert.Image")));
			this.menuSVNRevert.Key = "menuSVNRevert";
			this.menuSVNRevert.Name = "menuSVNRevert";
			this.menuSVNRevert.Text = "Re&vert";
			// 
			// menuCheckModifications
			// 
			this.menuCheckModifications.Image = ((System.Drawing.Image)(resources.GetObject("menuCheckModifications.Image")));
			this.menuCheckModifications.Key = "menuCheckModifications";
			this.menuCheckModifications.Name = "menuCheckModifications";
			this.menuCheckModifications.Text = "Check &modifications";
			// 
			// menuHelp
			// 
			this.menuHelp.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdTSVNHelp1,
            this.cmdAbout1});
			this.menuHelp.Key = "menuHelp";
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.Text = "&Help";
			// 
			// cmdTSVNHelp1
			// 
			this.cmdTSVNHelp1.Key = "cmdTSVNHelp";
			this.cmdTSVNHelp1.Name = "cmdTSVNHelp1";
			// 
			// cmdAbout1
			// 
			this.cmdAbout1.Key = "cmdAbout";
			this.cmdAbout1.Name = "cmdAbout1";
			// 
			// cmdAbout
			// 
			this.cmdAbout.Image = ((System.Drawing.Image)(resources.GetObject("cmdAbout.Image")));
			this.cmdAbout.Key = "cmdAbout";
			this.cmdAbout.Name = "cmdAbout";
			this.cmdAbout.Text = "&About";
			// 
			// cmdNew
			// 
			this.cmdNew.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdNewSource1,
            this.cmdNewMonitor1});
			this.cmdNew.Key = "cmdNew";
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Text = "&New";
			// 
			// cmdNewSource1
			// 
			this.cmdNewSource1.Key = "cmdNewSource";
			this.cmdNewSource1.Name = "cmdNewSource1";
			// 
			// cmdNewMonitor1
			// 
			this.cmdNewMonitor1.Key = "cmdNewMonitor";
			this.cmdNewMonitor1.Name = "cmdNewMonitor1";
			// 
			// cmdNewSource
			// 
			this.cmdNewSource.Image = ((System.Drawing.Image)(resources.GetObject("cmdNewSource.Image")));
			this.cmdNewSource.Key = "cmdNewSource";
			this.cmdNewSource.Name = "cmdNewSource";
			this.cmdNewSource.Text = "&Source...";
			// 
			// cmdNewMonitor
			// 
			this.cmdNewMonitor.Image = ((System.Drawing.Image)(resources.GetObject("cmdNewMonitor.Image")));
			this.cmdNewMonitor.Key = "cmdNewMonitor";
			this.cmdNewMonitor.Name = "cmdNewMonitor";
			this.cmdNewMonitor.Text = "&Monitor...";
			// 
			// cmdTSVNSettings
			// 
			this.cmdTSVNSettings.Key = "cmdTSVNSettings";
			this.cmdTSVNSettings.Name = "cmdTSVNSettings";
			this.cmdTSVNSettings.Text = "TortoiseSVN Settings";
			// 
			// cmdTSVNHelp
			// 
			this.cmdTSVNHelp.Key = "cmdTSVNHelp";
			this.cmdTSVNHelp.Name = "cmdTSVNHelp";
			this.cmdTSVNHelp.Text = "TortoiseSVN Help";
			// 
			// menuDebug
			// 
			this.menuDebug.CategoryName = "Debug";
			this.menuDebug.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.menuDialogs1,
            this.Separator9,
            this.cmdGenerateError1,
            this.cmdGenerateInvokeError1,
            this.menuTestGridLayouts1});
			this.menuDebug.Key = "menuDebug";
			this.menuDebug.Name = "menuDebug";
			this.menuDebug.Text = "&Debug";
			this.menuDebug.Visible = Janus.Windows.UI.InheritableBoolean.False;
			// 
			// menuDialogs1
			// 
			this.menuDialogs1.Key = "menuDialogs";
			this.menuDialogs1.Name = "menuDialogs1";
			// 
			// Separator9
			// 
			this.Separator9.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator9.Key = "Separator";
			this.Separator9.Name = "Separator9";
			// 
			// cmdGenerateError1
			// 
			this.cmdGenerateError1.Key = "cmdGenerateError";
			this.cmdGenerateError1.Name = "cmdGenerateError1";
			// 
			// cmdGenerateInvokeError1
			// 
			this.cmdGenerateInvokeError1.Key = "cmdGenerateInvokeError";
			this.cmdGenerateInvokeError1.Name = "cmdGenerateInvokeError1";
			// 
			// menuTestGridLayouts1
			// 
			this.menuTestGridLayouts1.Key = "menuTestGridLayouts";
			this.menuTestGridLayouts1.Name = "menuTestGridLayouts1";
			// 
			// cmdGenerateError
			// 
			this.cmdGenerateError.CategoryName = "Debug";
			this.cmdGenerateError.Key = "cmdGenerateError";
			this.cmdGenerateError.Name = "cmdGenerateError";
			this.cmdGenerateError.Text = "&Generate Error";
			// 
			// cmdGenerateInvokeError
			// 
			this.cmdGenerateInvokeError.CategoryName = "Debug";
			this.cmdGenerateInvokeError.Key = "cmdGenerateInvokeError";
			this.cmdGenerateInvokeError.Name = "cmdGenerateInvokeError";
			this.cmdGenerateInvokeError.Text = "Generate &Invoke Error";
			// 
			// cmdBigCheckSources
			// 
			this.cmdBigCheckSources.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigCheckSources.Image")));
			this.cmdBigCheckSources.Key = "cmdBigCheckSources";
			this.cmdBigCheckSources.Name = "cmdBigCheckSources";
			this.cmdBigCheckSources.Text = "Check All";
			this.cmdBigCheckSources.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigCheckSource
			// 
			this.cmdBigCheckSource.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigCheckSource.Image")));
			this.cmdBigCheckSource.Key = "cmdBigCheckSource";
			this.cmdBigCheckSource.Name = "cmdBigCheckSource";
			this.cmdBigCheckSource.Text = "Check Source";
			this.cmdBigCheckSource.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigCheckModifications
			// 
			this.cmdBigCheckModifications.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigCheckModifications.Image")));
			this.cmdBigCheckModifications.Key = "cmdBigCheckModifications";
			this.cmdBigCheckModifications.Name = "cmdBigCheckModifications";
			this.cmdBigCheckModifications.Text = "Check Modifications";
			this.cmdBigCheckModifications.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigExplore
			// 
			this.cmdBigExplore.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigExplore.Image")));
			this.cmdBigExplore.Key = "cmdBigExplore";
			this.cmdBigExplore.Name = "cmdBigExplore";
			this.cmdBigExplore.Text = "Explore";
			this.cmdBigExplore.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigUpdate
			// 
			this.cmdBigUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigUpdate.Image")));
			this.cmdBigUpdate.Key = "cmdBigUpdate";
			this.cmdBigUpdate.Name = "cmdBigUpdate";
			this.cmdBigUpdate.Text = "Update";
			this.cmdBigUpdate.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigUpdateAll
			// 
			this.cmdBigUpdateAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigUpdateAll.Image")));
			this.cmdBigUpdateAll.Key = "cmdBigUpdateAll";
			this.cmdBigUpdateAll.Name = "cmdBigUpdateAll";
			this.cmdBigUpdateAll.Text = "Update All";
			this.cmdBigUpdateAll.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigUpdateAllAvailable
			// 
			this.cmdBigUpdateAllAvailable.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigUpdateAllAvailable.Image")));
			this.cmdBigUpdateAllAvailable.Key = "cmdBigUpdateAllAvailable";
			this.cmdBigUpdateAllAvailable.Name = "cmdBigUpdateAllAvailable";
			this.cmdBigUpdateAllAvailable.Text = "Update Available";
			this.cmdBigUpdateAllAvailable.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigOptions
			// 
			this.cmdBigOptions.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigOptions.Image")));
			this.cmdBigOptions.Key = "cmdBigOptions";
			this.cmdBigOptions.Name = "cmdBigOptions";
			this.cmdBigOptions.Text = "Options";
			this.cmdBigOptions.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigSourceCommit
			// 
			this.cmdBigSourceCommit.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigSourceCommit.Image")));
			this.cmdBigSourceCommit.Key = "cmdBigSourceCommit";
			this.cmdBigSourceCommit.Name = "cmdBigSourceCommit";
			this.cmdBigSourceCommit.Text = "Commit";
			this.cmdBigSourceCommit.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// cmdBigRevert
			// 
			this.cmdBigRevert.Image = ((System.Drawing.Image)(resources.GetObject("cmdBigRevert.Image")));
			this.cmdBigRevert.Key = "cmdBigRevert";
			this.cmdBigRevert.Name = "cmdBigRevert";
			this.cmdBigRevert.Text = "Revert";
			this.cmdBigRevert.TextImageRelation = Janus.Windows.UI.CommandBars.TextImageRelation.ImageAboveText;
			// 
			// menuDialogs
			// 
			this.menuDialogs.CategoryName = "Debug";
			this.menuDialogs.Key = "menuDialogs";
			this.menuDialogs.Name = "menuDialogs";
			this.menuDialogs.Text = "Dialogs";
			// 
			// menuTestGridLayouts
			// 
			this.menuTestGridLayouts.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdTestSetLogEntriesGridLayout1});
			this.menuTestGridLayouts.Key = "menuTestGridLayouts";
			this.menuTestGridLayouts.Name = "menuTestGridLayouts";
			this.menuTestGridLayouts.Text = "Grid Layouts";
			// 
			// cmdTestSetLogEntriesGridLayout1
			// 
			this.cmdTestSetLogEntriesGridLayout1.Key = "cmdTestSetLogEntriesGridLayout";
			this.cmdTestSetLogEntriesGridLayout1.Name = "cmdTestSetLogEntriesGridLayout1";
			// 
			// cmdTestSetLogEntriesGridLayout
			// 
			this.cmdTestSetLogEntriesGridLayout.Key = "cmdTestSetLogEntriesGridLayout";
			this.cmdTestSetLogEntriesGridLayout.Name = "cmdTestSetLogEntriesGridLayout";
			this.cmdTestSetLogEntriesGridLayout.Text = "Set Log Entries Grid Layout";
			// 
			// uiContextMenu1
			// 
			this.uiContextMenu1.CommandManager = this.uiCommandManager1;
			this.uiContextMenu1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdEnableUpdates1,
            this.Separator1,
            this.menuCheckModifications1,
            this.menuSVNUpdate1,
            this.cmdSVNUpdateAllUnread1,
            this.cmdSVNUpdateAll1,
            this.menuSVNCommit1,
            this.menuSVNRevert1,
            this.Separator4,
            this.cmdCheckAllSources1,
            this.Separator3,
            this.cmdOptions2,
            this.cmdOpen1,
            this.Separator2,
            this.cmdClose1});
			this.uiContextMenu1.Key = "ContextMenu1";
			// 
			// cmdEnableUpdates1
			// 
			this.cmdEnableUpdates1.Key = "cmdEnableUpdates";
			this.cmdEnableUpdates1.Name = "cmdEnableUpdates1";
			// 
			// Separator1
			// 
			this.Separator1.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator1.Key = "Separator";
			this.Separator1.Name = "Separator1";
			// 
			// menuCheckModifications1
			// 
			this.menuCheckModifications1.Key = "menuCheckModifications";
			this.menuCheckModifications1.Name = "menuCheckModifications1";
			// 
			// menuSVNUpdate1
			// 
			this.menuSVNUpdate1.Key = "menuSVNUpdate";
			this.menuSVNUpdate1.Name = "menuSVNUpdate1";
			// 
			// cmdSVNUpdateAllUnread1
			// 
			this.cmdSVNUpdateAllUnread1.Key = "cmdSVNUpdateAllAvailable";
			this.cmdSVNUpdateAllUnread1.Name = "cmdSVNUpdateAllUnread1";
			// 
			// cmdSVNUpdateAll1
			// 
			this.cmdSVNUpdateAll1.Key = "cmdSVNUpdateAll";
			this.cmdSVNUpdateAll1.Name = "cmdSVNUpdateAll1";
			// 
			// menuSVNCommit1
			// 
			this.menuSVNCommit1.Key = "menuSVNCommit";
			this.menuSVNCommit1.Name = "menuSVNCommit1";
			// 
			// menuSVNRevert1
			// 
			this.menuSVNRevert1.Key = "menuSVNRevert";
			this.menuSVNRevert1.Name = "menuSVNRevert1";
			// 
			// Separator4
			// 
			this.Separator4.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator4.Key = "Separator";
			this.Separator4.Name = "Separator4";
			// 
			// cmdCheckAllSources1
			// 
			this.cmdCheckAllSources1.Key = "cmdCheckAllSources";
			this.cmdCheckAllSources1.Name = "cmdCheckAllSources1";
			// 
			// Separator3
			// 
			this.Separator3.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator3.Key = "Separator";
			this.Separator3.Name = "Separator3";
			// 
			// cmdOptions2
			// 
			this.cmdOptions2.Key = "cmdOptions";
			this.cmdOptions2.Name = "cmdOptions2";
			// 
			// cmdOpen1
			// 
			this.cmdOpen1.DefaultItem = Janus.Windows.UI.InheritableBoolean.True;
			this.cmdOpen1.Key = "cmdOpen";
			this.cmdOpen1.Name = "cmdOpen1";
			// 
			// Separator2
			// 
			this.Separator2.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator2.Key = "Separator";
			this.Separator2.Name = "Separator2";
			// 
			// cmdClose1
			// 
			this.cmdClose1.Key = "cmdClose";
			this.cmdClose1.Name = "cmdClose1";
			// 
			// LeftRebar1
			// 
			this.LeftRebar1.CommandManager = this.uiCommandManager1;
			this.LeftRebar1.Dock = System.Windows.Forms.DockStyle.Left;
			this.LeftRebar1.Location = new System.Drawing.Point(0, 26);
			this.LeftRebar1.Name = "LeftRebar1";
			this.LeftRebar1.Size = new System.Drawing.Size(0, 393);
			// 
			// RightRebar1
			// 
			this.RightRebar1.CommandManager = this.uiCommandManager1;
			this.RightRebar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.RightRebar1.Location = new System.Drawing.Point(831, 26);
			this.RightRebar1.Name = "RightRebar1";
			this.RightRebar1.Size = new System.Drawing.Size(0, 393);
			// 
			// TopRebar1
			// 
			this.TopRebar1.CommandBars.AddRange(new Janus.Windows.UI.CommandBars.UICommandBar[] {
            this.uiCommandBar1,
            this.uiCommandBar2});
			this.TopRebar1.CommandManager = this.uiCommandManager1;
			this.TopRebar1.Controls.Add(this.uiCommandBar1);
			this.TopRebar1.Controls.Add(this.uiCommandBar2);
			this.TopRebar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopRebar1.Location = new System.Drawing.Point(0, 0);
			this.TopRebar1.Name = "TopRebar1";
			this.TopRebar1.Size = new System.Drawing.Size(984, 69);
			// 
			// Separator5
			// 
			this.Separator5.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator5.Key = "Separator";
			this.Separator5.Name = "Separator5";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(984, 762);
			this.Controls.Add(this.uiPanelLog);
			this.Controls.Add(this.uiPanelUpdatesGridContainer);
			this.Controls.Add(this.uiPanelPaths);
			this.Controls.Add(this.uiPanelLeft);
			this.Controls.Add(this.uiStatusBar1);
			this.Controls.Add(this.TopRebar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "MainForm";
			this.Text = "SVN-Monitor";
			((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiPanelEventLog)).EndInit();
			this.uiPanelEventLog.ResumeLayout(false);
			this.uiPanelEventLogCaptionContainer.ResumeLayout(false);
			this.uiPanelEventLogContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiPanelLeft)).EndInit();
			this.uiPanelLeft.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiPanelSources)).EndInit();
			this.uiPanelSources.ResumeLayout(false);
			this.uiPanelSourcesCaptionContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sourcesPanel1)).EndInit();
			this.uiPanelSourcesContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiPanelMonitors)).EndInit();
			this.uiPanelMonitors.ResumeLayout(false);
			this.uiPanelMonitorsCaptionContainer.ResumeLayout(false);
			this.uiPanelMonitorsContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiPanelPaths)).EndInit();
			this.uiPanelPaths.ResumeLayout(false);
			this.uiPanelPathsCaptionContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pathsPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.logEntriesPanel1)).EndInit();
			this.uiPanelPathsContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiPanelUpdatesGridContainer)).EndInit();
			this.uiPanelUpdatesGridContainer.ResumeLayout(false);
			this.uiPanelUpdatesGridContainerContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiPanelLog)).EndInit();
			this.uiPanelLog.ResumeLayout(false);
			this.uiPanelLogCaptionContainer.ResumeLayout(false);
			this.uiPanelLogContainer.ResumeLayout(false);
			this.uiStatusBar1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).EndInit();
			this.TopRebar1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private SVNMonitor.View.Controls.AnimationProgressBar animationProgressBar1;
		private Janus.Windows.UI.CommandBars.UIRebar BottomRebar1;
		private Janus.Windows.EditControls.UICheckBox checkGroupByBox;
		private Janus.Windows.UI.CommandBars.UICommand cmdAbout;
		private Janus.Windows.UI.CommandBars.UICommand cmdAbout1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigCheckModifications;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigCheckModifications1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigCheckSource;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigCheckSource1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigCheckSources;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigCheckSources1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigExplore;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigExplore1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigOptions;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigOptions1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigRevert;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigRevert1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigSourceCommit;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigSourceCommit1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigUpdate;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigUpdate1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigUpdateAll;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigUpdateAll1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigUpdateAllAvailable;
		private Janus.Windows.UI.CommandBars.UICommand cmdBigUpdateAllAvailable1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCheckAllSources;
		private Janus.Windows.UI.CommandBars.UICommand cmdCheckAllSources1;
		private Janus.Windows.UI.CommandBars.UICommand cmdClose;
		private Janus.Windows.UI.CommandBars.UICommand cmdClose1;
		private Janus.Windows.UI.CommandBars.UICommand cmdClose2;
		private Janus.Windows.UI.CommandBars.UICommand cmdEnableUpdates;
		private Janus.Windows.UI.CommandBars.UICommand cmdEnableUpdates1;
		private Janus.Windows.UI.CommandBars.UICommand cmdGenerateError;
		private Janus.Windows.UI.CommandBars.UICommand cmdGenerateError1;
		private Janus.Windows.UI.CommandBars.UICommand cmdGenerateInvokeError;
		private Janus.Windows.UI.CommandBars.UICommand cmdGenerateInvokeError1;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew1;
		private Janus.Windows.UI.CommandBars.UICommand cmdNewMonitor;
		private Janus.Windows.UI.CommandBars.UICommand cmdNewMonitor1;
		private Janus.Windows.UI.CommandBars.UICommand cmdNewSource;
		private Janus.Windows.UI.CommandBars.UICommand cmdNewSource1;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpen;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpen1;
		private Janus.Windows.UI.CommandBars.UICommand cmdOptions;
		private Janus.Windows.UI.CommandBars.UICommand cmdOptions1;
		private Janus.Windows.UI.CommandBars.UICommand cmdOptions2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdateAll;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdateAll1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdateAllAvailable;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdateAllUnread1;
		private Janus.Windows.UI.CommandBars.UICommand cmdTestSetLogEntriesGridLayout;
		private Janus.Windows.UI.CommandBars.UICommand cmdTestSetLogEntriesGridLayout1;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNHelp;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNHelp1;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNSettings;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNSettings1;
		private SVNMonitor.View.Controls.EventLogEntrySearchTextBox eventLogEntrySearchTextBox1;
		private SVNMonitor.View.Panels.EventLogPanel eventLogPanel1;
		private System.Windows.Forms.FormWindowState lastWindowState;
		private Janus.Windows.UI.CommandBars.UIRebar LeftRebar1;
		private SVNMonitor.View.Panels.LogEntriesPanel logEntriesPanel1;
		private Janus.Windows.UI.CommandBars.UICommand menuCheckModifications;
		private Janus.Windows.UI.CommandBars.UICommand menuCheckModifications1;
		private Janus.Windows.UI.CommandBars.UICommand menuDebug;
		private Janus.Windows.UI.CommandBars.UICommand menuDebug1;
		private Janus.Windows.UI.CommandBars.UICommand menuDialogs;
		private Janus.Windows.UI.CommandBars.UICommand menuDialogs1;
		private Janus.Windows.UI.CommandBars.UICommand menuEventLog;
		private Janus.Windows.UI.CommandBars.UICommand menuEventLog1;
		private Janus.Windows.UI.CommandBars.UICommand menuFile;
		private Janus.Windows.UI.CommandBars.UICommand menuFile1;
		private Janus.Windows.UI.CommandBars.UICommand menuHelp;
		private Janus.Windows.UI.CommandBars.UICommand menuHelp1;
		private Janus.Windows.UI.CommandBars.UICommand menuItem;
		private Janus.Windows.UI.CommandBars.UICommand menuItem1;
		private Janus.Windows.UI.CommandBars.UICommand menuLog;
		private Janus.Windows.UI.CommandBars.UICommand menuLog1;
		private Janus.Windows.UI.CommandBars.UICommand menuMonitor;
		private Janus.Windows.UI.CommandBars.UICommand menuMonitor1;
		private Janus.Windows.UI.CommandBars.UICommand menuSource;
		private Janus.Windows.UI.CommandBars.UICommand menuSource1;
		private Janus.Windows.UI.CommandBars.UICommand menuSVNCommit;
		private Janus.Windows.UI.CommandBars.UICommand menuSVNCommit1;
		private Janus.Windows.UI.CommandBars.UICommand menuSVNRevert;
		private Janus.Windows.UI.CommandBars.UICommand menuSVNRevert1;
		private Janus.Windows.UI.CommandBars.UICommand menuSVNUpdate;
		private Janus.Windows.UI.CommandBars.UICommand menuSVNUpdate1;
		private Janus.Windows.UI.CommandBars.UICommand menuTestGridLayouts;
		private Janus.Windows.UI.CommandBars.UICommand menuTestGridLayouts1;
		private Janus.Windows.UI.CommandBars.UICommand menuTools;
		private Janus.Windows.UI.CommandBars.UICommand menuTools1;
		private SVNMonitor.View.Controls.MonitorSearchTextBox monitorSearchTextBox1;
		private SVNMonitor.View.Panels.MonitorsPanel monitorsPanel1;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private SVNMonitor.View.Panels.PathsPanel pathsPanel1;
		private Janus.Windows.UI.CommandBars.UIRebar RightRebar1;
		private Janus.Windows.UI.CommandBars.UICommand Separator1;
		private Janus.Windows.UI.CommandBars.UICommand Separator2;
		private Janus.Windows.UI.CommandBars.UICommand Separator3;
		private Janus.Windows.UI.CommandBars.UICommand Separator4;
		private Janus.Windows.UI.CommandBars.UICommand Separator5;
		private Janus.Windows.UI.CommandBars.UICommand Separator6;
		private Janus.Windows.UI.CommandBars.UICommand Separator7;
		private Janus.Windows.UI.CommandBars.UICommand Separator8;
		private Janus.Windows.UI.CommandBars.UICommand Separator9;
		private SVNMonitor.View.Controls.SourceSearchTextBox sourceSearchTextBox1;
		private SVNMonitor.View.Panels.SourcesPanel sourcesPanel1;
		private SVNMonitor.View.Controls.SVNLogEntrySearchTextBox svnLogEntrySearchTextBox1;
		private SVNMonitor.View.Controls.SVNPathSearchTextBox svnPathSearchTextBox1;
		private Janus.Windows.UI.CommandBars.UIRebar TopRebar1;
		private Janus.Windows.UI.CommandBars.UICommandBar uiCommandBar1;
		private Janus.Windows.UI.CommandBars.UICommandBar uiCommandBar2;
		private Janus.Windows.UI.CommandBars.UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
		private Janus.Windows.UI.Dock.UIPanel uiPanelEventLog;
		private Janus.Windows.UI.Dock.UIPanelCaptionContainer uiPanelEventLogCaptionContainer;
		private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanelEventLogContainer;
		private Janus.Windows.UI.Dock.UIPanelGroup uiPanelLeft;
		private Janus.Windows.UI.Dock.UIPanel uiPanelLog;
		private Janus.Windows.UI.Dock.UIPanelCaptionContainer uiPanelLogCaptionContainer;
		private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanelLogContainer;
		private Janus.Windows.UI.Dock.UIPanelManager uiPanelManager1;
		private Janus.Windows.UI.Dock.UIPanel uiPanelMonitors;
		private Janus.Windows.UI.Dock.UIPanelCaptionContainer uiPanelMonitorsCaptionContainer;
		private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanelMonitorsContainer;
		private Janus.Windows.UI.Dock.UIPanel uiPanelPaths;
		private Janus.Windows.UI.Dock.UIPanelCaptionContainer uiPanelPathsCaptionContainer;
		private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanelPathsContainer;
		private Janus.Windows.UI.Dock.UIPanel uiPanelSources;
		private Janus.Windows.UI.Dock.UIPanelCaptionContainer uiPanelSourcesCaptionContainer;
		private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanelSourcesContainer;
		private Janus.Windows.UI.Dock.UIPanel uiPanelUpdatesGridContainer;
		private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanelUpdatesGridContainerContainer;
		private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
		private SVNMonitor.View.Panels.UpdatesGridContainer updatesGridContainer1;
	}
}
