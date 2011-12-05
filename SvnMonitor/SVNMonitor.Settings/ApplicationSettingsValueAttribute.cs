using System;
using SVNMonitor.Settings.DefaultProviders;

namespace SVNMonitor.Settings
{
[AttributeUsage(AttributeTargets.Property)]
internal class ApplicationSettingsValueAttribute : Attribute
{
	public object DefaultValue
	{
		get;
		private set;
	}

	public string Description
	{
		get;
		set;
	}

	public bool IsCDATA
	{
		get;
		set;
	}

	public ApplicationSettingsValueAttribute(Type defaultValueProvider)
	{
		IDefaultValueProvider provider = (IDefaultValueProvider)Activator.CreateInstance(defaultValueProvider);
		this.DefaultValue = provider.GetDefaultValue();
	}

	public ApplicationSettingsValueAttribute(object defaultValue)
	{
		this.DefaultValue = defaultValue;
	}
}
}