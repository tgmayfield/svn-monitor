using System;
using System.Diagnostics;
using SVNMonitor.Logging;
using System.Xml.Serialization;
using System.IO;
using SVNMonitor.Helpers;
using System.ComponentModel;
using SVNMonitor;

namespace SVNMonitor.Entities
{
[Serializable]
public abstract class UserEntity : VersionEntity, IComparable
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private bool enabled;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[NonSerialized]
	private long errorEventID;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[NonSerialized]
	private string errorText;

	private string guid;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[NonSerialized]
	private bool hasError;

	[NonSerialized]
	private bool isInEditMode;

	[NonSerialized]
	private bool rejectionEnabled;

	[NonSerialized]
	private string rejectionName;

	private bool saved;

	[NonSerialized]
	private EventHandler<StatusChangedEventArgs> statusChanged;

	public bool Enabled
	{
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
		get
		{
			return this.errorEventID;
		}
		protected set
		{
			this.errorEventID = value;
		}
	}

	[XmlIgnore]
	public string ErrorText
	{
		get
		{
			return this.errorText;
		}
		protected set
		{
			this.errorText = value;
		}
	}

	public string FileExtension { get; }

	protected string FileName
	{
		get
		{
			string fileName = string.Concat(Path.Combine(FileSystemHelper.AppData, this.Guid), this.FileExtension);
			return fileName;
		}
	}

	public string Guid
	{
		get
		{
			return this.guid;
		}
	}

	[XmlIgnore]
	public bool HasError
	{
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

	public bool IsAlive { get; }

	[Browsable(false)]
	public bool IsInEditMode
	{
		get
		{
			return this.isInEditMode;
		}
	}

	public string Name
	{
		get;
		set;
	}

	public int OrderNumber
	{
		get;
		set;
	}

	public bool Saved
	{
		get
		{
			return this.saved;
		}
	}

	public UserEntity()
	{
		this.enabled = true;
		base();
		this.GenerateGuid();
	}

	internal virtual void BeginEdit()
	{
		this.SetRejectionPoint();
		this.isInEditMode = true;
	}

	internal virtual void ClearError()
	{
		if (!this.HasError)
		{
			return;
		}
		Logger.Log.InfoFormat("Error cleared: {0}", this.ErrorText);
		this.ErrorText = null;
		this.ErrorEventID = (long)0;
		this.HasError = false;
	}

	public int CompareTo(object obj)
	{
		UserEntity entity = (UserEntity)obj;
		int orderNumber = this.OrderNumber;
		return orderNumber.CompareTo(entity.OrderNumber);
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
		Guid guid = Guid.NewGuid().guid = guid.ToString();
	}

	public static T Load<T>(string fileName, bool save)
	{
		T entity = (T)SerializationHelper.BinaryDeserialize(fileName);
		&entity.Upgrade();
		if (save)
		{
			&entity.Save();
		}
		return entity;
	}

	protected virtual void OnStatusChanged(StatusChangedReason reason)
	{
		if (Status.Closing)
		{
			Status.OnCanExit();
			return;
		}
		if (this.statusChanged != null)
		{
			this.statusChanged(this, new StatusChangedEventArgs(this, reason));
		}
		Status.OnStatusChanged();
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
			return;
		}
		Logger.Log.InfoFormat("{0} does not exist.", this.FileName);
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

	public event EventHandler<StatusChangedEventArgs> StatusChanged;
}
}