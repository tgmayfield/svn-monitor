using System;

using SVNMonitor.Settings.DefaultProviders;

namespace SVNMonitor.Settings
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class ApplicationSettingsValueAttribute : Attribute
	{
		public ApplicationSettingsValueAttribute(object defaultValue)
		{
			DefaultValue = defaultValue;
		}

		public ApplicationSettingsValueAttribute(Type defaultValueProvider)
		{
			DefaultValue = ((IDefaultValueProvider)Activator.CreateInstance(defaultValueProvider)).GetDefaultValue();
		}

		public object DefaultValue { get; private set; }

		public string Description { get; set; }

		public bool IsCDATA { get; set; }
	}
}