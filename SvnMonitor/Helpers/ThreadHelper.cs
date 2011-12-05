using System.Globalization;
using System;
using System.Threading;
using SVNMonitor.Logging;

namespace SVNMonitor.Helpers
{
public class ThreadHelper
{
	private static CultureInfo mainCulture;

	public ThreadHelper()
	{
	}

	public static bool Queue(WaitCallback callBack, string threadName)
	{
		return ThreadHelper.Queue(callBack, threadName, null);
	}

	public static bool Queue(WaitCallback callBack, string threadName, object state)
	{
		WaitCallback start = delegate {
			ThreadHelper.SetThreadName(threadName);
			callBack(_state);
		};
		return ThreadPool.QueueUserWorkItem(start, state);
	}

	public static void SetMainThreadUICulture(string cultureName)
	{
		try
		{
			Logger.Log.Info(string.Format("UICulture = {0}", cultureName));
			CultureInfo culture = new CultureInfo(cultureName);
			ThreadHelper.mainCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error setting UICulture: {0}", cultureName), ex);
		}
	}

	public static void SetThreadName(string name)
	{
		if (Thread.CurrentThread.Name != null)
		{
			return;
		}
		Thread.CurrentThread.Name = string.Concat("SVNM_", name.PadRight(10));
		if (ThreadHelper.mainCulture != null)
		{
			Thread.CurrentThread.CurrentUICulture = ThreadHelper.mainCulture;
		}
	}

	public static void Sleep(int millisecondsTimeout)
	{
		Thread.Sleep(millisecondsTimeout);
	}

	public static void Sleep(TimeSpan timeOut)
	{
		Thread.Sleep(timeOut);
	}
}
}