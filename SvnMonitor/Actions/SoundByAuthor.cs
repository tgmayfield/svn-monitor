using System.ComponentModel;
using System;

namespace SVNMonitor.Actions
{
[DisplayName("Sound By Author")]
[Serializable]
internal class SoundByAuthor
{
	private string author;

	[NonSerialized]
	private string rejectionAuthor;

	[NonSerialized]
	private string rejectionSoundFile;

	private string soundFile;

	[Description("The author's name.")]
	[Category("Sound")]
	public string Author
	{
		get
		{
			if (string.IsNullOrEmpty(this.author))
			{
				return "Author's Name";
			}
			return this.author;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("Author's name cannot be empty.");
			}
			this.author = value;
		}
	}

	[Category("Sound")]
	[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
	[DisplayName("Sound File")]
	[Description("The sound file name to be played.")]
	public string SoundFile
	{
		get
		{
			if (string.IsNullOrEmpty(this.soundFile))
			{
				return "[Browse for a sound file]";
			}
			return this.soundFile;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("Sound file cannot be empty.");
			}
			this.soundFile = value;
		}
	}

	public SoundByAuthor()
	{
	}

	public virtual void RejectChanges()
	{
		this.Author = this.rejectionAuthor;
		this.SoundFile = this.rejectionSoundFile;
	}

	public virtual void SetRejectionPoint()
	{
		this.rejectionAuthor = this.Author;
		this.rejectionSoundFile = this.SoundFile;
	}

	public override string ToString()
	{
		return string.Format("Sound for {0}", this.Author);
	}
}
}