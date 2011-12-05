namespace SVNMonitor.View.Panels
{
    using Janus.Windows.GridEX;
    using Janus.Windows.UI;
    using Janus.Windows.UI.CommandBars;
    using SVNMonitor.Actions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.View;
    using SVNMonitor.View.Controls;
    using SVNMonitor.View.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ActionsPanel : UserControl
    {
        private List<Action> actions;
        private ActionsGrid actionsGrid1;
        private UIRebar BottomRebar1;
        private UICommand cmdDelete;
        private UICommand cmdDelete1;
        private UICommand cmdDelete2;
        private UICommand cmdEdit;
        private UICommand cmdEdit1;
        private UICommand cmdEdit2;
        private UICommand cmdMoveDown;
        private UICommand cmdMoveDown1;
        private UICommand cmdMoveDown2;
        private UICommand cmdMoveUp;
        private UICommand cmdMoveUp1;
        private UICommand cmdMoveUp2;
        private UICommand cmdNew;
        private UICommand cmdNew1;
        private UICommand cmdNew2;
        private UICommand cmdTest;
        private UICommand cmdTest1;
        private UICommand cmdTest2;
        private IContainer components;
        private UIRebar LeftRebar1;
        private UIRebar RightRebar1;
        private UICommand Separator1;
        private UICommand Separator2;
        private UICommand Separator3;
        private UICommand Separator4;
        private UIRebar TopRebar1;
        private UICommandBar uiCommandBar1;
        private UICommandManager uiCommandManager1;
        private UIContextMenu uiContextMenu1;

        public event EventHandler ActionsChanged
        {
            [DebuggerNonUserCode] add
            {
                this.Grid.RowCountChanged += value;
            }
            [DebuggerNonUserCode] remove
            {
                this.Grid.RowCountChanged -= value;
            }
        }

        public ActionsPanel()
        {
            this.InitializeComponent();
            if (!base.DesignMode)
            {
                UIHelper.ApplyResources(this.uiCommandManager1, true);
                this.EnableCommands();
            }
        }

        private void actionsGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Logger.LogUserAction("key=" + e.KeyCode);
                e.Handled = true;
                this.DeleteAction();
            }
        }

        private void actionsGrid1_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            Logger.LogUserAction();
            this.EditAction();
        }

        private void actionsGrid1_SelectionChanged(object sender, EventArgs e)
        {
            this.EnableCommands();
        }

        private void cmdDelete_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.DeleteAction();
        }

        private void cmdEdit_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.EditAction();
        }

        private void cmdMoveDown_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.MoveActionDown();
        }

        private void cmdMoveUp_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.MoveActionUp();
        }

        private void cmdNew_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.NewAction();
        }

        private void cmdTest_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.TestAction();
        }

        protected virtual void DeleteAction()
        {
            Action action = this.SelectedAction;
            if (action != null)
            {
                DialogResult result = MessageBox.Show(MainForm.FormInstance, string.Format("Delete action '{0}'?", action.DisplayName), "Delete Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                Logger.Log.InfoFormat("Delete action: User clicked {0}", result);
                if (result != DialogResult.No)
                {
                    this.Grid.Delete();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual void EditAction()
        {
            Action action = this.SelectedAction;
            if (action != null)
            {
                ActionPropertiesDialog <>g__initLocal0 = new ActionPropertiesDialog {
                    Action = action
                };
                ActionPropertiesDialog dialog = <>g__initLocal0;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int index = this.actions.IndexOf(action);
                    this.actions.Remove(action);
                    this.actions.Insert(index, dialog.Action);
                    this.Grid.Refetch();
                }
            }
        }

        protected virtual void EnableCommands()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.EnableCommands));
            }
            else
            {
                Action action = this.SelectedAction;
                this.cmdNew.Enabled = Janus.Windows.UI.InheritableBoolean.True;
                if (action == null)
                {
                    this.cmdDelete.Enabled = this.cmdEdit.Enabled = this.cmdMoveDown.Enabled = this.cmdMoveUp.Enabled = this.cmdTest.Enabled = Janus.Windows.UI.InheritableBoolean.False;
                }
                else
                {
                    this.cmdDelete.Enabled = this.cmdEdit.Enabled = Janus.Windows.UI.InheritableBoolean.True;
                    bool notLastRow = this.Grid.Row < (this.Grid.RowCount - 1);
                    bool notFirstRow = this.Grid.Row > 0;
                    this.cmdMoveDown.Enabled = notLastRow.ToInheritableBoolean();
                    this.cmdMoveUp.Enabled = notFirstRow.ToInheritableBoolean();
                    this.cmdTest.Enabled = action.CanBeTested.ToInheritableBoolean();
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            GridEXLayout actionsGrid1_DesignTimeLayout = new GridEXLayout();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ActionsPanel));
            this.actionsGrid1 = new ActionsGrid();
            this.uiCommandManager1 = new UICommandManager(this.components);
            this.uiContextMenu1 = new UIContextMenu();
            this.cmdNew2 = new UICommand("cmdNew");
            this.cmdEdit2 = new UICommand("cmdEdit");
            this.cmdDelete2 = new UICommand("cmdDelete");
            this.Separator2 = new UICommand("Separator");
            this.cmdMoveUp2 = new UICommand("cmdMoveUp");
            this.cmdMoveDown2 = new UICommand("cmdMoveDown");
            this.Separator4 = new UICommand("Separator");
            this.cmdTest2 = new UICommand("cmdTest");
            this.BottomRebar1 = new UIRebar();
            this.uiCommandBar1 = new UICommandBar();
            this.cmdNew1 = new UICommand("cmdNew");
            this.cmdEdit1 = new UICommand("cmdEdit");
            this.cmdDelete1 = new UICommand("cmdDelete");
            this.Separator1 = new UICommand("Separator");
            this.cmdMoveUp1 = new UICommand("cmdMoveUp");
            this.cmdMoveDown1 = new UICommand("cmdMoveDown");
            this.Separator3 = new UICommand("Separator");
            this.cmdTest1 = new UICommand("cmdTest");
            this.cmdNew = new UICommand("cmdNew");
            this.cmdEdit = new UICommand("cmdEdit");
            this.cmdDelete = new UICommand("cmdDelete");
            this.cmdMoveUp = new UICommand("cmdMoveUp");
            this.cmdMoveDown = new UICommand("cmdMoveDown");
            this.cmdTest = new UICommand("cmdTest");
            this.LeftRebar1 = new UIRebar();
            this.RightRebar1 = new UIRebar();
            this.TopRebar1 = new UIRebar();
            ((ISupportInitialize) this.actionsGrid1).BeginInit();
            ((ISupportInitialize) this.uiCommandManager1).BeginInit();
            ((ISupportInitialize) this.uiContextMenu1).BeginInit();
            ((ISupportInitialize) this.BottomRebar1).BeginInit();
            ((ISupportInitialize) this.uiCommandBar1).BeginInit();
            ((ISupportInitialize) this.LeftRebar1).BeginInit();
            ((ISupportInitialize) this.RightRebar1).BeginInit();
            ((ISupportInitialize) this.TopRebar1).BeginInit();
            this.TopRebar1.SuspendLayout();
            base.SuspendLayout();
            this.actionsGrid1.Actions = null;
            this.actionsGrid1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.actionsGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.actionsGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.actionsGrid1.ColumnAutoResize = true;
            this.actionsGrid1.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
            this.uiCommandManager1.SetContextMenu(this.actionsGrid1, this.uiContextMenu1);
            actionsGrid1_DesignTimeLayout.LayoutString = resources.GetString("actionsGrid1_DesignTimeLayout.LayoutString");
            this.actionsGrid1.DesignTimeLayout = actionsGrid1_DesignTimeLayout;
            this.actionsGrid1.Dock = DockStyle.Fill;
            this.actionsGrid1.FocusCellFormatStyle.BackColor = Color.Gainsboro;
            this.actionsGrid1.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.actionsGrid1.GridLineColor = SystemColors.ControlLight;
            this.actionsGrid1.GridLines = GridLines.Horizontal;
            this.actionsGrid1.GridLineStyle = GridLineStyle.Solid;
            this.actionsGrid1.GroupByBoxVisible = false;
            this.actionsGrid1.HideSelection = HideSelection.HighlightInactive;
            this.actionsGrid1.Location = new Point(0, 0x1c);
            this.actionsGrid1.Name = "actionsGrid1";
            this.actionsGrid1.SelectedInactiveFormatStyle.BackColor = Color.WhiteSmoke;
            this.actionsGrid1.Size = new Size(0x27e, 0x7a);
            this.actionsGrid1.TabIndex = 0;
            this.actionsGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.actionsGrid1.KeyDown += new KeyEventHandler(this.actionsGrid1_KeyDown);
            this.actionsGrid1.RowDoubleClick += new RowActionEventHandler(this.actionsGrid1_RowDoubleClick);
            this.actionsGrid1.SelectionChanged += new EventHandler(this.actionsGrid1_SelectionChanged);
            this.uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.BottomRebar = this.BottomRebar1;
            this.uiCommandManager1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.uiCommandManager1.Commands.AddRange(new UICommand[] { this.cmdNew, this.cmdEdit, this.cmdDelete, this.cmdMoveUp, this.cmdMoveDown, this.cmdTest });
            this.uiCommandManager1.ContainerControl = this;
            this.uiCommandManager1.ContextMenus.AddRange(new UIContextMenu[] { this.uiContextMenu1 });
            this.uiCommandManager1.Id = new Guid("cad69911-d561-4dfc-abef-211805c32ca8");
            this.uiCommandManager1.LeftRebar = this.LeftRebar1;
            this.uiCommandManager1.LockCommandBars = true;
            this.uiCommandManager1.RightRebar = this.RightRebar1;
            this.uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandManager1.ShowQuickCustomizeMenu = false;
            this.uiCommandManager1.TopRebar = this.TopRebar1;
            this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
            this.uiContextMenu1.CommandManager = this.uiCommandManager1;
            this.uiContextMenu1.Commands.AddRange(new UICommand[] { this.cmdNew2, this.cmdEdit2, this.cmdDelete2, this.Separator2, this.cmdMoveUp2, this.cmdMoveDown2, this.Separator4, this.cmdTest2 });
            this.uiContextMenu1.Key = "ContextMenu1";
            this.cmdNew2.Key = "cmdNew";
            this.cmdNew2.Name = "cmdNew2";
            this.cmdEdit2.Key = "cmdEdit";
            this.cmdEdit2.Name = "cmdEdit2";
            this.cmdDelete2.Key = "cmdDelete";
            this.cmdDelete2.Name = "cmdDelete2";
            this.Separator2.CommandType = CommandType.Separator;
            this.Separator2.Key = "Separator";
            this.Separator2.Name = "Separator2";
            this.cmdMoveUp2.Key = "cmdMoveUp";
            this.cmdMoveUp2.Name = "cmdMoveUp2";
            this.cmdMoveDown2.Key = "cmdMoveDown";
            this.cmdMoveDown2.Name = "cmdMoveDown2";
            this.Separator4.CommandType = CommandType.Separator;
            this.Separator4.Key = "Separator";
            this.Separator4.Name = "Separator4";
            this.cmdTest2.Key = "cmdTest";
            this.cmdTest2.Name = "cmdTest2";
            this.BottomRebar1.CommandManager = this.uiCommandManager1;
            this.BottomRebar1.Dock = DockStyle.Bottom;
            this.BottomRebar1.Location = new Point(0, 0x152);
            this.BottomRebar1.Name = "BottomRebar1";
            this.BottomRebar1.Size = new Size(0x1f9, 0);
            this.uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.Animation = DropDownAnimation.System;
            this.uiCommandBar1.CommandManager = this.uiCommandManager1;
            this.uiCommandBar1.Commands.AddRange(new UICommand[] { this.cmdNew1, this.cmdEdit1, this.cmdDelete1, this.Separator1, this.cmdMoveUp1, this.cmdMoveDown1, this.Separator3, this.cmdTest1 });
            this.uiCommandBar1.FullRow = true;
            this.uiCommandBar1.Key = "CommandBar1";
            this.uiCommandBar1.Location = new Point(0, 0);
            this.uiCommandBar1.Name = "uiCommandBar1";
            this.uiCommandBar1.RowIndex = 0;
            this.uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
            this.uiCommandBar1.Size = new Size(0x27e, 0x1c);
            this.uiCommandBar1.Text = "Actions";
            this.cmdNew1.Key = "cmdNew";
            this.cmdNew1.Name = "cmdNew1";
            this.cmdEdit1.Key = "cmdEdit";
            this.cmdEdit1.Name = "cmdEdit1";
            this.cmdDelete1.Key = "cmdDelete";
            this.cmdDelete1.Name = "cmdDelete1";
            this.Separator1.CommandType = CommandType.Separator;
            this.Separator1.Key = "Separator";
            this.Separator1.Name = "Separator1";
            this.cmdMoveUp1.Key = "cmdMoveUp";
            this.cmdMoveUp1.Name = "cmdMoveUp1";
            this.cmdMoveDown1.Key = "cmdMoveDown";
            this.cmdMoveDown1.Name = "cmdMoveDown1";
            this.Separator3.CommandType = CommandType.Separator;
            this.Separator3.Key = "Separator";
            this.Separator3.Name = "Separator3";
            this.cmdTest1.Key = "cmdTest";
            this.cmdTest1.Name = "cmdTest1";
            this.cmdNew.Image = (Image) resources.GetObject("cmdNew.Image");
            this.cmdNew.Key = "cmdNew";
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Text = "&New";
            this.cmdNew.ToolTipText = "New action";
            this.cmdNew.Click += new CommandEventHandler(this.cmdNew_Click);
            this.cmdEdit.Image = (Image) resources.GetObject("cmdEdit.Image");
            this.cmdEdit.Key = "cmdEdit";
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Text = "&Properties";
            this.cmdEdit.ToolTipText = "Properties";
            this.cmdEdit.Click += new CommandEventHandler(this.cmdEdit_Click);
            this.cmdDelete.Image = (Image) resources.GetObject("cmdDelete.Image");
            this.cmdDelete.Key = "cmdDelete";
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Text = "&Delete";
            this.cmdDelete.ToolTipText = "Delete action";
            this.cmdDelete.Click += new CommandEventHandler(this.cmdDelete_Click);
            this.cmdMoveUp.Image = (Image) resources.GetObject("cmdMoveUp.Image");
            this.cmdMoveUp.Key = "cmdMoveUp";
            this.cmdMoveUp.Name = "cmdMoveUp";
            this.cmdMoveUp.Text = "Move &Up";
            this.cmdMoveUp.ToolTipText = "Move action up";
            this.cmdMoveUp.Click += new CommandEventHandler(this.cmdMoveUp_Click);
            this.cmdMoveDown.Image = (Image) resources.GetObject("cmdMoveDown.Image");
            this.cmdMoveDown.Key = "cmdMoveDown";
            this.cmdMoveDown.Name = "cmdMoveDown";
            this.cmdMoveDown.Text = "Move Do&wn";
            this.cmdMoveDown.ToolTipText = "Move action down";
            this.cmdMoveDown.Click += new CommandEventHandler(this.cmdMoveDown_Click);
            this.cmdTest.Image = (Image) resources.GetObject("cmdTest.Image");
            this.cmdTest.Key = "cmdTest";
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Text = "&Test";
            this.cmdTest.ToolTipText = "Test";
            this.cmdTest.Click += new CommandEventHandler(this.cmdTest_Click);
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
            this.TopRebar1.Size = new Size(0x27e, 0x1c);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.actionsGrid1);
            base.Controls.Add(this.TopRebar1);
            base.Name = "ActionsPanel";
            base.Size = new Size(0x27e, 150);
            ((ISupportInitialize) this.actionsGrid1).EndInit();
            ((ISupportInitialize) this.uiCommandManager1).EndInit();
            ((ISupportInitialize) this.uiContextMenu1).EndInit();
            ((ISupportInitialize) this.BottomRebar1).EndInit();
            ((ISupportInitialize) this.uiCommandBar1).EndInit();
            ((ISupportInitialize) this.LeftRebar1).EndInit();
            ((ISupportInitialize) this.RightRebar1).EndInit();
            ((ISupportInitialize) this.TopRebar1).EndInit();
            this.TopRebar1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void MoveAction(Direction direction)
        {
            Action action = this.SelectedAction;
            int newIndex = this.Actions.IndexOf(action) + direction;
            this.Actions.Remove(action);
            this.Actions.Insert(newIndex, action);
            this.Grid.Refetch();
            this.Grid.Row = newIndex;
        }

        protected virtual void MoveActionDown()
        {
            this.MoveAction(Direction.Down);
        }

        protected virtual void MoveActionUp()
        {
            this.MoveAction(Direction.Up);
        }

        protected virtual void NewAction()
        {
            ActionPropertiesDialog dialog = new ActionPropertiesDialog();
            if ((dialog.ShowDialog() == DialogResult.OK) && (dialog.Action != null))
            {
                this.Actions.Add(dialog.Action);
                this.Grid.Refetch();
            }
        }

        private void TestAction()
        {
            Action action = this.SelectedAction;
            if (action != null)
            {
                action.Test();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Action> Actions
        {
            [DebuggerNonUserCode]
            get
            {
                return this.actions;
            }
            set
            {
                this.actions = value;
                this.Grid.DataSource = this.actions;
                this.Grid.Refetch();
            }
        }

        private Janus.Windows.GridEX.GridEX Grid
        {
            [DebuggerNonUserCode]
            get
            {
                return this.actionsGrid1;
            }
        }

        [Browsable(false)]
        public Action SelectedAction
        {
            [DebuggerNonUserCode]
            get
            {
                return ((ActionsGrid) this.Grid).SelectedAction;
            }
        }

        private enum Direction
        {
            Down = 1,
            Up = -1
        }
    }
}

