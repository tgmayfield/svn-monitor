using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ComponentModel;
using System;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
[DebuggerStepThrough]
[DesignerCategory("code")]
public class getKnownIssues_ver2CompletedEventArgs : AsyncCompletedEventArgs
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

	internal getKnownIssues_ver2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
}