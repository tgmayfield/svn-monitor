namespace SVNMonitor.View.Controls
{
    using SVNMonitor.Helpers;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class KeyboardEditorRow
    {
        public string AssociatedSetting { get; set; }

        public string Description { get; set; }

        public System.Drawing.Image Image { get; set; }

        public SVNMonitor.Helpers.KeyInfo KeyInfo { get; set; }

        public string KeyString
        {
            get
            {
                return this.KeyInfo.KeyString;
            }
        }

        public string Text { get; set; }
    }
}

