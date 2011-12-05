namespace SVNMonitor.Helpers
{
    using SVNMonitor;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.SVN;
    using SVNMonitor.View;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    internal class SVNPathCommands
    {
        public SVNPathCommands(IEnumerable<SVNPath> paths)
        {
            this.Paths = paths;
            this.Path = paths.First<SVNPath>();
            this.FilePath = string.Join("*", paths.Select<SVNPath, string>(p => p.FilePath).ToArray<string>());
        }

        [AssociatedUserAction(UserAction.Blame)]
        internal void Blame()
        {
            TortoiseProcess.Blame(this.FilePath);
        }

        [AssociatedUserAction(UserAction.Browse)]
        internal void Browse()
        {
            FileSystemHelper.Browse(this.FilePath);
        }

        [AssociatedUserAction(UserAction.Commit)]
        internal void Commit()
        {
            TortoiseProcess.Commit(this.FilePath, new MethodInvoker(this.TortoiseProcessCallBack));
        }

        [AssociatedUserAction(UserAction.Diff)]
        internal void DiffAuto()
        {
            if (this.Path.Modified)
            {
                this.DiffLocalWithBase();
            }
            else
            {
                this.DiffWithPrevious();
            }
        }

        [AssociatedUserAction(UserAction.DiffLocalWithBase)]
        internal void DiffLocalWithBase()
        {
            TortoiseProcess.DiffLocalWithBase(this.FilePath);
        }

        [AssociatedUserAction(UserAction.DiffWithPrevious)]
        internal void DiffWithPrevious()
        {
            TortoiseProcess.DiffWithPrevious(this.FilePath, this.Path.Revision - 1L, this.Path.Revision);
        }

        internal void DoDefaultPathAction()
        {
            MethodInfo method = GetDefaultPathAction();
            if (method != null)
            {
                method.Invoke(this, null);
            }
        }

        [AssociatedUserAction(UserAction.Explore)]
        internal void Explore()
        {
            if (this.Path.Source.IsURL || FileSystemHelper.IsUrl(this.FilePath))
            {
                FileSystemHelper.Browse(this.FilePath);
            }
            else
            {
                FileSystemHelper.Explore(this.FilePath);
            }
        }

        [AssociatedUserAction(UserAction.Edit)]
        internal void FileEdit()
        {
            ProcessHelper.StartProcess(ApplicationSettingsManager.Settings.FileEditor, this.FilePath);
        }

        internal static Dictionary<UserAction, string> GetAvailableUserActions()
        {
            Dictionary<UserAction, string> dict = new Dictionary<UserAction, string>();
            foreach (MethodInfo method in typeof(SVNPathCommands).GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where<MethodInfo>(m => Attribute.IsDefined(m, typeof(AssociatedUserActionAttribute))))
            {
                AssociatedUserActionAttribute attribute = (AssociatedUserActionAttribute) Attribute.GetCustomAttribute(method, typeof(AssociatedUserActionAttribute));
                if (attribute != null)
                {
                    UserAction action = attribute.UserAction;
                    string actionDescription = EnumHelper.TranslateEnumValue<UserAction>(action);
                    if (string.IsNullOrEmpty(actionDescription))
                    {
                        actionDescription = action.ToString();
                    }
                    dict.Add(action, actionDescription);
                }
            }
            return dict;
        }

        private static MethodInfo GetDefaultPathAction()
        {
            MethodInfo retMethod = null;
            try
            {
                string actionString = ApplicationSettingsManager.Settings.DefaultPathAction;
                UserAction action = EnumHelper.ParseEnum<UserAction>(actionString);
                foreach (MethodInfo method in typeof(SVNPathCommands).GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where<MethodInfo>(m => Attribute.IsDefined(m, typeof(AssociatedUserActionAttribute))))
                {
                    AssociatedUserActionAttribute attribute = (AssociatedUserActionAttribute) Attribute.GetCustomAttribute(method, typeof(AssociatedUserActionAttribute));
                    if ((attribute != null) && (action == attribute.UserAction))
                    {
                        Logger.Log.InfoFormat("Default action is {0}", actionString);
                        retMethod = method;
                        break;
                    }
                }
                if (retMethod == null)
                {
                    EventLog.LogWarning(Strings.ErrorNoPathActionFound_FORMAT.FormatWith(new object[] { actionString }));
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
            return retMethod;
        }

        [AssociatedUserAction(UserAction.Open)]
        internal void Open()
        {
            if (this.Path.Source.IsURL)
            {
                FileSystemHelper.Browse(this.FilePath);
            }
            else
            {
                FileSystemHelper.Open(this.FilePath);
            }
        }

        [AssociatedUserAction(UserAction.ShowLog)]
        internal void OpenSVNLog()
        {
            TortoiseProcess.Log(this.FilePath, this.Path.Revision, this.Path.Revision);
        }

        [AssociatedUserAction(UserAction.OpenWith)]
        internal void OpenWith()
        {
            FileSystemHelper.OpenWith(this.FilePath);
        }

        [AssociatedUserAction(UserAction.Revert)]
        internal void Revert()
        {
            TortoiseProcess.Revert(this.FilePath, new MethodInvoker(this.TortoiseProcessCallBack));
        }

        [AssociatedUserAction(UserAction.Rollback)]
        internal void Rollback()
        {
            TortoiseProcess.Update(this.FilePath, (long) (this.Path.Revision - 1L), new MethodInvoker(this.TortoiseProcessCallBack));
        }

        [AssociatedUserAction(UserAction.SaveRevision)]
        internal void SaveRevision(string targetName)
        {
            SVNMonitor.Helpers.ThreadHelper.Queue(new WaitCallback(this.SaveRevisionAsync), "SAVEREVISION", targetName);
        }

        private void SaveRevisionAsync(object state)
        {
            try
            {
                string targetName = (string) state;
                EventLog.LogInfo(Strings.SavingItemRevision_FORMAT.FormatWith(new object[] { this.Path.Revision, this.Path.DisplayName }));
                TortoiseProcess.Cat(this.Path.Uri, targetName, this.Path.Revision);
                if (FileSystemHelper.FileExists(targetName))
                {
                    EventLog.LogInfo(Strings.SavingItemRevisionDone_FORMAT.FormatWith(new object[] { targetName }));
                    FileSystemHelper.Explore(targetName);
                }
            }
            catch (Exception ex)
            {
                string message = Strings.ErrorSavingItemRevision_FORMAT.FormatWith(new object[] { this.Path.Revision, this.Path.Uri });
                MainForm.FormInstance.ShowErrorMessage(message, Strings.SVNMonitorCaption);
                EventLog.LogError(message, this.Path.Source, ex);
            }
        }

        [AssociatedUserAction(UserAction.Update)]
        internal void SVNUpdate()
        {
            TortoiseProcess.Update(this.FilePath, this.Path.Revision, new MethodInvoker(this.TortoiseProcessCallBack));
        }

        private void TortoiseProcessCallBack()
        {
            this.Path.Source.Refresh();
        }

        private string FilePath { get; set; }

        private SVNPath Path { get; set; }

        private IEnumerable<SVNPath> Paths { get; set; }
    }
}

