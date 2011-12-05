using SVNMonitor.Resources;
using System.Collections.Generic;
using System;
using SVNMonitor.Actions;
using SVNMonitor.Resources.Text;
using SVNMonitor.View.Dialogs;
using System.Windows.Forms;

namespace SVNMonitor.Wizards
{
[ResourceProvider("WizardCodeReviewByMail")]
internal class CodeReviewMailerWizard : Wizard
{
	public CodeReviewMailerWizard()
	{
	}

	protected override IEnumerable<Action> CreateActions(string baseName)
	{
		object[] objArray;
		MailSenderAction action = new MailSenderAction();
		action.Subject = Strings.WizardCodeReviewByMailActionSubject_FORMAT.FormatWith(new object[] { baseName });
		ActionPropertiesDialog actionPropertiesDialog = new ActionPropertiesDialog();
		actionPropertiesDialog.Action = action;
		ActionPropertiesDialog dialog = actionPropertiesDialog;
		DialogResult result = dialog.ShowDialog();
		if (result == DialogResult.Cancel)
		{
			throw new WizardCancelledException();
		}
		Action[] actionArray = new Action[1];
		actionArray[0] = action;
		return actionArray;
	}

	protected override string GetWizardName(string baseName)
	{
		object[] objArray;
		return Strings.WizardCodeReviewByMailName_FORMAT.FormatWith(new object[] { baseName });
	}
}
}