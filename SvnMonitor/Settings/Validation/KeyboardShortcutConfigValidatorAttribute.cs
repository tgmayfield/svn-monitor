namespace SVNMonitor.Settings.Validation
{
    using System;

    internal class KeyboardShortcutConfigValidatorAttribute : ConfigValidatorAttribute
    {
        public KeyboardShortcutConfigValidatorAttribute() : base(typeof(KeyboardShortcutConfigValidator), new object[0])
        {
        }
    }
}

