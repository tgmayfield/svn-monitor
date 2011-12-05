namespace SVNMonitor.Presenters
{
    using SVNMonitor;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View;
    using SVNMonitor.View.Interfaces;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class UserEntityPresenter<T> where T: UserEntity
    {
        private readonly IUserEntityView<T> view;

        public UserEntityPresenter(IUserEntityView<T> view)
        {
            this.view = view;
            this.EnableCommands();
        }

        public virtual void Delete()
        {
            T entity = this.view.SelectedItem;
            if (entity != null)
            {
                DialogResult result = MainForm.FormInstance.ShowMessage(Strings.EntityDeleteConfirmationText_FORMAT.FormatWith(new object[] { entity.Name }), Strings.EntityDeleteConfirmationCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                Logger.Log.InfoFormat("Delete Confirmation: User clicked {0}", result);
                if (result == DialogResult.Yes)
                {
                    entity.DeleteFile();
                    this.view.Delete();
                }
            }
        }

        public virtual void Edit()
        {
            T entity = this.view.SelectedItem;
            if (entity != null)
            {
                entity.BeginEdit();
                if (this.view.UserEdit(entity) == DialogResult.OK)
                {
                    MonitorSettings.Instance.Save();
                    this.view.Refetch();
                }
                entity.EndEdit();
            }
        }

        public virtual void EnableCommands()
        {
            if (this.view.InvokeRequired)
            {
                this.view.Invoke(new MethodInvoker(this.EnableCommands));
            }
            else
            {
                T entity = this.view.SelectedItem;
                bool enabled = false;
                if ((entity != null) && this.view.ShowingAllItems)
                {
                    enabled = true;
                    bool lastRow = this.view.SelectedIndex == (this.view.Count - 1);
                    bool firstRow = this.view.SelectedIndex == 0;
                    this.view.CanMoveDown = !lastRow;
                    this.view.CanMoveUp = !firstRow;
                }
                else
                {
                    this.view.CanMoveUp = this.view.CanMoveDown = false;
                }
                this.view.CanDelete = enabled;
                this.view.CanEdit = enabled;
                this.view.CanNew = true;
                this.view.EnableCommands();
            }
        }

        public virtual void HandleKey(KeyEventArgs e)
        {
            if (this.view.SelectedItem != null)
            {
                e.Handled = true;
                if (e.KeyCode == Keys.Return)
                {
                    this.Edit();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    this.Delete();
                }
                else if (e.KeyCode == Keys.Insert)
                {
                    this.New();
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void Move(MoveDirection<T> direction)
        {
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
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error trying to move {0} '{1}'", direction, entity), ex);
            }
        }

        public virtual void MoveDown()
        {
            this.Move(MoveDirection<T>.Down);
        }

        public virtual void MoveUp()
        {
            this.Move(MoveDirection<T>.Up);
        }

        public virtual void New()
        {
            T entity = Activator.CreateInstance<T>();
            this.New(entity);
        }

        public virtual void New(T entity)
        {
            bool cancel;
            this.New(entity, out cancel);
            if (!cancel)
            {
                MonitorSettings.Instance.AddEntity(entity);
            }
        }

        public virtual void New(T entity, out bool cancel)
        {
            cancel = false;
            if (this.view.UserNew(entity) != DialogResult.OK)
            {
                cancel = true;
            }
        }

        private enum MoveDirection
        {
            public const UserEntityPresenter<T>.MoveDirection Down = UserEntityPresenter<T>.MoveDirection.Down;,
            public const UserEntityPresenter<T>.MoveDirection Up = UserEntityPresenter<T>.MoveDirection.Up;
        }
    }
}

