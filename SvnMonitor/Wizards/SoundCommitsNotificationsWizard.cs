using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SVNMonitor.Actions;
using SVNMonitor.Extensions;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.View.Dialogs;

namespace SVNMonitor.Wizards
{
	[ResourceProvider("WizardSoundCommitsNotifications")]
	internal class SoundCommitsNotificationsWizard : Wizard
	{
		protected override IEnumerable<Actions.Action> CreateActions(string baseName)
		{
			SoundPlayerAction action = new SoundPlayerAction();
			ActionPropertiesDialog tempLocal0 = new ActionPropertiesDialog
			{
				Action = action
			};
			ActionPropertiesDialog actionDialog = tempLocal0;
			if (actionDialog.ShowDialog() == DialogResult.Cancel)
			{
				throw new WizardCancelledException();
			}
			return new Actions.Action[]
			{
				action
			};
		}

		protected override string GetWizardName(string baseName)
		{
			return Strings.WizardSoundCommitsNotificationsName_FORMAT.FormatWith(new object[]
			{
				baseName
			});
		}
	}
}