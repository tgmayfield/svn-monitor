using System.Windows.Forms;
using System.ComponentModel;
using System;
using SVNMonitor.Logging;
using SVNMonitor.Web;
using System.Drawing;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Dialogs
{
public class AboutDialog : BaseDialog
{
	private Button btnDonate;

	private IContainer components;

	private const int CS_DROPSHADOW = 131072;

	private GroupBox groupBox1;

	private Label lblAuthor;

	private Label lblNameAndVersion;

	private LinkLabel linkBlog;

	private LinkLabel linkEmail;

	private LinkLabel linkHome;

	private PictureBox pictureBox1;

	public AboutDialog()
	{
		this.InitializeComponent();
		this.SetVersion();
	}

	private void AboutDialog_Click(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		base.Close();
	}

	private void AboutDialog_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == 27)
		{
			base.Close();
		}
	}

	private void btnDonate_Click(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		SharpRegion.BrowseDonate();
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
		ComponentResourceManager resources = new ComponentResourceManager(typeof(AboutDialog));
		this.linkHome = new LinkLabel();
		this.linkBlog = new LinkLabel();
		this.lblNameAndVersion = new Label();
		this.lblAuthor = new Label();
		this.linkEmail = new LinkLabel();
		this.pictureBox1 = new PictureBox();
		this.groupBox1 = new GroupBox();
		this.btnDonate = new Button();
		this.pictureBox1.BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.linkHome, "linkHome");
		this.linkHome.BackColor = SystemColors.Control;
		this.linkHome.ForeColor = SystemColors.ControlText;
		this.linkHome.LinkColor = Color.Blue;
		this.linkHome.Name = "linkHome";
		this.linkHome.TabStop = true;
		this.linkHome.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkHome_LinkClicked);
		resources.ApplyResources(this.linkBlog, "linkBlog");
		this.linkBlog.BackColor = SystemColors.Control;
		this.linkBlog.ForeColor = SystemColors.ControlText;
		this.linkBlog.LinkColor = Color.Blue;
		this.linkBlog.Name = "linkBlog";
		this.linkBlog.TabStop = true;
		this.linkBlog.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkBlog_LinkClicked);
		resources.ApplyResources(this.lblNameAndVersion, "lblNameAndVersion");
		this.lblNameAndVersion.BackColor = Color.Transparent;
		this.lblNameAndVersion.ForeColor = Color.Black;
		this.lblNameAndVersion.Name = "lblNameAndVersion";
		resources.ApplyResources(this.lblAuthor, "lblAuthor");
		this.lblAuthor.BackColor = Color.Transparent;
		this.lblAuthor.ForeColor = Color.Black;
		this.lblAuthor.Name = "lblAuthor";
		resources.ApplyResources(this.linkEmail, "linkEmail");
		this.linkEmail.BackColor = SystemColors.Control;
		this.linkEmail.ForeColor = SystemColors.ControlText;
		this.linkEmail.LinkColor = Color.Blue;
		this.linkEmail.Name = "linkEmail";
		this.linkEmail.TabStop = true;
		this.linkEmail.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkEmail_LinkClicked);
		resources.ApplyResources(this.pictureBox1, "pictureBox1");
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.TabStop = false;
		this.groupBox1.Controls.Add(this.btnDonate);
		this.groupBox1.Controls.Add(this.pictureBox1);
		this.groupBox1.Controls.Add(this.linkEmail);
		this.groupBox1.Controls.Add(this.lblNameAndVersion);
		this.groupBox1.Controls.Add(this.linkHome);
		this.groupBox1.Controls.Add(this.lblAuthor);
		this.groupBox1.Controls.Add(this.linkBlog);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.btnDonate, "btnDonate");
		this.btnDonate.Name = "btnDonate";
		this.btnDonate.UseVisualStyleBackColor = true;
		this.btnDonate.add_Click(new EventHandler(this.btnDonate_Click));
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = AutoScaleMode.Dpi;
		base.BackColor = SystemColors.Control;
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.Name = "AboutDialog";
		base.ShowInTaskbar = false;
		base.SizeGripStyle = SizeGripStyle.Hide;
		base.Click += new EventHandler(this.AboutDialog_Click);
		base.KeyDown += new KeyEventHandler(this.AboutDialog_KeyDown);
		this.pictureBox1.EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void linkBlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Logger.LogUserAction();
		SharpRegion.BrowseBlog();
	}

	private void linkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Logger.LogUserAction();
		SharpRegion.BrowseSendEmail();
	}

	private void linkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Logger.LogUserAction();
		SharpRegion.BrowseHome();
	}

	private void SetVersion()
	{
		string version = typeof(AboutDialog).Assembly.GetName().Version.ToString();
		this.lblNameAndVersion.Text = string.Format("{0} v.{1}", Strings.SVNMonitorCaption, version);
	}

	internal static void ShowStaticDialog(IWin32Window owner)
	{
		AboutDialog dialog = new AboutDialog();
		dialog.ShowDialog(owner);
	}
}
}