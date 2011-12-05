using System;

namespace SVNMonitor.Wizards
{
[Serializable]
internal class WizardCancelledException : Exception
{
	public WizardCancelledException()
	{
	}
}
}