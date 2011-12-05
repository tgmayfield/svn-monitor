using System.Windows.Forms;
using System.ComponentModel;
using SVNMonitor.View.Controls;
using System;
using Janus.Windows.UI.Tab;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.Helpers;
using SVNMonitor.SVN;
using System.Collections.Generic;
using SVNMonitor.Settings;
using SVNMonitor.Extensions;
using System.Drawing;
using SVNMonitor.Resources;
using Microsoft.Win32;
using System.IO;
using SVNMonitor;
using SVNMonitor.View;

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

	private LinkLabel linkUsageInformationHelp;

	private NumericUpDown numPageSize;

	private NumericUpDown numPreviewLines;

	private NumericUpDown numSecondsPerSource;

	private NumericUpDown numSVNUpdateSourcesQueueTimeoutSeconds;

	private NumericUpDown numUpdatesInterval;

	private NumericUpDown numVersionCheckInterval;

	private Panel panelBottom;

	private Panel panelLeft;

	private Panel panelRight;

	private const string RegistryRunKeyName = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";

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
		this.InitializeComponent();
	}

	private void AutoCheckTortoiseSVN()
	{
		object[] objArray;
		Logger.LogUserAction();
		DialogResult result = MessageBox.Show(this, Strings.AskCheckRegistryForInstalledFile, Strings.SVNMonitorCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		Logger.Log.InfoFormat("Check registry: User clicked {0}", result);
		if (result == DialogResult.No)
		{
			return;
		}
		string file = TortoiseSVNHelper.GetTortoiseSVNProcPath();
		file = EnvironmentHelper.ExpandEnvironmentVariables(file);
		if (FileSystemHelper.FileExists(file))
		{
			Logger.Log.InfoFormat("Found TortoiseSVN at {0}", file);
			MessageBox.Show(this, Strings.FoundFileAt_FORMAT.FormatWith(new object[] { file }), Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			this.txtTortoiseEXE.Text = file;
			return;
		}
		Logger.Log.Info("Can't find TortoiseSVN.");
		MessageBox.Show(this, Strings.AskCantFindTortoiseSVN, Strings.SVNMonitorCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
	}

	internal static string BrowseSvnExe(string initialDirectory)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.CheckFileExists = true;
		openFileDialog.DefaultExt = ".exe";
		openFileDialog.Filter = "svn.exe|svn.exe";
		openFileDialog.InitialDirectory = initialDirectory;
		OpenFileDialog dialog = openFileDialog;
		DialogResult result = dialog.ShowDialog();
		if (result != DialogResult.OK)
		{
			return null;
		}
		return dialog.FileName;
	}

	private void BrowseTextEditor()
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.CheckFileExists = true;
		openFileDialog.DefaultExt = ".exe";
		openFileDialog.Filter = string.Format("{0}(*.EXE;*.COM;*.BAT;*.CMD)|*.EXE;*.COM;*.BAT;*.CMD", Strings.ExecutablesFileTypes);
		OpenFileDialog dialog = openFileDialog;
		DialogResult result = dialog.ShowDialog();
		if (result != DialogResult.OK)
		{
			return;
		}
		this.txtTextEditor.Text = dialog.FileName;
	}

	private void BrowseTortoiseSvnExe()
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.CheckFileExists = true;
		openFileDialog.DefaultExt = ".exe";
		openFileDialog.Filter = "TortoiseProc.exe|TortoiseProc.exe";
		openFileDialog.InitialDirectory = this.txtTortoiseEXE.Text;
		OpenFileDialog dialog = openFileDialog;
		DialogResult result = dialog.ShowDialog();
		if (result != DialogResult.OK)
		{
			return;
		}
		this.txtTortoiseEXE.Text = dialog.FileName;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.WriteSettings();
	}

	private void checkAutomaticallyResolveTortoiseSVNProcess_CheckedChanged(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		bool flag1.set_Enabled(bool flag2 = flag1).Visible = flag2;
	}

	private void checkAutomaticInterval_CheckedChanged(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.RefreshAutomaticUpdatesInterval();
	}

	private void checkEnableVersionCheck_CheckedChanged(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.groupVersionChecks.Enabled = this.checkEnableVersionCheck.Checked;
	}

	private void checkSVNUpdateSourcesParallel_CheckedChanged(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.numSVNUpdateSourcesQueueTimeoutSeconds.Enabled = !this.checkSVNUpdateSourcesParallel.Checked;
	}

	private void checkUnlimitedPageSize_CheckedChanged(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		this.numPageSize.Enabled = !this.checkUnlimitedPageSize.Checked;
	}

	private void cmdTortoiseSVNSettings_Click(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		TortoiseProcess.Settings();
	}

	private IEnumerable<KeyboardEditorRow> CreateKeyboardDataSource()
	{
		IEnumerable<PropertyInfo> properties = typeof(ApplicationSettings).GetProperties().Where<PropertyInfo>(new Predicate<PropertyInfo>((p) => Attribute.IsDefined(p, typeof(KeyboardSettingAttribute))));
		IEnumerable<KeyboardEditorRow> rows = properties.Select<PropertyInfo,KeyboardEditorRow>(new Func<PropertyInfo, KeyboardEditorRow>((p) => {
			KeyboardEditorRow keyboardEditorRow = new KeyboardEditorRow();
			keyboardEditorRow.AssociatedSetting = p.Name;
			keyboardEditorRow.Description = Strings.ResourceManager.GetString(string.Concat(p.Name, "_Description"));
			keyboardEditorRow.Image = (Image)Images.ResourceManager.GetObject(string.Concat(p.Name, "_Image"));
			keyboardEditorRow.KeyInfo = KeyInfo.GetKeyInfo(ApplicationSettingsManager.Settings.GetValue<string>(p.Name));
			keyboardEditorRow.Text = Strings.ResourceManager.GetString(string.Concat(p.Name, "_Text"));
			return keyboardEditorRow;
		}
		));
		return rows;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && this.components != null)
		{
			this.components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		UITabPage[] uITabPageArray;
		int[] numArray;
		int[] numArray2;
		int[] numArray3;
		int[] numArray4;
		int[] numArray5;
		int[] numArray6;
		int[] numArray7;
		int[] numArray8;
		int[] numArray9;
		int[] numArray10;
		int[] numArray11;
		int[] numArray12;
		int[] numArray13;
		int[] numArray14;
		TreeNode[] treeNodeArray;
		ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsDialog));
		this.btnOK = new Button();
		this.btnCancel = new Button();
		this.uiTabs = new UITab();
		this.uiTabPageGeneral = new UITabPage();
		this.label13 = new Label();
		this.checkStartWithWindows = new CheckBox();
		this.checkStartMinimized = new CheckBox();
		this.checkHideWhenMinimized = new CheckBox();
		this.checkMinimizeWhenClosing = new CheckBox();
		this.uiTabPageDisplay = new UITabPage();
		this.checkShowDefaultTextInsteadOfEmptyLogMessage = new CheckBox();
		this.groupSourcesPanel = new GroupBox();
		this.checkSourcesPanelShowPath = new CheckBox();
		this.checkSourcesPanelShowNoUpdates = new CheckBox();
		this.checkSourcesPanelShowLastCheck = new CheckBox();
		this.checkSourcesPanelShowUrl = new CheckBox();
		this.label14 = new Label();
		this.numPreviewLines = new NumericUpDown();
		this.checkUnlimitedPageSize = new CheckBox();
		this.label8 = new Label();
		this.label4 = new Label();
		this.label10 = new Label();
		this.numPageSize = new NumericUpDown();
		this.uiTabPageOperation = new UITabPage();
		this.linkTextEditorBrowse = new LinkLabel();
		this.checkDismissErrorsWhenClicked = new CheckBox();
		this.checkPromptUpdateHeadRevision = new CheckBox();
		this.checkPromptUpdateOldRevision = new CheckBox();
		this.label15 = new Label();
		this.txtTextEditor = new TextBox();
		this.label11 = new Label();
		this.comboDefaultUserAction = new ComboBox();
		this.label6 = new Label();
		this.uiTabPageKeyboard = new UITabPage();
		this.keyboardEditor1 = new KeyboardEditor();
		this.label16 = new Label();
		this.uiTabPageUpdates = new UITabPage();
		this.lblWatchTheNetworkAvailability = new Label();
		this.label17 = new Label();
		this.checkWatchTheNetworkAvailability = new CheckBox();
		this.checkIgnoreIgnoreOnCommitConflicts = new CheckBox();
		this.checkIgnoreIgnoreOnCommit = new CheckBox();
		this.label1 = new Label();
		this.checkTreatUnversionedAsModified = new CheckBox();
		this.checkEnableUpdates = new CheckBox();
		this.checkAutomaticInterval = new CheckBox();
		this.numSecondsPerSource = new NumericUpDown();
		this.numUpdatesInterval = new NumericUpDown();
		this.label2 = new Label();
		this.uiTabPageTortoiseSVN = new UITabPage();
		this.linkTortoiseEXEBrowse = new LinkLabel();
		this.checkAutomaticallyResolveTortoiseSVNProcess = new CheckBox();
		this.numSVNUpdateSourcesQueueTimeoutSeconds = new NumericUpDown();
		this.label9 = new Label();
		this.checkSVNUpdateSourcesParallel = new CheckBox();
		this.checkIgnoreDisabledSourcesWhenUpdatingAll = new CheckBox();
		this.cmdTortoiseSVNSettings = new Button();
		this.label18 = new Label();
		this.comboAutoCloseTortoiseSVN = new ComboBox();
		this.label5 = new Label();
		this.linkAutoCheckTortoiseSVN = new LinkLabel();
		this.label3 = new Label();
		this.txtTortoiseEXE = new TextBox();
		this.uiTabPageRecommending = new UITabPage();
		this.uiGroupBoxRecommendMessage = new GroupBox();
		this.txtRecommendMessage = new TextBox();
		this.label19 = new Label();
		this.label22 = new Label();
		this.label21 = new Label();
		this.uiTabPageVersionCheck = new UITabPage();
		this.groupVersionChecks = new GroupBox();
		this.lblEnableVersionUpgrade = new Label();
		this.linkUsageInformationHelp = new LinkLabel();
		this.checkSendUsageInformation = new CheckBox();
		this.label7 = new Label();
		this.checkEnableVersionUpgrade = new CheckBox();
		this.numVersionCheckInterval = new NumericUpDown();
		this.checkVersionCheckAtStartup = new CheckBox();
		this.label20 = new Label();
		this.checkEnableVersionCheck = new CheckBox();
		this.panelBottom = new Panel();
		this.treeView1 = new TreeView();
		this.panelLeft = new Panel();
		this.panelRight = new Panel();
		this.checkCommitIsAlwaysEnabled = new CheckBox();
		this.uiTabs.BeginInit();
		this.uiTabs.SuspendLayout();
		this.uiTabPageGeneral.SuspendLayout();
		this.uiTabPageDisplay.SuspendLayout();
		this.groupSourcesPanel.SuspendLayout();
		this.numPreviewLines.BeginInit();
		this.numPageSize.BeginInit();
		this.uiTabPageOperation.SuspendLayout();
		this.uiTabPageKeyboard.SuspendLayout();
		this.uiTabPageUpdates.SuspendLayout();
		this.numSecondsPerSource.BeginInit();
		this.numUpdatesInterval.BeginInit();
		this.uiTabPageTortoiseSVN.SuspendLayout();
		this.numSVNUpdateSourcesQueueTimeoutSeconds.BeginInit();
		this.uiTabPageRecommending.SuspendLayout();
		this.uiGroupBoxRecommendMessage.SuspendLayout();
		this.uiTabPageVersionCheck.SuspendLayout();
		this.groupVersionChecks.SuspendLayout();
		this.numVersionCheckInterval.BeginInit();
		this.panelBottom.SuspendLayout();
		this.panelLeft.SuspendLayout();
		this.panelRight.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.btnOK, "btnOK");
		this.btnOK.DialogResult = DialogResult.OK;
		this.btnOK.Name = "btnOK";
		this.btnOK.add_Click(new EventHandler(this.btnOK_Click));
		resources.ApplyResources(this.btnCancel, "btnCancel");
		this.btnCancel.DialogResult = DialogResult.Cancel;
		this.btnCancel.Name = "btnCancel";
		this.uiTabs.BackColor = Color.Transparent;
		resources.ApplyResources(this.uiTabs, "uiTabs");
		this.uiTabs.Name = "uiTabs";
		this.uiTabs.ShowFocusRectangle = false;
		this.uiTabs.TabPages.AddRange(new UITabPage[] { this.uiTabPageGeneral, this.uiTabPageDisplay, this.uiTabPageOperation, this.uiTabPageKeyboard, this.uiTabPageUpdates, this.uiTabPageTortoiseSVN, this.uiTabPageRecommending, this.uiTabPageVersionCheck });
		this.uiTabs.TabStop = false;
		this.uiTabs.VisualStyle = TabVisualStyle.VS2005;
		this.uiTabPageGeneral.Controls.Add(this.label13);
		this.uiTabPageGeneral.Controls.Add(this.checkStartWithWindows);
		this.uiTabPageGeneral.Controls.Add(this.checkStartMinimized);
		this.uiTabPageGeneral.Controls.Add(this.checkHideWhenMinimized);
		this.uiTabPageGeneral.Controls.Add(this.checkMinimizeWhenClosing);
		this.uiTabPageGeneral.Key = "General";
		resources.ApplyResources(this.uiTabPageGeneral, "uiTabPageGeneral");
		this.uiTabPageGeneral.Name = "uiTabPageGeneral";
		this.uiTabPageGeneral.TabStop = true;
		this.label13.BackColor = Color.DimGray;
		this.label13.BorderStyle = BorderStyle.Fixed3D;
		resources.ApplyResources(this.label13, "label13");
		this.label13.ForeColor = Color.White;
		this.label13.Name = "label13";
		resources.ApplyResources(this.checkStartWithWindows, "checkStartWithWindows");
		this.checkStartWithWindows.BackColor = Color.Transparent;
		this.checkStartWithWindows.Checked = true;
		this.checkStartWithWindows.CheckState = CheckState.Checked;
		this.checkStartWithWindows.Name = "checkStartWithWindows";
		this.checkStartWithWindows.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkStartMinimized, "checkStartMinimized");
		this.checkStartMinimized.BackColor = Color.Transparent;
		this.checkStartMinimized.Checked = true;
		this.checkStartMinimized.CheckState = CheckState.Checked;
		this.checkStartMinimized.Name = "checkStartMinimized";
		this.checkStartMinimized.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkHideWhenMinimized, "checkHideWhenMinimized");
		this.checkHideWhenMinimized.BackColor = Color.Transparent;
		this.checkHideWhenMinimized.Checked = true;
		this.checkHideWhenMinimized.CheckState = CheckState.Checked;
		this.checkHideWhenMinimized.Name = "checkHideWhenMinimized";
		this.checkHideWhenMinimized.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkMinimizeWhenClosing, "checkMinimizeWhenClosing");
		this.checkMinimizeWhenClosing.BackColor = Color.Transparent;
		this.checkMinimizeWhenClosing.Checked = true;
		this.checkMinimizeWhenClosing.CheckState = CheckState.Checked;
		this.checkMinimizeWhenClosing.Name = "checkMinimizeWhenClosing";
		this.checkMinimizeWhenClosing.UseVisualStyleBackColor = true;
		this.uiTabPageDisplay.Controls.Add(this.checkShowDefaultTextInsteadOfEmptyLogMessage);
		this.uiTabPageDisplay.Controls.Add(this.groupSourcesPanel);
		this.uiTabPageDisplay.Controls.Add(this.label14);
		this.uiTabPageDisplay.Controls.Add(this.numPreviewLines);
		this.uiTabPageDisplay.Controls.Add(this.checkUnlimitedPageSize);
		this.uiTabPageDisplay.Controls.Add(this.label8);
		this.uiTabPageDisplay.Controls.Add(this.label4);
		this.uiTabPageDisplay.Controls.Add(this.label10);
		this.uiTabPageDisplay.Controls.Add(this.numPageSize);
		this.uiTabPageDisplay.Key = "Display";
		resources.ApplyResources(this.uiTabPageDisplay, "uiTabPageDisplay");
		this.uiTabPageDisplay.Name = "uiTabPageDisplay";
		this.uiTabPageDisplay.TabStop = true;
		resources.ApplyResources(this.checkShowDefaultTextInsteadOfEmptyLogMessage, "checkShowDefaultTextInsteadOfEmptyLogMessage");
		this.checkShowDefaultTextInsteadOfEmptyLogMessage.BackColor = Color.Transparent;
		this.checkShowDefaultTextInsteadOfEmptyLogMessage.Checked = true;
		this.checkShowDefaultTextInsteadOfEmptyLogMessage.CheckState = CheckState.Checked;
		this.checkShowDefaultTextInsteadOfEmptyLogMessage.Name = "checkShowDefaultTextInsteadOfEmptyLogMessage";
		this.checkShowDefaultTextInsteadOfEmptyLogMessage.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.groupSourcesPanel, "groupSourcesPanel");
		this.groupSourcesPanel.BackColor = Color.Transparent;
		this.groupSourcesPanel.Controls.Add(this.checkSourcesPanelShowPath);
		this.groupSourcesPanel.Controls.Add(this.checkSourcesPanelShowNoUpdates);
		this.groupSourcesPanel.Controls.Add(this.checkSourcesPanelShowLastCheck);
		this.groupSourcesPanel.Controls.Add(this.checkSourcesPanelShowUrl);
		this.groupSourcesPanel.Name = "groupSourcesPanel";
		this.groupSourcesPanel.TabStop = false;
		resources.ApplyResources(this.checkSourcesPanelShowPath, "checkSourcesPanelShowPath");
		this.checkSourcesPanelShowPath.Name = "checkSourcesPanelShowPath";
		this.checkSourcesPanelShowPath.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkSourcesPanelShowNoUpdates, "checkSourcesPanelShowNoUpdates");
		this.checkSourcesPanelShowNoUpdates.Name = "checkSourcesPanelShowNoUpdates";
		this.checkSourcesPanelShowNoUpdates.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkSourcesPanelShowLastCheck, "checkSourcesPanelShowLastCheck");
		this.checkSourcesPanelShowLastCheck.Name = "checkSourcesPanelShowLastCheck";
		this.checkSourcesPanelShowLastCheck.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkSourcesPanelShowUrl, "checkSourcesPanelShowUrl");
		this.checkSourcesPanelShowUrl.Name = "checkSourcesPanelShowUrl";
		this.checkSourcesPanelShowUrl.UseVisualStyleBackColor = true;
		this.label14.BackColor = Color.DimGray;
		this.label14.BorderStyle = BorderStyle.Fixed3D;
		resources.ApplyResources(this.label14, "label14");
		this.label14.ForeColor = Color.White;
		this.label14.Name = "label14";
		resources.ApplyResources(this.numPreviewLines, "numPreviewLines");
		this.numPreviewLines.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
		this.numPreviewLines.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		this.numPreviewLines.Name = "numPreviewLines";
		this.numPreviewLines.Value = new decimal(new int[] { 1, 0, 0, 0 });
		resources.ApplyResources(this.checkUnlimitedPageSize, "checkUnlimitedPageSize");
		this.checkUnlimitedPageSize.BackColor = Color.Transparent;
		this.checkUnlimitedPageSize.Checked = true;
		this.checkUnlimitedPageSize.CheckState = CheckState.Checked;
		this.checkUnlimitedPageSize.Name = "checkUnlimitedPageSize";
		this.checkUnlimitedPageSize.UseVisualStyleBackColor = true;
		this.checkUnlimitedPageSize.CheckedChanged += new EventHandler(this.checkUnlimitedPageSize_CheckedChanged);
		resources.ApplyResources(this.label8, "label8");
		this.label8.BackColor = Color.Transparent;
		this.label8.ForeColor = Color.MidnightBlue;
		this.label8.Name = "label8";
		resources.ApplyResources(this.label4, "label4");
		this.label4.BackColor = Color.Transparent;
		this.label4.Name = "label4";
		resources.ApplyResources(this.label10, "label10");
		this.label10.BackColor = Color.Transparent;
		this.label10.Name = "label10";
		resources.ApplyResources(this.numPageSize, "numPageSize");
		this.numPageSize.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
		this.numPageSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		this.numPageSize.Name = "numPageSize";
		this.numPageSize.Value = new decimal(new int[] { 1, 0, 0, 0 });
		this.uiTabPageOperation.Controls.Add(this.checkCommitIsAlwaysEnabled);
		this.uiTabPageOperation.Controls.Add(this.linkTextEditorBrowse);
		this.uiTabPageOperation.Controls.Add(this.checkDismissErrorsWhenClicked);
		this.uiTabPageOperation.Controls.Add(this.checkPromptUpdateHeadRevision);
		this.uiTabPageOperation.Controls.Add(this.checkPromptUpdateOldRevision);
		this.uiTabPageOperation.Controls.Add(this.label15);
		this.uiTabPageOperation.Controls.Add(this.txtTextEditor);
		this.uiTabPageOperation.Controls.Add(this.label11);
		this.uiTabPageOperation.Controls.Add(this.comboDefaultUserAction);
		this.uiTabPageOperation.Controls.Add(this.label6);
		this.uiTabPageOperation.Key = "Operation";
		resources.ApplyResources(this.uiTabPageOperation, "uiTabPageOperation");
		this.uiTabPageOperation.Name = "uiTabPageOperation";
		this.uiTabPageOperation.TabStop = true;
		resources.ApplyResources(this.linkTextEditorBrowse, "linkTextEditorBrowse");
		this.linkTextEditorBrowse.BackColor = Color.Transparent;
		this.linkTextEditorBrowse.Name = "linkTextEditorBrowse";
		this.linkTextEditorBrowse.TabStop = true;
		this.linkTextEditorBrowse.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkTextEditorBrowse_LinkClicked);
		resources.ApplyResources(this.checkDismissErrorsWhenClicked, "checkDismissErrorsWhenClicked");
		this.checkDismissErrorsWhenClicked.BackColor = Color.Transparent;
		this.checkDismissErrorsWhenClicked.Checked = true;
		this.checkDismissErrorsWhenClicked.CheckState = CheckState.Checked;
		this.checkDismissErrorsWhenClicked.Name = "checkDismissErrorsWhenClicked";
		this.checkDismissErrorsWhenClicked.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkPromptUpdateHeadRevision, "checkPromptUpdateHeadRevision");
		this.checkPromptUpdateHeadRevision.BackColor = Color.Transparent;
		this.checkPromptUpdateHeadRevision.Checked = true;
		this.checkPromptUpdateHeadRevision.CheckState = CheckState.Checked;
		this.checkPromptUpdateHeadRevision.Name = "checkPromptUpdateHeadRevision";
		this.checkPromptUpdateHeadRevision.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkPromptUpdateOldRevision, "checkPromptUpdateOldRevision");
		this.checkPromptUpdateOldRevision.BackColor = Color.Transparent;
		this.checkPromptUpdateOldRevision.Checked = true;
		this.checkPromptUpdateOldRevision.CheckState = CheckState.Checked;
		this.checkPromptUpdateOldRevision.Name = "checkPromptUpdateOldRevision";
		this.checkPromptUpdateOldRevision.UseVisualStyleBackColor = true;
		this.label15.BackColor = Color.DimGray;
		this.label15.BorderStyle = BorderStyle.Fixed3D;
		resources.ApplyResources(this.label15, "label15");
		this.label15.ForeColor = Color.White;
		this.label15.Name = "label15";
		resources.ApplyResources(this.txtTextEditor, "txtTextEditor");
		this.txtTextEditor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		this.txtTextEditor.AutoCompleteSource = AutoCompleteSource.FileSystem;
		this.txtTextEditor.Name = "txtTextEditor";
		resources.ApplyResources(this.label11, "label11");
		this.label11.BackColor = Color.Transparent;
		this.label11.Name = "label11";
		resources.ApplyResources(this.comboDefaultUserAction, "comboDefaultUserAction");
		this.comboDefaultUserAction.DisplayMember = "Description";
		this.comboDefaultUserAction.DropDownStyle = ComboBoxStyle.DropDownList;
		this.comboDefaultUserAction.Name = "comboDefaultUserAction";
		this.comboDefaultUserAction.ValueMember = "Value";
		resources.ApplyResources(this.label6, "label6");
		this.label6.BackColor = Color.Transparent;
		this.label6.Name = "label6";
		this.uiTabPageKeyboard.Controls.Add(this.keyboardEditor1);
		this.uiTabPageKeyboard.Controls.Add(this.label16);
		this.uiTabPageKeyboard.Key = "Keyboard";
		resources.ApplyResources(this.uiTabPageKeyboard, "uiTabPageKeyboard");
		this.uiTabPageKeyboard.Name = "uiTabPageKeyboard";
		this.uiTabPageKeyboard.TabStop = true;
		this.keyboardEditor1.BackColor = Color.Transparent;
		resources.ApplyResources(this.keyboardEditor1, "keyboardEditor1");
		this.keyboardEditor1.Name = "keyboardEditor1";
		this.label16.BackColor = Color.DimGray;
		this.label16.BorderStyle = BorderStyle.Fixed3D;
		resources.ApplyResources(this.label16, "label16");
		this.label16.ForeColor = Color.White;
		this.label16.Name = "label16";
		this.uiTabPageUpdates.Controls.Add(this.lblWatchTheNetworkAvailability);
		this.uiTabPageUpdates.Controls.Add(this.label17);
		this.uiTabPageUpdates.Controls.Add(this.checkWatchTheNetworkAvailability);
		this.uiTabPageUpdates.Controls.Add(this.checkIgnoreIgnoreOnCommitConflicts);
		this.uiTabPageUpdates.Controls.Add(this.checkIgnoreIgnoreOnCommit);
		this.uiTabPageUpdates.Controls.Add(this.label1);
		this.uiTabPageUpdates.Controls.Add(this.checkTreatUnversionedAsModified);
		this.uiTabPageUpdates.Controls.Add(this.checkEnableUpdates);
		this.uiTabPageUpdates.Controls.Add(this.checkAutomaticInterval);
		this.uiTabPageUpdates.Controls.Add(this.numSecondsPerSource);
		this.uiTabPageUpdates.Controls.Add(this.numUpdatesInterval);
		this.uiTabPageUpdates.Controls.Add(this.label2);
		this.uiTabPageUpdates.Key = "Sources";
		resources.ApplyResources(this.uiTabPageUpdates, "uiTabPageUpdates");
		this.uiTabPageUpdates.Name = "uiTabPageUpdates";
		this.uiTabPageUpdates.TabStop = true;
		resources.ApplyResources(this.lblWatchTheNetworkAvailability, "lblWatchTheNetworkAvailability");
		this.lblWatchTheNetworkAvailability.BackColor = Color.Transparent;
		this.lblWatchTheNetworkAvailability.ForeColor = Color.MidnightBlue;
		this.lblWatchTheNetworkAvailability.Name = "lblWatchTheNetworkAvailability";
		this.label17.BackColor = Color.DimGray;
		this.label17.BorderStyle = BorderStyle.Fixed3D;
		resources.ApplyResources(this.label17, "label17");
		this.label17.ForeColor = Color.White;
		this.label17.Name = "label17";
		resources.ApplyResources(this.checkWatchTheNetworkAvailability, "checkWatchTheNetworkAvailability");
		this.checkWatchTheNetworkAvailability.BackColor = Color.Transparent;
		this.checkWatchTheNetworkAvailability.Checked = true;
		this.checkWatchTheNetworkAvailability.CheckState = CheckState.Checked;
		this.checkWatchTheNetworkAvailability.Name = "checkWatchTheNetworkAvailability";
		this.checkWatchTheNetworkAvailability.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkIgnoreIgnoreOnCommitConflicts, "checkIgnoreIgnoreOnCommitConflicts");
		this.checkIgnoreIgnoreOnCommitConflicts.BackColor = Color.Transparent;
		this.checkIgnoreIgnoreOnCommitConflicts.Checked = true;
		this.checkIgnoreIgnoreOnCommitConflicts.CheckState = CheckState.Checked;
		this.checkIgnoreIgnoreOnCommitConflicts.Name = "checkIgnoreIgnoreOnCommitConflicts";
		this.checkIgnoreIgnoreOnCommitConflicts.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkIgnoreIgnoreOnCommit, "checkIgnoreIgnoreOnCommit");
		this.checkIgnoreIgnoreOnCommit.BackColor = Color.Transparent;
		this.checkIgnoreIgnoreOnCommit.Checked = true;
		this.checkIgnoreIgnoreOnCommit.CheckState = CheckState.Checked;
		this.checkIgnoreIgnoreOnCommit.Name = "checkIgnoreIgnoreOnCommit";
		this.checkIgnoreIgnoreOnCommit.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.label1, "label1");
		this.label1.BackColor = Color.Transparent;
		this.label1.ForeColor = Color.MidnightBlue;
		this.label1.Name = "label1";
		resources.ApplyResources(this.checkTreatUnversionedAsModified, "checkTreatUnversionedAsModified");
		this.checkTreatUnversionedAsModified.BackColor = Color.Transparent;
		this.checkTreatUnversionedAsModified.Checked = true;
		this.checkTreatUnversionedAsModified.CheckState = CheckState.Checked;
		this.checkTreatUnversionedAsModified.Name = "checkTreatUnversionedAsModified";
		this.checkTreatUnversionedAsModified.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkEnableUpdates, "checkEnableUpdates");
		this.checkEnableUpdates.BackColor = Color.Transparent;
		this.checkEnableUpdates.Checked = true;
		this.checkEnableUpdates.CheckState = CheckState.Checked;
		this.checkEnableUpdates.Name = "checkEnableUpdates";
		this.checkEnableUpdates.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkAutomaticInterval, "checkAutomaticInterval");
		this.checkAutomaticInterval.BackColor = Color.Transparent;
		this.checkAutomaticInterval.Checked = true;
		this.checkAutomaticInterval.CheckState = CheckState.Checked;
		this.checkAutomaticInterval.Name = "checkAutomaticInterval";
		this.checkAutomaticInterval.UseVisualStyleBackColor = true;
		this.checkAutomaticInterval.CheckedChanged += new EventHandler(this.checkAutomaticInterval_CheckedChanged);
		resources.ApplyResources(this.numSecondsPerSource, "numSecondsPerSource");
		this.numSecondsPerSource.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
		this.numSecondsPerSource.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		this.numSecondsPerSource.Name = "numSecondsPerSource";
		this.numSecondsPerSource.Value = new decimal(new int[] { 1, 0, 0, 0 });
		resources.ApplyResources(this.numUpdatesInterval, "numUpdatesInterval");
		this.numUpdatesInterval.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
		this.numUpdatesInterval.Name = "numUpdatesInterval";
		resources.ApplyResources(this.label2, "label2");
		this.label2.BackColor = Color.Transparent;
		this.label2.Name = "label2";
		this.uiTabPageTortoiseSVN.Controls.Add(this.linkTortoiseEXEBrowse);
		this.uiTabPageTortoiseSVN.Controls.Add(this.checkAutomaticallyResolveTortoiseSVNProcess);
		this.uiTabPageTortoiseSVN.Controls.Add(this.numSVNUpdateSourcesQueueTimeoutSeconds);
		this.uiTabPageTortoiseSVN.Controls.Add(this.label9);
		this.uiTabPageTortoiseSVN.Controls.Add(this.checkSVNUpdateSourcesParallel);
		this.uiTabPageTortoiseSVN.Controls.Add(this.checkIgnoreDisabledSourcesWhenUpdatingAll);
		this.uiTabPageTortoiseSVN.Controls.Add(this.cmdTortoiseSVNSettings);
		this.uiTabPageTortoiseSVN.Controls.Add(this.label18);
		this.uiTabPageTortoiseSVN.Controls.Add(this.comboAutoCloseTortoiseSVN);
		this.uiTabPageTortoiseSVN.Controls.Add(this.label5);
		this.uiTabPageTortoiseSVN.Controls.Add(this.linkAutoCheckTortoiseSVN);
		this.uiTabPageTortoiseSVN.Controls.Add(this.label3);
		this.uiTabPageTortoiseSVN.Controls.Add(this.txtTortoiseEXE);
		this.uiTabPageTortoiseSVN.Key = "TortoiseSVN";
		resources.ApplyResources(this.uiTabPageTortoiseSVN, "uiTabPageTortoiseSVN");
		this.uiTabPageTortoiseSVN.Name = "uiTabPageTortoiseSVN";
		this.uiTabPageTortoiseSVN.TabStop = true;
		resources.ApplyResources(this.linkTortoiseEXEBrowse, "linkTortoiseEXEBrowse");
		this.linkTortoiseEXEBrowse.BackColor = Color.Transparent;
		this.linkTortoiseEXEBrowse.Name = "linkTortoiseEXEBrowse";
		this.linkTortoiseEXEBrowse.TabStop = true;
		this.linkTortoiseEXEBrowse.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkTortoiseEXEBrowse_LinkClicked);
		resources.ApplyResources(this.checkAutomaticallyResolveTortoiseSVNProcess, "checkAutomaticallyResolveTortoiseSVNProcess");
		this.checkAutomaticallyResolveTortoiseSVNProcess.BackColor = Color.Transparent;
		this.checkAutomaticallyResolveTortoiseSVNProcess.Checked = true;
		this.checkAutomaticallyResolveTortoiseSVNProcess.CheckState = CheckState.Checked;
		this.checkAutomaticallyResolveTortoiseSVNProcess.Name = "checkAutomaticallyResolveTortoiseSVNProcess";
		this.checkAutomaticallyResolveTortoiseSVNProcess.UseVisualStyleBackColor = true;
		this.checkAutomaticallyResolveTortoiseSVNProcess.CheckedChanged += new EventHandler(this.checkAutomaticallyResolveTortoiseSVNProcess_CheckedChanged);
		resources.ApplyResources(this.numSVNUpdateSourcesQueueTimeoutSeconds, "numSVNUpdateSourcesQueueTimeoutSeconds");
		this.numSVNUpdateSourcesQueueTimeoutSeconds.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
		this.numSVNUpdateSourcesQueueTimeoutSeconds.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		this.numSVNUpdateSourcesQueueTimeoutSeconds.Name = "numSVNUpdateSourcesQueueTimeoutSeconds";
		this.numSVNUpdateSourcesQueueTimeoutSeconds.Value = new decimal(new int[] { 1, 0, 0, 0 });
		resources.ApplyResources(this.label9, "label9");
		this.label9.BackColor = Color.Transparent;
		this.label9.Name = "label9";
		resources.ApplyResources(this.checkSVNUpdateSourcesParallel, "checkSVNUpdateSourcesParallel");
		this.checkSVNUpdateSourcesParallel.BackColor = Color.Transparent;
		this.checkSVNUpdateSourcesParallel.Checked = true;
		this.checkSVNUpdateSourcesParallel.CheckState = CheckState.Checked;
		this.checkSVNUpdateSourcesParallel.Name = "checkSVNUpdateSourcesParallel";
		this.checkSVNUpdateSourcesParallel.UseVisualStyleBackColor = true;
		this.checkSVNUpdateSourcesParallel.CheckedChanged += new EventHandler(this.checkSVNUpdateSourcesParallel_CheckedChanged);
		resources.ApplyResources(this.checkIgnoreDisabledSourcesWhenUpdatingAll, "checkIgnoreDisabledSourcesWhenUpdatingAll");
		this.checkIgnoreDisabledSourcesWhenUpdatingAll.BackColor = Color.Transparent;
		this.checkIgnoreDisabledSourcesWhenUpdatingAll.Checked = true;
		this.checkIgnoreDisabledSourcesWhenUpdatingAll.CheckState = CheckState.Checked;
		this.checkIgnoreDisabledSourcesWhenUpdatingAll.Name = "checkIgnoreDisabledSourcesWhenUpdatingAll";
		this.checkIgnoreDisabledSourcesWhenUpdatingAll.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.cmdTortoiseSVNSettings, "cmdTortoiseSVNSettings");
		this.cmdTortoiseSVNSettings.Name = "cmdTortoiseSVNSettings";
		this.cmdTortoiseSVNSettings.add_Click(new EventHandler(this.cmdTortoiseSVNSettings_Click));
		this.label18.BackColor = Color.DimGray;
		this.label18.BorderStyle = BorderStyle.Fixed3D;
		resources.ApplyResources(this.label18, "label18");
		this.label18.ForeColor = Color.White;
		this.label18.Name = "label18";
		resources.ApplyResources(this.comboAutoCloseTortoiseSVN, "comboAutoCloseTortoiseSVN");
		this.comboAutoCloseTortoiseSVN.DisplayMember = "Description";
		this.comboAutoCloseTortoiseSVN.DropDownStyle = ComboBoxStyle.DropDownList;
		this.comboAutoCloseTortoiseSVN.Name = "comboAutoCloseTortoiseSVN";
		this.comboAutoCloseTortoiseSVN.ValueMember = "Value";
		resources.ApplyResources(this.label5, "label5");
		this.label5.BackColor = Color.Transparent;
		this.label5.Name = "label5";
		resources.ApplyResources(this.linkAutoCheckTortoiseSVN, "linkAutoCheckTortoiseSVN");
		this.linkAutoCheckTortoiseSVN.BackColor = Color.Transparent;
		this.linkAutoCheckTortoiseSVN.Name = "linkAutoCheckTortoiseSVN";
		this.linkAutoCheckTortoiseSVN.TabStop = true;
		this.linkAutoCheckTortoiseSVN.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkAutoCheckTortoiseSVN_LinkClicked);
		resources.ApplyResources(this.label3, "label3");
		this.label3.BackColor = Color.Transparent;
		this.label3.Name = "label3";
		resources.ApplyResources(this.txtTortoiseEXE, "txtTortoiseEXE");
		this.txtTortoiseEXE.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		this.txtTortoiseEXE.AutoCompleteSource = AutoCompleteSource.FileSystem;
		this.txtTortoiseEXE.Name = "txtTortoiseEXE";
		this.txtTortoiseEXE.add_Enter(new EventHandler(this.txtTortoiseEXE_Enter));
		this.uiTabPageRecommending.Controls.Add(this.uiGroupBoxRecommendMessage);
		this.uiTabPageRecommending.Controls.Add(this.label22);
		this.uiTabPageRecommending.Controls.Add(this.label21);
		this.uiTabPageRecommending.Key = "Recommending";
		resources.ApplyResources(this.uiTabPageRecommending, "uiTabPageRecommending");
		this.uiTabPageRecommending.Name = "uiTabPageRecommending";
		this.uiTabPageRecommending.TabStop = true;
		resources.ApplyResources(this.uiGroupBoxRecommendMessage, "uiGroupBoxRecommendMessage");
		this.uiGroupBoxRecommendMessage.BackColor = Color.Transparent;
		this.uiGroupBoxRecommendMessage.Controls.Add(this.txtRecommendMessage);
		this.uiGroupBoxRecommendMessage.Controls.Add(this.label19);
		this.uiGroupBoxRecommendMessage.Name = "uiGroupBoxRecommendMessage";
		this.uiGroupBoxRecommendMessage.TabStop = false;
		resources.ApplyResources(this.txtRecommendMessage, "txtRecommendMessage");
		this.txtRecommendMessage.Name = "txtRecommendMessage";
		resources.ApplyResources(this.label19, "label19");
		this.label19.BackColor = Color.Transparent;
		this.label19.ForeColor = Color.MidnightBlue;
		this.label19.Name = "label19";
		resources.ApplyResources(this.label22, "label22");
		this.label22.BackColor = Color.Transparent;
		this.label22.Name = "label22";
		this.label21.BackColor = Color.DimGray;
		this.label21.BorderStyle = BorderStyle.Fixed3D;
		resources.ApplyResources(this.label21, "label21");
		this.label21.ForeColor = Color.White;
		this.label21.Name = "label21";
		this.uiTabPageVersionCheck.Controls.Add(this.groupVersionChecks);
		this.uiTabPageVersionCheck.Controls.Add(this.label20);
		this.uiTabPageVersionCheck.Controls.Add(this.checkEnableVersionCheck);
		this.uiTabPageVersionCheck.Key = "VersionChecking";
		resources.ApplyResources(this.uiTabPageVersionCheck, "uiTabPageVersionCheck");
		this.uiTabPageVersionCheck.Name = "uiTabPageVersionCheck";
		this.uiTabPageVersionCheck.TabStop = true;
		resources.ApplyResources(this.groupVersionChecks, "groupVersionChecks");
		this.groupVersionChecks.BackColor = Color.Transparent;
		this.groupVersionChecks.Controls.Add(this.lblEnableVersionUpgrade);
		this.groupVersionChecks.Controls.Add(this.linkUsageInformationHelp);
		this.groupVersionChecks.Controls.Add(this.checkSendUsageInformation);
		this.groupVersionChecks.Controls.Add(this.label7);
		this.groupVersionChecks.Controls.Add(this.checkEnableVersionUpgrade);
		this.groupVersionChecks.Controls.Add(this.numVersionCheckInterval);
		this.groupVersionChecks.Controls.Add(this.checkVersionCheckAtStartup);
		this.groupVersionChecks.Name = "groupVersionChecks";
		this.groupVersionChecks.TabStop = false;
		resources.ApplyResources(this.lblEnableVersionUpgrade, "lblEnableVersionUpgrade");
		this.lblEnableVersionUpgrade.ForeColor = Color.Blue;
		this.lblEnableVersionUpgrade.Name = "lblEnableVersionUpgrade";
		resources.ApplyResources(this.linkUsageInformationHelp, "linkUsageInformationHelp");
		this.linkUsageInformationHelp.Name = "linkUsageInformationHelp";
		this.linkUsageInformationHelp.TabStop = true;
		this.linkUsageInformationHelp.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkUsageInformationHelp_LinkClicked);
		resources.ApplyResources(this.checkSendUsageInformation, "checkSendUsageInformation");
		this.checkSendUsageInformation.BackColor = Color.Transparent;
		this.checkSendUsageInformation.Checked = true;
		this.checkSendUsageInformation.CheckState = CheckState.Checked;
		this.checkSendUsageInformation.Name = "checkSendUsageInformation";
		this.checkSendUsageInformation.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.label7, "label7");
		this.label7.BackColor = Color.Transparent;
		this.label7.Name = "label7";
		resources.ApplyResources(this.checkEnableVersionUpgrade, "checkEnableVersionUpgrade");
		this.checkEnableVersionUpgrade.BackColor = Color.Transparent;
		this.checkEnableVersionUpgrade.Checked = true;
		this.checkEnableVersionUpgrade.CheckState = CheckState.Checked;
		this.checkEnableVersionUpgrade.Name = "checkEnableVersionUpgrade";
		this.checkEnableVersionUpgrade.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.numVersionCheckInterval, "numVersionCheckInterval");
		this.numVersionCheckInterval.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
		this.numVersionCheckInterval.Name = "numVersionCheckInterval";
		resources.ApplyResources(this.checkVersionCheckAtStartup, "checkVersionCheckAtStartup");
		this.checkVersionCheckAtStartup.BackColor = Color.Transparent;
		this.checkVersionCheckAtStartup.Checked = true;
		this.checkVersionCheckAtStartup.CheckState = CheckState.Checked;
		this.checkVersionCheckAtStartup.Name = "checkVersionCheckAtStartup";
		this.checkVersionCheckAtStartup.UseVisualStyleBackColor = true;
		this.label20.BackColor = Color.DimGray;
		this.label20.BorderStyle = BorderStyle.Fixed3D;
		resources.ApplyResources(this.label20, "label20");
		this.label20.ForeColor = Color.White;
		this.label20.Name = "label20";
		resources.ApplyResources(this.checkEnableVersionCheck, "checkEnableVersionCheck");
		this.checkEnableVersionCheck.BackColor = Color.Transparent;
		this.checkEnableVersionCheck.Checked = true;
		this.checkEnableVersionCheck.CheckState = CheckState.Checked;
		this.checkEnableVersionCheck.Name = "checkEnableVersionCheck";
		this.checkEnableVersionCheck.UseVisualStyleBackColor = true;
		this.checkEnableVersionCheck.CheckedChanged += new EventHandler(this.checkEnableVersionCheck_CheckedChanged);
		this.panelBottom.Controls.Add(this.btnOK);
		this.panelBottom.Controls.Add(this.btnCancel);
		resources.ApplyResources(this.panelBottom, "panelBottom");
		this.panelBottom.Name = "panelBottom";
		resources.ApplyResources(this.treeView1, "treeView1");
		this.treeView1.FullRowSelect = true;
		this.treeView1.HideSelection = false;
		this.treeView1.HotTracking = true;
		this.treeView1.ItemHeight = 18;
		this.treeView1.Name = "treeView1";
		this.treeView1.Nodes.AddRange(new TreeNode[] { (TreeNode)resources.GetObject("treeView1.Nodes"), (TreeNode)resources.GetObject("treeView1.Nodes1"), (TreeNode)resources.GetObject("treeView1.Nodes2"), (TreeNode)resources.GetObject("treeView1.Nodes3"), (TreeNode)resources.GetObject("treeView1.Nodes4"), (TreeNode)resources.GetObject("treeView1.Nodes5"), (TreeNode)resources.GetObject("treeView1.Nodes6"), (TreeNode)resources.GetObject("treeView1.Nodes7") });
		this.treeView1.ShowRootLines = false;
		this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
		this.panelLeft.Controls.Add(this.panelRight);
		this.panelLeft.Controls.Add(this.treeView1);
		resources.ApplyResources(this.panelLeft, "panelLeft");
		this.panelLeft.Name = "panelLeft";
		this.panelRight.Controls.Add(this.uiTabs);
		resources.ApplyResources(this.panelRight, "panelRight");
		this.panelRight.Name = "panelRight";
		resources.ApplyResources(this.checkCommitIsAlwaysEnabled, "checkCommitIsAlwaysEnabled");
		this.checkCommitIsAlwaysEnabled.BackColor = Color.Transparent;
		this.checkCommitIsAlwaysEnabled.Checked = true;
		this.checkCommitIsAlwaysEnabled.CheckState = CheckState.Checked;
		this.checkCommitIsAlwaysEnabled.Name = "checkCommitIsAlwaysEnabled";
		this.checkCommitIsAlwaysEnabled.UseVisualStyleBackColor = true;
		base.AcceptButton = this.btnOK;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.Controls.Add(this.panelLeft);
		base.Controls.Add(this.panelBottom);
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.Name = "SettingsDialog";
		base.ShowInTaskbar = false;
		this.uiTabs.EndInit();
		this.uiTabs.ResumeLayout(false);
		this.uiTabPageGeneral.ResumeLayout(false);
		this.uiTabPageGeneral.PerformLayout();
		this.uiTabPageDisplay.ResumeLayout(false);
		this.uiTabPageDisplay.PerformLayout();
		this.groupSourcesPanel.ResumeLayout(false);
		this.groupSourcesPanel.PerformLayout();
		this.numPreviewLines.EndInit();
		this.numPageSize.EndInit();
		this.uiTabPageOperation.ResumeLayout(false);
		this.uiTabPageOperation.PerformLayout();
		this.uiTabPageKeyboard.ResumeLayout(false);
		this.uiTabPageUpdates.ResumeLayout(false);
		this.uiTabPageUpdates.PerformLayout();
		this.numSecondsPerSource.EndInit();
		this.numUpdatesInterval.EndInit();
		this.uiTabPageTortoiseSVN.ResumeLayout(false);
		this.uiTabPageTortoiseSVN.PerformLayout();
		this.numSVNUpdateSourcesQueueTimeoutSeconds.EndInit();
		this.uiTabPageRecommending.ResumeLayout(false);
		this.uiGroupBoxRecommendMessage.ResumeLayout(false);
		this.uiGroupBoxRecommendMessage.PerformLayout();
		this.uiTabPageVersionCheck.ResumeLayout(false);
		this.uiTabPageVersionCheck.PerformLayout();
		this.groupVersionChecks.ResumeLayout(false);
		this.groupVersionChecks.PerformLayout();
		this.numVersionCheckInterval.EndInit();
		this.panelBottom.ResumeLayout(false);
		this.panelLeft.ResumeLayout(false);
		this.panelRight.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private static bool IsRegistryStartWithWindows()
	{
		RegistryKey key = RegistryHelper.OpenSubKey(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", false);
		using (key)
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
			bool isRegistryStartWithWindows = currentExePath.Equals(registryValue, StringComparison.OrdinalIgnoreCase);
			return isRegistryStartWithWindows;
		}
	}

	private void linkAutoCheckTortoiseSVN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Logger.LogUserAction();
		this.AutoCheckTortoiseSVN();
	}

	private void linkTextEditorBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Logger.LogUserAction();
		this.BrowseTextEditor();
	}

	private void linkTortoiseEXEBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Logger.LogUserAction();
		this.BrowseTortoiseSvnExe();
	}

	private void linkUsageInformationHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Logger.LogUserAction();
		this.ShowSendUsageInformationHelp();
	}

	protected override void OnFormClosed(FormClosedEventArgs e)
	{
		base.OnFormClosed(e);
		SettingsDialog.instance = null;
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		this.SetDefaultUserActionCombo();
		this.SetTortoiseSVNCombo();
		this.ReadSettings();
		this.uiTabs.ShowTabs = false;
		this.treeView1.SelectedNode = this.treeView1.Nodes[0];
		this.treeView1.Select();
	}

	private void ReadKeyboardSettings()
	{
		this.keyboardEditor1.List = this.CreateKeyboardDataSource();
	}

	private void ReadPageSize()
	{
		int size = ApplicationSettingsManager.Settings.LogEntriesPageSize;
		if (size < 0)
		{
			this.checkUnlimitedPageSize.Checked = true;
			this.numPageSize.Value = new decimal(100);
			this.numPageSize.Enabled = false;
			return;
		}
		this.checkUnlimitedPageSize.Checked = false;
		this.numPageSize.Value = decimal.op_Implicit(size);
	}

	private void ReadSettings()
	{
		this.checkStartWithWindows.Checked = SettingsDialog.TryIsRegistryStartWithWindows();
		this.checkHideWhenMinimized.Checked = ApplicationSettingsManager.Settings.HideWhenMinimized;
		this.checkStartMinimized.Checked = ApplicationSettingsManager.Settings.StartMinimized;
		this.checkMinimizeWhenClosing.Checked = ApplicationSettingsManager.Settings.MinimizeWhenClosing;
		this.ReadPageSize();
		this.numPreviewLines.Value = decimal.op_Implicit(ApplicationSettingsManager.Settings.PreviewRowLines);
		this.checkShowDefaultTextInsteadOfEmptyLogMessage.Checked = ApplicationSettingsManager.Settings.ShowDefaultTextInsteadOfEmptyLogMessage;
		this.checkSourcesPanelShowPath.Checked = ApplicationSettingsManager.Settings.SourcesPanelShowPath;
		this.checkSourcesPanelShowUrl.Checked = ApplicationSettingsManager.Settings.SourcesPanelShowUrl;
		this.checkSourcesPanelShowLastCheck.Checked = ApplicationSettingsManager.Settings.SourcesPanelShowLastCheck;
		this.checkSourcesPanelShowNoUpdates.Checked = ApplicationSettingsManager.Settings.SourcesPanelShowNoUpdates;
		this.comboDefaultUserAction.SelectedValue = Enum.Parse(Type.GetTypeFromHandle(UserAction), ApplicationSettingsManager.Settings.DefaultPathAction);
		this.txtTextEditor.Text = EnvironmentHelper.ExpandEnvironmentVariables(ApplicationSettingsManager.Settings.FileEditor);
		this.checkPromptUpdateOldRevision.Checked = ApplicationSettingsManager.Settings.PromptRollbackOldRevision;
		this.checkPromptUpdateHeadRevision.Checked = ApplicationSettingsManager.Settings.PromptUpdateHeadRevision;
		this.checkDismissErrorsWhenClicked.Checked = ApplicationSettingsManager.Settings.DismissErrorsWhenClicked;
		this.checkCommitIsAlwaysEnabled.Checked = ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled;
		this.ReadKeyboardSettings();
		this.checkEnableUpdates.Checked = ApplicationSettingsManager.Settings.EnableUpdates;
		this.checkAutomaticInterval.Checked = ApplicationSettingsManager.Settings.UpdatesInterval <= 0;
		this.checkTreatUnversionedAsModified.Checked = ApplicationSettingsManager.Settings.TreatUnversionedAsModified;
		this.checkIgnoreIgnoreOnCommit.Checked = ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommit;
		this.checkIgnoreIgnoreOnCommitConflicts.Checked = ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommitConflicts;
		this.checkWatchTheNetworkAvailability.Checked = ApplicationSettingsManager.Settings.WatchTheNetworkAvailability;
		this.numUpdatesInterval.Value = decimal.op_Implicit(ApplicationSettingsManager.Settings.UpdatesInterval);
		this.numUpdatesInterval.Enabled = !this.checkAutomaticInterval.Checked;
		this.numSecondsPerSource.Value = decimal.op_Implicit(ApplicationSettingsManager.Settings.UpdatesIntervalPerSource);
		this.numSecondsPerSource.Enabled = this.checkAutomaticInterval.Checked;
		this.txtTortoiseEXE.Text = EnvironmentHelper.ExpandEnvironmentVariables(ApplicationSettingsManager.Settings.TortoiseSVNPath);
		this.comboAutoCloseTortoiseSVN.SelectedValue = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
		this.checkIgnoreDisabledSourcesWhenUpdatingAll.Checked = ApplicationSettingsManager.Settings.IgnoreDisabledSourcesWhenUpdatingAll;
		this.checkAutomaticallyResolveTortoiseSVNProcess.Checked = ApplicationSettingsManager.Settings.AutomaticallyResolveTortoiseSVNProcess;
		bool flag1.set_Enabled(bool flag2 = flag1).Visible = flag2;
		this.checkSVNUpdateSourcesParallel.Checked = ApplicationSettingsManager.Settings.SVNUpdateSourcesParallel;
		this.numSVNUpdateSourcesQueueTimeoutSeconds.Value = decimal.op_Implicit(ApplicationSettingsManager.Settings.SVNUpdateSourcesQueueTimeoutSeconds);
		this.numSVNUpdateSourcesQueueTimeoutSeconds.Enabled = !this.checkSVNUpdateSourcesParallel.Checked;
		this.txtRecommendMessage.Text = ApplicationSettingsManager.Settings.RecommendationMessage;
		this.checkEnableVersionCheck.Checked = ApplicationSettingsManager.Settings.EnableVersionCheck;
		this.checkEnableVersionUpgrade.Checked = ApplicationSettingsManager.Settings.EnableVersionUpgrade;
		this.checkEnableVersionUpgrade.Enabled = ProcessHelper.IsRunningAsAdministrator();
		this.numVersionCheckInterval.Value = decimal.op_Implicit(ApplicationSettingsManager.Settings.VersionCheckInterval);
		this.checkVersionCheckAtStartup.Checked = ApplicationSettingsManager.Settings.VersionCheckAtStartup;
		this.checkSendUsageInformation.Checked = ApplicationSettingsManager.Settings.EnableSendingUsageInformation;
	}

	private void RefreshAutomaticUpdatesInterval()
	{
		this.numUpdatesInterval.Enabled = !this.checkAutomaticInterval.Checked;
		this.numSecondsPerSource.Enabled = this.checkAutomaticInterval.Checked;
		if (this.numUpdatesInterval.Enabled && this.numUpdatesInterval.Value == new decimal(0))
		{
			int minutes = Status.EnabledSourcesCount;
			if (minutes == 0)
			{
				minutes = 1;
			}
			this.numUpdatesInterval.Value = decimal.op_Implicit(minutes);
		}
	}

	private void SetDefaultUserActionCombo()
	{
		List<object> items = new List<object>();
		Dictionary<UserAction, string> dict = SVNPathCommands.GetAvailableUserActions();
		foreach (UserAction action in dict.Keys)
		{
			items.Add(new <>f__AnonymousType0<string, UserAction>(dict[action], action));
		}
		this.comboDefaultUserAction.DataSource = items;
	}

	private static void SetRegistryStartWithWindows(bool start)
	{
		if (start)
		{
			string currentExePath = Application.ExecutablePath;
			RegistryHelper.SetValue(string.Format("{0}\\{1}", Registry.CurrentUser.Name, "Software\\Microsoft\\Windows\\CurrentVersion\\Run"), "SVNMonitor", currentExePath);
			return;
		}
		RegistryKey key = RegistryHelper.OpenSubKey(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
		using (key)
		{
			if (key != null)
			{
				key.DeleteValue("SVNMonitor", false);
			}
		}
	}

	private void SetSelectedTab(TreeNode node)
	{
		this.uiTabs.SelectedTab = this.uiTabs.TabPages[node.Tag.ToString()];
	}

	private void SetTortoiseSVNCombo()
	{
		List<object> items = new List<object>();
		foreach (TortoiseSVNAutoClose @value in Enum.GetValues(typeof(TortoiseSVNAutoClose)))
		{
			string description = EnumHelper.TranslateEnumValue<TortoiseSVNAutoClose>(@value);
			items.Add(new <>f__AnonymousType0<string, TortoiseSVNAutoClose>(description, @value));
		}
		this.comboAutoCloseTortoiseSVN.DataSource = items;
	}

	private void SetVersionCheck()
	{
		bool oldValue = ApplicationSettingsManager.Settings.EnableVersionCheck;
		bool newValue = this.checkEnableVersionCheck.Checked;
		ApplicationSettingsManager.Settings.EnableVersionCheck = newValue;
		if (!newValue)
		{
			EventLog.LogWarning(Strings.WarningVersionChecksDisabled, this);
			return;
		}
		if (oldValue != newValue)
		{
			EventLog.LogInfo(Strings.WarningVersionChecksEnabled, this);
		}
	}

	private void SetVersionUpgrade()
	{
		bool oldValue = ApplicationSettingsManager.Settings.EnableVersionUpgrade;
		bool newValue = this.checkEnableVersionUpgrade.Checked;
		if (!newValue)
		{
			EventLog.LogWarning(Strings.WarningVersionUpgradesDisabled, this);
			return;
		}
		if (oldValue != newValue)
		{
			EventLog.LogInfo(Strings.WarningVersionUpgradesEnabled, this);
		}
	}

	internal static void ShowInstanceDialog()
	{
		if (SettingsDialog.instance == null)
		{
			SettingsDialog.instance = new SettingsDialog();
			SettingsDialog.instance.ShowDialog();
			return;
		}
		SettingsDialog.instance.Activate();
	}

	private void ShowSendUsageInformationHelp()
	{
		UsageInformationHelp.ShowUsageInformationHelp();
	}

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		Logger.LogUserAction();
		this.SetSelectedTab(e.Node);
	}

	private static bool TryIsRegistryStartWithWindows()
	{
		try
		{
			return SettingsDialog.IsRegistryStartWithWindows();
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
			SettingsDialog.SetRegistryStartWithWindows(start);
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
		if (string.IsNullOrEmpty(this.txtTortoiseEXE.Text))
		{
			this.BrowseTortoiseSvnExe();
		}
	}

	private void WriteKeyboardSettings()
	{
		foreach (KeyboardEditorRow row in this.keyboardEditor1.List)
		{
			ApplicationSettingsManager.Settings.SetValue(row.AssociatedSetting, row.KeyInfo.ToString());
		}
	}

	private void WriteLogEntriesPageSize()
	{
		int oldValue = ApplicationSettingsManager.Settings.LogEntriesPageSize;
		int newValue = (this.checkUnlimitedPageSize.Checked ? -1 : decimal.op_Explicit(this.numPageSize.Value));
		if (oldValue != newValue)
		{
			Logger.Log.InfoFormat("Page size has been changed from {0} to {1}.", oldValue, newValue);
			MessageBox.Show(Strings.PageSizeChangedUseRefreshLog, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		ApplicationSettingsManager.Settings.LogEntriesPageSize = newValue;
	}

	private void WriteSettings()
	{
		SettingsDialog.TrySetRegistryStartWithWindows(this.checkStartWithWindows.Checked);
		ApplicationSettingsManager.Settings.HideWhenMinimized = this.checkHideWhenMinimized.Checked;
		ApplicationSettingsManager.Settings.StartMinimized = this.checkStartMinimized.Checked;
		ApplicationSettingsManager.Settings.MinimizeWhenClosing = this.checkMinimizeWhenClosing.Checked;
		this.WriteLogEntriesPageSize();
		ApplicationSettingsManager.Settings.PreviewRowLines = decimal.op_Explicit(this.numPreviewLines.Value);
		ApplicationSettingsManager.Settings.ShowDefaultTextInsteadOfEmptyLogMessage = this.checkShowDefaultTextInsteadOfEmptyLogMessage.Checked;
		ApplicationSettingsManager.Settings.SourcesPanelShowPath = this.checkSourcesPanelShowPath.Checked;
		ApplicationSettingsManager.Settings.SourcesPanelShowUrl = this.checkSourcesPanelShowUrl.Checked;
		ApplicationSettingsManager.Settings.SourcesPanelShowLastCheck = this.checkSourcesPanelShowLastCheck.Checked;
		ApplicationSettingsManager.Settings.SourcesPanelShowNoUpdates = this.checkSourcesPanelShowNoUpdates.Checked;
		ApplicationSettingsManager.Settings.DefaultPathAction = this.comboDefaultUserAction.SelectedValue.ToString();
		ApplicationSettingsManager.Settings.PromptRollbackOldRevision = this.checkPromptUpdateOldRevision.Checked;
		ApplicationSettingsManager.Settings.PromptUpdateHeadRevision = this.checkPromptUpdateHeadRevision.Checked;
		ApplicationSettingsManager.Settings.DismissErrorsWhenClicked = this.checkDismissErrorsWhenClicked.Checked;
		ApplicationSettingsManager.Settings.CommitIsAlwaysEnabled = this.checkCommitIsAlwaysEnabled.Checked;
		ApplicationSettingsManager.Settings.FileEditor = this.txtTextEditor.Text.Trim();
		this.WriteKeyboardSettings();
		ApplicationSettingsManager.Settings.EnableUpdates = this.checkEnableUpdates.Checked;
		ApplicationSettingsManager.Settings.TreatUnversionedAsModified = this.checkTreatUnversionedAsModified.Checked;
		ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommit = this.checkIgnoreIgnoreOnCommit.Checked;
		ApplicationSettingsManager.Settings.IgnoreIgnoreOnCommitConflicts = this.checkIgnoreIgnoreOnCommitConflicts.Checked;
		ApplicationSettingsManager.Settings.WatchTheNetworkAvailability = this.checkWatchTheNetworkAvailability.Checked;
		ApplicationSettingsManager.Settings.UpdatesInterval = (this.checkAutomaticInterval.Checked ? 0 : decimal.op_Explicit(this.numUpdatesInterval.Value));
		ApplicationSettingsManager.Settings.UpdatesIntervalPerSource = decimal.op_Explicit(this.numSecondsPerSource.Value);
		ApplicationSettingsManager.Settings.TortoiseSVNPath = this.txtTortoiseEXE.Text.Trim();
		ApplicationSettingsManager.Settings.TortoiseSVNAutoClose = (int)this.comboAutoCloseTortoiseSVN.SelectedValue;
		ApplicationSettingsManager.Settings.IgnoreDisabledSourcesWhenUpdatingAll = this.checkIgnoreDisabledSourcesWhenUpdatingAll.Checked;
		ApplicationSettingsManager.Settings.AutomaticallyResolveTortoiseSVNProcess = this.checkAutomaticallyResolveTortoiseSVNProcess.Checked;
		ApplicationSettingsManager.Settings.SVNUpdateSourcesParallel = this.checkSVNUpdateSourcesParallel.Checked;
		ApplicationSettingsManager.Settings.SVNUpdateSourcesQueueTimeoutSeconds = decimal.op_Explicit(this.numSVNUpdateSourcesQueueTimeoutSeconds.Value);
		ApplicationSettingsManager.Settings.RecommendationMessage = this.txtRecommendMessage.Text;
		this.SetVersionCheck();
		this.SetVersionUpgrade();
		ApplicationSettingsManager.Settings.VersionCheckInterval = decimal.op_Explicit(this.numVersionCheckInterval.Value);
		ApplicationSettingsManager.Settings.EnableSendingUsageInformation = this.checkSendUsageInformation.Checked;
		ApplicationSettingsManager.Settings.VersionCheckAtStartup = this.checkVersionCheckAtStartup.Checked;
		ApplicationSettingsManager.SaveSettings();
	}
}
}