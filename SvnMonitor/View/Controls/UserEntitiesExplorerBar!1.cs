using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Janus.Windows.ExplorerBar;

using SVNMonitor.Entities;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Settings;

namespace SVNMonitor.View.Controls
{
	public class UserEntitiesExplorerBar<T> : Janus.Windows.ExplorerBar.ExplorerBar
		where T : UserEntity
	{
		private List<T> entities;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UserEntityExplorerBarGroup<T> selectedGroup;
		private int selectedIndex;
		private bool selectFirst;

		public event EventHandler ErrorClick;

		public event EventHandler SelectionChanged;

		private void ClearSelected()
		{
			SelectedGroup = null;
			selectedIndex = -1;
		}

		public bool Contains(UserEntity entity)
		{
			return (((base.Groups != null) && (base.Groups.Count != 0)) && (GetGroupByEntity(entity) != null));
		}

		private void CreateEntityGroup(T entity)
		{
			UserEntityExplorerBarGroup<T> group = GetEntityGroup(entity);
			group.Key = entity.Guid;
			int count = base.Groups.Count;
			base.Groups.Add(group);
			group.RefreshEntityAsync();
		}

		public void Delete()
		{
			if (SelectedGroup != null)
			{
				base.Groups.Remove(SelectedGroup);
			}
		}

		protected virtual UserEntityExplorerBarGroup<T> GetEntityGroup(T entity)
		{
			return new UserEntityExplorerBarGroup<T>(entity);
		}

		protected IEnumerable GetEnumerableGroups()
		{
			return new ArrayList(base.Groups);
		}

		private UserEntityExplorerBarGroup<T> GetGroupByEntity(UserEntity entity)
		{
			foreach (UserEntityExplorerBarGroup<T> group in GetEnumerableGroups())
			{
				if (group.Entity == entity)
				{
					return group;
				}
			}
			return null;
		}

		internal bool IsGroupAtLocation(Point point, out bool cancel)
		{
			cancel = false;
			if (base.GetGroupAt(point) == null)
			{
				ExplorerBarItem item = base.GetItemAt(point);
				if (item == null)
				{
					return false;
				}
				if (item.ItemType == ItemType.LinkButton)
				{
					cancel = true;
					return false;
				}
			}
			return true;
		}

		protected virtual void OnErrorClick()
		{
			if (ErrorClick != null)
			{
				ErrorClick(this, EventArgs.Empty);
			}
			try
			{
				if (ApplicationSettingsManager.Settings.DismissErrorsWhenClicked)
				{
					SelectedEntity.ClearError();
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error trying to clear the error of " + SelectedEntity, ex);
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
				if ((Entities == null) || (Entities.Count == 0))
				{
					e.Graphics.DrawString(NoEntitiesString, Font, Brushes.DarkGray, 10f, 10f);
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
			if (SelectionChanged != null)
			{
				SelectionChanged(this, EventArgs.Empty);
			}
		}

		public void RefreshEntities()
		{
			base.GroupClick -= UserEntitiesExplorerBar_GroupClick;
			base.ItemClick -= UserEntitiesExplorerBar_ItemClick;
			T previouslySelectedEntity = SelectedEntity;
			ClearSelected();
			base.Groups.Clear();
			if ((Entities != null) && (Entities.Count != 0))
			{
				foreach (T entity in Entities)
				{
					CreateEntityGroup(entity);
				}
				base.GroupClick += UserEntitiesExplorerBar_GroupClick;
				base.ItemClick += UserEntitiesExplorerBar_ItemClick;
				if (selectFirst)
				{
					SelectEntity(0);
					selectFirst = false;
				}
				else
				{
					SelectEntity(previouslySelectedEntity);
				}
			}
		}

		public void RefreshEntity(T entity)
		{
			try
			{
				if (base.InvokeRequired)
				{
					base.BeginInvoke(new Action<T>(RefreshEntity), new object[]
					{
						entity
					});
				}
				else
				{
					try
					{
						if (Contains(entity))
						{
							((UserEntityExplorerBarGroup<T>)base.Groups[entity.Guid]).RefreshEntityAsync();
						}
					}
					catch (IndexOutOfRangeException)
					{
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Debug(string.Format("Error(Entity={0})", entity.Name), ex);
			}
		}

		public void SelectEntity(Point location)
		{
			if ((base.Groups != null) && (base.Groups.Count != 0))
			{
				UserEntityExplorerBarGroup<T> group = (UserEntityExplorerBarGroup<T>)base.GetGroupAt(location);
				if (group == null)
				{
					ExplorerBarItem item = base.GetItemAt(location);
					if (item != null)
					{
						group = (UserEntityExplorerBarGroup<T>)item.Group;
					}
				}
				if (group == null)
				{
					Logger.Log.InfoFormat("There is no source group at location {0}", location);
				}
				else
				{
					SelectEntity(group.Entity);
				}
			}
		}

		public void SelectEntity(T entity)
		{
			if (entity == null)
			{
				ClearSelected();
			}
			else
			{
				for (int i = 0; i < base.Groups.Count; i++)
				{
					UserEntityExplorerBarGroup<T> group = (UserEntityExplorerBarGroup<T>)base.Groups[i];
					if (group.Entity == entity)
					{
						SelectEntity(i);
						return;
					}
				}
			}
		}

		public void SelectEntity(int index)
		{
			if ((base.Groups != null) && (base.Groups.Count != 0))
			{
				UserEntityExplorerBarGroup<T> group = (UserEntityExplorerBarGroup<T>)base.Groups[index];
				if (group != null)
				{
					selectedIndex = index;
					SelectGroup(group);
				}
			}
		}

		private void SelectGroup(UserEntityExplorerBarGroup<T> group)
		{
			if (group != null)
			{
				SetSelectGroupStyle(group);
				OnSelectionChanged();
			}
		}

		private void SetSelectGroupStyle(UserEntityExplorerBarGroup<T> group)
		{
			if ((SelectedGroup != null) && (SelectedGroup != group))
			{
				SelectedGroup.SpecialGroup = false;
				SelectedGroup.BackgroundImage = null;
			}
			SelectedGroup = group;
			if ((SelectedGroup != null) && !SelectedGroup.SpecialGroup)
			{
				SelectedGroup.SpecialGroup = true;
				SelectedGroup.BackgroundImage = Images.background_01;
				base.Focus();
			}
		}

		public virtual void SetVisibleEntities()
		{
			foreach (UserEntityExplorerBarGroup<T> group in GetEnumerableGroups())
			{
				group.Visible = true;
			}
		}

		public virtual void SetVisibleEntities(IEnumerable<T> entities)
		{
			SetVisibleEntities();
			List<UserEntityExplorerBarGroup<T>> groups = new List<UserEntityExplorerBarGroup<T>>();
			foreach (UserEntityExplorerBarGroup<T> group in GetEnumerableGroups())
			{
				groups.Add(group);
			}
			foreach (UserEntity entity in entities)
			{
				UserEntityExplorerBarGroup<T> group = GetGroupByEntity(entity);
				if (group != null)
				{
					groups.Remove(group);
				}
			}
			foreach (UserEntityExplorerBarGroup<T> group in groups)
			{
				group.Visible = false;
			}
		}

		private void UserEntitiesExplorerBar_GroupClick(object sender, GroupEventArgs e)
		{
			SelectEntity(((UserEntityExplorerBarGroup<T>)e.Group).Entity);
		}

		private void UserEntitiesExplorerBar_ItemClick(object sender, ItemEventArgs e)
		{
			SelectEntity(((UserEntityExplorerBarGroup<T>)e.Item.Group).Entity);
			if (!string.IsNullOrEmpty(e.Item.Key))
			{
				base.GetType().GetMethod(string.Format("On{0}Click", e.Item.Key), BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
			}
		}

		[Browsable(false)]
		public int Count
		{
			get { return base.Groups.Count; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<T> Entities
		{
			[DebuggerNonUserCode]
			get { return entities; }
			set
			{
				if (entities != value)
				{
					if (entities == null)
					{
						selectFirst = true;
					}
					entities = value;
				}
				RefreshEntities();
			}
		}

		protected virtual string NoEntitiesString
		{
			get { return "[EMPTY]"; }
		}

		public T SelectedEntity
		{
			get
			{
				if ((SelectedGroup != null) && SelectedGroup.Visible)
				{
					return SelectedGroup.Entity;
				}
				return default(T);
			}
		}

		private UserEntityExplorerBarGroup<T> SelectedGroup
		{
			get { return selectedGroup; }
			set { selectedGroup = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get { return selectedIndex; }
			set { SelectEntity(value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<T> VisibleEntities
		{
			get { return base.Groups.Cast<UserEntityExplorerBarGroup<T>>().Where<UserEntityExplorerBarGroup<T>>(g => g.Visible).Select<UserEntityExplorerBarGroup<T>, T>(g => g.Entity); }
		}
	}
}