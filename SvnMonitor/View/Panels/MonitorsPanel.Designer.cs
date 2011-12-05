namespace SVNMonitor.View.Panels
{
	internal partial class MonitorsPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorsPanel));
			this.cmdNew = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.cmdMoveUp = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.uiCommandManager1 = new Janus.Windows.UI.CommandBars.UICommandManager(this.components);
			this.BottomRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.uiCommandBar1 = new Janus.Windows.UI.CommandBars.UICommandBar();
			this.cmdNew1 = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit1 = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete3 = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.Separator1 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdMoveUp1 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown1 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.cmdEnabled = new Janus.Windows.UI.CommandBars.UICommand("cmdEnabled");
			this.cmdClearError = new Janus.Windows.UI.CommandBars.UICommand("cmdClearError");
			this.cmdClearAllErrors = new Janus.Windows.UI.CommandBars.UICommand("cmdClearAllErrors");
			this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			this.cmdEnabled1 = new Janus.Windows.UI.CommandBars.UICommand("cmdEnabled");
			this.cmdClearError1 = new Janus.Windows.UI.CommandBars.UICommand("cmdClearError");
			this.cmdClearAllErrors1 = new Janus.Windows.UI.CommandBars.UICommand("cmdClearAllErrors");
			this.cmdNew2 = new Janus.Windows.UI.CommandBars.UICommand("cmdNew");
			this.cmdEdit2 = new Janus.Windows.UI.CommandBars.UICommand("cmdEdit");
			this.cmdDelete2 = new Janus.Windows.UI.CommandBars.UICommand("cmdDelete");
			this.Separator2 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.cmdMoveUp2 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveUp");
			this.cmdMoveDown2 = new Janus.Windows.UI.CommandBars.UICommand("cmdMoveDown");
			this.LeftRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.RightRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.TopRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
			this.monitorsExplorerBar1 = new SVNMonitor.View.Controls.MonitorsExplorerBar();
			this.Separator3 = new Janus.Windows.UI.CommandBars.UICommand("Separator");
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).BeginInit();
			this.TopRebar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.monitorsExplorerBar1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdNew
			// 
			this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
			this.cmdNew.Key = "cmdNew";
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Text = "New Monitor";
			this.cmdNew.ToolTipText = "New monitor";
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
			this.cmdDelete.ToolTipText = "Delete monitor";
			// 
			// cmdMoveUp
			// 
			this.cmdMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveUp.Image")));
			this.cmdMoveUp.Key = "cmdMoveUp";
			this.cmdMoveUp.Name = "cmdMoveUp";
			this.cmdMoveUp.Text = "Move Up";
			this.cmdMoveUp.ToolTipText = "Move monitor up";
			// 
			// cmdMoveDown
			// 
			this.cmdMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveDown.Image")));
			this.cmdMoveDown.Key = "cmdMoveDown";
			this.cmdMoveDown.Name = "cmdMoveDown";
			this.cmdMoveDown.Text = "Move Down";
			this.cmdMoveDown.ToolTipText = "Move monitor down";
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
            this.cmdEnabled,
            this.cmdClearError,
            this.cmdClearAllErrors});
			this.uiCommandManager1.ContainerControl = this;
			this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] {
            this.uiContextMenu1});
			this.uiCommandManager1.Id = new System.Guid("45030045-5a26-407f-8868-a5a7535cf928");
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
            this.cmdDelete3,
            this.Separator1,
            this.cmdMoveUp1,
            this.cmdMoveDown1});
			this.uiCommandBar1.FullRow = true;
			this.uiCommandBar1.Key = "CommandBar1";
			this.uiCommandBar1.Location = new System.Drawing.Point(0, 0);
			this.uiCommandBar1.Name = "uiCommandBar1";
			this.uiCommandBar1.RowIndex = 0;
			this.uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			this.uiCommandBar1.Size = new System.Drawing.Size(301, 28);
			this.uiCommandBar1.Text = "Monitors";
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
			// cmdDelete3
			// 
			this.cmdDelete3.CommandStyle = Janus.Windows.UI.CommandBars.CommandStyle.Image;
			this.cmdDelete3.Key = "cmdDelete";
			this.cmdDelete3.Name = "cmdDelete3";
			// 
			// Separator1
			// 
			this.Separator1.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator1.Key = "Separator";
			this.Separator1.Name = "Separator1";
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
			// cmdEnabled
			// 
			this.cmdEnabled.CommandType = Janus.Windows.UI.CommandBars.CommandType.ToggleButton;
			this.cmdEnabled.Key = "cmdEnabled";
			this.cmdEnabled.Name = "cmdEnabled";
			this.cmdEnabled.Text = "Enabled";
			this.cmdEnabled.ToolTipText = "Enabled";
			// 
			// cmdClearError
			// 
			this.cmdClearError.Image = ((System.Drawing.Image)(resources.GetObject("cmdClearError.Image")));
			this.cmdClearError.Key = "cmdClearError";
			this.cmdClearError.Name = "cmdClearError";
			this.cmdClearError.Text = "Clear err&or";
			// 
			// cmdClearAllErrors
			// 
			this.cmdClearAllErrors.Key = "cmdClearAllErrors";
			this.cmdClearAllErrors.Name = "cmdClearAllErrors";
			this.cmdClearAllErrors.Text = "Clear All Errors";
			// 
			// uiContextMenu1
			// 
			this.uiContextMenu1.CommandManager = this.uiCommandManager1;
			this.uiContextMenu1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.cmdEnabled1,
            this.cmdClearError1,
            this.cmdClearAllErrors1,
            this.cmdNew2,
            this.cmdEdit2,
            this.cmdDelete2,
            this.Separator2,
            this.cmdMoveUp2,
            this.cmdMoveDown2});
			this.uiContextMenu1.Key = "ContextMenu1";
			// 
			// cmdEnabled1
			// 
			this.cmdEnabled1.Key = "cmdEnabled";
			this.cmdEnabled1.Name = "cmdEnabled1";
			// 
			// cmdClearError1
			// 
			this.cmdClearError1.Key = "cmdClearError";
			this.cmdClearError1.Name = "cmdClearError1";
			// 
			// cmdClearAllErrors1
			// 
			this.cmdClearAllErrors1.Key = "cmdClearAllErrors";
			this.cmdClearAllErrors1.Name = "cmdClearAllErrors1";
			// 
			// cmdNew2
			// 
			this.cmdNew2.Key = "cmdNew";
			this.cmdNew2.Name = "cmdNew2";
			this.cmdNew2.Text = "&New Monitor";
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
			this.cmdMoveUp2.Text = "Move &Up";
			// 
			// cmdMoveDown2
			// 
			this.cmdMoveDown2.Key = "cmdMoveDown";
			this.cmdMoveDown2.Name = "cmdMoveDown2";
			this.cmdMoveDown2.Text = "Move Do&wn";
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
			this.TopRebar1.Size = new System.Drawing.Size(301, 28);
			// 
			// monitorsExplorerBar1
			// 
			this.monitorsExplorerBar1.BackgroundFormatStyle.BackColor = System.Drawing.Color.White;
			this.monitorsExplorerBar1.BorderStyle = Janus.Windows.ExplorerBar.BorderStyle.None;
			this.uiCommandManager1.SetContextMenu(this.monitorsExplorerBar1, this.uiContextMenu1);
			this.monitorsExplorerBar1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.monitorsExplorerBar1.GroupSeparation = 14;
			this.monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackColor = System.Drawing.Color.Gainsboro;
			this.monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackColorGradient = System.Drawing.Color.Silver;
			this.monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			this.monitorsExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColor = System.Drawing.SystemColors.ControlDark;
			this.monitorsExplorerBar1.GroupsStateStyles.HotFormatStyle.ForeColor = System.Drawing.Color.Black;
			this.monitorsExplorerBar1.GroupsStateStyles.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Control;
			this.monitorsExplorerBar1.Location = new System.Drawing.Point(0, 1);
			this.monitorsExplorerBar1.Name = "monitorsExplorerBar1";
			this.monitorsExplorerBar1.Size = new System.Drawing.Size(301, 293);
			this.monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColor = System.Drawing.Color.DarkGray;
			this.monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColorGradient = System.Drawing.Color.DimGray;
			this.monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			this.monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.ForeColor = System.Drawing.Color.White;
			this.monitorsExplorerBar1.TabIndex = 1;
			this.monitorsExplorerBar1.ThemedAreas = Janus.Windows.ExplorerBar.ThemedArea.None;
			this.monitorsExplorerBar1.TopMargin = 14;
			// 
			// Separator3
			// 
			this.Separator3.CommandType = Janus.Windows.UI.CommandBars.CommandType.Separator;
			this.Separator3.Key = "Separator";
			this.Separator3.Name = "Separator3";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.DimGray;
			this.panel1.Controls.Add(this.monitorsExplorerBar1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 28);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.panel1.Size = new System.Drawing.Size(301, 294);
			this.panel1.TabIndex = 2;
			// 
			// MonitorsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TopRebar1);
			this.Name = "MonitorsPanel";
			this.Size = new System.Drawing.Size(301, 322);
			((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiCommandBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).EndInit();
			this.TopRebar1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.monitorsExplorerBar1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Janus.Windows.UI.CommandBars.UIRebar BottomRebar1;
		private Janus.Windows.UI.CommandBars.UICommand cmdClearAllErrors;
		private Janus.Windows.UI.CommandBars.UICommand cmdClearAllErrors1;
		private Janus.Windows.UI.CommandBars.UICommand cmdClearError;
		private Janus.Windows.UI.CommandBars.UICommand cmdClearError1;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete3;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit;
		private Janus.Windows.UI.CommandBars.UICommand cmdEnabled;
		private Janus.Windows.UI.CommandBars.UICommand cmdEnabled1;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew;
		private Janus.Windows.UI.CommandBars.UIRebar LeftRebar1;
		private SVNMonitor.View.Controls.MonitorsExplorerBar monitorsExplorerBar1;
		private System.Windows.Forms.Panel panel1;
		private Janus.Windows.UI.CommandBars.UIRebar RightRebar1;
		private Janus.Windows.UI.CommandBars.UICommand Separator3;
		private Janus.Windows.UI.CommandBars.UIRebar TopRebar1;
		private Janus.Windows.UI.CommandBars.UICommandBar uiCommandBar1;
		private Janus.Windows.UI.CommandBars.UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew1;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit1;
		private Janus.Windows.UI.CommandBars.UICommand Separator1;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp1;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown1;
		private Janus.Windows.UI.CommandBars.UICommand cmdNew2;
		private Janus.Windows.UI.CommandBars.UICommand cmdEdit2;
		private Janus.Windows.UI.CommandBars.UICommand cmdDelete2;
		private Janus.Windows.UI.CommandBars.UICommand Separator2;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveUp2;
		private Janus.Windows.UI.CommandBars.UICommand cmdMoveDown2;
	}
}