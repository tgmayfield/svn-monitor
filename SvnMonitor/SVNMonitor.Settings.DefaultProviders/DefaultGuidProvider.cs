using System;

namespace SVNMonitor.Settings.DefaultProviders
{
internal class DefaultGuidProvider : IDefaultValueProvider
{
	public DefaultGuidProvider()
	{
	}

	public object GetDefaultValue()
	{
		Guid guid = Guid.NewGuid();
		return guid.ToString();
	}
}
}