using SVNMonitor.Resources;
using System;
using System.ComponentModel;
using System.IO;

namespace SVNMonitor.Actions
{
[ResourceProvider("Write information to a text file")]
[Serializable]
internal class FileAppenderAction : TextAppenderAction
{
	[NonSerialized]
	private string rejectionFileName;

	[Description("Name of the file to append the text into.")]
	[DisplayName("File Name")]
	[Category("File Appender")]
	[Editor(typeof(OptionalFileNameEditor), typeof(UITypeEditor))]
	public string FileName
	{
		get;
		set;
	}

	public bool IsValid
	{
		get
		{
			if (string.IsNullOrEmpty(this.FileName))
			{
				return false;
			}
			return true;
		}
	}

	public string SummaryInfo
	{
		get
		{
			return string.Format("Append log information to: {0}.", this.FileName);
		}
	}

	public FileAppenderAction()
	{
	}

	protected override void AppendText(string text)
	{
		File.AppendAllText(this.FileName, text);
	}

	public override void RejectChanges()
	{
		this.FileName = this.rejectionFileName;
	}

	public override void SetRejectionPoint()
	{
		this.rejectionFileName = this.FileName;
	}
}
}