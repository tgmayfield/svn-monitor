namespace SVNMonitor.View.Panels
{
	internal partial class LogEntriesPanel : GridPanel
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
			Janus.Windows.GridEX.GridEXLayout logEntriesGrid1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogEntriesPanel));
			this.logEntriesGrid1 = new SVNMonitor.View.Controls.LogEntriesGrid();
			this.uiCommandManager1 = new Janus.Windows.UI.CommandBars.UICommandManager(this.components);
			this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			this.cmdSVNUpdate2 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdRollback1 = new Janus.Windows.UI.CommandBars.UICommand("cmdRollback");
			this.cmdRecommend1 = new Janus.Windows.UI.CommandBars.UICommand("cmdRecommend");
			this.Separator5 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdSVNLog1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNLog");
			this.cmdShowDetails2 = new Janus.Windows.UI.CommandBars.UICommand("cmdShowDetails");
			this.cmdDiff1 = new Janus.Windows.UI.CommandBars.UICommand("cmdDiff");
			this.menuClipboard1 = new Janus.Windows.UI.CommandBars.UICommand("menuClipboard");
			this.Separator1 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.menuAuthorWizards1 = new Janus.Windows.UI.CommandBars.UICommand("menuAuthorWizards");
			this.BottomRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.uiCommandBar1 = new Janus.Windows.UI.CommandBars.UICommandBar();
			this.cmdSVNUpdate1 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdRollback2 = new Janus.Windows.UI.CommandBars.UICommand("cmdRollback");
			this.cmdRecommend2 = new Janus.Windows.UI.CommandBars.UICommand("cmdRecommend");
			this.Separator6 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdSVNLog2 = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNLog");
			this.cmdShowDetails1 = new Janus.Windows.UI.CommandBars.UICommand("cmdShowDetails");
			this.cmdDiff2 = new Janus.Windows.UI.CommandBars.UICommand("cmdDiff");
			this.Separator2 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.menuAuthorWizards2 = new Janus.Windows.UI.CommandBars.UICommand("menuAuthorWizards");
			this.cmdSVNLog = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNLog");
			this.menuAuthorWizards = new Janus.Windows.UI.CommandBars.UICommand("menuAuthorWizards");
			this.cmdCopyMessage1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyMessage");
			this.cmdCopyAuthor1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyAuthor");
			this.cmdCopyRevision1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyRevision");
			this.cmdCopyDate1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyDate");
			this.cmdDiff = new Janus.Windows.UI.CommandBars.UICommand("cmdDiff");
			this.cmdSVNUpdate = new Janus.Windows.UI.CommandBars.UICommand("cmdSVNUpdate");
			this.cmdRollback = new Janus.Windows.UI.CommandBars.UICommand("cmdRollback");
			this.cmdRecommend = new Janus.Windows.UI.CommandBars.UICommand("cmdRecommend");
			this.menuClipboard = new Janus.Windows.UI.CommandBars.UICommand("menuClipboard");
			this.cmdCopyMessage2 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyMessage");
			this.cmdCopyAuthor2 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyAuthor");
			this.cmdCopyRevision2 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyRevision");
			this.cmdCopyDate2 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyDate");
			this.cmdCopyPaths1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyPaths");
			this.cmdCopyLocalPaths1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyLocalPaths");
			this.cmdCopyRevision = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyRevision");
			this.cmdCopyAuthor = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyAuthor");
			this.cmdCopyDate = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyDate");
			this.cmdCopyMessage = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyMessage");
			this.cmdCopyPaths = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyPaths");
			this.cmdCopyLocalPaths = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyLocalPaths");
			this.cmdShowDetails = new Janus.Windows.UI.CommandBars.UICommand("cmdShowDetails");
			this.LeftRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.RightRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.TopRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelNote = new System.Windows.Forms.Panel();
			this.lblNote = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.logEntriesGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).BeginInit();
			this.TopRebar1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panelNote.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// logEntriesGrid1
			// 
			this.logEntriesGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			this.logEntriesGrid1.AlternatingColors = true;
			this.logEntriesGrid1.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.WhiteSmoke;
			this.logEntriesGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			this.logEntriesGrid1.ColumnAutoResize = true;
			this.uiCommandManager1.SetContextMenu(this.logEntriesGrid1, this.uiContextMenu1);
			logEntriesGrid1_DesignTimeLayout.LayoutString = resources.GetString("logEntriesGrid1_DesignTimeLayout.LayoutString");
			this.logEntriesGrid1.DesignTimeLayout = logEntriesGrid1_DesignTimeLayout;
			this.logEntriesGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logEntriesGrid1.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
			this.logEntriesGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.logEntriesGrid1.GridLineColor = System.Drawing.Color.Gainsboro;
			this.logEntriesGrid1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal;
			this.logEntriesGrid1.GroupByBoxVisible = false;
			this.logEntriesGrid1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
			this.logEntriesGrid1.Location = new System.Drawing.Point(0, 23);
			this.logEntriesGrid1.Name = "logEntriesGrid1";
			this.logEntriesGrid1.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
			this.logEntriesGrid1.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
			this.logEntriesGrid1.SelectedFormatStyle.ForeColor = System.Drawing.Color.White;
			this.logEntriesGrid1.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
			this.logEntriesGrid1.SettingsKey = "logEntriesGrid1";
			this.logEntriesGrid1.Size = new System.Drawing.Size(579, 331);
			this.logEntriesGrid1.TabIndex = 4;
			this.logEntriesGrid1.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation;
			this.logEntriesGrid1.ThemedAreas = ((Janus.Windows.GridEX.ThemedArea)(((((((((Janus.Windows.GridEX.ThemedArea.ScrollBars | Janus.Windows.GridEX.ThemedArea.EditControls) 
            | Janus.Windows.GridEX.ThemedArea.Headers) 
            | Janus.Windows.GridEX.ThemedArea.GroupByBox) 
            | Janus.Windows.GridEX.ThemedArea.TreeGliphs) 
            | Janus.Windows.GridEX.ThemedArea.GroupRows) 
            | Janus.Windows.GridEX.ThemedArea.ControlBorder) 
            | Janus.Windows.GridEX.ThemedArea.Cards) 
            | Janus.Windows.GridEX.ThemedArea.CheckBoxes)));
			this.logEntriesGrid1.TreeLineColor = System.Drawing.Color.Gainsboro;
			this.logEntriesGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			// 
			// uiCommandManager1
			// 
			this.uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.BottomRebar = this.BottomRebar1;
			this.uiCommandManager1.CommandBars.AddRange(new Janus.Windows.UI.CommandBars.UICommandBar[] {
            this.uiCommandBar1});
			this.uiCommandManager1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdSVNLog,
            this.menuAuthorWizards,
            this.cmdDiff,
            this.cmdSVNUpdate,
            this.cmdRollback,
            this.cmdRecommend,
            this.menuClipboard,
            this.cmdCopyRevision,
            this.cmdCopyAuthor,
            this.cmdCopyDate,
            this.cmdCopyMessage,
            this.cmdCopyPaths,
            this.cmdCopyLocalPaths,
            this.cmdShowDetails});
			this.uiCommandManager1.ContainerControl = this;
			this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] {
            this.uiContextMenu1});
			this.uiCommandManager1.Id = new System.Guid("456fbda7-0fae-48a1-84cd-e9cc0c1a5be1");
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
            this.cmdRollback1,
            this.cmdRecommend1,
            this.Separator5,
            this.cmdSVNLog1,
            this.cmdShowDetails2,
            this.cmdDiff1,
            this.menuClipboard1,
            this.Separator1,
            this.menuAuthorWizards1});
			this.uiContextMenu1.Key = "ContextMenu1";
			// 
			// cmdSVNUpdate2
			// 
			this.cmdSVNUpdate2.Key = "cmdSVNUpdate";
			this.cmdSVNUpdate2.Name = "cmdSVNUpdate2";
			this.cmdSVNUpdate2.Text = "&Update to this revision";
			// 
			// cmdRollback1
			// 
			this.cmdRollback1.Key = "cmdRollback";
			this.cmdRollback1.Name = "cmdRollback1";
			this.cmdRollback1.Text = "&Rollback this revision";
			// 
			// cmdRecommend1
			// 
			this.cmdRecommend1.Key = "cmdRecommend";
			this.cmdRecommend1.Name = "cmdRecommend1";
			this.cmdRecommend1.Text = "Rec&ommend";
			// 
			// Separator5
			// 
			this.Separator5.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator5.Key = "Separator";
			this.Separator5.Name = "Separator5";
			// 
			// cmdSVNLog1
			// 
			this.cmdSVNLog1.Key = "cmdSVNLog";
			this.cmdSVNLog1.Name = "cmdSVNLog1";
			this.cmdSVNLog1.Text = "Show &log";
			// 
			// cmdShowDetails2
			// 
			this.cmdShowDetails2.Key = "cmdShowDetails";
			this.cmdShowDetails2.Name = "cmdShowDetails2";
			// 
			// cmdDiff1
			// 
			this.cmdDiff1.Key = "cmdDiff";
			this.cmdDiff1.Name = "cmdDiff1";
			this.cmdDiff1.Text = "&Diff";
			// 
			// menuClipboard1
			// 
			this.menuClipboard1.Key = "menuClipboard";
			this.menuClipboard1.Name = "menuClipboard1";
			// 
			// Separator1
			// 
			this.Separator1.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator1.Key = "Separator";
			this.Separator1.Name = "Separator1";
			// 
			// menuAuthorWizards1
			// 
			this.menuAuthorWizards1.Key = "menuAuthorWizards";
			this.menuAuthorWizards1.Name = "menuAuthorWizards1";
			this.menuAuthorWizards1.Text = "&Monitor this author";
			// 
			// BottomRebar1
			// 
			this.BottomRebar1.CommandManager = this.uiCommandManager1;
			this.BottomRebar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomRebar1.Location = new System.Drawing.Point(0, 382);
			this.BottomRebar1.Name = "BottomRebar1";
			this.BottomRebar1.Size = new System.Drawing.Size(568, 0);
			// 
			// uiCommandBar1
			// 
			this.uiCommandBar1.CommandManager = this.uiCommandManager1;
			this.uiCommandBar1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdSVNUpdate1,
            this.cmdRollback2,
            this.cmdRecommend2,
            this.Separator6,
            this.cmdSVNLog2,
            this.cmdShowDetails1,
            this.cmdDiff2,
            this.Separator2,
            this.menuAuthorWizards2});
			this.uiCommandBar1.FullRow = true;
			this.uiCommandBar1.Key = "CommandBar1";
			this.uiCommandBar1.Location = new System.Drawing.Point(0, 0);
			this.uiCommandBar1.Name = "uiCommandBar1";
			this.uiCommandBar1.RowIndex = 0;
			this.uiCommandBar1.Size = new System.Drawing.Size(579, 28);
			this.uiCommandBar1.Text = "CommandBar1";
			// 
			// cmdSVNUpdate1
			// 
			this.cmdSVNUpdate1.Key = "cmdSVNUpdate";
			this.cmdSVNUpdate1.Name = "cmdSVNUpdate1";
			this.cmdSVNUpdate1.Text = "Update";
			// 
			// cmdRollback2
			// 
			this.cmdRollback2.Key = "cmdRollback";
			this.cmdRollback2.Name = "cmdRollback2";
			this.cmdRollback2.Text = "Rollback";
			// 
			// cmdRecommend2
			// 
			this.cmdRecommend2.Key = "cmdRecommend";
			this.cmdRecommend2.Name = "cmdRecommend2";
			// 
			// Separator6
			// 
			this.Separator6.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator6.Key = "Separator";
			this.Separator6.Name = "Separator6";
			// 
			// cmdSVNLog2
			// 
			this.cmdSVNLog2.Key = "cmdSVNLog";
			this.cmdSVNLog2.Name = "cmdSVNLog2";
			// 
			// cmdShowDetails1
			// 
			this.cmdShowDetails1.Key = "cmdShowDetails";
			this.cmdShowDetails1.Name = "cmdShowDetails1";
			// 
			// cmdDiff2
			// 
			this.cmdDiff2.Key = "cmdDiff";
			this.cmdDiff2.Name = "cmdDiff2";
			// 
			// Separator2
			// 
			this.Separator2.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator2.Key = "Separator";
			this.Separator2.Name = "Separator2";
			// 
			// menuAuthorWizards2
			// 
			this.menuAuthorWizards2.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.TextImage;
			this.menuAuthorWizards2.Key = "menuAuthorWizards";
			this.menuAuthorWizards2.Name = "menuAuthorWizards2";
			this.menuAuthorWizards2.Text = "Monitor";
			// 
			// cmdSVNLog
			// 
			this.cmdSVNLog.Image = ((System.Drawing.Image)(resources.GetObject("cmdSVNLog.Image")));
			this.cmdSVNLog.Key = "cmdSVNLog";
			this.cmdSVNLog.Name = "cmdSVNLog";
			this.cmdSVNLog.Text = "Show log";
			this.cmdSVNLog.ToolTipText = "Show log";
			// 
			// menuAuthorWizards
			// 
			this.menuAuthorWizards.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdCopyMessage1,
            this.cmdCopyAuthor1,
            this.cmdCopyRevision1,
            this.cmdCopyDate1});
			this.menuAuthorWizards.Image = ((System.Drawing.Image)(resources.GetObject("menuAuthorWizards.Image")));
			this.menuAuthorWizards.Key = "menuAuthorWizards";
			this.menuAuthorWizards.Name = "menuAuthorWizards";
			this.menuAuthorWizards.Text = "Monitor this author";
			this.menuAuthorWizards.ToolTipText = "Monitor this author";
			// 
			// cmdCopyMessage1
			// 
			this.cmdCopyMessage1.Key = "cmdCopyMessage";
			this.cmdCopyMessage1.Name = "cmdCopyMessage1";
			// 
			// cmdCopyAuthor1
			// 
			this.cmdCopyAuthor1.Key = "cmdCopyAuthor";
			this.cmdCopyAuthor1.Name = "cmdCopyAuthor1";
			// 
			// cmdCopyRevision1
			// 
			this.cmdCopyRevision1.Key = "cmdCopyRevision";
			this.cmdCopyRevision1.Name = "cmdCopyRevision1";
			// 
			// cmdCopyDate1
			// 
			this.cmdCopyDate1.Key = "cmdCopyDate";
			this.cmdCopyDate1.Name = "cmdCopyDate1";
			// 
			// cmdDiff
			// 
			this.cmdDiff.Image = ((System.Drawing.Image)(resources.GetObject("cmdDiff.Image")));
			this.cmdDiff.Key = "cmdDiff";
			this.cmdDiff.Name = "cmdDiff";
			this.cmdDiff.Text = "Diff";
			this.cmdDiff.ToolTipText = "Diff with previous version";
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
			// cmdRecommend
			// 
			this.cmdRecommend.Image = ((System.Drawing.Image)(resources.GetObject("cmdRecommend.Image")));
			this.cmdRecommend.Key = "cmdRecommend";
			this.cmdRecommend.Name = "cmdRecommend";
			this.cmdRecommend.Text = "Recommend...";
			this.cmdRecommend.ToolTipText = "Recommend this revision to other users and commit the changes";
			// 
			// menuClipboard
			// 
			this.menuClipboard.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdCopyMessage2,
            this.cmdCopyAuthor2,
            this.cmdCopyRevision2,
            this.cmdCopyDate2,
            this.cmdCopyPaths1,
            this.cmdCopyLocalPaths1});
			this.menuClipboard.Image = ((System.Drawing.Image)(resources.GetObject("menuClipboard.Image")));
			this.menuClipboard.Key = "menuClipboard";
			this.menuClipboard.Name = "menuClipboard";
			this.menuClipboard.Text = "&Copy to clipboard";
			// 
			// cmdCopyMessage2
			// 
			this.cmdCopyMessage2.Key = "cmdCopyMessage";
			this.cmdCopyMessage2.Name = "cmdCopyMessage2";
			// 
			// cmdCopyAuthor2
			// 
			this.cmdCopyAuthor2.Key = "cmdCopyAuthor";
			this.cmdCopyAuthor2.Name = "cmdCopyAuthor2";
			// 
			// cmdCopyRevision2
			// 
			this.cmdCopyRevision2.Key = "cmdCopyRevision";
			this.cmdCopyRevision2.Name = "cmdCopyRevision2";
			// 
			// cmdCopyDate2
			// 
			this.cmdCopyDate2.Key = "cmdCopyDate";
			this.cmdCopyDate2.Name = "cmdCopyDate2";
			// 
			// cmdCopyPaths1
			// 
			this.cmdCopyPaths1.Key = "cmdCopyPaths";
			this.cmdCopyPaths1.Name = "cmdCopyPaths1";
			// 
			// cmdCopyLocalPaths1
			// 
			this.cmdCopyLocalPaths1.Key = "cmdCopyLocalPaths";
			this.cmdCopyLocalPaths1.Name = "cmdCopyLocalPaths1";
			// 
			// cmdCopyRevision
			// 
			this.cmdCopyRevision.Key = "cmdCopyRevision";
			this.cmdCopyRevision.Name = "cmdCopyRevision";
			this.cmdCopyRevision.Text = "&Revision";
			// 
			// cmdCopyAuthor
			// 
			this.cmdCopyAuthor.Key = "cmdCopyAuthor";
			this.cmdCopyAuthor.Name = "cmdCopyAuthor";
			this.cmdCopyAuthor.Text = "&Author";
			// 
			// cmdCopyDate
			// 
			this.cmdCopyDate.Key = "cmdCopyDate";
			this.cmdCopyDate.Name = "cmdCopyDate";
			this.cmdCopyDate.Text = "&Date and Time";
			// 
			// cmdCopyMessage
			// 
			this.cmdCopyMessage.Key = "cmdCopyMessage";
			this.cmdCopyMessage.Name = "cmdCopyMessage";
			this.cmdCopyMessage.Text = "&Message";
			// 
			// cmdCopyPaths
			// 
			this.cmdCopyPaths.Key = "cmdCopyPaths";
			this.cmdCopyPaths.Name = "cmdCopyPaths";
			this.cmdCopyPaths.Text = "&List Of Items";
			// 
			// cmdCopyLocalPaths
			// 
			this.cmdCopyLocalPaths.Key = "cmdCopyLocalPaths";
			this.cmdCopyLocalPaths.Name = "cmdCopyLocalPaths";
			this.cmdCopyLocalPaths.Text = "List Of Local Items";
			// 
			// cmdShowDetails
			// 
			this.cmdShowDetails.Image = ((System.Drawing.Image)(resources.GetObject("cmdShowDetails.Image")));
			this.cmdShowDetails.Key = "cmdShowDetails";
			this.cmdShowDetails.Name = "cmdShowDetails";
			this.cmdShowDetails.Text = "Show details";
			// 
			// LeftRebar1
			// 
			this.LeftRebar1.CommandManager = this.uiCommandManager1;
			this.LeftRebar1.Dock = System.Windows.Forms.DockStyle.Left;
			this.LeftRebar1.Location = new System.Drawing.Point(0, 0);
			this.LeftRebar1.Name = "LeftRebar1";
			this.LeftRebar1.Size = new System.Drawing.Size(0, 382);
			// 
			// RightRebar1
			// 
			this.RightRebar1.CommandManager = this.uiCommandManager1;
			this.RightRebar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.RightRebar1.Location = new System.Drawing.Point(568, 0);
			this.RightRebar1.Name = "RightRebar1";
			this.RightRebar1.Size = new System.Drawing.Size(0, 382);
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
			this.TopRebar1.Size = new System.Drawing.Size(579, 28);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.DimGray;
			this.panel1.Controls.Add(this.logEntriesGrid1);
			this.panel1.Controls.Add(this.panelNote);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 28);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.panel1.Size = new System.Drawing.Size(579, 354);
			this.panel1.TabIndex = 5;
			// 
			// panelNote
			// 
			this.panelNote.BackColor = System.Drawing.Color.Gray;
			this.panelNote.Controls.Add(this.lblNote);
			this.panelNote.Controls.Add(this.pictureBox1);
			this.panelNote.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelNote.Location = new System.Drawing.Point(0, 1);
			this.panelNote.Name = "panelNote";
			this.panelNote.Padding = new System.Windows.Forms.Padding(2, 3, 2, 2);
			this.panelNote.Size = new System.Drawing.Size(579, 22);
			this.panelNote.TabIndex = 5;
			this.panelNote.Visible = false;
			// 
			// lblNote
			// 
			this.lblNote.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.lblNote.ForeColor = System.Drawing.Color.White;
			this.lblNote.Location = new System.Drawing.Point(20, 3);
			this.lblNote.Name = "lblNote";
			this.lblNote.Padding = new System.Windows.Forms.Padding(2, 1, 2, 2);
			this.lblNote.Size = new System.Drawing.Size(557, 17);
			this.lblNote.TabIndex = 0;
			this.lblNote.Text = "This source is readonly. All svn actions are disabled.";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(2, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(18, 17);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// LogEntriesPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TopRebar1);
			this.Name = "LogEntriesPanel";
			this.Size = new System.Drawing.Size(579, 382);
			((System.ComponentModel.ISupportInitialize)(this.logEntriesGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).EndInit();
			this.TopRebar1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panelNote.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private Janus.Windows.UI.CommandBars.UIRebar BottomRebar1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyAuthor;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyAuthor1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyAuthor2;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyDate;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyDate1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyDate2;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyLocalPaths;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyLocalPaths1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyMessage;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyMessage1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyMessage2;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyPaths;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyPaths1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyRevision;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyRevision1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyRevision2;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiff;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiff1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDiff2;
		private Janus.Windows.UI.CommandBars.UICommand cmdRecommend;
		private Janus.Windows.UI.CommandBars.UICommand cmdRecommend1;
		private Janus.Windows.UI.CommandBars.UICommand cmdRecommend2;
		private Janus.Windows.UI.CommandBars.UICommand cmdRollback;
		private Janus.Windows.UI.CommandBars.UICommand cmdRollback1;
		private Janus.Windows.UI.CommandBars.UICommand cmdRollback2;
		private Janus.Windows.UI.CommandBars.UICommand cmdShowDetails;
		private Janus.Windows.UI.CommandBars.UICommand cmdShowDetails1;
		private Janus.Windows.UI.CommandBars.UICommand cmdShowDetails2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNLog;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNLog1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNLog2;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate1;
		private Janus.Windows.UI.CommandBars.UICommand cmdSVNUpdate2;
		private System.Windows.Forms.Label lblNote;
		private Janus.Windows.UI.CommandBars.UIRebar LeftRebar1;
		private SVNMonitor.View.Controls.LogEntriesGrid logEntriesGrid1;
		private Janus.Windows.UI.CommandBars.UICommand menuAuthorWizards;
		private Janus.Windows.UI.CommandBars.UICommand menuAuthorWizards1;
		private Janus.Windows.UI.CommandBars.UICommand menuAuthorWizards2;
		private Janus.Windows.UI.CommandBars.UICommand menuClipboard;
		private Janus.Windows.UI.CommandBars.UICommand menuClipboard1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelNote;
		private System.Windows.Forms.PictureBox pictureBox1;
		private Janus.Windows.UI.CommandBars.UIRebar RightRebar1;
		private Janus.Windows.UI.CommandBars.UICommand Separator1;
		private Janus.Windows.UI.CommandBars.UICommand Separator2;
		private Janus.Windows.UI.CommandBars.UICommand Separator5;
		private Janus.Windows.UI.CommandBars.UICommand Separator6;
		private SVNMonitor.Entities.Source source;
		private Janus.Windows.UI.CommandBars.UIRebar TopRebar1;
		private Janus.Windows.UI.CommandBars.UICommandBar uiCommandBar1;
		private Janus.Windows.UI.CommandBars.UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
	}
}