using System;

namespace SVNMonitor.Extensions
{
public sealed class Func<T1, T2, TResult> : MulticastDelegate
{
	public Func(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(T1 arg1, T2 arg2, AsyncCallback callback, object object);

	public virtual TResult EndInvoke(IAsyncResult result);

	public virtual TResult Invoke(T1 arg1, T2 arg2);
}
}