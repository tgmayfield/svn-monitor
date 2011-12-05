namespace SVNMonitor.View.Panels
{
	internal partial class PathsPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathsPanel));
			Janus.Windows.GridEX.GridEXLayout pathsGrid1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			this.cmdExplore = new Janus.Windows.UI.CommandBars.UICommand("cmdExplore");
			this.pathsGrid1 = new SVNMonitor.View.Controls.PathsGrid();
			this.uiCommandManager1 = new Janus.Windows.UI.CommandBars.UICommandManager(this.components);
			this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			this.cmdSVNUpdate2 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdCommit2 = new Janus.Windows.UI.CommandBars.UICommand("cmdCommit");
			this.cmdRevert2 = new Janus.Windows.UI.CommandBars.UICommand("cmdRevert");
			this.cmdRollback1 = new Janus.Windows.UI.CommandBars.UICommand("cmdRollback");
			this.Separator3 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdSVNLog1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNLog");
			this.cmdDiffWithPrevious2 = new Janus.Windows.UI.CommandBars.UICommand("cmdDiffWithPrevious");
			this.cmdDiffLocalWithBase2 = new Janus.Windows.UI.CommandBars.UICommand("cmdDiffLocalWithBase");
			this.cmdBlame1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBlame");
			this.Separator1 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdExplore1 = new Janus.Windows.UI.CommandBars.UICommand("cmdExplore");
			this.cmdBrowse2 = new Janus.Windows.UI.CommandBars.UICommand("cmdBrowse");
			this.cmdOpen1 = new Janus.Windows.UI.CommandBars.UICommand("cmdOpen");
			this.cmdOpenWith2 = new Janus.Windows.UI.CommandBars.UICommand("cmdOpenWith");
			this.cmdEdit2 = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdExport1 = new Janus.Windows.UI.CommandBars.UICommand("cmdExport");
			this.menuClipboard1 = new Janus.Windows.UI.CommandBars.UICommand("menuClipboard");
			this.Separator5 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.menuFileWizard1 = new Janus.Windows.UI.CommandBars.UICommand("menuFileWizard");
			this.BottomRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.uiCommandBar1 = new Janus.Windows.UI.CommandBars.UICommandBar();
			this.cmdSVNUpdate1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdCommit1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCommit");
			this.cmdRevert1 = new Janus.Windows.UI.CommandBars.UICommand("cmdRevert");
			this.cmdRollback2 = new Janus.Windows.UI.CommandBars.UICommand("cmdRollback");
			this.Separator4 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdSVNLog2 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNLog");
			this.cmdDiff2 = new Janus.Windows.UI.CommandBars.UICommand("cmdDiff");
			this.cmdBlame2 = new Janus.Windows.UI.CommandBars.UICommand("cmdBlame");
			this.Separator2 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdExplore2 = new Janus.Windows.UI.CommandBars.UICommand("cmdExplore");
			this.cmdBrowse1 = new Janus.Windows.UI.CommandBars.UICommand("cmdBrowse");
			this.cmdOpen2 = new Janus.Windows.UI.CommandBars.UICommand("cmdOpen");
			this.cmdOpenWith3 = new Janus.Windows.UI.CommandBars.UICommand("cmdOpenWith");
			this.cmdEdit1 = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.Separator6 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.menuFileWizard2 = new Janus.Windows.UI.CommandBars.UICommand("menuFileWizard");
			this.cmdOpen = new Janus.Windows.UI.CommandBars.UICommand("cmdOpen");
			this.cmdOpenWith = new Janus.Windows.UI.CommandBars.UICommand("cmdOpenWith");
			this.cmdSVNLog = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNLog");
			this.cmdDiff = new Janus.Windows.UI.CommandBars.UICommand("cmdDiff");
			this.cmdDiffWithPrevious1 = new Janus.Windows.UI.CommandBars.UICommand("cmdDiffWithPrevious");
			this.cmdDiffLocalWithBase1 = new Janus.Windows.UI.CommandBars.UICommand("cmdDiffLocalWithBase");
			this.cmdDiffWithPrevious = new Janus.Windows.UI.CommandBars.UICommand("cmdDiffWithPrevious");
			this.cmdDiffLocalWithBase = new Janus.Windows.UI.CommandBars.UICommand("cmdDiffLocalWithBase");
			this.cmdBlame = new Janus.Windows.UI.CommandBars.UICommand("cmdBlame");
			this.cmdSVNUpdate = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdRollback = new Janus.Windows.UI.CommandBars.UICommand("cmdRollback");
			this.menuFileWizard = new Janus.Windows.UI.CommandBars.UICommand("menuFileWizard");
			this.cmdCommit = new Janus.Windows.UI.CommandBars.UICommand("cmdCommit");
			this.cmdRevert = new Janus.Windows.UI.CommandBars.UICommand("cmdRevert");
			this.cmdBrowse = new Janus.Windows.UI.CommandBars.UICommand("cmdBrowse");
			this.cmdEdit = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.menuClipboard = new Janus.Windows.UI.CommandBars.UICommand("menuClipboard");
			this.cmdCopyFullName1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyFullName");
			this.cmdCopyName1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyName");
			this.cmdCopyRelativeUrl1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyRelativeURL");
			this.cmdCopyURL1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyURL");
			this.cmdCopyFullName = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyFullName");
			this.cmdCopyName = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyName");
			this.cmdCopyURL = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyURL");
			this.cmdCopyRelativeURL = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyRelativeURL");
			this.cmdExport = new Janus.Windows.UI.CommandBars.UICommand("cmdExport");
			this.LeftRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.RightRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.TopRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pathsGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).BeginInit();
			this.TopRebar1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdExplore
			// 
			this.cmdExplore.Image = ((System.Drawing.Image)(resources.GetObject("cmdExplore.Image")));
			this.cmdExplore.Key = "cmdExplore";
			this.cmdExplore.Name = "cmdExplore";
			this.cmdExplore.Text = "Explore";
			this.cmdExplore.ToolTipText = "Explore";
			// 
			// pathsGrid1
			// 
			this.pathsGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			this.pathsGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			this.pathsGrid1.ColumnAutoResize = true;
			this.uiCommandManager1.SetContextMenu(this.pathsGrid1, this.uiContextMenu1);
			pathsGrid1_DesignTimeLayout.LayoutString = resources.GetString("pathsGrid1_DesignTimeLayout.LayoutString");
			this.pathsGrid1.DesignTimeLayout = pathsGrid1_DesignTimeLayout;
			this.pathsGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pathsGrid1.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
			this.pathsGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.pathsGrid1.GridLineColor = System.Drawing.SystemColors.Control;
			this.pathsGrid1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal;
			this.pathsGrid1.GroupByBoxVisible = false;
			this.pathsGrid1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
			this.pathsGrid1.Location = new System.Drawing.Point(0, 1);
			this.pathsGrid1.Name = "pathsGrid1";
			this.pathsGrid1.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
			this.pathsGrid1.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
			this.pathsGrid1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
			this.pathsGrid1.SettingsKey = "pathsGrid1";
			this.pathsGrid1.Size = new System.Drawing.Size(923, 121);
			this.pathsGrid1.TabIndex = 4;
			this.pathsGrid1.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation;
			this.pathsGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			// 
			// uiCommandManager1
			// 
			this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.BottomRebar = this.BottomRebar1;
			this.uiCommandManager1.CommandBars.AddRange(new Janus.Windows.UI.CommandBars.UICommandBar[] {
            this.uiCommandBar1});
			this.uiCommandManager1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdExplore,
            this.cmdOpen,
            this.cmdOpenWith,
            this.cmdSVNLog,
            this.cmdDiff,
            this.cmdDiffWithPrevious,
            this.cmdDiffLocalWithBase,
            this.cmdBlame,
            this.cmdSVNUpdate,
            this.cmdRollback,
            this.menuFileWizard,
            this.cmdCommit,
            this.cmdRevert,
            this.cmdBrowse,
            this.cmdEdit,
            this.menuClipboard,
            this.cmdCopyFullName,
            this.cmdCopyName,
            this.cmdCopyURL,
            this.cmdCopyRelativeURL,
            this.cmdExport});
			this.uiCommandManager1.ContainerControl = this;
			this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] {
            this.uiContextMenu1});
			this.uiCommandManager1.Id = new System.Guid("3057650b-f916-4520-b6e8-0eb30e775936");
			this.uiCommandManager1.LeftRebar = this.LeftRebar1;
			this.uiCommandManager1.LockCommandBars = true;
			this.uiCommandManager1.RightRebar = this.RightRebar1;
			this.uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.ShowQuickCustomizeMenu = false;
			this.uiCommandManager1.TopRebar = this.TopRebar1;
			this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			// 
			// uiContextMenu1
			// 
			this.uiContextMenu1.CommandManager = this.uiCommandManager1;
			this.uiContextMenu1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdSVNUpdate2,
            this.cmdCommit2,
            this.cmdRevert2,
            this.cmdRollback1,
            this.Separator3,
            this.cmdSVNLog1,
            this.cmdDiffWithPrevious2,
            this.cmdDiffLocalWithBase2,
            this.cmdBlame1,
            this.Separator1,
            this.cmdExplore1,
            this.cmdBrowse2,
            this.cmdOpen1,
            this.cmdOpenWith2,
            this.cmdEdit2,
            this.cmdExport1,
            this.menuClipboard1,
            this.Separator5,
            this.menuFileWizard1});
			this.uiContextMenu1.Key = "ContextMenu1";
			// 
			// cmdSVNUpdate2
			// 
			this.cmdSVNUpdate2.Key = "cmdSVNUpdate";
			this.cmdSVNUpdate2.Name = "cmdSVNUpdate2";
			this.cmdSVNUpdate2.Text = "&Update to this revision";
			// 
			// cmdCommit2
			// 
			this.cmdCommit2.Key = "cmdCommit";
			this.cmdCommit2.Name = "cmdCommit2";
			this.cmdCommit2.Text = "&Commit";
			// 
			// cmdRevert2
			// 
			this.cmdRevert2.Key = "cmdRevert";
			this.cmdRevert2.Name = "cmdRevert2";
			this.cmdRevert2.Text = "Re&vert";
			// 
			// cmdRollback1
			// 
			this.cmdRollback1.Key = "cmdRollback";
			this.cmdRollback1.Name = "cmdRollback1";
			this.cmdRollback1.Text = "&Rollback this revision";
			// 
			// Separator3
			// 
			this.Separator3.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator3.Key = "Separator";
			this.Separator3.Name = "Separator3";
			// 
			// cmdSVNLog1
			// 
			this.cmdSVNLog1.Key = "cmdSVNLog";
			this.cmdSVNLog1.Name = "cmdSVNLog1";
			this.cmdSVNLog1.Text = "Show &log";
			// 
			// cmdDiffWithPrevious2
			// 
			this.cmdDiffWithPrevious2.Key = "cmdDiffWithPrevious";
			this.cmdDiffWithPrevious2.Name = "cmdDiffWithPrevious2";
			// 
			// cmdDiffLocalWithBase2
			// 
			this.cmdDiffLocalWithBase2.Key = "cmdDiffLocalWithBase";
			this.cmdDiffLocalWithBase2.Name = "cmdDiffLocalWithBase2";
			// 
			// cmdBlame1
			// 
			this.cmdBlame1.Key = "cmdBlame";
			this.cmdBlame1.Name = "cmdBlame1";
			this.cmdBlame1.Text = "&Blame";
			// 
			// Separator1
			// 
			this.Separator1.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator1.Key = "Separator";
			this.Separator1.Name = "Separator1";
			// 
			// cmdExplore1
			// 
			this.cmdExplore1.Key = "cmdExplore";
			this.cmdExplore1.Name = "cmdExplore1";
			this.cmdExplore1.Text = "&Explore";
			// 
			// cmdBrowse2
			// 
			this.cmdBrowse2.Key = "cmdBrowse";
			this.cmdBrowse2.Name = "cmdBrowse2";
			this.cmdBrowse2.Text = "Bro&wse";
			// 
			// cmdOpen1
			// 
			this.cmdOpen1.Key = "cmdOpen";
			this.cmdOpen1.Name = "cmdOpen1";
			this.cmdOpen1.Text = "&Open";
			// 
			// cmdOpenWith2
			// 
			this.cmdOpenWith2.Key = "cmdOpenWith";
			this.cmdOpenWith2.Name = "cmdOpenWith2";
			this.cmdOpenWith2.Text = "Open &with...";
			// 
			// cmdEdit2
			// 
			this.cmdEdit2.Key = "cmdEdit";
			this.cmdEdit2.Name = "cmdEdit2";
			this.cmdEdit2.Text = "Edi&t";
			// 
			// cmdExport1
			// 
			this.cmdExport1.Key = "cmdExport";
			this.cmdExport1.Name = "cmdExport1";
			// 
			// menuClipboard1
			// 
			this.menuClipboard1.Key = "menuClipboard";
			this.menuClipboard1.Name = "menuClipboard1";
			this.menuClipboard1.Text = "Cop&y to clipboard";
			// 
			// Separator5
			// 
			this.Separator5.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator5.Key = "Separator";
			this.Separator5.Name = "Separator5";
			// 
			// menuFileWizard1
			// 
			this.menuFileWizard1.Key = "menuFileWizard";
			this.menuFileWizard1.Name = "menuFileWizard1";
			this.menuFileWizard1.Text = "&Monitor this file";
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
			this.uiCommandBar1.CommandManager = this.uiCommandManager1;
			this.uiCommandBar1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdSVNUpdate1,
            this.cmdCommit1,
            this.cmdRevert1,
            this.cmdRollback2,
            this.Separator4,
            this.cmdSVNLog2,
            this.cmdDiff2,
            this.cmdBlame2,
            this.Separator2,
            this.cmdExplore2,
            this.cmdBrowse1,
            this.cmdOpen2,
            this.cmdOpenWith3,
            this.cmdEdit1,
            this.Separator6,
            this.menuFileWizard2});
			this.uiCommandBar1.FullRow = true;
			this.uiCommandBar1.Key = "CommandBar1";
			this.uiCommandBar1.Location = new System.Drawing.Point(0, 0);
			this.uiCommandBar1.Name = "uiCommandBar1";
			this.uiCommandBar1.RowIndex = 0;
			this.uiCommandBar1.Size = new System.Drawing.Size(923, 28);
			this.uiCommandBar1.Text = "CommandBar1";
			// 
			// cmdSVNUpdate1
			// 
			this.cmdSVNUpdate1.Key = "cmdSVNUpdate";
			this.cmdSVNUpdate1.Name = "cmdSVNUpdate1";
			this.cmdSVNUpdate1.Text = "Update";
			// 
			// cmdCommit1
			// 
			this.cmdCommit1.Key = "cmdCommit";
			this.cmdCommit1.Name = "cmdCommit1";
			// 
			// cmdRevert1
			// 
			this.cmdRevert1.Key = "cmdRevert";
			this.cmdRevert1.Name = "cmdRevert1";
			// 
			// cmdRollback2
			// 
			this.cmdRollback2.Key = "cmdRollback";
			this.cmdRollback2.Name = "cmdRollback2";
			this.cmdRollback2.Text = "Rollback";
			this.cmdRollback2.ToolTipText = "Rollback this revision";
			// 
			// Separator4
			// 
			this.Separator4.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator4.Key = "Separator";
			this.Separator4.Name = "Separator4";
			// 
			// cmdSVNLog2
			// 
			this.cmdSVNLog2.Key = "cmdSVNLog";
			this.cmdSVNLog2.Name = "cmdSVNLog2";
			// 
			// cmdDiff2
			// 
			this.cmdDiff2.Key = "cmdDiff";
			this.cmdDiff2.Name = "cmdDiff2";
			// 
			// cmdBlame2
			// 
			this.cmdBlame2.Key = "cmdBlame";
			this.cmdBlame2.Name = "cmdBlame2";
			// 
			// Separator2
			// 
			this.Separator2.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator2.Key = "Separator";
			this.Separator2.Name = "Separator2";
			// 
			// cmdExplore2
			// 
			this.cmdExplore2.Key = "cmdExplore";
			this.cmdExplore2.Name = "cmdExplore2";
			// 
			// cmdBrowse1
			// 
			this.cmdBrowse1.Key = "cmdBrowse";
			this.cmdBrowse1.Name = "cmdBrowse1";
			// 
			// cmdOpen2
			// 
			this.cmdOpen2.Key = "cmdOpen";
			this.cmdOpen2.Name = "cmdOpen2";
			// 
			// cmdOpenWith3
			// 
			this.cmdOpenWith3.Key = "cmdOpenWith";
			this.cmdOpenWith3.Name = "cmdOpenWith3";
			// 
			// cmdEdit1
			// 
			this.cmdEdit1.Key = "cmdEdit";
			this.cmdEdit1.Name = "cmdEdit1";
			// 
			// Separator6
			// 
			this.Separator6.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator6.Key = "Separator";
			this.Separator6.Name = "Separator6";
			// 
			// menuFileWizard2
			// 
			this.menuFileWizard2.Key = "menuFileWizard";
			this.menuFileWizard2.Name = "menuFileWizard2";
			this.menuFileWizard2.Text = "Monitor";
			// 
			// cmdOpen
			// 
			this.cmdOpen.Image = ((System.Drawing.Image)(resources.GetObject("cmdOpen.Image")));
			this.cmdOpen.Key = "cmdOpen";
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Text = "Open";
			this.cmdOpen.ToolTipText = "Open";
			// 
			// cmdOpenWith
			// 
			this.cmdOpenWith.Image = ((System.Drawing.Image)(resources.GetObject("cmdOpenWith.Image")));
			this.cmdOpenWith.Key = "cmdOpenWith";
			this.cmdOpenWith.Name = "cmdOpenWith";
			this.cmdOpenWith.Text = "Open with...";
			this.cmdOpenWith.ToolTipText = "Open with...";
			// 
			// cmdSVNLog
			// 
			this.cmdSVNLog.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNLog.Image")));
			this.cmdSVNLog.Key = "cmdSVNLog";
			this.cmdSVNLog.Name = "cmdSVNLog";
			this.cmdSVNLog.Text = "Show log";
			this.cmdSVNLog.ToolTipText = "Show log";
			// 
			// cmdDiff
			// 
			this.cmdDiff.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdDiffWithPrevious1,
            this.cmdDiffLocalWithBase1});
			this.cmdDiff.Image = ((System.Drawing.Image)(resources.GetObject("cmdDiff.Image")));
			this.cmdDiff.Key = "cmdDiff";
			this.cmdDiff.Name = "cmdDiff";
			this.cmdDiff.Text = "Diff";
			this.cmdDiff.ToolTipText = "Diff with previous version";
			// 
			// cmdDiffWithPrevious1
			// 
			this.cmdDiffWithPrevious1.Key = "cmdDiffWithPrevious";
			this.cmdDiffWithPrevious1.Name = "cmdDiffWithPrevious1";
			// 
			// cmdDiffLocalWithBase1
			// 
			this.cmdDiffLocalWithBase1.Key = "cmdDiffLocalWithBase";
			this.cmdDiffLocalWithBase1.Name = "cmdDiffLocalWithBase1";
			// 
			// cmdDiffWithPrevious
			// 
			this.cmdDiffWithPrevious.Image = ((System.Drawing.Image)(resources.GetObject("cmdDiffWithPrevious.Image")));
			this.cmdDiffWithPrevious.Key = "cmdDiffWithPrevious";
			this.cmdDiffWithPrevious.Name = "cmdDiffWithPrevious";
			this.cmdDiffWithPrevious.Text = "Diff With &Previous";
			// 
			// cmdDiffLocalWithBase
			// 
			this.cmdDiffLocalWithBase.Image = ((System.Drawing.Image)(resources.GetObject("cmdDiffLocalWithBase.Image")));
			this.cmdDiffLocalWithBase.Key = "cmdDiffLocalWithBase";
			this.cmdDiffLocalWithBase.Name = "cmdDiffLocalWithBase";
			this.cmdDiffLocalWithBase.Text = "&Diff With Base";
			// 
			// cmdBlame
			// 
			this.cmdBlame.Image = ((System.Drawing.Image)(resources.GetObject("cmdBlame.Image")));
			this.cmdBlame.Key = "cmdBlame";
			this.cmdBlame.Name = "cmdBlame";
			this.cmdBlame.Text = "Blame";
			this.cmdBlame.ToolTipText = "Blame";
			// 
			// cmdSVNUpdate
			// 
			this.cmdSVNUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNUpdate.Image")));
			this.cmdSVNUpdate.Key = "cmdSVNUpdate";
			this.cmdSVNUpdate.Name = "cmdSVNUpdate";
			this.cmdSVNUpdate.Text = "Update to this revision";
			this.cmdSVNUpdate.ToolTipText = "Update to this revision";
			// 
			// cmdRollback
			// 
			this.cmdRollback.Image = ((System.Drawing.Image)(resources.GetObject("cmdRollback.Image")));
			this.cmdRollback.Key = "cmdRollback";
			this.cmdRollback.Name = "cmdRollback";
			this.cmdRollback.Text = "Rollback this revision";
			this.cmdRollback.ToolTipText = "Rollback this revision";
			// 
			// menuFileWizard
			// 
			this.menuFileWizard.Image = ((System.Drawing.Image)(resources.GetObject("menuFileWizard.Image")));
			this.menuFileWizard.Key = "menuFileWizard";
			this.menuFileWizard.Name = "menuFileWizard";
			this.menuFileWizard.Text = "Monitor this file";
			// 
			// cmdCommit
			// 
			this.cmdCommit.Image = ((System.Drawing.Image)(resources.GetObject("cmdCommit.Image")));
			this.cmdCommit.Key = "cmdCommit";
			this.cmdCommit.Name = "cmdCommit";
			this.cmdCommit.Text = "Commit";
			this.cmdCommit.ToolTipText = "Commit";
			// 
			// cmdRevert
			// 
			this.cmdRevert.Image = ((System.Drawing.Image)(resources.GetObject("cmdRevert.Image")));
			this.cmdRevert.Key = "cmdRevert";
			this.cmdRevert.Name = "cmdRevert";
			this.cmdRevert.Text = "Revert";
			this.cmdRevert.ToolTipText = "Revert";
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.Image")));
			this.cmdBrowse.Key = "cmdBrowse";
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Text = "Browse";
			this.cmdBrowse.ToolTipText = "Browse";
			// 
			// cmdEdit
			// 
			this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
			this.cmdEdit.Key = "cmdEdit";
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.Text = "Edit";
			this.cmdEdit.ToolTipText = "Edit";
			// 
			// menuClipboard
			// 
			this.menuClipboard.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdCopyFullName1,
            this.cmdCopyName1,
            this.cmdCopyRelativeUrl1,
            this.cmdCopyURL1});
			this.menuClipboard.Image = ((System.Drawing.Image)(resources.GetObject("menuClipboard.Image")));
			this.menuClipboard.Key = "menuClipboard";
			this.menuClipboard.Name = "menuClipboard";
			this.menuClipboard.Text = "Copy to clipboard";
			// 
			// cmdCopyFullName1
			// 
			this.cmdCopyFullName1.Key = "cmdCopyFullName";
			this.cmdCopyFullName1.Name = "cmdCopyFullName1";
			// 
			// cmdCopyName1
			// 
			this.cmdCopyName1.Key = "cmdCopyName";
			this.cmdCopyName1.Name = "cmdCopyName1";
			// 
			// cmdCopyRelativeUrl1
			// 
			this.cmdCopyRelativeUrl1.Key = "cmdCopyRelativeURL";
			this.cmdCopyRelativeUrl1.Name = "cmdCopyRelativeUrl1";
			// 
			// cmdCopyURL1
			// 
			this.cmdCopyURL1.Key = "cmdCopyURL";
			this.cmdCopyURL1.Name = "cmdCopyURL1";
			// 
			// cmdCopyFullName
			// 
			this.cmdCopyFullName.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopyFullName.Image")));
			this.cmdCopyFullName.Key = "cmdCopyFullName";
			this.cmdCopyFullName.Name = "cmdCopyFullName";
			this.cmdCopyFullName.Text = "&Full name";
			// 
			// cmdCopyName
			// 
			this.cmdCopyName.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopyName.Image")));
			this.cmdCopyName.Key = "cmdCopyName";
			this.cmdCopyName.Name = "cmdCopyName";
			this.cmdCopyName.Text = "&Name";
			// 
			// cmdCopyURL
			// 
			this.cmdCopyURL.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopyURL.Image")));
			this.cmdCopyURL.Key = "cmdCopyURL";
			this.cmdCopyURL.Name = "cmdCopyURL";
			this.cmdCopyURL.Text = "&URL";
			// 
			// cmdCopyRelativeURL
			// 
			this.cmdCopyRelativeURL.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopyRelativeURL.Image")));
			this.cmdCopyRelativeURL.Key = "cmdCopyRelativeURL";
			this.cmdCopyRelativeURL.Name = "cmdCopyRelativeURL";
			this.cmdCopyRelativeURL.Text = "&Relative URL";
			// 
			// cmdExport
			// 
			this.cmdExport.Image = ((System.Drawing.Image)(resources.GetObject("cmdExport.Image")));
			this.cmdExport.Key = "cmdExport";
			this.cmdExport.Name = "cmdExport";
			this.cmdExport.Text = "&Save this revision...";
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
			this.TopRebar1.Size = new System.Drawing.Size(923, 28);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.DimGray;
			this.panel1.Controls.Add(this.pathsGrid1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 28);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.panel1.Size = new System.Drawing.Size(923, 122);
			this.panel1.TabIndex = 5;
			// 
			// PathsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TopRebar1);
			this.Name = "PathsPanel";
			this.Size = new System.Drawing.Size(923, 150);
			((System.ComponentModel.ISupportInitialize)(this.pathsGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).EndInit();
			this.TopRebar1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Janus.Windows.UI.CommandBars.UIRebar BottomRebar1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBlame;
		private Janus.Windows.UI.CommandBars.UICommand cmdBlame1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBlame2;
		private Janus.Windows.UI.CommandBars.UICommand cmdBrowse;
		private Janus.Windows.UI.CommandBars.UICommand cmdBrowse1;
		private Janus.Windows.UI.CommandBars.UICommand cmdBrowse2;
		private Janus.Windows.UI.CommandBars.UICommand cmdCommit;
		private Janus.Windows.UI.CommandBars.UICommand cmdCommit1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCommit2;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyFullName;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyFullName1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyName;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyName1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyRelativeURL;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyRelativeUrl1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyURL;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyURL1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiff;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiff2;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiffLocalWithBase;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiffLocalWithBase1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiffLocalWithBase2;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiffWithPrevious;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiffWithPrevious1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiffWithPrevious2;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit1;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit2;
		private Janus.Windows.UI.CommandBars.UICommand cmdExplore;
		private Janus.Windows.UI.CommandBars.UICommand cmdExplore1;
		private Janus.Windows.UI.CommandBars.UICommand cmdExplore2;
		private Janus.Windows.UI.CommandBars.UICommand cmdExport;
		private Janus.Windows.UI.CommandBars.UICommand cmdExport1;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpen;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpen1;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpen2;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpenWith;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpenWith2;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpenWith3;
		private Janus.Windows.UI.CommandBars.UICommand cmdRevert;
		private Janus.Windows.UI.CommandBars.UICommand cmdRevert1;
		private Janus.Windows.UI.CommandBars.UICommand cmdRevert2;
		private Janus.Windows.UI.CommandBars.UICommand cmdRollback;
		private Janus.Windows.UI.CommandBars.UICommand cmdRollback1;
		private Janus.Windows.UI.CommandBars.UICommand cmdRollback2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNLog;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNLog1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNLog2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate2;
		private Janus.Windows.UI.CommandBars.UIRebar LeftRebar1;
		private System.Timers.Timer logEntrySelectionTimer;
		private Janus.Windows.UI.CommandBars.UICommand menuClipboard;
		private Janus.Windows.UI.CommandBars.UICommand menuClipboard1;
		private Janus.Windows.UI.CommandBars.UICommand menuFileWizard;
		private Janus.Windows.UI.CommandBars.UICommand menuFileWizard1;
		private Janus.Windows.UI.CommandBars.UICommand menuFileWizard2;
		private System.Windows.Forms.Panel panel1;
		private System.Timers.Timer pathSelectionTimer;
		private SVNMonitor.View.Controls.PathsGrid pathsGrid1;
		private Janus.Windows.UI.CommandBars.UIRebar RightRebar1;
		private Janus.Windows.UI.CommandBars.UICommand Separator1;
		private Janus.Windows.UI.CommandBars.UICommand Separator2;
		private Janus.Windows.UI.CommandBars.UICommand Separator3;
		private Janus.Windows.UI.CommandBars.UICommand Separator4;
		private Janus.Windows.UI.CommandBars.UICommand Separator5;
		private Janus.Windows.UI.CommandBars.UICommand Separator6;
		private Janus.Windows.UI.CommandBars.UIRebar TopRebar1;
		private Janus.Windows.UI.CommandBars.UICommandBar uiCommandBar1;
		private Janus.Windows.UI.CommandBars.UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
	}
}