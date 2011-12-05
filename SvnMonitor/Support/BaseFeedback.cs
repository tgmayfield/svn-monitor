using System;
using System.Diagnostics;
using System.Xml;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;

namespace SVNMonitor.Support
{
	public abstract class BaseFeedback : BaseSendable
	{
		private readonly XmlDocument xmldoc;

		protected BaseFeedback()
			: this(null)
		{
		}

		protected BaseFeedback(Exception ex)
		{
			xmldoc = new XmlDocument();
			XmlElement documentElement = xmldoc.CreateElement("Feedback");
			xmldoc.AppendChild(documentElement);
			if (ex != null)
			{
				AddException(ex, 0);
			}
			CollectApplicationData();
		}

		public void Add(string key, string value)
		{
			XmlElement element = xmldoc.CreateElement(key);
			value = EscapeCDATA(value);
			value = Environment.NewLine + value;
			XmlCDataSection cdata = xmldoc.CreateCDataSection(value);
			element.AppendChild(cdata);
			xmldoc.DocumentElement.AppendChild(element);
		}

		private void AddException(Exception ex, int index)
		{
			if (ex != null)
			{
				Add("Exception" + index, ex.ToString());
				if (ex.InnerException != null)
				{
					AddException(ex.InnerException, index++);
				}
			}
		}

		private void AddLog()
		{
			string log = Logger.GetLogAsString();
			Add("Log", log);
		}

		private void AddProcessInfo()
		{
			Add("Assemblies", ProcessHelper.GetLoadedAssemblies());
		}

		private void AddUsageInfo()
		{
			Add("Usage", UsageInformationSender.GetUsageInformation());
		}

		private void CollectApplicationData()
		{
			AddUsageInfo();
			AddProcessInfo();
			AddLog();
		}

		protected virtual void EndSend(SendCallback callback, int id)
		{
			if (callback != null)
			{
				SendableResult tempLocal0 = new SendableResult
				{
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
			get { return xmldoc.OuterXml; }
		}
	}
}