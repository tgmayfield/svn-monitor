using System;
using System.Xml.Serialization;

namespace SVNMonitor.Support
{
	[Serializable]
	public class Issue
	{
		public override string ToString()
		{
			return string.Format("{0} at {1}", ExceptionName, StackFrame);
		}

		[XmlAttribute]
		public string ExceptionName { get; set; }

		[XmlAttribute]
		public int ID { get; set; }

		[XmlAttribute]
		public string StackFrame { get; set; }
	}
}