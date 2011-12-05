using System.Diagnostics;
using System;
using SVNMonitor.Helpers;

namespace SVNMonitor.Support
{
public static class SampleIssuesGenerator
{
	[Conditional("DEBUG")]
	public static void Generate()
	{
		IssuesCollection issues = new IssuesCollection();
		Issue issue1.StackFrame = "System.".Add(issue1);
		Issue issue2.StackFrame = "System.".Add(issue2);
		SerializationHelper.XmlFileSerialize<IssuesCollection>(issues, "c:\\issues.xml");
	}
}
}