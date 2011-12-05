namespace SVNMonitor.Wizards
{
    using SVNMonitor.Actions;
    using SVNMonitor.Extensions;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using System;
    using System.Collections.Generic;

    [ResourceProvider("WizardCommitsNotifications")]
    internal class CommitsNotificationsWizard : Wizard
    {
        protected override IEnumerable<Actions.Action> CreateActions(string baseName)
        {
			return new Actions.Action[] { new InfoBalloonTipAction() };
        }

        protected override string GetWizardName(string baseName)
        {
            return Strings.WizardCommitsNotificationsName_FORMAT.FormatWith(new object[] { baseName });
        }
    }
}

