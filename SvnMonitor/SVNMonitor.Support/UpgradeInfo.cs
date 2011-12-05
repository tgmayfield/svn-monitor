using System;
using System.IO;
using SVNMonitor.Helpers;
using System.Text;
using System.Security.Cryptography;
using SVNMonitor.Logging;
using System.Diagnostics;
using System.Windows.Forms;
using SVNMonitor;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Support
{
[Serializable]
public class UpgradeInfo
{
	internal const string FileName = "svnmonitor.upgrade";

	private const string SomeGuid = "0ddb3be7-06e8-4562-9ec5-a0be99f34d75";

	private static string FileFullName
	{
		get
		{
			return Path.Combine(FileSystemHelper.AppData, "svnmonitor.upgrade");
		}
	}

	public byte[] Hash
	{
		get;
		set;
	}

	public string Location
	{
		get;
		set;
	}

	public string Version
	{
		get;
		set;
	}

	public UpgradeInfo()
	{
	}

	private static string BuildUpgraderCommandLineArguments(int processId, string processName, string version, string target, string args)
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendFormat("-guid={0} ", "0ddb3be7-06e8-4562-9ec5-a0be99f34d75");
		sb.AppendFormat("-id={0} ", processId);
		sb.AppendFormat("-name=\"{0}\" ", processName);
		sb.AppendFormat("-version={0} ", version);
		sb.AppendFormat("-target=\"{0}\" ", target);
		sb.AppendFormat("-args=\"{0}\" ", args);
		return sb.ToString();
	}

	private static byte[] CalculateHash(string location)
	{
		MD5.Create();
		byte[] hash = FileSystemHelper.CalculateMD5Hash(location);
		return hash;
	}

	internal static bool CheckIfUpgradeReady()
	{
		UpgradeInfo info = null;
		bool isUpgradeReady = UpgradeInfo.IsUpgradeReady(out info);
		if (isUpgradeReady)
		{
			Logger.Log.Info("An upgrade is ready from a previous session:");
			Logger.Log.InfoFormat("Version: {0}", info.Version);
			Logger.Log.InfoFormat("Location: {0}", info.Location);
			try
			{
				UpgradeInfo.UpgradeSaved(info);
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error trying to upgrade: ", ex);
				UpgradeInfo.DeleteSavedUpgradeInfo();
				return false;
			}
		}
		return isUpgradeReady;
	}

	internal static void DeleteSavedUpgradeInfo()
	{
		try
		{
			Logger.Log.InfoFormat("Deleting upgrade info: {0}", UpgradeInfo.FileFullName);
			FileSystemHelper.DeleteFile(UpgradeInfo.FileFullName);
		}
		catch (Exception ex)
		{
			Logger.Log.Debug("Error deleting the upgrade info: ", ex);
		}
	}

	private static bool IsUpgradeReady(out UpgradeInfo info)
	{
		try
		{
			if (!FileSystemHelper.FileExists(UpgradeInfo.FileFullName))
			{
				return false;
			}
			if (info == null)
			{
				return false;
			}
			if (!info.Verify())
			{
				Logger.Log.InfoFormat("Upgrade info was not verified", new object[0]);
				return false;
			}
			Version savedVersion = new Version(info.Version);
			if (FileSystemHelper.CurrentVersion >= savedVersion)
			{
				Logger.Log.InfoFormat("The version of the saved upgrade is not higher than the current version.", new object[0]);
				Logger.Log.InfoFormat("Current version={0}, saved upgrade version={1}", FileSystemHelper.CurrentVersion, info.Version);
				UpgradeInfo.DeleteSavedUpgradeInfo();
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error trying to verify upgrade info", ex);
			UpgradeInfo.DeleteSavedUpgradeInfo();
			return false;
		}
	}

	internal static void Save(Version version, string location)
	{
		byte[] hash = UpgradeInfo.CalculateHash(location);
		UpgradeInfo upgradeInfo = new UpgradeInfo();
		upgradeInfo.Version = version.ToString();
		upgradeInfo.Location = location;
		upgradeInfo.Hash = hash;
		UpgradeInfo info = upgradeInfo;
		SerializationHelper.XmlFileSerialize<UpgradeInfo>(info, UpgradeInfo.FileFullName);
	}

	internal static void Upgrade(string versionPath, string version)
	{
		string upgraderFileName = Path.Combine(versionPath, "svnmonitor-upgrade.exe");
		Process process = Process.GetCurrentProcess();
		int processId = process.Id;
		string processName = process.ProcessName;
		string arguments = UpgradeInfo.BuildUpgraderCommandLineArguments(processId, processName, version, Application.StartupPath, SessionInfo.CommandLineArguments);
		EventLog.LogSystem(Strings.PreUpgradeNotification);
		Process.Start(upgraderFileName, arguments);
	}

	private static void UpgradeSaved(UpgradeInfo info)
	{
		UpgradeInfo.Upgrade(info.Location, info.Version);
	}

	private bool Verify()
	{
		byte[] hash = FileSystemHelper.CalculateMD5Hash(this.Location);
		int i = 0;
		i++;
		while (i < (int)hash.Length)
		{
			if (hash[i] != this.Hash[i])
			{
				return false;
			}
		}
		return true;
	}
}
}