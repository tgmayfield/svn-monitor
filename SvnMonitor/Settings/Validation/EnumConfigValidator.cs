using System;

namespace SVNMonitor.Settings.Validation
{
internal class EnumConfigValidator : IConfigValidator
{
	internal Type EnumType
	{
		get;
		private set;
	}

	public EnumConfigValidator(Type enumType)
	{
		this.EnumType = enumType;
	}

	public object Validate(object value, out bool isValid)
	{
		string retValue = (string)value;
		Enum.Parse(this.EnumType, retValue);
		isValid = 1;
		return retValue;
	}
}
}