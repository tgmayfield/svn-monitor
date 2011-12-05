namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Logging;
    using SVNMonitor.Support;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class UsageInformationHelp : BaseDialog
    {
        private Button btnSend;
        private IContainer components;
        private Label label1;
        private Label label2;
        private TextBox txtUsageInformation;

        public UsageInformationHelp()
        {
            this.InitializeComponent();
            this.ShowUsageInformation();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            base.Close();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(UsageInformationHelp));
            this.btnSend = new Button();
            this.txtUsageInformation = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            base.SuspendLayout();
            this.btnSend.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnSend.DialogResult = DialogResult.OK;
            this.btnSend.Location = new Point(0x195, 0x152);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new Size(0x4b, 0x17);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "&OK";
            this.btnSend.Click += new EventHandler(this.btnOK_Click);
            this.txtUsageInformation.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtUsageInformation.BackColor = Color.White;
            this.txtUsageInformation.Location = new Point(12, 0x53);
            this.txtUsageInformation.Multiline = true;
            this.txtUsageInformation.Name = "txtUsageInformation";
            this.txtUsageInformation.ReadOnly = true;
            this.txtUsageInformation.ScrollBars = ScrollBars.Both;
            this.txtUsageInformation.Size = new Size(0x1d4, 0xf5);
            this.txtUsageInformation.TabIndex = 1;
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d4, 0x35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usage information contains anonymous information about the usage of the application, such as application settings, number of sources and monitors and environment variables.";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x42);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "This will be sent:";
            base.AcceptButton = this.btnSend;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnSend;
            base.ClientSize = new Size(0x1ec, 0x175);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnSend);
            base.Controls.Add(this.txtUsageInformation);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            this.MinimumSize = new Size(350, 300);
            base.Name = "UsageInformationHelp";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Show;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Usage Information";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ShowUsageInformation()
        {
            string info = UsageInformationSender.GetUsageInformation();
            this.txtUsageInformation.Text = info;
            this.txtUsageInformation.Select(0, 0);
        }

        public static void ShowUsageInformationHelp()
        {
            new UsageInformationHelp().ShowDialog();
        }
    }
}

