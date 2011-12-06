namespace SVNMonitor.View.Dialogs
{
	public partial class RecommendWizard
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecommendWizard));
			panelUpdate = new System.Windows.Forms.Panel();
			groupUpdate = new System.Windows.Forms.GroupBox();
			btnUpdate = new System.Windows.Forms.Button();
			lblUpdateAgain = new System.Windows.Forms.Label();
			lblUpdate = new System.Windows.Forms.Label();
			panelRecommend = new System.Windows.Forms.Panel();
			groupRecommend = new System.Windows.Forms.GroupBox();
			btnRecommend = new System.Windows.Forms.Button();
			linkUndo = new System.Windows.Forms.LinkLabel();
			lblRecommend = new System.Windows.Forms.Label();
			panelCommit = new System.Windows.Forms.Panel();
			groupCommit = new System.Windows.Forms.GroupBox();
			txtCommitMessage = new System.Windows.Forms.TextBox();
			btnCommit = new System.Windows.Forms.Button();
			lblCommit = new System.Windows.Forms.Label();
			panelHeader = new System.Windows.Forms.Panel();
			lblHeader = new System.Windows.Forms.Label();
			btnClose = new System.Windows.Forms.Button();
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
			btnUpdate.Image = SVNMonitor.Resources.Images.svn_update_32;
			btnUpdate.Name = "btnUpdate";
			btnUpdate.Click += btnUpdate_Click;
			lblUpdateAgain.BackColor = System.Drawing.Color.Transparent;
			lblUpdateAgain.ForeColor = System.Drawing.Color.MidnightBlue;
			resources.ApplyResources(lblUpdateAgain, "lblUpdateAgain");
			lblUpdateAgain.Name = "lblUpdateAgain";
			lblUpdate.BackColor = System.Drawing.Color.Transparent;
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
			btnRecommend.Image = SVNMonitor.Resources.Images.star_yellow_32;
			btnRecommend.Name = "btnRecommend";
			btnRecommend.Click += btnRecommend_Click;
			resources.ApplyResources(linkUndo, "linkUndo");
			linkUndo.BackColor = System.Drawing.Color.Transparent;
			linkUndo.ForeColor = System.Drawing.Color.MidnightBlue;
			linkUndo.LinkColor = System.Drawing.Color.MidnightBlue;
			linkUndo.Name = "linkUndo";
			linkUndo.TabStop = true;
			linkUndo.LinkClicked += linkUndo_LinkClicked;
			lblRecommend.BackColor = System.Drawing.Color.Transparent;
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
			btnCommit.Image = SVNMonitor.Resources.Images.svn_commit_32;
			btnCommit.Name = "btnCommit";
			btnCommit.Click += btnCommit_Click;
			lblCommit.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(lblCommit, "lblCommit");
			lblCommit.Name = "lblCommit";
			resources.ApplyResources(panelHeader, "panelHeader");
			panelHeader.Controls.Add(lblHeader);
			panelHeader.Name = "panelHeader";
			lblHeader.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(lblHeader, "lblHeader");
			lblHeader.Name = "lblHeader";
			resources.ApplyResources(btnClose, "btnClose");
			btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClose.Name = "btnClose";
			btnClose.Click += btnClose_Click;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnClose;
			base.Controls.Add(btnClose);
			base.Controls.Add(panelCommit);
			base.Controls.Add(panelRecommend);
			base.Controls.Add(panelUpdate);
			base.Controls.Add(panelHeader);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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

		#endregion
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnRecommend;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.GroupBox groupCommit;
		private System.Windows.Forms.GroupBox groupRecommend;
		private System.Windows.Forms.GroupBox groupUpdate;
		private System.Windows.Forms.Label lblCommit;
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.Label lblRecommend;
		private System.Windows.Forms.Label lblUpdate;
		private System.Windows.Forms.Label lblUpdateAgain;
		private System.Windows.Forms.LinkLabel linkUndo;
		private System.Windows.Forms.Panel panelCommit;
		private System.Windows.Forms.Panel panelHeader;
		private System.Windows.Forms.Panel panelRecommend;
		private System.Windows.Forms.Panel panelUpdate;
		private System.Windows.Forms.TextBox txtCommitMessage;
	}
}