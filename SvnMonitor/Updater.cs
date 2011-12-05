using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Janus.Windows.GridEX;

using Microsoft.VisualBasic.Devices;

using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.Settings;

using SharpSvn;

namespace SVNMonitor
{
	internal class Updater
	{
		private static Updater instance = new Updater();
		private Network networkWatcher;
		private readonly System.Windows.Forms.Timer timer;

		public event EventHandler StatusChanged;

		public Updater()
		{
			instance = this;
			timer = new System.Windows.Forms.Timer();
			timer.Tick += timer_Tick;
			ApplicationSettingsManager.SavedSettings += (s, ea) => ReadSettings();
			MonitorSettings.Instance.SourcesChanged += (s, ea) => ReadSettings();
			SetNetworkWatcher();
		}

		private void CheckMonitor(SVNMonitor.Entities.Monitor monitor, List<SVNLogEntry> updates)
		{
			try
			{
				if (IgnoreMonitor(monitor))
				{
					string status = GetMonitorStatusString(monitor);
					Logger.Log.DebugFormat("Ignoring monitor '{0}'", monitor.Name);
					Logger.Log.DebugFormat("Status: {0}", status);
				}
				else
				{
					List<SVNLogEntry> matchingLogEntries = updates;
					List<SVNPath> matchingPaths = new List<SVNPath>();
					GridEXFilterCondition condition = monitor.FilterCondition;
					int matchesCount = matchingLogEntries.Count;
					if (condition != null)
					{
						matchesCount = GetMatches(condition, ref matchingLogEntries, matchingPaths);
					}
					if (matchesCount > 0)
					{
						monitor.Trigger(matchingLogEntries, matchingPaths);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleEntityException(monitor, ex);
			}
		}

		private void CheckMonitors(List<SVNLogEntry> updates)
		{
			try
			{
				if ((MonitorSettings.Instance.Monitors != null) && (MonitorSettings.Instance.Monitors.Count != 0))
				{
					UpdatesGrid.DataSource = updates;
					foreach (SVNMonitor.Entities.Monitor monitor in MonitorSettings.Instance.GetEnumerableMonitors().Where(m => m.IsAlive))
					{
						CheckMonitor(monitor, updates);
					}
				}
			}
			finally
			{
				UpdatesGrid.DataSource = null;
			}
		}

		private int GetMatches(GridEXFilterCondition condition, ref List<SVNLogEntry> logEntries, ICollection<SVNPath> paths)
		{
			int matchesCount = UpdatesGrid.FindAll(condition);
			logEntries = new List<SVNLogEntry>();
			if (matchesCount == 0)
			{
				return 0;
			}
			foreach (GridEXSelectedItem item in UpdatesGrid.SelectedItems)
			{
				object dataRow = item.GetRow().DataRow;
				if (dataRow is SVNLogEntry)
				{
					logEntries.Add((SVNLogEntry)dataRow);
				}
				else if (dataRow is SVNPath)
				{
					paths.Add((SVNPath)dataRow);
				}
				else
				{
					Debugger.Launch();
				}
			}
			return matchesCount;
		}

		private string GetMonitorStatusString(SVNMonitor.Entities.Monitor monitor)
		{
			if (monitor != null)
			{
				string disabled = !monitor.Enabled ? "v" : "x";
				string edit = monitor.IsInEditMode ? "v" : "x";
				return string.Format("Disabled[{0}] Edit[{1}]", disabled, edit);
			}
			return "Monitor is NULL";
		}

		private string GetSourceStatusString(Source source)
		{
			if (source != null)
			{
				string disabled = !source.Enabled ? "v" : "x";
				string edit = source.IsInEditMode ? "v" : "x";
				string updating = source.Updating ? "v" : "x";
				return string.Format("Disabled[{0}] Edit[{1}] Updating[{2}]", disabled, edit, updating);
			}
			return "Source is NULL";
		}

		private bool IgnoreMonitor(SVNMonitor.Entities.Monitor monitor)
		{
			return ((monitor == null) || (!monitor.Enabled || monitor.IsInEditMode));
		}

		private bool IgnoreSource(Source source)
		{
			return ((source == null) || (!source.Enabled || (source.IsInEditMode || source.Updating)));
		}

		private void InternalStart()
		{
			ReadSettings();
		}

		protected virtual void OnStatusChanged()
		{
			if (Status.Closing)
			{
				Status.OnCanExit();
			}
			else if (StatusChanged != null)
			{
				StatusChanged(this, EventArgs.Empty);
			}
		}

		public void QueueUpdate(Source source, bool force)
		{
			SourceUpdateInfo tempLocal4 = new SourceUpdateInfo
			{
				Source = source,
				Force = force
			};
			SourceUpdateInfo info = tempLocal4;
			SVNMonitor.Helpers.ThreadHelper.Queue(UpdateLog, "UPDATE", info);
		}

		public void QueueUpdates()
		{
			SVNMonitor.Helpers.ThreadHelper.Queue(UpdateLogs, "QUEUE");
		}

		public void ReadSettings()
		{
			int milliseconds = 0x3e8;
			if (ApplicationSettingsManager.Settings.UpdatesInterval <= 0)
			{
				int minutes = Status.EnabledSourcesCount;
				if (minutes == 0)
				{
					minutes = 1;
				}
				SetInterval((minutes * ApplicationSettingsManager.Settings.UpdatesIntervalPerSource) * milliseconds);
			}
			else
			{
				int oneMinute = milliseconds * 60;
				SetInterval(ApplicationSettingsManager.Settings.UpdatesInterval * oneMinute);
			}
			if (Enabled != ApplicationSettingsManager.Settings.EnableUpdates)
			{
				Enabled = ApplicationSettingsManager.Settings.EnableUpdates;
			}
			RefreshLocalStatusAsync();
			SetNetworkWatcher();
		}

		private void RefreshLocalStatus(object state)
		{
			Logger.Log.Debug("Refreshing local sources status...");
			foreach (Source source in Status.EnabledSources)
			{
				Logger.Log.DebugFormat("Refreshing local status of '{0}'", source);
				source.RefreshLocalStatus();
			}
		}

		private void RefreshLocalStatusAsync()
		{
			SVNMonitor.Helpers.ThreadHelper.Queue(RefreshLocalStatus, "STATUS");
		}

		public void RefreshSource(Source source)
		{
			SVNMonitor.Helpers.ThreadHelper.Queue(RefreshSource, "REFRESH", source);
		}

		private void RefreshSource(object sourceObject)
		{
			Source source = (Source)sourceObject;
			if (!source.Updating)
			{
				Logger.Log.InfoFormat("Refreshing {0}", source.Name);
				source.RefreshLog();
			}
		}

		private void SetInterval(int value)
		{
			if (timer.Interval != value)
			{
				Logger.Log.InfoFormat("Setting the updater interval to {0}", value);
				try
				{
					timer.Interval = value;
				}
				catch (ArgumentOutOfRangeException ex)
				{
					Logger.Log.Error("Trying to set timer's interval to: " + value, ex);
					value = 0x927c0;
					Logger.Log.Info("Setting interval to default: " + value);
					timer.Interval = value;
				}
			}
		}

		internal void SetNetworkWatcher()
		{
			try
			{
				if (networkWatcher == null)
				{
					networkWatcher = new Network();
				}
				Logger.Log.InfoFormat("WatchTheNetworkAvailability = " + ApplicationSettingsManager.Settings.WatchTheNetworkAvailability, new object[0]);
				networkWatcher.NetworkAvailabilityChanged -= Updater_NetworkAvailabilityChanged;
				if (ApplicationSettingsManager.Settings.WatchTheNetworkAvailability)
				{
					networkWatcher.NetworkAvailabilityChanged += Updater_NetworkAvailabilityChanged;
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error trying to watch the network: ", ex);
			}
		}

		public static void Start()
		{
			Instance.InternalStart();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			QueueUpdates();
		}

		private void UpdateLog(object sourceObject)
		{
			SourceUpdateInfo info = (SourceUpdateInfo)sourceObject;
			UpdateLog(info.Source, info.Force);
		}

		private List<SVNLogEntry> UpdateLog(Source source, bool force)
		{
			if (source.Updating)
			{
				return null;
			}
			List<SVNLogEntry> updates = null;
			bool ignoreSource = IgnoreSource(source);
			try
			{
				if (ignoreSource && !force)
				{
					string status = GetSourceStatusString(source);
					Logger.Log.DebugFormat("Ignoring source '{0}'", source.Name);
					Logger.Log.DebugFormat("Status: {0}", status);
				}
				else
				{
					updates = source.CheckUpdates();
					OnStatusChanged();
				}
			}
			catch (SvnException svnex)
			{
				ErrorHandler.HandleSourceSVNException(source, svnex);
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleEntityException(source, ex);
			}
			if ((force && (updates != null)) && ((updates.Count > 0) && (UpdatesGrid != null)))
			{
				CheckMonitors(updates);
			}
			ProcessHelper.FlushMemory();
			return updates;
		}

		private void UpdateLogs()
		{
			List<SVNLogEntry> updates = new List<SVNLogEntry>();
			foreach (Source source in Status.EnabledSources)
			{
				if (Status.Closing)
				{
					return;
				}
				List<SVNLogEntry> entries = UpdateLog(source, false);
				if ((entries != null) && (entries.Count > 0))
				{
					updates.AddRange(entries);
				}
			}
			if ((updates.Count > 0) && (UpdatesGrid != null))
			{
				CheckMonitors(updates);
			}
		}

		protected void UpdateLogs(object state)
		{
			UpdateLogs();
		}

		private void Updater_NetworkAvailabilityChanged(object sender, NetworkAvailableEventArgs e)
		{
			try
			{
				if (e.IsNetworkAvailable)
				{
					QueueUpdates();
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error checking for the network availablility.", ex);
			}
		}

		public bool Enabled
		{
			get { return timer.Enabled; }
			set
			{
				timer.Enabled = value;
				if (Enabled)
				{
					Logger.Log.Info("The updater has started working!");
					SVNMonitor.EventLog.LogInfo(Strings.UpdatesAreEnabled, this);
					QueueUpdates();
				}
				else
				{
					SVNMonitor.EventLog.LogWarning(Strings.UpdatesAreDisabled, this);
				}
				Status.OnStatusChanged();
			}
		}

		internal static Updater Instance
		{
			[DebuggerNonUserCode]
			get { return instance; }
		}

		public Janus.Windows.GridEX.GridEX UpdatesGrid { get; set; }

		private class SourceUpdateInfo
		{
			public bool Force { get; set; }

			public SVNMonitor.Entities.Source Source { get; set; }
		}
	}
}