using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Janus.Windows.EditControls;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;
using Janus.Windows.UI.Dock;
using Janus.Windows.UI.StatusBar;

using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;
using SVNMonitor.View.Controls;
using SVNMonitor.View.Dialogs;
using SVNMonitor.View.Panels;

namespace SVNMonitor.View
{
	internal partial class MainForm : Form
	{
		private bool endSessionPending;
		private static MainForm formInstance;
		private const int BalloonTipTimeOut = 0xea60;
		private readonly System.Collections.Generic.Dictionary<Janus.Windows.UI.CommandBars.UICommand, string> baseMenuTexts = new System.Collections.Generic.Dictionary<Janus.Windows.UI.CommandBars.UICommand, string>();
		private bool realClose;
		private const int WM_QUERYENDSESSION = 0x11;
		
		public MainForm()
		{
			if (formInstance != null)
			{
				throw new InvalidOperationException("An instance of MainForm already exists.");
			}
			ProcessHelper.LogLoadedAssemblies();
			InitializeComponent();
			if (!base.DesignMode)
			{
				UIHelper.ApplyResources(uiCommandManager1);
				Notifier = new MainNotifier(notifyIcon1);
				if (ApplicationSettingsManager.Settings.StartMinimized)
				{
					base.Opacity = 0.0;
				}
				MonitorSettings.Instance.LoadCaches();
				SetLogGroupByBoxImage();
				formInstance = this;
			}
		}

		private void CheckAllSources()
		{
			SourcesPanel.UpdateAllSources();
		}

		private void checkGroupByBox_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SetGroupByBox();
		}

		private void CheckModifications()
		{
			SourcesPanel.SVNCheckModifications();
		}

		private void CheckSelectedSource()
		{
			SourcesPanel.UpdateSource();
		}

		private void CheckStartMinimized()
		{
			if (ApplicationSettingsManager.Settings.StartMinimized)
			{
				base.WindowState = FormWindowState.Minimized;
			}
		}

