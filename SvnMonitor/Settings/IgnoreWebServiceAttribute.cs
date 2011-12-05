namespace SVNMonitor.Settings
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    internal class IgnoreWebServiceAttribute : Attribute
    {
    }
}

