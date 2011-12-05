namespace SVNMonitor.Design
{
    using SVNMonitor.Logging;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public class OptionalFileNameEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((provider != null) && (provider.GetService(typeof(IWindowsFormsEditorService)) != null))
            {
                OpenFileDialog tempLocal0 = new OpenFileDialog {
                    CheckFileExists = false
                };
                OpenFileDialog dialog = tempLocal0;
                if (value is string)
                {
                    dialog.FileName = (string) value;
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Logger.Log.DebugFormat("User selected file: {0}", dialog.FileName);
                    value = dialog.FileName;
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}

