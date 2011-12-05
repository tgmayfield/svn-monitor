using System;

namespace SVNMonitor.Settings.DefaultProviders
{
	internal interface IDefaultValueProvider
	{
		object GetDefaultValue();
	}
}