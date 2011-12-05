using Janus.Windows.ExplorerBar;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.ComponentModel;
using SVNMonitor.Entities;
using System.Collections;
using System.Drawing;
using SVNMonitor.Settings;
using SVNMonitor.Logging;
using System.Windows.Forms;
using SVNMonitor.Resources;
using System.Reflection;

namespace SVNMonitor.View.Controls
{
public class UserEntitiesExplorerBar<T> : ExplorerBar
{
	private List<T> entities;

	private EventHandler ErrorClick;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private UserEntityExplorerBarGroup<T> selectedGroup;

	private int selectedIndex;

	private bool selectFirst;

	private EventHandler SelectionChanged;

	[Browsable(false)]
	public int Count
	{
		get
		{
			return base.Groups.Count;
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public List<T> Entities
	{
		get
		{
			return this.entities;
		}
		set
		{
			if (this.entities != value)
			{
				if (this.entities == null)
				{
					this.selectFirst = true;
				}
				this.entities = value;
			}
			base.RefreshEntities();
		}
	}

	protected string NoEntitiesString
	{
		get
		{
			return "[EMPTY]";
		}
	}

	public T SelectedEntity
	{
		get
		{
			if (base.SelectedGroup != null && base.SelectedGroup.Visible)
			{
				return base.SelectedGroup.Entity;
			}
			T t = default(T);
			return t;
		}
	}

	private UserEntityExplorerBarGroup<T> SelectedGroup
	{
		get
		{
			return this.selectedGroup;
		}
		set
		{
			this.selectedGroup = value;
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int SelectedIndex
	{
		get
		{
			return this.selectedIndex;
		}
		set
		{
			base.SelectEntity(value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IEnumerable<T> VisibleEntities
	{
		get
		{
			return base.Groups.Cast<UserEntityExplorerBarGroup<T>>().Where<UserEntityExplorerBarGroup<T>>(UserEntitiesExplorerBar<T>.CS$<>9__CachedAnonymousMethodDelegate2).Select<UserEntityExplorerBarGroup<T>,T>(UserEntitiesExplorerBar<T>.CS$<>9__CachedAnonymousMethodDelegate3);
		}
	}

	public UserEntitiesExplorerBar()
	{
	}

	private void ClearSelected()
	{
		base.SelectedGroup = null;
		this.selectedIndex = -1;
	}

	public bool Contains(UserEntity entity)
	{
		if (base.Groups == null || base.Groups.Count == 0)
		{
			return false;
		}
		UserEntityExplorerBarGroup<T> @group = base.GetGroupByEntity(entity);
		return @group != null;
	}

	private void CreateEntityGroup(T entity)
	{
		UserEntityExplorerBarGroup<T> @group = base.GetEntityGroup(entity);
		@group.Key = &entity.Guid;
		base.Groups.Count;
		base.Groups.Add(@group);
		@group.RefreshEntityAsync();
	}

	public void Delete()
	{
		if (base.SelectedGroup == null)
		{
			return;
		}
		base.Groups.Remove(base.SelectedGroup);
	}

	protected virtual UserEntityExplorerBarGroup<T> GetEntityGroup(T entity)
	{
		return new UserEntityExplorerBarGroup<T>(entity);
	}

	protected IEnumerable GetEnumerableGroups()
	{
		ArrayList list = new ArrayList(base.Groups);
		return list;
	}

	private UserEntityExplorerBarGroup<T> GetGroupByEntity(UserEntity entity)
	{
		UserEntityExplorerBarGroup<T> foundGroup = null;
		foreach (UserEntityExplorerBarGroup<T> @group in base.GetEnumerableGroups())
		{
			if (@group.Entity == entity)
			{
				foundGroup = @group;
				break;
			}
		}
		return foundGroup;
	}

	internal bool IsGroupAtLocation(Point point, out bool cancel)
	{
		cancel = 0;
		ExplorerBarGroup @group = base.GetGroupAt(point);
		if (@group != null)
		{
			return true;
		}
		ExplorerBarItem item = base.GetItemAt(point);
		if (item == null)
		{
			return false;
		}
		if (item.ItemType == 0)
		{
			cancel = 1;
			return false;
		}
		return true;
	}

	protected virtual void OnErrorClick()
	{
		if (this.ErrorClick != null)
		{
			this.ErrorClick(this, EventArgs.Empty);
		}
		try
		{
			if (ApplicationSettingsManager.Settings.DismissErrorsWhenClicked)
			{
				T selectedEntity = base.SelectedEntity;
				&selectedEntity.ClearError();
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Concat("Error trying to clear the error of ", base.SelectedEntity), ex);
		}
	}

	protected override void OnGroupCollapsing(GroupCancelEventArgs e)
	{
		e.Cancel = true;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		try
		{
			base.OnPaint(e);
			if (base.Entities != null && base.Entities.Count == 0)
			{
				e.Graphics.DrawString(base.NoEntitiesString, base.Font, Brushes.DarkGray, 10, 10);
			}
		}
		catch (Exception ex1)
		{
			Logger.Log.Error("Error painting the explorer-bar.", ex1);
			try
			{
				e.Graphics.ReleaseHdc();
				base.OnPaint(e);
			}
			catch (Exception ex2)
			{
				Logger.Log.Error("Error painting the explorer-bar, again.", ex2);
			}
		}
	}

	protected virtual void OnSelectionChanged()
	{
		if (this.SelectionChanged != null)
		{
			this.SelectionChanged(this, EventArgs.Empty);
		}
	}

	public void RefreshEntities()
	{
		base.GroupClick -= new GroupEventHandler(this.UserEntitiesExplorerBar_GroupClick);
		base.ItemClick -= new ItemEventHandler(this.UserEntitiesExplorerBar_ItemClick);
		T previouslySelectedEntity = base.SelectedEntity;
		base.ClearSelected();
		base.Groups.Clear();
		if (base.Entities == null || base.Entities.Count == 0)
		{
			return;
		}
		foreach (T entity in base.Entities)
		{
			base.CreateEntityGroup(entity);
		}
		base.GroupClick += new GroupEventHandler(this.UserEntitiesExplorerBar_GroupClick);
		base.ItemClick += new ItemEventHandler(this.UserEntitiesExplorerBar_ItemClick);
		if (this.selectFirst)
		{
			base.SelectEntity(0);
			this.selectFirst = false;
			return;
		}
		base.SelectEntity(previouslySelectedEntity);
	}

	public void RefreshEntity(T entity)
	{
		object[] objArray;
		try
		{
			base.BeginInvoke(new Action<T>(this.RefreshEntity), new object[] { entity });
		}
		catch (Exception ex)
		{
			Logger.Log.Debug(string.Format("Error(Entity={0})", entity.Name), ex);
		}
		if (base.InvokeRequired)
		{
		}
		try
		{
			if (base.Contains(entity))
			{
				UserEntityExplorerBarGroup<T> @group = (UserEntityExplorerBarGroup<T>)base.Groups[entity.Guid];
				@group.RefreshEntityAsync();
			}
		}
		catch (IndexOutOfRangeException)
		{
		}
		catch (ArgumentOutOfRangeException)
		{
		}
	}

	public void SelectEntity(Point location)
	{
		if (base.Groups == null || base.Groups.Count == 0)
		{
			return;
		}
		UserEntityExplorerBarGroup<T> @group = (UserEntityExplorerBarGroup<T>)base.GetGroupAt(location);
		if (@group == null)
		{
			ExplorerBarItem item = base.GetItemAt(location);
			if (item != null)
			{
				@group = (UserEntityExplorerBarGroup<T>)item.Group;
			}
		}
		if (@group == null)
		{
			Logger.Log.InfoFormat("There is no source group at location {0}", location);
			return;
		}
		base.SelectEntity(@group.Entity);
	}

	public void SelectEntity(int index)
	{
		if (base.Groups == null || base.Groups.Count == 0)
		{
			return;
		}
		UserEntityExplorerBarGroup<T> @group = (UserEntityExplorerBarGroup<T>)base.Groups[index];
		if (@group != null)
		{
			this.selectedIndex = index;
			base.SelectGroup(@group);
		}
	}

	public void SelectEntity(T entity)
	{
		if (entity == null)
		{
			base.ClearSelected();
			return;
		}
		int i = 0;
		i++;
		while (i < base.Groups.Count)
		{
			UserEntityExplorerBarGroup<T> @group = (UserEntityExplorerBarGroup<T>)base.Groups[i];
			if (@group.Entity == entity)
			{
				base.SelectEntity(i);
				return;
			}
		}
	}

	private void SelectGroup(UserEntityExplorerBarGroup<T> group)
	{
		if (group == null)
		{
			return;
		}
		base.SetSelectGroupStyle(group);
		base.OnSelectionChanged();
	}

	private void SetSelectGroupStyle(UserEntityExplorerBarGroup<T> group)
	{
		if (base.SelectedGroup != null && base.SelectedGroup != group)
		{
			this.SelectedGroup.SpecialGroup = false;
			this.SelectedGroup.BackgroundImage = null;
		}
		base.SelectedGroup = group;
		if (base.SelectedGroup != null && !base.SelectedGroup.SpecialGroup)
		{
			this.SelectedGroup.SpecialGroup = true;
			this.SelectedGroup.BackgroundImage = Images.background_01;
			base.Focus();
		}
	}

	public virtual void SetVisibleEntities()
	{
		foreach (UserEntityExplorerBarGroup<T> @group in base.GetEnumerableGroups())
		{
			@group.Visible = true;
		}
	}

	public virtual void SetVisibleEntities(IEnumerable<T> entities)
	{
		base.SetVisibleEntities();
		List<UserEntityExplorerBarGroup<T>> groups = new List<UserEntityExplorerBarGroup<T>>();
		foreach (UserEntityExplorerBarGroup<T> @group in base.GetEnumerableGroups())
		{
			groups.Add(@group);
		}
		foreach (UserEntity entity in entities)
		{
			UserEntityExplorerBarGroup<T> @group = base.GetGroupByEntity(entity);
			if (@group != null)
			{
				groups.Remove(@group);
			}
		}
		foreach (UserEntityExplorerBarGroup<T> @group in groups)
		{
			@group.Visible = false;
		}
	}

	private void UserEntitiesExplorerBar_GroupClick(object sender, GroupEventArgs e)
	{
		base.SelectEntity((UserEntityExplorerBarGroup<T>)e.Group.Entity);
	}

	private void UserEntitiesExplorerBar_ItemClick(object sender, ItemEventArgs e)
	{
		base.SelectEntity((UserEntityExplorerBarGroup<T>)e.Item.Group.Entity);
		if (string.IsNullOrEmpty(e.Item.Key))
		{
			return;
		}
		MethodInfo method = base.GetType().GetMethod(string.Format("On{0}Click", e.Item.Key), BindingFlags.Instance | BindingFlags.NonPublic);
		method.Invoke(this, null);
	}

	public event EventHandler ErrorClick;
	public event EventHandler SelectionChanged;
}
}