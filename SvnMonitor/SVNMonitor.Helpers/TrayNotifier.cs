using System.Windows.Forms;
using SVNMonitor.Resources.Text;
using System;
using System.Drawing;
using SVNMonitor.Entities;

namespace SVNMonitor.Helpers
{
internal class TrayNotifier
{
	private static NotifyIcon notifyIcon;

	private static Timer timer;

	public TrayNotifier()
	{
	}

	private static ContextMenuStrip CreateContextMenu(ToolStripItem[] menuItems)
	{
		ContextMenuStrip menu = new ContextMenuStrip();
		menu.Items.Add(Strings.TrayCommandHide, null, new EventHandler(null.TrayNotifier.HideClick));
		menu.Items.Add(new ToolStripSeparator());
		if (menuItems != null)
		{
			menu.Items.AddRange(menuItems);
		}
		return menu;
	}

	private static void HideClick(object sender, EventArgs e)
	{
		if (TrayNotifier.notifyIcon != null)
		{
			TrayNotifier.notifyIcon.Dispose();
		}
	}

	public static void Show(TrayNotifierInfo trayNotifierInfo)
	{
		if (trayNotifierInfo.TimeOut < 0)
		{
			return;
		}
		if (TrayNotifier.notifyIcon != null)
		{
			TrayNotifier.notifyIcon.Dispose();
		}
		NotifyIcon notifyIcon = new NotifyIcon();
		notifyIcon.Text = trayNotifier.trayNotifierInfo.Text;
		notifyIcon.Icon = trayNotifier.trayNotifierInfo.Icon;
		notifyIcon.BalloonTipIcon = trayNotifier.trayNotifierInfo.TipIcon;
		notifyIcon.BalloonTipText = trayNotifier.trayNotifierInfo.TipText;
		notifyIcon.BalloonTipTitle = trayNotifier.trayNotifierInfo.TipTitle;
		notifyIcon.ContextMenuStrip = TrayNotifier.CreateContextMenu(trayNotifier.trayNotifierInfo.MenuItems);
		TrayNotifier.notifyIcon = notifyIcon;
		EventHandler showFormClick = new EventHandler(trayNotifier.<Show>b__2);
		TrayNotifier.notifyIcon.BalloonTipClicked += showFormClick;
		TrayNotifier.notifyIcon.DoubleClick += showFormClick;
		TrayNotifier.notifyIcon.Click += showFormClick;
		TrayNotifier.notifyIcon.Visible = true;
		if (trayNotifier.trayNotifierInfo.ShowBalloonTip)
		{
			TrayNotifier.notifyIcon.ShowBalloonTip(trayNotifier.trayNotifierInfo.TimeOut * 1000);
		}
		if (trayNotifier.trayNotifierInfo.TimeOut > 0)
		{
			Timer timer = new Timer();
			timer.Interval = trayNotifier.trayNotifierInfo.TimeOut * 1000;
			TrayNotifier.timer = timer;
			TrayNotifier.CS$<>9__CachedAnonymousMethodDelegate4.add_Tick(new EventHandler((, ) => {
				TrayNotifier.notifyIcon.Dispose();
				TrayNotifier.timer.Stop();
			}
			));
			TrayNotifier.timer.Start();
		}
	}

	public static string TrimLongTipText(string tipText)
	{
		if (tipText.Length <= 64)
		{
			return tipText;
		}
		tipText = string.Concat(tipText.Substring(0, 59), "...");
		return tipText;
	}

	public static string TrimLongTipTextMore(string tipText)
	{
		if (tipText.Length <= 60)
		{
			return tipText;
		}
		tipText = string.Concat(tipText.Substring(0, 55), "...");
		return tipText;
	}

	internal class TrayNotifierInfo
	{
		public Icon Icon;

		public ToolStripMenuItem[] MenuItems;

		public bool ShowBalloonTip;

		public Source Source;

		public string Text;

		public int TimeOut;

		public ToolTipIcon TipIcon;

		public string TipText;

		public string TipTitle;

		public TrayNotifierInfo();
	}
}
}