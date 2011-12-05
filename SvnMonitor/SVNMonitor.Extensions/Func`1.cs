using System;

namespace SVNMonitor.Extensions
{
public sealed class Func<TResult> : MulticastDelegate
{
	public Func(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(AsyncCallback callback, object object);

	public virtual TResult EndInvoke(IAsyncResult result);

	public virtual TResult Invoke();
}
}