using System;

namespace SVNMonitor
{
[AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
public class ParameterAttribute : Attribute
{
	public string ArgName
	{
		get;
		private set;
	}

	public ParameterAttribute(string argName)
	{
		this.ArgName = argName;
	}
}
}