namespace SVNMonitor.Wizards
{
    using SVNMonitor.Actions;
    using SVNMonitor.Extensions;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    [ResourceProvider("WizardSoundCommitsNotifications")]
    internal class SoundCommitsNotificationsWizard : Wizard
    {
        protected override IEnumerable<Action> CreateActions(string baseName)
        {
            SoundPlayerAction action = new SoundPlayerAction();
            ActionPropertiesDialog <>g__initLocal0 = new ActionPropertiesDialog {
                Action = action
            };
            ActionPropertiesDialog actionDialog = <>g__initLocal0;
            if (actionDialog.ShowDialog() == DialogResult.Cancel)
            {
                throw new WizardCancelledException();
            }
            return new Action[] { action };
        }

        protected override string GetWizardName(string baseName)
        {
            return Strings.WizardSoundCommitsNotificationsName_FORMAT.FormatWith(new object[] { baseName });
        }
    }
}

