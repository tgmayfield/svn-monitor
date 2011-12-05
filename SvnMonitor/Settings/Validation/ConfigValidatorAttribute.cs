using System;

namespace SVNMonitor.Settings.Validation
{
[AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
internal class ConfigValidatorAttribute : Attribute
{
	public IConfigValidator ConfigValidator
	{
		get;
		private set;
	}

	public ConfigValidatorAttribute(Type configValidatorType, object[] args)
	{
		this.ConfigValidator = (IConfigValidator)Activator.CreateInstance(configValidatorType, args);
	}
}
}