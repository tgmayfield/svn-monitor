namespace SVNMonitor.Support
{
    using SVNMonitor.Logging;
    using SVNMonitor.SharpRegion;
    using SVNMonitor.Web;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class BaseSendable : ISendable
    {
        private bool aborted;

        protected BaseSendable()
        {
        }

        protected abstract void SendInternal(SendCallback callback);
        void ISendable.Abort()
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

        void ISendable.Send(SendCallback callback)
        {
            this.Proxy = SharpRegion.GetServer();
            this.SendInternal(callback);
        }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        protected svnmonitor_server Proxy { get; private set; }

        bool ISendable.Aborted
        {
            get
            {
                return this.aborted;
            }
        }
    }
}

