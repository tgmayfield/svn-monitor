using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Support;

namespace SVNMonitor.View.Dialogs
{
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
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			base.Close();
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Send();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ErrorReportDialog));
			lblTitle = new Label();
			txtUserNote = new TextBox();
			btnSend = new Button();
			btnCancel = new Button();
			pictureBox1 = new PictureBox();
			linkDetails = new LinkLabel();
			txtEmail = new TextBox();
			txtName = new TextBox();
			lblEmail = new Label();
			lblName = new Label();
			lblNote = new Label();
			label1 = new Label();
			panel1 = new Panel();
			((ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			base.SuspendLayout();
			lblTitle.AutoSize = true;
			lblTitle.BackColor = Color.Transparent;
			lblTitle.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
			lblTitle.ForeColor = Color.Maroon;
			lblTitle.Location = new Point(7, 12);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(230, 50);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "You have found a bug!\r\nPlease send it to us.";
			txtUserNote.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
			txtUserNote.BackColor = Color.White;
			txtUserNote.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
			txtUserNote.Location = new Point(12, 0xe2);
			txtUserNote.Multiline = true;
			txtUserNote.Name = "txtUserNote";
			txtUserNote.ScrollBars = ScrollBars.Vertical;
			txtUserNote.Size = new Size(520, 0x9d);
			txtUserNote.TabIndex = 5;
			txtUserNote.Text = "This is the crappiest application I have ever seen in my life!";
			btnSend.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnSend.BackColor = SystemColors.Control;
			btnSend.Location = new Point(0x178, 0x185);
			btnSend.Name = "btnSend";
			btnSend.Size = new Size(0x4b, 0x17);
			btnSend.TabIndex = 6;
			btnSend.Text = "&Send";
			btnSend.UseVisualStyleBackColor = true;
			btnSend.Click += btnSend_Click;
			btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnCancel.BackColor = SystemColors.Control;
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(0x1c9, 0x185);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(0x4b, 0x17);
			btnCancel.TabIndex = 7;
			btnCancel.Text = "&Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			pictureBox1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			pictureBox1.BackColor = Color.Transparent;
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(0x1e4, 12);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(0x30, 0x30);
			pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			pictureBox1.TabIndex = 6;
			pictureBox1.TabStop = false;
			linkDetails.ActiveLinkColor = Color.Black;
			linkDetails.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			linkDetails.AutoSize = true;
			linkDetails.BackColor = Color.Transparent;
			linkDetails.LinkColor = Color.White;
			linkDetails.Location = new Point(0x114, 0x18b);
			linkDetails.Name = "linkDetails";
			linkDetails.Size = new Size(0x5e, 13);
			linkDetails.TabIndex = 8;
			linkDetails.TabStop = true;
			linkDetails.Text = "What's the report?";
			linkDetails.LinkClicked += linkDetails_LinkClicked;
			txtEmail.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			txtEmail.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
			txtEmail.Location = new Point(12, 0xb9);
			txtEmail.Name = "txtEmail";
			txtEmail.Size = new Size(520, 0x16);
			txtEmail.TabIndex = 3;
			txtName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			txtName.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
			txtName.Location = new Point(12, 0x90);
			txtName.Name = "txtName";
			txtName.Size = new Size(520, 0x16);
			txtName.TabIndex = 1;
			lblEmail.AutoSize = true;
			lblEmail.BackColor = Color.Transparent;
			lblEmail.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
			lblEmail.Location = new Point(9, 0xa9);
			lblEmail.Name = "lblEmail";
			lblEmail.Size = new Size(0x23, 13);
			lblEmail.TabIndex = 2;
			lblEmail.Text = "&Email:";
			lblName.AutoSize = true;
			lblName.BackColor = Color.Transparent;
			lblName.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
			lblName.Location = new Point(9, 0x80);
			lblName.Name = "lblName";
			lblName.Size = new Size(0x26, 13);
			lblName.TabIndex = 0;
			lblName.Text = "&Name:";
			lblNote.AutoSize = true;
			lblNote.BackColor = Color.Transparent;
			lblNote.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
			lblNote.Location = new Point(9, 210);
			lblNote.Name = "lblNote";
			lblNote.Size = new Size(0x21, 13);
			lblNote.TabIndex = 4;
			lblNote.Text = "&Note:";
			label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			label1.AutoSize = true;
			label1.BackColor = Color.Transparent;
			label1.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
			label1.Location = new Point(9, 0x4d);
			label1.Name = "label1";
			label1.Size = new Size(0x15a, 0x27);
			label1.TabIndex = 0x10;
			label1.Text = "● Unless you provide your details, the information is sent annonymously.\r\n● You may add your contact details if you want to.\r\n● We promise not to send any spam. We hate it just as much as you do.\r\n";
			panel1.BackColor = Color.Transparent;
			panel1.Controls.Add(pictureBox1);
			panel1.Controls.Add(linkDetails);
			panel1.Controls.Add(lblNote);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(txtEmail);
			panel1.Controls.Add(btnCancel);
			panel1.Controls.Add(txtName);
			panel1.Controls.Add(btnSend);
			panel1.Controls.Add(lblEmail);
			panel1.Controls.Add(lblTitle);
			panel1.Controls.Add(lblName);
			panel1.Controls.Add(txtUserNote);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(0x220, 0x1a8);
			panel1.TabIndex = 0x11;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
			BackgroundImageLayout = ImageLayout.Stretch;
			base.CancelButton = btnCancel;
			base.ClientSize = new Size(0x220, 0x1a8);
			base.ControlBox = false;
			base.Controls.Add(panel1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.Name = "ErrorReportDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			Text = "SVN-Monitor: Error Reporting";
			((ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		private void linkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			ErrorReportDetailsDialog.ShowErrorReportDetails(ErrorReport, this);
		}

		public static void Report(BaseFeedback report)
		{
			new ErrorReportDialog
			{
				ErrorReport = report
			}.Show();
		}

		private void Send()
		{
			ErrorReport.Name = txtName.Text;
			ErrorReport.Email = txtEmail.Text;
			ErrorReport.Note = txtUserNote.Text;
			SendFeedback(ErrorReport);
		}

		public BaseFeedback ErrorReport { get; private set; }
	}
}