using System;
using System.Windows.Forms;

using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.View;
using SVNMonitor.View.Interfaces;

namespace SVNMonitor.Presenters
{
	internal class UserEntityPresenter<T>
		where T : UserEntity
	{
		private readonly IUserEntityView<T> view;

		public UserEntityPresenter(IUserEntityView<T> view)
		{
			view = view;
			EnableCommands();
		}

		public virtual void Delete()
		{
			T entity = view.SelectedItem;
			if (entity != null)
			{
				DialogResult result = MainForm.FormInstance.ShowMessage(Strings.EntityDeleteConfirmationText_FORMAT.FormatWith(new object[]
				{
					entity.Name
				}), Strings.EntityDeleteConfirmationCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				Logger.Log.InfoFormat("Delete Confirmation: User clicked {0}", result);
				if (result == DialogResult.Yes)
				{
					entity.DeleteFile();
					view.Delete();
				}
			}
		}

		public virtual void Edit()
		{
			T entity = view.SelectedItem;
			if (entity != null)
			{
				entity.BeginEdit();
				if (view.UserEdit(entity) == DialogResult.OK)
				{
					MonitorSettings.Instance.Save();
					view.Refetch();
				}
				entity.EndEdit();
			}
		}

		public virtual void EnableCommands()
		{
			if (view.InvokeRequired)
			{
				view.Invoke(new MethodInvoker(EnableCommands));
			}
			else
			{
				T entity = view.SelectedItem;
				bool enabled = false;
				if ((entity != null) && view.ShowingAllItems)
				{
					enabled = true;
					bool lastRow = view.SelectedIndex == (view.Count - 1);
					bool firstRow = view.SelectedIndex == 0;
					view.CanMoveDown = !lastRow;
					view.CanMoveUp = !firstRow;
				}
				else
				{
					view.CanMoveUp = view.CanMoveDown = false;
				}
				view.CanDelete = enabled;
				view.CanEdit = enabled;
				view.CanNew = true;
				view.EnableCommands();
			}
		}

		public virtual void HandleKey(KeyEventArgs e)
		{
			if (view.SelectedItem != null)
			{
				e.Handled = true;
				if (e.KeyCode == Keys.Return)
				{
					Edit();
				}
				else if (e.KeyCode == Keys.Delete)
				{
					Delete();
				}
				else if (e.KeyCode == Keys.Insert)
				{
					New();
				}
				else
				{
					e.Handled = false;
				}
			}
		}

		private void Move(Direction direction)
		{
			T entity = view.SelectedItem;
			try
			{
				if (entity != null)
				{
					int index = view.Entities.IndexOf(entity);
					int newIndex = index + (int)direction;
					Logger.Log.InfoFormat("Moving {0} {1} from {2} to {3}", new object[]
					{
						entity, direction, index, newIndex
					});
					view.Entities.Remove(entity);
					view.Entities.Insert(newIndex, entity);
					view.Refetch();
					view.SelectedIndex = newIndex;
					MonitorSettings.Instance.Save();
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Error trying to move {0} '{1}'", direction, entity), ex);
			}
		}

		public virtual void MoveDown()
		{
			Move(Direction.Down);
		}

		public virtual void MoveUp()
		{
			Move(Direction.Up);
		}

		public virtual void New()
		{
			T entity = Activator.CreateInstance<T>();
			New(entity);
		}

		public virtual void New(T entity)
		{
			bool cancel;
			New(entity, out cancel);
			if (!cancel)
			{
				MonitorSettings.Instance.AddEntity(entity);
			}
		}

		public virtual void New(T entity, out bool cancel)
		{
			cancel = false;
			if (view.UserNew(entity) != DialogResult.OK)
			{
				cancel = true;
			}
		}
	}
}