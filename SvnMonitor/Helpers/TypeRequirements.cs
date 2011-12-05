namespace SVNMonitor.Helpers
{
    using System;

    [Flags]
    internal enum TypeRequirements
    {
        None,
        Serializable,
        NonCustom
    }
}

