using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Support;

namespace SVNMonitor.View.Dialogs
{
	public class SendProgressDialog : BaseDialog
	{
		private Button btnAbort;
		private IContainer components;
		private Label label1;
		private Panel panel1;
		private PictureBox pictureBox1;

		public SendProgressDialog()
		{
			InitializeComponent();
		}

		private void btnAbort_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			if (Packet != null)
			{
				Packet.Abort();
			}
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(SendProgressDialog));
			pictureBox1 = new PictureBox();
			btnAbort = new Button();
			label1 = new Label();
			panel1 = new Panel();
			((ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			base.SuspendLayout();
			pictureBox1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
			pictureBox1.BackColor = Color.Transparent;
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(12, 0x59);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(220, 0x13);
			pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			btnAbort.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnAbort.BackColor = Color.Transparent;
			btnAbort.DialogResult = DialogResult.Abort;
			btnAbort.Location = new Point(0xf2, 0x56);
			btnAbort.Name = "btnAbort";
			btnAbort.Size = new Size(0x4b, 0x17);
			btnAbort.TabIndex = 1;
			btnAbort.Text = "&Abort";
			btnAbort.UseVisualStyleBackColor = true;
			btnAbort.Click += btnAbort_Click;
			label1.AutoSize = true;
			label1.BackColor = Color.Transparent;
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(0x8e, 0x1a);
			label1.TabIndex = 2;
			label1.Text = "Your feedback is being sent.\r\nJust a few seconds to go...";
			panel1.BackColor = Color.Transparent;
			panel1.Controls.Add(btnAbort);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(pictureBox1);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(0x149, 0x79);
			panel1.TabIndex = 3;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
			BackgroundImageLayout = ImageLayout.Stretch;
			base.CancelButton = btnAbort;
			base.ClientSize = new Size(0x149, 0x79);
			base.ControlBox = false;
			base.Controls.Add(panel1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.Name = "SendProgressDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			Text = "Sending...";
			((ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
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
			if (Packet != null)
			{
				Packet.Send(SendCallback);
			}
		}

		public void SendCallback(SendableResult result)
		{
			if ((Packet != null) && Packet.Aborted)
			{
				if (result.Id > 0)
				{
					Logger.Log.Info("Feedback was aborted, but a result has already returned : " + result.Id);
				}
			}
			else if (base.InvokeRequired)
			{
				base.BeginInvoke(new SVNMonitor.Support.SendCallback(SendCallback), new object[]
				{
					result
				});
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
			SendProgressDialog tempLocal0 = new SendProgressDialog
			{
				Packet = packet
			};
			SendProgressDialog dialog = tempLocal0;
			return dialog.ShowDialog(owner);
		}

		public ISendable Packet { get; private set; }
	}
}