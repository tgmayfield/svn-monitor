using System.Windows.Forms;
using System.ComponentModel;
using SVNMonitor.View.Controls;
using System;
using SVNMonitor.Support;
using SVNMonitor.Logging;
using System.Drawing;

namespace SVNMonitor.View.Dialogs
{
public class ErrorReportDetailsDialog : Form
{
	private Button btnOK;

	private IContainer components;

	private HtmlViewer htmlViewer1;

	private Panel panel1;

	public string ReportXml
	{
		get;
		private set;
	}

	public ErrorReportDetailsDialog(BaseFeedback errorReport)
	{
		this.ReportXml = errorReport.Xml;
	}

	public ErrorReportDetailsDialog()
	{
		this.InitializeComponent();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		base.Close();
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
		this.btnOK = new Button();
		this.panel1 = new Panel();
		this.htmlViewer1 = new HtmlViewer();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		this.btnOK.DialogResult = DialogResult.OK;
		this.btnOK.Location = new Point(505, 438);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new Size(75, 23);
		this.btnOK.TabIndex = 5;
		this.btnOK.Text = "&OK";
		this.btnOK.add_Click(new EventHandler(this.btnOK_Click));
		this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		this.panel1.BorderStyle = BorderStyle.Fixed3D;
		this.panel1.Controls.Add(this.htmlViewer1);
		this.panel1.Location = new Point(12, 12);
		this.panel1.Name = "panel1";
		this.panel1.Size = new Size(568, 420);
		this.panel1.TabIndex = 7;
		this.htmlViewer1.AllowNavigation = false;
		this.htmlViewer1.AllowWebBrowserDrop = false;
		this.htmlViewer1.Dock = DockStyle.Fill;
		this.htmlViewer1.IsWebBrowserContextMenuEnabled = false;
		this.htmlViewer1.Location = new Point(0, 0);
		this.htmlViewer1.MinimumSize = new Size(20, 20);
		this.htmlViewer1.Name = "htmlViewer1";
		this.htmlViewer1.Size = new Size(564, 416);
		this.htmlViewer1.TabIndex = 0;
		this.htmlViewer1.WebBrowserShortcutsEnabled = false;
		base.AcceptButton = this.btnOK;
		base.AutoScaleDimensions = new SizeF(6, 13);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.CancelButton = this.btnOK;
		base.ClientSize = new Size(592, 473);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.btnOK);
		base.MinimumSize = new Size(500, 400);
		base.Name = "ErrorReportDetailsDialog";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.SizeGripStyle = SizeGripStyle.Show;
		base.StartPosition = FormStartPosition.CenterScreen;
		base.Text = "Error-Report Details";
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		this.htmlViewer1.SetXml(this.ReportXml);
	}

	public static DialogResult ShowErrorReportDetails(BaseFeedback feedback)
	{
		return ErrorReportDetailsDialog.ShowErrorReportDetails(feedback, null);
	}

	public static DialogResult ShowErrorReportDetails(BaseFeedback feedback, IWin32Window owner)
	{
		ErrorReportDetailsDialog dialog = new ErrorReportDetailsDialog(feedback);
		DialogResult result = dialog.ShowDialog(owner);
		return result;
	}
}
}