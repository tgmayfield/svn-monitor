using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using SVNMonitor.Logging;

namespace SVNMonitor.View.Dialogs
{
	internal partial class ActionPropertiesDialog : BaseDialog
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SVNMonitor.Actions.Action action;
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