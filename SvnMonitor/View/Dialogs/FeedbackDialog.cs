namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Logging;
    using SVNMonitor.Support;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FeedbackDialog : BaseFeedbackDialog
    {
        private Button btnCancel;
        private Button btnSend;
        private CheckBox checkIncludeUsageInfo;
        private IContainer components;
        private Label label1;
        private Label lblEmail;
        private Label lblName;
        private Label lblNote;
        private Label lblTitle;
        private Label lblTitle2;
        private LinkLabel linkUsageInformationHelp;
        private Panel panel1;
        private PictureBox pictureBox1;
        private TextBox txtEmail;
        private TextBox txtName;
        private TextBox txtNote;

        public FeedbackDialog()
        {
            this.InitializeComponent();
            this.UserFeedback = new SVNMonitor.Support.UserFeedback();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            base.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.Send();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FeedbackDialog));
            this.lblTitle2 = new Label();
            this.txtNote = new TextBox();
            this.btnSend = new Button();
            this.lblName = new Label();
            this.lblEmail = new Label();
            this.txtName = new TextBox();
            this.txtEmail = new TextBox();
            this.lblNote = new Label();
            this.btnCancel = new Button();
            this.pictureBox1 = new PictureBox();
            this.lblTitle = new Label();
            this.label1 = new Label();
            this.checkIncludeUsageInfo = new CheckBox();
            this.linkUsageInformationHelp = new LinkLabel();
            this.panel1 = new Panel();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblTitle2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.BackColor = Color.Transparent;
            this.lblTitle2.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblTitle2.Location = new Point(9, 0x4d);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new Size(0x15a, 0x27);
            this.lblTitle2.TabIndex = 0;
            this.lblTitle2.Text = "● Unless you provide your details, the information is sent annonymously.\r\n● You may add your contact details if you want to.\r\n● We promise not to send any spam. We hate it just as much as you do.";
            this.txtNote.AcceptsReturn = true;
            this.txtNote.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtNote.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.txtNote.Location = new Point(12, 0xe2);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = ScrollBars.Vertical;
            this.txtNote.Size = new Size(520, 0x94);
            this.txtNote.TabIndex = 5;
            this.txtNote.TextChanged += new EventHandler(this.txtNote_TextChanged);
            this.btnSend.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnSend.BackColor = SystemColors.Control;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new Point(0x178, 0x185);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new Size(0x4b, 0x17);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new EventHandler(this.btnSend_Click);
            this.lblName.AutoSize = true;
            this.lblName.BackColor = Color.Transparent;
            this.lblName.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblName.Location = new Point(9, 0x80);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x26, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "&Name:";
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = Color.Transparent;
            this.lblEmail.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblEmail.Location = new Point(9, 0xa9);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(0x23, 13);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "&Email:";
            this.txtName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtName.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.txtName.Location = new Point(12, 0x90);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(520, 0x16);
            this.txtName.TabIndex = 1;
            this.txtEmail.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtEmail.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.txtEmail.Location = new Point(12, 0xb9);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(520, 0x16);
            this.txtEmail.TabIndex = 3;
            this.lblNote.AutoSize = true;
            this.lblNote.BackColor = Color.Transparent;
            this.lblNote.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblNote.Location = new Point(9, 210);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new Size(0x21, 13);
            this.lblNote.TabIndex = 4;
            this.lblNote.Text = "&Note:";
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.BackColor = SystemColors.Control;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x1c9, 0x185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.pictureBox1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.pictureBox1.BackColor = Color.Transparent;
            this.pictureBox1.Image = (Image) resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(0x1e4, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x30, 0x30);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.lblTitle.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.lblTitle.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblTitle.Location = new Point(7, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0xef, 0x19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Send us your feedback!";
            this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.ForeColor = Color.White;
            this.label1.Location = new Point(12, 0x179);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Reporting a bug?";
            this.checkIncludeUsageInfo.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.checkIncludeUsageInfo.AutoSize = true;
            this.checkIncludeUsageInfo.BackColor = Color.Transparent;
            this.checkIncludeUsageInfo.ForeColor = Color.White;
            this.checkIncludeUsageInfo.Location = new Point(12, 0x189);
            this.checkIncludeUsageInfo.Name = "checkIncludeUsageInfo";
            this.checkIncludeUsageInfo.Size = new Size(0xf3, 0x11);
            this.checkIncludeUsageInfo.TabIndex = 6;
            this.checkIncludeUsageInfo.Text = "&Include additional information that may help us";
            this.checkIncludeUsageInfo.UseVisualStyleBackColor = true;
            this.linkUsageInformationHelp.ActiveLinkColor = Color.Black;
            this.linkUsageInformationHelp.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.linkUsageInformationHelp.AutoSize = true;
            this.linkUsageInformationHelp.BackColor = Color.Transparent;
            this.linkUsageInformationHelp.LinkColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.linkUsageInformationHelp.Location = new Point(0x105, 0x18b);
            this.linkUsageInformationHelp.Name = "linkUsageInformationHelp";
            this.linkUsageInformationHelp.Size = new Size(0x3a, 13);
            this.linkUsageInformationHelp.TabIndex = 7;
            this.linkUsageInformationHelp.TabStop = true;
            this.linkUsageInformationHelp.Text = "&What this?";
            this.linkUsageInformationHelp.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkUsageInformationHelp_LinkClicked);
            this.panel1.BackColor = Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.linkUsageInformationHelp);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.checkIncludeUsageInfo);
            this.panel1.Controls.Add(this.lblTitle2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.txtNote);
            this.panel1.Controls.Add(this.lblNote);
            this.panel1.Controls.Add(this.lblEmail);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x220, 0x1a8);
            this.panel1.TabIndex = 10;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.BackgroundImage = (Image) resources.GetObject("$this.BackgroundImage");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x220, 0x1a8);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "FeedbackDialog";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Send Feedback";
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void linkUsageInformationHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.LogUserAction();
            ErrorReportDetailsDialog.ShowErrorReportDetails(this.UserFeedback, this);
        }

        private void Send()
        {
            this.UserFeedback.Name = this.txtName.Text;
            this.UserFeedback.Email = this.txtEmail.Text;
            this.UserFeedback.Note = this.txtNote.Text;
            this.UserFeedback.IncludeAdditionalInfo = this.checkIncludeUsageInfo.Checked;
            this.SendFeedback(this.UserFeedback);
        }

        internal static void ShowFeedbackDialog()
        {
            new FeedbackDialog().Show();
        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            this.btnSend.Enabled = !string.IsNullOrEmpty(this.txtNote.Text);
        }

        public SVNMonitor.Support.UserFeedback UserFeedback { get; private set; }
    }
}

