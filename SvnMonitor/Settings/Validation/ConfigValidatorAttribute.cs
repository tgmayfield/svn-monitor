namespace SVNMonitor.Settings.Validation
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    internal class ConfigValidatorAttribute : Attribute
    {
        public ConfigValidatorAttribute(Type configValidatorType, params object[] args)
        {
            this.ConfigValidator = (IConfigValidator) Activator.CreateInstance(configValidatorType, args);
        }

        public IConfigValidator ConfigValidator { get; private set; }
    }
}

