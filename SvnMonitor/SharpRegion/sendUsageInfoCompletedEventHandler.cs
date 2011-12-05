using System.CodeDom.Compiler;
using System;
using System.ComponentModel;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
public sealed class sendUsageInfoCompletedEventHandler : MulticastDelegate
{
	public sendUsageInfoCompletedEventHandler(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(object sender, AsyncCompletedEventArgs e, AsyncCallback callback, object object);

	public virtual void EndInvoke(IAsyncResult result);

	public virtual void Invoke(object sender, AsyncCompletedEventArgs e);
}
}