namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Logging;
    using SVNMonitor.Support;
    using SVNMonitor.View.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ErrorReportDetailsDialog : Form
    {
        private Button btnOK;
        private IContainer components;
        private HtmlViewer htmlViewer1;
        private Panel panel1;

        public ErrorReportDetailsDialog()
        {
            this.InitializeComponent();
        }

        public ErrorReportDetailsDialog(BaseFeedback errorReport) : this()
        {
            this.ReportXml = errorReport.Xml;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            base.Close();
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
            this.btnOK = new Button();
            this.panel1 = new Panel();
            this.htmlViewer1 = new HtmlViewer();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x1f9, 0x1b6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.panel1.BorderStyle = BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.htmlViewer1);
            this.panel1.Location = new Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x238, 420);
            this.panel1.TabIndex = 7;
            this.htmlViewer1.AllowNavigation = false;
            this.htmlViewer1.AllowWebBrowserDrop = false;
            this.htmlViewer1.Dock = DockStyle.Fill;
            this.htmlViewer1.IsWebBrowserContextMenuEnabled = false;
            this.htmlViewer1.Location = new Point(0, 0);
            this.htmlViewer1.MinimumSize = new Size(20, 20);
            this.htmlViewer1.Name = "htmlViewer1";
            this.htmlViewer1.Size = new Size(0x234, 0x1a0);
            this.htmlViewer1.TabIndex = 0;
            this.htmlViewer1.WebBrowserShortcutsEnabled = false;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnOK;
            base.ClientSize = new Size(0x250, 0x1d9);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnOK);
            this.MinimumSize = new Size(500, 400);
            base.Name = "ErrorReportDetailsDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Show;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Error-Report Details";
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

