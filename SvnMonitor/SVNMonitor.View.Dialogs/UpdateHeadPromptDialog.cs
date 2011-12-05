using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Drawing;
using SVNMonitor.Settings;

namespace SVNMonitor.View.Dialogs
{
public class UpdateHeadPromptDialog : BasePromptDialog
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
		this.InitializeComponent();
	}

	private void checkDontShowAgain_CheckedChanged(object sender, EventArgs e)
	{
		this.btnNo.Enabled = !this.checkDontShowAgain.Checked;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && this.components != null)
		{
			this.components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		ComponentResourceManager resources = new ComponentResourceManager(typeof(UpdateHeadPromptDialog));
		this.pictureBox1 = new PictureBox();
		this.lblPrompt = new Label();
		this.btnNo = new Button();
		this.btnYes = new Button();
		this.checkDontShowAgain = new CheckBox();
		this.panel1 = new Panel();
		this.pictureBox1.BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new Point(12, 12);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new Size(34, 34);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.lblPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		this.lblPrompt.Location = new Point(52, 12);
		this.lblPrompt.Name = "lblPrompt";
		this.lblPrompt.Size = new Size(280, 52);
		this.lblPrompt.TabIndex = 1;
		this.lblPrompt.Text = "Are you sure you want to update to the HEAD revision?";
		this.btnNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		this.btnNo.DialogResult = DialogResult.No;
		this.btnNo.Location = new Point(250, 99);
		this.btnNo.Name = "btnNo";
		this.btnNo.Size = new Size(82, 23);
		this.btnNo.TabIndex = 1;
		this.btnNo.Text = "&Cancel";
		this.btnYes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		this.btnYes.DialogResult = DialogResult.Yes;
		this.btnYes.Location = new Point(162, 99);
		this.btnYes.Name = "btnYes";
		this.btnYes.Size = new Size(82, 23);
		this.btnYes.TabIndex = 0;
		this.btnYes.Text = "&Update";
		this.checkDontShowAgain.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		this.checkDontShowAgain.AutoSize = true;
		this.checkDontShowAgain.Location = new Point(12, 76);
		this.checkDontShowAgain.Name = "checkDontShowAgain";
		this.checkDontShowAgain.Size = new Size(177, 17);
		this.checkDontShowAgain.TabIndex = 2;
		this.checkDontShowAgain.Text = "&Don't ask again (always update)";
		this.checkDontShowAgain.CheckedChanged += new EventHandler(this.checkDontShowAgain_CheckedChanged);
		this.panel1.Controls.Add(this.pictureBox1);
		this.panel1.Controls.Add(this.checkDontShowAgain);
		this.panel1.Controls.Add(this.lblPrompt);
		this.panel1.Controls.Add(this.btnYes);
		this.panel1.Controls.Add(this.btnNo);
		this.panel1.Dock = DockStyle.Fill;
		this.panel1.Location = new Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new Size(344, 134);
		this.panel1.TabIndex = 3;
		base.AcceptButton = this.btnYes;
		base.AutoScaleDimensions = new SizeF(6, 13);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.CancelButton = this.btnNo;
		base.ClientSize = new Size(344, 134);
		base.Controls.Add(this.panel1);
		base.Name = "UpdateHeadPromptDialog";
		base.StartPosition = FormStartPosition.CenterScreen;
		base.Text = "Update HEAD Revision";
		this.pictureBox1.EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		ApplicationSettingsManager.Settings.PromptUpdateHeadRevision = !this.checkDontShowAgain.Checked;
		ApplicationSettingsManager.SaveSettings();
	}

	public static DialogResult Prompt()
	{
		return UpdateHeadPromptDialog.Prompt(null);
	}

	public static DialogResult Prompt(IWin32Window parent)
	{
		if (!ApplicationSettingsManager.Settings.PromptUpdateHeadRevision)
		{
			return 6;
		}
		UpdateHeadPromptDialog dialog = new UpdateHeadPromptDialog();
		DialogResult result = dialog.ShowDialog(parent);
		return result;
	}
}
}