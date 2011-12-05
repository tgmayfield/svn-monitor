using System;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;

using SVNMonitor.Logging;

namespace SVNMonitor.Helpers
{
	internal class SoundPlayerHelper
	{
		private static void Close()
		{
			string command = "close MediaFile";
			mciSendString(command, null, 0, IntPtr.Zero);
		}

		private static void GetLength()
		{
			string command = "Set MediaFile time format milliseconds";
			mciSendString(command, null, 0, IntPtr.Zero);
			command = "Status MediaFile length";
			StringBuilder retStr = new StringBuilder(0x100);
			mciSendString(command, retStr, retStr.Length, IntPtr.Zero);
		}

		[DllImport("winmm.dll")]
		private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

		private static void Open(string fileName)
		{
			mciSendString("open \"" + fileName + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
		}

		private static void Play(bool loop)
		{
			string command = "play MediaFile";
			if (loop)
			{
				command = command + " REPEAT";
			}
			mciSendString(command, null, 0, IntPtr.Zero);
		}

		public static void Play(string soundFile)
		{
			try
			{
				soundFile = soundFile.Trim();
				if (soundFile.EndsWith(".wav", StringComparison.InvariantCultureIgnoreCase))
				{
					PlayWav(soundFile);
				}
				else
				{
					Close();
					Open(soundFile);
					GetLength();
					Play(false);
				}
			}
			catch (Exception ex)
			{
				Close();
				Logger.Log.Error(string.Format("Error playing '{0}'", soundFile), ex);
			}
		}

		private static void PlayWav(string soundFile)
		{
			new SoundPlayer(soundFile).Play();
		}
	}
}