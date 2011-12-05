namespace SVNMonitor.Settings.Validation
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    internal class CultureConfigValidator : IConfigValidator
    {
        public object Validate(object value, out bool isValid)
        {
            string retCulture = (string) value;
            isValid = false;
            try
            {
                CultureInfo culture = new CultureInfo(retCulture);
                isValid = culture != null;
            }
            catch
            {
            }
            return retCulture;
        }
    }
}

