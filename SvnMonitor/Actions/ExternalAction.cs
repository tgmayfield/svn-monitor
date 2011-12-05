using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;

namespace SVNMonitor.Actions
{
	[Serializable, ResourceProvider("Run an external program")]
	internal class ExternalAction : Action
	{
		[NonSerialized]
		private string rejectionArguments;
		[NonSerialized]
		private string rejectionFileName;
		[NonSerialized]
		private int rejectionTimeout;
		[NonSerialized]
		private string rejectionWorkingDirectory;

		public override void RejectChanges()
		{
			FileName = rejectionFileName;
			Arguments = rejectionArguments;
			WorkingDirectory = rejectionWorkingDirectory;
			Timeout = rejectionTimeout;
		}

		protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
		{
			Logger.Log.DebugFormat("FileName={0}, Arguments={1}, WorkingDirectory={2}", FileName, Arguments, WorkingDirectory);
			ProcessHelper.StreamProcess(FileName, Arguments, WorkingDirectory, Timeout);
		}

		public override void SetRejectionPoint()
		{
			rejectionFileName = FileName;
			rejectionArguments = Arguments;
			rejectionWorkingDirectory = WorkingDirectory;
			rejectionTimeout = Timeout;
		}

		[Description("Arguments to pass to the file."), Category("File")]
		public string Arguments { get; set; }

		[DisplayName("File Name"), Category("File"), Editor(typeof(FileNameEditor), typeof(UITypeEditor)), Description("Name of the file to run.")]
		public string FileName { get; set; }

		public override bool IsValid
		{
			get
			{
				if (string.IsNullOrEmpty(FileName))
				{
					return false;
				}
				return true;
			}
		}

		public override string SummaryInfo
		{
			get
			{
				string workDirString = string.Empty;
				if (!string.IsNullOrEmpty(WorkingDirectory))
				{
					workDirString = string.Format(" in {0}", WorkingDirectory);
				}
				string argsString = string.Empty;
				if (!string.IsNullOrEmpty(Arguments))
				{
					argsString = string.Format(" {0}", Arguments);
				}
				return string.Format("Run \"{0}{1}\"{2}.", FileName, argsString, workDirString);
			}
		}

		[Description("Timeout, in milliseconds, before the process is terminated (0 = no timeout)."), Category("File"), DisplayName("Timeout")]
		public int Timeout { get; set; }

		[Description("The directory where the file will run."), Category("File"), DisplayName("Working Directory")]
		public string WorkingDirectory { get; set; }
	}
}