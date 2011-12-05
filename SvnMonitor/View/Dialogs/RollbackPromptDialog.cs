using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Settings;

namespace SVNMonitor.View.Dialogs
{
	public partial class RollbackPromptDialog : BasePromptDialog
	{
		private Button btnNo;
		private Button btnYes;
		private CheckBox checkDontShowAgain;
		private IContainer components;
		private Label lblPrompt;
		private Panel panel1;
		private PictureBox pictureBox1;

		public RollbackPromptDialog()
		{
			InitializeComponent();
		}

		public RollbackPromptDialog(long revision)
			: this()
		{
			lblPrompt.Text = string.Format(lblPrompt.Text, revision);
		}

		private void checkDontShowAgain_CheckedChanged(object sender, EventArgs e)
		{
			btnNo.Enabled = !checkDontShowAgain.Checked;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(RollbackPromptDialog));
			checkDontShowAgain = new CheckBox();
			btnYes = new Button();
			btnNo = new Button();
			lblPrompt = new Label();
			pictureBox1 = new PictureBox();
			panel1 = new Panel();
			((ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			base.SuspendLayout();
			checkDontShowAgain.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
			checkDontShowAgain.AutoSize = true;
			checkDontShowAgain.Location = new Point(0x10, 0x5e);
			checkDontShowAgain.Margin = new Padding(4, 4, 4, 4);
			checkDontShowAgain.Name = "checkDontShowAgain";
			checkDontShowAgain.Size = new Size(0xea, 0x15);
			checkDontShowAgain.TabIndex = 7;
			checkDontShowAgain.Text = "&Don't ask again (always rollback)";
			checkDontShowAgain.CheckedChanged += checkDontShowAgain_CheckedChanged;
			btnYes.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnYes.DialogResult = DialogResult.Yes;
			btnYes.Location = new Point(0xd8, 0x7a);
			btnYes.Margin = new Padding(4, 4, 4, 4);
			btnYes.Name = "btnYes";
			btnYes.Size = new Size(0x6d, 0x1c);
			btnYes.TabIndex = 3;
			btnYes.Text = "&Rollback";
			btnNo.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnNo.DialogResult = DialogResult.No;
			btnNo.Location = new Point(0x14d, 0x7a);
			btnNo.Margin = new Padding(4, 4, 4, 4);
			btnNo.Name = "btnNo";
			btnNo.Size = new Size(0x6d, 0x1c);
			btnNo.TabIndex = 6;
			btnNo.Text = "&Cancel";
			lblPrompt.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			lblPrompt.Location = new Point(0x45, 15);
			lblPrompt.Margin = new Padding(4, 0, 4, 0);
			lblPrompt.Name = "lblPrompt";
			lblPrompt.Size = new Size(0x175, 0x40);
			lblPrompt.TabIndex = 5;
			lblPrompt.Text = "Are you sure you want to rollback to revision {0}?";
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(0x10, 15);
			pictureBox1.Margin = new Padding(4, 4, 4, 4);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(0x2d, 0x2a);
			pictureBox1.TabIndex = 4;
			pictureBox1.TabStop = false;
			panel1.Controls.Add(pictureBox1);
			panel1.Controls.Add(checkDontShowAgain);
			panel1.Controls.Add(lblPrompt);
			panel1.Controls.Add(btnYes);
			panel1.Controls.Add(btnNo);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Margin = new Padding(4, 4, 4, 4);
			panel1.Name = "panel1";
			panel1.Size = new Size(0x1cb, 0xa5);
			panel1.TabIndex = 8;
			base.AcceptButton = btnYes;
			base.AutoScaleDimensions = new SizeF(8f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnNo;
			base.ClientSize = new Size(0x1cb, 0xa5);
			base.Controls.Add(panel1);
			base.Margin = new Padding(4, 4, 4, 4);
			base.Name = "RollbackPromptDialog";
			Text = "Rollback";
			((ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			ApplicationSettingsManager.Settings.PromptRollbackOldRevision = !checkDontShowAgain.Checked;
			ApplicationSettingsManager.SaveSettings();
		}

		public static DialogResult Prompt(long revision)
		{
			return Prompt(null, revision);
		}

		public static DialogResult Prompt(IWin32Window parent, long revision)
		{
			if (!ApplicationSettingsManager.Settings.PromptRollbackOldRevision)
			{
				return DialogResult.Yes;
			}
			RollbackPromptDialog dialog = new RollbackPromptDialog(revision);
			return dialog.ShowDialog(parent);
		}
	}
}