using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;
using SVNMonitor.View.Dialogs;
using SVNMonitor.View.Interfaces;

using SharpSvn;

namespace SVNMonitor.Entities
{
	[Serializable]
	public class Source : UserEntity, ISearchable
	{
		private bool enableRecommendations = true;
		[NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
		[NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
		[NonSerialized, DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool updating;

		internal void ApplyPatch()
		{
			TortoiseProcess.ApplyPatch(Path, TortoiseProcessApplyPatchCallBack);
		}

		protected override void BeforeUpgrade()
		{
			base.BeforeUpgrade();
			if (base.Version < new Version("1.0.0.215"))
			{
				Logger.Log.InfoFormat("Upgrading Source {0} from {1} to {2}: Deleting the log cache", this, base.Version, FileSystemHelper.CurrentVersion);
				DeleteCache();
			}
			if (base.Version < new Version("1.0.1.392"))
			{
				Logger.Log.InfoFormat("Upgrading Source {0} from {1} to {2}: Enabling recommendations", this, base.Version, FileSystemHelper.CurrentVersion);
				EnableRecommendations = true;
			}
		}

		internal void BranchTag()
		{
			TortoiseProcess.Copy(Path);
		}

		internal void CheckModifications()
		{
			TortoiseProcess.CheckModifications(Path);
		}

		internal void Checkout()
		{
			SVNInfo info = GetInfo(true);
			if (info == null)
			{
				ErrorHandler.Append(Strings.ErrorCantGetSourceInformationForTheCheckout, this, null);
			}
			else
			{
				TortoiseProcess.Checkout(info.URL);
			}
		}

		internal virtual List<SVNLogEntry> CheckUpdates()
		{
			List<SVNLogEntry> tempAnotherLocal0;
			try
			{
				LastCheck = DateTime.Now;
				SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.CheckingUpdates, Strings.CheckingSourceForUpdates_FORMAT.FormatWith(new object[]
				{
					base.Name
				}), this);
				Updating = true;
				if (Log == null)
				{
					Logger.Log.Debug("GetLog");
					SVNLog newLog = GetLog(true);
					SetLog(newLog);
					SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Source, Strings.LogCreatedForSource_FORMAT.FormatWith(new object[]
					{
						base.Name
					}), this);
				}
				if (IsURL || PathExists)
				{
					RefreshInfo();
				}
				List<SVNLogEntry> updates = Log.Update();
				ClearError();
				if (!IsURL)
				{
					RefreshLocalStatus();
					RefreshRemoteStatus();
				}
				if (CanRecommend)
				{
					RefreshRecommended();
				}
				if (updates.Count > 0)
				{
					Logger.Log.DebugFormat("updates={0}", updates.Count);
					LogUpdatesToEventLog(updates);
					OnStatusChanged(StatusChangedReason.Updated);
				}
				tempAnotherLocal0 = updates;
			}
			catch
			{
				throw;
			}
			finally
			{
				Updating = false;
			}
			return tempAnotherLocal0;
		}

		internal void CleanUp()
		{
			TortoiseProcess.CleanUp(Path);
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
			foreach (SVNLogEntry entry in Log.UnreadLogEntries)
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
			unreadRecommendedCount = Log.UnreadLogEntries.Where(e => e.Recommended).Count<SVNLogEntry>();
		}

		internal void CreatePatch()
		{
			TortoiseProcess.CreatePatch(Path);
		}

		internal void DeleteCache()
		{
			Logger.Log.DebugFormat("Deleting cache. (source={0})", this);
			string fileName = CacheFileName;
			Logger.Log.InfoFormat("Deleting {0}", fileName);
			FileSystemHelper.DeleteFile(fileName);
		}

		internal override void DeleteFile()
		{
			Logger.Log.Debug("Source.DeleteFile()");
			base.DeleteFile();
			Logger.Log.DebugFormat("HasLog={0}", HasLog);
			if (HasLog)
			{
				try
				{
					if (Log != null)
					{
						Logger.Log.Debug("log.Delete()");
						Log.Delete();
					}
				}
				catch (Exception ex)
				{
					string message = Strings.ErrorDeletingLog_FORMAT.FormatWith(new object[]
					{
						base.Name
					});
					ErrorHandler.Append(string.Format("{0} ({1})", message, ex.Message), this, ex);
				}
			}
			MonitorSettings.Instance.RemoveSource(this);
			SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Source, Strings.SourceDeleted_FORMAT.FormatWith(new object[]
			{
				base.Name
			}), this);
			OnStatusChanged(StatusChangedReason.Deleted);
			Status.OnStatusChanged();
		}

