using System.CodeDom.Compiler;
using System;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
public sealed class getKnownIssues_ver1CompletedEventHandler : MulticastDelegate
{
	public getKnownIssues_ver1CompletedEventHandler(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(object sender, getKnownIssues_ver1CompletedEventArgs e, AsyncCallback callback, object object);

	public virtual void EndInvoke(IAsyncResult result);

	public virtual void Invoke(object sender, getKnownIssues_ver1CompletedEventArgs e);
}
}