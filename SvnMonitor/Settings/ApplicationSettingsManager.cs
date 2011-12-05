namespace SVNMonitor.Settings
{
    using SVNMonitor;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings.Validation;
    using SVNMonitor.View;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class ApplicationSettingsManager
    {
        private const string CDataEnd = "]]>";
        private const string CDataStart = "<![CDATA[";
        internal const int DefaultLogEntriesPageSize = 100;
        internal const int DefaultPreviewRowLines = 3;
        internal const string FileName = "SVNMonitor.config";
        internal const string NoneKeyboardShortcut = "none";
        private static ApplicationSettings settingsInstance;
        private static Dictionary<PropertyInfo, ApplicationSettingsValueAttribute> settingsProperties;

        public static  event EventHandler SavedSettings;

        static ApplicationSettingsManager()
        {
            if (!ProcessHelper.IsInVisualStudio())
            {
                CreateSettingsProperties();
                Load();
                if (Settings != null)
                {
                    SaveSettings();
                }
            }
        }

        private static ApplicationSettings CreateDefaultSettings()
        {
            Logger.Log.Info("Creating default settings.");
            ApplicationSettings settings = new ApplicationSettings();
            foreach (PropertyInfo property in settingsProperties.Keys)
            {
                ApplicationSettingsValueAttribute attribute = settingsProperties[property];
                if (attribute != null)
                {
                    Logger.Log.InfoFormat("SetValue({0} = {1})", property.Name, attribute.DefaultValue);
                    property.SetValue(settings, attribute.DefaultValue, null);
                }
            }
            SaveSettings(settings);
            return settings;
        }

        private static void CreateSettingsProperties()
        {
            settingsProperties = new Dictionary<PropertyInfo, ApplicationSettingsValueAttribute>();
            foreach (PropertyInfo property in typeof(ApplicationSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ApplicationSettingsValueAttribute attribute = (ApplicationSettingsValueAttribute) Attribute.GetCustomAttribute(property, typeof(ApplicationSettingsValueAttribute));
                if (attribute != null)
                {
                    settingsProperties.Add(property, attribute);
                }
            }
        }

        internal static T GetDefault<T>()
        {
            return GetDefault<T>(ApplicationSettings.GetSettingKeyName());
        }

        private static object GetDefault(string key)
        {
            PropertyInfo property = typeof(ApplicationSettings).GetProperty(key.ToString(), BindingFlags.Public | BindingFlags.Instance);
            ApplicationSettingsValueAttribute attribute = settingsProperties[property];
            return attribute.DefaultValue;
        }

        internal static T GetDefault<T>(string key)
        {
            return (T) GetDefault(key);
        }

        internal static void Init()
        {
        }

        public static void Load()
        {
            Logger.Log.InfoFormat("Loading configuration...", new object[0]);
            if (!FileSystemHelper.FileExists(FullFileName))
            {
                Logger.Log.InfoFormat("Settings file does not exist (FullFileName={0})", FullFileName);
                Settings = CreateDefaultSettings();
            }
            else
            {
                try
                {
                    Settings = ApplicationSettings.Load(FullFileName);
                    if (Settings == null)
                    {
                        throw new Exception("ApplicationSettings deserialize as null.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(Strings.ErrorLoadingSettingsCreatingDefault, ex);
                    Settings = CreateDefaultSettings();
                    ErrorHandler.Append(string.Format("{0} ({1})", Strings.ErrorLoadingSettingsCreatingDefault, ex.Message), FullFileName, ex);
                    if (MainForm.FormInstance != null)
                    {
                        MainForm.FormInstance.ShowErrorMessage(Strings.ErrorLoadingSettingsSeeLogForDetails, Strings.SVNMonitorCaption);
                    }
                }
            }
        }

        private static void OnSavedSettings()
        {
            if (SavedSettings != null)
            {
                SavedSettings(Settings, EventArgs.Empty);
            }
            Status.OnStatusChanged();
        }

        internal static void ReadXml(XmlReader reader, ApplicationSettings settings)
        {
            Logger.Log.InfoFormat("Reading config xml file", new object[0]);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string key = reader.Name;
                    try
                    {
                        string value = string.Empty;
                        if (!reader.HasAttributes)
                        {
                            value = reader.ReadInnerXml();
                            if (!value.StartsWith("<![CDATA[") || !value.EndsWith("]]>"))
                            {
                                throw new NotSupportedException("Value not supported: " + value);
                            }
                            value = value.Substring("<![CDATA[".Length, (value.Length - "<![CDATA[".Length) - "]]>".Length);
                        }
                        else
                        {
                            value = reader["value"];
                        }
                        PropertyInfo property = typeof(ApplicationSettings).GetProperty(key, BindingFlags.Public | BindingFlags.Instance);
                        if (property != null)
                        {
                            object validatedValue = ValidateValue(Convert.ChangeType(value, property.PropertyType), property, key);
                            settings.SetValue(key, validatedValue);
                        }
                        continue;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(string.Format("Invalid setting key: {0}", key), ex);
                        continue;
                    }
                }
            }
            Logger.Log.InfoFormat("Done reading config xml file", new object[0]);
        }

        public static void SaveSettings()
        {
            SaveSettings(Settings);
        }

        public static void SaveSettings(ApplicationSettings settings)
        {
            try
            {
                Logger.Log.Debug("ApplicationSettingsManager.SaveSettings()");
                string tempFileName = FullFileName + "~tmp";
                FileSystemHelper.DeleteFile(tempFileName);
                settings.Save(tempFileName);
                FileSystemHelper.CopyFile(tempFileName, FullFileName);
                FileSystemHelper.DeleteFile(tempFileName);
                OnSavedSettings();
            }
            catch (Exception ex)
            {
                string message = Strings.ErrorSavingTheSettings;
                Logger.Log.Error(message, ex);
                ErrorHandler.Append(string.Format("{0}. ({1})", message, ex.Message), settings, ex);
                string text = Strings.ErrorSavingTheSettingsFullMessage;
                MainForm.FormInstance.ShowErrorMessage(text, Strings.SVNMonitorCaption);
            }
        }

        private static object ValidateValue(object value, PropertyInfo property, string key)
        {
            if (!Attribute.IsDefined(property, typeof(ConfigValidatorAttribute)))
            {
                return value;
            }
            ConfigValidatorAttribute[] attributes = (ConfigValidatorAttribute[]) Attribute.GetCustomAttributes(property, typeof(ConfigValidatorAttribute));
            object validatedValue = null;
            foreach (ConfigValidatorAttribute attribute in attributes)
            {
                IConfigValidator validator = attribute.ConfigValidator;
                bool isValid = false;
                try
                {
                    validatedValue = validator.Validate(value, out isValid);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Error trying to validate '{0}={1}'".FormatWith(new object[] { key, value }), ex);
                }
                if (!isValid)
                {
                    Logger.Log.ErrorFormat("Invalid key value '{0}' for {1}", value, key);
                    Logger.Log.InfoFormat("Getting default for {0}", key);
                    return GetDefault(key);
                }
            }
            return validatedValue;
        }

        internal static void WriteXml(XmlWriter writer, ApplicationSettings settings)
        {
            Logger.Log.InfoFormat("Writing config xml file", new object[0]);
            foreach (PropertyInfo property in settingsProperties.Keys)
            {
                string name = property.Name;
                object value = property.GetValue(settings, null);
                ApplicationSettingsValueAttribute attribute = settingsProperties[property];
                if ((attribute != null) && !string.IsNullOrEmpty(attribute.Description))
                {
                    writer.WriteComment(string.Format("{0}\t{1} (default={2}): {3}", new object[] { Environment.NewLine, name, attribute.DefaultValue, attribute.Description }));
                }
                writer.WriteStartElement(name);
                if (attribute.IsCDATA)
                {
                    writer.WriteCData(value.ToString());
                }
                else
                {
                    writer.WriteAttributeString("value", value.ToString());
                }
                writer.WriteEndElement();
            }
            Logger.Log.InfoFormat("Done writing config xml file", new object[0]);
        }

        private static string FullFileName
        {
            get
            {
                return Path.Combine(FileSystemHelper.AppData, "SVNMonitor.config");
            }
        }

        public static ApplicationSettings Settings
        {
            get
            {
                if (settingsInstance == null)
                {
                    settingsInstance = CreateDefaultSettings();
                }
                return settingsInstance;
            }
            set
            {
                settingsInstance = value;
            }
        }
    }
}

