using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace SVNMonitor.SharpRegion
{
	[GeneratedCode("System.Web.Services", "2.0.50727.4918"), DebuggerStepThrough, DesignerCategory("code")]
	public class getKnownIssues_ver2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private readonly object[] results;

		internal getKnownIssues_ver2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)results[0];
			}
		}
	}
}