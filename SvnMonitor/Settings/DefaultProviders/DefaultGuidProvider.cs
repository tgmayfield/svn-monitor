namespace SVNMonitor.Settings.DefaultProviders
{
    using System;

    internal class DefaultGuidProvider : IDefaultValueProvider
    {
        public object GetDefaultValue()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

