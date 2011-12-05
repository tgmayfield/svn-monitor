namespace SVNMonitor.View.Dialogs
{
    using Janus.Windows.EditControls;
    using SVNMonitor.Actions;
    using SVNMonitor.Logging;
    using SVNMonitor.View.Controls;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Forms;

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
            this.InitializeComponent();
        }

        private void actionSelector1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CreateNewAction();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.Cancel();
        }

        private void Cancel()
        {
            if (this.action != null)
            {
                this.action.RejectChanges();
            }
        }

        private void CheckChanges()
        {
            if (this.action == null)
            {
                this.btnOK.Enabled = false;
            }
            else
            {
                this.btnOK.Enabled = this.action.IsValid;
            }
        }

        private void CreateNewAction()
        {
            if (!this.suppressActionSelectorChange)
            {
                System.Type actionType = this.actionSelector1.SelectedActionType;
                this.action = (SVNMonitor.Actions.Action) Activator.CreateInstance(actionType);
                this.propertyGrid1.SelectedObject = this.action;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ActionPropertiesDialog));
            this.lblActionType = new Label();
            this.propertyGrid1 = new PropertyGrid();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.actionSelector1 = new ActionSelector();
            this.lblSelectActionTypeTitle = new Label();
            this.lblActionProperties = new Label();
            this.actionSelector1.BeginInit();
            base.SuspendLayout();
            resources.ApplyResources(this.lblActionType, "lblActionType");
            this.lblActionType.Name = "lblActionType";
            resources.ApplyResources(this.propertyGrid1, "propertyGrid1");
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObjectsChanged += new EventHandler(this.propertyGrid1_SelectedObjectsChanged);
            this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Name = "btnOK";
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.actionSelector1.ComboStyle = ComboStyle.DropDownList;
            resources.ApplyResources(this.actionSelector1, "actionSelector1");
            this.actionSelector1.MaxDropDownItems = 20;
            this.actionSelector1.Name = "actionSelector1";
            this.actionSelector1.SelectedIndexChanged += new EventHandler(this.actionSelector1_SelectedIndexChanged);
            resources.ApplyResources(this.lblSelectActionTypeTitle, "lblSelectActionTypeTitle");
            this.lblSelectActionTypeTitle.Name = "lblSelectActionTypeTitle";
            resources.ApplyResources(this.lblActionProperties, "lblActionProperties");
            this.lblActionProperties.Name = "lblActionProperties";
            base.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.Controls.Add(this.lblActionProperties);
            base.Controls.Add(this.lblSelectActionTypeTitle);
            base.Controls.Add(this.actionSelector1);
            base.Controls.Add(this.propertyGrid1);
            base.Controls.Add(this.lblActionType);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Name = "ActionPropertiesDialog";
            base.ShowInTaskbar = false;
            this.actionSelector1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.CheckChanges();
        }

        private void propertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            this.CheckChanges();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SVNMonitor.Actions.Action Action
        {
            [DebuggerNonUserCode]
            get
            {
                return this.action;
            }
            set
            {
                this.suppressActionSelectorChange = true;
                this.action = value;
                this.propertyGrid1.SelectedObject = this.action;
                this.action.SetRejectionPoint();
                this.actionSelector1.SelectedActionType = this.action.GetType();
                this.suppressActionSelectorChange = false;
            }
        }
    }
}

