using System;
using System.Globalization;

namespace SVNMonitor.Settings.Validation
{
internal class CultureConfigValidator : IConfigValidator
{
	public CultureConfigValidator()
	{
	}

	public object Validate(object value, out bool isValid)
	{
		string retCulture = (string)value;
		isValid = 0;
		try
		{
			CultureInfo culture = new CultureInfo(retCulture);
			isValid = culture != null;
		}
		catch
		{
		}
		return retCulture;
	}
}
}