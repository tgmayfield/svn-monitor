using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;

using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Presenters;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Dialogs;
using SVNMonitor.View.Interfaces;
using SVNMonitor.Wizards;

namespace SVNMonitor.View.Panels
{
	internal partial class SourcesPanel : UserControl, IUserEntityView<Source>, ISelectableView<Source>, ISupportInitialize, ISearchablePanel<Source>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Source> allSources;
		private Dictionary<Janus.Windows.UI.CommandBars.UICommand, Predicate<Source>> filterPredicates;
		private readonly UserEntityPresenter<Source> presenter;
		private EventHandler selectionChanged;
		

		public event EventHandler SelectionChanged
		{
			add
			{
				selectionChanged = (EventHandler)Delegate.Combine(selectionChanged, value);
				SourcesExplorerBar.SelectionChanged += value;
			}
			remove
			{
				selectionChanged = (EventHandler)Delegate.Remove(selectionChanged, value);
				SourcesExplorerBar.SelectionChanged -= value;
			}
		}

		public SourcesPanel()
		{
			InitializeComponent();
			if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
			{
				UIHelper.ApplyResources(uiCommandManager1);
				presenter = new UserEntityPresenter<Source>(this);
				InitializeClipboardDelegates();
				CreateFiltersMap();
				LoadFilterSettings();
			}
		}

		public void BeginInit()
		{
		}

		internal void Browse()
		{
			FileSystemHelper.Browse(SelectedItem.Path);
		}

		private void ClearAllErrors()
		{
			Source.ClearAllErrors();
		}

		private void ClearError()
		{
			SelectedItem.ClearError();
		}

		private void ClearFilter()
		{
			filterAll.Checked = InheritableBoolean.True;
			foreach (UICommand cmd in filterPredicates.Keys)
			{
				cmd.Checked = InheritableBoolean.False;
			}
			RefreshAndSetFilter(true);
		}

		public void ClearSearch()
		{
			SourcesExplorerBar.SetVisibleEntities();
			ClearFilter();
			ShowingAllItems = true;
			OnSelectionChanged();
		}

