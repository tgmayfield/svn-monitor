using System;

namespace SVNMonitor.Settings.DefaultProviders
{
	internal class DefaultGuidProvider : IDefaultValueProvider
	{
		public object GetDefaultValue()
		{
			return Guid.NewGuid().ToString();
		}
	}
}