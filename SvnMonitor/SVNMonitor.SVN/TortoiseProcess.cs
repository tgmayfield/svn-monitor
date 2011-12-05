using System;
using System.Windows.Forms;
using SVNMonitor.Settings;
using SVNMonitor.Helpers;
using System.Threading;
using SVNMonitor.Logging;
using SVNMonitor.View;
using SVNMonitor.Resources.Text;
using System.Diagnostics;

namespace SVNMonitor.SVN
{
internal class TortoiseProcess
{
	public TortoiseProcess()
	{
	}

	internal static void Add(string path, MethodInvoker callBack)
	{
		int autoClose = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
		string arguments = string.Format("/command:add /closeonend:{0} /path:\"{1}\"", autoClose, path);
		TortoiseProcess.RunTortoiseProc(arguments, callBack);
	}

	internal static void ApplyPatch(string path, MethodInvoker callBack)
	{
		string arguments = string.Format("/patchpath:\"{0}\"", path);
		TortoiseProcess.RunTortoiseMerge(arguments, callBack);
	}

	internal static void Blame(string path)
	{
		string arguments = string.Format("/command:blame /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Cat(string path, string savePath, long revision)
	{
		string arguments = string.Format("/command:cat /path:\"{0}\" /savepath:\"{1}\" /revision:{2}", path, savePath, revision);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void CheckModifications(string path)
	{
		string arguments = string.Format("/command:repostatus /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Checkout(string path)
	{
		string arguments = string.Format("/command:checkout /url:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void CleanUp(string path)
	{
		string arguments = string.Format("/command:cleanup /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Commit(string path, MethodInvoker callBack)
	{
		int autoClose = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
		string arguments = string.Format("/command:commit /closeonend:{0} /path:\"{1}\"", autoClose, path);
		TortoiseProcess.RunTortoiseProc(arguments, callBack);
	}

	internal static void Copy(string path)
	{
		string arguments = string.Format("/command:copy /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void CreatePatch(string path)
	{
		string arguments = string.Format("/command:createpatch /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void DeleteUnversioned(string path, MethodInvoker callback)
	{
		string arguments = string.Format("/command:delunversioned /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments, callback);
	}

	internal static void DiffLocalWithBase(string path)
	{
		string arguments = string.Format("/command:diff /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void DiffWithPrevious(string path, long revision1, long revision2)
	{
		string arguments = string.Format("/command:diff /path:\"{0}\" /startrev:{1} /endrev:{2}", path, revision1, revision2);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Export(string path)
	{
		string arguments = string.Format("/command:export /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void GetLock(string path)
	{
		string arguments = string.Format("/command:lock /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Help()
	{
		string arguments = "/command:help";
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Log(string path)
	{
		string arguments = string.Format("/command:log /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Log(string path, long startRevision, long endRevision)
	{
		string arguments = string.Format("/command:log /path:\"{0}\" /startrev:{1} /endrev:{2}", path, startRevision, endRevision);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Merge(string path, MethodInvoker callBack)
	{
		string arguments = string.Format("/command:merge /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments, callBack);
	}

	internal static void Properties(string path)
	{
		string arguments = string.Format("/command:properties /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Reintegrate(string path, MethodInvoker callBack)
	{
		string arguments = string.Format("/command:mergeall /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void ReleaseLock(string path)
	{
		string arguments = string.Format("/command:unlock /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Relocate(string path, MethodInvoker callBack)
	{
		string arguments = string.Format("/command:relocate /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments, callBack);
	}

	internal static void RepoBrowser(string path)
	{
		string arguments = string.Format("/command:repobrowser /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Resolve(string path, MethodInvoker callBack)
	{
		string arguments = string.Format("/command:resolve /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments, callBack);
	}

	internal static void Revert(string path, MethodInvoker callBack)
	{
		int autoClose = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
		string arguments = string.Format("/command:revert /closeonend:{0} /path:\"{1}\"", autoClose, path);
		TortoiseProcess.RunTortoiseProc(arguments, callBack);
	}

	internal static void RevisionGraph(string path)
	{
		string arguments = string.Format("/command:revisiongraph /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	private static void RunTortoiseMerge(string arguments)
	{
		TortoiseProcess.RunTortoiseMergeAsync(arguments, null);
	}

	private static void RunTortoiseMerge(string arguments, MethodInvoker callBack)
	{
		TortoiseProcess.RunTortoiseMergeAsync(arguments, callBack);
	}

	private static void RunTortoiseMergeAsync(string arguments, MethodInvoker callBack)
	{
		object[] objArray = new object[2];
		objArray[0] = arguments;
		objArray[1] = callBack;
		object[] state = objArray;
		ThreadHelper.Queue(new WaitCallback(null.TortoiseProcess.RunTortoiseMergeSync), "TORTOISEMERGE", state);
		ThreadHelper.Sleep(100);
	}

	private static void RunTortoiseMergeSync(object state)
	{
		object[] stateArray = (object[])state;
		string arguments = (string)stateArray[0];
		MethodInvoker callBack = (MethodInvoker)stateArray[1];
		string tortoiseSVNMerge = TortoiseSVNHelper.TortoiseSVNMergePath;
		TortoiseProcess.RunTortoiseSync(tortoiseSVNMerge, arguments, callBack);
	}

	private static void RunTortoiseProc(string arguments)
	{
		TortoiseProcess.RunTortoiseProcAsync(arguments, null);
	}

	private static void RunTortoiseProc(string arguments, MethodInvoker callBack)
	{
		TortoiseProcess.RunTortoiseProcAsync(arguments, callBack);
	}

	private static void RunTortoiseProcAsync(string arguments, MethodInvoker callBack)
	{
		object[] objArray = new object[2];
		objArray[0] = arguments;
		objArray[1] = callBack;
		object[] state = objArray;
		ThreadHelper.Queue(new WaitCallback(null.TortoiseProcess.RunTortoiseProcSync), "TORTOISE", state);
		ThreadHelper.Sleep(100);
	}

	private static void RunTortoiseProcSync(object state)
	{
		object[] stateArray = (object[])state;
		string arguments = (string)stateArray[0];
		arguments = string.Concat(arguments, " /notempfile");
		MethodInvoker callBack = (MethodInvoker)stateArray[1];
		string tortoiseSVNProc = TortoiseSVNHelper.TortoiseSVNProcPath;
		TortoiseProcess.RunTortoiseSync(tortoiseSVNProc, arguments, callBack);
	}

	private static void RunTortoiseSync(string exe, string arguments, MethodInvoker callBack)
	{
		object[] objArray;
		string tortoiseSVNExe = EnvironmentHelper.ExpandEnvironmentVariables(exe);
		if (!FileSystemHelper.FileExists(tortoiseSVNExe))
		{
			Logger.Log.InfoFormat("TortoiseMerge.EXE can't be found at {0}", tortoiseSVNExe);
			MainForm.FormInstance.ShowErrorMessage(Strings.ErrorTortoiseSVNProcessNotExists_FORMAT.FormatWith(new object[] { Environment.NewLine, tortoiseSVNExe }), Strings.SVNMonitorCaption);
			return;
		}
		ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.Arguments = arguments;
		startInfo.FileName = tortoiseSVNExe;
		startInfo.UseShellExecute = false;
		try
		{
			Logger.Log.InfoFormat("Running the Tortoise: {0} {1}", tortoiseSVNExe, arguments);
			DateTime startTime = DateTime.Now;
			Process process = ProcessHelper.StartProcess(startInfo);
			process.WaitForExit();
			"The Tortoise took {0}".DebugFormat(DateTime now = DateTime.Now, now.Subtract(startTime));
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

	internal static void Settings()
	{
		string arguments = "/command:settings";
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Switch(string path, MethodInvoker callBack)
	{
		string arguments = string.Format("/command:switch /path:\"{0}\"", path);
		TortoiseProcess.RunTortoiseProc(arguments);
	}

	internal static void Update(string path, MethodInvoker callBack)
	{
		TortoiseProcess.Update(path, "HEAD", callBack);
	}

	internal static void Update(string path, long revision, MethodInvoker callBack)
	{
		TortoiseProcess.Update(path, revision.ToString(), callBack);
	}

	private static void Update(string path, string revision, MethodInvoker callBack)
	{
		int autoClose = ApplicationSettingsManager.Settings.TortoiseSVNAutoClose;
		string arguments = string.Format("/command:update /rev:{0} /closeonend:{1} /path:\"{2}\"", revision, autoClose, path);
		TortoiseProcess.RunTortoiseProc(arguments, callBack);
	}
}
}