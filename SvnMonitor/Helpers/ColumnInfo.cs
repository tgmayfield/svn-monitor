using System;

namespace SVNMonitor.Helpers
{
	[Serializable]
	internal class ColumnInfo
	{
		public string ColumnKey { get; set; }

		public string TableKey { get; set; }
	}
}