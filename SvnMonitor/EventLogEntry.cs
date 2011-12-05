namespace SVNMonitor
{
    using SVNMonitor.View.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    [Serializable]
    public class EventLogEntry : ISearchable
    {
        private static long id = (id + 1L);

        internal EventLogEntry()
        {
            this.ID = id;
        }

        public IEnumerable<string> GetSearchKeywords()
        {
            List<string> keywords = new List<string>();
            keywords.AddRange(new string[] { this.DateTime.ToString(), this.ID.ToString(), this.Message, this.Type.ToString() });
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
            for (System.Exception ex = this.Exception; ex != null; ex = ex.InnerException)
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

        public System.DateTime DateTime { get; set; }

        public System.Exception Exception { get; set; }

        public bool HasException
        {
            get
            {
                return (this.Exception != null);
            }
        }

        public long ID { get; private set; }

        public string Message { get; set; }

        public object SourceObject { get; set; }

        public EventLogEntryType Type { get; set; }
    }
}

