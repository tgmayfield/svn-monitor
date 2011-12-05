using System;
using System.Diagnostics;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.Settings;

namespace SVNMonitor.Support
{
	internal class VersionChecker
	{
		private bool checking;
		private static readonly VersionChecker instance = new VersionChecker();
		private Version lastPromptedVersion;
		private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
		private readonly SVNMonitor.Support.UsageInformationSender usageInformationSender;

		public event EventHandler<VersionEventArgs> NewVersionAvailable;

		public event EventHandler NoNewVersionAvailable;

		public event EventHandler<VersionEventArgs> UpgradeAvailable;

		public VersionChecker()
		{
			timer.Tick += timer_Tick;
			usageInformationSender = new SVNMonitor.Support.UsageInformationSender();
			ApplicationSettingsManager.SavedSettings += (s, ea) => ReadSettings();
			if (ApplicationSettingsManager.Settings.VersionCheckAtStartup)
			{
				SVNMonitor.Helpers.ThreadHelper.Queue(QueueVersionCheck, "VERSION");
			}
		}

		private bool CheckVersion()
		{
			return CheckVersion(false);
		}

		private bool CheckVersion(bool force)
		{
			bool newVersion = false;
			if (!checking)
			{
				checking = true;
				Version currentVersion = FileSystemHelper.CurrentVersion;
				Version latestVersion = GetLatestVersion();
				UsageInformationSender.SendUsageInformation();
				Logger.Log.InfoFormat("Latest version: {0}", latestVersion);
				bool forceNewVersion = false;
				if ((latestVersion > currentVersion) || forceNewVersion)
				{
					if (((lastPromptedVersion != latestVersion) || force) || forceNewVersion)
					{
						string message = Web.SharpRegion.GetWhatsNewMessage();
						Logger.Log.InfoFormat("New version is available (Current={0}, New={1})", currentVersion, latestVersion);
						OnNewVersionAvailable(currentVersion, latestVersion, message);
						lastPromptedVersion = latestVersion;
						newVersion = true;
					}
				}
				else if (force)
				{
					OnNoNewVersionAvailable();
				}
				checking = false;
			}
			return newVersion;
		}

		private void CheckVersion(object state)
		{
			CheckVersion(true);
		}

		internal void CheckVersionAsync()
		{
			SVNMonitor.EventLog.LogInfo(Strings.CheckingForANewVersion, this);
			SVNMonitor.Helpers.ThreadHelper.Queue(CheckVersion, "VERSION");
		}

		private Version GetLatestVersion()
		{
			Version version = new Version();
			string versionString = Web.SharpRegion.GetLatestVersionString();
			try
			{
				version = new Version(versionString);
			}
			catch (Exception ex)
			{
				Logger.Log.Debug(string.Format("Error parsing version string: {0}", versionString), ex);
			}
			return version;
		}

		public string GetUsageInformation()
		{
			return string.Format("Application_Version={0}{1}", FileSystemHelper.CurrentVersion, SVNMonitor.Support.UsageInformationSender.Separator);
		}

		private void InternalStart()
		{
			ReadSettings();
		}

		protected virtual void OnNewVersionAvailable(Version currentVersion, Version latestVersion, string message)
		{
			Logger.Log.InfoFormat("Version upgrades = " + ApplicationSettingsManager.Settings.EnableVersionUpgrade, new object[0]);
			if (ApplicationSettingsManager.Settings.EnableVersionUpgrade)
			{
				PrepareUpgradeAsync(currentVersion, latestVersion, message);
			}
			else if (NewVersionAvailable != null)
			{
				NewVersionAvailable(this, new VersionEventArgs(currentVersion, latestVersion, message));
			}
		}

		protected virtual void OnNoNewVersionAvailable()
		{
			if (NoNewVersionAvailable != null)
			{
				NoNewVersionAvailable(this, EventArgs.Empty);
			}
		}

		protected virtual void OnUpgradeAvailable(Version currentVersion, Version latestVersion, string message, string versionFolder)
		{
			if (UpgradeAvailable != null)
			{
				UpgradeAvailable(this, new VersionEventArgs(currentVersion, latestVersion, message, versionFolder));
			}
		}

