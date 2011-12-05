using System.CodeDom.Compiler;
using System;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
public sealed class sendFeedbackCompletedEventHandler : MulticastDelegate
{
	public sendFeedbackCompletedEventHandler(object object, IntPtr method);

	public virtual IAsyncResult BeginInvoke(object sender, sendFeedbackCompletedEventArgs e, AsyncCallback callback, object object);

	public virtual void EndInvoke(IAsyncResult result);

	public virtual void Invoke(object sender, sendFeedbackCompletedEventArgs e);
}
}