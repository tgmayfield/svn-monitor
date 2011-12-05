namespace SVNMonitor.Helpers
{
    using Janus.Windows.UI;
    using System;
    using System.Runtime.CompilerServices;

    public static class InheritableBooleanHelper
    {
        public static bool ToBool(this InheritableBoolean value)
        {
            return (value == InheritableBoolean.True);
        }

        public static InheritableBoolean ToInheritableBoolean(this bool value)
        {
            return (value ? InheritableBoolean.True : InheritableBoolean.False);
        }
    }
}

