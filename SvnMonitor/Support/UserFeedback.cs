using System;

using SVNMonitor.Settings;

namespace SVNMonitor.Support
{
	public class UserFeedback : BaseFeedback
	{
		private void Send(object state)
		{
			SendCallback callback = (SendCallback)state;
			string info = "<No usage info>";
			if (IncludeAdditionalInfo)
			{
				info = base.Xml;
			}
			int id = Web.SharpRegion.TrySendFeedback(base.Proxy, ApplicationSettingsManager.Settings.InstanceID, base.Name, base.Email, base.Note, info);
			EndSend(callback, id);
		}

		protected override void SendInternal(SendCallback callback)
		{
			SVNMonitor.Helpers.ThreadHelper.Queue(Send, "USER_FEEDBACK", callback);
		}

		public bool IncludeAdditionalInfo { get; set; }
	}
}