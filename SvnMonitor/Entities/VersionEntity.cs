using System;
using System.Diagnostics;
using System.Xml.Serialization;
using SVNMonitor.Helpers;

namespace SVNMonitor.Entities
{
[Serializable]
public abstract class VersionEntity
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Version version;

	[XmlIgnore]
	public Version Version
	{
		get
		{
			return this.version;
		}
		private set
		{
			this.version = value;
		}
	}

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
}
}