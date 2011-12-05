namespace SVNMonitor.Remoting
{
    using SVNMonitor.Logging;
    using System;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Ipc;

    internal class RemotingServer
    {
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