		internal void DeleteUnversioned()
		{
			TortoiseProcess.DeleteUnversioned(Path, TortoiseProcessDeleteUnversionedCallBack);
		}

		internal void Export()
		{
			TortoiseProcess.Export(Path);
		}

		internal static Source FromURL(string url)
		{
			Source source = new Source
			{
				Enabled = true,
				Path = url
			};
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
			if ((Info == null) && forceIfNull)
			{
				Info = SVNInfo.Create(this);
			}
			return Info;
		}

		internal void GetLock()
		{
			TortoiseProcess.GetLock(Path);
		}

		internal SVNLog GetLog(bool createIfNotExist)
		{
			if (Log == null)
			{
				SVNLog loadedLog = SVNLog.Load(this, createIfNotExist);
				Logger.Log.DebugFormat("Log was null (source={0}). Tried to load it. Now it's {1}", this, (loadedLog == null) ? "still null" : "not null");
				SetLog(loadedLog);
				OnStatusChanged(StatusChangedReason.Updated);
			}
			return Log;
		}

		public IEnumerable<string> GetSearchKeywords()
		{
			return new[]
			{
				ToString(), base.ErrorText, base.Name, Path, Revision.ToString()
			};
		}

		internal bool IsRecommended(long revision)
		{
			return RecommendedRevisions.Contains(revision);
		}

		private static bool IsUpdatesContainsUnread(IEnumerable<SVNLogEntry> updates)
		{
			return updates.Any(e => e.Unread);
		}

		internal void LoadCache()
		{
			SVNLog cachedLog = SVNLog.Load(this, false);
			SetLog(cachedLog);
		}

		private void LogUpdatesToEventLog(List<SVNLogEntry> updates)
		{
			if (!IsUpdatesContainsUnread(updates))
			{
				Logger.Log.Info("No unread items. Probably self-commits.");
			}
			else
			{
				string message;
				if (updates.Count == 1)
				{
					string author = updates[0].Author;
					message = Strings.NewUpdateAvailableByAuthor_FORMAT.FormatWith(new object[]
					{
						base.Name, author
					});
				}
				else
				{
					message = Strings.NewUpdatesAreAvailable_FORMAT.FormatWith(new object[]
					{
						updates.Count, base.Name
					});
				}
				SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.AvailableUpdates, message, this);
			}
		}

		internal void Merge()
		{
			TortoiseProcess.Merge(Path, TortoiseProcessMergeCallBack);
		}

		protected override void OnStatusChanged(StatusChangedReason reason)
		{
			if ((reason != StatusChangedReason.Deleted) && IsAlive)
			{
				if (((reason == StatusChangedReason.Recommended) && Enabled) && !Updating)
				{
					CheckUpdates();
				}
				ResetStatusValues();
				if ((Log != null) && !Log.IsCached)
				{
					Log.Save();
				}
				base.OnStatusChanged(reason);
			}
		}

		internal void Properties()
		{
			TortoiseProcess.Properties(Path);
		}

		internal void Recommend(long revision)
		{
			if (!EnableRecommendations)
			{
				Logger.Log.InfoFormat("This source is not configured to use recommendations: {0}.", base.Name);
			}
			else
			{
				RecommendWizard(revision);
			}
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
			long[] revisions = new long[RecommendedRevisions.Count];
			RecommendedRevisions.CopyTo(revisions);
			if (SVNMonitor.View.Dialogs.RecommendWizard.Recommend(this, revision) == DialogResult.OK)
			{
				OnStatusChanged(StatusChangedReason.Recommended);
			}
			else
			{
				RecommendedRevisions.Clear();
				RecommendedRevisions.AddRange(revisions);
				UndoRecommend(revision);
			}
		}

