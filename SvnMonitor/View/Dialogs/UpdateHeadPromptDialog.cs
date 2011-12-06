using System;
using System.ComponentModel;
using System.Windows.Forms;

using SVNMonitor.Settings;

namespace SVNMonitor.View.Dialogs
{
	public partial class UpdateHeadPromptDialog : BasePromptDialog
	{

		public UpdateHeadPromptDialog()
		{
			InitializeComponent();
		}

		private void checkDontShowAgain_CheckedChanged(object sender, EventArgs e)
		{
			btnNo.Enabled = !checkDontShowAgain.Checked;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			ApplicationSettingsManager.Settings.PromptUpdateHeadRevision = !checkDontShowAgain.Checked;
			ApplicationSettingsManager.SaveSettings();
		}

		public static DialogResult Prompt()
		{
			return Prompt(null);
		}

		public static DialogResult Prompt(IWin32Window parent)
		{
			if (!ApplicationSettingsManager.Settings.PromptUpdateHeadRevision)
			{
				return DialogResult.Yes;
			}
			UpdateHeadPromptDialog dialog = new UpdateHeadPromptDialog();
			return dialog.ShowDialog(parent);
		}
	}
}