namespace SVNMonitor.View.Dialogs
{
	public partial class UpdateHeadPromptDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateHeadPromptDialog));
			pictureBox1 = new System.Windows.Forms.PictureBox();
			lblPrompt = new System.Windows.Forms.Label();
			btnNo = new System.Windows.Forms.Button();
			btnYes = new System.Windows.Forms.Button();
			checkDontShowAgain = new System.Windows.Forms.CheckBox();
			panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			base.SuspendLayout();
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(12, 12);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(0x22, 0x22);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			lblPrompt.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
			lblPrompt.Location = new System.Drawing.Point(0x34, 12);
			lblPrompt.Name = "lblPrompt";
			lblPrompt.Size = new System.Drawing.Size(280, 0x34);
			lblPrompt.TabIndex = 1;
			lblPrompt.Text = "Are you sure you want to update to the HEAD revision?";
			btnNo.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
			btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			btnNo.Location = new System.Drawing.Point(250, 0x63);
			btnNo.Name = "btnNo";
			btnNo.Size = new System.Drawing.Size(0x52, 0x17);
			btnNo.TabIndex = 1;
			btnNo.Text = "&Cancel";
			btnYes.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
			btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			btnYes.Location = new System.Drawing.Point(0xa2, 0x63);
			btnYes.Name = "btnYes";
			btnYes.Size = new System.Drawing.Size(0x52, 0x17);
			btnYes.TabIndex = 0;
			btnYes.Text = "&Update";
			checkDontShowAgain.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
			checkDontShowAgain.AutoSize = true;
			checkDontShowAgain.Location = new System.Drawing.Point(12, 0x4c);
			checkDontShowAgain.Name = "checkDontShowAgain";
			checkDontShowAgain.Size = new System.Drawing.Size(0xb1, 0x11);
			checkDontShowAgain.TabIndex = 2;
			checkDontShowAgain.Text = "&Don't ask again (always update)";
			checkDontShowAgain.CheckedChanged += checkDontShowAgain_CheckedChanged;
			panel1.Controls.Add(pictureBox1);
			panel1.Controls.Add(checkDontShowAgain);
			panel1.Controls.Add(lblPrompt);
			panel1.Controls.Add(btnYes);
			panel1.Controls.Add(btnNo);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(0x158, 0x86);
			panel1.TabIndex = 3;
			base.AcceptButton = btnYes;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnNo;
			base.ClientSize = new System.Drawing.Size(0x158, 0x86);
			base.Controls.Add(panel1);
			base.Name = "UpdateHeadPromptDialog";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Update HEAD Revision";
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