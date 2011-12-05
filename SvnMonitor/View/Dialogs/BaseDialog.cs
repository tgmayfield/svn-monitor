namespace SVNMonitor.View.Dialogs
{
    using System;
    using System.Windows.Forms;

    public class BaseDialog : Form
    {
        public BaseDialog()
        {
            base.MinimizeBox = false;
            base.MaximizeBox = false;
        }
    }
}

