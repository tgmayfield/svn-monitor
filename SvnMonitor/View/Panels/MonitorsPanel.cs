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
	internal partial class MonitorsPanel : UserControl, IUserEntityView<Monitor>, ISelectableView<Monitor>, ISearchablePanel<Monitor>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Monitor> allMonitors;
		private readonly UserEntityPresenter<Monitor> presenter;
		private EventHandler selectionChanged;
		private bool showingAllItems;
		
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