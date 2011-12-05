using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Janus.Windows.ExplorerBar;
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
	internal class SourcesPanel : UserControl, IUserEntityView<Source>, ISelectableView<Source>, ISupportInitialize, ISearchablePanel<Source>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Source> allSources;
		private UIRebar BottomRebar1;
		private UICommand cmdApplyPatch;
		private UICommand cmdApplyPatch1;
		private UICommand cmdApplyPatch2;
		private UICommand cmdBranchTag;
		private UICommand cmdBranchTag1;
		private UICommand cmdCheckout;
		private UICommand cmdCheckout1;
		private UICommand cmdCleanUp;
		private UICommand cmdCleanUp1;
		private UICommand cmdClearAllErrors;
		private UICommand cmdClearAllErrors1;
		private UICommand cmdClearError;
		private UICommand cmdClearError1;
		private UICommand cmdCopySourceConflictedItems;
		private UICommand cmdCopySourceConflictedItems1;
		private UICommand cmdCopySourceError;
		private UICommand cmdCopySourceError1;
		private UICommand cmdCopySourceModifiedItems;
		private UICommand cmdCopySourceModifiedItems1;
		private UICommand cmdCopySourceName;
		private UICommand cmdCopySourceName1;
		private UICommand cmdCopySourcePath;
		private UICommand cmdCopySourcePath1;
		private UICommand cmdCopySourceUnversionedItems;
		private UICommand cmdCopySourceUnversionedItems1;
		private UICommand cmdCopySourceURL;
		private UICommand cmdCopySourceUrl1;
		private UICommand cmdCreatePatch;
		private UICommand cmdCreatePatch1;
		private UICommand cmdCreatePatch2;
		private UICommand cmdDelete;
		private UICommand cmdDeleteUnversioned;
		private UICommand cmdDeleteUnversioned1;
		private UICommand cmdEdit;
		private UICommand cmdEnabled;
		private UICommand cmdEnabled1;
		private UICommand cmdExplore;
		private UICommand cmdExplore1;
		private UICommand cmdExport;
		private UICommand cmdExport1;
		private UICommand cmdLock;
		private UICommand cmdLock2;
		private UICommand cmdMerge;
		private UICommand cmdMerge1;
		private UICommand cmdMoveDown;
		private UICommand cmdMoveUp;
		private UICommand cmdNew;
		private UICommand cmdProperties;
		private UICommand cmdProperties1;
		private UICommand cmdProperties2;
		private UICommand cmdRefresh;
		private UICommand cmdRefresh1;
		private UICommand cmdReintegrate;
		private UICommand cmdReintegrate1;
		private UICommand cmdRelocate;
		private UICommand cmdRelocate1;
		private UICommand cmdRepoBrowser;
		private UICommand cmdRepoBrowser1;
		private UICommand cmdResolve;
		private UICommand cmdResolve1;
		private UICommand cmdRevisionGraph;
		private UICommand cmdRevisionGraph1;
		private UICommand cmdShowLog;
		private UICommand cmdShowLog1;
		private UICommand cmdShowLog2;
		private UICommand cmdSVNCommit;
		private UICommand cmdSVNCommit1;
		private UICommand cmdSVNCommit2;
		private UICommand cmdSVNModifications;
		private UICommand cmdSVNModifications1;
		private UICommand cmdSVNModifications2;
		private UICommand cmdSVNRevert;
		private UICommand cmdSVNRevert1;
		private UICommand cmdSVNRevert2;
		private UICommand cmdSVNUpdate;
		private UICommand cmdSVNUpdate1;
		private UICommand cmdSVNUpdate2;
		private UICommand cmdSwitch;
		private UICommand cmdSwitch1;
		private UICommand cmdTSVNHelp;
		private UICommand cmdTSVNHelp1;
		private UICommand cmdTSVNHelp2;
		private UICommand cmdTSVNSettings;
		private UICommand cmdTSVNSettings1;
		private UICommand cmdTSVNSettings2;
		private UICommand cmdUnlock;
		private UICommand cmdUnlock2;
		private UICommand cmdUpdate;
		private UICommand cmdUpdateAll;
		private IContainer components;
		private UICommand filterAll;
		private UICommand filterAll1;
		private UICommand filterEnabled;
		private UICommand filterEnabled1;
		private UICommand filterModified;
		private UICommand filterModified1;
		private UICommand filterModified3;
		private UICommand filterNotUpToDate;
		private UICommand filterNotUpToDate2;
		private UICommand filterNotUpToDate3;
		private Dictionary<UICommand, Predicate<Source>> filterPredicates;
		private ImageList imageList1;
		private UIRebar LeftRebar1;
		private LinkLabel linkShowAllSources;
		private bool loadingFilterSettings;
		private UICommand menuAdditionalTortoiseCommands;
		private UICommand menuAdditionalTortoiseCommands1;
		private UICommand menuClipboard;
		private UICommand menuClipboard1;
		private UICommand menuEvenMore;
		private UICommand menuShow;
		private UICommand menuShow2;
		private UICommand menuWizards;
		private UICommand menuWizards1;
		private UICommand menuWizards2;
		private Panel panel1;
		private readonly UserEntityPresenter<Source> presenter;
		private UIRebar RightRebar1;
		private EventHandler selectionChanged;
		private UICommand Separator10;
		private UICommand Separator11;
		private UICommand Separator12;
		private UICommand Separator13;
		private UICommand Separator14;
		private UICommand Separator15;
		private UICommand Separator4;
		private UICommand Separator5;
		private UICommand Separator6;
		private UICommand Separator7;
		private UICommand Separator8;
		private UICommand Separator9;
		private bool showingAllItems;
		private SVNMonitor.View.Controls.SourcesExplorerBar sourcesExplorerBar1;
		private UIRebar TopRebar1;
		private UICommandBar uiCommandBar1;
		private UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;

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

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
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

		private void InitializeComponent()
		{
			components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(SourcesPanel));
			cmdNew = new UICommand("cmdNew");
			cmdEdit = new UICommand("cmdEdit");
			cmdDelete = new UICommand("cmdDelete");
			cmdUpdate = new UICommand("cmdUpdate");
			cmdUpdateAll = new UICommand("cmdUpdateAll");
			cmdMoveUp = new UICommand("cmdMoveUp");
			cmdMoveDown = new UICommand("cmdMoveDown");
			imageList1 = new ImageList(components);
			uiCommandManager1 = new UICommandManager(components);
			BottomRebar1 = new UIRebar();
			uiCommandBar1 = new UICommandBar();
			cmdSVNModifications1 = new UICommand("cmdSVNModifications");
			cmdShowLog1 = new UICommand("cmdShowLog");
			cmdSVNUpdate2 = new UICommand("cmdSVNUpdate");
			cmdSVNCommit2 = new UICommand("cmdSVNCommit");
			cmdSVNRevert1 = new UICommand("cmdSVNRevert");
			Separator7 = new UICommand("Separator");
			Separator10 = new UICommand("Separator");
			Separator6 = new UICommand("Separator");
			menuWizards2 = new UICommand("menuWizards");
			menuWizards = new UICommand("menuWizards");
			cmdSVNUpdate = new UICommand("cmdSVNUpdate");
			cmdSVNCommit = new UICommand("cmdSVNCommit");
			cmdSVNRevert = new UICommand("cmdSVNRevert");
			cmdSVNModifications = new UICommand("cmdSVNModifications");
			cmdRefresh = new UICommand("cmdRefresh");
			cmdEnabled = new UICommand("cmdEnabled");
			cmdExplore = new UICommand("cmdExplore");
			cmdClearError = new UICommand("cmdClearError");
			cmdShowLog = new UICommand("cmdShowLog");
			menuClipboard = new UICommand("menuClipboard");
			cmdCopySourceName1 = new UICommand("cmdCopySourceName");
			cmdCopySourcePath1 = new UICommand("cmdCopySourcePath");
			cmdCopySourceUrl1 = new UICommand("cmdCopySourceURL");
			cmdCopySourceError1 = new UICommand("cmdCopySourceError");
			cmdCopySourceModifiedItems1 = new UICommand("cmdCopySourceModifiedItems");
			cmdCopySourceConflictedItems1 = new UICommand("cmdCopySourceConflictedItems");
			cmdCopySourceUnversionedItems1 = new UICommand("cmdCopySourceUnversionedItems");
			cmdCopySourceName = new UICommand("cmdCopySourceName");
			cmdCopySourcePath = new UICommand("cmdCopySourcePath");
			cmdCopySourceURL = new UICommand("cmdCopySourceURL");
			cmdCopySourceError = new UICommand("cmdCopySourceError");
			cmdCopySourceModifiedItems = new UICommand("cmdCopySourceModifiedItems");
			cmdCopySourceUnversionedItems = new UICommand("cmdCopySourceUnversionedItems");
			cmdCopySourceConflictedItems = new UICommand("cmdCopySourceConflictedItems");
			menuAdditionalTortoiseCommands = new UICommand("menuAdditionalTortoiseCommands");
			cmdCheckout1 = new UICommand("cmdCheckout");
			cmdRepoBrowser1 = new UICommand("cmdRepoBrowser");
			cmdRevisionGraph1 = new UICommand("cmdRevisionGraph");
			cmdResolve1 = new UICommand("cmdResolve");
			cmdCleanUp1 = new UICommand("cmdCleanUp");
			cmdDeleteUnversioned1 = new UICommand("cmdDeleteUnversioned");
			cmdLock2 = new UICommand("cmdLock");
			cmdUnlock2 = new UICommand("cmdUnlock");
			Separator13 = new UICommand("Separator");
			cmdBranchTag1 = new UICommand("cmdBranchTag");
			cmdSwitch1 = new UICommand("cmdSwitch");
			cmdMerge1 = new UICommand("cmdMerge");
			cmdReintegrate1 = new UICommand("cmdReintegrate");
			cmdExport1 = new UICommand("cmdExport");
			cmdRelocate1 = new UICommand("cmdRelocate");
			Separator4 = new UICommand("Separator");
			cmdCreatePatch2 = new UICommand("cmdCreatePatch");
			cmdApplyPatch2 = new UICommand("cmdApplyPatch");
			Separator11 = new UICommand("Separator");
			cmdProperties2 = new UICommand("cmdProperties");
			Separator14 = new UICommand("Separator");
			cmdTSVNSettings2 = new UICommand("cmdTSVNSettings");
			cmdTSVNHelp2 = new UICommand("cmdTSVNHelp");
			cmdRepoBrowser = new UICommand("cmdRepoBrowser");
			cmdRevisionGraph = new UICommand("cmdRevisionGraph");
			cmdCleanUp = new UICommand("cmdCleanUp");
			cmdExport = new UICommand("cmdExport");
			cmdSwitch = new UICommand("cmdSwitch");
			cmdRelocate = new UICommand("cmdRelocate");
			cmdProperties = new UICommand("cmdProperties");
			cmdCheckout = new UICommand("cmdCheckout");
			cmdResolve = new UICommand("cmdResolve");
			cmdMerge = new UICommand("cmdMerge");
			cmdReintegrate = new UICommand("cmdReintegrate");
			cmdBranchTag = new UICommand("cmdBranchTag");
			cmdCreatePatch = new UICommand("cmdCreatePatch");
			cmdApplyPatch = new UICommand("cmdApplyPatch");
			cmdLock = new UICommand("cmdLock");
			cmdUnlock = new UICommand("cmdUnlock");
			cmdDeleteUnversioned = new UICommand("cmdDeleteUnversioned");
			cmdTSVNSettings = new UICommand("cmdTSVNSettings");
			cmdTSVNHelp = new UICommand("cmdTSVNHelp");
			menuEvenMore = new UICommand("menuEvenMore");
			cmdCreatePatch1 = new UICommand("cmdCreatePatch");
			cmdApplyPatch1 = new UICommand("cmdApplyPatch");
			cmdProperties1 = new UICommand("cmdProperties");
			Separator12 = new UICommand("Separator");
			cmdTSVNSettings1 = new UICommand("cmdTSVNSettings");
			cmdTSVNHelp1 = new UICommand("cmdTSVNHelp");
			filterModified = new UICommand("filterModified");
			filterNotUpToDate = new UICommand("filterNotUpToDate");
			filterAll = new UICommand("filterAll");
			menuShow = new UICommand("menuShow");
			filterAll1 = new UICommand("filterAll");
			Separator15 = new UICommand("Separator");
			filterModified3 = new UICommand("filterModified");
			filterNotUpToDate3 = new UICommand("filterNotUpToDate");
			filterEnabled1 = new UICommand("filterEnabled");
			cmdClearAllErrors = new UICommand("cmdClearAllErrors");
			filterEnabled = new UICommand("filterEnabled");
			uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			cmdEnabled1 = new UICommand("cmdEnabled");
			cmdClearError1 = new UICommand("cmdClearError");
			cmdClearAllErrors1 = new UICommand("cmdClearAllErrors");
			cmdExplore1 = new UICommand("cmdExplore");
			menuClipboard1 = new UICommand("menuClipboard");
			cmdSVNModifications2 = new UICommand("cmdSVNModifications");
			cmdShowLog2 = new UICommand("cmdShowLog");
			cmdSVNUpdate1 = new UICommand("cmdSVNUpdate");
			cmdSVNCommit1 = new UICommand("cmdSVNCommit");
			cmdSVNRevert2 = new UICommand("cmdSVNRevert");
			menuAdditionalTortoiseCommands1 = new UICommand("menuAdditionalTortoiseCommands");
			Separator8 = new UICommand("Separator");
			Separator9 = new UICommand("Separator");
			cmdRefresh1 = new UICommand("cmdRefresh");
			menuShow2 = new UICommand("menuShow");
			Separator5 = new UICommand("Separator");
			menuWizards1 = new UICommand("menuWizards");
			LeftRebar1 = new UIRebar();
			RightRebar1 = new UIRebar();
			TopRebar1 = new UIRebar();
			sourcesExplorerBar1 = new SVNMonitor.View.Controls.SourcesExplorerBar();
			panel1 = new Panel();
			linkShowAllSources = new LinkLabel();
			filterModified1 = new UICommand("filterModified");
			filterNotUpToDate2 = new UICommand("filterNotUpToDate");
			UICommand cmdUpdate1 = new UICommand("cmdUpdate");
			UICommand cmdUpdateAll1 = new UICommand("cmdUpdateAll");
			UICommand Separator2 = new UICommand("Separator");
			UICommand cmdNew2 = new UICommand("cmdNew");
			UICommand cmdEdit2 = new UICommand("cmdEdit");
			UICommand cmdDelete2 = new UICommand("cmdDelete");
			UICommand Separator1 = new UICommand("Separator");
			UICommand cmdMoveUp2 = new UICommand("cmdMoveUp");
			UICommand cmdMoveDown2 = new UICommand("cmdMoveDown");
			UICommand cmdNew1 = new UICommand("cmdNew");
			UICommand cmdEdit1 = new UICommand("cmdEdit");
			UICommand cmdDelete1 = new UICommand("cmdDelete");
			UICommand cmdMoveUp1 = new UICommand("cmdMoveUp");
			UICommand cmdMoveDown1 = new UICommand("cmdMoveDown");
			UICommand cmdUpdate2 = new UICommand("cmdUpdate");
			UICommand cmdUpdateAll2 = new UICommand("cmdUpdateAll");
			UICommand Separator3 = new UICommand("Separator");
			((ISupportInitialize)uiCommandManager1).BeginInit();
			((ISupportInitialize)BottomRebar1).BeginInit();
			((ISupportInitialize)uiCommandBar1).BeginInit();
			((ISupportInitialize)uiContextMenu1).BeginInit();
			((ISupportInitialize)LeftRebar1).BeginInit();
			((ISupportInitialize)RightRebar1).BeginInit();
			((ISupportInitialize)TopRebar1).BeginInit();
			TopRebar1.SuspendLayout();
			((ISupportInitialize)sourcesExplorerBar1).BeginInit();
			panel1.SuspendLayout();
			base.SuspendLayout();
			cmdUpdate1.Key = "cmdUpdate";
			cmdUpdate1.Name = "cmdUpdate1";
			cmdUpdate1.Text = "Chec&k for updates";
			cmdUpdateAll1.Key = "cmdUpdateAll";
			cmdUpdateAll1.Name = "cmdUpdateAll1";
			cmdUpdateAll1.Text = "Check &all for updates";
			Separator2.CommandType = CommandType.Separator;
			Separator2.Key = "Separator";
			Separator2.Name = "Separator2";
			cmdNew2.Key = "cmdNew";
			cmdNew2.Name = "cmdNew2";
			cmdNew2.Text = "&New Source";
			cmdEdit2.Key = "cmdEdit";
			cmdEdit2.Name = "cmdEdit2";
			cmdEdit2.Text = "&Properties";
			cmdDelete2.Key = "cmdDelete";
			cmdDelete2.Name = "cmdDelete2";
			cmdDelete2.Text = "&Delete";
			Separator1.CommandType = CommandType.Separator;
			Separator1.Key = "Separator";
			Separator1.Name = "Separator1";
			cmdMoveUp2.Key = "cmdMoveUp";
			cmdMoveUp2.Name = "cmdMoveUp2";
			cmdMoveUp2.Text = "Mo&ve Up";
			cmdMoveDown2.Key = "cmdMoveDown";
			cmdMoveDown2.Name = "cmdMoveDown2";
			cmdMoveDown2.Text = "Move Do&wn";
			cmdNew1.CommandStyle = CommandStyle.Image;
			cmdNew1.Key = "cmdNew";
			cmdNew1.Name = "cmdNew1";
			cmdEdit1.CommandStyle = CommandStyle.Image;
			cmdEdit1.Key = "cmdEdit";
			cmdEdit1.Name = "cmdEdit1";
			cmdDelete1.CommandStyle = CommandStyle.Image;
			cmdDelete1.Key = "cmdDelete";
			cmdDelete1.Name = "cmdDelete1";
			cmdMoveUp1.CommandStyle = CommandStyle.Image;
			cmdMoveUp1.Key = "cmdMoveUp";
			cmdMoveUp1.Name = "cmdMoveUp1";
			cmdMoveDown1.CommandStyle = CommandStyle.Image;
			cmdMoveDown1.Key = "cmdMoveDown";
			cmdMoveDown1.Name = "cmdMoveDown1";
			cmdUpdate2.CommandStyle = CommandStyle.Image;
			cmdUpdate2.Key = "cmdUpdate";
			cmdUpdate2.Name = "cmdUpdate2";
			cmdUpdateAll2.CommandStyle = CommandStyle.Image;
			cmdUpdateAll2.Key = "cmdUpdateAll";
			cmdUpdateAll2.Name = "cmdUpdateAll2";
			Separator3.CommandType = CommandType.Separator;
			Separator3.Key = "Separator";
			Separator3.Name = "Separator3";
			cmdNew.Image = (Image)resources.GetObject("cmdNew.Image");
			cmdNew.Key = "cmdNew";
			cmdNew.Name = "cmdNew";
			cmdNew.Text = "New Source";
			cmdNew.ToolTipText = "New source";
			cmdNew.Click += cmdNew_Click;
			cmdEdit.Image = (Image)resources.GetObject("cmdEdit.Image");
			cmdEdit.Key = "cmdEdit";
			cmdEdit.Name = "cmdEdit";
			cmdEdit.Text = "Properties";
			cmdEdit.ToolTipText = "Properties";
			cmdEdit.Click += cmdEdit_Click;
			cmdDelete.Image = (Image)resources.GetObject("cmdDelete.Image");
			cmdDelete.Key = "cmdDelete";
			cmdDelete.Name = "cmdDelete";
			cmdDelete.Text = "Delete";
			cmdDelete.ToolTipText = "Delete source";
			cmdDelete.Click += cmdDelete_Click;
			cmdUpdate.Image = (Image)resources.GetObject("cmdUpdate.Image");
			cmdUpdate.Key = "cmdUpdate";
			cmdUpdate.Name = "cmdUpdate";
			cmdUpdate.Text = "Check for updates";
			cmdUpdate.ToolTipText = "Check for updates";
			cmdUpdate.Click += cmdUpdate_Click;
			cmdUpdateAll.Image = (Image)resources.GetObject("cmdUpdateAll.Image");
			cmdUpdateAll.Key = "cmdUpdateAll";
			cmdUpdateAll.Name = "cmdUpdateAll";
			cmdUpdateAll.Text = "Check all for updates";
			cmdUpdateAll.ToolTipText = "Check all for updates";
			cmdUpdateAll.Click += cmdUpdateAll_Click;
			cmdMoveUp.Image = (Image)resources.GetObject("cmdMoveUp.Image");
			cmdMoveUp.Key = "cmdMoveUp";
			cmdMoveUp.Name = "cmdMoveUp";
			cmdMoveUp.Text = "Move Up";
			cmdMoveUp.ToolTipText = "Move source up";
			cmdMoveUp.Click += cmdMoveUp_Click;
			cmdMoveDown.Image = (Image)resources.GetObject("cmdMoveDown.Image");
			cmdMoveDown.Key = "cmdMoveDown";
			cmdMoveDown.Name = "cmdMoveDown";
			cmdMoveDown.Text = "Move Down";
			cmdMoveDown.ToolTipText = "Move source down";
			cmdMoveDown.Click += cmdMoveDown_Click;
			imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = Color.Transparent;
			imageList1.Images.SetKeyName(0, "wc.png");
			imageList1.Images.SetKeyName(1, "wc.updating.png");
			imageList1.Images.SetKeyName(2, "repo.error.png");
			imageList1.Images.SetKeyName(3, "repo.png");
			imageList1.Images.SetKeyName(4, "repo.updating.png");
			imageList1.Images.SetKeyName(5, "wc.downdate.modified.png");
			imageList1.Images.SetKeyName(6, "wc.downdate.png");
			imageList1.Images.SetKeyName(7, "wc.error.png");
			imageList1.Images.SetKeyName(8, "wc.modified.png");
			uiCommandManager1.AllowClose = InheritableBoolean.False;
			uiCommandManager1.AllowCustomize = InheritableBoolean.False;
			uiCommandManager1.BottomRebar = BottomRebar1;
			uiCommandManager1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			uiCommandManager1.Commands.AddRange(new[]
			{
				cmdNew, cmdEdit, cmdDelete, cmdUpdate, cmdUpdateAll, cmdMoveUp, cmdMoveDown, menuWizards, cmdSVNUpdate, cmdSVNCommit, cmdSVNRevert, cmdSVNModifications, cmdRefresh, cmdEnabled, cmdExplore, cmdClearError,
				cmdShowLog, menuClipboard, cmdCopySourceName, cmdCopySourcePath, cmdCopySourceURL, cmdCopySourceError, cmdCopySourceModifiedItems, cmdCopySourceUnversionedItems, cmdCopySourceConflictedItems, menuAdditionalTortoiseCommands, cmdRepoBrowser, cmdRevisionGraph, cmdCleanUp, cmdExport, cmdSwitch, cmdRelocate,
				cmdProperties, cmdCheckout, cmdResolve, cmdMerge, cmdReintegrate, cmdBranchTag, cmdCreatePatch, cmdApplyPatch, cmdLock, cmdUnlock, cmdDeleteUnversioned, cmdTSVNSettings, cmdTSVNHelp, menuEvenMore, filterModified, filterNotUpToDate,
				filterAll, menuShow, cmdClearAllErrors, filterEnabled
			});
			uiCommandManager1.ContainerControl = this;
			uiCommandManager1.ContextMenus.AddRange(new[]
			{
				uiContextMenu1
			});
			uiCommandManager1.Id = new Guid("6dcff238-beed-4562-b52d-d0ba855fecb0");
			uiCommandManager1.LeftRebar = LeftRebar1;
			uiCommandManager1.LockCommandBars = true;
			uiCommandManager1.RightRebar = RightRebar1;
			uiCommandManager1.ShowAddRemoveButton = InheritableBoolean.False;
			uiCommandManager1.ShowQuickCustomizeMenu = false;
			uiCommandManager1.TopRebar = TopRebar1;
			uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			BottomRebar1.CommandManager = uiCommandManager1;
			BottomRebar1.Dock = DockStyle.Bottom;
			BottomRebar1.Location = new Point(0, 0x152);
			BottomRebar1.Name = "BottomRebar1";
			BottomRebar1.Size = new Size(0x1f9, 0);
			uiCommandBar1.AllowClose = InheritableBoolean.False;
			uiCommandBar1.AllowCustomize = InheritableBoolean.False;
			uiCommandBar1.Animation = DropDownAnimation.System;
			uiCommandBar1.CommandManager = uiCommandManager1;
			uiCommandBar1.Commands.AddRange(new[]
			{
				cmdNew1, cmdEdit1, cmdDelete1, Separator3, cmdSVNModifications1, cmdShowLog1, cmdSVNUpdate2, cmdSVNCommit2, cmdSVNRevert1, Separator7, cmdUpdate2, cmdUpdateAll2, Separator10, cmdMoveUp1, cmdMoveDown1, Separator6,
				menuWizards2
			});
			uiCommandBar1.FullRow = true;
			uiCommandBar1.Key = "CommandBar1";
			uiCommandBar1.Location = new Point(0, 0);
			uiCommandBar1.Name = "uiCommandBar1";
			uiCommandBar1.RowIndex = 0;
			uiCommandBar1.ShowAddRemoveButton = InheritableBoolean.False;
			uiCommandBar1.Size = new Size(0x1d4, 0x1c);
			uiCommandBar1.Text = "Source";
			cmdSVNModifications1.CommandStyle = CommandStyle.Image;
			cmdSVNModifications1.Key = "cmdSVNModifications";
			cmdSVNModifications1.Name = "cmdSVNModifications1";
			cmdShowLog1.CommandStyle = CommandStyle.Image;
			cmdShowLog1.Key = "cmdShowLog";
			cmdShowLog1.Name = "cmdShowLog1";
			cmdSVNUpdate2.CommandStyle = CommandStyle.Image;
			cmdSVNUpdate2.Key = "cmdSVNUpdate";
			cmdSVNUpdate2.Name = "cmdSVNUpdate2";
			cmdSVNCommit2.CommandStyle = CommandStyle.Image;
			cmdSVNCommit2.Key = "cmdSVNCommit";
			cmdSVNCommit2.Name = "cmdSVNCommit2";
			cmdSVNRevert1.CommandStyle = CommandStyle.Image;
			cmdSVNRevert1.Key = "cmdSVNRevert";
			cmdSVNRevert1.Name = "cmdSVNRevert1";
			Separator7.CommandType = CommandType.Separator;
			Separator7.Key = "Separator";
			Separator7.Name = "Separator7";
			Separator10.CommandType = CommandType.Separator;
			Separator10.Key = "Separator";
			Separator10.Name = "Separator10";
			Separator6.CommandType = CommandType.Separator;
			Separator6.Key = "Separator";
			Separator6.Name = "Separator6";
			menuWizards2.CommandStyle = CommandStyle.Image;
			menuWizards2.Key = "menuWizards";
			menuWizards2.Name = "menuWizards2";
			menuWizards2.Click += menuWizards2_Click;
			menuWizards.CommandType = CommandType.ControlPopup;
			menuWizards.Image = (Image)resources.GetObject("menuWizards.Image");
			menuWizards.Key = "menuWizards";
			menuWizards.Name = "menuWizards";
			menuWizards.Text = "Monitor this source";
			menuWizards.ToolTipText = "Monitor this source";
			cmdSVNUpdate.Image = (Image)resources.GetObject("cmdSVNUpdate.Image");
			cmdSVNUpdate.Key = "cmdSVNUpdate";
			cmdSVNUpdate.Name = "cmdSVNUpdate";
			cmdSVNUpdate.Text = "Update";
			cmdSVNUpdate.ToolTipText = "Update";
			cmdSVNUpdate.Visible = InheritableBoolean.False;
			cmdSVNUpdate.Click += cmdSVNUpdate_Click;
			cmdSVNCommit.Image = (Image)resources.GetObject("cmdSVNCommit.Image");
			cmdSVNCommit.Key = "cmdSVNCommit";
			cmdSVNCommit.Name = "cmdSVNCommit";
			cmdSVNCommit.Text = "Commit";
			cmdSVNCommit.ToolTipText = "Commit";
			cmdSVNCommit.Visible = InheritableBoolean.False;
			cmdSVNCommit.Click += cmdSVNCommit_Click;
			cmdSVNRevert.Image = (Image)resources.GetObject("cmdSVNRevert.Image");
			cmdSVNRevert.Key = "cmdSVNRevert";
			cmdSVNRevert.Name = "cmdSVNRevert";
			cmdSVNRevert.Text = "Revert";
			cmdSVNRevert.ToolTipText = "Revert";
			cmdSVNRevert.Click += cmdSVNRevert_Click;
			cmdSVNModifications.Image = (Image)resources.GetObject("cmdSVNModifications.Image");
			cmdSVNModifications.Key = "cmdSVNModifications";
			cmdSVNModifications.Name = "cmdSVNModifications";
			cmdSVNModifications.Text = "Check for modifications";
			cmdSVNModifications.ToolTipText = "Check for modifications";
			cmdSVNModifications.Click += cmdSVNModifications_Click;
			cmdRefresh.Image = (Image)resources.GetObject("cmdRefresh.Image");
			cmdRefresh.Key = "cmdRefresh";
			cmdRefresh.Name = "cmdRefresh";
			cmdRefresh.Text = "Refresh log (might take a few minutes...)";
			cmdRefresh.ToolTipText = "Refresh log (might take a few minutes...)";
			cmdRefresh.Click += cmdRefresh_Click;
			cmdEnabled.CommandType = CommandType.ToggleButton;
			cmdEnabled.Key = "cmdEnabled";
			cmdEnabled.Name = "cmdEnabled";
			cmdEnabled.Text = "Enabled";
			cmdEnabled.ToolTipText = "Enabled";
			cmdEnabled.Click += cmdEnabled_Click;
			cmdExplore.Image = (Image)resources.GetObject("cmdExplore.Image");
			cmdExplore.Key = "cmdExplore";
			cmdExplore.Name = "cmdExplore";
			cmdExplore.Text = "Explore";
			cmdExplore.ToolTipText = "Explore";
			cmdExplore.Click += cmdExplore_Click;
			cmdClearError.Image = (Image)resources.GetObject("cmdClearError.Image");
			cmdClearError.Key = "cmdClearError";
			cmdClearError.Name = "cmdClearError";
			cmdClearError.Text = "Clear error";
			cmdClearError.ToolTipText = "Clear Error";
			cmdClearError.Click += cmdClearError_Click;
			cmdShowLog.Image = (Image)resources.GetObject("cmdShowLog.Image");
			cmdShowLog.Key = "cmdShowLog";
			cmdShowLog.Name = "cmdShowLog";
			cmdShowLog.Text = "Show log";
			cmdShowLog.ToolTipText = "Show log";
			cmdShowLog.Click += cmdShowLog_Click;
			menuClipboard.Commands.AddRange(new[]
			{
				cmdCopySourceName1, cmdCopySourcePath1, cmdCopySourceUrl1, cmdCopySourceError1, cmdCopySourceModifiedItems1, cmdCopySourceConflictedItems1, cmdCopySourceUnversionedItems1
			});
			menuClipboard.Image = (Image)resources.GetObject("menuClipboard.Image");
			menuClipboard.Key = "menuClipboard";
			menuClipboard.Name = "menuClipboard";
			menuClipboard.Text = "Copy to clipboard";
			menuClipboard.Popup += menuClipboard_Popup;
			cmdCopySourceName1.Key = "cmdCopySourceName";
			cmdCopySourceName1.Name = "cmdCopySourceName1";
			cmdCopySourcePath1.Key = "cmdCopySourcePath";
			cmdCopySourcePath1.Name = "cmdCopySourcePath1";
			cmdCopySourceUrl1.Key = "cmdCopySourceURL";
			cmdCopySourceUrl1.Name = "cmdCopySourceUrl1";
			cmdCopySourceError1.Key = "cmdCopySourceError";
			cmdCopySourceError1.Name = "cmdCopySourceError1";
			cmdCopySourceModifiedItems1.Key = "cmdCopySourceModifiedItems";
			cmdCopySourceModifiedItems1.Name = "cmdCopySourceModifiedItems1";
			cmdCopySourceConflictedItems1.Key = "cmdCopySourceConflictedItems";
			cmdCopySourceConflictedItems1.Name = "cmdCopySourceConflictedItems1";
			cmdCopySourceUnversionedItems1.Key = "cmdCopySourceUnversionedItems";
			cmdCopySourceUnversionedItems1.Name = "cmdCopySourceUnversionedItems1";
			cmdCopySourceName.Image = (Image)resources.GetObject("cmdCopySourceName.Image");
			cmdCopySourceName.Key = "cmdCopySourceName";
			cmdCopySourceName.Name = "cmdCopySourceName";
			cmdCopySourceName.Text = "&Name";
			cmdCopySourcePath.Image = (Image)resources.GetObject("cmdCopySourcePath.Image");
			cmdCopySourcePath.Key = "cmdCopySourcePath";
			cmdCopySourcePath.Name = "cmdCopySourcePath";
			cmdCopySourcePath.Text = "&Path";
			cmdCopySourceURL.Image = (Image)resources.GetObject("cmdCopySourceURL.Image");
			cmdCopySourceURL.Key = "cmdCopySourceURL";
			cmdCopySourceURL.Name = "cmdCopySourceURL";
			cmdCopySourceURL.Text = "&URL";
			cmdCopySourceError.Image = (Image)resources.GetObject("cmdCopySourceError.Image");
			cmdCopySourceError.Key = "cmdCopySourceError";
			cmdCopySourceError.Name = "cmdCopySourceError";
			cmdCopySourceError.Text = "&Error";
			cmdCopySourceModifiedItems.Image = (Image)resources.GetObject("cmdCopySourceModifiedItems.Image");
			cmdCopySourceModifiedItems.Key = "cmdCopySourceModifiedItems";
			cmdCopySourceModifiedItems.Name = "cmdCopySourceModifiedItems";
			cmdCopySourceModifiedItems.Text = "&Modified items list";
			cmdCopySourceUnversionedItems.Image = (Image)resources.GetObject("cmdCopySourceUnversionedItems.Image");
			cmdCopySourceUnversionedItems.Key = "cmdCopySourceUnversionedItems";
			cmdCopySourceUnversionedItems.Name = "cmdCopySourceUnversionedItems";
			cmdCopySourceUnversionedItems.Text = "Un&versioned items list";
			cmdCopySourceConflictedItems.Image = (Image)resources.GetObject("cmdCopySourceConflictedItems.Image");
			cmdCopySourceConflictedItems.Key = "cmdCopySourceConflictedItems";
			cmdCopySourceConflictedItems.Name = "cmdCopySourceConflictedItems";
			cmdCopySourceConflictedItems.Text = "Possible &conflicted items list";
			menuAdditionalTortoiseCommands.Commands.AddRange(new[]
			{
				cmdCheckout1, cmdRepoBrowser1, cmdRevisionGraph1, cmdResolve1, cmdCleanUp1, cmdDeleteUnversioned1, cmdLock2, cmdUnlock2, Separator13, cmdBranchTag1, cmdSwitch1, cmdMerge1, cmdReintegrate1, cmdExport1, cmdRelocate1, Separator4,
				cmdCreatePatch2, cmdApplyPatch2, Separator11, cmdProperties2, Separator14, cmdTSVNSettings2, cmdTSVNHelp2
			});
			menuAdditionalTortoiseCommands.Image = (Image)resources.GetObject("menuAdditionalTortoiseCommands.Image");
			menuAdditionalTortoiseCommands.Key = "menuAdditionalTortoiseCommands";
			menuAdditionalTortoiseCommands.Name = "menuAdditionalTortoiseCommands";
			menuAdditionalTortoiseCommands.Text = "More";
			cmdCheckout1.Key = "cmdCheckout";
			cmdCheckout1.Name = "cmdCheckout1";
			cmdRepoBrowser1.Key = "cmdRepoBrowser";
			cmdRepoBrowser1.Name = "cmdRepoBrowser1";
			cmdRevisionGraph1.Key = "cmdRevisionGraph";
			cmdRevisionGraph1.Name = "cmdRevisionGraph1";
			cmdResolve1.Key = "cmdResolve";
			cmdResolve1.Name = "cmdResolve1";
			cmdCleanUp1.Key = "cmdCleanUp";
			cmdCleanUp1.Name = "cmdCleanUp1";
			cmdDeleteUnversioned1.Key = "cmdDeleteUnversioned";
			cmdDeleteUnversioned1.Name = "cmdDeleteUnversioned1";
			cmdLock2.Key = "cmdLock";
			cmdLock2.Name = "cmdLock2";
			cmdUnlock2.Key = "cmdUnlock";
			cmdUnlock2.Name = "cmdUnlock2";
			Separator13.CommandType = CommandType.Separator;
			Separator13.Key = "Separator";
			Separator13.Name = "Separator13";
			cmdBranchTag1.Key = "cmdBranchTag";
			cmdBranchTag1.Name = "cmdBranchTag1";
			cmdSwitch1.Key = "cmdSwitch";
			cmdSwitch1.Name = "cmdSwitch1";
			cmdMerge1.Key = "cmdMerge";
			cmdMerge1.Name = "cmdMerge1";
			cmdReintegrate1.Key = "cmdReintegrate";
			cmdReintegrate1.Name = "cmdReintegrate1";
			cmdExport1.Key = "cmdExport";
			cmdExport1.Name = "cmdExport1";
			cmdRelocate1.Key = "cmdRelocate";
			cmdRelocate1.Name = "cmdRelocate1";
			Separator4.CommandType = CommandType.Separator;
			Separator4.Key = "Separator";
			Separator4.Name = "Separator4";
			cmdCreatePatch2.Key = "cmdCreatePatch";
			cmdCreatePatch2.Name = "cmdCreatePatch2";
			cmdApplyPatch2.Key = "cmdApplyPatch";
			cmdApplyPatch2.Name = "cmdApplyPatch2";
			Separator11.CommandType = CommandType.Separator;
			Separator11.Key = "Separator";
			Separator11.Name = "Separator11";
			cmdProperties2.Key = "cmdProperties";
			cmdProperties2.Name = "cmdProperties2";
			Separator14.CommandType = CommandType.Separator;
			Separator14.Key = "Separator";
			Separator14.Name = "Separator14";
			cmdTSVNSettings2.Key = "cmdTSVNSettings";
			cmdTSVNSettings2.Name = "cmdTSVNSettings2";
			cmdTSVNHelp2.Key = "cmdTSVNHelp";
			cmdTSVNHelp2.Name = "cmdTSVNHelp2";
			cmdRepoBrowser.Image = (Image)resources.GetObject("cmdRepoBrowser.Image");
			cmdRepoBrowser.Key = "cmdRepoBrowser";
			cmdRepoBrowser.Name = "cmdRepoBrowser";
			cmdRepoBrowser.Text = "Repo &browser";
			cmdRepoBrowser.Click += cmdRepoBrowser_Click;
			cmdRevisionGraph.Image = (Image)resources.GetObject("cmdRevisionGraph.Image");
			cmdRevisionGraph.Key = "cmdRevisionGraph";
			cmdRevisionGraph.Name = "cmdRevisionGraph";
			cmdRevisionGraph.Text = "Revision &graph";
			cmdRevisionGraph.Click += cmdRevisionGraph_Click;
			cmdCleanUp.Image = (Image)resources.GetObject("cmdCleanUp.Image");
			cmdCleanUp.Key = "cmdCleanUp";
			cmdCleanUp.Name = "cmdCleanUp";
			cmdCleanUp.Text = "&Clean up";
			cmdCleanUp.Click += cmdCleanUp_Click;
			cmdExport.Image = (Image)resources.GetObject("cmdExport.Image");
			cmdExport.Key = "cmdExport";
			cmdExport.Name = "cmdExport";
			cmdExport.Text = "&Export";
			cmdExport.Click += cmdExport_Click;
			cmdSwitch.Image = (Image)resources.GetObject("cmdSwitch.Image");
			cmdSwitch.Key = "cmdSwitch";
			cmdSwitch.Name = "cmdSwitch";
			cmdSwitch.Text = "&Switch";
			cmdSwitch.Click += cmdSwitch_Click;
			cmdRelocate.Image = (Image)resources.GetObject("cmdRelocate.Image");
			cmdRelocate.Key = "cmdRelocate";
			cmdRelocate.Name = "cmdRelocate";
			cmdRelocate.Text = "&Relocate";
			cmdRelocate.Click += cmdRelocate_Click;
			cmdProperties.Image = (Image)resources.GetObject("cmdProperties.Image");
			cmdProperties.Key = "cmdProperties";
			cmdProperties.Name = "cmdProperties";
			cmdProperties.Text = "SVN-&Properties";
			cmdProperties.Click += cmdProperties_Click;
			cmdCheckout.Image = (Image)resources.GetObject("cmdCheckout.Image");
			cmdCheckout.Key = "cmdCheckout";
			cmdCheckout.Name = "cmdCheckout";
			cmdCheckout.Text = "Chec&kout";
			cmdCheckout.Click += cmdCheckout_Click;
			cmdResolve.Key = "cmdResolve";
			cmdResolve.Name = "cmdResolve";
			cmdResolve.Text = "Resolve";
			cmdResolve.Click += cmdResolve_Click;
			cmdMerge.Key = "cmdMerge";
			cmdMerge.Name = "cmdMerge";
			cmdMerge.Text = "Merge";
			cmdMerge.Click += cmdMerge_Click;
			cmdReintegrate.Key = "cmdReintegrate";
			cmdReintegrate.Name = "cmdReintegrate";
			cmdReintegrate.Text = "Reintegrate";
			cmdReintegrate.Click += cmdReintegrate_Click;
			cmdBranchTag.Key = "cmdBranchTag";
			cmdBranchTag.Name = "cmdBranchTag";
			cmdBranchTag.Text = "Branch/Tag";
			cmdBranchTag.Click += cmdBranchTag_Click;
			cmdCreatePatch.Key = "cmdCreatePatch";
			cmdCreatePatch.Name = "cmdCreatePatch";
			cmdCreatePatch.Text = "Create Patch";
			cmdCreatePatch.Click += cmdCreatePatch_Click;
			cmdApplyPatch.Key = "cmdApplyPatch";
			cmdApplyPatch.Name = "cmdApplyPatch";
			cmdApplyPatch.Text = "Apply Patch";
			cmdApplyPatch.Click += cmdApplyPatch_Click;
			cmdLock.Key = "cmdLock";
			cmdLock.Name = "cmdLock";
			cmdLock.Text = "Get Lock";
			cmdLock.Click += cmdLock_Click;
			cmdUnlock.Key = "cmdUnlock";
			cmdUnlock.Name = "cmdUnlock";
			cmdUnlock.Text = "Release Lock";
			cmdUnlock.Click += cmdUnlock_Click;
			cmdDeleteUnversioned.Key = "cmdDeleteUnversioned";
			cmdDeleteUnversioned.Name = "cmdDeleteUnversioned";
			cmdDeleteUnversioned.Text = "Delete Unversioned Items";
			cmdDeleteUnversioned.Click += cmdDeleteUnversioned_Click;
			cmdTSVNSettings.Key = "cmdTSVNSettings";
			cmdTSVNSettings.Name = "cmdTSVNSettings";
			cmdTSVNSettings.Text = "TortoiseSVN Settings";
			cmdTSVNSettings.Click += cmdTSVNSettings_Click;
			cmdTSVNHelp.Key = "cmdTSVNHelp";
			cmdTSVNHelp.Name = "cmdTSVNHelp";
			cmdTSVNHelp.Text = "TortoiseSVN Help";
			cmdTSVNHelp.Click += cmdTSVNHelp_Click;
			menuEvenMore.Commands.AddRange(new[]
			{
				cmdCreatePatch1, cmdApplyPatch1, cmdProperties1, Separator12, cmdTSVNSettings1, cmdTSVNHelp1
			});
			menuEvenMore.Key = "menuEvenMore";
			menuEvenMore.Name = "menuEvenMore";
			menuEvenMore.Text = "Even More";
			cmdCreatePatch1.Key = "cmdCreatePatch";
			cmdCreatePatch1.Name = "cmdCreatePatch1";
			cmdApplyPatch1.Key = "cmdApplyPatch";
			cmdApplyPatch1.Name = "cmdApplyPatch1";
			cmdProperties1.Key = "cmdProperties";
			cmdProperties1.Name = "cmdProperties1";
			Separator12.CommandType = CommandType.Separator;
			Separator12.Key = "Separator";
			Separator12.Name = "Separator12";
			cmdTSVNSettings1.Key = "cmdTSVNSettings";
			cmdTSVNSettings1.Name = "cmdTSVNSettings1";
			cmdTSVNHelp1.Key = "cmdTSVNHelp";
			cmdTSVNHelp1.Name = "cmdTSVNHelp1";
			filterModified.CommandType = CommandType.ToggleButton;
			filterModified.Key = "filterModified";
			filterModified.Name = "filterModified";
			filterModified.Text = "&Modified";
			filterModified.Click += filter_Click;
			filterNotUpToDate.CommandType = CommandType.ToggleButton;
			filterNotUpToDate.Key = "filterNotUpToDate";
			filterNotUpToDate.Name = "filterNotUpToDate";
			filterNotUpToDate.Text = "&Has Updates";
			filterNotUpToDate.Click += filter_Click;
			filterAll.Checked = InheritableBoolean.True;
			filterAll.CommandType = CommandType.ToggleButton;
			filterAll.Key = "filterAll";
			filterAll.Name = "filterAll";
			filterAll.Text = "&All";
			filterAll.Click += filterAll_Click;
			menuShow.Commands.AddRange(new[]
			{
				filterAll1, Separator15, filterModified3, filterNotUpToDate3, filterEnabled1
			});
			menuShow.IsEditableControl = InheritableBoolean.False;
			menuShow.Key = "menuShow";
			menuShow.Name = "menuShow";
			menuShow.Text = "&Show";
			filterAll1.Key = "filterAll";
			filterAll1.Name = "filterAll1";
			Separator15.CommandType = CommandType.Separator;
			Separator15.Key = "Separator";
			Separator15.Name = "Separator15";
			filterModified3.Key = "filterModified";
			filterModified3.Name = "filterModified3";
			filterNotUpToDate3.Key = "filterNotUpToDate";
			filterNotUpToDate3.Name = "filterNotUpToDate3";
			filterEnabled1.Key = "filterEnabled";
			filterEnabled1.Name = "filterEnabled1";
			cmdClearAllErrors.Key = "cmdClearAllErrors";
			cmdClearAllErrors.Name = "cmdClearAllErrors";
			cmdClearAllErrors.Text = "Clear All Errors";
			cmdClearAllErrors.Click += cmdClearAllErrors_Click;
			filterEnabled.CommandType = CommandType.ToggleButton;
			filterEnabled.Key = "filterEnabled";
			filterEnabled.Name = "filterEnabled";
			filterEnabled.Text = "&Enabled";
			filterEnabled.Click += filter_Click;
			uiContextMenu1.CommandManager = uiCommandManager1;
			uiContextMenu1.Commands.AddRange(new[]
			{
				cmdEnabled1, cmdClearError1, cmdClearAllErrors1, cmdExplore1, cmdNew2, cmdEdit2, cmdDelete2, menuClipboard1, Separator2, cmdSVNModifications2, cmdShowLog2, cmdSVNUpdate1, cmdSVNCommit1, cmdSVNRevert2, menuAdditionalTortoiseCommands1, Separator8,
				cmdUpdate1, cmdUpdateAll1, Separator9, cmdRefresh1, Separator1, cmdMoveUp2, cmdMoveDown2, menuShow2, Separator5, menuWizards1
			});
			uiContextMenu1.Key = "ContextMenu1";
			cmdEnabled1.Key = "cmdEnabled";
			cmdEnabled1.Name = "cmdEnabled1";
			cmdEnabled1.Text = "&Enabled";
			cmdClearError1.Key = "cmdClearError";
			cmdClearError1.Name = "cmdClearError1";
			cmdClearError1.Text = "Clear &error";
			cmdClearAllErrors1.Key = "cmdClearAllErrors";
			cmdClearAllErrors1.Name = "cmdClearAllErrors1";
			cmdExplore1.Key = "cmdExplore";
			cmdExplore1.Name = "cmdExplore1";
			cmdExplore1.Text = "E&xplore";
			menuClipboard1.Key = "menuClipboard";
			menuClipboard1.Name = "menuClipboard1";
			menuClipboard1.Text = "Copy &to clipboard";
			cmdSVNModifications2.Key = "cmdSVNModifications";
			cmdSVNModifications2.Name = "cmdSVNModifications2";
			cmdSVNModifications2.Text = "Check modi&fications";
			cmdShowLog2.Key = "cmdShowLog";
			cmdShowLog2.Name = "cmdShowLog2";
			cmdShowLog2.Text = "&Show log";
			cmdSVNUpdate1.Key = "cmdSVNUpdate";
			cmdSVNUpdate1.Name = "cmdSVNUpdate1";
			cmdSVNUpdate1.Text = "&Update";
			cmdSVNCommit1.Key = "cmdSVNCommit";
			cmdSVNCommit1.Name = "cmdSVNCommit1";
			cmdSVNCommit1.Text = "&Commit";
			cmdSVNRevert2.Key = "cmdSVNRevert";
			cmdSVNRevert2.Name = "cmdSVNRevert2";
			cmdSVNRevert2.Text = "Re&vert";
			menuAdditionalTortoiseCommands1.Key = "menuAdditionalTortoiseCommands";
			menuAdditionalTortoiseCommands1.Name = "menuAdditionalTortoiseCommands1";
			menuAdditionalTortoiseCommands1.Text = "M&ore";
			Separator8.CommandType = CommandType.Separator;
			Separator8.Key = "Separator";
			Separator8.Name = "Separator8";
			Separator9.CommandType = CommandType.Separator;
			Separator9.Key = "Separator";
			Separator9.Name = "Separator9";
			cmdRefresh1.Key = "cmdRefresh";
			cmdRefresh1.Name = "cmdRefresh1";
			cmdRefresh1.Text = "&Refresh log (might take a few minutes...)";
			menuShow2.Key = "menuShow";
			menuShow2.Name = "menuShow2";
			Separator5.CommandType = CommandType.Separator;
			Separator5.Key = "Separator";
			Separator5.Name = "Separator5";
			menuWizards1.Key = "menuWizards";
			menuWizards1.Name = "menuWizards1";
			menuWizards1.Text = "&Monitor this source";
			LeftRebar1.CommandManager = uiCommandManager1;
			LeftRebar1.Dock = DockStyle.Left;
			LeftRebar1.Location = new Point(0, 0x1c);
			LeftRebar1.Name = "LeftRebar1";
			LeftRebar1.Size = new Size(0, 310);
			RightRebar1.CommandManager = uiCommandManager1;
			RightRebar1.Dock = DockStyle.Right;
			RightRebar1.Location = new Point(0x1f9, 0x1c);
			RightRebar1.Name = "RightRebar1";
			RightRebar1.Size = new Size(0, 310);
			TopRebar1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			TopRebar1.CommandManager = uiCommandManager1;
			TopRebar1.Controls.Add(uiCommandBar1);
			TopRebar1.Dock = DockStyle.Top;
			TopRebar1.Location = new Point(0, 0);
			TopRebar1.Name = "TopRebar1";
			TopRebar1.Size = new Size(0x1d4, 0x1c);
			sourcesExplorerBar1.BackgroundFormatStyle.BackColor = Color.White;
			sourcesExplorerBar1.BackgroundFormatStyle.BackColorGradient = Color.Transparent;
			sourcesExplorerBar1.BorderStyle = Janus.Windows.ExplorerBar.BorderStyle.None;
			uiCommandManager1.SetContextMenu(sourcesExplorerBar1, uiContextMenu1);
			sourcesExplorerBar1.Dock = DockStyle.Fill;
			sourcesExplorerBar1.GroupSeparation = 14;
			sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackColor = Color.Gainsboro;
			sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackColorGradient = Color.Silver;
			sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			sourcesExplorerBar1.GroupsStateStyles.FormatStyle.ForeColor = Color.Black;
			sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColor = Color.Silver;
			sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColorGradient = Color.Gainsboro;
			sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackgroundThemeStyle = BackgroundThemeStyle.GroupHeader;
			sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.ForeColor = Color.Black;
			sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.ForegroundThemeStyle = ForegroundThemeStyle.GroupHeader;
			sourcesExplorerBar1.GroupsStateStyles.SelectedFormatStyle.BackColor = SystemColors.Control;
			sourcesExplorerBar1.Location = new Point(0, 0x17);
			sourcesExplorerBar1.Name = "sourcesExplorerBar1";
			sourcesExplorerBar1.Size = new Size(0x1d4, 0x15a);
			sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColor = Color.LightSkyBlue;
			sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColorGradient = Color.SteelBlue;
			sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.ForeColor = Color.White;
			sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackColor = Color.SteelBlue;
			sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackColorGradient = Color.LightSkyBlue;
			sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackgroundGradientMode = Janus.Windows.ExplorerBar.BackgroundGradientMode.Vertical;
			sourcesExplorerBar1.TabIndex = 1;
			sourcesExplorerBar1.ThemedAreas = ThemedArea.None;
			sourcesExplorerBar1.TopMargin = 14;
			sourcesExplorerBar1.KeyDown += sources1_KeyDown;
			sourcesExplorerBar1.SelectionChanged += sources1_SelectionChanged;
			sourcesExplorerBar1.MouseDoubleClick += sourcesExplorerBar1_MouseDoubleClick;
			sourcesExplorerBar1.MouseDown += sourcesExplorerBar1_MouseDown;
			panel1.BackColor = Color.DimGray;
			panel1.Controls.Add(sourcesExplorerBar1);
			panel1.Controls.Add(linkShowAllSources);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0x1c);
			panel1.Name = "panel1";
			panel1.Padding = new Padding(0, 1, 0, 0);
			panel1.Size = new Size(0x1d4, 0x171);
			panel1.TabIndex = 2;
			linkShowAllSources.ActiveLinkColor = Color.FromArgb(0xff, 0xff, 0xc0);
			linkShowAllSources.BackColor = Color.Moccasin;
			linkShowAllSources.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			linkShowAllSources.Dock = DockStyle.Top;
			linkShowAllSources.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
			linkShowAllSources.LinkBehavior = LinkBehavior.NeverUnderline;
			linkShowAllSources.LinkColor = Color.Black;
			linkShowAllSources.Location = new Point(0, 1);
			linkShowAllSources.Name = "linkShowAllSources";
			linkShowAllSources.Size = new Size(0x1d4, 0x16);
			linkShowAllSources.TabIndex = 2;
			linkShowAllSources.TabStop = true;
			linkShowAllSources.Text = "Some sources are hidden. Click here to show all.";
			linkShowAllSources.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			linkShowAllSources.Visible = false;
			linkShowAllSources.LinkClicked += linkShowAllSources_LinkClicked;
			filterModified1.Key = "filterModified";
			filterModified1.Name = "filterModified1";
			filterNotUpToDate2.Key = "filterNotUpToDate";
			filterNotUpToDate2.Name = "filterNotUpToDate2";
			AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(panel1);
			base.Controls.Add(TopRebar1);
			base.Name = "SourcesPanel";
			base.Size = new Size(0x1d4, 0x18d);
			base.DragDrop += SourcesPanel_DragDrop;
			base.DragEnter += SourcesPanel_DragEnter;
			((ISupportInitialize)uiCommandManager1).EndInit();
			((ISupportInitialize)BottomRebar1).EndInit();
			((ISupportInitialize)uiCommandBar1).EndInit();
			((ISupportInitialize)uiContextMenu1).EndInit();
			((ISupportInitialize)LeftRebar1).EndInit();
			((ISupportInitialize)RightRebar1).EndInit();
			((ISupportInitialize)TopRebar1).EndInit();
			TopRebar1.ResumeLayout(false);
			((ISupportInitialize)sourcesExplorerBar1).EndInit();
			panel1.ResumeLayout(false);
			base.ResumeLayout(false);
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