using System;
using SVNMonitor.View.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace SVNMonitor
{
[Serializable]
public class EventLogEntry : ISearchable
{
	private static long id;

	public DateTime DateTime
	{
		get;
		set;
	}

	public Exception Exception
	{
		get;
		set;
	}

	public bool HasException
	{
		get
		{
			return this.Exception != null;
		}
	}

	public long ID
	{
		get;
		private set;
	}

	public string Message
	{
		get;
		set;
	}

	public object SourceObject
	{
		get;
		set;
	}

	public EventLogEntryType Type
	{
		get;
		set;
	}

	internal EventLogEntry()
	{
		EventLogEntry.id = EventLogEntry.id + (long)1;
		this.ID = EventLogEntry.id;
	}

	public IEnumerable<string> GetSearchKeywords()
	{
		DateTime dateTime;
		long num;
		List<string> keywords = new List<string>();
		string[] str[3] = this.Type.ToString().AddRange(str);
		if (this.SourceObject != null)
		{
			keywords.Add(this.SourceObject.ToString());
		}
		return keywords;
	}

	public string ToErrorString()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine(this.ToMessageString());
		sb.AppendLine("=====================================================");
		for (Exception ex = this.Exception; ex; ex = ex.InnerException)
		{
			sb.AppendLine(ex.ToString());
			sb.AppendLine("=====================================================");
		}
		return sb.ToString();
	}

	public string ToMessageString()
	{
		return string.Format("{0}: {1}", this.DateTime, this.Message);
	}

	public override string ToString()
	{
		if (this.HasException)
		{
			return this.ToErrorString();
		}
		return this.ToMessageString();
	}
}
}