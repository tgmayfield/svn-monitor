namespace SVNMonitor.View.Dialogs
{
	public partial class RollbackPromptDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RollbackPromptDialog));
			checkDontShowAgain = new System.Windows.Forms.CheckBox();
			btnYes = new System.Windows.Forms.Button();
			btnNo = new System.Windows.Forms.Button();
			lblPrompt = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			base.SuspendLayout();
			checkDontShowAgain.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
			checkDontShowAgain.AutoSize = true;
			checkDontShowAgain.Location = new System.Drawing.Point(0x10, 0x5e);
			checkDontShowAgain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			checkDontShowAgain.Name = "checkDontShowAgain";
			checkDontShowAgain.Size = new System.Drawing.Size(0xea, 0x15);
			checkDontShowAgain.TabIndex = 7;
			checkDontShowAgain.Text = "&Don't ask again (always rollback)";
			checkDontShowAgain.CheckedChanged += checkDontShowAgain_CheckedChanged;
			btnYes.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
			btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			btnYes.Location = new System.Drawing.Point(0xd8, 0x7a);
			btnYes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnYes.Name = "btnYes";
			btnYes.Size = new System.Drawing.Size(0x6d, 0x1c);
			btnYes.TabIndex = 3;
			btnYes.Text = "&Rollback";
			btnNo.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
			btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			btnNo.Location = new System.Drawing.Point(0x14d, 0x7a);
			btnNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnNo.Name = "btnNo";
			btnNo.Size = new System.Drawing.Size(0x6d, 0x1c);
			btnNo.TabIndex = 6;
			btnNo.Text = "&Cancel";
			lblPrompt.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
			lblPrompt.Location = new System.Drawing.Point(0x45, 15);
			lblPrompt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPrompt.Name = "lblPrompt";
			lblPrompt.Size = new System.Drawing.Size(0x175, 0x40);
			lblPrompt.TabIndex = 5;
			lblPrompt.Text = "Are you sure you want to rollback to revision {0}?";
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(0x10, 15);
			pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(0x2d, 0x2a);
			pictureBox1.TabIndex = 4;
			pictureBox1.TabStop = false;
			panel1.Controls.Add(pictureBox1);
			panel1.Controls.Add(checkDontShowAgain);
			panel1.Controls.Add(lblPrompt);
			panel1.Controls.Add(btnYes);
			panel1.Controls.Add(btnNo);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(0x1cb, 0xa5);
			panel1.TabIndex = 8;
			base.AcceptButton = btnYes;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnNo;
			base.ClientSize = new System.Drawing.Size(0x1cb, 0xa5);
			base.Controls.Add(panel1);
			base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			base.Name = "RollbackPromptDialog";
			Text = "Rollback";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.CheckBox checkDontShowAgain;
		private System.Windows.Forms.Label lblPrompt;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}