using System;
using System.Collections.Generic;
using System.Timers;
using System.Text;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using System.Drawing;
using SVNMonitor.Resources;
using SVNMonitor.Settings;
using SVNMonitor.Support;

namespace SVNMonitor
{
[Serializable]
public class EventLog
{
	private static EventHandler AfterLog;

	private static int lastConflictReminder;

	private static int lastRecommendedReminder;

	private static List<EventLogEntry> list;

	private static EventHandler<EventArgs<long>> OpenEntry;

	private static Timer timer;

	public static List<EventLogEntry> List
	{
		get
		{
			return EventLog.list;
		}
		private set
		{
			EventLog.list = value;
		}
	}

	public static string ListString
	{
		get
		{
			StringBuilder sb = new StringBuilder();
			foreach (EventLogEntry entry in EventLog.List)
			{
				sb.AppendLine(entry.ToString());
			}
			return sb.ToString();
		}
	}

	static EventLog()
	{
		if (ProcessHelper.IsInVisualStudio())
		{
			return;
		}
		EventLog.List = new List<EventLogEntry>();
		EventLog.InitializeTimer();
	}

	public EventLog()
	{
	}

	public static EventLogEntry GetEventLogEntryByID(long eventID)
	{
		Predicate<EventLogEntry> predicate = null;
		EventLog eventLog = new EventLog();
		eventLog.eventID = eventID;
		EventLogEntry entry = null;
		try
		{
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error getting event id {0}", eventLog.eventID), ex);
		}
		return entry;
	}

	internal static Image GetImageByEventType(EventLogEntryType type)
	{
		Image image = Images.information;
		switch (type)
		{
			case EventLogEntryType.Error:
			{
				return Images.error;
			}
			case EventLogEntryType.Info:
			{
				return Images.information;
			}
			case EventLogEntryType.Warning:
			{
				return Images.warning;
			}
			case EventLogEntryType.Monitor:
			{
				return Images.satellite_dish;
			}
			case EventLogEntryType.CheckingUpdates:
			{
				return Images.data_refresh;
			}
			case EventLogEntryType.AvailableUpdates:
			{
				return Images.arrow_down_green;
			}
			case EventLogEntryType.System:
			{
				return Images.SVNMonitor_04;
			}
			case EventLogEntryType.Source:
			{
				return Images.data;
			}
			case EventLogEntryType.Conflict:
			{
				return Images.warning;
			}
			case EventLogEntryType.Recommended:
			{
				image = Images.star_yellow;
			}
		}
		return image;
	}

	private static void InitializeTimer()
	{
		EventLog.timer = new Timer();
		int oneMinute = 60000;
		EventLog.timer.Interval = (double)ApplicationSettingsManager.Settings.EventLogRemindersTimerInterval * oneMinute;
		EventLog.timer.AutoReset = true;
		EventLog.timer.Elapsed += new ElapsedEventHandler(null.EventLog.timer_Elapsed);
		EventLog.timer.Start();
	}

	public static long Log(EventLogEntryType type, string message, object sourceObject)
	{
		return EventLog.Log(type, message, sourceObject, null);
	}

	public static long Log(EventLogEntryType type, string message, object sourceObject, Exception ex)
	{
		if (ex != null && KnownIssuesHelper.IsKnownIssue(ex))
		{
			return (long)0;
		}
		Logger.Log.InfoFormat("Event-Log: Type={0}, Message={1}, HasException={2}", type, message, ex != null);
		EventLogEntry eventLogEntry = new EventLogEntry();
		eventLogEntry.DateTime = DateTime.Now;
		eventLogEntry.Type = type;
		eventLogEntry.Message = message;
		eventLogEntry.SourceObject = sourceObject;
		eventLogEntry.Exception = ex;
		EventLogEntry entry = eventLogEntry;
		EventLog.List.Add(entry);
		EventLog.OnAfterLog();
		return entry.ID;
	}

	public static long LogError(string message, object sourceObject, Exception ex)
	{
		return EventLog.Log(EventLogEntryType.Error, message, sourceObject, ex);
	}

	public static long LogInfo(string message)
	{
		return EventLog.LogInfo(message, null);
	}

	public static long LogInfo(string message, object sourceObject)
	{
		return EventLog.Log(EventLogEntryType.Info, message, sourceObject);
	}

	public static long LogSystem(string message)
	{
		return EventLog.LogSystem(message, null);
	}

	public static long LogSystem(string message, object sourceObject)
	{
		return EventLog.Log(EventLogEntryType.System, message, sourceObject);
	}

	public static long LogWarning(string message)
	{
		return EventLog.LogWarning(message, null);
	}

	public static long LogWarning(string message, object sourceObject)
	{
		return EventLog.Log(EventLogEntryType.Warning, message, sourceObject);
	}

	private static void OnAfterLog()
	{
		if (EventLog.AfterLog != null)
		{
			EventLog.AfterLog(null, EventArgs.Empty);
		}
	}

	private static void OnOpenEntry(long eventID)
	{
		if (EventLog.OpenEntry != null)
		{
			EventLog.OpenEntry(null, new EventArgs<long>(eventID));
		}
	}

	public static void Open(long eventID)
	{
		EventLog.OnOpenEntry(eventID);
	}

	public static List<EventLogEntry> Search(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return EventLog.List;
		}
		List<EventLogEntry> list = new List<EventLogEntry>();
		EventLogEntry[] listCopy = new EventLogEntry[EventLog.List.Count];
		EventLog.List.CopyTo(listCopy);
		foreach (EventLogEntry entry in listCopy)
		{
			if (entry.Message.ToLower().Contains(text.ToLower()))
			{
				list.Add(entry);
			}
		}
		return list;
	}

	private static void ShowReminders()
	{
		int count = EventLog.List.Count;
		if (Status.Conflict && count - EventLog.lastConflictReminder >= 10)
		{
			EventLog.Log(EventLogEntryType.Conflict, "You have one or more possible conflicts. You better update as soon as possible.", EventLog.timer);
			EventLog.lastConflictReminder = count;
		}
		if (Status.Recommended && count - EventLog.lastRecommendedReminder >= 10)
		{
			EventLog.Log(EventLogEntryType.Recommended, "You have one or more recommended revisions. You better update as soon as possible.", EventLog.timer);
			EventLog.lastRecommendedReminder = count;
		}
	}

	private static void timer_Elapsed(object sender, ElapsedEventArgs e)
	{
		try
		{
			EventLog.ShowReminders();
		}
		catch (Exception ex)
		{
			Logger.Log.Error(ex.Message, ex);
		}
	}

	public event EventHandler AfterLog;
	public event EventHandler<EventArgs<long>> OpenEntry;
}
}