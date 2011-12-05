using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Timers;

using SVNMonitor.Logging;

namespace SVNMonitor.Helpers
{
	internal class ProcessHelper
	{
		public static void FlushMemory()
		{
			try
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error flushing memory", ex);
			}
		}

		public static Process GetCurrentProcess()
		{
			return Process.GetCurrentProcess();
		}

		internal static string GetLoadedAssemblies()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				sb.AppendFormat("AppDomain_Assembly={0}{1}", assembly, Environment.NewLine);
				sb.AppendFormat("AppDomain_AssemblyLocation={0}{1}", assembly.Location, Environment.NewLine);
			}
			return sb.ToString();
		}

		public static Process[] GetProcessesByName(string processName)
		{
			return Process.GetProcessesByName(processName);
		}

		internal static string GetUsageInformation()
		{
			return string.Format("Process_IsAdmin={0}{1}", IsRunningAsAdministrator(), Environment.NewLine);
		}

		public static bool IsInVisualStudio()
		{
			return GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool IsRunningAsAdministrator()
		{
			bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
			Logger.Log.DebugFormat("IsRunningAsAdministrator = " + isAdmin, new object[0]);
			return isAdmin;
		}

		internal static void LogLoadedAssemblies()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				Logger.Log.DebugFormat("Assembly={0}", assembly);
				Logger.Log.DebugFormat("AssemblyLocation={0}", assembly.Location);
			}
		}

		internal static void SetAsyncWait(int timeout, ElapsedEventHandler handler)
		{
			Timer timer = new Timer(timeout)
			{
				AutoReset = false
			};
			timer.Elapsed += handler;
			timer.Start();
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

		public static Process StartProcess(ProcessStartInfo startInfo)
		{
			return Process.Start(startInfo);
		}

		public static Process StartProcess(string fileName)
		{
			return StartProcess(new ProcessStartInfo(fileName));
		}

		public static Process StartProcess(string fileName, string arguments)
		{
			return StartProcess(new ProcessStartInfo(fileName, arguments));
		}

		public static ProcessResult StreamProcess(string fileName, string arguments)
		{
			return StreamProcess(fileName, arguments, string.Empty, 0);
		}

		public static ProcessResult StreamProcess(string fileName, string arguments, int timeoutMilliseconds)
		{
			return StreamProcess(fileName, arguments, string.Empty, timeoutMilliseconds);
		}

		public static ProcessResult StreamProcess(string fileName, string arguments, string workingDirectory)
		{
			return StreamProcess(fileName, arguments, workingDirectory, 0);
		}

		public static ProcessResult StreamProcess(string fileName, string arguments, string workingDirectory, int timeoutMilliseconds)
		{
			fileName = EnvironmentHelper.ExpandEnvironmentVariables(fileName);
			if (!FileSystemHelper.FileExists(fileName))
			{
				throw new FileNotFoundException(string.Format("The system cannot find the path specified: {0}", fileName));
			}
			ProcessStartInfo tempLocal0 = new ProcessStartInfo(fileName)
			{
				Arguments = arguments,
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false,
				WorkingDirectory = workingDirectory
			};
			ProcessStartInfo startInfo = tempLocal0;
			bool timeoutOccured = false;
			bool processFinished = false;
			Process process = StartProcess(startInfo);
			if (timeoutMilliseconds > 0)
			{
				ElapsedEventHandler timeoutHandler = delegate
				{
					if (!processFinished)
					{
						try
						{
							if ((process != null) && !process.HasExited)
							{
								timeoutOccured = true;
								process.Kill();
							}
						}
						catch (Exception ex)
						{
							Logger.Log.Error(string.Format("Error trying to kill process {0} with arguments: {1}", fileName, arguments), ex);
						}
					}
				};
				SetAsyncWait(timeoutMilliseconds, timeoutHandler);
			}
			StreamReader errorStream = process.StandardError;
			StreamReader outputStream = process.StandardOutput;
			string output = outputStream.ReadToEnd();
			string error = errorStream.ReadToEnd();
			processFinished = true;
			if (timeoutOccured)
			{
				error = string.Format("Timeout processing {0} with arguments: {1}", fileName, arguments);
			}
			else
			{
				error = EnvironmentHelper.ConvertToUTF8(error, errorStream.CurrentEncoding);
				output = EnvironmentHelper.ConvertToUTF8(output, outputStream.CurrentEncoding);
			}
			return new ProcessResult
			{
				Output = output,
				Error = error
			};
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ProcessResult
		{
			public string Output { get; set; }
			public string Error { get; set; }
		}
	}
}