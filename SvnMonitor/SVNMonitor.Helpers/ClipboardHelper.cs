using System;
using SVNMonitor.Logging;
using System.Windows.Forms;
using SVNMonitor;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Helpers
{
internal class ClipboardHelper
{
	public ClipboardHelper()
	{
	}

	public static void SetText(string text)
	{
		object[] objArray;
		try
		{
			Logger.Log.DebugFormat("Copying '{0}'", text);
			Clipboard.SetText(text);
			EventLog.LogInfo(Strings.TextCopiedToClipboard_FORMAT.FormatWith(new object[] { text }), text);
		}
		catch (Exception ex)
		{
			EventLog.LogError(Strings.ErrorSettingClipboard, null, ex);
		}
		if (string.IsNullOrEmpty(text))
		{
		}
		else
		{
		}
	}
}
}