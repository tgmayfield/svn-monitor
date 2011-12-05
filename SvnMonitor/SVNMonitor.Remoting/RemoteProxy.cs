using System;
using System.Collections.Generic;
using SVNMonitor.View;
using System.Windows.Forms;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using System.Reflection;

namespace SVNMonitor.Remoting
{
public class RemoteProxy : MarshalByRefObject
{
	private static Dictionary<string, MethodInfo> remoteCommands;

	private static Dictionary<string, MethodInfo> RemoteCommands
	{
		get
		{
			if (RemoteProxy.remoteCommands == null)
			{
				RemoteProxy.remoteCommands = RemoteProxy.GetCommands();
			}
			return RemoteProxy.remoteCommands;
		}
	}

	public RemoteProxy()
	{
	}

	[RemoteCommand("close")]
	[RemoteCommand("exit")]
	private void Close()
	{
		MainForm.FormInstance.RealClose();
	}

	public void ExecuteCommand(string commandName)
	{
		MethodInvoker methodInvoker;
		RemoteProxy remoteProxy;
		try
		{
			methodInvoker = null;
			remoteProxy = new RemoteProxy();
			remoteProxy.<>4__this = this;
			ThreadHelper.SetThreadName("REMOTE");
			Logger.Log.InfoFormat("Remote command: {0}", commandName);
			commandName = commandName.ToLower();
		}
		catch (TargetInvocationException ex)
		{
			Logger.Log.Error(string.Format("Error executing remote command: {0}", commandName), ex);
			if (ex.InnerException != null)
			{
				Logger.Log.Error("Inner exception:", ex.InnerException);
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error executing remote command: {0}", commandName), ex);
		}
		if (!string.IsNullOrEmpty(commandName) && (!MainForm.FormInstance.IsDisposed && MainForm.FormInstance.Disposing || !RemoteProxy.RemoteCommands.ContainsKey(commandName)))
		{
			Logger.Log.InfoFormat("Remote command '{0}' does not exist. Aborting.", commandName);
		}
	}

	private static Dictionary<string, MethodInfo> GetCommands()
	{
		Logger.Log.InfoFormat("Collecting avaliable remote commands...", new object[0]);
		Dictionary<string, MethodInfo> dict = new Dictionary<string, MethodInfo>();
		MethodInfo[] methods = typeof(RemoteProxy).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		MethodInfo[] methodInfoArray = methods;
		foreach (MethodInfo method in methodInfoArray)
		{
			if (Attribute.IsDefined(method, typeof(RemoteCommandAttribute)))
			{
				RemoteCommandAttribute[] atts = (RemoteCommandAttribute[])Attribute.GetCustomAttributes(method, typeof(RemoteCommandAttribute));
				RemoteCommandAttribute[] remoteCommandAttributeArray = atts;
				foreach (RemoteCommandAttribute att in remoteCommandAttributeArray)
				{
					Logger.Log.InfoFormat("Found remote command: {0} for {1}", att.CommandName, method.Name);
					dict.Add(att.CommandName, method);
				}
			}
		}
		return dict;
	}

	internal static void Init()
	{
		RemoteProxy.remoteCommands = RemoteProxy.GetCommands();
	}

	[RemoteCommand("show")]
	private void Show()
	{
		MainForm.FormInstance.RestoreForm();
	}

	[RemoteCommand("showhide")]
	[RemoteCommand("showorhide")]
	private void ShowOrHide()
	{
		MainForm.FormInstance.ShowOrHideForm();
	}

	[AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
	private class RemoteCommandAttribute : Attribute
	{
		public string CommandName;

		public RemoteCommandAttribute(string commandName);
	}
}
}