namespace SVNMonitor.Support
{
    using SVNMonitor.Helpers;
    using System;
    using System.Diagnostics;

    public static class SampleIssuesGenerator
    {
        [Conditional("DEBUG")]
        public static void Generate()
        {
            IssuesCollection issues = new IssuesCollection();
            Issue <>g__initLocal0 = new Issue {
                ID = 1,
                ExceptionName = "System.Exception",
                StackFrame = "System."
            };
            issues.Add(<>g__initLocal0);
            Issue <>g__initLocal1 = new Issue {
                ID = 2,
                ExceptionName = "System.Exception",
                StackFrame = "System."
            };
            issues.Add(<>g__initLocal1);
            SerializationHelper.XmlFileSerialize<IssuesCollection>(issues, @"c:\issues.xml");
        }
    }
}

