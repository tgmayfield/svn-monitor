using System;

namespace SVNMonitor.Settings.Validation
{
	internal class EnumConfigValidatorAttribute : ConfigValidatorAttribute
	{
		public EnumConfigValidatorAttribute(Type enumType)
			: base(typeof(EnumConfigValidator), new object[]
			{
				enumType
			})
		{
		}
	}
}