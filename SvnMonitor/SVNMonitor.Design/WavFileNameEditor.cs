using System;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using SVNMonitor.Logging;

namespace SVNMonitor.Design
{
[Obsolete]
public class WavFileNameEditor : UITypeEditor
{
	public WavFileNameEditor()
	{
	}

	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		if (provider != null && provider.GetService(typeof(IWindowsFormsEditorService)) != null)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if (value as string)
			{
				dialog.FileName = (string)value;
			}
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				Logger.Log.DebugFormat("User selected file: {0}", dialog.FileName);
				value = dialog.FileName;
			}
		}
		return value;
	}

	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return 2;
	}
}
}