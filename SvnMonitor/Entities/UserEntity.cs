namespace SVNMonitor.Entities
{
    using SVNMonitor;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class UserEntity : VersionEntity, IComparable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool enabled = true;
        [NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long errorEventID;
        [NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string errorText;
        private string guid;
        [NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool hasError;
        [NonSerialized]
        private bool isInEditMode;
        [NonSerialized]
        private bool rejectionEnabled;
        [NonSerialized]
        private string rejectionName;
        private bool saved;

        [field: NonSerialized]
        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        public UserEntity()
        {
            this.GenerateGuid();
        }

        internal virtual void BeginEdit()
        {
            this.SetRejectionPoint();
            this.isInEditMode = true;
        }

        internal virtual void ClearError()
        {
            if (this.HasError)
            {
                Logger.Log.InfoFormat("Error cleared: {0}", this.ErrorText);
                this.ErrorText = null;
                this.ErrorEventID = 0L;
                this.HasError = false;
            }
        }

        public int CompareTo(object obj)
        {
            UserEntity entity = (UserEntity) obj;
            return this.OrderNumber.CompareTo(entity.OrderNumber);
        }

        internal virtual void DeleteFile()
        {
            Logger.Log.DebugFormat("Deleting file: {0}", this.FileName);
            FileSystemHelper.DeleteFile(this.FileName);
        }

        internal virtual void EndEdit()
        {
            this.isInEditMode = false;
        }

        internal void GenerateGuid()
        {
            this.guid = System.Guid.NewGuid().ToString();
        }

        public static T Load<T>(string fileName, bool save) where T: UserEntity
        {
            T entity = (T) SerializationHelper.BinaryDeserialize(fileName);
            entity.Upgrade();
            if (save)
            {
                entity.Save();
            }
            return entity;
        }

        protected virtual void OnStatusChanged(StatusChangedReason reason)
        {
            if (Status.Closing)
            {
                Status.OnCanExit();
            }
            else
            {
                if (this.StatusChanged != null)
                {
                    this.StatusChanged(this, new StatusChangedEventArgs(this, reason));
                }
                Status.OnStatusChanged();
            }
        }

        internal virtual void RejectChanges()
        {
            this.Enabled = this.rejectionEnabled;
            this.Name = this.rejectionName;
        }

        internal virtual void Save()
        {
            this.Save(this.FileName);
        }

        internal virtual void Save(string fileName)
        {
            Logger.Log.DebugFormat("BinarySerialize(FileName={0})", this.FileName);
            SerializationHelper.BinarySerialize(this, fileName);
            this.saved = true;
        }

        internal virtual void SaveExisting()
        {
            if (FileSystemHelper.FileExists(this.FileName))
            {
                this.Save();
            }
            else
            {
                Logger.Log.InfoFormat("{0} does not exist.", this.FileName);
            }
        }

        internal virtual void SetError(string errorText, long errorEventID)
        {
            this.ErrorText = errorText;
            this.ErrorEventID = errorEventID;
            this.HasError = true;
        }

        internal virtual void SetRejectionPoint()
        {
            this.rejectionEnabled = this.Enabled;
            this.rejectionName = this.Name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public virtual bool Enabled
        {
            [DebuggerNonUserCode]
            get
            {
                return this.enabled;
            }
            set
            {
                if (this.enabled != value)
                {
                    this.enabled = value;
                    Logger.Log.InfoFormat("Set {0} = {1}", this.Name, value);
                    this.SaveExisting();
                    if (!value)
                    {
                        this.ClearError();
                    }
                    this.OnStatusChanged(StatusChangedReason.Enabled);
                }
            }
        }

        [XmlIgnore]
        public long ErrorEventID
        {
            [DebuggerNonUserCode]
            get
            {
                return this.errorEventID;
            }
            [DebuggerNonUserCode]
            protected set
            {
                this.errorEventID = value;
            }
        }

        [XmlIgnore]
        public string ErrorText
        {
            [DebuggerNonUserCode]
            get
            {
                return this.errorText;
            }
            [DebuggerNonUserCode]
            protected set
            {
                this.errorText = value;
            }
        }

        public abstract string FileExtension { get; }

        protected string FileName
        {
            get
            {
                return (Path.Combine(FileSystemHelper.AppData, this.Guid) + this.FileExtension);
            }
        }

        public string Guid
        {
            [DebuggerNonUserCode]
            get
            {
                return this.guid;
            }
        }

        [XmlIgnore]
        public bool HasError
        {
            [DebuggerNonUserCode]
            get
            {
                return this.hasError;
            }
            protected set
            {
                if (this.hasError != value)
                {
                    this.hasError = value;
                    this.OnStatusChanged(StatusChangedReason.HasError);
                }
            }
        }

        public abstract bool IsAlive { get; }

        [Browsable(false)]
        public bool IsInEditMode
        {
            [DebuggerNonUserCode]
            get
            {
                return this.isInEditMode;
            }
        }

        public string Name { get; set; }

        public int OrderNumber { get; set; }

        public bool Saved
        {
            [DebuggerNonUserCode]
            get
            {
                return this.saved;
            }
        }
    }
}

