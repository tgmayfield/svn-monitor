using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace SVNMonitor.SharpRegion
{
	[DesignerCategory("code"), DebuggerStepThrough, GeneratedCode("System.Web.Services", "2.0.50727.4918")]
	public class getKnownIssues_ver1CompletedEventArgs : AsyncCompletedEventArgs
	{
		private readonly object[] results;

		internal getKnownIssues_ver1CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
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