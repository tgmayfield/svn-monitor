namespace SVNMonitor.Settings.Validation
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class EnumConfigValidator : IConfigValidator
    {
        public EnumConfigValidator(Type enumType)
        {
            this.EnumType = enumType;
        }

        public object Validate(object value, out bool isValid)
        {
            string retValue = (string) value;
            Enum.Parse(this.EnumType, retValue);
            isValid = true;
            return retValue;
        }

        internal Type EnumType { get; private set; }
    }
}

