using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace SVNMonitor.SharpRegion
{
	[GeneratedCode("System.Web.Services", "2.0.50727.4918"), DebuggerStepThrough, DesignerCategory("code")]
	public class sendErrorReport_ver2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private readonly object[] results;

		internal sendErrorReport_ver2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)results[0];
			}
		}
	}
}