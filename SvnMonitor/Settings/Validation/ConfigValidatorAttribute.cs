using System;

namespace SVNMonitor.Settings.Validation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	internal class ConfigValidatorAttribute : Attribute
	{
		public ConfigValidatorAttribute(Type configValidatorType, params object[] args)
		{
			ConfigValidator = (IConfigValidator)Activator.CreateInstance(configValidatorType, args);
		}

		public IConfigValidator ConfigValidator { get; private set; }
	}
}