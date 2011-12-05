using System;
using System.Windows.Forms;

using SVNMonitor.Extensions;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Helpers
{
	internal class ClipboardHelper
	{
		public static void SetText(string text)
		{
			try
			{
				Logger.Log.DebugFormat("Copying '{0}'", text);
				if (!string.IsNullOrEmpty(text))
				{
					Clipboard.SetText(text);
					EventLog.LogInfo(Strings.TextCopiedToClipboard_FORMAT.FormatWith(new object[]
					{
						text
					}), text);
				}
			}
			catch (Exception ex)
			{
				EventLog.LogError(Strings.ErrorSettingClipboard, null, ex);
			}
		}
	}
}