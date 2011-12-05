using System;
using SVNMonitor.Helpers;

namespace SVNMonitor.Settings.Validation
{
internal class FileNameConfigValidator : IConfigValidator
{
	public FileNameConfigValidator()
	{
	}

	public object Validate(object value, out bool isValid)
	{
		string path = EnvironmentHelper.ExpandEnvironmentVariables((string)value);
		isValid = !string.IsNullOrEmpty(path);
		return path;
	}
}
}