		private void cmdAbout_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ShowAbout();
		}

		private void cmdBigCheckModifications_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			CheckModifications();
		}

		private void cmdBigCheckSource_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			CheckSelectedSource();
		}

		private void cmdBigCheckSources_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			CheckAllSources();
		}

		private void cmdBigExplore_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ExploreSelectedSource();
		}

		private void cmdBigOptions_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenOptionsDialog();
		}

		private void cmdBigRevert_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceRevert();
		}

		private void cmdBigSourceCommit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNCommitSelectedSource();
		}

		private void cmdBigUpdate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateSelectedSource();
		}

		private void cmdBigUpdateAll_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateAllSources();
		}

		private void cmdBigUpdateAllAvailable_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateAllAvailableSources();
		}

		private void cmdCheckAllSources_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			UpdateAllSources();
		}

		private void cmdClose_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			RealClose();
		}

		private void cmdEnableUpdates_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			ToggleEnableUpdates();
		}

		private void cmdGenerateError_Click(object sender, CommandEventArgs e)
		{
		}

		private void cmdGenerateInvokeError_Click(object sender, CommandEventArgs e)
		{
		}

		private void cmdNewMonitor_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			NewMonitor();
		}

		private void cmdNewSource_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			NewSource();
		}

		private void cmdOpen_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			RestoreForm();
		}

		private void cmdOptions_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			OpenOptionsDialog();
		}

		private void cmdSVNCheckModifications_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceCheckModifications((Source)e.Command.Tag);
		}

		private void cmdSVNCommit_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceSVNCommit((Source)e.Command.Tag);
		}

		private void cmdSVNRevert_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceSVNRevert((Source)e.Command.Tag);
		}

		private void cmdSVNUpdate_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SourceSVNUpdate((Source)e.Command.Tag);
		}

		private void cmdSVNUpdateAll_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateAllSources();
		}

		private void cmdSVNUpdateAllAvailable_Click(object sender, CommandEventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdateAllAvailableSources();
		}

		private void cmdTestDialogNewVersion_Click(object sender, CommandEventArgs e)
		{
		}

		private void cmdTestNewVersion_Click(object sender, CommandEventArgs e)
		{
		}

		private void cmdTestSetLogEntriesGridLayout_Click(object sender, CommandEventArgs e)
		{
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

		private void CreateSourceCommandsSubMenu(UICommand menu, Predicate<Source> predicate, string keyPrefix, CommandSuffixProvider suffixProvider, CommandImageProvider imageProvider, CommandEventHandler eventHandler)
		{
			if (!baseMenuTexts.ContainsKey(menu))
			{
				baseMenuTexts.Add(menu, menu.Text);
			}
			else
			{
				menu.Text = baseMenuTexts[menu];
			}
			menu.Commands.Clear();
			menu.Click -= eventHandler;
			IEnumerable<Source> matchingSources = Status.EnabledSources.Where(s => predicate(s));
			switch (matchingSources.Count())
			{
				case 0:
					menu.Enabled = Janus.Windows.UI.InheritableBoolean.False;
					return;

				case 1:
				{
					Source source = matchingSources.First();
					string suffix = suffixProvider(source);
					string text = string.Format("{0} \"{1}\" {2}", menu.Text, source.Name, suffix).Trim();
					menu.Text = text;
					menu.Key = Guid.NewGuid().ToString();
					menu.Tag = source;
					menu.Click += eventHandler;
					menu.Enabled = Janus.Windows.UI.InheritableBoolean.True;
					return;
				}
			}
			foreach (Source source in matchingSources)
			{
				UICommand cmd = new UICommand();
				string suffix = suffixProvider(source);
				string text = string.Format("{0} {1}", source.Name, suffix).Trim();
				cmd.Text = text;
				if (imageProvider == null)
				{
					cmd.Image = menu.Image;
				}
				else
				{
					cmd.Image = imageProvider(source);
				}
				cmd.Key = Guid.NewGuid().ToString();
				cmd.Tag = source;
				cmd.Click += eventHandler;
				menu.Commands.Add(cmd);
			}
			menu.Enabled = (menu.Commands.Count > 0).ToInheritableBoolean();
		}

		private void CreateSVNCheckModificationsSubMenu()
		{
			CreateSourceCommandsSubMenu(menuCheckModifications, src => !src.IsURL, "SVNCheckModifications", ModifiedOrUnversionedCountSuffixProvider, CheckModificationsImageProvider, cmdSVNCheckModifications_Click);
		}

		private void CreateSVNCommitSubMenu()
		{
			CreateSourceCommandsSubMenu(menuSVNCommit, delegate(Source src)
			{
				if (!ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled)
				{
					return src.HasLocalChanges;
				}
				return true;
			}, "SVNCommit", ModifiedOrUnversionedCountSuffixProvider, null, cmdSVNCommit_Click);
		}

		private void CreateSVNRevertSubMenu()
		{
			CreateSourceCommandsSubMenu(menuSVNRevert, src => src.HasLocalVersionedChanges, "SVNRevert", ModifiedCountSuffixProvider, null, cmdSVNRevert_Click);
		}

		private void CreateSVNUpdateSubMenu()
		{
			CreateSourceCommandsSubMenu(menuSVNUpdate, src => !src.IsURL, "SVNUpdate", UnreadCountSuffixProvider, SVNUpdateImageProvider, cmdSVNUpdate_Click);
		}

		[Conditional("DEBUG")]
		private void DEBUG_GenerateError()
		{
			throw new Exception("Generated Error");
		}

		[Conditional("DEBUG")]
		private void DEBUG_GenerateInvokeError()
		{
			new Thread(DEBUG_GenerateInvokeException).Start();
		}

		private void DEBUG_GenerateInvokeException()
		{
			base.BeginInvoke(new MethodInvoker(DEBUG_GenerateNullableException));
		}

		private void DEBUG_GenerateNullableException()
		{
			int? number = null;
			int local1 = number.Value;
		}

		[Conditional("DEBUG")]
		private void DEBUG_SetLogEntriesGridLayout()
		{
			LogEntriesPanel.SetGridLayout(ApplicationSettingsManager.Settings.UILogEntriesGridLayout);
		}

		[Conditional("DEBUG")]
		private void DEBUG_ShowDebugCommands()
		{
			menuDebug.Visible = Janus.Windows.UI.InheritableBoolean.True;
		}

		private void DeleteObsoleteFiles()
		{
			string[] files = new[]
			{
				Path.Combine(FileSystemHelper.AppData, "SVNMonitor.state"), Path.Combine(FileSystemHelper.AppData, "SVNMonitor.activity")
			};
			foreach (string file in files)
			{
				if (FileSystemHelper.DeleteFile(file))
				{
					Logger.Log.Debug("Obsolete file deleted: " + file);
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
				bool tempAnotherLocal0;
				bool tempAnotherLocal1;
				bool tempAnotherLocal2;
				bool sourcesExists = MonitorSettings.Instance.Sources.Count > 0;
				bool availableUpdatesExists = Status.NotUpToDateSources.Count() > 0;
				CanSVNUpdateAllAvailable = availableUpdatesExists;
				CanSVNUpdateAll = sourcesExists;
				CanCheckUpdates = tempAnotherLocal0 = true;
				CanRevert = tempAnotherLocal1 = tempAnotherLocal0;
				CanCommit = tempAnotherLocal2 = tempAnotherLocal1;
				CanCheckModifications = CanUpdate = tempAnotherLocal2;
				CanBigCheckModifications = SourcesPanel.CanSVNCheckForModifications;
				CanBigCheckSource = SourcesPanel.CanCheckForUpdates;
				CanBigCheckSources = sourcesExists;
				CanBigExplore = SourcesPanel.CanExplore;
				CanBigOptions = true;
				CanBigUpdate = SourcesPanel.CanSVNUpdate;
				CanBigCommit = SourcesPanel.CanSVNCommit;
				CanBigRevert = SourcesPanel.CanSVNRevert;
				CanBigUpdateAll = sourcesExists;
				CanBigUpdateAllAvailable = availableUpdatesExists;
			}
		}

		private void ExploreSelectedSource()
		{
			SourcesPanel.Browse();
		}

		public void FocusEventLog()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(FocusEventLog));
			}
			else
			{
				base.BringToFront();
				base.Activate();
				uiPanelEventLog.AutoHideActive = true;
				uiPanelEventLog.Focus();
			}
		}

		public void FocusEventLog(long eventID)
		{
			FocusEventLog();
			SVNMonitor.EventLog.Open(eventID);
		}

		private new void Hide()
		{
			List<Form> relevantForms = new List<Form>();
			foreach (Form form in Application.OpenForms)
			{
				if (!form.GetType().Assembly.FullName.StartsWith("Janus."))
				{
					relevantForms.Add(form);
				}
			}
			if (relevantForms.Count > 1)
			{
				Logger.Log.Info("Trying to hide the main window, but there are more open windows.");
				try
				{
					for (int i = 1; i < relevantForms.Count; i++)
					{
						Form otherForm = relevantForms[i];
						Logger.Log.InfoFormat("Window: {0} ({1})", otherForm.Text, otherForm.GetType());
						if (i == (relevantForms.Count - 1))
						{
							otherForm.Activate();
						}
						WindowFlasher.Flash(otherForm);
					}
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Error trying to flash the window.", ex);
				}
			}
			else
			{
				foreach (Form form in Application.OpenForms)
				{
					try
					{
						form.Hide();
					}
					catch (Exception ex)
					{
						Logger.Log.Error(string.Format("Error trying to hide a window: {0} ({1})", form.Text, form.GetType()), ex);
					}
				}
				base.Hide();
			}
		}


		private void InstallKeyHooks()
		{
			KeyboardHookHelper.Install();
		}

		private void LoadUISettings()
		{
			Logger.Log.Info("Reading Application State for MainForm...");
			checkGroupByBox.Checked = ApplicationSettingsManager.Settings.LogGroupByBox;
			base.Height = ApplicationSettingsManager.Settings.UIMainFormHeight;
			base.Location = new Point(ApplicationSettingsManager.Settings.UIMainFormLocationX, ApplicationSettingsManager.Settings.UIMainFormLocationY);
			base.Width = ApplicationSettingsManager.Settings.UIMainFormWidth;
			base.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), ApplicationSettingsManager.Settings.UIMainFormWindowState);
			uiPanelEventLog.AutoHide = ApplicationSettingsManager.Settings.UIPanelEventLogAutoHide;
			uiPanelEventLog.Height = ApplicationSettingsManager.Settings.UIPanelEventLogHeight;
			uiPanelLeft.Width = ApplicationSettingsManager.Settings.UIPanelLeftWidth;
			uiPanelMonitors.AutoHide = ApplicationSettingsManager.Settings.UIPanelMonitiorsAutoHide;
			uiPanelMonitors.Height = ApplicationSettingsManager.Settings.UIPanelMonitiorsHeight;
			uiPanelPaths.AutoHide = ApplicationSettingsManager.Settings.UIPanelPathsAutoHide;
			uiPanelPaths.Height = ApplicationSettingsManager.Settings.UIPanelPathsHeight;
			uiPanelSources.AutoHide = ApplicationSettingsManager.Settings.UIPanelSourcesAutoHide;
			Logger.Log.Info("Done reading Application State for MainForm...");
		}

		private void logEntriesPanel1_SelectionChanged(object sender, EventArgs e)
		{
			SetLogEntryName();
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			bool log = true;
			switch (e.KeyCode)
			{
				case Keys.F3:
					Search();
					break;

				case Keys.F5:
					UpdateSource();
					break;

				case Keys.F8:
					FocusEventLog();
					break;

				case Keys.F9:
					UpdateAllSources();
					break;

				case Keys.F:
					if (e.Control)
					{
						Search();
					}
					break;

				default:
					log = false;
					break;
			}
			if (log)
			{
				Logger.LogUserAction("key=" + e.KeyCode);
			}
		}

		private void MainForm_SizeChanged(object sender, EventArgs e)
		{
			base.SizeChanged -= MainForm_SizeChanged;
			SetGridLayouts();
		}

		private void MergeCommands()
		{
			foreach (UICommand cmd in SourcesPanel.UIContextMenu.Commands)
			{
				menuSource.Commands.Add(cmd);
			}
			foreach (UICommand cmd in MonitorsPanel.UIContextMenu.Commands)
			{
				menuMonitor.Commands.Add(cmd);
			}
			foreach (UICommand cmd in LogEntriesPanel.UIContextMenu.Commands)
			{
				menuLog.Commands.Add(cmd);
			}
			foreach (UICommand cmd in PathsPanel.UIContextMenu.Commands)
			{
				menuItem.Commands.Add(cmd);
			}
			foreach (UICommand cmd in EventLogPanel.UIContextMenu.Commands)
			{
				menuEventLog.Commands.Add(cmd);
			}
		}

		private void NewMonitor()
		{
			MonitorsPanel.CreateNew();
		}

		private void NewSource()
		{
			SourcesPanel.CreateNew();
		}

		private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
		{
			Logger.LogUserAction();
			if (e.Button == MouseButtons.Right)
			{
				try
				{
					uiContextMenu1.Show();
				}
				catch (InvalidOperationException)
				{
				}
				catch (Win32Exception)
				{
				}
			}
			else if (e.Button == MouseButtons.Left)
			{
				ShowOrHideForm();
			}
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Logger.LogUserAction();
			RestoreForm();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (endSessionPending)
			{
				e.Cancel = false;
				SVNMonitor.EventLog.LogInfo(Strings.WindowsIsShuttingDown, this);
			}
			else
			{
				if (realClose || !ApplicationSettingsManager.Settings.MinimizeWhenClosing)
				{
					SVNMonitor.EventLog.LogSystem(Strings.ApplicationClosing, this);
					Logger.Log.Debug("Saving UI Settings");
					SaveUISettings();
					Logger.Log.Debug("Hiding the tray icon.");
					Notifier.HideTrayIcon();
					Logger.Log.Debug("Hiding the main window.");
					Hide();
					Logger.Log.Debug("Waiting for the updater to finish.");
					WaitForUpdaterToFinish();
					e.Cancel = false;
				}
				else
				{
					e.Cancel = true;
					base.WindowState = FormWindowState.Minimized;
				}
				base.OnClosing(e);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.DesignMode)
			{
				RegisterEvents();
				MergeCommands();
				Notifier.SetTrayIcon();
				StartupNotification();
				DeleteObsoleteFiles();
				LoadUISettings();
				CheckStartMinimized();
				Updater.Instance.UpdatesGrid = UpdatesGrid;
				ConditionSerializer.Grid = UpdatesGrid;
				SetPanelsEntities();
				RegisterMonitorSettingsEvents();
				Updater.Start();
				InstallKeyHooks();
				base.SizeChanged += MainForm_SizeChanged;
			}
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (!base.DesignMode)
			{
				if (base.WindowState == FormWindowState.Minimized)
				{
					if (ApplicationSettingsManager.Settings.HideWhenMinimized)
					{
						Notifier.SetTrayIconVisible(true);
						Hide();
					}
				}
				else
				{
					base.Opacity = 100.0;
					lastWindowState = base.WindowState;
				}
			}
		}

		private void OpenOptionsDialog()
		{
			SettingsDialog.ShowInstanceDialog();
		}

		private void PopupTrayContextMenu()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(PopupTrayContextMenu));
			}
			else
			{
				ReadSettings();
				if (!Updater.Instance.Enabled)
				{
					bool tempAnotherLocal0;
					bool tempAnotherLocal1;
					bool tempAnotherLocal2;
					bool tempAnotherLocal3;
					bool tempAnotherLocal4;
					CanCheckUpdates = tempAnotherLocal0 = false;
					CanRevert = tempAnotherLocal1 = tempAnotherLocal0;
					CanCommit = tempAnotherLocal2 = tempAnotherLocal1;
					CanUpdate = tempAnotherLocal3 = tempAnotherLocal2;
					CanCheckModifications = tempAnotherLocal4 = tempAnotherLocal3;
					CanSVNUpdateAll = CanSVNUpdateAllAvailable = tempAnotherLocal4;
				}
				else
				{
					CreateSVNCheckModificationsSubMenu();
					CreateSVNCommitSubMenu();
					CreateSVNRevertSubMenu();
					CreateSVNUpdateSubMenu();
					EnableCommands();
				}
			}
		}

		private void ReadSettings()
		{
			cmdEnableUpdates.Checked = ApplicationSettingsManager.Settings.EnableUpdates.ToInheritableBoolean();
		}

		public void RealClose()
		{
			realClose = true;
			base.Close();
		}

		private void RegisterEvents()
		{
			Status.StatusChanged += Status_StatusChanged;
			ApplicationSettingsManager.SavedSettings += (s, ea) => InstallKeyHooks();
		}

		private void RegisterMonitorSettingsEvents()
		{
			MonitorSettings settings = MonitorSettings.Instance;
			settings.SourcesChanged += (s, ea) => SetPanelsEntities();
			settings.MonitorsChanged += (s, ea) => SetPanelsEntities();
		}

		public void RestoreForm()
		{
			if (Form.ActiveForm != this)
			{
				base.Show();
				base.WindowState = lastWindowState;
				base.Activate();
			}
		}

		private void SaveUISettings()
		{
			Logger.Log.Info("Writing Application State for MainForm...");
			ApplicationSettingsManager.Settings.UIMainFormHeight = base.Height;
			ApplicationSettingsManager.Settings.UIMainFormLocationX = base.Location.X;
			ApplicationSettingsManager.Settings.UIMainFormLocationY = base.Location.Y;
			ApplicationSettingsManager.Settings.UIMainFormWidth = base.Width;
			ApplicationSettingsManager.Settings.UIMainFormWindowState = base.WindowState.ToString();
			ApplicationSettingsManager.Settings.UIPanelEventLogAutoHide = uiPanelEventLog.AutoHide;
			ApplicationSettingsManager.Settings.UIPanelEventLogHeight = uiPanelEventLog.Height;
			ApplicationSettingsManager.Settings.UIPanelLeftWidth = uiPanelLeft.Width;
			ApplicationSettingsManager.Settings.UIPanelMonitiorsAutoHide = uiPanelMonitors.AutoHide;
			ApplicationSettingsManager.Settings.UIPanelMonitiorsHeight = uiPanelMonitors.Height;
			ApplicationSettingsManager.Settings.UIPanelPathsAutoHide = uiPanelPaths.AutoHide;
			ApplicationSettingsManager.Settings.UIPanelPathsHeight = uiPanelPaths.Height;
			ApplicationSettingsManager.Settings.UIPanelSourcesAutoHide = uiPanelSources.AutoHide;
			ApplicationSettingsManager.Settings.UILogEntriesGridLayout = LogEntriesPanel.GetGridLayout();
			ApplicationSettingsManager.Settings.UIPathsGridLayout = PathsPanel.GetGridLayout();
			ApplicationSettingsManager.Settings.UIEventLogGridLayout = EventLogPanel.GetGridLayout();
			ApplicationSettingsManager.SaveSettings();
			Logger.Log.Info("Done writing Application State for MainForm.");
		}

		private void Search()
		{
			svnLogEntrySearchTextBox1.Focus();
		}

		public void SelectSource(Source source)
		{
			formInstance.SourcesPanel.SelectSource(source);
		}

		private void SetAnimation()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(SetAnimation));
			}
			else
			{
				AnimationProgressBar.Visible = Status.Updating;
				AnimationProgressBar.Text = Strings.StatusAnimationTitle_FORMAT.FormatWith(new object[]
				{
					Status.UpdatingSourcesString
				});
			}
		}

		private void SetGridLayouts()
		{
			LogEntriesPanel.SetGridLayout();
			PathsPanel.SetGridLayout();
			EventLogPanel.SetGridLayout();
		}

		private void SetGroupByBox()
		{
			ApplicationSettingsManager.Settings.LogGroupByBox = checkGroupByBox.Checked;
			ApplicationSettingsManager.SaveSettings();
			SetLogGroupByBoxImage();
		}

		private void SetLogEntryName()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(SetLogEntryName));
			}
			else
			{
				SVNLogEntry logEntry = LogEntriesPanel.SelectedItem;
				if (logEntry == null)
				{
					uiPanelPaths.ResetText();
				}
				else
				{
					uiPanelPaths.Text = string.Format("{0} {1} [{2}/{3}]", new object[]
					{
						Strings.LogItemsPanelTitle, logEntry.SourceName, logEntry.Revision, logEntry.Author
					});
				}
				uiPanelPathsCaptionContainer.Invalidate();
			}
		}

		private void SetLogGroupByBoxImage()
		{
			checkGroupByBox.Image = checkGroupByBox.Checked ? Images.elements1 : Images.elements1_unchecked;
		}

		private void SetLogPanelText()
		{
			Source source = SourcesPanel.SelectedItem;
			if (source == null)
			{
				uiPanelLog.Text = Strings.PanelCaptionLog;
			}
			else
			{
				uiPanelLog.Text = Strings.PanelCaptionLogAndName_FORMAT.FormatWith(new object[]
				{
					source.Name
				});
			}
			uiPanelLog.Refresh();
		}

		private void SetPanelsEntities()
		{
			MonitorSettings settings = MonitorSettings.Instance;
			SourcesPanel.Entities = settings.Sources;
			MonitorsPanel.Entities = settings.Monitors;
		}

		private void ShowAbout()
		{
			AboutDialog.ShowStaticDialog(this);
		}

		public DialogResult ShowErrorMessage(string text, string caption)
		{
			return ShowMessage(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		public static void ShowInstance()
		{
			if (formInstance != null)
			{
				formInstance.Show();
				formInstance.BringToFront();
				formInstance.WindowState = FormWindowState.Maximized;
			}
		}

		public static void ShowInstance(Source source)
		{
			ShowInstance();
			FormInstance.SelectSource(source);
		}

		public DialogResult ShowMessage(string text, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
		{
			if (base.InvokeRequired)
			{
				return (DialogResult)base.Invoke(new ShowMessageInvoker(ShowMessage), new object[]
				{
					text, caption, messageBoxButtons, messageBoxIcon
				});
			}
			return MessageBox.Show(this, text, caption, messageBoxButtons, messageBoxIcon);
		}

		internal void ShowOrHideForm()
		{
			if (base.Visible)
			{
				Hide();
			}
			else
			{
				RestoreForm();
			}
		}

		private static void Shutdown()
		{
			Logger.Log.Info("Finished.");
			Application.Exit();
		}

		private void SourceCheckModifications(Source source)
		{
			source.CheckModifications();
		}

		private void SourceRevert()
		{
			SourcesPanel.SVNRevert();
		}

		private void sourcesPanel1_SelectionChanged(object sender, EventArgs e)
		{
			SetLogPanelText();
			EnableCommands();
		}

		private void SourceSVNCommit(Source source)
		{
			source.SVNCommit();
		}

		private void SourceSVNRevert(Source source)
		{
			source.SVNRevert();
		}

		private void SourceSVNUpdate(Source source)
		{
			DialogResult result = UpdateHeadPromptDialog.Prompt();
			Logger.Log.InfoFormat("Update Source: User clicked {0}", result);
			if (result == DialogResult.Yes)
			{
				source.SVNUpdate();
			}
		}

		private void StartupNotification()
		{
			try
			{
				if (ShowFirstRunNotification)
				{
					Notifier.ShowBalloonTip(0xea60, Strings.SVNMonitorCaption, Strings.HereItIs, ToolTipIcon.Info);
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message, ex);
			}
		}

		private void Status_StatusChanged(object sender, EventArgs e)
		{
			Notifier.SetTrayIcon();
			EnableCommands();
			SetAnimation();
		}

		private void SVNCommitSelectedSource()
		{
			SourcesPanel.SVNCommit();
		}

		private void SVNUpdateAllAvailableSources()
		{
			SVNUpdateAllSources(true);
		}

		private void SVNUpdateAllSources()
		{
			SVNUpdateAllSources(false);
		}

		private void SVNUpdateAllSources(bool ignoreUpToDate)
		{
			Source.SVNUpdateAll(ignoreUpToDate);
		}

		private void SVNUpdateSelectedSource()
		{
			SourcesPanel.SVNUpdate();
		}

		[Conditional("DEBUG")]
		private void TestNewVersionUpgrade(string version, string upgradeFile)
		{
		}

		private void ToggleEnableUpdates()
		{
			ApplicationSettingsManager.Settings.EnableUpdates = cmdEnableUpdates.Checked == Janus.Windows.UI.InheritableBoolean.True;
			ApplicationSettingsManager.SaveSettings();
		}

		private static void TriggerShutdownTimeoutTimer()
		{
			System.Timers.Timer timer = new System.Timers.Timer();
			Logger.Log.Info("Waiting 20 seconds for current SVN activity to end before aborting...");
			timer.Interval = 20000.0;
			timer.AutoReset = false;
			timer.Elapsed += delegate
			{
				SVNMonitor.Helpers.ThreadHelper.SetThreadName("KILL");
				Logger.Log.Info("20 seconds elapsed. Aborting!");
				Shutdown();
			};
			timer.Start();
		}

		private void uiContextMenu1_Popup(object sender, EventArgs e)
		{
			PopupTrayContextMenu();
		}

		private void UpdateAllSources()
		{
			Updater.Instance.QueueUpdates();
		}

		private void UpdateSource()
		{
			Source source = SourcesPanel.SelectedItem;
			if (SourceHelper.CanCheckForUpdates(source))
			{
				Updater.Instance.QueueUpdate(source, true);
			}
		}

		private static void WaitForUpdaterToFinish()
		{
			if (!Status.Updating)
			{
				Application.Exit();
			}
			else
			{
				Logger.Log.Info("Waiting for the updater to finish...");
				Status.SetClosing(delegate
				{
					Status.SetClosing(null);
					Shutdown();
				});
				TriggerShutdownTimeoutTimer();
			}
		}

		[DebuggerNonUserCode]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x11)
			{
				endSessionPending = true;
			}
			base.WndProc(ref m);
		}

		private SVNMonitor.View.Controls.AnimationProgressBar AnimationProgressBar
		{
			get { return animationProgressBar1; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigCheckModifications
		{
			[DebuggerNonUserCode]
			get { return (cmdBigCheckModifications.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigCheckModifications.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigCheckSource
		{
			[DebuggerNonUserCode]
			get { return (cmdBigCheckSource.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigCheckSource.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigCheckSources
		{
			[DebuggerNonUserCode]
			get { return (cmdBigCheckSources.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigCheckSources.Enabled = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigCommit
		{
			[DebuggerNonUserCode]
			get { return (cmdBigSourceCommit.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigSourceCommit.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigExplore
		{
			[DebuggerNonUserCode]
			get { return (cmdBigExplore.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigExplore.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigOptions
		{
			[DebuggerNonUserCode]
			get { return (cmdBigOptions.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigOptions.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigRevert
		{
			[DebuggerNonUserCode]
			get { return (cmdBigRevert.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigRevert.Enabled = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigUpdate
		{
			[DebuggerNonUserCode]
			get { return (cmdBigUpdate.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigUpdate.Enabled = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanBigUpdateAll
		{
			[DebuggerNonUserCode]
			get { return (cmdBigUpdateAll.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigUpdateAll.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanBigUpdateAllAvailable
		{
			[DebuggerNonUserCode]
			get { return (cmdBigUpdateAllAvailable.Enabled == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdBigUpdateAllAvailable.Enabled = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanCheckModifications
		{
			[DebuggerNonUserCode]
			get { return (menuCheckModifications.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { menuCheckModifications.Visible = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanCheckUpdates
		{
			[DebuggerNonUserCode]
			get { return (cmdCheckAllSources.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdCheckAllSources.Visible = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanCommit
		{
			[DebuggerNonUserCode]
			get { return (menuSVNCommit.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { menuSVNCommit.Visible = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanRevert
		{
			[DebuggerNonUserCode]
			get { return (menuSVNRevert.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { menuSVNRevert.Visible = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanSVNUpdateAll
		{
			[DebuggerNonUserCode]
			get { return (cmdSVNUpdateAll.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdSVNUpdateAll.Visible = value.ToInheritableBoolean(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		private bool CanSVNUpdateAllAvailable
		{
			[DebuggerNonUserCode]
			get { return (cmdSVNUpdateAllAvailable.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { cmdSVNUpdateAllAvailable.Visible = value.ToInheritableBoolean(); }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool CanUpdate
		{
			[DebuggerNonUserCode]
			get { return (menuSVNUpdate.Visible == Janus.Windows.UI.InheritableBoolean.True); }
			[DebuggerNonUserCode]
			set { menuSVNUpdate.Visible = value.ToInheritableBoolean(); }
		}

		[Browsable(false)]
		private CommandImageProvider CheckModificationsImageProvider
		{
			get
			{
				return delegate(Source source)
				{
					if (source.ModifiedCount > 0)
					{
						return Images.svn_modifications_available;
					}
					return Images.svn_modifications;
				};
			}
		}

		private SVNMonitor.View.Panels.EventLogPanel EventLogPanel
		{
			get { return eventLogPanel1; }
		}

		public static MainForm FormInstance
		{
			[DebuggerNonUserCode]
			get { return formInstance; }
		}

		private SVNMonitor.View.Panels.LogEntriesPanel LogEntriesPanel
		{
			get { return logEntriesPanel1; }
		}

		[Browsable(false)]
		private CommandSuffixProvider ModifiedCountSuffixProvider
		{
			get
			{
				return delegate(Source source)
				{
					if (source.ModifiedCount == 0)
					{
						return string.Empty;
					}
					return string.Format("({0})", source.ModifiedCountString);
				};
			}
		}

		[Browsable(false)]
		private CommandSuffixProvider ModifiedOrUnversionedCountSuffixProvider
		{
			get
			{
				return delegate(Source source)
				{
					int count = source.ModifiedCount;
					int unversionedCount = source.UnversionedCount;
					if (count == 0)
					{
						if (unversionedCount > 0)
						{
							return string.Format("({0})", source.UnversionedCountString);
						}
						return string.Empty;
					}
					string unversionedString = string.Empty;
					if (unversionedCount > 0)
					{
						unversionedString = string.Format(" ({0})", source.UnversionedCountString);
					}
					string conflictsString = string.Empty;
					if (source.PossibleConflictedFilePathsCount > 0)
					{
						conflictsString = string.Format(" ({0})", source.PossibleConflictedFilePathsCountString);
					}
					return string.Format("({0}{1}{2})", source.ModifiedCountString, unversionedString, conflictsString);
				};
			}
		}

		private SVNMonitor.View.Panels.MonitorsPanel MonitorsPanel
		{
			get { return monitorsPanel1; }
		}

		private MainNotifier Notifier { get; set; }

		private SVNMonitor.View.Panels.PathsPanel PathsPanel
		{
			get { return pathsPanel1; }
		}

		internal bool ShowFirstRunNotification { get; set; }

		private SVNMonitor.View.Panels.SourcesPanel SourcesPanel
		{
			[DebuggerNonUserCode]
			get { return sourcesPanel1; }
		}

		[Browsable(false)]
		private CommandImageProvider SVNUpdateImageProvider
		{
			get
			{
				return delegate(Source source)
				{
					if (source.IsUpToDate)
					{
						return Images.svn_update;
					}
					if (source.ReadOnlyRecommendedRevisions.Count > 0)
					{
						return Images.recommend_down;
					}
					return Images.svn_update_available;
				};
			}
		}

		[Browsable(false)]
		private CommandSuffixProvider UnreadCountSuffixProvider
		{
			get
			{
				return delegate(Source source)
				{
					if (source.UnreadLogEntriesCount == 0)
					{
						return string.Empty;
					}
					return string.Format("({0})", source.UnreadCountString);
				};
			}
		}

		public Janus.Windows.GridEX.GridEX UpdatesGrid
		{
			[DebuggerNonUserCode]
			get { return updatesGridContainer1.Grid; }
		}

		private delegate Image CommandImageProvider(Source source);

		private delegate string CommandSuffixProvider(Source source);

		private delegate DialogResult ShowMessageInvoker(string text, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon);
	}
}
