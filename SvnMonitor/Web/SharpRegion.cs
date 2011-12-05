using System;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor;
using SVNMonitor.SharpRegion;
using System.Net;
using SVNMonitor.Resources.Text;
using System.Windows.Forms;
using SVNMonitor.View;

namespace SVNMonitor.Web
{
internal class SharpRegion
{
	private static string Version
	{
		get
		{
			return FileSystemHelper.CurrentVersion.ToString();
		}
	}

	public SharpRegion()
	{
	}

	internal static void BrowseBlog()
	{
		SharpRegion.OpenLink("http://www.sharpregion.com");
	}

	internal static void BrowseDonate()
	{
		SharpRegion.OpenLink("http://donate.svnmonitor.com");
	}

	internal static void BrowseDownloadPage()
	{
		SharpRegion.OpenLink("http://download.svnmonitor.com");
	}

	internal static void BrowseHome()
	{
		SharpRegion.OpenLink("http://www.svnmonitor.com");
	}

	internal static void BrowseSendEmail()
	{
		SharpRegion.OpenLink("mailto:adrian@sharpregion.com");
	}

	internal static bool DownloadLatestVersion(Version version, out string zipPath)
	{
		Logger.Log.Debug("Getting latest version url...");
		string url = WebHelper.DownloadString("http://upgrade.svnmonitor.com", string.Empty);
		Logger.Log.DebugFormat("Latest version url is: {0}", url);
		if (string.IsNullOrEmpty(url))
		{
			return false;
		}
		url = string.Format(url.Trim(), version);
		EventLog.LogSystem(string.Format("Downloading a new version: {0} ...", url));
		bool downloaded = WebHelper.DownloadFile(url, ref zipPath);
		if (!downloaded)
		{
			FileSystemHelper.DeleteFile(ref zipPath);
		}
		try
		{
		}
		catch
		{
		}
		return downloaded;
	}

	internal static string GetKnownIssues(string id, Exception ex)
	{
		svnmonitor_server server = SharpRegion.GetServer();
		using (server)
		{
			return server.getKnownIssues_ver2(id, SharpRegion.Version, ex.ToString());
		}
	}

	internal static string GetLatestVersionString()
	{
		string versionString = WebHelper.DownloadString("http://version.svnmonitor.com", "0.0.0.0");
		return versionString;
	}

	internal static svnmonitor_server GetServer()
	{
		ServicePointManager.Expect100Continue = false;
		IWebProxy proxy = WebRequest.DefaultWebProxy;
		proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
		svnmonitor_server svnmonitorServer = new svnmonitor_server();
		svnmonitorServer.Credentials = CredentialCache.DefaultNetworkCredentials;
		svnmonitorServer.Proxy = proxy;
		svnmonitor_server server = svnmonitorServer;
		return server;
	}

	internal static string GetWhatsNewMessage()
	{
		string message = WebHelper.DownloadString("http://readme.svnmonitor.com", string.Empty);
		return message;
	}

	private static void OpenLink(string address)
	{
		object[] objArray;
		try
		{
			ProcessHelper.StartProcess(address);
		}
		catch (Exception ex)
		{
			Logger.Log.ErrorFormat("Error navigating to {0}", address);
			Logger.Log.Error(ex.Message, ex);
			string message = string.Format("{0}.{1}{2} {3}", new object[] { ex.Message, Environment.NewLine, Strings.ManuallyBrowse, address });
			MessageBox.Show(MainForm.FormInstance, message, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private static int SendErrorReport(svnmonitor_server server, string id, string name, string email, string note, string report)
	{
		int reportID = server.sendErrorReport_ver2(id, SharpRegion.Version, name, email, note, report);
		return reportID;
	}

	private static int SendFeedback(svnmonitor_server server, string id, string name, string email, string note, string usageInfo)
	{
		int feedbackID = server.sendFeedback_ver2(id, SharpRegion.Version, name, email, note, usageInfo);
		return feedbackID;
	}

	internal static void SendUpgradeInfo(string id)
	{
		svnmonitor_server server = SharpRegion.GetServer();
		using (server)
		{
			server.sendUpgradeInfo(id, SharpRegion.Version);
		}
	}

	internal static void SendUsageInfo(string usageInfo)
	{
		svnmonitor_server server = SharpRegion.GetServer();
		using (server)
		{
			server.sendUsageInfo(usageInfo);
		}
	}

	internal static string TryGetKnownIssues(string id, Exception exception)
	{
		string knownIssues = string.Empty;
		try
		{
			return SharpRegion.GetKnownIssues(id, exception);
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error trying to get known issues: ", ex);
		}
		return knownIssues;
	}

	internal static int TrySendErrorReport(svnmonitor_server server, string id, string name, string email, string note, string report)
	{
		object[] objArray;
		Exception ex = null;
		try
		{
			int errorReportID = SharpRegion.SendErrorReport(server, id, name, email, note, report);
			return errorReportID;
		}
		catch (WebException webex)
		{
			if (webex.Status != 6)
			{
			}
		}
		catch (Exception otherex)
		{
		}
		if (ex != null)
		{
			Logger.Log.Error("Error trying to send error report: ", ex);
			Logger.Log.Error(string.Concat("Name = ", name));
			Logger.Log.Error(string.Concat("Email = ", email));
			Logger.Log.Error(string.Concat("Note = ", note));
			EventLog.LogError(Strings.ErrorSendingErrorReport_FORMAT.FormatWith(new object[] { ex.Message }), null, ex);
		}
		return 0;
	}

	internal static int TrySendFeedback(svnmonitor_server server, string id, string name, string email, string note, string usageInfo)
	{
		object[] objArray;
		Exception ex = null;
		try
		{
			int feedbackID = SharpRegion.SendFeedback(server, id, name, email, note, usageInfo);
			return feedbackID;
		}
		catch (WebException webex)
		{
			if (webex.Status != 6)
			{
			}
		}
		catch (Exception otherex)
		{
		}
		if (ex != null)
		{
			Logger.Log.Error("Error trying to send feedback:", ex);
			Logger.Log.Error(string.Concat("Name = ", name));
			Logger.Log.Error(string.Concat("Email = ", email));
			Logger.Log.Error(string.Concat("Note = ", note));
			EventLog.LogError(Strings.ErrorSendingFeedback_FORMAT.FormatWith(new object[] { ex.Message }), null, ex);
		}
		return 0;
	}

	internal static void TrySendUpgradeInfo(string id)
	{
		try
		{
			SharpRegion.SendUpgradeInfo(id);
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error trying to save upgrade info:", ex);
		}
	}

	internal static void TrySendUsageInfo(string usageInfo)
	{
		try
		{
			SharpRegion.SendUsageInfo(usageInfo);
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error trying to send usage info: ", ex);
		}
	}
}
}