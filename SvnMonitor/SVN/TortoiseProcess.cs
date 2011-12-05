namespace SVNMonitor.SVN
{
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.View;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Forms;

    internal class TortoiseProcess
    {
        internal static void Add(string path, MethodInvoker callBack)
        {
            int autoClose = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
            RunTortoiseProc(string.Format("/command:add /closeonend:{0} /path:\"{1}\"", autoClose, path), callBack);
        }

        internal static void ApplyPatch(string path, MethodInvoker callBack)
        {
            RunTortoiseMerge(string.Format("/patchpath:\"{0}\"", path), callBack);
        }

        internal static void Blame(string path)
        {
            RunTortoiseProc(string.Format("/command:blame /path:\"{0}\"", path));
        }

        internal static void Cat(string path, string savePath, long revision)
        {
            RunTortoiseProc(string.Format("/command:cat /path:\"{0}\" /savepath:\"{1}\" /revision:{2}", path, savePath, revision));
        }

        internal static void CheckModifications(string path)
        {
            RunTortoiseProc(string.Format("/command:repostatus /path:\"{0}\"", path));
        }

        internal static void Checkout(string path)
        {
            RunTortoiseProc(string.Format("/command:checkout /url:\"{0}\"", path));
        }

        internal static void CleanUp(string path)
        {
            RunTortoiseProc(string.Format("/command:cleanup /path:\"{0}\"", path));
        }

        internal static void Commit(string path, MethodInvoker callBack)
        {
            int autoClose = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
            RunTortoiseProc(string.Format("/command:commit /closeonend:{0} /path:\"{1}\"", autoClose, path), callBack);
        }

        internal static void Copy(string path)
        {
            RunTortoiseProc(string.Format("/command:copy /path:\"{0}\"", path));
        }

        internal static void CreatePatch(string path)
        {
            RunTortoiseProc(string.Format("/command:createpatch /path:\"{0}\"", path));
        }

        internal static void DeleteUnversioned(string path, MethodInvoker callback)
        {
            RunTortoiseProc(string.Format("/command:delunversioned /path:\"{0}\"", path), callback);
        }

        internal static void DiffLocalWithBase(string path)
        {
            RunTortoiseProc(string.Format("/command:diff /path:\"{0}\"", path));
        }

        internal static void DiffWithPrevious(string path, long revision1, long revision2)
        {
            RunTortoiseProc(string.Format("/command:diff /path:\"{0}\" /startrev:{1} /endrev:{2}", path, revision1, revision2));
        }

        internal static void Export(string path)
        {
            RunTortoiseProc(string.Format("/command:export /path:\"{0}\"", path));
        }

        internal static void GetLock(string path)
        {
            RunTortoiseProc(string.Format("/command:lock /path:\"{0}\"", path));
        }

        internal static void Help()
        {
            string arguments = "/command:help";
            RunTortoiseProc(arguments);
        }

        internal static void Log(string path)
        {
            RunTortoiseProc(string.Format("/command:log /path:\"{0}\"", path));
        }

        internal static void Log(string path, long startRevision, long endRevision)
        {
            RunTortoiseProc(string.Format("/command:log /path:\"{0}\" /startrev:{1} /endrev:{2}", path, startRevision, endRevision));
        }

        internal static void Merge(string path, MethodInvoker callBack)
        {
            RunTortoiseProc(string.Format("/command:merge /path:\"{0}\"", path), callBack);
        }

        internal static void Properties(string path)
        {
            RunTortoiseProc(string.Format("/command:properties /path:\"{0}\"", path));
        }

        internal static void Reintegrate(string path, MethodInvoker callBack)
        {
            RunTortoiseProc(string.Format("/command:mergeall /path:\"{0}\"", path));
        }

        internal static void ReleaseLock(string path)
        {
            RunTortoiseProc(string.Format("/command:unlock /path:\"{0}\"", path));
        }

        internal static void Relocate(string path, MethodInvoker callBack)
        {
            RunTortoiseProc(string.Format("/command:relocate /path:\"{0}\"", path), callBack);
        }

        internal static void RepoBrowser(string path)
        {
            RunTortoiseProc(string.Format("/command:repobrowser /path:\"{0}\"", path));
        }

        internal static void Resolve(string path, MethodInvoker callBack)
        {
            RunTortoiseProc(string.Format("/command:resolve /path:\"{0}\"", path), callBack);
        }

        internal static void Revert(string path, MethodInvoker callBack)
        {
            int autoClose = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
            RunTortoiseProc(string.Format("/command:revert /closeonend:{0} /path:\"{1}\"", autoClose, path), callBack);
        }

        internal static void RevisionGraph(string path)
        {
            RunTortoiseProc(string.Format("/command:revisiongraph /path:\"{0}\"", path));
        }

        private static void RunTortoiseMerge(string arguments)
        {
            RunTortoiseMergeAsync(arguments, null);
        }

        private static void RunTortoiseMerge(string arguments, MethodInvoker callBack)
        {
            RunTortoiseMergeAsync(arguments, callBack);
        }

        private static void RunTortoiseMergeAsync(string arguments, MethodInvoker callBack)
        {
            object[] state = new object[] { arguments, callBack };
            SVNMonitor.Helpers.ThreadHelper.Queue(new WaitCallback(TortoiseProcess.RunTortoiseMergeSync), "TORTOISEMERGE", state);
            SVNMonitor.Helpers.ThreadHelper.Sleep(100);
        }

        private static void RunTortoiseMergeSync(object state)
        {
            object[] stateArray = (object[]) state;
            string arguments = (string) stateArray[0];
            MethodInvoker callBack = (MethodInvoker) stateArray[1];
            RunTortoiseSync(TortoiseSVNHelper.TortoiseSVNMergePath, arguments, callBack);
        }

        private static void RunTortoiseProc(string arguments)
        {
            RunTortoiseProcAsync(arguments, null);
        }

        private static void RunTortoiseProc(string arguments, MethodInvoker callBack)
        {
            RunTortoiseProcAsync(arguments, callBack);
        }

        private static void RunTortoiseProcAsync(string arguments, MethodInvoker callBack)
        {
            object[] state = new object[] { arguments, callBack };
            SVNMonitor.Helpers.ThreadHelper.Queue(new WaitCallback(TortoiseProcess.RunTortoiseProcSync), "TORTOISE", state);
            SVNMonitor.Helpers.ThreadHelper.Sleep(100);
        }

        private static void RunTortoiseProcSync(object state)
        {
            object[] stateArray = (object[]) state;
            string arguments = (string) stateArray[0];
            arguments = arguments + " /notempfile";
            MethodInvoker callBack = (MethodInvoker) stateArray[1];
            RunTortoiseSync(TortoiseSVNHelper.TortoiseSVNProcPath, arguments, callBack);
        }

        private static void RunTortoiseSync(string exe, string arguments, MethodInvoker callBack)
        {
            string tortoiseSVNExe = EnvironmentHelper.ExpandEnvironmentVariables(exe);
            if (!FileSystemHelper.FileExists(tortoiseSVNExe))
            {
                Logger.Log.InfoFormat("TortoiseMerge.EXE can't be found at {0}", tortoiseSVNExe);
                MainForm.FormInstance.ShowErrorMessage(Strings.ErrorTortoiseSVNProcessNotExists_FORMAT.FormatWith(new object[] { Environment.NewLine, tortoiseSVNExe }), Strings.SVNMonitorCaption);
            }
            else
            {
                ProcessStartInfo startInfo = new ProcessStartInfo {
                    Arguments = arguments,
                    FileName = tortoiseSVNExe,
                    UseShellExecute = false
                };
                try
                {
                    Logger.Log.InfoFormat("Running the Tortoise: {0} {1}", tortoiseSVNExe, arguments);
                    DateTime startTime = DateTime.Now;
                    ProcessHelper.StartProcess(startInfo).WaitForExit();
                    Logger.Log.DebugFormat("The Tortoise took {0}", DateTime.Now.Subtract(startTime));
                    if (callBack != null)
                    {
                        callBack();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.ErrorFormat("Error starting TortoiseSVN with these arguments:{0}{1}", Environment.NewLine, arguments);
                    Logger.Log.Error(ex.Message, ex);
                    MainForm.FormInstance.ShowErrorMessage(Strings.ErrorWithTortoiseSVN, Strings.SVNMonitorCaption);
                }
            }
        }

        internal static void Settings()
        {
            string arguments = "/command:settings";
            RunTortoiseProc(arguments);
        }

        internal static void Switch(string path, MethodInvoker callBack)
        {
            RunTortoiseProc(string.Format("/command:switch /path:\"{0}\"", path));
        }

        internal static void Update(string path, MethodInvoker callBack)
        {
            Update(path, "HEAD", callBack);
        }

        internal static void Update(string path, long revision, MethodInvoker callBack)
        {
            Update(path, revision.ToString(), callBack);
        }

        private static void Update(string path, string revision, MethodInvoker callBack)
        {
            int autoClose = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
            RunTortoiseProc(string.Format("/command:update /rev:{0} /closeonend:{1} /path:\"{2}\"", revision, autoClose, path), callBack);
        }
    }
}

