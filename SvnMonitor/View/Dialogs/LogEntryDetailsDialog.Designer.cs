namespace SVNMonitor.View.Dialogs
{
	public partial class LogEntryDetailsDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogEntryDetailsDialog));
			txtLogMessage = new System.Windows.Forms.TextBox();
			btnOK = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			txtAuthor = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			txtDateTime = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			pathsPanel1 = new SVNMonitor.View.Panels.PathsPanel();
			btnUpdate = new System.Windows.Forms.Button();
			btnRollback = new System.Windows.Forms.Button();
			btnLog = new System.Windows.Forms.Button();
			btnDiff = new System.Windows.Forms.Button();
			btnPrevious = new System.Windows.Forms.Button();
			btnNext = new System.Windows.Forms.Button();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			pathsPanel1.BeginInit();
			base.SuspendLayout();
			txtLogMessage.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
			txtLogMessage.BackColor = System.Drawing.Color.White;
			txtLogMessage.Location = new System.Drawing.Point(0, 20);
			txtLogMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			txtLogMessage.Multiline = true;
			txtLogMessage.Name = "txtLogMessage";
			txtLogMessage.ReadOnly = true;
			txtLogMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			txtLogMessage.Size = new System.Drawing.Size(0x289, 0x9f);
			txtLogMessage.TabIndex = 1;
			btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
			btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOK.Location = new System.Drawing.Point(0x237, 610);
			btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(100, 0x1c);
			btnOK.TabIndex = 11;
			btnOK.Text = "&Close";
			btnOK.Click += btnOK_Click;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(0x10, 0x12);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(0x36, 0x11);
			label1.TabIndex = 0;
			label1.Text = "Author:";
			txtAuthor.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
			txtAuthor.BackColor = System.Drawing.Color.White;
			txtAuthor.Location = new System.Drawing.Point(0x10, 0x26);
			txtAuthor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			txtAuthor.Name = "txtAuthor";
			txtAuthor.ReadOnly = true;
			txtAuthor.Size = new System.Drawing.Size(0x289, 0x16);
			txtAuthor.TabIndex = 1;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(0x10, 0x42);
			label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(100, 0x11);
			label2.TabIndex = 2;
			label2.Text = "Date and time:";
			txtDateTime.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
			txtDateTime.BackColor = System.Drawing.Color.White;
			txtDateTime.Location = new System.Drawing.Point(0x10, 0x56);
			txtDateTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			txtDateTime.Name = "txtDateTime";
			txtDateTime.ReadOnly = true;
			txtDateTime.Size = new System.Drawing.Size(0x289, 0x16);
			txtDateTime.TabIndex = 3;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(0, 0);
			label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(0x45, 0x11);
			label3.TabIndex = 0;
			label3.Text = "Message:";
			splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
			splitContainer1.Location = new System.Drawing.Point(0x10, 0x76);
			splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			splitContainer1.Panel1.Controls.Add(label3);
			splitContainer1.Panel1.Controls.Add(txtLogMessage);
			splitContainer1.Panel2.Controls.Add(pathsPanel1);
			splitContainer1.Size = new System.Drawing.Size(0x28b, 0x1e5);
			splitContainer1.SplitterDistance = 0xb8;
			splitContainer1.SplitterWidth = 7;
			splitContainer1.TabIndex = 4;
			pathsPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pathsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			pathsPanel1.Location = new System.Drawing.Point(0, 0);
			pathsPanel1.LogEntriesView = null;
			pathsPanel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			pathsPanel1.Name = "pathsPanel1";
			pathsPanel1.SearchTextBox = null;
			pathsPanel1.Size = new System.Drawing.Size(0x28b, 0x126);
			pathsPanel1.TabIndex = 0;
			btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
			btnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnUpdate.Image = (System.Drawing.Image)resources.GetObject("btnUpdate.Image");
			btnUpdate.Location = new System.Drawing.Point(0x10, 610);
			btnUpdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnUpdate.Name = "btnUpdate";
			btnUpdate.Size = new System.Drawing.Size(100, 0x1c);
			btnUpdate.TabIndex = 5;
			btnUpdate.Text = "&Update";
			btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnUpdate.Click += btnUpdate_Click;
			btnRollback.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
			btnRollback.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnRollback.Image = (System.Drawing.Image)resources.GetObject("btnRollback.Image");
			btnRollback.Location = new System.Drawing.Point(0x7c, 610);
			btnRollback.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnRollback.Name = "btnRollback";
			btnRollback.Size = new System.Drawing.Size(100, 0x1c);
			btnRollback.TabIndex = 6;
			btnRollback.Text = "&Rollback";
			btnRollback.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnRollback.Click += btnRollback_Click;
			btnLog.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
			btnLog.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnLog.Image = (System.Drawing.Image)resources.GetObject("btnLog.Image");
			btnLog.Location = new System.Drawing.Point(0xe8, 610);
			btnLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnLog.Name = "btnLog";
			btnLog.Size = new System.Drawing.Size(100, 0x1c);
			btnLog.TabIndex = 7;
			btnLog.Text = "&Log";
			btnLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnLog.Click += btnLog_Click;
			btnDiff.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
			btnDiff.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnDiff.Image = (System.Drawing.Image)resources.GetObject("btnDiff.Image");
			btnDiff.Location = new System.Drawing.Point(340, 610);
			btnDiff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnDiff.Name = "btnDiff";
			btnDiff.Size = new System.Drawing.Size(100, 0x1c);
			btnDiff.TabIndex = 8;
			btnDiff.Text = "&Diff";
			btnDiff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			btnDiff.Click += btnDiff_Click;
			btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
			btnPrevious.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnPrevious.Image = (System.Drawing.Image)resources.GetObject("btnPrevious.Image");
			btnPrevious.Location = new System.Drawing.Point(0x1e9, 610);
			btnPrevious.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnPrevious.Name = "btnPrevious";
			btnPrevious.Size = new System.Drawing.Size(0x1f, 0x1c);
			btnPrevious.TabIndex = 9;
			btnPrevious.Click += btnPrevious_Click;
			btnNext.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
			btnNext.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnNext.Image = (System.Drawing.Image)resources.GetObject("btnNext.Image");
			btnNext.Location = new System.Drawing.Point(0x210, 610);
			btnNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnNext.Name = "btnNext";
			btnNext.Size = new System.Drawing.Size(0x1f, 0x1c);
			btnNext.TabIndex = 10;
			btnNext.Click += btnNext_Click;
			base.AcceptButton = btnOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnOK;
			base.ClientSize = new System.Drawing.Size(0x2ab, 0x28e);
			base.Controls.Add(btnNext);
			base.Controls.Add(btnPrevious);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(txtDateTime);
			base.Controls.Add(label2);
			base.Controls.Add(txtAuthor);
			base.Controls.Add(label1);
			base.Controls.Add(btnDiff);
			base.Controls.Add(btnLog);
			base.Controls.Add(btnRollback);
			base.Controls.Add(btnUpdate);
			base.Controls.Add(btnOK);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			base.MaximizeBox = true;
			base.MinimizeBox = true;
			MinimumSize = new System.Drawing.Size(0x26d, 0x1fb);
			base.Name = "LogEntryDetailsDialog";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "[Name [Revision]]";
			base.KeyDown += LogEntryDetailsDialog_KeyDown;
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.ResumeLayout(false);
			pathsPanel1.EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		#endregion
		private System.Windows.Forms.Button btnDiff;
		private System.Windows.Forms.Button btnLog;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.Button btnRollback;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private SVNMonitor.View.Panels.PathsPanel pathsPanel1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox txtAuthor;
		private System.Windows.Forms.TextBox txtDateTime;
		private System.Windows.Forms.TextBox txtLogMessage;

	}
}