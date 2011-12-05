namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Logging;
    using SVNMonitor.Support;
    using SVNMonitor.View;
    using SVNMonitor.View.Controls;
    using SVNMonitor.Web;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class NewVersionDialog : BaseDialog
    {
        private Button btnBrowse;
        private Button btnClose;
        private Button btnUpgrade;
        private IContainer components;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Version currentVersion;
        private static NewVersionDialog dialog;
        private HtmlViewer htmlViewer1;
        private Label lblNewVersion;
        private Label lblNewVersionTitle;
        private Label lblTitle;
        private Label lblUpgradeTakesSeconds;
        private Label lblYourVersion;
        private Label lblYourVersionTitle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Version newVersion;
        private Panel panel1;
        private PictureBox pictureBox1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string upgradePath;

        public NewVersionDialog()
        {
            this.InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            SharpRegion.BrowseDownloadPage();
            base.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            base.Close();
        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.Upgrade();
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

        private static string GetCompatibleHtml(string body)
        {
            body = body.Replace(Environment.NewLine, "<br>");
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<style type=\"text/css\">body { font-family: Verdana, sans-serif; font-size: x-small; }</style>");
            sb.Append("<body>");
            sb.Append(body);
            sb.Append("</body></html>");
            return sb.ToString();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(NewVersionDialog));
            this.lblTitle = new Label();
            this.lblYourVersionTitle = new Label();
            this.lblNewVersionTitle = new Label();
            this.lblYourVersion = new Label();
            this.lblNewVersion = new Label();
            this.btnBrowse = new Button();
            this.btnClose = new Button();
            this.pictureBox1 = new PictureBox();
            this.btnUpgrade = new Button();
            this.lblUpgradeTakesSeconds = new Label();
            this.panel1 = new Panel();
            this.htmlViewer1 = new HtmlViewer();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            resources.ApplyResources(this.lblYourVersionTitle, "lblYourVersionTitle");
            this.lblYourVersionTitle.Name = "lblYourVersionTitle";
            resources.ApplyResources(this.lblNewVersionTitle, "lblNewVersionTitle");
            this.lblNewVersionTitle.Name = "lblNewVersionTitle";
            resources.ApplyResources(this.lblYourVersion, "lblYourVersion");
            this.lblYourVersion.Name = "lblYourVersion";
            resources.ApplyResources(this.lblNewVersion, "lblNewVersion");
            this.lblNewVersion.Name = "lblNewVersion";
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.DialogResult = DialogResult.OK;
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            resources.ApplyResources(this.btnUpgrade, "btnUpgrade");
            this.btnUpgrade.DialogResult = DialogResult.OK;
            this.btnUpgrade.Name = "btnUpgrade";
            this.btnUpgrade.Click += new EventHandler(this.btnUpgrade_Click);
            resources.ApplyResources(this.lblUpgradeTakesSeconds, "lblUpgradeTakesSeconds");
            this.lblUpgradeTakesSeconds.ForeColor = Color.Black;
            this.lblUpgradeTakesSeconds.Name = "lblUpgradeTakesSeconds";
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BorderStyle = BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.htmlViewer1);
            this.panel1.Name = "panel1";
            this.htmlViewer1.AllowNavigation = false;
            this.htmlViewer1.AllowWebBrowserDrop = false;
            resources.ApplyResources(this.htmlViewer1, "htmlViewer1");
            this.htmlViewer1.IsWebBrowserContextMenuEnabled = false;
            this.htmlViewer1.MinimumSize = new Size(20, 20);
            this.htmlViewer1.Name = "htmlViewer1";
            this.htmlViewer1.WebBrowserShortcutsEnabled = false;
            base.AcceptButton = this.btnUpgrade;
            resources.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnClose;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.lblUpgradeTakesSeconds);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnUpgrade);
            base.Controls.Add(this.btnBrowse);
            base.Controls.Add(this.lblNewVersionTitle);
            base.Controls.Add(this.lblNewVersion);
            base.Controls.Add(this.lblYourVersion);
            base.Controls.Add(this.lblYourVersionTitle);
            base.Controls.Add(this.lblTitle);
            base.MaximizeBox = true;
            base.Name = "NewVersionDialog";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = SizeGripStyle.Show;
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void SetMessage(string message)
        {
            this.htmlViewer1.SetHtml(message);
        }

        public static void ShowNewVersionDialog(Version currentVersion, Version newVersion, string message)
        {
            ShowNewVersionDialog(currentVersion, newVersion, message, null);
        }

        public static void ShowNewVersionDialog(Version currentVersion, Version newVersion, string message, string upgradePath)
        {
            if (dialog != null)
            {
                dialog.Close();
            }
            dialog = new NewVersionDialog();
            dialog.CurrentVersion = currentVersion;
            dialog.NewVersion = newVersion;
            dialog.UpgradePath = upgradePath;
            if (!message.ToLower().Contains("<html>"))
            {
                message = GetCompatibleHtml(message);
            }
            dialog.SetMessage(message);
            dialog.Show();
            dialog.BringToFront();
        }

        private void Upgrade()
        {
            UpgradeInfo.Upgrade(this.UpgradePath, this.NewVersion.ToString());
            if (MainForm.FormInstance != null)
            {
                MainForm.FormInstance.RealClose();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public Version CurrentVersion
        {
            [DebuggerNonUserCode]
            get
            {
                return this.currentVersion;
            }
            private set
            {
                this.currentVersion = value;
                this.lblYourVersion.Text = this.currentVersion.ToString();
            }
        }

        public Version NewVersion
        {
            [DebuggerNonUserCode]
            get
            {
                return this.newVersion;
            }
            private set
            {
                this.newVersion = value;
                this.lblNewVersion.Text = this.newVersion.ToString();
            }
        }

        public string UpgradePath
        {
            [DebuggerNonUserCode]
            get
            {
                return this.upgradePath;
            }
            private set
            {
                this.upgradePath = value;
                this.lblUpgradeTakesSeconds.Visible = this.btnUpgrade.Visible = this.upgradePath != null;
            }
        }
    }
}

