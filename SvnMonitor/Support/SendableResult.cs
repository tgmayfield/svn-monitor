namespace SVNMonitor.Support
{
    using System;
    using System.Runtime.CompilerServices;

    public class SendableResult
    {
        public int Id { get; set; }

        public IDisposable Proxy { get; set; }
    }
}

