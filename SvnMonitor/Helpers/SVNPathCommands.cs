using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;
using SVNMonitor.View;

namespace SVNMonitor.Helpers
{
	internal class SVNPathCommands
	{
		public SVNPathCommands(IEnumerable<SVNPath> paths)
		{
			Paths = paths;
			Path = paths.First();
			FilePath = string.Join("*", paths.Select(p => p.FilePath).ToArray());
		}

		[AssociatedUserAction(UserAction.Blame)]
		internal void Blame()
		{
			TortoiseProcess.Blame(FilePath);
		}

		[AssociatedUserAction(UserAction.Browse)]
		internal void Browse()
		{
			FileSystemHelper.Browse(FilePath);
		}

		[AssociatedUserAction(UserAction.Commit)]
		internal void Commit()
		{
			TortoiseProcess.Commit(FilePath, TortoiseProcessCallBack);
		}

		[AssociatedUserAction(UserAction.Diff)]
		internal void DiffAuto()
		{
			if (Path.Modified)
			{
				DiffLocalWithBase();
			}
			else
			{
				DiffWithPrevious();
			}
		}

		[AssociatedUserAction(UserAction.DiffLocalWithBase)]
		internal void DiffLocalWithBase()
		{
			TortoiseProcess.DiffLocalWithBase(FilePath);
		}

		[AssociatedUserAction(UserAction.DiffWithPrevious)]
		internal void DiffWithPrevious()
		{
			TortoiseProcess.DiffWithPrevious(FilePath, Path.Revision - 1L, Path.Revision);
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
			if (Path.Source.IsURL || FileSystemHelper.IsUrl(FilePath))
			{
				FileSystemHelper.Browse(FilePath);
			}
			else
			{
				FileSystemHelper.Explore(FilePath);
			}
		}

		[AssociatedUserAction(UserAction.Edit)]
		internal void FileEdit()
		{
			ProcessHelper.StartProcess(ApplicationSettingsManager.Settings.FileEditor, FilePath);
		}

		internal static Dictionary<UserAction, string> GetAvailableUserActions()
		{
			Dictionary<UserAction, string> dict = new Dictionary<UserAction, string>();
			foreach (MethodInfo method in typeof(SVNPathCommands).GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(m => Attribute.IsDefined(m, typeof(AssociatedUserActionAttribute))))
			{
				AssociatedUserActionAttribute attribute = (AssociatedUserActionAttribute)Attribute.GetCustomAttribute(method, typeof(AssociatedUserActionAttribute));
				if (attribute != null)
				{
					UserAction action = attribute.UserAction;
					string actionDescription = EnumHelper.TranslateEnumValue(action);
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
				foreach (MethodInfo method in typeof(SVNPathCommands).GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(m => Attribute.IsDefined(m, typeof(AssociatedUserActionAttribute))))
				{
					AssociatedUserActionAttribute attribute = (AssociatedUserActionAttribute)Attribute.GetCustomAttribute(method, typeof(AssociatedUserActionAttribute));
					if ((attribute != null) && (action == attribute.UserAction))
					{
						Logger.Log.InfoFormat("Default action is {0}", actionString);
						retMethod = method;
						break;
					}
				}
				if (retMethod == null)
				{
					EventLog.LogWarning(Strings.ErrorNoPathActionFound_FORMAT.FormatWith(new object[]
					{
						actionString
					}));
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
			if (Path.Source.IsURL)
			{
				FileSystemHelper.Browse(FilePath);
			}
			else
			{
				FileSystemHelper.Open(FilePath);
			}
		}

		[AssociatedUserAction(UserAction.ShowLog)]
		internal void OpenSVNLog()
		{
			TortoiseProcess.Log(FilePath, Path.Revision, Path.Revision);
		}

		[AssociatedUserAction(UserAction.OpenWith)]
		internal void OpenWith()
		{
			FileSystemHelper.OpenWith(FilePath);
		}

		[AssociatedUserAction(UserAction.Revert)]
		internal void Revert()
		{
			TortoiseProcess.Revert(FilePath, TortoiseProcessCallBack);
		}

		[AssociatedUserAction(UserAction.Rollback)]
		internal void Rollback()
		{
			TortoiseProcess.Update(FilePath, (Path.Revision - 1L), TortoiseProcessCallBack);
		}

		[AssociatedUserAction(UserAction.SaveRevision)]
		internal void SaveRevision(string targetName)
		{
			SVNMonitor.Helpers.ThreadHelper.Queue(SaveRevisionAsync, "SAVEREVISION", targetName);
		}

		private void SaveRevisionAsync(object state)
		{
			try
			{
				string targetName = (string)state;
				EventLog.LogInfo(Strings.SavingItemRevision_FORMAT.FormatWith(new object[]
				{
					Path.Revision, Path.DisplayName
				}));
				TortoiseProcess.Cat(Path.Uri, targetName, Path.Revision);
				if (FileSystemHelper.FileExists(targetName))
				{
					EventLog.LogInfo(Strings.SavingItemRevisionDone_FORMAT.FormatWith(new object[]
					{
						targetName
					}));
					FileSystemHelper.Explore(targetName);
				}
			}
			catch (Exception ex)
			{
				string message = Strings.ErrorSavingItemRevision_FORMAT.FormatWith(new object[]
				{
					Path.Revision, Path.Uri
				});
				MainForm.FormInstance.ShowErrorMessage(message, Strings.SVNMonitorCaption);
				EventLog.LogError(message, Path.Source, ex);
			}
		}

		[AssociatedUserAction(UserAction.Update)]
		internal void SVNUpdate()
		{
			TortoiseProcess.Update(FilePath, Path.Revision, TortoiseProcessCallBack);
		}

		private void TortoiseProcessCallBack()
		{
			Path.Source.Refresh();
		}

		private string FilePath { get; set; }

		private SVNPath Path { get; set; }

		private IEnumerable<SVNPath> Paths { get; set; }
	}
}