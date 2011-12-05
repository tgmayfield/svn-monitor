using System.Xml;
using System;
using SVNMonitor.Logging;
using SVNMonitor.Helpers;

namespace SVNMonitor.Support
{
public abstract class BaseFeedback : BaseSendable
{
	private XmlDocument xmldoc;

	public string Xml
	{
		get
		{
			return this.xmldoc.OuterXml;
		}
	}

	protected BaseFeedback()
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
		value = BaseFeedback.EscapeCDATA(value);
		value = string.Concat(Environment.NewLine, value);
		XmlCDataSection cdata = this.xmldoc.CreateCDataSection(value);
		element.AppendChild(cdata);
		this.xmldoc.DocumentElement.AppendChild(element);
	}

	private void AddException(Exception ex, int index)
	{
		if (ex != null)
		{
			this.Add(string.Concat("Exception", index), ex.ToString());
			if (ex.InnerException != null)
			{
				ex.InnerException.AddException(index, index++);
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
			SendableResult sendableResult = new SendableResult();
			sendableResult.Id = id;
			sendableResult.Proxy = this.Proxy;
			SendableResult result = sendableResult;
			callback(result);
		}
	}

	private static string EscapeCDATA(string text)
	{
		return text.Replace("]]>", "] ]>");
	}
}
}