namespace SVNMonitor.Settings.Validation
{
    using System;

    internal class CultureConfigValidatorAttribute : ConfigValidatorAttribute
    {
        public CultureConfigValidatorAttribute() : base(typeof(CultureConfigValidator), new object[0])
        {
        }
    }
}

