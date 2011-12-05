namespace SVNMonitor.Settings.Validation
{
    using Janus.Windows.GridEX;
    using System;
    using System.Runtime.InteropServices;

    internal class GridLayoutConfigValidator : IConfigValidator
    {
        public object Validate(object value, out bool isValid)
        {
            string retValue = string.Empty;
            isValid = false;
            try
            {
                GridEXLayout.FromXMLString((string) value);
                retValue = (string) value;
                isValid = true;
            }
            catch
            {
            }
            return retValue;
        }
    }
}

