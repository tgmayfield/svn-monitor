using System;

namespace SVNMonitor.Settings.Validation
{
internal class CultureConfigValidatorAttribute : ConfigValidatorAttribute
{
	public CultureConfigValidatorAttribute() : base(typeof(CultureConfigValidator), new object[0])
	{
	}
}
}