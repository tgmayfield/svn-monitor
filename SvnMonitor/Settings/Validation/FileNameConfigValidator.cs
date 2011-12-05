namespace SVNMonitor.Settings.Validation
{
    using SVNMonitor.Helpers;
    using System;
    using System.Runtime.InteropServices;

    internal class FileNameConfigValidator : IConfigValidator
    {
        public object Validate(object value, out bool isValid)
        {
            string path = EnvironmentHelper.ExpandEnvironmentVariables((string) value);
            isValid = !string.IsNullOrEmpty(path);
            return path;
        }
    }
}

