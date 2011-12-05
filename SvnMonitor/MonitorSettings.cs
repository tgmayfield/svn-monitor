using System;
using SVNMonitor.Entities;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using SVNMonitor.Logging;
using System.Text;
using SVNMonitor.Support;
using System.IO;
using SVNMonitor.Helpers;
using SVNMonitor.View;
using SVNMonitor.Resources.Text;

namespace SVNMonitor
{
[Serializable]
internal class MonitorSettings : VersionEntity
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private List<Monitor> monitors;

	[NonSerialized]
	private EventHandler monitorsChanged;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly static MonitorSettings monitorSettings;

	private const string SettingsFileName = "SVNMonitor.settings";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private List<Source> sources;

	[NonSerialized]
	private EventHandler sourcesChanged;

	public static MonitorSettings Instance
	{
		get
		{
			return MonitorSettings.monitorSettings;
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public List<Monitor> Monitors
	{
		get
		{
			return this.monitors;
		}
		private set
		{
			this.monitors = value;
			this.OnMonitorsChanged();
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public List<Source> Sources
	{
		get
		{
			return this.sources;
		}
		private set
		{
			this.sources = value;
			this.OnSourcesChanged();
		}
	}

	static MonitorSettings()
	{
		Logger.Log.Info("Loading Monitor Settings...");
		MonitorSettings.monitorSettings = MonitorSettings.LoadSeperateFiles();
		if (MonitorSettings.monitorSettings == null)
		{
			MonitorSettings.monitorSettings = new MonitorSettings();
		}
	}

	protected MonitorSettings()
	{
		this.monitors = new List<Monitor>();
		this.sources = new List<Source>();
	}

	public void AddEntity(UserEntity entity)
	{
		if (entity as Source)
		{
			this.Sources.Add((Source)entity);
		}
		else
		{
			if (entity as Monitor)
			{
				this.Monitors.Add((Monitor)entity);
			}
		}
		this.Save();
	}

	public IEnumerable<Monitor> GetEnumerableMonitors()
	{
		Monitor[] array = new Monitor[this.Monitors.Count];
		this.Monitors.CopyTo(array);
		return array;
	}

	public IEnumerable<Source> GetEnumerableSources()
	{
		Source[] array = new Source[this.Sources.Count];
		this.Sources.CopyTo(array);
		return array;
	}

	private float GetMonitorsAverageActions()
	{
		try
		{
			if (this.Monitors.Count == 0)
			{
				return 0;
			}
			int sum = 0;
			foreach (Monitor monitor in this.GetEnumerableMonitors())
			{
				sum = sum + monitor.Actions.Count;
			}
			float average = (float)sum / (float)this.Monitors.Count;
			return average;
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error calculating average actions per monitor", ex);
			return -1;
		}
	}

	public string GetUsageInformation()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendFormat("MonitorSettings_SourcesCount={0}{1}", this.Sources.Count, UsageInformationSender.Separator);
		sb.AppendFormat("MonitorSettings_MonitorsCount={0}{1}", this.Monitors.Count, UsageInformationSender.Separator);
		sb.AppendFormat("MonitorSettings_AverageActions={0}{1}", this.GetMonitorsAverageActions(), UsageInformationSender.Separator);
		return sb.ToString();
	}

	public void LoadCaches()
	{
		Logger.Log.Info("MonitorSettings.LoadCaches");
		foreach (Source source in this.GetEnumerableSources())
		{
			source.LoadCache();
		}
	}

	private static List<T> LoadItems<T>(string pattern)
	{
		object[] objArray;
		List<T> items = new List<T>();
		string[] itemFiles = Directory.GetFiles(FileSystemHelper.AppData, pattern);
		Logger.Log.InfoFormat("Found {0} files", (int)itemFiles.Length);
		string[] strArrays = itemFiles;
		foreach (string itemFile in strArrays)
		{
			try
			{
				Logger.Log.InfoFormat("Loading '{0}'", itemFile);
				T item = UserEntity.Load<T>(itemFile, true);
				items.Add(item);
				Logger.Log.InfoFormat("Loaded '{0}'. Version {1}", item, item.Version);
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message, ex);
				FileInfo fileInfo = new FileInfo(itemFile);
				try
				{
					fileInfo.MoveTo(string.Concat(itemFile, ".error"));
				}
				catch (Exception ex2)
				{
					Logger.Log.Error(ex2.Message, ex2);
					MainForm.FormInstance.ShowErrorMessage(Strings.ErrorLoadingFileSeeLog_FORMAT.FormatWith(new object[] { itemFile }), Strings.SVNMonitorCaption);
				}
			}
		}
		return items;
	}

	private static MonitorSettings LoadSeperateFiles()
	{
		MonitorSettings settings = new MonitorSettings();
		Logger.Log.Info("Loading Sources...");
		settings.Sources = MonitorSettings.LoadItems<Source>("*.source");
		settings.Sources.Sort();
		Logger.Log.Info("Loading Monitors...");
		settings.Monitors = MonitorSettings.LoadItems<Monitor>("*.monitor");
		settings.Monitors.Sort();
		return settings;
	}

	private static MonitorSettings LoadSingleFile()
	{
		MonitorSettings settings = null;
		string settingsFullFileName = Path.Combine(FileSystemHelper.AppData, "SVNMonitor.settings");
		if (FileSystemHelper.FileExists(settingsFullFileName))
		{
			return (MonitorSettings)SerializationHelper.BinaryDeserialize(settingsFullFileName);
			ErrorHandler.Append(Strings.ErrorLoadingMonitorSettings, null, ex);
		}
		try
		{
		}
		catch (Exception ex)
		{
		}
		return settings;
	}

	internal virtual void OnMonitorsChanged()
	{
		if (this.monitorsChanged != null)
		{
			this.monitorsChanged(this, EventArgs.Empty);
		}
	}

	internal virtual void OnSourcesChanged()
	{
		if (this.sourcesChanged != null)
		{
			this.sourcesChanged(this, EventArgs.Empty);
		}
	}

	public void RemoveMonitor(Monitor monitor)
	{
		this.Monitors.Remove(monitor);
		this.Save();
	}

	public void RemoveSource(Source source)
	{
		this.Sources.Remove(source);
		this.Save();
	}

	public virtual void Save()
	{
		this.Save(true);
	}

	public virtual void Save(bool raiseChanged)
	{
		Logger.Log.InfoFormat("Saving (raiseChanged={0})", raiseChanged);
		if (raiseChanged)
		{
			this.OnSourcesChanged();
			this.OnMonitorsChanged();
		}
		this.SaveSeparateFiles();
	}

	private static void SaveItems<T>(IEnumerable<T> entities)
	{
		if (entities == null)
		{
			return;
		}
		foreach (T entity in entities)
		{
			&entity.OrderNumber = 0++;
			&entity.Save();
		}
	}

	private void SaveSeparateFiles()
	{
		MonitorSettings.SaveItems<Source>(this.Sources);
		MonitorSettings.SaveItems<Monitor>(this.Monitors);
	}

	private void SaveSingleFile()
	{
		string settingsFullFileName = Path.Combine(FileSystemHelper.AppData, "SVNMonitor.settings");
		SerializationHelper.BinarySerialize(this, settingsFullFileName);
	}

	public event EventHandler MonitorsChanged;
	public event EventHandler SourcesChanged;
}
}