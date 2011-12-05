using System;
using SVNMonitor.Helpers;

namespace SVNMonitor.Settings.Validation
{
internal class AdministratorConfigValidator : IConfigValidator
{
	public object ValueIfNonAdmin
	{
		get;
		private set;
	}

	public AdministratorConfigValidator(object valueIfNonAdmin)
	{
		this.ValueIfNonAdmin = valueIfNonAdmin;
	}

	public object Validate(object value, out bool isValid)
	{
		isValid = 1;
		if (ProcessHelper.IsRunningAsAdministrator())
		{
			return value;
		}
		return this.ValueIfNonAdmin;
	}
}
}