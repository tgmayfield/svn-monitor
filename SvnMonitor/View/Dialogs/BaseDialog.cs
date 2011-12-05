using System.Windows.Forms;

namespace SVNMonitor.View.Dialogs
{
public class BaseDialog : Form
{
	public BaseDialog()
	{
		base.MinimizeBox = false;
		base.MaximizeBox = false;
	}
}
}