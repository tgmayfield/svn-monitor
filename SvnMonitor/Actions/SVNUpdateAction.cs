using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

using SVNMonitor.Entities;
using SVNMonitor.Resources;
using SVNMonitor.SVN;

namespace SVNMonitor.Actions
{
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
			Path = rejectionPath;
			UserName = rejectionUserName;
			Password = rejectionPassword;
		}

		protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
		{
			EventLog.Log(EventLogEntryType.Monitor, string.Format("Updating '{0}'...", Path), Path);
			SVNFactory.Update(Path, UserName, Password);
		}

		public override void SetRejectionPoint()
		{
			rejectionPath = Path;
			rejectionUserName = UserName;
			rejectionPassword = Password;
		}

		public override bool CanBeTested
		{
			get { return false; }
		}

		public override bool IsValid
		{
			get
			{
				if (string.IsNullOrEmpty(Path))
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
			get { return string.Format("SVN-Update folder '{0}' (Notice: The folder needs to be a checked-out folder).", Path); }
		}

		[DisplayName("User Name"), Description("Your user-name, if required by the SVN repository."), Category("SVN Update")]
		public string UserName { get; set; }
	}
}