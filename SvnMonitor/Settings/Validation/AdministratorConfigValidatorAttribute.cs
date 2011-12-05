using System;

namespace SVNMonitor.Settings.Validation
{
	internal class AdministratorConfigValidatorAttribute : ConfigValidatorAttribute
	{
		public AdministratorConfigValidatorAttribute(object valueIfNonAdmin)
			: base(typeof(AdministratorConfigValidator), new[]
			{
				valueIfNonAdmin
			})
		{
		}
	}
}