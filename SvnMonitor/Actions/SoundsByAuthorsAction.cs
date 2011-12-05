using SVNMonitor.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using SVNMonitor.Entities;
using SVNMonitor.Helpers;

namespace SVNMonitor.Actions
{
[ResourceProvider("Play a sound by the author (experimental)")]
[Serializable]
internal class SoundsByAuthorsAction : Action
{
	[NonSerialized]
	private List<SoundByAuthor> rejectionSounds;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private List<SoundByAuthor> sounds;

	public bool CanBeTested
	{
		get
		{
			return false;
		}
	}

	public bool IsValid
	{
		get
		{
			return true;
		}
	}

	[Editor(typeof(SoundsByAuthorsCollectionEditor), typeof(UITypeEditor))]
	[Category("Sound Player")]
	[Browsable(true)]
	[Description("The list of sounds to be played.")]
	public List<SoundByAuthor> Sounds
	{
		get
		{
			return this.sounds;
		}
	}

	public string SummaryInfo
	{
		get
		{
			return "Play sound files according to authors.";
		}
	}

	public SoundsByAuthorsAction()
	{
		this.rejectionSounds = new List<SoundByAuthor>();
		this.sounds = new List<SoundByAuthor>();
	}

	public override void RejectChanges()
	{
		foreach (SoundByAuthor sound in this.rejectionSounds)
		{
			sound.RejectChanges();
		}
		this.sounds = this.rejectionSounds;
	}

	protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
	{
		SVNLogEntry lastLogEntry = logEntries[logEntries.Count - 1];
		foreach (SoundByAuthor soundByAuthor in this.sounds)
		{
			if (soundByAuthor.Author.Equals(lastLogEntry.Author, StringComparison.InvariantCultureIgnoreCase))
			{
				SoundPlayerHelper.Play(soundByAuthor.SoundFile);
			}
		}
	}

	public override void SetRejectionPoint()
	{
		foreach (SoundByAuthor sound in this.sounds)
		{
			sound.SetRejectionPoint();
		}
		this.rejectionSounds = new List<SoundByAuthor>(this.sounds);
	}
}
}