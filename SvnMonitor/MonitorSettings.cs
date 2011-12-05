using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.View;

namespace SVNMonitor
{
	[Serializable]
	internal class MonitorSettings : VersionEntity
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Monitor> monitors = new List<Monitor>();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static readonly MonitorSettings monitorSettings;
		private const string SettingsFileName = "SVNMonitor.settings";
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Source> sources = new List<Source>();

		[field: NonSerialized]
		public event EventHandler MonitorsChanged;

		[field: NonSerialized]
		public event EventHandler SourcesChanged;

		static MonitorSettings()
		{
			Logger.Log.Info("Loading Monitor Settings...");
			monitorSettings = LoadSeperateFiles();
			if (monitorSettings == null)
			{
				monitorSettings = new MonitorSettings();
			}
		}

		protected MonitorSettings()
		{
		}

		public void AddEntity(UserEntity entity)
		{
			if (entity is Source)
			{
				Sources.Add((Source)entity);
			}
			else if (entity is Monitor)
			{
				Monitors.Add((Monitor)entity);
			}
			Save();
		}

		public IEnumerable<Monitor> GetEnumerableMonitors()
		{
			Monitor[] array = new Monitor[Monitors.Count];
			Monitors.CopyTo(array);
			return array;
		}

		public IEnumerable<Source> GetEnumerableSources()
		{
			Source[] array = new Source[Sources.Count];
			Sources.CopyTo(array);
			return array;
		}

		private float GetMonitorsAverageActions()
		{
			try
			{
				if (Monitors.Count == 0)
				{
					return 0f;
				}
				int sum = 0;
				foreach (Monitor monitor in GetEnumerableMonitors())
				{
					sum += monitor.Actions.Count;
				}
				return ((sum) / ((float)Monitors.Count));
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error calculating average actions per monitor", ex);
				return -1f;
			}
		}

		public string GetUsageInformation()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("MonitorSettings_SourcesCount={0}{1}", Sources.Count, Environment.NewLine);
			sb.AppendFormat("MonitorSettings_MonitorsCount={0}{1}", Monitors.Count, Environment.NewLine);
			sb.AppendFormat("MonitorSettings_AverageActions={0}{1}", GetMonitorsAverageActions(), Environment.NewLine);
			return sb.ToString();
		}

		public void LoadCaches()
		{
			Logger.Log.Info("MonitorSettings.LoadCaches");
			foreach (Source source in GetEnumerableSources())
			{
				source.LoadCache();
			}
		}

		private static List<T> LoadItems<T>(string pattern) where T : UserEntity
		{
			List<T> items = new List<T>();
			string[] itemFiles = Directory.GetFiles(FileSystemHelper.AppData, pattern);
			Logger.Log.InfoFormat("Found {0} files", itemFiles.Length);
			foreach (string itemFile in itemFiles)
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
						fileInfo.MoveTo(itemFile + ".error");
					}
					catch (Exception ex2)
					{
						Logger.Log.Error(ex2.Message, ex2);
						MainForm.FormInstance.ShowErrorMessage(Strings.ErrorLoadingFileSeeLog_FORMAT.FormatWith(new object[]
						{
							itemFile
						}), Strings.SVNMonitorCaption);
					}
				}
			}
			return items;
		}

		private static MonitorSettings LoadSeperateFiles()
		{
			MonitorSettings settings = new MonitorSettings();
			Logger.Log.Info("Loading Sources...");
			settings.Sources = LoadItems<Source>("*.source");
			settings.Sources.Sort();
			Logger.Log.Info("Loading Monitors...");
			settings.Monitors = LoadItems<Monitor>("*.monitor");
			settings.Monitors.Sort();
			return settings;
		}

		private static MonitorSettings LoadSingleFile()
		{
			MonitorSettings settings = null;
			string settingsFullFileName = Path.Combine(FileSystemHelper.AppData, "SVNMonitor.settings");
			if (FileSystemHelper.FileExists(settingsFullFileName))
			{
				try
				{
					settings = (MonitorSettings)SerializationHelper.BinaryDeserialize(settingsFullFileName);
				}
				catch (Exception ex)
				{
					ErrorHandler.Append(Strings.ErrorLoadingMonitorSettings, null, ex);
				}
			}
			return settings;
		}

		internal virtual void OnMonitorsChanged()
		{
			if (MonitorsChanged != null)
			{
				MonitorsChanged(this, EventArgs.Empty);
			}
		}

		internal virtual void OnSourcesChanged()
		{
			if (SourcesChanged != null)
			{
				SourcesChanged(this, EventArgs.Empty);
			}
		}

		public void RemoveMonitor(Monitor monitor)
		{
			Monitors.Remove(monitor);
			Save();
		}

		public void RemoveSource(Source source)
		{
			Sources.Remove(source);
			Save();
		}

		public virtual void Save()
		{
			Save(true);
		}

		public virtual void Save(bool raiseChanged)
		{
			Logger.Log.InfoFormat("Saving (raiseChanged={0})", raiseChanged);
			if (raiseChanged)
			{
				OnSourcesChanged();
				OnMonitorsChanged();
			}
			SaveSeparateFiles();
		}

		private static void SaveItems<T>(IEnumerable<T> entities) where T : UserEntity
		{
			if (entities != null)
			{
				int orderNumber = 0;
				foreach (T entity in entities)
				{
					entity.OrderNumber = orderNumber++;
					entity.Save();
				}
			}
		}

		private void SaveSeparateFiles()
		{
			SaveItems(Sources);
			SaveItems(Monitors);
		}

		private void SaveSingleFile()
		{
			string settingsFullFileName = Path.Combine(FileSystemHelper.AppData, "SVNMonitor.settings");
			SerializationHelper.BinarySerialize(this, settingsFullFileName);
		}

		public static MonitorSettings Instance
		{
			[DebuggerNonUserCode]
			get { return monitorSettings; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<Monitor> Monitors
		{
			[DebuggerNonUserCode]
			get { return monitors; }
			private set
			{
				monitors = value;
				OnMonitorsChanged();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public List<Source> Sources
		{
			[DebuggerNonUserCode]
			get { return sources; }
			private set
			{
				sources = value;
				OnSourcesChanged();
			}
		}
	}
}