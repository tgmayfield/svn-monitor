namespace SVNMonitor
{
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class Status
    {
        private static EventHandler statusChanged;

        private static  event EventHandler CanExit;

        internal static  event EventHandler StatusChanged
        {
            add
            {
                statusChanged = (EventHandler) Delegate.Combine(statusChanged, value);
                Updater.Instance.StatusChanged += value;
            }
            remove
            {
                statusChanged = (EventHandler) Delegate.Remove(statusChanged, value);
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
            get
            {
                return EnabledSources.All<Source>(s => !s.HasLocalChanges);
            }
        }

        internal static bool AllUpToDate
        {
            get
            {
                return EnabledSources.All<Source>(s => s.IsUpToDate);
            }
        }

        internal static bool Closing
        {
            [CompilerGenerated]
            get
            {
                return <Closing>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                <Closing>k__BackingField = value;
            }
        }

        internal static bool Conflict
        {
            get
            {
                return EnabledSources.Any<Source>(s => (s.PossibleConflictedFilePathsCount > 0));
            }
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
                return MonitorSettings.Instance.GetEnumerableMonitors().Where<Monitor>(m => m.Enabled);
            }
        }

        internal static IEnumerable<Source> EnabledSources
        {
            get
            {
                return MonitorSettings.Instance.GetEnumerableSources().Where<Source>(s => (s.IsAlive && s.Enabled));
            }
        }

        internal static int EnabledSourcesCount
        {
            get
            {
                return EnabledSources.Count<Source>();
            }
        }

        internal static bool Error
        {
            get
            {
                return EnabledSources.Any<Source>(s => s.HasError);
            }
        }

        internal static IEnumerable<Source> ModifiedSources
        {
            get
            {
                return EnabledSources.Where<Source>(s => s.HasLocalChanges);
            }
        }

        internal static IEnumerable<Source> NotUpToDateSources
        {
            get
            {
                return EnabledSources.Where<Source>(s => !s.IsUpToDate);
            }
        }

        internal static bool Recommended
        {
            get
            {
                return EnabledSources.Any<Source>(s => (s.UnreadRecommendedCount > 0));
            }
        }

        internal static bool Updating
        {
            get
            {
                return (UpdatingSources.Count<string>() > 0);
            }
        }

        internal static IEnumerable<string> UpdatingSources
        {
            get
            {
                return EnabledSources.Where<Source>(s => s.Updating).Select<Source, string>(s => s.Name);
            }
        }

        internal static string UpdatingSourcesString
        {
            get
            {
                return string.Join(", ", UpdatingSources.ToArray<string>());
            }
        }
    }
}

