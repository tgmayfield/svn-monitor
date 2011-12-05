using System;

using Janus.Windows.GridEX;

namespace SVNMonitor.Settings.Validation
{
	internal class GridLayoutConfigValidator : IConfigValidator
	{
		public object Validate(object value, out bool isValid)
		{
			string retValue = string.Empty;
			isValid = false;
			try
			{
				GridEXLayout.FromXMLString((string)value);
				retValue = (string)value;
				isValid = true;
			}
			catch
			{
			}
			return retValue;
		}
	}
}