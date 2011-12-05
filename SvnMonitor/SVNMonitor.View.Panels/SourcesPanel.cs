using System.Windows.Forms;
using SVNMonitor.View.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using Janus.Windows.UI.CommandBars;
using System;
using SVNMonitor.Presenters;
using SVNMonitor.View.Controls;
using SVNMonitor.Helpers;
using SVNMonitor.Entities;
using Janus.Windows.UI;
using SVNMonitor.Logging;
using SVNMonitor.SVN;
using SVNMonitor.View;
using System.Text;
using System.Drawing;
using Janus.Windows.ExplorerBar;
using SVNMonitor.Settings;
using SVNMonitor;
using SVNMonitor.Wizards;
using SVNMonitor.Resources.Text;
using SVNMonitor.View.Dialogs;

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

	private SourcesExplorerBar sourcesExplorerBar1;

	private UIRebar TopRebar1;

	private UICommandBar uiCommandBar1;

	private UICommandManager uiCommandManager1;

	private UIContextMenu uiContextMenu1;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanAdditionalTortoiseCommands
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.menuAdditionalTortoiseCommands);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.menuAdditionalTortoiseCommands, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanApplyPatch
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdApplyPatch);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdApplyPatch, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanBranchTag
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdBranchTag);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdBranchTag, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanCheckAllForUpdates
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdUpdateAll);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdUpdateAll, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanCheckForUpdates
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdUpdate);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdUpdate, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanCheckout
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCheckout);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCheckout, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanCleanUp
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCleanUp);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCleanUp, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanClearAllErrors
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdClearAllErrors);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdClearAllErrors, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanClearError
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdClearError);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdClearError, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanCopyConflictedItems
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCopySourceConflictedItems);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCopySourceConflictedItems, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanCopyError
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCopySourceError);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCopySourceError, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanCopyModifiedItems
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCopySourceModifiedItems);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCopySourceModifiedItems, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanCopyName
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCopySourceName);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCopySourceName, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanCopyPath
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdCopySourcePath);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdCopySourcePath, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanCopyToClipboard
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.menuClipboard);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.menuClipboard, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanCopyUnversionedItems
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCopySourceUnversionedItems);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCopySourceUnversionedItems, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanCopyURL
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCopySourceURL);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCopySourceURL, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanCreatePatch
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdCreatePatch);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdCreatePatch, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanDelete
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdDelete);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdDelete, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanDeleteUnversioned
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdDeleteUnversioned);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdDeleteUnversioned, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanEdit
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdEdit);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdEdit, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanEnable
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdEnabled);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdEnabled, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanExplore
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdExplore);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdExplore, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanExport
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdExport);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdExport, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanGetLock
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdLock);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdLock, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanMerge
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdMerge);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdMerge, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanMoveDown
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdMoveDown);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdMoveDown, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanMoveUp
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdMoveUp);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdMoveUp, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanNew
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdNew);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdNew, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanProperties
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdProperties);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdProperties, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanRefreshLog
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdRefresh);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdRefresh, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanReintegrate
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdReintegrate);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdReintegrate, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanReleaseLock
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdUnlock);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdUnlock, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanRelocate
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdRelocate);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdRelocate, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanRepoBrowser
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdRepoBrowser);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdRepoBrowser, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanResolve
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdResolve);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdResolve, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanRevisionGraph
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdRevisionGraph);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdRevisionGraph, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanRunWizard
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.menuWizards);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.menuWizards, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanShowLog
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdShowLog);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdShowLog, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanSVNCheckForModifications
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdSVNModifications);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdSVNModifications, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanSVNCommit
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdSVNCommit);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdSVNCommit, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanSVNRevert
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdSVNRevert);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdSVNRevert, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanSVNUpdate
	{
		get
		{
			return UIHelper.IsCommandVisible(this.cmdSVNUpdate);
		}
		set
		{
			UIHelper.SetCommandVisible(this.cmdSVNUpdate, value);
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanSwitch
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdSwitch);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdSwitch, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanTSVNHelp
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdTSVNHelp);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdTSVNHelp, value);
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool CanTSVNSettings
	{
		get
		{
			return UIHelper.IsCommandEnabled(this.cmdTSVNSettings);
		}
		set
		{
			UIHelper.SetCommandEnabled(this.cmdTSVNSettings, value);
		}
	}

	public int Count
	{
		get
		{
			return this.SourcesExplorerBar.Count;
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public List<Source> Entities
	{
		get
		{
			return this.allSources;
		}
		set
		{
			Action<Source> action1 = null;
			Action<Source> action2 = null;
			if (this.allSources != null)
			{
			}
			this.allSources = value;
			if (this.allSources != null)
			{
			}
			this.SourcesExplorerBar.Entities = this.allSources;
			this.RegisterExplorerBarEvents();
			this.RefreshAndSetFilter(false);
		}
	}

	[Browsable(false)]
	public SearchTextBox<Source> SearchTextBox
	{
		get;
		set;
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int SelectedIndex
	{
		get
		{
			return this.SourcesExplorerBar.SelectedIndex;
		}
		set
		{
			this.SourcesExplorerBar.SelectedIndex = value;
		}
	}

	[Browsable(false)]
	public Source SelectedItem
	{
		get
		{
			return this.SourcesExplorerBar.SelectedEntity;
		}
	}

	public bool ShowingAllItems
	{
		get
		{
			return this.showingAllItems;
		}
		set
		{
			this.showingAllItems = value;
			this.linkShowAllSources.Visible = this.showingAllItems == 0;
		}
	}

	private SourcesExplorerBar SourcesExplorerBar
	{
		get
		{
			return this.sourcesExplorerBar1;
		}
	}

	public UIContextMenu UIContextMenu
	{
		get
		{
			return this.uiContextMenu1;
		}
	}

	public SourcesPanel()
	{
		this.InitializeComponent();
		if (base.DesignMode || ProcessHelper.IsInVisualStudio())
		{
			return;
		}
		UIHelper.ApplyResources(this.uiCommandManager1);
		this.presenter = new UserEntityPresenter<Source>(this);
		this.InitializeClipboardDelegates();
		this.CreateFiltersMap();
		this.LoadFilterSettings();
	}

	public void BeginInit()
	{
	}

	internal void Browse()
	{
		Source source = this.SelectedItem;
		FileSystemHelper.Browse(source.Path);
	}

	private void ClearAllErrors()
	{
		Source.ClearAllErrors();
	}

	private void ClearError()
	{
		Source source = this.SelectedItem;
		source.ClearError();
	}

	private void ClearFilter()
	{
		this.filterAll.Checked = InheritableBoolean.True;
		foreach (UICommand cmd in this.filterPredicates.Keys)
		{
			cmd.Checked = InheritableBoolean.False;
		}
		this.RefreshAndSetFilter(true);
	}

	public void ClearSearch()
	{
		this.SourcesExplorerBar.SetVisibleEntities();
		this.ClearFilter();
		this.ShowingAllItems = true;
		this.OnSelectionChanged();
	}

	private void cmdApplyPatch_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNApplyPatch();
	}

	private void cmdBranchTag_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNBranchTag();
	}

	private void cmdCheckout_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNCheckout();
	}

	private void cmdCleanUp_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNCleanUp();
	}

	private void cmdClearAllErrors_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.ClearAllErrors();
	}

	private void cmdClearError_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.ClearError();
	}

	private void cmdCreatePatch_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNCreatePatch();
	}

	private void cmdDelete_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.presenter.Delete();
	}

	private void cmdDeleteUnversioned_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNDeleteUnversioned();
	}

	private void cmdEdit_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.Edit();
	}

	private void cmdEnabled_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.EnableSource();
	}

	private void cmdExplore_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.Browse();
	}

	private void cmdExport_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNExport();
	}

	private void cmdLock_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNGetLock();
	}

	private void cmdMerge_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNMerge();
	}

	private void cmdMoveDown_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.presenter.MoveDown();
	}

	private void cmdMoveUp_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.presenter.MoveUp();
	}

	private void cmdNew_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.CreateNew();
	}

	private void cmdProperties_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNProperties();
	}

	private void cmdRefresh_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.RefreshLog();
	}

	private void cmdReintegrate_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNReintegrate();
	}

	private void cmdRelocate_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNRelocate();
	}

	private void cmdRepoBrowser_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNRepoBrowser();
	}

	private void cmdResolve_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNResolve();
	}

	private void cmdRevisionGraph_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNRevisionGraph();
	}

	private void cmdShowLog_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNShowLog();
	}

	private void cmdSVNCommit_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNCommit();
	}

	private void cmdSVNModifications_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNCheckModifications();
	}

	private void cmdSVNRevert_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNRevert();
	}

	private void cmdSVNUpdate_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNUpdate();
	}

	private void cmdSwitch_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.SVNSwitch();
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
		this.SVNReleaseLock();
	}

	private void cmdUpdate_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.UpdateSource();
	}

	private void cmdUpdateAll_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.UpdateAllSources();
	}

	private void CreateFiltersMap()
	{
		this.filterPredicates = new Dictionary<UICommand, Predicate<Source>>();
		this.filterPredicates.Add(this.filterEnabled, new Predicate<Source>((source) => source.Enabled));
		this.filterPredicates.Add(this.filterModified, new Predicate<Source>((source) => source.HasLocalChanges));
		this.filterPredicates.Add(this.filterNotUpToDate, new Predicate<Source>((source) => !source.IsUpToDate));
	}

	public virtual void CreateNew()
	{
		this.presenter.New();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && this.components != null)
		{
			this.components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void Edit()
	{
		this.presenter.Edit();
	}

	public void EnableCommands()
	{
		if (base.DesignMode || ProcessHelper.IsInVisualStudio())
		{
			return;
		}
		if (base.InvokeRequired)
		{
			base.BeginInvoke(new MethodInvoker(this.EnableCommands));
			return;
		}
		Source source = this.SelectedItem;
		this.CanCheckForUpdates = SourceHelper.CanCheckForUpdates(source);
		this.CanRunWizard = SourceHelper.CanRunWizard(source);
		this.CanSVNCheckForModifications = SourceHelper.CanSVNCheckForModifications(source);
		this.CanRefreshLog = SourceHelper.CanRefreshLog(source);
		this.CanSVNUpdate = SourceHelper.CanSVNUpdate(source);
		this.CanEnable = SourceHelper.CanEnable(source);
		this.CanSVNCommit = SourceHelper.CanSVNCommit(source);
		this.CanSVNRevert = SourceHelper.CanSVNRevert(source);
		this.CanExplore = SourceHelper.CanExplore(source);
		this.CanClearError = SourceHelper.CanClearError(source);
		this.CanClearAllErrors = SourceHelper.CanClearAllErrors();
		this.CanShowLog = SourceHelper.CanShowSVNLog(source);
		this.CanCopyToClipboard = SourceHelper.CanCopyToClipboard(source);
		this.CanCopyName = SourceHelper.CanCopyName(source);
		this.CanCopyURL = SourceHelper.CanCopyURL(source);
		this.CanCopyPath = SourceHelper.CanCopyPath(source);
		this.CanCopyError = SourceHelper.CanCopyError(source);
		this.CanCopyModifiedItems = SourceHelper.CanCopyModifiedItems(source);
		this.CanCopyUnversionedItems = SourceHelper.CanCopyUnversionedItems(source);
		this.CanCopyConflictedItems = SourceHelper.CanCopyConflictedItems(source);
		this.CanAdditionalTortoiseCommands = SourceHelper.CanAdditionalTortoiseCommands(source);
		this.CanCleanUp = SourceHelper.CanCleanUp(source);
		this.CanExport = SourceHelper.CanExport(source);
		this.CanProperties = SourceHelper.CanProperties(source);
		this.CanRelocate = SourceHelper.CanRelocate(source);
		this.CanRepoBrowser = SourceHelper.CanRepoBrowser(source);
		this.CanRevisionGraph = SourceHelper.CanRevisionGraph(source);
		this.CanSwitch = SourceHelper.CanSwitch(source);
		this.CanCheckout = SourceHelper.CanCheckout(source);
		this.CanApplyPatch = SourceHelper.CanApplyPatch(source);
		this.CanBranchTag = SourceHelper.CanBranchTag(source);
		this.CanCreatePatch = SourceHelper.CanCreatePatch(source);
		this.CanDeleteUnversioned = SourceHelper.CanDeleteUnversioned(source);
		this.CanGetLock = SourceHelper.CanGetLock(source);
		this.CanMerge = SourceHelper.CanMerge(source);
		this.CanReintegrate = SourceHelper.CanReintegrate(source);
		this.CanResolve = SourceHelper.CanResolve(source);
		this.CanTSVNHelp = SourceHelper.CanTSVNHelp(source);
		this.CanTSVNSettings = SourceHelper.CanTSVNSettings(source);
		this.CanReleaseLock = SourceHelper.CanReleaseLock(source);
		this.CanCheckAllForUpdates = this.SourcesExplorerBar.Groups.Count > 0;
		this.cmdEnabled.Checked = (source == null ? 0 : source.Enabled).ToInheritableBoolean();
	}

	private void EnableSource()
	{
		Source source = this.SelectedItem;
		source.Enabled = this.cmdEnabled.Checked == 1;
	}

	public void EndInit()
	{
		if (!base.DesignMode)
		{
			GetBaseName getBaseName = new GetBaseName((item) => {
				Source source = (Source)item;
				return source.Name;
			}
			);
			UIHelper.InitializeWizardsMenu<Source>(this, this.uiContextMenu1, this.menuWizards, getBaseName, "LogEntries", "Source");
		}
	}

	internal void Explore()
	{
		Source source = this.SelectedItem;
		this.Explore(source);
	}

	private void Explore(Source source)
	{
		FileSystemHelper.Explore(source.Path);
	}

	private void filter_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.filterAll.Checked = InheritableBoolean.False;
		this.RefreshAndSetFilter(true);
	}

	private void filterAll_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.ClearFilter();
	}

	private void FocusError()
	{
		Source source = this.SelectedItem;
		MainForm.FormInstance.FocusEventLog(source.ErrorEventID);
	}

	public IEnumerable<Source> GetAllItems()
	{
		return this.Entities;
	}

	private string GetConflictedItemsToClipboard()
	{
		Source source = this.SelectedItem;
		SVNLog log = source.GetLog(false);
		if (log == null)
		{
			return string.Empty;
		}
		string text = string.Join(Environment.NewLine, log.PossibleConflictedFilePaths.ToArray());
		return text;
	}

	private string[] GetDraggedFolderNames(IDataObject data)
	{
		if (data == null)
		{
			return new string[0];
		}
		string[] folderNames = (string[])data.GetData(DataFormats.FileDrop);
		if (folderNames == null || (int)folderNames.Length == 0)
		{
			return new string[0];
		}
		return folderNames;
	}

	private string GetErrorToClipboard()
	{
		Source source = this.SelectedItem;
		if (!source.HasError)
		{
			return string.Empty;
		}
		return source.ErrorText;
	}

	private Predicate<Source> GetFilterPredicate()
	{
		Predicate<Source> predicate = new Predicate<Source>((source) => {
			if (this.filterPredicates.Keys.All<UICommand>(new Predicate<UICommand>((cmd) => !cmd.Checked.ToBool())))
			{
				return 1;
			}
			bool aggregatedFilter = false;
			foreach (UICommand cmd in this.filterPredicates.Keys)
			{
				if (cmd.Checked.ToBool())
				{
					aggregatedFilter = aggregatedFilter | this.filterPredicates[cmd](source);
				}
			}
			return aggregatedFilter;
		}
		);
		return predicate;
	}

	private string GetModifiedItemsToClipboard()
	{
		Source source = this.SelectedItem;
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
		Source source = this.SelectedItem;
		return source.Name;
	}

	private string GetSourcePathToClipboard()
	{
		Source source = this.SelectedItem;
		return source.Path;
	}

	private string GetSourceUrlToClipboard()
	{
		Source source = this.SelectedItem;
		if (source.IsURL)
		{
			return source.Path;
		}
		SVNInfo info = source.GetInfo(false);
		if (info == null)
		{
			return string.Empty;
		}
		string text = info.URL;
		return text;
	}

	private string GetUnversionedItemsToClipboard()
	{
		Source source = this.SelectedItem;
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
		UIHelper.AddCopyCommand(this.cmdCopySourceName, new GetStringDelegate(this.GetSourceNameToClipboard));
		UIHelper.AddCopyCommand(this.cmdCopySourceURL, new GetStringDelegate(this.GetSourceUrlToClipboard));
		UIHelper.AddCopyCommand(this.cmdCopySourcePath, new GetStringDelegate(this.GetSourcePathToClipboard));
		UIHelper.AddCopyCommand(this.cmdCopySourceConflictedItems, new GetStringDelegate(this.GetConflictedItemsToClipboard));
		UIHelper.AddCopyCommand(this.cmdCopySourceError, new GetStringDelegate(this.GetErrorToClipboard));
		UIHelper.AddCopyCommand(this.cmdCopySourceModifiedItems, new GetStringDelegate(this.GetModifiedItemsToClipboard));
		UIHelper.AddCopyCommand(this.cmdCopySourceUnversionedItems, new GetStringDelegate(this.GetUnversionedItemsToClipboard));
	}

	private void InitializeComponent()
	{
		UICommandBar[] uICommandBarArray;
		UICommand[] uICommandArray;
		UIContextMenu[] uIContextMenuArray;
		UICommand[] uICommandArray2;
		UICommand[] uICommandArray3;
		UICommand[] uICommandArray4;
		UICommand[] uICommandArray5;
		UICommand[] uICommandArray6;
		UICommand[] uICommandArray7;
		UICommandBar[] uICommandBarArray2;
		this.components = new Container();
		ComponentResourceManager resources = new ComponentResourceManager(typeof(SourcesPanel));
		this.cmdNew = new UICommand("cmdNew");
		this.cmdEdit = new UICommand("cmdEdit");
		this.cmdDelete = new UICommand("cmdDelete");
		this.cmdUpdate = new UICommand("cmdUpdate");
		this.cmdUpdateAll = new UICommand("cmdUpdateAll");
		this.cmdMoveUp = new UICommand("cmdMoveUp");
		this.cmdMoveDown = new UICommand("cmdMoveDown");
		this.imageList1 = new ImageList(this.components);
		this.uiCommandManager1 = new UICommandManager(this.components);
		this.BottomRebar1 = new UIRebar();
		this.uiCommandBar1 = new UICommandBar();
		this.cmdSVNModifications1 = new UICommand("cmdSVNModifications");
		this.cmdShowLog1 = new UICommand("cmdShowLog");
		this.cmdSVNUpdate2 = new UICommand("cmdSVNUpdate");
		this.cmdSVNCommit2 = new UICommand("cmdSVNCommit");
		this.cmdSVNRevert1 = new UICommand("cmdSVNRevert");
		this.Separator7 = new UICommand("Separator");
		this.Separator10 = new UICommand("Separator");
		this.Separator6 = new UICommand("Separator");
		this.menuWizards2 = new UICommand("menuWizards");
		this.menuWizards = new UICommand("menuWizards");
		this.cmdSVNUpdate = new UICommand("cmdSVNUpdate");
		this.cmdSVNCommit = new UICommand("cmdSVNCommit");
		this.cmdSVNRevert = new UICommand("cmdSVNRevert");
		this.cmdSVNModifications = new UICommand("cmdSVNModifications");
		this.cmdRefresh = new UICommand("cmdRefresh");
		this.cmdEnabled = new UICommand("cmdEnabled");
		this.cmdExplore = new UICommand("cmdExplore");
		this.cmdClearError = new UICommand("cmdClearError");
		this.cmdShowLog = new UICommand("cmdShowLog");
		this.menuClipboard = new UICommand("menuClipboard");
		this.cmdCopySourceName1 = new UICommand("cmdCopySourceName");
		this.cmdCopySourcePath1 = new UICommand("cmdCopySourcePath");
		this.cmdCopySourceUrl1 = new UICommand("cmdCopySourceURL");
		this.cmdCopySourceError1 = new UICommand("cmdCopySourceError");
		this.cmdCopySourceModifiedItems1 = new UICommand("cmdCopySourceModifiedItems");
		this.cmdCopySourceConflictedItems1 = new UICommand("cmdCopySourceConflictedItems");
		this.cmdCopySourceUnversionedItems1 = new UICommand("cmdCopySourceUnversionedItems");
		this.cmdCopySourceName = new UICommand("cmdCopySourceName");
		this.cmdCopySourcePath = new UICommand("cmdCopySourcePath");
		this.cmdCopySourceURL = new UICommand("cmdCopySourceURL");
		this.cmdCopySourceError = new UICommand("cmdCopySourceError");
		this.cmdCopySourceModifiedItems = new UICommand("cmdCopySourceModifiedItems");
		this.cmdCopySourceUnversionedItems = new UICommand("cmdCopySourceUnversionedItems");
		this.cmdCopySourceConflictedItems = new UICommand("cmdCopySourceConflictedItems");
		this.menuAdditionalTortoiseCommands = new UICommand("menuAdditionalTortoiseCommands");
		this.cmdCheckout1 = new UICommand("cmdCheckout");
		this.cmdRepoBrowser1 = new UICommand("cmdRepoBrowser");
		this.cmdRevisionGraph1 = new UICommand("cmdRevisionGraph");
		this.cmdResolve1 = new UICommand("cmdResolve");
		this.cmdCleanUp1 = new UICommand("cmdCleanUp");
		this.cmdDeleteUnversioned1 = new UICommand("cmdDeleteUnversioned");
		this.cmdLock2 = new UICommand("cmdLock");
		this.cmdUnlock2 = new UICommand("cmdUnlock");
		this.Separator13 = new UICommand("Separator");
		this.cmdBranchTag1 = new UICommand("cmdBranchTag");
		this.cmdSwitch1 = new UICommand("cmdSwitch");
		this.cmdMerge1 = new UICommand("cmdMerge");
		this.cmdReintegrate1 = new UICommand("cmdReintegrate");
		this.cmdExport1 = new UICommand("cmdExport");
		this.cmdRelocate1 = new UICommand("cmdRelocate");
		this.Separator4 = new UICommand("Separator");
		this.cmdCreatePatch2 = new UICommand("cmdCreatePatch");
		this.cmdApplyPatch2 = new UICommand("cmdApplyPatch");
		this.Separator11 = new UICommand("Separator");
		this.cmdProperties2 = new UICommand("cmdProperties");
		this.Separator14 = new UICommand("Separator");
		this.cmdTSVNSettings2 = new UICommand("cmdTSVNSettings");
		this.cmdTSVNHelp2 = new UICommand("cmdTSVNHelp");
		this.cmdRepoBrowser = new UICommand("cmdRepoBrowser");
		this.cmdRevisionGraph = new UICommand("cmdRevisionGraph");
		this.cmdCleanUp = new UICommand("cmdCleanUp");
		this.cmdExport = new UICommand("cmdExport");
		this.cmdSwitch = new UICommand("cmdSwitch");
		this.cmdRelocate = new UICommand("cmdRelocate");
		this.cmdProperties = new UICommand("cmdProperties");
		this.cmdCheckout = new UICommand("cmdCheckout");
		this.cmdResolve = new UICommand("cmdResolve");
		this.cmdMerge = new UICommand("cmdMerge");
		this.cmdReintegrate = new UICommand("cmdReintegrate");
		this.cmdBranchTag = new UICommand("cmdBranchTag");
		this.cmdCreatePatch = new UICommand("cmdCreatePatch");
		this.cmdApplyPatch = new UICommand("cmdApplyPatch");
		this.cmdLock = new UICommand("cmdLock");
		this.cmdUnlock = new UICommand("cmdUnlock");
		this.cmdDeleteUnversioned = new UICommand("cmdDeleteUnversioned");
		this.cmdTSVNSettings = new UICommand("cmdTSVNSettings");
		this.cmdTSVNHelp = new UICommand("cmdTSVNHelp");
		this.menuEvenMore = new UICommand("menuEvenMore");
		this.cmdCreatePatch1 = new UICommand("cmdCreatePatch");
		this.cmdApplyPatch1 = new UICommand("cmdApplyPatch");
		this.cmdProperties1 = new UICommand("cmdProperties");
		this.Separator12 = new UICommand("Separator");
		this.cmdTSVNSettings1 = new UICommand("cmdTSVNSettings");
		this.cmdTSVNHelp1 = new UICommand("cmdTSVNHelp");
		this.filterModified = new UICommand("filterModified");
		this.filterNotUpToDate = new UICommand("filterNotUpToDate");
		this.filterAll = new UICommand("filterAll");
		this.menuShow = new UICommand("menuShow");
		this.filterAll1 = new UICommand("filterAll");
		this.Separator15 = new UICommand("Separator");
		this.filterModified3 = new UICommand("filterModified");
		this.filterNotUpToDate3 = new UICommand("filterNotUpToDate");
		this.filterEnabled1 = new UICommand("filterEnabled");
		this.cmdClearAllErrors = new UICommand("cmdClearAllErrors");
		this.filterEnabled = new UICommand("filterEnabled");
		this.uiContextMenu1 = new UIContextMenu();
		this.cmdEnabled1 = new UICommand("cmdEnabled");
		this.cmdClearError1 = new UICommand("cmdClearError");
		this.cmdClearAllErrors1 = new UICommand("cmdClearAllErrors");
		this.cmdExplore1 = new UICommand("cmdExplore");
		this.menuClipboard1 = new UICommand("menuClipboard");
		this.cmdSVNModifications2 = new UICommand("cmdSVNModifications");
		this.cmdShowLog2 = new UICommand("cmdShowLog");
		this.cmdSVNUpdate1 = new UICommand("cmdSVNUpdate");
		this.cmdSVNCommit1 = new UICommand("cmdSVNCommit");
		this.cmdSVNRevert2 = new UICommand("cmdSVNRevert");
		this.menuAdditionalTortoiseCommands1 = new UICommand("menuAdditionalTortoiseCommands");
		this.Separator8 = new UICommand("Separator");
		this.Separator9 = new UICommand("Separator");
		this.cmdRefresh1 = new UICommand("cmdRefresh");
		this.menuShow2 = new UICommand("menuShow");
		this.Separator5 = new UICommand("Separator");
		this.menuWizards1 = new UICommand("menuWizards");
		this.LeftRebar1 = new UIRebar();
		this.RightRebar1 = new UIRebar();
		this.TopRebar1 = new UIRebar();
		this.sourcesExplorerBar1 = new SourcesExplorerBar();
		this.panel1 = new Panel();
		this.linkShowAllSources = new LinkLabel();
		this.filterModified1 = new UICommand("filterModified");
		this.filterNotUpToDate2 = new UICommand("filterNotUpToDate");
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
		this.uiCommandManager1.BeginInit();
		this.BottomRebar1.BeginInit();
		this.uiCommandBar1.BeginInit();
		this.uiContextMenu1.BeginInit();
		this.LeftRebar1.BeginInit();
		this.RightRebar1.BeginInit();
		this.TopRebar1.BeginInit();
		this.TopRebar1.SuspendLayout();
		this.sourcesExplorerBar1.BeginInit();
		this.panel1.SuspendLayout();
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
		this.cmdNew.Image = (Image)resources.GetObject("cmdNew.Image");
		this.cmdNew.Key = "cmdNew";
		this.cmdNew.Name = "cmdNew";
		this.cmdNew.Text = "New Source";
		this.cmdNew.ToolTipText = "New source";
		this.cmdNew.Click += new CommandEventHandler(this.cmdNew_Click);
		this.cmdEdit.Image = (Image)resources.GetObject("cmdEdit.Image");
		this.cmdEdit.Key = "cmdEdit";
		this.cmdEdit.Name = "cmdEdit";
		this.cmdEdit.Text = "Properties";
		this.cmdEdit.ToolTipText = "Properties";
		this.cmdEdit.Click += new CommandEventHandler(this.cmdEdit_Click);
		this.cmdDelete.Image = (Image)resources.GetObject("cmdDelete.Image");
		this.cmdDelete.Key = "cmdDelete";
		this.cmdDelete.Name = "cmdDelete";
		this.cmdDelete.Text = "Delete";
		this.cmdDelete.ToolTipText = "Delete source";
		this.cmdDelete.Click += new CommandEventHandler(this.cmdDelete_Click);
		this.cmdUpdate.Image = (Image)resources.GetObject("cmdUpdate.Image");
		this.cmdUpdate.Key = "cmdUpdate";
		this.cmdUpdate.Name = "cmdUpdate";
		this.cmdUpdate.Text = "Check for updates";
		this.cmdUpdate.ToolTipText = "Check for updates";
		this.cmdUpdate.Click += new CommandEventHandler(this.cmdUpdate_Click);
		this.cmdUpdateAll.Image = (Image)resources.GetObject("cmdUpdateAll.Image");
		this.cmdUpdateAll.Key = "cmdUpdateAll";
		this.cmdUpdateAll.Name = "cmdUpdateAll";
		this.cmdUpdateAll.Text = "Check all for updates";
		this.cmdUpdateAll.ToolTipText = "Check all for updates";
		this.cmdUpdateAll.Click += new CommandEventHandler(this.cmdUpdateAll_Click);
		this.cmdMoveUp.Image = (Image)resources.GetObject("cmdMoveUp.Image");
		this.cmdMoveUp.Key = "cmdMoveUp";
		this.cmdMoveUp.Name = "cmdMoveUp";
		this.cmdMoveUp.Text = "Move Up";
		this.cmdMoveUp.ToolTipText = "Move source up";
		this.cmdMoveUp.Click += new CommandEventHandler(this.cmdMoveUp_Click);
		this.cmdMoveDown.Image = (Image)resources.GetObject("cmdMoveDown.Image");
		this.cmdMoveDown.Key = "cmdMoveDown";
		this.cmdMoveDown.Name = "cmdMoveDown";
		this.cmdMoveDown.Text = "Move Down";
		this.cmdMoveDown.ToolTipText = "Move source down";
		this.cmdMoveDown.Click += new CommandEventHandler(this.cmdMoveDown_Click);
		this.imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "wc.png");
		this.imageList1.Images.SetKeyName(1, "wc.updating.png");
		this.imageList1.Images.SetKeyName(2, "repo.error.png");
		this.imageList1.Images.SetKeyName(3, "repo.png");
		this.imageList1.Images.SetKeyName(4, "repo.updating.png");
		this.imageList1.Images.SetKeyName(5, "wc.downdate.modified.png");
		this.imageList1.Images.SetKeyName(6, "wc.downdate.png");
		this.imageList1.Images.SetKeyName(7, "wc.error.png");
		this.imageList1.Images.SetKeyName(8, "wc.modified.png");
		this.uiCommandManager1.AllowClose = InheritableBoolean.False;
		this.uiCommandManager1.AllowCustomize = InheritableBoolean.False;
		this.uiCommandManager1.BottomRebar = this.BottomRebar1;
		this.uiCommandManager1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
		this.uiCommandManager1.Commands.AddRange(new UICommand[] { this.cmdNew, this.cmdEdit, this.cmdDelete, this.cmdUpdate, this.cmdUpdateAll, this.cmdMoveUp, this.cmdMoveDown, this.menuWizards, this.cmdSVNUpdate, this.cmdSVNCommit, this.cmdSVNRevert, this.cmdSVNModifications, this.cmdRefresh, this.cmdEnabled, this.cmdExplore, this.cmdClearError, this.cmdShowLog, this.menuClipboard, this.cmdCopySourceName, this.cmdCopySourcePath, this.cmdCopySourceURL, this.cmdCopySourceError, this.cmdCopySourceModifiedItems, this.cmdCopySourceUnversionedItems, this.cmdCopySourceConflictedItems, this.menuAdditionalTortoiseCommands, this.cmdRepoBrowser, this.cmdRevisionGraph, this.cmdCleanUp, this.cmdExport, this.cmdSwitch, this.cmdRelocate, this.cmdProperties, this.cmdCheckout, this.cmdResolve, this.cmdMerge, this.cmdReintegrate, this.cmdBranchTag, this.cmdCreatePatch, this.cmdApplyPatch, this.cmdLock, this.cmdUnlock, this.cmdDeleteUnversioned, this.cmdTSVNSettings, this.cmdTSVNHelp, this.menuEvenMore, this.filterModified, this.filterNotUpToDate, this.filterAll, this.menuShow, this.cmdClearAllErrors, this.filterEnabled });
		this.uiCommandManager1.ContainerControl = this;
		this.uiCommandManager1.ContextMenus.AddRange(new UIContextMenu[] { this.uiContextMenu1 });
		this.uiCommandManager1.Id = new Guid("6dcff238-beed-4562-b52d-d0ba855fecb0");
		this.uiCommandManager1.LeftRebar = this.LeftRebar1;
		this.uiCommandManager1.LockCommandBars = true;
		this.uiCommandManager1.RightRebar = this.RightRebar1;
		this.uiCommandManager1.ShowAddRemoveButton = InheritableBoolean.False;
		this.uiCommandManager1.ShowQuickCustomizeMenu = false;
		this.uiCommandManager1.TopRebar = this.TopRebar1;
		this.uiCommandManager1.VisualStyle = VisualStyle.Standard;
		this.BottomRebar1.CommandManager = this.uiCommandManager1;
		this.BottomRebar1.Dock = DockStyle.Bottom;
		this.BottomRebar1.Location = new Point(0, 338);
		this.BottomRebar1.Name = "BottomRebar1";
		this.BottomRebar1.Size = new Size(505, 0);
		this.uiCommandBar1.AllowClose = InheritableBoolean.False;
		this.uiCommandBar1.AllowCustomize = InheritableBoolean.False;
		this.uiCommandBar1.Animation = DropDownAnimation.System;
		this.uiCommandBar1.CommandManager = this.uiCommandManager1;
		this.uiCommandBar1.Commands.AddRange(new UICommand[] { cmdNew1, cmdEdit1, cmdDelete1, Separator3, this.cmdSVNModifications1, this.cmdShowLog1, this.cmdSVNUpdate2, this.cmdSVNCommit2, this.cmdSVNRevert1, this.Separator7, cmdUpdate2, cmdUpdateAll2, this.Separator10, cmdMoveUp1, cmdMoveDown1, this.Separator6, this.menuWizards2 });
		this.uiCommandBar1.FullRow = true;
		this.uiCommandBar1.Key = "CommandBar1";
		this.uiCommandBar1.Location = new Point(0, 0);
		this.uiCommandBar1.Name = "uiCommandBar1";
		this.uiCommandBar1.RowIndex = 0;
		this.uiCommandBar1.ShowAddRemoveButton = InheritableBoolean.False;
		this.uiCommandBar1.Size = new Size(468, 28);
		this.uiCommandBar1.Text = "Source";
		this.cmdSVNModifications1.CommandStyle = CommandStyle.Image;
		this.cmdSVNModifications1.Key = "cmdSVNModifications";
		this.cmdSVNModifications1.Name = "cmdSVNModifications1";
		this.cmdShowLog1.CommandStyle = CommandStyle.Image;
		this.cmdShowLog1.Key = "cmdShowLog";
		this.cmdShowLog1.Name = "cmdShowLog1";
		this.cmdSVNUpdate2.CommandStyle = CommandStyle.Image;
		this.cmdSVNUpdate2.Key = "cmdSVNUpdate";
		this.cmdSVNUpdate2.Name = "cmdSVNUpdate2";
		this.cmdSVNCommit2.CommandStyle = CommandStyle.Image;
		this.cmdSVNCommit2.Key = "cmdSVNCommit";
		this.cmdSVNCommit2.Name = "cmdSVNCommit2";
		this.cmdSVNRevert1.CommandStyle = CommandStyle.Image;
		this.cmdSVNRevert1.Key = "cmdSVNRevert";
		this.cmdSVNRevert1.Name = "cmdSVNRevert1";
		this.Separator7.CommandType = CommandType.Separator;
		this.Separator7.Key = "Separator";
		this.Separator7.Name = "Separator7";
		this.Separator10.CommandType = CommandType.Separator;
		this.Separator10.Key = "Separator";
		this.Separator10.Name = "Separator10";
		this.Separator6.CommandType = CommandType.Separator;
		this.Separator6.Key = "Separator";
		this.Separator6.Name = "Separator6";
		this.menuWizards2.CommandStyle = CommandStyle.Image;
		this.menuWizards2.Key = "menuWizards";
		this.menuWizards2.Name = "menuWizards2";
		this.menuWizards2.Click += new CommandEventHandler(this.menuWizards2_Click);
		this.menuWizards.CommandType = CommandType.ControlPopup;
		this.menuWizards.Image = (Image)resources.GetObject("menuWizards.Image");
		this.menuWizards.Key = "menuWizards";
		this.menuWizards.Name = "menuWizards";
		this.menuWizards.Text = "Monitor this source";
		this.menuWizards.ToolTipText = "Monitor this source";
		this.cmdSVNUpdate.Image = (Image)resources.GetObject("cmdSVNUpdate.Image");
		this.cmdSVNUpdate.Key = "cmdSVNUpdate";
		this.cmdSVNUpdate.Name = "cmdSVNUpdate";
		this.cmdSVNUpdate.Text = "Update";
		this.cmdSVNUpdate.ToolTipText = "Update";
		this.cmdSVNUpdate.Visible = InheritableBoolean.False;
		this.cmdSVNUpdate.Click += new CommandEventHandler(this.cmdSVNUpdate_Click);
		this.cmdSVNCommit.Image = (Image)resources.GetObject("cmdSVNCommit.Image");
		this.cmdSVNCommit.Key = "cmdSVNCommit";
		this.cmdSVNCommit.Name = "cmdSVNCommit";
		this.cmdSVNCommit.Text = "Commit";
		this.cmdSVNCommit.ToolTipText = "Commit";
		this.cmdSVNCommit.Visible = InheritableBoolean.False;
		this.cmdSVNCommit.Click += new CommandEventHandler(this.cmdSVNCommit_Click);
		this.cmdSVNRevert.Image = (Image)resources.GetObject("cmdSVNRevert.Image");
		this.cmdSVNRevert.Key = "cmdSVNRevert";
		this.cmdSVNRevert.Name = "cmdSVNRevert";
		this.cmdSVNRevert.Text = "Revert";
		this.cmdSVNRevert.ToolTipText = "Revert";
		this.cmdSVNRevert.Click += new CommandEventHandler(this.cmdSVNRevert_Click);
		this.cmdSVNModifications.Image = (Image)resources.GetObject("cmdSVNModifications.Image");
		this.cmdSVNModifications.Key = "cmdSVNModifications";
		this.cmdSVNModifications.Name = "cmdSVNModifications";
		this.cmdSVNModifications.Text = "Check for modifications";
		this.cmdSVNModifications.ToolTipText = "Check for modifications";
		this.cmdSVNModifications.Click += new CommandEventHandler(this.cmdSVNModifications_Click);
		this.cmdRefresh.Image = (Image)resources.GetObject("cmdRefresh.Image");
		this.cmdRefresh.Key = "cmdRefresh";
		this.cmdRefresh.Name = "cmdRefresh";
		this.cmdRefresh.Text = "Refresh log (might take a few minutes...)";
		this.cmdRefresh.ToolTipText = "Refresh log (might take a few minutes...)";
		this.cmdRefresh.Click += new CommandEventHandler(this.cmdRefresh_Click);
		this.cmdEnabled.CommandType = CommandType.ToggleButton;
		this.cmdEnabled.Key = "cmdEnabled";
		this.cmdEnabled.Name = "cmdEnabled";
		this.cmdEnabled.Text = "Enabled";
		this.cmdEnabled.ToolTipText = "Enabled";
		this.cmdEnabled.Click += new CommandEventHandler(this.cmdEnabled_Click);
		this.cmdExplore.Image = (Image)resources.GetObject("cmdExplore.Image");
		this.cmdExplore.Key = "cmdExplore";
		this.cmdExplore.Name = "cmdExplore";
		this.cmdExplore.Text = "Explore";
		this.cmdExplore.ToolTipText = "Explore";
		this.cmdExplore.Click += new CommandEventHandler(this.cmdExplore_Click);
		this.cmdClearError.Image = (Image)resources.GetObject("cmdClearError.Image");
		this.cmdClearError.Key = "cmdClearError";
		this.cmdClearError.Name = "cmdClearError";
		this.cmdClearError.Text = "Clear error";
		this.cmdClearError.ToolTipText = "Clear Error";
		this.cmdClearError.Click += new CommandEventHandler(this.cmdClearError_Click);
		this.cmdShowLog.Image = (Image)resources.GetObject("cmdShowLog.Image");
		this.cmdShowLog.Key = "cmdShowLog";
		this.cmdShowLog.Name = "cmdShowLog";
		this.cmdShowLog.Text = "Show log";
		this.cmdShowLog.ToolTipText = "Show log";
		this.cmdShowLog.Click += new CommandEventHandler(this.cmdShowLog_Click);
		this.menuClipboard.Commands.AddRange(new UICommand[] { this.cmdCopySourceName1, this.cmdCopySourcePath1, this.cmdCopySourceUrl1, this.cmdCopySourceError1, this.cmdCopySourceModifiedItems1, this.cmdCopySourceConflictedItems1, this.cmdCopySourceUnversionedItems1 });
		this.menuClipboard.Image = (Image)resources.GetObject("menuClipboard.Image");
		this.menuClipboard.Key = "menuClipboard";
		this.menuClipboard.Name = "menuClipboard";
		this.menuClipboard.Text = "Copy to clipboard";
		this.menuClipboard.Popup += new CommandEventHandler(this.menuClipboard_Popup);
		this.cmdCopySourceName1.Key = "cmdCopySourceName";
		this.cmdCopySourceName1.Name = "cmdCopySourceName1";
		this.cmdCopySourcePath1.Key = "cmdCopySourcePath";
		this.cmdCopySourcePath1.Name = "cmdCopySourcePath1";
		this.cmdCopySourceUrl1.Key = "cmdCopySourceURL";
		this.cmdCopySourceUrl1.Name = "cmdCopySourceUrl1";
		this.cmdCopySourceError1.Key = "cmdCopySourceError";
		this.cmdCopySourceError1.Name = "cmdCopySourceError1";
		this.cmdCopySourceModifiedItems1.Key = "cmdCopySourceModifiedItems";
		this.cmdCopySourceModifiedItems1.Name = "cmdCopySourceModifiedItems1";
		this.cmdCopySourceConflictedItems1.Key = "cmdCopySourceConflictedItems";
		this.cmdCopySourceConflictedItems1.Name = "cmdCopySourceConflictedItems1";
		this.cmdCopySourceUnversionedItems1.Key = "cmdCopySourceUnversionedItems";
		this.cmdCopySourceUnversionedItems1.Name = "cmdCopySourceUnversionedItems1";
		this.cmdCopySourceName.Image = (Image)resources.GetObject("cmdCopySourceName.Image");
		this.cmdCopySourceName.Key = "cmdCopySourceName";
		this.cmdCopySourceName.Name = "cmdCopySourceName";
		this.cmdCopySourceName.Text = "&Name";
		this.cmdCopySourcePath.Image = (Image)resources.GetObject("cmdCopySourcePath.Image");
		this.cmdCopySourcePath.Key = "cmdCopySourcePath";
		this.cmdCopySourcePath.Name = "cmdCopySourcePath";
		this.cmdCopySourcePath.Text = "&Path";
		this.cmdCopySourceURL.Image = (Image)resources.GetObject("cmdCopySourceURL.Image");
		this.cmdCopySourceURL.Key = "cmdCopySourceURL";
		this.cmdCopySourceURL.Name = "cmdCopySourceURL";
		this.cmdCopySourceURL.Text = "&URL";
		this.cmdCopySourceError.Image = (Image)resources.GetObject("cmdCopySourceError.Image");
		this.cmdCopySourceError.Key = "cmdCopySourceError";
		this.cmdCopySourceError.Name = "cmdCopySourceError";
		this.cmdCopySourceError.Text = "&Error";
		this.cmdCopySourceModifiedItems.Image = (Image)resources.GetObject("cmdCopySourceModifiedItems.Image");
		this.cmdCopySourceModifiedItems.Key = "cmdCopySourceModifiedItems";
		this.cmdCopySourceModifiedItems.Name = "cmdCopySourceModifiedItems";
		this.cmdCopySourceModifiedItems.Text = "&Modified items list";
		this.cmdCopySourceUnversionedItems.Image = (Image)resources.GetObject("cmdCopySourceUnversionedItems.Image");
		this.cmdCopySourceUnversionedItems.Key = "cmdCopySourceUnversionedItems";
		this.cmdCopySourceUnversionedItems.Name = "cmdCopySourceUnversionedItems";
		this.cmdCopySourceUnversionedItems.Text = "Un&versioned items list";
		this.cmdCopySourceConflictedItems.Image = (Image)resources.GetObject("cmdCopySourceConflictedItems.Image");
		this.cmdCopySourceConflictedItems.Key = "cmdCopySourceConflictedItems";
		this.cmdCopySourceConflictedItems.Name = "cmdCopySourceConflictedItems";
		this.cmdCopySourceConflictedItems.Text = "Possible &conflicted items list";
		this.menuAdditionalTortoiseCommands.Commands.AddRange(new UICommand[] { this.cmdCheckout1, this.cmdRepoBrowser1, this.cmdRevisionGraph1, this.cmdResolve1, this.cmdCleanUp1, this.cmdDeleteUnversioned1, this.cmdLock2, this.cmdUnlock2, this.Separator13, this.cmdBranchTag1, this.cmdSwitch1, this.cmdMerge1, this.cmdReintegrate1, this.cmdExport1, this.cmdRelocate1, this.Separator4, this.cmdCreatePatch2, this.cmdApplyPatch2, this.Separator11, this.cmdProperties2, this.Separator14, this.cmdTSVNSettings2, this.cmdTSVNHelp2 });
		this.menuAdditionalTortoiseCommands.Image = (Image)resources.GetObject("menuAdditionalTortoiseCommands.Image");
		this.menuAdditionalTortoiseCommands.Key = "menuAdditionalTortoiseCommands";
		this.menuAdditionalTortoiseCommands.Name = "menuAdditionalTortoiseCommands";
		this.menuAdditionalTortoiseCommands.Text = "More";
		this.cmdCheckout1.Key = "cmdCheckout";
		this.cmdCheckout1.Name = "cmdCheckout1";
		this.cmdRepoBrowser1.Key = "cmdRepoBrowser";
		this.cmdRepoBrowser1.Name = "cmdRepoBrowser1";
		this.cmdRevisionGraph1.Key = "cmdRevisionGraph";
		this.cmdRevisionGraph1.Name = "cmdRevisionGraph1";
		this.cmdResolve1.Key = "cmdResolve";
		this.cmdResolve1.Name = "cmdResolve1";
		this.cmdCleanUp1.Key = "cmdCleanUp";
		this.cmdCleanUp1.Name = "cmdCleanUp1";
		this.cmdDeleteUnversioned1.Key = "cmdDeleteUnversioned";
		this.cmdDeleteUnversioned1.Name = "cmdDeleteUnversioned1";
		this.cmdLock2.Key = "cmdLock";
		this.cmdLock2.Name = "cmdLock2";
		this.cmdUnlock2.Key = "cmdUnlock";
		this.cmdUnlock2.Name = "cmdUnlock2";
		this.Separator13.CommandType = CommandType.Separator;
		this.Separator13.Key = "Separator";
		this.Separator13.Name = "Separator13";
		this.cmdBranchTag1.Key = "cmdBranchTag";
		this.cmdBranchTag1.Name = "cmdBranchTag1";
		this.cmdSwitch1.Key = "cmdSwitch";
		this.cmdSwitch1.Name = "cmdSwitch1";
		this.cmdMerge1.Key = "cmdMerge";
		this.cmdMerge1.Name = "cmdMerge1";
		this.cmdReintegrate1.Key = "cmdReintegrate";
		this.cmdReintegrate1.Name = "cmdReintegrate1";
		this.cmdExport1.Key = "cmdExport";
		this.cmdExport1.Name = "cmdExport1";
		this.cmdRelocate1.Key = "cmdRelocate";
		this.cmdRelocate1.Name = "cmdRelocate1";
		this.Separator4.CommandType = CommandType.Separator;
		this.Separator4.Key = "Separator";
		this.Separator4.Name = "Separator4";
		this.cmdCreatePatch2.Key = "cmdCreatePatch";
		this.cmdCreatePatch2.Name = "cmdCreatePatch2";
		this.cmdApplyPatch2.Key = "cmdApplyPatch";
		this.cmdApplyPatch2.Name = "cmdApplyPatch2";
		this.Separator11.CommandType = CommandType.Separator;
		this.Separator11.Key = "Separator";
		this.Separator11.Name = "Separator11";
		this.cmdProperties2.Key = "cmdProperties";
		this.cmdProperties2.Name = "cmdProperties2";
		this.Separator14.CommandType = CommandType.Separator;
		this.Separator14.Key = "Separator";
		this.Separator14.Name = "Separator14";
		this.cmdTSVNSettings2.Key = "cmdTSVNSettings";
		this.cmdTSVNSettings2.Name = "cmdTSVNSettings2";
		this.cmdTSVNHelp2.Key = "cmdTSVNHelp";
		this.cmdTSVNHelp2.Name = "cmdTSVNHelp2";
		this.cmdRepoBrowser.Image = (Image)resources.GetObject("cmdRepoBrowser.Image");
		this.cmdRepoBrowser.Key = "cmdRepoBrowser";
		this.cmdRepoBrowser.Name = "cmdRepoBrowser";
		this.cmdRepoBrowser.Text = "Repo &browser";
		this.cmdRepoBrowser.Click += new CommandEventHandler(this.cmdRepoBrowser_Click);
		this.cmdRevisionGraph.Image = (Image)resources.GetObject("cmdRevisionGraph.Image");
		this.cmdRevisionGraph.Key = "cmdRevisionGraph";
		this.cmdRevisionGraph.Name = "cmdRevisionGraph";
		this.cmdRevisionGraph.Text = "Revision &graph";
		this.cmdRevisionGraph.Click += new CommandEventHandler(this.cmdRevisionGraph_Click);
		this.cmdCleanUp.Image = (Image)resources.GetObject("cmdCleanUp.Image");
		this.cmdCleanUp.Key = "cmdCleanUp";
		this.cmdCleanUp.Name = "cmdCleanUp";
		this.cmdCleanUp.Text = "&Clean up";
		this.cmdCleanUp.Click += new CommandEventHandler(this.cmdCleanUp_Click);
		this.cmdExport.Image = (Image)resources.GetObject("cmdExport.Image");
		this.cmdExport.Key = "cmdExport";
		this.cmdExport.Name = "cmdExport";
		this.cmdExport.Text = "&Export";
		this.cmdExport.Click += new CommandEventHandler(this.cmdExport_Click);
		this.cmdSwitch.Image = (Image)resources.GetObject("cmdSwitch.Image");
		this.cmdSwitch.Key = "cmdSwitch";
		this.cmdSwitch.Name = "cmdSwitch";
		this.cmdSwitch.Text = "&Switch";
		this.cmdSwitch.Click += new CommandEventHandler(this.cmdSwitch_Click);
		this.cmdRelocate.Image = (Image)resources.GetObject("cmdRelocate.Image");
		this.cmdRelocate.Key = "cmdRelocate";
		this.cmdRelocate.Name = "cmdRelocate";
		this.cmdRelocate.Text = "&Relocate";
		this.cmdRelocate.Click += new CommandEventHandler(this.cmdRelocate_Click);
		this.cmdProperties.Image = (Image)resources.GetObject("cmdProperties.Image");
		this.cmdProperties.Key = "cmdProperties";
		this.cmdProperties.Name = "cmdProperties";
		this.cmdProperties.Text = "SVN-&Properties";
		this.cmdProperties.Click += new CommandEventHandler(this.cmdProperties_Click);
		this.cmdCheckout.Image = (Image)resources.GetObject("cmdCheckout.Image");
		this.cmdCheckout.Key = "cmdCheckout";
		this.cmdCheckout.Name = "cmdCheckout";
		this.cmdCheckout.Text = "Chec&kout";
		this.cmdCheckout.Click += new CommandEventHandler(this.cmdCheckout_Click);
		this.cmdResolve.Key = "cmdResolve";
		this.cmdResolve.Name = "cmdResolve";
		this.cmdResolve.Text = "Resolve";
		this.cmdResolve.Click += new CommandEventHandler(this.cmdResolve_Click);
		this.cmdMerge.Key = "cmdMerge";
		this.cmdMerge.Name = "cmdMerge";
		this.cmdMerge.Text = "Merge";
		this.cmdMerge.Click += new CommandEventHandler(this.cmdMerge_Click);
		this.cmdReintegrate.Key = "cmdReintegrate";
		this.cmdReintegrate.Name = "cmdReintegrate";
		this.cmdReintegrate.Text = "Reintegrate";
		this.cmdReintegrate.Click += new CommandEventHandler(this.cmdReintegrate_Click);
		this.cmdBranchTag.Key = "cmdBranchTag";
		this.cmdBranchTag.Name = "cmdBranchTag";
		this.cmdBranchTag.Text = "Branch/Tag";
		this.cmdBranchTag.Click += new CommandEventHandler(this.cmdBranchTag_Click);
		this.cmdCreatePatch.Key = "cmdCreatePatch";
		this.cmdCreatePatch.Name = "cmdCreatePatch";
		this.cmdCreatePatch.Text = "Create Patch";
		this.cmdCreatePatch.Click += new CommandEventHandler(this.cmdCreatePatch_Click);
		this.cmdApplyPatch.Key = "cmdApplyPatch";
		this.cmdApplyPatch.Name = "cmdApplyPatch";
		this.cmdApplyPatch.Text = "Apply Patch";
		this.cmdApplyPatch.Click += new CommandEventHandler(this.cmdApplyPatch_Click);
		this.cmdLock.Key = "cmdLock";
		this.cmdLock.Name = "cmdLock";
		this.cmdLock.Text = "Get Lock";
		this.cmdLock.Click += new CommandEventHandler(this.cmdLock_Click);
		this.cmdUnlock.Key = "cmdUnlock";
		this.cmdUnlock.Name = "cmdUnlock";
		this.cmdUnlock.Text = "Release Lock";
		this.cmdUnlock.Click += new CommandEventHandler(this.cmdUnlock_Click);
		this.cmdDeleteUnversioned.Key = "cmdDeleteUnversioned";
		this.cmdDeleteUnversioned.Name = "cmdDeleteUnversioned";
		this.cmdDeleteUnversioned.Text = "Delete Unversioned Items";
		this.cmdDeleteUnversioned.Click += new CommandEventHandler(this.cmdDeleteUnversioned_Click);
		this.cmdTSVNSettings.Key = "cmdTSVNSettings";
		this.cmdTSVNSettings.Name = "cmdTSVNSettings";
		this.cmdTSVNSettings.Text = "TortoiseSVN Settings";
		this.cmdTSVNSettings.Click += new CommandEventHandler(this.cmdTSVNSettings_Click);
		this.cmdTSVNHelp.Key = "cmdTSVNHelp";
		this.cmdTSVNHelp.Name = "cmdTSVNHelp";
		this.cmdTSVNHelp.Text = "TortoiseSVN Help";
		this.cmdTSVNHelp.Click += new CommandEventHandler(this.cmdTSVNHelp_Click);
		this.menuEvenMore.Commands.AddRange(new UICommand[] { this.cmdCreatePatch1, this.cmdApplyPatch1, this.cmdProperties1, this.Separator12, this.cmdTSVNSettings1, this.cmdTSVNHelp1 });
		this.menuEvenMore.Key = "menuEvenMore";
		this.menuEvenMore.Name = "menuEvenMore";
		this.menuEvenMore.Text = "Even More";
		this.cmdCreatePatch1.Key = "cmdCreatePatch";
		this.cmdCreatePatch1.Name = "cmdCreatePatch1";
		this.cmdApplyPatch1.Key = "cmdApplyPatch";
		this.cmdApplyPatch1.Name = "cmdApplyPatch1";
		this.cmdProperties1.Key = "cmdProperties";
		this.cmdProperties1.Name = "cmdProperties1";
		this.Separator12.CommandType = CommandType.Separator;
		this.Separator12.Key = "Separator";
		this.Separator12.Name = "Separator12";
		this.cmdTSVNSettings1.Key = "cmdTSVNSettings";
		this.cmdTSVNSettings1.Name = "cmdTSVNSettings1";
		this.cmdTSVNHelp1.Key = "cmdTSVNHelp";
		this.cmdTSVNHelp1.Name = "cmdTSVNHelp1";
		this.filterModified.CommandType = CommandType.ToggleButton;
		this.filterModified.Key = "filterModified";
		this.filterModified.Name = "filterModified";
		this.filterModified.Text = "&Modified";
		this.filterModified.Click += new CommandEventHandler(this.filter_Click);
		this.filterNotUpToDate.CommandType = CommandType.ToggleButton;
		this.filterNotUpToDate.Key = "filterNotUpToDate";
		this.filterNotUpToDate.Name = "filterNotUpToDate";
		this.filterNotUpToDate.Text = "&Has Updates";
		this.filterNotUpToDate.Click += new CommandEventHandler(this.filter_Click);
		this.filterAll.Checked = InheritableBoolean.True;
		this.filterAll.CommandType = CommandType.ToggleButton;
		this.filterAll.Key = "filterAll";
		this.filterAll.Name = "filterAll";
		this.filterAll.Text = "&All";
		this.filterAll.Click += new CommandEventHandler(this.filterAll_Click);
		this.menuShow.Commands.AddRange(new UICommand[] { this.filterAll1, this.Separator15, this.filterModified3, this.filterNotUpToDate3, this.filterEnabled1 });
		this.menuShow.IsEditableControl = InheritableBoolean.False;
		this.menuShow.Key = "menuShow";
		this.menuShow.Name = "menuShow";
		this.menuShow.Text = "&Show";
		this.filterAll1.Key = "filterAll";
		this.filterAll1.Name = "filterAll1";
		this.Separator15.CommandType = CommandType.Separator;
		this.Separator15.Key = "Separator";
		this.Separator15.Name = "Separator15";
		this.filterModified3.Key = "filterModified";
		this.filterModified3.Name = "filterModified3";
		this.filterNotUpToDate3.Key = "filterNotUpToDate";
		this.filterNotUpToDate3.Name = "filterNotUpToDate3";
		this.filterEnabled1.Key = "filterEnabled";
		this.filterEnabled1.Name = "filterEnabled1";
		this.cmdClearAllErrors.Key = "cmdClearAllErrors";
		this.cmdClearAllErrors.Name = "cmdClearAllErrors";
		this.cmdClearAllErrors.Text = "Clear All Errors";
		this.cmdClearAllErrors.Click += new CommandEventHandler(this.cmdClearAllErrors_Click);
		this.filterEnabled.CommandType = CommandType.ToggleButton;
		this.filterEnabled.Key = "filterEnabled";
		this.filterEnabled.Name = "filterEnabled";
		this.filterEnabled.Text = "&Enabled";
		this.filterEnabled.Click += new CommandEventHandler(this.filter_Click);
		this.uiContextMenu1.CommandManager = this.uiCommandManager1;
		this.uiContextMenu1.Commands.AddRange(new UICommand[] { this.cmdEnabled1, this.cmdClearError1, this.cmdClearAllErrors1, this.cmdExplore1, cmdNew2, cmdEdit2, cmdDelete2, this.menuClipboard1, Separator2, this.cmdSVNModifications2, this.cmdShowLog2, this.cmdSVNUpdate1, this.cmdSVNCommit1, this.cmdSVNRevert2, this.menuAdditionalTortoiseCommands1, this.Separator8, cmdUpdate1, cmdUpdateAll1, this.Separator9, this.cmdRefresh1, Separator1, cmdMoveUp2, cmdMoveDown2, this.menuShow2, this.Separator5, this.menuWizards1 });
		this.uiContextMenu1.Key = "ContextMenu1";
		this.cmdEnabled1.Key = "cmdEnabled";
		this.cmdEnabled1.Name = "cmdEnabled1";
		this.cmdEnabled1.Text = "&Enabled";
		this.cmdClearError1.Key = "cmdClearError";
		this.cmdClearError1.Name = "cmdClearError1";
		this.cmdClearError1.Text = "Clear &error";
		this.cmdClearAllErrors1.Key = "cmdClearAllErrors";
		this.cmdClearAllErrors1.Name = "cmdClearAllErrors1";
		this.cmdExplore1.Key = "cmdExplore";
		this.cmdExplore1.Name = "cmdExplore1";
		this.cmdExplore1.Text = "E&xplore";
		this.menuClipboard1.Key = "menuClipboard";
		this.menuClipboard1.Name = "menuClipboard1";
		this.menuClipboard1.Text = "Copy &to clipboard";
		this.cmdSVNModifications2.Key = "cmdSVNModifications";
		this.cmdSVNModifications2.Name = "cmdSVNModifications2";
		this.cmdSVNModifications2.Text = "Check modi&fications";
		this.cmdShowLog2.Key = "cmdShowLog";
		this.cmdShowLog2.Name = "cmdShowLog2";
		this.cmdShowLog2.Text = "&Show log";
		this.cmdSVNUpdate1.Key = "cmdSVNUpdate";
		this.cmdSVNUpdate1.Name = "cmdSVNUpdate1";
		this.cmdSVNUpdate1.Text = "&Update";
		this.cmdSVNCommit1.Key = "cmdSVNCommit";
		this.cmdSVNCommit1.Name = "cmdSVNCommit1";
		this.cmdSVNCommit1.Text = "&Commit";
		this.cmdSVNRevert2.Key = "cmdSVNRevert";
		this.cmdSVNRevert2.Name = "cmdSVNRevert2";
		this.cmdSVNRevert2.Text = "Re&vert";
		this.menuAdditionalTortoiseCommands1.Key = "menuAdditionalTortoiseCommands";
		this.menuAdditionalTortoiseCommands1.Name = "menuAdditionalTortoiseCommands1";
		this.menuAdditionalTortoiseCommands1.Text = "M&ore";
		this.Separator8.CommandType = CommandType.Separator;
		this.Separator8.Key = "Separator";
		this.Separator8.Name = "Separator8";
		this.Separator9.CommandType = CommandType.Separator;
		this.Separator9.Key = "Separator";
		this.Separator9.Name = "Separator9";
		this.cmdRefresh1.Key = "cmdRefresh";
		this.cmdRefresh1.Name = "cmdRefresh1";
		this.cmdRefresh1.Text = "&Refresh log (might take a few minutes...)";
		this.menuShow2.Key = "menuShow";
		this.menuShow2.Name = "menuShow2";
		this.Separator5.CommandType = CommandType.Separator;
		this.Separator5.Key = "Separator";
		this.Separator5.Name = "Separator5";
		this.menuWizards1.Key = "menuWizards";
		this.menuWizards1.Name = "menuWizards1";
		this.menuWizards1.Text = "&Monitor this source";
		this.LeftRebar1.CommandManager = this.uiCommandManager1;
		this.LeftRebar1.Dock = DockStyle.Left;
		this.LeftRebar1.Location = new Point(0, 28);
		this.LeftRebar1.Name = "LeftRebar1";
		this.LeftRebar1.Size = new Size(0, 310);
		this.RightRebar1.CommandManager = this.uiCommandManager1;
		this.RightRebar1.Dock = DockStyle.Right;
		this.RightRebar1.Location = new Point(505, 28);
		this.RightRebar1.Name = "RightRebar1";
		this.RightRebar1.Size = new Size(0, 310);
		this.TopRebar1.CommandBars.AddRange(new UICommandBar[] { this.uiCommandBar1 });
		this.TopRebar1.CommandManager = this.uiCommandManager1;
		this.TopRebar1.Controls.Add(this.uiCommandBar1);
		this.TopRebar1.Dock = DockStyle.Top;
		this.TopRebar1.Location = new Point(0, 0);
		this.TopRebar1.Name = "TopRebar1";
		this.TopRebar1.Size = new Size(468, 28);
		this.sourcesExplorerBar1.BackgroundFormatStyle.BackColor = Color.White;
		this.sourcesExplorerBar1.BackgroundFormatStyle.BackColorGradient = Color.Transparent;
		this.sourcesExplorerBar1.BorderStyle = BorderStyle.None;
		this.uiCommandManager1.SetContextMenu(this.sourcesExplorerBar1, this.uiContextMenu1);
		this.sourcesExplorerBar1.Dock = DockStyle.Fill;
		this.sourcesExplorerBar1.GroupSeparation = 14;
		this.sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackColor = Color.Gainsboro;
		this.sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackColorGradient = Color.Silver;
		this.sourcesExplorerBar1.GroupsStateStyles.FormatStyle.BackgroundGradientMode = BackgroundGradientMode.Vertical;
		this.sourcesExplorerBar1.GroupsStateStyles.FormatStyle.ForeColor = Color.Black;
		this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColor = Color.Silver;
		this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackColorGradient = Color.Gainsboro;
		this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackgroundGradientMode = BackgroundGradientMode.Vertical;
		this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.BackgroundThemeStyle = BackgroundThemeStyle.GroupHeader;
		this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.ForeColor = Color.Black;
		this.sourcesExplorerBar1.GroupsStateStyles.HotFormatStyle.ForegroundThemeStyle = ForegroundThemeStyle.GroupHeader;
		this.sourcesExplorerBar1.GroupsStateStyles.SelectedFormatStyle.BackColor = SystemColors.Control;
		this.sourcesExplorerBar1.Location = new Point(0, 23);
		this.sourcesExplorerBar1.Name = "sourcesExplorerBar1";
		this.sourcesExplorerBar1.Size = new Size(468, 346);
		this.sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColor = Color.LightSkyBlue;
		this.sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackColorGradient = Color.SteelBlue;
		this.sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.BackgroundGradientMode = BackgroundGradientMode.Vertical;
		this.sourcesExplorerBar1.SpecialGroupsStateStyles.FormatStyle.ForeColor = Color.White;
		this.sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackColor = Color.SteelBlue;
		this.sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackColorGradient = Color.LightSkyBlue;
		this.sourcesExplorerBar1.SpecialGroupsStateStyles.HotFormatStyle.BackgroundGradientMode = BackgroundGradientMode.Vertical;
		this.sourcesExplorerBar1.TabIndex = 1;
		this.sourcesExplorerBar1.ThemedAreas = ThemedArea.None;
		this.sourcesExplorerBar1.TopMargin = 14;
		this.sourcesExplorerBar1.add_KeyDown(new KeyEventHandler(this.sources1_KeyDown));
		this.sourcesExplorerBar1.add_SelectionChanged(new EventHandler(this.sources1_SelectionChanged));
		this.sourcesExplorerBar1.add_MouseDoubleClick(new MouseEventHandler(this.sourcesExplorerBar1_MouseDoubleClick));
		this.sourcesExplorerBar1.add_MouseDown(new MouseEventHandler(this.sourcesExplorerBar1_MouseDown));
		this.panel1.BackColor = Color.DimGray;
		this.panel1.Controls.Add(this.sourcesExplorerBar1);
		this.panel1.Controls.Add(this.linkShowAllSources);
		this.panel1.Dock = DockStyle.Fill;
		this.panel1.Location = new Point(0, 28);
		this.panel1.Name = "panel1";
		this.panel1.Padding = new Padding(0, 1, 0, 0);
		this.panel1.Size = new Size(468, 369);
		this.panel1.TabIndex = 2;
		this.linkShowAllSources.ActiveLinkColor = Color.FromArgb(255, 255, 192);
		this.linkShowAllSources.BackColor = Color.Moccasin;
		this.linkShowAllSources.BorderStyle = BorderStyle.FixedSingle;
		this.linkShowAllSources.Dock = DockStyle.Top;
		this.linkShowAllSources.Font = new Font("Microsoft Sans Serif", 8.25, FontStyle.Regular, GraphicsUnit.Point, 177);
		this.linkShowAllSources.LinkBehavior = LinkBehavior.NeverUnderline;
		this.linkShowAllSources.LinkColor = Color.Black;
		this.linkShowAllSources.Location = new Point(0, 1);
		this.linkShowAllSources.Name = "linkShowAllSources";
		this.linkShowAllSources.Size = new Size(468, 22);
		this.linkShowAllSources.TabIndex = 2;
		this.linkShowAllSources.TabStop = true;
		this.linkShowAllSources.Text = "Some sources are hidden. Click here to show all.";
		this.linkShowAllSources.TextAlign = ContentAlignment.MiddleLeft;
		this.linkShowAllSources.Visible = false;
		this.linkShowAllSources.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkShowAllSources_LinkClicked);
		this.filterModified1.Key = "filterModified";
		this.filterModified1.Name = "filterModified1";
		this.filterNotUpToDate2.Key = "filterNotUpToDate";
		this.filterNotUpToDate2.Name = "filterNotUpToDate2";
		base.AllowDrop = true;
		base.AutoScaleDimensions = new SizeF(6, 13);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.TopRebar1);
		base.Name = "SourcesPanel";
		base.Size = new Size(468, 397);
		base.DragDrop += new DragEventHandler(this.SourcesPanel_DragDrop);
		base.DragEnter += new DragEventHandler(this.SourcesPanel_DragEnter);
		this.uiCommandManager1.EndInit();
		this.BottomRebar1.EndInit();
		this.uiCommandBar1.EndInit();
		this.uiContextMenu1.EndInit();
		this.LeftRebar1.EndInit();
		this.RightRebar1.EndInit();
		this.TopRebar1.EndInit();
		this.TopRebar1.ResumeLayout(false);
		this.sourcesExplorerBar1.EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void linkShowAllSources_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Logger.LogUserAction();
		this.ShowAllSources();
	}

	private void LoadFilterSettings()
	{
		this.loadingFilterSettings = true;
		this.filterEnabled.Checked = ApplicationSettingsManager.Settings.SourcesFilter_Enabled.ToInheritableBoolean();
		this.filterModified.Checked = ApplicationSettingsManager.Settings.SourcesFilter_Modified.ToInheritableBoolean();
		this.filterNotUpToDate.Checked = ApplicationSettingsManager.Settings.SourcesFilter_NotUpToDate.ToInheritableBoolean();
		this.filterAll.Checked = !this.filterEnabled.Checked.ToBool() && !this.filterModified.Checked.ToBool() && this.filterNotUpToDate.Checked.ToBool().ToInheritableBoolean();
		this.loadingFilterSettings = false;
	}

	private void menuClipboard_Popup(object sender, CommandEventArgs e)
	{
		UIHelper.RefreshCopyCommands(e.Command.Commands);
	}

	private void menuWizards2_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		this.RunCustomWizard();
	}

	protected virtual void OnSelectionChanged()
	{
		if (this.selectionChanged != null)
		{
			this.selectionChanged(this, EventArgs.Empty);
		}
		this.presenter.EnableCommands();
	}

	private bool QuickCreateNewSource(string folderName, bool interactive)
	{
		bool cancel;
		Source source = Source.FromURL(folderName);
		if (interactive)
		{
			this.presenter.New(source, ref cancel);
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
		this.SourcesExplorerBar.RefreshEntities();
	}

	private void RefreshAndSetFilter(bool saveFilterSettings)
	{
		object[] objArray;
		if (base.InvokeRequired)
		{
			base.Invoke(new Action<bool>(this.RefreshAndSetFilter), new object[] { saveFilterSettings });
			return;
		}
		if (saveFilterSettings)
		{
			this.SaveFilterSettings();
		}
		this.SearchTextBox.Search(this.GetFilterPredicate());
	}

	private void RefreshLog()
	{
		Source source = this.SelectedItem;
		Updater.Instance.RefreshSource(source);
	}

	private void RegisterExplorerBarEvents()
	{
		this.UnregisterExplorerBarEvents();
		this.SourcesExplorerBar.add_ChangesClick(new EventHandler(this.SourcesExplorerBar_ItemChangesClick));
		this.SourcesExplorerBar.add_ConflictsClick(new EventHandler(this.SourcesExplorerBar_ItemConflictsClick));
		this.SourcesExplorerBar.add_ErrorClick(new EventHandler(this.SourcesExplorerBar_ItemErrorClick));
		this.SourcesExplorerBar.add_PathClick(new EventHandler(this.SourcesExplorerBar_ItemPathClick));
		this.SourcesExplorerBar.add_SyncdClick(new EventHandler(this.SourcesExplorerBar_ItemSyncdClick));
		this.SourcesExplorerBar.add_UnversionedClick(new EventHandler(this.SourcesExplorerBar_ItemUnversionedClick));
		this.SourcesExplorerBar.add_UpdatesClick(new EventHandler(this.SourcesExplorerBar_ItemUpdatesClick));
	}

	private void RunCustomWizard()
	{
		Source source = this.SelectedItem;
		CustomWizard wizard = new CustomWizard();
		wizard.Run(source.Name, "LogEntries", "Source");
	}

	private void SaveFilterSettings()
	{
		if (this.loadingFilterSettings)
		{
			return;
		}
		ApplicationSettingsManager.Settings.SourcesFilter_Enabled = this.filterEnabled.Checked.ToBool();
		ApplicationSettingsManager.Settings.SourcesFilter_Modified = this.filterModified.Checked.ToBool();
		ApplicationSettingsManager.Settings.SourcesFilter_NotUpToDate = this.filterNotUpToDate.Checked.ToBool();
		ApplicationSettingsManager.SaveSettings();
	}

	public void SelectSource(Source source)
	{
		this.SourcesExplorerBar.SelectEntity(source);
	}

	public void SetSearchResults(IEnumerable<Source> results)
	{
		this.SourcesExplorerBar.SetVisibleEntities(results);
		int count = results.Count<Source>();
		this.ShowingAllItems = count == this.Entities.Count;
		this.OnSelectionChanged();
	}

	private void ShowAllSources()
	{
		this.SearchTextBox.ClearNoFocus();
		this.filterAll.Checked = InheritableBoolean.True;
		this.ClearFilter();
	}

	private void Source_StatusChanged(object sender, StatusChangedEventArgs e)
	{
		this.SourcesExplorerBar.RefreshEntity((Source)e.Entity);
		this.presenter.EnableCommands();
		this.RefreshAndSetFilter(false);
	}

	private void SourceDragDrop(DragEventArgs e)
	{
		object[] objArray;
		if (e.Data == null)
		{
			return;
		}
		string[] folderNames = this.GetDraggedFolderNames(e.Data);
		if (folderNames == null || (int)folderNames.Length == 0)
		{
			return;
		}
		int count = 0;
		foreach (string folderName in folderNames)
		{
			if (FileSystemHelper.DirectoryExists(folderName))
			{
				bool interactive = (e.KeyState & 8) != 8;
				if (this.QuickCreateNewSource(folderName, interactive))
				{
					count++;
				}
			}
		}
		if (count > 0)
		{
			string message = Strings.SourcesCreated_FORMAT.FormatWith(new object[] { count, (count == 1 ? Strings.SourcesCreated_Source : Strings.SourcesCreated_Sources) });
			EventLog.LogInfo(message, folderNames);
			MainForm.FormInstance.ShowMessage(message, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void SourceDragEnter(DragEventArgs e)
	{
		e.Effect = DragDropEffects.None;
		if (e.Data == null)
		{
			return;
		}
		string[] folderNames = this.GetDraggedFolderNames(e.Data);
		if (folderNames == null || (int)folderNames.Length == 0)
		{
			return;
		}
		e.Effect = DragDropEffects.None;
		string[] strArrays = folderNames;
		int num = 0;
		num++;
		while (num < (int)strArrays.Length)
		{
			if (FileSystemHelper.DirectoryExists(folderName))
			{
				e.Effect = DragDropEffects.Copy;
				return;
			}
		}
	}

	private void sources1_KeyDown(object sender, KeyEventArgs e)
	{
		this.presenter.HandleKey(e);
	}

	private void sources1_SelectionChanged(object sender, EventArgs e)
	{
		this.presenter.EnableCommands();
	}

	private void SourcesExplorerBar_ItemChangesClick(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.SVNCommit();
	}

	private void SourcesExplorerBar_ItemConflictsClick(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.SVNDiff();
	}

	private void SourcesExplorerBar_ItemErrorClick(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.FocusError();
	}

	private void SourcesExplorerBar_ItemPathClick(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.Browse();
	}

	private void SourcesExplorerBar_ItemSyncdClick(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.SVNCheckModifications();
	}

	private void SourcesExplorerBar_ItemUnversionedClick(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.SVNAdd();
	}

	private void SourcesExplorerBar_ItemUpdatesClick(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.SVNUpdate();
	}

	private void sourcesExplorerBar1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		bool cancel;
		Logger.LogUserAction();
		if (cancel)
		{
			return;
		}
		if (this.SourcesExplorerBar.IsGroupAtLocation(e.Location, ref cancel))
		{
			this.Edit();
			return;
		}
		this.presenter.New();
	}

	private void sourcesExplorerBar1_MouseDown(object sender, MouseEventArgs e)
	{
		this.SourcesExplorerBar.SelectEntity(e.Location);
	}

	private void SourcesPanel_DragDrop(object sender, DragEventArgs e)
	{
		Logger.LogUserAction();
		this.SourceDragDrop(e);
	}

	private void SourcesPanel_DragEnter(object sender, DragEventArgs e)
	{
		Logger.LogUserAction();
		this.SourceDragEnter(e);
	}

	private void SVNAdd()
	{
		Source source = this.SelectedItem;
		this.SVNAdd(source);
	}

	private void SVNAdd(Source source)
	{
		source.SVNAdd();
	}

	private void SVNApplyPatch()
	{
		Source source = this.SelectedItem;
		source.ApplyPatch();
	}

	private void SVNBranchTag()
	{
		Source source = this.SelectedItem;
		source.BranchTag();
	}

	internal void SVNCheckModifications()
	{
		Source source = this.SelectedItem;
		this.SVNCheckModifications(source);
	}

	private void SVNCheckModifications(Source source)
	{
		source.CheckModifications();
	}

	private void SVNCheckout()
	{
		Source source = this.SelectedItem;
		source.Checkout();
	}

	private void SVNCleanUp()
	{
		Source source = this.SelectedItem;
		source.CleanUp();
	}

	internal void SVNCommit()
	{
		Source source = this.SelectedItem;
		this.SVNCommit(source);
	}

	private void SVNCommit(Source source)
	{
		source.SVNCommit();
	}

	private void SVNCreatePatch()
	{
		Source source = this.SelectedItem;
		source.CreatePatch();
	}

	private void SVNDeleteUnversioned()
	{
		Source source = this.SelectedItem;
		source.DeleteUnversioned();
	}

	private void SVNDiff()
	{
		Source source = this.SelectedItem;
		source.SVNDiff();
	}

	private void SVNExport()
	{
		Source source = this.SelectedItem;
		source.Export();
	}

	private void SVNGetLock()
	{
		Source source = this.SelectedItem;
		source.GetLock();
	}

	private void SVNMerge()
	{
		Source source = this.SelectedItem;
		source.Merge();
	}

	private virtual object SVNMonitor.View.Interfaces.ISelectableView<SVNMonitor.Entities.Source>.Invoke(Delegate )
	{
		return base.Invoke();
	}

	private void SVNMonitor.View.Interfaces.IUserEntityView<SVNMonitor.Entities.Source>.Delete()
	{
		this.SourcesExplorerBar.Delete();
		this.OnSelectionChanged();
	}

	private DialogResult SVNMonitor.View.Interfaces.IUserEntityView<SVNMonitor.Entities.Source>.UserEdit(Source entity)
	{
		Source source = entity;
		Logger.Log.InfoFormat("Editing source (source={0})", source);
		DialogResult result = SourcePropertiesDialog.ShowDialog(source);
		if (result == DialogResult.OK && source != null)
		{
			Logger.Log.InfoFormat("User pressed OK (source={0})", source);
			Updater.Instance.QueueUpdate(source, false);
		}
		return result;
	}

	private DialogResult SVNMonitor.View.Interfaces.IUserEntityView<SVNMonitor.Entities.Source>.UserNew(Source entity)
	{
		Source source = entity;
		Logger.Log.InfoFormat("Creating source (GUID={0})", source.Guid);
		DialogResult result = SourcePropertiesDialog.ShowDialog(source);
		if (result == DialogResult.OK && source != null)
		{
			Logger.Log.InfoFormat("User pressed OK (source={0})", source);
			EventLog.Log(EventLogEntryType.Source, string.Format("Source '{0}' created", entity.Name), entity);
			if (source.Enabled)
			{
				Logger.Log.InfoFormat("Source is enabled. Updating.", new object[0]);
				EventLog.Log(EventLogEntryType.Source, "Getting source information...", entity);
				Updater.Instance.QueueUpdate(source, false);
			}
		}
		return result;
	}

	private void SVNProperties()
	{
		Source source = this.SelectedItem;
		source.Properties();
	}

	private void SVNReintegrate()
	{
		Source source = this.SelectedItem;
		source.Reintegrate();
	}

	private void SVNReleaseLock()
	{
		Source source = this.SelectedItem;
		source.ReleaseLock();
	}

	private void SVNRelocate()
	{
		Source source = this.SelectedItem;
		source.Relocate();
	}

	private void SVNRepoBrowser()
	{
		Source source = this.SelectedItem;
		source.RepoBrowser();
	}

	private void SVNResolve()
	{
		Source source = this.SelectedItem;
		source.Resolve();
	}

	internal void SVNRevert()
	{
		Source source = this.SelectedItem;
		source.SVNRevert();
	}

	private void SVNRevisionGraph()
	{
		Source source = this.SelectedItem;
		source.RevisionGraph();
	}

	private void SVNShowLog()
	{
		Source source = this.SelectedItem;
		source.SVNShowLog();
	}

	private void SVNSwitch()
	{
		Source source = this.SelectedItem;
		source.Switch();
	}

	internal void SVNUpdate()
	{
		Source source = this.SelectedItem;
		this.SVNUpdate(source);
	}

	private void SVNUpdate(Source source)
	{
		DialogResult result = UpdateHeadPromptDialog.Prompt();
		Logger.Log.InfoFormat("Update Source: User clicked {0}", result);
		if (result != DialogResult.Yes)
		{
			return;
		}
		source.SVNUpdate();
	}

	private void SVNUpdateAll(bool ignoreUpToDate)
	{
		Source.SVNUpdateAll(ignoreUpToDate);
	}

	private void UnregisterExplorerBarEvents()
	{
		this.SourcesExplorerBar.remove_ChangesClick(new EventHandler(this.SourcesExplorerBar_ItemChangesClick));
		this.SourcesExplorerBar.remove_ConflictsClick(new EventHandler(this.SourcesExplorerBar_ItemConflictsClick));
		this.SourcesExplorerBar.remove_ErrorClick(new EventHandler(this.SourcesExplorerBar_ItemErrorClick));
		this.SourcesExplorerBar.remove_PathClick(new EventHandler(this.SourcesExplorerBar_ItemPathClick));
		this.SourcesExplorerBar.remove_SyncdClick(new EventHandler(this.SourcesExplorerBar_ItemSyncdClick));
		this.SourcesExplorerBar.remove_UnversionedClick(new EventHandler(this.SourcesExplorerBar_ItemUnversionedClick));
		this.SourcesExplorerBar.remove_UpdatesClick(new EventHandler(this.SourcesExplorerBar_ItemUpdatesClick));
	}

	internal void UpdateAllSources()
	{
		Updater.Instance.QueueUpdates();
	}

	internal void UpdateSource()
	{
		Source source = this.SelectedItem;
		Updater.Instance.QueueUpdate(source, true);
	}

	public event EventHandler SelectionChanged;
}
}