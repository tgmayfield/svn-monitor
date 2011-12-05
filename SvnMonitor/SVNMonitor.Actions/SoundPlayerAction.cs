using SVNMonitor.Resources;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using SVNMonitor.Helpers;

namespace SVNMonitor.Actions
{
[ResourceProvider("Play a sound (experimental)")]
[Serializable]
internal class SoundPlayerAction : Action
{
	[NonSerialized]
	private string rejectionFileName;

	public bool IsValid
	{
		get
		{
			if (string.IsNullOrEmpty(this.MediaFileName))
			{
				return false;
			}
			return true;
		}
	}

	[Description("The sound file name to be played.")]
	[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
	[Category("Sound Player")]
	[DisplayName("Sound File")]
	public string MediaFileName
	{
		get;
		set;
	}

	public string SummaryInfo
	{
		get
		{
			return string.Format("Play a sound file:{0}{1}.", Environment.NewLine, this.MediaFileName);
		}
	}

	public SoundPlayerAction()
	{
	}

	public override void RejectChanges()
	{
		this.MediaFileName = this.rejectionFileName;
	}

	protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		SoundPlayerHelper.Play(this.MediaFileName);
	}

	public override void SetRejectionPoint()
	{
		this.rejectionFileName = this.MediaFileName;
	}
}
}