		private void cmdApplyPatch_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNApplyPatch();
		}

		private void cmdBranchTag_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNBranchTag();
		}

		private void cmdCheckout_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNCheckout();
		}

		private void cmdCleanUp_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNCleanUp();
		}

		private void cmdClearAllErrors_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ClearAllErrors();
		}

		private void cmdClearError_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ClearError();
		}

		private void cmdCreatePatch_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNCreatePatch();
		}

		private void cmdDelete_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			presenter.Delete();
		}

		private void cmdDeleteUnversioned_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNDeleteUnversioned();
		}

		private void cmdEdit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Edit();
		}

		private void cmdEnabled_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			EnableSource();
		}

		private void cmdExplore_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Browse();
		}

		private void cmdExport_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNExport();
		}

		private void cmdLock_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNGetLock();
		}

		private void cmdMerge_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNMerge();
		}

		private void cmdMoveDown_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			presenter.MoveDown();
		}

		private void cmdMoveUp_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			presenter.MoveUp();
		}

		private void cmdNew_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			CreateNew();
		}

		private void cmdProperties_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNProperties();
		}

		private void cmdRefresh_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			RefreshLog();
		}

		private void cmdReintegrate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNReintegrate();
		}

		private void cmdRelocate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNRelocate();
		}

		private void cmdRepoBrowser_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNRepoBrowser();
		}

		private void cmdResolve_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNResolve();
		}

		private void cmdRevisionGraph_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNRevisionGraph();
		}

		private void cmdShowLog_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNShowLog();
		}

		private void cmdSVNCommit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNCommit();
		}

		private void cmdSVNModifications_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNCheckModifications();
		}

		private void cmdSVNRevert_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNRevert();
		}

		private void cmdSVNUpdate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdate();
		}

		private void cmdSwitch_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNSwitch();
		}

		private void cmdTSVNHelp_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			TortoiseProcess.Help();
		}

		private void cmdTSVNSettings_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			TortoiseProcess.Settings();
		}

		private void cmdUnlock_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNReleaseLock();
		}

		private void cmdUpdate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			UpdateSource();
		}

		private void cmdUpdateAll_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			UpdateAllSources();
		}

		private void CreateFiltersMap()
		{
			filterPredicates = new Dictionary<UICommand, Predicate<Source>>();
			filterPredicates.Add(filterEnabled, source => source.Enabled);
			filterPredicates.Add(filterModified, source => source.HasLocalChanges);
			filterPredicates.Add(filterNotUpToDate, source => !source.IsUpToDate);
		}

		public virtual void CreateNew()
		{
			presenter.New();
		}

		private void Edit()
		{
			presenter.Edit();
		}

		public void EnableCommands()
		{
			if (!base.DesignMode && !ProcessHelper.IsInVisualStudio())
			{
				if (base.InvokeRequired)
				{
					base.BeginInvoke(new MethodInvoker(EnableCommands));
				}
				else
				{
					Source source = SelectedItem;
					CanCheckForUpdates = SourceHelper.CanCheckForUpdates(source);
					CanRunWizard = SourceHelper.CanRunWizard(source);
					CanSVNCheckForModifications = SourceHelper.CanSVNCheckForModifications(source);
					CanRefreshLog = SourceHelper.CanRefreshLog(source);
					CanSVNUpdate = SourceHelper.CanSVNUpdate(source);
					CanEnable = SourceHelper.CanEnable(source);
					CanSVNCommit = SourceHelper.CanSVNCommit(source);
					CanSVNRevert = SourceHelper.CanSVNRevert(source);
					CanExplore = SourceHelper.CanExplore(source);
					CanClearError = SourceHelper.CanClearError(source);
					CanClearAllErrors = SourceHelper.CanClearAllErrors();
					CanShowLog = SourceHelper.CanShowSVNLog(source);
					CanCopyToClipboard = SourceHelper.CanCopyToClipboard(source);
					CanCopyName = SourceHelper.CanCopyName(source);
					CanCopyURL = SourceHelper.CanCopyURL(source);
					CanCopyPath = SourceHelper.CanCopyPath(source);
					CanCopyError = SourceHelper.CanCopyError(source);
					CanCopyModifiedItems = SourceHelper.CanCopyModifiedItems(source);
					CanCopyUnversionedItems = SourceHelper.CanCopyUnversionedItems(source);
					CanCopyConflictedItems = SourceHelper.CanCopyConflictedItems(source);
					CanAdditionalTortoiseCommands = SourceHelper.CanAdditionalTortoiseCommands(source);
					CanCleanUp = SourceHelper.CanCleanUp(source);
					CanExport = SourceHelper.CanExport(source);
					CanProperties = SourceHelper.CanProperties(source);
					CanRelocate = SourceHelper.CanRelocate(source);
					CanRepoBrowser = SourceHelper.CanRepoBrowser(source);
					CanRevisionGraph = SourceHelper.CanRevisionGraph(source);
					CanSwitch = SourceHelper.CanSwitch(source);
					CanCheckout = SourceHelper.CanCheckout(source);
					CanApplyPatch = SourceHelper.CanApplyPatch(source);
					CanBranchTag = SourceHelper.CanBranchTag(source);
					CanCreatePatch = SourceHelper.CanCreatePatch(source);
					CanDeleteUnversioned = SourceHelper.CanDeleteUnversioned(source);
					CanGetLock = SourceHelper.CanGetLock(source);
					CanMerge = SourceHelper.CanMerge(source);
					CanReintegrate = SourceHelper.CanReintegrate(source);
					CanResolve = SourceHelper.CanResolve(source);
					CanTSVNHelp = SourceHelper.CanTSVNHelp(source);
					CanTSVNSettings = SourceHelper.CanTSVNSettings(source);
					CanReleaseLock = SourceHelper.CanReleaseLock(source);
					CanCheckAllForUpdates = SourcesExplorerBar.Groups.Count > 0;
					cmdEnabled.Checked = ((source != null) && source.Enabled).ToInheritableBoolean();
				}
			}
		}

		private void EnableSource()
		{
			SelectedItem.Enabled = cmdEnabled.Checked == InheritableBoolean.True;
		}

		public void EndInit()
		{
			if (!base.DesignMode)
			{
				UIHelper.GetBaseName getBaseName = item => ((Source)item).Name;
				UIHelper.InitializeWizardsMenu(this, uiContextMenu1, menuWizards, getBaseName, "LogEntries", "Source");
			}
		}

		internal void Explore()
		{
			Source source = SelectedItem;
			Explore(source);
		}

		private void Explore(Source source)
		{
			FileSystemHelper.Explore(source.Path);
		}

		private void filter_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			filterAll.Checked = InheritableBoolean.False;
			RefreshAndSetFilter(true);
		}

		private void filterAll_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ClearFilter();
		}

		private void FocusError()
		{
			Source source = SelectedItem;
			MainForm.FormInstance.FocusEventLog(source.ErrorEventID);
		}

		public IEnumerable<Source> GetAllItems()
		{
			return Entities;
		}

		private string GetConflictedItemsToClipboard()
		{
			SVNLog log = SelectedItem.GetLog(false);
			if (log == null)
			{
				return string.Empty;
			}
			return string.Join(Environment.NewLine, log.PossibleConflictedFilePaths.ToArray());
		}

		private string[] GetDraggedFolderNames(IDataObject data)
		{
			if (data != null)
			{
				string[] folderNames = (string[])data.GetData(DataFormats.FileDrop);
				if ((folderNames != null) && (folderNames.Length != 0))
				{
					return folderNames;
				}
			}
			return new string[0];
		}

		private string GetErrorToClipboard()
		{
			Source source = SelectedItem;
			if (!source.HasError)
			{
				return string.Empty;
			}
			return source.ErrorText;
		}

		private Predicate<Source> GetFilterPredicate()
		{
			return delegate(Source source)
			{
				if (filterPredicates.Keys.All(cmd => !cmd.Checked.ToBool()))
				{
					return true;
				}
				bool aggregatedFilter = false;
				foreach (UICommand cmd in filterPredicates.Keys)
				{
					if (cmd.Checked.ToBool())
					{
						aggregatedFilter |= filterPredicates[cmd](source);
					}
				}
				return aggregatedFilter;
			};
		}

		private string GetModifiedItemsToClipboard()
		{
			Source source = SelectedItem;
			if (source.LocalStatus == null)
			{
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			foreach (SVNStatusEntry entry in source.LocalStatus.ModifiedEntries)
			{
				sb.AppendLine(entry.Path);
			}
			return sb.ToString();
		}

		private string GetSourceNameToClipboard()
		{
			return SelectedItem.Name;
		}

		private string GetSourcePathToClipboard()
		{
			return SelectedItem.Path;
		}

		private string GetSourceUrlToClipboard()
		{
			Source source = SelectedItem;
			if (source.IsURL)
			{
				return source.Path;
			}
			SVNInfo info = source.GetInfo(false);
			if (info == null)
			{
				return string.Empty;
			}
			return info.URL;
		}

		private string GetUnversionedItemsToClipboard()
		{
			Source source = SelectedItem;
			if (source.LocalStatus == null)
			{
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			foreach (SVNStatusEntry entry in source.LocalStatus.UnversionedEntries)
			{
				sb.AppendLine(entry.Path);
			}
			return sb.ToString();
		}

		private void InitializeClipboardDelegates()
		{
			UIHelper.AddCopyCommand(cmdCopySourceName, GetSourceNameToClipboard);
			UIHelper.AddCopyCommand(cmdCopySourceURL, GetSourceUrlToClipboard);
			UIHelper.AddCopyCommand(cmdCopySourcePath, GetSourcePathToClipboard);
			UIHelper.AddCopyCommand(cmdCopySourceConflictedItems, GetConflictedItemsToClipboard);
			UIHelper.AddCopyCommand(cmdCopySourceError, GetErrorToClipboard);
			UIHelper.AddCopyCommand(cmdCopySourceModifiedItems, GetModifiedItemsToClipboard);
			UIHelper.AddCopyCommand(cmdCopySourceUnversionedItems, GetUnversionedItemsToClipboard);
		}

		private void linkShowAllSources_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			ShowAllSources();
		}

		private void LoadFilterSettings()
		{
			loadingFilterSettings = true;
			filterEnabled.Checked = ApplicationSettingsManager.Settings.SourcesFilter_Enabled.ToInheritableBoolean();
			filterModified.Checked = ApplicationSettingsManager.Settings.SourcesFilter_Modified.ToInheritableBoolean();
			filterNotUpToDate.Checked = ApplicationSettingsManager.Settings.SourcesFilter_NotUpToDate.ToInheritableBoolean();
			filterAll.Checked = ((!filterEnabled.Checked.ToBool() && !filterModified.Checked.ToBool()) && !filterNotUpToDate.Checked.ToBool()).ToInheritableBoolean();
			loadingFilterSettings = false;
		}

		private void menuClipboard_Popup(object sender, CommandEventArgs e)
		{
			UIHelper.RefreshCopyCommands(e.Command.Commands);
		}

		private void menuWizards2_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			RunCustomWizard();
		}

		protected virtual void OnSelectionChanged()
		{
			if (selectionChanged != null)
			{
				selectionChanged(this, EventArgs.Empty);
			}
			presenter.EnableCommands();
		}

		private bool QuickCreateNewSource(string folderName, bool interactive)
		{
			Source source = Source.FromURL(folderName);
			if (interactive)
			{
				bool cancel;
				presenter.New(source, out cancel);
				if (cancel)
				{
					return false;
				}
			}
			MonitorSettings.Instance.AddEntity(source);
			return true;
		}

		public void Refetch()
		{
			SourcesExplorerBar.RefreshEntities();
		}

		private void RefreshAndSetFilter(bool saveFilterSettings)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new Action<bool>(RefreshAndSetFilter), new object[]
				{
					saveFilterSettings
				});
			}
			else
			{
				if (saveFilterSettings)
				{
					SaveFilterSettings();
				}
				SearchTextBox.Search(GetFilterPredicate());
			}
		}

		private void RefreshLog()
		{
			Source source = SelectedItem;
			Updater.Instance.RefreshSource(source);
		}

		private void RegisterExplorerBarEvents()
		{
			UnregisterExplorerBarEvents();
			SourcesExplorerBar.ChangesClick += SourcesExplorerBar_ItemChangesClick;
			SourcesExplorerBar.ConflictsClick += SourcesExplorerBar_ItemConflictsClick;
			SourcesExplorerBar.ErrorClick += SourcesExplorerBar_ItemErrorClick;
			SourcesExplorerBar.PathClick += SourcesExplorerBar_ItemPathClick;
			SourcesExplorerBar.SyncdClick += SourcesExplorerBar_ItemSyncdClick;
			SourcesExplorerBar.UnversionedClick += SourcesExplorerBar_ItemUnversionedClick;
			SourcesExplorerBar.UpdatesClick += SourcesExplorerBar_ItemUpdatesClick;
		}

		private void RunCustomWizard()
		{
			Source source = SelectedItem;
			new CustomWizard().Run(source.Name, "LogEntries", "Source");
		}

		private void SaveFilterSettings()
		{
			if (!loadingFilterSettings)
			{
				ApplicationSettingsManager.Settings.SourcesFilter_Enabled = filterEnabled.Checked.ToBool();
				ApplicationSettingsManager.Settings.SourcesFilter_Modified = filterModified.Checked.ToBool();
				ApplicationSettingsManager.Settings.SourcesFilter_NotUpToDate = filterNotUpToDate.Checked.ToBool();
				ApplicationSettingsManager.SaveSettings();
			}
		}

		public void SelectSource(Source source)
		{
			SourcesExplorerBar.SelectEntity(source);
		}

		public void SetSearchResults(IEnumerable<Source> results)
		{
			SourcesExplorerBar.SetVisibleEntities(results);
			int count = results.Count();
			ShowingAllItems = count == Entities.Count;
			OnSelectionChanged();
		}

		private void ShowAllSources()
		{
			SearchTextBox.ClearNoFocus();
			filterAll.Checked = InheritableBoolean.True;
			ClearFilter();
		}

		private void Source_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			SourcesExplorerBar.RefreshEntity((Source)e.Entity);
			presenter.EnableCommands();
			RefreshAndSetFilter(false);
		}

		private void SourceDragDrop(DragEventArgs e)
		{
			if (e.Data != null)
			{
				string[] folderNames = GetDraggedFolderNames(e.Data);
				if ((folderNames != null) && (folderNames.Length != 0))
				{
					int count = 0;
					for (int i = 0; i < folderNames.Length; i++)
					{
						string folderName = folderNames[i];
						if (FileSystemHelper.DirectoryExists(folderName))
						{
							bool interactive = (e.KeyState & 8) != 8;
							if (QuickCreateNewSource(folderName, interactive))
							{
								count++;
							}
						}
					}
					if (count > 0)
					{
						string message = Strings.SourcesCreated_FORMAT.FormatWith(new object[]
						{
							count, (count == 1) ? Strings.SourcesCreated_Source : Strings.SourcesCreated_Sources
						});
						SVNMonitor.EventLog.LogInfo(message, folderNames);
						MainForm.FormInstance.ShowMessage(message, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
		}

		private void SourceDragEnter(DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			if (e.Data != null)
			{
				string[] folderNames = GetDraggedFolderNames(e.Data);
				if ((folderNames != null) && (folderNames.Length != 0))
				{
					e.Effect = DragDropEffects.None;
					foreach (string folderName in folderNames)
					{
						if (FileSystemHelper.DirectoryExists(folderName))
						{
							e.Effect = DragDropEffects.Copy;
							return;
						}
					}
				}
			}
		}

		private void sources1_KeyDown(object sender, KeyEventArgs e)
		{
			presenter.HandleKey(e);
		}

		private void sources1_SelectionChanged(object sender, EventArgs e)
		{
			presenter.EnableCommands();
		}

		private void SourcesExplorerBar_ItemChangesClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SVNCommit();
		}

		private void SourcesExplorerBar_ItemConflictsClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SVNDiff();
		}

		private void SourcesExplorerBar_ItemErrorClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			FocusError();
		}

		private void SourcesExplorerBar_ItemPathClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Browse();
		}

		private void SourcesExplorerBar_ItemSyncdClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SVNCheckModifications();
		}

		private void SourcesExplorerBar_ItemUnversionedClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SVNAdd();
		}

		private void SourcesExplorerBar_ItemUpdatesClick(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdate();
		}

		private void sourcesExplorerBar1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			bool cancel;
			Logger.LogUserAction();
			bool isGroup = SourcesExplorerBar.IsGroupAtLocation(e.Location, out cancel);
			if (!cancel)
			{
				if (isGroup)
				{
					Edit();
				}
				else
				{
					presenter.New();
				}
			}
		}

		private void sourcesExplorerBar1_MouseDown(object sender, MouseEventArgs e)
		{
			SourcesExplorerBar.SelectEntity(e.Location);
		}

		private void SourcesPanel_DragDrop(object sender, DragEventArgs e)
		{
			Logger.LogUserAction();
			SourceDragDrop(e);
		}

		private void SourcesPanel_DragEnter(object sender, DragEventArgs e)
		{
			Logger.LogUserAction();
			SourceDragEnter(e);
		}

		private void SVNAdd()
		{
			Source source = SelectedItem;
			SVNAdd(source);
		}

		private void SVNAdd(Source source)
		{
			source.SVNAdd();
		}

		private void SVNApplyPatch()
		{
			SelectedItem.ApplyPatch();
		}

		private void SVNBranchTag()
		{
			SelectedItem.BranchTag();
		}

		internal void SVNCheckModifications()
		{
			Source source = SelectedItem;
			SVNCheckModifications(source);
		}

		private void SVNCheckModifications(Source source)
		{
			source.CheckModifications();
		}

		private void SVNCheckout()
		{
			SelectedItem.Checkout();
		}

		private void SVNCleanUp()
		{
			SelectedItem.CleanUp();
		}

		internal void SVNCommit()
		{
			Source source = SelectedItem;
			SVNCommit(source);
		}

		private void SVNCommit(Source source)
		{
			source.SVNCommit();
		}

		private void SVNCreatePatch()
		{
			SelectedItem.CreatePatch();
		}

		private void SVNDeleteUnversioned()
		{
			SelectedItem.DeleteUnversioned();
		}

		private void SVNDiff()
		{
			SelectedItem.SVNDiff();
		}

		private void SVNExport()
		{
			SelectedItem.Export();
		}

		private void SVNGetLock()
		{
			SelectedItem.GetLock();
		}

		private void SVNMerge()
		{
			SelectedItem.Merge();
		}

		object ISelectableView<Source>.Invoke(Delegate delegate1)
		{
			return base.Invoke(delegate1);
		}

		void IUserEntityView<Source>.Delete()
		{
			SourcesExplorerBar.Delete();
			OnSelectionChanged();
		}

		DialogResult IUserEntityView<Source>.UserEdit(Source entity)
		{
			Source source = entity;
			Logger.Log.InfoFormat("Editing source (source={0})", source);
			DialogResult result = SourcePropertiesDialog.ShowDialog(source);
			if ((result == DialogResult.OK) && (source != null))
			{
				Logger.Log.InfoFormat("User pressed OK (source={0})", source);
				Updater.Instance.QueueUpdate(source, false);
			}
			return result;
		}

		DialogResult IUserEntityView<Source>.UserNew(Source entity)
		{
			Source source = entity;
			Logger.Log.InfoFormat("Creating source (GUID={0})", source.Guid);
			DialogResult result = SourcePropertiesDialog.ShowDialog(source);
			if ((result == DialogResult.OK) && (source != null))
			{
				Logger.Log.InfoFormat("User pressed OK (source={0})", source);
				SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Source, string.Format("Source '{0}' created", entity.Name), entity);
				if (source.Enabled)
				{
					Logger.Log.InfoFormat("Source is enabled. Updating.", new object[0]);
					SVNMonitor.EventLog.Log(SVNMonitor.EventLogEntryType.Source, "Getting source information...", entity);
					Updater.Instance.QueueUpdate(source, false);
				}
			}
			return result;
		}

		private void SVNProperties()
		{
			SelectedItem.Properties();
		}

		private void SVNReintegrate()
		{
			SelectedItem.Reintegrate();
		}

		private void SVNReleaseLock()
		{
			SelectedItem.ReleaseLock();
		}

		private void SVNRelocate()
		{
			SelectedItem.Relocate();
		}

		private void SVNRepoBrowser()
		{
			SelectedItem.RepoBrowser();
		}

		private void SVNResolve()
		{
			SelectedItem.Resolve();
		}

		internal void SVNRevert()
		{
			SelectedItem.SVNRevert();
		}

		private void SVNRevisionGraph()
		{
			SelectedItem.RevisionGraph();
		}

		private void SVNShowLog()
		{
			SelectedItem.SVNShowLog();
		}

		private void SVNSwitch()
		{
			SelectedItem.Switch();
		}

		internal void SVNUpdate()
		{
			Source source = SelectedItem;
			SVNUpdate(source);
		}

		private void SVNUpdate(Source source)
		{
			DialogResult result = UpdateHeadPromptDialog.Prompt();
			Logger.Log.InfoFormat("Update Source: User clicked {0}", result);
			if (result == DialogResult.Yes)
			{
				source.SVNUpdate();
			}
		}

		private void SVNUpdateAll(bool ignoreUpToDate)
		{
			Source.SVNUpdateAll(ignoreUpToDate);
		}

		private void UnregisterExplorerBarEvents()
		{
			SourcesExplorerBar.ChangesClick -= SourcesExplorerBar_ItemChangesClick;
			SourcesExplorerBar.ConflictsClick -= SourcesExplorerBar_ItemConflictsClick;
			SourcesExplorerBar.ErrorClick -= SourcesExplorerBar_ItemErrorClick;
			SourcesExplorerBar.PathClick -= SourcesExplorerBar_ItemPathClick;
			SourcesExplorerBar.SyncdClick -= SourcesExplorerBar_ItemSyncdClick;
			SourcesExplorerBar.UnversionedClick -= SourcesExplorerBar_ItemUnversionedClick;
			SourcesExplorerBar.UpdatesClick -= SourcesExplorerBar_ItemUpdatesClick;
		}

		internal void UpdateAllSources()
		{
			Updater.Instance.QueueUpdates();
		}

		internal void UpdateSource()
		{
			Source source = SelectedItem;
			Updater.Instance.QueueUpdate(source, true);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanAdditionalTortoiseCommands
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(menuAdditionalTortoiseCommands); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(menuAdditionalTortoiseCommands, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanApplyPatch
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdApplyPatch); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdApplyPatch, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanBranchTag
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdBranchTag); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdBranchTag, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanCheckAllForUpdates
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdUpdateAll); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdUpdateAll, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCheckForUpdates
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdUpdate); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdUpdate, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCheckout
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCheckout); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCheckout, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanCleanUp
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCleanUp); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCleanUp, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanClearAllErrors
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdClearAllErrors); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdClearAllErrors, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanClearError
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdClearError); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdClearError, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanCopyConflictedItems
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopySourceConflictedItems); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopySourceConflictedItems, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyError
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopySourceError); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopySourceError, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanCopyModifiedItems
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopySourceModifiedItems); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopySourceModifiedItems, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyName
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopySourceName); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopySourceName, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyPath
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdCopySourcePath); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdCopySourcePath, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanCopyToClipboard
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(menuClipboard); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(menuClipboard, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyUnversionedItems
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopySourceUnversionedItems); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopySourceUnversionedItems, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanCopyURL
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopySourceURL); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopySourceURL, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCreatePatch
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCreatePatch); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCreatePatch, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanDelete
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdDelete); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdDelete, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanDeleteUnversioned
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdDeleteUnversioned); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdDeleteUnversioned, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanEdit
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdEdit); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdEdit, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanEnable
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdEnabled); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdEnabled, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanExplore
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdExplore); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdExplore, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanExport
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdExport); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdExport, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanGetLock
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdLock); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdLock, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanMerge
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdMerge); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdMerge, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanMoveDown
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdMoveDown); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdMoveDown, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanMoveUp
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdMoveUp); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdMoveUp, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanNew
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdNew); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdNew, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanProperties
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdProperties); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdProperties, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanRefreshLog
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdRefresh); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdRefresh, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanReintegrate
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdReintegrate); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdReintegrate, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanReleaseLock
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdUnlock); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdUnlock, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanRelocate
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdRelocate); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdRelocate, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanRepoBrowser
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdRepoBrowser); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdRepoBrowser, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanResolve
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdResolve); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdResolve, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanRevisionGraph
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdRevisionGraph); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdRevisionGraph, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanRunWizard
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(menuWizards); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(menuWizards, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanShowLog
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdShowLog); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdShowLog, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanSVNCheckForModifications
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdSVNModifications); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdSVNModifications, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanSVNCommit
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdSVNCommit); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdSVNCommit, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanSVNRevert
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdSVNRevert); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdSVNRevert, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanSVNUpdate
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandVisible(cmdSVNUpdate); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandVisible(cmdSVNUpdate, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanSwitch
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdSwitch); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdSwitch, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanTSVNHelp
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdTSVNHelp); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdTSVNHelp, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanTSVNSettings
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdTSVNSettings); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdTSVNSettings, value); }
		}

		public int Count
		{
			[DebuggerNonUserCode]
			get { return SourcesExplorerBar.Count; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<Source> Entities
		{
			[DebuggerNonUserCode]
			get { return allSources; }
			set
			{
				if (allSources != null)
				{
					allSources.ForEach(delegate(Source source) { source.StatusChanged -= Source_StatusChanged; });
				}
				allSources = value;
				if (allSources != null)
				{
					allSources.ForEach(delegate(Source source) { source.StatusChanged += Source_StatusChanged; });
				}
				SourcesExplorerBar.Entities = allSources;
				RegisterExplorerBarEvents();
				RefreshAndSetFilter(false);
			}
		}

		[Browsable(false)]
		public SearchTextBox<Source> SearchTextBox { get; set; }

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			[DebuggerNonUserCode]
			get { return SourcesExplorerBar.SelectedIndex; }
			[DebuggerNonUserCode]
			set { SourcesExplorerBar.SelectedIndex = value; }
		}

		[Browsable(false)]
		public Source SelectedItem
		{
			[DebuggerNonUserCode]
			get { return SourcesExplorerBar.SelectedEntity; }
		}

		public bool ShowingAllItems
		{
			get { return showingAllItems; }
			set
			{
				showingAllItems = value;
				linkShowAllSources.Visible = !showingAllItems;
			}
		}

		private SVNMonitor.View.Controls.SourcesExplorerBar SourcesExplorerBar
		{
			[DebuggerNonUserCode]
			get { return sourcesExplorerBar1; }
		}

		public Janus.Windows.UI.CommandBars.UIContextMenu UIContextMenu
		{
			get { return uiContextMenu1; }
		}
	}
}