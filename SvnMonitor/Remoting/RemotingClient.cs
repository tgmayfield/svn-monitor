using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace SVNMonitor.Remoting
{
	public class RemotingClient
	{
		public static void Close()
		{
			GetRemoteProxy().ExecuteCommand("close");
		}

		private static RemoteProxy GetRemoteProxy()
		{
			IpcChannel channel = new IpcChannel();
			ChannelServices.RegisterChannel(channel, false);
			RemotingConfiguration.RegisterWellKnownClientType(typeof(RemoteProxy), "ipc://svnmonitor/proxy");
			return new RemoteProxy();
		}

		public static void Show()
		{
			GetRemoteProxy().ExecuteCommand("show");
		}

		public static void ShowOrHide()
		{
			GetRemoteProxy().ExecuteCommand("showorhide");
		}
	}
}