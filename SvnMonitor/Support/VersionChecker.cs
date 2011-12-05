using System;
using System.Windows.Forms;
using SVNMonitor.Logging;
using SVNMonitor.Settings;
using SVNMonitor.Helpers;
using System.Threading;
using SVNMonitor.Web;
using SVNMonitor;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Support
{
internal class VersionChecker
{
	private bool checking;

	private static VersionChecker instance;

	private Version lastPromptedVersion;

	private EventHandler<VersionEventArgs> NewVersionAvailable;

	private EventHandler NoNewVersionAvailable;

	private Timer timer;

	private EventHandler<VersionEventArgs> UpgradeAvailable;

	private UsageInformationSender usageInformationSender;

	public bool Enabled
	{
		get
		{
			return this.timer.Enabled;
		}
		set
		{
			this.timer.Enabled = value;
			Logger.Log.InfoFormat("Version checker is now {0}", (value ? "enabled" : "disabled"));
		}
	}

	internal static VersionChecker Instance
	{
		get
		{
			return VersionChecker.instance;
		}
	}

	private UsageInformationSender UsageInformationSender
	{
		get
		{
			return this.usageInformationSender;
		}
	}

	static VersionChecker()
	{
		VersionChecker.instance = new VersionChecker();
	}

	public VersionChecker()
	{
		EventHandler eventHandler = null;
		base();
		this.timer = new Timer();
		this.timer.Tick += new EventHandler(this.timer_Tick);
		this.usageInformationSender = new UsageInformationSender();
		if (eventHandler == null)
		{
			eventHandler = new EventHandler((, ) => this.ReadSettings());
		}
		ApplicationSettingsManager.add_SavedSettings(eventHandler);
		if (ApplicationSettingsManager.Settings.VersionCheckAtStartup)
		{
			ThreadHelper.Queue(new WaitCallback(this.QueueVersionCheck), "VERSION");
		}
	}

	private void CheckVersion(object state)
	{
		this.CheckVersion(true);
	}

	private bool CheckVersion()
	{
		return this.CheckVersion(false);
	}

	private bool CheckVersion(bool force)
	{
		bool newVersion = false;
		if (this.checking)
		{
			return newVersion;
		}
		this.checking = true;
		Version currentVersion = FileSystemHelper.CurrentVersion;
		Version latestVersion = this.GetLatestVersion();
		this.UsageInformationSender.SendUsageInformation();
		Logger.Log.InfoFormat("Latest version: {0}", latestVersion);
		if ((latestVersion > currentVersion || false) && (this.lastPromptedVersion != latestVersion || force) || forceNewVersion)
		{
			string message = SharpRegion.GetWhatsNewMessage();
			Logger.Log.InfoFormat("New version is available (Current={0}, New={1})", currentVersion, latestVersion);
			this.OnNewVersionAvailable(currentVersion, latestVersion, message);
			this.lastPromptedVersion = latestVersion;
			newVersion = true;
		}
		this.checking = false;
		return newVersion;
	}

	internal void CheckVersionAsync()
	{
		EventLog.LogInfo(Strings.CheckingForANewVersion, this);
		ThreadHelper.Queue(new WaitCallback(this.CheckVersion), "VERSION");
	}

	private Version GetLatestVersion()
	{
		Version version = new Version();
		string versionString = SharpRegion.GetLatestVersionString();
		try
		{
			return new Version(versionString);
		}
		catch (Exception ex)
		{
			Logger.Log.Debug(string.Format("Error parsing version string: {0}", versionString), ex);
		}
		return version;
	}

	public string GetUsageInformation()
	{
		return string.Format("Application_Version={0}{1}", FileSystemHelper.CurrentVersion, UsageInformationSender.Separator);
	}

	private void InternalStart()
	{
		this.ReadSettings();
	}

	protected virtual void OnNewVersionAvailable(Version currentVersion, Version latestVersion, string message)
	{
		Logger.Log.InfoFormat(string.Concat("Version upgrades = ", ApplicationSettingsManager.Settings.EnableVersionUpgrade), new object[0]);
		if (ApplicationSettingsManager.Settings.EnableVersionUpgrade)
		{
			this.PrepareUpgradeAsync(currentVersion, latestVersion, message);
			return;
		}
		if (this.NewVersionAvailable != null)
		{
			this.NewVersionAvailable(this, new VersionEventArgs(currentVersion, latestVersion, message));
		}
	}

	protected virtual void OnNoNewVersionAvailable()
	{
		if (this.NoNewVersionAvailable != null)
		{
			this.NoNewVersionAvailable(this, EventArgs.Empty);
		}
	}

	protected virtual void OnUpgradeAvailable(Version currentVersion, Version latestVersion, string message, string versionFolder)
	{
		if (this.UpgradeAvailable != null)
		{
			this.UpgradeAvailable(this, new VersionEventArgs(currentVersion, latestVersion, message, versionFolder));
		}
	}

	private void PrepareUpgrade(object state)
	{
		string zipFileName = null;
		string extractedPath = null;
		object[] args = (object[])state;
		Version currentVersion = (Version)args[0];
		Version latestVersion = (Version)args[1];
		string message = (string)args[2];
		Logger.Log.InfoFormat("Downloading a new version: {0}", latestVersion);
		if (!SharpRegion.DownloadLatestVersion(latestVersion, out zipFileName))
		{
			EventLog.LogSystem(string.Format("Failed to download version {0} of SVN-Monitor", latestVersion));
			return;
		}
		if (!FileSystemHelper.UnZip(zipFileName, out extractedPath))
		{
			EventLog.LogSystem(Strings.FailedToExtractTheDownloadedVersion);
			return;
		}
		FileSystemHelper.DeleteFile(zipFileName);
		this.SaveUpgradeState(latestVersion, extractedPath);
		this.OnUpgradeAvailable(currentVersion, latestVersion, message, extractedPath);
	}

	private void PrepareUpgradeAsync(Version currentVersion, Version latestVersion, string message)
	{
		object[] objArray = new object[3];
		objArray[0] = currentVersion;
		objArray[1] = latestVersion;
		objArray[2] = message;
		object[] state = objArray;
		ThreadHelper.Queue(new WaitCallback(this.PrepareUpgrade), "UPGRADE", state);
	}

	private void QueueVersionCheck(object state)
	{
		Logger.Log.Info("Version checker is sleeping one minute before checking");
		ThreadHelper.Sleep(60000);
		if (!this.CheckVersion())
		{
			Logger.Log.Info("No new version");
		}
	}

	public void ReadSettings()
	{
		int interval = ApplicationSettingsManager.Settings.VersionCheckInterval * 1000 * 60;
		try
		{
			this.timer.Interval = interval;
		}
		catch (ArgumentOutOfRangeException ex)
		{
			Logger.Log.Error(string.Concat("Trying to set timer's interval to: ", interval), ex);
			interval = 3600000;
			Logger.Log.Info(string.Concat("Setting interval to default: ", interval));
			this.timer.Interval = interval;
		}
		if (this.Enabled != ApplicationSettingsManager.Settings.EnableVersionCheck)
		{
			this.Enabled = ApplicationSettingsManager.Settings.EnableVersionCheck;
		}
	}

	private void SaveUpgradeState(Version version, string location)
	{
		Logger.Log.InfoFormat("Saving upgrade info: version={0}, location={1}", version, location);
		UpgradeInfo.Save(version, location);
	}

	public static void Start()
	{
		VersionChecker.Instance.InternalStart();
	}

	private void timer_Tick(object sender, EventArgs e)
	{
		try
		{
			this.CheckVersion();
		}
		catch (Exception ex)
		{
			ErrorHandler.Append(ex.Message, this, ex);
		}
	}

	public event EventHandler<VersionEventArgs> NewVersionAvailable;
	public event EventHandler NoNewVersionAvailable;
	public event EventHandler<VersionEventArgs> UpgradeAvailable;
	internal class VersionEventArgs : EventArgs
	{
		public Version CurrentVersion;

		public Version LatestVersion;

		public string Message;

		public bool UpgradeAvailable;

		public string VersionFolder;

		public VersionEventArgs(Version currentVersion, Version latestVersion, string message);

		public VersionEventArgs(Version currentVersion, Version latestVersion, string message, string versionFolder);
	}
}
}