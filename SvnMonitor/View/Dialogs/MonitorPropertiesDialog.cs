using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Janus.Windows.GridEX;
using Janus.Windows.UI.Dock;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.View.Panels;

namespace SVNMonitor.View.Dialogs
{
	internal class MonitorPropertiesDialog : BaseDialog
	{
		private ActionsPanel actionsPanel1;
		private Button btnCancel;
		private Button btnOK;
		private CheckBox checkEnabled;
		private IContainer components;
		private Janus.Windows.FilterEditor.FilterEditor filterEditor1;
		private GroupBox groupDo;
		private GroupBox groupWhen;
		private Label lblMonitorName;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SVNMonitor.Entities.Monitor monitor;
		private TextBox txtMonitorName;

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
				IEnumerable<string> existingConflictingMonitors = MonitorSettings.Instance.GetEnumerableMonitors().Where(m => (((m.FilterCondition != null) && (m != Monitor)) && (m.FilterCondition.ToString() == thisFilterConditionString))).Select<SVNMonitor.Entities.Monitor, string>(m => m.Name);
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void Field_Changed(object sender, EventArgs e)
		{
			CheckChanges();
		}

		private void InitializeComponent()
		{
			components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MonitorPropertiesDialog));
			filterEditor1 = new Janus.Windows.FilterEditor.FilterEditor();
			btnCancel = new Button();
			btnOK = new Button();
			txtMonitorName = new TextBox();
			lblMonitorName = new Label();
			groupWhen = new GroupBox();
			groupDo = new GroupBox();
			actionsPanel1 = new ActionsPanel();
			checkEnabled = new CheckBox();
			groupWhen.SuspendLayout();
			groupDo.SuspendLayout();
			base.SuspendLayout();
			filterEditor1.BackColor = SystemColors.Control;
			resources.ApplyResources(filterEditor1, "filterEditor1");
			filterEditor1.InnerAreaStyle = PanelInnerAreaStyle.UseFormatStyle;
			filterEditor1.MinSize = new Size(0, 0);
			filterEditor1.Name = "filterEditor1";
			filterEditor1.ScrollMode = ScrollMode.Both;
			filterEditor1.ScrollStep = 15;
			filterEditor1.VisualStyle = Janus.Windows.Common.VisualStyle.Standard;
			resources.ApplyResources(btnCancel, "btnCancel");
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Name = "btnCancel";
			btnCancel.Click += btnCancel_Click;
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Name = "btnOK";
			btnOK.Click += btnOK_Click;
			resources.ApplyResources(txtMonitorName, "txtMonitorName");
			txtMonitorName.Name = "txtMonitorName";
			resources.ApplyResources(lblMonitorName, "lblMonitorName");
			lblMonitorName.Name = "lblMonitorName";
			resources.ApplyResources(groupWhen, "groupWhen");
			groupWhen.Controls.Add(filterEditor1);
			groupWhen.Name = "groupWhen";
			groupWhen.TabStop = false;
			resources.ApplyResources(groupDo, "groupDo");
			groupDo.Controls.Add(actionsPanel1);
			groupDo.Name = "groupDo";
			groupDo.TabStop = false;
			actionsPanel1.BackColor = Color.Transparent;
			resources.ApplyResources(actionsPanel1, "actionsPanel1");
			actionsPanel1.Name = "actionsPanel1";
			resources.ApplyResources(checkEnabled, "checkEnabled");
			checkEnabled.Checked = true;
			checkEnabled.CheckState = CheckState.Checked;
			checkEnabled.Name = "checkEnabled";
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.Controls.Add(checkEnabled);
			base.Controls.Add(groupDo);
			base.Controls.Add(groupWhen);
			base.Controls.Add(lblMonitorName);
			base.Controls.Add(txtMonitorName);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCancel);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Name = "MonitorPropertiesDialog";
			base.ShowInTaskbar = false;
			base.Load += MonitorPropertiesDialog_Load;
			groupWhen.ResumeLayout(false);
			groupDo.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
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