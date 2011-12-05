using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using Janus.Windows.EditControls;

using SVNMonitor.Logging;
using SVNMonitor.View.Controls;

namespace SVNMonitor.View.Dialogs
{
	internal class ActionPropertiesDialog : BaseDialog
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SVNMonitor.Actions.Action action;
		private ActionSelector actionSelector1;
		private Button btnCancel;
		private Button btnOK;
		private IContainer components;
		private Label lblActionProperties;
		private Label lblActionType;
		private Label lblSelectActionTypeTitle;
		private PropertyGrid propertyGrid1;
		private bool suppressActionSelectorChange;

		public ActionPropertiesDialog()
		{
			InitializeComponent();
		}

		private void actionSelector1_SelectedIndexChanged(object sender, EventArgs e)
		{
			CreateNewAction();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Cancel();
		}

		private void Cancel()
		{
			if (action != null)
			{
				action.RejectChanges();
			}
		}

		private void CheckChanges()
		{
			if (action == null)
			{
				btnOK.Enabled = false;
			}
			else
			{
				btnOK.Enabled = action.IsValid;
			}
		}

		private void CreateNewAction()
		{
			if (!suppressActionSelectorChange)
			{
				System.Type actionType = actionSelector1.SelectedActionType;
				action = (SVNMonitor.Actions.Action)Activator.CreateInstance(actionType);
				propertyGrid1.SelectedObject = action;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ActionPropertiesDialog));
			lblActionType = new Label();
			propertyGrid1 = new PropertyGrid();
			btnOK = new Button();
			btnCancel = new Button();
			actionSelector1 = new ActionSelector();
			lblSelectActionTypeTitle = new Label();
			lblActionProperties = new Label();
			actionSelector1.BeginInit();
			base.SuspendLayout();
			resources.ApplyResources(lblActionType, "lblActionType");
			lblActionType.Name = "lblActionType";
			resources.ApplyResources(propertyGrid1, "propertyGrid1");
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.SelectedObjectsChanged += propertyGrid1_SelectedObjectsChanged;
			propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Name = "btnOK";
			resources.ApplyResources(btnCancel, "btnCancel");
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Name = "btnCancel";
			btnCancel.Click += btnCancel_Click;
			actionSelector1.ComboStyle = ComboStyle.DropDownList;
			resources.ApplyResources(actionSelector1, "actionSelector1");
			actionSelector1.MaxDropDownItems = 20;
			actionSelector1.Name = "actionSelector1";
			actionSelector1.SelectedIndexChanged += actionSelector1_SelectedIndexChanged;
			resources.ApplyResources(lblSelectActionTypeTitle, "lblSelectActionTypeTitle");
			lblSelectActionTypeTitle.Name = "lblSelectActionTypeTitle";
			resources.ApplyResources(lblActionProperties, "lblActionProperties");
			lblActionProperties.Name = "lblActionProperties";
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.Controls.Add(lblActionProperties);
			base.Controls.Add(lblSelectActionTypeTitle);
			base.Controls.Add(actionSelector1);
			base.Controls.Add(propertyGrid1);
			base.Controls.Add(lblActionType);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCancel);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Name = "ActionPropertiesDialog";
			base.ShowInTaskbar = false;
			actionSelector1.EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			CheckChanges();
		}

		private void propertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
		{
			CheckChanges();
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SVNMonitor.Actions.Action Action
		{
			[DebuggerNonUserCode]
			get { return action; }
			set
			{
				suppressActionSelectorChange = true;
				action = value;
				propertyGrid1.SelectedObject = action;
				action.SetRejectionPoint();
				actionSelector1.SelectedActionType = action.GetType();
				suppressActionSelectorChange = false;
			}
		}
	}
}