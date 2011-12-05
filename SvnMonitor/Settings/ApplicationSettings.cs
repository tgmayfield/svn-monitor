namespace SVNMonitor.Settings
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Settings.DefaultProviders;
    using SVNMonitor.Settings.Validation;
    using SVNMonitor.Support;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable]
    public class ApplicationSettings : IXmlSerializable
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        internal static string GetSettingKeyName()
        {
            foreach (StackFrame frame in new StackTrace().GetFrames())
            {
                string name = frame.GetMethod().Name;
                if (name.StartsWith("get_") || name.StartsWith("set_"))
                {
                    name = name.Substring("get_".Length);
                    if (Attribute.IsDefined(typeof(ApplicationSettings).GetProperty(name), typeof(ApplicationSettingsValueAttribute)))
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
            string key = GetSettingKeyName();
            return this.GetValue<T>(key);
        }

        public T GetValue<T>(string key)
        {
            if (!this.values.ContainsKey(key))
            {
                object defaultValue = ApplicationSettingsManager.GetDefault<T>(key);
                this.values[key] = defaultValue;
            }
            object value = this.values[key];
            return (T) value;
        }

        private bool IgnoreWebService(string key)
        {
            bool ignore = false;
            try
            {
                ignore = Attribute.IsDefined(base.GetType().GetProperty(key), typeof(IgnoreWebServiceAttribute));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.ToString());
            }
            return ignore;
        }

        internal static ApplicationSettings Load(string FullFileName)
        {
            return SerializationHelper.XmlFileDeserialize<ApplicationSettings>(FullFileName);
        }

        internal void Save(string fileName)
        {
            SerializationHelper.XmlFileSerialize<ApplicationSettings>(this, fileName);
        }

        internal void SetValue(object value)
        {
            string key = GetSettingKeyName();
            this.SetValue(key, value);
        }

        internal void SetValue(string key, object value)
        {
            Logger.Log.InfoFormat("{0} = {1}", key, value);
            this.values[key] = value;
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            ApplicationSettingsManager.ReadXml(reader, this);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
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

        [ApplicationSettingsValue(true, Description="Automatically locate TortoiseSVN's executable before each command")]
        public bool AutomaticallyResolveTortoiseSVNProcess
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Always enable the Commit buttons")]
        public bool CommitIsAlwaysEnabled
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [EnumConfigValidator(typeof(UserAction)), ApplicationSettingsValue("Diff", Description="The default action to perform when double-clicking a path in the grid")]
        public string DefaultPathAction
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Dismiss (clear) Sources and Monitors errors when clicked.")]
        public bool DismissErrorsWhenClicked
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Enable sending anonymous usage information such as sources and monitors count and application settings.")]
        public bool EnableSendingUsageInformation
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Enable automatic checking for updates")]
        public bool EnableUpdates
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Enable checking for new SVN-Monitor versions")]
        public bool EnableVersionCheck
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [AdministratorConfigValidator(false), ApplicationSettingsValue(true, Description="Enable auto-upgrading to new SVN-Monitor versions")]
        public bool EnableVersionUpgrade
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.More, 0), ApplicationSettingsValue(5, Description="Event-Log reminders interval, in minutes.")]
        public int EventLogRemindersTimerInterval
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [FileNameConfigValidator, ApplicationSettingsValue("Notepad", Description="File editor")]
        public string FileEditor
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Hide the main window when minimizing")]
        public bool HideWhenMinimized
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Ignore disabled sources when updating all using TortoiseSVN.")]
        public bool IgnoreDisabledSourcesWhenUpdatingAll
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Ignore items that are in the ignore-on-commit changelist")]
        public bool IgnoreIgnoreOnCommit
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Ignore conflicted items that are in the ignore-on-commit changelist")]
        public bool IgnoreIgnoreOnCommitConflicts
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(typeof(DefaultGuidProvider))]
        public string InstanceID
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [KeyboardSetting, KeyboardShortcutConfigValidator, ApplicationSettingsValue("Control+Win+C", Description="Shortcut key for checking all modifications")]
        public string KeyboardShortcutCheckModifications
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [KeyboardShortcutConfigValidator, KeyboardSetting, ApplicationSettingsValue("Control+Win+S", Description="Shortcut key for checking all sources for updates")]
        public string KeyboardShortcutCheckSources
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [KeyboardShortcutConfigValidator, ApplicationSettingsValue("Win+S", Description="Shortcut key for activating the main window"), KeyboardSetting]
        public string KeyboardShortcutShowMainWindow
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [KeyboardShortcutConfigValidator, KeyboardSetting, ApplicationSettingsValue("Control+Win+U", Description="Shortcut key for updating all available sources")]
        public string KeyboardShortcutUpdateAllAvailable
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(100, Description="Maximum number of log-entries to show in the grid (-1 = unlimited)"), NumberConfigValidator(ValidationOperator.MoreOrEqual, -1)]
        public int LogEntriesPageSize
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(100, Description="X-Location of the Log-Entry Details Dialog"), NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
        public int LogEntryDetailsDialogLocationX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(100, Description="Y-Location of the Log-Entry Details Dialog"), NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
        public int LogEntryDetailsDialogLocationY
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(470, Description="Height of the Log-Entry Details Dialog"), NumberConfigValidator(ValidationOperator.MoreOrEqual, 470)]
        public int LogEntryDetailsDialogSizeHeight
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(420, Description="Width of the Log-Entry Details Dialog"), NumberConfigValidator(ValidationOperator.MoreOrEqual, 420)]
        public int LogEntryDetailsDialogSizeWidth
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.MoreOrEqual, 50), ApplicationSettingsValue(150, Description="Splitter distance of the Log-Entry Details Dialog")]
        public int LogEntryDetailsDialogSplitterDistance
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Show the group-by box above the log grid")]
        public bool LogGroupByBox
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Minimize the main window when closing")]
        public bool MinimizeWhenClosing
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(3, Description="Number of preview rows"), NumberConfigValidator(ValidationOperator.More, 1)]
        public int PreviewRowLines
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Prompt when updating or rolling-back to an older revision than the current source's revision.")]
        public bool PromptRollbackOldRevision
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Prompt when updating to the HEAD revision.")]
        public bool PromptUpdateHeadRevision
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue("Recommended revision ${REVISION}", Description="The default log-message when recommending a revision.")]
        public string RecommendationMessage
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Show 'No Comment' instead of empty log messages")]
        public bool ShowDefaultTextInsteadOfEmptyLogMessage
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Show only enabled sources")]
        public bool SourcesFilter_Enabled
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Show only modified sources")]
        public bool SourcesFilter_Modified
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Show only not up-to-date sources")]
        public bool SourcesFilter_NotUpToDate
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Show the 'Last Check' item in the Sources panel")]
        public bool SourcesPanelShowLastCheck
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Show the 'No Updates' item in the Sources panel")]
        public bool SourcesPanelShowNoUpdates
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Show the Path item in the Sources panel")]
        public bool SourcesPanelShowPath
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Show the URL item in the Sources panel")]
        public bool SourcesPanelShowUrl
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Start the application minimized")]
        public bool StartMinimized
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="When updating all sources (using TortoiseSVN) - do it in parallel.")]
        public bool SVNUpdateSourcesParallel
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.More, 0), ApplicationSettingsValue(10, Description="When not updating sources in parallel - set a waiting timeout (seconds) for each iteration.")]
        public int SVNUpdateSourcesQueueTimeoutSeconds
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(3, Description="Default auto-close behavior of the TortoiseSVN:\n\t0: don't close the dialog automatically\n\t1: Auto close if no errors\n\t2: Auto close if no errors and conflicts\n\t3: Auto close if no errors, conflicts and merges\n\t4: Auto close if no errors, conflicts and merges for local operations"), NumberConfigValidator(ValidationOperator.LessOrEqual, 4), NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
        public int TortoiseSVNAutoClose
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [FileNameConfigValidator, ApplicationSettingsValue(@"%PROGRAMFILES%\TortoiseSVN\bin\TortoiseProc.exe", Description="Location of the TortoiseSVN process (TortoiseProc.exe)")]
        public string TortoiseSVNPath
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Treat unversioned items as modified")]
        public bool TreatUnversionedAsModified
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue("", IsCDATA=true, Description="Layout of the Events grid"), IgnoreWebService, GridLayoutConfigValidator]
        public string UIEventLogGridLayout
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue("en", Description="User-Interface language."), CultureConfigValidator]
        public string UILanguage
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [IgnoreWebService, GridLayoutConfigValidator, ApplicationSettingsValue("", IsCDATA=true, Description="Layout of the Log grid")]
        public string UILogEntriesGridLayout
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.More, 100), ApplicationSettingsValue(600, Description="Height of the main window")]
        public int UIMainFormHeight
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.MoreOrEqual, 0), ApplicationSettingsValue(20, Description="X-Location of the main window")]
        public int UIMainFormLocationX
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(20, Description="Y-Location of the main window"), NumberConfigValidator(ValidationOperator.MoreOrEqual, 0)]
        public int UIMainFormLocationY
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(800, Description="Width of the main window"), NumberConfigValidator(ValidationOperator.More, 100)]
        public int UIMainFormWidth
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue("Normal", Description="State of the main window"), EnumConfigValidator(typeof(FormWindowState))]
        public string UIMainFormWindowState
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Auto-Hide the Events panel")]
        public bool UIPanelEventLogAutoHide
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(200, Description="Height of the Events panel"), NumberConfigValidator(ValidationOperator.More, 100)]
        public int UIPanelEventLogHeight
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.More, 100), ApplicationSettingsValue(300, Description="Width of the left panel (Sources and Monitors)")]
        public int UIPanelLeftWidth
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Auto-Hide the Monitors panel")]
        public bool UIPanelMonitiorsAutoHide
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.More, 100), ApplicationSettingsValue(200, Description="Height of the Monitors panel")]
        public int UIPanelMonitiorsHeight
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Auto-Hide the Paths panel")]
        public bool UIPanelPathsAutoHide
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(200, Description="Height of the Paths panel"), NumberConfigValidator(ValidationOperator.More, 100)]
        public int UIPanelPathsHeight
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(false, Description="Auto-Hide the Sources panel")]
        public bool UIPanelSourcesAutoHide
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue("", IsCDATA=true, Description="Layout of the Paths grid"), IgnoreWebService, GridLayoutConfigValidator]
        public string UIPathsGridLayout
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.MoreOrEqual, 0), ApplicationSettingsValue(0, Description="The interval (in minutes) between each update-check queue. 0 means automatic (UpdatesIntervalPerSource per source)"), NumberConfigValidator(ValidationOperator.LessOrEqual, 0x5a0)]
        public int UpdatesInterval
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [NumberConfigValidator(ValidationOperator.LessOrEqual, 0xe10), ApplicationSettingsValue(60, Description="The interval (in seconds) per source when UpdatesInterval is zero."), NumberConfigValidator(ValidationOperator.More, 0)]
        public int UpdatesIntervalPerSource
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue("1/1/0001 12:00:00 AM", Description="Date and time of latest usage info send")]
        public string UsageSendDate
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<string>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="[OBSOLETE(Always true)] Use TortoiseSVN's global-ignore-pattern")]
        public bool UseTortoiseSVNGlobalIgnorePattern
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return true;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Check for new SVN-Monitor versions when the application starts")]
        public bool VersionCheckAtStartup
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(60, Description="The interval (in minutes) between automatic checking of new SVN-Monitor versions"), NumberConfigValidator(ValidationOperator.More, 0), NumberConfigValidator(ValidationOperator.LessOrEqual, 0x5a0)]
        public int VersionCheckInterval
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<int>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }

        [ApplicationSettingsValue(true, Description="Watch the network availability")]
        public bool WatchTheNetworkAvailability
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.GetValue<bool>();
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.SetValue(value);
            }
        }
    }
}

