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
		private System.Version version;

		protected VersionEntity()
		{
			SetCurrentVersion();
		}

		protected virtual void AfterUpgrade()
		{
		}

		protected virtual void BeforeUpgrade()
		{
		}

		private void SetCurrentVersion()
		{
			Version = FileSystemHelper.CurrentVersion;
		}

		internal void Upgrade()
		{
			BeforeUpgrade();
			SetCurrentVersion();
			AfterUpgrade();
		}

		[XmlIgnore]
		public System.Version Version
		{
			[DebuggerNonUserCode]
			get { return version; }
			[DebuggerNonUserCode]
			private set { version = value; }
		}
	}
}