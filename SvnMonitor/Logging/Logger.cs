using log4net;
using System;
using SVNMonitor.Helpers;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using log4net.Config;
using SVNMonitor;
using SVNMonitor.Resources.Text;
using ICSharpCode.SharpZipLib.Zip;
using log4net.Core;

namespace SVNMonitor.Logging
{
public class Logger
{
	private static ILog log;

	private static string logsDir;

	public static ILog Log
	{
		get
		{
			if (Logger.log == null)
			{
				Logger.log = new EmptyLog();
			}
			return Logger.log;
		}
		private set
		{
			Logger.log = value;
		}
	}

	public Logger()
	{
	}

	private static void ArchiveLogs()
	{
		string appData = FileSystemHelper.AppData;
		string logsDir = Path.Combine(appData, "logs");
		string archiveDir = Path.Combine(logsDir, "archive");
		Logger.ArchiveLogZips(logsDir, archiveDir);
		Logger.ZipLogs(logsDir, archiveDir);
		Logger.ZipLogs(appData, archiveDir);
	}

	private static void ArchiveLogZips(string inputDir, string outputDir)
	{
		string[] zipFiles = FileSystemHelper.GetFiles(inputDir, "*.zip", SearchOption.TopDirectoryOnly);
		if (zipFiles == null || (int)zipFiles.Length == 0)
		{
			return;
		}
		FileSystemHelper.CreateDirectory(outputDir);
		string[] strArrays = zipFiles;
		foreach (string zipFile in strArrays)
		{
			FileSystemHelper.MoveFileToDir(zipFile, outputDir);
		}
	}

	private static MethodBase GetCallingMethod()
	{
		StackTrace st = new StackTrace();
		StackFrame sf = st.GetFrame(2);
		MethodBase mb = sf.GetMethod();
		return mb;
	}

	internal static string GetLogAsString()
	{
		string tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
		FileSystemHelper.CreateDirectory(tempFolder);
		string pattern = "log*";
		FileSystemHelper.CopyFiles(Logger.logsDir, pattern, tempFolder);
		string[] files = FileSystemHelper.GetFiles(tempFolder, pattern);
		Sort<string>(files);
		StringBuilder sb = new StringBuilder();
		string[] strArrays = files;
		foreach (string file in strArrays)
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
			Logger.ArchiveLogs();
		}
		catch (Exception ex)
		{
		}
		try
		{
			Logger.logsDir = Path.Combine(FileSystemHelper.AppData, "logs");
			string logFile = string.Format(FileSystemHelper.CurrentVersion, DateTime dateTime = DateTime.Now, dateTime.Ticks);
			logFile = Path.Combine(Logger.logsDir, logFile);
			Environment.SetEnvironmentVariable("SVNMONITOR_VER", FileSystemHelper.CurrentVersion.ToString());
			Environment.SetEnvironmentVariable("SVNMONITOR_LOGFILE", logFile);
			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SVNMonitor.Logging.LoggerConfig.xml");
			using (stream)
			{
				XmlConfigurator.Configure(stream);
			}
			Logger.Log = LogManager.GetLogger("user");
			Logger.Log.Info(string.Empty.PadRight(50, 45));
			Logger.Log.Info("Log initialized.");
		}
		catch (Exception ex)
		{
			EventLog.LogError(Strings.ErrorInitializingLogger, null, ex);
		}
		try
		{
			Logger.LogSystemInfo();
			Logger.LogSessionInfo();
		}
		catch (Exception)
		{
		}
		if (archiveEx != null)
		{
			Logger.Log.Error("Could not archive previous log file.", archiveEx);
		}
	}

	private static void LogSessionInfo()
	{
		foreach (PropertyInfo prop in SessionInfo.Properties)
		{
			string name = prop.Name;
			object @value = prop.GetValue(null, null);
			Logger.Log.InfoFormat("SessionInfo.{0}: {1}", name, @value);
		}
	}

