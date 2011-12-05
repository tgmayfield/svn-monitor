using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Janus.Windows.ExplorerBar;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;

using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Presenters;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Dialogs;
using SVNMonitor.View.Interfaces;

namespace SVNMonitor.View.Panels
{
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
				MonitorsExplorerBar.SelectionChanged += value;
				selectionChanged = (EventHandler)Delegate.Combine(selectionChanged, value);
			}
			remove
			{
				MonitorsExplorerBar.SelectionChanged -= value;
				selectionChanged = (EventHandler)Delegate.Remove(selectionChanged, value);
			}
		}

		public MonitorsPanel()
		{
			InitializeComponent();
			if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
			{
				UIHelper.ApplyResources(uiCommandManager1);
				presenter = new UserEntityPresenter<Monitor>(this);
			}
		}

		private void ClearAllErrors()
		{
			Monitor.ClearAllErrors();
		}

		private void ClearError()
		{
			SelectedItem.ClearError();
		}

		public void ClearSearch()
		{
			MonitorsExplorerBar.SetVisibleEntities();
			showingAllItems = true;
			OnSelectionChanged();
		}

		private void cmdClearAllErrors_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ClearAllErrors();
		}

		private void cmdClearError_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ClearError();
		}

		private void cmdDelete_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			presenter.Delete();
		}

		private void cmdEdit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Edit();
		}

		private void cmdEnabled_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			EnableMonitor();
		}

		private void cmdMoveDown_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			presenter.MoveDown();
		}

		private void cmdMoveUp_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			presenter.MoveUp();
		}

		private void cmdNew_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			CreateNew();
		}

		public virtual void CreateNew()
		{
			presenter.New();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void Edit()
		{
			presenter.Edit();
		}

		public void EnableCommands()
		{
			if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
			{
				if (base.InvokeRequired)
				{
					base.BeginInvoke(new MethodInvoker(EnableCommands));
				}
				else
				{
					Monitor monitor = SelectedItem;
					CanEnable = monitor != null;
					cmdEnabled.Checked = ((monitor != null) && monitor.Enabled).ToInheritableBoolean();
					CanClearError = (monitor != null) && monitor.HasError;
					CanClearAllErrors = MonitorSettings.Instance.GetEnumerableMonitors().Any(m => m.HasError);
				}
			}
		}

		private void EnableMonitor()
		{
			SelectedItem.Enabled = cmdEnabled.Checked == InheritableBoolean.True;
		}

		private void FocusError()
		{
			Monitor monitor = SelectedItem;
			MainForm.FormInstance.FocusEventLog(monitor.ErrorEventID);
		}

		public IEnumerable<Monitor> GetAllItems()
		{
			return Entities;
		}

		private void InitializeComponent()
		{
			components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MonitorsPanel));
			cmdNew = new UICommand("cmdNew");
			cmdEdit = new UICommand("cmdEdit");
			cmdDelete = new UICommand("cmdDelete");
			cmdMoveUp = new UICommand("cmdMoveUp");
			cmdMoveDown = new UICommand("cmdMoveDown");
			uiCommandManager1 = new UICommandManager(components);
			BottomRebar1 = new UIRebar();
			uiCommandBar1 = new UICommandBar();
			cmdDelete3 = new UICommand("cmdDelete");
			cmdEnabled = new UICommand("cmdEnabled");
			cmdClearError = new UICommand("cmdClearError");
			cmdClearAllErrors = new UICommand("cmdClearAllErrors");
			uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			cmdEnabled1 = new UICommand("cmdEnabled");
			cmdClearError1 = new UICommand("cmdClearError");
			cmdClearAllErrors1 = new UICommand("cmdClearAllErrors");
			LeftRebar1 = new UIRebar();
			RightRebar1 = new UIRebar();
			TopRebar1 = new UIRebar();
			monitorsExplorerBar1 = new SVNMonitor.View.Controls.MonitorsExplorerBar();
			Separator3 = new UICommand("Separator");
			panel1 = new Panel();
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
			((ISupportInitialize)uiCommandManager1).BeginInit();
			((ISupportInitialize)BottomRebar1).BeginInit();
			((ISupportInitialize)uiCommandBar1).BeginInit();
			((ISupportInitialize)uiContextMenu1).BeginInit();
			((ISupportInitialize)LeftRebar1).BeginInit();
			((ISupportInitialize)RightRebar1).BeginInit();
			((ISupportInitialize)TopRebar1).BeginInit();
			TopRebar1.SuspendLayout();
			((ISupportInitialize)monitorsExplorerBar1).BeginInit();
			panel1.SuspendLayout();
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
			cmdNew.Image = (Image)resources.GetObject("cmdNew.Image");
			cmdNew.Key = "cmdNew";
			cmdNew.Name = "cmdNew";
			cmdNew.Text = "New Monitor";
			cmdNew.ToolTipText = "New monitor";
			cmdNew.Click += cmdNew_Click;
			cmdEdit.Image = (Image)resources.GetObject("cmdEdit.Image");
			cmdEdit.Key = "cmdEdit";
			cmdEdit.Name = "cmdEdit";
			cmdEdit.Text = "Properties";
			cmdEdit.ToolTipText = "Properties";
			cmdEdit.Click += cmdEdit_Click;
			cmdDelete.Image = (Image)resources.GetObject("cmdDelete.Image");
			cmdDelete.Key = "cmdDelete";
			cmdDelete.Name = "cmdDelete";
			cmdDelete.Text = "Delete";
			cmdDelete.ToolTipText = "Delete monitor";
			cmdDelete.Click += cmdDelete_Click;
			cmdMoveUp.Image = (Image)resources.GetObject("cmdMoveUp.Image");
			cmdMoveUp.Key = "cmdMoveUp";
			cmdMoveUp.Name = "cmdMoveUp";
			cmdMoveUp.Text = "Move Up";
			cmdMoveUp.ToolTipText = "Move monitor up";
			cmdMoveUp.Click += cmdMoveUp_Click;
			cmdMoveDown.Image = (Image)resources.GetObject("cmdMoveDown.Image");
			cmdMoveDown.Key = "cmdMoveDown";
			cmdMoveDown.Name = "cmdMoveDown";
			cmdMoveDown.Text = "Move Down";
			cmdMoveDown.ToolTipText = "Move monitor down";
			cmdMoveDown.Click += cmdMoveDown_Click;
			uiCommandManager1.AllowClose = InheritableBoolean.False;
			uiCommandManager1.AllowCustomize = InheritableBoolean.False;
			uiCommandManager1.BottomRebar = BottomRebar1;
			uiCommandManager1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			uiCommandManager1.Commands.AddRange(new[]
			{
				cmdNew, cmdEdit, cmdDelete, cmdMoveUp, cmdMoveDown, cmdEnabled, cmdClearError, cmdClearAllErrors
			});
			uiCommandManager1.ContainerControl = this;
			uiCommandManager1.ContextMenus.AddRange(new[]
			{
				uiContextMenu1
			});
			uiCommandManager1.Id = new Guid("45030045-5a26-407f-8868-a5a7535cf928");
			uiCommandManager1.LeftRebar = LeftRebar1;
			uiCommandManager1.LockCommandBars = true;
			uiCommandManager1.RightRebar = RightRebar1;
			uiCommandManager1.ShowAddRemoveButton = InheritableBoolean.False;
			uiCommandManager1.ShowQuickCustomizeMenu = false;
			uiCommandManager1.TopRebar = TopRebar1;
			uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			BottomRebar1.CommandManager = uiCommandManager1;
			BottomRebar1.Dock = DockStyle.Bottom;
			BottomRebar1.Location = new Point(0, 0x152);
			BottomRebar1.Name = "BottomRebar1";
			BottomRebar1.Size = new Size(0x1f9, 0);
			uiCommandBar1.AllowClose = InheritableBoolean.False;
			uiCommandBar1.AllowCustomize = InheritableBoolean.False;
			uiCommandBar1.Animation = DropDownAnimation.System;
			uiCommandBar1.CommandManager = uiCommandManager1;
			uiCommandBar1.Commands.AddRange(new[]
			{
				cmdNew1, cmdEdit1, cmdDelete3, Separator1, cmdMoveUp1, cmdMoveDown1
			});
			uiCommandBar1.FullRow = true;
			uiCommandBar1.Key = "CommandBar1";
			uiCommandBar1.Location = new Point(0, 0);
			uiCommandBar1.Name = "uiCommandBar1";
			uiCommandBar1.RowIndex = 0;
			uiCommandBar1.ShowAddRemoveButton = InheritableBoolean.False;
			uiCommandBar1.Size = new Size(0x12d, 0x1c);
			uiCommandBar1.Text = "Monitors";
			cmdDelete3.CommandStyle = CommandStyle.Image;
			cmdDelete3.Key = "cmdDelete";
			cmdDelete3.Name = "cmdDelete3";
			cmdEnabled.CommandType = CommandType.ToggleButton;
			cmdEnabled.Key = "cmdEnabled";
			cmdEnabled.Name = "cmdEnabled";
			cmdEnabled.Text = "Enabled";
			cmdEnabled.ToolTipText = "Enabled";
			cmdEnabled.Click += cmdEnabled_Click;
			cmdClearError.Image = (Image)resources.GetObject("cmdClearError.Image");
			cmdClearError.Key = "cmdClearError";
			cmdClearError.Name = "cmdClearError";
			cmdClearError.Text = "Clear err&or";
			cmdClearError.Click += cmdClearError_Click;
			cmdClearAllErrors.Key = "cmdClearAllErrors";
			cmdClearAllErrors.Name = "cmdClearAllErrors";
			cmdClearAllErrors.Text = "Clear All Errors";
			cmdClearAllErrors.Click += cmdClearAllErrors_Click;
			uiContextMenu1.CommandManager = uiCommandManager1;
			uiContextMenu1.Commands.AddRange(new[]
			{
				cmdEnabled1, cmdClearError1, cmdClearAllErrors1, cmdNew2, cmdEdit2, cmdDelete2, Separator2, cmdMoveUp2, cmdMoveDown2
			});
			uiContextMenu1.Key = "ContextMenu1";
			cmdEnabled1.Key = "cmdEnabled";
			cmdEnabled1.Name = "cmdEnabled1";
			cmdClearError1.Key = "cmdClearError";
			cmdClearError1.Name = "cmdClearError1";
			cmdClearAllErrors1.Key = "cmdClearAllErrors";
			cmdClearAllErrors1.Name = "cmdClearAllErrors1";
			LeftRebar1.CommandManager = uiCommandManager1;
			LeftRebar1.Dock = DockStyle.Left;
			LeftRebar1.Location = new Point(0, 0x1c);
			LeftRebar1.Name = "LeftRebar1";
			LeftRebar1.Size = new Size(0, 310);
			RightRebar1.CommandManager = uiCommandManager1;
			RightRebar1.Dock = DockStyle.Right;
			RightRebar1.Location = new Point(0x1f9, 0x1c);
			RightRebar1.Name = "RightRebar1";
			RightRebar1.Size = new Size(0, 310);
			TopRebar1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			TopRebar1.CommandManager = uiCommandManager1;
			TopRebar1.Controls.Add(uiCommandBar1);
			TopRebar1.Dock = DockStyle.Top;
			TopRebar1.Location = new Point(0, 0);
			TopRebar1.Name = "TopRebar1";
			TopRebar1.Size = new Size(0x12d, 0x1c);
			monitorsExplorerBar1.BackgroundFormatStyle.BackColor = Color.White;
			monitorsExplorerBar1.BorderStyle = Janus.Windows.ExplorerBar.BorderStyle.None;
			uiCommandManager1.SetContextMenu(monitorsExplorerBar1, uiContextMenu1);
			monitorsExplorerBar1.Dock = DockStyle.Fill;
			monitorsExplorerBar1.GroupSeparation = 14;
			monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackColor = Color.Gainsboro;
			monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackColorGradient = Color.Silver;
			monitorsExplorerBar1.GroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			monitorsExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColor = SystemColors.ControlDark;
			monitorsExplorerBar1.GroupsStateStyles.HotFormatStyle.ForeColor = Color.Black;
			monitorsExplorerBar1.GroupsStateStyles.SelectedFormatStyle.BackColor = SystemColors.Control;
			monitorsExplorerBar1.Location = new Point(0, 1);
			monitorsExplorerBar1.Name = "monitorsExplorerBar1";
			monitorsExplorerBar1.Size = new Size(0x12d, 0x125);
			monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColor = Color.DarkGray;
			monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColorGradient = Color.DimGray;
			monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			monitorsExplorerBar1.SpecialGroupsStateStyles.FormatStyle.ForeColor = Color.White;
			monitorsExplorerBar1.TabIndex = 1;
			monitorsExplorerBar1.ThemedAreas = ThemedArea.None;
			monitorsExplorerBar1.TopMargin = 14;
			monitorsExplorerBar1.KeyDown += monitorsExplorerBar1_KeyDown;
			monitorsExplorerBar1.SelectionChanged += monitorsExplorerBar1_SelectionChanged;
			monitorsExplorerBar1.MouseDoubleClick += monitorsExplorerBar1_MouseDoubleClick;
			monitorsExplorerBar1.MouseDown += monitorsExplorerBar1_MouseDown;
			Separator3.CommandType = CommandType.Separator;
			Separator3.Key = "Separator";
			Separator3.Name = "Separator3";
			panel1.BackColor = Color.DimGray;
			panel1.Controls.Add(monitorsExplorerBar1);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0x1c);
			panel1.Name = "panel1";
			panel1.Padding = new Padding(0, 1, 0, 0);
			panel1.Size = new Size(0x12d, 0x126);
			panel1.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(panel1);
			base.Controls.Add(TopRebar1);
			base.Name = "MonitorsPanel";
			base.Size = new Size(0x12d, 0x142);
			((ISupportInitialize)uiCommandManager1).EndInit();
			((ISupportInitialize)BottomRebar1).EndInit();
			((ISupportInitialize)uiCommandBar1).EndInit();
			((ISupportInitialize)uiContextMenu1).EndInit();
			((ISupportInitialize)LeftRebar1).EndInit();
			((ISupportInitialize)RightRebar1).EndInit();
			((ISupportInitialize)TopRebar1).EndInit();
			TopRebar1.ResumeLayout(false);
			((ISupportInitialize)monitorsExplorerBar1).EndInit();
			panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void Monitor_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			MonitorsExplorerBar.RefreshEntity((Monitor)e.Entity);
			presenter.EnableCommands();
		}

		private void MonitorsExplorerBar_ActionClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			presenter.Edit();
		}

		private void MonitorsExplorerBar_ConditionClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			presenter.Edit();
		}

		private void MonitorsExplorerBar_ErrorClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			FocusError();
		}

		private void monitorsExplorerBar1_KeyDown(object sender, KeyEventArgs e)
		{
			presenter.HandleKey(e);
		}

		private void monitorsExplorerBar1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			bool cancel;
			Logger.LogUserAction();
			bool isGroup = MonitorsExplorerBar.IsGroupAtLocation(e.Location, out cancel);
			if (!cancel)
			{
				if (isGroup)
				{
					Edit();
				}
				else
				{
					presenter.New();
				}
			}
		}

		private void monitorsExplorerBar1_MouseDown(object sender, MouseEventArgs e)
		{
			MonitorsExplorerBar.SelectEntity(e.Location);
		}

		private void monitorsExplorerBar1_SelectionChanged(object sender, EventArgs e)
		{
			presenter.EnableCommands();
		}

		protected virtual void OnSelectionChanged()
		{
			if (selectionChanged != null)
			{
				selectionChanged(this, EventArgs.Empty);
			}
			presenter.EnableCommands();
		}

		public void Refetch()
		{
			MonitorsExplorerBar.RefreshEntities();
		}

		private void RegisterExplorerBarEvents()
		{
			UnregisterExplorerBarEvents();
			MonitorsExplorerBar.ConditionClick += MonitorsExplorerBar_ConditionClick;
			MonitorsExplorerBar.ActionClick += MonitorsExplorerBar_ActionClick;
			MonitorsExplorerBar.ErrorClick += MonitorsExplorerBar_ErrorClick;
		}

		public void SetSearchResults(IEnumerable<Monitor> results)
		{
			MonitorsExplorerBar.SetVisibleEntities(results);
			showingAllItems = false;
			OnSelectionChanged();
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
			MonitorsExplorerBar.ConditionClick -= MonitorsExplorerBar_ConditionClick;
			MonitorsExplorerBar.ActionClick -= MonitorsExplorerBar_ActionClick;
			MonitorsExplorerBar.ErrorClick -= MonitorsExplorerBar_ErrorClick;
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanClearAllErrors
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdClearAllErrors); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdClearAllErrors, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanClearError
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdClearError); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdClearError, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanDelete
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdDelete); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdDelete, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanEdit
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdEdit); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdEdit, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanEnable
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdEnabled); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdEnabled, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanMoveDown
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdMoveDown); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdMoveDown, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanMoveUp
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdMoveUp); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdMoveUp, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanNew
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdNew); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdNew, value); }
		}

		public int Count
		{
			[DebuggerNonUserCode]
			get { return MonitorsExplorerBar.Count; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public List<Monitor> Entities
		{
			[DebuggerNonUserCode]
			get { return allMonitors; }
			set
			{
				if (allMonitors != null)
				{
					allMonitors.ForEach(delegate(Monitor monitor) { monitor.StatusChanged -= Monitor_StatusChanged; });
				}
				allMonitors = value;
				if (allMonitors != null)
				{
					allMonitors.ForEach(delegate(Monitor monitor) { monitor.StatusChanged += Monitor_StatusChanged; });
				}
				MonitorsExplorerBar.Entities = allMonitors;
				RegisterExplorerBarEvents();
				SearchTextBox.Search();
			}
		}

		private SVNMonitor.View.Controls.MonitorsExplorerBar MonitorsExplorerBar
		{
			[DebuggerNonUserCode]
			get { return monitorsExplorerBar1; }
		}

		[Browsable(false)]
		public SearchTextBox<Monitor> SearchTextBox { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public int SelectedIndex
		{
			[DebuggerNonUserCode]
			get { return MonitorsExplorerBar.SelectedIndex; }
			[DebuggerNonUserCode]
			set { MonitorsExplorerBar.SelectedIndex = value; }
		}

		[Browsable(false)]
		public Monitor SelectedItem
		{
			[DebuggerNonUserCode]
			get { return MonitorsExplorerBar.SelectedEntity; }
		}

		public bool ShowingAllItems
		{
			[DebuggerNonUserCode]
			get { return showingAllItems; }
		}

		public Janus.Windows.UI.CommandBars.UIContextMenu UIContextMenu
		{
			get { return uiContextMenu1; }
		}
	}
}