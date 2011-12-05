using SVNMonitor.Entities;

namespace SVNMonitor.Actions
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms.Design;

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
            this.FileName = this.rejectionFileName;
            this.Arguments = this.rejectionArguments;
            this.WorkingDirectory = this.rejectionWorkingDirectory;
            this.Timeout = this.rejectionTimeout;
        }

        protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
        {
            Logger.Log.DebugFormat("FileName={0}, Arguments={1}, WorkingDirectory={2}", this.FileName, this.Arguments, this.WorkingDirectory);
            ProcessHelper.StreamProcess(this.FileName, this.Arguments, this.WorkingDirectory, this.Timeout);
        }

        public override void SetRejectionPoint()
        {
            this.rejectionFileName = this.FileName;
            this.rejectionArguments = this.Arguments;
            this.rejectionWorkingDirectory = this.WorkingDirectory;
            this.rejectionTimeout = this.Timeout;
        }

        [Description("Arguments to pass to the file."), Category("File")]
        public string Arguments { get; set; }

        [DisplayName("File Name"), Category("File"), Editor(typeof(FileNameEditor), typeof(UITypeEditor)), Description("Name of the file to run.")]
        public string FileName { get; set; }

        public override bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.FileName))
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
                if (!string.IsNullOrEmpty(this.WorkingDirectory))
                {
                    workDirString = string.Format(" in {0}", this.WorkingDirectory);
                }
                string argsString = string.Empty;
                if (!string.IsNullOrEmpty(this.Arguments))
                {
                    argsString = string.Format(" {0}", this.Arguments);
                }
                return string.Format("Run \"{0}{1}\"{2}.", this.FileName, argsString, workDirString);
            }
        }

        [Description("Timeout, in milliseconds, before the process is terminated (0 = no timeout)."), Category("File"), DisplayName("Timeout")]
        public int Timeout { get; set; }

        [Description("The directory where the file will run."), Category("File"), DisplayName("Working Directory")]
        public string WorkingDirectory { get; set; }
    }
}

