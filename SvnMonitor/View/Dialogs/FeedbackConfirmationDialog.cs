using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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

		public FeedbackConfirmationDialog()
		{
			InitializeComponent();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(FeedbackConfirmationDialog));
			label1 = new Label();
			txtFeedbackNumber = new TextBox();
			label2 = new Label();
			btnOK = new Button();
			label3 = new Label();
			panel1 = new Panel();
			panel1.SuspendLayout();
			base.SuspendLayout();
			label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			label1.AutoSize = true;
			label1.BackColor = Color.Transparent;
			label1.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(0x77, 0x19);
			label1.TabIndex = 0;
			label1.Text = "Thank you.";
			txtFeedbackNumber.BackColor = Color.MintCream;
			txtFeedbackNumber.BorderStyle = BorderStyle.FixedSingle;
			txtFeedbackNumber.Location = new Point(15, 0x47);
			txtFeedbackNumber.Name = "txtFeedbackNumber";
			txtFeedbackNumber.ReadOnly = true;
			txtFeedbackNumber.Size = new Size(0xce, 20);
			txtFeedbackNumber.TabIndex = 1;
			label2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			label2.AutoSize = true;
			label2.BackColor = Color.Transparent;
			label2.Location = new Point(14, 0x5e);
			label2.Name = "label2";
			label2.Size = new Size(440, 13);
			label2.TabIndex = 2;
			label2.Text = "In case you would like to receive further information about your feedback, keep this number.";
			btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnOK.BackColor = SystemColors.Control;
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location = new Point(0x181, 140);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(0x4b, 0x17);
			btnOK.TabIndex = 3;
			btnOK.Text = "&OK";
			btnOK.UseVisualStyleBackColor = true;
			label3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			label3.AutoSize = true;
			label3.BackColor = Color.Transparent;
			label3.Location = new Point(12, 0x37);
			label3.Name = "label3";
			label3.Size = new Size(0x95, 13);
			label3.TabIndex = 2;
			label3.Text = "This is your feedback number:";
			panel1.BackColor = Color.Transparent;
			panel1.Controls.Add(btnOK);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(txtFeedbackNumber);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(0x1d8, 0xaf);
			panel1.TabIndex = 4;
			base.AcceptButton = btnOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
			base.ClientSize = new Size(0x1d8, 0xaf);
			base.Controls.Add(panel1);
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.Name = "FeedbackConfirmationDialog";
			base.StartPosition = FormStartPosition.CenterScreen;
			Text = "Feedback Confirmation";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		public static DialogResult ShowFeedbackConfirmation(int feedbackNumber)
		{
			return ShowFeedbackConfirmation(null, feedbackNumber);
		}

		public static DialogResult ShowFeedbackConfirmation(IWin32Window owner, int feedbackNumber)
		{
			FeedbackConfirmationDialog tempLocal0 = new FeedbackConfirmationDialog
			{
				FeedbackNumber = feedbackNumber
			};
			FeedbackConfirmationDialog dialog = tempLocal0;
			return dialog.ShowDialog(owner);
		}

		public int FeedbackNumber
		{
			get { return int.Parse(txtFeedbackNumber.Text); }
			private set { txtFeedbackNumber.Text = value.ToString(); }
		}
	}
}