namespace SVNMonitor.View.Dialogs
{
    using SharpSvn;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.SVN;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class RecommendWizard : BaseDialog
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
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            base.Close();
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.SVNCommit();
        }

        private void btnRecommend_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.Recommend();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.SVNUpdate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RecommendWizard));
            this.panelUpdate = new Panel();
            this.groupUpdate = new GroupBox();
            this.btnUpdate = new Button();
            this.lblUpdateAgain = new Label();
            this.lblUpdate = new Label();
            this.panelRecommend = new Panel();
            this.groupRecommend = new GroupBox();
            this.btnRecommend = new Button();
            this.linkUndo = new LinkLabel();
            this.lblRecommend = new Label();
            this.panelCommit = new Panel();
            this.groupCommit = new GroupBox();
            this.txtCommitMessage = new TextBox();
            this.btnCommit = new Button();
            this.lblCommit = new Label();
            this.panelHeader = new Panel();
            this.lblHeader = new Label();
            this.btnClose = new Button();
            this.panelUpdate.SuspendLayout();
            this.groupUpdate.SuspendLayout();
            this.panelRecommend.SuspendLayout();
            this.groupRecommend.SuspendLayout();
            this.panelCommit.SuspendLayout();
            this.groupCommit.SuspendLayout();
            this.panelHeader.SuspendLayout();
            base.SuspendLayout();
            resources.ApplyResources(this.panelUpdate, "panelUpdate");
            this.panelUpdate.Controls.Add(this.groupUpdate);
            this.panelUpdate.Name = "panelUpdate";
            this.groupUpdate.Controls.Add(this.btnUpdate);
            this.groupUpdate.Controls.Add(this.lblUpdateAgain);
            this.groupUpdate.Controls.Add(this.lblUpdate);
            resources.ApplyResources(this.groupUpdate, "groupUpdate");
            this.groupUpdate.Name = "groupUpdate";
            this.groupUpdate.TabStop = false;
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Image = Images.svn_update_32;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            this.lblUpdateAgain.BackColor = Color.Transparent;
            this.lblUpdateAgain.ForeColor = Color.MidnightBlue;
            resources.ApplyResources(this.lblUpdateAgain, "lblUpdateAgain");
            this.lblUpdateAgain.Name = "lblUpdateAgain";
            this.lblUpdate.BackColor = Color.Transparent;
            resources.ApplyResources(this.lblUpdate, "lblUpdate");
            this.lblUpdate.Name = "lblUpdate";
            resources.ApplyResources(this.panelRecommend, "panelRecommend");
            this.panelRecommend.Controls.Add(this.groupRecommend);
            this.panelRecommend.Name = "panelRecommend";
            this.groupRecommend.Controls.Add(this.btnRecommend);
            this.groupRecommend.Controls.Add(this.linkUndo);
            this.groupRecommend.Controls.Add(this.lblRecommend);
            resources.ApplyResources(this.groupRecommend, "groupRecommend");
            this.groupRecommend.Name = "groupRecommend";
            this.groupRecommend.TabStop = false;
            resources.ApplyResources(this.btnRecommend, "btnRecommend");
            this.btnRecommend.Image = Images.star_yellow_32;
            this.btnRecommend.Name = "btnRecommend";
            this.btnRecommend.Click += new EventHandler(this.btnRecommend_Click);
            resources.ApplyResources(this.linkUndo, "linkUndo");
            this.linkUndo.BackColor = Color.Transparent;
            this.linkUndo.ForeColor = Color.MidnightBlue;
            this.linkUndo.LinkColor = Color.MidnightBlue;
            this.linkUndo.Name = "linkUndo";
            this.linkUndo.TabStop = true;
            this.linkUndo.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkUndo_LinkClicked);
            this.lblRecommend.BackColor = Color.Transparent;
            resources.ApplyResources(this.lblRecommend, "lblRecommend");
            this.lblRecommend.Name = "lblRecommend";
            resources.ApplyResources(this.panelCommit, "panelCommit");
            this.panelCommit.Controls.Add(this.groupCommit);
            this.panelCommit.Name = "panelCommit";
            this.groupCommit.Controls.Add(this.txtCommitMessage);
            this.groupCommit.Controls.Add(this.btnCommit);
            this.groupCommit.Controls.Add(this.lblCommit);
            resources.ApplyResources(this.groupCommit, "groupCommit");
            this.groupCommit.Name = "groupCommit";
            this.groupCommit.TabStop = false;
            resources.ApplyResources(this.txtCommitMessage, "txtCommitMessage");
            this.txtCommitMessage.Name = "txtCommitMessage";
            resources.ApplyResources(this.btnCommit, "btnCommit");
            this.btnCommit.Image = Images.svn_commit_32;
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Click += new EventHandler(this.btnCommit_Click);
            this.lblCommit.BackColor = Color.Transparent;
            resources.ApplyResources(this.lblCommit, "lblCommit");
            this.lblCommit.Name = "lblCommit";
            resources.ApplyResources(this.panelHeader, "panelHeader");
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Name = "panelHeader";
            this.lblHeader.BackColor = Color.Transparent;
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.lblHeader.Name = "lblHeader";
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            resources.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnClose;
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.panelCommit);
            base.Controls.Add(this.panelRecommend);
            base.Controls.Add(this.panelUpdate);
            base.Controls.Add(this.panelHeader);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Name = "RecommendWizard";
            this.panelUpdate.ResumeLayout(false);
            this.groupUpdate.ResumeLayout(false);
            this.panelRecommend.ResumeLayout(false);
            this.groupRecommend.ResumeLayout(false);
            this.groupRecommend.PerformLayout();
            this.panelCommit.ResumeLayout(false);
            this.groupCommit.ResumeLayout(false);
            this.groupCommit.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void linkUndo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.LogUserAction();
            this.UndoRecommend();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if ((this.recommended && !this.committed) && (MessageBox.Show(this, Strings.AskCloseWizardWithoutCommitting, Strings.SVNMonitorCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes))
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        private void Recommend()
        {
            this.Source.RecommendWithoutHook(this.Revision);
            this.recommended = true;
            this.RefreshButtons();
            this.btnCommit.Focus();
        }

        public static DialogResult Recommend(SVNMonitor.Entities.Source source, long revision)
        {
            RecommendWizard wizard = new RecommendWizard {
                Source = source,
                Revision = revision,
                CommitMessage = ApplicationSettingsManager.Settings.RecommendationMessage.Replace("${REVISION}", revision.ToString())
            };
            return wizard.ShowDialog();
        }

        private void RefreshButtons()
        {
            this.btnUpdate.Enabled = true;
            this.btnRecommend.Enabled = !this.recommended && this.updated;
            this.btnCommit.Enabled = this.recommended;
            this.linkUndo.Visible = this.recommended;
            this.txtCommitMessage.Visible = this.recommended;
        }

        private void SVNCommit()
        {
            string error = string.Empty;
            Exception recommendException = null;
            try
            {
                SharpSVNClient.CommitRecommend(this.Source, this.Revision, this.CommitMessage);
                this.committed = true;
                this.RefreshButtons();
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
                ErrorHandler.Append(Strings.ErrorTryingToRecommend_FORMAT.FormatWith(new object[] { this.Revision, this.Source.Name }), this.Source, recommendException);
                error = Strings.ErrorCommitFailedCheckLog_FORMAT.FormatWith(new object[] { error });
                MessageBox.Show(this, error, Strings.SVNMonitorCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void SVNUpdate()
        {
            try
            {
                this.Source.SVNUpdate();
                try
                {
                    this.Source.RefreshRecommended();
                    this.lblUpdateAgain.Show();
                    this.updated = true;
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
            this.RefreshButtons();
            this.btnRecommend.Focus();
        }

        private void UndoRecommend()
        {
            this.Source.UndoRecommend(this.Revision);
            this.recommended = false;
            this.RefreshButtons();
        }

        private void UpdateHeader()
        {
            this.Text = string.Format("Recommend revision {0} of '{1}'", this.revision, this.source.Name);
            string revisionString = (this.Revision == 0L) ? "{0}" : this.Revision.ToString();
            string sourceName = (this.Source == null) ? "{1}" : this.Source.Name;
            this.lblHeader.Text = string.Format(this.lblHeader.Text, revisionString, sourceName);
        }

        [Browsable(false)]
        public string CommitMessage
        {
            get
            {
                return this.txtCommitMessage.Text;
            }
            set
            {
                this.txtCommitMessage.Text = value;
            }
        }

        [Browsable(false)]
        public long Revision
        {
            [DebuggerNonUserCode]
            get
            {
                return this.revision;
            }
            private set
            {
                this.revision = value;
                this.UpdateHeader();
            }
        }

        [Browsable(false)]
        public SVNMonitor.Entities.Source Source
        {
            [DebuggerNonUserCode]
            get
            {
                return this.source;
            }
            private set
            {
                this.source = value;
                this.UpdateHeader();
            }
        }
    }
}

