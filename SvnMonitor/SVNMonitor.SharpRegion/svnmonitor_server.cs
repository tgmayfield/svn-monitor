using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Threading;
using System;

namespace SVNMonitor.SharpRegion
{
[GeneratedCode("System.Web.Services", "2.0.50727.4918")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[WebServiceBinding(Name="svnmonitor_serverBinding", Namespace="urn:svnmonitor")]
public class svnmonitor_server : SoapHttpClientProtocol
{
	private getKnownIssues_ver1CompletedEventHandler getKnownIssues_ver1Completed;

	private SendOrPostCallback getKnownIssues_ver1OperationCompleted;

	private getKnownIssues_ver2CompletedEventHandler getKnownIssues_ver2Completed;

	private SendOrPostCallback getKnownIssues_ver2OperationCompleted;

	private sendErrorReport_ver1CompletedEventHandler sendErrorReport_ver1Completed;

	private SendOrPostCallback sendErrorReport_ver1OperationCompleted;

	private sendErrorReport_ver2CompletedEventHandler sendErrorReport_ver2Completed;

	private SendOrPostCallback sendErrorReport_ver2OperationCompleted;

	private sendErrorReportCompletedEventHandler sendErrorReportCompleted;

	private SendOrPostCallback sendErrorReportOperationCompleted;

	private sendFeedback_ver1CompletedEventHandler sendFeedback_ver1Completed;

	private SendOrPostCallback sendFeedback_ver1OperationCompleted;

	private sendFeedback_ver2CompletedEventHandler sendFeedback_ver2Completed;

	private SendOrPostCallback sendFeedback_ver2OperationCompleted;

	private sendFeedbackCompletedEventHandler sendFeedbackCompleted;

	private SendOrPostCallback sendFeedbackOperationCompleted;

	private sendUpgradeInfoCompletedEventHandler sendUpgradeInfoCompleted;

	private SendOrPostCallback sendUpgradeInfoOperationCompleted;

	private sendUsageInfoCompletedEventHandler sendUsageInfoCompleted;

	private SendOrPostCallback sendUsageInfoOperationCompleted;

	private testCompletedEventHandler testCompleted;

	private SendOrPostCallback testOperationCompleted;

	private bool useDefaultCredentialsSetExplicitly;

	public string Url
	{
		get
		{
			return base.Url;
		}
		set
		{
			if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
			{
				base.UseDefaultCredentials = false;
			}
			base.Url = value;
		}
	}

	public bool UseDefaultCredentials
	{
		get
		{
			return base.UseDefaultCredentials;
		}
		set
		{
			base.UseDefaultCredentials = value;
			this.useDefaultCredentialsSetExplicitly = true;
		}
	}

	public svnmonitor_server()
	{
		this.Url = "http://services.svnmonitor.com/";
		if (this.IsLocalFileSystemWebService(this.Url))
		{
			this.UseDefaultCredentials = true;
			this.useDefaultCredentialsSetExplicitly = false;
			return;
		}
		this.useDefaultCredentialsSetExplicitly = true;
	}

	public void CancelAsync(object userState)
	{
		base.CancelAsync(userState);
	}

	[SoapRpcMethod("urn:svnmonitor#getKnownIssues_ver1", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public string getKnownIssues_ver1(string id, string version)
	{
		object[] objArray;
		object[] results = base.Invoke("getKnownIssues_ver1", new object[] { id, version });
		return (string)results[0];
	}

	public void getKnownIssues_ver1Async(string id, string version)
	{
		this.getKnownIssues_ver1Async(id, version, null);
	}

	public void getKnownIssues_ver1Async(string id, string version, object userState)
	{
		object[] objArray;
		if (this.getKnownIssues_ver1OperationCompleted == null)
		{
			this.getKnownIssues_ver1OperationCompleted = new SendOrPostCallback(this.OngetKnownIssues_ver1OperationCompleted);
		}
		base.InvokeAsync("getKnownIssues_ver1", new object[] { id, version }, this.getKnownIssues_ver1OperationCompleted, userState);
	}

	[SoapRpcMethod("urn:svnmonitor#getKnownIssues_ver1", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public string getKnownIssues_ver2(string id, string version, string stack)
	{
		object[] objArray;
		object[] results = base.Invoke("getKnownIssues_ver2", new object[] { id, version, stack });
		return (string)results[0];
	}

	public void getKnownIssues_ver2Async(string id, string version, string stack)
	{
		this.getKnownIssues_ver2Async(id, version, stack, null);
	}

	public void getKnownIssues_ver2Async(string id, string version, string stack, object userState)
	{
		object[] objArray;
		if (this.getKnownIssues_ver2OperationCompleted == null)
		{
			this.getKnownIssues_ver2OperationCompleted = new SendOrPostCallback(this.OngetKnownIssues_ver2OperationCompleted);
		}
		base.InvokeAsync("getKnownIssues_ver2", new object[] { id, version, stack }, this.getKnownIssues_ver2OperationCompleted, userState);
	}

	private bool IsLocalFileSystemWebService(string url)
	{
		if (url == null || url == string.Empty)
		{
			return false;
		}
		Uri wsUri = new Uri(url);
		if (wsUri.Port >= 1024 && string.Compare(wsUri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return true;
		}
		return false;
	}

	private void OngetKnownIssues_ver1OperationCompleted(object arg)
	{
		if (this.getKnownIssues_ver1Completed != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.getKnownIssues_ver1Completed(this, new getKnownIssues_ver1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OngetKnownIssues_ver2OperationCompleted(object arg)
	{
		if (this.getKnownIssues_ver2Completed != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.getKnownIssues_ver2Completed(this, new getKnownIssues_ver2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OnsendErrorReport_ver1OperationCompleted(object arg)
	{
		if (this.sendErrorReport_ver1Completed != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.sendErrorReport_ver1Completed(this, new sendErrorReport_ver1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OnsendErrorReport_ver2OperationCompleted(object arg)
	{
		if (this.sendErrorReport_ver2Completed != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.sendErrorReport_ver2Completed(this, new sendErrorReport_ver2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OnsendErrorReportOperationCompleted(object arg)
	{
		if (this.sendErrorReportCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.sendErrorReportCompleted(this, new sendErrorReportCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OnsendFeedback_ver1OperationCompleted(object arg)
	{
		if (this.sendFeedback_ver1Completed != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.sendFeedback_ver1Completed(this, new sendFeedback_ver1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OnsendFeedback_ver2OperationCompleted(object arg)
	{
		if (this.sendFeedback_ver2Completed != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.sendFeedback_ver2Completed(this, new sendFeedback_ver2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OnsendFeedbackOperationCompleted(object arg)
	{
		if (this.sendFeedbackCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.sendFeedbackCompleted(this, new sendFeedbackCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OnsendUpgradeInfoOperationCompleted(object arg)
	{
		if (this.sendUpgradeInfoCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.sendUpgradeInfoCompleted(this, new AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OnsendUsageInfoOperationCompleted(object arg)
	{
		if (this.sendUsageInfoCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.sendUsageInfoCompleted(this, new AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	private void OntestOperationCompleted(object arg)
	{
		if (this.testCompleted != null)
		{
			InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
			this.testCompleted(this, new testCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
		}
	}

	[SoapRpcMethod("urn:svnmonitor#sendErrorReport", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public int sendErrorReport(string id, string report)
	{
		object[] objArray;
		object[] results = base.Invoke("sendErrorReport", new object[] { id, report });
		return (int)results[0];
	}

	[SoapRpcMethod("urn:svnmonitor#sendErrorReport_ver1", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public int sendErrorReport_ver1(string id, string version, string report)
	{
		object[] objArray;
		object[] results = base.Invoke("sendErrorReport_ver1", new object[] { id, version, report });
		return (int)results[0];
	}

	public void sendErrorReport_ver1Async(string id, string version, string report)
	{
		this.sendErrorReport_ver1Async(id, version, report, null);
	}

	public void sendErrorReport_ver1Async(string id, string version, string report, object userState)
	{
		object[] objArray;
		if (this.sendErrorReport_ver1OperationCompleted == null)
		{
			this.sendErrorReport_ver1OperationCompleted = new SendOrPostCallback(this.OnsendErrorReport_ver1OperationCompleted);
		}
		base.InvokeAsync("sendErrorReport_ver1", new object[] { id, version, report }, this.sendErrorReport_ver1OperationCompleted, userState);
	}

	[SoapRpcMethod("urn:svnmonitor#sendErrorReport_ver2", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public int sendErrorReport_ver2(string id, string version, string name, string email, string note, string report)
	{
		object[] objArray;
		object[] results = base.Invoke("sendErrorReport_ver2", new object[] { id, version, name, email, note, report });
		return (int)results[0];
	}

	public void sendErrorReport_ver2Async(string id, string version, string name, string email, string note, string report)
	{
		this.sendErrorReport_ver2Async(id, version, name, email, note, report, null);
	}

	public void sendErrorReport_ver2Async(string id, string version, string name, string email, string note, string report, object userState)
	{
		object[] objArray;
		if (this.sendErrorReport_ver2OperationCompleted == null)
		{
			this.sendErrorReport_ver2OperationCompleted = new SendOrPostCallback(this.OnsendErrorReport_ver2OperationCompleted);
		}
		base.InvokeAsync("sendErrorReport_ver2", new object[] { id, version, name, email, note, report }, this.sendErrorReport_ver2OperationCompleted, userState);
	}

	public void sendErrorReportAsync(string id, string report)
	{
		this.sendErrorReportAsync(id, report, null);
	}

	public void sendErrorReportAsync(string id, string report, object userState)
	{
		object[] objArray;
		if (this.sendErrorReportOperationCompleted == null)
		{
			this.sendErrorReportOperationCompleted = new SendOrPostCallback(this.OnsendErrorReportOperationCompleted);
		}
		base.InvokeAsync("sendErrorReport", new object[] { id, report }, this.sendErrorReportOperationCompleted, userState);
	}

	[SoapRpcMethod("urn:svnmonitor#sendFeedback", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public int sendFeedback(string id, string name, string email, string note)
	{
		object[] objArray;
		object[] results = base.Invoke("sendFeedback", new object[] { id, name, email, note });
		return (int)results[0];
	}

	[SoapRpcMethod("urn:svnmonitor#sendFeedback_ver1", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public int sendFeedback_ver1(string id, string version, string name, string email, string note)
	{
		object[] objArray;
		object[] results = base.Invoke("sendFeedback_ver1", new object[] { id, version, name, email, note });
		return (int)results[0];
	}

	public void sendFeedback_ver1Async(string id, string version, string name, string email, string note)
	{
		this.sendFeedback_ver1Async(id, version, name, email, note, null);
	}

	public void sendFeedback_ver1Async(string id, string version, string name, string email, string note, object userState)
	{
		object[] objArray;
		if (this.sendFeedback_ver1OperationCompleted == null)
		{
			this.sendFeedback_ver1OperationCompleted = new SendOrPostCallback(this.OnsendFeedback_ver1OperationCompleted);
		}
		base.InvokeAsync("sendFeedback_ver1", new object[] { id, version, name, email, note }, this.sendFeedback_ver1OperationCompleted, userState);
	}

	[SoapRpcMethod("urn:svnmonitor#sendFeedback_ver1", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public int sendFeedback_ver2(string id, string version, string name, string email, string note, string info)
	{
		object[] objArray;
		object[] results = base.Invoke("sendFeedback_ver2", new object[] { id, version, name, email, note, info });
		return (int)results[0];
	}

	public void sendFeedback_ver2Async(string id, string version, string name, string email, string note, string info)
	{
		this.sendFeedback_ver2Async(id, version, name, email, note, info, null);
	}

	public void sendFeedback_ver2Async(string id, string version, string name, string email, string note, string info, object userState)
	{
		object[] objArray;
		if (this.sendFeedback_ver2OperationCompleted == null)
		{
			this.sendFeedback_ver2OperationCompleted = new SendOrPostCallback(this.OnsendFeedback_ver2OperationCompleted);
		}
		base.InvokeAsync("sendFeedback_ver2", new object[] { id, version, name, email, note, info }, this.sendFeedback_ver2OperationCompleted, userState);
	}

	public void sendFeedbackAsync(string id, string name, string email, string note)
	{
		this.sendFeedbackAsync(id, name, email, note, null);
	}

	public void sendFeedbackAsync(string id, string name, string email, string note, object userState)
	{
		object[] objArray;
		if (this.sendFeedbackOperationCompleted == null)
		{
			this.sendFeedbackOperationCompleted = new SendOrPostCallback(this.OnsendFeedbackOperationCompleted);
		}
		base.InvokeAsync("sendFeedback", new object[] { id, name, email, note }, this.sendFeedbackOperationCompleted, userState);
	}

	[SoapRpcMethod("urn:svnmonitor#sendUpgradeInfo", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public void sendUpgradeInfo(string id, string version)
	{
		object[] objArray;
		base.Invoke("sendUpgradeInfo", new object[] { id, version });
	}

	public void sendUpgradeInfoAsync(string id, string version)
	{
		this.sendUpgradeInfoAsync(id, version, null);
	}

	public void sendUpgradeInfoAsync(string id, string version, object userState)
	{
		object[] objArray;
		if (this.sendUpgradeInfoOperationCompleted == null)
		{
			this.sendUpgradeInfoOperationCompleted = new SendOrPostCallback(this.OnsendUpgradeInfoOperationCompleted);
		}
		base.InvokeAsync("sendUpgradeInfo", new object[] { id, version }, this.sendUpgradeInfoOperationCompleted, userState);
	}

	[SoapRpcMethod("urn:svnmonitor#sendUsageInfo", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public void sendUsageInfo(string info)
	{
		object[] objArray;
		base.Invoke("sendUsageInfo", new object[] { info });
	}

	public void sendUsageInfoAsync(string info)
	{
		this.sendUsageInfoAsync(info, null);
	}

	public void sendUsageInfoAsync(string info, object userState)
	{
		object[] objArray;
		if (this.sendUsageInfoOperationCompleted == null)
		{
			this.sendUsageInfoOperationCompleted = new SendOrPostCallback(this.OnsendUsageInfoOperationCompleted);
		}
		base.InvokeAsync("sendUsageInfo", new object[] { info }, this.sendUsageInfoOperationCompleted, userState);
	}

	[SoapRpcMethod("urn:svnmonitor#test", RequestNamespace="urn:svnmonitor", ResponseNamespace="urn:svnmonitor")]
	public string test()
	{
		object[] results = base.Invoke("test", new object[0]);
		return (string)results[0];
	}

	public void testAsync()
	{
		this.testAsync(null);
	}

	public void testAsync(object userState)
	{
		if (this.testOperationCompleted == null)
		{
			this.testOperationCompleted = new SendOrPostCallback(this.OntestOperationCompleted);
		}
		base.InvokeAsync("test", new object[0], this.testOperationCompleted, userState);
	}

	public event getKnownIssues_ver1CompletedEventHandler getKnownIssues_ver1Completed;
	public event getKnownIssues_ver2CompletedEventHandler getKnownIssues_ver2Completed;
	public event sendErrorReport_ver1CompletedEventHandler sendErrorReport_ver1Completed;
	public event sendErrorReport_ver2CompletedEventHandler sendErrorReport_ver2Completed;
	public event sendErrorReportCompletedEventHandler sendErrorReportCompleted;
	public event sendFeedback_ver1CompletedEventHandler sendFeedback_ver1Completed;
	public event sendFeedback_ver2CompletedEventHandler sendFeedback_ver2Completed;
	public event sendFeedbackCompletedEventHandler sendFeedbackCompleted;
	public event sendUpgradeInfoCompletedEventHandler sendUpgradeInfoCompleted;
	public event sendUsageInfoCompletedEventHandler sendUsageInfoCompleted;
	public event testCompletedEventHandler testCompleted;
}
}