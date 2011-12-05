using System.Linq;

namespace SVNMonitor.View.Panels
{
    using Janus.Windows.ExplorerBar;
    using Janus.Windows.UI;
    using Janus.Windows.UI.CommandBars;
    using SVNMonitor;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Presenters;
    using SVNMonitor.View;
    using SVNMonitor.View.Controls;
    using SVNMonitor.View.Dialogs;
    using SVNMonitor.View.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class MonitorsPanel : UserControl, IUserEntityView<Monitor>, ISelectableView<Monitor>, ISearchablePanel<Monitor>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<Monitor> allMonitors;
        private UIRebar BottomRebar1;
        private UICommand cmdClearAllErrors;
        private UICommand cmdClearAllErrors1;
        private UICommand cmdClearError;
        private UICommand cmdClearError1;
        private UICommand cmdDelete;
        private UICommand cmdDelete3;
        private UICommand cmdEdit;
        private UICommand cmdEnabled;
        private UICommand cmdEnabled1;
        private UICommand cmdMoveDown;
        private UICommand cmdMoveUp;
        private UICommand cmdNew;
        private IContainer components;
        private UIRebar LeftRebar1;
        private SVNMonitor.View.Controls.MonitorsExplorerBar monitorsExplorerBar1;
        private Panel panel1;
        private readonly UserEntityPresenter<Monitor> presenter;
        private UIRebar RightRebar1;
        private EventHandler selectionChanged;
        private UICommand Separator3;
        private bool showingAllItems;
        private UIRebar TopRebar1;
        private UICommandBar uiCommandBar1;
        private UICommandManager uiCommandManager1;
        private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;

        public event EventHandler SelectionChanged
        {
            add
            {
                this.MonitorsExplorerBar.SelectionChanged += value;
                this.selectionChanged = (EventHandler) Delegate.Combine(this.selectionChanged, value);
            }
            remove
            {
                this.MonitorsExplorerBar.SelectionChanged -= value;
                this.selectionChanged = (EventHandler) Delegate.Remove(this.selectionChanged, value);
            }
        }

        public MonitorsPanel()
        {
            this.InitializeComponent();
            if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
            {
                UIHelper.ApplyResources(this.uiCommandManager1);
                this.presenter = new UserEntityPresenter<Monitor>(this);
            }
        }

        private void ClearAllErrors()
        {
            Monitor.ClearAllErrors();
        }

        private void ClearError()
        {
            this.SelectedItem.ClearError();
        }

        public void ClearSearch()
        {
            this.MonitorsExplorerBar.SetVisibleEntities();
            this.showingAllItems = true;
            this.OnSelectionChanged();
        }

