using System;
using SVNMonitor.SharpRegion;
using SVNMonitor.Logging;
using SVNMonitor.Web;

namespace SVNMonitor.Support
{
public abstract class BaseSendable : ISendable
{
	private bool aborted;

	public string Email
	{
		get;
		set;
	}

	public string Name
	{
		get;
		set;
	}

	public string Note
	{
		get;
		set;
	}

	protected svnmonitor_server Proxy
	{
		get;
		private set;
	}

	private bool SVNMonitor.Support.ISendable.Aborted
	{
		get
		{
			return this.aborted;
		}
	}

	protected BaseSendable()
	{
	}

	protected abstract void SendInternal(SendCallback callback);

	private void SVNMonitor.Support.ISendable.Abort()
	{
		this.aborted = true;
		if (this.Proxy != null)
		{
			Logger.Log.Debug("Aborting SharpRegion's proxy...");
			this.Proxy.Abort();
			this.Proxy.Dispose();
			Logger.Log.Debug("SharpRegion's proxy aborted");
		}
	}

	private void SVNMonitor.Support.ISendable.Send(SendCallback callback)
	{
		this.Proxy = SharpRegion.GetServer();
		this.SendInternal(callback);
	}
}
}