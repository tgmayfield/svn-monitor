using System.CodeDom.Compiler;
using System;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
public sealed class testCompletedEventHandler : MulticastDelegate
{
	public testCompletedEventHandler(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(object sender, testCompletedEventArgs e, AsyncCallback callback, object object);

	public virtual void EndInvoke(IAsyncResult result);

	public virtual void Invoke(object sender, testCompletedEventArgs e);
}
}