		internal void Refresh()
		{
			RefreshInfo();
			if (!IsURL)
			{
				RefreshLocalStatus();
				RefreshRemoteStatus();
			}
		}

		internal void RefreshInfo()
		{
			Info = null;
			GetInfo(true);
		}

		internal void RefreshLocalStatus()
		{
			if (PathExists)
			{
				LocalStatus = SVNStatus.Create(this, SVNStatus.StatusCreationOption.LocalOnly);
			}
		}

		internal void RefreshLog()
		{
			Updating = true;
			try
			{
				SVNLog createdLog = SVNLog.Create(this);
				SetLog(createdLog);
				OnStatusChanged(StatusChangedReason.Refreshed);
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
				Updating = false;
			}
		}

		internal void RefreshRecommended()
		{
			int beforeCount = RecommendedRevisions.Count;
			RecommendedRevisions = SharpSVNClient.GetRemoteRecommendedRevisions(this);
			if (RecommendedRevisions.Count != beforeCount)
			{
				StringBuilder sb = new StringBuilder();
				foreach (long revision in RecommendedRevisions)
				{
					sb.Append(revision).Append(", ");
				}
				Logger.Log.DebugFormat("Recommended revisions of '{0}': {1}", this, sb);
				Save();
			}
		}

		internal void RefreshRemoteStatus()
		{
			if (PathExists)
			{
				RemoteStatus = SVNStatus.Create(this, SVNStatus.StatusCreationOption.LocalAndRemote);
			}
		}

		internal void Reintegrate()
		{
			TortoiseProcess.Reintegrate(Path, TortoiseProcessMergeCallBack);
		}

		internal void ReleaseLock()
		{
			TortoiseProcess.ReleaseLock(Path);
		}

		internal void Relocate()
		{
			TortoiseProcess.Relocate(Path, TortoiseProcessRelocateCallBack);
		}

		internal void RepoBrowser()
		{
			TortoiseProcess.RepoBrowser(Path);
		}

		private void Reset()
		{
			revision = 0L;
			Info = null;
			if (Log != null)
			{
				Log.Delete();
			}
			log = null;
			hasLog = false;
			LocalStatus = null;
			pathExists = null;
			isURL = null;
			isFileURL = null;
		}

		private void ResetStatusValues()
		{
			unreadLogEntriesCount = null;
			unreadRecommendedCount = null;
			unreadPathsCount = null;
			modifiedCount = null;
			unversionedModifiedCount = null;
			possibleConflictedPathsCount = null;
		}

		internal void Resolve()
		{
			TortoiseProcess.Resolve(Path, TortoiseProcessResolveCallBack);
		}

		internal void RevisionGraph()
		{
			TortoiseProcess.RevisionGraph(Path);
		}

		internal void SetInfo(SVNInfo info)
		{
			Info = info;
		}

		internal void SetLog(SVNLog log)
		{
			this.log = log;
			hasLog = log != null;
			ClearError();
		}

		internal void SVNAdd()
		{
			TortoiseProcess.Add(Path, TortoiseProcessCommitCallBack);
		}

