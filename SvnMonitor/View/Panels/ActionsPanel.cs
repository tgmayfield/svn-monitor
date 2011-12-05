using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Janus.Windows.GridEX;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Dialogs;

namespace SVNMonitor.View.Panels
{
	internal class ActionsPanel : UserControl
	{
		private List<Actions.Action> actions;
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
			[DebuggerNonUserCode]
			add { Grid.RowCountChanged += value; }
			[DebuggerNonUserCode]
			remove { Grid.RowCountChanged -= value; }
		}

		public ActionsPanel()
		{
			InitializeComponent();
			if (!base.DesignMode)
			{
				UIHelper.ApplyResources(uiCommandManager1, true);
				EnableCommands();
			}
		}

		private void actionsGrid1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				Logger.LogUserAction("key=" + e.KeyCode);
				e.Handled = true;
				DeleteAction();
			}
		}

		private void actionsGrid1_RowDoubleClick(object sender, RowActionEventArgs e)
		{
			Logger.LogUserAction();
			EditAction();
		}

		private void actionsGrid1_SelectionChanged(object sender, EventArgs e)
		{
			EnableCommands();
		}

		private void cmdDelete_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			DeleteAction();
		}

		private void cmdEdit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			EditAction();
		}

		private void cmdMoveDown_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			MoveActionDown();
		}

		private void cmdMoveUp_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			MoveActionUp();
		}

		private void cmdNew_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			NewAction();
		}

		private void cmdTest_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			TestAction();
		}

		protected virtual void DeleteAction()
		{
			Actions.Action action = SelectedAction;
			if (action != null)
			{
				DialogResult result = MessageBox.Show(MainForm.FormInstance, string.Format("Delete action '{0}'?", action.DisplayName), "Delete Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				Logger.Log.InfoFormat("Delete action: User clicked {0}", result);
				if (result != DialogResult.No)
				{
					Grid.Delete();
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		protected virtual void EditAction()
		{
			Actions.Action action = SelectedAction;
			if (action != null)
			{
				ActionPropertiesDialog tempLocal0 = new ActionPropertiesDialog
				{
					Action = action
				};
				ActionPropertiesDialog dialog = tempLocal0;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					int index = actions.IndexOf(action);
					actions.Remove(action);
					actions.Insert(index, dialog.Action);
					Grid.Refetch();
				}
			}
		}

		protected virtual void EnableCommands()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(EnableCommands));
			}
			else
			{
				Actions.Action action = SelectedAction;
				cmdNew.Enabled = Janus.Windows.UI.InheritableBoolean.True;
				if (action == null)
				{
					cmdDelete.Enabled = cmdEdit.Enabled = cmdMoveDown.Enabled = cmdMoveUp.Enabled = cmdTest.Enabled = Janus.Windows.UI.InheritableBoolean.False;
				}
				else
				{
					cmdDelete.Enabled = cmdEdit.Enabled = Janus.Windows.UI.InheritableBoolean.True;
					bool notLastRow = Grid.Row < (Grid.RowCount - 1);
					bool notFirstRow = Grid.Row > 0;
					cmdMoveDown.Enabled = notLastRow.ToInheritableBoolean();
					cmdMoveUp.Enabled = notFirstRow.ToInheritableBoolean();
					cmdTest.Enabled = action.CanBeTested.ToInheritableBoolean();
				}
			}
		}

		private void InitializeComponent()
		{
			components = new Container();
			GridEXLayout actionsGrid1_DesignTimeLayout = new GridEXLayout();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ActionsPanel));
			actionsGrid1 = new ActionsGrid();
			uiCommandManager1 = new UICommandManager(components);
			uiContextMenu1 = new UIContextMenu();
			cmdNew2 = new UICommand("cmdNew");
			cmdEdit2 = new UICommand("cmdEdit");
			cmdDelete2 = new UICommand("cmdDelete");
			Separator2 = new UICommand("Separator");
			cmdMoveUp2 = new UICommand("cmdMoveUp");
			cmdMoveDown2 = new UICommand("cmdMoveDown");
			Separator4 = new UICommand("Separator");
			cmdTest2 = new UICommand("cmdTest");
			BottomRebar1 = new UIRebar();
			uiCommandBar1 = new UICommandBar();
			cmdNew1 = new UICommand("cmdNew");
			cmdEdit1 = new UICommand("cmdEdit");
			cmdDelete1 = new UICommand("cmdDelete");
			Separator1 = new UICommand("Separator");
			cmdMoveUp1 = new UICommand("cmdMoveUp");
			cmdMoveDown1 = new UICommand("cmdMoveDown");
			Separator3 = new UICommand("Separator");
			cmdTest1 = new UICommand("cmdTest");
			cmdNew = new UICommand("cmdNew");
			cmdEdit = new UICommand("cmdEdit");
			cmdDelete = new UICommand("cmdDelete");
			cmdMoveUp = new UICommand("cmdMoveUp");
			cmdMoveDown = new UICommand("cmdMoveDown");
			cmdTest = new UICommand("cmdTest");
			LeftRebar1 = new UIRebar();
			RightRebar1 = new UIRebar();
			TopRebar1 = new UIRebar();
			((ISupportInitialize)actionsGrid1).BeginInit();
			((ISupportInitialize)uiCommandManager1).BeginInit();
			((ISupportInitialize)uiContextMenu1).BeginInit();
			((ISupportInitialize)BottomRebar1).BeginInit();
			((ISupportInitialize)uiCommandBar1).BeginInit();
			((ISupportInitialize)LeftRebar1).BeginInit();
			((ISupportInitialize)RightRebar1).BeginInit();
			((ISupportInitialize)TopRebar1).BeginInit();
			TopRebar1.SuspendLayout();
			base.SuspendLayout();
			actionsGrid1.Actions = null;
			actionsGrid1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
			actionsGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			actionsGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			actionsGrid1.ColumnAutoResize = true;
			actionsGrid1.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
			uiCommandManager1.SetContextMenu(actionsGrid1, uiContextMenu1);
			actionsGrid1_DesignTimeLayout.LayoutString = resources.GetString("actionsGrid1_DesignTimeLayout.LayoutString");
			actionsGrid1.DesignTimeLayout = actionsGrid1_DesignTimeLayout;
			actionsGrid1.Dock = DockStyle.Fill;
			actionsGrid1.FocusCellFormatStyle.BackColor = Color.Gainsboro;
			actionsGrid1.Font = new Font("Microsoft Sans Serif", 8.25f);
			actionsGrid1.GridLineColor = SystemColors.ControlLight;
			actionsGrid1.GridLines = GridLines.Horizontal;
			actionsGrid1.GridLineStyle = GridLineStyle.Solid;
			actionsGrid1.GroupByBoxVisible = false;
			actionsGrid1.HideSelection = HideSelection.HighlightInactive;
			actionsGrid1.Location = new Point(0, 0x1c);
			actionsGrid1.Name = "actionsGrid1";
			actionsGrid1.SelectedInactiveFormatStyle.BackColor = Color.WhiteSmoke;
			actionsGrid1.Size = new Size(0x27e, 0x7a);
			actionsGrid1.TabIndex = 0;
			actionsGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			actionsGrid1.KeyDown += actionsGrid1_KeyDown;
			actionsGrid1.RowDoubleClick += actionsGrid1_RowDoubleClick;
			actionsGrid1.SelectionChanged += actionsGrid1_SelectionChanged;
			uiCommandManager1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.BottomRebar = BottomRebar1;
			uiCommandManager1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			uiCommandManager1.Commands.AddRange(new[]
			{
				cmdNew, cmdEdit, cmdDelete, cmdMoveUp, cmdMoveDown, cmdTest
			});
			uiCommandManager1.ContainerControl = this;
			uiCommandManager1.ContextMenus.AddRange(new[]
			{
				uiContextMenu1
			});
			uiCommandManager1.Id = new Guid("cad69911-d561-4dfc-abef-211805c32ca8");
			uiCommandManager1.LeftRebar = LeftRebar1;
			uiCommandManager1.LockCommandBars = true;
			uiCommandManager1.RightRebar = RightRebar1;
			uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.ShowQuickCustomizeMenu = false;
			uiCommandManager1.TopRebar = TopRebar1;
			uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			uiContextMenu1.CommandManager = uiCommandManager1;
			uiContextMenu1.Commands.AddRange(new[]
			{
				cmdNew2, cmdEdit2, cmdDelete2, Separator2, cmdMoveUp2, cmdMoveDown2, Separator4, cmdTest2
			});
			uiContextMenu1.Key = "ContextMenu1";
			cmdNew2.Key = "cmdNew";
			cmdNew2.Name = "cmdNew2";
			cmdEdit2.Key = "cmdEdit";
			cmdEdit2.Name = "cmdEdit2";
			cmdDelete2.Key = "cmdDelete";
			cmdDelete2.Name = "cmdDelete2";
			Separator2.CommandType = CommandType.Separator;
			Separator2.Key = "Separator";
			Separator2.Name = "Separator2";
			cmdMoveUp2.Key = "cmdMoveUp";
			cmdMoveUp2.Name = "cmdMoveUp2";
			cmdMoveDown2.Key = "cmdMoveDown";
			cmdMoveDown2.Name = "cmdMoveDown2";
			Separator4.CommandType = CommandType.Separator;
			Separator4.Key = "Separator";
			Separator4.Name = "Separator4";
			cmdTest2.Key = "cmdTest";
			cmdTest2.Name = "cmdTest2";
			BottomRebar1.CommandManager = uiCommandManager1;
			BottomRebar1.Dock = DockStyle.Bottom;
			BottomRebar1.Location = new Point(0, 0x152);
			BottomRebar1.Name = "BottomRebar1";
			BottomRebar1.Size = new Size(0x1f9, 0);
			uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.Animation = DropDownAnimation.System;
			uiCommandBar1.CommandManager = uiCommandManager1;
			uiCommandBar1.Commands.AddRange(new[]
			{
				cmdNew1, cmdEdit1, cmdDelete1, Separator1, cmdMoveUp1, cmdMoveDown1, Separator3, cmdTest1
			});
			uiCommandBar1.FullRow = true;
			uiCommandBar1.Key = "CommandBar1";
			uiCommandBar1.Location = new Point(0, 0);
			uiCommandBar1.Name = "uiCommandBar1";
			uiCommandBar1.RowIndex = 0;
			uiCommandBar1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.Size = new Size(0x27e, 0x1c);
			uiCommandBar1.Text = "Actions";
			cmdNew1.Key = "cmdNew";
			cmdNew1.Name = "cmdNew1";
			cmdEdit1.Key = "cmdEdit";
			cmdEdit1.Name = "cmdEdit1";
			cmdDelete1.Key = "cmdDelete";
			cmdDelete1.Name = "cmdDelete1";
			Separator1.CommandType = CommandType.Separator;
			Separator1.Key = "Separator";
			Separator1.Name = "Separator1";
			cmdMoveUp1.Key = "cmdMoveUp";
			cmdMoveUp1.Name = "cmdMoveUp1";
			cmdMoveDown1.Key = "cmdMoveDown";
			cmdMoveDown1.Name = "cmdMoveDown1";
			Separator3.CommandType = CommandType.Separator;
			Separator3.Key = "Separator";
			Separator3.Name = "Separator3";
			cmdTest1.Key = "cmdTest";
			cmdTest1.Name = "cmdTest1";
			cmdNew.Image = (Image)resources.GetObject("cmdNew.Image");
			cmdNew.Key = "cmdNew";
			cmdNew.Name = "cmdNew";
			cmdNew.Text = "&New";
			cmdNew.ToolTipText = "New action";
			cmdNew.Click += cmdNew_Click;
			cmdEdit.Image = (Image)resources.GetObject("cmdEdit.Image");
			cmdEdit.Key = "cmdEdit";
			cmdEdit.Name = "cmdEdit";
			cmdEdit.Text = "&Properties";
			cmdEdit.ToolTipText = "Properties";
			cmdEdit.Click += cmdEdit_Click;
			cmdDelete.Image = (Image)resources.GetObject("cmdDelete.Image");
			cmdDelete.Key = "cmdDelete";
			cmdDelete.Name = "cmdDelete";
			cmdDelete.Text = "&Delete";
			cmdDelete.ToolTipText = "Delete action";
			cmdDelete.Click += cmdDelete_Click;
			cmdMoveUp.Image = (Image)resources.GetObject("cmdMoveUp.Image");
			cmdMoveUp.Key = "cmdMoveUp";
			cmdMoveUp.Name = "cmdMoveUp";
			cmdMoveUp.Text = "Move &Up";
			cmdMoveUp.ToolTipText = "Move action up";
			cmdMoveUp.Click += cmdMoveUp_Click;
			cmdMoveDown.Image = (Image)resources.GetObject("cmdMoveDown.Image");
			cmdMoveDown.Key = "cmdMoveDown";
			cmdMoveDown.Name = "cmdMoveDown";
			cmdMoveDown.Text = "Move Do&wn";
			cmdMoveDown.ToolTipText = "Move action down";
			cmdMoveDown.Click += cmdMoveDown_Click;
			cmdTest.Image = (Image)resources.GetObject("cmdTest.Image");
			cmdTest.Key = "cmdTest";
			cmdTest.Name = "cmdTest";
			cmdTest.Text = "&Test";
			cmdTest.ToolTipText = "Test";
			cmdTest.Click += cmdTest_Click;
			LeftRebar1.CommandManager = uiCommandManager1;
			LeftRebar1.Dock = DockStyle.Left;
			LeftRebar1.Location = new Point(0, 0x1c);
			LeftRebar1.Name = "LeftRebar1";
			LeftRebar1.Size = new Size(0, 310);
			RightRebar1.CommandManager = uiCommandManager1;
			RightRebar1.Dock = DockStyle.Right;
			RightRebar1.Location = new Point(0x1f9, 0x1c);
			RightRebar1.Name = "RightRebar1";
			RightRebar1.Size = new Size(0, 310);
			TopRebar1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			TopRebar1.CommandManager = uiCommandManager1;
			TopRebar1.Controls.Add(uiCommandBar1);
			TopRebar1.Dock = DockStyle.Top;
			TopRebar1.Location = new Point(0, 0);
			TopRebar1.Name = "TopRebar1";
			TopRebar1.Size = new Size(0x27e, 0x1c);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(actionsGrid1);
			base.Controls.Add(TopRebar1);
			base.Name = "ActionsPanel";
			base.Size = new Size(0x27e, 150);
			((ISupportInitialize)actionsGrid1).EndInit();
			((ISupportInitialize)uiCommandManager1).EndInit();
			((ISupportInitialize)uiContextMenu1).EndInit();
			((ISupportInitialize)BottomRebar1).EndInit();
			((ISupportInitialize)uiCommandBar1).EndInit();
			((ISupportInitialize)LeftRebar1).EndInit();
			((ISupportInitialize)RightRebar1).EndInit();
			((ISupportInitialize)TopRebar1).EndInit();
			TopRebar1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void MoveAction(Direction direction)
		{
			Actions.Action action = SelectedAction;
			int newIndex = Actions.IndexOf(action) + (int)direction;
			Actions.Remove(action);
			Actions.Insert(newIndex, action);
			Grid.Refetch();
			Grid.Row = newIndex;
		}

		protected virtual void MoveActionDown()
		{
			MoveAction(Direction.Down);
		}

		protected virtual void MoveActionUp()
		{
			MoveAction(Direction.Up);
		}

		protected virtual void NewAction()
		{
			ActionPropertiesDialog dialog = new ActionPropertiesDialog();
			if ((dialog.ShowDialog() == DialogResult.OK) && (dialog.Action != null))
			{
				Actions.Add(dialog.Action);
				Grid.Refetch();
			}
		}

		private void TestAction()
		{
			Actions.Action action = SelectedAction;
			if (action != null)
			{
				action.Test();
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<Actions.Action> Actions
		{
			[DebuggerNonUserCode]
			get { return actions; }
			set
			{
				actions = value;
				Grid.DataSource = actions;
				Grid.Refetch();
			}
		}

		private Janus.Windows.GridEX.GridEX Grid
		{
			[DebuggerNonUserCode]
			get { return actionsGrid1; }
		}

		[Browsable(false)]
		public Actions.Action SelectedAction
		{
			[DebuggerNonUserCode]
			get { return ((ActionsGrid)Grid).SelectedAction; }
		}
	}
}