namespace SVNMonitor
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class ParameterAttribute : Attribute
    {
        public ParameterAttribute(string argName)
        {
            this.ArgName = argName;
        }

        public string ArgName { get; private set; }
    }
}

