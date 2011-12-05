using System;

using SVNMonitor.Helpers;

namespace SVNMonitor.Settings.Validation
{
	internal class KeyboardShortcutConfigValidator : IConfigValidator
	{
		public object Validate(object value, out bool isValid)
		{
			string retString = (string)value;
			isValid = false;
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