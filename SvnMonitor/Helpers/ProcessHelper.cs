using System;
using System.Diagnostics;
using SVNMonitor.Logging;
using System.Text;
using System.Reflection;
using SVNMonitor.Support;
using System.Security.Principal;
using System.Timers;
using System.Runtime.InteropServices;
using System.IO;

namespace SVNMonitor.Helpers
{
internal class ProcessHelper
{
	public ProcessHelper()
	{
	}

	public static void FlushMemory()
	{
		try
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			if (Environment.OSVersion.Platform == 2)
			{
				ProcessHelper.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error flushing memory", ex);
		}
	}

	public static Process GetCurrentProcess()
	{
		Process process = Process.GetCurrentProcess();
		return process;
	}

	internal static string GetLoadedAssemblies()
	{
		StringBuilder sb = new StringBuilder();
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		Assembly[] assemblyArray = assemblies;
		foreach (Assembly assembly in assemblyArray)
		{
			sb.AppendFormat("AppDomain_Assembly={0}{1}", assembly, UsageInformationSender.Separator);
			sb.AppendFormat("AppDomain_AssemblyLocation={0}{1}", assembly.Location, UsageInformationSender.Separator);
		}
		return sb.ToString();
	}

	public static Process[] GetProcessesByName(string processName)
	{
		Process[] processes = Process.GetProcessesByName(processName);
		return processes;
	}

	internal static string GetUsageInformation()
	{
		return string.Format("Process_IsAdmin={0}{1}", ProcessHelper.IsRunningAsAdministrator(), UsageInformationSender.Separator);
	}

	public static bool IsInVisualStudio()
	{
		string currentProcessName = ProcessHelper.GetCurrentProcess().ProcessName;
		return currentProcessName.Equals("devenv", StringComparison.InvariantCultureIgnoreCase);
	}

	public static bool IsRunningAsAdministrator()
	{
		WindowsIdentity wi = WindowsIdentity.GetCurrent();
		WindowsPrincipal wp = new WindowsPrincipal(wi);
		bool isAdmin = wp.IsInRole(WindowsBuiltInRole.Administrator);
		Logger.Log.DebugFormat(string.Concat("IsRunningAsAdministrator = ", isAdmin), new object[0]);
		return isAdmin;
	}

	internal static void LogLoadedAssemblies()
	{
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		Assembly[] assemblyArray = assemblies;
		foreach (Assembly assembly in assemblyArray)
		{
			Logger.Log.DebugFormat("Assembly={0}", assembly);
			Logger.Log.DebugFormat("AssemblyLocation={0}", assembly.Location);
		}
	}

	internal static void SetAsyncWait(int timeout, ElapsedEventHandler handler)
	{
		Timer timer = new Timer((double)timeout);
		timer.AutoReset = false;
		timer.Elapsed += handler;
		timer.Start();
	}

	[DllImport("kernel32.dll", CharSet=CharSet.Ansi)]
	private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

	public static Process StartProcess(string fileName)
	{
		Process process = ProcessHelper.StartProcess(new ProcessStartInfo(fileName));
		return process;
	}

	public static Process StartProcess(string fileName, string arguments)
	{
		Process process = ProcessHelper.StartProcess(new ProcessStartInfo(fileName, arguments));
		return process;
	}

	public static Process StartProcess(ProcessStartInfo startInfo)
	{
		Process process = Process.Start(startInfo);
		return process;
	}

	public static ProcessResult StreamProcess(string fileName, string arguments)
	{
		ProcessResult result = ProcessHelper.StreamProcess(fileName, arguments, string.Empty, 0);
		return result;
	}

	public static ProcessResult StreamProcess(string fileName, string arguments, int timeoutMilliseconds)
	{
		ProcessResult result = ProcessHelper.StreamProcess(fileName, arguments, string.Empty, timeoutMilliseconds);
		return result;
	}

	public static ProcessResult StreamProcess(string fileName, string arguments, string workingDirectory)
	{
		ProcessResult result = ProcessHelper.StreamProcess(fileName, arguments, workingDirectory, 0);
		return result;
	}

	public static ProcessResult StreamProcess(string fileName, string arguments, string workingDirectory, int timeoutMilliseconds)
	{
		ElapsedEventHandler elapsedEventHandler = null;
		ProcessHelper processHelper = new ProcessHelper();
		processHelper.fileName = fileName;
		processHelper.arguments = arguments;
		processHelper.fileName = EnvironmentHelper.ExpandEnvironmentVariables(processHelper.fileName);
		if (!FileSystemHelper.FileExists(processHelper.fileName))
		{
			throw new FileNotFoundException(string.Format("The system cannot find the path specified: {0}", processHelper.fileName));
		}
		ProcessStartInfo processStartInfo = new ProcessStartInfo(processHelper.fileName);
		processStartInfo.Arguments = processHelper.arguments;
		processStartInfo.CreateNoWindow = true;
		processStartInfo.RedirectStandardOutput = true;
		processStartInfo.RedirectStandardError = true;
		processStartInfo.UseShellExecute = false;
		processStartInfo.WorkingDirectory = workingDirectory;
		ProcessStartInfo startInfo = processStartInfo;
		processHelper.timeoutOccured = false;
		processHelper.processFinished = false;
		processHelper.process = ProcessHelper.StartProcess(startInfo);
		if (timeoutMilliseconds > 0)
		{
			if (elapsedEventHandler == null)
			{
				elapsedEventHandler = new ElapsedEventHandler(processHelper.<StreamProcess>b__2);
			}
			ElapsedEventHandler timeoutHandler = elapsedEventHandler;
			ProcessHelper.SetAsyncWait(timeoutMilliseconds, timeoutHandler);
		}
		StreamReader errorStream = processHelper.process.StandardError;
		StreamReader outputStream = processHelper.process.StandardOutput;
		string output = outputStream.ReadToEnd();
		string error = errorStream.ReadToEnd();
		processHelper.processFinished = true;
		if (processHelper.timeoutOccured)
		{
			error = string.Format("Timeout processing {0} with arguments: {1}", processHelper.fileName, processHelper.arguments);
		}
		else
		{
			error = EnvironmentHelper.ConvertToUTF8(error, errorStream.CurrentEncoding);
			output = EnvironmentHelper.ConvertToUTF8(output, outputStream.CurrentEncoding);
		}
		ProcessResult processResult1 = new ProcessResult();
		ProcessResult processResult2 = processResult1;
		processResult2.Output = output;
		processResult2.Error = error;
		ProcessResult result = processResult2;
		return result;
	}

	public struct ProcessResult
	{
		public string Error;

		public string Output;
	}
}
}