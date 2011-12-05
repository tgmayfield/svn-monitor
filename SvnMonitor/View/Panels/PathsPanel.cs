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
	internal class PathsPanel : GridPanel, ISupportInitialize, ISelectableView<SVNPath>, ISearchablePanel<SVNPath>
	{
		private List<SVNPath> allPaths;
		private UIRebar BottomRebar1;
		private UICommand cmdBlame;
		private UICommand cmdBlame1;
		private UICommand cmdBlame2;
		private UICommand cmdBrowse;
		private UICommand cmdBrowse1;
		private UICommand cmdBrowse2;
		private UICommand cmdCommit;
		private UICommand cmdCommit1;
		private UICommand cmdCommit2;
		private UICommand cmdCopyFullName;
		private UICommand cmdCopyFullName1;
		private UICommand cmdCopyName;
		private UICommand cmdCopyName1;
		private UICommand cmdCopyRelativeURL;
		private UICommand cmdCopyRelativeUrl1;
		private UICommand cmdCopyURL;
		private UICommand cmdCopyURL1;
		private UICommand cmdDiff;
		private UICommand cmdDiff2;
		private UICommand cmdDiffLocalWithBase;
		private UICommand cmdDiffLocalWithBase1;
		private UICommand cmdDiffLocalWithBase2;
		private UICommand cmdDiffWithPrevious;
		private UICommand cmdDiffWithPrevious1;
		private UICommand cmdDiffWithPrevious2;
		private UICommand cmdEdit;
		private UICommand cmdEdit1;
		private UICommand cmdEdit2;
		private UICommand cmdExplore;
		private UICommand cmdExplore1;
		private UICommand cmdExplore2;
		private UICommand cmdExport;
		private UICommand cmdExport1;
		private UICommand cmdOpen;
		private UICommand cmdOpen1;
		private UICommand cmdOpen2;
		private UICommand cmdOpenWith;
		private UICommand cmdOpenWith2;
		private UICommand cmdOpenWith3;
		private UICommand cmdRevert;
		private UICommand cmdRevert1;
		private UICommand cmdRevert2;
		private UICommand cmdRollback;
		private UICommand cmdRollback1;
		private UICommand cmdRollback2;
		private UICommand cmdSVNLog;
		private UICommand cmdSVNLog1;
		private UICommand cmdSVNLog2;
		private UICommand cmdSVNUpdate;
		private UICommand cmdSVNUpdate1;
		private UICommand cmdSVNUpdate2;
		private IContainer components;
		private SVNLogEntry lastLogEntry;
		private Source lastSource;
		private UIRebar LeftRebar1;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ILogEntriesView logEntriesView;
		private System.Timers.Timer logEntrySelectionTimer;
		private UICommand menuClipboard;
		private UICommand menuClipboard1;
		private UICommand menuFileWizard;
		private UICommand menuFileWizard1;
		private UICommand menuFileWizard2;
		private Panel panel1;
		private System.Timers.Timer pathSelectionTimer;
		private PathsGrid pathsGrid1;
		private UIRebar RightRebar1;
		private bool selectedWithKeyboard;
		private UICommand Separator1;
		private UICommand Separator2;
		private UICommand Separator3;
		private UICommand Separator4;
		private UICommand Separator5;
		private UICommand Separator6;
		private bool suppressMoveFirst;
		private bool suppressSelectionChanged;
		private UIRebar TopRebar1;
		private UICommandBar uiCommandBar1;
		private UICommandManager uiCommandManager1;
		private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
		private Dictionary<UserAction, UICommand> userCommandsMap;

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

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
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
			return string.Join(Environment.NewLine, paths.Select(p => p.FilePath).ToArray<string>());
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
			return string.Join(Environment.NewLine, paths.Select(p => p.Name).ToArray<string>());
		}

		private string GetURLToClipboard()
		{
			IEnumerable<SVNPath> paths = SelectedItems;
			if ((paths == null) || (paths.Count() == 0))
			{
				return string.Empty;
			}
			return string.Join(Environment.NewLine, paths.Select(p => p.Uri).ToArray<string>());
		}

		private void InitializeClipboardDelegates()
		{
			UIHelper.AddCopyCommand(cmdCopyFullName, GetFullNameToClipboard);
			UIHelper.AddCopyCommand(cmdCopyName, GetNameToClipboard);
			UIHelper.AddCopyCommand(cmdCopyURL, GetURLToClipboard);
			UIHelper.AddCopyCommand(cmdCopyRelativeURL, GetRelativeURLToClipboard);
		}

		private void InitializeComponent()
		{
			components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(PathsPanel));
			GridEXLayout pathsGrid1_DesignTimeLayout = new GridEXLayout();
			cmdExplore = new UICommand("cmdExplore");
			pathsGrid1 = new PathsGrid();
			uiCommandManager1 = new UICommandManager(components);
			uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
			cmdSVNUpdate2 = new UICommand("cmdSVNUpdate");
			cmdCommit2 = new UICommand("cmdCommit");
			cmdRevert2 = new UICommand("cmdRevert");
			cmdRollback1 = new UICommand("cmdRollback");
			Separator3 = new UICommand("Separator");
			cmdSVNLog1 = new UICommand("cmdSVNLog");
			cmdDiffWithPrevious2 = new UICommand("cmdDiffWithPrevious");
			cmdDiffLocalWithBase2 = new UICommand("cmdDiffLocalWithBase");
			cmdBlame1 = new UICommand("cmdBlame");
			Separator1 = new UICommand("Separator");
			cmdExplore1 = new UICommand("cmdExplore");
			cmdBrowse2 = new UICommand("cmdBrowse");
			cmdOpen1 = new UICommand("cmdOpen");
			cmdOpenWith2 = new UICommand("cmdOpenWith");
			cmdEdit2 = new UICommand("cmdEdit");
			cmdExport1 = new UICommand("cmdExport");
			menuClipboard1 = new UICommand("menuClipboard");
			Separator5 = new UICommand("Separator");
			menuFileWizard1 = new UICommand("menuFileWizard");
			BottomRebar1 = new UIRebar();
			uiCommandBar1 = new UICommandBar();
			cmdSVNUpdate1 = new UICommand("cmdSVNUpdate");
			cmdCommit1 = new UICommand("cmdCommit");
			cmdRevert1 = new UICommand("cmdRevert");
			cmdRollback2 = new UICommand("cmdRollback");
			Separator4 = new UICommand("Separator");
			cmdSVNLog2 = new UICommand("cmdSVNLog");
			cmdDiff2 = new UICommand("cmdDiff");
			cmdBlame2 = new UICommand("cmdBlame");
			Separator2 = new UICommand("Separator");
			cmdExplore2 = new UICommand("cmdExplore");
			cmdBrowse1 = new UICommand("cmdBrowse");
			cmdOpen2 = new UICommand("cmdOpen");
			cmdOpenWith3 = new UICommand("cmdOpenWith");
			cmdEdit1 = new UICommand("cmdEdit");
			Separator6 = new UICommand("Separator");
			menuFileWizard2 = new UICommand("menuFileWizard");
			cmdOpen = new UICommand("cmdOpen");
			cmdOpenWith = new UICommand("cmdOpenWith");
			cmdSVNLog = new UICommand("cmdSVNLog");
			cmdDiff = new UICommand("cmdDiff");
			cmdDiffWithPrevious1 = new UICommand("cmdDiffWithPrevious");
			cmdDiffLocalWithBase1 = new UICommand("cmdDiffLocalWithBase");
			cmdDiffWithPrevious = new UICommand("cmdDiffWithPrevious");
			cmdDiffLocalWithBase = new UICommand("cmdDiffLocalWithBase");
			cmdBlame = new UICommand("cmdBlame");
			cmdSVNUpdate = new UICommand("cmdSVNUpdate");
			cmdRollback = new UICommand("cmdRollback");
			menuFileWizard = new UICommand("menuFileWizard");
			cmdCommit = new UICommand("cmdCommit");
			cmdRevert = new UICommand("cmdRevert");
			cmdBrowse = new UICommand("cmdBrowse");
			cmdEdit = new UICommand("cmdEdit");
			menuClipboard = new UICommand("menuClipboard");
			cmdCopyFullName1 = new UICommand("cmdCopyFullName");
			cmdCopyName1 = new UICommand("cmdCopyName");
			cmdCopyRelativeUrl1 = new UICommand("cmdCopyRelativeURL");
			cmdCopyURL1 = new UICommand("cmdCopyURL");
			cmdCopyFullName = new UICommand("cmdCopyFullName");
			cmdCopyName = new UICommand("cmdCopyName");
			cmdCopyURL = new UICommand("cmdCopyURL");
			cmdCopyRelativeURL = new UICommand("cmdCopyRelativeURL");
			cmdExport = new UICommand("cmdExport");
			LeftRebar1 = new UIRebar();
			RightRebar1 = new UIRebar();
			TopRebar1 = new UIRebar();
			panel1 = new Panel();
			((ISupportInitialize)pathsGrid1).BeginInit();
			((ISupportInitialize)uiCommandManager1).BeginInit();
			((ISupportInitialize)uiContextMenu1).BeginInit();
			((ISupportInitialize)BottomRebar1).BeginInit();
			((ISupportInitialize)uiCommandBar1).BeginInit();
			((ISupportInitialize)LeftRebar1).BeginInit();
			((ISupportInitialize)RightRebar1).BeginInit();
			((ISupportInitialize)TopRebar1).BeginInit();
			TopRebar1.SuspendLayout();
			panel1.SuspendLayout();
			base.SuspendLayout();
			cmdExplore.Image = (Image)resources.GetObject("cmdExplore.Image");
			cmdExplore.Key = "cmdExplore";
			cmdExplore.Name = "cmdExplore";
			cmdExplore.Text = "Explore";
			cmdExplore.ToolTipText = "Explore";
			cmdExplore.Click += cmdExplore_Click;
			pathsGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			pathsGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			pathsGrid1.ColumnAutoResize = true;
			uiCommandManager1.SetContextMenu(pathsGrid1, uiContextMenu1);
			pathsGrid1_DesignTimeLayout.LayoutString = resources.GetString("pathsGrid1_DesignTimeLayout.LayoutString");
			pathsGrid1.DesignTimeLayout = pathsGrid1_DesignTimeLayout;
			pathsGrid1.Dock = DockStyle.Fill;
			pathsGrid1.EnterKeyBehavior = EnterKeyBehavior.None;
			pathsGrid1.Font = new Font("Microsoft Sans Serif", 8.25f);
			pathsGrid1.GridLineColor = SystemColors.Control;
			pathsGrid1.GridLines = GridLines.Horizontal;
			pathsGrid1.GroupByBoxVisible = false;
			pathsGrid1.HideSelection = HideSelection.HighlightInactive;
			pathsGrid1.Location = new Point(0, 1);
			pathsGrid1.Name = "pathsGrid1";
			pathsGrid1.SelectedFormatStyle.BackColor = Color.SteelBlue;
			pathsGrid1.SelectedInactiveFormatStyle.BackColor = Color.FromArgb(0xec, 0xf5, 0xff);
			pathsGrid1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
			pathsGrid1.SettingsKey = "pathsGrid1";
			pathsGrid1.Size = new Size(0x39b, 0x79);
			pathsGrid1.TabIndex = 4;
			pathsGrid1.TabKeyBehavior = TabKeyBehavior.ControlNavigation;
			pathsGrid1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			pathsGrid1.KeyDown += pathsGrid1_KeyDown;
			pathsGrid1.RowDoubleClick += pathsGrid1_RowDoubleClick;
			pathsGrid1.MouseDown += pathsGrid1_MouseDown;
			pathsGrid1.SelectionChanged += pathsGrid1_SelectionChanged;
			uiCommandManager1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.BottomRebar = BottomRebar1;
			uiCommandManager1.CommandBars.AddRange(new[]
			{
				uiCommandBar1
			});
			uiCommandManager1.Commands.AddRange(new[]
			{
				cmdExplore, cmdOpen, cmdOpenWith, cmdSVNLog, cmdDiff, cmdDiffWithPrevious, cmdDiffLocalWithBase, cmdBlame, cmdSVNUpdate, cmdRollback, menuFileWizard, cmdCommit, cmdRevert, cmdBrowse, cmdEdit, menuClipboard,
				cmdCopyFullName, cmdCopyName, cmdCopyURL, cmdCopyRelativeURL, cmdExport
			});
			uiCommandManager1.ContainerControl = this;
			uiCommandManager1.ContextMenus.AddRange(new[]
			{
				uiContextMenu1
			});
			uiCommandManager1.Id = new Guid("3057650b-f916-4520-b6e8-0eb30e775936");
			uiCommandManager1.LeftRebar = LeftRebar1;
			uiCommandManager1.LockCommandBars = true;
			uiCommandManager1.RightRebar = RightRebar1;
			uiCommandManager1.ShowAddRemoveButton = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandManager1.ShowQuickCustomizeMenu = false;
			uiCommandManager1.TopRebar = TopRebar1;
			uiCommandManager1.VisualStyle = Janus.Windows.UI.VisualStyle.Standard;
			uiContextMenu1.CommandManager = uiCommandManager1;
			uiContextMenu1.Commands.AddRange(new[]
			{
				cmdSVNUpdate2, cmdCommit2, cmdRevert2, cmdRollback1, Separator3, cmdSVNLog1, cmdDiffWithPrevious2, cmdDiffLocalWithBase2, cmdBlame1, Separator1, cmdExplore1, cmdBrowse2, cmdOpen1, cmdOpenWith2, cmdEdit2, cmdExport1,
				menuClipboard1, Separator5, menuFileWizard1
			});
			uiContextMenu1.Key = "ContextMenu1";
			uiContextMenu1.Popup += uiContextMenu1_Popup;
			cmdSVNUpdate2.Key = "cmdSVNUpdate";
			cmdSVNUpdate2.Name = "cmdSVNUpdate2";
			cmdSVNUpdate2.Text = "&Update to this revision";
			cmdCommit2.Key = "cmdCommit";
			cmdCommit2.Name = "cmdCommit2";
			cmdCommit2.Text = "&Commit";
			cmdRevert2.Key = "cmdRevert";
			cmdRevert2.Name = "cmdRevert2";
			cmdRevert2.Text = "Re&vert";
			cmdRollback1.Key = "cmdRollback";
			cmdRollback1.Name = "cmdRollback1";
			cmdRollback1.Text = "&Rollback this revision";
			Separator3.CommandType = CommandType.Separator;
			Separator3.Key = "Separator";
			Separator3.Name = "Separator3";
			cmdSVNLog1.Key = "cmdSVNLog";
			cmdSVNLog1.Name = "cmdSVNLog1";
			cmdSVNLog1.Text = "Show &log";
			cmdDiffWithPrevious2.Key = "cmdDiffWithPrevious";
			cmdDiffWithPrevious2.Name = "cmdDiffWithPrevious2";
			cmdDiffLocalWithBase2.Key = "cmdDiffLocalWithBase";
			cmdDiffLocalWithBase2.Name = "cmdDiffLocalWithBase2";
			cmdBlame1.Key = "cmdBlame";
			cmdBlame1.Name = "cmdBlame1";
			cmdBlame1.Text = "&Blame";
			Separator1.CommandType = CommandType.Separator;
			Separator1.Key = "Separator";
			Separator1.Name = "Separator1";
			cmdExplore1.Key = "cmdExplore";
			cmdExplore1.Name = "cmdExplore1";
			cmdExplore1.Text = "&Explore";
			cmdBrowse2.Key = "cmdBrowse";
			cmdBrowse2.Name = "cmdBrowse2";
			cmdBrowse2.Text = "Bro&wse";
			cmdOpen1.Key = "cmdOpen";
			cmdOpen1.Name = "cmdOpen1";
			cmdOpen1.Text = "&Open";
			cmdOpenWith2.Key = "cmdOpenWith";
			cmdOpenWith2.Name = "cmdOpenWith2";
			cmdOpenWith2.Text = "Open &with...";
			cmdEdit2.Key = "cmdEdit";
			cmdEdit2.Name = "cmdEdit2";
			cmdEdit2.Text = "Edi&t";
			cmdExport1.Key = "cmdExport";
			cmdExport1.Name = "cmdExport1";
			menuClipboard1.Key = "menuClipboard";
			menuClipboard1.Name = "menuClipboard1";
			menuClipboard1.Text = "Cop&y to clipboard";
			Separator5.CommandType = CommandType.Separator;
			Separator5.Key = "Separator";
			Separator5.Name = "Separator5";
			menuFileWizard1.Key = "menuFileWizard";
			menuFileWizard1.Name = "menuFileWizard1";
			menuFileWizard1.Text = "&Monitor this file";
			BottomRebar1.CommandManager = uiCommandManager1;
			BottomRebar1.Dock = DockStyle.Bottom;
			BottomRebar1.Location = new Point(0, 0x152);
			BottomRebar1.Name = "BottomRebar1";
			BottomRebar1.Size = new Size(0x1f9, 0);
			uiCommandBar1.AllowClose = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.AllowCustomize = Janus.Windows.UI.InheritableBoolean.False;
			uiCommandBar1.CommandManager = uiCommandManager1;
			uiCommandBar1.Commands.AddRange(new[]
			{
				cmdSVNUpdate1, cmdCommit1, cmdRevert1, cmdRollback2, Separator4, cmdSVNLog2, cmdDiff2, cmdBlame2, Separator2, cmdExplore2, cmdBrowse1, cmdOpen2, cmdOpenWith3, cmdEdit1, Separator6, menuFileWizard2
			});
			uiCommandBar1.FullRow = true;
			uiCommandBar1.Key = "CommandBar1";
			uiCommandBar1.Location = new Point(0, 0);
			uiCommandBar1.Name = "uiCommandBar1";
			uiCommandBar1.RowIndex = 0;
			uiCommandBar1.Size = new Size(0x39b, 0x1c);
			uiCommandBar1.Text = "CommandBar1";
			cmdSVNUpdate1.Key = "cmdSVNUpdate";
			cmdSVNUpdate1.Name = "cmdSVNUpdate1";
			cmdSVNUpdate1.Text = "Update";
			cmdCommit1.Key = "cmdCommit";
			cmdCommit1.Name = "cmdCommit1";
			cmdRevert1.Key = "cmdRevert";
			cmdRevert1.Name = "cmdRevert1";
			cmdRollback2.Key = "cmdRollback";
			cmdRollback2.Name = "cmdRollback2";
			cmdRollback2.Text = "Rollback";
			cmdRollback2.ToolTipText = "Rollback this revision";
			Separator4.CommandType = CommandType.Separator;
			Separator4.Key = "Separator";
			Separator4.Name = "Separator4";
			cmdSVNLog2.Key = "cmdSVNLog";
			cmdSVNLog2.Name = "cmdSVNLog2";
			cmdDiff2.Key = "cmdDiff";
			cmdDiff2.Name = "cmdDiff2";
			cmdBlame2.Key = "cmdBlame";
			cmdBlame2.Name = "cmdBlame2";
			Separator2.CommandType = CommandType.Separator;
			Separator2.Key = "Separator";
			Separator2.Name = "Separator2";
			cmdExplore2.Key = "cmdExplore";
			cmdExplore2.Name = "cmdExplore2";
			cmdBrowse1.Key = "cmdBrowse";
			cmdBrowse1.Name = "cmdBrowse1";
			cmdOpen2.Key = "cmdOpen";
			cmdOpen2.Name = "cmdOpen2";
			cmdOpenWith3.Key = "cmdOpenWith";
			cmdOpenWith3.Name = "cmdOpenWith3";
			cmdEdit1.Key = "cmdEdit";
			cmdEdit1.Name = "cmdEdit1";
			Separator6.CommandType = CommandType.Separator;
			Separator6.Key = "Separator";
			Separator6.Name = "Separator6";
			menuFileWizard2.Key = "menuFileWizard";
			menuFileWizard2.Name = "menuFileWizard2";
			menuFileWizard2.Text = "Monitor";
			menuFileWizard2.Click += menuFileWizard2_Click;
			cmdOpen.Image = (Image)resources.GetObject("cmdOpen.Image");
			cmdOpen.Key = "cmdOpen";
			cmdOpen.Name = "cmdOpen";
			cmdOpen.Text = "Open";
			cmdOpen.ToolTipText = "Open";
			cmdOpen.Click += cmdOpen_Click;
			cmdOpenWith.Image = (Image)resources.GetObject("cmdOpenWith.Image");
			cmdOpenWith.Key = "cmdOpenWith";
			cmdOpenWith.Name = "cmdOpenWith";
			cmdOpenWith.Text = "Open with...";
			cmdOpenWith.ToolTipText = "Open with...";
			cmdOpenWith.Click += cmdOpenWith_Click;
			cmdSVNLog.Image = (Image)resources.GetObject("cmdSVNLog.Image");
			cmdSVNLog.Key = "cmdSVNLog";
			cmdSVNLog.Name = "cmdSVNLog";
			cmdSVNLog.Text = "Show log";
			cmdSVNLog.ToolTipText = "Show log";
			cmdSVNLog.Click += cmdSVNLog_Click;
			cmdDiff.Commands.AddRange(new[]
			{
				cmdDiffWithPrevious1, cmdDiffLocalWithBase1
			});
			cmdDiff.Image = (Image)resources.GetObject("cmdDiff.Image");
			cmdDiff.Key = "cmdDiff";
			cmdDiff.Name = "cmdDiff";
			cmdDiff.Text = "Diff";
			cmdDiff.ToolTipText = "Diff with previous version";
			cmdDiff.Click += cmdDiff_Click;
			cmdDiffWithPrevious1.Key = "cmdDiffWithPrevious";
			cmdDiffWithPrevious1.Name = "cmdDiffWithPrevious1";
			cmdDiffLocalWithBase1.Key = "cmdDiffLocalWithBase";
			cmdDiffLocalWithBase1.Name = "cmdDiffLocalWithBase1";
			cmdDiffWithPrevious.Image = (Image)resources.GetObject("cmdDiffWithPrevious.Image");
			cmdDiffWithPrevious.Key = "cmdDiffWithPrevious";
			cmdDiffWithPrevious.Name = "cmdDiffWithPrevious";
			cmdDiffWithPrevious.Text = "Diff With &Previous";
			cmdDiffWithPrevious.Click += cmdDiffWithPrevious_Click;
			cmdDiffLocalWithBase.Image = (Image)resources.GetObject("cmdDiffLocalWithBase.Image");
			cmdDiffLocalWithBase.Key = "cmdDiffLocalWithBase";
			cmdDiffLocalWithBase.Name = "cmdDiffLocalWithBase";
			cmdDiffLocalWithBase.Text = "&Diff With Base";
			cmdDiffLocalWithBase.Click += cmdDiffLocalWithBase_Click;
			cmdBlame.Image = (Image)resources.GetObject("cmdBlame.Image");
			cmdBlame.Key = "cmdBlame";
			cmdBlame.Name = "cmdBlame";
			cmdBlame.Text = "Blame";
			cmdBlame.ToolTipText = "Blame";
			cmdBlame.Click += cmdBlame_Click;
			cmdSVNUpdate.Image = (Image)resources.GetObject("cmdSVNUpdate.Image");
			cmdSVNUpdate.Key = "cmdSVNUpdate";
			cmdSVNUpdate.Name = "cmdSVNUpdate";
			cmdSVNUpdate.Text = "Update to this revision";
			cmdSVNUpdate.ToolTipText = "Update to this revision";
			cmdSVNUpdate.Click += cmdSVNUpdate_Click;
			cmdRollback.Image = (Image)resources.GetObject("cmdRollback.Image");
			cmdRollback.Key = "cmdRollback";
			cmdRollback.Name = "cmdRollback";
			cmdRollback.Text = "Rollback this revision";
			cmdRollback.ToolTipText = "Rollback this revision";
			cmdRollback.Click += cmdRollback_Click;
			menuFileWizard.Image = (Image)resources.GetObject("menuFileWizard.Image");
			menuFileWizard.Key = "menuFileWizard";
			menuFileWizard.Name = "menuFileWizard";
			menuFileWizard.Text = "Monitor this file";
			cmdCommit.Image = (Image)resources.GetObject("cmdCommit.Image");
			cmdCommit.Key = "cmdCommit";
			cmdCommit.Name = "cmdCommit";
			cmdCommit.Text = "Commit";
			cmdCommit.ToolTipText = "Commit";
			cmdCommit.Click += cmdCommit_Click;
			cmdRevert.Image = (Image)resources.GetObject("cmdRevert.Image");
			cmdRevert.Key = "cmdRevert";
			cmdRevert.Name = "cmdRevert";
			cmdRevert.Text = "Revert";
			cmdRevert.ToolTipText = "Revert";
			cmdRevert.Click += cmdRevert_Click;
			cmdBrowse.Image = (Image)resources.GetObject("cmdBrowse.Image");
			cmdBrowse.Key = "cmdBrowse";
			cmdBrowse.Name = "cmdBrowse";
			cmdBrowse.Text = "Browse";
			cmdBrowse.ToolTipText = "Browse";
			cmdBrowse.Click += cmdBrowse_Click;
			cmdEdit.Image = (Image)resources.GetObject("cmdEdit.Image");
			cmdEdit.Key = "cmdEdit";
			cmdEdit.Name = "cmdEdit";
			cmdEdit.Text = "Edit";
			cmdEdit.ToolTipText = "Edit";
			cmdEdit.Click += cmdEdit_Click;
			menuClipboard.Commands.AddRange(new[]
			{
				cmdCopyFullName1, cmdCopyName1, cmdCopyRelativeUrl1, cmdCopyURL1
			});
			menuClipboard.Image = (Image)resources.GetObject("menuClipboard.Image");
			menuClipboard.Key = "menuClipboard";
			menuClipboard.Name = "menuClipboard";
			menuClipboard.Text = "Copy to clipboard";
			menuClipboard.Popup += menuClipboard_Popup;
			cmdCopyFullName1.Key = "cmdCopyFullName";
			cmdCopyFullName1.Name = "cmdCopyFullName1";
			cmdCopyName1.Key = "cmdCopyName";
			cmdCopyName1.Name = "cmdCopyName1";
			cmdCopyRelativeUrl1.Key = "cmdCopyRelativeURL";
			cmdCopyRelativeUrl1.Name = "cmdCopyRelativeUrl1";
			cmdCopyURL1.Key = "cmdCopyURL";
			cmdCopyURL1.Name = "cmdCopyURL1";
			cmdCopyFullName.Image = (Image)resources.GetObject("cmdCopyFullName.Image");
			cmdCopyFullName.Key = "cmdCopyFullName";
			cmdCopyFullName.Name = "cmdCopyFullName";
			cmdCopyFullName.Text = "&Full name";
			cmdCopyName.Image = (Image)resources.GetObject("cmdCopyName.Image");
			cmdCopyName.Key = "cmdCopyName";
			cmdCopyName.Name = "cmdCopyName";
			cmdCopyName.Text = "&Name";
			cmdCopyURL.Image = (Image)resources.GetObject("cmdCopyURL.Image");
			cmdCopyURL.Key = "cmdCopyURL";
			cmdCopyURL.Name = "cmdCopyURL";
			cmdCopyURL.Text = "&URL";
			cmdCopyRelativeURL.Image = (Image)resources.GetObject("cmdCopyRelativeURL.Image");
			cmdCopyRelativeURL.Key = "cmdCopyRelativeURL";
			cmdCopyRelativeURL.Name = "cmdCopyRelativeURL";
			cmdCopyRelativeURL.Text = "&Relative URL";
			cmdExport.Image = (Image)resources.GetObject("cmdExport.Image");
			cmdExport.Key = "cmdExport";
			cmdExport.Name = "cmdExport";
			cmdExport.Text = "&Save this revision...";
			cmdExport.Click += cmdExport_Click;
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
			TopRebar1.Size = new Size(0x39b, 0x1c);
			panel1.BackColor = Color.DimGray;
			panel1.Controls.Add(pathsGrid1);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0x1c);
			panel1.Name = "panel1";
			panel1.Padding = new Padding(0, 1, 0, 0);
			panel1.Size = new Size(0x39b, 0x7a);
			panel1.TabIndex = 5;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(panel1);
			base.Controls.Add(TopRebar1);
			base.Name = "PathsPanel";
			base.Size = new Size(0x39b, 150);
			((ISupportInitialize)pathsGrid1).EndInit();
			((ISupportInitialize)uiCommandManager1).EndInit();
			((ISupportInitialize)uiContextMenu1).EndInit();
			((ISupportInitialize)BottomRebar1).EndInit();
			((ISupportInitialize)uiCommandBar1).EndInit();
			((ISupportInitialize)LeftRebar1).EndInit();
			((ISupportInitialize)RightRebar1).EndInit();
			((ISupportInitialize)TopRebar1).EndInit();
			TopRebar1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			base.ResumeLayout(false);
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