		internal static void SVNCheckAllModifications()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Source source in Status.EnabledSources.Where(s => !s.IsURL))
			{
				sb.Append(source.path).Append("*");
			}
			TortoiseProcess.CheckModifications(sb.ToString());
		}

		internal void SVNCommit()
		{
			TortoiseProcess.Commit(Path, TortoiseProcessCommitCallBack);
		}

		internal void SVNDiff()
		{
			TortoiseProcess.DiffWithPrevious(Path, Revision - 1L, Revision);
		}

		internal void SVNRevert()
		{
			TortoiseProcess.Revert(Path, TortoiseProcessRevertCallBack);
		}

		internal void SVNShowLog()
		{
			TortoiseProcess.Log(Path);
		}

		internal void SVNUpdate()
		{
			TortoiseProcess.Update(Path, TortoiseProcessUpdateCallBack);
		}

		internal static void SVNUpdateAll(bool ignoreUpToDate)
		{
			DialogResult result = UpdateHeadPromptDialog.Prompt();
			Logger.Log.InfoFormat("Update sll sources: User clicked {0}", result);
			if (result == DialogResult.Yes)
			{
				List<Source> sources = MonitorSettings.Instance.Sources;
				for (int i = 0; i < sources.Count; i++)
				{
					Source source = sources[i];
					if ((!ApplicationSettingsManager.Settings.IgnoreDisabledSourcesWhenUpdatingAll || source.Enabled) && (!ignoreUpToDate || !source.IsUpToDate))
					{
						if (ApplicationSettingsManager.Settings.SVNUpdateSourcesParallel)
						{
							source.SVNUpdate();
						}
						else
						{
							source.SVNUpdateAndWait(ApplicationSettingsManager.Settings.SVNUpdateSourcesQueueTimeoutSeconds * 0x3e8);
						}
					}
				}
			}
		}

		internal static void SVNUpdateAllAvailable()
		{
			SVNUpdateAll(true);
		}

		internal void SVNUpdateAndWait(int millisecondsTimeout)
		{
			tortoiseHandle = new AutoResetEvent(false);
			SVNUpdate();
			tortoiseHandle.WaitOne(millisecondsTimeout, false);
		}

		internal void Switch()
		{
			TortoiseProcess.Switch(Path, TortoiseProcessSwitchCallBack);
		}

		private void TortoiseProcessApplyPatchCallBack()
		{
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNApplyPatch);
		}

		private void TortoiseProcessCommitCallBack()
		{
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNCommit);
		}

		private void TortoiseProcessDeleteUnversionedCallBack()
		{
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNDeleteUnversioned);
		}

		private void TortoiseProcessMergeCallBack()
		{
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNMerge);
		}

		private void TortoiseProcessRelocateCallBack()
		{
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNRelocate);
		}

		private void TortoiseProcessResolveCallBack()
		{
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNResolve);
		}

		private void TortoiseProcessRevertCallBack()
		{
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNRevert);
		}

		private void TortoiseProcessSwitchCallBack()
		{
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNSwitch);
		}

		private void TortoiseProcessUpdateCallBack()
		{
			if (tortoiseHandle != null)
			{
				tortoiseHandle.Set();
			}
			Logger.Log.InfoFormat("Refreshing status after updating (source={0})", this);
			Refresh();
			OnStatusChanged(StatusChangedReason.SVNUpdate);
		}

		public override string ToString()
		{
			if (!IsURL && (Info != null))
			{
				return string.Format("{0} ({1}) [{2}]", base.Name, Path, Info.URL);
			}
			return string.Format("{0} ({1})", base.Name, Path);
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

		public bool Authenticate { get; set; }

		internal string CacheFileName
		{
			get { return (System.IO.Path.Combine(FileSystemHelper.AppData, base.Guid) + ".cache"); }
		}

		public bool CanRecommend
		{
			get
			{
				if (IsFileURL)
				{
					return false;
				}
				if (!EnableRecommendations)
				{
					return false;
				}
				return true;
			}
		}

		public override bool Enabled
		{
			get { return base.Enabled; }
			set
			{
				base.Enabled = value;
				MonitorSettings.Instance.OnSourcesChanged();
			}
		}

		public bool EnableRecommendations
		{
			[DebuggerNonUserCode]
			get { return enableRecommendations; }
			[DebuggerNonUserCode]
			set { enableRecommendations = value; }
		}

		public override string FileExtension
		{
			[DebuggerNonUserCode]
			get { return ".source"; }
		}

		public bool HasLocalChanges
		{
			get
			{
				if (IsURL)
				{
					return false;
				}
				if (LocalStatus == null)
				{
					return false;
				}
				return LocalStatus.GetEnumerableStatusEntries().Any(e => e.ModifiedOrUnversioned);
			}
		}

		public bool HasLocalVersionedChanges
		{
			get
			{
				if (IsURL)
				{
					return false;
				}
				if (LocalStatus == null)
				{
					return false;
				}
				return LocalStatus.GetEnumerableStatusEntries().Any(e => e.Modified);
			}
		}

		public bool HasLog
		{
			[DebuggerNonUserCode]
			get { return hasLog; }
		}

		private SVNInfo Info
		{
			[DebuggerNonUserCode]
			get { return info; }
			[DebuggerNonUserCode]
			set { info = value; }
		}

		public override bool IsAlive
		{
			get { return MonitorSettings.Instance.Sources.Contains(this); }
		}

		public bool IsFileURL
		{
			get
			{
				if (!isFileURL.HasValue)
				{
					if (Info == null)
					{
						Info = GetInfo(true);
					}
					IsFileURL = (Info != null) && FileSystemHelper.IsFileUrl(Info.URL);
				}
				return isFileURL.GetValueOrDefault(true);
			}
			[DebuggerNonUserCode]
			set { isFileURL = value; }
		}

		public bool IsUpToDate
		{
			get
			{
				if (!IsURL)
				{
					if (Log == null)
					{
						return true;
					}
					if (Log.GetEnumerableLogEntries().Any(e => e.Unread))
					{
						return false;
					}
				}
				return true;
			}
		}

		public bool IsURL
		{
			get
			{
				if (!isURL.HasValue)
				{
					IsURL = (Path != null) && FileSystemHelper.IsUrl(Path);
				}
				return isURL.GetValueOrDefault(true);
			}
			set { isURL = value; }
		}

		[XmlIgnore]
		public DateTime? LastCheck
		{
			[DebuggerNonUserCode]
			get { return lastCheck; }
			private set { lastCheck = value; }
		}

		internal SVNStatus LocalStatus
		{
			[DebuggerNonUserCode]
			get { return localStatus; }
			set
			{
				localStatus = value;
				ResetStatusValues();
				OnStatusChanged(StatusChangedReason.SVNStatusChanged);
			}
		}

		private SVNLog Log
		{
			[DebuggerNonUserCode]
			get { return log; }
		}

		public int ModifiedCount
		{
			get
			{
				if (!modifiedCount.HasValue)
				{
					if (LocalStatus == null)
					{
						return 0;
					}
					modifiedCount = LocalStatus.ModifiedEntries.Count;
				}
				return modifiedCount.GetValueOrDefault();
			}
		}

		public string ModifiedCountString
		{
			get
			{
				int count = ModifiedCount;
				if (count == 0)
				{
					return Strings.NoModifiedItems;
				}
				return Strings.ModifiedItems_FORMAT.FormatWith(new object[]
				{
					count, ((count == 1) ? Strings.ModifiedItems_Item : Strings.ModifiedItems_Items)
				});
			}
		}

		public string Password { get; set; }

		public string Path
		{
			[DebuggerNonUserCode]
			get { return path; }
			set
			{
				if ((path != value) && (value != null))
				{
					Logger.Log.DebugFormat("ResetInfo(Old path={0}, New path={1})", path, value);
					Reset();
					path = value.Trim();
				}
			}
		}

		public bool PathExists
		{
			get
			{
				if (!pathExists.HasValue)
				{
					pathExists = FileSystemHelper.Exists(Path);
				}
				return pathExists.GetValueOrDefault();
			}
		}

		public int PossibleConflictedFilePathsCount
		{
			get
			{
				if (!possibleConflictedPathsCount.HasValue)
				{
					if ((LocalStatus == null) || (Log == null))
					{
						return 0;
					}
					possibleConflictedPathsCount = Log.PossibleConflictedFilePaths.Count;
				}
				return possibleConflictedPathsCount.GetValueOrDefault();
			}
		}

		public string PossibleConflictedFilePathsCountString
		{
			get
			{
				int count = PossibleConflictedFilePathsCount;
				if (count == 0)
				{
					return Strings.NoPossibleConflicts;
				}
				return Strings.PossibleConflicts_FORMAT.FormatWith(new object[]
				{
					count, ((count == 1) ? Strings.PossibleConflicts_Conflict : Strings.PossibleConflicts_Conflicts)
				});
			}
		}

		internal ReadOnlyCollection<long> ReadOnlyRecommendedRevisions
		{
			[DebuggerNonUserCode]
			get { return RecommendedRevisions.AsReadOnly(); }
		}

		private List<long> RecommendedRevisions
		{
			get
			{
				if (recommendedRevisions == null)
				{
					recommendedRevisions = new List<long>();
				}
				return recommendedRevisions;
			}
			[DebuggerNonUserCode]
			set { recommendedRevisions = value; }
		}

		internal SVNStatus RemoteStatus
		{
			[DebuggerNonUserCode]
			get { return remoteStatus; }
			set
			{
				remoteStatus = value;
				ResetStatusValues();
				OnStatusChanged(StatusChangedReason.SVNStatusChanged);
			}
		}

		public long Revision
		{
			[DebuggerNonUserCode]
			get { return revision; }
			set { revision = value; }
		}

		public string UnreadCountString
		{
			get
			{
				if (UnreadLogEntriesCount == 0)
				{
					return Strings.NoAvailableUpdates;
				}
				return Strings.AvailableUpdates_FORMAT.FormatWith(new object[]
				{
					UnreadLogEntriesCount, ((UnreadLogEntriesCount == 1) ? Strings.AvailableUpdates_Update : Strings.AvailableUpdates_Updates), (UnreadPathsCount.ToString() + ((UnreadPathsCount >= ApplicationSettingsManager.Settings.LogEntriesPageSize) ? "+" : string.Empty)), ((UnreadPathsCount == 1) ? Strings.AvailableUpdates_Item : Strings.AvailableUpdates_Items)
				});
			}
		}

		public int UnreadLogEntriesCount
		{
			get
			{
				int _unreadLogEntriesCount;
				int _unreadPathsCount;
				if (unreadLogEntriesCount.HasValue)
				{
					return unreadLogEntriesCount.GetValueOrDefault();
				}
				if (Log == null)
				{
					return 0;
				}
				CountUnread(out _unreadLogEntriesCount, out _unreadPathsCount);
				unreadLogEntriesCount = _unreadLogEntriesCount;
				unreadPathsCount = _unreadPathsCount;
				return _unreadLogEntriesCount;
			}
		}

		public string UnreadLogEntriesCountString
		{
			get
			{
				if (UnreadLogEntriesCount == 0)
				{
					return string.Empty;
				}
				return UnreadLogEntriesCount.ToString();
			}
		}

		public int UnreadPathsCount
		{
			get
			{
				int _unreadLogEntriesCount;
				int _unreadPathsCount;
				if (unreadPathsCount.HasValue)
				{
					return unreadPathsCount.GetValueOrDefault();
				}
				if (Log == null)
				{
					return 0;
				}
				CountUnread(out _unreadLogEntriesCount, out _unreadPathsCount);
				unreadLogEntriesCount = _unreadLogEntriesCount;
				unreadPathsCount = _unreadPathsCount;
				return _unreadPathsCount;
			}
		}

		public string UnreadPathsCountString
		{
			get
			{
				if (UnreadPathsCount == 0)
				{
					return string.Empty;
				}
				return UnreadPathsCount.ToString();
			}
		}

		public int UnreadRecommendedCount
		{
			get
			{
				if (!unreadRecommendedCount.HasValue)
				{
					if (Log == null)
					{
						return 0;
					}
					CountUnreadRecommended();
				}
				return unreadRecommendedCount.GetValueOrDefault();
			}
		}

		public int UnversionedCount
		{
			get
			{
				if (!unversionedModifiedCount.HasValue)
				{
					if (LocalStatus == null)
					{
						return 0;
					}
					unversionedModifiedCount = LocalStatus.UnversionedEntries.Count;
				}
				return unversionedModifiedCount.GetValueOrDefault();
			}
		}

		public string UnversionedCountString
		{
			get
			{
				int count = UnversionedCount;
				if (count == 0)
				{
					return Strings.NoUnversionedItems;
				}
				return Strings.UnversionedItems_FORMAT.FormatWith(new object[]
				{
					count, ((count == 1) ? Strings.UnversionedItems_Item : Strings.UnversionedItems_Items)
				});
			}
		}

		[XmlIgnore]
		public bool Updating
		{
			[DebuggerNonUserCode]
			get { return updating; }
			private set
			{
				if (updating != value)
				{
					updating = value;
					OnStatusChanged(StatusChangedReason.Updating);
				}
			}
		}

		public string UserName { get; set; }
	}
}