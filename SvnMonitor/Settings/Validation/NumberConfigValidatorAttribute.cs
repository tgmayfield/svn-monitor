namespace SVNMonitor.Settings.Validation
{
    using System;

    internal class NumberConfigValidatorAttribute : ConfigValidatorAttribute
    {
        public NumberConfigValidatorAttribute(ValidationOperator op, int compareTo) : base(typeof(NumberConfigValidator), new object[] { op, compareTo })
        {
        }
    }
}

