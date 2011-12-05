namespace SVNMonitor.Support
{
    using SVNMonitor;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    [Serializable]
    public class UpgradeInfo
    {
        internal const string FileName = "svnmonitor.upgrade";
        private const string SomeGuid = "0ddb3be7-06e8-4562-9ec5-a0be99f34d75";

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
            return FileSystemHelper.CalculateMD5Hash(location);
        }

        internal static bool CheckIfUpgradeReady()
        {
            UpgradeInfo info = null;
            bool isUpgradeReady = IsUpgradeReady(out info);
            if (isUpgradeReady)
            {
                Logger.Log.Info("An upgrade is ready from a previous session:");
                Logger.Log.InfoFormat("Version: {0}", info.Version);
                Logger.Log.InfoFormat("Location: {0}", info.Location);
                try
                {
                    UpgradeSaved(info);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Error trying to upgrade: ", ex);
                    DeleteSavedUpgradeInfo();
                    isUpgradeReady = false;
                }
            }
            return isUpgradeReady;
        }

        internal static void DeleteSavedUpgradeInfo()
        {
            try
            {
                Logger.Log.InfoFormat("Deleting upgrade info: {0}", FileFullName);
                FileSystemHelper.DeleteFile(FileFullName);
            }
            catch (Exception ex)
            {
                Logger.Log.Debug("Error deleting the upgrade info: ", ex);
            }
        }

        private static bool IsUpgradeReady(out UpgradeInfo info)
        {
            info = null;
            try
            {
                if (!FileSystemHelper.FileExists(FileFullName))
                {
                    return false;
                }
                info = SerializationHelper.XmlFileDeserialize<UpgradeInfo>(FileFullName);
                if (info == null)
                {
                    return false;
                }
                if (!info.Verify())
                {
                    Logger.Log.InfoFormat("Upgrade info was not verified", new object[0]);
                    return false;
                }
                System.Version savedVersion = new System.Version(info.Version);
                if (FileSystemHelper.CurrentVersion >= savedVersion)
                {
                    Logger.Log.InfoFormat("The version of the saved upgrade is not higher than the current version.", new object[0]);
                    Logger.Log.InfoFormat("Current version={0}, saved upgrade version={1}", FileSystemHelper.CurrentVersion, info.Version);
                    DeleteSavedUpgradeInfo();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error trying to verify upgrade info", ex);
                DeleteSavedUpgradeInfo();
                return false;
            }
        }

        internal static void Save(System.Version version, string location)
        {
            byte[] hash = CalculateHash(location);
            UpgradeInfo <>g__initLocal0 = new UpgradeInfo {
                Version = version.ToString(),
                Location = location,
                Hash = hash
            };
            UpgradeInfo info = <>g__initLocal0;
            SerializationHelper.XmlFileSerialize<UpgradeInfo>(info, FileFullName);
        }

        internal static void Upgrade(string versionPath, string version)
        {
            string upgraderFileName = Path.Combine(versionPath, "svnmonitor-upgrade.exe");
            Process process = Process.GetCurrentProcess();
            int processId = process.Id;
            string processName = process.ProcessName;
            string arguments = BuildUpgraderCommandLineArguments(processId, processName, version, Application.StartupPath, SessionInfo.CommandLineArguments);
            SVNMonitor.EventLog.LogSystem(Strings.PreUpgradeNotification);
            Process.Start(upgraderFileName, arguments);
        }

        private static void UpgradeSaved(UpgradeInfo info)
        {
            Upgrade(info.Location, info.Version);
        }

        private bool Verify()
        {
            byte[] hash = FileSystemHelper.CalculateMD5Hash(this.Location);
            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != this.Hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static string FileFullName
        {
            get
            {
                return Path.Combine(FileSystemHelper.AppData, "svnmonitor.upgrade");
            }
        }

        public byte[] Hash { get; set; }

        public string Location { get; set; }

        public string Version { get; set; }
    }
}

