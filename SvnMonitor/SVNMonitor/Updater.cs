using Microsoft.VisualBasic.Devices;
using System;
using System.Windows.Forms;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using Janus.Windows.GridEX;
using SVNMonitor.Settings;
using SVNMonitor.Entities;
using System.Collections.Generic;
using SVNMonitor.Helpers;
using System.Diagnostics;
using System.Threading;
using SharpSvn;

namespace SVNMonitor
{
internal class Updater
{
	private static Updater instance;

	private Network networkWatcher;

	private EventHandler StatusChanged;

	private Timer timer;

	public bool Enabled
	{
		get
		{
			return this.timer.Enabled;
		}
		set
		{
			this.timer.Enabled = value;
			if (this.Enabled)
			{
				Logger.Log.Info("The updater has started working!");
				EventLog.LogInfo(Strings.UpdatesAreEnabled, this);
				this.QueueUpdates();
			}
			else
			{
				EventLog.LogWarning(Strings.UpdatesAreDisabled, this);
			}
			Status.OnStatusChanged();
		}
	}

	internal static Updater Instance
	{
		get
		{
			return Updater.instance;
		}
	}

	public GridEX UpdatesGrid
	{
		get;
		set;
	}

	static Updater()
	{
		Updater.instance = new Updater();
	}

	public Updater()
	{
		EventHandler eventHandler1 = null;
		EventHandler eventHandler2 = null;
		base();
		Updater.instance = this;
		this.timer = new Timer();
		this.timer.Tick += new EventHandler(this.timer_Tick);
		if (eventHandler1 == null)
		{
			eventHandler1 = new EventHandler((, ) => this.ReadSettings());
		}
		ApplicationSettingsManager.add_SavedSettings(eventHandler1);
	}

	private void CheckMonitor(Monitor monitor, List<SVNLogEntry> updates)
	{
		try
		{
		}
		catch (Exception ex)
		{
			ErrorHandler.HandleEntityException(monitor, ex);
		}
		if (this.IgnoreMonitor(monitor))
		{
			string status = this.GetMonitorStatusString(monitor);
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
				matchesCount = this.GetMatches(condition, ref matchingLogEntries, matchingPaths);
			}
			if (matchesCount > 0)
			{
				monitor.Trigger(matchingLogEntries, matchingPaths);
			}
		}
	}

	private void CheckMonitors(List<SVNLogEntry> updates)
	{
		try
		{
			if (MonitorSettings.Instance.Monitors == null || MonitorSettings.Instance.Monitors.Count != 0)
			{
				this.UpdatesGrid.DataSource = updates;
				foreach (Monitor monitor in MonitorSettings.Instance.GetEnumerableMonitors().Where<Monitor>(new Predicate<Monitor>((m) => m.IsAlive)))
				{
					this.CheckMonitor(monitor, updates);
				}
			}
		}
		finally
		{
			this.UpdatesGrid.DataSource = null;
		}
	}

	private int GetMatches(GridEXFilterCondition condition, ref List<SVNLogEntry> logEntries, ICollection<SVNPath> paths)
	{
		int matchesCount = this.UpdatesGrid.FindAll(condition);
		if (matchesCount == 0)
		{
			return 0;
		}
		foreach (GridEXSelectedItem item in this.UpdatesGrid.SelectedItems)
		{
			object dataRow = item.GetRow().DataRow;
			if (dataRow as SVNLogEntry)
			{
				logEntries.Add((SVNLogEntry)dataRow);
			}
			else
			{
				if (dataRow as SVNPath)
				{
					paths.Add((SVNPath)dataRow);
				}
				else
				{
					Debugger.Launch();
				}
			}
		}
		return matchesCount;
	}

	private string GetMonitorStatusString(Monitor monitor)
	{
		if (monitor != null)
		{
			string disabled = (!monitor.Enabled ? "v" : "x");
			string edit = (monitor.IsInEditMode ? "v" : "x");
			return string.Format("Disabled[{0}] Edit[{1}]", disabled, edit);
		}
		string status = "Monitor is NULL";
		return status;
	}

	private string GetSourceStatusString(Source source)
	{
		if (source != null)
		{
			string disabled = (!source.Enabled ? "v" : "x");
			string edit = (source.IsInEditMode ? "v" : "x");
			string updating = (source.Updating ? "v" : "x");
			return string.Format("Disabled[{0}] Edit[{1}] Updating[{2}]", disabled, edit, updating);
		}
		string status = "Source is NULL";
		return status;
	}

	private bool IgnoreMonitor(Monitor monitor)
	{
		if (monitor == null)
		{
			return true;
		}
		if (!monitor.Enabled)
		{
			return true;
		}
		if (monitor.IsInEditMode)
		{
			return true;
		}
		return false;
	}

	private bool IgnoreSource(Source source)
	{
		if (source == null)
		{
			return true;
		}
		if (!source.Enabled)
		{
			return true;
		}
		if (source.IsInEditMode)
		{
			return true;
		}
		if (source.Updating)
		{
			return true;
		}
		return false;
	}

	private void InternalStart()
	{
		this.ReadSettings();
	}

	protected virtual void OnStatusChanged()
	{
		if (Status.Closing)
		{
			Status.OnCanExit();
			return;
		}
		if (this.StatusChanged != null)
		{
			this.StatusChanged(this, EventArgs.Empty);
		}
	}

