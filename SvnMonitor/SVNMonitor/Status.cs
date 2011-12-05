using System;
using System.Collections.Generic;

namespace SVNMonitor
{
internal class Status
{
	private static EventHandler CanExit;

	private static EventHandler statusChanged;

	internal static bool AllNotModified
	{
		get;
	}

	internal static bool AllUpToDate
	{
		get;
	}

	internal static bool Closing
	{
		get
		{
			return Status.<Closing>k__BackingField;
		}
		private set
		{
			value;
		}
	}

	internal static bool Conflict
	{
		get;
	}

	internal static bool Enabled
	{
		get
		{
			return Updater.Instance.Enabled;
		}
	}

	internal static IEnumerable<Monitor> EnabledMonitors
	{
		get
		{
			return MonitorSettings.Instance.GetEnumerableMonitors().Where<Monitor>(new Predicate<Monitor>((m) => m.Enabled));
		}
	}

	internal static IEnumerable<Source> EnabledSources
	{
		get
		{
			return MonitorSettings.Instance.GetEnumerableSources().Where<Source>(new Predicate<Source>((s) => {
				if (s.IsAlive)
				{
					return s.Enabled;
				}
				return 0;
			}
			));
		}
	}

	internal static int EnabledSourcesCount
	{
		get
		{
			return Status.EnabledSources.Count<Source>();
		}
	}

	internal static bool Error
	{
		get;
	}

	internal static IEnumerable<Source> ModifiedSources
	{
		get;
	}

	internal static IEnumerable<Source> NotUpToDateSources
	{
		get;
	}

	internal static bool Recommended
	{
		get;
	}

	internal static bool Updating
	{
		get
		{
			return Status.UpdatingSources.Count<string>() > 0;
		}
	}

	internal static IEnumerable<string> UpdatingSources
	{
		get;
	}

	internal static string UpdatingSourcesString
	{
		get
		{
			string retString = string.Join(", ", Status.UpdatingSources.ToArray<string>());
			return retString;
		}
	}

	public Status()
	{
	}

	internal static void OnCanExit()
	{
		if (Status.CanExit != null)
		{
			Status.CanExit(null, EventArgs.Empty);
		}
	}

	internal static void OnStatusChanged()
	{
		if (Status.Closing)
		{
			Status.OnCanExit();
			return;
		}
		if (Status.statusChanged != null)
		{
			Status.statusChanged(null, EventArgs.Empty);
		}
	}

	internal static void SetClosing(EventHandler handler)
	{
		Status.CanExit = handler;
		Status.Closing = true;
	}

	private event EventHandler CanExit;
	internal event EventHandler StatusChanged;
}
}