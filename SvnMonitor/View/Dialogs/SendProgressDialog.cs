namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Logging;
    using SVNMonitor.Support;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class SendProgressDialog : BaseDialog
    {
        private Button btnAbort;
        private IContainer components;
        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;

        public SendProgressDialog()
        {
            this.InitializeComponent();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            if (this.Packet != null)
            {
                this.Packet.Abort();
            }
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(SendProgressDialog));
            this.pictureBox1 = new PictureBox();
            this.btnAbort = new Button();
            this.label1 = new Label();
            this.panel1 = new Panel();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.pictureBox1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.pictureBox1.BackColor = Color.Transparent;
            this.pictureBox1.Image = (Image) resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(12, 0x59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(220, 0x13);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.btnAbort.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnAbort.BackColor = Color.Transparent;
            this.btnAbort.DialogResult = DialogResult.Abort;
            this.btnAbort.Location = new Point(0xf2, 0x56);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new Size(0x4b, 0x17);
            this.btnAbort.TabIndex = 1;
            this.btnAbort.Text = "&Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new EventHandler(this.btnAbort_Click);
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x8e, 0x1a);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your feedback is being sent.\r\nJust a few seconds to go...";
            this.panel1.BackColor = Color.Transparent;
            this.panel1.Controls.Add(this.btnAbort);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x149, 0x79);
            this.panel1.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = (Image) resources.GetObject("$this.BackgroundImage");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.CancelButton = this.btnAbort;
            base.ClientSize = new Size(0x149, 0x79);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.KeyPreview = true;
            base.Name = "SendProgressDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Sending...";
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Alt && (e.KeyCode == Keys.F4))
            {
                e.Handled = true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Packet != null)
            {
                this.Packet.Send(new SVNMonitor.Support.SendCallback(this.SendCallback));
            }
        }

        public void SendCallback(SendableResult result)
        {
            if ((this.Packet != null) && this.Packet.Aborted)
            {
                if (result.Id > 0)
                {
                    Logger.Log.Info("Feedback was aborted, but a result has already returned : " + result.Id);
                }
            }
            else if (base.InvokeRequired)
            {
                base.BeginInvoke(new SVNMonitor.Support.SendCallback(this.SendCallback), new object[] { result });
            }
            else
            {
                base.Hide();
                Logger.Log.InfoFormat("Send result: id=" + result.Id, new object[0]);
                if (result.Id > 0)
                {
                    FeedbackConfirmationDialog.ShowFeedbackConfirmation(base.Owner, result.Id);
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    base.DialogResult = DialogResult.Cancel;
                }
                if (result.Proxy != null)
                {
                    result.Proxy.Dispose();
                }
                base.Close();
            }
        }

        public static DialogResult ShowProgress(IWin32Window owner, ISendable packet)
        {
            SendProgressDialog <>g__initLocal0 = new SendProgressDialog {
                Packet = packet
            };
            SendProgressDialog dialog = <>g__initLocal0;
            return dialog.ShowDialog(owner);
        }

        public ISendable Packet { get; private set; }
    }
}

