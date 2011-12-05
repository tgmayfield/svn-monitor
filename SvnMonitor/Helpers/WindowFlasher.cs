namespace SVNMonitor.Helpers
{
    using SVNMonitor.Logging;
    using System;
    using System.Runtime.InteropServices;
    using System.Timers;
    using System.Windows.Forms;

    internal class WindowFlasher
    {
        public static void Flash(Form form)
        {
            if (form.InvokeRequired)
            {
                form.BeginInvoke(new Action<Form>(WindowFlasher.Flash), new object[] { form });
            }
            else
            {
                InternalFlash(form, FlashFlags.FLASHW_ALL);
                System.Timers.Timer timer = new System.Timers.Timer {
                    AutoReset = false,
                    Interval = 2000.0
                };
                timer.Elapsed += (s, ea) => StopFlash(form);
                timer.Start();
            }
        }

        [DllImport("user32.dll")]
        private static extern int FlashWindowEx(ref FLASHWINFO pwfi);
        private static void InternalFlash(Form form, FlashFlags flags)
        {
            FLASHWINFO fw = new FLASHWINFO {
                cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO))),
                hwnd = form.Handle,
                dwFlags = (int) flags,
                uCount = uint.MaxValue
            };
            FlashWindowEx(ref fw);
        }

        public static void StopFlash(Form form)
        {
            try
            {
                if (form.InvokeRequired)
                {
                    form.BeginInvoke(new Action<Form>(WindowFlasher.StopFlash), new object[] { form });
                }
                else
                {
                    InternalFlash(form, FlashFlags.FLASHW_STOP);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error trying to stop flashing the window.", ex);
            }
        }

        private enum FlashFlags
        {
            FLASHW_ALL = 3,
            FLASHW_CAPTION = 1,
            FLASHW_STOP = 0,
            FLASHW_TIMER = 4,
            FLASHW_TIMERNOFG = 12,
            FLASHW_TRAY = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public int dwFlags;
            public uint uCount;
            public int dwTimeout;
        }
    }
}

