namespace SVNMonitor.Settings
{
    using SVNMonitor.Settings.DefaultProviders;
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property)]
    internal class ApplicationSettingsValueAttribute : Attribute
    {
        public ApplicationSettingsValueAttribute(object defaultValue)
        {
            this.DefaultValue = defaultValue;
        }

        public ApplicationSettingsValueAttribute(Type defaultValueProvider)
        {
            this.DefaultValue = ((IDefaultValueProvider) Activator.CreateInstance(defaultValueProvider)).GetDefaultValue();
        }

        public object DefaultValue { get; private set; }

        public string Description { get; set; }

        public bool IsCDATA { get; set; }
    }
}

