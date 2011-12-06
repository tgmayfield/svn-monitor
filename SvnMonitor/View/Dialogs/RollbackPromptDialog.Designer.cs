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
			this.checkDontShowAgain = new System.Windows.Forms.CheckBox();
			this.btnYes = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.lblPrompt = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkDontShowAgain
			// 
			this.checkDontShowAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkDontShowAgain.AutoSize = true;
			this.checkDontShowAgain.Location = new System.Drawing.Point(12, 77);
			this.checkDontShowAgain.Name = "checkDontShowAgain";
			this.checkDontShowAgain.Size = new System.Drawing.Size(181, 17);
			this.checkDontShowAgain.TabIndex = 7;
			this.checkDontShowAgain.Text = "&Don\'t ask again (always rollback)";
			// 
			// btnYes
			// 
			this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnYes.Location = new System.Drawing.Point(162, 99);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(82, 23);
			this.btnYes.TabIndex = 3;
			this.btnYes.Text = "&Rollback";
			// 
			// btnNo
			// 
			this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.btnNo.Location = new System.Drawing.Point(250, 99);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(82, 23);
			this.btnNo.TabIndex = 6;
			this.btnNo.Text = "&Cancel";
			// 
			// lblPrompt
			// 
			this.lblPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPrompt.Location = new System.Drawing.Point(52, 12);
			this.lblPrompt.Name = "lblPrompt";
			this.lblPrompt.Size = new System.Drawing.Size(280, 52);
			this.lblPrompt.TabIndex = 5;
			this.lblPrompt.Text = "Are you sure you want to rollback to revision {0}?";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(34, 34);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.checkDontShowAgain);
			this.panel1.Controls.Add(this.lblPrompt);
			this.panel1.Controls.Add(this.btnYes);
			this.panel1.Controls.Add(this.btnNo);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(344, 134);
			this.panel1.TabIndex = 8;
			// 
			// RollbackPromptDialog
			// 
			this.AcceptButton = this.btnYes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnNo;
			this.ClientSize = new System.Drawing.Size(344, 134);
			this.Controls.Add(this.panel1);
			this.Name = "RollbackPromptDialog";
			this.Text = "Rollback";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

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