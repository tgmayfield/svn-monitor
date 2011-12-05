using System;
using SVNMonitor.Resources.Text;
using System.Threading;
using SVNMonitor.Logging;
using SVNMonitor.Support;
using System.Reflection;
using SVNMonitor.View;
using SVNMonitor.View.Dialogs;
using SVNMonitor.Helpers;
using System.IO;
using SVNMonitor.Settings;
using System.Diagnostics;
using System.Windows.Forms;
using SVNMonitor.Remoting;

namespace SVNMonitor
{
internal static class Program
{
	private static bool closing;

	private static void AppendStartMessages()
	{
		EventLog.LogSystem(Strings.WelcomeMessage);
		Program.ShowWarningMessages();
	}

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
	{
		Program.HandleException(e.Exception, "Application_ThreadException");
	}

	private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
	{
		try
		{
			Logger.Log.DebugFormat("Assembly loaded: {0}", args.LoadedAssembly);
		}
		catch
		{
		}
	}

	private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		Program.HandleException((Exception)e.ExceptionObject, "CurrentDomain_UnhandledException");
	}

	internal static void End()
	{
		Program.closing = 1;
		Logger.Log.Info("Closing");
		MonitorSettings.Instance.Save(false);
		Program.LogEnd();
		Environment.Exit(0);
	}

	private static void HandleException(Exception ex, string handler)
	{
		ErrorReportFeedback report;
		if (ex as TargetInvocationException)
		{
			Program.HandleException(ex.InnerException, handler);
			return;
		}
		if (KnownIssuesHelper.IsKnownIssue(ex, true))
		{
			Logger.Log.Error("Known Issue: ", ex);
			return;
		}
		Logger.Log.Error(string.Format("Unhandled exception by {0}:", handler), ex);
		if (!Program.closing)
		{
			report = ErrorReportFeedback.Generate(ex);
			if (MainForm.FormInstance != null)
			{
				MainForm.FormInstance.ReportError(report);
				return;
			}
		}
		ErrorReportDialog.Report(report);
		return;
		Logger.Log.Info("Exception while closing");
		Program.LogEnd();
	}

	private static bool InitApplicationSettings()
	{
		bool firstRun = !FileSystemHelper.FileExists(Path.Combine(FileSystemHelper.AppData, "SVNMonitor.config"));
		ApplicationSettingsManager.Init();
		return firstRun;
	}

	private static void InitLog()
	{
		Logger.Init();
	}

	private static bool IsProcessRunning()
	{
		Process currentProcess = ProcessHelper.GetCurrentProcess();
		string processName = currentProcess.ProcessName;
		Process[] processList = ProcessHelper.GetProcessesByName(processName);
		return (int)processList.Length > 1;
	}

	private static void LogEnd()
	{
		Logger.Log.InfoFormat("{0}{0}{0}End.", Environment.NewLine);
	}

	[STAThread]
	private static void Main(string[] args)
	{
		bool firstRun;
		try
		{
			ThreadHelper.SetThreadName("MAIN");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			SessionInfo.SetSessionInfo(args);
			Program.InitLog();
			firstRun = Program.InitApplicationSettings();
			ThreadHelper.SetMainThreadUICulture(ApplicationSettingsManager.Settings.UILanguage);
			RemotingServer.Start();
		}
		catch (Exception ex)
		{
			Program.HandleException(ex, "Main");
			MessageBox.Show(ex.ToString());
		}
		if (Program.IsProcessRunning())
		{
			RemotingClient.Show();
		}
		else
		{
			if (!UpgradeInfo.CheckIfUpgradeReady())
			{
				UpgradeInfo.DeleteSavedUpgradeInfo();
				Program.AppendStartMessages();
				AppDomain.CurrentDomain.add_UnhandledException(new UnhandledExceptionEventHandler(null.Program.CurrentDomain_UnhandledException));
				AppDomain.CurrentDomain.add_AssemblyLoad(new AssemblyLoadEventHandler(null.Program.CurrentDomain_AssemblyLoad));
				Application.add_ThreadException(new ThreadExceptionEventHandler(null.Program.Application_ThreadException));
				try
				{
					MainForm form = new MainForm();
					form.ShowFirstRunNotification = firstRun;
					Application.Run(form);
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Fatal Error", ex);
				}
				finally
				{
					Program.End();
				}
			}
		}
	}

	private static void ShowWarningMessages()
	{
		if (!ApplicationSettingsManager.Settings.EnableUpdates)
		{
			EventLog.LogWarning("Updates are disabled.");
		}
		if (!ApplicationSettingsManager.Settings.EnableVersionCheck)
		{
			EventLog.LogWarning("Version-checks are disabled.");
		}
	}
}
}