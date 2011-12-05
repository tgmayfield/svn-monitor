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
	internal class SettingsDialog : BaseDialog
	{
		private Button btnCancel;
		private Button btnOK;
		private CheckBox checkAutomaticallyResolveTortoiseSVNProcess;
		private CheckBox checkAutomaticInterval;
		private CheckBox checkCommitIsAlwaysEnabled;
		private CheckBox checkDismissErrorsWhenClicked;
		private CheckBox checkEnableUpdates;
		private CheckBox checkEnableVersionCheck;
		private CheckBox checkEnableVersionUpgrade;
		private CheckBox checkHideWhenMinimized;
		private CheckBox checkIgnoreDisabledSourcesWhenUpdatingAll;
		private CheckBox checkIgnoreIgnoreOnCommit;
		private CheckBox checkIgnoreIgnoreOnCommitConflicts;
		private CheckBox checkMinimizeWhenClosing;
		private CheckBox checkPromptUpdateHeadRevision;
		private CheckBox checkPromptUpdateOldRevision;
		private CheckBox checkSendUsageInformation;
		private CheckBox checkShowDefaultTextInsteadOfEmptyLogMessage;
		private CheckBox checkSourcesPanelShowLastCheck;
		private CheckBox checkSourcesPanelShowNoUpdates;
		private CheckBox checkSourcesPanelShowPath;
		private CheckBox checkSourcesPanelShowUrl;
		private CheckBox checkStartMinimized;
		private CheckBox checkStartWithWindows;
		private CheckBox checkSVNUpdateSourcesParallel;
		private CheckBox checkTreatUnversionedAsModified;
		private CheckBox checkUnlimitedPageSize;
		private CheckBox checkVersionCheckAtStartup;
		private CheckBox checkWatchTheNetworkAvailability;
		private Button cmdTortoiseSVNSettings;
		private ComboBox comboAutoCloseTortoiseSVN;
		private ComboBox comboDefaultUserAction;
		private IContainer components;
		private GroupBox groupSourcesPanel;
		private GroupBox groupVersionChecks;
		private static SettingsDialog instance;
		private KeyboardEditor keyboardEditor1;
		private Label label1;
		private Label label10;
		private Label label11;
		private Label label13;
		private Label label14;
		private Label label15;
		private Label label16;
		private Label label17;
		private Label label18;
		private Label label19;
		private Label label2;
		private Label label20;
		private Label label21;
		private Label label22;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private Label label9;
		private Label lblEnableVersionUpgrade;
		private Label lblWatchTheNetworkAvailability;
		private LinkLabel linkAutoCheckTortoiseSVN;
		private LinkLabel linkTextEditorBrowse;
		private LinkLabel linkTortoiseEXEBrowse;
		private NumericUpDown numPageSize;
		private NumericUpDown numPreviewLines;
		private NumericUpDown numSecondsPerSource;
		private NumericUpDown numSVNUpdateSourcesQueueTimeoutSeconds;
		private NumericUpDown numUpdatesInterval;
		private NumericUpDown numVersionCheckInterval;
		private Panel panelBottom;
		private Panel panelLeft;
		private Panel panelRight;
		private const string RegistryRunKeyName = @"Software\Microsoft\Windows\CurrentVersion\Run";
		private const string RegistryRunValueName = "SVNMonitor";
		private TreeView treeView1;
		private TextBox txtRecommendMessage;
		private TextBox txtTextEditor;
		private TextBox txtTortoiseEXE;
		private GroupBox uiGroupBoxRecommendMessage;
		private UITabPage uiTabPageDisplay;
		private UITabPage uiTabPageGeneral;
		private UITabPage uiTabPageKeyboard;
		private UITabPage uiTabPageOperation;
		private UITabPage uiTabPageRecommending;
		private UITabPage uiTabPageTortoiseSVN;
		private UITabPage uiTabPageUpdates;
		private UITabPage uiTabPageVersionCheck;
		private UITab uiTabs;

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

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsDialog));
			btnOK = new Button();
			btnCancel = new Button();
			uiTabs = new UITab();
			uiTabPageGeneral = new UITabPage();
			label13 = new Label();
			checkStartWithWindows = new CheckBox();
			checkStartMinimized = new CheckBox();
			checkHideWhenMinimized = new CheckBox();
			checkMinimizeWhenClosing = new CheckBox();
			uiTabPageDisplay = new UITabPage();
			checkShowDefaultTextInsteadOfEmptyLogMessage = new CheckBox();
			groupSourcesPanel = new GroupBox();
			checkSourcesPanelShowPath = new CheckBox();
			checkSourcesPanelShowNoUpdates = new CheckBox();
			checkSourcesPanelShowLastCheck = new CheckBox();
			checkSourcesPanelShowUrl = new CheckBox();
			label14 = new Label();
			numPreviewLines = new NumericUpDown();
			checkUnlimitedPageSize = new CheckBox();
			label8 = new Label();
			label4 = new Label();
			label10 = new Label();
			numPageSize = new NumericUpDown();
			uiTabPageOperation = new UITabPage();
			linkTextEditorBrowse = new LinkLabel();
			checkDismissErrorsWhenClicked = new CheckBox();
			checkPromptUpdateHeadRevision = new CheckBox();
			checkPromptUpdateOldRevision = new CheckBox();
			label15 = new Label();
			txtTextEditor = new TextBox();
			label11 = new Label();
			comboDefaultUserAction = new ComboBox();
			label6 = new Label();
			uiTabPageKeyboard = new UITabPage();
			keyboardEditor1 = new KeyboardEditor();
			label16 = new Label();
			uiTabPageUpdates = new UITabPage();
			lblWatchTheNetworkAvailability = new Label();
			label17 = new Label();
			checkWatchTheNetworkAvailability = new CheckBox();
			checkIgnoreIgnoreOnCommitConflicts = new CheckBox();
			checkIgnoreIgnoreOnCommit = new CheckBox();
			label1 = new Label();
			checkTreatUnversionedAsModified = new CheckBox();
			checkEnableUpdates = new CheckBox();
			checkAutomaticInterval = new CheckBox();
			numSecondsPerSource = new NumericUpDown();
			numUpdatesInterval = new NumericUpDown();
			label2 = new Label();
			uiTabPageTortoiseSVN = new UITabPage();
			linkTortoiseEXEBrowse = new LinkLabel();
			checkAutomaticallyResolveTortoiseSVNProcess = new CheckBox();
			numSVNUpdateSourcesQueueTimeoutSeconds = new NumericUpDown();
			label9 = new Label();
			checkSVNUpdateSourcesParallel = new CheckBox();
			checkIgnoreDisabledSourcesWhenUpdatingAll = new CheckBox();
			cmdTortoiseSVNSettings = new Button();
			label18 = new Label();
			comboAutoCloseTortoiseSVN = new ComboBox();
			label5 = new Label();
			linkAutoCheckTortoiseSVN = new LinkLabel();
			label3 = new Label();
			txtTortoiseEXE = new TextBox();
			uiTabPageRecommending = new UITabPage();
			uiGroupBoxRecommendMessage = new GroupBox();
			txtRecommendMessage = new TextBox();
			label19 = new Label();
			label22 = new Label();
			label21 = new Label();
			uiTabPageVersionCheck = new UITabPage();
			groupVersionChecks = new GroupBox();
			lblEnableVersionUpgrade = new Label();
			checkSendUsageInformation = new CheckBox();
			label7 = new Label();
			checkEnableVersionUpgrade = new CheckBox();
			numVersionCheckInterval = new NumericUpDown();
			checkVersionCheckAtStartup = new CheckBox();
			label20 = new Label();
			checkEnableVersionCheck = new CheckBox();
			panelBottom = new Panel();
			treeView1 = new TreeView();
			panelLeft = new Panel();
			panelRight = new Panel();
			checkCommitIsAlwaysEnabled = new CheckBox();
			((ISupportInitialize)uiTabs).BeginInit();
			uiTabs.SuspendLayout();
			uiTabPageGeneral.SuspendLayout();
			uiTabPageDisplay.SuspendLayout();
			groupSourcesPanel.SuspendLayout();
			numPreviewLines.BeginInit();
			numPageSize.BeginInit();
			uiTabPageOperation.SuspendLayout();
			uiTabPageKeyboard.SuspendLayout();
			uiTabPageUpdates.SuspendLayout();
			numSecondsPerSource.BeginInit();
			numUpdatesInterval.BeginInit();
			uiTabPageTortoiseSVN.SuspendLayout();
			numSVNUpdateSourcesQueueTimeoutSeconds.BeginInit();
			uiTabPageRecommending.SuspendLayout();
			uiGroupBoxRecommendMessage.SuspendLayout();
			uiTabPageVersionCheck.SuspendLayout();
			groupVersionChecks.SuspendLayout();
			numVersionCheckInterval.BeginInit();
			panelBottom.SuspendLayout();
			panelLeft.SuspendLayout();
			panelRight.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Name = "btnOK";
			btnOK.Click += btnOK_Click;
			resources.ApplyResources(btnCancel, "btnCancel");
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Name = "btnCancel";
			uiTabs.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(uiTabs, "uiTabs");
			uiTabs.Name = "uiTabs";
			uiTabs.ShowFocusRectangle = false;
			uiTabs.TabPages.AddRange(new[]
			{
				uiTabPageGeneral, uiTabPageDisplay, uiTabPageOperation, uiTabPageKeyboard, uiTabPageUpdates, uiTabPageTortoiseSVN, uiTabPageRecommending, uiTabPageVersionCheck
			});
			uiTabs.TabStop = false;
			uiTabs.VisualStyle = TabVisualStyle.VS2005;
			uiTabPageGeneral.Controls.Add(label13);
			uiTabPageGeneral.Controls.Add(checkStartWithWindows);
			uiTabPageGeneral.Controls.Add(checkStartMinimized);
			uiTabPageGeneral.Controls.Add(checkHideWhenMinimized);
			uiTabPageGeneral.Controls.Add(checkMinimizeWhenClosing);
			uiTabPageGeneral.Key = "General";
			resources.ApplyResources(uiTabPageGeneral, "uiTabPageGeneral");
			uiTabPageGeneral.Name = "uiTabPageGeneral";
			uiTabPageGeneral.TabStop = true;
			label13.BackColor = System.Drawing.Color.DimGray;
			label13.BorderStyle = BorderStyle.Fixed3D;
			resources.ApplyResources(label13, "label13");
			label13.ForeColor = System.Drawing.Color.White;
			label13.Name = "label13";
			resources.ApplyResources(checkStartWithWindows, "checkStartWithWindows");
			checkStartWithWindows.BackColor = System.Drawing.Color.Transparent;
			checkStartWithWindows.Checked = true;
			checkStartWithWindows.CheckState = CheckState.Checked;
			checkStartWithWindows.Name = "checkStartWithWindows";
			checkStartWithWindows.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkStartMinimized, "checkStartMinimized");
			checkStartMinimized.BackColor = System.Drawing.Color.Transparent;
			checkStartMinimized.Checked = true;
			checkStartMinimized.CheckState = CheckState.Checked;
			checkStartMinimized.Name = "checkStartMinimized";
			checkStartMinimized.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkHideWhenMinimized, "checkHideWhenMinimized");
			checkHideWhenMinimized.BackColor = System.Drawing.Color.Transparent;
			checkHideWhenMinimized.Checked = true;
			checkHideWhenMinimized.CheckState = CheckState.Checked;
			checkHideWhenMinimized.Name = "checkHideWhenMinimized";
			checkHideWhenMinimized.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkMinimizeWhenClosing, "checkMinimizeWhenClosing");
			checkMinimizeWhenClosing.BackColor = System.Drawing.Color.Transparent;
			checkMinimizeWhenClosing.Checked = true;
			checkMinimizeWhenClosing.CheckState = CheckState.Checked;
			checkMinimizeWhenClosing.Name = "checkMinimizeWhenClosing";
			checkMinimizeWhenClosing.UseVisualStyleBackColor = true;
			uiTabPageDisplay.Controls.Add(checkShowDefaultTextInsteadOfEmptyLogMessage);
			uiTabPageDisplay.Controls.Add(groupSourcesPanel);
			uiTabPageDisplay.Controls.Add(label14);
			uiTabPageDisplay.Controls.Add(numPreviewLines);
			uiTabPageDisplay.Controls.Add(checkUnlimitedPageSize);
			uiTabPageDisplay.Controls.Add(label8);
			uiTabPageDisplay.Controls.Add(label4);
			uiTabPageDisplay.Controls.Add(label10);
			uiTabPageDisplay.Controls.Add(numPageSize);
			uiTabPageDisplay.Key = "Display";
			resources.ApplyResources(uiTabPageDisplay, "uiTabPageDisplay");
			uiTabPageDisplay.Name = "uiTabPageDisplay";
			uiTabPageDisplay.TabStop = true;
			resources.ApplyResources(checkShowDefaultTextInsteadOfEmptyLogMessage, "checkShowDefaultTextInsteadOfEmptyLogMessage");
			checkShowDefaultTextInsteadOfEmptyLogMessage.BackColor = System.Drawing.Color.Transparent;
			checkShowDefaultTextInsteadOfEmptyLogMessage.Checked = true;
			checkShowDefaultTextInsteadOfEmptyLogMessage.CheckState = CheckState.Checked;
			checkShowDefaultTextInsteadOfEmptyLogMessage.Name = "checkShowDefaultTextInsteadOfEmptyLogMessage";
			checkShowDefaultTextInsteadOfEmptyLogMessage.UseVisualStyleBackColor = true;
			resources.ApplyResources(groupSourcesPanel, "groupSourcesPanel");
			groupSourcesPanel.BackColor = System.Drawing.Color.Transparent;
			groupSourcesPanel.Controls.Add(checkSourcesPanelShowPath);
			groupSourcesPanel.Controls.Add(checkSourcesPanelShowNoUpdates);
			groupSourcesPanel.Controls.Add(checkSourcesPanelShowLastCheck);
			groupSourcesPanel.Controls.Add(checkSourcesPanelShowUrl);
			groupSourcesPanel.Name = "groupSourcesPanel";
			groupSourcesPanel.TabStop = false;
			resources.ApplyResources(checkSourcesPanelShowPath, "checkSourcesPanelShowPath");
			checkSourcesPanelShowPath.Name = "checkSourcesPanelShowPath";
			checkSourcesPanelShowPath.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkSourcesPanelShowNoUpdates, "checkSourcesPanelShowNoUpdates");
			checkSourcesPanelShowNoUpdates.Name = "checkSourcesPanelShowNoUpdates";
			checkSourcesPanelShowNoUpdates.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkSourcesPanelShowLastCheck, "checkSourcesPanelShowLastCheck");
			checkSourcesPanelShowLastCheck.Name = "checkSourcesPanelShowLastCheck";
			checkSourcesPanelShowLastCheck.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkSourcesPanelShowUrl, "checkSourcesPanelShowUrl");
			checkSourcesPanelShowUrl.Name = "checkSourcesPanelShowUrl";
			checkSourcesPanelShowUrl.UseVisualStyleBackColor = true;
			label14.BackColor = System.Drawing.Color.DimGray;
			label14.BorderStyle = BorderStyle.Fixed3D;
			resources.ApplyResources(label14, "label14");
			label14.ForeColor = System.Drawing.Color.White;
			label14.Name = "label14";
			resources.ApplyResources(numPreviewLines, "numPreviewLines");
			int[] tempAnotherLocal1 = new int[4];
			tempAnotherLocal1[0] = 0xf423f;
			numPreviewLines.Maximum = new decimal(tempAnotherLocal1);
			int[] tempAnotherLocal2 = new int[4];
			tempAnotherLocal2[0] = 1;
			numPreviewLines.Minimum = new decimal(tempAnotherLocal2);
			numPreviewLines.Name = "numPreviewLines";
			int[] tempAnotherLocal3 = new int[4];
			tempAnotherLocal3[0] = 1;
			numPreviewLines.Value = new decimal(tempAnotherLocal3);
			resources.ApplyResources(checkUnlimitedPageSize, "checkUnlimitedPageSize");
			checkUnlimitedPageSize.BackColor = System.Drawing.Color.Transparent;
			checkUnlimitedPageSize.Checked = true;
			checkUnlimitedPageSize.CheckState = CheckState.Checked;
			checkUnlimitedPageSize.Name = "checkUnlimitedPageSize";
			checkUnlimitedPageSize.UseVisualStyleBackColor = true;
			checkUnlimitedPageSize.CheckedChanged += checkUnlimitedPageSize_CheckedChanged;
			resources.ApplyResources(label8, "label8");
			label8.BackColor = System.Drawing.Color.Transparent;
			label8.ForeColor = System.Drawing.Color.MidnightBlue;
			label8.Name = "label8";
			resources.ApplyResources(label4, "label4");
			label4.BackColor = System.Drawing.Color.Transparent;
			label4.Name = "label4";
			resources.ApplyResources(label10, "label10");
			label10.BackColor = System.Drawing.Color.Transparent;
			label10.Name = "label10";
			resources.ApplyResources(numPageSize, "numPageSize");
			int[] tempAnotherLocal4 = new int[4];
			tempAnotherLocal4[0] = 0xf423f;
			numPageSize.Maximum = new decimal(tempAnotherLocal4);
			int[] tempAnotherLocal5 = new int[4];
			tempAnotherLocal5[0] = 1;
			numPageSize.Minimum = new decimal(tempAnotherLocal5);
			numPageSize.Name = "numPageSize";
			int[] tempAnotherLocal6 = new int[4];
			tempAnotherLocal6[0] = 1;
			numPageSize.Value = new decimal(tempAnotherLocal6);
			uiTabPageOperation.Controls.Add(checkCommitIsAlwaysEnabled);
			uiTabPageOperation.Controls.Add(linkTextEditorBrowse);
			uiTabPageOperation.Controls.Add(checkDismissErrorsWhenClicked);
			uiTabPageOperation.Controls.Add(checkPromptUpdateHeadRevision);
			uiTabPageOperation.Controls.Add(checkPromptUpdateOldRevision);
			uiTabPageOperation.Controls.Add(label15);
			uiTabPageOperation.Controls.Add(txtTextEditor);
			uiTabPageOperation.Controls.Add(label11);
			uiTabPageOperation.Controls.Add(comboDefaultUserAction);
			uiTabPageOperation.Controls.Add(label6);
			uiTabPageOperation.Key = "Operation";
			resources.ApplyResources(uiTabPageOperation, "uiTabPageOperation");
			uiTabPageOperation.Name = "uiTabPageOperation";
			uiTabPageOperation.TabStop = true;
			resources.ApplyResources(linkTextEditorBrowse, "linkTextEditorBrowse");
			linkTextEditorBrowse.BackColor = System.Drawing.Color.Transparent;
			linkTextEditorBrowse.Name = "linkTextEditorBrowse";
			linkTextEditorBrowse.TabStop = true;
			linkTextEditorBrowse.LinkClicked += linkTextEditorBrowse_LinkClicked;
			resources.ApplyResources(checkDismissErrorsWhenClicked, "checkDismissErrorsWhenClicked");
			checkDismissErrorsWhenClicked.BackColor = System.Drawing.Color.Transparent;
			checkDismissErrorsWhenClicked.Checked = true;
			checkDismissErrorsWhenClicked.CheckState = CheckState.Checked;
			checkDismissErrorsWhenClicked.Name = "checkDismissErrorsWhenClicked";
			checkDismissErrorsWhenClicked.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkPromptUpdateHeadRevision, "checkPromptUpdateHeadRevision");
			checkPromptUpdateHeadRevision.BackColor = System.Drawing.Color.Transparent;
			checkPromptUpdateHeadRevision.Checked = true;
			checkPromptUpdateHeadRevision.CheckState = CheckState.Checked;
			checkPromptUpdateHeadRevision.Name = "checkPromptUpdateHeadRevision";
			checkPromptUpdateHeadRevision.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkPromptUpdateOldRevision, "checkPromptUpdateOldRevision");
			checkPromptUpdateOldRevision.BackColor = System.Drawing.Color.Transparent;
			checkPromptUpdateOldRevision.Checked = true;
			checkPromptUpdateOldRevision.CheckState = CheckState.Checked;
			checkPromptUpdateOldRevision.Name = "checkPromptUpdateOldRevision";
			checkPromptUpdateOldRevision.UseVisualStyleBackColor = true;
			label15.BackColor = System.Drawing.Color.DimGray;
			label15.BorderStyle = BorderStyle.Fixed3D;
			resources.ApplyResources(label15, "label15");
			label15.ForeColor = System.Drawing.Color.White;
			label15.Name = "label15";
			resources.ApplyResources(txtTextEditor, "txtTextEditor");
			txtTextEditor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			txtTextEditor.AutoCompleteSource = AutoCompleteSource.FileSystem;
			txtTextEditor.Name = "txtTextEditor";
			resources.ApplyResources(label11, "label11");
			label11.BackColor = System.Drawing.Color.Transparent;
			label11.Name = "label11";
			resources.ApplyResources(comboDefaultUserAction, "comboDefaultUserAction");
			comboDefaultUserAction.DisplayMember = "Description";
			comboDefaultUserAction.DropDownStyle = ComboBoxStyle.DropDownList;
			comboDefaultUserAction.Name = "comboDefaultUserAction";
			comboDefaultUserAction.ValueMember = "Value";
			resources.ApplyResources(label6, "label6");
			label6.BackColor = System.Drawing.Color.Transparent;
			label6.Name = "label6";
			uiTabPageKeyboard.Controls.Add(keyboardEditor1);
			uiTabPageKeyboard.Controls.Add(label16);
			uiTabPageKeyboard.Key = "Keyboard";
			resources.ApplyResources(uiTabPageKeyboard, "uiTabPageKeyboard");
			uiTabPageKeyboard.Name = "uiTabPageKeyboard";
			uiTabPageKeyboard.TabStop = true;
			keyboardEditor1.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(keyboardEditor1, "keyboardEditor1");
			keyboardEditor1.Name = "keyboardEditor1";
			label16.BackColor = System.Drawing.Color.DimGray;
			label16.BorderStyle = BorderStyle.Fixed3D;
			resources.ApplyResources(label16, "label16");
			label16.ForeColor = System.Drawing.Color.White;
			label16.Name = "label16";
			uiTabPageUpdates.Controls.Add(lblWatchTheNetworkAvailability);
			uiTabPageUpdates.Controls.Add(label17);
			uiTabPageUpdates.Controls.Add(checkWatchTheNetworkAvailability);
			uiTabPageUpdates.Controls.Add(checkIgnoreIgnoreOnCommitConflicts);
			uiTabPageUpdates.Controls.Add(checkIgnoreIgnoreOnCommit);
			uiTabPageUpdates.Controls.Add(label1);
			uiTabPageUpdates.Controls.Add(checkTreatUnversionedAsModified);
			uiTabPageUpdates.Controls.Add(checkEnableUpdates);
			uiTabPageUpdates.Controls.Add(checkAutomaticInterval);
			uiTabPageUpdates.Controls.Add(numSecondsPerSource);
			uiTabPageUpdates.Controls.Add(numUpdatesInterval);
			uiTabPageUpdates.Controls.Add(label2);
			uiTabPageUpdates.Key = "Sources";
			resources.ApplyResources(uiTabPageUpdates, "uiTabPageUpdates");
			uiTabPageUpdates.Name = "uiTabPageUpdates";
			uiTabPageUpdates.TabStop = true;
			resources.ApplyResources(lblWatchTheNetworkAvailability, "lblWatchTheNetworkAvailability");
			lblWatchTheNetworkAvailability.BackColor = System.Drawing.Color.Transparent;
			lblWatchTheNetworkAvailability.ForeColor = System.Drawing.Color.MidnightBlue;
			lblWatchTheNetworkAvailability.Name = "lblWatchTheNetworkAvailability";
			label17.BackColor = System.Drawing.Color.DimGray;
			label17.BorderStyle = BorderStyle.Fixed3D;
			resources.ApplyResources(label17, "label17");
			label17.ForeColor = System.Drawing.Color.White;
			label17.Name = "label17";
			resources.ApplyResources(checkWatchTheNetworkAvailability, "checkWatchTheNetworkAvailability");
			checkWatchTheNetworkAvailability.BackColor = System.Drawing.Color.Transparent;
			checkWatchTheNetworkAvailability.Checked = true;
			checkWatchTheNetworkAvailability.CheckState = CheckState.Checked;
			checkWatchTheNetworkAvailability.Name = "checkWatchTheNetworkAvailability";
			checkWatchTheNetworkAvailability.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkIgnoreIgnoreOnCommitConflicts, "checkIgnoreIgnoreOnCommitConflicts");
			checkIgnoreIgnoreOnCommitConflicts.BackColor = System.Drawing.Color.Transparent;
			checkIgnoreIgnoreOnCommitConflicts.Checked = true;
			checkIgnoreIgnoreOnCommitConflicts.CheckState = CheckState.Checked;
			checkIgnoreIgnoreOnCommitConflicts.Name = "checkIgnoreIgnoreOnCommitConflicts";
			checkIgnoreIgnoreOnCommitConflicts.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkIgnoreIgnoreOnCommit, "checkIgnoreIgnoreOnCommit");
			checkIgnoreIgnoreOnCommit.BackColor = System.Drawing.Color.Transparent;
			checkIgnoreIgnoreOnCommit.Checked = true;
			checkIgnoreIgnoreOnCommit.CheckState = CheckState.Checked;
			checkIgnoreIgnoreOnCommit.Name = "checkIgnoreIgnoreOnCommit";
			checkIgnoreIgnoreOnCommit.UseVisualStyleBackColor = true;
			resources.ApplyResources(label1, "label1");
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.ForeColor = System.Drawing.Color.MidnightBlue;
			label1.Name = "label1";
			resources.ApplyResources(checkTreatUnversionedAsModified, "checkTreatUnversionedAsModified");
			checkTreatUnversionedAsModified.BackColor = System.Drawing.Color.Transparent;
			checkTreatUnversionedAsModified.Checked = true;
			checkTreatUnversionedAsModified.CheckState = CheckState.Checked;
			checkTreatUnversionedAsModified.Name = "checkTreatUnversionedAsModified";
			checkTreatUnversionedAsModified.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkEnableUpdates, "checkEnableUpdates");
			checkEnableUpdates.BackColor = System.Drawing.Color.Transparent;
			checkEnableUpdates.Checked = true;
			checkEnableUpdates.CheckState = CheckState.Checked;
			checkEnableUpdates.Name = "checkEnableUpdates";
			checkEnableUpdates.UseVisualStyleBackColor = true;
			resources.ApplyResources(checkAutomaticInterval, "checkAutomaticInterval");
			checkAutomaticInterval.BackColor = System.Drawing.Color.Transparent;
			checkAutomaticInterval.Checked = true;
			checkAutomaticInterval.CheckState = CheckState.Checked;
			checkAutomaticInterval.Name = "checkAutomaticInterval";
			checkAutomaticInterval.UseVisualStyleBackColor = true;
			checkAutomaticInterval.CheckedChanged += checkAutomaticInterval_CheckedChanged;
			resources.ApplyResources(numSecondsPerSource, "numSecondsPerSource");
			int[] tempAnotherLocal7 = new int[4];
			tempAnotherLocal7[0] = 0xe10;
			numSecondsPerSource.Maximum = new decimal(tempAnotherLocal7);
			int[] tempAnotherLocal8 = new int[4];
			tempAnotherLocal8[0] = 1;
			numSecondsPerSource.Minimum = new decimal(tempAnotherLocal8);
			numSecondsPerSource.Name = "numSecondsPerSource";
			int[] tempAnotherLocal9 = new int[4];
			tempAnotherLocal9[0] = 1;
			numSecondsPerSource.Value = new decimal(tempAnotherLocal9);
			resources.ApplyResources(numUpdatesInterval, "numUpdatesInterval");
			int[] tempAnotherLocal10 = new int[4];
			tempAnotherLocal10[0] = 0x5a0;
			numUpdatesInterval.Maximum = new decimal(tempAnotherLocal10);
			numUpdatesInterval.Name = "numUpdatesInterval";
			resources.ApplyResources(label2, "label2");
			label2.BackColor = System.Drawing.Color.Transparent;
			label2.Name = "label2";
			uiTabPageTortoiseSVN.Controls.Add(linkTortoiseEXEBrowse);
			uiTabPageTortoiseSVN.Controls.Add(checkAutomaticallyResolveTortoiseSVNProcess);
			uiTabPageTortoiseSVN.Controls.Add(numSVNUpdateSourcesQueueTimeoutSeconds);
			uiTabPageTortoiseSVN.Controls.Add(label9);
			uiTabPageTortoiseSVN.Controls.Add(checkSVNUpdateSourcesParallel);
			uiTabPageTortoiseSVN.Controls.Add(checkIgnoreDisabledSourcesWhenUpdatingAll);
			uiTabPageTortoiseSVN.Controls.Add(cmdTortoiseSVNSettings);
			uiTabPageTortoiseSVN.Controls.Add(label18);
			uiTabPageTortoiseSVN.Controls.Add(comboAutoCloseTortoiseSVN);
			uiTabPageTortoiseSVN.Controls.Add(label5);
			uiTabPageTortoiseSVN.Controls.Add(linkAutoCheckTortoiseSVN);
			uiTabPageTortoiseSVN.Controls.Add(label3);
			uiTabPageTortoiseSVN.Controls.Add(txtTortoiseEXE);
			uiTabPageTortoiseSVN.Key = "TortoiseSVN";
			resources.ApplyResources(uiTabPageTortoiseSVN, "uiTabPageTortoiseSVN");
			uiTabPageTortoiseSVN.Name = "uiTabPageTortoiseSVN";
			uiTabPageTortoiseSVN.TabStop = true;
			resources.ApplyResources(linkTortoiseEXEBrowse, "linkTortoiseEXEBrowse");
			linkTortoiseEXEBrowse.BackColor = System.Drawing.Color.Transparent;
			linkTortoiseEXEBrowse.Name = "linkTortoiseEXEBrowse";
			linkTortoiseEXEBrowse.TabStop = true;
			linkTortoiseEXEBrowse.LinkClicked += linkTortoiseEXEBrowse_LinkClicked;
			resources.ApplyResources(checkAutomaticallyResolveTortoiseSVNProcess, "checkAutomaticallyResolveTortoiseSVNProcess");
			checkAutomaticallyResolveTortoiseSVNProcess.BackColor = System.Drawing.Color.Transparent;
			checkAutomaticallyResolveTortoiseSVNProcess.Checked = true;
			checkAutomaticallyResolveTortoiseSVNProcess.CheckState = CheckState.Checked;
			checkAutomaticallyResolveTortoiseSVNProcess.Name = "checkAutomaticallyResolveTortoiseSVNProcess";
			checkAutomaticallyResolveTortoiseSVNProcess.UseVisualStyleBackColor = true;
			checkAutomaticallyResolveTortoiseSVNProcess.CheckedChanged += checkAutomaticallyResolveTortoiseSVNProcess_CheckedChanged;
			resources.ApplyResources(numSVNUpdateSourcesQueueTimeoutSeconds, "numSVNUpdateSourcesQueueTimeoutSeconds");
			int[] tempAnotherLocal11 = new int[4];
			tempAnotherLocal11[0] = 300;
			numSVNUpdateSourcesQueueTimeoutSeconds.Maximum = new decimal(tempAnotherLocal11);
			int[] tempAnotherLocal12 = new int[4];
			tempAnotherLocal12[0] = 1;
			numSVNUpdateSourcesQueueTimeoutSeconds.Minimum = new decimal(tempAnotherLocal12);
			numSVNUpdateSourcesQueueTimeoutSeconds.Name = "numSVNUpdateSourcesQueueTimeoutSeconds";
			int[] tempAnotherLocal13 = new int[4];
			tempAnotherLocal13[0] = 1;
			numSVNUpdateSourcesQueueTimeoutSeconds.Value = new decimal(tempAnotherLocal13);
			resources.ApplyResources(label9, "label9");
			label9.BackColor = System.Drawing.Color.Transparent;
			label9.Name = "label9";
			resources.ApplyResources(checkSVNUpdateSourcesParallel, "checkSVNUpdateSourcesParallel");
			checkSVNUpdateSourcesParallel.BackColor = System.Drawing.Color.Transparent;
			checkSVNUpdateSourcesParallel.Checked = true;
			checkSVNUpdateSourcesParallel.CheckState = CheckState.Checked;
			checkSVNUpdateSourcesParallel.Name = "checkSVNUpdateSourcesParallel";
			checkSVNUpdateSourcesParallel.UseVisualStyleBackColor = true;
			checkSVNUpdateSourcesParallel.CheckedChanged += checkSVNUpdateSourcesParallel_CheckedChanged;
			resources.ApplyResources(checkIgnoreDisabledSourcesWhenUpdatingAll, "checkIgnoreDisabledSourcesWhenUpdatingAll");
			checkIgnoreDisabledSourcesWhenUpdatingAll.BackColor = System.Drawing.Color.Transparent;
			checkIgnoreDisabledSourcesWhenUpdatingAll.Checked = true;
			checkIgnoreDisabledSourcesWhenUpdatingAll.CheckState = CheckState.Checked;
			checkIgnoreDisabledSourcesWhenUpdatingAll.Name = "checkIgnoreDisabledSourcesWhenUpdatingAll";
			checkIgnoreDisabledSourcesWhenUpdatingAll.UseVisualStyleBackColor = true;
			resources.ApplyResources(cmdTortoiseSVNSettings, "cmdTortoiseSVNSettings");
			cmdTortoiseSVNSettings.Name = "cmdTortoiseSVNSettings";
			cmdTortoiseSVNSettings.Click += cmdTortoiseSVNSettings_Click;
			label18.BackColor = System.Drawing.Color.DimGray;
			label18.BorderStyle = BorderStyle.Fixed3D;
			resources.ApplyResources(label18, "label18");
			label18.ForeColor = System.Drawing.Color.White;
			label18.Name = "label18";
			resources.ApplyResources(comboAutoCloseTortoiseSVN, "comboAutoCloseTortoiseSVN");
			comboAutoCloseTortoiseSVN.DisplayMember = "Description";
			comboAutoCloseTortoiseSVN.DropDownStyle = ComboBoxStyle.DropDownList;
			comboAutoCloseTortoiseSVN.Name = "comboAutoCloseTortoiseSVN";
			comboAutoCloseTortoiseSVN.ValueMember = "Value";
			resources.ApplyResources(label5, "label5");
			label5.BackColor = System.Drawing.Color.Transparent;
			label5.Name = "label5";
			resources.ApplyResources(linkAutoCheckTortoiseSVN, "linkAutoCheckTortoiseSVN");
			linkAutoCheckTortoiseSVN.BackColor = System.Drawing.Color.Transparent;
			linkAutoCheckTortoiseSVN.Name = "linkAutoCheckTortoiseSVN";
			linkAutoCheckTortoiseSVN.TabStop = true;
			linkAutoCheckTortoiseSVN.LinkClicked += linkAutoCheckTortoiseSVN_LinkClicked;
			resources.ApplyResources(label3, "label3");
			label3.BackColor = System.Drawing.Color.Transparent;
			label3.Name = "label3";
			resources.ApplyResources(txtTortoiseEXE, "txtTortoiseEXE");
			txtTortoiseEXE.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			txtTortoiseEXE.AutoCompleteSource = AutoCompleteSource.FileSystem;
			txtTortoiseEXE.Name = "txtTortoiseEXE";
			txtTortoiseEXE.Enter += txtTortoiseEXE_Enter;
			uiTabPageRecommending.Controls.Add(uiGroupBoxRecommendMessage);
			uiTabPageRecommending.Controls.Add(label22);
			uiTabPageRecommending.Controls.Add(label21);
			uiTabPageRecommending.Key = "Recommending";
			resources.ApplyResources(uiTabPageRecommending, "uiTabPageRecommending");
			uiTabPageRecommending.Name = "uiTabPageRecommending";
			uiTabPageRecommending.TabStop = true;
			resources.ApplyResources(uiGroupBoxRecommendMessage, "uiGroupBoxRecommendMessage");
			uiGroupBoxRecommendMessage.BackColor = System.Drawing.Color.Transparent;
			uiGroupBoxRecommendMessage.Controls.Add(txtRecommendMessage);
			uiGroupBoxRecommendMessage.Controls.Add(label19);
			uiGroupBoxRecommendMessage.Name = "uiGroupBoxRecommendMessage";
			uiGroupBoxRecommendMessage.TabStop = false;
			resources.ApplyResources(txtRecommendMessage, "txtRecommendMessage");
			txtRecommendMessage.Name = "txtRecommendMessage";
			resources.ApplyResources(label19, "label19");
			label19.BackColor = System.Drawing.Color.Transparent;
			label19.ForeColor = System.Drawing.Color.MidnightBlue;
			label19.Name = "label19";
			resources.ApplyResources(label22, "label22");
			label22.BackColor = System.Drawing.Color.Transparent;
			label22.Name = "label22";
			label21.BackColor = System.Drawing.Color.DimGray;
			label21.BorderStyle = BorderStyle.Fixed3D;
			resources.ApplyResources(label21, "label21");
			label21.ForeColor = System.Drawing.Color.White;
			label21.Name = "label21";
			uiTabPageVersionCheck.Controls.Add(groupVersionChecks);
			uiTabPageVersionCheck.Controls.Add(label20);
			uiTabPageVersionCheck.Controls.Add(checkEnableVersionCheck);
			uiTabPageVersionCheck.Key = "VersionChecking";
			resources.ApplyResources(uiTabPageVersionCheck, "uiTabPageVersionCheck");
			uiTabPageVersionCheck.Name = "uiTabPageVersionCheck";
			uiTabPageVersionCheck.TabStop = true;
			resources.ApplyResources(groupVersionChecks, "groupVersionChecks");
			groupVersionChecks.BackColor = System.Drawing.Color.Transparent;
			groupVersionChecks.Controls.Add(lblEnableVersionUpgrade);
			groupVersionChecks.Controls.Add(checkSendUsageInformation);
			groupVersionChecks.Controls.Add(label7);
			groupVersionChecks.Controls.Add(checkEnableVersionUpgrade);
			groupVersionChecks.Controls.Add(numVersionCheckInterval);
			groupVersionChecks.Controls.Add(checkVersionCheckAtStartup);
			groupVersionChecks.Name = "groupVersionChecks";
			groupVersionChecks.TabStop = false;
			resources.ApplyResources(lblEnableVersionUpgrade, "lblEnableVersionUpgrade");
			lblEnableVersionUpgrade.ForeColor = System.Drawing.Color.Blue;
			lblEnableVersionUpgrade.Name = "lblEnableVersionUpgrade";
			resources.ApplyResources(checkSendUsageInformation, "checkSendUsageInformation");
			checkSendUsageInformation.BackColor = System.Drawing.Color.Transparent;
			checkSendUsageInformation.Checked = true;
			checkSendUsageInformation.CheckState = CheckState.Checked;
			checkSendUsageInformation.Name = "checkSendUsageInformation";
			checkSendUsageInformation.UseVisualStyleBackColor = true;
			resources.ApplyResources(label7, "label7");
			label7.BackColor = System.Drawing.Color.Transparent;
			label7.Name = "label7";
			resources.ApplyResources(checkEnableVersionUpgrade, "checkEnableVersionUpgrade");
			checkEnableVersionUpgrade.BackColor = System.Drawing.Color.Transparent;
			checkEnableVersionUpgrade.Checked = true;
			checkEnableVersionUpgrade.CheckState = CheckState.Checked;
			checkEnableVersionUpgrade.Name = "checkEnableVersionUpgrade";
			checkEnableVersionUpgrade.UseVisualStyleBackColor = true;
			resources.ApplyResources(numVersionCheckInterval, "numVersionCheckInterval");
			int[] tempAnotherLocal14 = new int[4];
			tempAnotherLocal14[0] = 0x5a0;
			numVersionCheckInterval.Maximum = new decimal(tempAnotherLocal14);
			numVersionCheckInterval.Name = "numVersionCheckInterval";
			resources.ApplyResources(checkVersionCheckAtStartup, "checkVersionCheckAtStartup");
			checkVersionCheckAtStartup.BackColor = System.Drawing.Color.Transparent;
			checkVersionCheckAtStartup.Checked = true;
			checkVersionCheckAtStartup.CheckState = CheckState.Checked;
			checkVersionCheckAtStartup.Name = "checkVersionCheckAtStartup";
			checkVersionCheckAtStartup.UseVisualStyleBackColor = true;
			label20.BackColor = System.Drawing.Color.DimGray;
			label20.BorderStyle = BorderStyle.Fixed3D;
			resources.ApplyResources(label20, "label20");
			label20.ForeColor = System.Drawing.Color.White;
			label20.Name = "label20";
			resources.ApplyResources(checkEnableVersionCheck, "checkEnableVersionCheck");
			checkEnableVersionCheck.BackColor = System.Drawing.Color.Transparent;
			checkEnableVersionCheck.Checked = true;
			checkEnableVersionCheck.CheckState = CheckState.Checked;
			checkEnableVersionCheck.Name = "checkEnableVersionCheck";
			checkEnableVersionCheck.UseVisualStyleBackColor = true;
			checkEnableVersionCheck.CheckedChanged += checkEnableVersionCheck_CheckedChanged;
			panelBottom.Controls.Add(btnOK);
			panelBottom.Controls.Add(btnCancel);
			resources.ApplyResources(panelBottom, "panelBottom");
			panelBottom.Name = "panelBottom";
			resources.ApplyResources(treeView1, "treeView1");
			treeView1.FullRowSelect = true;
			treeView1.HideSelection = false;
			treeView1.HotTracking = true;
			treeView1.ItemHeight = 0x12;
			treeView1.Name = "treeView1";
			treeView1.Nodes.AddRange(new[]
			{
				(TreeNode)resources.GetObject("treeView1.Nodes"), (TreeNode)resources.GetObject("treeView1.Nodes1"), (TreeNode)resources.GetObject("treeView1.Nodes2"), (TreeNode)resources.GetObject("treeView1.Nodes3"), (TreeNode)resources.GetObject("treeView1.Nodes4"), (TreeNode)resources.GetObject("treeView1.Nodes5"), (TreeNode)resources.GetObject("treeView1.Nodes6"), (TreeNode)resources.GetObject("treeView1.Nodes7")
			});
			treeView1.ShowRootLines = false;
			treeView1.AfterSelect += treeView1_AfterSelect;
			panelLeft.Controls.Add(panelRight);
			panelLeft.Controls.Add(treeView1);
			resources.ApplyResources(panelLeft, "panelLeft");
			panelLeft.Name = "panelLeft";
			panelRight.Controls.Add(uiTabs);
			resources.ApplyResources(panelRight, "panelRight");
			panelRight.Name = "panelRight";
			resources.ApplyResources(checkCommitIsAlwaysEnabled, "checkCommitIsAlwaysEnabled");
			checkCommitIsAlwaysEnabled.BackColor = System.Drawing.Color.Transparent;
			checkCommitIsAlwaysEnabled.Checked = true;
			checkCommitIsAlwaysEnabled.CheckState = CheckState.Checked;
			checkCommitIsAlwaysEnabled.Name = "checkCommitIsAlwaysEnabled";
			checkCommitIsAlwaysEnabled.UseVisualStyleBackColor = true;
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.Controls.Add(panelLeft);
			base.Controls.Add(panelBottom);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Name = "SettingsDialog";
			base.ShowInTaskbar = false;
			((ISupportInitialize)uiTabs).EndInit();
			uiTabs.ResumeLayout(false);
			uiTabPageGeneral.ResumeLayout(false);
			uiTabPageGeneral.PerformLayout();
			uiTabPageDisplay.ResumeLayout(false);
			uiTabPageDisplay.PerformLayout();
			groupSourcesPanel.ResumeLayout(false);
			groupSourcesPanel.PerformLayout();
			numPreviewLines.EndInit();
			numPageSize.EndInit();
			uiTabPageOperation.ResumeLayout(false);
			uiTabPageOperation.PerformLayout();
			uiTabPageKeyboard.ResumeLayout(false);
			uiTabPageUpdates.ResumeLayout(false);
			uiTabPageUpdates.PerformLayout();
			numSecondsPerSource.EndInit();
			numUpdatesInterval.EndInit();
			uiTabPageTortoiseSVN.ResumeLayout(false);
			uiTabPageTortoiseSVN.PerformLayout();
			numSVNUpdateSourcesQueueTimeoutSeconds.EndInit();
			uiTabPageRecommending.ResumeLayout(false);
			uiGroupBoxRecommendMessage.ResumeLayout(false);
			uiGroupBoxRecommendMessage.PerformLayout();
			uiTabPageVersionCheck.ResumeLayout(false);
			uiTabPageVersionCheck.PerformLayout();
			groupVersionChecks.ResumeLayout(false);
			groupVersionChecks.PerformLayout();
			numVersionCheckInterval.EndInit();
			panelBottom.ResumeLayout(false);
			panelLeft.ResumeLayout(false);
			panelRight.ResumeLayout(false);
			base.ResumeLayout(false);
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