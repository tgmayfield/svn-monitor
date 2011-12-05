using System;
using Janus.Windows.GridEX;

namespace SVNMonitor.Settings.Validation
{
internal class GridLayoutConfigValidator : IConfigValidator
{
	public GridLayoutConfigValidator()
	{
	}

	public object Validate(object value, out bool isValid)
	{
		string retValue = string.Empty;
		isValid = 0;
		try
		{
			GridEXLayout.FromXMLString((string)value);
			retValue = (string)value;
			isValid = 1;
		}
		catch
		{
		}
		return retValue;
	}
}
}