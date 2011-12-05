using System.CodeDom.Compiler;
using System;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
public sealed class sendErrorReport_ver2CompletedEventHandler : MulticastDelegate
{
	public sendErrorReport_ver2CompletedEventHandler(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(object sender, sendErrorReport_ver2CompletedEventArgs e, AsyncCallback callback, object object);

	public virtual void EndInvoke(IAsyncResult result);

	public virtual void Invoke(object sender, sendErrorReport_ver2CompletedEventArgs e);
}
}