using System;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Dialogs
{
	public partial class AboutDialog : BaseDialog
	{
		private const int CS_DROPSHADOW = 0x20000;
		
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