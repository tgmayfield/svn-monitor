namespace SVNMonitor.Settings.Validation
{
    using System;

    internal class AdministratorConfigValidatorAttribute : ConfigValidatorAttribute
    {
        public AdministratorConfigValidatorAttribute(object valueIfNonAdmin) : base(typeof(AdministratorConfigValidator), new object[] { valueIfNonAdmin })
        {
        }
    }
}