	public void QueueUpdate(Source source, bool force)
	{
		SourceUpdateInfo sourceUpdateInfo = new SourceUpdateInfo();
		sourceUpdateInfo.Source = source;
		sourceUpdateInfo.Force = force;
		SourceUpdateInfo info = sourceUpdateInfo;
		ThreadHelper.Queue(new WaitCallback(this.UpdateLog), "UPDATE", info);
	}

	public void QueueUpdates()
	{
		ThreadHelper.Queue(new WaitCallback(this.UpdateLogs), "QUEUE");
	}

	public void ReadSettings()
	{
		int milliseconds = 1000;
		if (ApplicationSettingsManager.Settings.UpdatesInterval <= 0)
		{
			int minutes = Status.EnabledSourcesCount;
			if (minutes == 0)
			{
				minutes = 1;
			}
			this.SetInterval(minutes * ApplicationSettingsManager.Settings.UpdatesIntervalPerSource * milliseconds);
		}
		else
		{
			int oneMinute = milliseconds * 60;
			this.SetInterval(ApplicationSettingsManager.Settings.UpdatesInterval * oneMinute);
		}
		if (this.Enabled != ApplicationSettingsManager.Settings.EnableUpdates)
		{
			this.Enabled = ApplicationSettingsManager.Settings.EnableUpdates;
		}
		this.RefreshLocalStatusAsync();
		this.SetNetworkWatcher();
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
		ThreadHelper.Queue(new WaitCallback(this.RefreshLocalStatus), "STATUS");
	}

	public void RefreshSource(Source source)
	{
		ThreadHelper.Queue(new WaitCallback(this.RefreshSource), "REFRESH", source);
	}

	private void RefreshSource(object sourceObject)
	{
		Source source = (Source)sourceObject;
		if (source.Updating)
		{
			return;
		}
		Logger.Log.InfoFormat("Refreshing {0}", source.Name);
		source.RefreshLog();
	}

	private void SetInterval(int value)
	{
		if (this.timer.Interval == value)
		{
			return;
		}
		Logger.Log.InfoFormat("Setting the updater interval to {0}", value);
		try
		{
			this.timer.Interval = value;
		}
		catch (ArgumentOutOfRangeException ex)
		{
			Logger.Log.Error(string.Concat("Trying to set timer's interval to: ", value), ex);
			value = 600000;
			Logger.Log.Info(string.Concat("Setting interval to default: ", value));
			this.timer.Interval = value;
		}
	}

	internal void SetNetworkWatcher()
	{
		try
		{
			if (this.networkWatcher == null)
			{
				this.networkWatcher = new Network();
			}
			Logger.Log.InfoFormat(string.Concat("WatchTheNetworkAvailability = ", ApplicationSettingsManager.Settings.WatchTheNetworkAvailability), new object[0]);
			this.networkWatcher.NetworkAvailabilityChanged -= new NetworkAvailableEventHandler(this.Updater_NetworkAvailabilityChanged);
			if (ApplicationSettingsManager.Settings.WatchTheNetworkAvailability)
			{
				this.networkWatcher.NetworkAvailabilityChanged += new NetworkAvailableEventHandler(this.Updater_NetworkAvailabilityChanged);
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error trying to watch the network: ", ex);
		}
	}

	public static void Start()
	{
		Updater.Instance.InternalStart();
	}

	private void timer_Tick(object sender, EventArgs e)
	{
		this.QueueUpdates();
	}

	private void UpdateLog(object sourceObject)
	{
		SourceUpdateInfo info = (SourceUpdateInfo)sourceObject;
		this.UpdateLog(info.Source, info.Force);
	}

	private List<SVNLogEntry> UpdateLog(Source source, bool force)
	{
		if (source.Updating)
		{
			return null;
		}
		List<SVNLogEntry> updates = null;
		try
		{
			if (this.IgnoreSource(source) && !force)
			{
				string status = this.GetSourceStatusString(source);
				Logger.Log.DebugFormat("Ignoring source '{0}'", source.Name);
				Logger.Log.DebugFormat("Status: {0}", status);
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
		if (force && updates != null && updates.Count > 0 && this.UpdatesGrid != null)
		{
			this.CheckMonitors(updates);
		}
		ProcessHelper.FlushMemory();
		return updates;
	}

	protected void UpdateLogs(object state)
	{
		this.UpdateLogs();
	}

	private void UpdateLogs()
	{
		List<SVNLogEntry> updates = new List<SVNLogEntry>();
		IEnumerator<Source> enumerator = Status.EnabledSources.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Source source = enumerator.Current;
				if (!Status.Closing)
				{
					List<SVNLogEntry> entries = this.UpdateLog(source, false);
					if (entries != null && entries.Count > 0)
					{
						updates.AddRange(entries);
					}
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
					if (updates.Count > 0 && this.UpdatesGrid != null)
					{
						this.CheckMonitors(updates);
					}
				}
			}
		}
		finally
		{
		}
	}

	private void Updater_NetworkAvailabilityChanged(object sender, NetworkAvailableEventArgs e)
	{
		try
		{
			if (e.IsNetworkAvailable)
			{
				this.QueueUpdates();
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error checking for the network availablility.", ex);
		}
	}

	public event EventHandler StatusChanged;
	private class SourceUpdateInfo
	{
		public bool Force;

		public Source Source;

		public SourceUpdateInfo();
	}
}
}