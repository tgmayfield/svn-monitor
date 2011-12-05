using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SVNMonitor.Support
{
	[Serializable, XmlRoot("KnownIssues")]
	public class IssuesCollection
	{
		public IssuesCollection()
		{
			List = new List<Issue>();
		}

		public void Add(Issue issue)
		{
			List.Add(issue);
		}

		public override string ToString()
		{
			return ("Count: " + List.Count);
		}

		public List<Issue> List { get; set; }
	}
}