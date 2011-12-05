using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace SVNMonitor.Helpers
{
[Serializable]
internal class ConditionSerializationContext
{
	private readonly List<ColumnInfo> columnKeys;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private string conditionXml;

	private int currentColumnIndex;

	public ReadOnlyCollection<ColumnInfo> ColumnKeys
	{
		get
		{
			return this.columnKeys.AsReadOnly();
		}
	}

	public string ConditionXml
	{
		get
		{
			return this.conditionXml;
		}
	}

	public ConditionSerializationContext(string conditionXml)
	{
		this.columnKeys = new List<ColumnInfo>();
		base();
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
			this.currentColumnIndex = this.currentColumnIndex + 1;
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
}
}