using System.Diagnostics;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System;

namespace SVNMonitor.SharpRegion
{
[DebuggerStepThrough]
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
[DesignerCategory("code")]
public class testCompletedEventArgs : AsyncCompletedEventArgs
{
	private object[] results;

	public string Result
	{
		get
		{
			base.RaiseExceptionIfNecessary();
			return (string)this.results[0];
		}
	}

	internal testCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
}