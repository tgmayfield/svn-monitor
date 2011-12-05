using System;
using System.Globalization;

namespace SVNMonitor.Settings.Validation
{
	internal class CultureConfigValidator : IConfigValidator
	{
		public object Validate(object value, out bool isValid)
		{
			string retCulture = (string)value;
			isValid = false;
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