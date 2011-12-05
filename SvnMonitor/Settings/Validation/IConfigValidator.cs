namespace SVNMonitor.Settings.Validation
{
    using System;
    using System.Runtime.InteropServices;

    internal interface IConfigValidator
    {
        object Validate(object value, out bool isValid);
    }
}

