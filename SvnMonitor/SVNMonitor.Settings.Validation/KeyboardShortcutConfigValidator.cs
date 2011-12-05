using System;
using SVNMonitor.Helpers;

namespace SVNMonitor.Settings.Validation
{
internal class KeyboardShortcutConfigValidator : IConfigValidator
{
	public KeyboardShortcutConfigValidator()
	{
	}

	public object Validate(object value, out bool isValid)
	{
		string retString = (string)value;
		isValid = 0;
		try
		{
			isValid = KeyInfo.IsValid(retString);
		}
		catch
		{
		}
		return retString;
	}
}
}