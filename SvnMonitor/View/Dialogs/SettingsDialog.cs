using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Janus.Windows.UI.Tab;

using Microsoft.Win32;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;
using SVNMonitor.View.Controls;

namespace SVNMonitor.View.Dialogs
{
	internal partial class SettingsDialog : BaseDialog
	{
		private static SettingsDialog instance;
		private const string RegistryRunKeyName = @"Software\Microsoft\Windows\CurrentVersion\Run";
		private const string RegistryRunValueName = "SVNMonitor";
		

		public SettingsDialog()
		{
			InitializeComponent();
		}

		private void AutoCheckTortoiseSVN()
		{
			Logger.LogUserAction();
			DialogResult result = MessageBox.Show(this, Strings.AskCheckRegistryForInstalledFile, Strings.SVNMonitorCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			Logger.Log.InfoFormat("Check registry: User clicked {0}", result);
			if (result != DialogResult.No)
			{
				string file = EnvironmentHelper.ExpandEnvironmentVariables(TortoiseSVNHelper.GetTortoiseSVNProcPath());
				if (FileSystemHelper.FileExists(file))
				{
					Logger.Log.InfoFormat("Found TortoiseSVN at {0}", file);
					MessageBox.Show(this, Strings.FoundFileAt_FORMAT.FormatWith(new object[]
					{
						file
					}), Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					txtTortoiseEXE.Text = file;
				}
				else
				{
					Logger.Log.Info("Can't find TortoiseSVN.");
					MessageBox.Show(this, Strings.AskCantFindTortoiseSVN, Strings.SVNMonitorCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
				}
			}
		}

		internal static string BrowseSvnExe(string initialDirectory)
		{
			OpenFileDialog tempLocal0 = new OpenFileDialog
			{
				CheckFileExists = true,
				DefaultExt = ".exe",
				Filter = "svn.exe|svn.exe",
				InitialDirectory = initialDirectory
			};
			OpenFileDialog dialog = tempLocal0;
			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return null;
			}
			return dialog.FileName;
		}

		private void BrowseTextEditor()
		{
			OpenFileDialog tempLocal2 = new OpenFileDialog
			{
				CheckFileExists = true,
				DefaultExt = ".exe",
				Filter = string.Format("{0}(*.EXE;*.COM;*.BAT;*.CMD)|*.EXE;*.COM;*.BAT;*.CMD", Strings.ExecutablesFileTypes)
			};
			OpenFileDialog dialog = tempLocal2;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtTextEditor.Text = dialog.FileName;
			}
		}

		private void BrowseTortoiseSvnExe()
		{
			OpenFileDialog tempLocal1 = new OpenFileDialog
			{
				CheckFileExists = true,
				DefaultExt = ".exe",
				Filter = "TortoiseProc.exe|TortoiseProc.exe",
				InitialDirectory = txtTortoiseEXE.Text
			};
			OpenFileDialog dialog = tempLocal1;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtTortoiseEXE.Text = dialog.FileName;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			WriteSettings();
		}

		private void checkAutomaticallyResolveTortoiseSVNProcess_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			linkAutoCheckTortoiseSVN.Visible = txtTortoiseEXE.Enabled = linkTortoiseEXEBrowse.Visible = !checkAutomaticallyResolveTortoiseSVNProcess.Checked;
		}

		private void checkAutomaticInterval_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			RefreshAutomaticUpdatesInterval();
		}

