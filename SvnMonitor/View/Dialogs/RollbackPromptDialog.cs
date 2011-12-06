using System;
using System.ComponentModel;
using System.Windows.Forms;

using SVNMonitor.Settings;

namespace SVNMonitor.View.Dialogs
{
	public partial class RollbackPromptDialog : BasePromptDialog
	{

		public RollbackPromptDialog()
		{
			InitializeComponent();
		}

		public RollbackPromptDialog(long revision)
			: this()
		{
			lblPrompt.Text = string.Format(lblPrompt.Text, revision);
		}

		private void checkDontShowAgain_CheckedChanged(object sender, EventArgs e)
		{
			btnNo.Enabled = !checkDontShowAgain.Checked;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			ApplicationSettingsManager.Settings.PromptRollbackOldRevision = !checkDontShowAgain.Checked;
			ApplicationSettingsManager.SaveSettings();
		}

		public static DialogResult Prompt(long revision)
		{
			return Prompt(null, revision);
		}

		public static DialogResult Prompt(IWin32Window parent, long revision)
		{
			if (!ApplicationSettingsManager.Settings.PromptRollbackOldRevision)
			{
				return DialogResult.Yes;
			}
			RollbackPromptDialog dialog = new RollbackPromptDialog(revision);
			return dialog.ShowDialog(parent);
		}
	}
}