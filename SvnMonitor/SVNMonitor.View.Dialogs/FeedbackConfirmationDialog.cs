using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Drawing;

namespace SVNMonitor.View.Dialogs
{
public class FeedbackConfirmationDialog : BasePromptDialog
{
	private Button btnOK;

	private IContainer components;

	private Label label1;

	private Label label2;

	private Label label3;

	private Panel panel1;

	private TextBox txtFeedbackNumber;

	public int FeedbackNumber
	{
		get
		{
			return int.Parse(this.txtFeedbackNumber.Text);
		}
		private set
		{
			this.txtFeedbackNumber.Text = &value.ToString();
		}
	}

	public FeedbackConfirmationDialog()
	{
		this.InitializeComponent();
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
		ComponentResourceManager resources = new ComponentResourceManager(typeof(FeedbackConfirmationDialog));
		this.label1 = new Label();
		this.txtFeedbackNumber = new TextBox();
		this.label2 = new Label();
		this.btnOK = new Button();
		this.label3 = new Label();
		this.panel1 = new Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.BackColor = Color.Transparent;
		this.label1.Font = new Font("Microsoft Sans Serif", 15.75, FontStyle.Regular, GraphicsUnit.Point, 177);
		this.label1.Location = new Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new Size(119, 25);
		this.label1.TabIndex = 0;
		this.label1.Text = "Thank you.";
		this.txtFeedbackNumber.BackColor = Color.MintCream;
		this.txtFeedbackNumber.BorderStyle = BorderStyle.FixedSingle;
		this.txtFeedbackNumber.Location = new Point(15, 71);
		this.txtFeedbackNumber.Name = "txtFeedbackNumber";
		this.txtFeedbackNumber.ReadOnly = true;
		this.txtFeedbackNumber.Size = new Size(206, 20);
		this.txtFeedbackNumber.TabIndex = 1;
		this.label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.BackColor = Color.Transparent;
		this.label2.Location = new Point(14, 94);
		this.label2.Name = "label2";
		this.label2.Size = new Size(440, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "In case you would like to receive further information about your feedback, keep this number.";
		this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		this.btnOK.BackColor = SystemColors.Control;
		this.btnOK.DialogResult = DialogResult.OK;
		this.btnOK.Location = new Point(385, 140);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new Size(75, 23);
		this.btnOK.TabIndex = 3;
		this.btnOK.Text = "&OK";
		this.btnOK.UseVisualStyleBackColor = true;
		this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.BackColor = Color.Transparent;
		this.label3.Location = new Point(12, 55);
		this.label3.Name = "label3";
		this.label3.Size = new Size(149, 13);
		this.label3.TabIndex = 2;
		this.label3.Text = "This is your feedback number:";
		this.panel1.BackColor = Color.Transparent;
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.txtFeedbackNumber);
		this.panel1.Dock = DockStyle.Fill;
		this.panel1.Location = new Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new Size(472, 175);
		this.panel1.TabIndex = 4;
		base.AcceptButton = this.btnOK;
		base.AutoScaleDimensions = new SizeF(6, 13);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
		base.ClientSize = new Size(472, 175);
		base.Controls.Add(this.panel1);
		base.Icon = (Icon)resources.GetObject("$this.Icon");
		base.Name = "FeedbackConfirmationDialog";
		base.StartPosition = FormStartPosition.CenterScreen;
		base.Text = "Feedback Confirmation";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}

	public static DialogResult ShowFeedbackConfirmation(int feedbackNumber)
	{
		return FeedbackConfirmationDialog.ShowFeedbackConfirmation(null, feedbackNumber);
	}

	public static DialogResult ShowFeedbackConfirmation(IWin32Window owner, int feedbackNumber)
	{
		FeedbackConfirmationDialog feedbackConfirmationDialog = new FeedbackConfirmationDialog();
		feedbackConfirmationDialog.FeedbackNumber = feedbackNumber;
		FeedbackConfirmationDialog dialog = feedbackConfirmationDialog;
		return dialog.ShowDialog(owner);
	}
}
}