using System;
using System.Collections.Generic;
using System.Text;

using SVNMonitor.View.Interfaces;

namespace SVNMonitor
{
	[Serializable]
	public class EventLogEntry : ISearchable
	{
		private static readonly long id = (id + 1L);

		internal EventLogEntry()
		{
			ID = id;
		}

		public IEnumerable<string> GetSearchKeywords()
		{
			List<string> keywords = new List<string>();
			keywords.AddRange(new[]
			{
				DateTime.ToString(), ID.ToString(), Message, Type.ToString()
			});
			if (SourceObject != null)
			{
				keywords.Add(SourceObject.ToString());
			}
			return keywords;
		}

		public string ToErrorString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(ToMessageString());
			sb.AppendLine("=====================================================");
			for (System.Exception ex = Exception; ex != null; ex = ex.InnerException)
			{
				sb.AppendLine(ex.ToString());
				sb.AppendLine("=====================================================");
			}
			return sb.ToString();
		}

		public string ToMessageString()
		{
			return string.Format("{0}: {1}", DateTime, Message);
		}

		public override string ToString()
		{
			if (HasException)
			{
				return ToErrorString();
			}
			return ToMessageString();
		}

		public System.DateTime DateTime { get; set; }

		public System.Exception Exception { get; set; }

		public bool HasException
		{
			get { return (Exception != null); }
		}

		public long ID { get; private set; }

		public string Message { get; set; }

		public object SourceObject { get; set; }

		public EventLogEntryType Type { get; set; }
	}
}