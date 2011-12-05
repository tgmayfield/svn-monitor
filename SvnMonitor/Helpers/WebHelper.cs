using System;
using System.Net;

using SVNMonitor.Logging;

namespace SVNMonitor.Helpers
{
	public class WebHelper
	{
		public static bool DownloadFile(string address, string fileName)
		{
			WebClient webClient = GetWebClient();
			try
			{
				webClient.DownloadFile(address, fileName);
				return true;
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Error downloading file from {0}", address), ex);
			}
			finally
			{
				if (webClient != null)
				{
					webClient.Dispose();
				}
			}
			return false;
		}

		public static string DownloadString(string address, string defaultOnError)
		{
			using (WebClient webClient = GetWebClient())
			{
				webClient.Proxy = WebRequest.DefaultWebProxy;
				webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;
				string retString = defaultOnError;
				try
				{
					retString = webClient.DownloadString(address);
				}
				catch (Exception)
				{
				}
				return retString;
			}
		}

		private static WebClient GetWebClient()
		{
			WebClient webClient = new WebClient
			{
				Proxy = WebRequest.DefaultWebProxy
			};
			webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;
			return webClient;
		}
	}
}