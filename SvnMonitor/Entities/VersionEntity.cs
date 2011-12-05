namespace SVNMonitor.Entities
{
    using SVNMonitor.Helpers;
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class VersionEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private System.Version version;

        protected VersionEntity()
        {
            this.SetCurrentVersion();
        }

        protected virtual void AfterUpgrade()
        {
        }

        protected virtual void BeforeUpgrade()
        {
        }

        private void SetCurrentVersion()
        {
            this.Version = FileSystemHelper.CurrentVersion;
        }

        internal void Upgrade()
        {
            this.BeforeUpgrade();
            this.SetCurrentVersion();
            this.AfterUpgrade();
        }

        [XmlIgnore]
        public System.Version Version
        {
            [DebuggerNonUserCode]
            get
            {
                return this.version;
            }
            [DebuggerNonUserCode]
            private set
            {
                this.version = value;
            }
        }
    }
}

