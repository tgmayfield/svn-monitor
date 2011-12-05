using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SVNMonitor.Helpers
{
	[Serializable]
	internal class ConditionSerializationContext
	{
		private readonly List<ColumnInfo> columnKeys = new List<ColumnInfo>();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string conditionXml;
		private int currentColumnIndex;

		public ConditionSerializationContext(string conditionXml)
		{
			this.conditionXml = conditionXml;
		}

		public void AddColumnKey(ColumnInfo key)
		{
			columnKeys.Add(key);
		}

		public ColumnInfo NextColumnKey()
		{
			ColumnInfo key = null;
			if (currentColumnIndex < columnKeys.Count)
			{
				key = columnKeys[currentColumnIndex];
				currentColumnIndex++;
			}
			return key;
		}

		public void Reset()
		{
			currentColumnIndex = 0;
		}

		public override string ToString()
		{
			return conditionXml;
		}

		public ReadOnlyCollection<ColumnInfo> ColumnKeys
		{
			[DebuggerNonUserCode]
			get { return columnKeys.AsReadOnly(); }
		}

		public string ConditionXml
		{
			[DebuggerNonUserCode]
			get { return conditionXml; }
		}
	}
}