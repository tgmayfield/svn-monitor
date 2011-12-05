namespace SVNMonitor.View.Panels
{
	internal partial class ActionsPanel
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
			Janus.Windows.GridEX.GridEXLayout actionsGrid1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionsPanel));
			this.actionsGrid1 = new SVNMonitor.View.Controls.ActionsGrid();
			this.uiCommandManager1 = new Janus.Windows.UI.CommandBars.UICommandManager(this.components);
			this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			this.cmdNew2 = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit2 = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete2 = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.Separator2 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdMoveUp2 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown2 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.Separator4 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdTest2 = new Janus.Windows.UI.CommandBars.UICommand("cmdTest");
			this.BottomRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.uiCommandBar1 = new Janus.Windows.UI.CommandBars.UICommandBar();
			this.cmdNew1 = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit1 = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete1 = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.Separator1 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdMoveUp1 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown1 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.Separator3 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdTest1 = new Janus.Windows.UI.CommandBars.UICommand("cmdTest");
			this.cmdNew = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.cmdMoveUp = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.cmdTest = new Janus.Windows.UI.CommandBars.UICommand("cmdTest");
			this.LeftRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.RightRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.TopRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			((System.ComponentModel.ISupportInitialize)(this.actionsGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).BeginInit();
			this.TopRebar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// actionsGrid1
			// 
			this.actionsGrid1.Actions = null;
			this.actionsGrid1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
			this.actionsGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			this.actionsGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			this.actionsGrid1.ColumnAutoResize = true;
			this.actionsGrid1.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
			this.uiCommandManager1.SetContextMenu(this.actionsGrid1, this.uiContextMenu1);
			actionsGrid1_DesignTimeLayout.LayoutString = resources.GetString("actionsGrid1_DesignTimeLayout.LayoutString");
			this.actionsGrid1.DesignTimeLayout = actionsGrid1_DesignTimeLayout;
			this.actionsGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.actionsGrid1.FocusCellFormatStyle.BackColor = System.Drawing.Color.Gainsboro;
			this.actionsGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.actionsGrid1.GridLineColor = System.Drawing.SystemColors.ControlLight;
			this.actionsGrid1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal;
			this.actionsGrid1.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
			this.actionsGrid1.GroupByBoxVisible = false;
			this.actionsGrid1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
			this.actionsGrid1.Location = new System.Drawing.Point(0, 28);
			this.actionsGrid1.Name = "actionsGrid1";
			this.actionsGrid1.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.WhiteSmoke;
			this.actionsGrid1.Size = new System.Drawing.Size(638, 122);
			this.actionsGrid1.TabIndex = 0;
			this.actionsGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
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
            this.cmdMoveUp,
            this.cmdMoveDown,
            this.cmdTest});
			this.uiCommandManager1.ContainerControl = this;
			this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] {
            this.uiContextMenu1});
			this.uiCommandManager1.Id = new System.Guid("cad69911-d561-4dfc-abef-211805c32ca8");
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
            this.cmdNew2,
            this.cmdEdit2,
            this.cmdDelete2,
            this.Separator2,
            this.cmdMoveUp2,
            this.cmdMoveDown2,
            this.Separator4,
            this.cmdTest2});
			this.uiContextMenu1.Key = "ContextMenu1";
			// 
			// cmdNew2
			// 
			this.cmdNew2.Key = "cmdNew";
			this.cmdNew2.Name = "cmdNew2";
			// 
			// cmdEdit2
			// 
			this.cmdEdit2.Key = "cmdEdit";
			this.cmdEdit2.Name = "cmdEdit2";
			// 
			// cmdDelete2
			// 
			this.cmdDelete2.Key = "cmdDelete";
			this.cmdDelete2.Name = "cmdDelete2";
			// 
			// Separator2
			// 
			this.Separator2.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator2.Key = "Separator";
			this.Separator2.Name = "Separator2";
			// 
			// cmdMoveUp2
			// 
			this.cmdMoveUp2.Key = "cmdMoveUp";
			this.cmdMoveUp2.Name = "cmdMoveUp2";
			// 
			// cmdMoveDown2
			// 
			this.cmdMoveDown2.Key = "cmdMoveDown";
			this.cmdMoveDown2.Name = "cmdMoveDown2";
			// 
			// Separator4
			// 
			this.Separator4.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator4.Key = "Separator";
			this.Separator4.Name = "Separator4";
			// 
			// cmdTest2
			// 
			this.cmdTest2.Key = "cmdTest";
			this.cmdTest2.Name = "cmdTest2";
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
            this.Separator1,
            this.cmdMoveUp1,
            this.cmdMoveDown1,
            this.Separator3,
            this.cmdTest1});
			this.uiCommandBar1.FullRow = true;
			this.uiCommandBar1.Key = "CommandBar1";
			this.uiCommandBar1.Location = new System.Drawing.Point(0, 0);
			this.uiCommandBar1.Name = "uiCommandBar1";
			this.uiCommandBar1.RowIndex = 0;
			this.uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.Size = new System.Drawing.Size(638, 28);
			this.uiCommandBar1.Text = "Actions";
			// 
			// cmdNew1
			// 
			this.cmdNew1.Key = "cmdNew";
			this.cmdNew1.Name = "cmdNew1";
			// 
			// cmdEdit1
			// 
			this.cmdEdit1.Key = "cmdEdit";
			this.cmdEdit1.Name = "cmdEdit1";
			// 
			// cmdDelete1
			// 
			this.cmdDelete1.Key = "cmdDelete";
			this.cmdDelete1.Name = "cmdDelete1";
			// 
			// Separator1
			// 
			this.Separator1.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator1.Key = "Separator";
			this.Separator1.Name = "Separator1";
			// 
			// cmdMoveUp1
			// 
			this.cmdMoveUp1.Key = "cmdMoveUp";
			this.cmdMoveUp1.Name = "cmdMoveUp1";
			// 
			// cmdMoveDown1
			// 
			this.cmdMoveDown1.Key = "cmdMoveDown";
			this.cmdMoveDown1.Name = "cmdMoveDown1";
			// 
			// Separator3
			// 
			this.Separator3.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator3.Key = "Separator";
			this.Separator3.Name = "Separator3";
			// 
			// cmdTest1
			// 
			this.cmdTest1.Key = "cmdTest";
			this.cmdTest1.Name = "cmdTest1";
			// 
			// cmdNew
			// 
			this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
			this.cmdNew.Key = "cmdNew";
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Text = "&New";
			this.cmdNew.ToolTipText = "New action";
			// 
			// cmdEdit
			// 
			this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
			this.cmdEdit.Key = "cmdEdit";
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.Text = "&Properties";
			this.cmdEdit.ToolTipText = "Properties";
			// 
			// cmdDelete
			// 
			this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
			this.cmdDelete.Key = "cmdDelete";
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Text = "&Delete";
			this.cmdDelete.ToolTipText = "Delete action";
			// 
			// cmdMoveUp
			// 
			this.cmdMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveUp.Image")));
			this.cmdMoveUp.Key = "cmdMoveUp";
			this.cmdMoveUp.Name = "cmdMoveUp";
			this.cmdMoveUp.Text = "Move &Up";
			this.cmdMoveUp.ToolTipText = "Move action up";
			// 
			// cmdMoveDown
			// 
			this.cmdMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveDown.Image")));
			this.cmdMoveDown.Key = "cmdMoveDown";
			this.cmdMoveDown.Name = "cmdMoveDown";
			this.cmdMoveDown.Text = "Move Do&wn";
			this.cmdMoveDown.ToolTipText = "Move action down";
			// 
			// cmdTest
			// 
			this.cmdTest.Image = ((System.Drawing.Image)(resources.GetObject("cmdTest.Image")));
			this.cmdTest.Key = "cmdTest";
			this.cmdTest.Name = "cmdTest";
			this.cmdTest.Text = "&Test";
			this.cmdTest.ToolTipText = "Test";
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
			this.TopRebar1.Size = new System.Drawing.Size(638, 28);
			// 
			// ActionsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.actionsGrid1);
			this.Controls.Add(this.TopRebar1);
			this.Name = "ActionsPanel";
			this.Size = new System.Drawing.Size(638, 150);
			((System.ComponentModel.ISupportInitialize)(this.actionsGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).EndInit();
			this.TopRebar1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Collections.Generic.List<Actions.Action> actions;
		private SVNMonitor.View.Controls.ActionsGrid actionsGrid1;
		private Janus.Windows.UI.CommandBars.UIRebar BottomRebar1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete2;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit1;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit2;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown1;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown2;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp1;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp2;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew1;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew2;
		private Janus.Windows.UI.CommandBars.UICommand cmdTest;
		private Janus.Windows.UI.CommandBars.UICommand cmdTest1;
		private Janus.Windows.UI.CommandBars.UICommand cmdTest2;
		private Janus.Windows.UI.CommandBars.UIRebar LeftRebar1;
		private Janus.Windows.UI.CommandBars.UIRebar RightRebar1;
		private Janus.Windows.UI.CommandBars.UICommand Separator1;
		private Janus.Windows.UI.CommandBars.UICommand Separator2;
		private Janus.Windows.UI.CommandBars.UICommand Separator3;
		private Janus.Windows.UI.CommandBars.UICommand Separator4;
		private Janus.Windows.UI.CommandBars.UIRebar TopRebar1;
		private Janus.Windows.UI.CommandBars.UICommandBar uiCommandBar1;
		private Janus.Windows.UI.CommandBars.UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
	}
}