		private void PrepareUpgrade(object state)
		{
			string zipFileName;
			object[] args = (object[])state;
			Version currentVersion = (Version)args[0];
			Version latestVersion = (Version)args[1];
			string message = (string)args[2];
			Logger.Log.InfoFormat("Downloading a new version: {0}", latestVersion);
			if (!Web.SharpRegion.DownloadLatestVersion(latestVersion, out zipFileName))
			{
				SVNMonitor.EventLog.LogSystem(string.Format("Failed to download version {0} of SVN-Monitor", latestVersion));
			}
			else
			{
				string extractedPath;
				if (!FileSystemHelper.UnZip(zipFileName, out extractedPath))
				{
					SVNMonitor.EventLog.LogSystem(Strings.FailedToExtractTheDownloadedVersion);
				}
				else
				{
					FileSystemHelper.DeleteFile(zipFileName);
					SaveUpgradeState(latestVersion, extractedPath);
					OnUpgradeAvailable(currentVersion, latestVersion, message, extractedPath);
				}
			}
		}

		private void PrepareUpgradeAsync(Version currentVersion, Version latestVersion, string message)
		{
			object[] state = new object[]
			{
				currentVersion, latestVersion, message
			};
			SVNMonitor.Helpers.ThreadHelper.Queue(PrepareUpgrade, "UPGRADE", state);
		}

		private void QueueVersionCheck(object state)
		{
			Logger.Log.Info("Version checker is sleeping one minute before checking");
			SVNMonitor.Helpers.ThreadHelper.Sleep(0xea60);
			if (!CheckVersion())
			{
				Logger.Log.Info("No new version");
			}
		}

		public void ReadSettings()
		{
			int interval = (ApplicationSettingsManager.Settings.VersionCheckInterval * 0x3e8) * 60;
			try
			{
				timer.Interval = interval;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				Logger.Log.Error("Trying to set timer's interval to: " + interval, ex);
				interval = 0x36ee80;
				Logger.Log.Info("Setting interval to default: " + interval);
				timer.Interval = interval;
			}
			if (Enabled != ApplicationSettingsManager.Settings.EnableVersionCheck)
			{
				Enabled = ApplicationSettingsManager.Settings.EnableVersionCheck;
			}
		}

		private void SaveUpgradeState(Version version, string location)
		{
			Logger.Log.InfoFormat("Saving upgrade info: version={0}, location={1}", version, location);
			UpgradeInfo.Save(version, location);
		}

		public static void Start()
		{
			Instance.InternalStart();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			try
			{
				CheckVersion();
			}
			catch (Exception ex)
			{
				ErrorHandler.Append(ex.Message, this, ex);
			}
		}

		public bool Enabled
		{
			get { return timer.Enabled; }
			set
			{
				timer.Enabled = value;
				Logger.Log.InfoFormat("Version checker is now {0}", value ? "enabled" : "disabled");
			}
		}

		internal static VersionChecker Instance
		{
			[DebuggerNonUserCode]
			get { return instance; }
		}

		private SVNMonitor.Support.UsageInformationSender UsageInformationSender
		{
			[DebuggerNonUserCode]
			get { return usageInformationSender; }
		}

		internal class VersionEventArgs : EventArgs
		{
			public VersionEventArgs(Version currentVersion, Version latestVersion, string message)
				: this(currentVersion, latestVersion, message, null)
			{
			}

			public VersionEventArgs(Version currentVersion, Version latestVersion, string message, string versionFolder)
			{
				CurrentVersion = currentVersion;
				LatestVersion = latestVersion;
				Message = message;
				VersionFolder = versionFolder;
			}

			public Version CurrentVersion { get; private set; }

			public Version LatestVersion { get; private set; }

			public string Message { get; private set; }

			public bool UpgradeAvailable
			{
				get { return !string.IsNullOrEmpty(VersionFolder); }
			}

			public string VersionFolder { get; private set; }
		}
	}
}