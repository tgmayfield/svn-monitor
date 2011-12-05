using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
[DesignerCategory("code")]
[DebuggerStepThrough]
public class sendFeedbackCompletedEventArgs : AsyncCompletedEventArgs
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

	internal sendFeedbackCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
}