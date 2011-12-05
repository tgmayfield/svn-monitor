namespace SVNMonitor.View.Controls
{
    using SVNMonitor.Helpers;
    using System;
    using System.Windows.Forms;

    public class HtmlViewer : WebBrowser
    {
        public HtmlViewer()
        {
            base.AllowNavigation = false;
            base.AllowWebBrowserDrop = false;
            base.IsWebBrowserContextMenuEnabled = false;
            base.WebBrowserShortcutsEnabled = false;
        }

        private void SetDocument(string content, string extension)
        {
            string tempFile = FileSystemHelper.GetTempFileName();
            FileSystemHelper.DeleteFile(tempFile);
            tempFile = tempFile + extension;
            FileSystemHelper.WriteAllText(tempFile, content);
            base.DocumentCompleted += (s, ea) => FileSystemHelper.DeleteFile(tempFile);
            base.Navigate(tempFile);
        }

        public void SetHtml(string html)
        {
            this.SetDocument(html, ".html");
        }

        public void SetXml(string xml)
        {
            this.SetDocument(xml, ".xml");
        }
    }
}

