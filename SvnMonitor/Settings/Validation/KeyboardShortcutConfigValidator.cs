namespace SVNMonitor.Settings.Validation
{
    using SVNMonitor.Helpers;
    using System;
    using System.Runtime.InteropServices;

    internal class KeyboardShortcutConfigValidator : IConfigValidator
    {
        public object Validate(object value, out bool isValid)
        {
            string retString = (string) value;
            isValid = false;
            try
            {
                isValid = KeyInfo.IsValid(retString);
            }
            catch
            {
            }
            return retString;
        }
    }
}

