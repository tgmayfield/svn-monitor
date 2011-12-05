using System;

using SVNMonitor.Settings;

namespace SVNMonitor.Support
{
	public class ErrorReportFeedback : BaseFeedback
	{
		public ErrorReportFeedback(Exception ex)
			: base(ex)
		{
		}

		public static ErrorReportFeedback Generate()
		{
			return Generate(null);
		}

		public static ErrorReportFeedback Generate(Exception ex)
		{
			return new ErrorReportFeedback(ex);
		}

		private void Send(object state)
		{
			SendCallback callback = (SendCallback)state;
			int id = Web.SharpRegion.TrySendErrorReport(base.Proxy, ApplicationSettingsManager.Settings.InstanceID, base.Name, base.Email, base.Note, base.Xml);
			EndSend(callback, id);
		}

		protected override void SendInternal(SendCallback callback)
		{
			SVNMonitor.Helpers.ThreadHelper.Queue(Send, "ERROR_REPORT", callback);
		}
	}
}