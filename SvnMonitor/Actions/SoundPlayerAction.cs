namespace SVNMonitor.Actions
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms.Design;

    [Serializable, ResourceProvider("Play a sound (experimental)")]
    internal class SoundPlayerAction : Action
    {
        [NonSerialized]
        private string rejectionFileName;

        public override void RejectChanges()
        {
            this.MediaFileName = this.rejectionFileName;
        }

        protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
        {
            SoundPlayerHelper.Play(this.MediaFileName);
        }

        public override void SetRejectionPoint()
        {
            this.rejectionFileName = this.MediaFileName;
        }

        public override bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.MediaFileName))
                {
                    return false;
                }
                return true;
            }
        }

        [Description("The sound file name to be played."), Editor(typeof(FileNameEditor), typeof(UITypeEditor)), Category("Sound Player"), DisplayName("Sound File")]
        public string MediaFileName { get; set; }

        public override string SummaryInfo
        {
            get
            {
                return string.Format("Play a sound file:{0}{1}.", Environment.NewLine, this.MediaFileName);
            }
        }
    }
}

