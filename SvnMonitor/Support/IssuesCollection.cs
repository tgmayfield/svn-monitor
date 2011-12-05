namespace SVNMonitor.Support
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("KnownIssues")]
    public class IssuesCollection
    {
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
            return ("Count: " + this.List.Count);
        }

        public List<Issue> List { get; set; }
    }
}