        private void cmdClearAllErrors_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.ClearAllErrors();
        }

        private void cmdClearError_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.ClearError();
        }

        private void cmdDelete_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.presenter.Delete();
        }

        private void cmdEdit_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.Edit();
        }

        private void cmdEnabled_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.EnableMonitor();
        }

        private void cmdMoveDown_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.presenter.MoveDown();
        }

        private void cmdMoveUp_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.presenter.MoveUp();
        }

        private void cmdNew_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            this.CreateNew();
        }

        public virtual void CreateNew()
        {
            this.presenter.New();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Edit()
        {
            this.presenter.Edit();
        }

        public void EnableCommands()
        {
            if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new MethodInvoker(this.EnableCommands));
                }
                else
                {
                    Monitor monitor = this.SelectedItem;
                    this.CanEnable = monitor != null;
                    this.cmdEnabled.Checked = ((monitor != null) && monitor.Enabled).ToInheritableBoolean();
                    this.CanClearError = (monitor != null) && monitor.HasError;
                    this.CanClearAllErrors = MonitorSettings.Instance.GetEnumerableMonitors().Any<Monitor>(m => m.HasError);
                }
            }
        }

        private void EnableMonitor()
        {
            this.SelectedItem.Enabled = this.cmdEnabled.Checked == InheritableBoolean.True;
        }

        private void FocusError()
        {
            Monitor monitor = this.SelectedItem;
            MainForm.FormInstance.FocusEventLog(monitor.ErrorEventID);
        }

        public IEnumerable<Monitor> GetAllItems()
        {
            return this.Entities;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MonitorsPanel));
            this.cmdNew = new UICommand("cmdNew");
            this.cmdEdit = new UICommand("cmdEdit");
            this.cmdDelete = new UICommand("cmdDelete");
            this.cmdMoveUp = new UICommand("cmdMoveUp");
            this.cmdMoveDown = new UICommand("cmdMoveDown");
            this.uiCommandManager1 = new UICommandManager(this.components);
            this.BottomRebar1 = new UIRebar();
            this.uiCommandBar1 = new UICommandBar();
            this.cmdDelete3 = new UICommand("cmdDelete");
            this.cmdEnabled = new UICommand("cmdEnabled");
            this.cmdClearError = new UICommand("cmdClearError");
            this.cmdClearAllErrors = new UICommand("cmdClearAllErrors");
            this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
            this.cmdEnabled1 = new UICommand("cmdEnabled");
            this.cmdClearError1 = new UICommand("cmdClearError");
            this.cmdClearAllErrors1 = new UICommand("cmdClearAllErrors");
            this.LeftRebar1 = new UIRebar();
            this.RightRebar1 = new UIRebar();
            this.TopRebar1 = new UIRebar();
            this.monitorsExplorerBar1 = new SVNMonitor.View.Controls.MonitorsExplorerBar();
            this.Separator3 = new UICommand("Separator");
            this.panel1 = new Panel();
            UICommand cmdNew1 = new UICommand("cmdNew");
            UICommand cmdEdit1 = new UICommand("cmdEdit");
            UICommand cmdMoveUp1 = new UICommand("cmdMoveUp");
            UICommand cmdMoveDown1 = new UICommand("cmdMoveDown");
            UICommand Separator1 = new UICommand("Separator");
            UICommand cmdNew2 = new UICommand("cmdNew");
            UICommand cmdEdit2 = new UICommand("cmdEdit");
            UICommand cmdDelete2 = new UICommand("cmdDelete");
            UICommand cmdMoveUp2 = new UICommand("cmdMoveUp");
            UICommand cmdMoveDown2 = new UICommand("cmdMoveDown");
            UICommand Separator2 = new UICommand("Separator");
            ((ISupportInitialize) this.uiCommandManager1).BeginInit();
            ((ISupportInitialize) this.BottomRebar1).BeginInit();
            ((ISupportInitialize) this.uiCommandBar1).BeginInit();
            ((ISupportInitialize) this.uiContextMenu1).BeginInit();
            ((ISupportInitialize) this.LeftRebar1).BeginInit();
            ((ISupportInitialize) this.RightRebar1).BeginInit();
            ((ISupportInitialize) this.TopRebar1).BeginInit();
            this.TopRebar1.SuspendLayout();
            ((ISupportInitialize) this.monitorsExplorerBar1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            cmdNew1.CommandStyle = CommandStyle.Image;
            cmdNew1.Key = "cmdNew";
            cmdNew1.Name = "cmdNew1";
            cmdEdit1.CommandStyle = CommandStyle.Image;
            cmdEdit1.Key = "cmdEdit";
            cmdEdit1.Name = "cmdEdit1";
            cmdMoveUp1.CommandStyle = CommandStyle.Image;
            cmdMoveUp1.Key = "cmdMoveUp";
            cmdMoveUp1.Name = "cmdMoveUp1";
            cmdMoveDown1.CommandStyle = CommandStyle.Image;
            cmdMoveDown1.Key = "cmdMoveDown";
            cmdMoveDown1.Name = "cmdMoveDown1";
            Separator1.CommandType = CommandType.Separator;
            Separator1.Key = "Separator";
            Separator1.Name = "Separator1";
            cmdNew2.Key = "cmdNew";
            cmdNew2.Name = "cmdNew2";
            cmdNew2.Text = "&New Monitor";
            cmdEdit2.Key = "cmdEdit";
            cmdEdit2.Name = "cmdEdit2";
            cmdEdit2.Text = "&Properties";
            cmdDelete2.Key = "cmdDelete";
            cmdDelete2.Name = "cmdDelete2";
            cmdDelete2.Text = "&Delete";
            cmdMoveUp2.Key = "cmdMoveUp";
            cmdMoveUp2.Name = "cmdMoveUp2";
            cmdMoveUp2.Text = "Move &Up";
            cmdMoveDown2.Key = "cmdMoveDown";
            cmdMoveDown2.Name = "cmdMoveDown2";
            cmdMoveDown2.Text = "Move Do&wn";
            Separator2.CommandType = CommandType.Separator;
            Separator2.Key = "Separator";
            Separator2.Name = "Separator2";
            this.cmdNew.Image = (Image) resources.GetObject("cmdNew.Image");
            this.cmdNew.Key = "cmdNew";
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Text = "New Monitor";
            this.cmdNew.ToolTipText = "New monitor";
            this.cmdNew.Click += new CommandEventHandler(this.cmdNew_Click);
            this.cmdEdit.Image = (Image) resources.GetObject("cmdEdit.Image");
            this.cmdEdit.Key = "cmdEdit";
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Text = "Properties";
            this.cmdEdit.ToolTipText = "Properties";
            this.cmdEdit.Click += new CommandEventHandler(this.cmdEdit_Click);
            this.cmdDelete.Image = (Image) resources.GetObject("cmdDelete.Image");
            this.cmdDelete.Key = "cmdDelete";
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.ToolTipText = "Delete monitor";
            this.cmdDelete.Click += new CommandEventHandler(this.cmdDelete_Click);
            this.cmdMoveUp.Image = (Image) resources.GetObject("cmdMoveUp.Image");
            this.cmdMoveUp.Key = "cmdMoveUp";
            this.cmdMoveUp.Name = "cmdMoveUp";
            this.cmdMoveUp.Text = "Move Up";
            this.cmdMoveUp.ToolTipText = "Move monitor up";
            this.cmdMoveUp.Click += new CommandEventHandler(this.cmdMoveUp_Click);
            this.cmdMoveDown.Image = (Image) resources.GetObject("cmdMoveDown.Image");
            this.cmdMoveDown.Key = "cmdMoveDown";
            this.cmdMoveDown.Name = "cmdMoveDown";
            this.cmdMoveDown.Text = "Move Down";
            this.cmdMoveDown.ToolTipText = "Move monitor down";
            this.cmdMoveDown.Click += new CommandEventHandler(this.cmdMoveDown_Click);
            this.uiCommandManager1.AllowClose = InheritableBoolean.False;
            this.uiCommandManager1.AllowCustomize = InheritableBoolean.False;
            this.uiCommandManager1.BottomRebar = this.BottomRebar1;
            this.uiCommandManager1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.uiCommandManager1.Commands.AddRange(new UICommand[] { this.cmdNew, this.cmdEdit, this.cmdDelete, this.cmdMoveUp, this.cmdMoveDown, this.cmdEnabled, this.cmdClearError, this.cmdClearAllErrors });
            this.uiCommandManager1.ContainerControl = this;
            this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] { this.uiContextMenu1 });
            this.uiCommandManager1.Id = new Guid("45030045-5a26-407f-8868-a5a7535cf928");
            this.uiCommandManager1.LeftRebar = this.LeftRebar1;
            this.uiCommandManager1.LockCommandBars = true;
            this.uiCommandManager1.RightRebar = this.RightRebar1;
            this.uiCommandManager1.ShowAddRemoveButton = InheritableBoolean.False;
            this.uiCommandManager1.ShowQuickCustomizeMenu = false;
            this.uiCommandManager1.TopRebar = this.TopRebar1;
            this.uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
            this.BottomRebar1.CommandManager = this.uiCommandManager1;
            this.BottomRebar1.Dock = DockStyle.Bottom;
            this.BottomRebar1.Location = new Point(0, 0x152);
            this.BottomRebar1.Name = "BottomRebar1";
            this.BottomRebar1.Size = new Size(0x1f9, 0);
            this.uiCommandBar1.AllowClose = InheritableBoolean.False;
            this.uiCommandBar1.AllowCustomize = InheritableBoolean.False;
            this.uiCommandBar1.Animation = DropDownAnimation.System;
            this.uiCommandBar1.CommandManager = this.uiCommandManager1;
            this.uiCommandBar1.Commands.AddRange(new UICommand[] { cmdNew1, cmdEdit1, this.cmdDelete3, Separator1, cmdMoveUp1, cmdMoveDown1 });
            this.uiCommandBar1.FullRow = true;
            this.uiCommandBar1.Key = "CommandBar1";
            this.uiCommandBar1.Location = new Point(0, 0);
            this.uiCommandBar1.Name = "uiCommandBar1";
            this.uiCommandBar1.RowIndex = 0;
            this.uiCommandBar1.ShowAddRemoveButton = InheritableBoolean.False;
            this.uiCommandBar1.Size = new Size(0x12d, 0x1c);
            this.uiCommandBar1.Text = "Monitors";
            this.cmdDelete3.CommandStyle = CommandStyle.Image;
            this.cmdDelete3.Key = "cmdDelete";
            this.cmdDelete3.Name = "cmdDelete3";
            this.cmdEnabled.CommandType = CommandType.ToggleButton;
            this.cmdEnabled.Key = "cmdEnabled";
            this.cmdEnabled.Name = "cmdEnabled";
            this.cmdEnabled.Text = "Enabled";
            this.cmdEnabled.ToolTipText = "Enabled";
            this.cmdEnabled.Click += new CommandEventHandler(this.cmdEnabled_Click);
            this.cmdClearError.Image = (Image) resources.GetObject("cmdClearError.Image");
            this.cmdClearError.Key = "cmdClearError";
            this.cmdClearError.Name = "cmdClearError";
            this.cmdClearError.Text = "Clear err&or";
            this.cmdClearError.Click += new CommandEventHandler(this.cmdClearError_Click);
            this.cmdClearAllErrors.Key = "cmdClearAllErrors";
            this.cmdClearAllErrors.Name = "cmdClearAllErrors";
            this.cmdClearAllErrors.Text = "Clear All Errors";
            this.cmdClearAllErrors.Click += new CommandEventHandler(this.cmdClearAllErrors_Click);
            this.uiContextMenu1.CommandManager = this.uiCommandManager1;
            this.uiContextMenu1.Commands.AddRange(new UICommand[] { this.cmdEnabled1, this.cmdClearError1, this.cmdClearAllErrors1, cmdNew2, cmdEdit2, cmdDelete2, Separator2, cmdMoveUp2, cmdMoveDown2 });
            this.uiContextMenu1.Key = "ContextMenu1";
            this.cmdEnabled1.Key = "cmdEnabled";
            this.cmdEnabled1.Name = "cmdEnabled1";
            this.cmdClearError1.Key = "cmdClearError";
            this.cmdClearError1.Name = "cmdClearError1";
            this.cmdClearAllErrors1.Key = "cmdClearAllErrors";
            this.cmdClearAllErrors1.Name = "cmdClearAllErrors1";
            this.LeftRebar1.CommandManager = this.uiCommandManager1;
            this.LeftRebar1.Dock = DockStyle.Left;
            this.LeftRebar1.Location = new Point(0, 0x1c);
            this.LeftRebar1.Name = "LeftRebar1";
            this.LeftRebar1.Size = new Size(0, 310);
            this.RightRebar1.CommandManager = this.uiCommandManager1;
            this.RightRebar1.Dock = DockStyle.Right;
            this.RightRebar1.Location = new Point(0x1f9, 0x1c);
            this.RightRebar1.Name = "RightRebar1";
            this.RightRebar1.Size = new Size(0, 310);
            this.TopRebar1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
            this.TopRebar1.CommandManager = this.uiCommandManager1;
            this.TopRebar1.Controls.Add(this.uiCommandBar1);
            this.TopRebar1.Dock = DockStyle.Top;
            this.TopRebar1.Location = new Point(0, 0);
            this.TopRebar1.Name = "TopRebar1";
            this.TopRebar1.Size = new Size(0x12d, 0x1c);
            this.monitorsExplorerBar1.BackgroundFormatStyle.BackColor = Color.White;
            this.monitorsExplorerBar1.BorderStyle = Janus.Windows.ExplorerBar.BorderStyle.None;
            this.uiCommandManager1.SetContextMenu(this.monitorsExplorerBar1, this.uiContextMenu1);
            this.monitorsExplorerBar1.Dock = DockStyle.Fill;
            this.monitorsExplorerBar1.GroupSeparation = 14;
            this.monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackColor = Color.Gainsboro;
            this.monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackColorGradient = Color.Silver;
            this.monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
            this.monitorsExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColor = SystemColors.ControlDark;
            this.monitorsExplorerBar1.GroupsStateStyles.HotFormatStyle.ForeColor = Color.Black;
            this.monitorsExplorerBar1.GroupsStateStyles.SelectedFormatStyle.BackColor = SystemColors.Control;
            this.monitorsExplorerBar1.Location = new Point(0, 1);
            this.monitorsExplorerBar1.Name = "monitorsExplorerBar1";
            this.monitorsExplorerBar1.Size = new Size(0x12d, 0x125);
            this.monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColor = Color.DarkGray;
            this.monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColorGradient = Color.DimGray;
            this.monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
            this.monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.ForeColor = Color.White;
            this.monitorsExplorerBar1.TabIndex = 1;
            this.monitorsExplorerBar1.ThemedAreas = ThemedArea.None;
            this.monitorsExplorerBar1.TopMargin = 14;
            this.monitorsExplorerBar1.KeyDown += new KeyEventHandler(this.monitorsExplorerBar1_KeyDown);
            this.monitorsExplorerBar1.SelectionChanged += new EventHandler(this.monitorsExplorerBar1_SelectionChanged);
            this.monitorsExplorerBar1.MouseDoubleClick += new MouseEventHandler(this.monitorsExplorerBar1_MouseDoubleClick);
            this.monitorsExplorerBar1.MouseDown += new MouseEventHandler(this.monitorsExplorerBar1_MouseDown);
            this.Separator3.CommandType = CommandType.Separator;
            this.Separator3.Key = "Separator";
            this.Separator3.Name = "Separator3";
            this.panel1.BackColor = Color.DimGray;
            this.panel1.Controls.Add(this.monitorsExplorerBar1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x1c);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(0, 1, 0, 0);
            this.panel1.Size = new Size(0x12d, 0x126);
            this.panel1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.TopRebar1);
            base.Name = "MonitorsPanel";
            base.Size = new Size(0x12d, 0x142);
            ((ISupportInitialize) this.uiCommandManager1).EndInit();
            ((ISupportInitialize) this.BottomRebar1).EndInit();
            ((ISupportInitialize) this.uiCommandBar1).EndInit();
            ((ISupportInitialize) this.uiContextMenu1).EndInit();
            ((ISupportInitialize) this.LeftRebar1).EndInit();
            ((ISupportInitialize) this.RightRebar1).EndInit();
            ((ISupportInitialize) this.TopRebar1).EndInit();
            this.TopRebar1.ResumeLayout(false);
            ((ISupportInitialize) this.monitorsExplorerBar1).EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void Monitor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.MonitorsExplorerBar.RefreshEntity((Monitor) e.Entity);
            this.presenter.EnableCommands();
        }

        private void MonitorsExplorerBar_ActionClick(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.presenter.Edit();
        }

        private void MonitorsExplorerBar_ConditionClick(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.presenter.Edit();
        }

        private void MonitorsExplorerBar_ErrorClick(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.FocusError();
        }

        private void monitorsExplorerBar1_KeyDown(object sender, KeyEventArgs e)
        {
            this.presenter.HandleKey(e);
        }

        private void monitorsExplorerBar1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bool cancel;
            Logger.LogUserAction();
            bool isGroup = this.MonitorsExplorerBar.IsGroupAtLocation(e.Location, out cancel);
            if (!cancel)
            {
                if (isGroup)
                {
                    this.Edit();
                }
                else
                {
                    this.presenter.New();
                }
            }
        }

        private void monitorsExplorerBar1_MouseDown(object sender, MouseEventArgs e)
        {
            this.MonitorsExplorerBar.SelectEntity(e.Location);
        }

        private void monitorsExplorerBar1_SelectionChanged(object sender, EventArgs e)
        {
            this.presenter.EnableCommands();
        }

        protected virtual void OnSelectionChanged()
        {
            if (this.selectionChanged != null)
            {
                this.selectionChanged(this, EventArgs.Empty);
            }
            this.presenter.EnableCommands();
        }

        public void Refetch()
        {
            this.MonitorsExplorerBar.RefreshEntities();
        }

        private void RegisterExplorerBarEvents()
        {
            this.UnregisterExplorerBarEvents();
            this.MonitorsExplorerBar.ConditionClick += new EventHandler(this.MonitorsExplorerBar_ConditionClick);
            this.MonitorsExplorerBar.ActionClick += new EventHandler(this.MonitorsExplorerBar_ActionClick);
            this.MonitorsExplorerBar.ErrorClick += new EventHandler(this.MonitorsExplorerBar_ErrorClick);
        }

        public void SetSearchResults(IEnumerable<Monitor> results)
        {
            this.MonitorsExplorerBar.SetVisibleEntities(results);
            this.showingAllItems = false;
            this.OnSelectionChanged();
        }

        object ISelectableView<Monitor>.Invoke(Delegate delegate1)
        {
            return base.Invoke(delegate1);
        }

        void IUserEntityView<Monitor>.Delete()
        {
        }

        DialogResult IUserEntityView<Monitor>.UserEdit(Monitor entity)
        {
            Logger.Log.InfoFormat("Editing monitor (monitor={0})", entity);
            DialogResult result = MonitorPropertiesDialog.ShowDialog(entity);
            Logger.Log.InfoFormat("User pressed {0}", result);
            return result;
        }

        DialogResult IUserEntityView<Monitor>.UserNew(Monitor entity)
        {
            Logger.Log.InfoFormat("Creating monitor (Guid={0})", entity.Guid);
            DialogResult result = MonitorPropertiesDialog.ShowDialog(entity);
            Logger.Log.InfoFormat("User pressed {0}", result);
            if ((result == DialogResult.OK) && (entity != null))
            {
                SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Monitor, string.Format("Monitor '{0}' created", entity.Name), entity);
            }
            return result;
        }

        private void UnregisterExplorerBarEvents()
        {
            this.MonitorsExplorerBar.ConditionClick -= new EventHandler(this.MonitorsExplorerBar_ConditionClick);
            this.MonitorsExplorerBar.ActionClick -= new EventHandler(this.MonitorsExplorerBar_ActionClick);
            this.MonitorsExplorerBar.ErrorClick -= new EventHandler(this.MonitorsExplorerBar_ErrorClick);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanClearAllErrors
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandVisible(this.cmdClearAllErrors);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandVisible(this.cmdClearAllErrors, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanClearError
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandVisible(this.cmdClearError);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandVisible(this.cmdClearError, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanDelete
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdDelete);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdDelete, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanEdit
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdEdit);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdEdit, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanEnable
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandVisible(this.cmdEnabled);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandVisible(this.cmdEnabled, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanMoveDown
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdMoveDown);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdMoveDown, value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanMoveUp
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdMoveUp);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdMoveUp, value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool CanNew
        {
            [DebuggerNonUserCode]
            get
            {
                return UIHelper.IsCommandEnabled(this.cmdNew);
            }
            [DebuggerNonUserCode]
            set
            {
                UIHelper.SetCommandEnabled(this.cmdNew, value);
            }
        }

        public int Count
        {
            [DebuggerNonUserCode]
            get
            {
                return this.MonitorsExplorerBar.Count;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public List<Monitor> Entities
        {
            [DebuggerNonUserCode]
            get
            {
                return this.allMonitors;
            }
            set
            {
                if (this.allMonitors != null)
                {
                    this.allMonitors.ForEach(delegate (Monitor monitor) {
                        monitor.StatusChanged -= new EventHandler<StatusChangedEventArgs>(this.Monitor_StatusChanged);
                    });
                }
                this.allMonitors = value;
                if (this.allMonitors != null)
                {
                    this.allMonitors.ForEach(delegate (Monitor monitor) {
                        monitor.StatusChanged += new EventHandler<StatusChangedEventArgs>(this.Monitor_StatusChanged);
                    });
                }
                this.MonitorsExplorerBar.Entities = this.allMonitors;
                this.RegisterExplorerBarEvents();
                this.SearchTextBox.Search();
            }
        }

        private SVNMonitor.View.Controls.MonitorsExplorerBar MonitorsExplorerBar
        {
            [DebuggerNonUserCode]
            get
            {
                return this.monitorsExplorerBar1;
            }
        }

        [Browsable(false)]
        public SearchTextBox<Monitor> SearchTextBox { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectedIndex
        {
            [DebuggerNonUserCode]
            get
            {
                return this.MonitorsExplorerBar.SelectedIndex;
            }
            [DebuggerNonUserCode]
            set
            {
                this.MonitorsExplorerBar.SelectedIndex = value;
            }
        }

        [Browsable(false)]
        public Monitor SelectedItem
        {
            [DebuggerNonUserCode]
            get
            {
                return this.MonitorsExplorerBar.SelectedEntity;
            }
        }

        public bool ShowingAllItems
        {
            [DebuggerNonUserCode]
            get
            {
                return this.showingAllItems;
            }
        }

        public Janus.Windows.UI.CommandBars.UIContextMenu UIContextMenu
        {
            get
            {
                return this.uiContextMenu1;
            }
        }
    }
}

