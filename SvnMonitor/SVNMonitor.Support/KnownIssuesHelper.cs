using System;
using System.IO;
using System.Reflection;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Web;
using SVNMonitor.Settings;

namespace SVNMonitor.Support
{
internal class KnownIssuesHelper
{
	private const int NotKnownIssue = -1;

	public KnownIssuesHelper()
	{
	}

	private static int GetEmbeddedKnownIssue(Exception ex)
	{
		Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SVNMonitor.Support.KnownIssues.xml");
		using (stream)
		{
			using (StreamReader reader = new StreamReader(stream))
			{
				string xml = reader.ReadToEnd();
				int id = KnownIssuesHelper.GetKnownIssueFromXml(ex, xml);
				return id;
			}
		}
	}

	private static int GetKnownIssueFromXml(Exception exception, string xml)
	{
		if (string.IsNullOrEmpty(xml))
		{
			return -1;
		}
		try
		{
			string exceptionFullName = exception.GetType().FullName;
			string exceptionStackTrace = exception.StackTrace;
			IssuesCollection issues = SerializationHelper.XmlDeserialize<IssuesCollection>(xml);
			foreach (Issue issue in issues.List)
			{
				if (exceptionStackTrace.Contains(issue.StackFrame) && (string.IsNullOrEmpty(issue.ExceptionName) || exceptionFullName.Equals(issue.ExceptionName, StringComparison.CurrentCultureIgnoreCase)))
				{
					Logger.Log.DebugFormat(string.Concat("Known issue found: ", issue.ID), new object[0]);
					return issue.ID;
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Log.DebugFormat("Error checking for known issue: {0}", ex.ToString());
		}
		return -1;
	}

	private static int GetWebKnownIssue(Exception ex)
	{
		string xml = SharpRegion.TryGetKnownIssues(ApplicationSettingsManager.Settings.InstanceID, ex);
		int id = KnownIssuesHelper.GetKnownIssueFromXml(ex, xml);
		return id;
	}

	public static bool IsKnownIssue(Exception ex)
	{
		return KnownIssuesHelper.IsKnownIssue(ex, false);
	}

	public static bool IsKnownIssue(Exception ex, bool checkOnLine)
	{
		int id = KnownIssuesHelper.GetEmbeddedKnownIssue(ex);
		if (id != -1)
		{
			return true;
		}
		if (!checkOnLine)
		{
			return false;
		}
		id = KnownIssuesHelper.GetWebKnownIssue(ex);
		if (id != -1)
		{
			return true;
		}
		return false;
	}
}
}