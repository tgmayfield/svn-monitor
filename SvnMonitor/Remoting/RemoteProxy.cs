namespace SVNMonitor.Remoting
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.View;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class RemoteProxy : MarshalByRefObject
    {
        private static Dictionary<string, MethodInfo> remoteCommands;

        [RemoteCommand("close"), RemoteCommand("exit")]
        private void Close()
        {
            MainForm.FormInstance.RealClose();
        }

        public void ExecuteCommand(string commandName)
        {
            try
            {
                MethodInfo method;
                ThreadHelper.SetThreadName("REMOTE");
                Logger.Log.InfoFormat("Remote command: {0}", commandName);
                if (!string.IsNullOrEmpty(commandName) && (!MainForm.FormInstance.IsDisposed && !MainForm.FormInstance.Disposing))
                {
                    commandName = commandName.ToLower();
                    if (!RemoteCommands.ContainsKey(commandName))
                    {
                        Logger.Log.InfoFormat("Remote command '{0}' does not exist. Aborting.", commandName);
                    }
                    else
                    {
                        method = RemoteCommands[commandName];
                        Logger.Log.InfoFormat("Executing remote command: {0}", commandName);
                        if (MainForm.FormInstance.InvokeRequired)
                        {
                            MainForm.FormInstance.BeginInvoke(() => method.Invoke(this, null));
                        }
                        else
                        {
                            method.Invoke(this, null);
                        }
                    }
                }
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
        }

        private static Dictionary<string, MethodInfo> GetCommands()
        {
            Logger.Log.InfoFormat("Collecting avaliable remote commands...", new object[0]);
            Dictionary<string, MethodInfo> dict = new Dictionary<string, MethodInfo>();
            foreach (MethodInfo method in typeof(RemoteProxy).GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (Attribute.IsDefined(method, typeof(RemoteCommandAttribute)))
                {
                    RemoteCommandAttribute[] atts = (RemoteCommandAttribute[]) Attribute.GetCustomAttributes(method, typeof(RemoteCommandAttribute));
                    foreach (RemoteCommandAttribute att in atts)
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
            remoteCommands = GetCommands();
        }

        [RemoteCommand("show")]
        private void Show()
        {
            MainForm.FormInstance.RestoreForm();
        }

        [RemoteCommand("showhide"), RemoteCommand("showorhide")]
        private void ShowOrHide()
        {
            MainForm.FormInstance.ShowOrHideForm();
        }

        private static Dictionary<string, MethodInfo> RemoteCommands
        {
            get
            {
                if (remoteCommands == null)
                {
                    remoteCommands = GetCommands();
                }
                return remoteCommands;
            }
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
        private class RemoteCommandAttribute : Attribute
        {
            public RemoteCommandAttribute(string commandName)
            {
                this.CommandName = commandName.ToLower();
            }

            public string CommandName { get; private set; }
        }
    }
}

