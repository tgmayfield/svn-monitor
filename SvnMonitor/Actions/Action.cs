using System;
using System.Collections.Generic;
using System.ComponentModel;

using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.View;

namespace SVNMonitor.Actions
{
	[Serializable]
	/*[Obsolete("Actions will be rewritten fram scratch")]*/
	public abstract class Action
	{
		private bool enabled = true;

		public abstract void RejectChanges();
		protected abstract void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths);

		public void RunAction(List<SVNLogEntry> logEntries, List<SVNPath> paths)
		{
			if (!Enabled)
			{
				Logger.Log.Debug(string.Format("Action '{0}' is disabled.", DisplayName));
			}
			else
			{
				EventLog.LogInfo(string.Format("Running action '{0}'...", DisplayName), this);
				Run(logEntries, paths);
			}
		}

		public virtual void RunTest()
		{
			List<SVNLogEntry> list = new List<SVNLogEntry>();
			Source tempLocal0 = new Source
			{
				Name = "SVNMonitor"
			};
			Source source = tempLocal0;
			SVNLogEntry entry = new SVNLogEntry(source, 0x4d2L, "Chuck", DateTime.Now, "Fixed all bugs in the world.");
			list.Add(entry);
			EventLog.LogInfo(string.Format("Testing '{0}'...", DisplayName), this);
			Run(list, null);
		}

		public abstract void SetRejectionPoint();

		public void Test()
		{
			try
			{
				if (CanBeTested)
				{
					RunTest();
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message, ex);
				MainForm.FormInstance.ShowErrorMessage(ex.Message, DisplayName);
			}
		}

		[Description("Indicates that this action can be tested at design-time."), DisplayName("Can Be Tested"), Category("System Resources")]
		public virtual bool CanBeTested
		{
			get { return true; }
		}

		[Browsable(false)]
		public string DisplayName
		{
			get { return UserTypesFactory<Action>.GetUserTypeDisplayName(this); }
		}

		[Category("General"), Description("Indicates that this action is enabled.")]
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		[Browsable(false)]
		public abstract bool IsValid { get; }

		[Browsable(false)]
		public virtual string SummaryInfo
		{
			get { return DisplayName; }
		}
	}
}