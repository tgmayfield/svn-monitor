using System;

namespace SVNMonitor.Support
{
	public interface ISendable
	{
		void Abort();
		void Send(SendCallback callback);

		bool Aborted { get; }
	}
}