using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Settings;

namespace SVNMonitor.View.Dialogs
{
	public partial class UpdateHeadPromptDialog : BasePromptDialog
	{
		private Button btnNo;
		private Button btnYes;
		private CheckBox checkDontShowAgain;
		private IContainer components;
		private Label lblPrompt;
		private Panel panel1;
		private PictureBox pictureBox1;

		public UpdateHeadPromptDialog()
		{
			InitializeComponent();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(UpdateHeadPromptDialog));
			pictureBox1 = new PictureBox();
			lblPrompt = new Label();
			btnNo = new Button();
			btnYes = new Button();
			checkDontShowAgain = new CheckBox();
			panel1 = new Panel();
			((ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			base.SuspendLayout();
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(12, 12);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(0x22, 0x22);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			lblPrompt.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			lblPrompt.Location = new Point(0x34, 12);
			lblPrompt.Name = "lblPrompt";
			lblPrompt.Size = new Size(280, 0x34);
			lblPrompt.TabIndex = 1;
			lblPrompt.Text = "Are you sure you want to update to the HEAD revision?";
			btnNo.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnNo.DialogResult = DialogResult.No;
			btnNo.Location = new Point(250, 0x63);
			btnNo.Name = "btnNo";
			btnNo.Size = new Size(0x52, 0x17);
			btnNo.TabIndex = 1;
			btnNo.Text = "&Cancel";
			btnYes.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnYes.DialogResult = DialogResult.Yes;
			btnYes.Location = new Point(0xa2, 0x63);
			btnYes.Name = "btnYes";
			btnYes.Size = new Size(0x52, 0x17);
			btnYes.TabIndex = 0;
			btnYes.Text = "&Update";
			checkDontShowAgain.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
			checkDontShowAgain.AutoSize = true;
			checkDontShowAgain.Location = new Point(12, 0x4c);
			checkDontShowAgain.Name = "checkDontShowAgain";
			checkDontShowAgain.Size = new Size(0xb1, 0x11);
			checkDontShowAgain.TabIndex = 2;
			checkDontShowAgain.Text = "&Don't ask again (always update)";
			checkDontShowAgain.CheckedChanged += checkDontShowAgain_CheckedChanged;
			panel1.Controls.Add(pictureBox1);
			panel1.Controls.Add(checkDontShowAgain);
			panel1.Controls.Add(lblPrompt);
			panel1.Controls.Add(btnYes);
			panel1.Controls.Add(btnNo);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(0x158, 0x86);
			panel1.TabIndex = 3;
			base.AcceptButton = btnYes;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnNo;
			base.ClientSize = new Size(0x158, 0x86);
			base.Controls.Add(panel1);
			base.Name = "UpdateHeadPromptDialog";
			base.StartPosition = FormStartPosition.CenterScreen;
			Text = "Update HEAD Revision";
			((ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			ApplicationSettingsManager.Settings.PromptUpdateHeadRevision = !checkDontShowAgain.Checked;
			ApplicationSettingsManager.SaveSettings();
		}

		public static DialogResult Prompt()
		{
			return Prompt(null);
		}

		public static DialogResult Prompt(IWin32Window parent)
		{
			if (!ApplicationSettingsManager.Settings.PromptUpdateHeadRevision)
			{
				return DialogResult.Yes;
			}
			UpdateHeadPromptDialog dialog = new UpdateHeadPromptDialog();
			return dialog.ShowDialog(parent);
		}
	}
}