using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using SVNMonitor.Settings.Validation;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using SVNMonitor.Support;
using SVNMonitor.Logging;
using SVNMonitor.Helpers;
using System.Xml.Schema;
using System.Xml;

namespace SVNMonitor.Settings
{
[Serializable]
public class ApplicationSettings : IXmlSerializable
{
	private Dictionary<string, object> values;

	[ApplicationSettingsValue(true, Description="Automatically locate TortoiseSVN's executable before each command")]
	public bool AutomaticallyResolveTortoiseSVNProcess
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Always enable the Commit buttons")]
	public bool CommitIsAlwaysEnabled
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[EnumConfigValidator(typeof(UserAction))]
	[ApplicationSettingsValue("Diff", Description="The default action to perform when double-clicking a path in the grid")]
	public string DefaultPathAction
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Dismiss (clear) Sources and Monitors errors when clicked.")]
	public bool DismissErrorsWhenClicked
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Enable sending anonymous usage information such as sources and monitors count and application settings.")]
	public bool EnableSendingUsageInformation
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Enable automatic checking for updates")]
	public bool EnableUpdates
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Enable checking for new SVN-Monitor versions")]
	public bool EnableVersionCheck
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[AdministratorConfigValidator(false)]
	[ApplicationSettingsValue(true, Description="Enable auto-upgrading to new SVN-Monitor versions")]
	public bool EnableVersionUpgrade
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.More, 0)]
	[ApplicationSettingsValue(5, Description="Event-Log reminders interval, in minutes.")]
	public int EventLogRemindersTimerInterval
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[FileNameConfigValidator]
	[ApplicationSettingsValue("Notepad", Description="File editor")]
	public string FileEditor
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Hide the main window when minimizing")]
	public bool HideWhenMinimized
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Ignore disabled sources when updating all using TortoiseSVN.")]
	public bool IgnoreDisabledSourcesWhenUpdatingAll
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Ignore items that are in the ignore-on-commit changelist")]
	public bool IgnoreIgnoreOnCommit
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Ignore conflicted items that are in the ignore-on-commit changelist")]
	public bool IgnoreIgnoreOnCommitConflicts
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(typeof(DefaultGuidProvider))]
	public string InstanceID
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[KeyboardSetting]
	[KeyboardShortcutConfigValidator]
	[ApplicationSettingsValue("Control+Win+C", Description="Shortcut key for checking all modifications")]
	public string KeyboardShortcutCheckModifications
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[KeyboardShortcutConfigValidator]
	[KeyboardSetting]
	[ApplicationSettingsValue("Control+Win+S", Description="Shortcut key for checking all sources for updates")]
	public string KeyboardShortcutCheckSources
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[KeyboardShortcutConfigValidator]
	[ApplicationSettingsValue("Win+S", Description="Shortcut key for activating the main window")]
	[KeyboardSetting]
	public string KeyboardShortcutShowMainWindow
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[KeyboardShortcutConfigValidator]
	[KeyboardSetting]
	[ApplicationSettingsValue("Control+Win+U", Description="Shortcut key for updating all available sources")]
	public string KeyboardShortcutUpdateAllAvailable
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(100, Description="Maximum number of log-entries to show in the grid (-1 = unlimited)")]
	[NumberConfigValidator(ValidationOperator.MoreOrEqual, -1)]
	public int LogEntriesPageSize
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(100, Description="X-Location of the Log-Entry Details Dialog")]
	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
	public int LogEntryDetailsDialogLocationX
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(100, Description="Y-Location of the Log-Entry Details Dialog")]
	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
	public int LogEntryDetailsDialogLocationY
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(470, Description="Height of the Log-Entry Details Dialog")]
	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 470)]
	public int LogEntryDetailsDialogSizeHeight
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(420, Description="Width of the Log-Entry Details Dialog")]
	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 420)]
	public int LogEntryDetailsDialogSizeWidth
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 50)]
	[ApplicationSettingsValue(150, Description="Splitter distance of the Log-Entry Details Dialog")]
	public int LogEntryDetailsDialogSplitterDistance
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Show the group-by box above the log grid")]
	public bool LogGroupByBox
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Minimize the main window when closing")]
	public bool MinimizeWhenClosing
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(3, Description="Number of preview rows")]
	[NumberConfigValidator(ValidationOperator.More, 1)]
	public int PreviewRowLines
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Prompt when updating or rolling-back to an older revision than the current source's revision.")]
	public bool PromptRollbackOldRevision
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Prompt when updating to the HEAD revision.")]
	public bool PromptUpdateHeadRevision
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue("Recommended revision ${REVISION}", Description="The default log-message when recommending a revision.")]
	public string RecommendationMessage
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Show 'No Comment' instead of empty log messages")]
	public bool ShowDefaultTextInsteadOfEmptyLogMessage
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Show only enabled sources")]
	public bool SourcesFilter_Enabled
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Show only modified sources")]
	public bool SourcesFilter_Modified
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Show only not up-to-date sources")]
	public bool SourcesFilter_NotUpToDate
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Show the 'Last Check' item in the Sources panel")]
	public bool SourcesPanelShowLastCheck
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Show the 'No Updates' item in the Sources panel")]
	public bool SourcesPanelShowNoUpdates
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Show the Path item in the Sources panel")]
	public bool SourcesPanelShowPath
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Show the URL item in the Sources panel")]
	public bool SourcesPanelShowUrl
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Start the application minimized")]
	public bool StartMinimized
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="When updating all sources (using TortoiseSVN) - do it in parallel.")]
	public bool SVNUpdateSourcesParallel
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.More, 0)]
	[ApplicationSettingsValue(10, Description="When not updating sources in parallel - set a waiting timeout (seconds) for each iteration.")]
	public int SVNUpdateSourcesQueueTimeoutSeconds
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(3, Description="Default auto-close behavior of the TortoiseSVN:\n\t0: don't close the dialog automatically\n\t1: Auto close if no errors\n\t2: Auto close if no errors and conflicts\n\t3: Auto close if no errors, conflicts and merges\n\t4: Auto close if no errors, conflicts and merges for local operations")]
	[NumberConfigValidator(ValidationOperator.LessOrEqual, 4)]
	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
	public int TortoiseSVNAutoClose
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[FileNameConfigValidator]
	[ApplicationSettingsValue("%PROGRAMFILES%\\TortoiseSVN\\bin\\TortoiseProc.exe", Description="Location of the TortoiseSVN process (TortoiseProc.exe)")]
	public string TortoiseSVNPath
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Treat unversioned items as modified")]
	public bool TreatUnversionedAsModified
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue("", IsCDATA=true, Description="Layout of the Events grid")]
	[IgnoreWebService]
	[GridLayoutConfigValidator]
	public string UIEventLogGridLayout
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue("en", Description="User-Interface language.")]
	[CultureConfigValidator]
	public string UILanguage
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[IgnoreWebService]
	[GridLayoutConfigValidator]
	[ApplicationSettingsValue("", IsCDATA=true, Description="Layout of the Log grid")]
	public string UILogEntriesGridLayout
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.More, 100)]
	[ApplicationSettingsValue(600, Description="Height of the main window")]
	public int UIMainFormHeight
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
	[ApplicationSettingsValue(20, Description="X-Location of the main window")]
	public int UIMainFormLocationX
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(20, Description="Y-Location of the main window")]
	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
	public int UIMainFormLocationY
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(800, Description="Width of the main window")]
	[NumberConfigValidator(ValidationOperator.More, 100)]
	public int UIMainFormWidth
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue("Normal", Description="State of the main window")]
	[EnumConfigValidator(typeof(FormWindowState))]
	public string UIMainFormWindowState
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Auto-Hide the Events panel")]
	public bool UIPanelEventLogAutoHide
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(200, Description="Height of the Events panel")]
	[NumberConfigValidator(ValidationOperator.More, 100)]
	public int UIPanelEventLogHeight
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.More, 100)]
	[ApplicationSettingsValue(300, Description="Width of the left panel (Sources and Monitors)")]
	public int UIPanelLeftWidth
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Auto-Hide the Monitors panel")]
	public bool UIPanelMonitiorsAutoHide
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.More, 100)]
	[ApplicationSettingsValue(200, Description="Height of the Monitors panel")]
	public int UIPanelMonitiorsHeight
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Auto-Hide the Paths panel")]
	public bool UIPanelPathsAutoHide
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(200, Description="Height of the Paths panel")]
	[NumberConfigValidator(ValidationOperator.More, 100)]
	public int UIPanelPathsHeight
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(false, Description="Auto-Hide the Sources panel")]
	public bool UIPanelSourcesAutoHide
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue("", IsCDATA=true, Description="Layout of the Paths grid")]
	[IgnoreWebService]
	[GridLayoutConfigValidator]
	public string UIPathsGridLayout
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
	[ApplicationSettingsValue(0, Description="The interval (in minutes) between each update-check queue. 0 means automatic (UpdatesIntervalPerSource per source)")]
	[NumberConfigValidator(ValidationOperator.LessOrEqual, 1440)]
	public int UpdatesInterval
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[NumberConfigValidator(ValidationOperator.LessOrEqual, 3600)]
	[ApplicationSettingsValue(60, Description="The interval (in seconds) per source when UpdatesInterval is zero.")]
	[NumberConfigValidator(ValidationOperator.More, 0)]
	public int UpdatesIntervalPerSource
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue("1/1/0001 12:00:00 AM", Description="Date and time of latest usage info send")]
	public string UsageSendDate
	{
		get
		{
			return this.GetValue<string>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="[OBSOLETE(Always true)] Use TortoiseSVN's global-ignore-pattern")]
	public bool UseTortoiseSVNGlobalIgnorePattern
	{
		get
		{
			return true;
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Check for new SVN-Monitor versions when the application starts")]
	public bool VersionCheckAtStartup
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(60, Description="The interval (in minutes) between automatic checking of new SVN-Monitor versions")]
	[NumberConfigValidator(ValidationOperator.More, 0)]
	[NumberConfigValidator(ValidationOperator.LessOrEqual, 1440)]
	public int VersionCheckInterval
	{
		get
		{
			return this.GetValue<int>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	[ApplicationSettingsValue(true, Description="Watch the network availability")]
	public bool WatchTheNetworkAvailability
	{
		get
		{
			return this.GetValue<bool>();
		}
		set
		{
			this.SetValue(value);
		}
	}

	public ApplicationSettings()
	{
		this.values = new Dictionary<string, object>();
	}

	internal static string GetSettingKeyName()
	{
		StackFrame[] frames = new StackTrace().GetFrames();
		StackFrame[] stackFrameArray = frames;
		foreach (StackFrame frame in stackFrameArray)
		{
			MethodBase method = frame.GetMethod();
			string name = method.Name;
			if (name.StartsWith("get_") || name.StartsWith("set_"))
			{
				name = name.Substring("get_".Length);
				PropertyInfo property = typeof(ApplicationSettings).GetProperty(name);
				if (Attribute.IsDefined(property, typeof(ApplicationSettingsValueAttribute)))
				{
					return name;
				}
			}
		}
		return null;
	}

	public string GetUsageInformation()
	{
		StringBuilder sb = new StringBuilder();
		foreach (KeyValuePair<string, object> kvp in this.values)
		{
			if (!this.IgnoreWebService(kvp.Key))
			{
				sb.AppendFormat("ApplicationSettings_{0}={1}{2}", kvp.Key, kvp.Value, UsageInformationSender.Separator);
			}
		}
		return sb.ToString();
	}

	private T GetValue<T>()
	{
		string key = ApplicationSettings.GetSettingKeyName();
		T @value = this.GetValue<T>(key);
		return @value;
	}

	public T GetValue<T>(string key)
	{
		if (!this.values.ContainsKey(key))
		{
			object defaultValue = ApplicationSettingsManager.GetDefault<T>(key);
			this.values.Item = key;
		}
		object @value = this.values[key];
		return (T)@value;
	}

	private bool IgnoreWebService(string key)
	{
		bool ignore = false;
		try
		{
			PropertyInfo property = this.GetType().GetProperty(key);
			return Attribute.IsDefined(property, typeof(IgnoreWebServiceAttribute));
		}
		catch (Exception ex)
		{
			Logger.Log.Error(ex.ToString());
		}
		return ignore;
	}

	internal static ApplicationSettings Load(string FullFileName)
	{
		ApplicationSettings settings = SerializationHelper.XmlFileDeserialize<ApplicationSettings>(FullFileName);
		return settings;
	}

	internal void Save(string fileName)
	{
		SerializationHelper.XmlFileSerialize<ApplicationSettings>(this, fileName);
	}

	internal void SetValue(object value)
	{
		string key = ApplicationSettings.GetSettingKeyName();
		this.SetValue(key, value);
	}

	internal void SetValue(string key, object value)
	{
		Logger.Log.InfoFormat("{0} = {1}", key, value);
		this.values.Item = key;
	}

	private XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema()
	{
		return null;
	}

	private void System.Xml.Serialization.IXmlSerializable.ReadXml(XmlReader reader)
	{
		ApplicationSettingsManager.ReadXml(reader, this);
	}

	private void System.Xml.Serialization.IXmlSerializable.WriteXml(XmlWriter writer)
	{
		ApplicationSettingsManager.WriteXml(writer, this);
	}

	public string ToXml()
	{
		string xml = SerializationHelper.XmlSerialize<ApplicationSettings>(this);
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(xml);
		return doc.DocumentElement.OuterXml;
	}
}
}