namespace SVNMonitor.Actions
{
    using SVNMonitor;
    using SVNMonitor.Resources;
    using SVNMonitor.SVN;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms.Design;

    [Serializable, ResourceProvider("SVN-Update a checked-out folder")]
    internal class SVNUpdateAction : Action
    {
        [NonSerialized]
        private string rejectionPassword;
        [NonSerialized]
        private string rejectionPath;
        [NonSerialized]
        private string rejectionUserName;

        public override void RejectChanges()
        {
            this.Path = this.rejectionPath;
            this.UserName = this.rejectionUserName;
            this.Password = this.rejectionPassword;
        }

        protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
        {
            EventLog.Log(EventLogEntryType.Monitor, string.Format("Updating '{0}'...", this.Path), this.Path);
            SVNFactory.Update(this.Path, this.UserName, this.Password);
        }

        public override void SetRejectionPoint()
        {
            this.rejectionPath = this.Path;
            this.rejectionUserName = this.UserName;
            this.rejectionPassword = this.Password;
        }

        public override bool CanBeTested
        {
            get
            {
                return false;
            }
        }

        public override bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Path))
                {
                    return false;
                }
                return true;
            }
        }

        [Category("SVN Update"), PasswordPropertyText(true), Description("Your password, if required by the SVN repository.")]
        public string Password { get; set; }

        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor)), Description("A checked-out path to update."), Category("SVN Update")]
        public string Path { get; set; }

        public override string SummaryInfo
        {
            get
            {
                return string.Format("SVN-Update folder '{0}' (Notice: The folder needs to be a checked-out folder).", this.Path);
            }
        }

        [DisplayName("User Name"), Description("Your user-name, if required by the SVN repository."), Category("SVN Update")]
        public string UserName { get; set; }
    }
}

