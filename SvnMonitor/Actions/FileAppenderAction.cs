namespace SVNMonitor.Actions
{
    using SVNMonitor.Design;
    using SVNMonitor.Resources;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.IO;
    using System.Runtime.CompilerServices;

    [Serializable, ResourceProvider("Write information to a text file")]
    internal class FileAppenderAction : TextAppenderAction
    {
        [NonSerialized]
        private string rejectionFileName;

        protected override void AppendText(string text)
        {
            File.AppendAllText(this.FileName, text);
        }

        public override void RejectChanges()
        {
            this.FileName = this.rejectionFileName;
        }

        public override void SetRejectionPoint()
        {
            this.rejectionFileName = this.FileName;
        }

        [Description("Name of the file to append the text into."), DisplayName("File Name"), Category("File Appender"), Editor(typeof(OptionalFileNameEditor), typeof(UITypeEditor))]
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
                return string.Format("Append log information to: {0}.", this.FileName);
            }
        }
    }
}

