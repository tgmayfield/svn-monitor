using System;

namespace SVNMonitor.Settings.Validation
{
	internal class EnumConfigValidator : IConfigValidator
	{
		public EnumConfigValidator(Type enumType)
		{
			EnumType = enumType;
		}

		public object Validate(object value, out bool isValid)
		{
			string retValue = (string)value;
			Enum.Parse(EnumType, retValue);
			isValid = true;
			return retValue;
		}

		internal Type EnumType { get; private set; }
	}
}