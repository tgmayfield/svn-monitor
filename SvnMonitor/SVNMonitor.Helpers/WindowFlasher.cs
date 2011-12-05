using System;
using System.Windows.Forms;
using System.Timers;
using System.Runtime.InteropServices;
using SVNMonitor.Logging;

namespace SVNMonitor.Helpers
{
internal class WindowFlasher
{
	public WindowFlasher()
	{
	}

	public static void Flash(Form form)
	{
		object[] objArray;
		ElapsedEventHandler elapsedEventHandler = null;
		WindowFlasher windowFlasher = new WindowFlasher();
		windowFlasher.form = form;
		if (windowFlasher.form.InvokeRequired)
		{
			windowFlasher.form.BeginInvoke(new Action<Form>(null.WindowFlasher.Flash), new object[] { windowFlasher.form });
			return;
		}
		WindowFlasher.InternalFlash(windowFlasher.form, FlashFlags.FLASHW_ALL);
		Timer timer = new Timer();
		timer.AutoReset = false;
		timer.Interval = 2000;
	}

	[DllImport("user32.dll", CharSet=CharSet.None)]
	private static extern int FlashWindowEx(ref FLASHWINFO pwfi);

	private static void InternalFlash(Form form, FlashFlags flags)
	{
		FLASHWINFO fw = new FLASHWINFO();
		fw.cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO)));
		fw.hwnd = form.Handle;
		fw.dwFlags = flags;
		fw.uCount = -1;
		WindowFlasher.FlashWindowEx(ref fw);
	}

	public static void StopFlash(Form form)
	{
		object[] objArray;
		try
		{
			if (form.InvokeRequired)
			{
				form.BeginInvoke(new Action<Form>(null.WindowFlasher.StopFlash), new object[] { form });
			}
			else
			{
				WindowFlasher.InternalFlash(form, FlashFlags.FLASHW_STOP);
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

	private struct FLASHWINFO
	{
		public uint cbSize;

		public int dwFlags;

		public int dwTimeout;

		public IntPtr hwnd;

		public uint uCount;
	}
}
}