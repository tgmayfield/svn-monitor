using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace SVNMonitor.Actions
{
	[Serializable, DisplayName("Sound By Author")]
	internal class SoundByAuthor
	{
		private string author;
		[NonSerialized]
		private string rejectionAuthor;
		[NonSerialized]
		private string rejectionSoundFile;
		private string soundFile;

		public virtual void RejectChanges()
		{
			Author = rejectionAuthor;
			SoundFile = rejectionSoundFile;
		}

		public virtual void SetRejectionPoint()
		{
			rejectionAuthor = Author;
			rejectionSoundFile = SoundFile;
		}

		public override string ToString()
		{
			return string.Format("Sound for {0}", Author);
		}

		[Description("The author's name."), Category("Sound")]
		public string Author
		{
			get
			{
				if (string.IsNullOrEmpty(author))
				{
					return "Author's Name";
				}
				return author;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Author's name cannot be empty.");
				}
				author = value;
			}
		}

		[Category("Sound"), Editor(typeof(FileNameEditor), typeof(UITypeEditor)), DisplayName("Sound File"), Description("The sound file name to be played.")]
		public string SoundFile
		{
			get
			{
				if (string.IsNullOrEmpty(soundFile))
				{
					return "[Browse for a sound file]";
				}
				return soundFile;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Sound file cannot be empty.");
				}
				soundFile = value;
			}
		}
	}
}