using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Support;

namespace SVNMonitor.View.Dialogs
{
	public class UsageInformationHelp : BaseDialog
	{
		private Button btnSend;
		private IContainer components;
		private Label label1;
		private Label label2;
		private TextBox txtUsageInformation;

		public UsageInformationHelp()
		{
			InitializeComponent();
			ShowUsageInformation();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			base.Close();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(UsageInformationHelp));
			btnSend = new Button();
			txtUsageInformation = new TextBox();
			label1 = new Label();
			label2 = new Label();
			base.SuspendLayout();
			btnSend.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnSend.DialogResult = DialogResult.OK;
			btnSend.Location = new Point(0x195, 0x152);
			btnSend.Name = "btnSend";
			btnSend.Size = new Size(0x4b, 0x17);
			btnSend.TabIndex = 2;
			btnSend.Text = "&OK";
			btnSend.Click += btnOK_Click;
			txtUsageInformation.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
			txtUsageInformation.BackColor = Color.White;
			txtUsageInformation.Location = new Point(12, 0x53);
			txtUsageInformation.Multiline = true;
			txtUsageInformation.Name = "txtUsageInformation";
			txtUsageInformation.ReadOnly = true;
			txtUsageInformation.ScrollBars = ScrollBars.Both;
			txtUsageInformation.Size = new Size(0x1d4, 0xf5);
			txtUsageInformation.TabIndex = 1;
			label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(0x1d4, 0x35);
			label1.TabIndex = 0;
			label1.Text = "Usage information contains anonymous information about the usage of the application, such as application settings, number of sources and monitors and environment variables.";
			label2.AutoSize = true;
			label2.Location = new Point(12, 0x42);
			label2.Name = "label2";
			label2.Size = new Size(0x55, 13);
			label2.TabIndex = 3;
			label2.Text = "This will be sent:";
			base.AcceptButton = btnSend;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnSend;
			base.ClientSize = new Size(0x1ec, 0x175);
			base.Controls.Add(label2);
			base.Controls.Add(btnSend);
			base.Controls.Add(txtUsageInformation);
			base.Controls.Add(label1);
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			MinimumSize = new Size(350, 300);
			base.Name = "UsageInformationHelp";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Show;
			base.StartPosition = FormStartPosition.CenterScreen;
			Text = "Usage Information";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void ShowUsageInformation()
		{
			string info = UsageInformationSender.GetUsageInformation();
			txtUsageInformation.Text = info;
			txtUsageInformation.Select(0, 0);
		}

		public static void ShowUsageInformationHelp()
		{
			new UsageInformationHelp().ShowDialog();
		}
	}
}