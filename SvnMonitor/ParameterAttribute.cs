using System;

namespace SVNMonitor
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class ParameterAttribute : Attribute
	{
		public ParameterAttribute(string argName)
		{
			ArgName = argName;
		}

		public string ArgName { get; private set; }
	}
}