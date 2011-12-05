using System;
using SVNMonitor.View.Interfaces;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using SVNMonitor.Helpers;
using SVNMonitor;
using System.Xml.Serialization;
using SVNMonitor.Resources.Text;
using SVNMonitor.Logging;
using System.Collections.ObjectModel;
using SVNMonitor.SVN;
using System.Windows.Forms;
using SVNMonitor.View.Dialogs;
using SharpSvn;
using System.Text;
using SVNMonitor.Settings;

namespace SVNMonitor.Entities
{
[Serializable]
public class Source : UserEntity, ISearchable
{
	private bool enableRecommendations;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[NonSerialized]
	private bool hasLog;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private SVNInfo info;

	[NonSerialized]
	private bool? isFileURL;

	[NonSerialized]
	private bool? isURL;

	[NonSerialized]
	private DateTime? lastCheck;

	[NonSerialized]
	private SVNStatus localStatus;

	[NonSerialized]
	private SVNLog log;

	[NonSerialized]
	private int? modifiedCount;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private string path;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[NonSerialized]
	private bool? pathExists;

	[NonSerialized]
	private int? possibleConflictedPathsCount;

	private List<long> recommendedRevisions;

	[NonSerialized]
	private SVNStatus remoteStatus;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private long revision;

	[NonSerialized]
	private AutoResetEvent tortoiseHandle;

	[NonSerialized]
	private int? unreadLogEntriesCount;

	[NonSerialized]
	private int? unreadPathsCount;

	[NonSerialized]
	private int? unreadRecommendedCount;

	[NonSerialized]
	private int? unversionedModifiedCount;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[NonSerialized]
	private bool updating;

	public bool Authenticate
	{
		get;
		set;
	}

	internal string CacheFileName
	{
		get
		{
			string fileName = string.Concat(Path.Combine(FileSystemHelper.AppData, base.Guid), ".cache");
			return fileName;
		}
	}

	public bool CanRecommend
	{
		get
		{
			if (this.IsFileURL)
			{
				return false;
			}
			if (!this.EnableRecommendations)
			{
				return false;
			}
			return true;
		}
	}

