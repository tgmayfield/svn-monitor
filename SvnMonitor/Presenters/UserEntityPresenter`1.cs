using SVNMonitor.View.Interfaces;
using System;
using System.Windows.Forms;
using SVNMonitor.View;
using SVNMonitor.Resources.Text;
using SVNMonitor.Logging;
using SVNMonitor;

namespace SVNMonitor.Presenters
{
internal class UserEntityPresenter<T>
{
	private readonly IUserEntityView<T> view;

	public UserEntityPresenter(IUserEntityView<T> view)
	{
		this.view = view;
		this.EnableCommands();
	}

	public virtual void Delete()
	{
		object[] objArray;
		T entity = this.view.SelectedItem;
		if (entity == null)
		{
			return;
		}
		DialogResult result = MainForm.FormInstance.ShowMessage(Strings.EntityDeleteConfirmationText_FORMAT.FormatWith(new object[] { entity.Name }), Strings.EntityDeleteConfirmationCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		Logger.Log.InfoFormat("Delete Confirmation: User clicked {0}", result);
		if (result != DialogResult.Yes)
		{
			return;
		}
		&entity.DeleteFile();
		this.view.Delete();
	}

	public virtual void Edit()
	{
		T entity = this.view.SelectedItem;
		if (entity == null)
		{
			return;
		}
		&entity.BeginEdit();
		DialogResult result = this.view.UserEdit(entity);
		if (result == DialogResult.OK)
		{
			MonitorSettings.Instance.Save();
			this.view.Refetch();
		}
		&entity.EndEdit();
	}

	public virtual void EnableCommands()
	{
		if (this.view.InvokeRequired)
		{
			this.Invoke(new MethodInvoker(this.EnableCommands));
			return;
		}
		T entity = this.view.SelectedItem;
		bool enabled = false;
		if (entity != null && this.view.ShowingAllItems)
		{
			enabled = true;
			bool lastRow = this.view.SelectedIndex == this.view.Count - 1;
			bool firstRow = this.view.SelectedIndex == 0;
			this.view.CanMoveDown = lastRow == 0;
			this.view.CanMoveUp = firstRow == 0;
		}
		this.view.CanDelete = enabled;
		this.view.CanEdit = enabled;
		this.view.CanNew = true;
		this.view.EnableCommands();
	}

	public virtual void HandleKey(KeyEventArgs e)
	{
		T entity = this.view.SelectedItem;
		if (entity == null)
		{
			return;
		}
		e.Handled = true;
		if (e.KeyCode == 13)
		{
			this.Edit();
			return;
		}
		if (e.KeyCode == 46)
		{
			this.Delete();
			return;
		}
		if (e.KeyCode == 45)
		{
			this.New();
			return;
		}
		e.Handled = false;
	}

	private void Move(MoveDirecti<T> direction)
	{
		object[] objArray;
		T entity = this.view.SelectedItem;
		try
		{
			if (entity != null)
			{
				int index = this.view.Entities.IndexOf(entity);
				int newIndex = index + direction;
				Logger.Log.InfoFormat("Moving {0} {1} from {2} to {3}", new object[] { entity, direction, index, newIndex });
				this.view.Entities.Remove(entity);
				this.view.Entities.Insert(newIndex, entity);
				this.view.Refetch();
				this.view.SelectedIndex = newIndex;
				MonitorSettings.Instance.Save();
				Logger.Log.Error(string.Format("Error trying to move {0} '{1}'", direction, entity), ex);
			}
		}
		catch (Exception ex)
		{
		}
	}

	public virtual void MoveDown()
	{
		this.Move(1);
	}

	public virtual void MoveUp()
	{
		this.Move(-1);
	}

	public virtual void New()
	{
		T entity = Activator.CreateInstance<T>();
		this.New(entity);
	}

	public virtual void New(T entity)
	{
		bool cancel;
		this.New(entity, ref cancel);
		if (!cancel)
		{
			MonitorSettings.Instance.AddEntity(entity);
		}
	}

	public virtual void New(T entity, out bool cancel)
	{
		cancel = 0;
		DialogResult result = this.view.UserNew(entity);
		if (result != DialogResult.OK)
		{
			cancel = 1;
		}
	}

	private enum MoveDirection
	{
		Down = 1,
		Up = -1
	}
}
}