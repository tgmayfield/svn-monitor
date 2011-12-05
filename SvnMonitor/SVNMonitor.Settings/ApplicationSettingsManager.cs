using System;
using System.Collections.Generic;
using System.IO;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using System.Reflection;
using SVNMonitor.Resources.Text;
using SVNMonitor.View;
using SVNMonitor;
using System.Xml;
using SVNMonitor.Settings.Validation;

namespace SVNMonitor.Settings
{
public class ApplicationSettingsManager
{
	private const string CDataEnd = "]]>";

	private const string CDataStart = "<![CDATA[";

	internal const int DefaultLogEntriesPageSize = 100;

	internal const int DefaultPreviewRowLines = 3;

	internal const string FileName = "SVNMonitor.config";

	internal const string NoneKeyboardShortcut = "none";

	private static EventHandler SavedSettings;

	private static ApplicationSettings settingsInstance;

	private static Dictionary<PropertyInfo, ApplicationSettingsValueAttribute> settingsProperties;

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
			if (ApplicationSettingsManager.settingsInstance == null)
			{
				ApplicationSettingsManager.settingsInstance = ApplicationSettingsManager.CreateDefaultSettings();
			}
			return ApplicationSettingsManager.settingsInstance;
		}
		set
		{
			ApplicationSettingsManager.settingsInstance = value;
		}
	}

	static ApplicationSettingsManager()
	{
		if (ProcessHelper.IsInVisualStudio())
		{
			return;
		}
		ApplicationSettingsManager.CreateSettingsProperties();
		ApplicationSettingsManager.Load();
		if (ApplicationSettingsManager.Settings != null)
		{
			ApplicationSettingsManager.SaveSettings();
		}
	}

	public ApplicationSettingsManager()
	{
	}

	private static ApplicationSettings CreateDefaultSettings()
	{
		Logger.Log.Info("Creating default settings.");
		ApplicationSettings settings = new ApplicationSettings();
		foreach (PropertyInfo property in ApplicationSettingsManager.settingsProperties.Keys)
		{
			ApplicationSettingsValueAttribute attribute = ApplicationSettingsManager.settingsProperties[property];
			if (attribute != null)
			{
				Logger.Log.InfoFormat("SetValue({0} = {1})", property.Name, attribute.DefaultValue);
				property.SetValue(settings, attribute.DefaultValue, null);
			}
		}
		ApplicationSettingsManager.SaveSettings(settings);
		return settings;
	}

	private static void CreateSettingsProperties()
	{
		ApplicationSettingsManager.settingsProperties = new Dictionary<PropertyInfo, ApplicationSettingsValueAttribute>();
		PropertyInfo[] props = typeof(ApplicationSettings).GetProperties(BindingFlags.Instance | BindingFlags.Public);
		PropertyInfo[] propertyInfoArray = props;
		foreach (PropertyInfo property in propertyInfoArray)
		{
			ApplicationSettingsValueAttribute attribute = (ApplicationSettingsValueAttribute)Attribute.GetCustomAttribute(property, typeof(ApplicationSettingsValueAttribute));
			if (attribute != null)
			{
				ApplicationSettingsManager.settingsProperties.Add(property, attribute);
			}
		}
	}

	internal static T GetDefault<T>()
	{
		string key = ApplicationSettings.GetSettingKeyName();
		T @value = ApplicationSettingsManager.GetDefault<T>(key);
		return @value;
	}

	internal static T GetDefault<T>(string key)
	{
		return (T)ApplicationSettingsManager.GetDefault(key);
	}

	private static object GetDefault(string key)
	{
		PropertyInfo property = typeof(ApplicationSettings).GetProperty(key.ToString(), BindingFlags.Instance | BindingFlags.Public);
		ApplicationSettingsValueAttribute attribute = ApplicationSettingsManager.settingsProperties[property];
		return attribute.DefaultValue;
	}

	internal static void Init()
	{
	}

	public static void Load()
	{
		Logger.Log.InfoFormat("Loading configuration...", new object[0]);
		if (!FileSystemHelper.FileExists(ApplicationSettingsManager.FullFileName))
		{
			Logger.Log.InfoFormat("Settings file does not exist (FullFileName={0})", ApplicationSettingsManager.FullFileName);
			ApplicationSettingsManager.Settings = ApplicationSettingsManager.CreateDefaultSettings();
			return;
		}
		try
		{
			ApplicationSettingsManager.Settings = ApplicationSettings.Load(ApplicationSettingsManager.FullFileName);
			if (ApplicationSettingsManager.Settings == null)
			{
				throw new Exception("ApplicationSettings deserialize as null.");
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error(Strings.ErrorLoadingSettingsCreatingDefault, ex);
			ApplicationSettingsManager.Settings = ApplicationSettingsManager.CreateDefaultSettings();
			ErrorHandler.Append(string.Format("{0} ({1})", Strings.ErrorLoadingSettingsCreatingDefault, ex.Message), ApplicationSettingsManager.FullFileName, ex);
			if (MainForm.FormInstance != null)
			{
				MainForm.FormInstance.ShowErrorMessage(Strings.ErrorLoadingSettingsSeeLogForDetails, Strings.SVNMonitorCaption);
			}
		}
	}

	private static void OnSavedSettings()
	{
		if (ApplicationSettingsManager.SavedSettings != null)
		{
			ApplicationSettingsManager.SavedSettings(ApplicationSettingsManager.Settings, EventArgs.Empty);
		}
		Status.OnStatusChanged();
	}

	internal static void ReadXml(XmlReader reader, ApplicationSettings settings)
	{
		Logger.Log.InfoFormat("Reading config xml file", new object[0]);
		while (reader.Read())
		{
			if (reader.NodeType == 1)
			{
				string key = reader.Name;
				try
				{
					string @value = string.Empty;
					if (!reader.HasAttributes)
					{
						@value = reader.ReadInnerXml();
						if (@value.StartsWith("<![CDATA[") && @value.EndsWith("]]>"))
						{
							@value = @value.Substring("<![CDATA[".Length, @value.Length - "<![CDATA[".Length - "]]>".Length);
						}
						throw new NotSupportedException(string.Concat("Value not supported: ", @value));
					}
					else
					{
						@value = reader["value"];
					}
					PropertyInfo property = typeof(ApplicationSettings).GetProperty(key, BindingFlags.Instance | BindingFlags.Public);
					if (property != null)
					{
						object realValue = Convert.ChangeType(@value, property.PropertyType);
						object validatedValue = ApplicationSettingsManager.ValidateValue(realValue, property, key);
						settings.SetValue(key, validatedValue);
						Logger.Log.Error(string.Format("Invalid setting key: {0}", key), ex);
					}
				}
				catch (Exception ex)
				{
				}
			}
		}
		Logger.Log.InfoFormat("Done reading config xml file", new object[0]);
	}

	public static void SaveSettings()
	{
		ApplicationSettingsManager.SaveSettings(ApplicationSettingsManager.Settings);
	}

	public static void SaveSettings(ApplicationSettings settings)
	{
		try
		{
			Logger.Log.Debug("ApplicationSettingsManager.SaveSettings()");
			string tempFileName = string.Concat(ApplicationSettingsManager.FullFileName, "~tmp");
			FileSystemHelper.DeleteFile(tempFileName);
			settings.Save(tempFileName);
			FileSystemHelper.CopyFile(tempFileName, ApplicationSettingsManager.FullFileName);
			FileSystemHelper.DeleteFile(tempFileName);
			ApplicationSettingsManager.OnSavedSettings();
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
		object[] objArray;
		if (!Attribute.IsDefined(property, typeof(ConfigValidatorAttribute)))
		{
			return value;
		}
		ConfigValidatorAttribute[] attributes = (ConfigValidatorAttribute[])Attribute.GetCustomAttributes(property, typeof(ConfigValidatorAttribute));
		object validatedValue = null;
		ConfigValidatorAttribute[] configValidatorAttributeArray = attributes;
		foreach (ConfigValidatorAttribute attribute in configValidatorAttributeArray)
		{
			IConfigValidator validator = attribute.ConfigValidator;
			try
			{
				validatedValue = validator.Validate(value, out isValid);
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error trying to validate '{0}={1}'".FormatWith(new object[] { key, value }), ex);
			}
			if (!false)
			{
				Logger.Log.ErrorFormat("Invalid key value '{0}' for {1}", value, key);
				Logger.Log.InfoFormat("Getting default for {0}", key);
				return ApplicationSettingsManager.GetDefault(key);
				break;
			}
		}
		return validatedValue;
	}

	internal static void WriteXml(XmlWriter writer, ApplicationSettings settings)
	{
		object[] objArray;
		Logger.Log.InfoFormat("Writing config xml file", new object[0]);
		foreach (PropertyInfo property in ApplicationSettingsManager.settingsProperties.Keys)
		{
			string name = property.Name;
			object @value = property.GetValue(settings, null);
			ApplicationSettingsValueAttribute attribute = ApplicationSettingsManager.settingsProperties[property];
			if (attribute != null && !string.IsNullOrEmpty(attribute.Description))
			{
				writer.WriteComment(string.Format("{0}\t{1} (default={2}): {3}", new object[] { Environment.NewLine, name, attribute.DefaultValue, attribute.Description }));
			}
			writer.WriteStartElement(name);
			if (attribute.IsCDATA)
			{
				writer.WriteCData(@value.ToString());
				continue;
			}
			writer.WriteAttributeString("value", @value.ToString());
			writer.WriteEndElement();
		}
		Logger.Log.InfoFormat("Done writing config xml file", new object[0]);
	}

	public event EventHandler SavedSettings;
}
}