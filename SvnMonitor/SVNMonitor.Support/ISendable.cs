using System;

namespace SVNMonitor.Support
{
public interface ISendable
{
	bool Aborted { get; }

	void Abort();

	void Send(SendCallback callback);
}
}