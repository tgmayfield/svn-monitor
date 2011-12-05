namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

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
            if (disposing && (this.components != null))
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
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.pictureBox1.Image = (Image) resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x22, 0x22);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.lblPrompt.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblPrompt.Location = new Point(0x34, 12);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new Size(280, 0x34);
            this.lblPrompt.TabIndex = 1;
            this.lblPrompt.Text = "Are you sure you want to update to the HEAD revision?";
            this.btnNo.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnNo.DialogResult = DialogResult.No;
            this.btnNo.Location = new Point(250, 0x63);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new Size(0x52, 0x17);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "&Cancel";
            this.btnYes.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnYes.DialogResult = DialogResult.Yes;
            this.btnYes.Location = new Point(0xa2, 0x63);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new Size(0x52, 0x17);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "&Update";
            this.checkDontShowAgain.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.checkDontShowAgain.AutoSize = true;
            this.checkDontShowAgain.Location = new Point(12, 0x4c);
            this.checkDontShowAgain.Name = "checkDontShowAgain";
            this.checkDontShowAgain.Size = new Size(0xb1, 0x11);
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
            this.panel1.Size = new Size(0x158, 0x86);
            this.panel1.TabIndex = 3;
            base.AcceptButton = this.btnYes;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnNo;
            base.ClientSize = new Size(0x158, 0x86);
            base.Controls.Add(this.panel1);
            base.Name = "UpdateHeadPromptDialog";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Update HEAD Revision";
            ((ISupportInitialize) this.pictureBox1).EndInit();
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

