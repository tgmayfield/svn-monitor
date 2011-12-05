using System;
using System.Collections.Generic;

using SVNMonitor.Helpers;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Wizards
{
	[ResourceProvider("WizardCustom"), Custom]
	internal class CustomWizard : Wizard
	{
		protected override IEnumerable<Actions.Action> CreateActions(string baseName)
		{
			return null;
		}

		protected override string GetWizardName(string baseName)
		{
			return Strings.WizardCustomName;
		}
	}
}