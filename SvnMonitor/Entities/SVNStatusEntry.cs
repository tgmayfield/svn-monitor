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

		internal SVNStatusEntry(SVNStatus parent)
		{
			ParentSVNStatus = parent;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is SVNStatusEntry))
			{
				return false;
			}
			SVNStatusEntry that = (SVNStatusEntry)obj;
			if (Path != that.Path)
			{
				return false;
			}
			if (WorkingCopyStatus != that.WorkingCopyStatus)
			{
				return false;
			}
			if (WorkingCopyRevision != that.WorkingCopyRevision)
			{
				return false;
			}
			if (RepositoryStatus != that.RepositoryStatus)
			{
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", WorkingCopyRevision, Path);
		}

		public string ChangeList { get; set; }

		public bool GlobalIgnore
		{
			get
			{
				bool ignore = false;
				try
				{
					ignore = TortoiseSVNHelper.IsInGlogalIgnorePattern(Path);
				}
				catch (Exception ex)
				{
					Logger.Log.Error(string.Format("Error checking the global-ignore-pattern for '{0}'.", Path), ex);
				}
				return ignore;
			}
		}

		public bool IgnoreOnCommit
		{
			get
			{
				if ((ParentSVNStatus != null) && ParentSVNStatus.ChangeLists.ContainsKey("ignore-on-commit"))
				{
					SVNChangeList list = ParentSVNStatus.ChangeLists["ignore-on-commit"];
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
				if (IgnoreOnCommit && ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommit)
				{
					return false;
				}
				return ModifiedNoIgnore;
			}
		}

		public bool ModifiedForConflict
		{
			get
			{
				if (IgnoreOnCommit && ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommitConflicts)
				{
					return false;
				}
				return ModifiedNoIgnore;
			}
		}

		public bool ModifiedNoIgnore
		{
			get
			{
				if (((WorkingCopyStatus != SvnStatus.Modified) && (WorkingCopyStatus != SvnStatus.Deleted)) && (WorkingCopyStatus != SvnStatus.Missing))
				{
					return (WorkingCopyStatus == SvnStatus.Added);
				}
				return true;
			}
		}

		public bool ModifiedOrUnversioned
		{
			get
			{
				if (!ApplicationSettingsManager.Settings.TreatUnversionedAsModified)
				{
					return Modified;
				}
				if (!Modified)
				{
					return Unversioned;
				}
				return true;
			}
		}

		public bool ModifiedOrUnversionedForConflict
		{
			get
			{
				if (!ApplicationSettingsManager.Settings.TreatUnversionedAsModified)
				{
					return ModifiedForConflict;
				}
				if (!ModifiedForConflict)
				{
					return Unversioned;
				}
				return true;
			}
		}

		public bool ModifiedOrUnversionedNoIgnore
		{
			get
			{
				if (!ApplicationSettingsManager.Settings.TreatUnversionedAsModified)
				{
					return ModifiedNoIgnore;
				}
				if (!ModifiedNoIgnore)
				{
					return Unversioned;
				}
				return true;
			}
		}

		public SVNStatus ParentSVNStatus { get; private set; }

		public string Path { get; set; }

		public SvnStatus RepositoryStatus { get; set; }

		public bool Unversioned
		{
			get { return ((WorkingCopyStatus == SvnStatus.NotVersioned) && !GlobalIgnore); }
		}

		public string Uri { get; set; }

		public bool Versioned
		{
			get { return !Unversioned; }
		}

		public long WorkingCopyRevision { get; set; }

		public SvnStatus WorkingCopyStatus { get; set; }
	}
}