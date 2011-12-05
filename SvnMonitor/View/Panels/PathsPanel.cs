using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Windows.Forms;

using Janus.Windows.GridEX;
using Janus.Windows.UI.CommandBars;

using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.Settings;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Interfaces;
using SVNMonitor.Wizards;

namespace SVNMonitor.View.Panels
{
	internal partial class PathsPanel : GridPanel, ISupportInitialize, ISelectableView<SVNPath>, ISearchablePanel<SVNPath>
	{
		private bool selectedWithKeyboard;
		private bool suppressMoveFirst;
		private bool suppressSelectionChanged;
		private List<SVNPath> allPaths;
		
		private SVNLogEntry lastLogEntry;
		private Source lastSource;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ILogEntriesView logEntriesView;
		private Dictionary<UserAction, Janus.Windows.UI.CommandBars.UICommand> userCommandsMap;

		public PathsPanel()
		{
			InitializeComponent();
			if (!base.DesignMode)
			{
				UIHelper.ApplyResources(uiCommandManager1);
				UIHelper.ApplyResources(Grid, this);
				InitSelectionTimer();
				MapUserCommands();
				InitializeClipboardDelegates();
			}
		}

		public void BeginInit()
		{
		}

		private void Browse()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.Browse();
			}
		}

		private bool CanDoDefaultAction()
		{
			string actionString = ApplicationSettingsManager.Settings.DefaultPathAction;
			UserAction action = EnumHelper.ParseEnum<UserAction>(actionString);
			foreach (PropertyInfo prop in typeof(PathsPanel).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(p => Attribute.IsDefined(p, typeof(AssociatedUserActionAttribute))))
			{
				AssociatedUserActionAttribute attribute = (AssociatedUserActionAttribute)Attribute.GetCustomAttribute(prop, typeof(AssociatedUserActionAttribute));
				if ((attribute != null) && (action == attribute.UserAction))
				{
					return (bool)prop.GetValue(this, null);
				}
			}
			Logger.Log.ErrorFormat("Can't find property for associated user action: {0}", actionString);
			return false;
		}

		public void ClearSearch()
		{
			SetPaths(allPaths);
		}

		private void cmdBlame_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenBlame();
		}

		private void cmdBrowse_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Browse();
		}

		private void cmdCommit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNCommit();
		}

		private void cmdDiff_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenDiffAuto();
		}

		private void cmdDiffLocalWithBase_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenDiffLocalWithBase();
		}

		private void cmdDiffWithPrevious_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenDiffWithPrevious();
		}

		private void cmdEdit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			FileEdit();
		}

		private void cmdExplore_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Explore();
		}

		private void cmdExport_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SaveRevision();
		}

		private void cmdOpen_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Open();
		}

		private void cmdOpenWith_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenWith();
		}

		private void cmdRevert_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNRevert();
		}

		private void cmdRollback_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			Rollback();
		}

		private void cmdSVNLog_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenSVNLog();
		}

		private void cmdSVNUpdate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdate();
		}

		private void DoDefaultPathAction()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				if (!CanDoDefaultAction())
				{
					string action = ApplicationSettingsManager.Settings.DefaultPathAction;
					string message = Strings.ErrorCantPerformDefaultPathAction_FORMAT.FormatWith(new object[]
					{
						action
					});
					MainForm.FormInstance.ShowMessage(message, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					SVNMonitor.EventLog.LogWarning(message);
				}
				else
				{
					cmd.DoDefaultPathAction();
				}
			}
		}

		private void EnableCommands()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(EnableCommands));
			}
			else
			{
				SVNPath path = SelectedItem;
				if (path != null)
				{
					EnableCommandsForSingleSelectedPath(path);
				}
				else
				{
					IEnumerable<SVNPath> paths = SelectedItems;
					EnableCommandsForMultipleSelectedPaths(paths);
				}
			}
		}

		private void EnableCommandsForMultipleSelectedPaths(IEnumerable<SVNPath> paths)
		{
			CanBlame = false;
			CanDiff = false;
			CanDiffLocalWithBase = false;
			CanDiffWithPrevious = false;
			CanExplore = false;
			CanBrowse = false;
			CanOpen = false;
			CanOpenSVNLog = false;
			CanOpenWith = false;
			CanRollback = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanRollback);
			CanRunPathWizard = false;
			CanSVNUpdate = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanSVNUpdate);
			CanCommit = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanCommit);
			CanRevert = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanRevert);
			CanEdit = false;
			CanCopyToClipboard = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanCopyToClipboard);
			CanCopyFullName = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanCopyFullName);
			CanCopyName = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanCopyName);
			CanCopyURL = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanCopyURL);
			CanCopyRelativeURL = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanCopyRelativeURL);
			CanExport = SVNPathHelper.CanMultiple(paths, SVNPathHelper.CanExport);
		}

		private void EnableCommandsForSingleSelectedPath(SVNPath path)
		{
			CanBlame = SVNPathHelper.CanBlame(path);
			CanDiff = SVNPathHelper.CanDiff(path);
			CanDiffLocalWithBase = SVNPathHelper.CanDiff(path);
			CanDiffWithPrevious = SVNPathHelper.CanDiff(path);
			CanExplore = SVNPathHelper.CanExplore(path);
			CanBrowse = SVNPathHelper.CanBrowse(path);
			CanOpen = SVNPathHelper.CanOpen(path);
			CanOpenSVNLog = SVNPathHelper.CanOpenSVNLog(path);
			CanOpenWith = SVNPathHelper.CanOpenWith(path);
			CanRollback = SVNPathHelper.CanRollback(path);
			CanRunPathWizard = SVNPathHelper.CanRunPathWizard(path);
			CanSVNUpdate = SVNPathHelper.CanSVNUpdate(path);
			CanCommit = SVNPathHelper.CanCommit(path);
			CanRevert = SVNPathHelper.CanRevert(path);
			CanEdit = SVNPathHelper.CanEdit(path);
			CanCopyToClipboard = SVNPathHelper.CanCopyToClipboard(path);
			CanCopyFullName = SVNPathHelper.CanCopyFullName(path);
			CanCopyName = SVNPathHelper.CanCopyName(path);
			CanCopyURL = SVNPathHelper.CanCopyURL(path);
			CanCopyRelativeURL = SVNPathHelper.CanCopyRelativeURL(path);
			CanExport = SVNPathHelper.CanExport(path);
		}

		public void EndInit()
		{
			if (!base.DesignMode)
			{
				UIHelper.GetBaseName getFileName = item => ((SVNPath)item).FilePath;
				UIHelper.InitializeWizardsMenu(this, uiContextMenu1, menuFileWizard, getFileName, "Paths", "Path");
				EnableCommands();
			}
		}

		private void Explore()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.Explore();
			}
		}

		private void FileEdit()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.FileEdit();
			}
		}

		public IEnumerable<SVNPath> GetAllItems()
		{
			return Paths;
		}

		private string GetFullNameToClipboard()
		{
			IEnumerable<SVNPath> paths = SelectedItems;
			if ((paths == null) || (paths.Count() == 0))
			{
				return string.Empty;
			}
			return string.Join(Environment.NewLine, paths.Select(p => p.FilePath).ToArray());
		}

		private string GetNameToClipboard()
		{
			IEnumerable<SVNPath> paths = SelectedItems;
			if ((paths == null) || (paths.Count() == 0))
			{
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			foreach (SVNPath path in paths)
			{
				string[] parts = path.Name.Split(new[]
				{
					'/', '\\'
				});
				if ((parts == null) || (parts.Length == 0))
				{
					return string.Empty;
				}
				sb.AppendLine(parts[parts.Length - 1]);
			}
			return sb.ToString();
		}

		private string GetRelativeURLToClipboard()
		{
			IEnumerable<SVNPath> paths = SelectedItems;
			if ((paths == null) || (paths.Count() == 0))
			{
				return string.Empty;
			}
			return string.Join(Environment.NewLine, paths.Select(p => p.Name).ToArray());
		}

		private string GetURLToClipboard()
		{
			IEnumerable<SVNPath> paths = SelectedItems;
			if ((paths == null) || (paths.Count() == 0))
			{
				return string.Empty;
			}
			return string.Join(Environment.NewLine, paths.Select(p => p.Uri).ToArray());
		}

		private void InitializeClipboardDelegates()
		{
			UIHelper.AddCopyCommand(cmdCopyFullName, GetFullNameToClipboard);
			UIHelper.AddCopyCommand(cmdCopyName, GetNameToClipboard);
			UIHelper.AddCopyCommand(cmdCopyURL, GetURLToClipboard);
			UIHelper.AddCopyCommand(cmdCopyRelativeURL, GetRelativeURLToClipboard);
		}

		private void InitSelectionTimer()
		{
			logEntrySelectionTimer = new System.Timers.Timer();
			pathSelectionTimer = new System.Timers.Timer();
			logEntrySelectionTimer.Interval = 200.0;
			pathSelectionTimer.Interval = 200.0;
			logEntrySelectionTimer.AutoReset = false;
			pathSelectionTimer.AutoReset = false;
			logEntrySelectionTimer.Elapsed += logEntrySelectionTimer_Elapsed;
			pathSelectionTimer.Elapsed += pathSelectionTimer_Elapsed;
		}

		private void logEntriesView_SelectionChanged(object sender, EventArgs e)
		{
			StartLogEntrySelectionTimer();
		}

		private void logEntrySelectionTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			UpdateSelectedLogEntry();
		}

		private void MapUserCommands()
		{
			userCommandsMap = new Dictionary<UserAction, UICommand>();
			UserCommandsMap.Add(UserAction.Blame, cmdBlame);
			UserCommandsMap.Add(UserAction.Browse, cmdBrowse);
			UserCommandsMap.Add(UserAction.Commit, cmdCommit);
			UserCommandsMap.Add(UserAction.Diff, cmdDiff);
			UserCommandsMap.Add(UserAction.DiffLocalWithBase, cmdDiffLocalWithBase);
			UserCommandsMap.Add(UserAction.DiffWithPrevious, cmdDiffWithPrevious);
			UserCommandsMap.Add(UserAction.Edit, cmdEdit);
			UserCommandsMap.Add(UserAction.Explore, cmdExplore);
			UserCommandsMap.Add(UserAction.Open, cmdOpen);
			UserCommandsMap.Add(UserAction.OpenWith, cmdOpenWith);
			UserCommandsMap.Add(UserAction.Revert, cmdRevert);
			UserCommandsMap.Add(UserAction.Rollback, cmdRollback);
			UserCommandsMap.Add(UserAction.ShowLog, cmdSVNLog);
			UserCommandsMap.Add(UserAction.Update, cmdSVNUpdate);
			UserCommandsMap.Add(UserAction.SaveRevision, cmdExport);
		}

		private void menuClipboard_Popup(object sender, CommandEventArgs e)
		{
			UIHelper.RefreshCopyCommands(e.Command.Commands);
		}

		private void menuFileWizard2_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			RunCustomWizard();
		}

		private void Open()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.Open();
			}
		}

		private void OpenBlame()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.Blame();
			}
		}

		private void OpenDiffAuto()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.DiffAuto();
			}
		}

		private void OpenDiffLocalWithBase()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.DiffLocalWithBase();
			}
		}

		private void OpenDiffWithPrevious()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.DiffWithPrevious();
			}
		}

		private void OpenSVNLog()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.OpenSVNLog();
			}
		}

		private void OpenWith()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.OpenWith();
			}
		}

		private void pathSelectionTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			UpdateSelectedPath();
		}

		private void pathsGrid1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				Logger.LogUserAction("key=" + e.KeyCode);
				DoDefaultPathAction();
			}
			if ((((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down)) || ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right))) || ((e.KeyCode == Keys.Next) || (e.KeyCode == Keys.Prior)))
			{
				selectedWithKeyboard = true;
			}
		}

		private void pathsGrid1_MouseDown(object sender, MouseEventArgs e)
		{
			selectedWithKeyboard = false;
		}

		private void pathsGrid1_RowDoubleClick(object sender, RowActionEventArgs e)
		{
			Logger.LogUserAction();
			if ((e.Row != null) && (e.Row.DataRow is SVNPath))
			{
				DoDefaultPathAction();
			}
		}

		private void pathsGrid1_SelectionChanged(object sender, EventArgs e)
		{
			if (!suppressSelectionChanged)
			{
				StartPathSelectionTimer();
			}
		}

		private void Refetch()
		{
			suppressSelectionChanged = true;
			int[] currentRows = Grid.SelectedItems.Cast<GridEXSelectedItem>().Select<GridEXSelectedItem, int>(item => item.Position).ToArray<int>();
			Grid.Refetch();
			Grid.SelectedItems.Clear();
			foreach (int row in currentRows)
			{
				Grid.SelectedItems.Add(row);
			}
			suppressSelectionChanged = false;
		}

		private void Rollback()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.Rollback();
			}
		}

		private void RunCustomWizard()
		{
			SVNPath path = SelectedItem;
			new CustomWizard().Run(path.FilePath, "Paths", "Path");
		}

		private void SaveRevision()
		{
			SVNPath path = SelectedItem;
			if (path != null)
			{
				using (SaveFileDialog dialog = new SaveFileDialog())
				{
					dialog.FileName = path.FileName;
					dialog.Title = Strings.SavingItemRevision_FORMAT.FormatWith(new object[]
					{
						path.Revision, path.Uri
					});
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						string targetName = dialog.FileName;
						path.CreateCommands().SaveRevision(targetName);
					}
				}
			}
		}

		private void SetDefaultPopupCommand()
		{
			UserAction action = EnumHelper.ParseEnum<UserAction>(ApplicationSettingsManager.Settings.DefaultPathAction);
			foreach (UICommand cmd in uiCommandManager1.Commands)
			{
				cmd.DefaultItem = Janus.Windows.UI.InheritableBoolean.False;
			}
			UICommand defaultCommand = UserCommandsMap[action];
			string key = defaultCommand.Key;
			defaultCommand = uiCommandManager1.Commands[key];
			defaultCommand.DefaultItem = Janus.Windows.UI.InheritableBoolean.True;
		}

		private void SetOpenCommandImage()
		{
			cmdOpen.Image = Images.document_plain;
			SVNPath path = SelectedItem;
			if ((path != null) && path.ExistsLocally)
			{
				try
				{
					Image image = FileSystemHelper.GetAssociatedIcon(path.FilePath);
					if (cmdOpen.Image != null)
					{
						cmdOpen.Image.Dispose();
					}
					cmdOpen.Image = image;
				}
				catch (Exception ex)
				{
					Logger.Log.InfoFormat("Error trying to extract associated icon of '{0}'", path.FilePath);
					Logger.Log.Info(ex.Message, ex);
				}
			}
		}

		internal void SetPaths()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(SetPaths));
			}
			else
			{
				if (SearchTextBox != null)
				{
					SearchTextBox.Search();
				}
				try
				{
					Refetch();
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Error trying to refetch paths:", ex);
				}
			}
		}

		internal void SetPaths(IEnumerable<SVNPath> paths)
		{
			((PathsGrid)Grid).Paths = paths;
			if (!suppressMoveFirst)
			{
				Grid.MoveFirst();
			}
		}

		public void SetSearchResults(IEnumerable<SVNPath> results)
		{
			SetPaths(results);
		}

		private void StartLogEntrySelectionTimer()
		{
			SVNLogEntry selectedLogEntry = LogEntriesView.SelectedItem;
			if (selectedLogEntry == null)
			{
				Paths = null;
			}
			else if ((selectedLogEntry.Source != lastSource) || !LogEntriesView.SelectedWithKeyboard)
			{
				UpdateSelectedLogEntry();
			}
			else
			{
				logEntrySelectionTimer.Stop();
				logEntrySelectionTimer.Start();
			}
		}

		private void StartPathSelectionTimer()
		{
			if (!selectedWithKeyboard)
			{
				UpdateSelectedPath();
			}
			else
			{
				pathSelectionTimer.Stop();
				pathSelectionTimer.Start();
			}
		}

		private void SVNCommit()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.Commit();
			}
		}

		object ISelectableView<SVNPath>.Invoke(Delegate delegate1)
		{
			return base.Invoke(delegate1);
		}

		private void SVNRevert()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.Revert();
			}
		}

		private void SVNUpdate()
		{
			SVNPathCommands cmd = SelectedItems.CreateCommands();
			if (cmd != null)
			{
				cmd.SVNUpdate();
			}
		}

		private void uiContextMenu1_Popup(object sender, EventArgs e)
		{
			SetDefaultPopupCommand();
		}

		private void UpdateSelectedLogEntry()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(UpdateSelectedLogEntry));
			}
			else
			{
				SVNLogEntry selectedLogEntry = LogEntriesView.SelectedItem;
				if (selectedLogEntry == null)
				{
					Paths = null;
				}
				else
				{
					if (lastLogEntry == selectedLogEntry)
					{
						suppressMoveFirst = true;
					}
					Paths = selectedLogEntry.Paths;
					suppressMoveFirst = false;
					lastSource = selectedLogEntry.Source;
					lastLogEntry = selectedLogEntry;
				}
			}
		}

		private void UpdateSelectedPath()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(UpdateSelectedPath));
			}
			else
			{
				EnableCommands();
				SetOpenCommandImage();
			}
		}

		[Browsable(false), AssociatedUserAction(UserAction.Blame), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanBlame
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdBlame); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdBlame, value); }
		}

		[AssociatedUserAction(UserAction.Browse), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanBrowse
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdBrowse); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdBrowse, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), AssociatedUserAction(UserAction.Commit)]
		public bool CanCommit
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCommit); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCommit, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanCopyFullName
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopyFullName); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopyFullName, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanCopyName
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopyName); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopyName, value); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanCopyRelativeURL
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopyRelativeURL); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopyRelativeURL, value); }
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
		public bool CanCopyURL
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdCopyURL); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdCopyURL, value); }
		}

		[Browsable(false), AssociatedUserAction(UserAction.Diff), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanDiff
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdDiff); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdDiff, value); }
		}

		[AssociatedUserAction(UserAction.DiffLocalWithBase), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanDiffLocalWithBase
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdDiffLocalWithBase); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdDiffLocalWithBase, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), AssociatedUserAction(UserAction.DiffWithPrevious)]
		public bool CanDiffWithPrevious
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdDiffWithPrevious); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdDiffWithPrevious, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), AssociatedUserAction(UserAction.Edit), Browsable(false)]
		public bool CanEdit
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdEdit); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdEdit, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), AssociatedUserAction(UserAction.Explore)]
		public bool CanExplore
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdExplore); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdExplore, value); }
		}

		[AssociatedUserAction(UserAction.SaveRevision), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanExport
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdExport); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdExport, value); }
		}

		[Browsable(false), AssociatedUserAction(UserAction.Open), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanOpen
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdOpen); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdOpen, value); }
		}

		[Browsable(false), AssociatedUserAction(UserAction.ShowLog), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanOpenSVNLog
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdSVNLog); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdSVNLog, value); }
		}

		[AssociatedUserAction(UserAction.OpenWith), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanOpenWith
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdOpenWith); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdOpenWith, value); }
		}

		[Browsable(false), AssociatedUserAction(UserAction.Revert), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanRevert
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdRevert); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdRevert, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), AssociatedUserAction(UserAction.Rollback), Browsable(false)]
		public bool CanRollback
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdRollback); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdRollback, value); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool CanRunPathWizard
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(menuFileWizard); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(menuFileWizard, value); }
		}

		[AssociatedUserAction(UserAction.Update), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanSVNUpdate
		{
			[DebuggerNonUserCode]
			get { return UIHelper.IsCommandEnabled(cmdSVNUpdate); }
			[DebuggerNonUserCode]
			set { UIHelper.SetCommandEnabled(cmdSVNUpdate, value); }
		}

		protected override Janus.Windows.GridEX.GridEX Grid
		{
			[DebuggerNonUserCode]
			get { return pathsGrid1; }
		}

		protected override string LayoutSettings
		{
			get { return ApplicationSettingsManager.Settings.UIPathsGridLayout; }
		}

		[Browsable(true)]
		public ILogEntriesView LogEntriesView
		{
			[DebuggerNonUserCode]
			get { return logEntriesView; }
			[DebuggerNonUserCode]
			set
			{
				if (logEntriesView != null)
				{
					logEntriesView.SelectionChanged -= logEntriesView_SelectionChanged;
				}
				logEntriesView = value;
				if (logEntriesView != null)
				{
					logEntriesView.SelectionChanged += logEntriesView_SelectionChanged;
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private List<SVNPath> Paths
		{
			[DebuggerNonUserCode]
			get { return allPaths; }
			set
			{
				allPaths = value;
				SetPaths();
				EnableCommands();
			}
		}

		[Browsable(false)]
		public SearchTextBox<SVNPath> SearchTextBox { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public SVNPath SelectedItem
		{
			get { return ((PathsGrid)Grid).SelectedPath; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public IEnumerable<SVNPath> SelectedItems
		{
			get { return ((PathsGrid)Grid).SelectedPaths; }
		}

		public Janus.Windows.UI.CommandBars.UIContextMenu UIContextMenu
		{
			get { return uiContextMenu1; }
		}

		private Dictionary<UserAction, UICommand> UserCommandsMap
		{
			[DebuggerNonUserCode]
			get { return userCommandsMap; }
		}
	}
}