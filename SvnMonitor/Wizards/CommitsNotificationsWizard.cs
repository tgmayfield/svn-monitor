using SVNMonitor.Resources;
using System.Collections.Generic;
using System;
using SVNMonitor.Actions;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Wizards
{
[ResourceProvider("WizardCommitsNotifications")]
internal class CommitsNotificationsWizard : Wizard
{
	public CommitsNotificationsWizard()
	{
	}

	protected override IEnumerable<Action> CreateActions(string baseName)
	{
		Action[] infoBalloonTipAction = new Action[1];
		infoBalloonTipAction[0] = new InfoBalloonTipAction();
		return infoBalloonTipAction;
	}

	protected override string GetWizardName(string baseName)
	{
		object[] objArray;
		return Strings.WizardCommitsNotificationsName_FORMAT.FormatWith(new object[] { baseName });
	}
}
}