using System;

using SVNMonitor.Helpers;

namespace SVNMonitor.Settings.Validation
{
	internal class AdministratorConfigValidator : IConfigValidator
	{
		public AdministratorConfigValidator(object valueIfNonAdmin)
		{
			ValueIfNonAdmin = valueIfNonAdmin;
		}

		public object Validate(object value, out bool isValid)
		{
			isValid = true;
			if (ProcessHelper.IsRunningAsAdministrator())
			{
				return value;
			}
			return ValueIfNonAdmin;
		}

		public object ValueIfNonAdmin { get; private set; }
	}
}