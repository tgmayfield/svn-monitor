using System;
using SVNMonitor.Web;
using SVNMonitor.Settings;
using SVNMonitor.Helpers;
using System.Threading;

namespace SVNMonitor.Support
{
public class UserFeedback : BaseFeedback
{
	public bool IncludeAdditionalInfo
	{
		get;
		set;
	}

	public UserFeedback()
	{
	}

	private void Send(object state)
	{
		SendCallback callback = (SendCallback)state;
		string info = "<No usage info>";
		if (this.IncludeAdditionalInfo)
		{
			info = base.Xml;
		}
		int id = SharpRegion.TrySendFeedback(base.Proxy, ApplicationSettingsManager.Settings.InstanceID, base.Name, base.Email, base.Note, info);
		base.EndSend(callback, id);
	}

	protected override void SendInternal(SendCallback callback)
	{
		ThreadHelper.Queue(new WaitCallback(this.Send), "USER_FEEDBACK", callback);
	}
}
}