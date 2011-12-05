namespace SVNMonitor.Support
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Settings;
    using SVNMonitor.Web;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class UserFeedback : BaseFeedback
    {
        private void Send(object state)
        {
            SendCallback callback = (SendCallback) state;
            string info = "<No usage info>";
            if (this.IncludeAdditionalInfo)
            {
                info = base.Xml;
            }
            int id = SharpRegion.TrySendFeedback(base.Proxy, ApplicationSettingsManager.Settings.InstanceID, base.Name, base.Email, base.Note, info);
            this.EndSend(callback, id);
        }

        protected override void SendInternal(SendCallback callback)
        {
            SVNMonitor.Helpers.ThreadHelper.Queue(new WaitCallback(this.Send), "USER_FEEDBACK", callback);
        }

        public bool IncludeAdditionalInfo { get; set; }
    }
}

