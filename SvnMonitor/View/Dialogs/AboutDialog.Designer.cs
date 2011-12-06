namespace SVNMonitor.View.Dialogs
{
	public partial class AboutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
			lblNameAndVersion = new System.Windows.Forms.Label();
			lblAuthor = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			groupBox1.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(lblNameAndVersion, "lblNameAndVersion");
			lblNameAndVersion.BackColor = System.Drawing.Color.Transparent;
			lblNameAndVersion.ForeColor = System.Drawing.Color.Black;
			lblNameAndVersion.Name = "lblNameAndVersion";
			resources.ApplyResources(lblAuthor, "lblAuthor");
			lblAuthor.BackColor = System.Drawing.Color.Transparent;
			lblAuthor.ForeColor = System.Drawing.Color.Black;
			lblAuthor.Name = "lblAuthor";
			resources.ApplyResources(pictureBox1, "pictureBox1");
			pictureBox1.Name = "pictureBox1";
			pictureBox1.TabStop = false;
			groupBox1.Controls.Add(pictureBox1);
			groupBox1.Controls.Add(lblNameAndVersion);
			groupBox1.Controls.Add(lblAuthor);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			BackColor = System.Drawing.SystemColors.Control;
			base.Controls.Add(groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.Name = "AboutDialog";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.Click += AboutDialog_Click;
			base.KeyDown += AboutDialog_KeyDown;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.Label lblNameAndVersion;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}