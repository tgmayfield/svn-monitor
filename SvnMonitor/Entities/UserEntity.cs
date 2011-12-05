using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;

namespace SVNMonitor.Entities
{
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
			GenerateGuid();
		}

		internal virtual void BeginEdit()
		{
			SetRejectionPoint();
			isInEditMode = true;
		}

		internal virtual void ClearError()
		{
			if (HasError)
			{
				Logger.Log.InfoFormat("Error cleared: {0}", ErrorText);
				ErrorText = null;
				ErrorEventID = 0L;
				HasError = false;
			}
		}

		public int CompareTo(object obj)
		{
			UserEntity entity = (UserEntity)obj;
			return OrderNumber.CompareTo(entity.OrderNumber);
		}

		internal virtual void DeleteFile()
		{
			Logger.Log.DebugFormat("Deleting file: {0}", FileName);
			FileSystemHelper.DeleteFile(FileName);
		}

		internal virtual void EndEdit()
		{
			isInEditMode = false;
		}

		internal void GenerateGuid()
		{
			guid = System.Guid.NewGuid().ToString();
		}

		public static T Load<T>(string fileName, bool save) where T : UserEntity
		{
			T entity = (T)SerializationHelper.BinaryDeserialize(fileName);
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
				if (StatusChanged != null)
				{
					StatusChanged(this, new StatusChangedEventArgs(this, reason));
				}
				Status.OnStatusChanged();
			}
		}

		internal virtual void RejectChanges()
		{
			Enabled = rejectionEnabled;
			Name = rejectionName;
		}

		internal virtual void Save()
		{
			Save(FileName);
		}

		internal virtual void Save(string fileName)
		{
			Logger.Log.DebugFormat("BinarySerialize(FileName={0})", FileName);
			SerializationHelper.BinarySerialize(this, fileName);
			saved = true;
		}

		internal virtual void SaveExisting()
		{
			if (FileSystemHelper.FileExists(FileName))
			{
				Save();
			}
			else
			{
				Logger.Log.InfoFormat("{0} does not exist.", FileName);
			}
		}

		internal virtual void SetError(string errorText, long errorEventID)
		{
			ErrorText = errorText;
			ErrorEventID = errorEventID;
			HasError = true;
		}

		internal virtual void SetRejectionPoint()
		{
			rejectionEnabled = Enabled;
			rejectionName = Name;
		}

		public override string ToString()
		{
			return Name;
		}

		public virtual bool Enabled
		{
			[DebuggerNonUserCode]
			get { return enabled; }
			set
			{
				if (enabled != value)
				{
					enabled = value;
					Logger.Log.InfoFormat("Set {0} = {1}", Name, value);
					SaveExisting();
					if (!value)
					{
						ClearError();
					}
					OnStatusChanged(StatusChangedReason.Enabled);
				}
			}
		}

		[XmlIgnore]
		public long ErrorEventID
		{
			[DebuggerNonUserCode]
			get { return errorEventID; }
			[DebuggerNonUserCode]
			protected set { errorEventID = value; }
		}

		[XmlIgnore]
		public string ErrorText
		{
			[DebuggerNonUserCode]
			get { return errorText; }
			[DebuggerNonUserCode]
			protected set { errorText = value; }
		}

		public abstract string FileExtension { get; }

		protected string FileName
		{
			get { return (Path.Combine(FileSystemHelper.AppData, Guid) + FileExtension); }
		}

		public string Guid
		{
			[DebuggerNonUserCode]
			get { return guid; }
		}

		[XmlIgnore]
		public bool HasError
		{
			[DebuggerNonUserCode]
			get { return hasError; }
			protected set
			{
				if (hasError != value)
				{
					hasError = value;
					OnStatusChanged(StatusChangedReason.HasError);
				}
			}
		}

		public abstract bool IsAlive { get; }

		[Browsable(false)]
		public bool IsInEditMode
		{
			[DebuggerNonUserCode]
			get { return isInEditMode; }
		}

		public string Name { get; set; }

		public int OrderNumber { get; set; }

		public bool Saved
		{
			[DebuggerNonUserCode]
			get { return saved; }
		}
	}
}