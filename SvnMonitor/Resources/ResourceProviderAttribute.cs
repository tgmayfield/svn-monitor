namespace SVNMonitor.Resources
{
    using System;
    using System.Runtime.CompilerServices;

    public class ResourceProviderAttribute : Attribute
    {
        public ResourceProviderAttribute(string resourceName) : this(typeof(Strings), resourceName)
        {
        }

        public ResourceProviderAttribute(Type resourceType, string resourceName)
        {
            this.ResourceType = resourceType;
            this.ResourceName = resourceName;
        }

        public string ResourceName { get; private set; }

        public Type ResourceType { get; private set; }
    }
}

