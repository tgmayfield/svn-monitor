namespace SVNMonitor.Helpers
{
    using SVNMonitor.Entities;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class TrayNotifier
    {
        private static NotifyIcon notifyIcon;
        private static Timer timer;

        private static ContextMenuStrip CreateContextMenu(ToolStripItem[] menuItems)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add(Strings.TrayCommandHide, null, new EventHandler(TrayNotifier.HideClick));
            menu.Items.Add(new ToolStripSeparator());
            if (menuItems != null)
            {
                menu.Items.AddRange(menuItems);
            }
            return menu;
        }

        private static void HideClick(object sender, EventArgs e)
        {
            if (notifyIcon != null)
            {
                notifyIcon.Dispose();
            }
        }

        public static void Show(TrayNotifierInfo trayNotifierInfo)
        {
            if ((trayNotifierInfo != null) && (trayNotifierInfo.TimeOut >= 0))
            {
                if (notifyIcon != null)
                {
                    notifyIcon.Dispose();
                }
                NotifyIcon tempLocal0 = new NotifyIcon {
                    Text = trayNotifierInfo.Text,
                    Icon = trayNotifierInfo.Icon,
                    BalloonTipIcon = trayNotifierInfo.TipIcon,
                    BalloonTipText = trayNotifierInfo.TipText,
                    BalloonTipTitle = trayNotifierInfo.TipTitle,
                    ContextMenuStrip = CreateContextMenu(trayNotifierInfo.MenuItems)
                };
                notifyIcon = tempLocal0;
                EventHandler showFormClick = delegate {
                    if (trayNotifierInfo.Source != null)
                    {
                        MainForm.ShowInstance(trayNotifierInfo.Source);
                    }
                    else
                    {
                        MainForm.ShowInstance();
                    }
                    notifyIcon.Dispose();
                };
                notifyIcon.BalloonTipClicked += showFormClick;
                notifyIcon.DoubleClick += showFormClick;
                notifyIcon.Click += showFormClick;
                notifyIcon.Visible = true;
                if (trayNotifierInfo.ShowBalloonTip)
                {
                    notifyIcon.ShowBalloonTip(trayNotifierInfo.TimeOut * 0x3e8);
                }
                if (trayNotifierInfo.TimeOut > 0)
                {
                    Timer tempLocal1 = new Timer {
                        Interval = trayNotifierInfo.TimeOut * 0x3e8
                    };
                    timer = tempLocal1;
                    timer.Tick += delegate {
                        notifyIcon.Dispose();
                        timer.Stop();
                    };
                    timer.Start();
                }
            }
        }

        public static string TrimLongTipText(string tipText)
        {
            if (tipText.Length > 0x40)
            {
                tipText = tipText.Substring(0, 0x3b) + "...";
            }
            return tipText;
        }

        public static string TrimLongTipTextMore(string tipText)
        {
            if (tipText.Length > 60)
            {
                tipText = tipText.Substring(0, 0x37) + "...";
            }
            return tipText;
        }

        internal class TrayNotifierInfo
        {
            public System.Drawing.Icon Icon { get; set; }

            public ToolStripMenuItem[] MenuItems { get; set; }

            public bool ShowBalloonTip { get; set; }

            public SVNMonitor.Entities.Source Source { get; set; }

            public string Text { get; set; }

            public int TimeOut { get; set; }

            public ToolTipIcon TipIcon { get; set; }

            public string TipText { get; set; }

            public string TipTitle { get; set; }
        }
    }
}

