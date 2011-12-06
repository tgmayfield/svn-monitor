using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using Janus.Windows.GridEX;
using Janus.Windows.UI.CommandBars;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Dialogs;

namespace SVNMonitor.View.Panels
{
	internal partial class ActionsPanel : UserControl
	{
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