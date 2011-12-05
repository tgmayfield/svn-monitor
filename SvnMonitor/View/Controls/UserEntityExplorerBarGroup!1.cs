using System;
using System.Drawing;
using System.Windows.Forms;

using Janus.Windows.ExplorerBar;

using SVNMonitor.Entities;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Controls
{
	public class UserEntityExplorerBarGroup<T> : ExplorerBarGroup
		where T : UserEntity
	{
		private ExplorerBarItem errorItem;

		public UserEntityExplorerBarGroup(T entity)
		{
			Entity = entity;
			SetProperties();
			CreateSubItems();
		}

		protected void ClearError()
		{
			ErrorItem.Text = string.Empty;
			ErrorItem.ToolTipText = string.Empty;
			ErrorItem.Visible = false;
		}

		protected virtual void CreateSubItems()
		{
			ErrorItem = new ExplorerBarItem();
			ErrorItem.Key = "Error";
			ErrorItem.Visible = false;
			ErrorItem.Image = Images.error;
			ErrorItem.ItemType = ItemType.LinkButton;
			ErrorItem.Cursor = Cursors.Hand;
			ErrorItem.StateStyles.FormatStyle.ForeColor = Color.Red;
			ErrorItem.StateStyles.FormatStyle.FontBold = TriState.True;
		}

		public virtual void RefreshEntity()
		{
			if (MainForm.FormInstance.InvokeRequired)
			{
				MainForm.FormInstance.BeginInvoke(new MethodInvoker(RefreshEntity));
			}
			else
			{
				SetNameAndImage();
				SetError();
			}
		}

		public virtual void RefreshEntity(object state)
		{
			RefreshEntity();
		}

		public virtual void RefreshEntityAsync()
		{
			SVNMonitor.Helpers.ThreadHelper.Queue(RefreshEntity, "EXPBAR");
		}

		protected bool SetError()
		{
			if (Entity.Enabled && Entity.HasError)
			{
				ErrorItem.Text = Strings.UserEntityError;
				ErrorItem.Visible = true;
				ErrorItem.ToolTipText = Entity.ErrorText;
			}
			else
			{
				ClearError();
			}
			return errorItem.Visible;
		}

		protected virtual void SetNameAndImage()
		{
		}

		protected virtual void SetProperties()
		{
		}

		public T Entity { get; private set; }

		protected ExplorerBarItem ErrorItem
		{
			get { return errorItem; }
			private set { errorItem = value; }
		}
	}
}