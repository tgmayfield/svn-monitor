namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Logging;
    using SVNMonitor.Support;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ErrorReportDialog : BaseFeedbackDialog
    {
        private Button btnCancel;
        private Button btnSend;
        private IContainer components;
        private Label label1;
        private Label lblEmail;
        private Label lblName;
        private Label lblNote;
        private Label lblTitle;
        private LinkLabel linkDetails;
        private Panel panel1;
        private PictureBox pictureBox1;
        private TextBox txtEmail;
        private TextBox txtName;
        private TextBox txtUserNote;

        public ErrorReportDialog()
        {
            this.InitializeComponent();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ErrorReportDialog));
            this.lblTitle = new Label();
            this.txtUserNote = new TextBox();
            this.btnSend = new Button();
            this.btnCancel = new Button();
            this.pictureBox1 = new PictureBox();
            this.linkDetails = new LinkLabel();
            this.txtEmail = new TextBox();
            this.txtName = new TextBox();
            this.lblEmail = new Label();
            this.lblName = new Label();
            this.lblNote = new Label();
            this.label1 = new Label();
            this.panel1 = new Panel();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.lblTitle.ForeColor = Color.Maroon;
            this.lblTitle.Location = new Point(7, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(230, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "You have found a bug!\r\nPlease send it to us.";
            this.txtUserNote.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtUserNote.BackColor = Color.White;
            this.txtUserNote.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.txtUserNote.Location = new Point(12, 0xe2);
            this.txtUserNote.Multiline = true;
            this.txtUserNote.Name = "txtUserNote";
            this.txtUserNote.ScrollBars = ScrollBars.Vertical;
            this.txtUserNote.Size = new Size(520, 0x9d);
            this.txtUserNote.TabIndex = 5;
            this.txtUserNote.Text = "This is the crappiest application I have ever seen in my life!";
            this.btnSend.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnSend.BackColor = SystemColors.Control;
            this.btnSend.Location = new Point(0x178, 0x185);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new Size(0x4b, 0x17);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new EventHandler(this.btnSend_Click);
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.BackColor = SystemColors.Control;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x1c9, 0x185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 7;
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
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.linkDetails.ActiveLinkColor = Color.Black;
            this.linkDetails.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.linkDetails.AutoSize = true;
            this.linkDetails.BackColor = Color.Transparent;
            this.linkDetails.LinkColor = Color.White;
            this.linkDetails.Location = new Point(0x114, 0x18b);
            this.linkDetails.Name = "linkDetails";
            this.linkDetails.Size = new Size(0x5e, 13);
            this.linkDetails.TabIndex = 8;
            this.linkDetails.TabStop = true;
            this.linkDetails.Text = "What's the report?";
            this.linkDetails.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkDetails_LinkClicked);
            this.txtEmail.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtEmail.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.txtEmail.Location = new Point(12, 0xb9);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(520, 0x16);
            this.txtEmail.TabIndex = 3;
            this.txtName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtName.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.txtName.Location = new Point(12, 0x90);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(520, 0x16);
            this.txtName.TabIndex = 1;
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = Color.Transparent;
            this.lblEmail.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblEmail.Location = new Point(9, 0xa9);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(0x23, 13);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "&Email:";
            this.lblName.AutoSize = true;
            this.lblName.BackColor = Color.Transparent;
            this.lblName.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblName.Location = new Point(9, 0x80);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x26, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "&Name:";
            this.lblNote.AutoSize = true;
            this.lblNote.BackColor = Color.Transparent;
            this.lblNote.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblNote.Location = new Point(9, 210);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new Size(0x21, 13);
            this.lblNote.TabIndex = 4;
            this.lblNote.Text = "&Note:";
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.label1.Location = new Point(9, 0x4d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x15a, 0x27);
            this.label1.TabIndex = 0x10;
            this.label1.Text = "● Unless you provide your details, the information is sent annonymously.\r\n● You may add your contact details if you want to.\r\n● We promise not to send any spam. We hate it just as much as you do.\r\n";
            this.panel1.BackColor = Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.linkDetails);
            this.panel1.Controls.Add(this.lblNote);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.lblEmail);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.txtUserNote);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x220, 0x1a8);
            this.panel1.TabIndex = 0x11;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = (Image) resources.GetObject("$this.BackgroundImage");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x220, 0x1a8);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "ErrorReportDialog";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "SVN-Monitor: Error Reporting";
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void linkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.LogUserAction();
            ErrorReportDetailsDialog.ShowErrorReportDetails(this.ErrorReport, this);
        }

        public static void Report(BaseFeedback report)
        {
            new ErrorReportDialog { ErrorReport = report }.Show();
        }

        private void Send()
        {
            this.ErrorReport.Name = this.txtName.Text;
            this.ErrorReport.Email = this.txtEmail.Text;
            this.ErrorReport.Note = this.txtUserNote.Text;
            this.SendFeedback(this.ErrorReport);
        }

        public BaseFeedback ErrorReport { get; private set; }
    }
}

