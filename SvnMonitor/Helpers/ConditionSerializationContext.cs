namespace SVNMonitor.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    [Serializable]
    internal class ConditionSerializationContext
    {
        private readonly List<ColumnInfo> columnKeys = new List<ColumnInfo>();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string conditionXml;
        private int currentColumnIndex;

        public ConditionSerializationContext(string conditionXml)
        {
            this.conditionXml = conditionXml;
        }

        public void AddColumnKey(ColumnInfo key)
        {
            this.columnKeys.Add(key);
        }

        public ColumnInfo NextColumnKey()
        {
            ColumnInfo key = null;
            if (this.currentColumnIndex < this.columnKeys.Count)
            {
                key = this.columnKeys[this.currentColumnIndex];
                this.currentColumnIndex++;
            }
            return key;
        }

        public void Reset()
        {
            this.currentColumnIndex = 0;
        }

        public override string ToString()
        {
            return this.conditionXml;
        }

        public ReadOnlyCollection<ColumnInfo> ColumnKeys
        {
            [DebuggerNonUserCode]
            get
            {
                return this.columnKeys.AsReadOnly();
            }
        }

        public string ConditionXml
        {
            [DebuggerNonUserCode]
            get
            {
                return this.conditionXml;
            }
        }
    }
}

