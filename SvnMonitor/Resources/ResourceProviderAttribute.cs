using System;

using SVNMonitor.Resources.Text;

namespace SVNMonitor.Resources
{
	public class ResourceProviderAttribute : Attribute
	{
		public ResourceProviderAttribute(string resourceName)
			: this(typeof(Strings), resourceName)
		{
		}

		public ResourceProviderAttribute(Type resourceType, string resourceName)
		{
			ResourceType = resourceType;
			ResourceName = resourceName;
		}

		public string ResourceName { get; private set; }

		public Type ResourceType { get; private set; }
	}
}