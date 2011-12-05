using SVNMonitor.Resources;
using System.Collections.Generic;
using System;
using SVNMonitor.Actions;
using SVNMonitor.View.Dialogs;
using System.Windows.Forms;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Wizards
{
[ResourceProvider("WizardSoundCommitsNotifications")]
internal class SoundCommitsNotificationsWizard : Wizard
{
	public SoundCommitsNotificationsWizard()
	{
	}

	protected override IEnumerable<Action> CreateActions(string baseName)
	{
		SoundPlayerAction action = new SoundPlayerAction();
		ActionPropertiesDialog actionPropertiesDialog = new ActionPropertiesDialog();
		actionPropertiesDialog.Action = action;
		ActionPropertiesDialog actionDialog = actionPropertiesDialog;
		DialogResult result = actionDialog.ShowDialog();
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
		return Strings.WizardSoundCommitsNotificationsName_FORMAT.FormatWith(new object[] { baseName });
	}
}
}