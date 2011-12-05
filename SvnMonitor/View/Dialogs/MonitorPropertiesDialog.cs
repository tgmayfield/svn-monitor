using SVNMonitor.View.Panels;
using System.Windows.Forms;
using System.ComponentModel;
using Janus.Windows.FilterEditor;
using System.Diagnostics;
using SVNMonitor.Entities;
using Janus.Windows.GridEX;
using System;
using SVNMonitor.Helpers;
using SVNMonitor;
using SVNMonitor.Logging;
using SVNMonitor.View;
using SVNMonitor.Resources.Text;
using System.Collections.Generic;
using SVNMonitor.Extensions;
using System.Drawing;
using Janus.Windows.UI.Dock;
using Janus.Windows.Common;

namespace SVNMonitor.View.Dialogs
{
internal class MonitorPropertiesDialog : BaseDialog
{
	private ActionsPanel actionsPanel1;

	private Button btnCancel;

	private Button btnOK;

	private CheckBox checkEnabled;

	private IContainer components;

	private FilterEditor filterEditor1;

	private GroupBox groupDo;

	private GroupBox groupWhen;

	private Label lblMonitorName;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Monitor monitor;

	private TextBox txtMonitorName;

	private GridEXFilterCondition FilterCondition
	{
		get
		{
			GridEXFilterCondition filterCondition = (GridEXFilterCondition)this.filterEditor1.FilterCondition;
			if (filterCondition != null && filterCondition.Column == null)
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
			if (string.IsNullOrEmpty(this.txtMonitorName.Text))
			{
				return false;
			}
			if (this.actionsPanel1.Actions == null || this.actionsPanel1.Actions.Count == 0)
			{
				return false;
			}
			return true;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public Monitor Monitor
	{
		get
		{
			return this.monitor;
		}
		private set
		{
			this.monitor = value;
			if (this.monitor != null)
			{
				this.BindData();
			}
		}
	}

	public MonitorPropertiesDialog()
	{
		this.InitializeComponent();
		UIHelper.ApplyResources(this.filterEditor1);
	}

	public MonitorPropertiesDialog(Monitor monitor)
	{
		this.Monitor = monitor;
		this.filterEditor1.SourceControl = Updater.Instance.UpdatesGrid;
	}

	private void BindData()
	{
		this.txtMonitorName.Text = this.monitor.Name;
		this.checkEnabled.Checked = this.monitor.Enabled;
		this.actionsPanel1.Actions = this.monitor.Actions;
		this.filterEditor1.DataBindings.Add("FilterCondition", this.monitor, "FilterCondition", true, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.RejectChanges();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.Save();
	}

	private bool CancelEmptyFilter()
	{
		if (this.FilterCondition == null)
		{
			DialogResult result = MainForm.FormInstance.ShowMessage(Strings.MonitorWithoutCondition, base.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			Logger.Log.InfoFormat("No condition: User clicked {0}", result);
			if (result == DialogResult.No)
			{
				return true;
			}
		}
		this.monitor.FilterCondition = null;
		return false;
		return false;
	}

	private bool CancelExistingFilter()
	{
		object[] objArray;
		object[] objArray2;
		if (this.FilterCondition == null)
		{
			return false;
		}
		MonitorPropertiesDialog monitorPropertiesDialog = new MonitorPropertiesDialog();
		monitorPropertiesDialog.<>4__this = this;
		monitorPropertiesDialog.thisFilterConditionString = this.FilterCondition.ToString();
		IEnumerable<string> existingConflictingMonitors = MonitorSettings.Instance.GetEnumerableMonitors().Where<Monitor>(new Predicate<Monitor>(monitorPropertiesDialog.<CancelExistingFilter>b__0)).Select<Monitor,string>(new Func<Monitor, string>((m) => m.Name));
		int count = existingConflictingMonitors.Count<string>();
		if (count > 0)
		{
			string message = string.Empty;
			if (count == 1)
			{
				message = Strings.MonitorExistsWithSameCondition_FORMAT.FormatWith(new object[] { existingConflictingMonitors.First<string>() });
			}
			else
			{
				string monitorNames = string.Join(Environment.NewLine, existingConflictingMonitors.ToArray<string>());
				message = Strings.MonitorsExistWithSameCondition_FORMAT.FormatWith(new object[] { monitorNames });
			}
			DialogResult result = MainForm.FormInstance.ShowMessage(message, base.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			Logger.Log.InfoFormat("Condition exists: User clicked {0}", result);
			if (result == DialogResult.No)
			{
				return true;
			}
		}
		return false;
	}

	private void CheckChanges()
	{
		this.btnOK.Enabled = this.IsValid;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && this.components != null)
		{
			this.components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void Field_Changed(object sender, EventArgs e)
	{
		this.CheckChanges();
	}

	private void InitializeComponent()
	{
		this.components = new Container();
		ComponentResourceManager resources = new ComponentResourceManager(typeof(MonitorPropertiesDialog));
		this.filterEditor1 = new FilterEditor();
		this.btnCancel = new Button();
		this.btnOK = new Button();
		this.txtMonitorName = new TextBox();
		this.lblMonitorName = new Label();
		this.groupWhen = new GroupBox();
		this.groupDo = new GroupBox();
		this.actionsPanel1 = new ActionsPanel();
		this.checkEnabled = new CheckBox();
		this.groupWhen.SuspendLayout();
		this.groupDo.SuspendLayout();
		base.SuspendLayout();
		this.filterEditor1.BackColor = SystemColors.Control;
		resources.ApplyResources(this.filterEditor1, "filterEditor1");
		this.filterEditor1.InnerAreaStyle = PanelInnerAreaStyle.UseFormatStyle;
		this.filterEditor1.MinSize = new Size(0, 0);
		this.filterEditor1.Name = "filterEditor1";
		this.filterEditor1.ScrollMode = ScrollMode.Both;
		this.filterEditor1.ScrollStep = 15;
		this.filterEditor1.VisualStyle = VisualStyle.Standard;
		resources.ApplyResources(this.btnCancel, "btnCancel");
		this.btnCancel.DialogResult = DialogResult.Cancel;
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.add_Click(new EventHandler(this.btnCancel_Click));
		resources.ApplyResources(this.btnOK, "btnOK");
		this.btnOK.DialogResult = DialogResult.OK;
		this.btnOK.Name = "btnOK";
		this.btnOK.add_Click(new EventHandler(this.btnOK_Click));
		resources.ApplyResources(this.txtMonitorName, "txtMonitorName");
		this.txtMonitorName.Name = "txtMonitorName";
		resources.ApplyResources(this.lblMonitorName, "lblMonitorName");
		this.lblMonitorName.Name = "lblMonitorName";
		resources.ApplyResources(this.groupWhen, "groupWhen");
		this.groupWhen.Controls.Add(this.filterEditor1);
		this.groupWhen.Name = "groupWhen";
		this.groupWhen.TabStop = false;
		resources.ApplyResources(this.groupDo, "groupDo");
		this.groupDo.Controls.Add(this.actionsPanel1);
		this.groupDo.Name = "groupDo";
		this.groupDo.TabStop = false;
		this.actionsPanel1.BackColor = Color.Transparent;
		resources.ApplyResources(this.actionsPanel1, "actionsPanel1");
		this.actionsPanel1.Name = "actionsPanel1";
		resources.ApplyResources(this.checkEnabled, "checkEnabled");
		this.checkEnabled.Checked = true;
		this.checkEnabled.CheckState = CheckState.Checked;
		this.checkEnabled.Name = "checkEnabled";
		base.AcceptButton = this.btnOK;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.Controls.Add(this.checkEnabled);
		base.Controls.Add(this.groupDo);
		base.Controls.Add(this.groupWhen);
		base.Controls.Add(this.lblMonitorName);
		base.Controls.Add(this.txtMonitorName);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.btnCancel);
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.Name = "MonitorPropertiesDialog";
		base.ShowInTaskbar = false;
		base.Load += new EventHandler(this.MonitorPropertiesDialog_Load);
		this.groupWhen.ResumeLayout(false);
		this.groupDo.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void MonitorPropertiesDialog_Load(object sender, EventArgs e)
	{
		this.txtMonitorName.add_TextChanged(new EventHandler(this.Field_Changed));
		this.actionsPanel1.ActionsChanged += new EventHandler(this.Field_Changed);
		this.CheckChanges();
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		base.OnFormClosing(e);
		if (base.DialogResult == 1)
		{
			if (this.CancelEmptyFilter())
			{
				e.Cancel = true;
			}
			if (this.CancelExistingFilter())
			{
				e.Cancel = true;
			}
		}
	}

	private void RejectChanges()
	{
		this.monitor.RejectChanges();
	}

	private void Save()
	{
		this.monitor.Name = this.txtMonitorName.Text;
		this.monitor.Enabled = this.checkEnabled.Checked;
		this.monitor.FilterCondition = this.FilterCondition;
		this.monitor.Actions = this.actionsPanel1.Actions;
	}

	public static DialogResult ShowDialog(Monitor monitor)
	{
		MonitorPropertiesDialog dialog = new MonitorPropertiesDialog(monitor);
		DialogResult result = dialog.ShowDialog();
		return result;
	}
}
}