	public bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			base.Enabled = value;
			MonitorSettings.Instance.OnSourcesChanged();
		}
	}

	public bool EnableRecommendations
	{
		get
		{
			return this.enableRecommendations;
		}
		set
		{
			this.enableRecommendations = value;
		}
	}

	public string FileExtension
	{
		get
		{
			return ".source";
		}
	}

	public bool HasLocalChanges
	{
		get
		{
			if (this.IsURL)
			{
				return false;
			}
			if (this.LocalStatus == null)
			{
				return false;
			}
			if (this.LocalStatus.GetEnumerableStatusEntries().Any<SVNStatusEntry>(new Predicate<SVNStatusEntry>((e) => e.ModifiedOrUnversioned)))
			{
				return true;
			}
			return false;
		}
	}

	public bool HasLocalVersionedChanges
	{
		get
		{
			if (this.IsURL)
			{
				return false;
			}
			if (this.LocalStatus == null)
			{
				return false;
			}
			if (this.LocalStatus.GetEnumerableStatusEntries().Any<SVNStatusEntry>(new Predicate<SVNStatusEntry>((e) => e.Modified)))
			{
				return true;
			}
			return false;
		}
	}

	public bool HasLog
	{
		get
		{
			return this.hasLog;
		}
	}

	private SVNInfo Info
	{
		get
		{
			return this.info;
		}
		set
		{
			this.info = value;
		}
	}

	public bool IsAlive
	{
		get
		{
			return MonitorSettings.Instance.Sources.Contains(this);
		}
	}

	public bool IsFileURL
	{
		get
		{
			if (!&this.isFileURL.HasValue)
			{
				if (this.Info == null)
				{
					this.Info = this.GetInfo(true);
				}
				this.IsFileURL = (!this.Info ? 0 : FileSystemHelper.IsFileUrl(this.Info.URL));
			}
			return &this.isFileURL.GetValueOrDefault(1);
		}
		set
		{
			this.isFileURL = new bool?(value);
		}
	}

	public bool IsUpToDate
	{
		get
		{
			if (this.IsURL)
			{
				return true;
			}
			if (this.Log == null)
			{
				return true;
			}
			if (this.Log.GetEnumerableLogEntries().Any<SVNLogEntry>(new Predicate<SVNLogEntry>((e) => e.Unread)))
			{
				return false;
			}
			return true;
		}
	}

	public bool IsURL
	{
		get
		{
			if (!&this.isURL.HasValue)
			{
				this.IsURL = (!this.Path ? 0 : FileSystemHelper.IsUrl(this.Path));
			}
			return &this.isURL.GetValueOrDefault(1);
		}
		set
		{
			this.isURL = new bool?(value);
		}
	}

	[XmlIgnore]
	public DateTime? LastCheck
	{
		get
		{
			return this.lastCheck;
		}
		private set
		{
			this.lastCheck = value;
		}
	}

	internal SVNStatus LocalStatus
	{
		get
		{
			return this.localStatus;
		}
		set
		{
			this.localStatus = value;
			this.ResetStatusValues();
			base.OnStatusChanged(StatusChangedReason.SVNStatusChanged);
		}
	}

	private SVNLog Log
	{
		get
		{
			return this.log;
		}
	}

	public int ModifiedCount
	{
		get
		{
			if (&this.modifiedCount.HasValue)
			{
				return &this.modifiedCount.GetValueOrDefault();
			}
			if (this.LocalStatus == null)
			{
				return 0;
			}
			this.modifiedCount = new int?(this.LocalStatus.ModifiedEntries.Count);
			return &this.modifiedCount.GetValueOrDefault();
		}
	}

	public string ModifiedCountString
	{
		get
		{
			object[] objArray;
			int count = this.ModifiedCount;
			if (count == 0)
			{
				return Strings.NoModifiedItems;
			}
			return Strings.ModifiedItems_FORMAT.FormatWith(new object[] { count, (count == 1 ? Strings.ModifiedItems_Item : Strings.ModifiedItems_Items) });
		}
	}

	public string Password
	{
		get;
		set;
	}

	public string Path
	{
		get
		{
			return this.path;
		}
		set
		{
			if (this.path != value && value != null)
			{
				Logger.Log.DebugFormat("ResetInfo(Old path={0}, New path={1})", this.path, value);
				this.Reset();
				this.path = value.Trim();
			}
		}
	}

	public bool PathExists
	{
		get
		{
			if (!&this.pathExists.HasValue)
			{
				this.pathExists = new bool?(FileSystemHelper.Exists(this.Path));
			}
			return &this.pathExists.GetValueOrDefault();
		}
	}

	public int PossibleConflictedFilePathsCount
	{
		get
		{
			if (&this.possibleConflictedPathsCount.HasValue)
			{
				return &this.possibleConflictedPathsCount.GetValueOrDefault();
			}
			if (this.LocalStatus == null || this.Log == null)
			{
				return 0;
			}
			this.possibleConflictedPathsCount = new int?(this.Log.PossibleConflictedFilePaths.Count);
			return &this.possibleConflictedPathsCount.GetValueOrDefault();
		}
	}

	public string PossibleConflictedFilePathsCountString
	{
		get
		{
			object[] objArray;
			int count = this.PossibleConflictedFilePathsCount;
			if (count == 0)
			{
				return Strings.NoPossibleConflicts;
			}
			return Strings.PossibleConflicts_FORMAT.FormatWith(new object[] { count, (count == 1 ? Strings.PossibleConflicts_Conflict : Strings.PossibleConflicts_Conflicts) });
		}
	}

	internal ReadOnlyCollection<long> ReadOnlyRecommendedRevisions
	{
		get
		{
			return this.RecommendedRevisions.AsReadOnly();
		}
	}

	private List<long> RecommendedRevisions
	{
		get
		{
			if (this.recommendedRevisions == null)
			{
				this.recommendedRevisions = new List<long>();
			}
			return this.recommendedRevisions;
		}
		set
		{
			this.recommendedRevisions = value;
		}
	}

	internal SVNStatus RemoteStatus
	{
		get
		{
			return this.remoteStatus;
		}
		set
		{
			this.remoteStatus = value;
			this.ResetStatusValues();
			base.OnStatusChanged(StatusChangedReason.SVNStatusChanged);
		}
	}

	public long Revision
	{
		get
		{
			return this.revision;
		}
		set
		{
			this.revision = value;
		}
	}

	public string UnreadCountString
	{
		get
		{
			int num;
			if (this.UnreadLogEntriesCount == 0)
			{
				return Strings.NoAvailableUpdates;
			}
			return object[] objArray[3] = (this.UnreadPathsCount == 1 ? Strings.AvailableUpdates_Item : Strings.AvailableUpdates_Items).FormatWith(objArray);
		}
	}

	public int UnreadLogEntriesCount
	{
		get
		{
			int _unreadLogEntriesCount = 0;
			int _unreadPathsCount = 0;
			if (&this.unreadLogEntriesCount.HasValue)
			{
				return &this.unreadLogEntriesCount.GetValueOrDefault();
			}
			if (this.Log == null)
			{
				return 0;
			}
			this.CountUnread(out _unreadLogEntriesCount, out _unreadPathsCount);
			this.unreadLogEntriesCount = new int?(_unreadLogEntriesCount);
			this.unreadPathsCount = new int?(_unreadPathsCount);
			return _unreadLogEntriesCount;
		}
	}

	public string UnreadLogEntriesCountString
	{
		get
		{
			if (this.UnreadLogEntriesCount == 0)
			{
				return string.Empty;
			}
			int unreadLogEntriesCount = this.UnreadLogEntriesCount;
			return unreadLogEntriesCount.ToString();
		}
	}

	public int UnreadPathsCount
	{
		get
		{
			int _unreadLogEntriesCount = 0;
			int _unreadPathsCount = 0;
			if (&this.unreadPathsCount.HasValue)
			{
				return &this.unreadPathsCount.GetValueOrDefault();
			}
			if (this.Log == null)
			{
				return 0;
			}
			this.CountUnread(out _unreadLogEntriesCount, out _unreadPathsCount);
			this.unreadLogEntriesCount = new int?(_unreadLogEntriesCount);
			this.unreadPathsCount = new int?(_unreadPathsCount);
			return _unreadPathsCount;
		}
	}

	public string UnreadPathsCountString
	{
		get
		{
			if (this.UnreadPathsCount == 0)
			{
				return string.Empty;
			}
			int unreadPathsCount = this.UnreadPathsCount;
			return unreadPathsCount.ToString();
		}
	}

	public int UnreadRecommendedCount
	{
		get
		{
			if (&this.unreadRecommendedCount.HasValue)
			{
				return &this.unreadRecommendedCount.GetValueOrDefault();
			}
			if (this.Log == null)
			{
				return 0;
			}
			this.CountUnreadRecommended();
			return &this.unreadRecommendedCount.GetValueOrDefault();
		}
	}

	public int UnversionedCount
	{
		get
		{
			if (&this.unversionedModifiedCount.HasValue)
			{
				return &this.unversionedModifiedCount.GetValueOrDefault();
			}
			if (this.LocalStatus == null)
			{
				return 0;
			}
			this.unversionedModifiedCount = new int?(this.LocalStatus.UnversionedEntries.Count);
			return &this.unversionedModifiedCount.GetValueOrDefault();
		}
	}

	public string UnversionedCountString
	{
		get
		{
			object[] objArray;
			int count = this.UnversionedCount;
			if (count == 0)
			{
				return Strings.NoUnversionedItems;
			}
			return Strings.UnversionedItems_FORMAT.FormatWith(new object[] { count, (count == 1 ? Strings.UnversionedItems_Item : Strings.UnversionedItems_Items) });
		}
	}

	[XmlIgnore]
	public bool Updating
	{
		get
		{
			return this.updating;
		}
		private set
		{
			if (this.updating != value)
			{
				this.updating = value;
				base.OnStatusChanged(StatusChangedReason.Updating);
			}
		}
	}

	public string UserName
	{
		get;
		set;
	}

	public Source()
	{
		this.enableRecommendations = true;
	}

	internal void ApplyPatch()
	{
		TortoiseProcess.ApplyPatch(this.Path, new MethodInvoker(this.TortoiseProcessApplyPatchCallBack));
	}

	protected override void BeforeUpgrade()
	{
		base.BeforeUpgrade();
		if (base.Version < new Version("1.0.0.215"))
		{
			Logger.Log.InfoFormat("Upgrading Source {0} from {1} to {2}: Deleting the log cache", this, base.Version, FileSystemHelper.CurrentVersion);
			this.DeleteCache();
		}
		if (base.Version < new Version("1.0.1.392"))
		{
			Logger.Log.InfoFormat("Upgrading Source {0} from {1} to {2}: Enabling recommendations", this, base.Version, FileSystemHelper.CurrentVersion);
			this.EnableRecommendations = true;
		}
	}

	internal void BranchTag()
	{
		TortoiseProcess.Copy(this.Path);
	}

	internal void CheckModifications()
	{
		TortoiseProcess.CheckModifications(this.Path);
	}

	internal void Checkout()
	{
		SVNInfo info = this.GetInfo(true);
		if (info == null)
		{
			ErrorHandler.Append(Strings.ErrorCantGetSourceInformationForTheCheckout, this, null);
			return;
		}
		TortoiseProcess.Checkout(info.URL);
	}

	internal virtual List<SVNLogEntry> CheckUpdates()
	{
		object[] objArray;
		object[] objArray2;
		try
		{
			this.LastCheck = new DateTime?(DateTime.Now);
			EventLog.Log(EventLogEntryType.CheckingUpdates, Strings.CheckingSourceForUpdates_FORMAT.FormatWith(new object[] { base.Name }), this);
			this.Updating = true;
			if (this.Log == null)
			{
				Logger.Log.Debug("GetLog");
				SVNLog newLog = this.GetLog(true);
				this.SetLog(newLog);
				EventLog.Log(EventLogEntryType.Source, Strings.LogCreatedForSource_FORMAT.FormatWith(new object[] { base.Name }), this);
			}
			if (this.IsURL || this.PathExists)
			{
				this.RefreshInfo();
			}
			List<SVNLogEntry> updates = this.Log.Update();
			base.ClearError();
			if (!this.IsURL)
			{
				this.RefreshLocalStatus();
				this.RefreshRemoteStatus();
			}
			if (this.CanRecommend)
			{
				this.RefreshRecommended();
			}
			if (updates.Count > 0)
			{
				Logger.Log.DebugFormat("updates={0}", updates.Count);
				this.LogUpdatesToEventLog(updates);
				base.OnStatusChanged(StatusChangedReason.Updated);
			}
			return updates;
		}
		catch
		{
			throw;
		}
		finally
		{
			this.Updating = false;
		}
	}

	internal void CleanUp()
	{
		TortoiseProcess.CleanUp(this.Path);
	}

	internal static void ClearAllErrors()
	{
		foreach (Source source in MonitorSettings.Instance.GetEnumerableSources())
		{
			source.ClearError();
		}
	}

	private void CountUnread(out int unreadLogEntriesCount, out int unreadPathsCount)
	{
		unreadLogEntriesCount = 0;
		unreadPathsCount = 0;
		List<string> unreadPathsList = new List<string>();
		IEnumerable<SVNLogEntry> unreadLogEntries = this.Log.UnreadLogEntries;
		foreach (SVNLogEntry entry in unreadLogEntries)
		{
			unreadLogEntriesCount++;
			foreach (SVNPath path in entry.UnreadPaths)
			{
				if (!unreadPathsList.Contains(path.Name))
				{
					unreadPathsList.Add(path.Name);
				}
			}
		}
		unreadPathsCount = unreadPathsList.Count;
	}

	private void CountUnreadRecommended()
	{
		this.unreadRecommendedCount = new int?(this.Log.UnreadLogEntries.Where<SVNLogEntry>(new Predicate<SVNLogEntry>((e) => e.Recommended)).Count<SVNLogEntry>());
	}

	internal void CreatePatch()
	{
		TortoiseProcess.CreatePatch(this.Path);
	}

	internal void DeleteCache()
	{
		Logger.Log.DebugFormat("Deleting cache. (source={0})", this);
		string fileName = this.CacheFileName;
		Logger.Log.InfoFormat("Deleting {0}", fileName);
		FileSystemHelper.DeleteFile(fileName);
	}

	internal override void DeleteFile()
	{
		object[] objArray;
		object[] objArray2;
		Logger.Log.Debug("Source.DeleteFile()");
		base.DeleteFile();
		Logger.Log.DebugFormat("HasLog={0}", this.HasLog);
		if (this.HasLog)
		{
			if (this.Log != null)
			{
				Logger.Log.Debug("log.Delete()");
				this.Log.Delete();
			}
			string message = Strings.ErrorDeletingLog_FORMAT.FormatWith(new object[] { base.Name });
			ErrorHandler.Append(string.Format("{0} ({1})", message, ex.Message), this, ex);
		}
		try
		{
		}
		catch (Exception ex)
		{
		}
		MonitorSettings.Instance.RemoveSource(this);
		EventLog.Log(EventLogEntryType.Source, Strings.SourceDeleted_FORMAT.FormatWith(new object[] { base.Name }), this);
		base.OnStatusChanged(StatusChangedReason.Deleted);
		Status.OnStatusChanged();
	}

	internal void DeleteUnversioned()
	{
		TortoiseProcess.DeleteUnversioned(this.Path, new MethodInvoker(this.TortoiseProcessDeleteUnversionedCallBack));
	}

	internal void Export()
	{
		TortoiseProcess.Export(this.Path);
	}

	internal static Source FromURL(string url)
	{
		Source source = new Source();
		source.Enabled = true;
		source.Path = url;
		try
		{
			source.Name = FileSystemHelper.GetFileName(url);
		}
		catch (Exception ex)
		{
			Logger.Log.Info("probably creating from a URL", ex);
			source.Name = url;
		}
		return source;
	}

	internal SVNInfo GetInfo(bool forceIfNull)
	{
		if (this.Info == null && forceIfNull)
		{
			this.Info = SVNInfo.Create(this);
		}
		return this.Info;
	}

	internal void GetLock()
	{
		TortoiseProcess.GetLock(this.Path);
	}

	internal SVNLog GetLog(bool createIfNotExist)
	{
		if (this.Log == null)
		{
			SVNLog loadedLog = SVNLog.Load(this, createIfNotExist);
			Logger.Log.DebugFormat("Log was null (source={0}). Tried to load it. Now it's {1}", this, (loadedLog == null ? "still null" : "not null"));
			this.SetLog(loadedLog);
			base.OnStatusChanged(StatusChangedReason.Updated);
		}
		return this.Log;
	}

	public IEnumerable<string> GetSearchKeywords()
	{
		string[] str = new string[5];
		str[0] = base.ToString();
		str[1] = base.ErrorText;
		str[2] = base.Name;
		str[3] = this.Path;
		4[long num = this.Revision] = num.ToString();
		string[] keywords = str;
		return keywords;
	}

	internal bool IsRecommended(long revision)
	{
		bool isRecommended = this.RecommendedRevisions.Contains(revision);
		return isRecommended;
	}

	private static bool IsUpdatesContainsUnread(IEnumerable<SVNLogEntry> updates)
	{
		return updates.Any<SVNLogEntry>(new Predicate<SVNLogEntry>((e) => e.Unread));
	}

	internal void LoadCache()
	{
		SVNLog cachedLog = SVNLog.Load(this, false);
		this.SetLog(cachedLog);
	}

	private void LogUpdatesToEventLog(List<SVNLogEntry> updates)
	{
		string message;
		object[] objArray;
		object[] objArray2;
		if (!Source.IsUpdatesContainsUnread(updates))
		{
			Logger.Log.Info("No unread items. Probably self-commits.");
			return;
		}
		if (updates.Count == 1)
		{
			string author = updates[0].Author;
			message = Strings.NewUpdateAvailableByAuthor_FORMAT.FormatWith(new object[] { base.Name, author });
		}
		else
		{
			message = Strings.NewUpdatesAreAvailable_FORMAT.FormatWith(new object[] { updates.Count, base.Name });
		}
		EventLog.Log(EventLogEntryType.AvailableUpdates, message, this);
	}

	internal void Merge()
	{
		TortoiseProcess.Merge(this.Path, new MethodInvoker(this.TortoiseProcessMergeCallBack));
	}

	protected override void OnStatusChanged(StatusChangedReason reason)
	{
		if (reason == StatusChangedReason.Deleted || !base.IsAlive)
		{
			return;
		}
		if (reason == StatusChangedReason.Recommended && base.Enabled && !this.Updating)
		{
			this.CheckUpdates();
		}
		this.ResetStatusValues();
		if (this.Log != null && !this.Log.IsCached)
		{
			this.Log.Save();
		}
		base.OnStatusChanged(reason);
	}

	internal void Properties()
	{
		TortoiseProcess.Properties(this.Path);
	}

	internal void Recommend(long revision)
	{
		if (!this.EnableRecommendations)
		{
			Logger.Log.InfoFormat("This source is not configured to use recommendations: {0}.", base.Name);
			return;
		}
		this.RecommendWizard(revision);
	}

	internal void RecommendWithoutHook(long revision)
	{
		Logger.Log.InfoFormat("Recommending revision {0} of '{1}'", revision, base.Name);
		try
		{
			SharpSVNClient.Recommend(this, revision);
		}
		catch (Exception ex)
		{
			ErrorHandler.HandleEntityException(this, ex);
		}
	}

	internal void RecommendWizard(long revision)
	{
		long[] revisions = new long[this.RecommendedRevisions.Count];
		this.RecommendedRevisions.CopyTo(revisions);
		DialogResult result = RecommendWizard.Recommend(this, revision);
		if (result == DialogResult.OK)
		{
			base.OnStatusChanged(StatusChangedReason.Recommended);
			return;
		}
		this.RecommendedRevisions.Clear();
		this.RecommendedRevisions.AddRange(revisions);
		this.UndoRecommend(revision);
	}

	internal void Refresh()
	{
		this.RefreshInfo();
		if (!this.IsURL)
		{
			this.RefreshLocalStatus();
			this.RefreshRemoteStatus();
		}
	}

	internal void RefreshInfo()
	{
		this.Info = null;
		this.GetInfo(true);
	}

	internal void RefreshLocalStatus()
	{
		if (this.PathExists)
		{
			this.LocalStatus = SVNStatus.Create(this, StatusCreationOption.LocalOnly);
		}
	}

	internal void RefreshLog()
	{
		this.Updating = true;
		try
		{
			SVNLog createdLog = SVNLog.Create(this);
			this.SetLog(createdLog);
			base.OnStatusChanged(StatusChangedReason.Refreshed);
		}
		catch (SvnException svnex)
		{
			ErrorHandler.HandleSourceSVNException(this, svnex);
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error refreshing the log of '{0}'", base.Name), ex);
			ErrorHandler.Append(ex.Message, this, ex);
		}
		finally
		{
			this.Updating = false;
		}
	}

	internal void RefreshRecommended()
	{
		int beforeCount = this.RecommendedRevisions.Count;
		this.RecommendedRevisions = SharpSVNClient.GetRemoteRecommendedRevisions(this);
		if (this.RecommendedRevisions.Count == beforeCount)
		{
			break;
		}
		StringBuilder sb = new StringBuilder();
		foreach (long revision in this.RecommendedRevisions)
		{
			sb.Append(revision).Append(", ");
		}
		Logger.Log.DebugFormat("Recommended revisions of '{0}': {1}", this, sb);
		base.Save();
	}

	internal void RefreshRemoteStatus()
	{
		if (this.PathExists)
		{
			this.RemoteStatus = SVNStatus.Create(this, StatusCreationOption.LocalAndRemote);
		}
	}

	internal void Reintegrate()
	{
		TortoiseProcess.Reintegrate(this.Path, new MethodInvoker(this.TortoiseProcessMergeCallBack));
	}

	internal void ReleaseLock()
	{
		TortoiseProcess.ReleaseLock(this.Path);
	}

	internal void Relocate()
	{
		TortoiseProcess.Relocate(this.Path, new MethodInvoker(this.TortoiseProcessRelocateCallBack));
	}

	internal void RepoBrowser()
	{
		TortoiseProcess.RepoBrowser(this.Path);
	}

	private void Reset()
	{
		this.revision = (long)0;
		this.Info = null;
		if (this.Log != null)
		{
			this.Log.Delete();
		}
		this.log = null;
		this.hasLog = false;
		this.LocalStatus = null;
		this.pathExists = null;
		this.isURL = null;
		this.isFileURL = null;
	}

	private void ResetStatusValues()
	{
		this.unreadLogEntriesCount = null;
		this.unreadRecommendedCount = null;
		this.unreadPathsCount = null;
		this.modifiedCount = null;
		this.unversionedModifiedCount = null;
		this.possibleConflictedPathsCount = null;
	}

	internal void Resolve()
	{
		TortoiseProcess.Resolve(this.Path, new MethodInvoker(this.TortoiseProcessResolveCallBack));
	}

	internal void RevisionGraph()
	{
		TortoiseProcess.RevisionGraph(this.Path);
	}

	internal void SetInfo(SVNInfo info)
	{
		this.Info = info;
	}

	internal void SetLog(SVNLog log)
	{
		this.log = log;
		this.hasLog = log != null;
		base.ClearError();
	}

	internal void SVNAdd()
	{
		TortoiseProcess.Add(this.Path, new MethodInvoker(this.TortoiseProcessCommitCallBack));
	}

	internal static void SVNCheckAllModifications()
	{
		StringBuilder sb = new StringBuilder();
		foreach (Source source in Status.EnabledSources.Where<Source>(new Predicate<Source>((s) => !s.IsURL)))
		{
			sb.Append(source.path).Append("*");
		}
		TortoiseProcess.CheckModifications(sb.ToString());
	}

	internal void SVNCommit()
	{
		TortoiseProcess.Commit(this.Path, new MethodInvoker(this.TortoiseProcessCommitCallBack));
	}

	internal void SVNDiff()
	{
		TortoiseProcess.DiffWithPrevious(this.Path, this.Revision - (long)1, this.Revision);
	}

	internal void SVNRevert()
	{
		TortoiseProcess.Revert(this.Path, new MethodInvoker(this.TortoiseProcessRevertCallBack));
	}

	internal void SVNShowLog()
	{
		TortoiseProcess.Log(this.Path);
	}

	internal void SVNUpdate()
	{
		TortoiseProcess.Update(this.Path, new MethodInvoker(this.TortoiseProcessUpdateCallBack));
	}

	internal static void SVNUpdateAll(bool ignoreUpToDate)
	{
		DialogResult result = UpdateHeadPromptDialog.Prompt();
		Logger.Log.InfoFormat("Update sll sources: User clicked {0}", result);
		if (result != DialogResult.Yes)
		{
			return;
		}
		List<Source> sources = MonitorSettings.Instance.Sources;
		for (int i = 0; i < sources.Count; i++)
		{
			Source source = sources[i];
			if ((ApplicationSettingsManager.Settings.IgnoreDisabledSourcesWhenUpdatingAll || source.Enabled) && (ignoreUpToDate || !source.IsUpToDate))
			{
				if (ApplicationSettingsManager.Settings.SVNUpdateSourcesParallel)
				{
					source.SVNUpdate();
					continue;
				}
				source.SVNUpdateAndWait(ApplicationSettingsManager.Settings.SVNUpdateSourcesQueueTimeoutSeconds * 1000);
			}
		}
	}

	internal static void SVNUpdateAllAvailable()
	{
		Source.SVNUpdateAll(true);
	}

	internal void SVNUpdateAndWait(int millisecondsTimeout)
	{
		this.tortoiseHandle = new AutoResetEvent(false);
		this.SVNUpdate();
		this.tortoiseHandle.WaitOne(millisecondsTimeout, false);
	}

	internal void Switch()
	{
		TortoiseProcess.Switch(this.Path, new MethodInvoker(this.TortoiseProcessSwitchCallBack));
	}

	private void TortoiseProcessApplyPatchCallBack()
	{
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNApplyPatch);
	}

	private void TortoiseProcessCommitCallBack()
	{
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNCommit);
	}

	private void TortoiseProcessDeleteUnversionedCallBack()
	{
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNDeleteUnversioned);
	}

	private void TortoiseProcessMergeCallBack()
	{
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNMerge);
	}

	private void TortoiseProcessRelocateCallBack()
	{
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNRelocate);
	}

	private void TortoiseProcessResolveCallBack()
	{
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNResolve);
	}

	private void TortoiseProcessRevertCallBack()
	{
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNRevert);
	}

	private void TortoiseProcessSwitchCallBack()
	{
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNSwitch);
	}

	private void TortoiseProcessUpdateCallBack()
	{
		if (this.tortoiseHandle != null)
		{
			this.tortoiseHandle.Set();
		}
		Logger.Log.InfoFormat("Refreshing status after updating (source={0})", this);
		this.Refresh();
		base.OnStatusChanged(StatusChangedReason.SVNUpdate);
	}

	public override string ToString()
	{
		if (this.IsURL)
		{
			return string.Format("{0} ({1})", base.Name, this.Path);
		}
		if (this.Info != null)
		{
			return string.Format("{0} ({1}) [{2}]", base.Name, this.Path, this.Info.URL);
		}
		return string.Format("{0} ({1})", base.Name, this.Path);
	}

	internal void UndoRecommend(long revision)
	{
		Logger.Log.InfoFormat("Undo-Recommending revision {0} of '{1}'", revision, base.Name);
		try
		{
			SharpSVNClient.UndoRecommend(this, revision);
		}
		catch (Exception ex)
		{
			ErrorHandler.HandleEntityException(this, ex);
		}
	}
}
}