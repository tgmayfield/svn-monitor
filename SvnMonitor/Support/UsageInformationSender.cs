using System;
using System.Text;
using SVNMonitor;
using SVNMonitor.Settings;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Web;
using System.Threading;

namespace SVNMonitor.Support
{
internal class UsageInformationSender
{
	internal readonly static string Separator;

	static UsageInformationSender()
	{
		UsageInformationSender.Separator = Environment.NewLine;
	}

	public UsageInformationSender()
	{
	}

	internal static string GetUsageInformation()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(VersionChecker.Instance.GetUsageInformation());
		sb.Append(MonitorSettings.Instance.GetUsageInformation());
		sb.Append(ApplicationSettingsManager.Settings.GetUsageInformation());
		sb.Append(EnvironmentHelper.GetUsageInformation());
		sb.Append(ProcessHelper.GetUsageInformation());
		sb.Append(SessionInfo.GetUsageInformation());
		string info = sb.ToString();
		if (info.EndsWith(UsageInformationSender.Separator))
		{
			info = info.Remove(info.Length - UsageInformationSender.Separator.Length);
		}
		return info;
	}

	private bool IsUsageSentThisDay()
	{
		DateTime sentDate = DateTime.Parse(ApplicationSettingsManager.Settings.UsageSendDate);
		DateTime now = DateTime.Now;
		DateTime date = now.Date;
		bool sentToday = date.Equals(sentDate.Date);
		return sentToday;
	}

	private void Send(string usageInfo)
	{
		Logger.Log.Info("Sending usage info.");
		Logger.Log.DebugFormat("Usage info: {0}", usageInfo);
		SharpRegion.TrySendUsageInfo(usageInfo);
	}

	internal void SendUsageInformation()
	{
		if (!ApplicationSettingsManager.Settings.EnableSendingUsageInformation)
		{
			return;
		}
		this.SendUsageInformationAsync();
	}

	private void SendUsageInformation(object state)
	{
		if (this.IsUsageSentThisDay())
		{
			Logger.Log.Debug("Usage information already sent for this day.");
			return;
		}
		string usageInfo = UsageInformationSender.GetUsageInformation();
		try
		{
			this.Send(usageInfo);
			DateTime now = DateTime.Now.UsageSendDate = now.ToString();
			ApplicationSettingsManager.SaveSettings();
		}
		catch (Exception ex)
		{
			Logger.Log.Debug("Error sending usage information", ex);
		}
	}

	private void SendUsageInformationAsync()
	{
		ThreadHelper.Queue(new WaitCallback(this.SendUsageInformation), "SEND_USAGE");
	}
}
}