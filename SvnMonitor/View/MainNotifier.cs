namespace SVNMonitor.View
{
    using SVNMonitor;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class MainNotifier
    {
        private const int NIF_ICON = 2;
        private const int NIF_INFO = 0x10;
        private const int NIF_MESSAGE = 1;
        private const int NIF_STATE = 8;
        private const int NIF_TIP = 4;
        private const int NIIF_ERROR = 3;
        private const int NIIF_INFO = 1;
        private const int NIIF_NONE = 0;
        private const int NIM_ADD = 0;
        private const int NIM_DELETE = 2;
        private const int NIM_MODIFY = 1;
        private const int NIM_SETFOCUS = 3;
        private const int NIM_SETVERSION = 4;
        private const long NIN_BALLOONHIDE = 0x403L;
        private const long NIN_BALLOONSHOW = 0x402L;
        private const long NIN_BALLOONTIMEOUT = 0x404L;
        private const long NIN_BALLOONUSERCLICK = 0x405L;
        private const int NOTIFYICON_VERSION = 5;
        private const long WM_USER = 0x400L;

        public MainNotifier(System.Windows.Forms.NotifyIcon notifyIcon)
        {
            this.NotifyIcon = notifyIcon;
        }

        private static Icon GetIcon(string iconName)
        {
            return (Icon) Icons.ResourceManager.GetObject(iconName);
        }

        private void HideBalloon()
        {
            this.ShowBalloon(string.Empty, string.Empty, ToolTipIcon.None);
        }

        internal void HideTrayIcon()
        {
            this.NotifyIcon.Visible = false;
        }

        internal void SetTrayIcon()
        {
            if (!Updater.Instance.Enabled)
            {
                this.SetTrayIconText(Strings.SVNMonitorDisabled);
                this.SetTrayIconIcon(Icons.status_disabled);
                this.SetTrayIconVisible(true);
            }
            else
            {
                bool allNotModified = Status.AllNotModified;
                if (Status.Conflict)
                {
                    string text = "SVN-Monitor - Possible conflict";
                    if (allNotModified)
                    {
                        this.SetTrayIcon("status_conflict_not_modified", text);
                    }
                    else
                    {
                        this.SetTrayIcon("status_conflict", text);
                    }
                }
                else
                {
                    bool allUpToDate = Status.AllUpToDate;
                    if (!allUpToDate && !allNotModified)
                    {
                        this.SetTrayIcon("status_updates_changes", "Local changes and updates are available");
                    }
                    else if (!allNotModified)
                    {
                        string formatter = string.Empty;
                        if (Status.ModifiedSources.Count<Source>() == 1)
                        {
                            formatter = string.Format(" for \"{0}\"", Status.ModifiedSources.First<Source>().Name);
                        }
                        this.SetTrayIcon("status_changes", string.Format("Local changes are available{0}", formatter));
                    }
                    else if (!allUpToDate)
                    {
                        string formatter = string.Empty;
                        if (Status.NotUpToDateSources.Count<Source>() == 1)
                        {
                            formatter = string.Format(" for \"{0}\"", Status.NotUpToDateSources.First<Source>().Name);
                        }
                        this.SetTrayIcon("status_updates", string.Format("Updates are available{0}", formatter));
                    }
                    else
                    {
                        this.SetTrayIcon("status_idle", "SVN-Monitor");
                    }
                }
            }
        }

        private void SetTrayIcon(Icon icon, string text)
        {
            try
            {
                this.SetTrayIconText(text + (Status.Updating ? " (checking...)" : string.Empty));
                this.SetTrayIconIcon(icon);
                this.SetTrayIconVisible(true);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error setting tray-icon", ex);
            }
        }

        private void SetTrayIcon(string iconName, string text)
        {
            if (Status.Updating)
            {
                iconName = iconName + "_checking";
            }
            if (Status.Error)
            {
                iconName = iconName + "_error";
            }
            if (Status.Recommended)
            {
                iconName = iconName + "_recommended";
                text = text + " (Recommended)";
            }
            Icon icon = GetIcon(iconName);
            this.SetTrayIcon(icon, text);
        }

        private void SetTrayIconIcon(Icon icon)
        {
            try
            {
                if (this.NotifyIcon.Icon != null)
                {
                    this.NotifyIcon.Icon.Dispose();
                }
                this.NotifyIcon.Icon = icon;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error trying to set the tray icon.", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void SetTrayIconText(string text)
        {
            try
            {
                text = TrayNotifier.TrimLongTipText(text);
                this.NotifyIcon.Text = text;
            }
            catch (NullReferenceException)
            {
            }
            catch (ArgumentOutOfRangeException)
            {
                text = TrayNotifier.TrimLongTipTextMore(text);
                this.NotifyIcon.Text = text;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Failed setting text in the tray-icon.", ex);
            }
        }

        internal void SetTrayIconVisible(bool visible)
        {
            try
            {
                this.NotifyIcon.Visible = visible;
            }
            catch (NullReferenceException)
            {
            }
        }

        [DllImport("shell32.dll")]
        private static extern int Shell_NotifyIconA(int dwMessage, ref NOTIFYICONDATA pnid);
        private void ShowBalloon(string title, string text, ToolTipIcon icon)
        {
            try
            {
                NOTIFYICONDATA clsNID;
                FieldInfo windowField = this.NotifyIcon.GetType().GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo idField = this.NotifyIcon.GetType().GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);
                IntPtr ptrWindow = ((NativeWindow) windowField.GetValue(this.NotifyIcon)).Handle;
                int id = (int) idField.GetValue(this.NotifyIcon);
                clsNID = new NOTIFYICONDATA {
                    szTip = "",
                    dwState = 0,
                    dwStateMask = 0,
                    hIcon = IntPtr.Zero,
                    uCallbackMessage = new IntPtr(0x200),
                    uID = id,
                    hwnd = ptrWindow,
                    szInfo = text,
                    szInfoTitle = title,
                    uFlags = 0x10,
                    cbSize = Marshal.SizeOf(clsNID),
                    dwInfoFlags = (int) icon,
                    uTimeout = 5
                };
                Shell_NotifyIconA(1, ref clsNID);
            }
            catch (NullReferenceException)
            {
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error showing balloon (title={0},text={1},icon={2})", title, text, icon), ex);
            }
        }

        public void ShowBalloonTip(int timeOut, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            this.ShowBalloonTip(timeOut, tipTitle, tipText, tipIcon, null);
        }

        public void ShowBalloonTip(int timeOut, string tipTitle, string tipText, ToolTipIcon tipIcon, EventHandler balloonTipClickedHandler)
        {
            try
            {
                if (balloonTipClickedHandler != null)
                {
                    this.NotifyIcon.BalloonTipClicked -= balloonTipClickedHandler;
                    this.NotifyIcon.BalloonTipClicked += balloonTipClickedHandler;
                }
                this.ShowBalloon(tipTitle, tipText, tipIcon);
            }
            catch (NullReferenceException)
            {
            }
        }

        private System.Windows.Forms.NotifyIcon NotifyIcon { get; set; }

        [StructLayout(LayoutKind.Sequential)]
        private struct NOTIFYICONDATA
        {
            public int cbSize;
            public IntPtr hwnd;
            public int uID;
            public int uFlags;
            public IntPtr uCallbackMessage;
            public IntPtr hIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x80)]
            public string szTip;
            public int dwState;
            public int dwStateMask;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x100)]
            public string szInfo;
            public int uTimeout;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x40)]
            public string szInfoTitle;
            public int dwInfoFlags;
        }
    }
}

