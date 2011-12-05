namespace SVNMonitor.Helpers
{
    using SVNMonitor;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using System;
    using System.Windows.Forms;

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
                    EventLog.LogInfo(Strings.TextCopiedToClipboard_FORMAT.FormatWith(new object[] { text }), text);
                }
            }
            catch (Exception ex)
            {
                EventLog.LogError(Strings.ErrorSettingClipboard, null, ex);
            }
        }
    }
}

