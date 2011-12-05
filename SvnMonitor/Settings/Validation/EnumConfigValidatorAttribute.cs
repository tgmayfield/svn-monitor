namespace SVNMonitor.Settings.Validation
{
    using System;

    internal class EnumConfigValidatorAttribute : ConfigValidatorAttribute
    {
        public EnumConfigValidatorAttribute(Type enumType) : base(typeof(EnumConfigValidator), new object[] { enumType })
        {
        }
    }
}

