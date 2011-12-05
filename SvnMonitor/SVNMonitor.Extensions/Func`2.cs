using System;

namespace SVNMonitor.Extensions
{
public sealed class Func<T, TResult> : MulticastDelegate
{
	public Func(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(T arg, AsyncCallback callback, object object);

	public virtual TResult EndInvoke(IAsyncResult result);

	public virtual TResult Invoke(T arg);
}
}