		private void checkEnableVersionCheck_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			groupVersionChecks.Enabled = checkEnableVersionCheck.Checked;
		}

		private void checkSVNUpdateSourcesParallel_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			numSVNUpdateSourcesQueueTimeoutSeconds.Enabled = !checkSVNUpdateSourcesParallel.Checked;
		}

		private void checkUnlimitedPageSize_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			numPageSize.Enabled = !checkUnlimitedPageSize.Checked;
		}

		private void cmdTortoiseSVNSettings_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			TortoiseProcess.Settings();
		}

		private IEnumerable<KeyboardEditorRow> CreateKeyboardDataSource()
		{
			return typeof(ApplicationSettings).GetProperties().Where(p => Attribute.IsDefined(p, typeof(KeyboardSettingAttribute))).Select(p => new KeyboardEditorRow
			{
				AssociatedSetting = p.Name,
				Description = Strings.ResourceManager.GetString(p.Name + "_Description"),
				Image = (Image)Images.ResourceManager.GetObject(p.Name + "_Image"),
				KeyInfo = KeyInfo.GetKeyInfo(ApplicationSettingsManager.Settings.GetValue<string>(p.Name)),
				Text = Strings.ResourceManager.GetString(p.Name + "_Text")
			});
		}

		private static bool IsRegistryStartWithWindows()
		{
			using (RegistryKey key = RegistryHelper.OpenSubKey(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Run", false))
			{
				if (key == null)
				{
					return false;
				}
				string registryValue = (string)key.GetValue("SVNMonitor");
				string currentExePath = Application.ExecutablePath;
				try
				{
					currentExePath = FileSystemHelper.GetFullPath(currentExePath);
				}
				catch (PathTooLongException ex)
				{
					Logger.Log.Error(string.Format("{0} is too long.", currentExePath), ex);
				}
				return currentExePath.Equals(registryValue, StringComparison.OrdinalIgnoreCase);
			}
		}

		private void linkAutoCheckTortoiseSVN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			AutoCheckTortoiseSVN();
		}

		private void linkTextEditorBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			BrowseTextEditor();
		}

		private void linkTortoiseEXEBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			BrowseTortoiseSvnExe();
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			instance = null;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			SetDefaultUserActionCombo();
			SetTortoiseSVNCombo();
			ReadSettings();
			uiTabs.ShowTabs = false;
			treeView1.SelectedNode = treeView1.Nodes[0];
			treeView1.Select();
		}

		private void ReadKeyboardSettings()
		{
			keyboardEditor1.List = CreateKeyboardDataSource();
		}

		private void ReadPageSize()
		{
			int size = ApplicationSettingsManager.Settings.LogEntriesPageSize;
			if (size < 0)
			{
				checkUnlimitedPageSize.Checked = true;
				numPageSize.Value = 100M;
				numPageSize.Enabled = false;
			}
			else
			{
				checkUnlimitedPageSize.Checked = false;
				numPageSize.Value = size;
			}
		}

		private void ReadSettings()
		{
			checkStartWithWindows.Checked = TryIsRegistryStartWithWindows();
			checkHideWhenMinimized.Checked = ApplicationSettingsManager.Settings.HideWhenMinimized;
			checkStartMinimized.Checked = ApplicationSettingsManager.Settings.StartMinimized;
			checkMinimizeWhenClosing.Checked = ApplicationSettingsManager.Settings.MinimizeWhenClosing;
			ReadPageSize();
			numPreviewLines.Value = ApplicationSettingsManager.Settings.PreviewRowLines;
			checkShowDefaultTextInsteadOfEmptyLogMessage.Checked = ApplicationSettingsManager.Settings.ShowDefaultTextInsteadOfEmptyLogMessage;
			checkSourcesPanelShowPath.Checked = ApplicationSettingsManager.Settings.SourcesPanelShowPath;
			checkSourcesPanelShowUrl.Checked = ApplicationSettingsManager.Settings.SourcesPanelShowUrl;
			checkSourcesPanelShowLastCheck.Checked = ApplicationSettingsManager.Settings.SourcesPanelShowLastCheck;
			checkSourcesPanelShowNoUpdates.Checked = ApplicationSettingsManager.Settings.SourcesPanelShowNoUpdates;
			comboDefaultUserAction.SelectedValue = Enum.Parse(typeof(UserAction), ApplicationSettingsManager.Settings.DefaultPathAction);
			txtTextEditor.Text = EnvironmentHelper.ExpandEnvironmentVariables(ApplicationSettingsManager.Settings.FileEditor);
			checkPromptUpdateOldRevision.Checked = ApplicationSettingsManager.Settings.PromptRollbackOldRevision;
			checkPromptUpdateHeadRevision.Checked = ApplicationSettingsManager.Settings.PromptUpdateHeadRevision;
			checkDismissErrorsWhenClicked.Checked = ApplicationSettingsManager.Settings.DismissErrorsWhenClicked;
			checkCommitIsAlwaysEnabled.Checked = ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled;
			ReadKeyboardSettings();
			checkEnableUpdates.Checked = ApplicationSettingsManager.Settings.EnableUpdates;
			checkAutomaticInterval.Checked = ApplicationSettingsManager.Settings.UpdatesInterval <= 0;
			checkTreatUnversionedAsModified.Checked = ApplicationSettingsManager.Settings.TreatUnversionedAsModified;
			checkIgnoreIgnoreOnCommit.Checked = ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommit;
			checkIgnoreIgnoreOnCommitConflicts.Checked = ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommitConflicts;
			checkWatchTheNetworkAvailability.Checked = ApplicationSettingsManager.Settings.WatchTheNetworkAvailability;
			numUpdatesInterval.Value = ApplicationSettingsManager.Settings.UpdatesInterval;
			numUpdatesInterval.Enabled = !checkAutomaticInterval.Checked;
			numSecondsPerSource.Value = ApplicationSettingsManager.Settings.UpdatesIntervalPerSource;
			numSecondsPerSource.Enabled = checkAutomaticInterval.Checked;
			txtTortoiseEXE.Text = EnvironmentHelper.ExpandEnvironmentVariables(ApplicationSettingsManager.Settings.TortoiseSVNPath);
			comboAutoCloseTortoiseSVN.SelectedValue = (TortoiseSVNAutoClose)ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
			checkIgnoreDisabledSourcesWhenUpdatingAll.Checked = ApplicationSettingsManager.Settings.IgnoreDisabledSourcesWhenUpdatingAll;
			checkAutomaticallyResolveTortoiseSVNProcess.Checked = ApplicationSettingsManager.Settings.AutomaticallyResolveTortoiseSVNProcess;
			linkAutoCheckTortoiseSVN.Visible = txtTortoiseEXE.Enabled = linkTortoiseEXEBrowse.Visible = !checkAutomaticallyResolveTortoiseSVNProcess.Checked;
			checkSVNUpdateSourcesParallel.Checked = ApplicationSettingsManager.Settings.SVNUpdateSourcesParallel;
			numSVNUpdateSourcesQueueTimeoutSeconds.Value = ApplicationSettingsManager.Settings.SVNUpdateSourcesQueueTimeoutSeconds;
			numSVNUpdateSourcesQueueTimeoutSeconds.Enabled = !checkSVNUpdateSourcesParallel.Checked;
			txtRecommendMessage.Text = ApplicationSettingsManager.Settings.RecommendationMessage;
			checkEnableVersionCheck.Checked = ApplicationSettingsManager.Settings.EnableVersionCheck;
			checkEnableVersionUpgrade.Checked = ApplicationSettingsManager.Settings.EnableVersionUpgrade;
			checkEnableVersionUpgrade.Enabled = ProcessHelper.IsRunningAsAdministrator();
			numVersionCheckInterval.Value = ApplicationSettingsManager.Settings.VersionCheckInterval;
			checkVersionCheckAtStartup.Checked = ApplicationSettingsManager.Settings.VersionCheckAtStartup;
			checkSendUsageInformation.Checked = ApplicationSettingsManager.Settings.EnableSendingUsageInformation;
		}

		private void RefreshAutomaticUpdatesInterval()
		{
			numUpdatesInterval.Enabled = !checkAutomaticInterval.Checked;
			numSecondsPerSource.Enabled = checkAutomaticInterval.Checked;
			if (numUpdatesInterval.Enabled && (numUpdatesInterval.Value == 0M))
			{
				int minutes = Status.EnabledSourcesCount;
				if (minutes == 0)
				{
					minutes = 1;
				}
				numUpdatesInterval.Value = minutes;
			}
		}

		private void SetDefaultUserActionCombo()
		{
			List<object> items = new List<object>();
			Dictionary<UserAction, string> dict = SVNPathCommands.GetAvailableUserActions();
			foreach (UserAction action in dict.Keys)
			{
				items.Add(new
				{
					Description = dict[action],
					Value = action
				});
			}
			comboDefaultUserAction.DataSource = items;
		}

		private static void SetRegistryStartWithWindows(bool start)
		{
			if (start)
			{
				string currentExePath = Application.ExecutablePath;
				RegistryHelper.SetValue(string.Format(@"{0}\{1}", Registry.CurrentUser.Name, @"Software\Microsoft\Windows\CurrentVersion\Run"), "SVNMonitor", currentExePath);
			}
			else
			{
				using (RegistryKey key = RegistryHelper.OpenSubKey(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Run", true))
				{
					if (key != null)
					{
						key.DeleteValue("SVNMonitor", false);
					}
				}
			}
		}

		private void SetSelectedTab(TreeNode node)
		{
			uiTabs.SelectedTab = uiTabs.TabPages[node.Tag.ToString()];
		}

		private void SetTortoiseSVNCombo()
		{
			List<object> items = new List<object>();
			foreach (TortoiseSVNAutoClose value in Enum.GetValues(typeof(TortoiseSVNAutoClose)))
			{
				string description = EnumHelper.TranslateEnumValue(value);
				items.Add(new
				{
					Description = description,
					Value = value
				});
			}
			comboAutoCloseTortoiseSVN.DataSource = items;
		}

		private void SetVersionCheck()
		{
			bool oldValue = ApplicationSettingsManager.Settings.EnableVersionCheck;
			bool newValue = checkEnableVersionCheck.Checked;
			ApplicationSettingsManager.Settings.EnableVersionCheck = newValue;
			if (!newValue)
			{
				EventLog.LogWarning(Strings.WarningVersionChecksDisabled, this);
			}
			else if (oldValue != newValue)
			{
				EventLog.LogInfo(Strings.WarningVersionChecksEnabled, this);
			}
		}

		private void SetVersionUpgrade()
		{
			bool oldValue = ApplicationSettingsManager.Settings.EnableVersionUpgrade;
			bool newValue = checkEnableVersionUpgrade.Checked;
			if (!newValue)
			{
				EventLog.LogWarning(Strings.WarningVersionUpgradesDisabled, this);
			}
			else if (oldValue != newValue)
			{
				EventLog.LogInfo(Strings.WarningVersionUpgradesEnabled, this);
			}
		}

		internal static void ShowInstanceDialog()
		{
			if (instance == null)
			{
				instance = new SettingsDialog();
				instance.ShowDialog();
			}
			else
			{
				instance.Activate();
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			Logger.LogUserAction();
			SetSelectedTab(e.Node);
		}

		private static bool TryIsRegistryStartWithWindows()
		{
			try
			{
				return IsRegistryStartWithWindows();
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message, ex);
				return false;
			}
		}

		private static void TrySetRegistryStartWithWindows(bool start)
		{
			try
			{
				SetRegistryStartWithWindows(start);
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message, ex);
				MessageBox.Show(MainForm.FormInstance, ex.Message, Strings.SVNMonitorCaption);
			}
		}

		private void txtTortoiseEXE_Enter(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			if (string.IsNullOrEmpty(txtTortoiseEXE.Text))
			{
				BrowseTortoiseSvnExe();
			}
		}

		private void WriteKeyboardSettings()
		{
			foreach (KeyboardEditorRow row in keyboardEditor1.List)
			{
				ApplicationSettingsManager.Settings.SetValue(row.AssociatedSetting, row.KeyInfo.ToString());
			}
		}

		private void WriteLogEntriesPageSize()
		{
			int oldValue = ApplicationSettingsManager.Settings.LogEntriesPageSize;
			int newValue = checkUnlimitedPageSize.Checked ? -1 : ((int)numPageSize.Value);
			if (oldValue != newValue)
			{
				Logger.Log.InfoFormat("Page size has been changed from {0} to {1}.", oldValue, newValue);
				MessageBox.Show(Strings.PageSizeChangedUseRefreshLog, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			ApplicationSettingsManager.Settings.LogEntriesPageSize = newValue;
		}

		private void WriteSettings()
		{
			TrySetRegistryStartWithWindows(checkStartWithWindows.Checked);
			ApplicationSettingsManager.Settings.HideWhenMinimized = checkHideWhenMinimized.Checked;
			ApplicationSettingsManager.Settings.StartMinimized = checkStartMinimized.Checked;
			ApplicationSettingsManager.Settings.MinimizeWhenClosing = checkMinimizeWhenClosing.Checked;
			WriteLogEntriesPageSize();
			ApplicationSettingsManager.Settings.PreviewRowLines = (int)numPreviewLines.Value;
			ApplicationSettingsManager.Settings.ShowDefaultTextInsteadOfEmptyLogMessage = checkShowDefaultTextInsteadOfEmptyLogMessage.Checked;
			ApplicationSettingsManager.Settings.SourcesPanelShowPath = checkSourcesPanelShowPath.Checked;
			ApplicationSettingsManager.Settings.SourcesPanelShowUrl = checkSourcesPanelShowUrl.Checked;
			ApplicationSettingsManager.Settings.SourcesPanelShowLastCheck = checkSourcesPanelShowLastCheck.Checked;
			ApplicationSettingsManager.Settings.SourcesPanelShowNoUpdates = checkSourcesPanelShowNoUpdates.Checked;
			ApplicationSettingsManager.Settings.DefaultPathAction = comboDefaultUserAction.SelectedValue.ToString();
			ApplicationSettingsManager.Settings.PromptRollbackOldRevision = checkPromptUpdateOldRevision.Checked;
			ApplicationSettingsManager.Settings.PromptUpdateHeadRevision = checkPromptUpdateHeadRevision.Checked;
			ApplicationSettingsManager.Settings.DismissErrorsWhenClicked = checkDismissErrorsWhenClicked.Checked;
			ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled = checkCommitIsAlwaysEnabled.Checked;
			ApplicationSettingsManager.Settings.FileEditor = txtTextEditor.Text.Trim();
			WriteKeyboardSettings();
			ApplicationSettingsManager.Settings.EnableUpdates = checkEnableUpdates.Checked;
			ApplicationSettingsManager.Settings.TreatUnversionedAsModified = checkTreatUnversionedAsModified.Checked;
			ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommit = checkIgnoreIgnoreOnCommit.Checked;
			ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommitConflicts = checkIgnoreIgnoreOnCommitConflicts.Checked;
			ApplicationSettingsManager.Settings.WatchTheNetworkAvailability = checkWatchTheNetworkAvailability.Checked;
			ApplicationSettingsManager.Settings.UpdatesInterval = checkAutomaticInterval.Checked ? 0 : ((int)numUpdatesInterval.Value);
			ApplicationSettingsManager.Settings.UpdatesIntervalPerSource = (int)numSecondsPerSource.Value;
			ApplicationSettingsManager.Settings.TortoiseSVNPath = txtTortoiseEXE.Text.Trim();
			ApplicationSettingsManager.Settings.TortoiseSVNAutoClose = (int)comboAutoCloseTortoiseSVN.SelectedValue;
			ApplicationSettingsManager.Settings.IgnoreDisabledSourcesWhenUpdatingAll = checkIgnoreDisabledSourcesWhenUpdatingAll.Checked;
			ApplicationSettingsManager.Settings.AutomaticallyResolveTortoiseSVNProcess = checkAutomaticallyResolveTortoiseSVNProcess.Checked;
			ApplicationSettingsManager.Settings.SVNUpdateSourcesParallel = checkSVNUpdateSourcesParallel.Checked;
			ApplicationSettingsManager.Settings.SVNUpdateSourcesQueueTimeoutSeconds = (int)numSVNUpdateSourcesQueueTimeoutSeconds.Value;
			ApplicationSettingsManager.Settings.RecommendationMessage = txtRecommendMessage.Text;
			SetVersionCheck();
			SetVersionUpgrade();
			ApplicationSettingsManager.Settings.VersionCheckInterval = (int)numVersionCheckInterval.Value;
			ApplicationSettingsManager.Settings.EnableSendingUsageInformation = checkSendUsageInformation.Checked;
			ApplicationSettingsManager.Settings.VersionCheckAtStartup = checkVersionCheckAtStartup.Checked;
			ApplicationSettingsManager.SaveSettings();
		}
	}
}