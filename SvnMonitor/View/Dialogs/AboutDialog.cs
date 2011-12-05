using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Dialogs
{
	public class AboutDialog : BaseDialog
	{
		private Button btnDonate;
		private IContainer components;
		private const int CS_DROPSHADOW = 0x20000;
		private GroupBox groupBox1;
		private Label lblAuthor;
		private Label lblNameAndVersion;
		private LinkLabel linkBlog;
		private LinkLabel linkEmail;
		private LinkLabel linkHome;
		private PictureBox pictureBox1;

		public AboutDialog()
		{
			InitializeComponent();
			SetVersion();
		}

		private void AboutDialog_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			base.Close();
		}

		private void AboutDialog_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				base.Close();
			}
		}

		private void btnDonate_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Web.SharpRegion.BrowseDonate();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(AboutDialog));
			linkHome = new LinkLabel();
			linkBlog = new LinkLabel();
			lblNameAndVersion = new Label();
			lblAuthor = new Label();
			linkEmail = new LinkLabel();
			pictureBox1 = new PictureBox();
			groupBox1 = new GroupBox();
			btnDonate = new Button();
			((ISupportInitialize)pictureBox1).BeginInit();
			groupBox1.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(linkHome, "linkHome");
			linkHome.BackColor = SystemColors.Control;
			linkHome.ForeColor = SystemColors.ControlText;
			linkHome.LinkColor = Color.Blue;
			linkHome.Name = "linkHome";
			linkHome.TabStop = true;
			linkHome.LinkClicked += linkHome_LinkClicked;
			resources.ApplyResources(linkBlog, "linkBlog");
			linkBlog.BackColor = SystemColors.Control;
			linkBlog.ForeColor = SystemColors.ControlText;
			linkBlog.LinkColor = Color.Blue;
			linkBlog.Name = "linkBlog";
			linkBlog.TabStop = true;
			linkBlog.LinkClicked += linkBlog_LinkClicked;
			resources.ApplyResources(lblNameAndVersion, "lblNameAndVersion");
			lblNameAndVersion.BackColor = Color.Transparent;
			lblNameAndVersion.ForeColor = Color.Black;
			lblNameAndVersion.Name = "lblNameAndVersion";
			resources.ApplyResources(lblAuthor, "lblAuthor");
			lblAuthor.BackColor = Color.Transparent;
			lblAuthor.ForeColor = Color.Black;
			lblAuthor.Name = "lblAuthor";
			resources.ApplyResources(linkEmail, "linkEmail");
			linkEmail.BackColor = SystemColors.Control;
			linkEmail.ForeColor = SystemColors.ControlText;
			linkEmail.LinkColor = Color.Blue;
			linkEmail.Name = "linkEmail";
			linkEmail.TabStop = true;
			linkEmail.LinkClicked += linkEmail_LinkClicked;
			resources.ApplyResources(pictureBox1, "pictureBox1");
			pictureBox1.Name = "pictureBox1";
			pictureBox1.TabStop = false;
			groupBox1.Controls.Add(btnDonate);
			groupBox1.Controls.Add(pictureBox1);
			groupBox1.Controls.Add(linkEmail);
			groupBox1.Controls.Add(lblNameAndVersion);
			groupBox1.Controls.Add(linkHome);
			groupBox1.Controls.Add(lblAuthor);
			groupBox1.Controls.Add(linkBlog);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			resources.ApplyResources(btnDonate, "btnDonate");
			btnDonate.Name = "btnDonate";
			btnDonate.UseVisualStyleBackColor = true;
			btnDonate.Click += btnDonate_Click;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Dpi;
			BackColor = SystemColors.Control;
			base.Controls.Add(groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.Name = "AboutDialog";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.Click += AboutDialog_Click;
			base.KeyDown += AboutDialog_KeyDown;
			((ISupportInitialize)pictureBox1).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		private void linkBlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			Web.SharpRegion.BrowseBlog();
		}

		private void linkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			Web.SharpRegion.BrowseSendEmail();
		}

		private void linkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			Web.SharpRegion.BrowseHome();
		}

		private void SetVersion()
		{
			string version = typeof(AboutDialog).Assembly.GetName().Version.ToString();
			lblNameAndVersion.Text = string.Format("{0} v.{1}", Strings.SVNMonitorCaption, version);
		}

		internal static void ShowStaticDialog(IWin32Window owner)
		{
			new AboutDialog().ShowDialog(owner);
		}
	}
}