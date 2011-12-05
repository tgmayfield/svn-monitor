namespace SVNMonitor.Support
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Issue
    {
        public override string ToString()
        {
            return string.Format("{0} at {1}", this.ExceptionName, this.StackFrame);
        }

        [XmlAttribute]
        public string ExceptionName { get; set; }

        [XmlAttribute]
        public int ID { get; set; }

        [XmlAttribute]
        public string StackFrame { get; set; }
    }
}

