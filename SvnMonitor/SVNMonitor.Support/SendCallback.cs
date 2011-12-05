using System;

namespace SVNMonitor.Support
{
public sealed class SendCallback : MulticastDelegate
{
	public SendCallback(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(SendableResult result, AsyncCallback callback, object object);

	public virtual void EndInvoke(IAsyncResult result);

	public virtual void Invoke(SendableResult result);
}
}