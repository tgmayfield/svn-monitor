using System;

namespace SVNMonitor.Settings
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class KeyboardSettingAttribute : Attribute
	{
	}
}