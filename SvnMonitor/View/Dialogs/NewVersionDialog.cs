using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Support;
using SVNMonitor.View.Controls;

namespace SVNMonitor.View.Dialogs
{
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
			InitializeComponent();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Web.SharpRegion.BrowseDownloadPage();
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
			Upgrade();
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
			lblTitle = new Label();
			lblYourVersionTitle = new Label();
			lblNewVersionTitle = new Label();
			lblYourVersion = new Label();
			lblNewVersion = new Label();
			btnBrowse = new Button();
			btnClose = new Button();
			pictureBox1 = new PictureBox();
			btnUpgrade = new Button();
			lblUpgradeTakesSeconds = new Label();
			panel1 = new Panel();
			htmlViewer1 = new HtmlViewer();
			((ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(lblTitle, "lblTitle");
			lblTitle.Name = "lblTitle";
			resources.ApplyResources(lblYourVersionTitle, "lblYourVersionTitle");
			lblYourVersionTitle.Name = "lblYourVersionTitle";
			resources.ApplyResources(lblNewVersionTitle, "lblNewVersionTitle");
			lblNewVersionTitle.Name = "lblNewVersionTitle";
			resources.ApplyResources(lblYourVersion, "lblYourVersion");
			lblYourVersion.Name = "lblYourVersion";
			resources.ApplyResources(lblNewVersion, "lblNewVersion");
			lblNewVersion.Name = "lblNewVersion";
			resources.ApplyResources(btnBrowse, "btnBrowse");
			btnBrowse.DialogResult = DialogResult.OK;
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Click += btnBrowse_Click;
			resources.ApplyResources(btnClose, "btnClose");
			btnClose.DialogResult = DialogResult.Cancel;
			btnClose.Name = "btnClose";
			btnClose.Click += btnClose_Click;
			resources.ApplyResources(pictureBox1, "pictureBox1");
			pictureBox1.Name = "pictureBox1";
			pictureBox1.TabStop = false;
			resources.ApplyResources(btnUpgrade, "btnUpgrade");
			btnUpgrade.DialogResult = DialogResult.OK;
			btnUpgrade.Name = "btnUpgrade";
			btnUpgrade.Click += btnUpgrade_Click;
			resources.ApplyResources(lblUpgradeTakesSeconds, "lblUpgradeTakesSeconds");
			lblUpgradeTakesSeconds.ForeColor = Color.Black;
			lblUpgradeTakesSeconds.Name = "lblUpgradeTakesSeconds";
			resources.ApplyResources(panel1, "panel1");
			panel1.BorderStyle = BorderStyle.Fixed3D;
			panel1.Controls.Add(htmlViewer1);
			panel1.Name = "panel1";
			htmlViewer1.AllowNavigation = false;
			htmlViewer1.AllowWebBrowserDrop = false;
			resources.ApplyResources(htmlViewer1, "htmlViewer1");
			htmlViewer1.IsWebBrowserContextMenuEnabled = false;
			htmlViewer1.MinimumSize = new Size(20, 20);
			htmlViewer1.Name = "htmlViewer1";
			htmlViewer1.WebBrowserShortcutsEnabled = false;
			base.AcceptButton = btnUpgrade;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnClose;
			base.Controls.Add(panel1);
			base.Controls.Add(lblUpgradeTakesSeconds);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(btnClose);
			base.Controls.Add(btnUpgrade);
			base.Controls.Add(btnBrowse);
			base.Controls.Add(lblNewVersionTitle);
			base.Controls.Add(lblNewVersion);
			base.Controls.Add(lblYourVersion);
			base.Controls.Add(lblYourVersionTitle);
			base.Controls.Add(lblTitle);
			base.MaximizeBox = true;
			base.Name = "NewVersionDialog";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Show;
			((ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public void SetMessage(string message)
		{
			htmlViewer1.SetHtml(message);
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
			UpgradeInfo.Upgrade(UpgradePath, NewVersion.ToString());
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
			get { return currentVersion; }
			private set
			{
				currentVersion = value;
				lblYourVersion.Text = currentVersion.ToString();
			}
		}

		public Version NewVersion
		{
			[DebuggerNonUserCode]
			get { return newVersion; }
			private set
			{
				newVersion = value;
				lblNewVersion.Text = newVersion.ToString();
			}
		}

		public string UpgradePath
		{
			[DebuggerNonUserCode]
			get { return upgradePath; }
			private set
			{
				upgradePath = value;
				lblUpgradeTakesSeconds.Visible = btnUpgrade.Visible = upgradePath != null;
			}
		}
	}
}