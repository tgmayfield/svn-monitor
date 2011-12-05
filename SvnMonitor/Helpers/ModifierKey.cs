namespace SVNMonitor.Helpers
{
    using System;

    [Flags]
    public enum ModifierKey : uint
    {
        Alt = 1,
        Control = 2,
        None = 0,
        Shift = 4,
        Win = 8
    }
}

