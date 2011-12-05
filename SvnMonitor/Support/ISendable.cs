namespace SVNMonitor.Support
{
    using System;

    public interface ISendable
    {
        void Abort();
        void Send(SendCallback callback);

        bool Aborted { get; }
    }
}

