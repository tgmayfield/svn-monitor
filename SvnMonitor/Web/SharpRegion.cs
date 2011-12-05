using System;
using System.Net;
using System.Windows.Forms;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.SharpRegion;
using SVNMonitor.View;

namespace SVNMonitor.Web
{
	internal class SharpRegion
	{
		internal static void BrowseBlog()
		{
			OpenLink("http://www.sharpregion.com");
		}

		internal static void BrowseDonate()
		{
			OpenLink("http://donate.svnmonitor.com");
		}

		internal static void BrowseDownloadPage()
		{
			OpenLink("http://download.svnmonitor.com");
		}

		internal static void BrowseHome()
		{
			OpenLink("http://www.svnmonitor.com");
		}

		internal static void BrowseSendEmail()
		{
			OpenLink("mailto:adrian@sharpregion.com");
		}

		internal static bool DownloadLatestVersion(System.Version version, out string zipPath)
		{
			zipPath = string.Empty;
			Logger.Log.Debug("Getting latest version url...");
			string url = WebHelper.DownloadString("http://upgrade.svnmonitor.com", string.Empty);
			Logger.Log.DebugFormat("Latest version url is: {0}", url);
			if (string.IsNullOrEmpty(url))
			{
				return false;
			}
			url = string.Format(url.Trim(), version);
			zipPath = FileSystemHelper.GetTempFileName();
			EventLog.LogSystem(string.Format("Downloading a new version: {0} ...", url));
			bool downloaded = WebHelper.DownloadFile(url, zipPath);
			if (!downloaded)
			{
				try
				{
					FileSystemHelper.DeleteFile(zipPath);
				}
				catch
				{
				}
			}
			return downloaded;
		}

		internal static string GetKnownIssues(string id, Exception ex)
		{
			using (svnmonitor_server server = GetServer())
			{
				return server.getKnownIssues_ver2(id, Version, ex.ToString());
			}
		}

		internal static string GetLatestVersionString()
		{
			return WebHelper.DownloadString("http://version.svnmonitor.com", "0.0.0.0");
		}

		internal static svnmonitor_server GetServer()
		{
			ServicePointManager.Expect100Continue = false;
			IWebProxy proxy = WebRequest.DefaultWebProxy;
			proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
			return new svnmonitor_server
			{
				Credentials = CredentialCache.DefaultNetworkCredentials,
				Proxy = proxy
			};
		}

		internal static string GetWhatsNewMessage()
		{
			return WebHelper.DownloadString("http://readme.svnmonitor.com", string.Empty);
		}

		private static void OpenLink(string address)
		{
			try
			{
				ProcessHelper.StartProcess(address);
			}
			catch (Exception ex)
			{
				Logger.Log.ErrorFormat("Error navigating to {0}", address);
				Logger.Log.Error(ex.Message, ex);
				string message = string.Format("{0}.{1}{2} {3}", new object[]
				{
					ex.Message, Environment.NewLine, Strings.ManuallyBrowse, address
				});
				MessageBox.Show(MainForm.FormInstance, message, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private static int SendErrorReport(svnmonitor_server server, string id, string name, string email, string note, string report)
		{
			return server.sendErrorReport_ver2(id, Version, name, email, note, report);
		}

		private static int SendFeedback(svnmonitor_server server, string id, string name, string email, string note, string usageInfo)
		{
			return server.sendFeedback_ver2(id, Version, name, email, note, usageInfo);
		}

		internal static void SendUpgradeInfo(string id)
		{
			using (svnmonitor_server server = GetServer())
			{
				server.sendUpgradeInfo(id, Version);
			}
		}

		internal static void SendUsageInfo(string usageInfo)
		{
			using (svnmonitor_server server = GetServer())
			{
				server.sendUsageInfo(usageInfo);
			}
		}

		internal static string TryGetKnownIssues(string id, Exception exception)
		{
			string knownIssues = string.Empty;
			try
			{
				knownIssues = GetKnownIssues(id, exception);
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error trying to get known issues: ", ex);
			}
			return knownIssues;
		}

		internal static int TrySendErrorReport(svnmonitor_server server, string id, string name, string email, string note, string report)
		{
			Exception ex = null;
			try
			{
				return SendErrorReport(server, id, name, email, note, report);
			}
			catch (WebException webex)
			{
				if (webex.Status != WebExceptionStatus.RequestCanceled)
				{
					ex = webex;
				}
			}
			catch (Exception otherex)
			{
				ex = otherex;
			}
			if (ex != null)
			{
				Logger.Log.Error("Error trying to send error report: ", ex);
				Logger.Log.Error("Name = " + name);
				Logger.Log.Error("Email = " + email);
				Logger.Log.Error("Note = " + note);
				EventLog.LogError(Strings.ErrorSendingErrorReport_FORMAT.FormatWith(new object[]
				{
					ex.Message
				}), null, ex);
			}
			return 0;
		}

		internal static int TrySendFeedback(svnmonitor_server server, string id, string name, string email, string note, string usageInfo)
		{
			Exception ex = null;
			try
			{
				return SendFeedback(server, id, name, email, note, usageInfo);
			}
			catch (WebException webex)
			{
				if (webex.Status != WebExceptionStatus.RequestCanceled)
				{
					ex = webex;
				}
			}
			catch (Exception otherex)
			{
				ex = otherex;
			}
			if (ex != null)
			{
				Logger.Log.Error("Error trying to send feedback:", ex);
				Logger.Log.Error("Name = " + name);
				Logger.Log.Error("Email = " + email);
				Logger.Log.Error("Note = " + note);
				EventLog.LogError(Strings.ErrorSendingFeedback_FORMAT.FormatWith(new object[]
				{
					ex.Message
				}), null, ex);
			}
			return 0;
		}

		internal static void TrySendUpgradeInfo(string id)
		{
			try
			{
				SendUpgradeInfo(id);
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
				SendUsageInfo(usageInfo);
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error trying to send usage info: ", ex);
			}
		}

		private static string Version
		{
			get { return FileSystemHelper.CurrentVersion.ToString(); }
		}
	}
}