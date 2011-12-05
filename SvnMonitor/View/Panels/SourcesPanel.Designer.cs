namespace SVNMonitor.View.Panels
{
	internal partial class SourcesPanel
	{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
 
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
 
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourcesPanel));
			this.cmdNew = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.cmdUpdate = new Janus.Windows.UI.CommandBars.UICommand("cmdUpdate");
			this.cmdUpdateAll = new Janus.Windows.UI.CommandBars.UICommand("cmdUpdateAll");
			this.cmdMoveUp = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.uiCommandManager1 = new Janus.Windows.UI.CommandBars.UICommandManager(this.components);
			this.BottomRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.uiCommandBar1 = new Janus.Windows.UI.CommandBars.UICommandBar();
			this.cmdNew1 = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit1 = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete1 = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.Separator3 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdSVNModifications1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNModifications");
			this.cmdShowLog1 = new Janus.Windows.UI.CommandBars.UICommand("cmdShowLog");
			this.cmdSVNUpdate2 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdSVNCommit2 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNCommit");
			this.cmdSVNRevert1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNRevert");
			this.Separator7 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdUpdate2 = new Janus.Windows.UI.CommandBars.UICommand("cmdUpdate");
			this.cmdUpdateAll2 = new Janus.Windows.UI.CommandBars.UICommand("cmdUpdateAll");
			this.Separator10 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdMoveUp1 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown1 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.Separator6 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.menuWizards2 = new Janus.Windows.UI.CommandBars.UICommand("menuWizards");
			this.menuWizards = new Janus.Windows.UI.CommandBars.UICommand("menuWizards");
			this.cmdSVNUpdate = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdSVNCommit = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNCommit");
			this.cmdSVNRevert = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNRevert");
			this.cmdSVNModifications = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNModifications");
			this.cmdRefresh = new Janus.Windows.UI.CommandBars.UICommand("cmdRefresh");
			this.cmdEnabled = new Janus.Windows.UI.CommandBars.UICommand("cmdEnabled");
			this.cmdExplore = new Janus.Windows.UI.CommandBars.UICommand("cmdExplore");
			this.cmdClearError = new Janus.Windows.UI.CommandBars.UICommand("cmdClearError");
			this.cmdShowLog = new Janus.Windows.UI.CommandBars.UICommand("cmdShowLog");
			this.menuClipboard = new Janus.Windows.UI.CommandBars.UICommand("menuClipboard");
			this.cmdCopySourceName1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceName");
			this.cmdCopySourcePath1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourcePath");
			this.cmdCopySourceUrl1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceURL");
			this.cmdCopySourceError1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceError");
			this.cmdCopySourceModifiedItems1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceModifiedItems");
			this.cmdCopySourceConflictedItems1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceConflictedItems");
			this.cmdCopySourceUnversionedItems1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceUnversionedItems");
			this.cmdCopySourceName = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceName");
			this.cmdCopySourcePath = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourcePath");
			this.cmdCopySourceURL = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceURL");
			this.cmdCopySourceError = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceError");
			this.cmdCopySourceModifiedItems = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceModifiedItems");
			this.cmdCopySourceUnversionedItems = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceUnversionedItems");
			this.cmdCopySourceConflictedItems = new Janus.Windows.UI.CommandBars.UICommand("cmdCopySourceConflictedItems");
			this.menuAdditionalTortoiseCommands = new Janus.Windows.UI.CommandBars.UICommand("menuAdditionalTortoiseCommands");
			this.cmdCheckout1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCheckout");
			this.cmdRepoBrowser1 = new Janus.Windows.UI.CommandBars.UICommand("cmdRepoBrowser");
			this.cmdRevisionGraph1 = new Janus.Windows.UI.CommandBars.UICommand("cmdRevisionGraph");
			this.cmdResolve1 = new Janus.Windows.UI.CommandBars.UICommand("cmdResolve");
			this.cmdCleanUp1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCleanUp");
			this.cmdDeleteUnversioned1 = new Janus.Windows.UI.CommandBars.UICommand("cmdDeleteUnversioned");
			this.cmdLock2 = new Janus.Windows.UI.CommandBars.UICommand("cmdLock");
			this.cmdUnlock2 = new Janus.Windows.UI.CommandBars.UICommand("cmdUnlock");
			this.Separator13 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdBranchTag1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBranchTag");
			this.cmdSwitch1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSwitch");
			this.cmdMerge1 = new Janus.Windows.UI.CommandBars.UICommand("cmdMerge");
			this.cmdReintegrate1 = new Janus.Windows.UI.CommandBars.UICommand("cmdReintegrate");
			this.cmdExport1 = new Janus.Windows.UI.CommandBars.UICommand("cmdExport");
			this.cmdRelocate1 = new Janus.Windows.UI.CommandBars.UICommand("cmdRelocate");
			this.Separator4 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdCreatePatch2 = new Janus.Windows.UI.CommandBars.UICommand("cmdCreatePatch");
			this.cmdApplyPatch2 = new Janus.Windows.UI.CommandBars.UICommand("cmdApplyPatch");
			this.Separator11 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdProperties2 = new Janus.Windows.UI.CommandBars.UICommand("cmdProperties");
			this.Separator14 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdTSVNSettings2 = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNSettings");
			this.cmdTSVNHelp2 = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNHelp");
			this.cmdRepoBrowser = new Janus.Windows.UI.CommandBars.UICommand("cmdRepoBrowser");
			this.cmdRevisionGraph = new Janus.Windows.UI.CommandBars.UICommand("cmdRevisionGraph");
			this.cmdCleanUp = new Janus.Windows.UI.CommandBars.UICommand("cmdCleanUp");
			this.cmdExport = new Janus.Windows.UI.CommandBars.UICommand("cmdExport");
			this.cmdSwitch = new Janus.Windows.UI.CommandBars.UICommand("cmdSwitch");
			this.cmdRelocate = new Janus.Windows.UI.CommandBars.UICommand("cmdRelocate");
			this.cmdProperties = new Janus.Windows.UI.CommandBars.UICommand("cmdProperties");
			this.cmdCheckout = new Janus.Windows.UI.CommandBars.UICommand("cmdCheckout");
			this.cmdResolve = new Janus.Windows.UI.CommandBars.UICommand("cmdResolve");
			this.cmdMerge = new Janus.Windows.UI.CommandBars.UICommand("cmdMerge");
			this.cmdReintegrate = new Janus.Windows.UI.CommandBars.UICommand("cmdReintegrate");
			this.cmdBranchTag = new Janus.Windows.UI.CommandBars.UICommand("cmdBranchTag");
			this.cmdCreatePatch = new Janus.Windows.UI.CommandBars.UICommand("cmdCreatePatch");
			this.cmdApplyPatch = new Janus.Windows.UI.CommandBars.UICommand("cmdApplyPatch");
			this.cmdLock = new Janus.Windows.UI.CommandBars.UICommand("cmdLock");
			this.cmdUnlock = new Janus.Windows.UI.CommandBars.UICommand("cmdUnlock");
			this.cmdDeleteUnversioned = new Janus.Windows.UI.CommandBars.UICommand("cmdDeleteUnversioned");
			this.cmdTSVNSettings = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNSettings");
			this.cmdTSVNHelp = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNHelp");
			this.menuEvenMore = new Janus.Windows.UI.CommandBars.UICommand("menuEvenMore");
			this.cmdCreatePatch1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCreatePatch");
			this.cmdApplyPatch1 = new Janus.Windows.UI.CommandBars.UICommand("cmdApplyPatch");
			this.cmdProperties1 = new Janus.Windows.UI.CommandBars.UICommand("cmdProperties");
			this.Separator12 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdTSVNSettings1 = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNSettings");
			this.cmdTSVNHelp1 = new Janus.Windows.UI.CommandBars.UICommand("cmdTSVNHelp");
			this.filterModified = new Janus.Windows.UI.CommandBars.UICommand("filterModified");
			this.filterNotUpToDate = new Janus.Windows.UI.CommandBars.UICommand("filterNotUpToDate");
			this.filterAll = new Janus.Windows.UI.CommandBars.UICommand("filterAll");
			this.menuShow = new Janus.Windows.UI.CommandBars.UICommand("menuShow");
			this.filterAll1 = new Janus.Windows.UI.CommandBars.UICommand("filterAll");
			this.Separator15 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.filterModified3 = new Janus.Windows.UI.CommandBars.UICommand("filterModified");
			this.filterNotUpToDate3 = new Janus.Windows.UI.CommandBars.UICommand("filterNotUpToDate");
			this.filterEnabled1 = new Janus.Windows.UI.CommandBars.UICommand("filterEnabled");
			this.cmdClearAllErrors = new Janus.Windows.UI.CommandBars.UICommand("cmdClearAllErrors");
			this.filterEnabled = new Janus.Windows.UI.CommandBars.UICommand("filterEnabled");
			this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			this.cmdEnabled1 = new Janus.Windows.UI.CommandBars.UICommand("cmdEnabled");
			this.cmdClearError1 = new Janus.Windows.UI.CommandBars.UICommand("cmdClearError");
			this.cmdClearAllErrors1 = new Janus.Windows.UI.CommandBars.UICommand("cmdClearAllErrors");
			this.cmdExplore1 = new Janus.Windows.UI.CommandBars.UICommand("cmdExplore");
			this.cmdNew2 = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit2 = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete2 = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.menuClipboard1 = new Janus.Windows.UI.CommandBars.UICommand("menuClipboard");
			this.Separator2 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdSVNModifications2 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNModifications");
			this.cmdShowLog2 = new Janus.Windows.UI.CommandBars.UICommand("cmdShowLog");
			this.cmdSVNUpdate1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdSVNCommit1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNCommit");
			this.cmdSVNRevert2 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNRevert");
			this.menuAdditionalTortoiseCommands1 = new Janus.Windows.UI.CommandBars.UICommand("menuAdditionalTortoiseCommands");
			this.Separator8 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdUpdate1 = new Janus.Windows.UI.CommandBars.UICommand("cmdUpdate");
			this.cmdUpdateAll1 = new Janus.Windows.UI.CommandBars.UICommand("cmdUpdateAll");
			this.Separator9 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdRefresh1 = new Janus.Windows.UI.CommandBars.UICommand("cmdRefresh");
			this.Separator1 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdMoveUp2 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown2 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.menuShow2 = new Janus.Windows.UI.CommandBars.UICommand("menuShow");
			this.Separator5 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.menuWizards1 = new Janus.Windows.UI.CommandBars.UICommand("menuWizards");
			this.LeftRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.RightRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.TopRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.sourcesExplorerBar1 = new SVNMonitor.View.Controls.SourcesExplorerBar();
			this.panel1 = new System.Windows.Forms.Panel();
			this.linkShowAllSources = new System.Windows.Forms.LinkLabel();
			this.filterModified1 = new Janus.Windows.UI.CommandBars.UICommand("filterModified");
			this.filterNotUpToDate2 = new Janus.Windows.UI.CommandBars.UICommand("filterNotUpToDate");
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).BeginInit();
			this.TopRebar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sourcesExplorerBar1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdNew
			// 
			this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
			this.cmdNew.Key = "cmdNew";
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Text = "New Source";
			this.cmdNew.ToolTipText = "New source";
			// 
			// cmdEdit
			// 
			this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
			this.cmdEdit.Key = "cmdEdit";
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.Text = "Properties";
			this.cmdEdit.ToolTipText = "Properties";
			// 
			// cmdDelete
			// 
			this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
			this.cmdDelete.Key = "cmdDelete";
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.ToolTipText = "Delete source";
			// 
			// cmdUpdate
			// 
			this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
			this.cmdUpdate.Key = "cmdUpdate";
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Text = "Check for updates";
			this.cmdUpdate.ToolTipText = "Check for updates";
			// 
			// cmdUpdateAll
			// 
			this.cmdUpdateAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdateAll.Image")));
			this.cmdUpdateAll.Key = "cmdUpdateAll";
			this.cmdUpdateAll.Name = "cmdUpdateAll";
			this.cmdUpdateAll.Text = "Check all for updates";
			this.cmdUpdateAll.ToolTipText = "Check all for updates";
			// 
			// cmdMoveUp
			// 
			this.cmdMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveUp.Image")));
			this.cmdMoveUp.Key = "cmdMoveUp";
			this.cmdMoveUp.Name = "cmdMoveUp";
			this.cmdMoveUp.Text = "Move Up";
			this.cmdMoveUp.ToolTipText = "Move source up";
			// 
			// cmdMoveDown
			// 
			this.cmdMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveDown.Image")));
			this.cmdMoveDown.Key = "cmdMoveDown";
			this.cmdMoveDown.Name = "cmdMoveDown";
			this.cmdMoveDown.Text = "Move Down";
			this.cmdMoveDown.ToolTipText = "Move source down";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "wc.png");
			this.imageList1.Images.SetKeyName(1, "wc.updating.png");
			this.imageList1.Images.SetKeyName(2, "repo.error.png");
			this.imageList1.Images.SetKeyName(3, "repo.png");
			this.imageList1.Images.SetKeyName(4, "repo.updating.png");
			this.imageList1.Images.SetKeyName(5, "wc.downdate.modified.png");
			this.imageList1.Images.SetKeyName(6, "wc.downdate.png");
			this.imageList1.Images.SetKeyName(7, "wc.error.png");
			this.imageList1.Images.SetKeyName(8, "wc.modified.png");
			// 
			// uiCommandManager1
			// 
			this.uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.BottomRebar = this.BottomRebar1;
			this.uiCommandManager1.CommandBars.AddRange(new Janus.Windows.UI.CommandBars.UICommandBar[] {
            this.uiCommandBar1});
			this.uiCommandManager1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdNew,
            this.cmdEdit,
            this.cmdDelete,
            this.cmdUpdate,
            this.cmdUpdateAll,
            this.cmdMoveUp,
            this.cmdMoveDown,
            this.menuWizards,
            this.cmdSVNUpdate,
            this.cmdSVNCommit,
            this.cmdSVNRevert,
            this.cmdSVNModifications,
            this.cmdRefresh,
            this.cmdEnabled,
            this.cmdExplore,
            this.cmdClearError,
            this.cmdShowLog,
            this.menuClipboard,
            this.cmdCopySourceName,
            this.cmdCopySourcePath,
            this.cmdCopySourceURL,
            this.cmdCopySourceError,
            this.cmdCopySourceModifiedItems,
            this.cmdCopySourceUnversionedItems,
            this.cmdCopySourceConflictedItems,
            this.menuAdditionalTortoiseCommands,
            this.cmdRepoBrowser,
            this.cmdRevisionGraph,
            this.cmdCleanUp,
            this.cmdExport,
            this.cmdSwitch,
            this.cmdRelocate,
            this.cmdProperties,
            this.cmdCheckout,
            this.cmdResolve,
            this.cmdMerge,
            this.cmdReintegrate,
            this.cmdBranchTag,
            this.cmdCreatePatch,
            this.cmdApplyPatch,
            this.cmdLock,
            this.cmdUnlock,
            this.cmdDeleteUnversioned,
            this.cmdTSVNSettings,
            this.cmdTSVNHelp,
            this.menuEvenMore,
            this.filterModified,
            this.filterNotUpToDate,
            this.filterAll,
            this.menuShow,
            this.cmdClearAllErrors,
            this.filterEnabled});
			this.uiCommandManager1.ContainerControl = this;
			this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] {
            this.uiContextMenu1});
			this.uiCommandManager1.Id = new System.Guid("6dcff238-beed-4562-b52d-d0ba855fecb0");
			this.uiCommandManager1.LeftRebar = this.LeftRebar1;
			this.uiCommandManager1.LockCommandBars = true;
			this.uiCommandManager1.RightRebar = this.RightRebar1;
			this.uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.ShowQuickCustomizeMenu = false;
			this.uiCommandManager1.TopRebar = this.TopRebar1;
			this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			// 
			// BottomRebar1
			// 
			this.BottomRebar1.CommandManager = this.uiCommandManager1;
			this.BottomRebar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomRebar1.Location = new System.Drawing.Point(0, 338);
			this.BottomRebar1.Name = "BottomRebar1";
			this.BottomRebar1.Size = new System.Drawing.Size(505, 0);
			// 
			// uiCommandBar1
			// 
			this.uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.Animation = Janus.Windows.UI.DropDownAnimation.System;
			this.uiCommandBar1.CommandManager = this.uiCommandManager1;
			this.uiCommandBar1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdNew1,
            this.cmdEdit1,
            this.cmdDelete1,
            this.Separator3,
            this.cmdSVNModifications1,
            this.cmdShowLog1,
            this.cmdSVNUpdate2,
            this.cmdSVNCommit2,
            this.cmdSVNRevert1,
            this.Separator7,
            this.cmdUpdate2,
            this.cmdUpdateAll2,
            this.Separator10,
            this.cmdMoveUp1,
            this.cmdMoveDown1,
            this.Separator6,
            this.menuWizards2});
			this.uiCommandBar1.FullRow = true;
			this.uiCommandBar1.Key = "CommandBar1";
			this.uiCommandBar1.Location = new System.Drawing.Point(0, 0);
			this.uiCommandBar1.Name = "uiCommandBar1";
			this.uiCommandBar1.RowIndex = 0;
			this.uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.Size = new System.Drawing.Size(468, 28);
			this.uiCommandBar1.Text = "Source";
			// 
			// cmdNew1
			// 
			this.cmdNew1.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdNew1.Key = "cmdNew";
			this.cmdNew1.Name = "cmdNew1";
			// 
			// cmdEdit1
			// 
			this.cmdEdit1.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdEdit1.Key = "cmdEdit";
			this.cmdEdit1.Name = "cmdEdit1";
			// 
			// cmdDelete1
			// 
			this.cmdDelete1.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdDelete1.Key = "cmdDelete";
			this.cmdDelete1.Name = "cmdDelete1";
			// 
			// Separator3
			// 
			this.Separator3.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator3.Key = "Separator";
			this.Separator3.Name = "Separator3";
			// 
			// cmdSVNModifications1
			// 
			this.cmdSVNModifications1.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdSVNModifications1.Key = "cmdSVNModifications";
			this.cmdSVNModifications1.Name = "cmdSVNModifications1";
			// 
			// cmdShowLog1
			// 
			this.cmdShowLog1.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdShowLog1.Key = "cmdShowLog";
			this.cmdShowLog1.Name = "cmdShowLog1";
			// 
			// cmdSVNUpdate2
			// 
			this.cmdSVNUpdate2.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdSVNUpdate2.Key = "cmdSVNUpdate";
			this.cmdSVNUpdate2.Name = "cmdSVNUpdate2";
			// 
			// cmdSVNCommit2
			// 
			this.cmdSVNCommit2.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdSVNCommit2.Key = "cmdSVNCommit";
			this.cmdSVNCommit2.Name = "cmdSVNCommit2";
			// 
			// cmdSVNRevert1
			// 
			this.cmdSVNRevert1.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdSVNRevert1.Key = "cmdSVNRevert";
			this.cmdSVNRevert1.Name = "cmdSVNRevert1";
			// 
			// Separator7
			// 
			this.Separator7.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator7.Key = "Separator";
			this.Separator7.Name = "Separator7";
			// 
			// cmdUpdate2
			// 
			this.cmdUpdate2.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdUpdate2.Key = "cmdUpdate";
			this.cmdUpdate2.Name = "cmdUpdate2";
			// 
			// cmdUpdateAll2
			// 
			this.cmdUpdateAll2.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdUpdateAll2.Key = "cmdUpdateAll";
			this.cmdUpdateAll2.Name = "cmdUpdateAll2";
			// 
			// Separator10
			// 
			this.Separator10.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator10.Key = "Separator";
			this.Separator10.Name = "Separator10";
			// 
			// cmdMoveUp1
			// 
			this.cmdMoveUp1.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdMoveUp1.Key = "cmdMoveUp";
			this.cmdMoveUp1.Name = "cmdMoveUp1";
			// 
			// cmdMoveDown1
			// 
			this.cmdMoveDown1.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdMoveDown1.Key = "cmdMoveDown";
			this.cmdMoveDown1.Name = "cmdMoveDown1";
			// 
			// Separator6
			// 
			this.Separator6.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator6.Key = "Separator";
			this.Separator6.Name = "Separator6";
			// 
			// menuWizards2
			// 
			this.menuWizards2.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.menuWizards2.Key = "menuWizards";
			this.menuWizards2.Name = "menuWizards2";
			// 
			// menuWizards
			// 
			this.menuWizards.CommandType = Janus.Windows.UI.CommandBars.CommandType.ControlPopup;
			this.menuWizards.Image = ((System.Drawing.Image)(resources.GetObject("menuWizards.Image")));
			this.menuWizards.Key = "menuWizards";
			this.menuWizards.Name = "menuWizards";
			this.menuWizards.Text = "Monitor this source";
			this.menuWizards.ToolTipText = "Monitor this source";
			// 
			// cmdSVNUpdate
			// 
			this.cmdSVNUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNUpdate.Image")));
			this.cmdSVNUpdate.Key = "cmdSVNUpdate";
			this.cmdSVNUpdate.Name = "cmdSVNUpdate";
			this.cmdSVNUpdate.Text = "Update";
			this.cmdSVNUpdate.ToolTipText = "Update";
			this.cmdSVNUpdate.Visible = Janus.Windows.UI.InheritableBoolean.False;
			// 
			// cmdSVNCommit
			// 
			this.cmdSVNCommit.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNCommit.Image")));
			this.cmdSVNCommit.Key = "cmdSVNCommit";
			this.cmdSVNCommit.Name = "cmdSVNCommit";
			this.cmdSVNCommit.Text = "Commit";
			this.cmdSVNCommit.ToolTipText = "Commit";
			this.cmdSVNCommit.Visible = Janus.Windows.UI.InheritableBoolean.False;
			// 
			// cmdSVNRevert
			// 
			this.cmdSVNRevert.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNRevert.Image")));
			this.cmdSVNRevert.Key = "cmdSVNRevert";
			this.cmdSVNRevert.Name = "cmdSVNRevert";
			this.cmdSVNRevert.Text = "Revert";
			this.cmdSVNRevert.ToolTipText = "Revert";
			// 
			// cmdSVNModifications
			// 
			this.cmdSVNModifications.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNModifications.Image")));
			this.cmdSVNModifications.Key = "cmdSVNModifications";
			this.cmdSVNModifications.Name = "cmdSVNModifications";
			this.cmdSVNModifications.Text = "Check for modifications";
			this.cmdSVNModifications.ToolTipText = "Check for modifications";
			// 
			// cmdRefresh
			// 
			this.cmdRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmdRefresh.Image")));
			this.cmdRefresh.Key = "cmdRefresh";
			this.cmdRefresh.Name = "cmdRefresh";
			this.cmdRefresh.Text = "Refresh log (might take a few minutes...)";
			this.cmdRefresh.ToolTipText = "Refresh log (might take a few minutes...)";
			// 
			// cmdEnabled
			// 
			this.cmdEnabled.CommandType = Janus.Windows.UI.CommandBars.CommandType.ToggleButton;
			this.cmdEnabled.Key = "cmdEnabled";
			this.cmdEnabled.Name = "cmdEnabled";
			this.cmdEnabled.Text = "Enabled";
			this.cmdEnabled.ToolTipText = "Enabled";
			// 
			// cmdExplore
			// 
			this.cmdExplore.Image = ((System.Drawing.Image)(resources.GetObject("cmdExplore.Image")));
			this.cmdExplore.Key = "cmdExplore";
			this.cmdExplore.Name = "cmdExplore";
			this.cmdExplore.Text = "Explore";
			this.cmdExplore.ToolTipText = "Explore";
			// 
			// cmdClearError
			// 
			this.cmdClearError.Image = ((System.Drawing.Image)(resources.GetObject("cmdClearError.Image")));
			this.cmdClearError.Key = "cmdClearError";
			this.cmdClearError.Name = "cmdClearError";
			this.cmdClearError.Text = "Clear error";
			this.cmdClearError.ToolTipText = "Clear Error";
			// 
			// cmdShowLog
			// 
			this.cmdShowLog.Image = ((System.Drawing.Image)(resources.GetObject("cmdShowLog.Image")));
			this.cmdShowLog.Key = "cmdShowLog";
			this.cmdShowLog.Name = "cmdShowLog";
			this.cmdShowLog.Text = "Show log";
			this.cmdShowLog.ToolTipText = "Show log";
			// 
			// menuClipboard
			// 
			this.menuClipboard.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdCopySourceName1,
            this.cmdCopySourcePath1,
            this.cmdCopySourceUrl1,
            this.cmdCopySourceError1,
            this.cmdCopySourceModifiedItems1,
            this.cmdCopySourceConflictedItems1,
            this.cmdCopySourceUnversionedItems1});
			this.menuClipboard.Image = ((System.Drawing.Image)(resources.GetObject("menuClipboard.Image")));
			this.menuClipboard.Key = "menuClipboard";
			this.menuClipboard.Name = "menuClipboard";
			this.menuClipboard.Text = "Copy to clipboard";
			// 
			// cmdCopySourceName1
			// 
			this.cmdCopySourceName1.Key = "cmdCopySourceName";
			this.cmdCopySourceName1.Name = "cmdCopySourceName1";
			// 
			// cmdCopySourcePath1
			// 
			this.cmdCopySourcePath1.Key = "cmdCopySourcePath";
			this.cmdCopySourcePath1.Name = "cmdCopySourcePath1";
			// 
			// cmdCopySourceUrl1
			// 
			this.cmdCopySourceUrl1.Key = "cmdCopySourceURL";
			this.cmdCopySourceUrl1.Name = "cmdCopySourceUrl1";
			// 
			// cmdCopySourceError1
			// 
			this.cmdCopySourceError1.Key = "cmdCopySourceError";
			this.cmdCopySourceError1.Name = "cmdCopySourceError1";
			// 
			// cmdCopySourceModifiedItems1
			// 
			this.cmdCopySourceModifiedItems1.Key = "cmdCopySourceModifiedItems";
			this.cmdCopySourceModifiedItems1.Name = "cmdCopySourceModifiedItems1";
			// 
			// cmdCopySourceConflictedItems1
			// 
			this.cmdCopySourceConflictedItems1.Key = "cmdCopySourceConflictedItems";
			this.cmdCopySourceConflictedItems1.Name = "cmdCopySourceConflictedItems1";
			// 
			// cmdCopySourceUnversionedItems1
			// 
			this.cmdCopySourceUnversionedItems1.Key = "cmdCopySourceUnversionedItems";
			this.cmdCopySourceUnversionedItems1.Name = "cmdCopySourceUnversionedItems1";
			// 
			// cmdCopySourceName
			// 
			this.cmdCopySourceName.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopySourceName.Image")));
			this.cmdCopySourceName.Key = "cmdCopySourceName";
			this.cmdCopySourceName.Name = "cmdCopySourceName";
			this.cmdCopySourceName.Text = "&Name";
			// 
			// cmdCopySourcePath
			// 
			this.cmdCopySourcePath.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopySourcePath.Image")));
			this.cmdCopySourcePath.Key = "cmdCopySourcePath";
			this.cmdCopySourcePath.Name = "cmdCopySourcePath";
			this.cmdCopySourcePath.Text = "&Path";
			// 
			// cmdCopySourceURL
			// 
			this.cmdCopySourceURL.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopySourceURL.Image")));
			this.cmdCopySourceURL.Key = "cmdCopySourceURL";
			this.cmdCopySourceURL.Name = "cmdCopySourceURL";
			this.cmdCopySourceURL.Text = "&URL";
			// 
			// cmdCopySourceError
			// 
			this.cmdCopySourceError.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopySourceError.Image")));
			this.cmdCopySourceError.Key = "cmdCopySourceError";
			this.cmdCopySourceError.Name = "cmdCopySourceError";
			this.cmdCopySourceError.Text = "&Error";
			// 
			// cmdCopySourceModifiedItems
			// 
			this.cmdCopySourceModifiedItems.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopySourceModifiedItems.Image")));
			this.cmdCopySourceModifiedItems.Key = "cmdCopySourceModifiedItems";
			this.cmdCopySourceModifiedItems.Name = "cmdCopySourceModifiedItems";
			this.cmdCopySourceModifiedItems.Text = "&Modified items list";
			// 
			// cmdCopySourceUnversionedItems
			// 
			this.cmdCopySourceUnversionedItems.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopySourceUnversionedItems.Image")));
			this.cmdCopySourceUnversionedItems.Key = "cmdCopySourceUnversionedItems";
			this.cmdCopySourceUnversionedItems.Name = "cmdCopySourceUnversionedItems";
			this.cmdCopySourceUnversionedItems.Text = "Un&versioned items list";
			// 
			// cmdCopySourceConflictedItems
			// 
			this.cmdCopySourceConflictedItems.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopySourceConflictedItems.Image")));
			this.cmdCopySourceConflictedItems.Key = "cmdCopySourceConflictedItems";
			this.cmdCopySourceConflictedItems.Name = "cmdCopySourceConflictedItems";
			this.cmdCopySourceConflictedItems.Text = "Possible &conflicted items list";
			// 
			// menuAdditionalTortoiseCommands
			// 
			this.menuAdditionalTortoiseCommands.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdCheckout1,
            this.cmdRepoBrowser1,
            this.cmdRevisionGraph1,
            this.cmdResolve1,
            this.cmdCleanUp1,
            this.cmdDeleteUnversioned1,
            this.cmdLock2,
            this.cmdUnlock2,
            this.Separator13,
            this.cmdBranchTag1,
            this.cmdSwitch1,
            this.cmdMerge1,
            this.cmdReintegrate1,
            this.cmdExport1,
            this.cmdRelocate1,
            this.Separator4,
            this.cmdCreatePatch2,
            this.cmdApplyPatch2,
            this.Separator11,
            this.cmdProperties2,
            this.Separator14,
            this.cmdTSVNSettings2,
            this.cmdTSVNHelp2});
			this.menuAdditionalTortoiseCommands.Image = ((System.Drawing.Image)(resources.GetObject("menuAdditionalTortoiseCommands.Image")));
			this.menuAdditionalTortoiseCommands.Key = "menuAdditionalTortoiseCommands";
			this.menuAdditionalTortoiseCommands.Name = "menuAdditionalTortoiseCommands";
			this.menuAdditionalTortoiseCommands.Text = "More";
			// 
			// cmdCheckout1
			// 
			this.cmdCheckout1.Key = "cmdCheckout";
			this.cmdCheckout1.Name = "cmdCheckout1";
			// 
			// cmdRepoBrowser1
			// 
			this.cmdRepoBrowser1.Key = "cmdRepoBrowser";
			this.cmdRepoBrowser1.Name = "cmdRepoBrowser1";
			// 
			// cmdRevisionGraph1
			// 
			this.cmdRevisionGraph1.Key = "cmdRevisionGraph";
			this.cmdRevisionGraph1.Name = "cmdRevisionGraph1";
			// 
			// cmdResolve1
			// 
			this.cmdResolve1.Key = "cmdResolve";
			this.cmdResolve1.Name = "cmdResolve1";
			// 
			// cmdCleanUp1
			// 
			this.cmdCleanUp1.Key = "cmdCleanUp";
			this.cmdCleanUp1.Name = "cmdCleanUp1";
			// 
			// cmdDeleteUnversioned1
			// 
			this.cmdDeleteUnversioned1.Key = "cmdDeleteUnversioned";
			this.cmdDeleteUnversioned1.Name = "cmdDeleteUnversioned1";
			// 
			// cmdLock2
			// 
			this.cmdLock2.Key = "cmdLock";
			this.cmdLock2.Name = "cmdLock2";
			// 
			// cmdUnlock2
			// 
			this.cmdUnlock2.Key = "cmdUnlock";
			this.cmdUnlock2.Name = "cmdUnlock2";
			// 
			// Separator13
			// 
			this.Separator13.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator13.Key = "Separator";
			this.Separator13.Name = "Separator13";
			// 
			// cmdBranchTag1
			// 
			this.cmdBranchTag1.Key = "cmdBranchTag";
			this.cmdBranchTag1.Name = "cmdBranchTag1";
			// 
			// cmdSwitch1
			// 
			this.cmdSwitch1.Key = "cmdSwitch";
			this.cmdSwitch1.Name = "cmdSwitch1";
			// 
			// cmdMerge1
			// 
			this.cmdMerge1.Key = "cmdMerge";
			this.cmdMerge1.Name = "cmdMerge1";
			// 
			// cmdReintegrate1
			// 
			this.cmdReintegrate1.Key = "cmdReintegrate";
			this.cmdReintegrate1.Name = "cmdReintegrate1";
			// 
			// cmdExport1
			// 
			this.cmdExport1.Key = "cmdExport";
			this.cmdExport1.Name = "cmdExport1";
			// 
			// cmdRelocate1
			// 
			this.cmdRelocate1.Key = "cmdRelocate";
			this.cmdRelocate1.Name = "cmdRelocate1";
			// 
			// Separator4
			// 
			this.Separator4.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator4.Key = "Separator";
			this.Separator4.Name = "Separator4";
			// 
			// cmdCreatePatch2
			// 
			this.cmdCreatePatch2.Key = "cmdCreatePatch";
			this.cmdCreatePatch2.Name = "cmdCreatePatch2";
			// 
			// cmdApplyPatch2
			// 
			this.cmdApplyPatch2.Key = "cmdApplyPatch";
			this.cmdApplyPatch2.Name = "cmdApplyPatch2";
			// 
			// Separator11
			// 
			this.Separator11.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator11.Key = "Separator";
			this.Separator11.Name = "Separator11";
			// 
			// cmdProperties2
			// 
			this.cmdProperties2.Key = "cmdProperties";
			this.cmdProperties2.Name = "cmdProperties2";
			// 
			// Separator14
			// 
			this.Separator14.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator14.Key = "Separator";
			this.Separator14.Name = "Separator14";
			// 
			// cmdTSVNSettings2
			// 
			this.cmdTSVNSettings2.Key = "cmdTSVNSettings";
			this.cmdTSVNSettings2.Name = "cmdTSVNSettings2";
			// 
			// cmdTSVNHelp2
			// 
			this.cmdTSVNHelp2.Key = "cmdTSVNHelp";
			this.cmdTSVNHelp2.Name = "cmdTSVNHelp2";
			// 
			// cmdRepoBrowser
			// 
			this.cmdRepoBrowser.Image = ((System.Drawing.Image)(resources.GetObject("cmdRepoBrowser.Image")));
			this.cmdRepoBrowser.Key = "cmdRepoBrowser";
			this.cmdRepoBrowser.Name = "cmdRepoBrowser";
			this.cmdRepoBrowser.Text = "Repo &browser";
			// 
			// cmdRevisionGraph
			// 
			this.cmdRevisionGraph.Image = ((System.Drawing.Image)(resources.GetObject("cmdRevisionGraph.Image")));
			this.cmdRevisionGraph.Key = "cmdRevisionGraph";
			this.cmdRevisionGraph.Name = "cmdRevisionGraph";
			this.cmdRevisionGraph.Text = "Revision &graph";
			// 
			// cmdCleanUp
			// 
			this.cmdCleanUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdCleanUp.Image")));
			this.cmdCleanUp.Key = "cmdCleanUp";
			this.cmdCleanUp.Name = "cmdCleanUp";
			this.cmdCleanUp.Text = "&Clean up";
			// 
			// cmdExport
			// 
			this.cmdExport.Image = ((System.Drawing.Image)(resources.GetObject("cmdExport.Image")));
			this.cmdExport.Key = "cmdExport";
			this.cmdExport.Name = "cmdExport";
			this.cmdExport.Text = "&Export";
			// 
			// cmdSwitch
			// 
			this.cmdSwitch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSwitch.Image")));
			this.cmdSwitch.Key = "cmdSwitch";
			this.cmdSwitch.Name = "cmdSwitch";
			this.cmdSwitch.Text = "&Switch";
			// 
			// cmdRelocate
			// 
			this.cmdRelocate.Image = ((System.Drawing.Image)(resources.GetObject("cmdRelocate.Image")));
			this.cmdRelocate.Key = "cmdRelocate";
			this.cmdRelocate.Name = "cmdRelocate";
			this.cmdRelocate.Text = "&Relocate";
			// 
			// cmdProperties
			// 
			this.cmdProperties.Image = ((System.Drawing.Image)(resources.GetObject("cmdProperties.Image")));
			this.cmdProperties.Key = "cmdProperties";
			this.cmdProperties.Name = "cmdProperties";
			this.cmdProperties.Text = "SVN-&Properties";
			// 
			// cmdCheckout
			// 
			this.cmdCheckout.Image = ((System.Drawing.Image)(resources.GetObject("cmdCheckout.Image")));
			this.cmdCheckout.Key = "cmdCheckout";
			this.cmdCheckout.Name = "cmdCheckout";
			this.cmdCheckout.Text = "Chec&kout";
			// 
			// cmdResolve
			// 
			this.cmdResolve.Key = "cmdResolve";
			this.cmdResolve.Name = "cmdResolve";
			this.cmdResolve.Text = "Resolve";
			// 
			// cmdMerge
			// 
			this.cmdMerge.Key = "cmdMerge";
			this.cmdMerge.Name = "cmdMerge";
			this.cmdMerge.Text = "Merge";
			// 
			// cmdReintegrate
			// 
			this.cmdReintegrate.Key = "cmdReintegrate";
			this.cmdReintegrate.Name = "cmdReintegrate";
			this.cmdReintegrate.Text = "Reintegrate";
			// 
			// cmdBranchTag
			// 
			this.cmdBranchTag.Key = "cmdBranchTag";
			this.cmdBranchTag.Name = "cmdBranchTag";
			this.cmdBranchTag.Text = "Branch/Tag";
			// 
			// cmdCreatePatch
			// 
			this.cmdCreatePatch.Key = "cmdCreatePatch";
			this.cmdCreatePatch.Name = "cmdCreatePatch";
			this.cmdCreatePatch.Text = "Create Patch";
			// 
			// cmdApplyPatch
			// 
			this.cmdApplyPatch.Key = "cmdApplyPatch";
			this.cmdApplyPatch.Name = "cmdApplyPatch";
			this.cmdApplyPatch.Text = "Apply Patch";
			// 
			// cmdLock
			// 
			this.cmdLock.Key = "cmdLock";
			this.cmdLock.Name = "cmdLock";
			this.cmdLock.Text = "Get Lock";
			// 
			// cmdUnlock
			// 
			this.cmdUnlock.Key = "cmdUnlock";
			this.cmdUnlock.Name = "cmdUnlock";
			this.cmdUnlock.Text = "Release Lock";
			// 
			// cmdDeleteUnversioned
			// 
			this.cmdDeleteUnversioned.Key = "cmdDeleteUnversioned";
			this.cmdDeleteUnversioned.Name = "cmdDeleteUnversioned";
			this.cmdDeleteUnversioned.Text = "Delete Unversioned Items";
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
			// menuEvenMore
			// 
			this.menuEvenMore.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdCreatePatch1,
            this.cmdApplyPatch1,
            this.cmdProperties1,
            this.Separator12,
            this.cmdTSVNSettings1,
            this.cmdTSVNHelp1});
			this.menuEvenMore.Key = "menuEvenMore";
			this.menuEvenMore.Name = "menuEvenMore";
			this.menuEvenMore.Text = "Even More";
			// 
			// cmdCreatePatch1
			// 
			this.cmdCreatePatch1.Key = "cmdCreatePatch";
			this.cmdCreatePatch1.Name = "cmdCreatePatch1";
			// 
			// cmdApplyPatch1
			// 
			this.cmdApplyPatch1.Key = "cmdApplyPatch";
			this.cmdApplyPatch1.Name = "cmdApplyPatch1";
			// 
			// cmdProperties1
			// 
			this.cmdProperties1.Key = "cmdProperties";
			this.cmdProperties1.Name = "cmdProperties1";
			// 
			// Separator12
			// 
			this.Separator12.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator12.Key = "Separator";
			this.Separator12.Name = "Separator12";
			// 
			// cmdTSVNSettings1
			// 
			this.cmdTSVNSettings1.Key = "cmdTSVNSettings";
			this.cmdTSVNSettings1.Name = "cmdTSVNSettings1";
			// 
			// cmdTSVNHelp1
			// 
			this.cmdTSVNHelp1.Key = "cmdTSVNHelp";
			this.cmdTSVNHelp1.Name = "cmdTSVNHelp1";
			// 
			// filterModified
			// 
			this.filterModified.CommandType = Janus.Windows.UI.CommandBars.CommandType.ToggleButton;
			this.filterModified.Key = "filterModified";
			this.filterModified.Name = "filterModified";
			this.filterModified.Text = "&Modified";
			// 
			// filterNotUpToDate
			// 
			this.filterNotUpToDate.CommandType = Janus.Windows.UI.CommandBars.CommandType.ToggleButton;
			this.filterNotUpToDate.Key = "filterNotUpToDate";
			this.filterNotUpToDate.Name = "filterNotUpToDate";
			this.filterNotUpToDate.Text = "&Has Updates";
			// 
			// filterAll
			// 
			this.filterAll.Checked = Janus.Windows.UI.InheritableBoolean.True;
			this.filterAll.CommandType = Janus.Windows.UI.CommandBars.CommandType.ToggleButton;
			this.filterAll.Key = "filterAll";
			this.filterAll.Name = "filterAll";
			this.filterAll.Text = "&All";
			// 
			// menuShow
			// 
			this.menuShow.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.filterAll1,
            this.Separator15,
            this.filterModified3,
            this.filterNotUpToDate3,
            this.filterEnabled1});
			this.menuShow.IsEditableControl = Janus.Windows.UI.InheritableBoolean.False;
			this.menuShow.Key = "menuShow";
			this.menuShow.Name = "menuShow";
			this.menuShow.Text = "&Show";
			// 
			// filterAll1
			// 
			this.filterAll1.Key = "filterAll";
			this.filterAll1.Name = "filterAll1";
			// 
			// Separator15
			// 
			this.Separator15.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator15.Key = "Separator";
			this.Separator15.Name = "Separator15";
			// 
			// filterModified3
			// 
			this.filterModified3.Key = "filterModified";
			this.filterModified3.Name = "filterModified3";
			// 
			// filterNotUpToDate3
			// 
			this.filterNotUpToDate3.Key = "filterNotUpToDate";
			this.filterNotUpToDate3.Name = "filterNotUpToDate3";
			// 
			// filterEnabled1
			// 
			this.filterEnabled1.Key = "filterEnabled";
			this.filterEnabled1.Name = "filterEnabled1";
			// 
			// cmdClearAllErrors
			// 
			this.cmdClearAllErrors.Key = "cmdClearAllErrors";
			this.cmdClearAllErrors.Name = "cmdClearAllErrors";
			this.cmdClearAllErrors.Text = "Clear All Errors";
			// 
			// filterEnabled
			// 
			this.filterEnabled.CommandType = Janus.Windows.UI.CommandBars.CommandType.ToggleButton;
			this.filterEnabled.Key = "filterEnabled";
			this.filterEnabled.Name = "filterEnabled";
			this.filterEnabled.Text = "&Enabled";
			// 
			// uiContextMenu1
			// 
			this.uiContextMenu1.CommandManager = this.uiCommandManager1;
			this.uiContextMenu1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdEnabled1,
            this.cmdClearError1,
            this.cmdClearAllErrors1,
            this.cmdExplore1,
            this.cmdNew2,
            this.cmdEdit2,
            this.cmdDelete2,
            this.menuClipboard1,
            this.Separator2,
            this.cmdSVNModifications2,
            this.cmdShowLog2,
            this.cmdSVNUpdate1,
            this.cmdSVNCommit1,
            this.cmdSVNRevert2,
            this.menuAdditionalTortoiseCommands1,
            this.Separator8,
            this.cmdUpdate1,
            this.cmdUpdateAll1,
            this.Separator9,
            this.cmdRefresh1,
            this.Separator1,
            this.cmdMoveUp2,
            this.cmdMoveDown2,
            this.menuShow2,
            this.Separator5,
            this.menuWizards1});
			this.uiContextMenu1.Key = "ContextMenu1";
			// 
			// cmdEnabled1
			// 
			this.cmdEnabled1.Key = "cmdEnabled";
			this.cmdEnabled1.Name = "cmdEnabled1";
			this.cmdEnabled1.Text = "&Enabled";
			// 
			// cmdClearError1
			// 
			this.cmdClearError1.Key = "cmdClearError";
			this.cmdClearError1.Name = "cmdClearError1";
			this.cmdClearError1.Text = "Clear &error";
			// 
			// cmdClearAllErrors1
			// 
			this.cmdClearAllErrors1.Key = "cmdClearAllErrors";
			this.cmdClearAllErrors1.Name = "cmdClearAllErrors1";
			// 
			// cmdExplore1
			// 
			this.cmdExplore1.Key = "cmdExplore";
			this.cmdExplore1.Name = "cmdExplore1";
			this.cmdExplore1.Text = "E&xplore";
			// 
			// cmdNew2
			// 
			this.cmdNew2.Key = "cmdNew";
			this.cmdNew2.Name = "cmdNew2";
			this.cmdNew2.Text = "&New Source";
			// 
			// cmdEdit2
			// 
			this.cmdEdit2.Key = "cmdEdit";
			this.cmdEdit2.Name = "cmdEdit2";
			this.cmdEdit2.Text = "&Properties";
			// 
			// cmdDelete2
			// 
			this.cmdDelete2.Key = "cmdDelete";
			this.cmdDelete2.Name = "cmdDelete2";
			this.cmdDelete2.Text = "&Delete";
			// 
			// menuClipboard1
			// 
			this.menuClipboard1.Key = "menuClipboard";
			this.menuClipboard1.Name = "menuClipboard1";
			this.menuClipboard1.Text = "Copy &to clipboard";
			// 
			// Separator2
			// 
			this.Separator2.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator2.Key = "Separator";
			this.Separator2.Name = "Separator2";
			// 
			// cmdSVNModifications2
			// 
			this.cmdSVNModifications2.Key = "cmdSVNModifications";
			this.cmdSVNModifications2.Name = "cmdSVNModifications2";
			this.cmdSVNModifications2.Text = "Check modi&fications";
			// 
			// cmdShowLog2
			// 
			this.cmdShowLog2.Key = "cmdShowLog";
			this.cmdShowLog2.Name = "cmdShowLog2";
			this.cmdShowLog2.Text = "&Show log";
			// 
			// cmdSVNUpdate1
			// 
			this.cmdSVNUpdate1.Key = "cmdSVNUpdate";
			this.cmdSVNUpdate1.Name = "cmdSVNUpdate1";
			this.cmdSVNUpdate1.Text = "&Update";
			// 
			// cmdSVNCommit1
			// 
			this.cmdSVNCommit1.Key = "cmdSVNCommit";
			this.cmdSVNCommit1.Name = "cmdSVNCommit1";
			this.cmdSVNCommit1.Text = "&Commit";
			// 
			// cmdSVNRevert2
			// 
			this.cmdSVNRevert2.Key = "cmdSVNRevert";
			this.cmdSVNRevert2.Name = "cmdSVNRevert2";
			this.cmdSVNRevert2.Text = "Re&vert";
			// 
			// menuAdditionalTortoiseCommands1
			// 
			this.menuAdditionalTortoiseCommands1.Key = "menuAdditionalTortoiseCommands";
			this.menuAdditionalTortoiseCommands1.Name = "menuAdditionalTortoiseCommands1";
			this.menuAdditionalTortoiseCommands1.Text = "M&ore";
			// 
			// Separator8
			// 
			this.Separator8.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator8.Key = "Separator";
			this.Separator8.Name = "Separator8";
			// 
			// cmdUpdate1
			// 
			this.cmdUpdate1.Key = "cmdUpdate";
			this.cmdUpdate1.Name = "cmdUpdate1";
			this.cmdUpdate1.Text = "Chec&k for updates";
			// 
			// cmdUpdateAll1
			// 
			this.cmdUpdateAll1.Key = "cmdUpdateAll";
			this.cmdUpdateAll1.Name = "cmdUpdateAll1";
			this.cmdUpdateAll1.Text = "Check &all for updates";
			// 
			// Separator9
			// 
			this.Separator9.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator9.Key = "Separator";
			this.Separator9.Name = "Separator9";
			// 
			// cmdRefresh1
			// 
			this.cmdRefresh1.Key = "cmdRefresh";
			this.cmdRefresh1.Name = "cmdRefresh1";
			this.cmdRefresh1.Text = "&Refresh log (might take a few minutes...)";
			// 
			// Separator1
			// 
			this.Separator1.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator1.Key = "Separator";
			this.Separator1.Name = "Separator1";
			// 
			// cmdMoveUp2
			// 
			this.cmdMoveUp2.Key = "cmdMoveUp";
			this.cmdMoveUp2.Name = "cmdMoveUp2";
			this.cmdMoveUp2.Text = "Mo&ve Up";
			// 
			// cmdMoveDown2
			// 
			this.cmdMoveDown2.Key = "cmdMoveDown";
			this.cmdMoveDown2.Name = "cmdMoveDown2";
			this.cmdMoveDown2.Text = "Move Do&wn";
			// 
			// menuShow2
			// 
			this.menuShow2.Key = "menuShow";
			this.menuShow2.Name = "menuShow2";
			// 
			// Separator5
			// 
			this.Separator5.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator5.Key = "Separator";
			this.Separator5.Name = "Separator5";
			// 
			// menuWizards1
			// 
			this.menuWizards1.Key = "menuWizards";
			this.menuWizards1.Name = "menuWizards1";
			this.menuWizards1.Text = "&Monitor this source";
			// 
			// LeftRebar1
			// 
			this.LeftRebar1.CommandManager = this.uiCommandManager1;
			this.LeftRebar1.Dock = System.Windows.Forms.DockStyle.Left;
			this.LeftRebar1.Location = new System.Drawing.Point(0, 28);
			this.LeftRebar1.Name = "LeftRebar1";
			this.LeftRebar1.Size = new System.Drawing.Size(0, 310);
			// 
			// RightRebar1
			// 
			this.RightRebar1.CommandManager = this.uiCommandManager1;
			this.RightRebar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.RightRebar1.Location = new System.Drawing.Point(505, 28);
			this.RightRebar1.Name = "RightRebar1";
			this.RightRebar1.Size = new System.Drawing.Size(0, 310);
			// 
			// TopRebar1
			// 
			this.TopRebar1.CommandBars.AddRange(new Janus.Windows.UI.CommandBars.UICommandBar[] {
            this.uiCommandBar1});
			this.TopRebar1.CommandManager = this.uiCommandManager1;
			this.TopRebar1.Controls.Add(this.uiCommandBar1);
			this.TopRebar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.TopRebar1.Location = new System.Drawing.Point(0, 0);
			this.TopRebar1.Name = "TopRebar1";
			this.TopRebar1.Size = new System.Drawing.Size(468, 28);
			// 
			// sourcesExplorerBar1
			// 
			this.sourcesExplorerBar1.BackgroundFormatStyle.BackColor = System.Drawing.Color.White;
			this.sourcesExplorerBar1.BackgroundFormatStyle.BackColorGradient = System.Drawing.Color.Transparent;
			this.sourcesExplorerBar1.BorderStyle = Janus.Windows.ExplorerBar.BorderStyle.None;
			this.uiCommandManager1.SetContextMenu(this.sourcesExplorerBar1, this.uiContextMenu1);
			this.sourcesExplorerBar1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sourcesExplorerBar1.GroupSeparation = 14;
			this.sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackColor = System.Drawing.Color.Gainsboro;
			this.sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackColorGradient = System.Drawing.Color.Silver;
			this.sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			this.sourcesExplorerBar1.GroupsStateStyles.FormatStyle.ForeColor = System.Drawing.Color.Black;
			this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColor = System.Drawing.Color.Silver;
			this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColorGradient = System.Drawing.Color.Gainsboro;
			this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackgroundThemeStyle = Janus.Windows.ExplorerBar.BackgroundThemeStyle.GroupHeader;
			this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.ForeColor = System.Drawing.Color.Black;
			this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.ForegroundThemeStyle = Janus.Windows.ExplorerBar.ForegroundThemeStyle.GroupHeader;
			this.sourcesExplorerBar1.GroupsStateStyles.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Control;
			this.sourcesExplorerBar1.Location = new System.Drawing.Point(0, 23);
			this.sourcesExplorerBar1.Name = "sourcesExplorerBar1";
			this.sourcesExplorerBar1.Size = new System.Drawing.Size(468, 346);
			this.sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColor = System.Drawing.Color.LightSkyBlue;
			this.sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColorGradient = System.Drawing.Color.SteelBlue;
			this.sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			this.sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.ForeColor = System.Drawing.Color.White;
			this.sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
			this.sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackColorGradient = System.Drawing.Color.LightSkyBlue;
			this.sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			this.sourcesExplorerBar1.TabIndex = 1;
			this.sourcesExplorerBar1.ThemedAreas = Janus.Windows.ExplorerBar.ThemedArea.None;
			this.sourcesExplorerBar1.TopMargin = 14;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.DimGray;
			this.panel1.Controls.Add(this.sourcesExplorerBar1);
			this.panel1.Controls.Add(this.linkShowAllSources);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 28);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.panel1.Size = new System.Drawing.Size(468, 369);
			this.panel1.TabIndex = 2;
			// 
			// linkShowAllSources
			// 
			this.linkShowAllSources.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.linkShowAllSources.BackColor = System.Drawing.Color.Moccasin;
			this.linkShowAllSources.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.linkShowAllSources.Dock = System.Windows.Forms.DockStyle.Top;
			this.linkShowAllSources.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.linkShowAllSources.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.linkShowAllSources.LinkColor = System.Drawing.Color.Black;
			this.linkShowAllSources.Location = new System.Drawing.Point(0, 1);
			this.linkShowAllSources.Name = "linkShowAllSources";
			this.linkShowAllSources.Size = new System.Drawing.Size(468, 22);
			this.linkShowAllSources.TabIndex = 2;
			this.linkShowAllSources.TabStop = true;
			this.linkShowAllSources.Text = "Some sources are hidden. Click here to show all.";
			this.linkShowAllSources.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkShowAllSources.Visible = false;
			// 
			// filterModified1
			// 
			this.filterModified1.Key = "filterModified";
			this.filterModified1.Name = "filterModified1";
			// 
			// filterNotUpToDate2
			// 
			this.filterNotUpToDate2.Key = "filterNotUpToDate";
			this.filterNotUpToDate2.Name = "filterNotUpToDate2";
			// 
			// SourcesPanel
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TopRebar1);
			this.Name = "SourcesPanel";
			this.Size = new System.Drawing.Size(468, 397);
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).EndInit();
			this.TopRebar1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sourcesExplorerBar1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        #endregion
		private Janus.Windows.UI.CommandBars.UIRebar BottomRebar1;
		private Janus.Windows.UI.CommandBars.UICommand cmdApplyPatch;
		private Janus.Windows.UI.CommandBars.UICommand cmdApplyPatch1;
		private Janus.Windows.UI.CommandBars.UICommand cmdApplyPatch2;
		private Janus.Windows.UI.CommandBars.UICommand cmdBranchTag;
		private Janus.Windows.UI.CommandBars.UICommand cmdBranchTag1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCheckout;
		private Janus.Windows.UI.CommandBars.UICommand cmdCheckout1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCleanUp;
		private Janus.Windows.UI.CommandBars.UICommand cmdCleanUp1;
		private Janus.Windows.UI.CommandBars.UICommand cmdClearAllErrors;
		private Janus.Windows.UI.CommandBars.UICommand cmdClearAllErrors1;
		private Janus.Windows.UI.CommandBars.UICommand cmdClearError;
		private Janus.Windows.UI.CommandBars.UICommand cmdClearError1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceConflictedItems;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceConflictedItems1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceError;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceError1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceModifiedItems;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceModifiedItems1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceName;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceName1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourcePath;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourcePath1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceUnversionedItems;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceUnversionedItems1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceURL;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopySourceUrl1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCreatePatch;
		private Janus.Windows.UI.CommandBars.UICommand cmdCreatePatch1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCreatePatch2;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete;
		private Janus.Windows.UI.CommandBars.UICommand cmdDeleteUnversioned;
		private Janus.Windows.UI.CommandBars.UICommand cmdDeleteUnversioned1;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit;
		private Janus.Windows.UI.CommandBars.UICommand cmdEnabled;
		private Janus.Windows.UI.CommandBars.UICommand cmdEnabled1;
		private Janus.Windows.UI.CommandBars.UICommand cmdExplore;
		private Janus.Windows.UI.CommandBars.UICommand cmdExplore1;
		private Janus.Windows.UI.CommandBars.UICommand cmdExport;
		private Janus.Windows.UI.CommandBars.UICommand cmdExport1;
		private Janus.Windows.UI.CommandBars.UICommand cmdLock;
		private Janus.Windows.UI.CommandBars.UICommand cmdLock2;
		private Janus.Windows.UI.CommandBars.UICommand cmdMerge;
		private Janus.Windows.UI.CommandBars.UICommand cmdMerge1;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew;
		private Janus.Windows.UI.CommandBars.UICommand cmdProperties;
		private Janus.Windows.UI.CommandBars.UICommand cmdProperties1;
		private Janus.Windows.UI.CommandBars.UICommand cmdProperties2;
		private Janus.Windows.UI.CommandBars.UICommand cmdRefresh;
		private Janus.Windows.UI.CommandBars.UICommand cmdRefresh1;
		private Janus.Windows.UI.CommandBars.UICommand cmdReintegrate;
		private Janus.Windows.UI.CommandBars.UICommand cmdReintegrate1;
		private Janus.Windows.UI.CommandBars.UICommand cmdRelocate;
		private Janus.Windows.UI.CommandBars.UICommand cmdRelocate1;
		private Janus.Windows.UI.CommandBars.UICommand cmdRepoBrowser;
		private Janus.Windows.UI.CommandBars.UICommand cmdRepoBrowser1;
		private Janus.Windows.UI.CommandBars.UICommand cmdResolve;
		private Janus.Windows.UI.CommandBars.UICommand cmdResolve1;
		private Janus.Windows.UI.CommandBars.UICommand cmdRevisionGraph;
		private Janus.Windows.UI.CommandBars.UICommand cmdRevisionGraph1;
		private Janus.Windows.UI.CommandBars.UICommand cmdShowLog;
		private Janus.Windows.UI.CommandBars.UICommand cmdShowLog1;
		private Janus.Windows.UI.CommandBars.UICommand cmdShowLog2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNCommit;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNCommit1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNCommit2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNModifications;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNModifications1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNModifications2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNRevert;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNRevert1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNRevert2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSwitch;
		private Janus.Windows.UI.CommandBars.UICommand cmdSwitch1;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNHelp;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNHelp1;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNHelp2;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNSettings;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNSettings1;
		private Janus.Windows.UI.CommandBars.UICommand cmdTSVNSettings2;
		private Janus.Windows.UI.CommandBars.UICommand cmdUnlock;
		private Janus.Windows.UI.CommandBars.UICommand cmdUnlock2;
		private Janus.Windows.UI.CommandBars.UICommand cmdUpdate;
		private Janus.Windows.UI.CommandBars.UICommand cmdUpdateAll;
		private Janus.Windows.UI.CommandBars.UICommand filterAll;
		private Janus.Windows.UI.CommandBars.UICommand filterAll1;
		private Janus.Windows.UI.CommandBars.UICommand filterEnabled;
		private Janus.Windows.UI.CommandBars.UICommand filterEnabled1;
		private Janus.Windows.UI.CommandBars.UICommand filterModified;
		private Janus.Windows.UI.CommandBars.UICommand filterModified1;
		private Janus.Windows.UI.CommandBars.UICommand filterModified3;
		private Janus.Windows.UI.CommandBars.UICommand filterNotUpToDate;
		private Janus.Windows.UI.CommandBars.UICommand filterNotUpToDate2;
		private Janus.Windows.UI.CommandBars.UICommand filterNotUpToDate3;
		private System.Windows.Forms.ImageList imageList1;
		private Janus.Windows.UI.CommandBars.UIRebar LeftRebar1;
		private System.Windows.Forms.LinkLabel linkShowAllSources;
		private bool loadingFilterSettings;
		private Janus.Windows.UI.CommandBars.UICommand menuAdditionalTortoiseCommands;
		private Janus.Windows.UI.CommandBars.UICommand menuAdditionalTortoiseCommands1;
		private Janus.Windows.UI.CommandBars.UICommand menuClipboard;
		private Janus.Windows.UI.CommandBars.UICommand menuClipboard1;
		private Janus.Windows.UI.CommandBars.UICommand menuEvenMore;
		private Janus.Windows.UI.CommandBars.UICommand menuShow;
		private Janus.Windows.UI.CommandBars.UICommand menuShow2;
		private Janus.Windows.UI.CommandBars.UICommand menuWizards;
		private Janus.Windows.UI.CommandBars.UICommand menuWizards1;
		private Janus.Windows.UI.CommandBars.UICommand menuWizards2;
		private System.Windows.Forms.Panel panel1;
		private Janus.Windows.UI.CommandBars.UIRebar RightRebar1;
		private Janus.Windows.UI.CommandBars.UICommand Separator10;
		private Janus.Windows.UI.CommandBars.UICommand Separator11;
		private Janus.Windows.UI.CommandBars.UICommand Separator12;
		private Janus.Windows.UI.CommandBars.UICommand Separator13;
		private Janus.Windows.UI.CommandBars.UICommand Separator14;
		private Janus.Windows.UI.CommandBars.UICommand Separator15;
		private Janus.Windows.UI.CommandBars.UICommand Separator4;
		private Janus.Windows.UI.CommandBars.UICommand Separator5;
		private Janus.Windows.UI.CommandBars.UICommand Separator6;
		private Janus.Windows.UI.CommandBars.UICommand Separator7;
		private Janus.Windows.UI.CommandBars.UICommand Separator8;
		private Janus.Windows.UI.CommandBars.UICommand Separator9;
		private bool showingAllItems;
		private SVNMonitor.View.Controls.SourcesExplorerBar sourcesExplorerBar1;
		private Janus.Windows.UI.CommandBars.UIRebar TopRebar1;
		private Janus.Windows.UI.CommandBars.UICommandBar uiCommandBar1;
		private Janus.Windows.UI.CommandBars.UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew1;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete1;
		private Janus.Windows.UI.CommandBars.UICommand Separator3;
		private Janus.Windows.UI.CommandBars.UICommand cmdUpdate2;
		private Janus.Windows.UI.CommandBars.UICommand cmdUpdateAll2;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp1;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown1;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew2;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit2;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete2;
		private Janus.Windows.UI.CommandBars.UICommand Separator2;
		private Janus.Windows.UI.CommandBars.UICommand cmdUpdate1;
		private Janus.Windows.UI.CommandBars.UICommand cmdUpdateAll1;
		private Janus.Windows.UI.CommandBars.UICommand Separator1;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp2;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown2;
	}
}