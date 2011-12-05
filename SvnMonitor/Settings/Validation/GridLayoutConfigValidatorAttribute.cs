namespace SVNMonitor.Settings.Validation
{
    using System;

    internal class GridLayoutConfigValidatorAttribute : ConfigValidatorAttribute
    {
        public GridLayoutConfigValidatorAttribute() : base(typeof(GridLayoutConfigValidator), new object[0])
        {
        }
    }
}

