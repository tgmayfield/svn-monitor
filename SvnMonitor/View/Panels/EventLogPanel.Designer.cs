namespace SVNMonitor.View.Panels
{
	internal partial class EventLogPanel
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
			Janus.Windows.GridEX.GridEXLayout eventLogGrid1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			Janus.Windows.Common.Layouts.JanusLayoutReference eventLogGrid1_DesignTimeLayout_Reference_0 = new Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.HeaderImage");
			Janus.Windows.Common.Layouts.JanusLayoutReference eventLogGrid1_DesignTimeLayout_Reference_1 = new Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.Image");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventLogPanel));
			this.eventLogGrid1 = new SVNMonitor.View.Controls.EventLogGrid();
			this.uiCommandManager1 = new Janus.Windows.UI.CommandBars.UICommandManager(this.components);
			this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			this.cmdExport2 = new Janus.Windows.UI.CommandBars.UICommand("cmdExport");
			this.cmdCopy2 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopy");
			this.cmdCopyError2 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyError");
			this.cmdDeleteAll2 = new Janus.Windows.UI.CommandBars.UICommand("cmdDeleteAll");
			this.BottomRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.uiCommandBar1 = new Janus.Windows.UI.CommandBars.UICommandBar();
			this.cmdExport1 = new Janus.Windows.UI.CommandBars.UICommand("cmdExport");
			this.cmdCopy1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopy");
			this.cmdCopyError1 = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyError");
			this.cmdDeleteAll1 = new Janus.Windows.UI.CommandBars.UICommand("cmdDeleteAll");
			this.cmdExport = new Janus.Windows.UI.CommandBars.UICommand("cmdExport");
			this.cmdOpen = new Janus.Windows.UI.CommandBars.UICommand("cmdOpen");
			this.cmdCopy = new Janus.Windows.UI.CommandBars.UICommand("cmdCopy");
			this.cmdCopyError = new Janus.Windows.UI.CommandBars.UICommand("cmdCopyError");
			this.cmdDeleteAll = new Janus.Windows.UI.CommandBars.UICommand("cmdDeleteAll");
			this.LeftRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.RightRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.TopRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.eventLogGrid1)).BeginInit();
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
			// eventLogGrid1
			// 
			this.eventLogGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			this.eventLogGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			this.eventLogGrid1.ColumnAutoResize = true;
			this.uiCommandManager1.SetContextMenu(this.eventLogGrid1, this.uiContextMenu1);
			eventLogGrid1_DesignTimeLayout_Reference_0.Instance = ((object)(resources.GetObject("eventLogGrid1_DesignTimeLayout_Reference_0.Instance")));
			eventLogGrid1_DesignTimeLayout_Reference_1.Instance = ((object)(resources.GetObject("eventLogGrid1_DesignTimeLayout_Reference_1.Instance")));
			eventLogGrid1_DesignTimeLayout.LayoutReferences.AddRange(new Janus.Windows.Common.Layouts.JanusLayoutReference[] {
            eventLogGrid1_DesignTimeLayout_Reference_0,
            eventLogGrid1_DesignTimeLayout_Reference_1});
			eventLogGrid1_DesignTimeLayout.LayoutString = resources.GetString("eventLogGrid1_DesignTimeLayout.LayoutString");
			this.eventLogGrid1.DesignTimeLayout = eventLogGrid1_DesignTimeLayout;
			this.eventLogGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.eventLogGrid1.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
			this.eventLogGrid1.GridLineColor = System.Drawing.SystemColors.Control;
			this.eventLogGrid1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal;
			this.eventLogGrid1.GroupByBoxVisible = false;
			this.eventLogGrid1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
			this.eventLogGrid1.Location = new System.Drawing.Point(0, 1);
			this.eventLogGrid1.Name = "eventLogGrid1";
			this.eventLogGrid1.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
			this.eventLogGrid1.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
			this.eventLogGrid1.SettingsKey = "eventLogGrid1";
			this.eventLogGrid1.Size = new System.Drawing.Size(591, 288);
			this.eventLogGrid1.TabIndex = 0;
			this.eventLogGrid1.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation;
			this.eventLogGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			// 
			// uiCommandManager1
			// 
			this.uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandManager1.BottomRebar = this.BottomRebar1;
			this.uiCommandManager1.CommandBars.AddRange(new Janus.Windows.UI.CommandBars.UICommandBar[] {
            this.uiCommandBar1});
			this.uiCommandManager1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdExport,
            this.cmdOpen,
            this.cmdCopy,
            this.cmdCopyError,
            this.cmdDeleteAll});
			this.uiCommandManager1.ContainerControl = this;
			this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] {
            this.uiContextMenu1});
			this.uiCommandManager1.Id = new System.Guid("cf934eb0-aa69-4ae6-b41b-8ea4204b5814");
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
            this.cmdExport2,
            this.cmdCopy2,
            this.cmdCopyError2,
            this.cmdDeleteAll2});
			this.uiContextMenu1.Key = "ContextMenu1";
			// 
			// cmdExport2
			// 
			this.cmdExport2.Key = "cmdExport";
			this.cmdExport2.Name = "cmdExport2";
			// 
			// cmdCopy2
			// 
			this.cmdCopy2.Key = "cmdCopy";
			this.cmdCopy2.Name = "cmdCopy2";
			// 
			// cmdCopyError2
			// 
			this.cmdCopyError2.Key = "cmdCopyError";
			this.cmdCopyError2.Name = "cmdCopyError2";
			// 
			// cmdDeleteAll2
			// 
			this.cmdDeleteAll2.Key = "cmdDeleteAll";
			this.cmdDeleteAll2.Name = "cmdDeleteAll2";
			// 
			// BottomRebar1
			// 
			this.BottomRebar1.CommandManager = this.uiCommandManager1;
			this.BottomRebar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomRebar1.Location = new System.Drawing.Point(0, 317);
			this.BottomRebar1.Name = "BottomRebar1";
			this.BottomRebar1.Size = new System.Drawing.Size(591, 0);
			// 
			// uiCommandBar1
			// 
			this.uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.Animation = Janus.Windows.UI.DropDownAnimation.System;
			this.uiCommandBar1.CommandManager = this.uiCommandManager1;
			this.uiCommandBar1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdExport1,
            this.cmdCopy1,
            this.cmdCopyError1,
            this.cmdDeleteAll1});
			this.uiCommandBar1.FullRow = true;
			this.uiCommandBar1.Key = "CommandBar1";
			this.uiCommandBar1.Location = new System.Drawing.Point(0, 0);
			this.uiCommandBar1.LockCommandBar = Janus.Windows.UI.InheritableBoolean.True;
			this.uiCommandBar1.Name = "uiCommandBar1";
			this.uiCommandBar1.RowIndex = 0;
			this.uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.Size = new System.Drawing.Size(591, 28);
			this.uiCommandBar1.Text = "CommandBar1";
			// 
			// cmdExport1
			// 
			this.cmdExport1.Key = "cmdExport";
			this.cmdExport1.Name = "cmdExport1";
			// 
			// cmdCopy1
			// 
			this.cmdCopy1.Key = "cmdCopy";
			this.cmdCopy1.Name = "cmdCopy1";
			// 
			// cmdCopyError1
			// 
			this.cmdCopyError1.Key = "cmdCopyError";
			this.cmdCopyError1.Name = "cmdCopyError1";
			// 
			// cmdDeleteAll1
			// 
			this.cmdDeleteAll1.Key = "cmdDeleteAll";
			this.cmdDeleteAll1.Name = "cmdDeleteAll1";
			// 
			// cmdExport
			// 
			this.cmdExport.Image = ((System.Drawing.Image)(resources.GetObject("cmdExport.Image")));
			this.cmdExport.Key = "cmdExport";
			this.cmdExport.Name = "cmdExport";
			this.cmdExport.Text = "Export";
			this.cmdExport.ToolTipText = "Export list to file";
			// 
			// cmdOpen
			// 
			this.cmdOpen.Image = ((System.Drawing.Image)(resources.GetObject("cmdOpen.Image")));
			this.cmdOpen.Key = "cmdOpen";
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Text = "Open";
			this.cmdOpen.ToolTipText = "Open selected entry";
			// 
			// cmdCopy
			// 
			this.cmdCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopy.Image")));
			this.cmdCopy.Key = "cmdCopy";
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Text = "Copy";
			this.cmdCopy.ToolTipText = "Copy details to clipboard";
			// 
			// cmdCopyError
			// 
			this.cmdCopyError.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopyError.Image")));
			this.cmdCopyError.Key = "cmdCopyError";
			this.cmdCopyError.Name = "cmdCopyError";
			this.cmdCopyError.Text = "Copy Error";
			this.cmdCopyError.ToolTipText = "Copy error details to clipboard";
			// 
			// cmdDeleteAll
			// 
			this.cmdDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeleteAll.Image")));
			this.cmdDeleteAll.Key = "cmdDeleteAll";
			this.cmdDeleteAll.Name = "cmdDeleteAll";
			this.cmdDeleteAll.Text = "Delete All";
			this.cmdDeleteAll.ToolTipText = "Delete all messages";
			// 
			// LeftRebar1
			// 
			this.LeftRebar1.CommandManager = this.uiCommandManager1;
			this.LeftRebar1.Dock = System.Windows.Forms.DockStyle.Left;
			this.LeftRebar1.Location = new System.Drawing.Point(0, 28);
			this.LeftRebar1.Name = "LeftRebar1";
			this.LeftRebar1.Size = new System.Drawing.Size(0, 289);
			// 
			// RightRebar1
			// 
			this.RightRebar1.CommandManager = this.uiCommandManager1;
			this.RightRebar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.RightRebar1.Location = new System.Drawing.Point(591, 28);
			this.RightRebar1.Name = "RightRebar1";
			this.RightRebar1.Size = new System.Drawing.Size(0, 289);
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
			this.TopRebar1.Size = new System.Drawing.Size(591, 28);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.DimGray;
			this.panel1.Controls.Add(this.eventLogGrid1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 28);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.panel1.Size = new System.Drawing.Size(591, 289);
			this.panel1.TabIndex = 1;
			// 
			// EventLogPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TopRebar1);
			this.Name = "EventLogPanel";
			this.Size = new System.Drawing.Size(591, 317);
			((System.ComponentModel.ISupportInitialize)(this.eventLogGrid1)).EndInit();
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
		private Janus.Windows.UI.CommandBars.UICommand cmdCopy;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopy1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopy2;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyError;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyError1;
		private Janus.Windows.UI.CommandBars.UICommand cmdCopyError2;
		private Janus.Windows.UI.CommandBars.UICommand cmdDeleteAll;
		private Janus.Windows.UI.CommandBars.UICommand cmdDeleteAll1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDeleteAll2;
		private Janus.Windows.UI.CommandBars.UICommand cmdExport;
		private Janus.Windows.UI.CommandBars.UICommand cmdExport1;
		private Janus.Windows.UI.CommandBars.UICommand cmdExport2;
		private Janus.Windows.UI.CommandBars.UICommand cmdOpen;
		private SVNMonitor.View.Controls.EventLogGrid eventLogGrid1;
		private Janus.Windows.UI.CommandBars.UIRebar LeftRebar1;
		private System.Windows.Forms.Panel panel1;
		private Janus.Windows.UI.CommandBars.UIRebar RightRebar1;
		private Janus.Windows.UI.CommandBars.UIRebar TopRebar1;
		private Janus.Windows.UI.CommandBars.UICommandBar uiCommandBar1;
		private Janus.Windows.UI.CommandBars.UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
	}
}