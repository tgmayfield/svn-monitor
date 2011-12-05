using System;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace SVNMonitor.Remoting
{
public class RemotingClient
{
	public RemotingClient()
	{
	}

	public static void Close()
	{
		RemoteProxy proxy = RemotingClient.GetRemoteProxy();
		proxy.ExecuteCommand("close");
	}

	private static RemoteProxy GetRemoteProxy()
	{
		IpcChannel channel = new IpcChannel();
		ChannelServices.RegisterChannel(channel, false);
		RemotingConfiguration.RegisterWellKnownClientType(typeof(RemoteProxy), "ipc://svnmonitor/proxy");
		RemoteProxy obj = new RemoteProxy();
		return obj;
	}

	public static void Show()
	{
		RemoteProxy proxy = RemotingClient.GetRemoteProxy();
		proxy.ExecuteCommand("show");
	}

	public static void ShowOrHide()
	{
		RemoteProxy proxy = RemotingClient.GetRemoteProxy();
		proxy.ExecuteCommand("showorhide");
	}
}
}