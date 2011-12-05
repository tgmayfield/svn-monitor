using SVNMonitor.Resources;
using SVNMonitor.Helpers;
using System.Collections.Generic;
using System;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Wizards
{
[ResourceProvider("WizardCustom")]
[Custom]
internal class CustomWizard : Wizard
{
	public CustomWizard()
	{
	}

	protected override IEnumerable<Action> CreateActions(string baseName)
	{
		return null;
	}

	protected override string GetWizardName(string baseName)
	{
		return Strings.WizardCustomName;
	}
}
}