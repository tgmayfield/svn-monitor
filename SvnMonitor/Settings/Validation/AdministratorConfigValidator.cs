namespace SVNMonitor.Settings.Validation
{
    using SVNMonitor.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class AdministratorConfigValidator : IConfigValidator
    {
        public AdministratorConfigValidator(object valueIfNonAdmin)
        {
            this.ValueIfNonAdmin = valueIfNonAdmin;
        }

        public object Validate(object value, out bool isValid)
        {
            isValid = true;
            if (ProcessHelper.IsRunningAsAdministrator())
            {
                return value;
            }
            return this.ValueIfNonAdmin;
        }

        public object ValueIfNonAdmin { get; private set; }
    }
}

