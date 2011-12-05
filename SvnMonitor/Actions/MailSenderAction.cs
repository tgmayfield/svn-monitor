using SVNMonitor.Resources;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using SVNMonitor.Logging;
using SVNMonitor;

namespace SVNMonitor.Actions
{
[ResourceProvider("Send an email")]
[Serializable]
internal class MailSenderAction : Action
{
	[NonSerialized]
	private string rejectionFrom;

	[NonSerialized]
	private string rejectionPassword;

	[NonSerialized]
	private int rejectionPort;

	[NonSerialized]
	private string rejectionSmtpHost;

	[NonSerialized]
	private string rejectionSubject;

	[NonSerialized]
	private string rejectionTo;

	[NonSerialized]
	private string rejectionUserName;

	[Description("The sender's email address.")]
	[Category("Message")]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	public string From
	{
		get;
		set;
	}

	public bool IsValid
	{
		get
		{
			if (string.IsNullOrEmpty(this.To))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.From))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.SmtpHost))
			{
				return false;
			}
			return true;
		}
	}

	[PasswordPropertyText(true)]
	[Description("Your password when connecting to the SMTP host.")]
	[Category("SMTP")]
	public string Password
	{
		get;
		set;
	}

	[DefaultValue(25)]
	[Category("SMTP")]
	[Description("The port of your SMTP host.")]
	public int Port
	{
		get;
		set;
	}

	[DisplayName("Host")]
	[Category("SMTP")]
	public string SmtpHost
	{
		get;
		set;
	}

	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[Category("Message")]
	[Description("The email's subject.")]
	public string Subject
	{
		get;
		set;
	}

	public string SummaryInfo
	{
		get
		{
			return string.Format("Send mail to {0}.", this.To);
		}
	}

	[Description("The recipient's email address. Might be a list of comma-separated addresses.")]
	[Category("Message")]
	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	public string To
	{
		get;
		set;
	}

	[DisplayName("User Name")]
	[Category("SMTP")]
	[Description("Your user-name when connecting to the SMTP host.")]
	public string UserName
	{
		get;
		set;
	}

	public MailSenderAction()
	{
		this.Subject = "SVN-Monitor Update";
		this.Port = 25;
	}

	public override void RejectChanges()
	{
		this.From = this.rejectionFrom;
		this.Password = this.rejectionPassword;
		this.Port = this.rejectionPort;
		this.SmtpHost = this.rejectionSmtpHost;
		this.Subject = this.rejectionSubject;
		this.To = this.rejectionTo;
		this.UserName = this.rejectionUserName;
	}

	protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		object[] objArray;
		MailMessage mailMessage = new MailMessage();
		mailMessage.From = new MailAddress(this.From);
		mailMessage.Subject = this.Subject;
		mailMessage.Body = logEntries[0].Message;
		MailMessage message = mailMessage;
		message.To.Add(this.To);
		SmtpClient smtpClient = new SmtpClient();
		smtpClient.Host = this.SmtpHost;
		smtpClient.Port = this.Port;
		SmtpClient smtp = smtpClient;
		if (!string.IsNullOrEmpty(this.UserName))
		{
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new NetworkCredential(this.UserName, this.Password);
		}
		Logger.Log.DebugFormat("From={0}, Subject={1}, To={2}, SmtpHost={3}, Port={4}, smtp.UseDefaultCredentials={5}", new object[] { this.From, this.Subject, this.To, this.SmtpHost, this.Port, smtp.UseDefaultCredentials });
		EventLog.Log(EventLogEntryType.Monitor, string.Format("Sending mail to '{0}'.", this.To), this);
		smtp.Send(message);
	}

	public override void SetRejectionPoint()
	{
		this.rejectionFrom = this.From;
		this.rejectionPassword = this.Password;
		this.rejectionPort = this.Port;
		this.rejectionSmtpHost = this.SmtpHost;
		this.rejectionSubject = this.Subject;
		this.rejectionTo = this.To;
		this.rejectionUserName = this.UserName;
	}
}
}