namespace SVNMonitor.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    internal class ColumnInfo
    {
        public string ColumnKey { get; set; }

        public string TableKey { get; set; }
    }
}

