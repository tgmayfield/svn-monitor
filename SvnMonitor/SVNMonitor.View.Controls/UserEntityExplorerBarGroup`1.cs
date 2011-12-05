using Janus.Windows.ExplorerBar;
using System;
using SVNMonitor.Resources;
using System.Windows.Forms;
using System.Drawing;
using SVNMonitor.View;
using SVNMonitor.Helpers;
using System.Threading;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Controls
{
public class UserEntityExplorerBarGroup<T> : ExplorerBarGroup
{
	private ExplorerBarItem errorItem;

	public T Entity
	{
		get
		{
			return this.<Entity>k__BackingField;
		}
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

	public UserEntityExplorerBarGroup(T entity)
	{
		base.Entity = entity;
		base.SetProperties();
		base.CreateSubItems();
	}

	protected void ClearError()
	{
		this.ErrorItem.Text = string.Empty;
		this.ErrorItem.ToolTipText = string.Empty;
		this.ErrorItem.Visible = false;
	}

	protected virtual void CreateSubItems()
	{
		base.ErrorItem = new ExplorerBarItem();
		this.ErrorItem.Key = "Error";
		this.ErrorItem.Visible = false;
		this.ErrorItem.Image = Images.error;
		this.ErrorItem.ItemType = ItemType.LinkButton;
		this.ErrorItem.Cursor = Cursors.Hand;
		this.ErrorItem.StateStyles.FormatStyle.ForeColor = Color.Red;
		this.ErrorItem.StateStyles.FormatStyle.FontBold = TriState.True;
	}

	public virtual void RefreshEntity(object state)
	{
		base.RefreshEntity();
	}

	public virtual void RefreshEntity()
	{
		if (MainForm.FormInstance.InvokeRequired)
		{
			base.BeginInvoke(new MethodInvoker(this.RefreshEntity));
			return;
		}
		base.SetNameAndImage();
		base.SetError();
	}

	public virtual void RefreshEntityAsync()
	{
		ThreadHelper.Queue(new WaitCallback(this.RefreshEntity), "EXPBAR");
	}

	protected bool SetError()
	{
		T entity1 = base.Entity;
		if (&entity1.Enabled)
		{
			T t = base.Entity;
			if (&t.HasError)
			{
				this.ErrorItem.Text = Strings.UserEntityError;
				this.ErrorItem.Visible = true;
				T entity2 = this.Entity.ToolTipText = &entity2.ErrorText;
			}
			else
			{
				base.ClearError();
			}
		}
		return this.errorItem.Visible;
	}

	protected virtual void SetNameAndImage()
	{
	}

	protected virtual void SetProperties()
	{
	}
}
}