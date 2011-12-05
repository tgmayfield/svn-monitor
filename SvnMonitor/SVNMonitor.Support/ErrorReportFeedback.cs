using System;
using SVNMonitor.Web;
using SVNMonitor.Settings;
using SVNMonitor.Helpers;
using System.Threading;

namespace SVNMonitor.Support
{
public class ErrorReportFeedback : BaseFeedback
{
	public ErrorReportFeedback(Exception ex) : base(ex)
	{
	}

	public static ErrorReportFeedback Generate()
	{
		return ErrorReportFeedback.Generate(null);
	}

	public static ErrorReportFeedback Generate(Exception ex)
	{
		ErrorReportFeedback report = new ErrorReportFeedback(ex);
		return report;
	}

	private void Send(object state)
	{
		SendCallback callback = (SendCallback)state;
		int id = SharpRegion.TrySendErrorReport(base.Proxy, ApplicationSettingsManager.Settings.InstanceID, base.Name, base.Email, base.Note, base.Xml);
		base.EndSend(callback, id);
	}

	protected override void SendInternal(SendCallback callback)
	{
		ThreadHelper.Queue(new WaitCallback(this.Send), "ERROR_REPORT", callback);
	}
}
}