namespace SVNMonitor.View.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

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
            this.InitializeComponent();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FeedbackConfirmationDialog));
            this.label1 = new Label();
            this.txtFeedbackNumber = new TextBox();
            this.label2 = new Label();
            this.btnOK = new Button();
            this.label3 = new Label();
            this.panel1 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Regular, GraphicsUnit.Point, 0xb1);
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x77, 0x19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thank you.";
            this.txtFeedbackNumber.BackColor = Color.MintCream;
            this.txtFeedbackNumber.BorderStyle = BorderStyle.FixedSingle;
            this.txtFeedbackNumber.Location = new Point(15, 0x47);
            this.txtFeedbackNumber.Name = "txtFeedbackNumber";
            this.txtFeedbackNumber.ReadOnly = true;
            this.txtFeedbackNumber.Size = new Size(0xce, 20);
            this.txtFeedbackNumber.TabIndex = 1;
            this.label2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Location = new Point(14, 0x5e);
            this.label2.Name = "label2";
            this.label2.Size = new Size(440, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "In case you would like to receive further information about your feedback, keep this number.";
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.BackColor = SystemColors.Control;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x181, 140);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.label3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.Transparent;
            this.label3.Location = new Point(12, 0x37);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x95, 13);
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
            this.panel1.Size = new Size(0x1d8, 0xaf);
            this.panel1.TabIndex = 4;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = (Image) resources.GetObject("$this.BackgroundImage");
            base.ClientSize = new Size(0x1d8, 0xaf);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "FeedbackConfirmationDialog";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Feedback Confirmation";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        public static DialogResult ShowFeedbackConfirmation(int feedbackNumber)
        {
            return ShowFeedbackConfirmation(null, feedbackNumber);
        }

        public static DialogResult ShowFeedbackConfirmation(IWin32Window owner, int feedbackNumber)
        {
            FeedbackConfirmationDialog <>g__initLocal0 = new FeedbackConfirmationDialog {
                FeedbackNumber = feedbackNumber
            };
            FeedbackConfirmationDialog dialog = <>g__initLocal0;
            return dialog.ShowDialog(owner);
        }

        public int FeedbackNumber
        {
            get
            {
                return int.Parse(this.txtFeedbackNumber.Text);
            }
            private set
            {
                this.txtFeedbackNumber.Text = value.ToString();
            }
        }
    }
}

