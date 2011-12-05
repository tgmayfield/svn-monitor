using System;
using System.Text;
using System.Runtime.InteropServices;
using SVNMonitor.Logging;
using System.Media;

namespace SVNMonitor.Helpers
{
internal class SoundPlayerHelper
{
	public SoundPlayerHelper()
	{
	}

	private static void Close()
	{
		string command = "close MediaFile";
		SoundPlayerHelper.mciSendString(command, null, 0, IntPtr.Zero);
	}

	private static void GetLength()
	{
		string command = "Set MediaFile time format milliseconds";
		SoundPlayerHelper.mciSendString(command, null, 0, IntPtr.Zero);
		command = "Status MediaFile length";
		StringBuilder retStr = new StringBuilder(256);
		SoundPlayerHelper.mciSendString(command, retStr, retStr.Length, IntPtr.Zero);
	}

	[DllImport("winmm.dll", CharSet=CharSet.None)]
	private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

	private static void Open(string fileName)
	{
		string command = string.Concat("open \"", fileName, "\" type mpegvideo alias MediaFile");
		SoundPlayerHelper.mciSendString(command, null, 0, IntPtr.Zero);
	}

	public static void Play(string soundFile)
	{
		try
		{
			soundFile = soundFile.Trim();
			if (soundFile.EndsWith(".wav", StringComparison.InvariantCultureIgnoreCase))
			{
				SoundPlayerHelper.PlayWav(soundFile);
			}
			else
			{
				SoundPlayerHelper.Close();
				SoundPlayerHelper.Open(soundFile);
				SoundPlayerHelper.GetLength();
				SoundPlayerHelper.Play(false);
			}
		}
		catch (Exception ex)
		{
			SoundPlayerHelper.Close();
			Logger.Log.Error(string.Format("Error playing '{0}'", soundFile), ex);
		}
	}

	private static void Play(bool loop)
	{
		string command = "play MediaFile";
		if (loop)
		{
			command = string.Concat(command, " REPEAT");
		}
		SoundPlayerHelper.mciSendString(command, null, 0, IntPtr.Zero);
	}

	private static void PlayWav(string soundFile)
	{
		SoundPlayer player = new SoundPlayer(soundFile);
		player.Play();
	}
}
}