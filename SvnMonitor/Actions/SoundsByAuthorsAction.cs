using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;

using SVNMonitor.Design;
using SVNMonitor.Entities;
using SVNMonitor.Helpers;
using SVNMonitor.Resources;

namespace SVNMonitor.Actions
{
	[Serializable, ResourceProvider("Play a sound by the author (experimental)")]
	internal class SoundsByAuthorsAction : Action
	{
		[NonSerialized]
		private List<SoundByAuthor> rejectionSounds = new List<SoundByAuthor>();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<SoundByAuthor> sounds = new List<SoundByAuthor>();

		public override void RejectChanges()
		{
			foreach (SoundByAuthor sound in rejectionSounds)
			{
				sound.RejectChanges();
			}
			sounds = rejectionSounds;
		}

		protected override void Run(List<SVNLogEntry> logEntries, List<SVNPath> paths)
		{
			SVNLogEntry lastLogEntry = logEntries[logEntries.Count - 1];
			foreach (SoundByAuthor soundByAuthor in sounds)
			{
				if (soundByAuthor.Author.Equals(lastLogEntry.Author, StringComparison.InvariantCultureIgnoreCase))
				{
					SoundPlayerHelper.Play(soundByAuthor.SoundFile);
				}
			}
		}

		public override void SetRejectionPoint()
		{
			foreach (SoundByAuthor sound in sounds)
			{
				sound.SetRejectionPoint();
			}
			rejectionSounds = new List<SoundByAuthor>(sounds);
		}

		public override bool CanBeTested
		{
			get { return false; }
		}

		public override bool IsValid
		{
			get { return true; }
		}

		[Editor(typeof(SoundsByAuthorsCollectionEditor), typeof(UITypeEditor)), Category("Sound Player"), Browsable(true), Description("The list of sounds to be played.")]
		public List<SoundByAuthor> Sounds
		{
			get { return sounds; }
		}

		public override string SummaryInfo
		{
			get { return "Play sound files according to authors."; }
		}
	}
}