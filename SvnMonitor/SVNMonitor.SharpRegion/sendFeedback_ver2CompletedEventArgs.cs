using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ComponentModel;
using System;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
[DebuggerStepThrough]
[DesignerCategory("code")]
public class sendFeedback_ver2CompletedEventArgs : AsyncCompletedEventArgs
{
	private object[] results;

	public int Result
	{
		get
		{
			base.RaiseExceptionIfNecessary();
			return (int)this.results[0];
		}
	}

	internal sendFeedback_ver2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
}