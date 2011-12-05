using System;

namespace SVNMonitor.Support
{
	public class SendableResult
	{
		public int Id { get; set; }

		public IDisposable Proxy { get; set; }
	}
}