namespace SVNMonitor.View.Dialogs
{
	internal partial class ActionPropertiesDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionPropertiesDialog));
			lblActionType = new System.Windows.Forms.Label();
			propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			btnOK = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			actionSelector1 = new SVNMonitor.View.Controls.ActionSelector();
			lblSelectActionTypeTitle = new System.Windows.Forms.Label();
			lblActionProperties = new System.Windows.Forms.Label();
			actionSelector1.BeginInit();
			base.SuspendLayout();
			resources.ApplyResources(lblActionType, "lblActionType");
			lblActionType.Name = "lblActionType";
			resources.ApplyResources(propertyGrid1, "propertyGrid1");
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.SelectedObjectsChanged += propertyGrid1_SelectedObjectsChanged;
			propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOK.Name = "btnOK";
			resources.ApplyResources(btnCancel, "btnCancel");
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Name = "btnCancel";
			btnCancel.Click += btnCancel_Click;
			actionSelector1.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
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
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.Controls.Add(lblActionProperties);
			base.Controls.Add(lblSelectActionTypeTitle);
			base.Controls.Add(actionSelector1);
			base.Controls.Add(propertyGrid1);
			base.Controls.Add(lblActionType);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "ActionPropertiesDialog";
			base.ShowInTaskbar = false;
			actionSelector1.EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		#endregion
		private SVNMonitor.View.Controls.ActionSelector actionSelector1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblActionProperties;
		private System.Windows.Forms.Label lblActionType;
		private System.Windows.Forms.Label lblSelectActionTypeTitle;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
	}
}