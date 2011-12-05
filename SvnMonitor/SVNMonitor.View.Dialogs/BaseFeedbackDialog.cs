using System;
using SVNMonitor.Support;
using SVNMonitor.Logging;
using System.Windows.Forms;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Dialogs
{
public class BaseFeedbackDialog : BaseDialog
{
	public BaseFeedbackDialog()
	{
	}

	protected virtual void SendFeedback(BaseFeedback feedback)
	{
		Logger.Log.Info("Sending feedback...");
		DialogResult result = SendProgressDialog.ShowProgress(this, feedback);
		if (result == DialogResult.OK)
		{
			base.Close();
			return;
		}
		if (result == DialogResult.Abort)
		{
			Logger.Log.InfoFormat("Feedback aborted", new object[0]);
			base.Close();
			return;
		}
		if (result == DialogResult.Cancel)
		{
			Logger.Log.InfoFormat("Feedback not sent", new object[0]);
			result = MessageBox.Show(this, Strings.FeedbackErrorText, Strings.FeedbackErrorTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand);
			if (result == DialogResult.Retry)
			{
				Logger.Log.InfoFormat("Retrying feedback", new object[0]);
				this.SendFeedback(feedback);
				return;
			}
		}
		Logger.Log.InfoFormat("Feedback cancelled", new object[0]);
		base.Close();
	}
}
}