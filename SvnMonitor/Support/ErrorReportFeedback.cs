namespace SVNMonitor.Support
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Settings;
    using SVNMonitor.Web;
    using System;
    using System.Threading;

    public class ErrorReportFeedback : BaseFeedback
    {
        public ErrorReportFeedback(Exception ex) : base(ex)
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
            SendCallback callback = (SendCallback) state;
            int id = SharpRegion.TrySendErrorReport(base.Proxy, ApplicationSettingsManager.Settings.InstanceID, base.Name, base.Email, base.Note, base.Xml);
            this.EndSend(callback, id);
        }

        protected override void SendInternal(SendCallback callback)
        {
            SVNMonitor.Helpers.ThreadHelper.Queue(new WaitCallback(this.Send), "ERROR_REPORT", callback);
        }
    }
}

