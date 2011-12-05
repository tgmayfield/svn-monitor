using System;

namespace SVNMonitor.View.Controls
{
	public class KeyboardEditorRow
	{
		public string AssociatedSetting { get; set; }

		public string Description { get; set; }

		public System.Drawing.Image Image { get; set; }

		public SVNMonitor.Helpers.KeyInfo KeyInfo { get; set; }

		public string KeyString
		{
			get { return KeyInfo.KeyString; }
		}

		public string Text { get; set; }
	}
}