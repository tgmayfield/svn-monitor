using System;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.Support;

namespace SVNMonitor.View.Dialogs
{
	public class BaseFeedbackDialog : BaseDialog
	{
		protected virtual void SendFeedback(BaseFeedback feedback)
		{
			Logger.Log.Info("Sending feedback...");
			DialogResult result = SendProgressDialog.ShowProgress(this, feedback);
			switch (result)
			{
				case DialogResult.OK:
					base.Close();
					return;

				case DialogResult.Abort:
					Logger.Log.InfoFormat("Feedback aborted", new object[0]);
					base.Close();
					return;
			}
			if (result == DialogResult.Cancel)
			{
				Logger.Log.InfoFormat("Feedback not sent", new object[0]);
				if (MessageBox.Show(this, Strings.FeedbackErrorText, Strings.FeedbackErrorTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) == DialogResult.Retry)
				{
					Logger.Log.InfoFormat("Retrying feedback", new object[0]);
					SendFeedback(feedback);
				}
				else
				{
					Logger.Log.InfoFormat("Feedback cancelled", new object[0]);
					base.Close();
				}
			}
		}
	}
}