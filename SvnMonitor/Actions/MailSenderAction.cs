using SVNMonitor.Entities;

namespace SVNMonitor.Actions
{
    using SVNMonitor;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing.Design;
    using System.Net;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;

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
            this.Subject = "SVN-Monitor Update";
            this.Port = 0x19;
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
            MailMessage tempLocal0 = new MailMessage {
                From = new MailAddress(this.From),
                Subject = this.Subject,
                Body = logEntries[0].Message
            };
            MailMessage message = tempLocal0;
            message.To.Add(this.To);
            SmtpClient tempLocal1 = new SmtpClient {
                Host = this.SmtpHost,
                Port = this.Port
            };
            SmtpClient smtp = tempLocal1;
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

        [Description("The sender's email address."), Category("Message"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string From { get; set; }

        public override bool IsValid
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
            get
            {
                return string.Format("Send mail to {0}.", this.To);
            }
        }

        [Description("The recipient's email address. Might be a list of comma-separated addresses."), Category("Message"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string To { get; set; }

        [DisplayName("User Name"), Category("SMTP"), Description("Your user-name when connecting to the SMTP host.")]
        public string UserName { get; set; }
    }
}

