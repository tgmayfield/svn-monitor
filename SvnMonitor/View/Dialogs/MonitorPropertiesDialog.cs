using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using Janus.Windows.GridEX;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Dialogs
{
	internal partial class MonitorPropertiesDialog : BaseDialog
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SVNMonitor.Entities.Monitor monitor;
		
		
		public MonitorPropertiesDialog()
		{
			InitializeComponent();
			UIHelper.ApplyResources(filterEditor1);
		}

		public MonitorPropertiesDialog(SVNMonitor.Entities.Monitor monitor)
			: this()
		{
			Monitor = monitor;
			filterEditor1.SourceControl = Updater.Instance.UpdatesGrid;
		}

		private void BindData()
		{
			txtMonitorName.Text = monitor.Name;
			checkEnabled.Checked = monitor.Enabled;
			actionsPanel1.Actions = monitor.Actions;
			filterEditor1.DataBindings.Add("FilterCondition", monitor, "FilterCondition", true, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			RejectChanges();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Save();
		}

		private bool CancelEmptyFilter()
		{
			if (FilterCondition == null)
			{
				DialogResult result = MainForm.FormInstance.ShowMessage(Strings.MonitorWithoutCondition, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				Logger.Log.InfoFormat("No condition: User clicked {0}", result);
				if (result == DialogResult.No)
				{
					return true;
				}
				monitor.FilterCondition = null;
			}
			return false;
		}

		private bool CancelExistingFilter()
		{
			if (FilterCondition != null)
			{
				string thisFilterConditionString = FilterCondition.ToString();
				IEnumerable<string> existingConflictingMonitors = MonitorSettings.Instance.GetEnumerableMonitors().Where(m => (((m.FilterCondition != null) && (m != Monitor)) && (m.FilterCondition.ToString() == thisFilterConditionString))).Select(m => m.Name);
				int count = existingConflictingMonitors.Count();
				if (count > 0)
				{
					string message = string.Empty;
					if (count == 1)
					{
						message = Strings.MonitorExistsWithSameCondition_FORMAT.FormatWith(new object[]
						{
							existingConflictingMonitors.First()
						});
					}
					else
					{
						string monitorNames = string.Join(Environment.NewLine, existingConflictingMonitors.ToArray());
						message = Strings.MonitorsExistWithSameCondition_FORMAT.FormatWith(new object[]
						{
							monitorNames
						});
					}
					DialogResult result = MainForm.FormInstance.ShowMessage(message, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					Logger.Log.InfoFormat("Condition exists: User clicked {0}", result);
					if (result == DialogResult.No)
					{
						return true;
					}
				}
			}
			return false;
		}

		private void CheckChanges()
		{
			btnOK.Enabled = IsValid;
		}

		private void Field_Changed(object sender, EventArgs e)
		{
			CheckChanges();
		}

		private void MonitorPropertiesDialog_Load(object sender, EventArgs e)
		{
			txtMonitorName.TextChanged += Field_Changed;
			actionsPanel1.ActionsChanged += Field_Changed;
			CheckChanges();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			if (base.DialogResult == DialogResult.OK)
			{
				if (CancelEmptyFilter())
				{
					e.Cancel = true;
				}
				if (CancelExistingFilter())
				{
					e.Cancel = true;
				}
			}
		}

		private void RejectChanges()
		{
			monitor.RejectChanges();
		}

		private void Save()
		{
			monitor.Name = txtMonitorName.Text;
			monitor.Enabled = checkEnabled.Checked;
			monitor.FilterCondition = FilterCondition;
			monitor.Actions = actionsPanel1.Actions;
		}

		public static DialogResult ShowDialog(SVNMonitor.Entities.Monitor monitor)
		{
			MonitorPropertiesDialog dialog = new MonitorPropertiesDialog(monitor);
			return dialog.ShowDialog();
		}

		private GridEXFilterCondition FilterCondition
		{
			get
			{
				GridEXFilterCondition filterCondition = (GridEXFilterCondition)filterEditor1.FilterCondition;
				if ((filterCondition != null) && (filterCondition.Column == null))
				{
					filterCondition = null;
				}
				return filterCondition;
			}
		}

		[Browsable(false)]
		private bool IsValid
		{
			get
			{
				if (string.IsNullOrEmpty(txtMonitorName.Text))
				{
					return false;
				}
				return ((actionsPanel1.Actions != null) && (actionsPanel1.Actions.Count != 0));
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public SVNMonitor.Entities.Monitor Monitor
		{
			[DebuggerNonUserCode]
			get { return monitor; }
			private set
			{
				monitor = value;
				if (monitor != null)
				{
					BindData();
				}
			}
		}
	}
}