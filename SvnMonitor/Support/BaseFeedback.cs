namespace SVNMonitor.Support
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using System;
    using System.Diagnostics;
    using System.Xml;

    public abstract class BaseFeedback : BaseSendable
    {
        private XmlDocument xmldoc;

        protected BaseFeedback() : this(null)
        {
        }

        protected BaseFeedback(Exception ex)
        {
            this.xmldoc = new XmlDocument();
            XmlElement documentElement = this.xmldoc.CreateElement("Feedback");
            this.xmldoc.AppendChild(documentElement);
            if (ex != null)
            {
                this.AddException(ex, 0);
            }
            this.CollectApplicationData();
        }

        public void Add(string key, string value)
        {
            XmlElement element = this.xmldoc.CreateElement(key);
            value = EscapeCDATA(value);
            value = Environment.NewLine + value;
            XmlCDataSection cdata = this.xmldoc.CreateCDataSection(value);
            element.AppendChild(cdata);
            this.xmldoc.DocumentElement.AppendChild(element);
        }

        private void AddException(Exception ex, int index)
        {
            if (ex != null)
            {
                this.Add("Exception" + index, ex.ToString());
                if (ex.InnerException != null)
                {
                    this.AddException(ex.InnerException, index++);
                }
            }
        }

        private void AddLog()
        {
            string log = Logger.GetLogAsString();
            this.Add("Log", log);
        }

        private void AddProcessInfo()
        {
            this.Add("Assemblies", ProcessHelper.GetLoadedAssemblies());
        }

        private void AddUsageInfo()
        {
            this.Add("Usage", UsageInformationSender.GetUsageInformation());
        }

        private void CollectApplicationData()
        {
            this.AddUsageInfo();
            this.AddProcessInfo();
            this.AddLog();
        }

        protected virtual void EndSend(SendCallback callback, int id)
        {
            if (callback != null)
            {
                SendableResult tempLocal0 = new SendableResult {
                    Id = id,
                    Proxy = base.Proxy
                };
                SendableResult result = tempLocal0;
                callback(result);
            }
        }

        private static string EscapeCDATA(string text)
        {
            return text.Replace("]]>", "] ]>");
        }

        public string Xml
        {
            [DebuggerNonUserCode]
            get
            {
                return this.xmldoc.OuterXml;
            }
        }
    }
}

