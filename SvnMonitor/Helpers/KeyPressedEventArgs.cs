namespace SVNMonitor.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class KeyPressedEventArgs : EventArgs
    {
        internal KeyPressedEventArgs(ModifierKey modifier, Keys key)
        {
            this.KeyInfo = SVNMonitor.Helpers.KeyInfo.GetKeyInfo(modifier, key);
        }

        public SVNMonitor.Helpers.KeyInfo KeyInfo { get; private set; }
    }
}

