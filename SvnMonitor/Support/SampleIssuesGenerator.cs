using System;
using System.Diagnostics;

using SVNMonitor.Helpers;

namespace SVNMonitor.Support
{
	public static class SampleIssuesGenerator
	{
		[Conditional("DEBUG")]
		public static void Generate()
		{
			IssuesCollection issues = new IssuesCollection();
			Issue tempLocal0 = new Issue
			{
				ID = 1,
				ExceptionName = "System.Exception",
				StackFrame = "System."
			};
			issues.Add(tempLocal0);
			Issue tempLocal1 = new Issue
			{
				ID = 2,
				ExceptionName = "System.Exception",
				StackFrame = "System."
			};
			issues.Add(tempLocal1);
			SerializationHelper.XmlFileSerialize(issues, @"c:\issues.xml");
		}
	}
}