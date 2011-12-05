using System.Xml.Serialization;
using System;
using System.Collections.Generic;

namespace SVNMonitor.Support
{
[XmlRoot("KnownIssues")]
[Serializable]
public class IssuesCollection
{
	public List<Issue> List
	{
		get;
		set;
	}

	public IssuesCollection()
	{
		this.List = new List<Issue>();
	}

	public void Add(Issue issue)
	{
		this.List.Add(issue);
	}

	public override string ToString()
	{
		return string.Concat("Count: ", this.List.Count);
	}
}
}