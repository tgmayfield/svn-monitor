using System;
using System.Windows.Forms;
using System.Drawing;
using SVNMonitor.Resources;
using SVNMonitor;
using SVNMonitor.Resources.Text;
using SVNMonitor.Logging;
using SVNMonitor.Helpers;
using System.Runtime.InteropServices;
using System.Reflection;

namespace SVNMonitor.View
{
public class MainNotifier
{
	private const int NIF_ICON = 2;

	private const int NIF_INFO = 16;

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

	private const long NIN_BALLOONHIDE = 1027L;

	private const long NIN_BALLOONSHOW = 1026L;

	private const long NIN_BALLOONTIMEOUT = 1028L;

	private const long NIN_BALLOONUSERCLICK = 1029L;

	private const int NOTIFYICON_VERSION = 5;

	private const long WM_USER = 1024L;

	private NotifyIcon NotifyIcon
	{
		get;
		set;
	}

	public MainNotifier(NotifyIcon notifyIcon)
	{
		this.NotifyIcon = notifyIcon;
	}

	private static Icon GetIcon(string iconName)
	{
		Icon icon = (Icon)Icons.ResourceManager.GetObject(iconName);
		return icon;
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
		string text;
		if (!Updater.Instance.Enabled)
		{
			this.SetTrayIconText(Strings.SVNMonitorDisabled);
			this.SetTrayIconIcon(Icons.status_disabled);
			this.SetTrayIconVisible(true);
			return;
		}
		if (Status.Conflict)
		{
			text = "SVN-Monitor - Possible conflict";
			if (Status.AllNotModified)
			{
				this.SetTrayIcon("status_conflict_not_modified", text);
				return;
			}
		}
		this.SetTrayIcon("status_conflict", text);
		return;
		if (!Status.AllUpToDate && !allNotModified)
		{
			this.SetTrayIcon("status_updates_changes", "Local changes and updates are available");
			return;
		}
		if (!allNotModified)
		{
			string formatter = string.Empty;
			if (Status.ModifiedSources.Count<Source>() == 1)
			{
				formatter = string.Format(" for \"{0}\"", Status.ModifiedSources.First<Source>().Name);
			}
			this.SetTrayIcon("status_changes", string.Format("Local changes are available{0}", formatter));
			return;
		}
		if (!allUpToDate)
		{
			string formatter = string.Empty;
			if (Status.NotUpToDateSources.Count<Source>() == 1)
			{
				formatter = string.Format(" for \"{0}\"", Status.NotUpToDateSources.First<Source>().Name);
			}
			this.SetTrayIcon("status_updates", string.Format("Updates are available{0}", formatter));
			return;
		}
		this.SetTrayIcon("status_idle", "SVN-Monitor");
	}

	private void SetTrayIcon(Icon icon, string text)
	{
		try
		{
			string updating = (Status.Updating ? " (checking...)" : string.Empty);
			this.SetTrayIconText(string.Concat(text, updating));
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
			iconName = string.Concat(iconName, "_checking");
		}
		if (Status.Error)
		{
			iconName = string.Concat(iconName, "_error");
		}
		if (Status.Recommended)
		{
			iconName = string.Concat(iconName, "_recommended");
			text = string.Concat(text, " (Recommended)");
		}
		Icon icon = MainNotifier.GetIcon(iconName);
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

	[DllImport("shell32.dll", CharSet=CharSet.None)]
	private static extern int Shell_NotifyIconA(int dwMessage, ref NOTIFYICONDATA pnid);

	private void ShowBalloon(string title, string text, ToolTipIcon icon)
	{
		try
		{
			FieldInfo windowField = this.NotifyIcon.GetType().GetField("window", BindingFlags.Instance | BindingFlags.NonPublic);
			FieldInfo idField = this.NotifyIcon.GetType().GetField("id", BindingFlags.Instance | BindingFlags.NonPublic);
			IntPtr ptrWindow = (NativeWindow)windowField.GetValue(this.NotifyIcon).Handle;
			int id = (int)idField.GetValue(this.NotifyIcon);
			NOTIFYICONDATA clsNID = new NOTIFYICONDATA();
			clsNID.szTip = "";
			clsNID.dwState = 0;
			clsNID.dwStateMask = 0;
			clsNID.hIcon = IntPtr.Zero;
			clsNID.uCallbackMessage = new IntPtr(512);
			clsNID.uID = id;
			clsNID.hwnd = ptrWindow;
			clsNID.szInfo = text;
			clsNID.szInfoTitle = title;
			clsNID.uFlags = 16;
			clsNID.cbSize = Marshal.SizeOf(clsNID);
			clsNID.dwInfoFlags = icon;
			clsNID.uTimeout = 5;
			MainNotifier.Shell_NotifyIconA(1, ref clsNID);
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
				this.NotifyIcon.remove_BalloonTipClicked(balloonTipClickedHandler);
				this.NotifyIcon.add_BalloonTipClicked(balloonTipClickedHandler);
			}
			this.ShowBalloon(tipTitle, tipText, tipIcon);
		}
		catch (NullReferenceException)
		{
		}
	}

	private struct NOTIFYICONDATA
	{
		public int cbSize;

		public int dwInfoFlags;

		public int dwState;

		public int dwStateMask;

		public IntPtr hIcon;

		public IntPtr hwnd;

		public string szInfo;

		public string szInfoTitle;

		public string szTip;

		public IntPtr uCallbackMessage;

		public int uFlags;

		public int uID;

		public int uTimeout;
	}
}
}