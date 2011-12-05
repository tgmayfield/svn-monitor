using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

using ICSharpCode.SharpZipLib.Zip;

using SVNMonitor.Helpers;
using SVNMonitor.Resources.Text;

using log4net;
using log4net.Config;
using log4net.Core;

namespace SVNMonitor.Logging
{
	public class Logger
	{
		private static ILog log;
		private static string logsDir;

		private static void ArchiveLogs()
		{
			string appData = FileSystemHelper.AppData;
			string logsDir = Path.Combine(appData, "logs");
			string archiveDir = Path.Combine(logsDir, "archive");
			ArchiveLogZips(logsDir, archiveDir);
			ZipLogs(logsDir, archiveDir);
			ZipLogs(appData, archiveDir);
		}

		private static void ArchiveLogZips(string inputDir, string outputDir)
		{
			string[] zipFiles = FileSystemHelper.GetFiles(inputDir, "*.zip", SearchOption.TopDirectoryOnly);
			if ((zipFiles != null) && (zipFiles.Length != 0))
			{
				FileSystemHelper.CreateDirectory(outputDir);
				foreach (string zipFile in zipFiles)
				{
					FileSystemHelper.MoveFileToDir(zipFile, outputDir);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static MethodBase GetCallingMethod()
		{
			StackTrace st = new StackTrace();
			return st.GetFrame(2).GetMethod();
		}

		internal static string GetLogAsString()
		{
			string tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			FileSystemHelper.CreateDirectory(tempFolder);
			string pattern = "log*";
			FileSystemHelper.CopyFiles(logsDir, pattern, tempFolder);
			string[] files = FileSystemHelper.GetFiles(tempFolder, pattern);
			Array.Sort(files);
			StringBuilder sb = new StringBuilder();
			foreach (string file in files)
			{
				string text = FileSystemHelper.ReadAllText(file);
				sb.AppendLine(file);
				sb.AppendLine("-------");
				sb.AppendLine(text);
				sb.AppendLine("#################");
			}
			FileSystemHelper.DeleteDirectory(tempFolder, true);
			return sb.ToString();
		}

		public static void Init()
		{
			Exception archiveEx = null;
			try
			{
				ArchiveLogs();
			}
			catch (Exception ex)
			{
				archiveEx = ex;
			}
			try
			{
				logsDir = Path.Combine(FileSystemHelper.AppData, "logs");
				string logFile = string.Format("log.{0}.{1}", FileSystemHelper.CurrentVersion, DateTime.Now.Ticks);
				logFile = Path.Combine(logsDir, logFile);
				Environment.SetEnvironmentVariable("SVNMONITOR_VER", FileSystemHelper.CurrentVersion.ToString());
				Environment.SetEnvironmentVariable("SVNMONITOR_LOGFILE", logFile);
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SVNMonitor.Logging.LoggerConfig.xml"))
				{
					XmlConfigurator.Configure(stream);
				}
				Log = LogManager.GetLogger("user");
				Log.Info(string.Empty.PadRight(50, '-'));
				Log.Info("Log initialized.");
			}
			catch (Exception ex)
			{
				SVNMonitor.EventLog.LogError(Strings.ErrorInitializingLogger, null, ex);
				return;
			}
			try
			{
				LogSystemInfo();
				LogSessionInfo();
			}
			catch (Exception)
			{
			}
			if (archiveEx != null)
			{
				Log.Error("Could not archive previous log file.", archiveEx);
			}
		}

		private static void LogSessionInfo()
		{
			foreach (PropertyInfo prop in SessionInfo.Properties)
			{
				string name = prop.Name;
				object value = prop.GetValue(null, null);
				Log.InfoFormat("SessionInfo.{0}: {1}", name, value);
			}
		}

		private static void LogSystemInfo()
		{
			Log.Info("AppDomain.CurrentDomain.FriendlyName: " + AppDomain.CurrentDomain.FriendlyName);
			Log.Info("Assembly.FullName: " + Assembly.GetExecutingAssembly().FullName);
			Log.Info("Environment.CommandLine: " + Environment.CommandLine);
			Log.Info("Environment.CurrentDirectory: " + Environment.CurrentDirectory);
			Log.Info("Environment.MachineName: " + Environment.MachineName);
			Log.Info("Environment.OSVersion: " + Environment.OSVersion);
			Log.Info("Environment.ProcessorCount: " + Environment.ProcessorCount);
			Log.Info("Environment.SystemDirectory: " + Environment.SystemDirectory);
			Log.Info("Environment.UserDomainName: " + Environment.UserDomainName);
			Log.Info("Environment.UserInteractive: " + Environment.UserInteractive);
			Log.Info("Environment.UserName: " + Environment.UserName);
			Log.Info("Environment.Version: " + Environment.Version);
			Log.Info("Environment.WorkingSet: " + Environment.WorkingSet);
		}

		public static void LogUserAction()
		{
			try
			{
				MethodBase mb = GetCallingMethod();
				Log.DebugFormat("User: {0}.{1}", mb.DeclaringType.Name, mb.Name);
			}
			catch (Exception ex)
			{
				Log.Error("Error: ", ex);
			}
		}

		public static void LogUserAction(string info)
		{
			try
			{
				MethodBase mb = GetCallingMethod();
				Log.DebugFormat("User: {0}.{1} ({2})", mb.DeclaringType.Name, mb.Name, info);
			}
			catch (Exception ex)
			{
				Log.Error("Error getting method details: ", ex);
			}
		}

		private static void ZipLogs(string inputDir, string outputDir)
		{
			string logsPattern = "log*.*";
			string[] logFiles = FileSystemHelper.GetFiles(inputDir, logsPattern, SearchOption.TopDirectoryOnly);
			if ((logFiles != null) && (logFiles.Length != 0))
			{
				Array.Sort(logFiles);
				string firstFile = logFiles[0];
				string firstDate = Path.GetExtension(firstFile).Replace(".", string.Empty);
				string firstVersion = Path.GetFileNameWithoutExtension(firstFile).Replace("log.", string.Empty).Replace(".zip", string.Empty);
				string zipFileName = Path.Combine(outputDir, string.Format("log.{0}.{1}.zip", firstVersion, firstDate));
				FileSystemHelper.CreateDirectory(outputDir);
				new FastZip().CreateZip(zipFileName, inputDir, false, logsPattern);
				FileSystemHelper.DeleteFiles(inputDir, logsPattern, SearchOption.TopDirectoryOnly);
			}
		}

		public static ILog Log
		{
			get
			{
				if (log == null)
				{
					log = new EmptyLog();
				}
				return log;
			}
			private set { log = value; }
		}

		internal class EmptyLog : ILog, ILoggerWrapper
		{
			public void Debug(object message)
			{
			}

			public void Debug(object message, Exception exception)
			{
			}

			public void DebugFormat(string format, object arg0)
			{
			}

			public void DebugFormat(string format, params object[] args)
			{
			}

			public void DebugFormat(IFormatProvider provider, string format, params object[] args)
			{
			}

			public void DebugFormat(string format, object arg0, object arg1)
			{
			}

			public void DebugFormat(string format, object arg0, object arg1, object arg2)
			{
			}

			public void Error(object message)
			{
			}

			public void Error(object message, Exception exception)
			{
			}

			public void ErrorFormat(string format, params object[] args)
			{
			}

			public void ErrorFormat(string format, object arg0)
			{
			}

			public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
			{
			}

			public void ErrorFormat(string format, object arg0, object arg1)
			{
			}

			public void ErrorFormat(string format, object arg0, object arg1, object arg2)
			{
			}

			public void Fatal(object message)
			{
			}

			public void Fatal(object message, Exception exception)
			{
			}

			public void FatalFormat(string format, params object[] args)
			{
			}

			public void FatalFormat(string format, object arg0)
			{
			}

			public void FatalFormat(IFormatProvider provider, string format, params object[] args)
			{
			}

			public void FatalFormat(string format, object arg0, object arg1)
			{
			}

			public void FatalFormat(string format, object arg0, object arg1, object arg2)
			{
			}

			public void Info(object message)
			{
			}

			public void Info(object message, Exception exception)
			{
			}

			public void InfoFormat(string format, params object[] args)
			{
			}

			public void InfoFormat(string format, object arg0)
			{
			}

			public void InfoFormat(IFormatProvider provider, string format, params object[] args)
			{
			}

			public void InfoFormat(string format, object arg0, object arg1)
			{
			}

			public void InfoFormat(string format, object arg0, object arg1, object arg2)
			{
			}

			public void Warn(object message)
			{
			}

			public void Warn(object message, Exception exception)
			{
			}

			public void WarnFormat(string format, params object[] args)
			{
			}

			public void WarnFormat(string format, object arg0)
			{
			}

			public void WarnFormat(IFormatProvider provider, string format, params object[] args)
			{
			}

			public void WarnFormat(string format, object arg0, object arg1)
			{
			}

			public void WarnFormat(string format, object arg0, object arg1, object arg2)
			{
			}

			public bool IsDebugEnabled
			{
				get { return false; }
			}

			public bool IsErrorEnabled
			{
				get { return false; }
			}

			public bool IsFatalEnabled
			{
				get { return false; }
			}

			public bool IsInfoEnabled
			{
				get { return false; }
			}

			public bool IsWarnEnabled
			{
				get { return false; }
			}

			public ILogger Logger
			{
				get { return null; }
			}
		}
	}
}