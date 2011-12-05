using System;

namespace SVNMonitor.Settings.Validation
{
	internal interface IConfigValidator
	{
		object Validate(object value, out bool isValid);
	}
}