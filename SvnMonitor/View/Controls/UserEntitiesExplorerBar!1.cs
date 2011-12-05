namespace SVNMonitor.View.Controls
{
    using Janus.Windows.ExplorerBar;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using SVNMonitor.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class UserEntitiesExplorerBar<T> : Janus.Windows.ExplorerBar.ExplorerBar where T: UserEntity
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
            this.SelectedGroup = null;
            this.selectedIndex = -1;
        }

        public bool Contains(UserEntity entity)
        {
            return (((base.Groups != null) && (base.Groups.Count != 0)) && (this.GetGroupByEntity(entity) != null));
        }

        private void CreateEntityGroup(T entity)
        {
            UserEntityExplorerBarGroup<T> group = this.GetEntityGroup(entity);
            group.Key = entity.Guid;
            int count = base.Groups.Count;
            base.Groups.Add(group);
            group.RefreshEntityAsync();
        }

        public void Delete()
        {
            if (this.SelectedGroup != null)
            {
                base.Groups.Remove(this.SelectedGroup);
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
            foreach (UserEntityExplorerBarGroup<T> group in this.GetEnumerableGroups())
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
            if (this.ErrorClick != null)
            {
                this.ErrorClick(this, EventArgs.Empty);
            }
            try
            {
                if (ApplicationSettingsManager.Settings.DismissErrorsWhenClicked)
                {
                    this.SelectedEntity.ClearError();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error trying to clear the error of " + this.SelectedEntity, ex);
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
                if ((this.Entities == null) || (this.Entities.Count == 0))
                {
                    e.Graphics.DrawString(this.NoEntitiesString, this.Font, Brushes.DarkGray, (float) 10f, (float) 10f);
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
            T previouslySelectedEntity = this.SelectedEntity;
            this.ClearSelected();
            base.Groups.Clear();
            if ((this.Entities != null) && (this.Entities.Count != 0))
            {
                foreach (T entity in this.Entities)
                {
                    this.CreateEntityGroup(entity);
                }
                base.GroupClick += new GroupEventHandler(this.UserEntitiesExplorerBar_GroupClick);
                base.ItemClick += new ItemEventHandler(this.UserEntitiesExplorerBar_ItemClick);
                if (this.selectFirst)
                {
                    this.SelectEntity(0);
                    this.selectFirst = false;
                }
                else
                {
                    this.SelectEntity(previouslySelectedEntity);
                }
            }
        }

        public void RefreshEntity(T entity)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new Action<T>(this.RefreshEntity), new object[] { entity });
                }
                else
                {
                    try
                    {
                        if (this.Contains(entity))
                        {
                            ((UserEntityExplorerBarGroup<T>) base.Groups[entity.Guid]).RefreshEntityAsync();
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
                UserEntityExplorerBarGroup<T> group = (UserEntityExplorerBarGroup<T>) base.GetGroupAt(location);
                if (group == null)
                {
                    ExplorerBarItem item = base.GetItemAt(location);
                    if (item != null)
                    {
                        group = (UserEntityExplorerBarGroup<T>) item.Group;
                    }
                }
                if (group == null)
                {
                    Logger.Log.InfoFormat("There is no source group at location {0}", location);
                }
                else
                {
                    this.SelectEntity(group.Entity);
                }
            }
        }

        public void SelectEntity(T entity)
        {
            if (entity == null)
            {
                this.ClearSelected();
            }
            else
            {
                for (int i = 0; i < base.Groups.Count; i++)
                {
                    UserEntityExplorerBarGroup<T> group = (UserEntityExplorerBarGroup<T>) base.Groups[i];
                    if (group.Entity == entity)
                    {
                        this.SelectEntity(i);
                        return;
                    }
                }
            }
        }

        public void SelectEntity(int index)
        {
            if ((base.Groups != null) && (base.Groups.Count != 0))
            {
                UserEntityExplorerBarGroup<T> group = (UserEntityExplorerBarGroup<T>) base.Groups[index];
                if (group != null)
                {
                    this.selectedIndex = index;
                    this.SelectGroup(group);
                }
            }
        }

        private void SelectGroup(UserEntityExplorerBarGroup<T> group)
        {
            if (group != null)
            {
                this.SetSelectGroupStyle(group);
                this.OnSelectionChanged();
            }
        }

        private void SetSelectGroupStyle(UserEntityExplorerBarGroup<T> group)
        {
            if ((this.SelectedGroup != null) && (this.SelectedGroup != group))
            {
                this.SelectedGroup.SpecialGroup = false;
                this.SelectedGroup.BackgroundImage = null;
            }
            this.SelectedGroup = group;
            if ((this.SelectedGroup != null) && !this.SelectedGroup.SpecialGroup)
            {
                this.SelectedGroup.SpecialGroup = true;
                this.SelectedGroup.BackgroundImage = Images.background_01;
                base.Focus();
            }
        }

        public virtual void SetVisibleEntities()
        {
            foreach (UserEntityExplorerBarGroup<T> group in this.GetEnumerableGroups())
            {
                group.Visible = true;
            }
        }

        public virtual void SetVisibleEntities(IEnumerable<T> entities)
        {
            this.SetVisibleEntities();
            List<UserEntityExplorerBarGroup<T>> groups = new List<UserEntityExplorerBarGroup<T>>();
            foreach (UserEntityExplorerBarGroup<T> group in this.GetEnumerableGroups())
            {
                groups.Add(group);
            }
            foreach (UserEntity entity in entities)
            {
                UserEntityExplorerBarGroup<T> group = this.GetGroupByEntity(entity);
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
            this.SelectEntity(((UserEntityExplorerBarGroup<T>) e.Group).Entity);
        }

        private void UserEntitiesExplorerBar_ItemClick(object sender, ItemEventArgs e)
        {
            this.SelectEntity(((UserEntityExplorerBarGroup<T>) e.Item.Group).Entity);
            if (!string.IsNullOrEmpty(e.Item.Key))
            {
                base.GetType().GetMethod(string.Format("On{0}Click", e.Item.Key), BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
            }
        }

        [Browsable(false)]
        public int Count
        {
            get
            {
                return base.Groups.Count;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<T> Entities
        {
            [DebuggerNonUserCode]
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
                this.RefreshEntities();
            }
        }

        protected virtual string NoEntitiesString
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
                if ((this.SelectedGroup != null) && this.SelectedGroup.Visible)
                {
                    return this.SelectedGroup.Entity;
                }
                return default(T);
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                this.SelectEntity(value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<T> VisibleEntities
        {
            get
            {
                return base.Groups.Cast<UserEntityExplorerBarGroup<T>>().Where<UserEntityExplorerBarGroup<T>>(g => g.Visible).Select<UserEntityExplorerBarGroup<T>, T>(g => g.Entity);
            }
        }
    }
}

