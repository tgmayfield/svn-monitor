using System;

using SVNMonitor.Logging;
using SVNMonitor.SharpRegion;

namespace SVNMonitor.Support
{
	public abstract class BaseSendable : ISendable
	{
		private bool aborted;

		protected abstract void SendInternal(SendCallback callback);

		void ISendable.Abort()
		{
			aborted = true;
			if (Proxy != null)
			{
				Logger.Log.Debug("Aborting SharpRegion's proxy...");
				Proxy.Abort();
				Proxy.Dispose();
				Logger.Log.Debug("SharpRegion's proxy aborted");
			}
		}

		void ISendable.Send(SendCallback callback)
		{
			Proxy = Web.SharpRegion.GetServer();
			SendInternal(callback);
		}

		public string Email { get; set; }

		public string Name { get; set; }

		public string Note { get; set; }

		protected svnmonitor_server Proxy { get; private set; }

		bool ISendable.Aborted
		{
			get { return aborted; }
		}
	}
}