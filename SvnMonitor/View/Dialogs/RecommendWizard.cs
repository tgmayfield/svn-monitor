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
		private Button btnClose;
		private Button btnCommit;
		private Button btnRecommend;
		private Button btnUpdate;
		private bool committed;
		private IContainer components;
		private GroupBox groupCommit;
		private GroupBox groupRecommend;
		private GroupBox groupUpdate;
		private Label lblCommit;
		private Label lblHeader;
		private Label lblRecommend;
		private Label lblUpdate;
		private Label lblUpdateAgain;
		private LinkLabel linkUndo;
		private Panel panelCommit;
		private Panel panelHeader;
		private Panel panelRecommend;
		private Panel panelUpdate;
		private bool recommended;
		private long revision;
		private SVNMonitor.Entities.Source source;
		private TextBox txtCommitMessage;
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(RecommendWizard));
			panelUpdate = new Panel();
			groupUpdate = new GroupBox();
			btnUpdate = new Button();
			lblUpdateAgain = new Label();
			lblUpdate = new Label();
			panelRecommend = new Panel();
			groupRecommend = new GroupBox();
			btnRecommend = new Button();
			linkUndo = new LinkLabel();
			lblRecommend = new Label();
			panelCommit = new Panel();
			groupCommit = new GroupBox();
			txtCommitMessage = new TextBox();
			btnCommit = new Button();
			lblCommit = new Label();
			panelHeader = new Panel();
			lblHeader = new Label();
			btnClose = new Button();
			panelUpdate.SuspendLayout();
			groupUpdate.SuspendLayout();
			panelRecommend.SuspendLayout();
			groupRecommend.SuspendLayout();
			panelCommit.SuspendLayout();
			groupCommit.SuspendLayout();
			panelHeader.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(panelUpdate, "panelUpdate");
			panelUpdate.Controls.Add(groupUpdate);
			panelUpdate.Name = "panelUpdate";
			groupUpdate.Controls.Add(btnUpdate);
			groupUpdate.Controls.Add(lblUpdateAgain);
			groupUpdate.Controls.Add(lblUpdate);
			resources.ApplyResources(groupUpdate, "groupUpdate");
			groupUpdate.Name = "groupUpdate";
			groupUpdate.TabStop = false;
			resources.ApplyResources(btnUpdate, "btnUpdate");
			btnUpdate.Image = Images.svn_update_32;
			btnUpdate.Name = "btnUpdate";
			btnUpdate.Click += btnUpdate_Click;
			lblUpdateAgain.BackColor = Color.Transparent;
			lblUpdateAgain.ForeColor = Color.MidnightBlue;
			resources.ApplyResources(lblUpdateAgain, "lblUpdateAgain");
			lblUpdateAgain.Name = "lblUpdateAgain";
			lblUpdate.BackColor = Color.Transparent;
			resources.ApplyResources(lblUpdate, "lblUpdate");
			lblUpdate.Name = "lblUpdate";
			resources.ApplyResources(panelRecommend, "panelRecommend");
			panelRecommend.Controls.Add(groupRecommend);
			panelRecommend.Name = "panelRecommend";
			groupRecommend.Controls.Add(btnRecommend);
			groupRecommend.Controls.Add(linkUndo);
			groupRecommend.Controls.Add(lblRecommend);
			resources.ApplyResources(groupRecommend, "groupRecommend");
			groupRecommend.Name = "groupRecommend";
			groupRecommend.TabStop = false;
			resources.ApplyResources(btnRecommend, "btnRecommend");
			btnRecommend.Image = Images.star_yellow_32;
			btnRecommend.Name = "btnRecommend";
			btnRecommend.Click += btnRecommend_Click;
			resources.ApplyResources(linkUndo, "linkUndo");
			linkUndo.BackColor = Color.Transparent;
			linkUndo.ForeColor = Color.MidnightBlue;
			linkUndo.LinkColor = Color.MidnightBlue;
			linkUndo.Name = "linkUndo";
			linkUndo.TabStop = true;
			linkUndo.LinkClicked += linkUndo_LinkClicked;
			lblRecommend.BackColor = Color.Transparent;
			resources.ApplyResources(lblRecommend, "lblRecommend");
			lblRecommend.Name = "lblRecommend";
			resources.ApplyResources(panelCommit, "panelCommit");
			panelCommit.Controls.Add(groupCommit);
			panelCommit.Name = "panelCommit";
			groupCommit.Controls.Add(txtCommitMessage);
			groupCommit.Controls.Add(btnCommit);
			groupCommit.Controls.Add(lblCommit);
			resources.ApplyResources(groupCommit, "groupCommit");
			groupCommit.Name = "groupCommit";
			groupCommit.TabStop = false;
			resources.ApplyResources(txtCommitMessage, "txtCommitMessage");
			txtCommitMessage.Name = "txtCommitMessage";
			resources.ApplyResources(btnCommit, "btnCommit");
			btnCommit.Image = Images.svn_commit_32;
			btnCommit.Name = "btnCommit";
			btnCommit.Click += btnCommit_Click;
			lblCommit.BackColor = Color.Transparent;
			resources.ApplyResources(lblCommit, "lblCommit");
			lblCommit.Name = "lblCommit";
			resources.ApplyResources(panelHeader, "panelHeader");
			panelHeader.Controls.Add(lblHeader);
			panelHeader.Name = "panelHeader";
			lblHeader.BackColor = Color.Transparent;
			resources.ApplyResources(lblHeader, "lblHeader");
			lblHeader.Name = "lblHeader";
			resources.ApplyResources(btnClose, "btnClose");
			btnClose.DialogResult = DialogResult.Cancel;
			btnClose.Name = "btnClose";
			btnClose.Click += btnClose_Click;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnClose;
			base.Controls.Add(btnClose);
			base.Controls.Add(panelCommit);
			base.Controls.Add(panelRecommend);
			base.Controls.Add(panelUpdate);
			base.Controls.Add(panelHeader);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Name = "RecommendWizard";
			panelUpdate.ResumeLayout(false);
			groupUpdate.ResumeLayout(false);
			panelRecommend.ResumeLayout(false);
			groupRecommend.ResumeLayout(false);
			groupRecommend.PerformLayout();
			panelCommit.ResumeLayout(false);
			groupCommit.ResumeLayout(false);
			groupCommit.PerformLayout();
			panelHeader.ResumeLayout(false);
			base.ResumeLayout(false);
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