	private static void LogSystemInfo()
	{
		Logger.Log.Info(string.Concat("AppDomain.CurrentDomain.FriendlyName: ", AppDomain.CurrentDomain.FriendlyName));
		Logger.Log.Info(string.Concat("Assembly.FullName: ", Assembly.GetExecutingAssembly().FullName));
		Logger.Log.Info(string.Concat("Environment.CommandLine: ", Environment.CommandLine));
		Logger.Log.Info(string.Concat("Environment.CurrentDirectory: ", Environment.CurrentDirectory));
		Logger.Log.Info(string.Concat("Environment.MachineName: ", Environment.MachineName));
		Logger.Log.Info(string.Concat("Environment.OSVersion: ", Environment.OSVersion));
		Logger.Log.Info(string.Concat("Environment.ProcessorCount: ", Environment.ProcessorCount));
		Logger.Log.Info(string.Concat("Environment.SystemDirectory: ", Environment.SystemDirectory));
		Logger.Log.Info(string.Concat("Environment.UserDomainName: ", Environment.UserDomainName));
		Logger.Log.Info(string.Concat("Environment.UserInteractive: ", Environment.UserInteractive));
		Logger.Log.Info(string.Concat("Environment.UserName: ", Environment.UserName));
		Logger.Log.Info(string.Concat("Environment.Version: ", Environment.Version));
		Logger.Log.Info(string.Concat("Environment.WorkingSet: ", Environment.WorkingSet));
	}

	public static void LogUserAction(string info)
	{
		try
		{
			MethodBase mb = Logger.GetCallingMethod();
			Logger.Log.DebugFormat("User: {0}.{1} ({2})", mb.DeclaringType.Name, mb.Name, info);
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error getting method details: ", ex);
		}
	}

	public static void LogUserAction()
	{
		try
		{
			MethodBase mb = Logger.GetCallingMethod();
			Logger.Log.DebugFormat("User: {0}.{1}", mb.DeclaringType.Name, mb.Name);
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error: ", ex);
		}
	}

	private static void ZipLogs(string inputDir, string outputDir)
	{
		string logsPattern = "log*.*";
		string[] logFiles = FileSystemHelper.GetFiles(inputDir, logsPattern, SearchOption.TopDirectoryOnly);
		if (logFiles == null || (int)logFiles.Length == 0)
		{
			return;
		}
		Sort<string>(logFiles);
		string firstFile = logFiles[0];
		string firstDate = Path.GetExtension(firstFile).Replace(".", string.Empty);
		string firstVersion = Path.GetFileNameWithoutExtension(firstFile).Replace("log.", string.Empty).Replace(".zip", string.Empty);
		string zipFileName = Path.Combine(outputDir, string.Format("log.{0}.{1}.zip", firstVersion, firstDate));
		FileSystemHelper.CreateDirectory(outputDir);
		FastZip zip = new FastZip();
		zip.CreateZip(zipFileName, inputDir, false, logsPattern);
		FileSystemHelper.DeleteFiles(inputDir, logsPattern, SearchOption.TopDirectoryOnly);
	}

	internal class EmptyLog : ILog, ILoggerWrapper
	{
		public bool IsDebugEnabled;

		public bool IsErrorEnabled;

		public bool IsFatalEnabled;

		public bool IsInfoEnabled;

		public bool IsWarnEnabled;

		public ILogger Logger;

		public EmptyLog();

		public void Debug(object message, Exception exception);

		public void Debug(object message);

		public void DebugFormat(IFormatProvider provider, string format, object[] args);

		public void DebugFormat(string format, object arg0, object arg1, object arg2);

		public void DebugFormat(string format, object arg0, object arg1);

		public void DebugFormat(string format, object arg0);

		public void DebugFormat(string format, object[] args);

		public void Error(object message, Exception exception);

		public void Error(object message);

		public void ErrorFormat(IFormatProvider provider, string format, object[] args);

		public void ErrorFormat(string format, object arg0, object arg1, object arg2);

		public void ErrorFormat(string format, object arg0, object arg1);

		public void ErrorFormat(string format, object arg0);

		public void ErrorFormat(string format, object[] args);

		public void Fatal(object message, Exception exception);

		public void Fatal(object message);

		public void FatalFormat(IFormatProvider provider, string format, object[] args);

		public void FatalFormat(string format, object arg0, object arg1, object arg2);

		public void FatalFormat(string format, object arg0, object arg1);

		public void FatalFormat(string format, object arg0);

		public void FatalFormat(string format, object[] args);

		public void Info(object message, Exception exception);

		public void Info(object message);

		public void InfoFormat(IFormatProvider provider, string format, object[] args);

		public void InfoFormat(string format, object arg0, object arg1, object arg2);

		public void InfoFormat(string format, object arg0, object arg1);

		public void InfoFormat(string format, object arg0);

		public void InfoFormat(string format, object[] args);

		public void Warn(object message, Exception exception);

		public void Warn(object message);

		public void WarnFormat(IFormatProvider provider, string format, object[] args);

		public void WarnFormat(string format, object arg0, object arg1, object arg2);

		public void WarnFormat(string format, object arg0, object arg1);

		public void WarnFormat(string format, object arg0);

		public void WarnFormat(string format, object[] args);
	}
}
}