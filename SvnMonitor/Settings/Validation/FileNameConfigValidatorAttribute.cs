using System;

namespace SVNMonitor.Settings.Validation
{
internal class FileNameConfigValidatorAttribute : ConfigValidatorAttribute
{
	public FileNameConfigValidatorAttribute() : base(typeof(FileNameConfigValidator), new object[0])
	{
	}
}
}