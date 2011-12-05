namespace SVNMonitor.View.Controls
{
    using Janus.Windows.ExplorerBar;
    using SVNMonitor.Helpers;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class UserEntityExplorerBarGroup<T> : ExplorerBarGroup where T: UserEntity
    {
        private ExplorerBarItem errorItem;

        public UserEntityExplorerBarGroup(T entity)
        {
            this.Entity = entity;
            this.SetProperties();
            this.CreateSubItems();
        }

        protected void ClearError()
        {
            this.ErrorItem.Text = string.Empty;
            this.ErrorItem.ToolTipText = string.Empty;
            this.ErrorItem.Visible = false;
        }

        protected virtual void CreateSubItems()
        {
            this.ErrorItem = new ExplorerBarItem();
            this.ErrorItem.Key = "Error";
            this.ErrorItem.Visible = false;
            this.ErrorItem.Image = Images.error;
            this.ErrorItem.ItemType = ItemType.LinkButton;
            this.ErrorItem.Cursor = Cursors.Hand;
            this.ErrorItem.StateStyles.FormatStyle.ForeColor = Color.Red;
            this.ErrorItem.StateStyles.FormatStyle.FontBold = TriState.True;
        }

        public virtual void RefreshEntity()
        {
            if (MainForm.FormInstance.InvokeRequired)
            {
                MainForm.FormInstance.BeginInvoke(new MethodInvoker(this.RefreshEntity));
            }
            else
            {
                this.SetNameAndImage();
                this.SetError();
            }
        }

        public virtual void RefreshEntity(object state)
        {
            this.RefreshEntity();
        }

        public virtual void RefreshEntityAsync()
        {
            SVNMonitor.Helpers.ThreadHelper.Queue(new WaitCallback(this.RefreshEntity), "EXPBAR");
        }

        protected bool SetError()
        {
            if (this.Entity.Enabled && this.Entity.HasError)
            {
                this.ErrorItem.Text = Strings.UserEntityError;
                this.ErrorItem.Visible = true;
                this.ErrorItem.ToolTipText = this.Entity.ErrorText;
            }
            else
            {
                this.ClearError();
            }
            return this.errorItem.Visible;
        }

        protected virtual void SetNameAndImage()
        {
        }

        protected virtual void SetProperties()
        {
        }

        public T Entity
        {
            [CompilerGenerated]
            get
            {
                return this.<Entity>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<Entity>k__BackingField = value;
            }
        }

        protected ExplorerBarItem ErrorItem
        {
            get
            {
                return this.errorItem;
            }
            private set
            {
                this.errorItem = value;
            }
        }
    }
}

