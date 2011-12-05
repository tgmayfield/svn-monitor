namespace SVNMonitor.Support
{
    using SVNMonitor;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Settings;
    using SVNMonitor.Web;
    using System;
    using System.Text;
    using System.Threading;

    internal class UsageInformationSender
    {
        internal static readonly string Separator = Environment.NewLine;

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
            if (info.EndsWith(Separator))
            {
                info = info.Remove(info.Length - Separator.Length);
            }
            return info;
        }

        private bool IsUsageSentThisDay()
        {
            DateTime sentDate = DateTime.Parse(ApplicationSettingsManager.Settings.UsageSendDate);
            return DateTime.Now.Date.Equals(sentDate.Date);
        }

        private void Send(string usageInfo)
        {
            Logger.Log.Info("Sending usage info.");
            Logger.Log.DebugFormat("Usage info: {0}", usageInfo);
            SharpRegion.TrySendUsageInfo(usageInfo);
        }

        internal void SendUsageInformation()
        {
            if (ApplicationSettingsManager.Settings.EnableSendingUsageInformation)
            {
                this.SendUsageInformationAsync();
            }
        }

        private void SendUsageInformation(object state)
        {
            if (this.IsUsageSentThisDay())
            {
                Logger.Log.Debug("Usage information already sent for this day.");
            }
            else
            {
                string usageInfo = GetUsageInformation();
                try
                {
                    this.Send(usageInfo);
                    ApplicationSettingsManager.Settings.UsageSendDate = DateTime.Now.ToString();
                    ApplicationSettingsManager.SaveSettings();
                }
                catch (Exception ex)
                {
                    Logger.Log.Debug("Error sending usage information", ex);
                }
            }
        }

        private void SendUsageInformationAsync()
        {
            SVNMonitor.Helpers.ThreadHelper.Queue(new WaitCallback(this.SendUsageInformation), "SEND_USAGE");
        }
    }
}

