using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Microsoft.Win32;

using SVNMonitor.Logging;
using SVNMonitor.Settings;

namespace SVNMonitor.Helpers
{
	internal class TortoiseSVNHelper
	{
		private const string TortoiseSVNCommandRegistryKey = @"svn\shell\open\command";
		private const string TortoiseSVNGlobalIgnoresValueName = "global-ignores";
		private const string TortoiseSVNRegistry_TortoiseMergeValueName = "TMergePath";
		private const string TortoiseSVNRegistry_TortoiseProcValueName = "ProcPath";
		private const string TortoiseSVNRegistryKey = @"SOFTWARE\TortoiseSVN";
		private const string TortoiseSVNSettingsRegistryKey = @"Software\Tigris.org\Subversion\Config\miscellany";

		public static string GetGlobalIgnorePattern()
		{
			string pattern = string.Empty;
			try
			{
				pattern = RegistryHelper.GetStringValue(Path.Combine(Registry.CurrentUser.Name, @"Software\Tigris.org\Subversion\Config\miscellany"), "global-ignores");
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error getting the global-ignore-pattern from TortoiseSVN.", ex);
			}
			return pattern;
		}

		private static string GetTortoiseSVNMergePath()
		{
			string path = GetTortoiseSVNPath("TMergePath");
			if (string.IsNullOrEmpty(path))
			{
				path = GetTortoiseSVNMergePath_Alternate();
			}
			return path;
		}

		private static string GetTortoiseSVNMergePath_Alternate()
		{
			string tortoiseSVNPath = TortoiseSVNProcPath;
			if (string.IsNullOrEmpty(tortoiseSVNPath))
			{
				return string.Empty;
			}
			int index = tortoiseSVNPath.ToLower().LastIndexOf("tortoiseproc.exe");
			return Path.Combine(tortoiseSVNPath.Substring(0, index), "TortoiseMerge.exe");
		}

		private static string GetTortoiseSVNPath(string valueName)
		{
			string path = RegistryHelper.GetStringValue(Path.Combine(Registry.LocalMachine.Name, @"SOFTWARE\TortoiseSVN"), valueName);
			Logger.Log.DebugFormat("TortoiseSVN should be located at " + path, new object[0]);
			if (FileSystemHelper.FileExists(path))
			{
				try
				{
					FileVersionInfo version = FileVersionInfo.GetVersionInfo(path);
					Logger.Log.InfoFormat("{0} version: {1}", path, version.FileVersion);
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Error trying to get TortoiseSVN version number.", ex);
				}
				return path;
			}
			Logger.Log.Error("TortoiseSVN was not found, it might not be installed.");
			return path;
		}

		internal static string GetTortoiseSVNProcPath()
		{
			string path = GetTortoiseSVNPath("ProcPath");
			if (string.IsNullOrEmpty(path))
			{
				Logger.Log.Debug("TortoiseProc.exe can't be found, trying an alternate method to locate it...");
				path = GetTortoiseSVNProcPath_Alternate();
			}
			return path;
		}

		private static string GetTortoiseSVNProcPath_Alternate()
		{
			string path = RegistryHelper.GetStringValue(Path.Combine(Registry.ClassesRoot.Name, @"svn\shell\open\command"));
			if (path != null)
			{
				int index = path.LastIndexOf("/command:");
				path = path.Substring(0, index).Trim();
				Logger.Log.DebugFormat("TortoiseProc.exe may be found at " + path, new object[0]);
				if (FileSystemHelper.FileExists(path))
				{
					return path;
				}
				path = path.Replace(FileSystemHelper.ProgramFiles, FileSystemHelper.ProgramFilesx86);
				Logger.Log.DebugFormat("TortoiseProc.exe may be found at " + path, new object[0]);
				if (FileSystemHelper.FileExists(path))
				{
					return path;
				}
			}
			return null;
		}

		public static bool IsInGlogalIgnorePattern(string name)
		{
			if (!ApplicationSettingsManager.Settings.UseTortoiseSVNGlobalIgnorePattern)
			{
				return false;
			}
			string ignorePattern = GetGlobalIgnorePattern();
			if (string.IsNullOrEmpty(ignorePattern))
			{
				return false;
			}
			return ignorePattern.Split(new[]
			{
				' '
			}).Any(p => WildcardPatternMatcher.IsFileOrDirectoryMatch(p, name));
		}

		public static string TortoiseSVNMergePath
		{
			get { return GetTortoiseSVNMergePath(); }
		}

		public static string TortoiseSVNProcPath
		{
			get
			{
				string path = string.Empty;
				if (ApplicationSettingsManager.Settings.AutomaticallyResolveTortoiseSVNProcess)
				{
					return GetTortoiseSVNProcPath();
				}
				path = ApplicationSettingsManager.Settings.TortoiseSVNPath;
				try
				{
					FileVersionInfo version = FileVersionInfo.GetVersionInfo(path);
					Logger.Log.InfoFormat("TortoiseSVN version: {0}", version.FileVersion);
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Error trying to get TortoiseSVN version number.", ex);
				}
				if (string.IsNullOrEmpty(path))
				{
					path = GetTortoiseSVNProcPath();
					ApplicationSettingsManager.Settings.TortoiseSVNPath = path;
					ApplicationSettingsManager.SaveSettings();
				}
				return path;
			}
		}
	}
}