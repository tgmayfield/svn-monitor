namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class RollbackPromptDialog : BasePromptDialog
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
            this.InitializeComponent();
        }

        public RollbackPromptDialog(long revision) : this()
        {
            this.lblPrompt.Text = string.Format(this.lblPrompt.Text, revision);
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(RollbackPromptDialog));
            this.checkDontShowAgain = new CheckBox();
            this.btnYes = new Button();
            this.btnNo = new Button();
            this.lblPrompt = new Label();
            this.pictureBox1 = new PictureBox();
            this.panel1 = new Panel();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.checkDontShowAgain.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.checkDontShowAgain.AutoSize = true;
            this.checkDontShowAgain.Location = new Point(0x10, 0x5e);
            this.checkDontShowAgain.Margin = new Padding(4, 4, 4, 4);
            this.checkDontShowAgain.Name = "checkDontShowAgain";
            this.checkDontShowAgain.Size = new Size(0xea, 0x15);
            this.checkDontShowAgain.TabIndex = 7;
            this.checkDontShowAgain.Text = "&Don't ask again (always rollback)";
            this.checkDontShowAgain.CheckedChanged += new EventHandler(this.checkDontShowAgain_CheckedChanged);
            this.btnYes.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnYes.DialogResult = DialogResult.Yes;
            this.btnYes.Location = new Point(0xd8, 0x7a);
            this.btnYes.Margin = new Padding(4, 4, 4, 4);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new Size(0x6d, 0x1c);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "&Rollback";
            this.btnNo.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnNo.DialogResult = DialogResult.No;
            this.btnNo.Location = new Point(0x14d, 0x7a);
            this.btnNo.Margin = new Padding(4, 4, 4, 4);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new Size(0x6d, 0x1c);
            this.btnNo.TabIndex = 6;
            this.btnNo.Text = "&Cancel";
            this.lblPrompt.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblPrompt.Location = new Point(0x45, 15);
            this.lblPrompt.Margin = new Padding(4, 0, 4, 0);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new Size(0x175, 0x40);
            this.lblPrompt.TabIndex = 5;
            this.lblPrompt.Text = "Are you sure you want to rollback to revision {0}?";
            this.pictureBox1.Image = (Image) resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(0x10, 15);
            this.pictureBox1.Margin = new Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x2d, 0x2a);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.checkDontShowAgain);
            this.panel1.Controls.Add(this.lblPrompt);
            this.panel1.Controls.Add(this.btnYes);
            this.panel1.Controls.Add(this.btnNo);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Margin = new Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1cb, 0xa5);
            this.panel1.TabIndex = 8;
            base.AcceptButton = this.btnYes;
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnNo;
            base.ClientSize = new Size(0x1cb, 0xa5);
            base.Controls.Add(this.panel1);
            base.Margin = new Padding(4, 4, 4, 4);
            base.Name = "RollbackPromptDialog";
            this.Text = "Rollback";
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            ApplicationSettingsManager.Settings.PromptRollbackOldRevision = !this.checkDontShowAgain.Checked;
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

