﻿namespace SVNMonitor
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Remoting;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.Support;
    using SVNMonitor.View;
    using SVNMonitor.View.Dialogs;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    internal static class Program
    {
        private static bool closing;

        private static void AppendStartMessages()
        {
            EventLog.LogSystem(Strings.WelcomeMessage);
            ShowWarningMessages();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, "Application_ThreadException");
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
            HandleException((Exception) e.ExceptionObject, "CurrentDomain_UnhandledException");
        }

        internal static void End()
        {
            closing = true;
            Logger.Log.Info("Closing");
            MonitorSettings.Instance.Save(false);
            LogEnd();
            Environment.Exit(0);
        }

        private static void HandleException(Exception ex, string handler)
        {
            if (ex is TargetInvocationException)
            {
                HandleException(ex.InnerException, handler);
            }
            else if (KnownIssuesHelper.IsKnownIssue(ex, true))
            {
                Logger.Log.Error("Known Issue: ", ex);
            }
            else
            {
                Logger.Log.Error(string.Format("Unhandled exception by {0}:", handler), ex);
                if (!closing)
                {
                    ErrorReportFeedback report = ErrorReportFeedback.Generate(ex);
                    if (MainForm.FormInstance != null)
                    {
                        MainForm.FormInstance.ReportError(report);
                    }
                    else
                    {
                        ErrorReportDialog.Report(report);
                    }
                }
                else
                {
                    Logger.Log.Info("Exception while closing");
                    LogEnd();
                }
            }
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
            return (ProcessHelper.GetProcessesByName(ProcessHelper.GetCurrentProcess().ProcessName).Length > 1);
        }

        private static void LogEnd()
        {
            Logger.Log.InfoFormat("{0}{0}{0}End.", Environment.NewLine);
        }

        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                if (IsProcessRunning())
                {
                    RemotingClient.Show();
                }
                else
                {
                    SVNMonitor.Helpers.ThreadHelper.SetThreadName("MAIN");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    SessionInfo.SetSessionInfo(args);
                    InitLog();
                    bool firstRun = InitApplicationSettings();
                    SVNMonitor.Helpers.ThreadHelper.SetMainThreadUICulture(ApplicationSettingsManager.Settings.UILanguage);
                    RemotingServer.Start();
                    if (!UpgradeInfo.CheckIfUpgradeReady())
                    {
                        UpgradeInfo.DeleteSavedUpgradeInfo();
                        AppendStartMessages();
                        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
                        AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(Program.CurrentDomain_AssemblyLoad);
                        Application.ThreadException += new ThreadExceptionEventHandler(Program.Application_ThreadException);
                        try
                        {
                            MainForm form = new MainForm {
                                ShowFirstRunNotification = firstRun
                            };
                            Application.Run(form);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log.Error("Fatal Error", ex);
                        }
                        finally
                        {
                            End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Main");
                MessageBox.Show(ex.ToString());
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
