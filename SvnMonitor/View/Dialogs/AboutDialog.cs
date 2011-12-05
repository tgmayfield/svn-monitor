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
		private IContainer components;
		private const int CS_DROPSHADOW = 0x20000;
		private GroupBox groupBox1;
		private Label lblAuthor;
		private Label lblNameAndVersion;
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
			lblNameAndVersion = new Label();
			lblAuthor = new Label();
			pictureBox1 = new PictureBox();
			groupBox1 = new GroupBox();
			((ISupportInitialize)pictureBox1).BeginInit();
			groupBox1.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(lblNameAndVersion, "lblNameAndVersion");
			lblNameAndVersion.BackColor = Color.Transparent;
			lblNameAndVersion.ForeColor = Color.Black;
			lblNameAndVersion.Name = "lblNameAndVersion";
			resources.ApplyResources(lblAuthor, "lblAuthor");
			lblAuthor.BackColor = Color.Transparent;
			lblAuthor.ForeColor = Color.Black;
			lblAuthor.Name = "lblAuthor";
			resources.ApplyResources(pictureBox1, "pictureBox1");
			pictureBox1.Name = "pictureBox1";
			pictureBox1.TabStop = false;
			groupBox1.Controls.Add(pictureBox1);
			groupBox1.Controls.Add(lblNameAndVersion);
			groupBox1.Controls.Add(lblAuthor);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
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