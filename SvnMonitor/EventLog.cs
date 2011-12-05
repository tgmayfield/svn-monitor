using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Timers;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Settings;
using SVNMonitor.Support;

namespace SVNMonitor
{
	[Serializable]
	public class EventLog
	{
		private static int lastConflictReminder;
		private static int lastRecommendedReminder;
		private static List<SVNMonitor.EventLogEntry> list;
		private static Timer timer;

		public static event EventHandler AfterLog;

		public static event EventHandler<EventArgs<long>> OpenEntry;

		static EventLog()
		{
			if (!ProcessHelper.IsInVisualStudio())
			{
				List = new List<SVNMonitor.EventLogEntry>();
				InitializeTimer();
			}
		}

		public static SVNMonitor.EventLogEntry GetEventLogEntryByID(long eventID)
		{
			SVNMonitor.EventLogEntry entry = null;
			try
			{
				entry = List.Find(e => e.ID == eventID);
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Error getting event id {0}", eventID), ex);
			}
			return entry;
		}

		internal static Image GetImageByEventType(SVNMonitor.EventLogEntryType type)
		{
			Image image = Images.information;
			switch (type)
			{
				case SVNMonitor.EventLogEntryType.Error:
					return Images.error;

				case SVNMonitor.EventLogEntryType.Info:
					return Images.information;

				case SVNMonitor.EventLogEntryType.Warning:
					return Images.warning;

				case SVNMonitor.EventLogEntryType.Monitor:
					return Images.satellite_dish;

				case SVNMonitor.EventLogEntryType.CheckingUpdates:
					return Images.data_refresh;

				case SVNMonitor.EventLogEntryType.AvailableUpdates:
					return Images.arrow_down_green;

				case SVNMonitor.EventLogEntryType.System:
					return Images.SVNMonitor_04;

				case SVNMonitor.EventLogEntryType.Source:
					return Images.data;

				case SVNMonitor.EventLogEntryType.Conflict:
					return Images.warning;

				case SVNMonitor.EventLogEntryType.Recommended:
					return Images.star_yellow;
			}
			return image;
		}

		private static void InitializeTimer()
		{
			timer = new Timer();
			int oneMinute = 0xea60;
			timer.Interval = ApplicationSettingsManager.Settings.EventLogRemindersTimerInterval * oneMinute;
			timer.AutoReset = true;
			timer.Elapsed += SVNMonitor.EventLog.timer_Elapsed;
			timer.Start();
		}

		public static long Log(SVNMonitor.EventLogEntryType type, string message, object sourceObject)
		{
			return Log(type, message, sourceObject, null);
		}

		public static long Log(SVNMonitor.EventLogEntryType type, string message, object sourceObject, Exception ex)
		{
			if ((ex != null) && KnownIssuesHelper.IsKnownIssue(ex))
			{
				return 0L;
			}
			Logger.Log.InfoFormat("Event-Log: Type={0}, Message={1}, HasException={2}", type, message, ex != null);
			SVNMonitor.EventLogEntry tempLocal0 = new SVNMonitor.EventLogEntry
			{
				DateTime = DateTime.Now,
				Type = type,
				Message = message,
				SourceObject = sourceObject,
				Exception = ex
			};
			SVNMonitor.EventLogEntry entry = tempLocal0;
			List.Add(entry);
			OnAfterLog();
			return entry.ID;
		}

		public static long LogError(string message, object sourceObject, Exception ex)
		{
			return Log(SVNMonitor.EventLogEntryType.Error, message, sourceObject, ex);
		}

		public static long LogInfo(string message)
		{
			return LogInfo(message, null);
		}

		public static long LogInfo(string message, object sourceObject)
		{
			return Log(SVNMonitor.EventLogEntryType.Info, message, sourceObject);
		}

		public static long LogSystem(string message)
		{
			return LogSystem(message, null);
		}

		public static long LogSystem(string message, object sourceObject)
		{
			return Log(SVNMonitor.EventLogEntryType.System, message, sourceObject);
		}

		public static long LogWarning(string message)
		{
			return LogWarning(message, null);
		}

		public static long LogWarning(string message, object sourceObject)
		{
			return Log(SVNMonitor.EventLogEntryType.Warning, message, sourceObject);
		}

		private static void OnAfterLog()
		{
			if (AfterLog != null)
			{
				AfterLog(null, EventArgs.Empty);
			}
		}

		private static void OnOpenEntry(long eventID)
		{
			if (OpenEntry != null)
			{
				OpenEntry(null, new EventArgs<long>(eventID));
			}
		}

		public static void Open(long eventID)
		{
			OnOpenEntry(eventID);
		}

		public static List<SVNMonitor.EventLogEntry> Search(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return List;
			}
			List<SVNMonitor.EventLogEntry> list = new List<SVNMonitor.EventLogEntry>();
			SVNMonitor.EventLogEntry[] listCopy = new SVNMonitor.EventLogEntry[List.Count];
			List.CopyTo(listCopy);
			for (int i = 0; i < listCopy.Length; i++)
			{
				SVNMonitor.EventLogEntry entry = listCopy[i];
				if (entry.Message.ToLower().Contains(text.ToLower()))
				{
					list.Add(entry);
				}
			}
			return list;
		}

		private static void ShowReminders()
		{
			int count = List.Count;
			if (Status.Conflict && ((count - lastConflictReminder) >= 10))
			{
				Log(SVNMonitor.EventLogEntryType.Conflict, "You have one or more possible conflicts. You better update as soon as possible.", timer);
				lastConflictReminder = count;
			}
			if (Status.Recommended && ((count - lastRecommendedReminder) >= 10))
			{
				Log(SVNMonitor.EventLogEntryType.Recommended, "You have one or more recommended revisions. You better update as soon as possible.", timer);
				lastRecommendedReminder = count;
			}
		}

		private static void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				ShowReminders();
			}
			catch (Exception ex)
			{
				Logger.Log.Error(ex.Message, ex);
			}
		}

		public static List<SVNMonitor.EventLogEntry> List
		{
			[DebuggerNonUserCode]
			get { return list; }
			[DebuggerNonUserCode]
			private set { list = value; }
		}

		public static string ListString
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				foreach (SVNMonitor.EventLogEntry entry in List)
				{
					sb.AppendLine(entry.ToString());
				}
				return sb.ToString();
			}
		}
	}
}