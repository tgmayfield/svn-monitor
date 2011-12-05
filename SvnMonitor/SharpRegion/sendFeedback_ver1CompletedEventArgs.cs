using System.ComponentModel;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System;

namespace SVNMonitor.SharpRegion
{
[DesignerCategory("code")]
[DebuggerStepThrough]
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
public class sendFeedback_ver1CompletedEventArgs : AsyncCompletedEventArgs
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

	internal sendFeedback_ver1CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
}