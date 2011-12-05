using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View
{
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
			NotifyIcon = notifyIcon;
		}

		private static Icon GetIcon(string iconName)
		{
			return (Icon)Icons.ResourceManager.GetObject(iconName);
		}

		private void HideBalloon()
		{
			ShowBalloon(string.Empty, string.Empty, ToolTipIcon.None);
		}

		internal void HideTrayIcon()
		{
			NotifyIcon.Visible = false;
		}

		internal void SetTrayIcon()
		{
			if (!Updater.Instance.Enabled)
			{
				SetTrayIconText(Strings.SVNMonitorDisabled);
				SetTrayIconIcon(Icons.status_disabled);
				SetTrayIconVisible(true);
			}
			else
			{
				bool allNotModified = Status.AllNotModified;
				if (Status.Conflict)
				{
					string text = "SVN-Monitor - Possible conflict";
					if (allNotModified)
					{
						SetTrayIcon("status_conflict_not_modified", text);
					}
					else
					{
						SetTrayIcon("status_conflict", text);
					}
				}
				else
				{
					bool allUpToDate = Status.AllUpToDate;
					if (!allUpToDate && !allNotModified)
					{
						SetTrayIcon("status_updates_changes", "Local changes and updates are available");
					}
					else if (!allNotModified)
					{
						string formatter = string.Empty;
						if (Status.ModifiedSources.Count() == 1)
						{
							formatter = string.Format(" for \"{0}\"", Status.ModifiedSources.First().Name);
						}
						SetTrayIcon("status_changes", string.Format("Local changes are available{0}", formatter));
					}
					else if (!allUpToDate)
					{
						string formatter = string.Empty;
						if (Status.NotUpToDateSources.Count() == 1)
						{
							formatter = string.Format(" for \"{0}\"", Status.NotUpToDateSources.First().Name);
						}
						SetTrayIcon("status_updates", string.Format("Updates are available{0}", formatter));
					}
					else
					{
						SetTrayIcon("status_idle", "SVN-Monitor");
					}
				}
			}
		}

		private void SetTrayIcon(Icon icon, string text)
		{
			try
			{
				SetTrayIconText(text + (Status.Updating ? " (checking...)" : string.Empty));
				SetTrayIconIcon(icon);
				SetTrayIconVisible(true);
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
			SetTrayIcon(icon, text);
		}

		private void SetTrayIconIcon(Icon icon)
		{
			try
			{
				if (NotifyIcon.Icon != null)
				{
					NotifyIcon.Icon.Dispose();
				}
				NotifyIcon.Icon = icon;
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
				NotifyIcon.Text = text;
			}
			catch (NullReferenceException)
			{
			}
			catch (ArgumentOutOfRangeException)
			{
				text = TrayNotifier.TrimLongTipTextMore(text);
				NotifyIcon.Text = text;
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
				NotifyIcon.Visible = visible;
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
				NOTIFYICONDATA clsNID = default(NOTIFYICONDATA);
				FieldInfo windowField = NotifyIcon.GetType().GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
				FieldInfo idField = NotifyIcon.GetType().GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);
				IntPtr ptrWindow = ((NativeWindow)windowField.GetValue(NotifyIcon)).Handle;
				int id = (int)idField.GetValue(NotifyIcon);
				clsNID = new NOTIFYICONDATA
				{
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
					dwInfoFlags = (int)icon,
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
			ShowBalloonTip(timeOut, tipTitle, tipText, tipIcon, null);
		}

		public void ShowBalloonTip(int timeOut, string tipTitle, string tipText, ToolTipIcon tipIcon, EventHandler balloonTipClickedHandler)
		{
			try
			{
				if (balloonTipClickedHandler != null)
				{
					NotifyIcon.BalloonTipClicked -= balloonTipClickedHandler;
					NotifyIcon.BalloonTipClicked += balloonTipClickedHandler;
				}
				ShowBalloon(tipTitle, tipText, tipIcon);
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
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
			public string szTip;
			public int dwState;
			public int dwStateMask;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
			public string szInfo;
			public int uTimeout;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x40)]
			public string szInfoTitle;
			public int dwInfoFlags;
		}
	}
}