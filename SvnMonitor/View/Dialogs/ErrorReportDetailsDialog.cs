using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Support;
using SVNMonitor.View.Controls;

namespace SVNMonitor.View.Dialogs
{
	public class ErrorReportDetailsDialog : Form
	{
		private Button btnOK;
		private IContainer components;
		private HtmlViewer htmlViewer1;
		private Panel panel1;

		public ErrorReportDetailsDialog()
		{
			InitializeComponent();
		}

		public ErrorReportDetailsDialog(BaseFeedback errorReport)
			: this()
		{
			ReportXml = errorReport.Xml;
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
			btnOK = new Button();
			panel1 = new Panel();
			htmlViewer1 = new HtmlViewer();
			panel1.SuspendLayout();
			base.SuspendLayout();
			btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location = new Point(0x1f9, 0x1b6);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(0x4b, 0x17);
			btnOK.TabIndex = 5;
			btnOK.Text = "&OK";
			btnOK.Click += btnOK_Click;
			panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
			panel1.BorderStyle = BorderStyle.Fixed3D;
			panel1.Controls.Add(htmlViewer1);
			panel1.Location = new Point(12, 12);
			panel1.Name = "panel1";
			panel1.Size = new Size(0x238, 420);
			panel1.TabIndex = 7;
			htmlViewer1.AllowNavigation = false;
			htmlViewer1.AllowWebBrowserDrop = false;
			htmlViewer1.Dock = DockStyle.Fill;
			htmlViewer1.IsWebBrowserContextMenuEnabled = false;
			htmlViewer1.Location = new Point(0, 0);
			htmlViewer1.MinimumSize = new Size(20, 20);
			htmlViewer1.Name = "htmlViewer1";
			htmlViewer1.Size = new Size(0x234, 0x1a0);
			htmlViewer1.TabIndex = 0;
			htmlViewer1.WebBrowserShortcutsEnabled = false;
			base.AcceptButton = btnOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnOK;
			base.ClientSize = new Size(0x250, 0x1d9);
			base.Controls.Add(panel1);
			base.Controls.Add(btnOK);
			MinimumSize = new Size(500, 400);
			base.Name = "ErrorReportDetailsDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Show;
			base.StartPosition = FormStartPosition.CenterScreen;
			Text = "Error-Report Details";
			panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			htmlViewer1.SetXml(ReportXml);
		}

		public static DialogResult ShowErrorReportDetails(BaseFeedback feedback)
		{
			return ShowErrorReportDetails(feedback, null);
		}

		public static DialogResult ShowErrorReportDetails(BaseFeedback feedback, IWin32Window owner)
		{
			ErrorReportDetailsDialog dialog = new ErrorReportDetailsDialog(feedback);
			return dialog.ShowDialog(owner);
		}

		public string ReportXml { get; private set; }
	}
}