using System;
using SVNMonitor.Logging;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace SVNMonitor.Remoting
{
internal class RemotingServer
{
	public RemotingServer()
	{
	}

	internal static void Start()
	{
		Logger.Log.Info("Starting remoting ipc-server...");
		IpcChannel channel = new IpcChannel("svnmonitor");
		ChannelServices.RegisterChannel(channel, false);
		RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteProxy), "proxy", WellKnownObjectMode.SingleCall);
		Logger.Log.Info("Remoting ipc-server started.");
		RemoteProxy.Init();
	}
}
}