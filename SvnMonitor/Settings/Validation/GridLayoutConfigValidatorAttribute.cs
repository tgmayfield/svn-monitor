using System;

namespace SVNMonitor.Settings.Validation
{
internal class GridLayoutConfigValidatorAttribute : ConfigValidatorAttribute
{
	public GridLayoutConfigValidatorAttribute() : base(typeof(GridLayoutConfigValidator), new object[0])
	{
	}
}
}