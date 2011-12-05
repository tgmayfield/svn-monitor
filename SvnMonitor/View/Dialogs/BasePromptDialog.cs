using System;
using System.Windows.Forms;

namespace SVNMonitor.View.Dialogs
{
	public class BasePromptDialog : BaseDialog
	{
		public BasePromptDialog()
		{
			base.ControlBox = false;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
		}
	}
}