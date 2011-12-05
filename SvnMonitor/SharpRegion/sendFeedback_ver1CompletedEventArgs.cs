using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace SVNMonitor.SharpRegion
{
	[DesignerCategory("code"), DebuggerStepThrough, GeneratedCode("System.Web.Services", "2.0.50727.4918")]
	public class sendFeedback_ver1CompletedEventArgs : AsyncCompletedEventArgs
	{
		private readonly object[] results;

		internal sendFeedback_ver1CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
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