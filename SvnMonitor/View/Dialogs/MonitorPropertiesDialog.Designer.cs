namespace SVNMonitor.View.Dialogs
{
	internal partial class MonitorPropertiesDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorPropertiesDialog));
			filterEditor1 = new Janus.Windows.FilterEditor.FilterEditor();
			btnCancel = new System.Windows.Forms.Button();
			btnOK = new System.Windows.Forms.Button();
			txtMonitorName = new System.Windows.Forms.TextBox();
			lblMonitorName = new System.Windows.Forms.Label();
			groupWhen = new System.Windows.Forms.GroupBox();
			groupDo = new System.Windows.Forms.GroupBox();
			actionsPanel1 = new SVNMonitor.View.Panels.ActionsPanel();
			checkEnabled = new System.Windows.Forms.CheckBox();
			groupWhen.SuspendLayout();
			groupDo.SuspendLayout();
			base.SuspendLayout();
			filterEditor1.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(filterEditor1, "filterEditor1");
			filterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle;
			filterEditor1.MinSize = new System.Drawing.Size(0, 0);
			filterEditor1.Name = "filterEditor1";
			filterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both;
			filterEditor1.ScrollStep = 15;
			filterEditor1.VisualStyle = Janus.Windows.Common.VisualStyle.Standard;
			resources.ApplyResources(btnCancel, "btnCancel");
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Name = "btnCancel";
			btnCancel.Click += btnCancel_Click;
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
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
			actionsPanel1.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(actionsPanel1, "actionsPanel1");
			actionsPanel1.Name = "actionsPanel1";
			resources.ApplyResources(checkEnabled, "checkEnabled");
			checkEnabled.Checked = true;
			checkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			checkEnabled.Name = "checkEnabled";
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.Controls.Add(checkEnabled);
			base.Controls.Add(groupDo);
			base.Controls.Add(groupWhen);
			base.Controls.Add(lblMonitorName);
			base.Controls.Add(txtMonitorName);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "MonitorPropertiesDialog";
			base.ShowInTaskbar = false;
			base.Load += MonitorPropertiesDialog_Load;
			groupWhen.ResumeLayout(false);
			groupDo.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		#endregion
		private SVNMonitor.View.Panels.ActionsPanel actionsPanel1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.CheckBox checkEnabled;
		private Janus.Windows.FilterEditor.FilterEditor filterEditor1;
		private System.Windows.Forms.GroupBox groupDo;
		private System.Windows.Forms.GroupBox groupWhen;
		private System.Windows.Forms.Label lblMonitorName;
		private System.Windows.Forms.TextBox txtMonitorName;
	}
}