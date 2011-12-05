using System;

namespace SVNMonitor.Resources
{
public class ResourceProviderAttribute : Attribute
{
	public string ResourceName
	{
		get;
		private set;
	}

	public Type ResourceType
	{
		get;
		private set;
	}

	public ResourceProviderAttribute(string resourceName)
	{
	}

	public ResourceProviderAttribute(Type resourceType, string resourceName)
	{
		this.ResourceType = resourceType;
		this.ResourceName = resourceName;
	}
}
}