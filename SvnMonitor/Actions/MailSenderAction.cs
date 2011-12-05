using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Net;
using System.Net.Mail;

using SVNMonitor.Entities;
using SVNMonitor.Logging;
using SVNMonitor.Resources;

namespace SVNMonitor.Actions
{
	[Serializable, ResourceProvider("Send an email")]
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

		public MailSenderAction()
		{
			Subject = "SVN-Monitor Update";
			Port = 0x19;
		}

		public override void RejectChanges()
		{
			From = rejectionFrom;
			Password = rejectionPassword;
			Port = rejectionPort;
			SmtpHost = rejectionSmtpHost;
			Subject = rejectionSubject;
			To = rejectionTo;
			UserName = rejectionUserName;
		}

		protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
		{
			MailMessage tempLocal0 = new MailMessage
			{
				From = new MailAddress(From),
				Subject = Subject,
				Body = logEntries[0].Message
			};
			MailMessage message = tempLocal0;
			message.To.Add(To);
			SmtpClient tempLocal1 = new SmtpClient
			{
				Host = SmtpHost,
				Port = Port
			};
			SmtpClient smtp = tempLocal1;
			if (!string.IsNullOrEmpty(UserName))
			{
				smtp.UseDefaultCredentials = false;
				smtp.Credentials = new NetworkCredential(UserName, Password);
			}
			Logger.Log.DebugFormat("From={0}, Subject={1}, To={2}, SmtpHost={3}, Port={4}, smtp.UseDefaultCredentials={5}", new object[]
			{
				From, Subject, To, SmtpHost, Port, smtp.UseDefaultCredentials
			});
			EventLog.Log(EventLogEntryType.Monitor, string.Format("Sending mail to '{0}'.", To), this);
			smtp.Send(message);
		}

		public override void SetRejectionPoint()
		{
			rejectionFrom = From;
			rejectionPassword = Password;
			rejectionPort = Port;
			rejectionSmtpHost = SmtpHost;
			rejectionSubject = Subject;
			rejectionTo = To;
			rejectionUserName = UserName;
		}

		[Description("The sender's email address."), Category("Message"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string From { get; set; }

		public override bool IsValid
		{
			get
			{
				if (string.IsNullOrEmpty(To))
				{
					return false;
				}
				if (string.IsNullOrEmpty(From))
				{
					return false;
				}
				if (string.IsNullOrEmpty(SmtpHost))
				{
					return false;
				}
				return true;
			}
		}

		[PasswordPropertyText(true), Description("Your password when connecting to the SMTP host."), Category("SMTP")]
		public string Password { get; set; }

		[DefaultValue(0x19), Category("SMTP"), Description("The port of your SMTP host.")]
		public int Port { get; set; }

		[DisplayName("Host"), Category("SMTP")]
		public string SmtpHost { get; set; }

		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), Category("Message"), Description("The email's subject.")]
		public string Subject { get; set; }

		public override string SummaryInfo
		{
			get { return string.Format("Send mail to {0}.", To); }
		}

		[Description("The recipient's email address. Might be a list of comma-separated addresses."), Category("Message"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string To { get; set; }

		[DisplayName("User Name"), Category("SMTP"), Description("Your user-name when connecting to the SMTP host.")]
		public string UserName { get; set; }
	}
}