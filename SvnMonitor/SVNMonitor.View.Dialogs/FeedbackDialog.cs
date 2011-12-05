using System.Windows.Forms;
using System.ComponentModel;
using SVNMonitor.Support;
using System;
using SVNMonitor.Logging;
using System.Drawing;

namespace SVNMonitor.View.Dialogs
{
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

	public UserFeedback UserFeedback
	{
		get;
		private set;
	}

	public FeedbackDialog()
	{
		this.InitializeComponent();
		this.UserFeedback = new UserFeedback();
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
		if (disposing && this.components != null)
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
		this.pictureBox1.BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.lblTitle2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		this.lblTitle2.AutoSize = true;
		this.lblTitle2.BackColor = Color.Transparent;
		this.lblTitle2.ForeColor = Color.FromArgb(64, 64, 64);
		this.lblTitle2.Location = new Point(9, 77);
		this.lblTitle2.Name = "lblTitle2";
		this.lblTitle2.Size = new Size(346, 39);
		this.lblTitle2.TabIndex = 0;
		this.lblTitle2.Text = "● Unless you provide your details, the information is sent annonymously.\r\n● You may add your contact details if you want to.\r\n● We promise not to send any spam. We hate it just as much as you do.";
		this.txtNote.AcceptsReturn = true;
		this.txtNote.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		this.txtNote.Font = new Font("Microsoft Sans Serif", 9.75, FontStyle.Regular, GraphicsUnit.Point, 177);
		this.txtNote.Location = new Point(12, 226);
		this.txtNote.Multiline = true;
		this.txtNote.Name = "txtNote";
		this.txtNote.ScrollBars = ScrollBars.Vertical;
		this.txtNote.Size = new Size(520, 148);
		this.txtNote.TabIndex = 5;
		this.txtNote.add_TextChanged(new EventHandler(this.txtNote_TextChanged));
		this.btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		this.btnSend.BackColor = SystemColors.Control;
		this.btnSend.Enabled = false;
		this.btnSend.Location = new Point(376, 389);
		this.btnSend.Name = "btnSend";
		this.btnSend.Size = new Size(75, 23);
		this.btnSend.TabIndex = 8;
		this.btnSend.Text = "&Send";
		this.btnSend.UseVisualStyleBackColor = true;
		this.btnSend.add_Click(new EventHandler(this.btnSend_Click));
		this.lblName.AutoSize = true;
		this.lblName.BackColor = Color.Transparent;
		this.lblName.ForeColor = Color.FromArgb(64, 64, 64);
		this.lblName.Location = new Point(9, 128);
		this.lblName.Name = "lblName";
		this.lblName.Size = new Size(38, 13);
		this.lblName.TabIndex = 0;
		this.lblName.Text = "&Name:";
		this.lblEmail.AutoSize = true;
		this.lblEmail.BackColor = Color.Transparent;
		this.lblEmail.ForeColor = Color.FromArgb(64, 64, 64);
		this.lblEmail.Location = new Point(9, 169);
		this.lblEmail.Name = "lblEmail";
		this.lblEmail.Size = new Size(35, 13);
		this.lblEmail.TabIndex = 2;
		this.lblEmail.Text = "&Email:";
		this.txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		this.txtName.Font = new Font("Microsoft Sans Serif", 9.75, FontStyle.Regular, GraphicsUnit.Point, 177);
		this.txtName.Location = new Point(12, 144);
		this.txtName.Name = "txtName";
		this.txtName.Size = new Size(520, 22);
		this.txtName.TabIndex = 1;
		this.txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		this.txtEmail.Font = new Font("Microsoft Sans Serif", 9.75, FontStyle.Regular, GraphicsUnit.Point, 177);
		this.txtEmail.Location = new Point(12, 185);
		this.txtEmail.Name = "txtEmail";
		this.txtEmail.Size = new Size(520, 22);
		this.txtEmail.TabIndex = 3;
		this.lblNote.AutoSize = true;
		this.lblNote.BackColor = Color.Transparent;
		this.lblNote.ForeColor = Color.FromArgb(64, 64, 64);
		this.lblNote.Location = new Point(9, 210);
		this.lblNote.Name = "lblNote";
		this.lblNote.Size = new Size(33, 13);
		this.lblNote.TabIndex = 4;
		this.lblNote.Text = "&Note:";
		this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		this.btnCancel.BackColor = SystemColors.Control;
		this.btnCancel.DialogResult = DialogResult.Cancel;
		this.btnCancel.Location = new Point(457, 389);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new Size(75, 23);
		this.btnCancel.TabIndex = 9;
		this.btnCancel.Text = "&Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.add_Click(new EventHandler(this.btnCancel_Click));
		this.pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		this.pictureBox1.BackColor = Color.Transparent;
		this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new Point(484, 12);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new Size(48, 48);
		this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
		this.pictureBox1.TabIndex = 8;
		this.pictureBox1.TabStop = false;
		this.lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		this.lblTitle.AutoSize = true;
		this.lblTitle.BackColor = Color.Transparent;
		this.lblTitle.Font = new Font("Microsoft Sans Serif", 15.75, FontStyle.Regular, GraphicsUnit.Point, 177);
		this.lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
		this.lblTitle.Location = new Point(7, 12);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new Size(239, 25);
		this.lblTitle.TabIndex = 0;
		this.lblTitle.Text = "Send us your feedback!";
		this.label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.BackColor = Color.Transparent;
		this.label1.ForeColor = Color.White;
		this.label1.Location = new Point(12, 377);
		this.label1.Name = "label1";
		this.label1.Size = new Size(89, 13);
		this.label1.TabIndex = 9;
		this.label1.Text = "Reporting a bug?";
		this.checkIncludeUsageInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		this.checkIncludeUsageInfo.AutoSize = true;
		this.checkIncludeUsageInfo.BackColor = Color.Transparent;
		this.checkIncludeUsageInfo.ForeColor = Color.White;
		this.checkIncludeUsageInfo.Location = new Point(12, 393);
		this.checkIncludeUsageInfo.Name = "checkIncludeUsageInfo";
		this.checkIncludeUsageInfo.Size = new Size(243, 17);
		this.checkIncludeUsageInfo.TabIndex = 6;
		this.checkIncludeUsageInfo.Text = "&Include additional information that may help us";
		this.checkIncludeUsageInfo.UseVisualStyleBackColor = true;
		this.linkUsageInformationHelp.ActiveLinkColor = Color.Black;
		this.linkUsageInformationHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		this.linkUsageInformationHelp.AutoSize = true;
		this.linkUsageInformationHelp.BackColor = Color.Transparent;
		this.linkUsageInformationHelp.LinkColor = Color.FromArgb(255, 255, 192);
		this.linkUsageInformationHelp.Location = new Point(261, 395);
		this.linkUsageInformationHelp.Name = "linkUsageInformationHelp";
		this.linkUsageInformationHelp.Size = new Size(58, 13);
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
		this.panel1.Size = new Size(544, 424);
		this.panel1.TabIndex = 10;
		base.AutoScaleDimensions = new SizeF(6, 13);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.BackColor = Color.White;
		base.BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
		base.BackgroundImageLayout = ImageLayout.Stretch;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new Size(544, 424);
		base.ControlBox = false;
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.Icon = (Icon)resources.GetObject("$this.Icon");
		base.Name = "FeedbackDialog";
		base.ShowInTaskbar = false;
		base.StartPosition = FormStartPosition.CenterScreen;
		base.Text = "Send Feedback";
		this.pictureBox1.EndInit();
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
		base.SendFeedback(this.UserFeedback);
	}

	internal static void ShowFeedbackDialog()
	{
		FeedbackDialog dialog = new FeedbackDialog();
		dialog.Show();
	}

	private void txtNote_TextChanged(object sender, EventArgs e)
	{
		this.btnSend.Enabled = !string.IsNullOrEmpty(this.txtNote.Text);
	}
}
}