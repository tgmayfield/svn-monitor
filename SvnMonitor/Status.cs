using System;
using System.Collections.Generic;
using System.Linq;

using SVNMonitor.Entities;

namespace SVNMonitor
{
	internal class Status
	{
		private static EventHandler statusChanged;

		private static event EventHandler CanExit;

		internal static event EventHandler StatusChanged
		{
			add
			{
				statusChanged = (EventHandler)Delegate.Combine(statusChanged, value);
				Updater.Instance.StatusChanged += value;
			}
			remove
			{
				statusChanged = (EventHandler)Delegate.Remove(statusChanged, value);
				Updater.Instance.StatusChanged -= value;
			}
		}

		internal static void OnCanExit()
		{
			if (CanExit != null)
			{
				CanExit(null, EventArgs.Empty);
			}
		}

		internal static void OnStatusChanged()
		{
			if (Closing)
			{
				OnCanExit();
			}
			else if (statusChanged != null)
			{
				statusChanged(null, EventArgs.Empty);
			}
		}

		internal static void SetClosing(EventHandler handler)
		{
			CanExit = handler;
			Closing = true;
		}

		internal static bool AllNotModified
		{
			get { return EnabledSources.All(s => !s.HasLocalChanges); }
		}

		internal static bool AllUpToDate
		{
			get { return EnabledSources.All(s => s.IsUpToDate); }
		}

		internal static bool Closing { get; private set; }

		internal static bool Conflict
		{
			get { return EnabledSources.Any(s => (s.PossibleConflictedFilePathsCount > 0)); }
		}

		internal static bool Enabled
		{
			get { return Updater.Instance.Enabled; }
		}

		internal static IEnumerable<Monitor> EnabledMonitors
		{
			get { return MonitorSettings.Instance.GetEnumerableMonitors().Where(m => m.Enabled); }
		}

		internal static IEnumerable<Source> EnabledSources
		{
			get { return MonitorSettings.Instance.GetEnumerableSources().Where(s => (s.IsAlive && s.Enabled)); }
		}

		internal static int EnabledSourcesCount
		{
			get { return EnabledSources.Count(); }
		}

		internal static bool Error
		{
			get { return EnabledSources.Any(s => s.HasError); }
		}

		internal static IEnumerable<Source> ModifiedSources
		{
			get { return EnabledSources.Where(s => s.HasLocalChanges); }
		}

		internal static IEnumerable<Source> NotUpToDateSources
		{
			get { return EnabledSources.Where(s => !s.IsUpToDate); }
		}

		internal static bool Recommended
		{
			get { return EnabledSources.Any(s => (s.UnreadRecommendedCount > 0)); }
		}

		internal static bool Updating
		{
			get { return (UpdatingSources.Count() > 0); }
		}

		internal static IEnumerable<string> UpdatingSources
		{
			get { return EnabledSources.Where(s => s.Updating).Select(s => s.Name); }
		}

		internal static string UpdatingSourcesString
		{
			get { return string.Join(", ", UpdatingSources.ToArray()); }
		}
	}
}