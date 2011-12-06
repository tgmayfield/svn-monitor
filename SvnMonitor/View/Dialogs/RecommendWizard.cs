using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;
using SVNMonitor.Settings;

using SharpSvn;

namespace SVNMonitor.View.Dialogs
{
	public partial class RecommendWizard : BaseDialog
	{
		private bool committed;
		private bool recommended;
		private long revision;
		private SVNMonitor.Entities.Source source;
		private bool updated;
		
		public RecommendWizard()
		{
			InitializeComponent();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			base.Close();
		}

		private void btnCommit_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SVNCommit();
		}

		private void btnRecommend_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Recommend();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SVNUpdate();
		}


		private void linkUndo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			UndoRecommend();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if ((recommended && !committed) && (MessageBox.Show(this, Strings.AskCloseWizardWithoutCommitting, Strings.SVNMonitorCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes))
			{
				e.Cancel = true;
			}
			base.OnFormClosing(e);
		}

		private void Recommend()
		{
			Source.RecommendWithoutHook(Revision);
			recommended = true;
			RefreshButtons();
			btnCommit.Focus();
		}

		public static DialogResult Recommend(SVNMonitor.Entities.Source source, long revision)
		{
			RecommendWizard wizard = new RecommendWizard
			{
				Source = source,
				Revision = revision,
				CommitMessage = ApplicationSettingsManager.Settings.RecommendationMessage.Replace("${REVISION}", revision.ToString())
			};
			return wizard.ShowDialog();
		}

		private void RefreshButtons()
		{
			btnUpdate.Enabled = true;
			btnRecommend.Enabled = !recommended && updated;
			btnCommit.Enabled = recommended;
			linkUndo.Visible = recommended;
			txtCommitMessage.Visible = recommended;
		}

		private void SVNCommit()
		{
			string error = string.Empty;
			Exception recommendException = null;
			try
			{
				SharpSVNClient.CommitRecommend(Source, Revision, CommitMessage);
				committed = true;
				RefreshButtons();
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			catch (SvnAuthorizationException svnaex)
			{
				recommendException = svnaex;
				error = Strings.ErrorNoCommitPermissions;
			}
			catch (SvnRepositoryIOException svnrex)
			{
				recommendException = svnrex;
				error = Strings.ErrorSvnVersionTooOld;
			}
			catch (Exception ex)
			{
				recommendException = ex;
				error = ex.Message;
			}
			if (recommendException != null)
			{
				Logger.Log.Error(recommendException.Message, recommendException);
				ErrorHandler.Append(Strings.ErrorTryingToRecommend_FORMAT.FormatWith(new object[]
				{
					Revision, Source.Name
				}), Source, recommendException);
				error = Strings.ErrorCommitFailedCheckLog_FORMAT.FormatWith(new object[]
				{
					error
				});
				MessageBox.Show(this, error, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void SVNUpdate()
		{
			try
			{
				Source.SVNUpdate();
				try
				{
					Source.RefreshRecommended();
					lblUpdateAgain.Show();
					updated = true;
				}
				catch (Exception ex2)
				{
					MessageBox.Show(this, string.Format("{0}:{1}{2}", Strings.ErrorUpdateSuccessfulButFailedRefreshingRecommended, Environment.NewLine, ex2.Message), Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			catch (Exception ex1)
			{
				MessageBox.Show(this, string.Format("{0}:{1}{2}", Strings.ErrorUpdateFailed, Environment.NewLine, ex1.Message), Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			RefreshButtons();
			btnRecommend.Focus();
		}

		private void UndoRecommend()
		{
			Source.UndoRecommend(Revision);
			recommended = false;
			RefreshButtons();
		}

		private void UpdateHeader()
		{
			Text = string.Format("Recommend revision {0} of '{1}'", revision, source.Name);
			string revisionString = (Revision == 0L) ? "{0}" : Revision.ToString();
			string sourceName = (Source == null) ? "{1}" : Source.Name;
			lblHeader.Text = string.Format(lblHeader.Text, revisionString, sourceName);
		}

		[Browsable(false)]
		public string CommitMessage
		{
			get { return txtCommitMessage.Text; }
			set { txtCommitMessage.Text = value; }
		}

		[Browsable(false)]
		public long Revision
		{
			[DebuggerNonUserCode]
			get { return revision; }
			private set
			{
				revision = value;
				UpdateHeader();
			}
		}

		[Browsable(false)]
		public SVNMonitor.Entities.Source Source
		{
			[DebuggerNonUserCode]
			get { return source; }
			private set
			{
				source = value;
				UpdateHeader();
			}
		}
	}
}