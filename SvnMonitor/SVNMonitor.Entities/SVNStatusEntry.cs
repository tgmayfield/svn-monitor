using System;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Settings;
using SharpSvn;

namespace SVNMonitor.Entities
{
[Serializable]
public class SVNStatusEntry
{
	private const string IgnoreOnCommitName = "ignore-on-commit";

	public string ChangeList
	{
		get;
		set;
	}

	public bool GlobalIgnore
	{
		get
		{
			bool ignore = false;
			try
			{
				return TortoiseSVNHelper.IsInGlogalIgnorePattern(this.Path);
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Error checking the global-ignore-pattern for '{0}'.", this.Path), ex);
			}
			return ignore;
		}
	}

	public bool IgnoreOnCommit
	{
		get
		{
			if (this.ParentSVNStatus == null)
			{
				return false;
			}
			if (this.ParentSVNStatus.ChangeLists.ContainsKey("ignore-on-commit"))
			{
				SVNChangeList list = this.ParentSVNStatus.ChangeLists["ignore-on-commit"];
				if (list.Contains(this))
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool Modified
	{
		get
		{
			if (this.IgnoreOnCommit && ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommit)
			{
				return false;
			}
			return this.ModifiedNoIgnore;
		}
	}

	public bool ModifiedForConflict
	{
		get
		{
			if (this.IgnoreOnCommit && ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommitConflicts)
			{
				return false;
			}
			return this.ModifiedNoIgnore;
		}
	}

	public bool ModifiedNoIgnore
	{
		get
		{
			if (this.WorkingCopyStatus != SvnStatus.Modified && this.WorkingCopyStatus != SvnStatus.Deleted && this.WorkingCopyStatus != SvnStatus.Missing)
			{
				return this.WorkingCopyStatus == SvnStatus.Added;
			}
			return true;
		}
	}

	public bool ModifiedOrUnversioned
	{
		get
		{
			if (ApplicationSettingsManager.Settings.TreatUnversionedAsModified || !this.Modified)
			{
				return this.Unversioned;
			}
			return true;
			return this.Modified;
		}
	}

	public bool ModifiedOrUnversionedForConflict
	{
		get
		{
			if (ApplicationSettingsManager.Settings.TreatUnversionedAsModified || !this.ModifiedForConflict)
			{
				return this.Unversioned;
			}
			return true;
			return this.ModifiedForConflict;
		}
	}

	public bool ModifiedOrUnversionedNoIgnore
	{
		get
		{
			if (ApplicationSettingsManager.Settings.TreatUnversionedAsModified || !this.ModifiedNoIgnore)
			{
				return this.Unversioned;
			}
			return true;
			return this.ModifiedNoIgnore;
		}
	}

	public SVNStatus ParentSVNStatus
	{
		get;
		private set;
	}

	public string Path
	{
		get;
		set;
	}

	public SvnStatus RepositoryStatus
	{
		get;
		set;
	}

	public bool Unversioned
	{
		get
		{
			if (this.WorkingCopyStatus == SvnStatus.NotVersioned)
			{
				return !this.GlobalIgnore;
			}
			return false;
		}
	}

	public string Uri
	{
		get;
		set;
	}

	public bool Versioned
	{
		get
		{
			return !this.Unversioned;
		}
	}

	public long WorkingCopyRevision
	{
		get;
		set;
	}

	public SvnStatus WorkingCopyStatus
	{
		get;
		set;
	}

	internal SVNStatusEntry(SVNStatus parent)
	{
		this.ParentSVNStatus = parent;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (!obj as SVNStatusEntry)
		{
			return false;
		}
		SVNStatusEntry that = (SVNStatusEntry)obj;
		if (this.Path != that.Path)
		{
			return false;
		}
		if (this.WorkingCopyStatus != that.WorkingCopyStatus)
		{
			return false;
		}
		if (this.WorkingCopyRevision != that.WorkingCopyRevision)
		{
			return false;
		}
		if (this.RepositoryStatus != that.RepositoryStatus)
		{
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return this.GetHashCode();
	}

	public override string ToString()
	{
		return string.Format("{0} {1}", this.WorkingCopyRevision, this.Path);
	}
}
}