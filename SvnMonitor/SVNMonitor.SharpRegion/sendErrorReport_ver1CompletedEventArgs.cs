using System.Diagnostics;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System;

namespace SVNMonitor.SharpRegion
{
[DebuggerStepThrough]
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
[DesignerCategory("code")]
public class sendErrorReport_ver1CompletedEventArgs : AsyncCompletedEventArgs
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

	internal sendErrorReport_ver1CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
}