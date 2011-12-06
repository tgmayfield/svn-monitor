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
			this.txtLogMessage = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtAuthor = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtDateTime = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.pathsPanel1 = new SVNMonitor.View.Panels.PathsPanel();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnRollback = new System.Windows.Forms.Button();
			this.btnLog = new System.Windows.Forms.Button();
			this.btnDiff = new System.Windows.Forms.Button();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pathsPanel1)).BeginInit();
			this.SuspendLayout();
			// 
			// txtLogMessage
			// 
			this.txtLogMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLogMessage.BackColor = System.Drawing.Color.White;
			this.txtLogMessage.Location = new System.Drawing.Point(0, 16);
			this.txtLogMessage.Multiline = true;
			this.txtLogMessage.Name = "txtLogMessage";
			this.txtLogMessage.ReadOnly = true;
			this.txtLogMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLogMessage.Size = new System.Drawing.Size(488, 129);
			this.txtLogMessage.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(425, 496);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "&Close";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Author:";
			// 
			// txtAuthor
			// 
			this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAuthor.BackColor = System.Drawing.Color.White;
			this.txtAuthor.Location = new System.Drawing.Point(12, 31);
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.ReadOnly = true;
			this.txtAuthor.Size = new System.Drawing.Size(488, 20);
			this.txtAuthor.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Date and time:";
			// 
			// txtDateTime
			// 
			this.txtDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDateTime.BackColor = System.Drawing.Color.White;
			this.txtDateTime.Location = new System.Drawing.Point(12, 70);
			this.txtDateTime.Name = "txtDateTime";
			this.txtDateTime.ReadOnly = true;
			this.txtDateTime.Size = new System.Drawing.Size(488, 20);
			this.txtDateTime.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Message:";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 96);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.txtLogMessage);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pathsPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(488, 394);
			this.splitContainer1.SplitterDistance = 149;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 4;
			// 
			// pathsPanel1
			// 
			this.pathsPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pathsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pathsPanel1.Location = new System.Drawing.Point(0, 0);
			this.pathsPanel1.LogEntriesView = null;
			this.pathsPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pathsPanel1.Name = "pathsPanel1";
			this.pathsPanel1.SearchTextBox = null;
			this.pathsPanel1.Size = new System.Drawing.Size(488, 239);
			this.pathsPanel1.TabIndex = 0;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
			this.btnUpdate.Location = new System.Drawing.Point(12, 496);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(75, 23);
			this.btnUpdate.TabIndex = 5;
			this.btnUpdate.Text = "&Update";
			this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			// 
			// btnRollback
			// 
			this.btnRollback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRollback.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnRollback.Image = ((System.Drawing.Image)(resources.GetObject("btnRollback.Image")));
			this.btnRollback.Location = new System.Drawing.Point(93, 496);
			this.btnRollback.Name = "btnRollback";
			this.btnRollback.Size = new System.Drawing.Size(75, 23);
			this.btnRollback.TabIndex = 6;
			this.btnRollback.Text = "&Rollback";
			this.btnRollback.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			// 
			// btnLog
			// 
			this.btnLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLog.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnLog.Image = ((System.Drawing.Image)(resources.GetObject("btnLog.Image")));
			this.btnLog.Location = new System.Drawing.Point(174, 496);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(75, 23);
			this.btnLog.TabIndex = 7;
			this.btnLog.Text = "&Log";
			this.btnLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			// 
			// btnDiff
			// 
			this.btnDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDiff.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnDiff.Image = ((System.Drawing.Image)(resources.GetObject("btnDiff.Image")));
			this.btnDiff.Location = new System.Drawing.Point(255, 496);
			this.btnDiff.Name = "btnDiff";
			this.btnDiff.Size = new System.Drawing.Size(75, 23);
			this.btnDiff.TabIndex = 8;
			this.btnDiff.Text = "&Diff";
			this.btnDiff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
			this.btnPrevious.Location = new System.Drawing.Point(367, 496);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(23, 23);
			this.btnPrevious.TabIndex = 9;
			// 
			// btnNext
			// 
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
			this.btnNext.Location = new System.Drawing.Point(396, 496);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(23, 23);
			this.btnNext.TabIndex = 10;
			// 
			// LogEntryDetailsDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(512, 531);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnPrevious);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.txtDateTime);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtAuthor);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnDiff);
			this.Controls.Add(this.btnLog);
			this.Controls.Add(this.btnRollback);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.btnOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.MinimumSize = new System.Drawing.Size(470, 419);
			this.Name = "LogEntryDetailsDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "[Name [Revision]]";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pathsPanel1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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