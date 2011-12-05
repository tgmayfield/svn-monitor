using System;

namespace SVNMonitor.Settings.Validation
{
	internal class NumberConfigValidatorAttribute : ConfigValidatorAttribute
	{
		public NumberConfigValidatorAttribute(ValidationOperator op, int compareTo)
			: base(typeof(NumberConfigValidator), new object[]
			{
				op, compareTo
			})
		{
		}
	}
}