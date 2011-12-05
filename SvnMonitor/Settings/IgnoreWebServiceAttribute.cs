using System;

namespace SVNMonitor.Settings
{
[AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
internal class IgnoreWebServiceAttribute : Attribute
{
	public IgnoreWebServiceAttribute()
	{
	}
}
}