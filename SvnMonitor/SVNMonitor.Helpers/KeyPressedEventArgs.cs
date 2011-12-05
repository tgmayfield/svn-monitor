using System;
using System.Windows.Forms;

namespace SVNMonitor.Helpers
{
public class KeyPressedEventArgs : EventArgs
{
	public KeyInfo KeyInfo
	{
		get;
		private set;
	}

	internal KeyPressedEventArgs(ModifierKey modifier, Keys key)
	{
		this.KeyInfo = KeyInfo.GetKeyInfo(modifier, key);
	}
}
}