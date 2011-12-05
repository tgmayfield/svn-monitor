using System;
using System.Windows.Forms;

namespace SVNMonitor.Helpers
{
	public class KeyPressedEventArgs : EventArgs
	{
		internal KeyPressedEventArgs(ModifierKey modifier, Keys key)
		{
			KeyInfo = SVNMonitor.Helpers.KeyInfo.GetKeyInfo(modifier, key);
		}

		public SVNMonitor.Helpers.KeyInfo KeyInfo { get; private set; }
	}
}